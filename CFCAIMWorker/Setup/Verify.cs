using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFCAIMWorker.Setup
{
	public partial class Verify : Form
	{
		public Verify()
		{
			InitializeComponent();
		}

		#region Funcation
		private async Task CheckSampleToAPIAsync()
		{
			try
			{
				await Task.Run(() => Bruker.CheckSampleToAPI());
			}
			catch (Exception)
			{
				throw;
			}
		}

		private async Task CheckSampleFromAPIAsync()
		{
			try
			{
				await Task.Run(() => Bruker.CheckSampleFromAPI());
			}
			catch (Exception)
			{
				throw;
			}
		}

		private async Task CheckPNEFromAPIAsync()
		{
			try
			{
				await Task.Run(() => Bruker.CheckPNEFromAPI());
			}
			catch (Exception)
			{
				throw;
			}
		}
		private async Task FixMissingBruker()
		{
			try
			{
				await Task.Run(() => Bruker.FixMissingBruker());
			}
			catch (Exception)
			{
				throw;
			}
		}
		private async Task FixMissingResult()
		{
			try
			{
				await Task.Run(() => Bruker.FixMissingResult());
			}
			catch (Exception)
			{
				throw;
			}
		}
		private async Task FixMissingPNERequest()
		{
			try
			{
				await Task.Run(() => Bruker.FixMissingPNERequest());
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void OptionChange()
		{
			Bruker.ClearAllVariable();
			btnFixMissingResult.Visible = false;
		}
		#endregion

		private async void btnVerify_Click(object sender, EventArgs e)
		{
			String databaseXML = Application.StartupPath + @"\" + "CFCAIMWorkerDatabase.xml";

			WorkerGlobal.DatabaseXML = databaseXML;
			txtMessage.Text = String.Empty;
			timer1.Enabled = true;
			btnVerify.Visible = false;
			btnCancel.Visible = true;
			pnlCount.Visible = true;
			Bruker.CurrentCount = 0;
			Bruker.TotalCount = 0;
			Bruker.IsCancel = false;
			btnFixMissingResult.Visible = false;
			Bruker.ClearAllVariable();

			if (rbNIR.Checked == true)
			{
				Library.FullLogFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "NIR" + Assembly.GetExecutingAssembly().GetName().Name + ".Log";
				Library.RemoveLogFile();
				await CheckSampleToAPIAsync();
				txtMessage.Text = Library.ReadFile(Library.FullLogFile);
				MessageBox.Show("NIR API verify is done", "Message...");
			}
			else if (rbAIM.Checked == true)
			{
				Library.FullLogFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "AIMAPI" + Assembly.GetExecutingAssembly().GetName().Name + ".Log";
				Library.RemoveLogFile();
				await CheckSampleFromAPIAsync();
				txtMessage.Text = Library.ReadFile(Library.FullLogFile);
				MessageBox.Show("AIM API verify is done", "Message...");
			}
			else
			{
				Library.FullLogFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "PNE" + Assembly.GetExecutingAssembly().GetName().Name + ".Log";
				Library.RemoveLogFile();
				await CheckPNEFromAPIAsync();
				txtMessage.Text = Library.ReadFile(Library.FullLogFile);
				MessageBox.Show("PNE API verify is done", "Message...");
			}

			timer1.Enabled = false;
			btnCancel.Visible = false;
			pnlCount.Visible = false;
			btnVerify.Visible = true;

			if (rbNIR.Checked == true)
			{
				if (Bruker.FileName.Count > 0)
				{
					btnFixMissingResult.Visible = true;
					MessageBox.Show("There are missing Bruker results. Run Fix Missing Result to resend missing results.", "Message...");
				}
			}
			else if (rbAIM.Checked == true)
			{
				if (Bruker.BrukerSampleResultID.Count > 0)
				{
					btnFixMissingResult.Visible = true;
					MessageBox.Show("There are missing AIM API results. Run Fix Missing Result to resend missing results.", "Message...");
				}
			}
			else
			{
				if (Bruker.SampleID.Count > 0)
				{
					btnFixMissingResult.Visible = true;
					MessageBox.Show("There are missing PNE results. Run Fix Missing Result to rerun PNE.", "Message...");
				}
			}
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

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (Bruker.CurrentCount == 0)
			{
				txtCurrentCount.Text = String.Empty;
				txtCurrentCount.Refresh();
				txtLastCount.Text = String.Empty;
				txtLastCount.Refresh();
			}
			else
			{
				txtCurrentCount.Text = Bruker.CurrentCount.ToString();
				txtCurrentCount.Refresh();
				txtLastCount.Text = Bruker.TotalCount.ToString();
				txtLastCount.Refresh();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Bruker.IsCancel = true;
		}

		private async void btnFixMissingResult_Click(object sender, EventArgs e)
		{
			if (rbNIR.Checked == true)
			{
				if (Bruker.FileName.Count == 0)
				{
					MessageBox.Show("There are no missing result to fix", "Message...");
				}
				else
				{
					btnCancel.Visible = true;
					btnVerify.Visible = false;
					btnFixMissingResult.Visible = false;
					Library.FullLogFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "FIX" + Assembly.GetExecutingAssembly().GetName().Name + ".Log";
					Library.RemoveLogFile();
					await FixMissingBruker();
					txtMessage.Text = Library.ReadFile(Library.FullLogFile);
					btnCancel.Visible = false;
					btnVerify.Visible = true;
					MessageBox.Show("Fix is done", "Message...");
				}
			}
			else if (rbAIM.Checked == true)
			{
				if (Bruker.BrukerSampleResultID.Count == 0)
				{
					MessageBox.Show("There are no missing result to fix", "Message...");
				}
				else
				{
					btnCancel.Visible = true;
					btnVerify.Visible = false;
					btnFixMissingResult.Visible = false;
					Library.FullLogFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "FIX" + Assembly.GetExecutingAssembly().GetName().Name + ".Log";
					Library.RemoveLogFile();
					await FixMissingResult();
					txtMessage.Text = Library.ReadFile(Library.FullLogFile);
					btnCancel.Visible = false;
					btnVerify.Visible = true;
					MessageBox.Show("Fix is done", "Message...");
				}
			}
			else
			{
				if (Bruker.SampleID.Count == 0)
				{
					Bruker.SampleID.Clear();
					MessageBox.Show("There are no missing PNE request to fix", "Message...");
				}
				else
				{
					btnCancel.Visible = true;
					btnVerify.Visible = false;
					btnFixMissingResult.Visible = false;
					Library.FullLogFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd") + "FIX" + Assembly.GetExecutingAssembly().GetName().Name + ".Log";
					Library.RemoveLogFile();
					await FixMissingPNERequest();
					txtMessage.Text = Library.ReadFile(Library.FullLogFile);
					btnCancel.Visible = false;
					btnVerify.Visible = true;
					MessageBox.Show("Fix is done", "Message...");
				}
			}
		}

		private void rbNIR_CheckedChanged(object sender, EventArgs e)
		{
			OptionChange();
		}

		private void rbAIM_CheckedChanged(object sender, EventArgs e)
		{
			OptionChange();
		}

		private void rbPNE_CheckedChanged(object sender, EventArgs e)
		{
			OptionChange();
		}
	}
}
