using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid.Views.Base;
using Core.Library.DevExpress;

namespace CFCAIM
{
    public partial class FileTypes : Form
    {
        CFCUser.Global userGlobal = new CFCUser.Global();

        public FileTypes()
        {
            InitializeComponent();
        }

        private void FileTypes_Load(Object sender, EventArgs e)
        {
            try
            {               
                DataBindlueDataSourceType();
				DataBindlueFileType();
				DatabindGrid();
                SetToolTips();
                ClearFields();

                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataBindlueDataSourceType()
        {
         	List<DataSourceAllTypesInfo> dataSource = new List<DataSourceAllTypesInfo>();
			dataSource = AIMManager.GetDataSourceAllTypes(true);

			dataSource.Insert
            (
				0,
				new DataSourceAllTypesInfo
				{
					RowNumber = 0,
					Name = "--Select--"
				}
			);
            lueDataSourceType.Properties.DataSource = dataSource;
            XtraEditors.LoadLookUpEdit(ref lueDataSourceType, "DataSourceTypeID", "Name");
        }

		private void DataBindlueFileType()
		{
			List<FileTypeInfo> fileType = new List<FileTypeInfo>();

			fileType = AIMManager.GetFileType(true);
			fileType.Insert
			(
				0,
				new FileTypeInfo
				{
					FileTypeID = 0,
					Name = "--Select--"
				}
			);
			lueFileType.Properties.DataSource = fileType;
			XtraEditors.LoadLookUpEdit(ref lueFileType, "FileTypeID", "Name");

			Refresh();

		}

		private void DatabindGrid()
        {
            Int32 rowNum = 0;
            Int32.TryParse(grdFileTypeView.FocusedRowHandle.ToString(), out rowNum);
            if (lueDataSourceType.EditValue != null)
            {
                grdFileType.DataSource = AIMManager.GetDataSourceTypeRefFileType((Int32)lueDataSourceType.EditValue);
            }
            grdFileTypeView.FocusedRowHandle = rowNum;
        }

        private void btnExit_Click(Object sender, EventArgs e)
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

        private void btnRefresh_Click(Object sender, EventArgs e)
        {
            try
            {
                DataBindlueDataSourceType();
                DatabindGrid();               
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetToolTips()
        {
            try
            {
                this.Icon = Properties.Resources.CFCAIM;
                ToolTip ttip = new ToolTip();
                foreach (Control ctrl in pnlTopBtns.Controls)
                {
                    ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
                }                
                ttip.SetToolTip(btnAddFileType, "Add Record");
                ttip.SetToolTip(btnPreviewGridFileType, "Preview Grid Results");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearFields()
        {            
            lueDataSourceType.EditValue = 0;
            lueFileType.EditValue = 0;

        }
        private void btnPreviewGridFileType_Click(Object sender, EventArgs e)
        {
            try
            {
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.previewGridResults(grdFileType, "File Types for Datasources");
                CFCAIMCore.Global.previewGridResults(grdFileType, "File Types for Datasources");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueDataSourceType_EditValueChanged(Object sender, EventArgs e)
        {
            DatabindGrid();
        }

        private void btnAddFileType_Click(Object sender, EventArgs e)
        {
            try
            {
                if ((Int32)lueDataSourceType.EditValue == 0)
                {
                    MessageBox.Show("Please select a Datasource. ", "Warning...", MessageBoxButtons.OK);
                    lueDataSourceType.Focus();
                    lueDataSourceType.ShowPopup();
                    return;
                }
                if ((Int32)lueFileType.EditValue == 0)
                {
                    MessageBox.Show("Please select a File Type. ", "Warning...", MessageBoxButtons.OK);
                    lueFileType.Focus();
                    lueFileType.ShowPopup();
                    return;
                }

                List<DataSourceTypeRefFileTypeInfo> xref = new List<DataSourceTypeRefFileTypeInfo>();
                xref = AIMManager.GetDataSourceTypeIDFileID((Int32)lueDataSourceType.EditValue, (Int32)lueFileType.EditValue);
                if (xref.Count != 0)
                {
                    MessageBox.Show("This file type already exists for the datasource. ", "Warning...", MessageBoxButtons.OK);
                    lueFileType.Focus();
                    return;
                }

                DataSourceTypeRefFileTypeInfo def = new DataSourceTypeRefFileTypeInfo();
                def.DataSourceTypeRefFileTypeID = 0;
                def.DataSourceTypeID = (Int32)lueDataSourceType.EditValue;
                def.FileTypeID = (Int32)lueFileType.EditValue;
                def.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                ErrorInfo error = new ErrorInfo();
                error = AIMManager.SaveDataSourceTypeRefFileType(ref def);
                if (error.ErrorNumber > 0)
                {
                    return;
                }                
                DatabindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		private void repbtnDeleteFileType_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				DataSourceTypeRefFileTypeInfo dataSource = new DataSourceTypeRefFileTypeInfo();
				dataSource = (DataSourceTypeRefFileTypeInfo)grdFileTypeView.GetFocusedRow();

				if (dataSource.IsSystem == true)
				{
					MessageBox.Show("This is a System datasource and file type. Cannot Delete.", "Warning...", MessageBoxButtons.OK);
					return;
				}

				UserDefinedDataSourceDefinitionInfo def = new UserDefinedDataSourceDefinitionInfo();							
				def = AIMManager.GetUserDefinedDataSourceDefinitionDataSourceUserIDFileTypeID(dataSource.DataSourceTypeID, dataSource.FileTypeID);

				if (def != null)
				{
					MessageBox.Show("This datasource/file-type is currently in use on a user-defined import map. Cannot Delete.", "Warning...", MessageBoxButtons.OK);					
					return;
				}

				dataSource.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteDataSourceTypeRefFileType(dataSource.DataSourceTypeRefFileTypeID);
												
				DatabindGrid();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void FileTypes_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }
    }
}
