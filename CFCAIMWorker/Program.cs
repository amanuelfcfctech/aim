using CFCAIMCore.Entity;
//4/5/23
using CFCAIMCore.Manager;
using CFCAIMWorker.Setup;
using CFCAPI;
// cursor change 2026-10-01 - Option B: entity DTOs come from the single CFCAPICore store (was CFCAPI.Entity).
using CFCAPICore.Entity.Updater;
using CFCAPICore.Entity;
using CFCAPICore.Entity.Updater;
using Core.Library.Notification;
using DevExpress.XtraPrinting.Native.Properties;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace CFCAIMWorker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Library.IsDebugOn = true;
            Library.FullLogFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "" + Assembly.GetExecutingAssembly().GetName().Name + ".Log";
            Library.WriteErrorLogFile("BEGIN CFCAIMWORKER");

            ////9/20/24 DB
            //String aimWorkerVersion = String.Empty;
            //aimWorkerVersion = "26.03.18";
            Library.WriteErrorLogFile("Version " + WorkerGlobal.ProgramVersion);
            //7/22/22
            SettingInfo setting = new SettingInfo();
            cmdTestPNEAPI mainMenu = new cmdTestPNEAPI();
            String[] arguments = Environment.GetCommandLineArgs();
            String output = String.Empty;

            //7/22/22
            String databaseXML = Application.StartupPath + @"\" + "CFCAIMWorkerDatabase";
            String settingXML = Application.StartupPath + @"\" + "CFCAIMWorkerSetting";

            //0924/25
            String settingV2XML = Application.StartupPath + @"\" + "CFCAIMWorkerSettingV2.xml";
            Boolean isSanitizeNeeded = false;

            //4/5/23
            String concept5SettingsXML = Application.StartupPath + @"\" + "CFCAIMWorkerConcept5Settings";

            //5/22/25 DB
            String aimApiKey = String.Empty;
            APIStatusInfo apiStatus = new APIStatusInfo();
            String programName = "cfcAIMWorker";

            //9/3/2025 farag
            WorkerGlobal.CFCAIMWorkerUpdaterFilePath = Path.Combine(WorkerGlobal.ApplicationDirectory, "CFCAIMWorkerUpdater.xml");
            if (!File.Exists(WorkerGlobal.CFCAIMWorkerUpdaterFilePath))
            {
                SetUpCFCAIMWorkerUpdaterFile();
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(WorkerGlobal.CFCAIMWorkerUpdaterFilePath);
            String isUpdatingText = doc.SelectSingleNode("//setting[@name='IsUpdating']/value")?.InnerText;
            Boolean isUpdating = Boolean.TryParse(isUpdatingText, out Boolean result) && result;
            WorkerGlobal.SystemOptions = new SystemOptionInfo
            {
                ProgramVersion = doc.SelectSingleNode("//setting[@name='AppVersion']/value")?.InnerText,
                IsUpdating = isUpdating,
                ScheduledUpdateDate = doc.SelectSingleNode("//setting[@name='ScheduledUpdateDate']/value")?.InnerText
            };

            Library.WriteErrorLogFile("Check XML files");
            //----------------------------------------------------------
            if (File.Exists(databaseXML) == true)
            {
                if (File.Exists(databaseXML + ".xml") == false)
                {
                    File.Move(databaseXML, databaseXML + ".xml");
                }
            }
            databaseXML = WorkerGlobal.DatabaseXML = Application.StartupPath + @"\" + "CFCAIMWorkerDatabase.xml";

            //----------------------------------------------------------
            if (File.Exists(settingXML) == true)
            {
                if (File.Exists(settingXML + ".xml") == false)
                {
                    File.Move(settingXML, settingXML + ".xml");
                }
            }
            settingXML = Application.StartupPath + @"\" + "CFCAIMWorkerSetting.xml";

            if (File.Exists(settingXML) == true)
            {
                if (File.Exists(settingV2XML) == false)
                {
                    File.Move(settingXML, settingV2XML);
                    isSanitizeNeeded = true;
                }
            }
            settingXML = settingV2XML;

            //----------------------------------------------------------
            Library.RemoveOldLogFile();
            Library.LoadSetting(ref setting, Utility.SettingXML);
            if (isSanitizeNeeded) 
            { 
                Library.SanitizeSettings(ref setting, Utility.SettingXML);
            }
            Library.IsDebugOn = setting.IsDebug;
            ApplicationLog.ProgramName = "CFCAIMWorker";

            //Task1048 DB250522 Start
            //         if (arguments.Length > 1)
            //{
            //             output = arguments[1].ToString().ToUpper();

            //         }
            //         else
            //{
            //             output = "No argument";
            //}
            //Task1048 DB250522 End

            Library.WriteErrorLogFile("Process Args");
            if (arguments.Length == 1)
            {
                mainMenu.ShowDialog();
            }
            else
            {
                //Just here for testing purposes, will be removed later
                //CFCAPIManager.Setting = setting;
                //CFCAPIManager.IsMaintenance = true;
                //CFCAPIManager.GetVersion();

                output = arguments[1].ToString().ToUpper();
                Library.WriteErrorLogFile("Output: " + output);
                aimApiKey = setting.AIMAPIKey;
                Library.WriteErrorLogFile("aimApiKey: " + aimApiKey);
                CFCAPIManager.Setting = setting;
                CFCAPIManager.IsMaintenance = true;
                Library.WriteErrorLogFile($"{aimApiKey}, {WorkerGlobal.ProgramVersion}, {programName}");
                apiStatus = CFCAPIManager.GetIsApiDown(aimApiKey, WorkerGlobal.ProgramVersion, programName);
                Library.WriteErrorLogFile($"apiStatus: " + apiStatus.IsAPIDown.ToString());
                CFCAPIManager.IsMaintenance = false;    //TODO DAN why

                if (apiStatus == null || apiStatus.IsAPIDown == true)
                {
                    Library.WriteErrorLogFile("CFC API is currently down for maintenace. Please contact CFC Tech for further assistance.");
                }
                else
                {
                    WorkerGlobal.ApiToken = CFCAPIManager.GetToken(aimApiKey).FirstOrDefault();

                    if (WorkerGlobal.ApiToken != null && !String.IsNullOrEmpty(WorkerGlobal.ApiToken.Token))
                    {
                        Boolean isUpdateApproved = false;
                        HandleProgramUpdate(ref isUpdateApproved, setting, programName, WorkerGlobal.ProgramVersion);

                        if (isUpdateApproved == false)
                        {

                            Library.WriteErrorLog(output);
                            Library.WriteErrorLogFile(output);
                            Library.WriteErrorLogFile("Version " + WorkerGlobal.ProgramVersion);
                            //---------------------------------------------------------------------
                            //This is for when you run it from the scheduled task

                            foreach (String argument in arguments)
                            {
                                String command = argument.ToUpper();

                                switch (command)
                                {
                                    case "SENDBRUKERDATATOAPI": // This get the spectrum data from a file
                                                                // 7/22/22
                                        Bruker.SendSampleToAPI();
                                        break;

                                    case "CHECKBRUKERDATATOAPI":
                                        Bruker.CheckSampleToAPI();
                                        break;

                                    case "CHECKSAMPLEFROMAPI":
                                        Bruker.CheckSampleFromAPI();
                                        break;

                                    case "SENDBRUKERTANGODATATOAPI": // This get the spectrum data from the Database  SendBrukerTangoDataToCFCAPI
                                                                     // 7/22/22
                                        Bruker.SendTangoSampleToAPI();
                                        break;

                                    case "GETBRUKERDATAFROMAPI": // GetBrukerDataFromAPI  This pull the data from CFC API call
                                                                 // 7/22/22
                                        Bruker.GetSampleFromAPI();
                                        break;

                                    case "SENDFOSSDATATOAPI":
                                        // 7/22/22
                                        FossImport.SendCSVResultSampleToAPI();
                                        break;

                                    case "GETFOSSDATAFROMAPI":
                                        // 7/22/22
                                        Library.WriteErrorLogFile("Enter GETFOSSDATAFROMAPI");
                                        FossImport.GetSampleFromAPI();          //TODO Check if userid is being update
                                        break;

                                    // --------------------------------------------------------------------------------
                                    case "GETOUTSIDELABDATAFROMAPI":
                                        // 8/5/25
                                        OutsideLabImport.GetSampleFromAPI();
                                        break;

                                    // --------------------------------------------------------------------------------
                                    case "FULLSYNC":
                                        // 4/4/23

                                        // CFCAIMCore.DataExchange.

                                        //// RUN FULL SYNC
                                        ////// AIMGlobal.SyncDatabase(_txtExistingPath, aimDatabase.AIMDatabaseID, _progressBarID, "L");

                                        //// UPDATE SYNC TIME ON AIMDATABASE
                                        // String dtTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        // AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
                                        // aimDatabase = (AIMDatabaseInfo)lueConnectionName.GetSelectedDataRow();
                                        // AIMDatabaseInfo updAIMDatabase = new AIMDatabaseInfo();
                                        // updAIMDatabase.AIMDatabaseID = aimDatabase.AIMDatabaseID;
                                        // updAIMDatabase.Name = aimDatabase.Name;
                                        // updAIMDatabase.CFCPath = aimDatabase.CFCPath;
                                        // updAIMDatabase.DefaultForPropose = aimDatabase.DefaultForPropose;
                                        // updAIMDatabase.IsActive = aimDatabase.IsActive;
                                        // updAIMDatabase.DataSourceTypeID = aimDatabase.DataSourceTypeID;
                                        // updAIMDatabase.SyncTime = Convert.ToDateTime(dtTime);
                                        // updAIMDatabase.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                                        // updAIMDatabase.AIMPriorityUsedValueID = aimDatabase.AIMPriorityUsedValueID;
                                        // AIMManager.SaveAIMDatabase(ref updAIMDatabase);
                                        break;

                                    default:
                                        //Note that it will always default on the first one because it will be the source no the argument
                                        //TODO: Show Error
                                        break;
                                }
                            }
                        }
                    }


                }

                //--------------------------------------------------------------------------------
                //if (arguments.Length > 1 && arguments[1].ToString().ToUpper() == "MAINTENANCETEST")
                //{
                //    //Bruker.DatabaseXML = databaseXML;


                //    Int32 customerID = 0;
                //    String aimApiKeyMaintenance = String.Empty;
                //    APIStatusInfo apiStatusMaintenance = new APIStatusInfo();

                //    aimApiKeyMaintenance = setting.AIMAPIKey;
                //    CFCAPIManager.IsMaintenance = true;

                //    apiStatusMaintenance = CFCAPIManager.GetIsApiDown(aimApiKeyMaintenance, aimWorkerVersion);

                //    CFCAPIManager.IsMaintenance = false;
                //}
            }

        }

        static void HandleProgramUpdate(ref Boolean isUpdateApproved, SettingInfo setting, String programName, String programVersion)
        {
            Library.WriteErrorLogFile("Handling Program Update");

            APIGlobal.LoadSettings(setting.IsTestUrl, setting.IsMaintenanceTestUrl, setting.APIUrl, setting.APIUrlTest, setting.APIMaintenanceUrl, setting.APIMaintenanceUrlTest);

            if (APIGlobal.IsAPIOrTokenDown(setting.AIMAPIKey, programName, programVersion))
            {
                //TODO: Error
            }
            else
            {
                Library.WriteErrorLogFile("Checking If Update Available");
                UpdateManager.UpdateCFCUpdaterIfAvailable();
                Library.WriteErrorLogFile("Checking Program Update");
                UpdateManager.UpdateProgramVersionRecord();

                String updateFileName = String.Empty;
                if (UpdateManager.IsUpdateScheduled() == true)
                {
                    Library.WriteErrorLogFile("Checking If Update");
                    UpdateManager.CheckSoftwareUpdate(ref isUpdateApproved, ref updateFileName);
                }

                if (isUpdateApproved == true)
                {
                    Library.WriteErrorLogFile("Launch Updater Program ");
                    UpdateManager.LaunchCFCUpdaterAndExit(updateFileName);
                }
            }
        }
        static void SetUpCFCAIMWorkerUpdaterFile()
        {
            XmlDocument newDoc = new XmlDocument();
            newDoc.LoadXml($@"<?xml version=""1.0"" encoding=""utf-8""?>
                <ProgramSettings>
                  <Settings>
                    <setting name=""AppVersion"" serializeAs=""String"">
                      <value>{WorkerGlobal.ProgramVersion}</value>
                    </setting>
                    <setting name=""IsUpdating"" serializeAs=""Boolean"">
                      <value>true</value>
                    </setting>
                    <setting name=""ScheduledUpdateDate"" serializeAs=""String"">
                      <value></value>
                    </setting>
                  </Settings>
                </ProgramSettings>");

            // Save the file
            newDoc.Save(WorkerGlobal.CFCAIMWorkerUpdaterFilePath);
        }
    }
}
