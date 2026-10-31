using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using CFCAPICore.Entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace CFCAIM.DataExchange
{
	public class AdisseoAPI
	{
		#region Maintenance
		public static void SendPendingSampleSpectra(Boolean isSampleID)
		{
			List<AnalyzerPendingSampleInfo> analyzerPendingSample = new List<AnalyzerPendingSampleInfo>();
			List<AnalyzerAnalyseInfo> analyzerAnalyse = new List<AnalyzerAnalyseInfo>();
			List<ImportConfigInfo> importConfig = new List<ImportConfigInfo>();
			List<String> analysesCodes = new List<string>();
			AnalysisRequestInfo analysisRequest = new AnalysisRequestInfo();
			AnalysisRequestParameterInfo analysisRequestParameter = new AnalysisRequestParameterInfo();
			SampleInfo sample = new SampleInfo();
			FilesInfo files = new FilesInfo();
			List<AnalyzerMaterialInfo> analyzerMaterial = new List<AnalyzerMaterialInfo>();
			FileTypeEnum fileType = FileTypeEnum.NIR;
			String uniqueID = String.Empty;
			String fileName = String.Empty;
			String sampleFile = String.Empty;
			String sampleFileFoss = String.Empty;
			Int32 customDate = 0;

			importConfig = AIMManager.GetImportConfig(DataSourceTypeEnum.Adisseo);
			foreach (ImportConfigInfo config in importConfig)
			{
				fileType = (FileTypeEnum)config.FileTypeID;
				analyzerPendingSample = AIMManager.GetAnalyzerPendingSample(AnalyzerStatusEnum.NotSent);
				foreach (String file in Directory.GetFiles(config.ProcessDirectory, "*." + fileType.ToString(), SearchOption.TopDirectoryOnly))
				{
					fileName = Path.GetFileName(file);
					foreach (AnalyzerPendingSampleInfo pendingSample in analyzerPendingSample)
					{
						sample = AIMManager.GetSample(pendingSample.SampleID);
						if (isSampleID == true)
						{
							sampleFile = " " + pendingSample.SampleID.ToString() + "." + fileType.ToString();
							sampleFileFoss = "_" + pendingSample.SampleID.ToString() + "." + fileType.ToString();
						}
						else
						{
							sampleFile = " " + sample.SampleCode.Trim() + " ";
							sampleFileFoss = "_" + sample.SampleCode.Trim() + " ";						
						}


						if (fileName.ToUpper().IndexOf(sampleFile) > 0 || fileName.ToUpper().IndexOf(sampleFileFoss) > 0)
						{
							analyzerMaterial = AIMManager.GetAnalyzerMaterial(DataSourceTypeEnum.Adisseo, sample.AIMMaterialID);
							if (analyzerMaterial.Count > 0)
							{
								analyzerAnalyse = AIMManager.GetAnalyzerAnalyse(analyzerMaterial[0].AnalyzerMaterialID);

								analysesCodes = new List<string>();

								for (int i = 0; i < analyzerAnalyse.Count; i++)
								{
									//if (analyzerAnalyse[i].Code != "STPP")
									//{
										analysesCodes.Add(analyzerAnalyse[i].Code);
									//}
								}

								analysisRequestParameter.materialCode = analyzerMaterial[0].AnalyzerMaterialCode;  // "COR";
								analysisRequestParameter.analysesCodes = analysesCodes;
								analysisRequestParameter.sampleCode = sample.SampleID.ToString();
								//MW230604 analysisRequestParameter.description = sample.SampleCode;
								//analysisRequestParameter.origin = "US";
								Int32.TryParse(DateTime.Now.ToString("yyMMddhhmm"), out customDate);
								//analysisRequestParameter.customDate = customDate;
								//analysisRequestParameter.spectraFileName = fileName;
								byte[] byteFile = System.IO.File.ReadAllBytes(config.ProcessDirectory + @"\" + fileName);
								// convert file byte to base 64
								analysisRequestParameter.spectraFile = Convert.ToBase64String(byteFile);
								analysisRequest = APIManager.CreateAnalysisRequest(analysisRequestParameter);
								if (analysisRequest != null)
								{
									pendingSample.RequestCode = analysisRequest.Id;
									pendingSample.SentStamp = DateTime.Now;
									pendingSample.SpectraName = fileName;
									AnalyzerPendingSampleInfo analyzerPending = pendingSample;
									AIMManager.SaveAnalyzerPendingSample(ref analyzerPending);
									files.FileName = fileName;
									files.ProcessPath = config.ProcessDirectory;
									files.BackupPath = config.BackUpDirectory;

									Int32 sampleID = 0;
									if (pendingSample != null)
									{
										sampleID = pendingSample.SampleID;
									}
									//DataExchangeManager.MoveFileToFolder(files, pendingSample.SampleID);
									DataExchangeManager.MoveFileToFolder(files, sampleID);
								}
							}
							break;
						}
					}
				}
			}
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
					result = APIManager.GetAnalysisResultList(pendingSample.RequestCode);
					isDone = true;
					for (int ii = 0; ii < result.Count; ii++)
					{
						if (result[ii].Status == "QUEUE")
						{
							isDone = false;
							break;
						}
					}

					if (isDone == true)
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
								sampleResult.DataSourceTypeID = (Int32)DataSourceTypeEnum.Adisseo;								
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
				}

				//if (analyzerPendingSample.Count == 0)
				//{
				//	break;
				//}
			//	Thread.Sleep(3000);
			//}
		}


		public static List<AnalysisResultInfo> GetAnalysisResultList()
		{
			List<AnalysisResultInfo> analysisResult = new List<AnalysisResultInfo>();

			analysisResult = AdisseoAPI.GetAnalysisResultList();
			return analysisResult;
		}

		public static void ValidateAnalysisCode()
		{
			Boolean isError = false;

			isError = AdisseoAPI.AnalyseCode();
			if (isError == false)
			{
				isError = AdisseoAPI.MaterialCode();
			}
		}

		public static Boolean AnalyseCode()
		{
			CFCUser.Global loginUser = new CFCUser.Global();
			AnalyzerAnalyseInfo updateAnalyse = new AnalyzerAnalyseInfo();
			List<AnalysesInfo> apiAnalyse = new List<AnalysesInfo>();
			List<AnalyzerAnalyseInfo> analyse = new List<AnalyzerAnalyseInfo>();
			Boolean isError = false;

			apiAnalyse = APIManager.GetAnalyse();
			if (apiAnalyse == null)
			{
				isError = true;
			}
			else
			{
				analyse = AIMManager.GetAnalyzerAnalyse();
				foreach (AnalysesInfo apiItem in apiAnalyse)
				{
					updateAnalyse = new AnalyzerAnalyseInfo();
					updateAnalyse.AnalyzerAnalyseID = 0;
					updateAnalyse.DataSourceTypeID = (Int32)DataSourceTypeEnum.Adisseo;					
					updateAnalyse.CFCUserID = loginUser.CFCLoginUser.CFCUserID;
					updateAnalyse.IsActive = false;
					for (int i = 0; i < analyse.Count; i++)
					{
						if (apiItem.Code == analyse[i].Code)
						{
							updateAnalyse.Code = apiItem.Code;
							updateAnalyse.Name = apiItem.Name;
							updateAnalyse.Description = apiItem.Description;
							updateAnalyse.IsActive = true;
							updateAnalyse.AnalyzerAnalyseID = analyse[i].AnalyzerAnalyseID;
							analyse[i].Code = String.Empty;
							break;
						}
					}

					if (updateAnalyse.AnalyzerAnalyseID == 0)
					{
						updateAnalyse.Code = apiItem.Code;
						updateAnalyse.Name = apiItem.Name;
						updateAnalyse.Description = apiItem.Description;
						updateAnalyse.IsActive = true;
					}

					AIMManager.SaveAnalyzerAnalyse(ref updateAnalyse);
				}

				for (int i = 0; i < analyse.Count; i++)
				{
					if (analyse[i].Code.Length > 0)
					{
						AIMManager.DeleteAnalyzerAnalyse(analyse[i].AnalyzerAnalyseID);
					}
				}
			}
			return isError;
		}

		public static Boolean MaterialCode()
		{
			CFCUser.Global loginUser = new CFCUser.Global();
			AnalyzerMaterialInfo updateMaterial = new AnalyzerMaterialInfo();
			ErrorInfo errorInfo = new ErrorInfo();
			List<MaterialInfo> apiMaterial = new List<MaterialInfo>();
			List<AnalyzerMaterialInfo> material = new List<AnalyzerMaterialInfo>();
			List<AnalyzerMaterialRefAnalyzerAnalyseInfo> materialAnalyse = new List<AnalyzerMaterialRefAnalyzerAnalyseInfo>();
			Boolean isError = false;


			apiMaterial = APIManager.GetMaterial();
			if (apiMaterial == null)
			{
				isError = true;
			}
			else
			{							
				material = AIMManager.GetAnalyzerMaterial((Int32)DataSourceTypeEnum.Adisseo);
				foreach (MaterialInfo apiItem in apiMaterial)
				{
					updateMaterial = new AnalyzerMaterialInfo();
					updateMaterial.AnalyzerMaterialID = 0;
					updateMaterial.DataSourceTypeID = (Int32)DataSourceTypeEnum.Adisseo;					
					updateMaterial.CFCUserID = loginUser.CFCLoginUser.CFCUserID;
					updateMaterial.IsActive = false;
					foreach (AnalyzerMaterialInfo item in material)
					{
						if (apiItem.Code == item.AnalyzerMaterialCode)
						{
							updateMaterial.AnalyzerMaterialID = item.AnalyzerMaterialID;
							updateMaterial.AnalyzerMaterialCode = apiItem.Code;
							updateMaterial.AnalyzerMaterialName = apiItem.Name;
							updateMaterial.IsActive = true;
							updateMaterial.CFCUserID = loginUser.CFCLoginUser.CFCUserID;
							break;
						}
					}

					if (updateMaterial.AnalyzerMaterialID == 0)
					{
						updateMaterial.AnalyzerMaterialCode = apiItem.Code;
						updateMaterial.AnalyzerMaterialName = apiItem.Name;
						updateMaterial.IsActive = true;
					}
					
					errorInfo = AIMManager.SaveAnalyzerMaterial(ref updateMaterial, true);
					if (errorInfo.ErrorNumber == 0)
					{
						updateMaterial.AnalyzerMaterialID = errorInfo.RecordID;
						materialAnalyse = AIMManager.GetAnalyzerMaterialAnalyse(updateMaterial.AnalyzerMaterialID, updateMaterial.DataSourceTypeID);
						for (int i = 0; i < apiItem.AnalysesCodes.Count; i++)
						{
							for (int ii = 0; ii < materialAnalyse.Count; ii++)
							{
								if (materialAnalyse[ii].Code == apiItem.AnalysesCodes[i])
								{
									apiItem.AnalysesCodes[i] = String.Empty;
									materialAnalyse[ii].Code = String.Empty;
									break;
								}
							}

							if (apiItem.AnalysesCodes[i].Length > 0)		//Not in table 
							{	  
								//10/20/20 - commented. AnalyzerMaterialRefAnalyse table does not exist
								//AIMManager.InsertAnalyzerMaterialRefAnalyse(updateMaterial.AnalyzerMaterialID, DataSourceTypeEnum.Adisseo, apiItem.AnalysesCodes[i]);
							}
						}

						for (int ii = 0; ii < materialAnalyse.Count; ii++)
						{
							if (materialAnalyse[ii].Code.Length > 0)	//No longer a valid code
							{
								//10/20/20 - commented. AnalyzerMaterialRefAnalyse table does not exist
								//AIMManager.DeleteAnalyzerMaterialRefAnalyse(materialAnalyse[ii].AnalyzerMaterialRefAnalyzerAnalyseID);
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
