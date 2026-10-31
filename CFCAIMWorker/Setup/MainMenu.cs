using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Bruker;
using CFCAIMCore.Entity.CFCAPI;
using CFCAIMCore.Manager;
using CFCAIMWorker.Setup;
using Core.Library.SQLDatabaseConnection;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAPI;
using CFCAPICore.Entity;
using CFCAIMWorker.Adisseo;

namespace CFCAIMWorker
{
	public partial class cmdTestPNEAPI : Form
	{
		public cmdTestPNEAPI()
		{
			InitializeComponent();
		}

		private void btnDatabaseConnection_Click(object sender, EventArgs e)
		{
			DatabaseConnection databaseConnection = new DatabaseConnection();

			this.Visible = false;
			databaseConnection.ShowDialog();
			this.Visible = true;

		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnSetting_Click(object sender, EventArgs e)
		{
			Setting setting = new Setting();

			this.Visible = false;
			setting.ShowDialog();
			this.Visible = true;
		}

		private void btnExit_Click_1(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnTestPNEAPI_Click(object sender, EventArgs e)
		{
			List<BrukerSampleInfo> brukerSampleList = new List<BrukerSampleInfo>();
			List<AnalysesInfo> apiAnalyse = new List<AnalysesInfo>();
			List<CustomerInfo> customer = new List<CustomerInfo>();
			List<APITokenInfo> apiToken = new List<APITokenInfo>();

			SettingInfo setting = new SettingInfo();
			String message = String.Empty;
			String aimApiKey = String.Empty;
			String token = String.Empty;
			Int32 customerID = 0;

			setting = CFCAPIManager.Setting;
			aimApiKey = setting.AIMAPIKey;
			BrukerManager.IsMessageDisplay = false;

			Library.WriteErrorLog(setting.APIUrl);
			Library.WriteErrorLog(aimApiKey);
			Library.WriteErrorLog(setting.AIMInstrumentKey);
			apiToken = CFCAPIManager.GetToken(aimApiKey);

			if (apiToken.Count == 0)
			{
				Library.WriteErrorLog(setting.AIMInstrumentKey);
				txtName.Text = "Error Token";
			}
			else
			{
				token = apiToken[0].Token;
				customerID = apiToken[0].CustomerID;
				Library.WriteErrorLog(token);
				Library.WriteErrorLog(customerID.ToString());
				brukerSampleList = CFCAPIManager.GetBrukerSampleNotSentResult(token, customerID);
				customer = CFCAPIManager.GetCustomer(token, customerID);

                //Task1160 DB260318 Start
                //Not used anymore since routing all PNE worker calls through CFC API

                //APIManager.Token = customer[0].PNEKey;

                //if (setting.IsPNETestUrl == true)
                //{
                //	APIManager.PNEAPIUrl = setting.PNEAPITestUrl;
                //}
                //else
                //{
                //	APIManager.PNEAPIUrl = setting.PNEAPIUrl;
                //}

                //Task1160 DB260318 End

                //Task1160 DB260318 apiAnalyse = APIManager.GetAnalyse();

                PNEAPIClient.Token = customer[0].PNEKey;
                Library.WriteErrorLog(PNEAPIClient.Token);
                apiAnalyse = PNEAPIClient.GetAnalyse(token)?.Data?.Data;		//Task1160 DB260318 

                if (apiAnalyse != null)
				{
					txtCount.Text = apiAnalyse.Count.ToString();
					txtName.Text = apiAnalyse[0].Code;
				}
				else
				{
					txtName.Text = "Error";
				}
			}
		}

		private void mainMenu_Load(object sender, EventArgs e)
		{
			lblVersion.Text = "Version: " + WorkerGlobal.ProgramVersion;

		}

		private void btnVerify_Click(object sender, EventArgs e)
		{
			Verify verify = new Verify();

			this.Visible = false;
			verify.ShowDialog();
			this.Visible = true;
		}

		//4/4/23
		private void btnConcept5Settings_Click(object sender, EventArgs e)
		{
			SettingsConcept5 settings = new SettingsConcept5();
			this.Visible = false;
			settings.ShowDialog();
			this.Visible = true;
		}

		private void cmdSendSamplePNEAPI_Click(object sender, EventArgs e)
		{
			Int32 sampleID = 0;

			Int32.TryParse(txtSampleID.Text, out sampleID);

			Bruker.TestAPI(sampleID);

		}


    }
}
