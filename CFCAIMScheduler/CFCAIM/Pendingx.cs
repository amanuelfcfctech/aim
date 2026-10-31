using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CFCAIM.Manager;
using System.IO;
using CFCAIM.Entity;
using DevExpress.XtraGrid.Views.Base;
using Core.Library.DevExpress;


namespace CFCAIM
{
	public partial class Pending : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();

		public Pending()
		{
			InitializeComponent();
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

		private void Pending_Load(object sender, EventArgs e)
		{
			try
			{
				System.Drawing.Icon ico = Properties.Resources.CFCAIM;
				this.Icon = ico;
				DataBindGrid();
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

		public void DataBindGrid()
		{
			List<AnalysisExportBatchMaintenance> exportBatchMaint = new List<AnalysisExportBatchMaintenance>();
			exportBatchMaint = AIMManager.GetAllUpdateIngredientHeaderID(1);
			Int32 currentRowNum = 0;
			Int32.TryParse(viewHeader.FocusedRowHandle.ToString(), out currentRowNum);
			grdHeader.DataSource = exportBatchMaint;
			viewHeader.FocusedRowHandle = currentRowNum;


		}

		private void grdHeader_Click(object sender, EventArgs e)
		{

		}

		private void repbtnDetail_Click(object sender, EventArgs e)
		{
			try
			{			
								
				AnalysisExportBatchMaintenance exportDetail = new AnalysisExportBatchMaintenance();
				exportDetail = (AnalysisExportBatchMaintenance)viewHeader.GetFocusedRow();			
				Global.exportbatchmaintenanceHeaderID = exportDetail.UpdateIngredientHeaderID;
				this.Hide();
				ExportBatchMaintenance exportForm = new ExportBatchMaintenance();
				exportForm.ShowDialog();
				this.Show();
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
