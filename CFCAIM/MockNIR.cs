using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.ServiceProcess;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;


namespace CFCAIM.Maintenance
{
	public partial class MockNIR : Form
	{
		Int32 _fileNumber = 1;
		Int32 _sampleID = 0;
		Boolean _furtherAnalyses = false;
		String _material = String.Empty;
		String _serviceName = "CFCAIMWorker";


		public MockNIR()
		{
			Boolean isServiceExists = false;

			InitializeComponent();
			btnProcess.Visible = false;
			System.Drawing.Icon ico = Properties.Resources.CFCAIM;
			this.Icon = ico;
			txtDatabaseName.Text = "CFCAIMDemo";
			txtBackupPath.Text = @"C:\AIMDemo\AIMDemo.bak";
			txtMachineName.Text = Environment.MachineName + @"\SQL2012";

			foreach (ServiceController sc in ServiceController.GetServices())
			{
				if (sc.ServiceName == _serviceName)
				{
					//service is found
					isServiceExists = true;
					break;
				}
			}

			if (isServiceExists == true)
			{
				ServiceController service = new ServiceController(_serviceName);
				if (service.Status.Equals(ServiceControllerStatus.Stopped) || service.Status.Equals(ServiceControllerStatus.StopPending))
				{
					btnStop.Visible = false;
				}
				else
				{
					btnStart.Visible = false;
				}
			}
			else
			{
				btnStart.Visible = false;
				btnStop.Visible = false;
				MessageBox.Show("CFCAIMWorker service is missing contract CFC", "Error...", MessageBoxButtons.OK);
			}
		}

		private void btnProcess_Click(object sender, EventArgs e)
		{
			String csvPath = txtFossResult.Text + @"\" + _material + @"\" + _fileNumber.ToString() + ".CSV";
			String csvToPath = txtFossResult.Text + @"\" + _material + " " + txtSampleNumber.Text + ".CSV";
			String nirPath = txtFossSpetrum.Text + @"\" + _material + @"\" + _fileNumber.ToString() + ".NIR";
			String nirToPath = txtFossSpetrum.Text + @"\" + _material + " " + txtSampleNumber.Text + ".NIR";
			String tempCSV = txtFossResult.Text + @"\" + _material + @"\Temp.CSV";
			Int32 sampleID = 0;
			Int32 line = 0;
			Int32 pointer = 0;

			Int32.TryParse(txtSampleNumber.Text, out sampleID);
			if (sampleID == 0)
			{
				MessageBox.Show("Please enter a sample.", "Message...", MessageBoxButtons.OK);
			}
			else
			{
				_fileNumber += 1;
				Int32.TryParse(txtSampleNumber.Text, out sampleID);

				if (_fileNumber > 10)
				{
					_fileNumber = 1;
				}

				if (File.Exists(csvPath))
				{
					if (!Directory.Exists(txtFossResult.Text))
					{
						Directory.CreateDirectory(txtFossResult.Text);
					}

					if (!File.Exists(csvToPath))
					{
						using (ReadWriteCSV.CsvFileReader reader = new ReadWriteCSV.CsvFileReader(csvPath))
						{
							using (ReadWriteCSV.CsvFileWriter writer = new ReadWriteCSV.CsvFileWriter(tempCSV))
							{
								ReadWriteCSV.CsvRow row = new ReadWriteCSV.CsvRow();
								while (reader.ReadRow(row))
								{
									if (line == 0)
									{
										if (row[4] == "Sample Number")
										{
											pointer = 4;
										}
										else
										{
											pointer = 5;
										}
									}
									else if (line == 1)
									{
										row[pointer] = sampleID.ToString();
									}
									writer.WriteRow(row);
									line += 1;
								}
								writer.Flush();
							}
						}

						File.Move(tempCSV, csvToPath);
					}
				}

				if (File.Exists(nirPath) == true && _furtherAnalyses == true)
				{
					if (!Directory.Exists(txtFossSpetrum.Text))
					{
						Directory.CreateDirectory(txtFossSpetrum.Text);
					}

					if (!File.Exists(nirToPath))
					{
						File.Copy(nirPath, nirToPath);
					}
				}

				Int32.TryParse(txtSampleNumber.Text, out _sampleID);
				prgProgress.Visible = true;
				prgProgress.Minimum = 0;
				bgwTimer.RunWorkerAsync();
			}
		}

