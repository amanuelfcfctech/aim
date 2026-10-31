using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.Threading;
using CFCAIM.DataExchange;
using Core.Library.Data;

namespace CFCAIM.Foss
{
	public partial class FossSample : Form
	{
		Int32 _fileNumber = 1;
		Int32 _sampleID = 0;
		Boolean _isRunning = false;
		String _material = String.Empty;

		public FossSample()
		{
			InitializeComponent();
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

				if (File.Exists(nirPath))
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

				_isRunning = true;

				if (bgwProcessData.IsBusy == false)
				{
					bgwProcessData.RunWorkerAsync();
				}

				Int32.TryParse(txtSampleNumber.Text, out _sampleID);
				prgProgress.Visible = true;
				prgProgress.Minimum = 0;
				bgwTimer.RunWorkerAsync();
			}
		}

		private void FossSample_Load(object sender, EventArgs e)
		{
			txtFossSpetrum.Text = @"C:\Foss";   // ConfigurationManager.AppSettings["FossNIR"];
			txtFossResult.Text = @"C:\Foss";     //ConfigurationManager.AppSettings["FossCSV"];
		}

		private void txtSampleNumber_Validated(object sender, EventArgs e)
		{
			SampleInfo sample = new SampleInfo();
			Int32 sampleNumber = 0;

			if (txtSampleNumber.Text.Length > 0)
			{
				Int32.TryParse(txtSampleNumber.Text, out sampleNumber);

				if (sampleNumber == 0)
				{
					txtSampleNumber.Text = String.Empty;
					_material = String.Empty;
					txtMaterial.Text = "Invalid Sample Number";
				}
				else
				{
					sample = AIMManager.GetSample(sampleNumber);
					txtMaterial.Text = sample.AnalyzerMaterialName;
					_material = sample.AIMMaterialCode;
				}
			}
		}

		private void txtSampleNumber_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (GetNextControl(ActiveControl, true) != null)
				{					
					e.Handled = true;
					GetNextControl(ActiveControl, true).Focus();
				}
			}
		}

		private void bgwProcessData_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			try
			{
				while (_isRunning)
				{
					AnalyzerPendingSampleInfo analyzerPending = new AnalyzerPendingSampleInfo();
					List<AnalyzerPendingSampleInfo> analyzerPendingSample = new List<AnalyzerPendingSampleInfo>();
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
					Int32 sampleID = 0;

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
										error = DataExchangeManager.CSVProcess(file, ref filesCount, dataSource, importConfig[i].BackUpDirectory);
										break;
									case FileTypeEnum.XML:
										MessageBox.Show("XML file processing is not yet programmed. Contact IT to process XML files.", "Error...", MessageBoxButtons.OK);
										break;
									case FileTypeEnum.TXT:
										MessageBox.Show("TXT file processing is not yet programmed. Contact IT to process TXT files.", "Error...", MessageBoxButtons.OK);
										break;
									default:
										MessageBox.Show("This file type is not supported, please contact IT to request this file type.", "Error...", MessageBoxButtons.OK);
										break;
								}

								if (error.RecordID > 0)
								{
									sampleID = error.RecordID;
									analyzerPending = AIMManager.GetAnalyzerPendingSample(sampleID);
									if (analyzerPending != null)
									{
										analyzerPending.NIRAnalysis = DateTime.Now;
										AIMManager.SaveAnalyzerPendingSample(ref analyzerPending);
									}
								}
							}
						}
					}

					analyzerPendingSample = AIMManager.GetAnalyzerPendingSample(AnalyzerStatusEnum.NotSentOrNotReceived);
					if (analyzerPendingSample.Count > 0)
					{
						foreach (AnalyzerPendingSampleInfo pendingSample in analyzerPendingSample)
						{
							if (pendingSample.SentStamp == DateTime.MinValue)
							{
								isError = AdisseoAPI.AnalyseCode();
								if (isError == false)
								{
									isError = AdisseoAPI.MaterialCode();
									if (isError == false)
									{
										AdisseoAPI.SendPendingSampleSpectra(true);
									}
								}
							}

							if (pendingSample.ReceivedStamp == DateTime.MinValue)
							{
								AdisseoAPI.GettingRequest();
							}
						}
					}
					Thread.Sleep(3000);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
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
			prgProgress.Visible = false;
			//MessageBox.Show("Done", "Message...",MessageBoxButtons.OK);
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			_isRunning = true;
			if (bgwProcessData.IsBusy == false)
			{
				bgwProcessData.RunWorkerAsync();
			}
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			_isRunning = false;
			if (bgwProcessData.IsBusy == true)
			{
				bgwProcessData.CancelAsync();
			}
		}

		private void btnProcessNIR_Click(object sender, EventArgs e)
		{
			List<AnalyzerPendingSampleInfo> analyzerPendingSample = new List<AnalyzerPendingSampleInfo>();
			AnalyzerPendingSampleInfo newPendingSample = new AnalyzerPendingSampleInfo();
			SampleInfo sample = new SampleInfo();
			String name = String.Empty;
			///String soybean = "CARGILL";			
			Boolean isError = false;

			DirectoryInfo directory = new DirectoryInfo(@"E:\AIM\Simmons\NIR Files 02162015 Fairland");
			FileInfo[] files = directory.GetFiles("*.nir", SearchOption.TopDirectoryOnly);

			foreach (FileInfo item in files)
			{
				name = item.Name.Substring(item.Name.IndexOf(' ') + 1);
				name = name.Substring(name.IndexOf(' ') + 1);
				name = name.Substring(0, name.IndexOf(' '));
				sample = AIMManager.GetSample(name);
				if (sample != null)
				{
					newPendingSample.SampleID = sample.SampleID;
					newPendingSample.NIRAnalysis = DateTime.Now;
					AIMManager.SaveAnalyzerPendingSample(ref newPendingSample);
					analyzerPendingSample = AIMManager.GetAnalyzerPendingSample(AnalyzerStatusEnum.NotSentOrNotReceived);
					if (analyzerPendingSample.Count > 0)
					{
						foreach (AnalyzerPendingSampleInfo pendingSample in analyzerPendingSample)
						{
							if (pendingSample.SentStamp == DateTime.MinValue)
							{
								isError = AdisseoAPI.AnalyseCode();
								if (isError == false)
								{
									isError = AdisseoAPI.MaterialCode();
									if (isError == false)
									{
										AdisseoAPI.SendPendingSampleSpectra(false);
									}
								}
							}

							if (pendingSample.ReceivedStamp == DateTime.MinValue)
							{
								AdisseoAPI.GettingRequest();
							}
						}
					}
					
				}
			}
		}
	}
}
