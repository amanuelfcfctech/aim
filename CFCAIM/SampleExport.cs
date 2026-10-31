using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using System.IO;
using System.Globalization;
using System.Linq;

namespace CFCAIM
{
	public partial class SampleExport : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		List<SampleExportDetailInfo> _sampleDetail = new List<SampleExportDetailInfo>();
		List<SampleExportTemplateInfo> _exportTemplate = new List<SampleExportTemplateInfo>();

		SampleExportMessage message;
		Int32 _sampleExportID = 0;

		public SampleExport()
		{
			InitializeComponent();
		}

		private void SampleExport_Load(object sender, EventArgs e)
		{
			DataBindTemplate();
			DatabindFileType();
			DatabindDelimiter();
			SetToolTips();
			ClearTextFields();
			_sampleDetail.Clear();			
			grdExport.DataSource = "";
						
			if (_exportTemplate == null)
			{
				tabControl1.SelectedIndex = 0;
			}
			else if (_exportTemplate.Count == 1)
			{
				tabControl1.SelectedIndex = 0;
			}
			else
			{
				tabControl1.SelectedIndex = 1;

				//selecting value if there is only one
				if (_exportTemplate.Count == 2)
				{
					lueTemplateName.EditValue = _exportTemplate[1].SampleExportTemplateID;
				}				
				lueTemplateName.Focus();									
			}

			ShowHideGrid();

			//10/6/21 - added for resize issue
			WindowState = FormWindowState.Maximized;
		}

		private void ShowHideGrid()
		{
			btnExport.BackColor = SystemColors.Control;
			btnSave.BackColor = SystemColors.Control;
			timer1.Stop();
			timer2.Stop();
			btnExport.Visible = true;
			pnlCriteria.Visible = true;
			grdExport.Visible = true;

			if (tabControl1.SelectedIndex == 0)
			{				
				btnExport.Visible = false;
				pnlCriteria.Visible = false;
				grdExport.Visible = false;
			}			
		}

		private void DataBindTemplate()
		{
			_exportTemplate = AIMManager.GetSampleExportTemplate();
			_exportTemplate.Insert
				(
					0,
					new SampleExportTemplateInfo
					{
						SampleExportTemplateID = 0,
						TemplateName = "--Select--"
					}
				);
			lueTemplateName.Properties.DataSource = _exportTemplate;
			XtraEditors.LoadLookUpEdit(ref lueTemplateName, "SampleExportTemplateID", "TemplateName", "ExportPath");
			lueTemplateName.Properties.Columns["ExportPath"].Visible = false;
		}
		
		private void DatabindFileType()
		{

			List<FileTypeInfo> fileType = new List<FileTypeInfo>();
			///////fileType = AIMManager.GetFileType(true);  --> commented until more than CSV is used
			fileType.Insert
			(
				0,
				new FileTypeInfo
				{
					FileTypeID = 0,
					Name = "--Select --"					
				}				
			);
			fileType.Insert
			(
				1,
				new FileTypeInfo
				{
					FileTypeID = 1,
					Name = "CSV"
				}
			);

			lueFileType.Properties.DataSource = fileType;
			XtraEditors.LoadLookUpEdit(ref lueFileType, "FileTypeID", "Name");

			//if (fileType.Count > 0)
			//{
			//	lueFileType.EditValue = fileType[0].FileTypeID;
			//}
			lueFileType.EditValue = fileType[1].FileTypeID;

			Refresh();

		}
		


		private void DatabindDelimiter()
		{
			Int32 fileTypeID = 0;
			if (lueFileType.EditValue != null)
			{
				fileTypeID = (Int32)lueFileType.EditValue;
			}
			List<FileTypeRefDelimiterTypeInfo> delimiterType = new List<FileTypeRefDelimiterTypeInfo>();
			delimiterType = AIMManager.GetFileTypeRefDelimiterType(fileTypeID);
			lueDelimiterType.Properties.DataSource = delimiterType;
			XtraEditors.LoadLookUpEdit(ref lueDelimiterType, "DelimiterTypeID", "Description");
			if (delimiterType.Count > 0)
			{
				lueDelimiterType.EditValue = delimiterType[0].DelimiterTypeID;
			}
		}

