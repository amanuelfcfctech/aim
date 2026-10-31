using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.Drawing;
using Core.Library.DevExpress;

namespace CFCAIM.Maintenance
{

    public partial class Instrument : Form
    {
		CFCUser.Global _UserGlobal = new CFCUser.Global();
		Boolean _isDataLoaded = false;

        public Instrument()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }       	       
        private void Instrument_Load(object sender, EventArgs e)
        {
            try
            {				
				DataBind();				
				SetToolTips();
				ClearTextFields();

                //6/14/17
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
		private void ClearTextFields()
		{
			txtInstrumentCodeNew.Text = String.Empty;
			txtInstrumentDescriptionNew.Text = String.Empty;
		}
		private void DataBind()
		{
			_isDataLoaded = false;
			List<AnalyzerInstrumentInfo> instrument = new List<AnalyzerInstrumentInfo>();
			List<FileTypeInfo> fileType = new List<FileTypeInfo>();
			List<FileTypeInfo> fileTypeNew = new List<FileTypeInfo>();
			List<DataSourceTypeInfo> dataSourceType = new List<DataSourceTypeInfo>();			
			List<DataSourceTypeInfo> dataSourceTypeNew = new List<DataSourceTypeInfo>();	

			instrument = AIMManager.GetAllAnalyzerInstrument();
			instrument.Insert(0, new AnalyzerInstrumentInfo
			{
				InstrumentID = 0,
				Description = "---Select---"
			});

			dataSourceType = AIMManager.GetDataSourceType(true);				
			fileType = AIMManager.GetFileType(true);

			dataSourceTypeNew = AIMManager.GetDataSourceType(true);
			dataSourceTypeNew.Insert(0, new DataSourceTypeInfo
			{
				DataSourceTypeID = 0,
				Name = "---Select---"
			});
					

			fileTypeNew = AIMManager.GetFileType(true);
			fileTypeNew.Insert(0, new FileTypeInfo
			{
				FileTypeID = 0,
				Name = "---Select---"
			});

			//--------------------------------------------------------------------

			lueInstrumentCode.Properties.DataSource = instrument;
			XtraEditors.LoadLookUpEdit(ref lueInstrumentCode, "InstrumentID", "Description");
			lueInstrumentCode.EditValue = 0;		

			lueDataSourceType.Properties.DataSource = dataSourceType;
			XtraEditors.LoadLookUpEdit(ref lueDataSourceType, "DataSourceTypeID", "Name", "TypeDisplay");
			lueDataSourceType.Properties.Columns["TypeDisplay"].Caption = "Type";


			lueFileType.Properties.DataSource = fileType;
			XtraEditors.LoadLookUpEdit(ref lueFileType, "FileTypeID", "Name");
			lueFileType.EditValue = 1;

			//--------------------------------------------------------------------
			//New Instrument Tab

			lueDataSourceTypeNew.Properties.DataSource = dataSourceTypeNew;
			XtraEditors.LoadLookUpEdit(ref lueDataSourceTypeNew, "DataSourceTypeID", "Name", "TypeDisplay");

			lueFileTypeNew.Properties.DataSource = fileTypeNew;
			XtraEditors.LoadLookUpEdit(ref lueFileTypeNew, "FileTypeID", "Name");
			lueFileTypeNew.EditValue = 1;

			_isDataLoaded = true;

		}
		private void SetToolTips()
		{
			Icon = (Icon)Properties.Resources.CFCAIM;
			ToolTip ttip = new ToolTip();
			foreach (Control ctrl in pnlTopBtns.Controls)
			{
				ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
			}
			ttip.SetToolTip(btnRefresh, "Refresh");
			ttip.SetToolTip(btnExit, "Exit");
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
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
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			try
			{
				System.Drawing.Icon ico = Properties.Resources.CFCAIM;
				this.Icon = ico;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueInstrumentCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (_isDataLoaded == false)
				{
					return;
				}
				List<AnalyzerInstrumentInfo> instrument = new List<AnalyzerInstrumentInfo>();

				instrument = (List<AnalyzerInstrumentInfo>)lueInstrumentCode.Properties.DataSource;
				txtInstrumentDescription.Text = instrument[lueInstrumentCode.ItemIndex].Description;
				lueFileType.EditValue = (Int32)instrument[lueInstrumentCode.ItemIndex].FileTypeID;
				lueDataSourceType.EditValue = (Int32)instrument[lueInstrumentCode.ItemIndex].DataSourceTypeID;				

				if (lueInstrumentCode.ItemIndex == 0)
				{
					rbActive.Checked = false;
					rbInactive.Checked = false;
				}

				else if (instrument[lueInstrumentCode.ItemIndex].IsActive == 1)
				{
					rbActive.Checked = true;
				}
				else 
				{
					rbInactive.Checked = true;
				}
				
							}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}				
		private void UpdateInstrument()
		{

			if (_isDataLoaded == false)
			{
				return;
			}
			Int32 fileTypeID = 0;
			Int32 dataSourceTypeID = 0;

			if (lueInstrumentCode.EditValue == null)
			{
				return;
			}
			if ((Int32)lueInstrumentCode.EditValue == 0)
			{
				return;
			}
			AnalyzerInstrumentInfo instrument = new AnalyzerInstrumentInfo();
			CFCUser.Global global = new CFCUser.Global();
			instrument.CFCUserID = global.CFCLoginUser.CFCUserID;
			instrument.Description = txtInstrumentDescription.Text;
			Int32 ID = 0;
			Int32.TryParse(lueInstrumentCode.EditValue.ToString(), out ID);
			instrument.InstrumentID = ID;
			Int32.TryParse(lueFileType.EditValue.ToString(), out fileTypeID);
			instrument.FileTypeID = (FileTypeEnum)fileTypeID;
			Int32.TryParse(lueDataSourceType.EditValue.ToString(), out dataSourceTypeID);
			instrument.DataSourceTypeID = (DataSourceTypeEnum)dataSourceTypeID;		
			instrument.CFCUserID = _UserGlobal.CFCLoginUser.CFCUserID;

			if (rbActive.Checked == true)
			{
				instrument.IsActive = 1;
			}
			else
			{
				instrument.IsActive = 0;
			}

			AIMManager.SaveAnalyzerInstrument(ref instrument);
			DataBind();
		}
		private void lueDataSourceType_Leave(object sender, EventArgs e)
		{
			try
			{
				UpdateInstrument();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueFileType_Leave(object sender, EventArgs e)
		{
			try
			{
				UpdateInstrument();
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
				Int32 fileTypeID = 0;
				Int32 dataSourceTypeID = 0;				

				if (txtInstrumentCodeNew.Text.Trim().Length == 0 ||
					txtInstrumentDescriptionNew.Text.Trim().Length == 0 ||					
					lueFileTypeNew.EditValue == null ||
					(lueDataSourceTypeNew.EditValue == null || (Int32)lueDataSourceTypeNew.EditValue == 0) 
				   )				
				{
					MessageBox.Show("Not everything is filled in. Please complete and press save.", "Warning...");
				}
				
				else
				{
					AnalyzerInstrumentInfo instrument = new AnalyzerInstrumentInfo();
					CFCUser.Global global = new CFCUser.Global();
					instrument.CFCUserID = global.CFCLoginUser.CFCUserID;					
					instrument.InstrumentID = 0;
					instrument.InstrumentCode = txtInstrumentCodeNew.Text;
					instrument.Description = txtInstrumentDescriptionNew.Text;
					Int32.TryParse(lueFileTypeNew.EditValue.ToString(), out fileTypeID);
					instrument.FileTypeID = (FileTypeEnum)fileTypeID;
					Int32.TryParse(lueDataSourceTypeNew.EditValue.ToString(), out dataSourceTypeID);
					instrument.DataSourceTypeID = (DataSourceTypeEnum)dataSourceTypeID;						
					instrument.CFCUserID = _UserGlobal.CFCLoginUser.CFCUserID;
					instrument.IsActive = 1;				
					AIMManager.SaveAnalyzerInstrument(ref instrument);
					RefreshEverything();
					
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void RefreshEverything()
		{
			txtInstrumentDescription.ResetText();
			txtInstrumentCodeNew.ResetText();
			txtInstrumentDescriptionNew.ResetText();					
			DataBind();
		}
		private void txtInstrumentDescription_Leave(object sender, EventArgs e)
		{
			try
			{
				UpdateInstrument();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnDelete_Click(object sender, EventArgs e)
		{
			Int32 instrumentID = 0;
			try
			{
				Int32.TryParse(lueInstrumentCode.EditValue.ToString(), out instrumentID);
				if (instrumentID > 0)
				{

					DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this instrument?", "Warning...", MessageBoxButtons.YesNo);
					if (dialogResult == DialogResult.Yes)
					{
						AIMManager.DeleteInstrument(instrumentID);
						DataBind();
						RefreshEverything();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void gbActive_Leave(object sender, EventArgs e)
		{
			try
			{
				UpdateInstrument();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
						

		private void rbInactive_Leave(object sender, EventArgs e)
		{
			try
			{
				UpdateInstrument();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void rbActive_Leave(object sender, EventArgs e)
		{
			try
			{
				UpdateInstrument();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        private void btnResize_Click(Object sender, EventArgs e)
        {
            //6/14/17
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void Instrument_SizeChanged(Object sender, EventArgs e)
        {
            //6/14/17
            btnResize.Visible = true;
        }
    }
}