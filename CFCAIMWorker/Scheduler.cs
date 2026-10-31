using CFCAIMCore.DataExchange;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Timers;
using Core.Library.Notification;
using Core.Library.SQLDatabaseConnection;
using CFCAPICore.Entity;
using CFCAIMCore.Entity.Bruker;
using System.Windows.Forms;
using CFCAIMWorker.Adisseo;

namespace CFCAIMWorker
{
    public partial class Scheduler : ServiceBase
    {
        CFCUser.Global userGlobal = new CFCUser.Global();
        private System.Timers.Timer timer1 = null;
        private System.Timers.Timer timerGetData = null;
//        private System.Timers.Timer timerBrukerData = null;
//        private System.Timers.Timer timerBrukerGet = null;
//        private static Boolean _isDone = false;

        //public void IsDone
        //{
        //    get
        //    {
        //        return _isDone;
        //    }

        //    set
        //    {
        //        _isDone = value;
        //    }
        //}

        public Scheduler()
        {
            InitializeComponent();
            ApplicationLog.ProgramName = Assembly.GetExecutingAssembly().GetName().Name;
        }

        public void OnDebug()
        {
            //OnStart(null);
            //BrukerData();
            //GetBrukerData();
            //service.Stop();
        }

        protected override void OnStart(string[] args)
        {
            //timer1 = new System.Timers.Timer();
            //timer1.Interval = Properties.Settings.Default.TimerSetting;   //interval is set on config file 
            //timer1.Elapsed += new System.Timers.ElapsedEventHandler(tmrGetResult_Tick);
            //timer1.Enabled = false;
            //timerGetData = new System.Timers.Timer();
            //timerGetData.Interval = Properties.Settings.Default.TimerSetting;   //interval is set on config file 
            //timerGetData.Elapsed += new System.Timers.ElapsedEventHandler(timerGetData_Tick);
            //timerGetData.Enabled = false;
            //timerBrukerData = new System.Timers.Timer();
            //timerBrukerData.Interval = Properties.Settings.Default.TimerSetting;   //interval is set on config file 
            //timerBrukerData.Elapsed += new System.Timers.ElapsedEventHandler(TimerBrukerDataTick);
            //timerBrukerData.Enabled = true;
            //timerBrukerGet = new System.Timers.Timer();
            //timerBrukerGet.Interval = Properties.Settings.Default.TimerSetting;   //interval is set on config file 
            //timerBrukerGet.Elapsed += new System.Timers.ElapsedEventHandler(TimerBrukerGetTick);
            //timerBrukerGet.Enabled = false;


            //    ApplicationLog.WriteLog("Service started.", EventLogEntryType.Information);
            //AIMManager.DeleteSystemLog();
        }

        protected override void OnStop()
        {
            ApplicationLog.WriteLog("Service stopped.", EventLogEntryType.Information);
        }
                
        private void Logging(Int32 typeID, String message, Int32 lineNumber, String methodName, Int32 cfcUserID)
        {
         //   if (Properties.Settings.Default.IsDebugOn == true)                          //TODO
         //   {
                AIMManager.InsertSystemLog(typeID, message, lineNumber, methodName, cfcUserID);
         //   }
        }

