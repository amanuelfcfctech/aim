using CFCAIMCore.Entity.Adisseo;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Manager;
using CFCAIMCore.Entity;

namespace CFCAIM
{
	public partial class SystemLog : Form
	{
		public SystemLog()
		{
			InitializeComponent();
		}

		private void SystemLog_Load(object sender, EventArgs e)
		{
			DataBind();
			SetToolTips();
			AIMGlobal.SplashScreenHide();
			WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
		}

		private void DataBind()
		{
			List<SystemLogInfo> log = new List<SystemLogInfo>();
			log = AIMManager.GetSystemLog();
			grdSystemLog.DataSource = log;
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

		private void btnPreview_Click(object sender, EventArgs e)
		{
			try
			{
				CFCAIMCore.Global.previewGridResults(grdSystemLog, "System Log");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
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
			ttip.SetToolTip(btnPreview, "Preview Grid");
		}

		private void SystemLog_SizeChanged(object sender, EventArgs e)
		{
			btnResize.Visible = true;
		}

		private void btnResize_Click(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}
	}
}
