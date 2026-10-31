using CFCAIMCore.Entity;
using CFCAIMCore.Entity.CFCAPI;
using CFCAIMCore.Entity.OutsideLab;
using CFCAIMCore.Manager;
using CFCAIMWorker.Setup;
using CFCAPICore.Entity;
using System;
using System.Collections.Generic;

namespace CFCAIMWorker
{
    public class OutsideLabImport
    {
        public static void GetSampleFromAPI()
        {
            ErrorInfo error = new ErrorInfo();
            Int32 prevSampleID = 0;
            String aimApiKey = String.Empty;
            String token = String.Empty;
            APITokenInfo apiToken = new APITokenInfo();
            Int32 customerID = 0;

            try
            {
                //8/11/25 farag 
                Boolean isError = false;
                isError = WorkerGlobal.SetUpDatabaseConnection("AIM");

                if (isError == false)
                {
                    SettingInfo setting = new SettingInfo();
                    setting = CFCAPIManager.Setting;
                    aimApiKey = setting.AIMAPIKey;
                    Library.WriteErrorLogFile("aimapikey:" + aimApiKey);
                    apiToken = CFCAPIManager.GetTokenAPIUser(aimApiKey);
                    token = apiToken.Token;
                    customerID = apiToken.CustomerID;
                    Library.WriteErrorLogFile("Token:" + token);
                    Library.WriteErrorLogFile("CustomerID:" + customerID.ToString());

                    List<OutsideLabSampleInfo> outsideLabSamples = new List<OutsideLabSampleInfo>();
                    outsideLabSamples = CFCAPIManager.GetOutsideLabSampleNotSent(token);
                    Library.WriteErrorLogFile("GetOutsideLabSampleNotSent" + customerID.ToString());

                    foreach (OutsideLabSampleInfo outsideLabsample in outsideLabSamples)
                    {
                        SampleInfo sample = new SampleInfo();
                        Int32 sampleID = 0;
                        Int32.TryParse(outsideLabsample.AIMCode, out sampleID);
                        if (sampleID != prevSampleID)
                        {
                            sample = AIMManager.GetSample(sampleID);
                            prevSampleID = sampleID;
                        }


                        if (sample == null || sampleID == 0)
                        {
                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "SampleID " + sampleID.ToString() + " is not found and will be Skipped ");
                            error = CFCAPIManager.PutOutsideLabSentLog(token, customerID, outsideLabsample.OutsideLabSampleID, true);

                            if (error.ErrorNumber > 0)
                            {
                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api OutsideLabSentLog - sample=null");
                            }
                        }
                        else
                        {
                            Library.WriteErrorLogFile("Sample Found ");
                            AIMManager.InsertAPILog(sampleID, outsideLabsample.OutsideLabSampleID, outsideLabsample.DataSourceTypeID);

                            List<OutsideLabSampleResultInfo> outsideLabSampleResults = new List<OutsideLabSampleResultInfo>();
                            outsideLabSampleResults = CFCAPIManager.GetOutsideLabSampleResult(token, outsideLabsample.OutsideLabSampleID);

                            DataSourceTypeInfo dataSourceType = new DataSourceTypeInfo();
                            dataSourceType = AIMManager.GetDataSourceType_DataSourceTypeID(outsideLabsample.DataSourceTypeID);
                            if (dataSourceType == null)
                            {
                                dataSourceType = CFCAPIManager.GetDataSourceType(token, outsideLabsample.DataSourceTypeID);
                                if (dataSourceType != null && dataSourceType.DataSourceTypeID > 0)
                                {
                                    error = AIMManager.InsertDataSourceType(ref dataSourceType);
                                }
                            }


                            foreach (OutsideLabSampleResultInfo outsideLabSampleResult in outsideLabSampleResults)
                            {
                                Library.WriteErrorLogFile("OutsideLabSampleResult = " + outsideLabSampleResult.OutsideLabSampleResultID.ToString());

                                String resultID = "OLSAPI" + outsideLabSampleResult.OutsideLabSampleResultID.ToString();  //"OLSAPI" = "OutsideLabSample API"
                                List<SampleResultInfo> checkResult = AIMManager.GetSampleResultForSQLResultID(sampleID, resultID);

                                if (checkResult.Count == 0)
                                {
                                    SampleResultInfo sampleResult = new SampleResultInfo();
                                    Library.WriteErrorLogFile("Add Result " + resultID);
                                    sampleResult.SampleID = sampleID;
                                    sampleResult.AnalyzerNutrientID = 0;
                                    sampleResult.CFCUserID = 0;
                                    sampleResult.AnalyzerNutrientCode = outsideLabSampleResult.AnalyteName;
                                    sampleResult.Unit = outsideLabSampleResult.AnalyteUnit;
                                    sampleResult.ResultValue = (Decimal)outsideLabSampleResult.AnalyteValue;
                                    sampleResult.DataSourceTypeID = dataSourceType?.DataSourceTypeID?? 0;
                                    sampleResult.AimDatabaseID = 0;
                                    sampleResult.SqlResultID = resultID;
                                    AIMManager.InsertSampleResult(sampleResult, null, 0);
                                }
                                else
                                {
                                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Sample " + sampleID.ToString() + "~" + resultID + " result Already in database.");
                                }

                                OutsideLabSampleResultSentInfo outsideLabSampleResultSent = new OutsideLabSampleResultSentInfo();
                                outsideLabSampleResultSent.OutsideLabSampleResultID = outsideLabSampleResult.OutsideLabSampleResultID;
                                error = CFCAPIManager.PutOutsideLabSampleResultSent(token, outsideLabSampleResultSent);

                                if (error.ErrorNumber > 0)
                                {
                                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api OutsideLabSampleResultSent Error... ");
                                }
                            }

                        }
                    }
                }
            }
            catch
            {
                //TODO: Catch Error
            }
        }
    }
}



