using CFCAIMCore.DataExchange;
using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Bruker;
using CFCAIMCore.Entity.CFCAPI;
using CFCAIMCore.Entity.PNE;
using CFCAIMCore.Manager;
using CFCAPI;
using CFCAPICore.Entity;
using CFCAPICore.Entity.Adisseo;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace CFCAIMWorker.Adisseo
{
	public class CFCAPIPne
	{
		#region Variables
		private static String _aimApiKey = String.Empty;
        private static String _aimInstrumentKey = String.Empty;
		private static String _pneApiKey = String.Empty;
		private static String _token = String.Empty;
        private static Int32 _customerId = 0;
        #endregion

        #region  Properties
        private static String ResolveAimApiKey()
        {
            if (String.IsNullOrWhiteSpace(_aimApiKey) == false)
            {
                return _aimApiKey;
            }

            _aimApiKey = CFCAPIManager.Setting.AIMAPIKey;
            return _aimApiKey;
        }

        public static String AimApiKey
		{
			get
			{
                return _aimApiKey;
			}

			set
			{
				_aimApiKey = value;
			}
		}

		public static String AimInstrumentKey
		{
			get
			{
				return _aimInstrumentKey;
			}

			set
			{
				_aimInstrumentKey = value;
			}
		}

        public static String PneApiKey
        {
            get
            {
                return _pneApiKey;
            }

            set
            {
                _pneApiKey = value;
            }
        }


        public static String Token
        {
            get
            {
                if (String.IsNullOrEmpty(_token) == false)
                {
                    return _token;
                }

                String aimApiKey = ResolveAimApiKey();
                if (String.IsNullOrWhiteSpace(aimApiKey) == true)
                {
                    return String.Empty;
                }

                APITokenInfo apiToken = CFCAPIManager.GetTokenV2(aimApiKey);
                if (apiToken == null)
                {
                    return String.Empty;
                }

                _token = apiToken.Token;
                return _token;
            }

            set
            {
                _token = value;
            }
        }

        public static Int32 CustomerId
        {
            get
            {
                return _customerId;
            }

            set
            {
                _customerId = value;
            }
        }	
        #endregion

        #region PNE Results Received Logging
        private static PNEResultsReceivedInfo BuildPNEResultsRecievedInfo(String apiName, String sampleCode, String message, Int32 analysisRequestId, Int32 brukerSpectrumID)
        {
            PNEResultsReceivedInfo pneResultsRecieved = new PNEResultsReceivedInfo();
            pneResultsRecieved.Token = Token;
			pneResultsRecieved.CustomerId = CustomerId;
            pneResultsRecieved.PNEKey = PneApiKey;
            pneResultsRecieved.PNEResultsRecievedID = 0;
            pneResultsRecieved.AnalysisRequestId = analysisRequestId;
            pneResultsRecieved.APIName = apiName;
            pneResultsRecieved.SampleCode = sampleCode;
            pneResultsRecieved.Message = message;
			pneResultsRecieved.ReceivedByEnum = (Int32)RecievedByEnum.CFCAIMWorker;
            pneResultsRecieved.BrukerSpectrumID = brukerSpectrumID;
            pneResultsRecieved.UpdateStamp = DateTime.Now;
            return pneResultsRecieved;
        }

        private static void LogPNEResultsRecieved(String apiName, String sampleCode, String message, Int32 analysisRequestId, Int32 brukerSpectrumID = 0)
        {
            if (String.IsNullOrWhiteSpace(Token) == true)
            {
                return;
            }

            PNEResultsReceivedInfo pneResultsRecieved = BuildPNEResultsRecievedInfo(apiName, sampleCode, message, analysisRequestId, brukerSpectrumID);
            CFCAPIManager.PutPNEResultsRecieved(pneResultsRecieved);
        }

        private static Int32 ResolveBrukerSpectrumID(Int32 sampleID)
        {
            Int32 brukerSpectrumID = 0;
            Int32 customerID = CustomerId;
            String token = Token;

            if (customerID <= 0)
            {
                APITokenInfo apiToken = CFCAPIManager.GetTokenV2(CFCAPIManager.Setting.AIMAPIKey);
                if (apiToken != null)
                {
                    customerID = apiToken.CustomerID;
                }
            }

            if (customerID <= 0 || String.IsNullOrWhiteSpace(token) == true)
            {
                return brukerSpectrumID;
            }

            List<BrukerSampleInfo> brukerSample = CFCAPIManager.GetBrukerSample(token, customerID, sampleID.ToString());
            if (brukerSample != null && brukerSample.Count > 0)
            {
                List<BrukerSpectrumInfo> brukerSpectrumFile = CFCAPIManager.GetBrukerSpectrumFileV2(token, brukerSample[0].BrukerSampleID);
                if (brukerSpectrumFile != null && brukerSpectrumFile.Count > 0)
                {
                    brukerSpectrumID = brukerSpectrumFile[0].BrukerSpectrumID;
                }
            }

            return brukerSpectrumID;
        }

        private static Int32 ParseAnalysisRequestId(String analysisRequestId)
        {
            Int32 parsedAnalysisRequestId = 0;
            if (Int32.TryParse(analysisRequestId, out parsedAnalysisRequestId) == false)
            {
                parsedAnalysisRequestId = 0;
            }
            return parsedAnalysisRequestId;
        }
        #endregion

        #region Maintenance
        public static void SendPendingSampleSpectra()
		{
			try
			{
				List<AnalyzerPendingSampleInfo> analyzerPendingSample = new List<AnalyzerPendingSampleInfo>();
				List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
				List<ImportConfigInfo> importConfig = new List<ImportConfigInfo>();
				List<String> analysesCodes = new List<string>();
				AnalysisRequestInfo analysisRequest = new AnalysisRequestInfo();
                AnalysisRequestParameterInfo newAnalysisRequest = new AnalysisRequestParameterInfo();
				SampleInfo sample = new SampleInfo();
				FilesInfo files = new FilesInfo();
				List<AnalyzerMaterialInfo> analyzerMaterial = new List<AnalyzerMaterialInfo>();
				String uniqueID = String.Empty;
				String fileName = String.Empty;
				String sampleFile = String.Empty;
				String sampleFileFoss = String.Empty;

				importConfig = AIMManager.GetImportConfig(DataSourceTypeEnum.AdisseoAPI);
				foreach (ImportConfigInfo config in importConfig)
				{
					analyzerPendingSample = AIMManager.GetAnalyzerPendingSample(AnalyzerStatusEnum.NotSent);
					foreach (String file in Directory.GetFiles(config.ProcessDirectory, "*.0", SearchOption.TopDirectoryOnly))
					{
						fileName = Path.GetFileName(file);
						foreach (AnalyzerPendingSampleInfo pendingSample in analyzerPendingSample)
						{
							sampleFile = " " + pendingSample.SampleID.ToString() + ".0";
							sampleFileFoss = "_" + pendingSample.SampleID.ToString() + ".0";
							if (fileName.ToUpper().IndexOf(sampleFile) > 0 || fileName.ToUpper().IndexOf(sampleFileFoss) > 0)
							{
								sample = AIMManager.GetSample(pendingSample.SampleID);
								analyzerMaterial = AIMManager.GetAnalyzerMaterial(DataSourceTypeEnum.AdisseoAPI, sample.AIMMaterialID);
								if (analyzerMaterial.Count > 0)
								{
									analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)DataSourceTypeEnum.AdisseoAPI, true);
									analysesCodes = new List<string>();

									for (int i = 0; i < analyzerNutrient.Count; i++)
									{
										if (analyzerNutrient[i].AnalyzerNutrientCode != "STTP")
										{
											analysesCodes.Add(analyzerNutrient[i].AnalyzerNutrientCode);
										}
									}

									newAnalysisRequest.materialCode = analyzerMaterial[0].AnalyzerMaterialCode;
									newAnalysisRequest.analysesCodes = analysesCodes;
									newAnalysisRequest.sampleCode = sample.SampleID.ToString();
									newAnalysisRequest.spectraFileName = fileName;
									byte[] byteFile = System.IO.File.ReadAllBytes(config.ProcessDirectory + @"\" + fileName);
									newAnalysisRequest.spectraFile = Convert.ToBase64String(byteFile);
                                    Int32 brukerSpectrumID = ResolveBrukerSpectrumID(sample.SampleID);
                                    analysisRequest = CreateAnalysisRequest(newAnalysisRequest, brukerSpectrumID);
									if (analysisRequest != null)
									{

										if (analysisRequest.SampleCode != null)
										{
											pendingSample.RequestCode = analysisRequest.Id;
											pendingSample.SentStamp = DateTime.Now;
											pendingSample.SpectraName = fileName;
											AnalyzerPendingSampleInfo analyzerPending = pendingSample;
											AIMManager.SaveAnalyzerPendingSample(ref analyzerPending);
											files.FileName = fileName;
											files.ProcessPath = config.ProcessDirectory;
											files.BackupPath = config.BackUpDirectory;
											DataExchangeManager.MoveFileToFolder(files, pendingSample.SampleID);
										}
									}
								}
								break;
							}
						}
					}
				}

			}
			catch (Exception ex)
			{
				AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Fatal, ex.Message, (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);
			}


		}

		public static List<AnalysisResultsInfo> GetAnalysisResultListAll()
		{
			List<AnalysisResultsInfo> analysisResult = new List<AnalysisResultsInfo>();
            PNEAPIClient.Token = PneApiKey;             //Task1160 260309
            RestResponse<WebApiEnvelope<List<AnalysisResultsInfo>>> response = PNEAPIClient.GetAnalysisResultList(Token);
			analysisResult = response?.Data?.Data;
			return analysisResult;
		}


		public static APICreditInfo GetPNECredit()
		{
            APICreditInfo pneCredit = new APICreditInfo();
			PNEAPIClient.Token = PneApiKey;				//Task1160 260306
			RestResponse<WebApiEnvelope<APICreditInfo>> response = PNEAPIClient.GetCredit(Token);
			pneCredit = response?.Data?.Data;
			return pneCredit;
		}
		//public static AnalysisRequestInfo CreateAnalysisRequest(AnalysisRequestParameterInfo newAnalysisRequest)
		//{
  //          PNEAPIClient.Token = PneApiKey;             //Task1160 260306
  //          RestResponse<WebApiEnvelope<AnalysisRequestInfo>> response = PNEAPIClient.CreateAnalysisRequest(newAnalysisRequest, Token);
  //          AnalysisRequestInfo analysisRequest = response?.Data?.Data;
		//	if (analysisRequest != null)
		//	{
		//		LogPNEResultsRecieved(
		//			nameof(CreateAnalysisRequest),
		//			analysisRequest.SampleCode ?? String.Empty,
		//			PNELogHelper.GeneratePNELogMessage(response),
		//			ParseAnalysisRequestId(analysisRequest.Id));
		//	}
  //          return analysisRequest;
  //      }

        public static AnalysisRequestInfo CreateAnalysisRequest(AnalysisRequestParameterInfo newAnalysisRequest, Int32 brukerSpectrumID)
        {
            PNEAPIClient.Token = PneApiKey;
            RestResponse<WebApiEnvelope<AnalysisRequestInfo>> response = PNEAPIClient.CreateAnalysisRequest(newAnalysisRequest, brukerSpectrumID, Token);
            AnalysisRequestInfo analysisRequest = response?.Data?.Data;
            if (analysisRequest != null)
            {
                LogPNEResultsRecieved(
                    nameof(CreateAnalysisRequest),
                    analysisRequest.SampleCode ?? String.Empty,
                    PNELogHelper.GeneratePNELogMessage(response),
                    ParseAnalysisRequestId(analysisRequest.Id),
                    brukerSpectrumID);
            }
            return analysisRequest;
        }

		//public static List<AnalysisResultInfo> GetAnalysisResultList(String analysisRequestId, String sampleCode)
		//{
		//	PNEAPIClient.Token = PneApiKey;
		//	RestResponse<WebApiEnvelope<List<AnalysisResultInfo>>> response = PNEAPIClient.GetAnalysisResultList(analysisRequestId, Token);
		//	List<AnalysisResultInfo> result = response?.Data?.Data;
		//	if (result != null)
		//	{
		//		foreach (AnalysisResultInfo row in result)
		//		{
		//			LogPNEResultsRecieved
		//			(
		//				nameof(GetAnalysisResultList),
		//				row.SampleCode,
		//				PNELogHelper.GeneratePNELogMessage(response),
		//				ParseAnalysisRequestId(row.AnalysisRequestId)
		//			);
		//		}
		//	}
		//	return result;
		//}

		public static List<AnalysisResultInfo> GetAnalysisResultList(String analysisRequestId, String sampleCode, Int32 brukerSpectrumID)
        {
            PNEAPIClient.Token = PneApiKey;
            RestResponse<WebApiEnvelope<List<AnalysisResultInfo>>> response = PNEAPIClient.GetAnalysisResultList(analysisRequestId, brukerSpectrumID, Token);
            List<AnalysisResultInfo> result = response?.Data?.Data;
            if (result != null)
            {
                foreach (AnalysisResultInfo row in result)
                {
                    LogPNEResultsRecieved
                    (
                        nameof(GetAnalysisResultList),
                        row.SampleCode,
                        PNELogHelper.GeneratePNELogMessage(response),
                        ParseAnalysisRequestId(row.AnalysisRequestId),
                        brukerSpectrumID
                    );
                }
            }
            return result;
        }

        public static void GettingRequest()
		{
			List<AnalyzerPendingSampleInfo> analyzerPendingSample = new List<AnalyzerPendingSampleInfo>();
			List<AnalysisResultInfo> result = new List<AnalysisResultInfo>();
			SampleResultInfo sampleResult = new SampleResultInfo();
			SampleResultPredictInfo sampleResultPredict = new SampleResultPredictInfo();
			Boolean isDone = false;

			analyzerPendingSample = AIMManager.GetAnalyzerPendingSample(AnalyzerStatusEnum.NotReceived);
			foreach (AnalyzerPendingSampleInfo pendingSample in analyzerPendingSample)
			{
				isDone = false;
				if (pendingSample.RequestCode != null)
				{

					Int32 brukerSpectrumID = ResolveBrukerSpectrumID(pendingSample.SampleID);
					result = GetAnalysisResultList(pendingSample.RequestCode, pendingSample.SampleID.ToString(), brukerSpectrumID);

					if (result != null)
					{
						if (result.Count > 0)
						{
							isDone = true;

							for (int ii = 0; ii < result.Count; ii++)
							{
								if (result[ii].Status == "PENDING")
								{
									isDone = false;
									break;
								}
							}
						}
					}
				}

				if (isDone == false)
				{
					if (DateTime.Now.AddDays(-10) > pendingSample.SentStamp)
					{
					}
				}
				else
				{
					foreach (AnalysisResultInfo header in result)
					{
						foreach (ParameterResultInfo detail in header.ParameterResult)
						{
							sampleResult = new SampleResultInfo();
							sampleResultPredict = new SampleResultPredictInfo();

							sampleResult.SampleID = pendingSample.SampleID;
							sampleResult.AnalysisDate = DateTime.Now;
							sampleResult.IsActive = true;
							sampleResult.ResultValue = detail.Value;
							sampleResult.AnalyzerNutrientCode = header.AnalysisCode + "-" + detail.ParameterCode;
							sampleResult.DataSourceTypeID = (Int32)DataSourceTypeEnum.AdisseoAPI;
							sampleResult.SqlResultID = "PNEAPI" + pendingSample.RequestCode;
							sampleResult.AimDatabaseID = 0;
							sampleResult.AnalyzerNutrientID = 0;
							sampleResult.CFCUserID = 0;

							sampleResultPredict.IsResultPredict = detail.PredictRange.IsIn;
							sampleResultPredict.GlobalH = detail.PredictRange.GlobalH;
							sampleResultPredict.Limit = detail.PredictRange.Limit;
							sampleResultPredict.IsInMaterialRange = detail.PredictRange.RefIn;
							sampleResultPredict.MaxMaterialRange = detail.PredictRange.RefMax;
							sampleResultPredict.MinMaterialRange = detail.PredictRange.RefMin;

							AIMManager.InsertSampleResult(ref sampleResult, ref sampleResultPredict, null, 0);
						}
					}
					pendingSample.ReceivedStamp = DateTime.Now;
					AnalyzerPendingSampleInfo analyzerPending = pendingSample;
					AIMManager.SaveAnalyzerPendingSample(ref analyzerPending);
				}
				Thread.Sleep(500);
			}
		}

		public static void ValidateAnalysisCode()
		{
			try
			{
				Boolean isError = false;

				isError = AnalyseCode();
				if (isError == false)
				{
					MaterialCode();
                }

            }
			catch (Exception ex)
			{
				AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Fatal, ex.Message, (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);
			}
		}

		public static void LoadAnalyse(ref List<AnalysesMaterialInfo> analysesMaterialList)
		{

		}

		public static Boolean AnalyseCode()
		{
			List<AnalysesMaterialInfo> analysesMaterialList = new List<AnalysesMaterialInfo>();
			return AnalyseCode(ref analysesMaterialList);
		}

		public static Boolean AnalyseCode(ref List<AnalysesMaterialInfo> analysesMaterialList)
		{
            AnalyzerNutrientInfo updateAnalyzerNutrient = new AnalyzerNutrientInfo();
			List<AnalysesInfo> apiAnalyse = new List<AnalysesInfo>();
			List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
			Boolean isUpdate = false;
			Boolean isError = false;

			analysesMaterialList = new List<AnalysesMaterialInfo>();
            PNEAPIClient.Token = PneApiKey;				//Task1160 260306

            apiAnalyse = PNEAPIClient.GetAnalyse(Token)?.Data?.Data;

            if (apiAnalyse == null)
			{
				isError = true;
			}
			else if (apiAnalyse.Count < 1)
			{
                isError = true;
            }
			else
			{

				analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)DataSourceTypeEnum.AdisseoAPIAnalyseCode);

				foreach (AnalysesInfo apiItem in apiAnalyse)
				{
					if (apiItem.Code == null)
                    {
                        isError = true;
                    }
                    else
					{
						updateAnalyzerNutrient = new AnalyzerNutrientInfo();
						updateAnalyzerNutrient.AnalyzerNutrientID = 0;
						updateAnalyzerNutrient.DataSourceTypeID = (Int32)DataSourceTypeEnum.AdisseoAPIAnalyseCode;
						updateAnalyzerNutrient.IsActive = false;
						isUpdate = false;

						for (int i = 0; i < analyzerNutrient.Count; i++)
						{
							if (apiItem.Code == analyzerNutrient[i].AnalyzerNutrientCode)
							{
								updateAnalyzerNutrient.AnalyzerNutrientID = analyzerNutrient[i].AnalyzerNutrientID;
								analyzerNutrient[i].AnalyzerNutrientCode = String.Empty;
								if (apiItem.Name != analyzerNutrient[i].AnalyzerNutrientName)
								{
									updateAnalyzerNutrient.AnalyzerNutrientName = apiItem.Name;
									isUpdate = true;
								}

								if (analyzerNutrient[i].IsActive == false)
								{
									isUpdate = true;
								}

								if (isUpdate == true)
								{
									updateAnalyzerNutrient.IsActive = true;
									updateAnalyzerNutrient.AnalyzerNutrientID = analyzerNutrient[i].AnalyzerNutrientID;
									analyzerNutrient[i].AnalyzerNutrientCode = String.Empty;
									AIMManager.SaveAnalyzerNutrient(ref updateAnalyzerNutrient);
								}
								break;
							}
						}

						if (updateAnalyzerNutrient.AnalyzerNutrientID == 0)
						{
							updateAnalyzerNutrient.AnalyzerNutrientCode = apiItem.Code;
							updateAnalyzerNutrient.AnalyzerNutrientName = apiItem.Name;
							updateAnalyzerNutrient.IsActive = true;
							AIMManager.SaveAnalyzerNutrient(ref updateAnalyzerNutrient);
						}

						for (int i = 0; i < apiItem.MaterialsCodes.Count; i++)
						{
							analysesMaterialList.Add(new AnalysesMaterialInfo
							{
								MaterialCodes = apiItem.MaterialsCodes[i],
								AnalysesCode = apiItem.Code
							});
						}
					}
				}

			}

			return isError;
		}

		public static Boolean MaterialCode()
		{
			List<PNEForAnalyzerMaterialRefAnalyzerNutrientInfo> pneForAnalyzerMaterialRefAnalyzerNutrients = new List<PNEForAnalyzerMaterialRefAnalyzerNutrientInfo>();
			List<AnalyzerMaterialRefAnalyzerAnalyseInfo> analyzerMaterialRefAnalyzerAnalyses = new List<AnalyzerMaterialRefAnalyzerAnalyseInfo>();
			List<PNEMaterialSampleTypeInfo> pneMaterialSampleTypes = new List<PNEMaterialSampleTypeInfo>();
			List<AnalyzerMaterialInfo> material = new List<AnalyzerMaterialInfo>();
			List<MaterialInfo> apiMaterial = new List<MaterialInfo>();
			List<PNEMaterialTypeInfo> pneMaterialTypes = new List<PNEMaterialTypeInfo>();

            PNEForAnalyzerMaterialRefAnalyzerNutrientInfo pneForAnalyzerMaterialRefAnalyzerNutrient = new PNEForAnalyzerMaterialRefAnalyzerNutrientInfo();
			PNEMaterialSampleTypeInfo pneMaterialSampleType = new PNEMaterialSampleTypeInfo();
			AnalyzerNutrientInfo analyzerNutrient = new AnalyzerNutrientInfo();
			PNEMaterialTypeInfo pneMaterialType = new PNEMaterialTypeInfo();
			AnalyzerMaterialInfo updateMaterial = new AnalyzerMaterialInfo();
            ErrorInfo error = new ErrorInfo();

			List<Int32> pneMaterialSampleTypeIDs = new List<Int32>();
			Int32 pneForAnalyzerMaterialRefAnalyzerNutrientID = 0;
			Int32 pneMaterialSampleTypeID = 0;
			Int32 analyzerMaterialID = 0;
			Int32 analyzerNutrientID = 0;
			Boolean isFound = false;
			Boolean isUpdate = false;
			Boolean isError = false;

            PNEAPIClient.Token = PneApiKey;             //Task1160 260306
            pneMaterialSampleTypes = AIMManager.GetPNEMaterialSampleType();
			apiMaterial = PNEAPIClient.GetMaterial(Token)?.Data?.Data;

            //Task1160 DB260306 if (apiMaterial == null || pneMaterialSampleTypes.Count == 0)
            if (apiMaterial == null)		//Task1160 DB260306 
            {
				isError = true;
			}
			else
			{
				material = AIMManager.GetAnalyzerMaterial((Int32)DataSourceTypeEnum.AdisseoAPI);
				foreach (MaterialInfo apiItem in apiMaterial)
				{
					pneMaterialSampleTypeIDs = new List<Int32>();

					updateMaterial = new AnalyzerMaterialInfo();
					updateMaterial.AnalyzerMaterialID = 0;
					updateMaterial.DataSourceTypeID = (Int32)DataSourceTypeEnum.AdisseoAPI;
                    updateMaterial.IsActive = false;
					isUpdate = false;

					foreach (AnalyzerMaterialInfo item in material)
					{
						if (apiItem.Code == item.AnalyzerMaterialCode.Trim())
						{
							updateMaterial.AnalyzerMaterialID = item.AnalyzerMaterialID;
							updateMaterial.AnalyzerMaterialCode = apiItem.Code;

							if (item.AnalyzerMaterialName != apiItem.Name)
							{
								updateMaterial.AnalyzerMaterialName = apiItem.Name;
								isUpdate = true;
							}

							if (item.IsActive == false)
							{
								isUpdate = true;
							}

							if (isUpdate == true)
							{
								updateMaterial.IsActive = true;
                                AIMManager.SaveAnalyzerMaterial(ref updateMaterial, true);
							}

							break;
						}
					}

					if (updateMaterial.AnalyzerMaterialID == 0)
					{
						updateMaterial.AnalyzerMaterialCode = apiItem.Code;
						updateMaterial.AnalyzerMaterialName = apiItem.Name;
						updateMaterial.IsActive = true;
						error = AIMManager.SaveAnalyzerMaterial(ref updateMaterial, false);
						analyzerMaterialID = error.RecordID;
					}
					else
					{
						analyzerMaterialID = updateMaterial.AnalyzerMaterialID;
					}

					for (Int32 i = 0; i < apiItem.samplePresentations.Count; i++)
					{
						isFound = false;
						pneMaterialSampleTypeID = 0;

						for (int t = 0; t < pneMaterialSampleTypes.Count; t++)
						{
							if (apiItem.samplePresentations[i] == pneMaterialSampleTypes[t].PNEMaterialSampleTypeCode)
							{
								pneMaterialSampleTypeID = pneMaterialSampleTypes[t].PNEMaterialSampleTypeID;
								isFound = true;
								break;
							}
						}

						if (isFound == false)
						{
							pneMaterialSampleType = new PNEMaterialSampleTypeInfo();
							pneMaterialSampleType.PNEMaterialSampleTypeID = 0;
							pneMaterialSampleType.PNEMaterialSampleTypeCode = apiItem.samplePresentations[i];
							pneMaterialSampleType.IsActive = true;
                            error = AIMManager.SavePNEMaterialSampleType(ref pneMaterialSampleType);

							pneMaterialSampleTypeID = error.RecordID;
							pneMaterialSampleTypes = AIMManager.GetPNEMaterialSampleType();
                        }

                        pneMaterialSampleTypeIDs.Add(pneMaterialSampleTypeID);
					}

					pneMaterialTypes = AIMManager.GetPNEMaterialType(analyzerMaterialID);

					for (Int32 i = 0; i < pneMaterialSampleTypeIDs.Count; ++i)
					{
						isFound = false;
						pneMaterialSampleTypeID = pneMaterialSampleTypeIDs[i];

						for (Int32 t = 0; t < pneMaterialTypes.Count; t++)
						{
							if (pneMaterialSampleTypeID == pneMaterialTypes[t].PNEMaterialSampleTypeID)
							{
								pneMaterialType = pneMaterialTypes[t];
								isFound = true;
								break;
							}
						}

						if (isFound == false)
						{
							pneMaterialType.PNEMaterialSampleTypeID = 0;
							pneMaterialType.AnalyzerMaterialID = analyzerMaterialID;
							pneMaterialType.PNEMaterialSampleTypeID = pneMaterialSampleTypeID;
							pneMaterialType.IsActive = true;
                            error = AIMManager.SavePNEMaterialType(ref pneMaterialType);
							pneMaterialTypes = AIMManager.GetPNEMaterialType(analyzerMaterialID);
						}
						else
						{
							if (pneMaterialType.IsActive == false)
							{
								pneMaterialType.IsActive = true;
                                error = AIMManager.SavePNEMaterialType(ref pneMaterialType);
							}
						}
					}

					for (Int32 p = 0; p < pneMaterialTypes.Count; p++)
					{
						isFound = false;
						for (Int32 s = 0; s < pneMaterialSampleTypeIDs.Count; s++)
						{
							if (pneMaterialTypes[p].PNEMaterialSampleTypeID == pneMaterialSampleTypeIDs[s])
							{
								isFound = true;
								break;
							}
						}

						if (isFound == false)
						{
							pneMaterialType = pneMaterialTypes[p];
							pneMaterialType.IsActive = false;
                            error = AIMManager.SavePNEMaterialType(ref pneMaterialType);
						}
					}

					pneForAnalyzerMaterialRefAnalyzerNutrients = AIMManager.GetPNEForAnalyzerMaterialRefAnalyzerNutrient(analyzerMaterialID);

					for (Int32 i = 0; i < apiItem.AnalysesCodes.Count; i++)
					{
						isFound = false;
						pneForAnalyzerMaterialRefAnalyzerNutrientID = 0;

						for (int t = 0; t < pneForAnalyzerMaterialRefAnalyzerNutrients.Count; t++)
						{
							if (apiItem.AnalysesCodes[i] == pneForAnalyzerMaterialRefAnalyzerNutrients[t].AnalyzerNutrientCode)
							{
								pneForAnalyzerMaterialRefAnalyzerNutrientID = pneForAnalyzerMaterialRefAnalyzerNutrients[t].PNEForAnalyzerMaterialRefAnalyzerNutrientID;
								pneForAnalyzerMaterialRefAnalyzerNutrient = pneForAnalyzerMaterialRefAnalyzerNutrients[t];
								isFound = true;
								break;
							}
						}

						if (isFound == false)
						{
							analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)DataSourceTypeEnum.AdisseoAPI, apiItem.AnalysesCodes[i]);

							if (analyzerNutrient != null)
							{
								pneForAnalyzerMaterialRefAnalyzerNutrient = new PNEForAnalyzerMaterialRefAnalyzerNutrientInfo();
								pneForAnalyzerMaterialRefAnalyzerNutrient.PNEForAnalyzerMaterialRefAnalyzerNutrientID = 0;
								pneForAnalyzerMaterialRefAnalyzerNutrient.AnalyzerMaterialID = analyzerMaterialID;
								pneForAnalyzerMaterialRefAnalyzerNutrient.AnalyzerNutrientID = analyzerNutrient.AnalyzerNutrientID;
								pneForAnalyzerMaterialRefAnalyzerNutrient.IsActive = true;
                                error = AIMManager.SavePNEForAnalyzerMaterialRefAnalyzerNutrient(ref pneForAnalyzerMaterialRefAnalyzerNutrient);
								pneForAnalyzerMaterialRefAnalyzerNutrients = AIMManager.GetPNEForAnalyzerMaterialRefAnalyzerNutrient(analyzerMaterialID);
							}
						}
						else
						{
							if (pneForAnalyzerMaterialRefAnalyzerNutrient.IsActive == false)
							{
								pneForAnalyzerMaterialRefAnalyzerNutrient.IsActive = true;
                                error = AIMManager.SavePNEForAnalyzerMaterialRefAnalyzerNutrient(ref pneForAnalyzerMaterialRefAnalyzerNutrient);
							}
						}
					}
				}
			}
			return isError;
		}
		#endregion
	}
}