		private void txtSampleNumber_Validated(object sender, EventArgs e)
		{
			SampleInfo sample = new SampleInfo();
			Int32 sampleNumber = 0;

			if (txtSampleNumber.Text.Length > 0)
			{
				Int32.TryParse(txtSampleNumber.Text, out sampleNumber);
				btnProcess.Visible = false;

				if (sampleNumber == 0)
				{
					txtSampleNumber.Text = String.Empty;
					_material = String.Empty;
					txtMaterial.Text = "Invalid Sample Number";
				}
				else
				{
					sample = AIMManager.GetSample(sampleNumber);
					if (sample == null)
					{
						txtMaterial.Text = "Invalid Sample Number";
					}
					else
					{
						txtMaterial.Text = sample.AIMMaterialCode;
						_material = sample.AIMMaterialCode;
						_furtherAnalyses = false;
						if (sample.FurtherAnalysisID > 0)
						{
							_furtherAnalyses = true;
						}
						btnProcess.Visible = true;
					}
				}
			}
		}

		private void bgwTimer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			for (int i = 1; i < 5; i++)
			{
				bgwTimer.ReportProgress(i);
				Thread.Sleep(1000);
			}
			bgwTimer.Dispose();
		}

		private void bgwTimer_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			prgProgress.Value = e.ProgressPercentage;
		}

		private void bgwTimer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			txtSampleNumber.Text = String.Empty;
			txtMaterial.Text = String.Empty;
			_furtherAnalyses = true;
			prgProgress.Visible = false;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			try
			{
				ServiceController service = new ServiceController(_serviceName);
				if (service.Status.Equals(ServiceControllerStatus.Stopped) || service.Status.Equals(ServiceControllerStatus.StopPending))
				{
					service.Start();
					btnStop.Visible = true;
					btnStart.Visible = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
				throw;
			}
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			try
			{
				ServiceController service = new ServiceController(_serviceName);
				if (service.Status.Equals(ServiceControllerStatus.Running) || service.Status.Equals(ServiceControllerStatus.StartPending))
				{
					service.Stop();
					btnStop.Visible = false;
					btnStart.Visible = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
				throw;
			}
		}

		private void btnRestoreDatabase_Click(object sender, EventArgs e)
		{
			RestoreDatabase(txtDatabaseName.Text, txtBackupPath.Text, txtMachineName.Text, "sa", "SQL2012");
		}

		public void RestoreDatabase(String databaseName, String backUpFile, String serverName, String userName, String password)
		{

			try
			{
				DialogResult dialogResult = MessageBox.Show("Do you want to restore the data to demo default?  This will take a couple of mins.", "Message...", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					ServerConnection connection = new ServerConnection(serverName, userName, password);
					Server sqlServer = new Server(connection);
					if (File.Exists(backUpFile))
					{
						sqlServer.KillDatabase(databaseName);
						Restore rstDatabase = new Restore();
						rstDatabase.Action = RestoreActionType.Database;
						rstDatabase.Database = databaseName;
						BackupDeviceItem bkpDevice = new BackupDeviceItem(backUpFile, DeviceType.File);
						rstDatabase.Devices.Add(bkpDevice);
						rstDatabase.ReplaceDatabase = true;
						rstDatabase.SqlRestore(sqlServer);
						AIMManager.UpdateSampleDate();

						MessageBox.Show("The database has been restore", "Error...", MessageBoxButtons.OK);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
			}
		}
	}
}
