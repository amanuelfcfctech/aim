using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.Drawing;

namespace CFCAIM
{
	public partial class ExportBatchPending : Form
	{
		public ExportBatchPending()
		{
			InitializeComponent();
		}

		private void ExportBatchPending_Load(object sender, EventArgs e)
		{
			try
			{				
				DataBindGrid();         
                btnRefresh.PerformClick();
                SetToolTips();			   
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		public void DataBindGrid()
		{            
            List<AnalysisExportBatchMaintenance> exportBatch = new List<AnalysisExportBatchMaintenance>();
			exportBatch = AIMManager.GetUpdateIngredient(1);            
            Int32 currentRowNum = 0;
			currentRowNum = viewHeader.FocusedRowHandle;
			grdHeader.DataSource = exportBatch;
			viewHeader.FocusedRowHandle = currentRowNum;
        }
			
		private void repViewDetail_Click(object sender, EventArgs e)
		{
			try
			{
				AnalysisExportBatchMaintenance exportBatch = new AnalysisExportBatchMaintenance();				
				MainMenu mainMenu = new MainMenu();
				String plantName = "";

				exportBatch = (AnalysisExportBatchMaintenance)viewHeader.GetFocusedRow();
				CFCAIMCore.Global.exportbatchmaintenanceHeaderID = (Int32)(exportBatch.UpdateIngredientHeaderID);
				CFCAIMCore.Global.exportbatchmaintenanceHeaderName = exportBatch.UpdateName;
				CFCAIMCore.Global.exportbatchTargetID = exportBatch.ExportTargetID;

				//Expand variables 1/10/24
				AIMGlobal.exportbatchIngredientGroupID = exportBatch.ExportExpandID;
				AIMGlobal.exportbatchIngredientGroupName = exportBatch.ExportExpandName;

				//Plant Group variables 1/12/24
				AIMGlobal.exportbatchPlantGroupID = exportBatch.ExportPlantGroupID;
				AIMGlobal.exportbatchPlantGroupName = exportBatch.ExportPlantGroupName;

				AIMGlobal.exportbatchAIMOwnerLocationID = exportBatch.AIMOwnerLocationID;

				if (exportBatch.ExportTargetID == (Int32)ExportTargetEnum.SpecificOverlay ||
					exportBatch.ExportTargetID == (Int32)ExportTargetEnum.SpecificOverlayExpand)			//1/10/24
                {
					CFCPlantInfo cfcplant = new CFCPlantInfo();
					cfcplant = AIMManager.Get_CFCPlant(exportBatch.AIMDatabaseID, exportBatch.CFCPlantID);
					if (cfcplant != null)
					{
						plantName = cfcplant.CFCPlantName.ToString();
					}					
				}

				AIMGlobal.exportbatchCFCPlantName = plantName;


				//2/29/24
				//For all but base exports, update IsOverlay field on UpdateIngredientDetail table (added for Maschhoffs)
				if (exportBatch.ExportTargetID != (Int32)ExportTargetEnum.Base &&
					exportBatch.ExportTargetID != (Int32)ExportTargetEnum.BaseExpand)
				{
					ErrorInfo overlay = new ErrorInfo();
					overlay = AIMManager.UpdateIngredientDetailIsOverlay((Int32)(exportBatch.UpdateIngredientHeaderID));
					if (overlay.ErrorNumber > 0)
					{
						MessageBox.Show(overlay.ErrorMessage, "Critical...", MessageBoxButtons.OK);
						Close();
						return;
					}
				}


				Close();

                AIMGlobal.exportComingFromAnalysis = false;
                ExportBatchMaintenance exportForm;
				AIMGlobal.open_form(exportForm = new ExportBatchMaintenance());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        //5/14/19 - attempting to speed things up for Perdue
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
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
						
		private void ExportBatchPending_Activated(object sender, EventArgs e)
		{                        
            DataBindGrid();          
            btnRefresh.PerformClick();
		}

		
		private void btnPreviewGridResults_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{					
					CFCAIMCore.Global.previewGridResults(grdHeader, "Export Pending Batch");
                }
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnBatchDetailReport_Click(object sender, EventArgs e)
		{
			//AIMGlobal.SplashScreenShow();

			////get data
			//List<ExportBatchMaintenanceReportInfo> reportDataSet = new List<ExportBatchMaintenanceReportInfo>();
			//reportDataSet = AIMManager.GetExportBatchDetailReportData(AIMGlobal.exportbatchmaintenanceHeaderID);
			//ReportFunctions.DataSetExportBatchDetail = reportDataSet;

			//AIMGlobal.SplashScreenHide();

			////run report
			//Int32 reportID = (Int32)ReportEnum.ExportBatchDetail;
			//ReportFunctions.LoadReport(reportID);

			//this.Show();
		}

        private void btnResize_Click(Object sender, EventArgs e)
        {         
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void ExportBatchPending_SizeChanged(Object sender, EventArgs e)
        {            
            btnResize.Visible = true;
        }

		private void SetToolTips()
		{
			try
			{
				ToolTip ttip = new ToolTip();
				this.Icon = (Icon)Properties.Resources.CFCAIM;
				foreach (Control ctrl in pnlTopBtns.Controls)
				{
					ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
				}
				ttip.SetToolTip(btnPreviewGridResults, "Preview Grid");
				ttip.SetToolTip(btnResize, "Back to Normal Screen Size");

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

		private void grdHeader_Click(object sender, EventArgs e)
		{

		}
	}
}
