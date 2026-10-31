using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using System.Drawing;

namespace CFCAIM.CrossReference
{
    public partial class DataSourceCrossReference : Form
    {
		CFCUser.Global userGlobal = new CFCUser.Global();

        public DataSourceCrossReference()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }

        private void DataSourceCrossReference_Load(Object sender, EventArgs e)
        {
            SetToolTips();


            //8/20/18
            grdDataSourceView.Columns["colDelete"].Visible = true;
            if (AIMGlobal.dataSourceXRefComingFromPreviewImportScreen == true)
            {
                grdDataSourceView.Columns["colDelete"].Visible = false;
            }

            DataBindLue();
            DataBindGrid();
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void DataBindLue()
        {
            List<DataSourceTypeInfo> aimDataSource = new List<DataSourceTypeInfo>();
            aimDataSource = AIMManager.GetDataSourceType(true);
            aimDataSource.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--Select--"
                }
            );
            lueAIMDataSourceType.Properties.DataSource = aimDataSource;
            XtraEditors.LoadLookUpEdit(ref lueAIMDataSourceType, "DataSourceTypeID", "Name");
            if (aimDataSource.Count == 2)
            {
                lueAIMDataSourceType.EditValue = aimDataSource[1].DataSourceTypeID;
            }
            else
            {
                lueAIMDataSourceType.EditValue = 0;
            }

