using CFCAIMWorker;
using CFCAPICore.DataExchange;
using System;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using CFCAIMCore;
using CFCAIMCore.Manager;
using CFCAIMCore.Entity.CFCAPI;
using System.Collections.Generic;
using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Bruker;
using CFCAIMWorker.Setup;

namespace CFCAIMWorkerTest
{
    public partial class UnitTest : Form
    {
        public UnitTest()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            SettingInfo setting = new SettingInfo();
           // CFCAPIManager apiManager = new CFCAPIManager();
            List<APITokenInfo> apiToken = new List<APITokenInfo>();
            String aimApiKey = String.Empty;
            String token = String.Empty;
            List<QAUnitTestInfo> qAUnitTestInfo = new List<QAUnitTestInfo>();


            String databaseXML = Application.StartupPath + @"\" + "CFCAIMWorkerDatabase.xml";
            String databaseXML2 = Application.StartupPath + @"\" + "CFCAIMWorkerDatabase2.xml";
            String settingXML = Application.StartupPath + @"\" + "CFCAIMWorkerSetting.xml";



            //4/5/23
            String concept5SettingsXML = Application.StartupPath + @"\" + "CFCAIMWorkerConcept5Settings";


            Library.FullLogFile = "E:\\AIM\\AIMWorkerUnitTestFiles\\AIMWorkerUnitTestLog";
                
            //----------------------------------------------------------
            //if (File.Exists(databaseXML) == true)
            //{
            //    if (File.Exists(databaseXML + ".xml") == false)
            //    {
            //        File.Move(databaseXML, databaseXML + ".xml");
            //    }
            //}
            //if (File.Exists(databaseXML2) == true)
            //{
            //    if (File.Exists(databaseXML2 + ".xml") == false)
            //    {
            //        File.Move(databaseXML2, databaseXML2 + ".xml");
            //    }
            //}


            Bruker.Path = new List<string>();
            Bruker.FileName = new List<string>();
            Bruker.IsUnitTest = true;
            lblStatus.Text = "Sending data from API";
            Bruker.DatabaseXML = databaseXML2;
            Bruker.SendSampleToAPI();

            Bruker.CheckSampleToAPI();
            //Bruker.CheckSampleFromAPI();
            //Bruker.GetSampleFromApiPne();


			Bruker.DatabaseXML = databaseXML;
            lblStatus.Text = "Sending tango data to API";
            Bruker.SendTangoSampleToAPI();

            lblStatus.Text = "Pulling data from API";
            Bruker.GetSampleFromAPI();


            Bruker.CheckSampleToAPI();
            Bruker.CheckSampleFromAPI();

            Library.LoadSetting(ref setting, Utility.SettingXML);

            aimApiKey = setting.AIMAPIKey;
            CFCAPIManager.Setting = setting;
            lblStatus.Text = "Getting Token";
            apiToken = CFCAPIManager.GetToken(aimApiKey);

            if (apiToken == null)
            {
                Library.WriteErrorLogFile("Batch api is not working.");
            }
            else
            {
                token = apiToken[0].Token;
                lblStatus.Text = "Pulling test results";
                qAUnitTestInfo = CFCAPIManager.GetQAResult(token);

                if (qAUnitTestInfo[0].IsBrukerSampleError == true)
                {
                    lblBrukerSampleError.Text = "Fail";
                    lblBrukerSampleError.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblBrukerSampleError.Text = "OK";
                    lblBrukerSampleError.ForeColor = System.Drawing.Color.Green;
                }
                if (qAUnitTestInfo[0].IsBrukerSampleResultError == true)
                {
                    lblBrukerSampleResultError.Text = "Fail";
                    lblBrukerSampleResultError.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblBrukerSampleResultError.Text = "OK";
                    lblBrukerSampleResultError.ForeColor = System.Drawing.Color.Green;
                }
                if (qAUnitTestInfo[0].IsBrukerSampleResultReceiveError == true)
                {
                    lblBrukerSampleResultReceiveError.Text = "Fail";
                    lblBrukerSampleResultReceiveError.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblBrukerSampleResultReceiveError.Text = "OK";
                    lblBrukerSampleResultReceiveError.ForeColor = System.Drawing.Color.Green;
                }
                if (qAUnitTestInfo[0].IsBrukerSampleResultSentError == true)
                {
                    lblBrukerSampleResultSentError.Text = "Fail";
                    lblBrukerSampleResultSentError.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblBrukerSampleResultSentError.Text = "OK";
                    lblBrukerSampleResultSentError.ForeColor = System.Drawing.Color.Green;
                }
                if (qAUnitTestInfo[0].IsBrukerSentLogError == true)
                { 
                    lblBrukerSentLogError.Text = "Fail";
                    lblBrukerSentLogError.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblBrukerSentLogError.Text = "OK";
                    lblBrukerSentLogError.ForeColor = System.Drawing.Color.Green;
                }
                if (qAUnitTestInfo[0].IsBrukerSpectrumError == true)
                {
                    lblBrukerSpectrumError.Text = "Fail";
                    lblBrukerSpectrumError.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblBrukerSpectrumError.Text = "OK";
                    lblBrukerSpectrumError.ForeColor = System.Drawing.Color.Green;
                }
                if (qAUnitTestInfo[0].IsBrukerSpectrumReceiveError == true)
                {
                    lblBrukerSpectrumReceiveError.Text = "Fail";
                    lblBrukerSpectrumReceiveError.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblBrukerSpectrumReceiveError.Text = "OK";
                    lblBrukerSpectrumReceiveError.ForeColor = System.Drawing.Color.Green;
                }
                if (qAUnitTestInfo[0].IsBrukerSpectrumSentError == true)
                {
                    lblBrukerSpectrumSentError.Text = "Fail";
                    lblBrukerSpectrumSentError.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblBrukerSpectrumSentError.Text = "OK";
                    lblBrukerSpectrumSentError.ForeColor = System.Drawing.Color.Green;
                }
            }

