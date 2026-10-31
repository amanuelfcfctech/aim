using CFCAIMCore.Entity;
using CFCAIMWorker.Setup;
using System;
using System.Threading;
using System.Windows.Forms;


namespace CFCAIMWorker
{
	public partial class Setting : Form
	{
		private String _SettingXML = Application.StartupPath + @"\" + "CFCAIMWorkerSettingV2.xml"; //09/24/2025 Farag "CFCAIMWorkerSetting.xml";

        private String BrowserFolder()
		{
			String startPath = @"C:\Foss\DirectExport";

			return BrowserFolder(startPath);
		}

		private String BrowserFolder(String startPath)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			String path = String.Empty;

			Thread t = new Thread((ThreadStart)(() =>
			{
				if (startPath.Length > 0)
				{
					folderBrowserDialog.SelectedPath = startPath;
				}
				else
				{
					folderBrowserDialog.SelectedPath = @"C:\Foss\DirectExport";
				}

				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
				{
					path = folderBrowserDialog.SelectedPath;
				}
			}
			));

			// Run your code from a thread that joins the STA Thread
			t.SetApartmentState(ApartmentState.STA);
			t.Start();
			t.Join();

			return path;
		}
		public Setting()
		{
			InitializeComponent();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			try
			{
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnFossCSVPath_Click(object sender, EventArgs e)
		{
			String path = String.Empty;

			txtFossCSVPath.Text = String.Empty;
			path = BrowserFolder(txtFossCSVPath.Text);

			if (path.Length > 0)
			{
				txtFossCSVPath.Text = path;
			}
		}

		private void btnFossNIRPath_Click(object sender, EventArgs e)
		{
			String path = String.Empty;

			txtFossNIRPath.Text = String.Empty;
			path = BrowserFolder(txtFossNIRPath.Text);

			if (path.Length > 0)
			{
				txtFossNIRPath.Text = path;
			}
		}

		private void btnFossArchivePath_Click(object sender, EventArgs e)
		{
			String path = String.Empty;

			txtFossArchivePath.Text = String.Empty;
			path = BrowserFolder(txtFossArchivePath.Text);

			if (path.Length > 0)
			{
				txtFossArchivePath.Text = path;
			}
		}

		private void btnFossErrorPath_Click(object sender, EventArgs e)
		{
			String path = String.Empty;

			txtFossErrorPath.Text = String.Empty;
			path = BrowserFolder(txtFossErrorPath.Text);

			if (path.Length > 0)
			{
				txtFossErrorPath.Text = path;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			SettingInfo setting = new SettingInfo();
			Int32 id = 0;

			if (txtID.Text.Length == 0)
			{
				txtID.Text = "0";
			}

			txtID.Text = id.ToString();
			setting.SettingID = id;
			setting.IsDebug = chkDebugOn.Checked;
			setting.IsTestUrl = chkTestUrlOn.Checked;
			setting.APIUrl = txtAPIUrl.Text;
			setting.APIUrlTest = txtAPIUrlTest.Text;
			setting.AIMAPIKey = txtAIMAPIKey.Text;
			setting.AIMInstrumentKey = txtAIMInstrumentKey.Text;
			setting.FossCSVPath = txtFossCSVPath.Text;
			setting.FossNIRPath = txtFossNIRPath.Text;
			setting.FossArchivePath = txtFossArchivePath.Text;
			setting.FossErrorPath = txtFossErrorPath.Text;
			setting.IsPNETestUrl = chkTestPNEOn.Checked;
			setting.PNEAPIUrl = txtPNEAPIUrl.Text;
			setting.PNEAPITestUrl = txtPNEAPITestUrl.Text;
			setting.IsMaintenanceTestUrl = chkMaintenanceTestUrlOn.Checked;         //DB240927
            setting.APIMaintenanceUrl = txtApiUrlMaintenance.Text;					//DB240927
			setting.APIMaintenanceUrlTest = txtApiUrlMaintenanceTest.Text;          //DB240927
            Library.SaveXmlSettingData(ref setting, _SettingXML);
			MessageBox.Show("Save was successful.", "Message...", MessageBoxButtons.OK);

		}


		private void Setting_Load(object sender, EventArgs e)
		{
			SettingInfo setting = new SettingInfo();

			Library.LoadSetting(ref setting, _SettingXML);

			if (setting.SettingID != -1)
			{
				txtID.Text = setting.SettingID.ToString();
			}

			if (setting.IsDebug.ToString().ToUpper() == "TRUE")
			{
				chkDebugOn.Checked = true;
			}

			if (setting.IsTestUrl.ToString().ToUpper() == "TRUE")
			{
				chkTestUrlOn.Checked = true;
			}

			txtAPIUrl.Text = setting.APIUrl;
			txtAPIUrlTest.Text = setting.APIUrlTest;
			txtAIMAPIKey.Text = setting.AIMAPIKey;
			txtAIMInstrumentKey.Text = setting.AIMInstrumentKey;
			txtFossCSVPath.Text = setting.FossCSVPath;
			txtFossNIRPath.Text = setting.FossNIRPath;
			txtFossArchivePath.Text = setting.FossArchivePath;
			txtFossErrorPath.Text = setting.FossErrorPath;

			if (setting.IsPNETestUrl.ToString().ToUpper() == "TRUE")
			{
				chkTestPNEOn.Checked = true;
			}

			txtPNEAPIUrl.Text = setting.PNEAPIUrl;
			txtPNEAPITestUrl.Text = setting.PNEAPITestUrl;

            if (setting.IsMaintenanceTestUrl.ToString().ToUpper() == "TRUE")			//DB240927 Start
            {
                chkMaintenanceTestUrlOn.Checked = true;
            }

            txtApiUrlMaintenance.Text = setting.APIMaintenanceUrl;
            txtApiUrlMaintenanceTest.Text = setting.APIMaintenanceUrlTest;				//DB240927 End

            chkDebugOn.Focus();

		}
	}
}
