using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using CFCAIM.ReportDevExpress;
using Core.Library.DevExpress;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CFCAIM
{
	public partial class SampleMessageDetail : Form
	{

		public Boolean _isFormLoaded;
		CFCUser.Global _userGlobal = new CFCUser.Global();

		public SampleMessageDetail()
		{
			InitializeComponent();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void SampleMessageDetail_Load(object sender, EventArgs e)
		{
			try
			{
				_isFormLoaded = false;
				DataBind();
				SetToolTips();
				_isFormLoaded = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public void DataBind()
		{
			try
			{

				_isFormLoaded = false;
				Int32 _currentRow = grdMessageDetailView.FocusedRowHandle;
				//grdMessageDetailView.Columns.ColumnByName("colAIMOwnerLocationCode").Visible = false;
								
				//grdMessageDetailView.Columns.ColumnByName("colNumberOfSample").Visible = false;
				//grdMessageDetailView.Columns.ColumnByName("colDate").Visible = false;

				//grdMessageDetailView.DataSource = AIMGlobal.sampleMessage;
				


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
				this.Icon = (Icon)Properties.Resources.CFCAIM;
				ToolTip ttip = new ToolTip();
				foreach (Control ctrl in pnlTopBtns.Controls)
				{
					ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
