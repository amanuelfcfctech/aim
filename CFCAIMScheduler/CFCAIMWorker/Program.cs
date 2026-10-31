using CFCAIMCore.Entity;
using CFCAIMWorker.Setup;
using System;
using System.Windows.Forms;
using System.IO;
using Core.Library.Notification;
using System.Reflection;

//4/5/23
using CFCAIMCore.DataExchange;

namespace CFCAIMWorker
{
    static class Program
	{
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
		{
            //7/22/22
            SettingInfo setting = new SettingInfo();
            cmdTestPNEAPI mainMenu = new cmdTestPNEAPI();
            String[] arguments = Environment.GetCommandLineArgs();
            String output = String.Empty;

            //7/22/22
            String databaseXML = Application.StartupPath + @"\" + "CFCAIMWorkerDatabase";
            String settingXML = Application.StartupPath + @"\" + "CFCAIMWorkerSetting";

            //4/5/23
            String concept5SettingsXML = Application.StartupPath + @"\" + "CFCAIMWorkerConcept5Settings";


            Library.FullLogFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "" + Assembly.GetExecutingAssembly().GetName().Name + ".Log";

            //----------------------------------------------------------
            if (File.Exists(databaseXML) == true)
			{
				if (File.Exists(databaseXML + ".xml") == false)
				{
                    File.Move(databaseXML, databaseXML + ".xml");
				}
			}
            databaseXML = Application.StartupPath + @"\" + "CFCAIMWorkerDatabase.xml";

            //----------------------------------------------------------
            if (File.Exists(settingXML) == true)
            {
                if (File.Exists(settingXML + ".xml") == false)
                {
                    File.Move(settingXML, settingXML + ".xml");
                }
            }

            //----------------------------------------------------------
            Library.RemoveOldLogFile();
//            settingXML = Utility.SettingXML;
            //Utility.LoadConnection(ref setting);
            Library.LoadSetting(ref setting, Utility.SettingXML);
            Library.IsDebugOn = setting.IsDebug;
            ApplicationLog.ProgramName = "CFCAIMWorker";

            if (arguments.Length > 1)
			{
                output = arguments[1].ToString().ToUpper();
            }
            else
			{
                output = "No argument";
			}

            Library.WriteErrorLog(output);
            Library.WriteErrorLogFile("Version 24.03.07");
            //---------------------------------------------------------------------
            //This is for when you run it from the scheduled task
            if (arguments.Length > 1 && arguments[1].ToString().ToUpper() == "SENDBRUKERDATATOAPI")             //This get the spectrum data from a file
            {
                //7/22/22
                Bruker.DatabaseXML = databaseXML;

                Bruker.SendSampleToAPI();
            }
            else if (arguments.Length > 1 && arguments[1].ToString().ToUpper() == "CHECKBRUKERDATATOAPI")
            {
                Bruker.DatabaseXML = databaseXML;
                Bruker.CheckSampleToAPI();
            }
            else if (arguments.Length > 1 && arguments[1].ToString().ToUpper() == "CHECKSAMPLEFROMAPI")
            {
                Bruker.DatabaseXML = databaseXML;
                Bruker.CheckSampleFromAPI();
            }
            else if (arguments.Length > 1 && arguments[1].ToString().ToUpper() == "SENDBRUKERTANGODATATOAPI")   //This get the spectrum data from the Database  SendBrukerTangoDataToCFCAPI
            {
                //7/22/22
                Bruker.DatabaseXML = databaseXML;

                Bruker.SendTangoSampleToAPI();
            }
            else if (arguments.Length > 1 && arguments[1].ToString().ToUpper() == "GETBRUKERDATAFROMAPI")       //GetBrukerDataFromAPI  This pull the data from CFC API call
            {
                //7/22/22
                Bruker.DatabaseXML = databaseXML;

                Bruker.GetSampleFromAPI();
            }
            else if (arguments.Length > 1 && arguments[1].ToString().ToUpper() == "SENDFOSSDATATOAPI")
            {
                //7/22/22
                FossImport.DatabaseXML = databaseXML;

                FossImport.SendSampleToAPI();
            }
            else if (arguments.Length > 1 && arguments[1].ToString().ToUpper() == "GETFOSSDATAFROMAPI")
            {
                //7/22/22
                FossImport.DatabaseXML = databaseXML;

                FossImport.GetSampleFromAPI();
            }
            else if (arguments.Length > 1 && arguments[1].ToString().ToUpper() == "SETTING")
            {
                mainMenu.ShowDialog();
            }

            //--------------------------------------------------------------------------------
            //4/4/23
            else if (arguments.Length > 1 && arguments[1].ToString().ToUpper() == "FULLSYNC")
            {

                //CFCAIMCore.DataExchange.

                ////RUN FULL SYNC
                //////AIMGlobal.SyncDatabase(_txtExistingPath, aimDatabase.AIMDatabaseID, _progressBarID, "L");


                ////UPDATE SYNC TIME ON AIMDATABASE
                //String dtTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
                //aimDatabase = (AIMDatabaseInfo)lueConnectionName.GetSelectedDataRow();
                //AIMDatabaseInfo updAIMDatabase = new AIMDatabaseInfo();
                //updAIMDatabase.AIMDatabaseID = aimDatabase.AIMDatabaseID;
                //updAIMDatabase.Name = aimDatabase.Name;
                //updAIMDatabase.CFCPath = aimDatabase.CFCPath;
                //updAIMDatabase.DefaultForPropose = aimDatabase.DefaultForPropose;
                //updAIMDatabase.IsActive = aimDatabase.IsActive;
                //updAIMDatabase.DataSourceTypeID = aimDatabase.DataSourceTypeID;
                //updAIMDatabase.SyncTime = Convert.ToDateTime(dtTime);
                //updAIMDatabase.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                //updAIMDatabase.AIMPriorityUsedValueID = aimDatabase.AIMPriorityUsedValueID;
                //AIMManager.SaveAIMDatabase(ref updAIMDatabase);
            }

            //--------------------------------------------------------------------------------
            else
            {
				Bruker.DatabaseXML = databaseXML;
				mainMenu.ShowDialog();
            }
        }
	}
}