		private void SetToolTips()
		{
			System.Drawing.Icon ico = Properties.Resources.CFCAIM;
			this.Icon = ico;
			ToolTip ttip = new ToolTip();
			foreach (Control ctrl in pnlTopBtns.Controls)
			{
				ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
			}
			ttip.SetToolTip(btnPreviewGrid, "Preview Samples to Export");			
			ttip.SetToolTip(btnBrowse, "Browse Files");
			ttip.SetToolTip(btnBrowseNew, "Browse Files");
			ttip.SetToolTip(btnDelete, "Delete Export Template");
			ttip.SetToolTip(btnSave, "Save Export Template");
			ttip.SetToolTip(btnExport, "Export Samples to File");

		}		

		private void btnPreviewGrid_Click(object sender, EventArgs e)
		{
			if (tabControl1.SelectedIndex == 0)
			{
				if ((txtTemplateNameNew.Text.Length == 0))
				{
					MessageBox.Show("Please enter Template Name.", "Warning...");
					txtTemplateNameNew.Focus();
					return;
				}
				if (txtExportPathNew.Text.Length == 0)
				{
					MessageBox.Show("Please enter Export Path.");
					btnBrowseNew.Focus();
					btnBrowseNew.PerformClick();
					return;
				}
			}
			else			
			{
				if ((Int32)lueTemplateName.EditValue == 0)
				{
					MessageBox.Show("Please select Template Name.", "Warning...");
					lueTemplateName.Focus();
					return;
				}
			}
			


			if (dtBeg.Text.Length == 0 && dtEnd.Text.Length == 0 && txtSampleBegin.Text.Length == 0 && txtSampleEnd.Text.Length == 0)
			{
				MessageBox.Show("Please enter criteria above.", "Warning...");
				return;
			}

			String sampleBeg = txtSampleBegin.Text;
			String sampleEnd = txtSampleEnd.Text;
			String dateBeg = dtBeg.Text;
			String dateEnd = dtEnd.Text;
						
			_sampleDetail = AIMManager.GetAllSampleForExport(sampleBeg, sampleEnd, dateBeg, dateEnd);
			grdExport.DataSource = _sampleDetail;
			grdExportView.RefreshData();

			btnExport.Visible = false;
			if (_sampleDetail.Count > 0)
			{
				btnExport.Visible = true;
				timer1.Start();			
			}

		}

		private void WriteCSVFile(String filePath)
		{
			String csvText = "";

			AIMGlobal.SplashScreenShow();						
		
			for (int i = 0; i < _sampleDetail.Count; i++)
			{					
				var field1 = _sampleDetail[i].MaterialTypeRawOrFinished;
				var field2 = _sampleDetail[i].MaterialCode;
				var field3 = _sampleDetail[i].SampleDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
				var field4 = _sampleDetail[i].SampleCode;					
				csvText = csvText + string.Format("{0},{1},{2},{3}", field1, field2, field3, field4) + Environment.NewLine;									
			}
			if (AIMGlobal.exportFileVal == (Int32)EnumSampleExport.Append)
			{
				File.AppendAllText(filePath, csvText);
			}
			else
			{		
				File.WriteAllText(filePath, csvText);
			}
			AIMGlobal.SplashScreenHide();
			
		}
		
		private void SaveSampleExportDetail()
		{

			AIMGlobal.SplashScreenShow();

			SampleExportDetailInfo export = new SampleExportDetailInfo();
			DateTime dtStamp = DateTime.Now;

			//--------------------------------------------------------------
			// Save to SampleExportDetail table			
			for (int row = 0; row < _sampleDetail.Count; row++)
			{
				export.SampleExportDetailID = 0;
				export.SampleExportID = _sampleExportID;
				export.MaterialType = _sampleDetail[row].MaterialType;
				export.MaterialCode = _sampleDetail[row].MaterialCode;
				export.SampleDate = _sampleDetail[row].SampleDate;
				export.SampleCode = _sampleDetail[row].SampleCode;
				export.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				export.UpdateStamp = dtStamp;
				ErrorInfo outPut = AIMManager.SaveSampleExportDetail(ref export);
			}

			AIMGlobal.SplashScreenHide();
		}
			

