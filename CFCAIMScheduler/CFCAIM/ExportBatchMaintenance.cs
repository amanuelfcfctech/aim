using CFCAIM.Report;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace CFCAIM
{
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraReports.UI;
    using System.Diagnostics;
    using System.Threading;

    public partial class ExportBatchMaintenance : Form
    {
        CFCUser.Global userGlobal = new CFCUser.Global();
        private Int32 _progressBarID = 0;

        public ExportBatchMaintenance()
        {
            InitializeComponent();
        }

        private void Export_Load(object sender, EventArgs e)
        {
            try
            {
                DataBindGrid();
                SetRadioButtons();
                SetToolTips();
                CheckLastSyncDateTime();

                AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
                aimDatabase = AIMManager.GetAIMDatabase(CFCAIMCore.Global.aimDatabase.AIMDatabaseID);

                txtExportPath.Text = aimDatabase.CFCPath;
                txtBatchName.Text = CFCAIMCore.Global.exportbatchmaintenanceHeaderName.ToString();

                grdviewExportBatchMaintenance.Focus();

                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckLastSyncDateTime()
        {
            AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();

            //9/9/20 - change to use AIMCoreGlobal
            //aimDatabase = AIMManager.GetAIMDatabase(AIMGlobal.aimDatabase.AIMDatabaseID);
            aimDatabase = AIMManager.GetAIMDatabase(CFCAIMCore.Global.aimDatabase.AIMDatabaseID);

            if (aimDatabase != null)
            {
                aimDatabase = AIMManager.GetAIMDatabase(aimDatabase.AIMDatabaseID);
                if (aimDatabase != null)
                {
                    if (aimDatabase.Name != "--Select--")
                    {
                        lblSyncTime.Text = "Last Sync with AIM on " + aimDatabase.SyncTime;
                    }
                }
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
                ttip.SetToolTip(btnExportBatchPreview, "Preview Grid Results");
                ttip.SetToolTip(btnAcceptAll, "Accept All");
                ttip.SetToolTip(btnCancelAll, "Reject All");
                ttip.SetToolTip(btnBatchDetailReport, "Preview Export Batch Detail Report");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            ErrorInfo result = new ErrorInfo();
            result = AIMManager.SaveIngredientDetailStatusAll(CFCAIMCore.Global.exportbatchmaintenanceHeaderID, (Int32)ExportBatchDetailStatusEnum.Cancelled, CFCAIMCore.Global.exportbatchTargetID);
            DataBindGrid();
        }

        private void btnAcceptAll_Click(object sender, EventArgs e)
        {
            ErrorInfo result = new ErrorInfo();
            result = AIMManager.SaveIngredientDetailStatusAll(CFCAIMCore.Global.exportbatchmaintenanceHeaderID, (Int32)ExportBatchDetailStatusEnum.Accepted, CFCAIMCore.Global.exportbatchTargetID);
            DataBindGrid();
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

                if (AIMGlobal.exportComingFromAnalysis == true)
                {
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    ExportBatchPending exportForm;
                    AIMGlobal.open_form(exportForm = new ExportBatchPending());
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                //Check UserAccess Permission
                Int32 rtn = CFCAIM.AIMUtilities.GlobalFunctions.CheckUserPermissions(userGlobal.CFCLoginUser.CFCUserID, (Int32)EnumPermissionTable.AnalysisExport);
                if (rtn == -1) return;

                //Make sure status is updated before exporting -- must be either 1 or 2 to export				
                ErrorInfo result = new ErrorInfo();
                AnalysisExportBatchMaintenance exportBatch = new AnalysisExportBatchMaintenance();
                exportBatch = AIMManager.GetUpdateIngredientHeader(CFCAIMCore.Global.exportbatchmaintenanceHeaderID);

                //1/25/24
                //result = AIMManager.GetIngredientDetailStatusCount(ref exportBatch);
                result = AIMManager.GetIngredientDetailStatusCount(exportBatch.UpdateIngredientHeaderID);

                if (result.ErrorNumber == (Int32)AcceptReject.Reject)
                {
                    MessageBox.Show("All items are either rejected or the values are computed. Nothing to export.", "Information ...", MessageBoxButtons.OK);
                    return;
                }
                if (result.RecordCount != 0)
                {
                    MessageBox.Show("You must set the status on ALL records, to either Accept or Reject.", "Warning ...", MessageBoxButtons.OK);
                    return;
                }


                // Insert record into ProgressBar SQL table				
                ProgressBarInfo progressBar = new ProgressBarInfo();
                progressBar.ProgressBarID = 0;
                progressBar.ProgressBarValue = 1;
                progressBar.ProgressBarDescription = "Sync Program Running";
                progressBar.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                progressBar.ProgressBarType = (Int32)EnumProgressBarType.ExportToConcept5;
                _progressBarID = AIMManager.InsertProgressBar(ref progressBar).RecordID;

                //2/7/18 - added background worker
                AIMGlobal.SplashScreenShow();
                pbExport.Visible = true;
                bgwTimer.RunWorkerAsync();

                //Call sync program --> sync is vb6 program; 
                //6 parameters (customer dbase, SQL connection, new aimDatabaseID, "U" for update, userID, ProgressBarID)				
                AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
                String arg = String.Empty;
                String separator = "~~";
                String sqlConn = String.Empty;

                //String filePath = Application.StartupPath + @"\\CFC5AIM.exe";
                String filePath = Application.StartupPath + @"\CFC5AIM.exe";

                //6/19/18
                String customType = "";

                //9/18/17
                //sqlConn = "Provider=SQLNCLI11;" + CFCAIMCore.Data.AIMDataAccess._database.ConnectionString;				
                sqlConn = "Provider=SQLOLEDB;" + CFCAIMCore.Data.AIMDataAccess._database.ConnectionString;

                sqlConn = sqlConn.Replace("Initial Catalog", "Database");
                aimDatabase = AIMManager.GetAIMDatabase(exportBatch.AIMDatabaseID);

                //6/19/18
                //arg = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}", separator, aimDatabase.CFCPath, sqlConn, aimDatabase.AIMDatabaseID, "U", userGlobal.CFCLoginUser.CFCUserID, _progressBarID, AIMGlobal.exportbatchmaintenanceHeaderID);
                //9/9/20 - change to use AIMCoreGlobal
                //arg = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}", separator, aimDatabase.CFCPath, sqlConn, aimDatabase.AIMDatabaseID, "U", userGlobal.CFCLoginUser.CFCUserID, _progressBarID, AIMGlobal.exportbatchmaintenanceHeaderID, customType);
                arg = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}", separator, aimDatabase.CFCPath, sqlConn, aimDatabase.AIMDatabaseID, "U", userGlobal.CFCLoginUser.CFCUserID, _progressBarID, CFCAIMCore.Global.exportbatchmaintenanceHeaderID, customType);

                if (File.Exists(filePath))
                {

                    ProcessStartInfo proc = new ProcessStartInfo();
                    proc.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.CreateNoWindow = true;
                    proc.UseShellExecute = false;
                    proc.FileName = filePath;
                    proc.Arguments = arg;
                    Process.Start(proc);

                }
                else
                {
                    MessageBox.Show("Formula Database is missing.  No export done.", "Warning...", MessageBoxButtons.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + Environment.NewLine + "Didn't export the file.", "warning...");
            }
        }

        private void btnExportBatchPreview_Click(object sender, EventArgs e)
        {
            try
            {
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.previewGridResults(grdExportBatch, "Export Batch Maintenance");
                CFCAIMCore.Global.previewGridResults(grdExportBatch, "Export Batch Maintenance");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteExportBatch_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = new DialogResult();
                ErrorInfo result = new ErrorInfo();

                dr = MessageBox.Show("Do you want to delete this export batch?", "Warning...", MessageBoxButtons.YesNoCancel);
                if (dr != DialogResult.Yes)
                {
                    return;
                }

                AnalysisExportBatchMaintenance exportBatch = new AnalysisExportBatchMaintenance();

                exportBatch.UpdateIngredientHeaderID = CFCAIMCore.Global.exportbatchmaintenanceHeaderID;
                exportBatch.Status = 3;
                exportBatch.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                exportBatch.UpdateStamp = DateTime.Now;
                exportBatch.UpdateCFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                result = AIMManager.SaveIngredientHeader(ref exportBatch);

                //11/17/23 - commented, irritating
                //dr = MessageBox.Show("Batch Deleted");		//(batch is set to inactive, and detail still exists)

                Close();

                //2/6/18
                if (AIMGlobal.exportComingFromAnalysis == true)
                {
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    ExportBatchPending exportForm;
                    AIMGlobal.open_form(exportForm = new ExportBatchPending());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void SetRadioButtons()
        {

            //1/9/24
            //         if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlay)
            //         {
            //	rdbSpecificOverlay.Checked = true;				
            //	txtCFCPlantName.Text = AIMGlobal.exportbatchCFCPlantName;
            //             txtExpandName.Visible = false;               
            //         }

            //         else if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlay)
            //         {
            //	rdbDefaultOverlay.Checked = true;
            //	txtCFCPlantName.Visible = false;
            //	lblCFCPlantName.Visible = false;
            //             txtExpandName.Visible = false;               
            //         }
            //         else if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.BaseExpand)
            //         {
            //             chkExpand.Checked = true;
            //             txtCFCPlantName.Visible = false;
            //             lblCFCPlantName.Visible = false;
            //             txtExpandName.Text = AIMGlobal.exportbatchCFCPlantName;
            //             txtExpandName.Visible = true;
            //         }
            //         else
            //{
            //	rdbBase.Checked = true;
            //	txtCFCPlantName.Visible = false;
            //	lblCFCPlantName.Visible = false;
            //             txtExpandName.Visible = false;               //10/16/23
            //         }

            txtCFCPlantName.Text = "";
            txtPlantGroupName.Text = "";
            txtExpandName.Text = "";
            txtExpandName.Visible = false;
            chkExpand.Checked = false;
            chkExpand.Visible = false;
            lblExpand.Visible = false;
            lblCFCPlantName.Visible = false;
            txtCFCPlantName.Visible = false;
            lblPlantGroup.Visible = false;
            txtPlantGroupName.Visible = false;

            //--------------------------------------
            //Check Expand
            if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayExpand ||
                    CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlayExpand ||
                    CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.BaseExpand ||
                    (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayPlantGroup && AIMGlobal.exportbatchIngredientGroupID > 0))     //2/5/24
            {
                chkExpand.Visible = true;
                chkExpand.Checked = true;
                lblExpand.Visible = true;
                txtExpandName.Visible = true;
                txtExpandName.Text = AIMGlobal.exportbatchIngredientGroupName;
            }

            //--------------------------------------
            if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlay ||
                CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayExpand ||
                CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayPlantGroup)                 //2/5/24
            {
                rdbSpecificOverlay.Checked = true;
                if (AIMGlobal.exportbatchPlantSingleOrGroup == "Single" ||
                    (AIMGlobal.exportbatchPlantSingleOrGroup == "" && AIMGlobal.exportbatchCFCPlantName != ""))             //2/5/24
                {
                    lblCFCPlantName.Visible = true;
                    txtCFCPlantName.Visible = true;
                    txtCFCPlantName.Text = AIMGlobal.exportbatchCFCPlantName;
                }
                else
                {
                    lblPlantGroup.Visible = true;
                    txtPlantGroupName.Visible = true;
                    txtPlantGroupName.Text = AIMGlobal.exportbatchPlantGroupName;
                }

            }
            else if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlay ||
                CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlayExpand) { rdbDefaultOverlay.Checked = true; }
            else if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.Base ||
                CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.BaseExpand) { rdbBase.Checked = true; }
            else
            {
                rdbBase.Checked = true;
            }

        }

        private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            try
            {

                PageInfoBrick dateTime = e.Graph.DrawPageInfo(PageInfo.DateTime, "Date: {0:M/d/yy h:mm tt}", Color.Black,
                                                                new RectangleF(0, 0, 100, 20), BorderSide.None);

                dateTime.LineAlignment = BrickAlignment.Center;
                dateTime.Alignment = BrickAlignment.Far;
                dateTime.AutoWidth = true;

                PageInfoBrick userName = e.Graph.DrawPageInfo(PageInfo.UserName, "Printed By: {0}", Color.Black,
                                                                new RectangleF(0, 0, 100, 20), BorderSide.None);

                userName.LineAlignment = BrickAlignment.Near;
                userName.Alignment = BrickAlignment.Far;
                userName.AutoWidth = true;

                PageInfoBrick pageNumber = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page: {0} of {1}", Color.Black,
                                                                new RectangleF(0, 0, 100, 20), BorderSide.None);

                pageNumber.LineAlignment = BrickAlignment.Far;
                pageNumber.Alignment = BrickAlignment.Far;
                pageNumber.AutoWidth = true;

                TextBrick pageNumberHeading = e.Graph.DrawString(this.Text + " " + txtBatchName.Text, Color.Black,
                                                                    new RectangleF(0, 0, 200, 20), BorderSide.None);

                pageNumberHeading.Location = new Point(800, 100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void DataBindGrid()
        {
            List<UpdateIngredientDetail> exportBatchMaint = new List<UpdateIngredientDetail>();
            exportBatchMaint = CFCAIM.AIMUtilities.ExportFunctions.GetExportBatch.GetExportBatchMaint();

            if (exportBatchMaint.Count == 0)
            {
                MessageBox.Show("Formulation Cross-references need to be set up before exporting.");
            }
            txtTotalNumberOfSamples.Text = exportBatchMaint.Count.ToString();

            Int32 currentRowNum = 0;
            Int32.TryParse(grdviewExportBatchMaintenance.FocusedRowHandle.ToString(), out currentRowNum);


            //2/2/24
            //grdviewExportBatchMaintenance.Columns.ColumnByName("colCFCPlantCode").Visible = false;
            if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.Base ||
                CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.BaseExpand)
            {
                grdviewExportBatchMaintenance.Columns.ColumnByName("colCFCPlantCode").Visible = false;
            }

            //2/2/24 - commented
            //if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlay)            
            //{
            //    grdviewExportBatchMaintenance.Columns.ColumnByName("colCFCPlantCode").Visible = true;
            //}           

            grdExportBatch.DataSource = exportBatchMaint;
            grdviewExportBatchMaintenance.FocusedRowHandle = currentRowNum;     //This keeps the cursor on the row that was just updated.  
                                                                                //If this line is set before the datasource above, then after the row is updated,
                                                                                //the cursor will jump to the first line instead.
        }

        //6/3/19 - commented - causing problems with accept/reject
        //private void ValidateRow()
        //{
        //    UpdateIngredientDetail exportBatch = new UpdateIngredientDetail();
        //    exportBatch = (UpdateIngredientDetail)grdviewExportBatchMaintenance.GetFocusedRow();
        //    exportBatch.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
        //    AIMManager.SaveIngredientDetail(ref exportBatch);
        //}

        private void repbtnAccept_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            ErrorInfo result = new ErrorInfo();
            DialogResult dr = new DialogResult();
            //ValidateRow();                            //6/3/19 - commented - causing problems with accept/reject

            //-----------------------------------------------------------
            //2/12/24
            Int32 headerID = 0;
            Int32 detailID = 0;
            Boolean isComputed = false;
            Boolean isOverlay = true;
            headerID = CFCAIMCore.Global.exportbatchmaintenanceHeaderID;
            detailID = (Int32)grdviewExportBatchMaintenance.GetRowCellValue(grdviewExportBatchMaintenance.FocusedRowHandle, "UpdateIngredientDetailID");

            //Check for Computed
            List<CFCNutrientInfo> computed = new List<CFCNutrientInfo>();
            computed = AIMManager.GetCFCNutrientUpdateIngredientDetail(detailID);
            if (computed.Count > 0)
            {
                if (computed[0].IsComputed == true)
                {
                    isComputed = true;
                }
            }
            if (isComputed == true)
            {
                dr = MessageBox.Show("Nutrient value is computed in the Formulation system.", "Information ...");
                return;
            }

            //Check for Overlay    
            if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlay ||
                CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlayExpand ||
                CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlay ||
                CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayExpand ||
                CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayPlantGroup)
            {
                List<UpdateIngredientDetail> det = new List<UpdateIngredientDetail>();
                det = AIMManager.GetExportBatchMaintenance(headerID, detailID);
                if (det.Count > 0)
                {
                    isOverlay = det[0].IsOverlay;
                }
                if (isOverlay == false)
                {
                    dr = MessageBox.Show("No overlay exists in Formulation. Do you want to create one?", "Warning...", MessageBoxButtons.YesNo);
                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                }
            }
            //-----------------------------------------------------------

            Decimal overrideAmt = 0;
            UpdateIngredientDetail detail = new UpdateIngredientDetail();
            detail = (UpdateIngredientDetail)grdviewExportBatchMaintenance.GetFocusedRow();
            overrideAmt = Convert.ToDecimal(detail.Override);

            //2/12/24            
            //result = AIMManager.SaveIngredientDetailStatusOverride((Int32)grdviewExportBatchMaintenance.GetRowCellValue(grdviewExportBatchMaintenance.FocusedRowHandle, "UpdateIngredientDetailID"), (Int32)ExportBatchDetailStatusEnum.Accepted, CFCAIMCore.Global.exportbatchTargetID, false, true, overrideAmt);
            result = AIMManager.SaveIngredientDetailStatusOverride(detailID, (Int32)ExportBatchDetailStatusEnum.Accepted, overrideAmt);


            //2/12/24 - commented
            //DialogResult dr = new DialogResult();
            //if (result.ErrorNumber == (Int32)AcceptReject.Computed)
            //{
            //    dr = MessageBox.Show("Nutrient value is computed in the Formulation system.", "Information ...");
            //    return;
            //}
            //if (result.ErrorNumber == (Int32)AcceptReject.NoOverlay)
            //{
            //    dr = MessageBox.Show("No overlay exists in Formulation. Do you want to create one?", "Warning...", MessageBoxButtons.YesNo);
            //    if (dr != DialogResult.Yes)
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        //update ingredientdetail status                     
            //        result = AIMManager.SaveIngredientDetailStatus((Int32)grdviewExportBatchMaintenance.GetRowCellValue(grdviewExportBatchMaintenance.FocusedRowHandle, "UpdateIngredientDetailID"), (Int32)ExportBatchDetailStatusEnum.Accepted, CFCAIMCore.Global.exportbatchTargetID, true, true);
            //    }               
            //}



            DataBindGrid();

        }


        private void repbtnReject_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            UpdateIngredientDetail exportBatch = new UpdateIngredientDetail();
            exportBatch = (UpdateIngredientDetail)grdviewExportBatchMaintenance.GetFocusedRow();

            //6/25/21 - commented 
            //exportBatch.Override = 0;

            exportBatch.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            AIMManager.SaveIngredientDetail(ref exportBatch);

            ErrorInfo result = new ErrorInfo();
            result = AIMManager.SaveIngredientDetailStatus((Int32)grdviewExportBatchMaintenance.GetRowCellValue(grdviewExportBatchMaintenance.FocusedRowHandle, "UpdateIngredientDetailID"), (Int32)ExportBatchDetailStatusEnum.Cancelled, CFCAIMCore.Global.exportbatchTargetID, false, true);
            DataBindGrid();
        }


        private void btnBatchDetailReport_Click(object sender, EventArgs e)
        {
            AIMGlobal.SplashScreenShow();

            List<ExportBatchMaintenanceReportInfo> reportDataSet = new List<ExportBatchMaintenanceReportInfo>();
            reportDataSet = AIMManager.GetExportBatchDetailReportData(CFCAIMCore.Global.exportbatchmaintenanceHeaderID, CFCAIMCore.Global.exportbatchTargetID);
            CFCAIMCore.Global.DataSetExportBatchDetail = reportDataSet;
            AIMGlobal.SplashScreenHide();
            Int32 reportID = (Int32)ReportEnum.ExportBatchDetail;
            ReportFunctions.LoadReport(reportID);

            this.Show();
        }

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void ExportBatchMaintenance_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void grdviewExportBatchMaintenance_CustomDrawCell(Object sender, RowCellCustomDrawEventArgs e)
        {
            try
            {
                Boolean isOverlay = false;
                String statusText = "";
                Boolean isComputed = false;

                if (CFCAIMCore.Global.exportbatchTargetID != (Int32)ExportTargetEnum.Base)

                {
                    //5/14/19 - added to skip out if not these 2 columns 
                    if (e.Column == colStatusDisplay || e.Column == colCurrentNutrientVal)
                    {
                        //5/28/19 uncommented, because possible problem showing IsCompute correctly
                        //5/14/19 - commented because column is already on grid
                        Int32 nutrientID = 0;
                        nutrientID = (Int32)(grdviewExportBatchMaintenance.GetRowCellValue(e.RowHandle, colCFCNutrientID));
                        List<CFCNutrientInfo> cfcNutrient = new List<CFCNutrientInfo>();
                        cfcNutrient = AIMManager.GetCFCNutrientID(nutrientID);
                        if (cfcNutrient.Count > 0)
                        {
                            if (cfcNutrient[0].IsComputed == true)
                            {
                                isComputed = true;
                            }
                        }

                        //5/28/19 removed this, because possible problem showing IsCompute correctly
                        //5/14/19 - added this
                        //isComputed = (Boolean)(grdviewExportBatchMaintenance.GetRowCellValue(e.RowHandle, colIsComputed));

                        isOverlay = (Boolean)(grdviewExportBatchMaintenance.GetRowCellValue(e.RowHandle, colIsOverlay));
                        statusText = grdviewExportBatchMaintenance.GetRowCellValue(e.RowHandle, colStatusDisplay).ToString();

                        if (e.Column == colStatusDisplay)
                        {
                            if (isOverlay == false)
                            {
                                if (statusText != "")
                                {
                                    e.Appearance.BackColor = Color.Yellow;
                                    e.Appearance.ForeColor = Color.Black;                   //11/17/23
                                }
                            }

                            if (isComputed == true)
                            {
                                if (statusText != "")
                                {
                                    e.Appearance.BackColor = Color.LightSkyBlue;
                                }
                            }
                        }


                        if (e.Column == colStatusDisplay)
                        {
                            if (isOverlay == false)
                            {
                                if (statusText != "")
                                {
                                    e.Appearance.BackColor = Color.Yellow;
                                    e.Appearance.ForeColor = Color.Black;               //11/17/23
                                }
                            }
                        }

                        if (e.Column == colCurrentNutrientVal)
                        {
                            if (isOverlay == true)
                            {
                                e.Appearance.ForeColor = Color.Blue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bgwTimer_DoWork(Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Int32 count = 1;
                Process[] pname;
                String processName = "CFC5AIM";

                //9/9/20 - change to use AIMCoreGlobal
                //CFCAIM.AIMGlobal.importProgressBarCounter = 0;
                CFCAIMCore.Global.importProgressBarCounter = 0;

                //pname.length will be 0 until proc (CFC5AIM.exe) is started
                pname = Process.GetProcessesByName(processName);
                while (pname.Length == 0)
                {
                    pname = Process.GetProcessesByName(processName);
                }
                while (pname.Length != 0)
                {
                    count++;
                    bgwTimer.ReportProgress(count);
                    Thread.Sleep(2000);
                    pname = Process.GetProcessesByName(processName);
                }

                bgwTimer.Dispose();

                // Delete record from SQL ProgressBar table			
                AIMManager.DeleteProgressBar(_progressBarID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void bgwTimer_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            AIMGlobal.SplashScreenHide();
            bgwTimer.Dispose();

            //9/9/20 - change to use AIMCoreGlobal
            //CFCAIM.AIMGlobal.importProgressBarCounter = 0;
            CFCAIMCore.Global.importProgressBarCounter = 0;

            pbExport.Visible = false;
            MessageBox.Show("Formula Export completed.");


            //5/15/19                              
            List<UpdateIngredientLogInfo> log = new List<UpdateIngredientLogInfo>();

            //9/9/20 - change to use AIMCoreGlobal
            //log = AIMManager.GetUpdateIngredientLog(AIMGlobal.exportbatchmaintenanceHeaderID);
            log = AIMManager.GetUpdateIngredientLog(CFCAIMCore.Global.exportbatchmaintenanceHeaderID);

            if (log.Count > 0)
            {
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("View the export log?", "Information...", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    ReportDevExpress.UpdateIngredientLogReport report = new ReportDevExpress.UpdateIngredientLogReport();
                    report.DataSource = log;
                    ReportPrintTool tool = new ReportPrintTool(report);
                    AIMGlobal.SplashScreenHide();
                    tool.ShowPreviewDialog();
                }
            }



            Close();
            if (AIMGlobal.exportComingFromAnalysis == true)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                ExportBatchPending exportForm;
                AIMGlobal.open_form(exportForm = new ExportBatchPending());
            }
        }

        private void bgwTimer_ProgressChanged(Object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            ProgressBarInfo progressBar = new ProgressBarInfo();

            try
            {
                if (pbExport.Value != pbExport.Maximum)
                {
                    progressBar = AIMManager.GetProgressBarValue(_progressBarID);
                    if (progressBar.ProgressBarValue > pbExport.Maximum)
                    {
                        progressBar.ProgressBarValue = 1;
                    }
                    pbExport.Value = progressBar.ProgressBarValue;
                }
                else
                {
                    pbExport.Value = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //5/29/19
        private void grdviewExportBatchMaintenance_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            AIMGlobal.focusedRowGreen(sender, e);
        }


        //6/25/21
        private void grdviewExportBatchMaintenance_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            ErrorInfo result = new ErrorInfo();

            //2/13/24 
            DialogResult dr = new DialogResult();
            Int32 headerID = 0;
            Int32 detailID = 0;
            Boolean isComputed = false;
            Boolean isOverlay = true;
            Int32 acceptCol = 0;

            if (e.Column.FieldName != "Override") return;
            GridView gv = sender as GridView;

            //-----------------------------------------------------------
            //2/13/24
            headerID = CFCAIMCore.Global.exportbatchmaintenanceHeaderID;
            detailID = (Int32)gv.GetRowCellValue(grdviewExportBatchMaintenance.FocusedRowHandle, "UpdateIngredientDetailID");
            acceptCol = (Int32)gv.GetRowCellValue(grdviewExportBatchMaintenance.FocusedRowHandle, "Status");

            //Check for Computed
            List<CFCNutrientInfo> computed = new List<CFCNutrientInfo>();
            computed = AIMManager.GetCFCNutrientUpdateIngredientDetail(detailID);
            if (computed.Count > 0)
            {
                if (computed[0].IsComputed == true)
                {
                    isComputed = true;
                }
            }
            if (isComputed == true)
            {
                ///dr = MessageBox.Show("Nutrient value is computed in the Formulation system.", "Information ...");
                return;
            }
            ////Check for Overlay    
            //if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlay ||
            //    CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlayExpand ||
            //    CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlay ||
            //    CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayExpand ||
            //    CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayPlantGroup)
            //{
            //    List<UpdateIngredientDetail> det = new List<UpdateIngredientDetail>();
            //    det = AIMManager.GetExportBatchMaintenance(headerID, detailID);
            //    if (det.Count > 0)
            //    {
            //        isOverlay = det[0].IsOverlay;
            //    }
            //    if (isOverlay == false && acceptCol == (Int32)AcceptReject.NoOverlay)
            //    {
            //        //dr = MessageBox.Show("No overlay exists in Formulation. Do you want to create one?", "Warning...", MessageBoxButtons.YesNo);
            //        //if (dr != DialogResult.Yes)
            //        //{
            //        //    return;
            //        //}
            //        return;
            //    }
            //}
            //-----------------------------------------------------------


            Decimal overrideAmt = 0;
            UpdateIngredientDetail detail = new UpdateIngredientDetail();
            detail = (UpdateIngredientDetail)grdviewExportBatchMaintenance.GetFocusedRow();
            overrideAmt = Convert.ToDecimal(detail.Override);

            //2/12/24
            //result = AIMManager.SaveIngredientDetailStatusOverride((Int32)grdviewExportBatchMaintenance.GetRowCellValue(grdviewExportBatchMaintenance.FocusedRowHandle, "UpdateIngredientDetailID"), (Int32)ExportBatchDetailStatusEnum.Accepted, CFCAIMCore.Global.exportbatchTargetID, false, true, overrideAmt);
            result = AIMManager.SaveIngredientDetailStatusOverride((Int32)grdviewExportBatchMaintenance.GetRowCellValue(grdviewExportBatchMaintenance.FocusedRowHandle, "UpdateIngredientDetailID"), acceptCol, overrideAmt);
        }

        private void fbdExport_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<UpdateIngredientLogInfo> log = new List<UpdateIngredientLogInfo>();
            log = AIMManager.GetUpdateIngredientLog(8);

            if (log.Count > 0)
            {
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("View the export log?", "Information...", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    ReportDevExpress.UpdateIngredientLogReport report = new ReportDevExpress.UpdateIngredientLogReport();
                    report.DataSource = log;
                    ReportPrintTool tool = new ReportPrintTool(report);
                    AIMGlobal.SplashScreenHide();
                    tool.ShowPreviewDialog();
                }
            }
        }
    }
}
