using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;


namespace CFCAIMCore.DataExchange
{
	public class AdisseoAPI
	{
		#region Variables
		private static String _aimApiKey = String.Empty;
		private static String _aimInstrumentKey = String.Empty;
		#endregion

		#region  Properties
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
		#endregion

		#region Maintenance
		public static void SendPendingSampleSpectra()
		{
			try
			{
				List<AnalyzerPendingSampleInfo> analyzerPendingSample = new List<AnalyzerPendingSampleInfo>();
				//List<AnalyzerAnalyseInfo> analyzerAnalyse = new List<AnalyzerAnalyseInfo>();
				List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
				List<ImportConfigInfo> importConfig = new List<ImportConfigInfo>();
				List<String> analysesCodes = new List<string>();
				AnalysisRequestInfo analysisRequest = new AnalysisRequestInfo();
				AnalysisRequestParameterInfo newAnalysisRequest = new AnalysisRequestParameterInfo();
				SampleInfo sample = new SampleInfo();
				FilesInfo files = new FilesInfo();
				List<AnalyzerMaterialInfo> analyzerMaterial = new List<AnalyzerMaterialInfo>();
				// DB230315 FileTypeEnum fileType = FileTypeEnum.NIR;
				String uniqueID = String.Empty;
				String fileName = String.Empty;
				String sampleFile = String.Empty;
				String sampleFileFoss = String.Empty;

				importConfig = AIMManager.GetImportConfig(DataSourceTypeEnum.AdisseoAPI);
				foreach (ImportConfigInfo config in importConfig)
				{
					//fileType = (FileTypeEnum)config.FileTypeID;                    
					analyzerPendingSample = AIMManager.GetAnalyzerPendingSample(AnalyzerStatusEnum.NotSent);
					foreach (String file in Directory.GetFiles(config.ProcessDirectory, "*.0", SearchOption.TopDirectoryOnly))
					{
						fileName = Path.GetFileName(file);
						foreach (AnalyzerPendingSampleInfo pendingSample in analyzerPendingSample)
						{
							sampleFile = " " + pendingSample.SampleID.ToString() + ".0";  // + fileType.ToString();
							sampleFileFoss = "_" + pendingSample.SampleID.ToString() + ".0";  // + fileType.ToString();
							if (fileName.ToUpper().IndexOf(sampleFile) > 0 || fileName.ToUpper().IndexOf(sampleFileFoss) > 0)
							{
								sample = AIMManager.GetSample(pendingSample.SampleID);
								analyzerMaterial = AIMManager.GetAnalyzerMaterial(DataSourceTypeEnum.AdisseoAPI, sample.AIMMaterialID);
								if (analyzerMaterial.Count > 0)
								{
									//analyzerAnalyse = AIMManager.GetAnalyzerAnalyse(analyzerMaterial[0].AnalyzerMaterialID);
									analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)DataSourceTypeEnum.AdisseoAPI, true);
									analysesCodes = new List<string>();

									for (int i = 0; i < analyzerNutrient.Count; i++)
									{
										if (analyzerNutrient[i].AnalyzerNutrientCode != "STTP")
										{
											analysesCodes.Add(analyzerNutrient[i].AnalyzerNutrientCode);
										}
									}

									newAnalysisRequest.materialCode = analyzerMaterial[0].AnalyzerMaterialCode;  // "COR";
									newAnalysisRequest.analysesCodes = analysesCodes;
									newAnalysisRequest.sampleCode = sample.SampleID.ToString();
									//remove mw230530                             newAnalysisRequest.description = sample.SampleCode;
									//newAnalysisRequest.origin = "US";
									//Int32.TryParse(DateTime.Now.ToString("yyMMddhhmm"), out customDate);
									//newAnalysisRequest.customDate = customDate;
									newAnalysisRequest.spectraFileName = fileName;
									byte[] byteFile = System.IO.File.ReadAllBytes(config.ProcessDirectory + @"\" + fileName);
									// convert file byte to base 64
									newAnalysisRequest.spectraFile = Convert.ToBase64String(byteFile);
									analysisRequest = APIManager.CreateAnalysisRequest(newAnalysisRequest);
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

		public static List<AnalysisResultsInfo> GetAnalysisResultList()
		{
			List<AnalysisResultsInfo> analysisResult = new List<AnalysisResultsInfo>();

			analysisResult = APIManager.GetAnalysisResultList();
			return analysisResult;
		}


		public static APICreditInfo GetPNECredit()
		{
			APICreditInfo pneCredit = new APICreditInfo();

			pneCredit = APIManager.GetCredit();
			return pneCredit;
		}

		public static void GettingRequest()
		{
			List<AnalyzerPendingSampleInfo> analyzerPendingSample = new List<AnalyzerPendingSampleInfo>();
			List<AnalysisResultInfo> result = new List<AnalysisResultInfo>();
			SampleResultInfo sampleResult = new SampleResultInfo();
			SampleResultPredictInfo sampleResultPredict = new SampleResultPredictInfo();
			Boolean isDone = false;

			//for (int i = 0; i < 3; i++)
			//{
			analyzerPendingSample = AIMManager.GetAnalyzerPendingSample(AnalyzerStatusEnum.NotReceived);
			foreach (AnalyzerPendingSampleInfo pendingSample in analyzerPendingSample)
			{
				isDone = false;
				if (pendingSample.RequestCode != null)
				{
					result = APIManager.GetAnalysisResultList(pendingSample.RequestCode);

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
					else
					{
						//TODO Mark will take care of this
						//AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Fatal, ex.Message, (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);
					}
				}

				if (isDone == false)
				{
					if (DateTime.Now.AddDays(-10) > pendingSample.SentStamp)
					{
						//pendingSample.ReceivedStamp = DateTime.Now;
						//AnalyzerPendingSampleInfo analyzerPending = pendingSample;
						//AIMManager.SaveAnalyzerPendingSample(ref analyzerPending);
						//AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "PNE Never created a result for sample ID = " + pendingSample.SampleID.ToString());
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
							//sampleResult.SqlResultID = "PNEAPI";
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

							//8/17/22
							//AIMManager.InsertSampleResult(ref sampleResult, ref sampleResultPredict, null);
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

				isError = AdisseoAPI.AnalyseCode();
				if (isError == false)
				{
					//TODO DAN PUT back in isError = AdisseoAPI.MaterialCode();
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
            // DB240710 CFCUser.Global loginUser = new CFCUser.Global();
            AnalyzerNutrientInfo updateAnalyzerNutrient = new AnalyzerNutrientInfo();
			List<AnalysesInfo> apiAnalyse = new List<AnalysesInfo>();
			List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
			Boolean isUpdate = false;
			Boolean isError = false;

			
			analysesMaterialList = new List<AnalysesMaterialInfo>();
            // DB241118 apiAnalyse = APIManager.GetAnalyse();			
            // apiAnalyse = CFCAPIManager.GetAnalyse(AimInstrumentKey);        // DB241118  Moved Adisseoo API access through CFC API instead of direct access through AIMworker

            apiAnalyse = APIManager.GetAnalyse();			//Task115? DB260129

            if (apiAnalyse == null)
			{
				isError = true;
			}
			else if (apiAnalyse.Count < 1)			//Task115? DB260129
			{
                isError = true;
            }
			else
			{

				analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)DataSourceTypeEnum.AdisseoAPIAnalyseCode);

				foreach (AnalysesInfo apiItem in apiAnalyse)
				{
					if (apiItem.Code == null)   //Task115? 260129
                    {							//Task115? 260129
                        isError = true;			//Task115? 260129
                    }                           //Task115? 260129
                    else
					{
						updateAnalyzerNutrient = new AnalyzerNutrientInfo();
						updateAnalyzerNutrient.AnalyzerNutrientID = 0;
						updateAnalyzerNutrient.DataSourceTypeID = (Int32)DataSourceTypeEnum.AdisseoAPIAnalyseCode;
						// DB240710 updateAnalyzerNutrient.CFCUserID = loginUser.CFCLoginUser.CFCUserID;
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

            // DB240710 CFCUser.Global loginUser = new CFCUser.Global();
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

		//TODO DAN ADD BACKIN	pneMaterialSampleTypes = AIMManager.GetPNEMaterialSampleType();
			apiMaterial = APIManager.GetMaterial();

			if (apiMaterial == null || pneMaterialSampleTypes.Count == 0)
			{
				isError = true;
			}
			else
			{
				//6/15/17
				//material = AIMManager.GetAnalyzerMaterial(DataSourceTypeEnum.Adisseo);
				material = AIMManager.GetAnalyzerMaterial((Int32)DataSourceTypeEnum.AdisseoAPI);
				foreach (MaterialInfo apiItem in apiMaterial)
				{
					pneMaterialSampleTypeIDs = new List<Int32>();

					updateMaterial = new AnalyzerMaterialInfo();
					updateMaterial.AnalyzerMaterialID = 0;
					updateMaterial.DataSourceTypeID = (Int32)DataSourceTypeEnum.AdisseoAPI;
                    // DB240710 updateMaterial.CFCUserID = loginUser.CFCLoginUser.CFCUserID;
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
                                // DB240710 updateMaterial.CFCUserID = loginUser.CFCLoginUser.CFCUserID;
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

					// Keep PNEMaterialSampleType up to date with PNE API call
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
                            // DB240710 pneMaterialSampleType.CFCUserID = loginUser.CFCLoginUser.CFCUserID;
                            error = AIMManager.SavePNEMaterialSampleType(ref pneMaterialSampleType);

							pneMaterialSampleTypeID = error.RecordID;
                            //TODO DAN ADD BACKIN	pneMaterialSampleTypes = AIMManager.GetPNEMaterialSampleType();
                        }

                        pneMaterialSampleTypeIDs.Add(pneMaterialSampleTypeID);
					}

					// Keep PNEMaterialType up to date with PNE API call

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
                            // DB240710 pneMaterialType.CFCUserID = loginUser.CFCLoginUser.CFCUserID;
                            error = AIMManager.SavePNEMaterialType(ref pneMaterialType);
							pneMaterialTypes = AIMManager.GetPNEMaterialType(analyzerMaterialID);
						}
						else
						{
							if (pneMaterialType.IsActive == false)
							{
								pneMaterialType.IsActive = true;
                                // DB240710 pneMaterialType.CFCUserID = loginUser.CFCLoginUser.CFCUserID;
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
                            // DB240710 pneMaterialType.CFCUserID = loginUser.CFCLoginUser.CFCUserID;
                            error = AIMManager.SavePNEMaterialType(ref pneMaterialType);
						}
					}

					// Keep PNEForAnalyzerMaterialRefAnalyzerNutrient up to date with PNE API call

					pneForAnalyzerMaterialRefAnalyzerNutrients = AIMManager.GetPNEForAnalyzerMaterialRefAnalyzerNutrient(analyzerMaterialID);

					// Keep PNEForAnalyzerMaterialRefAnalyzerNutrient up to date with PNE API call
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
                                // DB240710 pneForAnalyzerMaterialRefAnalyzerNutrient.CFCUserID = loginUser.CFCLoginUser.CFCUserID;
                                error = AIMManager.SavePNEForAnalyzerMaterialRefAnalyzerNutrient(ref pneForAnalyzerMaterialRefAnalyzerNutrient);
								pneForAnalyzerMaterialRefAnalyzerNutrients = AIMManager.GetPNEForAnalyzerMaterialRefAnalyzerNutrient(analyzerMaterialID);
							}
						}
						else
						{
							if (pneForAnalyzerMaterialRefAnalyzerNutrient.IsActive == false)
							{
								pneForAnalyzerMaterialRefAnalyzerNutrient.IsActive = true;
                                // DB240710 pneForAnalyzerMaterialRefAnalyzerNutrient.CFCUserID = loginUser.CFCLoginUser.CFCUserID;
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