            //Analyzer---------------------------------------------------------------------
            List<AnalyzerDataSourceTypeInfo> analyzerDataSource = new List<AnalyzerDataSourceTypeInfo>();
			analyzerDataSource = AIMManager.GetAnalyzerDataSource(AIMGlobal.analyzerDataSourceTypeID, true);
			analyzerDataSource.Insert
            (
                0,
                new AnalyzerDataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    AnalyzerDataSourceTypeName = "--Select--"
                }
            );
			lueAnalyzerDataSourceType.Properties.DataSource = analyzerDataSource;
            XtraEditors.LoadLookUpEdit(ref lueAnalyzerDataSourceType, "AnalyzerDataSourceTypeID", "AnalyzerDataSourceTypeName");
            if (analyzerDataSource.Count == 2)
            {
                lueAnalyzerDataSourceType.EditValue = analyzerDataSource[1].AnalyzerDataSourceTypeID;
            }
            else
            {
                lueAnalyzerDataSourceType.EditValue = 0;
            }
			lueAnalyzerDataSourceType.Properties.Columns["AnalyzerDataSourceTypeName"].Caption = "Data Source Name";


		}
        private void DataBindGrid()
        {
            {
                Int32 rowNum = 0;
                Int32.TryParse(grdDataSourceView.FocusedRowHandle.ToString(), out rowNum);
                if (AIMGlobal.analyzerDataSourceTypeID != 0)
                {

                    //8/20/18
                    if (AIMGlobal.dataSourceXRefComingFromPreviewImportScreen == true)
                    {
                        grdDataSource.DataSource = AIMManager.GetDataSourceCrossReference(AIMGlobal.analyzerDataSourceTypeID);
                    }
                    else
                    {
                        grdDataSource.DataSource = AIMManager.GetDataSourceDetail(AIMGlobal.analyzerDataSourceTypeID);
                    }                   

                    grdDataSourceView.FocusedRowHandle = rowNum;
                }

                List<DataSourceTypeInfo> aimDataSource = new List<DataSourceTypeInfo>();
                aimDataSource = AIMManager.GetDataSourceType(true);


                //8/20/18
                replueAIMDataSourceTypeName.ReadOnly = false;
                if (AIMGlobal.dataSourceXRefComingFromPreviewImportScreen == true)
                {
                    aimDataSource.Insert
                    (
                        0,
                        new DataSourceTypeInfo
                        {
                            DataSourceTypeID = 0,
                            Name = "Please add data source cross-reference ABOVE."
                        }
                    );

                    replueAIMDataSourceTypeName.ReadOnly = true;
                }

                replueAIMDataSourceTypeName.DataSource = aimDataSource;
                XtraEditors.LoadGridLookUpEdit(ref replueAIMDataSourceTypeName, "DataSourceTypeID", "Name");
                replueAIMDataSourceTypeName.Columns["Name"].Visible = true;

                
                

                List<AnalyzerDataSourceTypeInfo> dataSource = new List<AnalyzerDataSourceTypeInfo>();
                dataSource = AIMManager.GetAnalyzerDataSource(AIMGlobal.analyzerDataSourceTypeID, true);
                replueAnalyzerDataSourceTypeName.DataSource = dataSource;
                XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerDataSourceTypeName, "AnalyzerDataSourceTypeID", "AnalyzerDataSourceTypeName");
                replueAnalyzerDataSourceTypeName.Columns["AnalyzerDataSourceTypeName"].Visible = true;
				replueAnalyzerDataSourceTypeName.Columns["AnalyzerDataSourceTypeName"].Caption = "Data Source Name";

			}
        }


        private void btnResize_Click(Object sender, EventArgs e)
        {            
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void DataSourceCrossReference_SizeChanged(Object sender, EventArgs e)
        {            
            btnResize.Visible = true;
        }
        
        private void SetToolTips()
        {
			this.Icon = (Icon)Properties.Resources.CFCAIM;
			ToolTip ttip = new ToolTip();
			foreach (Control ctrl in pnlTopBtns.Controls)
			{
				ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
			}
			ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
            ttip.SetToolTip(btnAddAnalyzerDataSource, "Add Data Source");
            ttip.SetToolTip(btnPreviewGridResultsAnalyzerDataSource, "Preview Grid");
            
        }

        private void btnAddAnalyzerDataSource_Click(Object sender, EventArgs e)
        {
			try
			{
				Int32 headerID = 0;
				
				if (lueAnalyzerDataSourceType.Text.Length == 0 || (Int32)lueAnalyzerDataSourceType.EditValue == 0)
				{
					MessageBox.Show("Please select a data source name.", "Warning...");
					lueAnalyzerDataSourceType.Focus();
                    lueAnalyzerDataSourceType.ShowPopup();
					return;
				}

                if (lueAIMDataSourceType.Text.Length == 0 || (Int32)lueAIMDataSourceType.EditValue == 0)
                {
                    MessageBox.Show("Please select an AIM data source name.", "Warning...");
                    lueAIMDataSourceType.Focus();
                    lueAIMDataSourceType.ShowPopup();
                    return;
                }



                ErrorInfo result = new ErrorInfo();
				DataSourceHeaderInfo hdr = new DataSourceHeaderInfo();
				hdr.DataSourceHeaderID = 0;
				hdr.DataSourceTypeID = AIMGlobal.analyzerDataSourceTypeID;
				hdr.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				result = AIMManager.SaveDataSourceHeader(ref hdr);

				if (result.ErrorNumber != 0)
				{
					MessageBox.Show("Unable to save the cross-reference..", "Critical...", MessageBoxButtons.OK);
					this.Close();
					return;
				}

				headerID = result.RecordID;  

				DataSourceDetailInfo dtl = new DataSourceDetailInfo();
				dtl.DataSourceDetailID = 0;
				dtl.DataSourceHeaderID = headerID;
				dtl.AnalyzerDataSourceTypeID = (Int32)lueAnalyzerDataSourceType.EditValue;
				dtl.DataSourceTypeID = (Int32)lueAIMDataSourceType.EditValue;
				dtl.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveDataSourceDetail(ref dtl);
				
				if (error.ErrorNumber > 0)
				{
					return;
				}
				DataBindGrid();
				lueAIMDataSourceType.EditValue = 0;
				lueAnalyzerDataSourceType.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        private void btnPreviewGridResultsAnalyzerDataSource_Click(Object sender, EventArgs e)
        {
			try
			{
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.previewGridResults(grdDataSource, "Data Source Cross References");
                CFCAIMCore.Global.previewGridResults(grdDataSource, "Data Source Cross References");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnExit_Click(Object sender, EventArgs e)
		{
			try
			{
				Close();				
				ImportDataUserDefined importDataUserDefined;
                AIMGlobal.open_form(importDataUserDefined = new ImportDataUserDefined());

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnRefresh_Click(Object sender, EventArgs e)
		{							
			DataBindGrid();
		}

		private void repbtnDeleteAnalyzerDataSourceType_Click(Object sender, EventArgs e)
		{
			try
			{
				DataSourceDetailInfo datasourceRef = new DataSourceDetailInfo();
				datasourceRef = (DataSourceDetailInfo)grdDataSourceView.GetFocusedRow();
				AIMManager.DeleteDataSourceDetail(datasourceRef.DataSourceDetailID);

				//If all the detail records are deleted, delete the header rec too.
				AIMManager.DeleteDataSourceHeader(datasourceRef.DataSourceHeaderID);

				DataBindGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void label1_Click(Object sender, EventArgs e)
		{

		}
	}
}