            lblStatus.Text = "Done";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<BrukerSampleInfo> brukerSampleList = new List<BrukerSampleInfo>();
            brukerSampleList = BrukerManager.GetBrukerSpectrum();

            foreach (var sample in brukerSampleList)
            {
                

            }


        }

        private void btnRunCFCAPIPne_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "Running CFCAPIPne tests...";

                String databaseXML = Application.StartupPath + @"\" + "CFCAIMWorkerDatabase.xml";
                String settingXML = Application.StartupPath + @"\" + "CFCAIMWorkerSetting.xml";

                CFCAPIPneTest.DatabaseXML = databaseXML;
                CFCAPIPneTest.SettingXML = settingXML;

                // Set up database connection (required for AIMManager calls)
                CFCAIMWorker.WorkerGlobal.DatabaseXML = databaseXML;
                bool dbOk = CFCAIMWorker.WorkerGlobal.SetUpDatabaseConnection("AIM");

                if (!dbOk)
                {
                    MessageBox.Show("Database connection failed. Some tests may fail.", "CFCAPIPne Test", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Load settings and configure PNE API (required for API calls)
                SettingInfo setting = new SettingInfo();
                Library.LoadSetting(ref setting, Utility.SettingXML);
                CFCAPIManager.Setting = setting;
                CFCAPIPne.AimInstrumentKey = setting.AIMInstrumentKey ?? string.Empty;

                // Get PNE token from API
                var apiToken = CFCAPIManager.GetToken(setting.AIMAPIKey);
                if (apiToken != null && apiToken.Count > 0)
                {
                    var instrument = CFCAPIManager.GetInstrument(apiToken[0].Token, apiToken[0].CustomerID);
                    if (instrument != null && instrument.Count > 0)
                    {
                        CFCAIMCore.Manager.APIManager.PNEAPIUrl = setting.IsPNETestUrl == true ? setting.PNEAPITestUrl : setting.PNEAPIUrl;
                        CFCAIMCore.Manager.APIManager.Token = instrument[0].PNEKey;
                        CFCAIMCore.Manager.APIManager.Token = instrument[0].PNEKey;
                    }
                }

                var report = CFCAPIPneTest.RunAllTestsAndGetReport();
                lblStatus.Text = "CFCAPIPne tests complete";

                MessageBox.Show(report, "CFCAPIPne Test Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "CFCAPIPne test error";
                MessageBox.Show("Error running CFCAPIPne tests: " + ex.Message, "CFCAPIPne Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRunFoss_Click(object sender, EventArgs e)
        {
            SettingInfo setting = new SettingInfo();
            // CFCAPIManager apiManager = new CFCAPIManager();
            List<APITokenInfo> apiToken = new List<APITokenInfo>();
            String aimApiKey = String.Empty;
            String token = String.Empty;
            List<QAUnitTestInfo> qAUnitTestInfo = new List<QAUnitTestInfo>();


            String databaseXML = Application.StartupPath + @"\" + "CFCAIMWorkerDatabase.xml";
            String settingXML = Application.StartupPath + @"\" + "CFCAIMWorkerSetting.xml";



            //4/5/23
            String concept5SettingsXML = Application.StartupPath + @"\" + "CFCAIMWorkerConcept5Settings";


            Library.FullLogFile = "E:\\AIM\\AIMWorkerUnitTestFiles\\AIMWorkerUnitTestLog";

            lblStatus.Text = "Sending data from API";
            FossImport.DatabaseXML = databaseXML;
            FossImport.SendSampleToAPI();
                       
            FossImport.GetSampleFromAPI();

            Library.LoadSetting(ref setting, Utility.SettingXML);

            aimApiKey = setting.AIMAPIKey;
            CFCAPIManager.Setting = setting;
            lblStatus.Text = "Getting Token";
            apiToken = CFCAPIManager.GetToken(aimApiKey);

        }
    }
}
