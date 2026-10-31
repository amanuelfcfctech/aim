
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using System.Drawing;
using CFCAIMCore.Manager;
using System.Diagnostics;
using System.Threading;
using System.IO;
using DevExpress.XtraReports.UI;

//9/16/20
//using CFCAIMCustom.Manager;
//using CFCAIMCustom.Entity.Vitapro;
using CFCAIM.Custom.Vitapro.Manager;
using CFCAIM.Custom.Vitapro.Entity;

//9/16/20
//namespace CFCAIMCustom.Vitapro
namespace CFCAIM.Custom.Vitapro.Forms

{
    public partial class Export : Form
    {

        

        Int32 _cfcUserID = CFCAIMCore.AIMCoreGlobal.CFCUserID;
        private Int32 _progressBarID = 0;
        private Boolean _exportSent = false;

        public Export()
        {
            InitializeComponent();
        }
                
        private void Export_Load(Object sender, EventArgs e)
        {
            //8/28/19 --------------
            grdviewExportBatchMaintenance.Columns.ColumnByName("colLotNumber").Visible = false;
            grdviewExportBatchMaintenance.Columns.ColumnByName("colCFCIngredientCode").Visible = false;
            pnlExportInfo.Visible = true;
            if (CFCAIM.Custom.Globals.Global.blnMassExport == true)
            {
                pnlExportInfo.Visible = false;
                grdviewExportBatchMaintenance.Columns.ColumnByName("colLotNumber").Visible = true;
                grdviewExportBatchMaintenance.Columns.ColumnByName("colCFCIngredientCode").Visible = true;
            }
            //---------------------

            DataBindGrid();
            SetToolTips();
            //MW200908 Global.exportbatchTargetID = (Int32)ExportTargetEnum.DefaultOverlay;
            //MW200908 Global.exportbatchTargetID = (Int32)ExportTargetEnum.DefaultOverlay;
            CFCAIMCore.Global.exportbatchTargetID = (Int32)ExportTargetEnum.DefaultOverlay;
            CFCAIMCore.Global.exportbatchTargetID = (Int32)ExportTargetEnum.DefaultOverlay;

            AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
            //MW200908 aimDatabase = AIMManager.GetAIMDatabase(AIMGlobal.aimDatabase.AIMDatabaseID);
            aimDatabase = AIMManager.GetAIMDatabase(CFCAIMCore.Global.aimDatabase.AIMDatabaseID);
            txtExportPath.Text = aimDatabase.CFCPath;

            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        public void DataBindGrid()
        {

            String msg = "";
            List<UpdateIngredientDetail> exportBatchMaint = new List<UpdateIngredientDetail>();
            exportBatchMaint = CFCAIM.Custom.Globals.Global.exportDetail;
            grdExportBatch.DataSource = exportBatchMaint;
            grdExportBatch.RefreshDataSource();
            Int32 currentRowNum = 0;
            Int32.TryParse(grdviewExportBatchMaintenance.FocusedRowHandle.ToString(), out currentRowNum);

            if (exportBatchMaint.Count == 0)
            {             
                msg = "Falta una referencia cruzada. Asegúrese de que la identificación de la planta de la muestra tenga referencias cruzadas en la sección Mantenimiento de línea.";
                msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                msg = msg + "(A cross-reference is missing.  Make sure the sample's plant id is cross-referenced under Line Maintenance.)";
                MessageBox.Show(msg);
            }
            else
            {
                grdviewExportBatchMaintenance.FocusedRowHandle = currentRowNum;

                txtLotNumber.Text = exportBatchMaint[0].LotNumber;
                txtIngredientCode.Text = exportBatchMaint[0].CFCIngredientCode;
                txtIngredientName.Text = exportBatchMaint[0].CFCIngredientName;
            }

        }

        private void SetToolTips()
        {
            try
            {                
                ToolTip ttip = new ToolTip();
                ttip.SetToolTip(btnExit, "Salida");
                ttip.SetToolTip(btnRefresh, "Refrescar");
                ttip.SetToolTip(btnResize, "Volver al tamaño normal de la pantalla");
                ttip.SetToolTip(btnExportBatchPreview, "Vista previa de cuadricula");
                ttip.SetToolTip(btnAcceptAll, "Aceptar todo");
                ttip.SetToolTip(btnCancelAll, "Rechazar todo");
                ttip.SetToolTip(btnExport, "Exportar");
                ttip.SetToolTip(btnBatchDetailReport, "Informe de detalle de exportación");
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

        private void Export_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnExit_Click(Object sender, EventArgs e)
        {
            try
            {
                String msg = "";
                DialogResult dr = new DialogResult();

                if (CFCAIM.Custom.Globals.Global.blnMassExport == false)
                {
                    if (_exportSent == false)
                    {
                        //9/2/20 - Set Concept5ExportStatus = false
                        CustomManager.SaveConcept5ExportStatus(CFCAIMCore.Global.sampleID, false);
                                               

                        if (((List<UpdateIngredientDetail>)grdExportBatch.DataSource).Count == 0)     //(Count could be zero if cross-refs are not set up, so that's why this check is here.)
                        {
                            //update record back to available (from current)
                            CustomManager.SaveProductionLineStatus(CFCAIMCore.Global.sampleID, CFCAIM.Custom.Globals.Global.lineNumber, (Int32)EnumProductionStatus.Available, _cfcUserID);
                        }
                        else
                        {
                            msg = "Exportar a Concept5 no ha sido hecho. Desea mantener este elemento de mantenimiento actual?";
                            msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                            msg = msg + "(Export to Concept5 has not been done. Do you want to keep this maintenance item Current?)";
                            dr = MessageBox.Show(msg, "Advertencia...", MessageBoxButtons.YesNo);

                            if (dr == DialogResult.Yes)
                            {
                                // make "..." visible

                                // change export status so Lot hyperlink color turns yellow                       
                                CustomManager.SaveExportStatus(CFCAIMCore.Global.sampleID, true);
                            }
                            else
                            {
                                //update record back to available (from current)
                                CustomManager.SaveProductionLineStatus(CFCAIMCore.Global.sampleID, CFCAIM.Custom.Globals.Global.lineNumber, (Int32)EnumProductionStatus.Available, _cfcUserID);

                                //change export status so Lot hyperlink color goes back to regular color
                                CustomManager.SaveExportStatus(CFCAIMCore.Global.sampleID, false);
                            }
                        }
                    }
                    else
                    {
                        //change export status so Lot hyperlink color goes back to regular color
                        CustomManager.SaveExportStatus(CFCAIMCore.Global.sampleID, false);
                    }

                }
                Close();
                SampleProduction sampleProduction;
                AIMGlobal.open_form(sampleProduction = new SampleProduction());
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                
        private void btnPreviewGrid_Click(Object sender, EventArgs e)
        {
            try
            {
                //MW200908 Global.previewGridResults(grdExportBatch, "Exportar");
                CFCAIMCore.Global.previewGridResults(grdExportBatch, "Exportar");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExportBatchPreview_Click(Object sender, EventArgs e)
        {
            try
            {
                //MW200908 CFCAIMCore.Global.previewGridResults(grdExportBatch, "Exportar");
                CFCAIMCore.Global.previewGridResults(grdExportBatch, "Exportar");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAcceptAll_Click(Object sender, EventArgs e)
        {            
            //result = AIMManager.SaveIngredientDetailStatusAll(AIMGlobal.exportbatchmaintenanceHeaderID, (Int32)ExportBatchDetailStatusEnum.Accepted, AIMGlobal.exportbatchTargetID);                        
            List<CFCNutrientInfo> cfcNutrient = new List<CFCNutrientInfo>();
            Boolean isComputed = false;
            Boolean isOverlay = false;

            List<UpdateIngredientDetail> detail = new List<UpdateIngredientDetail>();            
            detail = (List<UpdateIngredientDetail>)grdExportBatch.DataSource;
            for (int i = 0; i < detail.Count; i++)
            {
                //for each nutrient, check to see if it's computed
                isComputed = false;
                //cfcNutrient = AIMManager.GetCFCNutrientID(detail[i].CFCIngredientID);
                cfcNutrient = AIMManager.GetCFCNutrientID(detail[i].CFCNutrientID);
                if (cfcNutrient.Count > 0)
                {
                    if (cfcNutrient[0].IsComputed == true)
                    {
                        isComputed = true;
                    }
                }

                //set overlay variable
                isOverlay = detail[i].IsOverlay;

                //Target = 1 -----------------------------------------------------------
                //MW200908 if (Global.exportbatchTargetID == 1)
                if (CFCAIMCore.Global.exportbatchTargetID == 1)
                    {                    
                    if (isComputed == true)
                    {
                        detail[i].Status = (Int32)ExportBatchDetailStatusEnum.Computed;
                    }
                    else
                    {
                        detail[i].Status = (Int32)ExportBatchDetailStatusEnum.Accepted;
                    }                    
                }

                //----------------------------------------------------------------------
                else
                {
                    if (isComputed == true)
                    {
                        detail[i].Status = (Int32)ExportBatchDetailStatusEnum.Computed;
                    }
                    else if (detail[i].IsOverlay == false)
                    {
                        detail[i].Status = (Int32)ExportBatchDetailStatusEnum.NoOverlay;
                    }
                    else
                    {
                        detail[i].Status = (Int32)ExportBatchDetailStatusEnum.Accepted;
                    }
                }
                
                          
            }
            CFCAIM.Custom.Globals.Global.exportDetail = detail;
            DataBindGrid();

        }

        private void btnCancelAll_Click(Object sender, EventArgs e)
        {
            List<UpdateIngredientDetail> detail = new List<UpdateIngredientDetail>();
            detail = (List<UpdateIngredientDetail>)grdExportBatch.DataSource;
            for (int i = 0; i < detail.Count; i++)
            {
                detail[i].Status = (Int32)ExportBatchDetailStatusEnum.Cancelled;

            }
            CFCAIM.Custom.Globals.Global.exportDetail = detail;
            DataBindGrid();
        }

        private void ValidateRow()
        {            
            List<UpdateIngredientDetail> detail = new List<UpdateIngredientDetail>();
            detail = (List<UpdateIngredientDetail>)grdExportBatch.DataSource;
            CFCAIM.Custom.Globals.Global.exportDetail = detail;
            DataBindGrid();
        }

        private void repbtnReject_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {           
            ValidateRow();
            List<UpdateIngredientDetail> detail = new List<UpdateIngredientDetail>();
            UpdateIngredientDetail exportBatch = new UpdateIngredientDetail();
            exportBatch = (UpdateIngredientDetail)grdviewExportBatchMaintenance.GetFocusedRow();
            detail = (List<UpdateIngredientDetail>)grdExportBatch.DataSource;            
            for (int i = 0; i < detail.Count; i++)
            {
                if (detail[i].UpdateIngredientDetailID == exportBatch.UpdateIngredientDetailID)
                {
                    detail[i].Override = 0;
                    detail[i].Status = (Int32)ExportBatchDetailStatusEnum.Cancelled;
                }
            }
            CFCAIM.Custom.Globals.Global.exportDetail = detail;
            DataBindGrid();
        }

        private void repbtnAccept_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            String msg = "";

            ValidateRow();

            Boolean isComputed = false;
            Boolean bypassIsOverlay = false;
            Int32 status = (Int32)ExportBatchDetailStatusEnum.Unassigned;

            DialogResult dr = new DialogResult();
            List<UpdateIngredientDetail> detail = new List<UpdateIngredientDetail>();
            UpdateIngredientDetail exportBatch = new UpdateIngredientDetail();
            exportBatch = (UpdateIngredientDetail)grdviewExportBatchMaintenance.GetFocusedRow();
            detail = (List<UpdateIngredientDetail>)grdExportBatch.DataSource;

            List<CFCNutrientInfo> cfcNutrient = new List<CFCNutrientInfo>();
            cfcNutrient = AIMManager.GetCFCNutrientID(exportBatch.CFCNutrientID);
            if (cfcNutrient.Count > 0)
            {
                isComputed = cfcNutrient[0].IsComputed;
            }
            
            if (isComputed == true)
            {
                status = (Int32)ExportBatchDetailStatusEnum.Computed;                
                msg = "El valor de nutrientes se calcula en el sistema de Formulación.";
                msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                msg = msg + "(Nutrient value is computed in the Formulation system.)";
                dr = MessageBox.Show(msg, "Información ...");

                return;
            }
            //MW200908 else if (Global.exportbatchTargetID != 1 && exportBatch.IsOverlay == false && bypassIsOverlay == false)
            else if (CFCAIMCore.Global.exportbatchTargetID != 1 && exportBatch.IsOverlay == false && bypassIsOverlay == false)
            {
                status = (Int32)ExportBatchDetailStatusEnum.NoOverlay;
                msg = "No hay superposición en la formulación. ¿Quieres crear uno ? ";
                msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                msg = msg + "No overlay exists in Formulation. Do you want to create one?";
                dr = MessageBox.Show(msg, "Advertencia...", MessageBoxButtons.YesNo);

                if (dr != DialogResult.Yes)
                {}
                else
                {
                    status = (Int32)ExportBatchDetailStatusEnum.Accepted;
                }
            }
            else
            {
                status = (Int32)ExportBatchDetailStatusEnum.Accepted;
            }
            
            for (int i = 0; i < detail.Count; i++)
            {
                if (detail[i].UpdateIngredientDetailID == exportBatch.UpdateIngredientDetailID)
                {                   
                    detail[i].Status = status;
                }
            }
            CFCAIM.Custom.Globals.Global.exportDetail = detail;

            DataBindGrid();
        }

        private void btnExport_Click(Object sender, EventArgs e)
        {
            try
            {
                String msg = "";
                Int32 headerID = 0;
                Int32 totalCnt = 0;
                Int32 rejectCnt = 0;
                Int32 unassignedCnt = 0;

                //Check UserAccess Permission
                //MW200908 Int32 rtn = CFCAIM.AIMUtilities.GlobalFunctions.CheckUserPermissions(_cfcUserID, (Int32)EnumPermissionTable.AnalysisExport);
                Int32 rtn = CFCAIMCore.Global.CheckUserPermissions(_cfcUserID, (Int32)EnumPermissionTable.AnalysisExport);
                if (rtn == -1) return;

                //-------------------------------------------------------------------------------
                //9/2/20 - Set Concept5ExportStatus = false
                CustomManager.SaveConcept5ExportStatus(CFCAIMCore.Global.sampleID, false);               


                //-------------------------------------------------------------------------------
                //Make sure status is updated before exporting -- must be either 1 or 2 to export				               
                List<UpdateIngredientDetail> detailEntity = new List<UpdateIngredientDetail>();
                detailEntity = (List<UpdateIngredientDetail>)grdExportBatch.DataSource;
                for (int i = 0; i < detailEntity.Count; i++)
                {
                    totalCnt++;
                    if (detailEntity[i].Status == (Int32)ExportBatchDetailStatusEnum.Cancelled || detailEntity[i].Status == (Int32)ExportBatchDetailStatusEnum.Computed)
                    {
                        rejectCnt++;
                    }
                    if (detailEntity[i].Status == (Int32)ExportBatchDetailStatusEnum.Unassigned)
                    {
                        unassignedCnt++;
                    }               
                }
                if (totalCnt == rejectCnt)
                {                    
                    msg = "Todos los artículos son rechazados o los valores son calculados. Nada para exportar.";
                    msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    msg = msg + "(All items are either rejected or the values are computed. Nothing to export.)";
                    MessageBox.Show(msg, "Información...", MessageBoxButtons.OK);
                    return;
                }
                if (unassignedCnt != 0)
                {                    
                    msg = "Debe establecer el estado en TODOS los registros, en Aceptar o Rechazar.";
                    msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    msg = msg + "(You must set the status on ALL records, to either Accept or Reject.)";
                    MessageBox.Show(msg, "Advertencia ...", MessageBoxButtons.OK);
                    return;
                }
                //-------------------------------------------------------------------------------
                
                                
                //save header and detail entities to table
                AnalysisExportBatchMaintenance header = new AnalysisExportBatchMaintenance();                
                List<AnalysisExportBatchMaintenance> headerEntity = new List<AnalysisExportBatchMaintenance>();
                
                ErrorInfo result = new ErrorInfo();
                headerEntity = CFCAIM.Custom.Globals.Global.exportHeader;
                for (int i = 0; i < headerEntity.Count; i++)
                {
                    //----------------- HEADER -------------------------------------------- 	
                    header.UpdateIngredientHeaderID = 0;
                    header.UpdateName = headerEntity[i].LotNumber;
                    header.Status = 1;
                    //MW200908 header.AIMDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
                    header.AIMDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;
                    //MW200908 header.ExportTargetID = Global.exportbatchTargetID;
                    header.ExportTargetID = CFCAIMCore.Global.exportbatchTargetID;

                    //8/30/19
                    //header.AIMOwnerLocationID = AIMGlobal.exportbatchAIMOwnerLocationID;
                    //header.CFCPlantID = AIMGlobal.exportbatchAIMOwnerLocationID;

                    //9/21/20
                    //header.AIMOwnerLocationID = headerEntity[i].AIMOwnerLocationID;
                    header.AIMOwnerLocationID = CFCAIM.Custom.Globals.Global.lineNumber;

                    header.CFCPlantID = headerEntity[i].CFCPlantID;

                    header.CFCUserID = _cfcUserID;
                    header.CreateDate = DateTime.Now;
                    //MW200908 header.ExportStartDate = Global.ExportStartDate;
                    //MW200908 header.ExportEndDate = Global.ExportEndDate;
                    header.ExportStartDate = CFCAIMCore.Global.ExportStartDate;
                    header.ExportEndDate = CFCAIMCore.Global.ExportEndDate;

                    header.ShortName = headerEntity[i].LotNumber;                                      

                    result = AIMManager.SaveIngredientHeader(ref header);
                    if (result.ErrorNumber != 0)
                    {                        
                        msg = "No se puede guardar el lote de exportación.";
                        msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                        msg = msg + "(Unable to save the export Batch.)";
                        MessageBox.Show(msg, "Crítico...", MessageBoxButtons.OK);

                        Close();
                        return;
                    }
                    headerID = result.RecordID;  //save header ID 

                    //----------------- DETAIL -------------------------------------------- 
                    UpdateIngredientDetail detail = new UpdateIngredientDetail();
                    ErrorInfo resultDetail = new ErrorInfo();
                    foreach (UpdateIngredientDetail item in detailEntity)
                    {
                        detail.UpdateIngredientDetailID = 0;
                        detail.UpdateIngredientHeaderID = headerID;
                        detail.CFCIngredientID = item.CFCIngredientID;
                        detail.CFCNutrientID = item.CFCNutrientID;
                        detail.ProposedValue = item.Used;
                        detail.CurrentNutrientVal = item.CurrentNutrientVal;
                        detail.CFCUserID = _cfcUserID;
                        detail.Override = item.Override;
                        detail.Status = (Int32)ExportBatchDetailStatusEnum.Accepted;
                        detail.CFCPlantID = item.CFCPlantID;
                        detail.NumberOfSamples = 0;
                        detail.IsOverlay = item.IsOverlay;                        
                        resultDetail = AIMManager.SaveIngredientDetail(ref detail);
                        if (resultDetail.ErrorNumber != 0)
                        {                            
                            msg = "No se puede guardar el detalle del lote de exportación.";
                            msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                            msg = "(Unable to save the export batch detail.)";
                            MessageBox.Show(msg, "Crítico...", MessageBoxButtons.OK);
                            Close();
                            return;
                        }
                    }
                }

                //MW200908 AIMGlobal.SplashScreenShow();
                //MW200908 Global.exportbatchmaintenanceHeaderID = headerID;
                //MW200908 Global.exportbatchmaintenanceHeaderName = header.UpdateName;
                //MW200908 AIMGlobal.SplashScreenShow();
                CFCAIMCore.Global.SplashScreenShow();
                //MW200908 Global.exportbatchmaintenanceHeaderID = headerID;
                //MW200908 Global.exportbatchmaintenanceHeaderName = header.UpdateName;
                CFCAIMCore.Global.exportbatchmaintenanceHeaderID = headerID;
                CFCAIMCore.Global.exportbatchmaintenanceHeaderName = header.UpdateName;
                //---------------------------------------------------------------
                // Insert record into ProgressBar SQL table				
                ProgressBarInfo progressBar = new ProgressBarInfo();
                progressBar.ProgressBarID = 0;
                progressBar.ProgressBarValue = 1;
                progressBar.ProgressBarDescription = "Sincronización programa corriendo";
                progressBar.CFCUserID = _cfcUserID;
                progressBar.ProgressBarType = (Int32)EnumProgressBarType.ExportToConcept5;
                _progressBarID = AIMManager.InsertProgressBar(ref progressBar).RecordID;

                //MW200908  AIMGlobal.SplashScreenShow();
                CFCAIMCore.Global.SplashScreenShow();
                pbExport.Visible = true;
                bgwTimer.RunWorkerAsync();

                //Call sync program --> sync is vb6 program; 
                //6 parameters (customer dbase, SQL connection, new aimDatabaseID, "U" for update, userID, ProgressBarID)				
                AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
                String arg = String.Empty;
                String separator = "~~";
                String syncType = "U";
                String customType = "VITAPRO";
                String sqlConn = String.Empty;
                String filePath = Application.StartupPath + @"\\CFC5AIM.exe";				
                sqlConn = "Provider=SQLOLEDB;" + CFCAIMCore.Data.AIMDataAccess._database.ConnectionString;
                sqlConn = sqlConn.Replace("Initial Catalog", "Database");
                aimDatabase = AIMManager.GetAIMDatabase(header.AIMDatabaseID);

                //arg = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}", separator, aimDatabase.CFCPath, sqlConn, aimDatabase.AIMDatabaseID, syncType, _cfcUserID, _progressBarID, AIMGlobal.exportbatchmaintenanceHeaderID);
                //MW200908 arg = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}", separator, aimDatabase.CFCPath, sqlConn, aimDatabase.AIMDatabaseID, syncType, _cfcUserID, _progressBarID, Global.exportbatchmaintenanceHeaderID, customType);
                arg = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}", separator, aimDatabase.CFCPath, sqlConn, aimDatabase.AIMDatabaseID, syncType, _cfcUserID, _progressBarID, CFCAIMCore.Global.exportbatchmaintenanceHeaderID, customType);

                if (File.Exists(filePath))
                {
                    try
                    {
                        ProcessStartInfo proc = new ProcessStartInfo();
                        proc.WindowStyle = ProcessWindowStyle.Hidden;
                        proc.CreateNoWindow = true;
                        proc.UseShellExecute = false;
                        proc.FileName = filePath;
                        proc.Arguments = arg;

                        try
                        {
                            Process.Start(proc);
                        }
                        catch (Exception ex)
                        {                            
                            msg = "Error al exportar el archivo";
                            msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                            msg = msg + "(" + ex.Message + " - Error exporting file.)";
                            MessageBox.Show(msg, "Advertencia...", MessageBoxButtons.OK);
                            //MW200908 AIMGlobal.SplashScreenHide();
                            CFCAIMCore.Global.SplashScreenHide();
                        }
                        

                        _exportSent = true;
                    }
                    catch (Exception ex)
                    {                        
                        msg = "Error al exportar el archivo";
                        msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                        msg = ex.Message + " - (Error exporting file.)";
                        MessageBox.Show(msg, "Advertencia...", MessageBoxButtons.OK);
                        //MW200908 AIMGlobal.SplashScreenHide();
                        CFCAIMCore.Global.SplashScreenHide();
                    }
                                              
                }
                else
                {
                    //MW200908 AIMGlobal.SplashScreenHide();
                    CFCAIMCore.Global.SplashScreenHide();
                    msg = "La base de datos de fórmulas no se encuentra. Sin exportar.";
                    msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    msg = msg + "(Formula Database is missing.  No export done.)";
                    MessageBox.Show(msg, "Advertencia...", MessageBoxButtons.OK);

                }

            }
            catch (Exception ex)
            {
                String mess = "";
                //MW200908 AIMGlobal.SplashScreenHide();
                CFCAIMCore.Global.SplashScreenHide();
                mess = ex + Environment.NewLine + "El archivo no fue exportado.";
                mess = mess + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                mess = mess + "(File was not exported.)";
                MessageBox.Show(mess, "Advertencia...");
            }
        }

        #region Timer

        private void bgwTimer_DoWork(Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Int32 count = 1;
                Process[] pname;
                String processName = "CFC5AIM";

                //9/9/20 - change to use AIMCoreGlobal
                //Global.importProgressBarCounter = 0;
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

        private void bgwTimer_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            String msg = "";

            //9/9/20 - change to use AIMCoreGlobal
            //Global.SplashScreenHide();
            CFCAIMCore.Global.SplashScreenHide();

            bgwTimer.Dispose();

            //9/9/20 - change to use AIMCoreGlobal
            //Global.importProgressBarCounter = 0;
            CFCAIMCore.Global.importProgressBarCounter = 0;

            pbExport.Visible = false;

            //Make sure status on UpdateIngredientHeader table has been changed to 2 (CFC5AIM updates the status to 2 when the program completes.)
            AnalysisExportBatchMaintenance exportBatch = new AnalysisExportBatchMaintenance();

            //9/9/20 - change to use AIMCoreGlobal
            //exportBatch = AIMManager.GetUpdateIngredientHeader(Global.exportbatchmaintenanceHeaderID);
            exportBatch = AIMManager.GetUpdateIngredientHeader(CFCAIMCore.Global.exportbatchmaintenanceHeaderID);

            if (exportBatch != null)
            {
                //////////Update LotStatus on Lot table to InProcess (5)
                ////////CustomManager.SaveLotStatus(Globals.sampleID, 5, _cfcUserID);

                //////////Update ProductionStatus on Lot table for Line#
                ////////CustomManager.SaveProductionLineStatus(Globals.sampleID, Globals.lineNumber, (Int32)EnumProductionStatus.InProcess, _cfcUserID);

                //change export status so Lot hyperlink color is normal                                      
                CustomManager.SaveExportStatus(CFCAIMCore.Global.sampleID, false);

                //9/2/20 - Set Concept5ExportStatus = true
                CustomManager.SaveConcept5ExportStatus(CFCAIMCore.Global.sampleID, true);
            }
                        
            msg = "Exportación de fórmula completada.";
            msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            msg = msg + "(Formula Export completed.)";
            MessageBox.Show(msg);

            Close();

            SampleProduction sampleProduction;
            AIMGlobal.open_form(sampleProduction = new SampleProduction());

        }

        #endregion

        private void grdviewExportBatchMaintenance_CustomDrawCell(Object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //MW200908 if (Global.exportbatchTargetID != (Int32)ExportTargetEnum.Base)
            if (CFCAIMCore.Global.exportbatchTargetID != (Int32)ExportTargetEnum.Base)
                {
                Boolean isComputed = false;
                Boolean isOverlay = false;
                String statusText = "";
                isOverlay = (Boolean)(grdviewExportBatchMaintenance.GetRowCellValue(e.RowHandle, colIsOverlay));
                isComputed = (Boolean)(grdviewExportBatchMaintenance.GetRowCellValue(e.RowHandle, colIsComputed));
                statusText = grdviewExportBatchMaintenance.GetRowCellValue(e.RowHandle, colStatusDisplay).ToString();
                if (e.Column == colStatusDisplay)
                {
                    if (isOverlay == false)
                    {
                        if (statusText != "")
                        {
                            e.Appearance.BackColor = Color.Yellow;
                        }
                    }
                    if (isComputed == true)
                    {
                        e.Appearance.BackColor = Color.LightSkyBlue;                        
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

        private void btnBatchDetailReport_Click(Object sender, EventArgs e)
        {
            //9/9/20 - change to use AIMCoreGlobal
            //AIMGlobal.SplashScreenShow();
            //AIMGlobal.SplashScreenShow();
            CFCAIMCore.Global.SplashScreenShow();
            CFCAIMCore.Global.SplashScreenShow();

            List<ExportBatchMaintenanceReportInfo> reportDataSet = new List<ExportBatchMaintenanceReportInfo>();
            for (int i = 0; i < CFCAIM.Custom.Globals.Global.exportDetail.Count; i++)
            {
                reportDataSet.Add(new ExportBatchMaintenanceReportInfo
                {
                    UpdateIngredientHeaderID = CFCAIM.Custom.Globals.Global.exportHeader[0].UpdateIngredientHeaderID,
                    UpdateName = CFCAIM.Custom.Globals.Global.exportHeader[0].LotNumber,
                    UpdateStamp = DateTime.Now,
                    AimDatabaseName = "",
                    TargetOverlay = "Default",
                    CreateDate = DateTime.Now,
                    CFCPlantName = CFCAIM.Custom.Globals.Global.exportDetail[i].CFCPlantName,
                    CFCIngredientID = CFCAIM.Custom.Globals.Global.exportDetail[i].CFCIngredientID,
                    CFCNutrientID = CFCAIM.Custom.Globals.Global.exportDetail[i].CFCNutrientID,
                    ProposedValue = CFCAIM.Custom.Globals.Global.exportDetail[i].ProposedValue,
                    Mean = 0,
                    StandardDeviation = 0,
                    Override = CFCAIM.Custom.Globals.Global.exportDetail[i].Override,
                    CFCIngredientName = CFCAIM.Custom.Globals.Global.exportDetail[i].CFCIngredientName,
                    CFCIngredientCode = CFCAIM.Custom.Globals.Global.exportDetail[i].CFCIngredientCode,
                    CFCNutrientCode = "",
                    CFCNutrientName = CFCAIM.Custom.Globals.Global.exportDetail[i].CFCNutrientName,
                    UsedValue = CFCAIM.Custom.Globals.Global.exportDetail[i].Used,                    
                    Status = CFCAIM.Custom.Globals.Global.exportDetail[i].Status,
                    CFCPlantCode = CFCAIM.Custom.Globals.Global.exportDetail[i].CFCPlantCode,
                    CurrentNutrientVal = CFCAIM.Custom.Globals.Global.exportDetail[i].CurrentNutrientVal,
                    //UserName = Global.CFCLoginUser.UserName,
                    PlantName = "Línea " + CFCAIM.Custom.Globals.Global.lineNumber.ToString()
                }
                );
            }
            
            CFCAIMCore.Global.DataSetExportBatchDetail = reportDataSet;

            //9/9/20 - change to use AIMCoreGlobal
            //AIMGlobal.SplashScreenHide();
            CFCAIMCore.Global.SplashScreenHide();

            //run report
            //Int32 reportID = (Int32)ReportEnum.ExportBatchDetail;
            //ReportFunctions.LoadReport(reportID);

            //9/9/20
            //CFCAIM.ReportDevExpress.ExportBatchDetail_Spanish report = new CFCAIM.ReportDevExpress.ExportBatchDetail_Spanish();
            ReportDevExpress.ExportBatchDetail_Spanish report = new ReportDevExpress.ExportBatchDetail_Spanish();            
            
            report.DataSource = reportDataSet;
            ReportPrintTool tool = new ReportPrintTool(report);

            //9/9/20 - change to use AIMCoreGlobal
            //AIMGlobal.SplashScreenHide();
            CFCAIMCore.Global.SplashScreenHide();

            tool.ShowPreviewDialog();
            
            this.Show();
        }
    }
}
