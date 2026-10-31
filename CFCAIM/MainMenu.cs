using CFCAIM.DataExchange;
using CFCAIM.Maintenance;
using CFCAIM.Report;

using CFCAIM.ReportDevExpress;
using CFCAIMCore;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using CFCUser;
using CFCUser.Entity;
using CFCUser.Manager;
using Core.Library.DevExpress;
using Core.Library.SQLDatabaseConnection;
using DevExpress.LookAndFeel;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using CFCAIM.Scheduler;
using CFCAIM.Chart;
using Ionic.Zip;
using CFCAIM.Cloud;                         //1/16/24


//9/16/20
//using CFCAIMCustom.Vitapro;
using CFCAIM.Custom.Vitapro.Forms;
using CFCAIM.Sample;
using CFCUser.Data;
using System.Linq;
using CFCAPICore.Entity.Adisseo;

namespace CFCAIM
{
    public partial class MainMenu : Form
    {

        Resizer resize = new Resizer();

        CFCUser.Global userGlobal = new CFCUser.Global();
        Analysis analysis;
        ReportSimple reportView;
        ReportAdvanced nreportView;
        ImportData processR;
        UserListing userListing;
        Utilities utilities;
        DeveloperWindow devWin;
        DataMapping dataMapping;
        PrinterSetup printerSetup;

        testChart testChart;

        Instrument instrument;

        tpProdSpec sample;
        ExportBatchPending exportbatchpending;
        String _versionFromInternet = String.Empty;
        String[] _allVersions;
        SampleInfoForProcessResult sampleInfoForProcessResult;

        WebService webService;
        About about;
        EmailAddress emailAddress;
        EmailAddressXRefMessage emailAddressXRefMessage;

        ImportMapping importMapping;
        ImportDataUserDefined importSampleUserDefined;
        DataSourceUser dataSourceUser;

        DefaultSettings defaultSettings;
        Charts charts;
        AnalyzerCrossReference analyzerCrossReference;
        FormulationCrossReference formulationCrossReference;

        Maintenance.Analyzer analyzer;
        AIM aim;
        SystemSettings systemSettings;
        UtilitiesCFCTech utilitiesCFCTech;
        MathType mathType;
        GuaranteeRefund guaranteeRefund;

        TestPivotGrid testPivotGrid;
        TestGrid testGrid;
        RebuildSampleAIMSync sampleAIMSync;
        FileTypes fileTypes;

        ExportOther exportOther;
        Imports.MapWizard mapWizard;

        UserDefaults userDefaults;
        UsersLoggedIn usersLoggedIn;

        LanguageTest languageTest;
        Test2 Test2;
        test4 testcm4;


        //---------------------------------------
        Int32 _batchID = 1;
        String _message = "";
        String _sqlCommand = "";
        Int32 _commandType = 0;
        Int32 _errorNumber = 0;
        //---------------------------------------

        //6/9/17 (Task 403)
        Boolean _showCloseAllFormsMessage = true;

        //1/12/18
        UserInfo _loginUser = new UserInfo();
        List<RolesInfo> _loginUserRoles = new List<RolesInfo>();

        //9/9/20 - change to use CFCUser.Global
        //Global _global = new Global();
        CFCUser.Global _global = new CFCUser.Global();

        //5/8/18
        SampleReceiving sampleReceiving;
        SampleProduction sampleProduction;
        LineMaintenance lineMaintenance;

        SchedulerMain schedulerMain;

        public Boolean IsBackGroundSync = false;


        //8/26/19
        SettingsCustom settingsCustom;

        //1/22/21
        SampleType sampleType;

        //2/12/21
        ProductTolerance aimMaterialGrouping;

        //2/18/21
        AnalysisProduct analysisProduct;

        //6/27/22
        Test5 test5;

        //11/23/22
        NutrientTolerance nutrientTolerance;

        //11/30/22
        xSampleFurtherAnalysisPending sampleFurtherAnalysisPending;

        
        ReportComments reportComments;                          //9/5/23
        Report_Designer report_Designer;                        //12/27/23
        PNECredit pneCredit;                                    //1/16/24
        FurtherAnalysis furtherAnalysis;                        //1/16/24
        PendingSample pendingSample;                            //1/16/24
        GroundUnGroundCrossReference groundXRef;                //1/19/24

        #region Background Worker
        private void backgroundWorkerSyncProgress_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Thread.Sleep(100);
            AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();

            //9/9/20 - change to use AIMCoreGlobal
            //aimDatabase = AIMGlobal.aimDatabase;
            aimDatabase = CFCAIMCore.Global.aimDatabase;

