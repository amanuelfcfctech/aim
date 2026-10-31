using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIM.Entity;
using CFCAIM.Manager;
using DevExpress.XtraGrid.Views.Base;
using CFCAIM.ReportDevExpress;
using DevExpress.XtraReports.UI;

namespace CFCAIM
{
	public partial class ExportPending : Form
	{
		public ExportPending()
		{
			//TODOCMInitializeComponent();
		}

		private void ExportPending_Load(object sender, EventArgs e)
		{
			try
			{
				System.Drawing.Icon ico = Properties.Resources.CFCAIM;
				this.Icon = ico;
				DataBindGrid();
				ToolTip ttip = new ToolTip();
				//TODOCMforeach (Control ctrl in pnlTopBtns.Controls)
				//{
				//	ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
				//}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public void DataBindGrid()
		{
			List<AnalysisExportBatchMaintenance> exportBatch = new List<AnalysisExportBatchMaintenance>();
			exportBatch = AIMManager.GetAllUpdateIngredientHeaderID(1);
			Int32 currentRowNum = 0;
			//TODOCMcurrentRowNum = viewHeader.FocusedRowHandle;
			//grdHeader.DataSource = exportBatch;
			//viewHeader.FocusedRowHandle = currentRowNum;

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

		private void repViewDetail_Click(object sender, EventArgs e)
		{
			try
			{

				AnalysisExportBatchMaintenance exportBatch = new AnalysisExportBatchMaintenance();
				//TODOCMexportBatch = (AnalysisExportBatchMaintenance)viewHeader.GetFocusedRow();
				Global.exportbatchmaintenanceHeaderID = (Int32)(exportBatch.UpdateIngredientHeaderID);
				Global.exportbatchmaintenanceHeaderName = exportBatch.UpdateName;
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

		private void viewHeader_Click(object sender, EventArgs e)
		{

		}

		private void btnExit_Click_1(object sender, EventArgs e)
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

		private void grdHeader_Click(object sender, EventArgs e)
		{

		}
	}
}

