using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid.Views.Base;
using CFCAIM.ReportDevExpress;
using DevExpress.XtraReports.UI;
using Core.Library.DevExpress;


namespace CFCAIM
{
	public partial class SampleLog : Form
	{
		#region private functions
		private void DataBind()
		{
			Int32 currentRowNum = 0;
			currentRowNum = grdViewLog.FocusedRowHandle;
			grdLog.DataSource = AIMManager.GetSampleLog(true);
			grdViewLog.FocusedRowHandle = currentRowNum;
		}
		#endregion

		public SampleLog()
		{
			InitializeComponent();
		}

		private void SampleLog_Load(object sender, EventArgs e)
		{			
			DataBind();
			SetToolTips();
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
		
		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}		
	}
}