		private void btnExport_Click(object sender, EventArgs e)
		{
			try
			{
				timer1.Stop();
				btnExport.BackColor = SystemColors.Control;	
				
				if (_sampleDetail.Count == 0)
				{
					MessageBox.Show("There are no samples to export.", "Message...", MessageBoxButtons.OK);
					return;
				}
				if ((Int32)lueTemplateName.EditValue == 0)
				{
					MessageBox.Show("Please select template.", "Message...", MessageBoxButtons.OK);
					lueTemplateName.Focus();
					return;
				}
				if (txtExportPath.Text.Length == 0)
				{
					MessageBox.Show("Please select export path.", "Message...", MessageBoxButtons.OK);
					txtExportPath.Focus();
					return;
				}
				if (txtExportFileName.Text.Length == 0)
				{
					MessageBox.Show("Please enter export file name.", "Message...", MessageBoxButtons.OK);
					txtExportFileName.Focus();
					return;
				}

				String filePath = "";
				if (tabControl1.SelectedIndex == 0)
				{
					filePath = txtExportPathNew.Text;
					filePath += txtExportFileNameNew.Text;
				}
				else
				{
					filePath = txtExportPath.Text;					
					filePath += "\\" + txtExportFileName.Text + "." + lueFileType.Text;
				}

				if (File.Exists(filePath) == false)
				{					
					WriteCSVFile(filePath);
					SaveSampleExportDetail();
				}
				else if (File.Exists(filePath) == true)
				{					
					try
					{
						AIMGlobal.exportFileVal = (Int32)EnumSampleExport.Cancel;
						                      
                        message = new SampleExportMessage();

						//10/6/21 - changed for resize issue
						//this.Hide();
						//message.WindowState = FormWindowState.Normal;                   
						//message.ShowDialog();
						//this.Show();
						message.ShowDialog();


						if (AIMGlobal.exportFileVal == (Int32)EnumSampleExport.Cancel)
						{
							return;
						}

						WriteCSVFile(filePath);
						SaveSampleExportDetail();
						
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
					
				}

				_sampleDetail.Clear();							
				grdExport.DataSource = "";

				MessageBox.Show("Export complete.", "Message...", MessageBoxButtons.OK);			
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ClearTextFields()
		{
			txtSampleBegin.Text = "";
			txtSampleEnd.Text = "";
			dtBeg.Text = "";
			dtEnd.Text = "";
		}
				
		private void btnBrowse_Click(object sender, EventArgs e)
		{
			try
			{

				if ((Int32)lueTemplateName.EditValue == 0)
				{
					MessageBox.Show("Please select Template Name.");
					lueTemplateName.Focus();
					return;
				}
				fbdSelectedDirectory.ShowDialog();
				txtExportPath.Text = fbdSelectedDirectory.SelectedPath;
				txtExportPath.SelectAll();


				/*
				OpenFileDialog openFileDialog = new OpenFileDialog();
				String path = txtExportPath.Text.ToString();
				if (Directory.Exists(path))
				{
					openFileDialog.InitialDirectory = path;
				}
				else
				{
					openFileDialog.InitialDirectory = @"C:\";
				}

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					String filePath = openFileDialog.FileName;
					txtExportPath.Text = openFileDialog.FileName;					
				}
				 */
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
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

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{			
				timer2.Stop();
				btnSave.BackColor = SystemColors.Control;

				if (txtTemplateNameNew.Text.Length == 0)
				{
					MessageBox.Show("Please enter Template Name.");
					txtTemplateNameNew.Focus();
					return;
				}
				if (txtExportPathNew.Text.Length == 0)
				{
					MessageBox.Show("Please select Export Path.");
					btnBrowseNew.Focus();
					return;
				}
				if (txtExportFileNameNew.Text.Length == 0)
				{
					MessageBox.Show("Please enter Export File Name.");
					txtExportFileNameNew.Focus();
					return;
				}
				if ((Int32)lueFileType.EditValue == 0)
				{
					MessageBox.Show("Please select File Type.");
					lueFileType.Focus();
					return;
				}				
							

				SampleExportTemplateInfo export = new SampleExportTemplateInfo();
				export.TemplateName = txtTemplateNameNew.Text;
				export.FileTypeID = (Int32)FileTypeEnum.CSV;
				export.DelimiterTypeID = 1;
				export.ExportPath = txtExportPathNew.Text;
				export.ExportFileName = txtExportFileNameNew.Text;
				export.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo outPut = AIMManager.SaveSampleExportTemplate(ref export);
				DataBindTemplate();

				txtTemplateNameNew.Text = "";
				txtExportPathNew.Text = "";
				txtExportFileNameNew.Text = "";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		private void lueExportName_EditValueChanged(object sender, EventArgs e)
		{
			_sampleExportID = (Int32)lueTemplateName.EditValue;
			txtExportPath.Text = _exportTemplate[lueTemplateName.ItemIndex].ExportPath;
			txtExportFileName.Text = _exportTemplate[lueTemplateName.ItemIndex].ExportFileName;
			txtExportFileName.SelectAll();
		}

		private void btnBrowseNew_Click(object sender, EventArgs e)
		{
			fbdSelectedDirectory.ShowDialog();
			txtExportPathNew.Text = fbdSelectedDirectory.SelectedPath;
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if ((Int32)lueTemplateName.EditValue == 0)
			{
				MessageBox.Show("Please select Template to delete.");
				lueTemplateName.Focus();
				return;
			}


			DialogResult dialogResult = MessageBox.Show("Delete Template '" + lueTemplateName.Text.ToString().TrimEnd() + "'?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{
				Int32 sampleTemplateID = 0;
				sampleTemplateID = (Int32)lueTemplateName.EditValue;
				AIMManager.DeleteSampleExportTemplate(sampleTemplateID);
				DataBindTemplate();
				MessageBox.Show("Template Deleted.");
			}

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (btnExport.BackColor == SystemColors.Control)
			{
				btnExport.BackColor = SystemColors.Info;
			}
			else
			{
				btnExport.BackColor = SystemColors.Control;
			}

		}

		private void txtExportFileName_Enter(object sender, EventArgs e)
		{
			txtExportFileName.Select();
			txtExportFileName.SelectAll();
			
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{

			ShowHideGrid();

			if (tabControl1.SelectedIndex == 1)
			{
				txtExportFileName.Focus();	

			}
			
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			if (btnSave.BackColor == SystemColors.Control)
			{
				btnSave.BackColor = SystemColors.Info;
			}
			else
			{
				btnSave.BackColor = SystemColors.Control;
			}
		}

		private void txtTemplateNameNew_TextChanged(object sender, EventArgs e)
		{
			//check to see if template name already exists and, if so, display message			
			var results = from g in _exportTemplate
						  where g.TemplateName == txtTemplateNameNew.Text
						  select g;
			if (results.Count() != 0)
			{
				MessageBox.Show("This template name already exists. Please enter a new name.", "Warning...");
				txtTemplateNameNew.Focus();
				txtTemplateNameNew.Text = "";
				return;				
			}

			CheckNewTemplateFields();
		}

		private void CheckNewTemplateFields()
		{
			if (txtTemplateNameNew.Text.Length > 0 && txtExportPathNew.Text.Length > 0 && txtExportFileNameNew.Text.Length > 0 && (Int32)lueFileType.EditValue != 0)
			{
				timer2.Start();
			}
			else
			{
				timer2.Stop();
				btnSave.BackColor = SystemColors.Control;
			}
		}
		
		private void txtExportPathNew_TextChanged(object sender, EventArgs e)
		{
			CheckNewTemplateFields();
		}

		private void txtExportFileNameNew_TextChanged(object sender, EventArgs e)
		{
			CheckNewTemplateFields();
		}

		private void lueFileType_EditValueChanged(object sender, EventArgs e)
		{
			CheckNewTemplateFields();
		}		
	}
}