            if (IsBackGroundSync)
            {
                if (aimDatabase == null)
                {
                    IsBackGroundSync = false;
                    backgroundWorkerSyncProgress.CancelAsync();
                    return;
                }

                while (aimDatabase.SyncProgressPercentage > 0)
                {
                    //9/9/20 - change to use AIMCoreGlobal
                    //aimDatabase = AIMGlobal.aimDatabase;
                    aimDatabase = CFCAIMCore.Global.aimDatabase;

                    aimDatabase = AIMManager.GetAIMDatabase(aimDatabase.AIMDatabaseID);
                    if (aimDatabase == null)
                    {
                        IsBackGroundSync = false;
                        backgroundWorkerSyncProgress.CancelAsync();
                        return;
                    }

                    //9/9/20 - change to use AIMCoreGlobal
                    //AIMGlobal.aimDatabase = aimDatabase;
                    CFCAIMCore.Global.aimDatabase = aimDatabase;

                    backgroundWorkerSyncProgress.ReportProgress(aimDatabase.SyncProgressPercentage);
                    Thread.Sleep(3000);
                }
            }
            e.Result = aimDatabase;
        }
        private void backgroundWorkerSyncProgress_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage == 0)
                {
                    lblSyncTimeStamp.Text = "Never sync or having problem getting info.";
                    return;
                }
                else
                {
                    lblSyncTimeStamp.Text = "Synchronizing right now!";
                    pbAimSync.Value = e.ProgressPercentage;

                    //ToDo - should this be uncommented? 
                    //pbAimSync.Visible = true;

                    btnSyncNow.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void backgroundWorkerSyncProgress_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
                aimDatabase = (AIMDatabaseInfo)e.Result;
                if (aimDatabase == null || aimDatabase.SyncProgressPercentage == 0)
                {
                    lblSyncTimeStamp.Text = "Never sync or having problem getting info.";
                }
                else
                {
                    lblSyncTimeStamp.Text = "Last Sync with AIM on " + aimDatabase.SyncTime;
                    pbAimSync.Value = aimDatabase.SyncProgressPercentage;
                }
                pbAimSync.Visible = false;

                //ToDo -- should this be uncommented?
                //btnSyncNow.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void backgroundWorkerCheckUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                DownLoadVersionFileAndGetLastestVersion();
                Thread.Sleep(1000);
                CheckIfVersionListed();
                if (IsValidVersionPair() == false)
                {
                    //TODO
                    //application is updated but not database
                    //    application, or user will download the database script to update it.
                    //database is updated but not application
                    //    application will ask to user to download application from cfc website then install it.
                    //*it may also happen that we forgot to change one or another but lets think about it later.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private Boolean CloseAllForms()
        {
            Boolean isCancel = false;
            Int32 count = 0;

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "MainMenu" && Application.OpenForms[i].Name.Length > 0)
                {
                    count += 1;
                }
            }

            if (count > 0)
            {
                //-----------------------------------
                if (AIMGlobal.dataMapForm == true)
                {
                    for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                    {
                        if (Application.OpenForms[i].Name != "MainMenu")
                        {
                            Application.OpenForms[i].Close();
                        }
                    }
                    AIMGlobal.dataMapForm = false;
                }
                else
                //-----------------------------------

                {

                    //-------------------------------
                    //6/9/17 (Task 403
                    if (_showCloseAllFormsMessage == false)
                    {
                        _showCloseAllFormsMessage = true;
                        for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                        {
                            if (Application.OpenForms[i].Name != "MainMenu")
                            {
                                Application.OpenForms[i].Close();
                            }
                        }
                    }
                    //-------------------------------

                    else
                    {
                        DialogResult dr = new DialogResult();
                        dr = MessageBox.Show("There are multiple forms open. Are you sure you want to change? Your work may be lost.", "Warning...", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No)
                        {
                            isCancel = true;
                        }
                        else
                        {
                            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                            {
                                if (Application.OpenForms[i].Name != "MainMenu")
                                {
                                    Application.OpenForms[i].Close();
                                }
                            }
                        }
                    }
                }
            }
            return isCancel;
        }
        #endregion
        #region Private Functions

        private void LoadlueDataMap()
        {
            AIMConfigInfo aimConfig = new AIMConfigInfo();
            List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();            

            aimDatabase = AIMManager.GetAIMDatabase(true);            

            //5/19/23
            //check to see if user is NOT in config table
            aimConfig = AIMManager.GetAIMConfig(userGlobal.CFCLoginUser.CFCUserID);

            //5/19/23
            //if not there, then insert
            if (aimConfig == null && aimDatabase != null)
            {
                AIMManager.SaveAIMConfig(userGlobal.CFCLoginUser.CFCUserID, aimDatabase[0].AIMDatabaseID, 1);
            }
            
                                    
            aimConfig = AIMManager.GetAIMConfig(userGlobal.CFCLoginUser.CFCUserID);
            if (aimConfig == null)
            {
                aimDatabase.Insert
                    (
                        0,
                        new AIMDatabaseInfo
                        {
                            AIMDatabaseID = 0,
                            Name = "--Select--"
                        }
                    );
            }

            lueDataMap.Properties.DataSource = aimDatabase;
            XtraEditors.LoadLookUpEdit(ref lueDataMap, "AIMDatabaseID", "Name");

            lueDataMap.Properties.Columns["AIMDatabaseID"].AllowSort = DevExpress.Utils.DefaultBoolean.True;
            lueDataMap.Properties.Columns["AIMDatabaseID"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            lueDataMap.Properties.SortColumnIndex = lueDataMap.Properties.Columns.IndexOf(lueDataMap.Properties.Columns["AIMDatabaseID"]);

            if (aimConfig != null)
            {
                lueDataMap.EditValue = aimConfig.AIMDatabaseID;
                CFCAIMCore.Global.aimDatabase = (AIMDatabaseInfo)lueDataMap.GetSelectedDataRow();
            }
            else
            {
                lueDataMap.EditValue = 0;
                CFCAIMCore.Global.aimDatabase = new AIMDatabaseInfo();
            }

            AIMCoreGlobal.AIMDatabaseID = (Int32)lueDataMap.EditValue;

            
        }
        private void LoadlueDatabase()
        {
            List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
            Int32 databaseID = 0;

            lueDatabase.Visible = false;
            lblDatabase.Visible = false;

            if (File.Exists(SQLDatabaseConnection.CFCAIMDatabaseFile))
            {
                lueDatabase.Visible = true;
                lblDatabase.Visible = true;
                SQLDatabaseConnection.LoadConnection(ref databases, true, SQLDatabaseConnection.CFCAIMDatabaseFile);

                //12/6/21
                //databaseID = SQLDatabaseConnection.GetDefaultConnection(ref databases);
                databaseID = userGlobal.AIMDatabaseID;

                lueDatabase.Properties.DataSource = databases;
                XtraEditors.LoadLookUpEdit(ref lueDatabase, "DatabaseID", "ConnectName");
                lueDatabase.EditValue = databaseID;

            }
        }

        private void CheckIfVersionListed()
        {
            //if (_allVersions.Contains(Application.ProductVersion) == false)
            //{
            //	//product version is not setup properly
            //	//or list on Internet is not updated
            //	MessageBox.Show("Application version is not listed in update list on OneDrive.");
            //}

        }
        private void DownLoadVersionFileAndGetLastestVersion()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile("https://onedrive.live.com/download?cid=1B759AEF7A6E9391&resid=1B759AEF7A6E9391%21108&authkey=AKZj_GV9EY5Hwl0", @AppDomain.CurrentDomain.BaseDirectory + "//updateInfo.txt");
            }
            _allVersions = File.ReadAllLines(@AppDomain.CurrentDomain.BaseDirectory + "//updateInfo.txt");
            _versionFromInternet = _allVersions[_allVersions.Length - 1];
        }
        private Boolean IsValidVersionPair()
        {
            //TODO make a call to database to get its version
            //create a pair by taking first two numbers from database
            //see if they are the starting of your product version
            return true;
        }
        //private void ShowSyncProgress()
        //{
        //    try
        //    {
        //        AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
        //        aimDatabase = (AIMDatabaseInfo)lueDataMap.GetSelectedDataRow();
        //        aimDatabase = AIMManager.GetAIMDatabase(aimDatabase.AIMDatabaseID);
        //        if (aimDatabase == null || (aimDatabase.SyncProgressPercentage == 0 && aimDatabase.SyncTime == DateTime.MinValue))
        //        {
        //            lblSyncTimeStamp.Text = "Never sync or having problem getting info.";
        //            return;
        //        }
        //        if (aimDatabase.SyncProgressPercentage > 99)
        //        {
        //            lblSyncTimeStamp.Text = "Last Sync with AIM on " + aimDatabase.SyncTime;
        //            pbAimSync.Value = aimDatabase.SyncProgressPercentage;
        //            pbAimSync.Visible = false;
        //            btnSyncNow.Visible = true;
        //        }
        //        else
        //        {
        //            lblSyncTimeStamp.Text = "Synchronizing right now!";
        //            pbAimSync.Value = aimDatabase.SyncProgressPercentage;
        //            pbAimSync.Visible = true;
        //            btnSyncNow.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
        //    }
        //}
        #endregion
        #region Service test Code
        private void testServices()
        {
            UpdaterService.AppUpdaterWebService updService = new UpdaterService.AppUpdaterWebService();
            String a = updService.GetLatestUpdateLink(1);
            MessageBoxCFCshow(a);
        }

        #endregion
        #region Message Box Functions
        //CFC Message Box Methods, to make it accessible from other forms
        public void MessageBoxCFCshow(String message)
        {
            //messageBoxCFC.Show(message);
        }

        public void MessageBoxCFCshow(String message, Boolean checkOrcrossPic)
        {
            //messageBoxCFC.Show(message, checkOrcrossPic);
        }
        #endregion
        #region lueDataMap functions
        public void SetLueDataMapEditValue(Int32 val)
        {
            lueDataMap.EditValue = val;
            AIMCoreGlobal.AIMDatabaseID = val;
            AIMManager.SaveAIMConfig();
        }
        #endregion

        #region Splash Screen Functions
        public void SplashScreenShow()
        {
            if (splashScreenManager1.IsSplashFormVisible == false)
            {
                splashScreenManager1.ShowWaitForm();
            }
        }
        public void SplashScreenHide()
        {
            if (splashScreenManager1.IsSplashFormVisible == true)
            {
                splashScreenManager1.CloseWaitForm();
            }
        }
        #endregion
        #region Analysis
        private void analysisToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //2/27/17 - split out menu item by ingredient and product

            //try
            //{
            //             //AIMGlobal.SplashScreenShow();
            //             //AIMGlobal.open_form(analysis = new Analysis());
            //             //AIMGlobal.SplashScreenHide();
            //             Int32 openForms = Application.OpenForms.Count;
            //             Boolean chartsIsOpen = false;
            //             if (Application.OpenForms.Count > 1)
            //             {
            //                 for (int i = 0; i < Application.OpenForms.Count; i++)
            //                 {
            //                     if ((Application.OpenForms)[i].Name == "Charts")
            //                     {
            //                         chartsIsOpen = true;
            //                         DialogResult dr = new DialogResult();
            //                         AIMGlobal.SplashScreenHide();
            //                         dr = MessageBox.Show("Analysis needs the Chart screen, which is open. Do you want the Analysis screen to close the Chart?", "Warning ...", MessageBoxButtons.YesNo);
            //                         if (dr == DialogResult.Yes)
            //                         {
            //                             AIMGlobal.SplashScreenShow();
            //                             Application.OpenForms[i].Close();                   //close Charts screen 
            //                             AIMGlobal.open_form(analysis = new Analysis());     //open Analysis
            //                             AIMGlobal.SplashScreenHide();
            //                         }
            //                     }
            //                 }
            //                 if (chartsIsOpen == false)
            //                 {
            //                     AIMGlobal.SplashScreenShow();
            //                     AIMGlobal.open_form(analysis = new Analysis());
            //                     AIMGlobal.SplashScreenHide();
            //                 }
            //             }
            //             else
            //             {
            //                 AIMGlobal.SplashScreenShow();
            //                 AIMGlobal.open_form(analysis = new Analysis());
            //                 AIMGlobal.SplashScreenHide();
            //             }
            //         }
            //catch (Exception ex)
            //{
            //	MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            //}
        }

        #endregion
        #region Sample
        private void sampleToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            CFCAIMCore.Global.sampleID = 0;

            try
            {

                ////// Get all screens (monitors)                           //todo DB250915 Testing dual screens
                ////Screen[] screens = Screen.AllScreens;

                ////// Show this form on the primary screen
                ////this.StartPosition = FormStartPosition.Manual;
                ////this.Location = screens[0].WorkingArea.Location;

                ////// If a second screen exists, show another form there
                ////if (screens.Length > 1)
                ////{
                ////    tpProdSpec sample = new tpProdSpec();

                ////    sample.StartPosition = FormStartPosition.Manual;
                ////    sample.Location = screens[1].WorkingArea.Location;

                ////    AIMGlobal.SplashScreenShow();
                ////    AIMGlobal.open_form(sample);
                ////    AIMGlobal.SplashScreenHide();
                ////}                                                           //todo DB250915 testing dual screens end
                ////else
                ////{
                ///




                AIMGlobal.SplashScreenShow();


                AIMGlobal.open_form(sample = new tpProdSpec());
                AIMGlobal.SplashScreenHide();

                

                //sample.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        #endregion
        #region Data Exchange
        private void analysisDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void pendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(exportbatchpending = new ExportBatchPending());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void cloudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        #endregion
        #region Report
        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.SplashScreenShow();

                AIMGlobal.open_form(reportView = new ReportSimple());
                AIMGlobal.SplashScreenHide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void advanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.SplashScreenShow();
                AIMGlobal.open_form(nreportView = new ReportAdvanced());
                AIMGlobal.SplashScreenHide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region Analyzer
        private void instrumentToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                AIMGlobal.open_form(instrument = new Instrument());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        #endregion

        public MainMenu()
        {
            try
            {
                InitializeComponent();
                CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...");
            }
        }


        

        private void MainMenu_Load(object sender, EventArgs e)
        {
            try
            {

                Icon ico = Properties.Resources.CFCAIM;
                this.Icon = ico;
                WindowState = FormWindowState.Maximized;
                LoadlueDatabase();

                LoadlueDataMap();

                AIMGlobal.activeDataMap = (Int32)lueDataMap.EditValue;

                //TODO?				
                String version = Application.ProductVersion.ToString();

                resize.FindAllControls(this);

                if (Convert.ToBoolean(CFCAIM.AIMGlobal.AimDatabaseExists()) == false)
                {
                    try
                    {
                        AIMGlobal.open_form(dataMapping = new DataMapping());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
                    }
                }

                // set program and database version text fields                             			
                txtProgramVersion.Text = Version.ProgramAppVersion.ToString();
                txtDatabaseVersion.Text = Version.ProgramDatabaseVersion.ToString();

                /*
				//TODO - UPDATER - NOT DONE YET 
                VersionInfo version = new VersionInfo();
                version = AIMManager.GetVersion();	
                String currentVersion = version.Version;
				String currentDBVersion = version.Version;
                Int32 notificationNumber = 0;

				// get application appID			
				List<ApplicationInfo> app = new List<ApplicationInfo>();
				app = AIMManager.GetAppID("AIM");
				String appId = Convert.ToString(app[0].AppID);							

				//Check to see if there are updates to be made; if so, change color of flag								
				CFCUpdaterCore.Updater updater = new CFCUpdaterCore.Updater();
				if (updater.IsActive(appId))
				{
					if (updater.HasUpdate(appId, currentVersion, currentDBVersion))
					{
						notificationNumber = btnCFCSysNotification.GetNumberOfNotification();
						notificationNumber++;
						btnCFCSysNotification.SetNumberOfNotification(notificationNumber);						
						Int32 numberOfNotification = btnCFCSysNotification.GetNumberOfNotification();

						//Insert into AIMMessage table						
						AIMManager.InsertAIMMessage((Int32)EnumMessageType.System, "Update", userGlobal.CFCLoginUser.CFCUserID);
					}					
				}
				*/

                //1/8/24 - commented, until PNE tables are finalized
                //--------------------------------------------------------------                
                ////Int32 notificationNumber = 0;
                ////List<PNECreditInfo> result = new List<PNECreditInfo>();

                
                ////result = AIMManager.GetPNECredit();
                ////if (result.Count > 1)
                ////{
                ////    notificationNumber = btnCFCSysNotification.GetNumberOfNotification();
                ////    foreach (var item in result)
                ////    {
                ////        if (item.Credit == 0)
                ////        {
                ////            notificationNumber++;
                ////            btnCFCSysNotification.SetNumberOfNotification(notificationNumber);

                ////            AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Warning, "PNE 0 Credits for " + item.PNEDescription, 0,"Login", 0);


                ////        }
                ////    }
                ////    Int32 numberOfNotification = btnCFCSysNotification.GetNumberOfNotification();
                ////}                


                //--------------------------------------------------------------
                //4/10/18
                comboBox1.Items.Add("English");
                comboBox1.Items.Add("Spanish");
                comboBox1.Items.Add("French");
                comboBox1.SelectedIndex = 0;

                //--------------------------------------------------------------
                //2/10/20 - added this so error message on import will know to include (or not include) Spanish version of error
                AIMGlobal.mainMenuCustomToolStrip = false;
                if (this.customToolStripMenuItem.Enabled == true)
                {
                    AIMGlobal.mainMenuCustomToolStrip = true;
                }

                
                //Currently, this shows/hides JUST the main menu.  Need to add check of submenus.
                //--------------------------------------------------------------
                //10/26/22 - check UserMenuAccess table, using CFCUserID - to see what menu items the user is allowed to see.
                //           Disable/or make invisible any that the user should not see.
                foreach (var item in menuStrip.Items)
                {
                    String text = string.Empty;
                    text = item.ToString().Trim();

                    UserMenuAccessInfo userMenuAccess = new UserMenuAccessInfo();                    
                    {
                        userMenuAccess = UserManager.GetUserMenuAccessCFCUserID(userGlobal.CFCLoginUser.CFCUserID, text);
                        if (userMenuAccess != null)
                        {
                            if (userMenuAccess.IsNotAllowed == true)
                            {
                                ((ToolStripItem)item).Visible = false;
                            }
                        }                        
                    }                  
                }               

                //--------------------------------------------------------------

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                String appPath = @AppDomain.CurrentDomain.BaseDirectory;
                //SplashScreenShow();
                if (Directory.Exists(appPath + "\\AppUpdater") == false)
                {
                    MessageBox.Show("Application updater has not been setup yet.", "Message...");
                    return;
                }
                if (File.Exists(appPath + "\\AppUpdater\\config.xml") == false)
                {
                    MessageBox.Show("Application updater does not have config.xml. Could not load AppUpdater.", "Message...");
                    return;
                }
                String appName = AIMGlobal.appName;//Properties.Settings.Default.AppName;
                String appVersion = AIMGlobal.appVersion;//Properties.Settings.Default.AppVersion;
                String versionFileLink = AIMGlobal.versionFileLink; //Properties.Settings.Default.VersionFileLink;
                String updateVersion = AIMGlobal.updateID;//Properties.Settings.Default.UpdateVersion;
                String processName = System.AppDomain.CurrentDomain.FriendlyName;

                #region updating XML
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(appPath + "\\AppUpdater\\config.xml");
                    IEnumerator ie = xmlDoc.SelectNodes("/config/file").GetEnumerator();

                    while (ie.MoveNext())
                    {
                        (ie.Current as XmlNode).Attributes["SoftwareName"].Value = appName;
                        (ie.Current as XmlNode).Attributes["Version"].Value = appVersion;
                        (ie.Current as XmlNode).Attributes["VersionFileLink"].Value = versionFileLink;
                        (ie.Current as XmlNode).Attributes["UpdateVersion"].Value = updateVersion;
                        (ie.Current as XmlNode).Attributes["ProcessName"].Value = processName;
                    }

                    xmlDoc.Save(appPath + "\\AppUpdater\\config.xml");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
                #endregion

                #region setting appConfig and other config file
                if (File.Exists(appPath + "\\AppUpdater\\CFCAppUpdater.exe.config"))
                {
                    File.Delete(appPath + "\\AppUpdater\\CFCAppUpdater.exe.config");
                }
                if (File.Exists(appPath + "\\AppUpdater\\CFCAppUpdater.vshost.exe.config"))
                {
                    File.Delete(appPath + "\\AppUpdater\\CFCAppUpdater.vshost.exe.config");
                }
                File.Copy(appPath + "\\CFCAIM.exe.config", appPath + "\\AppUpdater\\CFCAppUpdater.exe.config");
                File.Copy(appPath + "\\CFCAIM.vshost.exe.config", appPath + "\\AppUpdater\\CFCAppUpdater.vshost.exe.config");
                #endregion

                #region starting update application
                ProcessStartInfo pi = new ProcessStartInfo();
                pi.Verb = "runas";//to run the app as admin
                pi.FileName = appPath + "\\AppUpdater\\CFCAppUpdater.exe";
                Process.Start(pi);
                #endregion
                //SplashScreenHide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void btnDeveloperWindow_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(devWin = new DeveloperWindow());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                //1/12/18
                _loginUser = _global.CFCLoginUser;
                _loginUserRoles = UserManager.GetUserRoles(_loginUser.UserName, _loginUser.ApplicationName);
                if (!IsAdmin())
                {
                    MessageBox.Show("Administrative privileges are required to access this form.", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (userListing == null || userListing.Visible == false)
                {

                    AIMGlobal.SplashScreenShow();
                    userListing = new UserListing();
                    userListing.MdiParent = this;
                    Icon ico = Properties.Resources.CFCAIM;
                    userListing.Icon = ico;
                    userListing.Show();                    
                    AIMGlobal.SplashScreenHide();
                    userListing.WindowState = FormWindowState.Maximized; //7/10/2026 FG
                }
                else
                {
                    //10/6/21 - changed for resize issue
                    //userListing.WindowState = FormWindowState.Normal;                   
                    userListing.WindowState = FormWindowState.Maximized;                  

                    userListing.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void utilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void dataMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(dataMapping = new DataMapping());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void lueDataMap_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

            //6/9/17 (Task 403) - bypass "close all forms" message if coming from Formulation screen, after deleting a template.
            if (((Form)((Control)sender).TopLevelControl).Text == "CFCAIM - [Formulation]")
            {
                _showCloseAllFormsMessage = false;
            }


            e.Cancel = CloseAllForms();
        }

        private void lueDataMap_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.activeDataMap = (Int32)lueDataMap.EditValue;

                if ((Int32)lueDataMap.EditValue > 0)
                {
                    AIMCoreGlobal.AIMDatabaseID = (Int32)lueDataMap.EditValue;
                    AIMCoreGlobal.AIMDatabaseIsActive = true;
                    AIMManager.SaveAIMConfig();

                    //9/9/20 - change to use AIMCoreGlobal
                    //AIMGlobal.aimDatabase = (AIMDatabaseInfo)lueDataMap.GetSelectedDataRow();
                    //if (AIMGlobal.aimDatabase != null)
                    //{
                    //    AIMCoreGlobal.DefaultForPropose = AIMGlobal.aimDatabase.DefaultForPropose;
                    //}
                    CFCAIMCore.Global.aimDatabase = (AIMDatabaseInfo)lueDataMap.GetSelectedDataRow();
                    if (CFCAIMCore.Global.aimDatabase != null)
                    {
                        AIMCoreGlobal.DefaultForPropose = CFCAIMCore.Global.aimDatabase.DefaultForPropose;
                    }


                    if (backgroundWorkerSyncProgress.IsBusy == false)
                    {
                        backgroundWorkerSyncProgress.RunWorkerAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void btnSyncNow_Click(object sender, EventArgs e)
        {
            //TODO UPDATE THE TABLE
            IsBackGroundSync = true;
        }
        private void versionManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DownLoadVersionFileAndGetLastestVersion();
                Thread.Sleep(1000);
                CheckIfVersionListed();
                if (IsValidVersionPair() == false)
                {
                    //application is updated but not database
                    //    application, or user will download the database script to update it.
                    //database is updated but not application
                    //    application will ask to user to download application from cfc website then install it.
                    //*it may also happen that we forgot to change one or another but lets think about it later.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LanguageTest langForm = new LanguageTest();
            langForm.Show();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(sampleInfoForProcessResult = new SampleInfoForProcessResult());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void CFCSysNotification_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("see funtion CFCSysNotification_Click in main menu to see the documentation of this tool.", "message from Faizan");
            ////to set number of notification use SetNumberOfNotification
            //btnCFCSysNotification.SetNumberOfNotification(10);
            ////to get number of notification use GetNumberOfNotification
            //Int32 numberOfNotification = btnCFCSysNotification.GetNumberOfNotification();
            SystemLog systemLogForm = new SystemLog();
            systemLogForm.Show();
        }
        
        private void nIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(instrument = new Instrument());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void serviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(webService = new WebService());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(about = new About());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(emailAddress = new EmailAddress());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    AIMGlobal.open_form(instrument = new Instrument());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
                }
            }
        }

        private void inSampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(utilities = new Utilities());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void messagingEmailAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(emailAddressXRefMessage = new EmailAddressXRefMessage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void formulationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void userDefinedDataSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(dataSourceUser = new DataSourceUser());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void preDefinedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                AIMGlobal.open_form(processR = new ImportData());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void userDefinedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(importSampleUserDefined = new ImportDataUserDefined());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }



        private void dataSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(dataSourceUser = new DataSourceUser());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(defaultSettings = new DefaultSettings());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void userDefinedToolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip_MouseHover(object sender, EventArgs e)
        {
            if (sender is ToolStripDropDownItem)
            {
                ToolStripDropDownItem item = sender as ToolStripDropDownItem;
                if (item.HasDropDownItems && !item.DropDown.Visible)
                {
                    item.ShowDropDown();
                }
            }
        }


        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(importMapping = new ImportMapping());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void chartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //8/3/17
                Int32 openForms = Application.OpenForms.Count;
                Boolean analysisIsOpen = false;
                if (Application.OpenForms.Count > 1)
                {
                    for (int i = 0; i < Application.OpenForms.Count; i++)
                    {
                        if ((Application.OpenForms)[i].Name == "Analysis")
                        {
                            analysisIsOpen = true;
                        }
                    }
                }
                if (analysisIsOpen == false)
                {
                    AIMGlobal.comingFromAnalysis = false;
                }

                AIMGlobal.open_form(charts = new Charts());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void tsmDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean isCancel = false;

                isCancel = CloseAllForms();
                if (isCancel == false)
                {
                    DatabaseConnection databaseConnection = new DatabaseConnection();
                    this.Visible = false;
                    databaseConnection.ShowDialog();

                    //1/25/18
                    if (AIMManager.DatabaseConnection == String.Empty)
                    {
                        //4/25/18
                        AIMManager.SaveCFCUsersLoggedInStatus(userGlobal.CFCLoginUser.CFCUserID, false);

                        Application.Exit();
                    }

                    this.Visible = true;
                    LoadlueDatabase();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void lueDatabase_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                AIMGlobal.activeDataMap = 0;
                List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
                Int32 databaseID = 0;
                Int32.TryParse(lueDatabase.EditValue.ToString(), out databaseID);
                databases = (List<SQLDatabaseInfo>)lueDatabase.Properties.DataSource;
                
                //12/6/21 - commented
                //SQLDatabaseConnection.SetDefaultConnection(ref databases, databaseID, SQLDatabaseConnection.CFCAIMDatabaseFile);

                
                //12/6/21 - Set user-default connection                   
                if (lueDatabase.OldEditValue != null)
                {
                    UserDefaultDatabase.UpdateUserDefault(userGlobal.CFCLoginUser.UserName, lueDatabase.Text);
                }

                //11/30/21 - for Non-Production connections, change font to red
                lueDatabase.ForeColor = Color.Black;
                if (databases[databaseID].ProductionOrNon == "N")
                {
                    lueDatabase.ForeColor = Color.Red;
                }


                AIMManager.IsMultipleDatabase = true;
                AIMManager.DatabaseConnection = SQLDatabaseConnection.GetSQLConnection(databases[databaseID].ServerName, databases[databaseID].IsSQLAuthentication, databases[databaseID].User, databases[databaseID].Password, databases[databaseID].Database);
                AIMManager.CloseAIMData();

                //11/3/22
                UserManager.CloseUserData();
                                
                
                LoadlueDataMap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueDatabase_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            try
            {
                e.Cancel = CloseAllForms();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void formulationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //3/12/18 - Check UserAccess Permission
                Int32 rtn = CFCAIM.AIMUtilities.GlobalFunctions.CheckUserPermissions(userGlobal.CFCLoginUser.CFCUserID, (Int32)EnumPermissionTable.CrossReferenceMaintenance);
                if (rtn == -1)
                {
                    return;
                }

                AIMGlobal.open_form(formulationCrossReference = new FormulationCrossReference());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void cFCToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void AnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(analyzer = new Maintenance.Analyzer());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(aim = new AIM());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void toolStripMenuItem5_Click_1(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(dataMapping = new DataMapping());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }



        private void InitialLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //{
            //	try
            //	{
            //		AIMGlobal.open_form(initialLoadAimNutrients = new InitialLoadAimNutrientsMaterials());
            //	}
            //	catch (Exception ex)
            //	{
            //		MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            //	}
            //}
        }

        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void testToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CFCAIM.AIMGlobal.ImportMappingTabControlVisible = false;
            AIMGlobal.lblWizardCaption = "Add New Template";

            try
            {
                AIMGlobal.open_form(mapWizard = new Imports.MapWizard());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }

        }

        private void testToolStripMenuItem_Click_2(object sender, EventArgs e)
        {

        }

        private void analyzerToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void aIMSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(systemSettings = new SystemSettings());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void testToolStripMenuItem_Click_3(object sender, EventArgs e)
        {
        }

        private void testToolStripMenuItem_Click_4(object sender, EventArgs e)
        {
        }

        private void mappingToolStripMenuItem1_Click(Object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.analyzerDataSourceDefinitionID = 0;
                AIMGlobal.open_form(importMapping = new ImportMapping());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void userDefinedToolStripMenuItem1_Click(Object sender, EventArgs e)
        {

        }

        private void dataSourceTypeToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(dataSourceUser = new DataSourceUser());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void analyzerToolStripMenuItem2_Click(Object sender, EventArgs e)
        {
            try
            {
                //3/12/18 - Check UserAccess Permission
                Int32 rtn = CFCAIM.AIMUtilities.GlobalFunctions.CheckUserPermissions(userGlobal.CFCLoginUser.CFCUserID, (Int32)EnumPermissionTable.CrossReferenceMaintenance);
                if (rtn == -1)
                {
                    return;
                }

                AIMGlobal.open_form(analyzerCrossReference = new AnalyzerCrossReference());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void updateCFCAIMToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {
                //TODO - NOT DONE YET
                /*
                  
                String msg = "";
				AIMGlobal.SplashScreenHide();
				DialogResult dr = new DialogResult();
				msg = "Updating AIM will close your current session. ";
				msg += Environment.NewLine + Environment.NewLine + "Are you sure you want to update AIM at this time? ";
				dr = MessageBox.Show(msg, "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dr == DialogResult.No)
				{
					return;
				}                		

				//LEFT TO BE DONE:  CALL NEW EXECUTABLE TO LOOP THROUGH SQL UPDATE COMMANDS 
				//					(Right now that code is under testUpdater_Click (see below))

				Application.Exit();

                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void testUpdater_Click(Object sender, EventArgs e)
        {
            //TODO - NOT DONE YET

            //THIS RUNS THROUGH SQL SCRIPT TO UPDATE CLIENT DATABASE (WITH STP'S, TABLES, ETC.)

            //LEFT TO BE DONE:  
            //		Ask Mark about back-up/restore commands -- make sure everything needed is there.
            //		Ask Mark about hard-coded fields - where will these come from?			
            //		Add section to run commands for update of supporting tables.		  
            //		Put this code into its own executable (to be called under updateCFCAIMToolStripMenuItem_Click above).
            //		As part of executable, on client side, backup current CFCAIM.exe and copy over new one.

            String sql = "";
            String str = "";
            String line = "";
            AIMUpdateLogInfo checkSql = new AIMUpdateLogInfo();

            try
            {
                String path = "C:\\Test\\";
                String file = "sqlupdate.txt";
                String filename = path + file;
                String backUpPath = "E:\\Program Files\\Microsoft SQL Server\\MSSQL11.SQL2012\\MSSQL\\Backup\\";
                String strConn = "Data Source = CFCSERVER\\SQL2012;Initial Catalog=CFCAIMDEVELOPMENT; User ID=sa; Password=SQL2012";
                String backupFile = "";
                String versionFile = "sqlVersion.txt";
                String versionFileName = path + versionFile;

                AIMGlobal.SplashScreenShow();

                //Steps:
                //  backup database
                //  back up C:\CFCAIM\app files
                //  run sql commands
                //  copy new files into C:\CFCAIM\app files
                //  update version-date

                //------------------------------------------------------
                #region Backup dbase
                //BACKUP database to the path of the database (get from sys.databases?)		
                backupFile = BackupDatabase(backUpPath, strConn);

                #endregion

                //------------------------------------------------------
                #region GetBatchID
                // get next batchID	  				
                List<AIMUpdateLogInfo> log = new List<AIMUpdateLogInfo>();
                log = AIMManager.GetAimUpdateLogLastBatchID();
                if (log != null && log.Count != 0)
                {
                    _batchID = log[0].BatchID + 1;
                }
                #endregion

                //------------------------------------------------------
                #region Loop through SqlUpdate commands
                SqlConnection conn = new SqlConnection(strConn);
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                SqlDataReader reader;
                StreamReader sReader = new StreamReader(filename);
                while ((line = sReader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line == "GO")
                    {
                        sql = "";
                        _commandType = GetCommandType(str.Substring(0, Math.Min(20, str.Length)).ToUpper());
                        _sqlCommand = str;

                        //----------------------------------------------------------						
                        //VALIDATE QUERY - makes sure sql is valid before sending	
                        checkSql = AIMManager.AIMUpdateCheckSqlStatement(str);
                        if (checkSql.ErrorNumber > 0)
                        {
                            _message = checkSql.Message;
                            _errorNumber = checkSql.ErrorNumber;
                            AIMUpdateLog();
                        }
                        else
                        {
                            //These are "batch" commands so don't include "begin/end try and catch"															
                            if ((_commandType == (Int32)EnumSqlCommandType.CreateProcedure) ||
                                 (_commandType == (Int32)EnumSqlCommandType.AlterProcedure) ||
                                 (_commandType == (Int32)EnumSqlCommandType.DropProcedure) ||
                                 (_commandType == (Int32)EnumSqlCommandType.CreateFunction) ||
                                 (_commandType == (Int32)EnumSqlCommandType.AlterFunction) ||
                                 (_commandType == (Int32)EnumSqlCommandType.DropFunction))
                            {
                                sql += str;
                            }
                            else
                            {
                                sql = " BEGIN TRY ";
                                sql += str;
                                sql += " END TRY BEGIN CATCH ";
                                sql += " IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; ";
                                sql += " SELECT ERROR_MESSAGE() AS ErrorMessage, ERROR_NUMBER() AS ErrorNumber, ";
                                sql += " ERROR_LINE() AS ErrorLine, ERROR_SEVERITY() AS ErrorSeverity, ERROR_STATE() as ErrorState ";
                                sql += " END CATCH ";
                            }

                            cmd.CommandText = sql;
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = conn;
                            conn.Open();
                            try
                            {
                                reader = cmd.ExecuteReader();
                                if (reader.FieldCount > 0)
                                {
                                    reader.Read();
                                    _message = reader.GetValue(0).ToString();
                                    _errorNumber = (Int32)reader.GetValue(1);
                                    AIMUpdateLog();
                                }
                                else
                                {
                                    AIMUpdateLog();
                                }
                            }
                            catch (Exception ex)
                            {
                                _message = ex.Message;
                                _errorNumber = ((SqlException)ex).Number;
                                AIMUpdateLog();

                            }

                            conn.Close();
                        }
                        str = "";
                    }
                    else
                    {
                        //Skip comment lines
                        if (line.Length > 1 && line.Substring(0, 2) == "--") { }
                        else
                        {
                            str += line + " ";
                        }
                    }
                }
                #endregion

                //------------------------------------------------------
                #region Restore If Errors; Otherwise, Update VersionDate
                //check AimErrorLog for BatchID -- to see if there were errors
                List<AIMUpdateLogInfo> errorLog = new List<AIMUpdateLogInfo>();
                errorLog = AIMManager.GetAimUpdateLog(_batchID);
                if (errorLog != null && errorLog.Count != 0)
                {
                    #region Restore dbase
                    //RESTORE database from the path of the database (get from sys.databases?)
                    RestoreDatabase(backupFile, strConn);
                    AIMGlobal.SplashScreenHide();
                    MessageBox.Show("There were " + errorLog.Count + " ERRORS on the update. Please contact IT.", "Warning...", MessageBoxButtons.OK);
                    #endregion
                }
                else
                {
                    #region Update Version	
                    StreamReader vReader = new StreamReader(versionFileName);
                    while ((line = vReader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        str += line + " ";
                        sql = "";
                        _commandType = (Int32)EnumSqlCommandType.Insert;
                        _sqlCommand = str;

                        sql = " BEGIN TRY ";
                        sql += str;
                        sql += " END TRY BEGIN CATCH ";
                        sql += " IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; ";
                        sql += " SELECT ERROR_MESSAGE() AS ErrorMessage, ERROR_NUMBER() AS ErrorNumber, ";
                        sql += " ERROR_LINE() AS ErrorLine, ERROR_SEVERITY() AS ErrorSeverity, ERROR_STATE() as ErrorState ";
                        sql += " END CATCH ";

                        cmd.CommandText = sql;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = conn;
                        conn.Open();
                        try
                        {
                            reader = cmd.ExecuteReader();
                            if (reader.FieldCount > 0)
                            {
                                reader.Read();
                                _message = reader.GetValue(0).ToString();
                                _errorNumber = (Int32)reader.GetValue(1);
                                AIMUpdateLog();
                            }
                            else
                            {
                                AIMUpdateLog();
                            }
                        }
                        catch (Exception ex)
                        {
                            _message = ex.Message;
                            _errorNumber = ((SqlException)ex).Number;
                            AIMUpdateLog();
                        }
                        conn.Close();
                    }

                    #endregion
                }

                #endregion

            }

            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
            AIMGlobal.SplashScreenHide();
        }

        private Int32 GetCommandType(String str)
        {
            Int32 rtn = 0;
            try
            {
                if (str.Contains("PRINT ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.Print;
                }
                else if (str.Contains("INSERT INTO ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.Insert;
                }
                else if (str.Contains("UPDATE ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.Update;
                }
                else if (str.Contains("DELETE ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.Delete;
                }
                else if (str.Contains("CREATE TABLE ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.CreateTable;
                }
                else if (str.Contains("ALTER TABLE ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.AlterTable;
                }
                else if (str.Contains("DROP TABLE ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.DropTable;
                }
                else if (str.Contains("CREATE PROCEDURE ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.CreateProcedure;
                }
                else if (str.Contains("ALTER PROCEDURE ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.AlterProcedure;
                }
                else if (str.Contains("DROP PROCEDURE ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.DropProcedure;
                }
                else if (str.Contains("CREATE FUNCTION ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.CreateProcedure;
                }
                else if (str.Contains("ALTER FUNCTION ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.AlterFunction;
                }
                else if (str.Contains("DROP FUNCTION ") == true)
                {
                    rtn = (Int32)EnumSqlCommandType.DropFunction;
                }

            }
            catch (Exception)
            {
                AIMGlobal.SplashScreenHide();
            }
            return rtn;
        }

        private void AIMUpdateLog()
        {
            //The log is updated for every transaction, not just errors.

            try
            {
                AIMUpdateLogInfo errorLog = new AIMUpdateLogInfo();
                errorLog.BatchID = _batchID;
                errorLog.SqlCommand = _sqlCommand;
                errorLog.Message = _message;
                errorLog.ErrorNumber = _errorNumber;
                errorLog.CommandType = _commandType;
                errorLog.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                ErrorInfo error = new ErrorInfo();
                error = AIMManager.InsertAIMUpdateLog(ref errorLog);
                if (error.ErrorNumber > 0)
                {
                    return;
                }

                _sqlCommand = "";
                _message = "";
                _errorNumber = 0;
                _commandType = 0;

            }
            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private String BackupDatabase(String backUpPath, String strConn)
        {
            String rtn = "";
            try
            {
                // get backup folder from config file  -- <add key="BackupFolder" value="E:\Program Files\Microsoft SQL Server\MSSQL11.SQL2012\MSSQL\Backup\"/>
                var backupFolder = ConfigurationManager.AppSettings["BackupFolder"];

                var sqlConStrBuilder = new SqlConnectionStringBuilder(strConn);

                // set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")
                var backupFileName = String.Format("{0}{1}-{2}.bak",
                    backupFolder, sqlConStrBuilder.InitialCatalog,
                    DateTime.Now.ToString("yyyy-MM-dd"));

                String str = "";
                SqlConnection conn = new SqlConnection(strConn);
                SqlCommand cmd = conn.CreateCommand();

                str = "BACKUP DATABASE[" + sqlConStrBuilder.InitialCatalog + "] ";
                str += "TO DISK = '" + backupFileName + "' ";
                str += "WITH INIT, NOUNLOAD, NOSKIP, STATS = 10, NOFORMAT";

                cmd.CommandText = str;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                rtn = backupFileName;
            }
            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
                rtn = "Error on Backup";

            }
            return rtn;
        }

        private void RestoreDatabase(String backupFile, String strConn)
        {
            try
            {
                String str = "";
                SqlConnection conn = new SqlConnection(strConn);
                SqlCommand cmd = conn.CreateCommand();
                var sqlConStrBuilder = new SqlConnectionStringBuilder(strConn);
                String dbaseName = sqlConStrBuilder.InitialCatalog;

                str = "USE MASTER ";
                str += "ALTER DATABASE " + dbaseName + " ";
                str += "SET SINGLE_USER WITH ROLLBACK IMMEDIATE ";
                str += "RESTORE DATABASE " + dbaseName + " ";
                str += "FROM DISK= '" + backupFile + "' ";
                str += "WITH REPLACE ";
                str += "ALTER DATABASE " + dbaseName + " ";
                str += "SET MULTI_USER WITH ROLLBACK IMMEDIATE";

                cmd.CommandText = str;
                cmd.CommandTimeout = 0;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);

            }
        }

        private void MainMenu_Resize(Object sender, EventArgs e)
        {                
            resize.ResizeAllControls(this);
        }

        private void testToolStripMenuItem_Click_7(Object sender, EventArgs e)
        {
            testUpdater_Click(sender, e);
        }        
                
        private Boolean CheckOpenForms()            
        {                                                                                                                   
            DialogResult dr = new DialogResult();            
            Int32 openForms = Application.OpenForms.Count;
            Boolean chartsIsOpen = false;

            if (Application.OpenForms.Count > 1)
            {                                                                  

                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    if ((Application.OpenForms)[i].Name == "Charts")
                    {                                                                                                            
            
                        chartsIsOpen = true;                                                                                                                                    
                        AIMGlobal.SplashScreenHide();                                                                        
                        dr = MessageBox.Show("Analysis needs the Chart screen, which is open. Do you want the Analysis screen to close the Chart?", "Warning ...", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {                        
                        
                            AIMGlobal.SplashScreenShow();                        
                            Application.OpenForms[i].Close();                   //close Charts screen 
                            AIMGlobal.open_form(analysis = new Analysis());     //open Analysis  
                            AIMGlobal.SplashScreenHide();
                        }
                    }
                }
                
                if (chartsIsOpen == false)
                {
                
                    AIMGlobal.SplashScreenShow();                                    
                    AIMGlobal.open_form(analysis = new Analysis());                
                    AIMGlobal.SplashScreenHide();                                                                
                
                }
            }
                    
            return chartsIsOpen;
        }               

        //2/18/21 - this is old I think -- and should have been removed at some point
        //////private void productToolStripMenuItem_Click(Object sender, EventArgs e)
        //////{        //////    try
        //////    {
        //////        Int32 openForms = Application.OpenForms.Count;
        //////        Boolean chartsIsOpen = false;
        //////        if (Application.OpenForms.Count > 1)
        //////        {
        //////            for (int i = 0; i < Application.OpenForms.Count; i++)
        //////            {
        //////                if ((Application.OpenForms)[i].Name == "Charts")
        //////                {
        //////                    chartsIsOpen = true;
        //////                    DialogResult dr = new DialogResult();
        //////                    AIMGlobal.SplashScreenHide();
        //////                    dr = MessageBox.Show("Analysis needs the Chart screen, which is open. Do you want the Analysis screen to close the Chart?", "Warning ...", MessageBoxButtons.YesNo);
        //////                    if (dr == DialogResult.Yes)
        //////                    {
        //////                        AIMGlobal.SplashScreenShow();
        //////                        Application.OpenForms[i].Close();                   //close Charts screen 
        //////                        AIMGlobal.open_form(analysis = new Analysis());     //open Analysis
        //////                        AIMGlobal.SplashScreenHide();
        //////                    }
        //////                }
        //////            }
        //////            if (chartsIsOpen == false)
        //////            {
        //////                AIMGlobal.SplashScreenShow();
        //////                AIMGlobal.open_form(analysis = new Analysis());
        //////                AIMGlobal.SplashScreenHide();
        //////            }
        //////        }
        //////        else
        //////        {
        //////            AIMGlobal.SplashScreenShow();
        //////            AIMGlobal.open_form(analysis = new Analysis());
        //////            AIMGlobal.SplashScreenHide();
        //////        }
        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
        //////    }
        //////}

        private void cFCTechToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(utilitiesCFCTech = new UtilitiesCFCTech());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void syncSampleAIMTableToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(sampleAIMSync = new RebuildSampleAIMSync());               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void ingredientToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {                                             
                Int32 rtn = 0;
                rtn = CheckSampleAIM();                                                
                if (rtn == 1)
                {                
                    AIMGlobal.open_form(sampleAIMSync = new RebuildSampleAIMSync());                                    
                    return;
                }

                Boolean chartsIsOpen = false;
                chartsIsOpen = CheckOpenForms();
                if (chartsIsOpen == false)
                {

                    AIMGlobal.SplashScreenShow();
                    AIMGlobal.open_form(analysis = new Analysis());
                    AIMGlobal.SplashScreenHide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }        
                
        private Int32 CheckSampleAIM()
        {
            try
            {

                DialogResult dr = new DialogResult();
                String msg = "";
                Int32 rtn = 0;

                //----------------------------------------------------------------------------
                //Check system setting to see if SampleAim table needs to be rebuilt
                SystemSettingsInfo systemSettings = new SystemSettingsInfo();
                systemSettings = AIMManager.GetSystemSettings();
                if (systemSettings != null)
                {
                    if (systemSettings.SampleAimNeedsRebuild == true)
                    {
                        AIMGlobal.SplashScreenHide();
                        msg = "Cross-reference changes have been made, but ";
                        msg += Environment.NewLine + "the changes will not be reflected in Analysis until";
                        msg += Environment.NewLine + "the Sample AIM table is rebuilt. ";

                        //5/3/18 - added this with default=yes -- so user is redirected to the Rebuild screen.
                        msg += Environment.NewLine + Environment.NewLine + "Do you want to rebuild the table now?";
                        msg += Environment.NewLine + Environment.NewLine + "(The rebuild may take a few minutes.)";

                        //1/27/25
                        //dr = MessageBox.Show(msg, "Warning ...", MessageBoxButtons.YesNo);
                        dr = MessageBox.Show(msg, "Warning ...", MessageBoxButtons.YesNo);

                        if (dr == DialogResult.Yes)
                        {
                            rtn = 1;
                        }
                    }
                }
                return rtn;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
                return -1;
            }
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 rtn = 0;
            rtn = CheckSampleAIM();
            if (rtn == 1)
            {
                AIMGlobal.open_form(sampleAIMSync = new RebuildSampleAIMSync());
                return;
            }

            Boolean chartsIsOpen = false;
            chartsIsOpen = CheckOpenForms();
            if (chartsIsOpen == false)
            {
                AIMGlobal.SplashScreenShow();
                AIMGlobal.open_form(analysisProduct = new AnalysisProduct());
                AIMGlobal.SplashScreenHide();
            }
        }

        private void importFileTypesToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {             
                AIMGlobal.open_form(fileTypes = new FileTypes());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void toolStripMenuItem3_Click(Object sender, EventArgs e)
        {

        }

        private void testMathTypeToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(mathType = new MathType());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void testGuaranteesToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(guaranteeRefund = new GuaranteeRefund());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void testToolStripMenuItem_Click_9(Object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(guaranteeRefund = new GuaranteeRefund());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void testToolStripMenuItem_Click_5(Object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(testGrid = new TestGrid());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void otherToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {            
                AIMGlobal.open_form(exportOther = new ExportOther());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void settingsToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            //1/11/18
            try
            {             
                AIMGlobal.open_form(userDefaults = new UserDefaults());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        //1/12/18
        private Boolean IsAdmin()
        {
            List<RolesInfo> loginUserRole = new List<RolesInfo>();
            loginUserRole = UserManager.GetUserRoles(_loginUser.UserName, _loginUser.ApplicationName);
            for (int i = 0; i < loginUserRole.Count; i++)
            {
                if (loginUserRole[i].RoleName == "Administrator")
                {
                    return true;
                }
            }
            return false;
        }

        //2/14/18
        private void userDefinedReportsToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            SplashScreenShow();
            XtraReport1 report = new XtraReport1();
            ReportDesignTool designTool = new ReportDesignTool(report);
            UserLookAndFeel lookAndFeel = new UserLookAndFeel(this);
            lookAndFeel.UseDefaultLookAndFeel = false;
            lookAndFeel.SkinName = "Visual Studio 2013 Blue";
            designTool.ShowRibbonDesignerDialog(lookAndFeel,
                     DevExpress.XtraReports.UserDesigner.DesignDockPanelType.ErrorList |
                     DevExpress.XtraReports.UserDesigner.DesignDockPanelType.ReportExplorer |
                     DevExpress.XtraReports.UserDesigner.DesignDockPanelType.PropertyGrid |
                     DevExpress.XtraReports.UserDesigner.DesignDockPanelType.GroupAndSort |
                     DevExpress.XtraReports.UserDesigner.DesignDockPanelType.ToolBox

                    );
            SplashScreenHide();

        }

        private void testToolStripMenuItem_Click_6(Object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(languageTest = new LanguageTest());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        
        private void comboBox1_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "English")
            {
                ChangeLanguage("en");
            }
            else if (comboBox1.SelectedItem.ToString() == "Spanish")
            {
                ChangeLanguage("es-ES");
            }
            else
            {
                ChangeLanguage("fr-FR");
            }
        }

        //4/10/18
        private void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(MainMenu));
                resources.ApplyResources(c, c.Name, new CultureInfo(lang));
            }
        }


        private void testToolStripMenuItem_Click_10(Object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(testPivotGrid = new TestPivotGrid());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void usersCurrentlyLoggedInToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            //4/25/18
            try
            {
                //9/9/21
                _loginUser = _global.CFCLoginUser;
                _loginUserRoles = UserManager.GetUserRoles(_loginUser.UserName, _loginUser.ApplicationName);
                if (!IsAdmin())
                {
                    MessageBox.Show("Administrative privileges are required to access this form.", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AIMGlobal.open_form(usersLoggedIn = new UsersLoggedIn());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void MainMenu_FormClosed(Object sender, FormClosedEventArgs e)
        {
            //4/25/18
            AIMManager.SaveCFCUsersLoggedInStatus(userGlobal.CFCLoginUser.CFCUserID, false);
        }

        private void customToolStripMenuItem_Click(Object sender, EventArgs e)
        {

        }

        //5/10/18
        private void sampleReceivingToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {
                //9/16/20
                //CFCAIMCustom.Manager.CustomManager.IsMultipleDatabase = true;
                //CFCAIMCustom.Manager.CustomManager.DatabaseConnection = AIMManager.DatabaseConnection;
                CFCAIM.Custom.Vitapro.Manager.CustomManager.IsMultipleDatabase = true;
                CFCAIM.Custom.Vitapro.Manager.CustomManager.DatabaseConnection = AIMManager.DatabaseConnection;
                AIMGlobal.open_form(sampleReceiving = new SampleReceiving());                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void sampleProductionToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {
                //9/16/20
                //CFCAIMCustom.Manager.CustomManager.IsMultipleDatabase = true;
                //CFCAIMCustom.Manager.CustomManager.DatabaseConnection = AIMManager.DatabaseConnection;
                CFCAIM.Custom.Vitapro.Manager.CustomManager.IsMultipleDatabase = true;
                CFCAIM.Custom.Vitapro.Manager.CustomManager.DatabaseConnection = AIMManager.DatabaseConnection;                
                AIMGlobal.open_form(sampleProduction = new SampleProduction());                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        //8/23/19 - changed to different menu line item
        ////5/24/18
        //private void lineaMantenimientoToolStripMenuItem_Click(Object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CFCAIMCustom.Manager.CustomManager.IsMultipleDatabase = true;
        //        CFCAIMCustom.Manager.CustomManager.DatabaseConnection = AIMManager.DatabaseConnection;
        //        AIMGlobal.open_form(lineMaintenance = new LineMaintenance());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
        //    }
        //}

        private void defaultPrintersToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(printerSetup = new PrinterSetup());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void printerSetupToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(printerSetup = new PrinterSetup());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void testToolStripMenuItem_Click_8(Object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(testChart = new testChart());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void schedulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(schedulerMain = new SchedulerMain());            
                ///MessageBox.Show("The event scheduler is not yet available.", "Warning...", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }


        //8/26/19 - moved to different menu item line
        private void lineaMantenimientoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //9/16/20
                //CFCAIMCustom.Manager.CustomManager.IsMultipleDatabase = true;
                //CFCAIMCustom.Manager.CustomManager.DatabaseConnection = AIMManager.DatabaseConnection;
                CFCAIM.Custom.Vitapro.Manager.CustomManager.IsMultipleDatabase = true;
                CFCAIM.Custom.Vitapro.Manager.CustomManager.DatabaseConnection = AIMManager.DatabaseConnection;

                AIMGlobal.open_form(lineMaintenance = new LineMaintenance());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        //8/26/19
        private void prepararToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    //9/16/20
                    //CFCAIMCustom.Manager.CustomManager.IsMultipleDatabase = true;
                    //CFCAIMCustom.Manager.CustomManager.DatabaseConnection = AIMManager.DatabaseConnection;
                    CFCAIM.Custom.Vitapro.Manager.CustomManager.IsMultipleDatabase = true;
                    CFCAIM.Custom.Vitapro.Manager.CustomManager.DatabaseConnection = AIMManager.DatabaseConnection;

                    AIMGlobal.open_form(settingsCustom = new SettingsCustom());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
                }
            }
        }

        private void testToolStripMenuItem_Click_11(object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(testcm4 = new test4());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void uploadDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                
                String str = String.Empty;
                String database = String.Empty;
                String backupPath = String.Empty;
                String fileName = String.Empty;
                String zipFileToCreate = String.Empty;
                Int32 rtn = 0;
                Int32 count = 0;


                //------------------------------------------------------------
                //GET DATABASE NAME
                if (lueDatabase.Text.Length == 0 || lueDatabase.Text == "--Select--")
                {
                    MessageBox.Show("Database is not selected. Unable to back up database.");
                    lueDatabase.Focus();
                    return;
                }
                database = lueDatabase.Text;
                AIMGlobal.SplashScreenShow();

                //------------------------------------------------------------
                //SET BACKUP PATH (ON SERVER)
                backupPath = "C:\\SQLBackup\\";

                //------------------------------------------------------------
                //BACK UP DATABASE (ON SERVER)
                rtn = BackupDatabase(database, backupPath, DateTime.Now.ToString("yyyyMMdd"));

                //------------------------------------------------------------
                //BACK UP DATABASE LOG and SHRINK LOG           
                if (rtn != -99)
                {
                    str = "BACKUP LOG " + database + " TO DISK = '" + backupPath + database + "Log" + DateTime.Now.ToString("yyyyMMdd") + ".trn' ";
                    rtn = AIMManager.BackupDbase(str);

                    //Get System log name
                    SystemMasterAndDatabaseFilesInfo log = new SystemMasterAndDatabaseFilesInfo();
                    log = AIMManager.GetMasterAndDatabaseInfo(database, "Log");
                    if (log != null)
                    {
                        str = "DBCC SHRINKFILE (" + log.LogicalName + ", 1)";
                        rtn = AIMManager.BackupDbase(str);
                        if (rtn != 1)
                        {
                            AIMGlobal.SplashScreenHide();
                            MessageBox.Show("Log file SHRINK did not complete.");
                        }
                    }
                }
                                
                //------------------------------------------------------------
                //ZIP DATABASE BACKUP (TO LOCAL DRIVE)
                using (ZipFile zip = new ZipFile())
                {
                    fileName = database + DateTime.Now.ToString("yyyyMMdd");
                    count += 1;
                    backupPath = @"C:\SQLBackup\Zip";
                    zipFileToCreate = String.Format(@"{0}\{1}{2}.zip", backupPath, fileName + "_", count);
                    zip.AddDirectory("C:\\SQLBackup", "C:\\SQLBackup\\Zip");
                    zip.ParallelDeflateThreshold = -1;
                    zip.Save(zipFileToCreate);
                }

                //------------------------------------------------------------
                //UPLOAD ZIPPED FILE                                                                            
                //                          * * * * *   TO DO  * * * * *

                rtn = 0;
                while (rtn < 10000000)
                    {
                        rtn += 1;
                        Application.DoEvents();
                    }



                AIMGlobal.SplashScreenHide();
                MessageBox.Show("Upload Completed Successfully.","Message...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void restoreDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                String str = "";
                String path = String.Empty;
                String database = String.Empty;
                Int32 rtn = 0;
                String fileName = String.Empty;
                String backupPath = String.Empty;
                String zippedFile = String.Empty;
                String targetDirectory = String.Empty;
                ErrorInfo newID = new ErrorInfo();
                String strNewID = String.Empty;
                String backupName = String.Empty;


                //------------------------------------------------------------
                //GET DATABASE NAME
                if (lueDatabase.Text.Length == 0 || lueDatabase.Text == "--Select--")
                {
                    MessageBox.Show("Database is not selected. Unable to restore database.");
                    lueDatabase.Focus();
                    return;
                }
                database = lueDatabase.Text;
                AIMGlobal.SplashScreenShow();

                //------------------------------------------------------------
                //DOWNLOAD ZIPPED FILE                                                                          
                //                          * * * * *   TO DO   * * * * *


                zippedFile = "c:\\SQLBackup\\Zip\\CFCAIMDevelopment20200225.zip";
                backupName = "CFCAIMDevelopment20200225.bak";

                //------------------------------------------------------------            
                //SET RESTORED PATH (ON LOCAL PC)            
                targetDirectory = "c:\\SQLBackup\\UnZip";


                //------------------------------------------------------------
                //UNZIP DATABASE BACKUP - TO LOCAL DRIVE   
                using (ZipFile zip = ZipFile.Read(zippedFile))
                {
                    zip.ExtractAll(targetDirectory, ExtractExistingFileAction.OverwriteSilently);
                }

                //------------------------------------------------------------
                //MAKE BACKUP OF EXISTING DATABASE   
                backupPath = "C:\\SQLBackupTemp\\";
                newID = AIMManager.GetNewID();
                if (newID != null)
                {
                    if (newID.ErrorNumber == 0)
                    {
                        strNewID = newID.ErrorMessage;
                    }
                }
                rtn = BackupDatabase(database, backupPath, DateTime.Now.ToString("yyyyMMdd_HH.mm.ss.fff") + "_" + strNewID);

                //------------------------------------------------------------
                //RESTORE DBASE FROM UN-ZIPPED FILE
                path = targetDirectory;
                str = "RESTORE DATABASE " + database + " ";
                str += "FROM DISK = '" + targetDirectory + backupName;

                AIMGlobal.SplashScreenHide();
                MessageBox.Show("Restore Completed Successfully.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }

        }

        private Int32 BackupDatabase(String databaseName, String backupPath, String restOfBackupName)
        {
            try
            {

                String str = "";
                Int32 rtn = 0;

                str = "USE " + databaseName + " ";
                str += "BACKUP DATABASE [" + databaseName + "] ";
                str += "TO DISK = '" + backupPath + databaseName + restOfBackupName + ".bak' ";
                str += "WITH NOFORMAT, NOINIT, ";
                str += "NAME = N'" + lueDatabase.Text + "', SKIP, NOREWIND, NOUNLOAD, STATS = 10";
                rtn = AIMManager.BackupDbase(str);
                return rtn;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
                return -1;
            }
        }

        private void testToolStripMenuItem_Click_12(object sender, EventArgs e)
        {
            try
            {

                foreach (var item in menuStrip.Items)
                {
                    
                }



                //for demo to hide cust name
                //lueDataMap.Visible = false;
                //lueDatabase.Visible = false;
                //label1.Visible = false;
                //lblDatabase.Visible = false;
                AIMGlobal.open_form(Test2 = new Test2());             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        //1/22/21
        private void sampleTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            try
            {
                AIMGlobal.open_form(sampleType = new SampleType());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }            
        }

        //6/27/22
        private void createReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.SplashScreenShow();                

                //12/27/23
                //AIMGlobal.open_form(test5 = new Test5());
                AIMGlobal.open_form(report_Designer = new Report_Designer());

                AIMGlobal.SplashScreenHide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
               

        //11/30/22
        private void pendingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(sampleFurtherAnalysisPending = new xSampleFurtherAnalysisPending());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        //12/7/22
        private void nutrientTolerancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        AIMGlobal.open_form(aimMaterialTemplates = new AIMMaterialTemplates());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
        //    }
        }

        private void materialGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    AIMGlobal.open_form(aimMaterialGrouping = new AIMMaterialGrouping());
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            //}
        }

        private void nutrientTolerancesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(nutrientTolerance = new NutrientTolerance());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void productToleranceGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(aimMaterialGrouping = new ProductTolerance());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        //9/5/23
        private void reportCommentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(reportComments = new ReportComments());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void aPICallsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("Are you sure you want to run the API calls?", "Warning...", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No)
                {
                    return;
                }

                List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
                List<ImportConfigInfo> importConfig = new List<ImportConfigInfo>();
                FileTypeEnum fileType = FileTypeEnum.CSV;
                FilesCountInfo filesCount = new FilesCountInfo();
                DataSourceTypeEnum dataSource = DataSourceTypeEnum.Adisseo;
                String argument = String.Empty;
                String sql = String.Empty;
                Boolean isError = false;
                ErrorInfo error = new ErrorInfo();
                FilesInfo file = new FilesInfo();

                importConfig = AIMManager.GetImportConfig(DataSourceTypeEnum.Foss);

                for (int i = 0; i < importConfig.Count; i++)
                {
                    if (Directory.Exists(importConfig[i].ProcessDirectory))
                    {
                        fileType = (FileTypeEnum)importConfig[i].FileTypeID;
                        dataSource = (DataSourceTypeEnum)importConfig[i].DataSourceTypeID;
                        DirectoryInfo directory = new DirectoryInfo(importConfig[i].ProcessDirectory);
                        FileInfo[] files = directory.GetFiles("*." + fileType.ToString(), SearchOption.TopDirectoryOnly);
                        file.ProcessPath = importConfig[i].ProcessDirectory;
                        file.BackupPath = importConfig[i].BackUpDirectory;
                        foreach (FileInfo item in files)
                        {
                            file.FileName = item.Name;
                            filesCount.TotalFilesCount += 1;
                            switch (fileType)
                            {
                                case FileTypeEnum.API:
                                    DataExchangeManager.APIProcess(dataSource);
                                    break;
                                case FileTypeEnum.CSV:
                                    //DataExchangeManager.CSVProcess(file, ref filesCount, dataSource);
                                    DataExchangeManager.CSVProcess(file, ref filesCount, dataSource, importConfig[i].BackUpDirectory);
                                    break;
                                case FileTypeEnum.XML:
                                    MessageBox.Show("XML file processing has not yet been programmed. Contact IT to process XML files.", "Error...", MessageBoxButtons.OK);
                                    break;
                                case FileTypeEnum.TXT:
                                    MessageBox.Show("TXT file processing has not yet been programmed. Contact IT to process TXT files.", "Error...", MessageBoxButtons.OK);
                                    break;
                                default:
                                    MessageBox.Show("This file type is not supported, please contact IT to request this file type.", "Error...", MessageBoxButtons.OK);
                                    break;
                            }
                        }
                    }
                }

                isError = AdisseoAPI.AnalyseCode();
                if (isError == false)
                {
                    isError = AdisseoAPI.MaterialCode();
                    if (isError == false)
                    {
                        AdisseoAPI.SendPendingSampleSpectra(true);
                    }
                }

                AdisseoAPI.GettingRequest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
                
        //1/16/24
        private void pNECreditsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(pneCredit = new PNECredit());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void furtherAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(furtherAnalysis = new FurtherAnalysis());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void pendingSampleStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(pendingSample = new PendingSample());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        //1/19/24
        private void groundWholeCrossReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(groundXRef = new GroundUnGroundCrossReference());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void userLogToolStripMenuItem_Click(object sender, EventArgs e)         //Task1115 DB251105
        {
            UserLoginLog cfcUserLoginLog = new UserLoginLog();

            try
            {
                AIMGlobal.open_form(cfcUserLoginLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
    }



    //////private void ingredientToolStripMenuItem_Click(Object sender, EventArgs e)
    //////{
    //////    try
    //////    {
    //////        //----------------------------------------------------------------------------
    //////        //2/18/21 - moved this code into its own function (because "Ingredient" and "Product" both use it) 
    //////        //DialogResult dr = new DialogResult();
    //////        //String msg = "";
    //////        //Int32 rtn = 0;                
    //////        ////Check system setting to see if SampleAim table needs to be rebuilt
    //////        //SystemSettingsInfo systemSettings = new SystemSettingsInfo();
    //////        //systemSettings = AIMManager.GetSystemSettings();
    //////        //if (systemSettings != null)
    //////        //{
    //////        //    if (systemSettings.SampleAimNeedsRebuild == true)
    //////        //    {
    //////        //        AIMGlobal.SplashScreenHide();
    //////        //        msg = "Cross-reference changes have been made, but ";
    //////        //        msg += Environment.NewLine + "the changes will not be reflected in Analysis until";
    //////        //        msg += Environment.NewLine + "the Sample AIM table is rebuilt. ";

    //////        //        //5/3/18 - added this with default=yes -- so user is redirected to the Rebuild screen.
    //////        //        msg += Environment.NewLine + Environment.NewLine + "Do you want to rebuild the table now?";
    //////        //        msg += Environment.NewLine + Environment.NewLine + "(The rebuild may take a few minutes.)";
    //////        //        dr = MessageBox.Show(msg, "Warning ...", MessageBoxButtons.YesNo);
    //////        //        if (dr == DialogResult.Yes)
    //////        //        {                            
    //////        //            AIMGlobal.open_form(sampleAIMSync = new SampleAIMSync());                        
    //////        //            return;
    //////        //        }
    //////        //    }
    //////        //}
    //////        //----------------------------------------------------------------------------
    //////        Int32 rtn = 0;
    //////        rtn = CheckSampleAIM();
    //////        if (rtn == 1)
    //////        {
    //////            AIMGlobal.open_form(sampleAIMSync = new SampleAIMSync());
    //////            return;
    //////        }

    //////        //----------------------------------------------------------------------------
    //////        //2/18/21 - moved this code into its own function (because "Ingredient" and "Product" both use it) 
    //////        //Boolean chartsIsOpen = false;
    //////        //Int32 openForms = Application.OpenForms.Count;
    //////        //if (Application.OpenForms.Count > 1)
    //////        //{
    //////        //    for (int i = 0; i < Application.OpenForms.Count; i++)
    //////        //    {
    //////        //        if ((Application.OpenForms)[i].Name == "Charts")
    //////        //        {
    //////        //            chartsIsOpen = true;
    //////        //            AIMGlobal.SplashScreenHide();
    //////        //            dr = MessageBox.Show("Analysis needs the Chart screen, which is open. Do you want the Analysis screen to close the Chart?", "Warning ...", MessageBoxButtons.YesNo);
    //////        //            if (dr == DialogResult.Yes)
    //////        //            {
    //////        //                AIMGlobal.SplashScreenShow();
    //////        //                Application.OpenForms[i].Close();                   //close Charts screen 
    //////        //                AIMGlobal.open_form(analysis = new Analysis());     //open Analysis                               
    //////        //                AIMGlobal.SplashScreenHide();
    //////        //            }
    //////        //        }
    //////        //    }
    //////        //    if (chartsIsOpen == false)
    //////        //    {
    //////        //        AIMGlobal.SplashScreenShow();
    //////        //        AIMGlobal.open_form(analysis = new Analysis());
    //////        //        AIMGlobal.SplashScreenHide();
    //////        //    }
    //////        //}
    //////        //else
    //////        //{
    //////        //    AIMGlobal.SplashScreenShow();
    //////        //    AIMGlobal.open_form(analysis = new Analysis());
    //////        //    AIMGlobal.SplashScreenHide();
    //////        //}
    //////        Boolean chartsIsOpen = false;
    //////        chartsIsOpen = CheckOpenForms();
    //////        if (chartsIsOpen == false)
    //////        {
    //////            AIMGlobal.SplashScreenShow();
    //////            AIMGlobal.open_form(analysis = new Analysis());
    //////            AIMGlobal.SplashScreenHide();
    //////        }
    //////        //----------------------------------------------------------------------------

    //////    }
    //////    catch (Exception ex)
    //////    {
    //////        MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
    //////    }
    //////}



}
