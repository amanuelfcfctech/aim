using CFCAIMCore.Entity;
using CFCAIMCore.Entity.CFCAPI;
using CFCAIMCore.Entity.Foss;
using CFCAIMCore.Manager;
using CFCAIMWorker.Setup;
using CFCAPICore.Entity;
using CFCWebAPI.Entity.Foss;
using Core.Library.SQLDatabaseConnection;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CFCAIMWorker
{
    public class FossImport
    {
        //DB250424 Reserved for when we send Foss Spectrum when we decide to add that functionality
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
                setting = CFCAPIManager.Setting;

                fossCSV = setting.FossCSVPath;
                fossNIR = setting.FossNIRPath;
                fossArchive = setting.FossArchivePath;
                fossError = setting.FossErrorPath;
                aimApiKey = setting.AIMAPIKey;
                aimInstrumentKey = setting.AIMInstrumentKey;

                if (fossCSV.Length == 0)
                {
                    Library.WriteErrorLogFile("FossCSV Setting is Missing");
                    isError = true;
                }


                if (fossNIR.Length == 0)
                {
                    Library.WriteErrorLogFile("FossNIR Setting is Missing");
                    isError = true;
                }
                else
                {
                    DirectoryInfo fossNIRDir = new DirectoryInfo(fossNIR);

                    if (fossNIRDir.Exists == false)
                    {
                        Library.WriteErrorLogFile(fossNIR + " folder does not exists");
                        isError = true;
                    }
                }

                if (fossArchive.Length == 0)
                {
                    Library.WriteErrorLogFile("FossCSV Setting is Missing");
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
                            Library.WriteErrorLogFile(fossArchive + " folder does not exists");
                            isError = true;
                        }
                    }
                }

                if (isError == false)
                {
                    apiToken = CFCAPIManager.GetToken(aimApiKey);

                    if (apiToken == null)
                    {
                        Library.WriteErrorLogFile("API GetToken fail contact CFCTech");
                        isError = true;
                    }
                    else
                    {
                        if (apiToken.Count == 0)
                        {
                            Library.WriteErrorLogFile("Bad AIMAPIKey contact CFCTech");
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
                            Library.WriteErrorLogFile("Bad AIMInstrumentKey contact CFCTech");
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
                        Library.WriteErrorLogFile(fossCSV + " folder does not exists");
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

                                fossHeader = new FossSampleInfo();
                                fossHeader.BatchID = batch.BatchID;
                                fossHeader.Token = token;

                                error = CFCAPIManager.PutFossSample(fossHeader);


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
                        Library.WriteErrorLogFile(fossNIR + " folder does not exists");
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
                                    Library.WriteErrorLogFile(ex.Message + " contact CFCTech");
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
                Library.WriteErrorLogFile(ex.Message);
            }
        }

        public static void SendCSVResultSampleToAPI()
        {
            List<FossSampleInfo> fossSamplesList = new List<FossSampleInfo>();
            List<APITokenInfo> apiToken = new List<APITokenInfo>();
            List<InstrumentInfo> instrument = new List<InstrumentInfo>();

            FossSampleInfo fossHeader = new FossSampleInfo();
            FossResultInfo fossDetailRow = new FossResultInfo();
            FossCSVResultInfo fossCsvResult = new FossCSVResultInfo();

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
                setting = CFCAPIManager.Setting;

                fossCSV = setting.FossCSVPath;
                fossNIR = setting.FossNIRPath;
                fossArchive = setting.FossArchivePath;
                fossError = setting.FossErrorPath;
                aimApiKey = setting.AIMAPIKey;
                aimInstrumentKey = setting.AIMInstrumentKey;

                if (fossCSV.Length == 0)
                {
                    Library.WriteErrorLogFile("FossCSV Setting is Missing");
                    isError = true;
                }


                if (fossNIR.Length == 0)
                {
                    Library.WriteErrorLogFile("FossNIR Setting is Missing");
                    isError = true;
                }
                else
                {
                    DirectoryInfo fossNIRDir = new DirectoryInfo(fossNIR);

                    if (fossNIRDir.Exists == false)
                    {
                        Library.WriteErrorLogFile(fossNIR + " folder does not exists");
                        isError = true;
                    }
                }

                if (fossArchive.Length == 0)
                {
                    Library.WriteErrorLogFile("Foss Archive folder is Missing");
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
                            Library.WriteErrorLogFile(fossArchive + " folder does not exists");
                            isError = true;
                        }
                    }
                }

                if (isError == false)
                {
                    apiToken = CFCAPIManager.GetToken(aimApiKey);

                    if (apiToken == null)
                    {
                        Library.WriteErrorLogFile("API GetToken fail contact CFCTech");
                        isError = true;
                    }
                    else
                    {
                        if (apiToken.Count == 0)
                        {
                            Library.WriteErrorLogFile("Bad AIMAPIKey contact CFCTech");
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
                            Library.WriteErrorLogFile("Bad AIMInstrumentKey contact CFCTech");
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
                        Library.WriteErrorLogFile(fossCSV + " folder does not exists");
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
                                try
                                {
                                    fossHeader.BatchID = batch.BatchID;
                                    fossHeader.Token = token;
                                    error = CFCAPIManager.PutFossSample(fossHeader);

                                    if (error != null)
                                    {
                                        try
                                        {
                                            fossCsvResult.FossFileName = list.Name;
                                            byte[] byteFile = File.ReadAllBytes(list.FullName);
                                            fossCsvResult.FossCSVResult = byteFile;
                                            fossCsvResult.Token = token;
                                            fossCsvResult.FossSampleID = error.RecordID;
                                            fossCsvResult.CreateStamp = DateTime.Now;
                                            error = CFCAPIManager.PutFossCSVResult(fossCsvResult);
                                            if (error.ErrorNumber == 0)
                                            {
                                                fossCsvResult.FossCSVResultID = error.RecordID;
                                                error = CFCAPIManager.PutFossCSVResultReceive(fossCsvResult);
                                            }
                                            else
                                            {
                                                Library.WriteErrorLogFile("PutFossCSVResult fail. Contact CFC.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Library.WriteErrorLogFile(ex.Message + " PutFossCSV fail. Contact CFC.");
                                        }

                                    }
                                    else
                                    {
                                        Library.WriteErrorLogFile("PutFossSample is null. Contact CFC.");
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Library.WriteErrorLogFile(ex.Message + " PutFossSample fail. Contact CFC.");
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
                Library.WriteErrorLogFile(ex.Message);
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
            List<FossSpectrumInfo> fossSpectrumList = new List<FossSpectrumInfo>();
            FossSampleResultSentInfo fossSampleResultSent = new FossSampleResultSentInfo();
            FossSpectrumSentInfo fossSpectrumSent = new FossSpectrumSentInfo();
            AnalyzerPendingSampleInfo analyzerPending = new AnalyzerPendingSampleInfo();
            AIMMaterialInfo aimMaterial = new AIMMaterialInfo();
            FilesCountInfo filesCount = new FilesCountInfo();
            SampleInfo sample = new SampleInfo();
            SampleResultInfo sampleResult = new SampleResultInfo();
            APIPendingAIMMaterialInfo apiPendingAIMMaterial = new APIPendingAIMMaterialInfo();
            AnalysisRequestParameterInfo newAnalysisRequest = new AnalysisRequestParameterInfo();
            AnalysisRequestInfo analysisRequest = new AnalysisRequestInfo();
            List<InstrumentInfo> instrument = new List<InstrumentInfo>();
            List<FossCSVResultHeaderInfo> fossCSVResult = new List<FossCSVResultHeaderInfo>();
            FossCSVResultSentInfo fossCSVResultSent = new FossCSVResultSentInfo();
            String fossCSVFileName = String.Empty;
            FossSampleInfo fossHeader = new FossSampleInfo();
            List<String> resultName = new List<String>();       //todo remove probably
            List<String> resultRow = new List<String>();        //todo remove probably
            List<List<String>> csvResult = new List<List<String>>();
            FossResultInfo fossDetailRow = new FossResultInfo();
            ErrorInfo error = new ErrorInfo();
            FilesInfo files = new FilesInfo();

            Int32 csvRow = new Int32();
            Int32 startIndex = 0;
            Int32 endIndex = 0;
            Int32 sampleIDIndex = -1;
            Boolean isFirstRow = false;
            Decimal resultValue = new Decimal();
            List<String> nutrientName = new List<String>();
            String fossArchive = String.Empty;
            DateTime holdDate = DateTime.Now;
            Int32 sampleID = 0;
            Int32 customerID = 0;
            String token = String.Empty;
            String argument = String.Empty;
            String sql = String.Empty;
            String fileName = String.Empty;
            String sampleFile = String.Empty;
            String resultID = String.Empty;
            String sqlResultID = String.Empty;
            String aimApiKey = String.Empty;
            Boolean isError = false;
            Boolean isAbort = false;
            
            try
            {
                SettingInfo setting = new SettingInfo();
                setting = CFCAPIManager.Setting;
                aimApiKey = setting.AIMAPIKey;
                apiToken = CFCAPIManager.GetToken(aimApiKey);

                if (apiToken == null)
                {
                    Library.WriteErrorLogFile("API GetToken fail contact CFCTech");
                    isError = true;
                }
                else
                {
                    if (apiToken.Count == 0)
                    {
                        Library.WriteErrorLogFile("Bad AIMAPIKey contact CFCTech");
                        isError = true;
                    }
                    else
                    {
                        token = apiToken[0].Token;
                        customerID = apiToken[0].CustomerID;
                    }

                    if (isError == false)
                    {
                        Library.WriteErrorLogFile("Loaded SQL connection setting. CustomerID = " + customerID.ToString());
                        if (customerID > 0)
                        {
                            WorkerGlobal.SetUpDatabaseConnection("AIM"); //8/11/25 farag

                            instrument = CFCAPIManager.GetInstrument(token, customerID);
                            APIManager.Token = instrument[0].PNEKey;

                            CFCAPIManager.Setting = setting;

                            fossCSVResult = CFCAPIManager.GetFossCSVResultNotSent(token, customerID);
                            if (fossCSVResult == null)
                            {
                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api did not return sample result.");
                            }
                            else
                            {
                                Library.WriteErrorLogFile("FossCSVResult Count = " + fossCSVResult.Count.ToString());
                                foreach (FossCSVResultHeaderInfo result in fossCSVResult)
                                {
                                    FossCSVResultByteInfo csvByte = CFCAPIManager.GetFossCSVResultByte(token, result.FossSampleID, result.FossCSVResultID);
                                    //FossSampleID = 205
                                    //fosscsvresultid = 205

                                    if (csvByte == null)
                                    {
                                        Library.WriteErrorLogFile("csvByte is null ");
                                    }
                                    else
                                    {
                                        resultID = result.FossCSVResultID.ToString();
                                        fossArchive = setting.FossArchivePath;
                                        fossCSVFileName = fossArchive + "\\" + resultID + ".csv";

                                        Library.WriteErrorLogFile("Writing fossCSVFile. FileName = " + fossCSVFileName);

                                        if (csvByte.FossCSVResult == null)
                                        {

                                            isError = true;
                                            Library.WriteErrorLogFile("FossCSVResult byte is null.  FossCSVResultID = " + result.FossCSVResultID.ToString());
                                            fossCSVResultSent = new FossCSVResultSentInfo();
                                            fossCSVResultSent.CustomerID = customerID;
                                            fossCSVResultSent.FossCSVResultID = result.FossCSVResultID;
                                            fossCSVResultSent.Token = token;
                                            fossCSVResultSent.IsSkip = 1;
                                            CFCAPIManager.PutFossCSVResultSent(fossCSVResultSent);
                                            error = CFCAPIManager.PutFossSentLog(token, customerID, sampleID, true);

                                            if (error == null)
                                            {
                                                isAbort = true;
                                            }
                                            else
                                            {
                                                if (error.ErrorNumber > 0)
                                                {
                                                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api FossSentLog - sample=null");
                                                }
                                            }

                                        }
                                        else
                                        {

                                            File.WriteAllBytes(fossCSVFileName, csvByte.FossCSVResult);

                                           // fossCSVFileName = "E:\\WorkingDoc\\Elmira\\102796.csv";       //DB This is for testing 

                                            resultName = new List<String>();
                                            List<String> row = new List<String>();
                                            csvRow = 1;
                                            using (StreamReader reader = new StreamReader(fossCSVFileName))
                                            {
                                                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                                                {
                                                    csvResult = new List<List<String>>();
                                                    var records = new List<dynamic>();
                                                    records = csv.GetRecords<dynamic>().ToList();
                                                   
                                                    if (0 < records.Count)
                                                    {
                                                        var rowCSV = (IDictionary<string, object>)records[0];
                                                        csvResult.Add(new List<String>());
                                                        csvResult.Add(new List<String>());
                                                        foreach (var kvp in rowCSV)
                                                        {
                                                            csvResult[0].Add($"{kvp.Key}");
                                                            csvResult[1].Add($"{kvp.Value}");                                                        
                                                        }
                                                    }
                                                    else
                                                    {
                                                        isAbort = true;
                                                    }

                                                }
                                            }

                                            try
                                            {
                                                if (isAbort == false)
                                                {
                                                    isFirstRow = true;
                                                    isAbort = false;
                                                    endIndex = 0;

                                                    fossHeader = new FossSampleInfo();
                                                    sampleResult = new SampleResultInfo();
                                                    fossHeader.FossResult = new List<FossResultInfo>();
                                                    isError = false;
                                                    sampleID = 0;
                                                    for (int h = 0; h < csvResult.Count; h++)
                                                    {
                                                        if (isFirstRow == true)
                                                        {
                                                            for (int i = 0; i < csvResult[0].Count; i++)
                                                            {

                                                                if (csvResult[0][i].ToUpper().Contains("BIN(S)#") == true && endIndex == 0)
                                                                {
                                                                    //Task1104 DB250929 endIndex = i - 1;
                                                                    endIndex = i;                       //Task1104 DB250929
                                                                }
                                                                else if (csvResult[0][i].ToUpper().Contains("1.") == true && endIndex == 0)
                                                                {
                                                                    //Task1104 DB250929 endIndex = i - 1;
                                                                    endIndex = i;                       //Task1104 DB250929
                                                                }
                                                                else if (csvResult[0][i].ToUpper() == "SAMPLE COMMENT")
                                                                {
                                                                    sampleIDIndex = i;
                                                                }
                                                                else if (csvResult[0][i].ToUpper() == "INSTRUMENT SERIAL NUMBER")
                                                                {
                                                                    startIndex = i + 1;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (sampleIDIndex == -1 && isError == false)
                                                            {
                                                                isError = true;
                                                                Library.WriteErrorLogFile("SampleIDIndex is zero.  FossCSVResultID = " + result.FossCSVResultID.ToString());
                                                                fossCSVResultSent = new FossCSVResultSentInfo();
                                                                fossCSVResultSent.CustomerID = customerID;
                                                                fossCSVResultSent.FossCSVResultID = result.FossCSVResultID;
                                                                fossCSVResultSent.Token = token;
                                                                fossCSVResultSent.IsSkip = 1;
                                                                CFCAPIManager.PutFossCSVResultSent(fossCSVResultSent);
                                                                error = CFCAPIManager.PutFossSentLog(token, customerID, sampleID, true);

                                                                if (error == null)
                                                                {
                                                                    isAbort = true;
                                                                }
                                                                else
                                                                {
                                                                    if (error.ErrorNumber > 0)
                                                                    {
                                                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api FossSentLog - sample=null");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Int32.TryParse(csvResult[1][sampleIDIndex], out sampleID);
                                                                Library.WriteErrorLogFile("SampleComment (sampleID) = " + csvResult[1][sampleIDIndex].ToString());
                                                                holdDate = DateTime.Now;

                                                                if (csvResult[1][0].Length > 0)
                                                                {
                                                                    DateTime.TryParse(csvResult[1][0].ToString(), out holdDate);
                                                                    if (holdDate == DateTime.MinValue)
                                                                    {
                                                                        holdDate = DateTime.Now;
                                                                    }
                                                                }

                                                                Library.WriteErrorLogFile("sampleID = " + sampleID.ToString());
                                                                if (sampleID == 0)
                                                                {
                                                                    isError = true;
                                                                    Library.WriteErrorLogFile("SampleID is zero.  FossCSVResultID = " + result.FossCSVResultID.ToString());
                                                                    fossCSVResultSent = new FossCSVResultSentInfo();
                                                                    fossCSVResultSent.CustomerID = customerID;
                                                                    fossCSVResultSent.FossCSVResultID = result.FossCSVResultID;
                                                                    fossCSVResultSent.Token = token;
                                                                    fossCSVResultSent.IsSkip = 1;
                                                                    CFCAPIManager.PutFossCSVResultSent(fossCSVResultSent);
                                                                    error = CFCAPIManager.PutFossSentLog(token, customerID, sampleID, true);

                                                                    if (error == null)
                                                                    {
                                                                        isAbort = true;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (error.ErrorNumber > 0)
                                                                        {
                                                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api FossSentLog - sample=null");
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    sample = AIMManager.GetSample(sampleID);



                                                                    if (sample == null)
                                                                    {
                                                                        isError = true;
                                                                        Library.WriteErrorLogFile("SampleID " + sampleID.ToString() + " is not found and will be Skip ");
                                                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "SampleID " + sampleID.ToString() + " is not found and will be Skip ");
                                                                        error = CFCAPIManager.PutFossSentLog(token, customerID, sampleID, true);

                                                                        if (error == null)
                                                                        {
                                                                            isAbort = true;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (error.ErrorNumber > 0)
                                                                            {
                                                                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api FossSentLog - sample=null");
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            Library.WriteErrorLogFile("if (isError == false && isAbort == false) = " + isError.ToString());
                                                            if (isError == false && isAbort == false)
                                                            {
                                                                Library.WriteErrorLogFile("endIndex = " + endIndex.ToString());
                                                                if (endIndex == 0)
                                                                {
                                                                    endIndex = row.Count - 1;
                                                                }

                                                                Library.WriteErrorLogFile("before for loop startIndex = " + startIndex.ToString() + "  endIndex = " + endIndex.ToString());
                                                                for (Int32 ii = startIndex; ii < endIndex; ii++)
                                                                {
                                                                   // Library.WriteErrorLogFile("for loop [i]= " + i.ToString());
                                                                    Decimal.TryParse(csvResult[1][ii], out resultValue);

                                                                    if (resultValue > 0)
                                                                    {
// TODO        Dan                                                               sqlResultID = "FossAPI" + resultID + resultName[i];
                                                                        AIMManager.InsertAPILog(sample.SampleID, fossCSVResult[0].FossSampleID, (Int32)DataSourceTypeEnum.Foss);
                                                                        checkResult = AIMManager.GetSampleResultForSQLResultID(sampleID, sqlResultID);

                                                                        if (checkResult.Count == 0)
                                                                        {
                                                                            Library.WriteErrorLogFile("Begin put sample result: " + sqlResultID);
                                                                            sampleResult.SampleID = sampleID;
                                                                            sampleResult.AnalyzerNutrientID = 0;
                                                                            sampleResult.CFCUserID = 0;
                                                                            sampleResult.DataSourceTypeID = (Int32)DataSourceTypeEnum.Foss;
                                                                            sampleResult.AimDatabaseID = 0;
                                                                            sampleResult.SqlResultID = sqlResultID;
                                                                            sampleResult.AIMNutrientName = csvResult[0][ii];
                                                                            sampleResult.ResultValue = resultValue;
                                                                            sampleResult.AnalyzerNutrientCode = csvResult[0][ii];
                                                                            sampleResult.AnalysisDate = holdDate;
                                                                            sampleResult.IsActive = true;
                                                                            AIMManager.InsertSampleResult(sampleResult, null, 0);
                                                                        }
                                                                    }
                                                                }

                                                                fossCSVResultSent = new FossCSVResultSentInfo();
                                                                fossCSVResultSent.CustomerID = customerID;
                                                                fossCSVResultSent.FossCSVResultID = result.FossCSVResultID;
                                                                fossCSVResultSent.Token = token;
                                                                error = CFCAPIManager.PutFossCSVResultSent(fossCSVResultSent);

                                                                if (error.ErrorNumber > 0)
                                                                {
                                                                    isAbort = true;
                                                                    Library.WriteErrorLogFile("API PutFossCSVResultSent error " + error.ErrorMessage);
                                                                }
                                                            }
                                                        
                                                        }
                                                        isFirstRow = false;
                                                    }
                                                  
                                                }
 
                                            }
                                            catch (Exception ex)
                                            {
                                                Library.WriteErrorLogFile(ex.Message + " contact CFCTech");
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "End further analyses");

            }
            catch (Exception ex)
            {
                Library.WriteErrorLogFile(ex.Message + " contact CFCTech");
                //ApplicationLog.WriteLog(ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
