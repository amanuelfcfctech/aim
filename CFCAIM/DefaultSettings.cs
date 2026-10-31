using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid.Views.Base;
using Core.Library.DevExpress;
using System.Data;

namespace CFCAIM
{
	public partial class DefaultSettings : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();

		public DefaultSettings()
		{
			InitializeComponent();
			CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
		}

		private void DefaultSettings_Load(object sender, EventArgs e)
		{
			try
			{
				System.Drawing.Icon ico = Properties.Resources.CFCAIM;
				this.Icon = ico;
				DataBindlue();
				DataBind();
				SetToolTips();
				WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void DataBind()
		{
			DefaultSettingsInfo defaultSettings = new DefaultSettingsInfo();
			//defaultSettings = AIMManager.GetDefaultSettings(userGlobal.CFCLoginUser.CFCUserID);
		}

		private void DataBindlue()
		{
			List<DataSourceTypeInfo> dataSource = new List<DataSourceTypeInfo>();
			dataSource = AIMManager.GetDataSourceType(true);
			dataSource.Insert
			(
				0,
				new DataSourceTypeInfo
				{
					DataSourceTypeID = 0,
					Name = "--Select--"
				}
			);
			lueDataSourceType.Properties.DataSource = dataSource;
			XtraEditors.LoadLookUpEdit(ref lueDataSourceType, "DataSourceTypeID", "Name", "TypeDisplay");
			lueDataSourceType.Properties.Columns["TypeDisplay"].Caption = "Type";

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
			DefaultSettingsInfo defaults = new DefaultSettingsInfo();
            defaults.DefaultSettingsID = 0;               
            defaults.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;																		
			defaults.DataSourceTypeID = (Int32)lueDataSourceType.EditValue;
							
            ErrorInfo error = new ErrorInfo();
            error = AIMManager.SaveDefaultSettings(ref defaults);
			if (error.ErrorNumber > 0)
			{
				return;
			}
		}

		private void lueDataSourceType_Enter(object sender, EventArgs e)
		{
			lueDataSourceType.SelectAll();
		}

		private void lueDataSourceType_Click(object sender, EventArgs e)
		{
			lueDataSourceType.SelectAll();
		}

		private void SetToolTips()
		{
			ToolTip ttip = new ToolTip();
			foreach (Control ctrl in pnlTopBtns.Controls)
			{
				ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
			}
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
        }

		private void btnResize_Click(Object sender, EventArgs e)
		{			
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}

		private void DefaultSettings_SizeChanged(Object sender, EventArgs e)
		{
			btnResize.Visible = true;
		}
	}
}