        private void tmrGetResult_Tick(object sender, ElapsedEventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                AnalyzerPendingSampleInfo analyzerPending = new AnalyzerPendingSampleInfo();
                SampleInfo sample = new SampleInfo();
                SampleResultInfo sampleResult = new SampleResultInfo();
                List<AnalyzerPendingSampleInfo> analyzerPendingSample = new List<AnalyzerPendingSampleInfo>();
                List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
                List<ImportConfigInfo> importConfig = new List<ImportConfigInfo>();
                List<BrukerSQLInfo> brukerSQL = new List<BrukerSQLInfo>();
                FileTypeEnum fileType = FileTypeEnum.CSV;
                FilesCountInfo filesCount = new FilesCountInfo();
                DataSourceTypeEnum dataSource = DataSourceTypeEnum.Adisseo;
                String argument = String.Empty;
                String sql = String.Empty;
                String userName = String.Empty;
                String passWord = String.Empty;
                String path = String.Empty;
                String fileName = String.Empty;
                Boolean isError = false;
                ErrorInfo error = new ErrorInfo();
                FilesInfo file = new FilesInfo();
                Int32 sampleID = 0;
                Int32 lastSampleID = 0;

                importConfig = AIMManager.GetImportConfig(DataSourceTypeEnum.Foss);
                for (int i = 0; i < importConfig.Count; i++)
                {
                    Logging((Int32)EnumSystemLogType.Info, "Checking Directory", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);

                    if (Directory.Exists(importConfig[i].ProcessDirectory))
                    {
                        ErrorInfo result = new ErrorInfo();
                        fileType = (FileTypeEnum)importConfig[i].FileTypeID;
                        dataSource = (DataSourceTypeEnum)importConfig[i].DataSourceTypeID;

                        //---------------------------------------------------------------------------																	
                        if (Directory.Exists(importConfig[i].BackUpDirectory))
                        {
                            DirectoryInfo directory = new DirectoryInfo(importConfig[i].ProcessDirectory);
                            FileInfo[] files = directory.GetFiles("*." + fileType.ToString(), SearchOption.TopDirectoryOnly);

                            file.ProcessPath = importConfig[i].ProcessDirectory;
                            file.BackupPath = importConfig[i].BackUpDirectory;
                            //---------------------------------------------------------------------------
                            foreach (FileInfo item in files)
                            {
                                file.FileName = item.Name;
                                filesCount.TotalFilesCount += 1;

                                switch (fileType)
                                {
                                    case FileTypeEnum.API:
                                        DataExchangeManager.APIProcess(dataSource);
                                        Logging((Int32)EnumSystemLogType.Info, "APIProcess complete", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                                        break;
                                    case FileTypeEnum.CSV:
                                        error = DataExchangeManager.CSVProcess(file, ref filesCount, dataSource, importConfig[i].BackUpDirectory);
                                        Logging((Int32)EnumSystemLogType.Info, "CSVProcess complete", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                                        break;
                                    case FileTypeEnum.XML:
                                        Logging((Int32)EnumSystemLogType.Info, "XML format error. Contact IT.", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                                        error.ErrorNumber = 91;
                                        break;
                                    case FileTypeEnum.TXT:
                                        Logging((Int32)EnumSystemLogType.Info, "TXT format error. Contact IT.", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                                        error.ErrorNumber = 92;
                                        break;
                                    default:
                                        Logging((Int32)EnumSystemLogType.Info, "Unsupported file type. Contact IT.", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                                        error.ErrorNumber = 93;
                                        break;
                                }

                                if (error.RecordID > 0 && error.ErrorNumber == 0)
                                {
                                    sampleID = error.RecordID;
                                    sample = AIMManager.GetSample(sampleID);
                                    if (sample.FurtherAnalysisID > 0)
                                    {
                                        analyzerPending = AIMManager.GetAnalyzerPendingSample(sampleID);
                                        if (analyzerPending == null)
                                        {
                                            analyzerPending = new AnalyzerPendingSampleInfo();
                                            analyzerPending.SampleID = sampleID;
                                            analyzerPending.CreatedStamp = DateTime.Now;
                                        }

                                        analyzerPending.NIRAnalysis = DateTime.Now;
                                        AIMManager.SaveAnalyzerPendingSample(ref analyzerPending);
                                    }
                                }
                                else
                                {
                                    Logging((Int32)EnumSystemLogType.Warning, "ErrorInfo recordID for pending sample = 0. ErrorNumber = " + error.ErrorNumber, (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                                }
                            }
                        }
                        else
                        {
                            Logging((Int32)EnumSystemLogType.Warning, "Backup path is missing " + importConfig[i].BackUpDirectory, (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                        }
                    }
                    else
                    {
                        Logging((Int32)EnumSystemLogType.Warning, "Process directory does not exist " + importConfig[i].ProcessDirectory, (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                    }
                }

                importConfig = AIMManager.GetImportConfig(DataSourceTypeEnum.Bruker);
                for (int i = 0; i < importConfig.Count; i++)
                {
                    Logging((Int32)EnumSystemLogType.Info, "Checking Bruker Database", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);

                    if (SQLDatabaseConnection.TestSqlConnection(importConfig[i].ServerName, importConfig[i].IsSqlAuthentication, importConfig[i].SqlUserName, importConfig[i].SqlPassword, importConfig[i].DatabaseName, false) == false)
                    {
                        Logging((Int32)EnumSystemLogType.Info, "Connection fail to Bruker Database", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                    }
                    else
                    {
                        BrukerManager.IsMessageDisplay = false;
                        BrukerManager.CreateBrukerConnection(importConfig[i].ServerName, importConfig[i].DatabaseName, importConfig[i].IsSqlAuthentication, importConfig[i].SqlUserName, importConfig[i].SqlPassword);
                        ErrorInfo result = new ErrorInfo();
                        dataSource = (DataSourceTypeEnum)importConfig[i].DataSourceTypeID;
                        brukerSQL = BrukerManager.GetBrukerSqlData();

                        foreach (BrukerSQLInfo bruker in brukerSQL)
                        {
                            sampleID = Int32.Parse(bruker.SampleCode);

                            if (sampleID != lastSampleID)
                            {
                                if (lastSampleID > 0)
                                {
                                 //   BrukerManager.InsertCFCLog(path, fileName, false, false);
                                }

                                sample = AIMManager.GetSample(sampleID);
                                path = bruker.Path;
                                fileName = bruker.FileName;
                            }

                            if (sample == null)
                            {
                                if (sampleID != lastSampleID)
                                {
                                   // BrukerManager.InsertCFCLog(bruker.Path, bruker.FileName, false , false);
                                    lastSampleID = sampleID;
                                }
                            }
                            else
                            {
                                DataExchangeManager.ProcessBrukerResult(bruker, sample, (Int32)DataSourceTypeEnum.Bruker, importConfig[i].AIMDatabaseID);

                                if (sample.FurtherAnalysisID > 0 && sampleID != lastSampleID)
                                {
                                    analyzerPending = AIMManager.GetAnalyzerPendingSample(sampleID);
                                    if (analyzerPending == null)
                                    {
                                        analyzerPending = new AnalyzerPendingSampleInfo();
                                        analyzerPending.SampleID = sampleID;
                                        analyzerPending.CreatedStamp = DateTime.Now;
                                    }

                                    analyzerPending.NIRAnalysis = DateTime.Now;
                                    AIMManager.SaveAnalyzerPendingSample(ref analyzerPending);

                                    lastSampleID = sampleID;
                                }
                            }
                        }

                        if (lastSampleID > 0)
                        {
                            //BrukerManager.InsertCFCLog(path, fileName, true, true);
                        }
                    }
                }

                //AnalysisRequestInfo analysisRequests = new AnalysisRequestInfo();
                //analysisRequests = APIManager.GetAnalysisRequests(x);
                analyzerPendingSample = AIMManager.GetAnalyzerPendingSample(AnalyzerStatusEnum.NotSentOrNotReceived);
                if (analyzerPendingSample.Count > 0)
                {
                    APIManager.Token = "1caddf65fe41b7802a2ed093a4e6952e";    //CSA
                                                                              //APIManager.Token = "b1d38edb765644228e1628bb3a6ddeee";
                                                                              // APIManager.Token = "099355ea677eb99e4efc48db1997aa61";  //development

                    Logging((Int32)EnumSystemLogType.Info, "Start further analyses", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                    foreach (AnalyzerPendingSampleInfo pendingSample in analyzerPendingSample)
                    {
                        if (pendingSample.SentStamp == DateTime.MinValue)
                        {
                            //Task1160 DB260309 isError = AdisseoAPI.AnalyseCode();
                            isError = CFCAPIPne.AnalyseCode(); //Task1160 DB2603091
                            if (isError == false)
                            {
                                //Task1160 DB260309 isError = AdisseoAPI.MaterialCode();
                                isError = CFCAPIPne.MaterialCode(); //Task1160 DB260309
                                if (isError == false)
                                {
                                    //Task1160 DB260309 AdisseoAPI.SendPendingSampleSpectra(); //Here Dan
                                    CFCAPIPne.SendPendingSampleSpectra(); //Task1160 DB260309
                                }
                            }
                            else
                            {
                                Logging((Int32)EnumSystemLogType.Fatal, "Error in CFCAPIPne.AnalyseCode", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                            }
                        }
                        else
                        {
                            Logging((Int32)EnumSystemLogType.Info, "Pending sampleID = " + pendingSample.SampleID.ToString() + ", Sent or Received stamp is null", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                        }

                        if (pendingSample.ReceivedStamp == DateTime.MinValue)
                        {
                            //  Thread.Sleep(3000);
                            //Task1160 DB260309 AdisseoAPI.GettingRequest();
                            CFCAPIPne.GettingRequest(); //Task1160 DB260309
                        }
                    }
                    Logging((Int32)EnumSystemLogType.Info, "End further analyses", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                }
                else
                {
                    Logging((Int32)EnumSystemLogType.Info, "No Analyzer pending", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                }

                Thread.Sleep(3000);
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message, EventLogEntryType.Error);
            }
        }

        private void timerGetData_Tick(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerGetData.Enabled = false;
                List<AnalysisResultsInfo> analysisResult = new List<AnalysisResultsInfo>();
                AnalyzerPendingSampleInfo analyzerPending = new AnalyzerPendingSampleInfo();
                SampleInfo sample = new SampleInfo();
                List<AnalyzerPendingSampleInfo> analyzerPendingSample = new List<AnalyzerPendingSampleInfo>();
                List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
                List<ImportConfigInfo> importConfig = new List<ImportConfigInfo>();
                FilesCountInfo filesCount = new FilesCountInfo();
                String argument = String.Empty;
                String sql = String.Empty;
              //  Boolean isError = false;
                ErrorInfo error = new ErrorInfo();
                FilesInfo file = new FilesInfo();

                Logging((Int32)EnumSystemLogType.Info, "Start further analyses", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                APIManager.Token = "1caddf65fe41b7802a2ed093a4e6952e";
                //Task1160 DB2603091 analysisResult = AdisseoAPI.GetAnalysisResultList();
                analysisResult = CFCAPIPne.GetAnalysisResultListAll(); //Task1160 DB2603091
                APIManager.Token = "b1d38edb765644228e1628bb3a6ddeee";
                //Task1160 DB2603091 analysisResult = AdisseoAPI.GetAnalysisResultList();
                analysisResult = CFCAPIPne.GetAnalysisResultListAll(); //Task1160 DB2603091
                //if (isError == false)
                //{
                //Task1160 DB2603091 //    isError = AdisseoAPI.MaterialCode();
                //    if (isError == false)
                //    {
                //Task1160 DB2603091 //        AdisseoAPI.SendPendingSampleSpectra();
                //    }
                //}
                //else
                //{
                //Task1160 DB2603091 //    Logging((Int32)SystemLogType.Fatal, "Error in AdisseoAPI.AnalyseCode", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);
                //}

                Logging((Int32)EnumSystemLogType.Info, "End further analyses", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, userGlobal.CFCLoginUser.CFCUserID);


                Thread.Sleep(3000);
                timerGetData.Enabled = true;
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message, EventLogEntryType.Error);
            }
        }

        //private void TimerBrukerGetTick(object sender, ElapsedEventArgs e)

    }
}
