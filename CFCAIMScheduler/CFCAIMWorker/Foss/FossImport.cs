using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Foss;
using CFCAIMCore.Entity.CFCAPI;
using CFCAIMCore.Manager;
using CFCAIMWorker.Foss;
using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.IO;
using CFCAIMCore.DataExchange;
using CFCAIMWorker.Setup;
using Core.Library.SQLDatabaseConnection;

namespace CFCAIMWorker
{
    public class FossImport
    {
        //7/22/22
        private static String _databaseXML;
        public static String DatabaseXML { get { return _databaseXML; } set { _databaseXML = value; } }

        public static void SendSampleToAPI()
        {
            List<FossSampleInfo> fossSamplesList = new List<FossSampleInfo>();
            List<APITokenInfo> apiToken = new List<APITokenInfo>();
            List<InstrumentInfo> instrument = new List<InstrumentInfo>();

            //7/22/22
            List<SQLDatabaseInfo> _databases = new List<SQLDatabaseInfo>();

            FossSampleInfo fossHeader = new FossSampleInfo();
            FossResultInfo fossDetailRow = new FossResultInfo();
            ErrorInfo error = new ErrorInfo();
            BatchInfo batch = new BatchInfo();
            FileInfo[] file;
            List<String> resultName;
            Boolean isError = false;
            Boolean isFirst = true;
            String token = String.Empty;
            String aimApiKey = String.Empty;
            String aimInstrumentKey = String.Empty;
            String fossCSV = String.Empty;
            String fossNIR = String.Empty;
            String fossArchive = String.Empty;
            String fossError = String.Empty;
            String from = String.Empty;
            String to = String.Empty;
            String str = String.Empty;
            Int32 customerID = 0;
            Int32 instrumentID = 0;
            Int32 batchID = 0;
            String fossSampleNumber = String.Empty;
            Int32 fossSampleID = 0;
            Int32 csvRow = 0;


            try
            {
                //7/22/22
                SettingInfo setting = new SettingInfo();
//                Utility.LoadConnection(ref setting);
				Library.LoadSetting(ref setting, Utility.SettingXML);

				//7/22/22
				//CFCAPIManager.APIUrl = Properties.Settings.Default.APIUrl;
				//fossCSV = Properties.Settings.Default.FossCSV;
				//fossNIR = Properties.Settings.Default.FossNir;
				//fossArchive = Properties.Settings.Default.FossArchive;
				//fossError = Properties.Settings.Default.FossError;
				//aimApiKey = Properties.Settings.Default.AIMAPIKey;
				//aimInstrumentKey = Properties.Settings.Default.AIMInstrumentKey;

				//CFCAPIManager.APIUrl = setting.APIUrl;
				CFCAPIManager.Setting = setting;

                fossCSV = setting.FossCSVPath;
                fossNIR = setting.FossNIRPath;
                fossArchive = setting.FossArchivePath;
                fossError = setting.FossErrorPath;
                aimApiKey = setting.AIMAPIKey;
                aimInstrumentKey = setting.AIMInstrumentKey;

                if (fossCSV.Length == 0)
                {
                    Library.WriteErrorLog("FossCSV Setting is Missing");
                    isError = true;
                }


                if (fossNIR.Length == 0)
                {
                    Library.WriteErrorLog("FossNIR Setting is Missing");
                    isError = true;
                }
                else
                {
                    DirectoryInfo fossNIRDir = new DirectoryInfo(fossNIR);

                    if (fossNIRDir.Exists == false)
                    {
                        Library.WriteErrorLog(fossNIR + " folder does not exists");
                        isError = true;
                    }
                }

                if (fossArchive.Length == 0)
                {
                    Library.WriteErrorLog("FossCSV Setting is Missing");
                    isError = true;
                }
                else
                {
                    DirectoryInfo fossArchiveDir = new DirectoryInfo(fossArchive);

                    if (fossArchiveDir.Exists == false)
                    {
                        fossArchiveDir.Create();
                        if (fossArchiveDir.Exists == false)
                        {
                            Library.WriteErrorLog(fossArchive + " folder does not exists");
                            isError = true;
                        }
                    }
                }

                if (isError == false)
                {
                    apiToken = CFCAPIManager.GetToken(aimApiKey);

                    if (apiToken == null)
                    {
                        Library.WriteErrorLog("API GetToken fail contact CFCTech");
                        isError = true;
                    }
                    else
                    {
                        if (apiToken.Count == 0)
                        {
                            Library.WriteErrorLog("Bad AIMAPIKey contact CFCTech");
                            isError = true;
                        }
                        else
                        {
                            token = apiToken[0].Token;
                            customerID = apiToken[0].CustomerID;
                        }

                        instrument = CFCAPIManager.GetInstrument(token, aimInstrumentKey);

                        if (instrument.Count == 0)
                        {
                            Library.WriteErrorLog("Bad AIMInstrumentKey contact CFCTech");
                            isError = true;
                        }
                        else
                        {
                            instrumentID = instrument[0].InstrumentID;
                        }
                    }
                }

                if (isError == false)
                {
                    DirectoryInfo fossCSVDir = new DirectoryInfo(fossCSV);

                    if (fossCSVDir.Exists == false)
                    {
                        Library.WriteErrorLog(fossCSV + " folder does not exists");
                        isError = true;
                    }
                    else
                    {
                        file = fossCSVDir.GetFiles(@"*.csv");

                        if (file.Length > 0)
                        {
                            batch.CustomerID = customerID;
                            batch.InstrumentID = instrumentID;
                            batch.Token = token;
                            error = CFCAPIManager.PutBatch(batch);
                            batch.BatchID = error.RecordID;
                            batchID = error.RecordID;
                            isFirst = false;

                            foreach (FileInfo list in file)
                            {
                                resultName = new List<String>();
                                csvRow = 1;

                                using (ReadWriteCSV.CsvFileReader reader = new ReadWriteCSV.CsvFileReader(list.FullName))
                                {
                                    try
                                    {
                                        ReadWriteCSV.CsvRow row = new ReadWriteCSV.CsvRow();

                                        while (reader.ReadRow(row))
                                        {
                                            fossHeader = new FossSampleInfo();
                                            fossHeader.FossResult = new List<FossResultInfo>();

                                            for (Int16 i = 0; i < row.Count; i++)
                                            {
                                                if (csvRow == 1)
                                                {
                                                    resultName.Add(row[i]);
                                                }
                                                else
                                                {
                                                    if (i < 8)
                                                    {
                                                        if (resultName[i].ToUpper() == "ANALYSIS TIME")
                                                        {
                                                            fossHeader.AnalysisTime = DateTime.Parse(row[i]);
                                                        }
                                                        else if (resultName[i].ToUpper() == "PRODUCTNAME")
                                                        {
                                                            fossHeader.ProductName = row[i];
                                                        }
                                                        else if (resultName[i].ToUpper() == "PRODUCTCODE")
                                                        {
                                                            fossHeader.ProductCode = row[i];
                                                        }
                                                        else if (resultName[i].ToUpper() == "SAMPLE_TYPE")
                                                        {
                                                            fossHeader.SampleType = row[i];
                                                        }
                                                        else if (resultName[i].ToUpper() == "SAMPLENUMBER")
                                                        {
                                                            fossHeader.SampleNumber = row[i];
                                                        }
                                                        else if (resultName[i].ToUpper() == "COMMENT")
                                                        {
                                                            fossHeader.Comment = row[i];
                                                        }
                                                        else if (resultName[i].ToUpper() == "INSTRUMENTNAME")
                                                        {
                                                            fossHeader.InstrumentName = row[i];
                                                        }
                                                        else if (resultName[i].ToUpper() == "INSTRUMENTSERIALNUMBER")
                                                        {
                                                            fossHeader.InstrumentSerialNumber = row[i];
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (row[i].Length > 0)
                                                        {
                                                            fossDetailRow = new FossResultInfo();
                                                            fossDetailRow.FossSampleID = csvRow;
                                                            fossDetailRow.ResultName = resultName[i];
                                                            fossDetailRow.ResultValue = Double.Parse(row[i]);
                                                            fossHeader.FossResult.Add(fossDetailRow);
                                                        }
                                                    }
                                                }
                                            }
                                            csvRow += 1;
                                        }
                                        fossHeader.BatchID = batch.BatchID;
                                        fossHeader.Token = token;
                                        error = CFCAPIManager.PutFossSample(fossHeader);
                                    }
                                    catch (Exception ex)
                                    {
                                        Library.WriteErrorLog(ex.Message + " contact CFCTech");
                                    }
                                }

                                if (error.ErrorNumber == 0)
                                {
                                    error = FileTool.MoveFile(list, fossArchive);
                                }
                                else
                                {
                                    error = FileTool.MoveFile(list, fossError);
                                }
                            }
                        }
                    }

                    //Upload NIR
                    DirectoryInfo fossNIRDir = new DirectoryInfo(fossNIR);

                    if (fossNIRDir.Exists == false)
                    {
                        Library.WriteErrorLog(fossNIR + " folder does not exists");
                        isError = true;
                    }
                    else
                    {
                        file = fossNIRDir.GetFiles(@"*.nir");

                        if (file.Length > 0)
                        {
                            if (isFirst == true)
                            {
                                batch.CustomerID = customerID;
                                batch.InstrumentID = instrumentID;
                                batch.Token = token;
                                error = CFCAPIManager.PutBatch(batch);
                                batch.BatchID = error.RecordID;
                                batchID = error.RecordID;
                            }

                            foreach (FileInfo list in file)
                            {
                                try
                                {
                                    fossSampleNumber = list.Name.Replace(list.Extension, "");

                                    if (fossSampleNumber.Length == 0)
                                    {
                                        error = FileTool.MoveFile(list, fossError);
                                    }
                                    else
                                    {
                                        fossSamplesList = CFCAPIManager.GetFossSample(customerID, batch.BatchID, fossSampleNumber, token);

                                        if (fossSamplesList.Count == 0)
                                        {
                                            error = FileTool.MoveFile(list, fossError);
                                        }
                                        else
                                        {
                                            fossSampleID = fossSamplesList[0].FossSampleID;
                                            FossSpectrumInfo fossSpectrumFile = new FossSpectrumInfo();
                                            fossSpectrumFile.Token = token;
                                            fossSpectrumFile.FossSampleID = fossSampleID;
                                            fossSpectrumFile.CreateStamp = DateTime.Now;
                                            byte[] byteFile = System.IO.File.ReadAllBytes(list.FullName);
                                            List<byte> byteList = new List<byte>();
                                            for (int i = 0; i < byteFile.Length; i++)
                                            {
                                                byteList.Add(byteFile[i]);
                                            }

                                            fossSpectrumFile.FossSpectrum = byteFile;
                                            str = System.Text.Encoding.Default.GetString(byteFile, 0, byteFile.Length);
                                            str = Convert.ToBase64String(byteFile);
                                            error = CFCAPIManager.PutFossSpectrumFile(fossSpectrumFile);

                                            if (error.ErrorNumber == 0)
                                            {
                                                CFCAPIManager.PutFossSpectrumReceive(error.RecordID, token);
                                                error = FileTool.MoveFile(list, fossArchive);
                                            }
                                            else
                                            {
                                                error = FileTool.MoveFile(list, fossError);
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Library.WriteErrorLog(ex.Message + " contact CFCTech");
                                }
                            }
                        }
                    }

                    if (isFirst == false)
                    {
                        batch = new BatchInfo();
                        batch.Token = token;
                        batch.BatchID = batchID;
                        error = CFCAPIManager.PutBatchEnd(batch);
                    }

                }
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex.Message);
            }
        }

        public static void GetSampleFromAPI()
        {
            List<AnalyzerPendingSampleInfo> analyzerPendingSample = new List<AnalyzerPendingSampleInfo>();
            List<AnalyzerPendingSampleInfo> pendingSample = new List<AnalyzerPendingSampleInfo>();
            List<FossSampleInfo> fossSampleList = new List<FossSampleInfo>();
            List<FossSampleResultInfo> fossSampleResults = new List<FossSampleResultInfo>();
            List<APITokenInfo> apiToken = new List<APITokenInfo>();
            List<CustomerInfo> customer = new List<CustomerInfo>();
            List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
            List<SampleResultInfo> checkResult = new List<SampleResultInfo>();
            List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
            List<String> analysesCodes = new List<string>();            
            List<ImportConfigInfo> importConfig = new List<ImportConfigInfo>();
            List<AnalyzerMaterialInfo> analyzerMaterial = new List<AnalyzerMaterialInfo>();
            FossSampleResultSentInfo fossSampleResultSent = new FossSampleResultSentInfo();
            AnalyzerPendingSampleInfo analyzerPending = new AnalyzerPendingSampleInfo();
            AIMMaterialInfo aimMaterial = new AIMMaterialInfo();
            FilesCountInfo filesCount = new FilesCountInfo();
            SampleInfo sample = new SampleInfo();
            SampleResultInfo sampleResult = new SampleResultInfo();

            //8/1/22
            List<SQLDatabaseInfo> _databases = new List<SQLDatabaseInfo>();

            ErrorInfo error = new ErrorInfo();
            Int32 sampleID = 0;
            Int32 customerID = 0;
            String token = String.Empty;
            String argument = String.Empty;
            String sql = String.Empty;
            String fileName = String.Empty;
            String sampleFile = String.Empty;
            String resultID = String.Empty;
            String aimApiKey = String.Empty;
            Boolean isError = false;
            FilesInfo files = new FilesInfo();

            try
            {

                //7/22/22
                SettingInfo setting = new SettingInfo();
//                Utility.LoadConnection(ref setting);
				Library.LoadSetting(ref setting, Utility.SettingXML);
				//7/22/22
				//CFCAPIManager.APIUrl = Properties.Settings.Default.APIUrl;
				//aimApiKey = Properties.Settings.Default.AIMAPIKey;

				//CFCAPIManager.APIUrl = setting.APIUrl;
				CFCAPIManager.Setting = setting;
                aimApiKey = setting.AIMAPIKey;


                apiToken = CFCAPIManager.GetToken(aimApiKey);

                if (apiToken == null)
                {
                    Library.WriteErrorLog("API GetToken fail contact CFCTech");
                    isError = true;
                }
                else
                {
                    if (apiToken.Count == 0)
                    {
                        Library.WriteErrorLog("Bad AIMAPIKey contact CFCTech");
                        isError = true;
                    }
                    else
                    {
                        token = apiToken[0].Token;
                        customerID = apiToken[0].CustomerID;
                    }
                }

                if (isError == false)
                {
                    if (customerID > 0)
                    {
                        customer = CFCAPIManager.GetCustomer(token, customerID);
                        APIManager.Token = customer[0].PNEKey;
                        isError = AdisseoAPI.AnalyseCode();

                        if (isError == false)
                        {
                            isError = AdisseoAPI.MaterialCode();
                        }

                        //if (Environment.MachineName == "CFC-1")
                        //{
                        //    //7/22/22
                        //    //CFCAPIManager.APIUrl = Properties.Settings.Default.APIUrlTest;
                        //    CFCAPIManager.APIUrl = setting.APIUrlTest;
                        //}
                        //else
                        //{
                        //    //7/22/22
                        //    //CFCAPIManager.APIUrl = Properties.Settings.Default.APIUrl;
                        //    CFCAPIManager.APIUrl = setting.APIUrl;
                        //}

                        CFCAPIManager.Setting = setting;

                        BrukerManager.IsMessageDisplay = false;
                        // BrukerManager.CreateBrukerConnection(Properties.Settings.Default.Server, Properties.Settings.Default.AIMDatabase, true, Properties.Settings.Default.User, Properties.Settings.Default.Pwd);
                        // Get Bruker Sample from API. Hardcoded for BrukerSampleID 10 right now.
                        fossSampleList = CFCAPIManager.GetFossSampleNotSentResult(token, customerID);

                        if (fossSampleList == null)
                        {
                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api is not working - fossSampleList=null");
                        }
                        else
                        {

                            //8/1/22 - get AIMDatabaseID for insert into sampleResult below
                            _databases = new List<SQLDatabaseInfo>();
                            SQLDatabaseConnection.LoadConnection(ref _databases, true, _databaseXML);

                            if (fossSampleList.Count > 0)
                            {
                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "Start get foss result data");
                            }

                            foreach (FossSampleInfo item in fossSampleList)
                            {
                                if (item == null)
                                {
                                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api is not working - item=null");
                                }
                                else
                                {
                                    Int32.TryParse(item.SampleNumber, out sampleID);

                                    if (sampleID == 0)
                                    {
                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Sample ID " + item.SampleNumber + " is not numeric and will be skip");
                                        error = CFCAPIManager.PutFossSentLog(token, customerID, sampleID, true);

                                        if (error.ErrorNumber > 0)
                                        {
                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api FossSentLog - sampleID=0");
                                        }
                                    }
                                    else
                                    {
                                        sample = AIMManager.GetSample(sampleID);

                                        if (sample == null)
                                        {
                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "SampleID " + sampleID.ToString() + " is not found and will be Skip ");
                                            error = CFCAPIManager.PutFossSentLog(token, customerID, sampleID, true);

                                            if (error.ErrorNumber > 0)
                                            {
                                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api FossSentLog - sample=null");
                                            }
                                        }
                                        else
                                        {
                                            fossSampleResults = CFCAPIManager.GetFossSampleResultNotSent(token, customerID, sampleID);
                                            if (fossSampleResults == null)
                                            {
                                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api did not return sample result.");
                                            }
                                            else
                                            {

                                                //8/1/22
                                                //Insert into APILog
                                                AIMManager.InsertAPILog(sample.SampleID, fossSampleResults[0].FossSampleID, (Int32)DataSourceTypeEnum.Foss);

                                                foreach (FossSampleResultInfo result in fossSampleResults)
                                                {
                                                    resultID = "CFCAPIFoss" + result.FossSampleResultID.ToString();
                                                    checkResult = AIMManager.GetSampleResultForSQLResultID(sampleID, resultID);
                                                    if (checkResult.Count == 0)
                                                    {
                                                        sampleResult.SampleID = sampleID;
                                                        sampleResult.AnalyzerNutrientCode = result.ResultName;
                                                        sampleResult.Unit = "";
                                                        sampleResult.ResultValue = result.ResultValue;

                                                        //8/1/22
                                                        //sampleResult.DataSourceTypeID = 4;    //TODO REPLACE                                                                                                 
                                                        sampleResult.DataSourceTypeID = (Int32)DataSourceTypeEnum.Foss;

                                                        //8/1/22
                                                        //sampleResult.AimDatabaseID = 1;       //TODO REPLACE 
                                                        sampleResult.AimDatabaseID = _databases[0].DatabaseID;

                                                        sampleResult.SqlResultID = resultID;

                                                        //8/17/22
                                                        //AIMManager.InsertSampleResult(sampleResult, null);
                                                        AIMManager.InsertSampleResult(sampleResult, null, 0);

                                                        fossSampleResultSent = new FossSampleResultSentInfo();
                                                        fossSampleResultSent.CustomerID = customerID;
                                                        fossSampleResultSent.FossSampleResultID = result.FossSampleResultID;
                                                        CFCAPIManager.PutFossSampleResultSent(fossSampleResultSent);
                                                    }
                                                    else
                                                    {
                                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Sample " + sampleID.ToString() + "~" + resultID + " result Already in database.");
                                                        error = CFCAPIManager.PutFossSentLog(token, customerID, item.FossSampleID, true);

                                                        if (error.ErrorNumber > 0)
                                                        {
                                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api FossSentLog - checkResult.Count>0");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            //if (FossSampleList.Count > 0)
                            //{
                            //    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "End get bruker result data");
                            //}
                        }

                    }


                    //Spectrum Section

                    //AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "Start get bruker spectrum data");
                    //brukerSampleList = CFCAPIManager.GetBrukerSampleNotSentSpectrum(customerID);

                    //if (brukerSampleList == null)
                    //{
                    //    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSampleNotSentSpectrum Api is not working");
                    //}
                    //else
                    //{
                    //    if (brukerSampleList.Count > 0)
                    //    {
                    //        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "Start send spectrum data");
                    //    }

                    //    foreach (BrukerSampleInfo item in brukerSampleList)
                    //    {
                    //        Int32.TryParse(item.ValueProductInfo1, out sampleID);

                    //        if (sampleID == 0)
                    //        {
                    //            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Sample ID " + item.ValueProductInfo1 + " is not numeric and will be skip");
                    //            error = CFCAPIManager.PutBrukerSentLog(customerID, item.BrukerSampleID, true);

                    //            if (error.ErrorNumber > 0)
                    //            {
                    //                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api BrukerSentLog");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            sample = AIMManager.GetSample(sampleID);

                    //            if (sample == null)
                    //            {
                    //                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Sample ID " + item.ValueProductInfo1 + " was not found in AIM and will be skip");

                    //                brukerSpectrumFile = CFCAPIManager.GetBrukerSpectrumFile(item.BrukerSampleID);
                    //                if (brukerSpectrumFile is null)
                    //                {
                    //                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Sample ID " + item.ValueProductInfo1 + " does not have a spectrum file");
                    //                }
                    //                else
                    //                {
                    //                    brukerSpectrumSent = new BrukerSpectrumSentInfo();
                    //                    brukerSpectrumSent.CustomerID = customerID;
                    //                    brukerSpectrumSent.BrukerSpectrumID = brukerSpectrumFile[0].BrukerSpectrumID;
                    //                    brukerSpectrumSent.IsSkip = 1;
                    //                    CFCAPIManager.PutBrukerSpectrumSent(brukerSpectrumSent);
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (sample.FurtherAnalysisID == (Int32)DataSourceTypeEnum.AdisseoAPI)
                    //                {
                    //                    aimMaterialID = sample.AIMMaterialID;
                    //                    aimMaterial = AIMManager.GetAIMMaterial(aimMaterialID, true);

                    //                    if (aimMaterial == null)
                    //                    {
                    //                        brukerSpectrumFile = CFCAPIManager.GetBrukerSpectrumFile(item.BrukerSampleID);

                    //                        if (brukerSpectrumFile == null)
                    //                        {
                    //                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum Api is not working");
                    //                        }
                    //                        else
                    //                        {
                    //                            brukerSpectrumSent = new BrukerSpectrumSentInfo();
                    //                            brukerSpectrumSent.CustomerID = customerID;
                    //                            brukerSpectrumSent.BrukerSpectrumID = brukerSpectrumFile[0].BrukerSpectrumID;
                    //                            brukerSpectrumSent.IsSkip = 1;
                    //                            CFCAPIManager.PutBrukerSpectrumSent(brukerSpectrumSent);
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        brukerSpectrumFile = CFCAPIManager.GetBrukerSpectrumFile(item.BrukerSampleID);

                    //                        if (brukerSpectrumFile == null)
                    //                        {
                    //                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum Api is not working");
                    //                        }
                    //                        else
                    //                        {
                    //                            if (brukerSpectrumFile.Count > 0)
                    //                            {
                    //                                analyzerMaterial = AIMManager.GetAnalyzerMaterial(DataSourceTypeEnum.AdisseoAPI, sample.AIMMaterialID);

                    //                                if (analyzerMaterial.Count == 0)
                    //                                {
                    //                                    apiPendingAIMMaterial = AIMManager.GetAPIPendingAIMMaterial(sample.AIMMaterialID, (Int32)DataSourceTypeEnum.AdisseoAPI, true);

                    //                                    if (apiPendingAIMMaterial is null)
                    //                                    {
                    //                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum a cross reference need to be setup for AdisseoAPI for " + sample.AIMMaterialCode);
                    //                                        apiPendingAIMMaterial = new APIPendingAIMMaterialInfo();
                    //                                        apiPendingAIMMaterial.APIPendingAIMMaterialID = 0;
                    //                                        apiPendingAIMMaterial.AIMMaterialID = sample.AIMMaterialID;
                    //                                        apiPendingAIMMaterial.DataSourceTypeID = (Int32)DataSourceTypeEnum.AdisseoAPI;
                    //                                        apiPendingAIMMaterial.IsActive = 1;
                    //                                        apiPendingAIMMaterial.CFCUserID = 0;
                    //                                        error = AIMManager.SaveAPIPendingAIMMaterial(ref apiPendingAIMMaterial);
                    //                                    }
                    //                                }
                    //                                else
                    //                                {
                    //                                    analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)DataSourceTypeEnum.AdisseoAPI, true);
                    //                                    analysesCodes = new List<string>();
                    //                                    for (int i = 0; i < analyzerNutrient.Count; i++)
                    //                                    {
                    //                                        if (analyzerNutrient[i].AnalyzerNutrientCode != "STTP")
                    //                                        {
                    //                                            analysesCodes.Add(analyzerNutrient[i].AnalyzerNutrientCode);
                    //                                        }
                    //                                    }

                    //                                    newAnalysisRequest.materialCode = analyzerMaterial[0].AnalyzerMaterialCode;  // "COR";
                    //                                    newAnalysisRequest.analysesCodes = analysesCodes;
                    //                                    newAnalysisRequest.sampleCode = sample.SampleID.ToString();
                    //                                    newAnalysisRequest.description = sample.SampleCode;
                    //                                    newAnalysisRequest.spectraFileName = item.FileName;
                    //                                    // convert file byte to base 64
                    //                                    newAnalysisRequest.spectraFile = Convert.ToBase64String(brukerSpectrumFile[0].BrukerSpectrum);
                    //                                    analysisRequest = APIManager.CreateAnalysisRequest(newAnalysisRequest);

                    //                                    analyzerPending = new AnalyzerPendingSampleInfo();
                    //                                    analyzerPending.SampleID = sampleID;
                    //                                    analyzerPending.RequestCode = analysisRequest.Id;
                    //                                    analyzerPending.SentStamp = DateTime.Now;
                    //                                    analyzerPending.SpectraName = item.FileName;
                    //                                    AIMManager.SaveAnalyzerPendingSample(ref analyzerPending);

                    //                                    brukerSpectrumSent = new BrukerSpectrumSentInfo();
                    //                                    brukerSpectrumSent.CustomerID = customerID;
                    //                                    brukerSpectrumSent.BrukerSpectrumID = brukerSpectrumFile[0].BrukerSpectrumID;
                    //                                    brukerSpectrumSent.IsSkip = 0;
                    //                                    CFCAPIManager.PutBrukerSpectrumSent(brukerSpectrumSent);
                    //                                }
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum 3");
                    //                    brukerSpectrumFile = CFCAPIManager.GetBrukerSpectrumFile(item.BrukerSampleID);

                    //                    if (brukerSpectrumFile == null)
                    //                    {
                    //                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum Api is not working");
                    //                    }
                    //                    else
                    //                    {
                    //                        brukerSpectrumSent = new BrukerSpectrumSentInfo();
                    //                        brukerSpectrumSent.CustomerID = customerID;
                    //                        brukerSpectrumSent.BrukerSpectrumID = brukerSpectrumFile[0].BrukerSpectrumID;
                    //                        brukerSpectrumSent.IsSkip = 1;
                    //                        CFCAPIManager.PutBrukerSpectrumSent(brukerSpectrumSent);
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }

                    //    if (brukerSampleList.Count > 0)
                    //    {
                    //        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "End send spectrum data");
                    //    }
                    //}

                    AdisseoAPI.GettingRequest();

                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "End further analyses");
                }
            }
            catch (Exception)
            {
                //ApplicationLog.WriteLog(ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
