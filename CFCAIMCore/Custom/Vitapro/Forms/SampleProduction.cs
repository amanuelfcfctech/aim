
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using Core.Library.DevExpress;
using System.Drawing;
using CFCAIMCore;
using CFCAIMCore.Manager;
using System.Linq;

//9/16/20
//using CFCAIMCustom.Entity.Vitapro;
//using CFCAIMCustom.Manager;
using CFCAIM.Custom.Vitapro.Entity;
using CFCAIM.Custom.Vitapro.Manager;
using CFCAIM.Custom.Vitapro.Forms;

//9/16/20
//namespace CFCAIMCustom.Vitapro
namespace CFCAIM

{
    public partial class SampleProduction : Form
    {
               
        tpProdSpec sample;

        Int32 _productionStatus = (Int32)EnumProductionStatus.All;
        Int32 _cfcUserID = AIMCoreGlobal.CFCUserID;
        Int32 _headerID = 0;
        Int32 _days = 0;
        Int32 _aimNutrientID1 = 0;

        Int32 _aimNutrientID2 = 0;      //9/27/19
        Int32 _aimNutrientID3 = 0;      //9/27/19
        Int32 _aimNutrientID4 = 0;      //9/27/19
        String _aimNutrientName1 = "";  //9/27/19
        String _aimNutrientName2 = "";  //9/27/19
        String _aimNutrientName3 = "";  //9/27/19
        String _aimNutrientName4 = "";  //9/27/19

        Boolean _export = false;
        Int32 _detailID = 1;            //8/28/19

        List<ReportAlternateNamesInfo> _names = new List<ReportAlternateNamesInfo>();           //9/30/19

        public SampleProduction()
        {
            InitializeComponent();
        }

        
        private void SampleProduction_Load(Object sender, EventArgs e)
        {            
            ShowYellowHelpText();
            GetAIMNutrients();          //8/26/19
            DataBind();
            ShowHideNutrientColumns();  //9/30/19 - originally had this in the Databind, but the columns would move around after update, so pulled code and put it in its own function.  Called once.
            GetAlternateColumnNames();  //9/30/19 - moved from Databind - for same reason as above.
            HideInactiveLines();        //9/30/19 - moved from Databind - for same reason as above.
            DataBindLineLue();            
            CheckSystemSettings();
            SetStatus();                //8/27/19
            SetToolTips();
            btnCurrent.BackColor = SystemColors.GradientInactiveCaption;
            btnHistorical.BackColor = SystemColors.Control;

            //9/2/20
            SetStatusRadioButtonChoice();
            SetConcept5StatusButtonChoice();


            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void DataBind()
        {

            //---------------------------------------------------------------------------------------------
            //SET LOT DATASOURCE
            List<LotInfo> lot = new List<LotInfo>();
            if (_productionStatus == (Int32)EnumReceivingStatus.All)
            {
                lot = CustomManager.GetLotProduction(_days, _aimNutrientID1, _aimNutrientID2, _aimNutrientID3, _aimNutrientID4);
            }
            else
            {
                lot = CustomManager.GetLotProduction(_productionStatus, _days, _aimNutrientID1, _aimNutrientID2, _aimNutrientID3, _aimNutrientID4);
            }
            Int32 currentRowNum = 0;
            if (grdSampleProductionView != null)
            {
                Int32.TryParse(grdSampleProductionView.FocusedRowHandle.ToString(), out currentRowNum);
            }
            grdSampleProduction.DataSource = lot;
            grdSampleProductionView.FocusedRowHandle = currentRowNum;           
                       
            
                       

            /////  OLD DATABIND CODE  //////
            ////////////////////////////////////////---------------------------------------------------------------------------------------------
            ////////////////////////////////////////SET LOT DATASOURCE
            //////////////////////////////////////List<LotInfo> lot = new List<LotInfo>();
            //////////////////////////////////////if (_productionStatus == (Int32)EnumReceivingStatus.All)
            //////////////////////////////////////{
            //////////////////////////////////////    lot = CustomManager.GetLotProduction(_days, _aimNutrientID1, _aimNutrientID2, _aimNutrientID3, _aimNutrientID4);
            //////////////////////////////////////}
            //////////////////////////////////////else
            //////////////////////////////////////{
            //////////////////////////////////////    lot = CustomManager.GetLotProduction(_productionStatus, _days, _aimNutrientID1, _aimNutrientID2, _aimNutrientID3, _aimNutrientID4);
            //////////////////////////////////////}
            //////////////////////////////////////Int32 currentRowNum = 0;
            //////////////////////////////////////if (grdSampleProductionView != null)
            //////////////////////////////////////{
            //////////////////////////////////////    Int32.TryParse(grdSampleProductionView.FocusedRowHandle.ToString(), out currentRowNum);
            //////////////////////////////////////}
            //////////////////////////////////////grdSampleProduction.DataSource = lot;
            //////////////////////////////////////grdSampleProductionView.FocusedRowHandle = currentRowNum;

            ////////////////////////////////////////---------------------------------------------------------------------------------------------
            ////////////////////////////////////////SHOW OR HIDE NUTRIENT COLUMNS            
            ////////////////////////////////////////9/27/19 - added 3 more nutrient columns to grid, and renamed 1, 2, 3 and 4
            //////////////////////////////////////grdSampleProductionView.Columns["AIMNutrientAmount1"].Visible = false;
            //////////////////////////////////////grdSampleProductionView.Columns["AIMNutrientAmount2"].Visible = false;
            //////////////////////////////////////grdSampleProductionView.Columns["AIMNutrientAmount3"].Visible = false;
            //////////////////////////////////////grdSampleProductionView.Columns["AIMNutrientAmount4"].Visible = false;
            //////////////////////////////////////if (_aimNutrientName1 != null && _aimNutrientName1 != "")
            //////////////////////////////////////{
            //////////////////////////////////////    grdSampleProductionView.Columns["AIMNutrientAmount1"].Caption = _aimNutrientName1;
            //////////////////////////////////////    grdSampleProductionView.Columns["AIMNutrientAmount1"].Visible = true;
            //////////////////////////////////////}
            //////////////////////////////////////if (_aimNutrientName2 != null && _aimNutrientName2 != "")
            //////////////////////////////////////{
            //////////////////////////////////////    grdSampleProductionView.Columns["AIMNutrientAmount2"].Caption = _aimNutrientName2;
            //////////////////////////////////////    grdSampleProductionView.Columns["AIMNutrientAmount2"].Visible = true;
            //////////////////////////////////////}
            //////////////////////////////////////if (_aimNutrientName3 != null && _aimNutrientName3 != "")
            //////////////////////////////////////{
            //////////////////////////////////////    grdSampleProductionView.Columns["AIMNutrientAmount3"].Caption = _aimNutrientName3;
            //////////////////////////////////////    grdSampleProductionView.Columns["AIMNutrientAmount3"].Visible = true;
            //////////////////////////////////////}
            //////////////////////////////////////if (_aimNutrientName4 != null && _aimNutrientName4 != "")
            //////////////////////////////////////{
            //////////////////////////////////////    grdSampleProductionView.Columns["AIMNutrientAmount4"].Caption = _aimNutrientName4;
            //////////////////////////////////////    grdSampleProductionView.Columns["AIMNutrientAmount4"].Visible = true;
            //////////////////////////////////////}

            ////////////////////////////////////////---------------------------------------------------------------------------------------------
            ////////////////////////////////////////GET ALTERNATE COLUMN NAMES -- currently Vitapro is using only Lot# and BOL fields from sample            
            //////////////////////////////////////_names = AIMManager.GetReportAlternateNames();
            //////////////////////////////////////foreach (var item in _names)
            //////////////////////////////////////{
            //////////////////////////////////////    if (item.ControlName == "lblBOL")
            //////////////////////////////////////    {
            //////////////////////////////////////        grdSampleProductionView.Columns["BillOfLoad"].Caption = item.AlternateName;
            //////////////////////////////////////    }
            //////////////////////////////////////    if (item.ControlName == "lblLotNumber")
            //////////////////////////////////////    {
            //////////////////////////////////////        grdSampleProductionView.Columns["LotNumber"].Caption = item.AlternateName;
            //////////////////////////////////////    }
            //////////////////////////////////////}

            ////////////////////////////////////////---------------------------------------------------------------------------------------------            
            ////////////////////////////////////////LOOP THRU XREF TABLE AND HIDE ANY INACTIVE LINES
            ////////////////////////////////////////5/10/19
            ////////////////////////////////////////Lines 3 and 4 were added to the grid and to the LineRefCFCPlant table. We want to keep them available but not showing on grid yet.
            ////////////////////////////////////////The status for Lines 3 and 4 and the xref table is set to inactive.
            ////////////////////////////////////////-----------------------------------------------------------------------------
            ////////////////////////////////////////* * * the LineID on the LineRefCFCPlant table MUST MATCH the CFCPlantID * * *
            ////////////////////////////////////////-----------------------------------------------------------------------------
            //////////////////////////////////////List<LineRefCFCPlantInfo> line = new List<LineRefCFCPlantInfo>();
            //////////////////////////////////////line = CustomManager.GetLineRefCFCPlant();
            //////////////////////////////////////foreach (var item in line)
            //////////////////////////////////////{
            //////////////////////////////////////    if (item.IsActive == false)
            //////////////////////////////////////    {
            //////////////////////////////////////        grdSampleProductionView.Columns["ProductionStatusLine" + item.LineID].Visible = false;
            //////////////////////////////////////        grdSampleProductionView.Columns["UnboundStatus" + item.LineID].Visible = false;

            //////////////////////////////////////    }
            //////////////////////////////////////    else
            //////////////////////////////////////    {
            //////////////////////////////////////        grdSampleProductionView.Columns["ProductionStatusLine" + item.LineID].Visible = true;
            //////////////////////////////////////        grdSampleProductionView.Columns["UnboundStatus" + item.LineID].Visible = true;

            //////////////////////////////////////        //9/27/19
            //////////////////////////////////////        //grdSampleProductionView.Columns["ProductionStatusLine" + item.LineID].Caption = item.LineID.ToString();
            //////////////////////////////////////        //If user doesn't enter anything for the line name, use the Line Number as the name on the grid.
            //////////////////////////////////////        if (item.LineName.Length > 0)
            //////////////////////////////////////        {
            //////////////////////////////////////            grdSampleProductionView.Columns["ProductionStatusLine" + item.LineID].Caption = item.LineName;
            //////////////////////////////////////        }
            //////////////////////////////////////        else
            //////////////////////////////////////        {
            //////////////////////////////////////            grdSampleProductionView.Columns["ProductionStatusLine" + item.LineID].Caption = item.LineID.ToString();
            //////////////////////////////////////        }
            //////////////////////////////////////    }
            //////////////////////////////////////}

            ////////////////////////////////////////---------------------------------------------------------------------------------------------
            ////////////////////////////////////////LOOP THRU DATASOURCE SETTING AMOUNTAVAILABLE            
            //////////////////////////////////////List<LotInfo> amtAvail = new List<LotInfo>();
            //////////////////////////////////////amtAvail = (List<LotInfo>)grdSampleProductionView.DataSource;
            //////////////////////////////////////foreach (var item in amtAvail)
            //////////////////////////////////////{
            //////////////////////////////////////    item.AmtAvailable = item.AmountAvailable.ToString();
            //////////////////////////////////////}


            
        }

        private void DataBindLineLue()
        {            
            List<ProductionStatusInfo> list = new List<ProductionStatusInfo>();
            list = CustomManager.GetProductionStatus();
            replueLine1.DataSource = list;
            replueLine2.DataSource = list;
            replueLine3.DataSource = list;           
            XtraEditors.LoadGridLookUpEdit(ref replueLine1, "ProductionStatusID", "ProductionStatusName");
            XtraEditors.LoadGridLookUpEdit(ref replueLine2, "ProductionStatusID", "ProductionStatusName");
            XtraEditors.LoadGridLookUpEdit(ref replueLine3, "ProductionStatusID", "ProductionStatusName");
            replueLine1.Columns["ProductionStatusName"].Caption = "";
            replueLine2.Columns["ProductionStatusName"].Caption = "";
            replueLine3.Columns["ProductionStatusName"].Caption = "";
            
            //6/4/19 - commented
            //replueLine4.DataSource = list;
            //XtraEditors.LoadGridLookUpEdit(ref replueLine4, "ProductionStatusID", "ProductionStatusName");
            //replueLine4.Properties.Columns["ProductionStatusName"].Caption = "";

        }

        private void HideInactiveLines()
        {
            //---------------------------------------------------------------------------------------------            
            //LOOP THRU XREF TABLE AND HIDE ANY INACTIVE LINES
            //5/10/19
            //Lines 3 and 4 were added to the grid and to the LineRefCFCPlant table. We want to keep them available but not showing on grid yet.
            //The status for Lines 3 and 4 and the xref table is set to inactive.
            //-----------------------------------------------------------------------------
            //* * * the LineID on the LineRefCFCPlant table MUST MATCH the CFCPlantID * * *
            //-----------------------------------------------------------------------------
            List<LineRefCFCPlantInfo> line = new List<LineRefCFCPlantInfo>();
            line = CustomManager.GetLineRefCFCPlant();
            foreach (var item in line)
            {
                if (item.IsActive == false)
                {
                    grdSampleProductionView.Columns["ProductionStatusLine" + item.LineID].Visible = false;
                    grdSampleProductionView.Columns["UnboundStatus" + item.LineID].Visible = false;

                }
                else
                {
                    grdSampleProductionView.Columns["ProductionStatusLine" + item.LineID].Visible = true;
                    grdSampleProductionView.Columns["UnboundStatus" + item.LineID].Visible = true;

                    //9/27/19
                    //grdSampleProductionView.Columns["ProductionStatusLine" + item.LineID].Caption = item.LineID.ToString();
                    //If user doesn't enter anything for the line name, use the Line Number as the name on the grid.
                    if (item.LineName == null)
                    {
                        grdSampleProductionView.Columns["ProductionStatusLine" + item.LineID].Caption = item.LineID.ToString();
                    }
                    else if (item.LineName.Length > 0)
                    {
                        grdSampleProductionView.Columns["ProductionStatusLine" + item.LineID].Caption = item.LineName;
                    }
                    else
                    {
                        grdSampleProductionView.Columns["ProductionStatusLine" + item.LineID].Caption = item.LineID.ToString();
                    }
                }
            }

        }

        private void SetConcept5StatusButtonChoice()
        {
            //9/2/20
            if (CFCAIM.Custom.Globals.Global.checkedConcept5Status != String.Empty)
            {
                switch (CFCAIM.Custom.Globals.Global.checkedButton)
                {
                    case "rbAllRecs":
                        rbAllRecs.Checked = true;
                        break;
                    case "rbExportedToConcept5":
                        rbExportedToConcept5.Checked = true;
                        break;
                    case "rbNotExportedToConcept5":
                        rbNotExportedToConcept5.Checked = true;
                        break;                    
                }
            }
            else
            {
                rbAll.Checked = true;

            }
        }
        private void SetStatusRadioButtonChoice()
        {
            //9/2/20
            if (CFCAIM.Custom.Globals.Global.checkedButton != String.Empty)
            {
                switch (CFCAIM.Custom.Globals.Global.checkedButton)
                {
                    case "rbAll":
                        rbAll.Checked = true;
                        break;
                    case "rbAvailable":
                        rbAvailable.Checked = true;
                        break;
                    case "rbCurrent":
                        rbCurrent.Checked = true;
                        break;
                    case "rbDepleted":
                        rbDepleted.Checked = true;
                        break;
                }
            }
            else
            {
                rbAll.Checked = true;

            }
        }

        private void GetAlternateColumnNames()
        {
            //---------------------------------------------------------------------------------------------
            //GET ALTERNATE COLUMN NAMES -- currently Vitapro is using only Lot# and BOL fields from sample            
            _names = AIMManager.GetReportAlternateNames();
            foreach (var item in _names)
            {
                if (item.ControlName == "lblBOL")
                {
                    grdSampleProductionView.Columns["BillOfLoad"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblLotNumber")
                {
                    grdSampleProductionView.Columns["LotNumber"].Caption = item.AlternateName;
                }
            }
        }

        private void ShowHideNutrientColumns()
        {
            //---------------------------------------------------------------------------------------------
            //SHOW OR HIDE NUTRIENT COLUMNS            
            //9/27/19 - added 3 more nutrient columns to grid, and renamed 1, 2, 3 and 4
            grdSampleProductionView.Columns["AIMNutrientAmount1"].Visible = false;
            grdSampleProductionView.Columns["AIMNutrientAmount2"].Visible = false;
            grdSampleProductionView.Columns["AIMNutrientAmount3"].Visible = false;
            grdSampleProductionView.Columns["AIMNutrientAmount4"].Visible = false;
            if (_aimNutrientName1 != null && _aimNutrientName1 != "")
            {
                grdSampleProductionView.Columns["AIMNutrientAmount1"].Caption = _aimNutrientName1;
                grdSampleProductionView.Columns["AIMNutrientAmount1"].Visible = true;
            }
            if (_aimNutrientName2 != null && _aimNutrientName2 != "")
            {
                grdSampleProductionView.Columns["AIMNutrientAmount2"].Caption = _aimNutrientName2;
                grdSampleProductionView.Columns["AIMNutrientAmount2"].Visible = true;
            }
            if (_aimNutrientName3 != null && _aimNutrientName3 != "")
            {
                grdSampleProductionView.Columns["AIMNutrientAmount3"].Caption = _aimNutrientName3;
                grdSampleProductionView.Columns["AIMNutrientAmount3"].Visible = true;
            }
            if (_aimNutrientName4 != null && _aimNutrientName4 != "")
            {
                grdSampleProductionView.Columns["AIMNutrientAmount4"].Caption = _aimNutrientName4;
                grdSampleProductionView.Columns["AIMNutrientAmount4"].Visible = true;
            }
        }

        private void CheckSystemSettings()
        {
            //Check SystemSetting to get number of days to show lots on the screen (based on date depleted)
            SystemSettingsInfo systemSettings = new SystemSettingsInfo();
            systemSettings = AIMManager.GetSystemSettings();
            if (systemSettings != null)
            {
                _days = systemSettings.SampleNumberOfDays;
                if (_days == 0)
                {
                    _days = 60;
                }                
                txtDays.Text = _days.ToString() + " días";
            }

        }
        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        //8/26/19
        private void GetAIMNutrients()
        {
            String msg = "";
            //Check system setting for nutrients to diplay. If not set up, pop msg            
            {
                SystemSettingsCustomInfo settings = new SystemSettingsCustomInfo();
                settings = CustomManager.GetSystemSettingsCustom();
                if (settings == null || (settings.AIMNutrientID1_ForDisplay == 0 && settings.AIMNutrientID2_ForDisplay == 0 && settings.AIMNutrientID3_ForDisplay == 0 && settings.AIMNutrientID4_ForDisplay == 0))
                {
                    msg = "No se han asignado códigos de nutrientes..";
                    msg += Environment.NewLine + Environment.NewLine + "Asigne los códigos en Mantenimiento> Configuración.";
                    msg += Environment.NewLine + Environment.NewLine;
                    msg += Environment.NewLine + Environment.NewLine + "Nutrient codes have not been assigned.";
                    msg += Environment.NewLine + Environment.NewLine + "Assign the codes under Maintenance > Settings.";
                    MessageBox.Show(msg, "Warning...");
                }
                else
                {
                    _aimNutrientID1 = settings.AIMNutrientID1_ForDisplay;
                    _aimNutrientID2 = settings.AIMNutrientID2_ForDisplay;
                    _aimNutrientID3 = settings.AIMNutrientID3_ForDisplay;
                    _aimNutrientID4 = settings.AIMNutrientID4_ForDisplay;

                    _aimNutrientName1 = settings.AIMNutrientName1;
                    _aimNutrientName2 = settings.AIMNutrientName2;
                    _aimNutrientName3 = settings.AIMNutrientName3;
                    _aimNutrientName4 = settings.AIMNutrientName4;
                }
            }

        }

        private void DataBindLineLueForDepleted()
        {        
            List<ProductionStatusInfo> list = new List<ProductionStatusInfo>();
            list = CustomManager.GetProductionStatusForDepleted();
            replueLine1.DataSource = list;
            replueLine2.DataSource = list;
            replueLine3.DataSource = list;            
            XtraEditors.LoadGridLookUpEdit(ref replueLine1, "ProductionStatusID", "ProductionStatusName");
            XtraEditors.LoadGridLookUpEdit(ref replueLine2, "ProductionStatusID", "ProductionStatusName");
            XtraEditors.LoadGridLookUpEdit(ref replueLine3, "ProductionStatusID", "ProductionStatusName");            
            replueLine1.Columns["ProductionStatusName"].Caption = "";
            replueLine2.Columns["ProductionStatusName"].Caption = "";
            replueLine3.Columns["ProductionStatusName"].Caption = "";

            //6/4/19 - commented
            //replueLine4.DataSource = list;
            //XtraEditors.LoadGridLookUpEdit(ref replueLine4, "ProductionStatusID", "ProductionStatusName");
            //replueLine4.Properties.Columns["ProductionStatusName"].Caption = "";
        }
               
        private void SetToolTips()
        {
            try
            {
               
                ToolTip ttip = new ToolTip();
                ttip.SetToolTip(btnExit, "Salida");
                ttip.SetToolTip(btnRefresh, "Refrescar");
                ttip.SetToolTip(btnResize, "Volver al tamaño normal de la pantalla");
                ttip.SetToolTip(btnPreviewGrid, "Vista previa de cuadricula");
                ttip.SetToolTip(btnAcceptAll, "Aceptar todo");                
                ttip.SetToolTip(btnExport, "Exportar");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SampleProduction_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
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

        private void btnPreviewGrid_Click(Object sender, EventArgs e)
        {
            try
            {
                //MW200908   Global.previewGridResults(grdSampleProduction, "Lotes Disponibles");
                CFCAIMCore.Global.previewGridResults(grdSampleProduction, "Lotes Disponibles");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void grdSampleProductionView_ValidateRow(Object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

            // ** DON'T PUT ANYTHING HERE! MESSES UP THE REPOSITORY DROPDOWNS LOGIC (CHECKSTATUS FUNCTION) **
                        
            //9/27/19 - will this be ok?? -- in order to avoid messing up repository dropdown logic??
            CheckStatus(sender, 99);

            //9/27/19 - if lot number is changed, update lot# on Sample table
            LotInfo lot = new LotInfo();
            lot = (LotInfo)grdSampleProductionView.GetFocusedRow();
            ErrorInfo outPut = AIMManager.SaveSampleBillOfLoad(lot.SampleID, lot.BillOfLoad, _cfcUserID);
            
        }

        private void CheckCellColors(DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs cell, DevExpress.XtraGrid.Columns.GridColumn colName)
        {
            if (Convert.ToInt32(grdSampleProductionView.GetRowCellValue(cell.RowHandle, colName)) == (Int32)EnumProductionStatus.Current)
            {
                cell.Appearance.BackColor = Color.LimeGreen;
                cell.Appearance.ForeColor = Color.White;            
            }
            if (Convert.ToInt32(grdSampleProductionView.GetRowCellValue(cell.RowHandle, colName)) == (Int32)EnumProductionStatus.Depleted)
            {
                cell.Appearance.BackColor = Color.Red;
                cell.Appearance.ForeColor = Color.White;                
            }
            if (Convert.ToInt32(grdSampleProductionView.GetRowCellValue(cell.RowHandle, colName)) == (Int32)EnumProductionStatus.Available)
            {
                cell.Appearance.BackColor = Color.White;
                cell.Appearance.ForeColor = Color.Black;                
            }

            //if (Convert.ToInt32(grdSampleProductionView.GetRowCellValue(cell.RowHandle, colName)) == (Int32)EnumProductionStatus.InProcess)
            //{
            //    cell.Appearance.BackColor = SystemColors.GradientInactiveCaption;
            //    cell.Appearance.ForeColor = Color.Black;
            //}


        }

        private void CheckCellDots(DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs cell, DevExpress.XtraGrid.Columns.GridColumn colName)
        {
            if (Convert.ToInt32(grdSampleProductionView.GetRowCellValue((Int32)cell.Row, colName)) == (Int32)EnumProductionStatus.Current)
            {               
                cell.Column.AppearanceCell.BackColor = Color.White;
                cell.Column.AppearanceCell.ForeColor = Color.Black;
            }
            else
            {
                cell.Column.AppearanceCell.BackColor = Color.White;
                cell.Column.AppearanceCell.ForeColor = Color.White;
            }
        }
        private void ShowYellowHelpText()
        {            
            txtYellowBox.Visible = false;
            lblYellowBox.Visible = false;
            List<LotInfo> lot = new List<LotInfo>();
            lot = CustomManager.GetLotExportStatus(true);
            if (lot.Count > 0)
            {
                txtYellowBox.Visible = true;
                lblYellowBox.Visible = true;
            }

        }

        private void grdSampleProductionView_CustomDrawCell(Object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            Decimal amtAvailable = 0;

            if (e.Column == colLotNumber)
            {
                //------------------------------------------------------------------------------
                //This "export status" shows that the user clicked the export arrow button, but chose not to export to Concept 5
                if ((Boolean)grdSampleProductionView.GetRowCellValue(e.RowHandle, "ExportStatus") == true)
                {
                    e.Appearance.BackColor = Color.Yellow;
                }               
                else
                {
                    e.Appearance.BackColor = Color.WhiteSmoke;                    
                }

                //------------------------------------------------------------------------------
                //9/2/20 - if record has been exported to Concept5, highlight lot# column in blue
                if ((Boolean)grdSampleProductionView.GetRowCellValue(e.RowHandle, "Concept5ExportStatus") == true)
                {
                    e.Appearance.BackColor = Color.LightSkyBlue;
                    e.Appearance.ForeColor = Color.White;                        
                }                     
                            
            }

            //------------------------------------------------------------------------------
            // okay, this has nothing to do with columns moving
            if (e.Column == colProductionStatusLine1)
            {
                CheckCellColors(e, colProductionStatusLine1);
            }
            if (e.Column == colProductionStatusLine2)
            {
                CheckCellColors(e, colProductionStatusLine2);
            }
            if (e.Column == colProductionStatusLine3)
            {
                CheckCellColors(e, colProductionStatusLine3);
            }

            //------------------------------------------------------------------------------
            if (e.Column == colAmountAvailable)
            {
                e.Appearance.ForeColor = Color.Black;
                decimal n;
                bool isNumeric = decimal.TryParse(grdSampleProductionView.GetRowCellValue(e.RowHandle, colAmountAvailable).ToString(), out n);
                if (isNumeric == true)
                {
                    amtAvailable = Convert.ToDecimal(grdSampleProductionView.GetRowCellValue(e.RowHandle, colAmountAvailable));
                }
                else
                {
                    amtAvailable = 0;
                }              

                if (amtAvailable == 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }                
            }
                      
        }

        
        private void repHyperLinkLot_Click(Object sender, EventArgs e)
        {
            //open sample
            try
            {
                try
                {

                    //MW200908 AIMGlobal.SplashScreenShow();
                    CFCAIMCore.Global.SplashScreenShow();

                    Int32 sampleID;
                    Int32.TryParse(grdSampleProductionView.GetFocusedRowCellValue(colSampleID).ToString(), out sampleID);

                    //MW200908 AIMGlobal.sampleID = sampleID;
                    CFCAIMCore.Global.sampleID = sampleID;
                                        
                    if (sample == null || sample.Visible == false)
                    {
                        sample = new tpProdSpec();
                        sample.MdiParent = this.MdiParent;

                        //10/6/21 - commented, not needed
                        //sample.WindowState = FormWindowState.Normal;                   
                        sample.Show();
                    }
                    else
                    {
                        //10/6/21 - changed for resize problem
                        //sample.WindowState = FormWindowState.Normal; 
                        sample.WindowState = FormWindowState.Maximized;

                        sample.Activate();
                    }


                    //MW200908 AIMGlobal.SplashScreenHide();
                    CFCAIMCore.Global.SplashScreenHide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void OpenExportForm(Object sender, Int32 lineNumber, Int32 status)
        {

            //9/2/20 - save status radio button - so the same button is clicked when returning from Export screen.
            var checkedButton = pnlActiveResults.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            CFCAIM.Custom.Globals.Global.checkedButton = checkedButton.Controls.Owner.Name;

            //9/2/20 - save Concept5 export radio button - so the same button is clicked when returning from Export screen.
            var checkedConcept5Status = pnlConcept5ExportStatus.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            CFCAIM.Custom.Globals.Global.checkedConcept5Status = checkedConcept5Status.Controls.Owner.Name;

            LotInfo lot = new LotInfo();
            lot = (LotInfo)grdSampleProductionView.GetFocusedRow();
            
            //status = (Int32)((DevExpress.XtraEditors.LookUpEdit)(sender)).Properties.KeyValue;
            if (status == (Int32)EnumProductionStatus.Current)
            {
                try
                {                    
                    _headerID++;
                    CFCAIM.Custom.Globals.Global.validationSuccess = false;
                    PopulateExportEntities(_headerID, lot.SampleID, lot.LotNumber, lineNumber, false);                    
                    if (CFCAIM.Custom.Globals.Global.validationSuccess == true)

                    {
                        Close();

                        //Global.sampleID = lot.SampleID;
                        CFCAIMCore.Global.sampleID = lot.SampleID;
                        
                        CFCAIM.Custom.Globals.Global.lineNumber = lineNumber;
                        CFCAIM.Custom.Globals.Global.blnMassExport = false;
                        Export exportForm;
                        AIMGlobal.open_form(exportForm = new Export());                       

                    }
                    else
                    {                                                
                        //change this lot back to available.
                        CustomManager.SaveProductionLineStatus(lot.SampleID, lineNumber, (Int32)EnumProductionStatus.Available, _cfcUserID);
                        DataBind(); // need DataBind for background color change
                    }
                        
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        
        private void PopulateExportEntities(Int32 headerID, Int32 sampleID, String lotNumber, Int32 lineNumber, Boolean massExport)
        {
            try
            {

                //Int32 detailID = 1;
                if (massExport == false)
                {
                    _detailID = 1;
                }

                Int32 cfcPlantID = 0;
                String cfcPlantCode = "";
                String msg = "";
                                
                //----------------- VALIDATE ------------------------------------------
                //11/1/18 - added stp to validate cross-references first (before trying to do export below)
                ErrorInfo validate = new ErrorInfo();
                validate = CustomManager.ValidateExportDetail(sampleID);
                if (validate.ErrorNumber != 0)
                {
                    return;
                }

                //9/16/20
                //Global.validationSuccess = true;
                CFCAIM.Custom.Globals.Global.validationSuccess = true;

                //--------------------------------------------------------------------- 	               
                List<LineRefCFCPlantInfo> xref = new List<LineRefCFCPlantInfo>();
                xref = CustomManager.GetLineRefCFCPlant(lineNumber);
                if (xref.Count != 0)
                {
                    cfcPlantID = xref[0].CFCPlantID;
                    cfcPlantCode = xref[0].CFCPlantCode;
                }
                else
                {                    
                    msg = "Faltan referencias cruzadas de la planta de formulación para la línea " + lineNumber.ToString() + ". Contacto IT.";
                    msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    msg = msg + "(Formulation Plant cross-reference missing for Line " + lineNumber.ToString() + ".)";
                    MessageBox.Show(msg, "Advertencia...", MessageBoxButtons.OK);

                    return;
                    
                }

                //----------------- HEADER -------------------------------------------- 	
                //MW200908 AIMGlobal.SplashScreenHide();
                CFCAIMCore.Global.SplashScreenHide();

                //8/27/19 -- if massExport = true, these 2 globals are cleared under the MassExport button
                if (massExport == false)
                {
                    CFCAIM.Custom.Globals.Global.exportHeader.Clear();
                    CFCAIM.Custom.Globals.Global.exportDetail.Clear();
                }


                CFCAIM.Custom.Globals.Global.exportHeader.Add(new AnalysisExportBatchMaintenance
                {
                    UpdateIngredientHeaderID = headerID,
                    UpdateName = sampleID + "_" + DateTime.Now.ToString("yyyyMMdd HH:mm"),
                    Status = 1,

                    //9/9/20 - change to use AIMCoreGlobal
                    //AIMDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID,
                    AIMDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID,

                    ExportTargetID = (Int32)ExportTargetEnum.DefaultOverlay,
                    AIMOwnerLocationID = cfcPlantID,
                    CFCPlantID = cfcPlantID,
                    CFCUserID = _cfcUserID,
                    CreateDate = DateTime.Now,

                    //9/9/20 - change to use AIMCoreGlobal
                    //ExportStartDate = AIMGlobals.ExportStartDate,
                    //ExportEndDate = AIMGlobals.ExportEndDate,
                    ExportStartDate = CFCAIMCore.Global.ExportStartDate,
                    ExportEndDate = CFCAIMCore.Global.ExportEndDate,

                    LotNumber = lotNumber,
                    ShortName = lotNumber
                }
                );                
                	
                //----------------- DETAIL -------------------------------------------- 	                                
                List<ExportInfo> export = new List<ExportInfo>();                               

                //9/3/20                
                //export = CustomManager.GetExportDetail(sampleID, cfcPlantID);
                export = CustomManager.GetExportDetail(sampleID, lineNumber);

                foreach (var item in export)
                {
                    Boolean isComputed = false;            
                    Int32 cfcNutCode = 0;
                    Int32.TryParse(item.CFCNutrientCode.ToString(), out cfcNutCode);

                    Int32 status = (Int32)ExportBatchDetailStatusEnum.Unassigned;                                       
                    List<CFCNutrientInfo> cfcNutrient = new List<CFCNutrientInfo>();
                    cfcNutrient = AIMManager.GetCFCNutrientID(item.CFCNutrientID);
                    if (cfcNutrient.Count > 0)
                    {
                        if (cfcNutrient[0].IsComputed == true)
                        {
                            status = (Int32)ExportBatchDetailStatusEnum.Computed;
                            isComputed = true;
                        }
                    }

                    CFCAIM.Custom.Globals.Global.exportDetail.Add(new UpdateIngredientDetail
                    {                        
                        UpdateIngredientDetailID = _detailID,
                        UpdateIngredientHeaderID = _headerID,
                        CFCIngredientID = item.CFCIngredientID,
                        CFCIngredientCode = item.CFCIngredientCode,
                        CFCIngredientName = item.CFCIngredientName,
                        CFCNutrientID = item.CFCNutrientID,
                        CFCNutrientCode = cfcNutCode,
                        CFCNutrientName = item.CFCNutrientName,
                        ProposedValue = item.AIMUsedValue, 
                        CurrentNutrientVal = item.CurrentNutrientVal,
                        Override = 0,
                        CFCUserID = _cfcUserID,                        
                        Status = status,
                        CFCPlantID = cfcPlantID, 
                        CFCPlantCode = cfcPlantCode,
                        NumberOfSamples = 1,
                        IsOverlay = item.IsOverlay,
                        LotNumber = item.LotNumber,
                        IsComputed = isComputed

                    }                    
                    );
                    //detailID++;
                    _detailID++;
                }
                //MW200908 AIMGlobal.SplashScreenHide();
                CFCAIMCore.Global.SplashScreenHide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MW200908 AIMGlobal.SplashScreenHide();
                CFCAIMCore.Global.SplashScreenHide();
            }
        }
        
        private static String CheckLineXRefCFCPlant(Int32 LineNumber)
        {

            String msg = "";

            //Make sure line is cross-referenced to CFCPlant
            String rtn = "";
            List<LineRefCFCPlantInfo> xref = new List<LineRefCFCPlantInfo>();
            xref = CustomManager.GetLineRefCFCPlant(LineNumber);
            if (xref.Count == 0)
            {                
                msg = "La referencia cruzada entre el número de línea y la planta de formulación debe configurarse antes de continuar";
                msg += msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                msg += msg + "(Cross - Reference between Line Number and Formulation Plant must be set up before continuing)";
                MessageBox.Show(msg, "Advertencia...", MessageBoxButtons.OK);



            }
            else if (xref[0].CFCPlantID == 0)
            {                
                msg = "La referencia cruzada entre el número de línea y la planta de formulación debe configurarse antes de continuar";
                msg += msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                msg += msg + "(Cross-Reference between Line Number and Formulation Plant must be set up before continuing)";
                MessageBox.Show(msg, "Advertencia...", MessageBoxButtons.OK);
            }
            
            return rtn;
        }

        private void OpenLineMaintForm()
        {
            Close();
            LineMaintenance lineMaint;
            AIMGlobal.open_form(lineMaint = new LineMaintenance());
        }

        private void replueLine1_EditValueChanged(Object sender, EventArgs e)
        {            
            if (CheckLineXRefCFCPlant(1) != "")
            {
                OpenLineMaintForm();
                return;
            }                      
            
            CheckStatus(sender, 1);


            //8/23/19 - commented - Vitapro doesn't want to auto-open the export screen
            //LotInfo lot = new LotInfo();
            //lot = (LotInfo)grdSampleProductionView.GetFocusedRow();
            //if (lot != null)
            //{
            //    if (lot.ProductionStatusLine1 == (Int32)EnumProductionStatus.Current)
            //    {            
            //        OpenExportForm(sender, 1, (Int32)((DevExpress.XtraEditors.LookUpEdit)(sender)).Properties.KeyValue);
            //    }
            //}
        }

        private void replueLine2_EditValueChanged(Object sender, EventArgs e)
        {
            if (CheckLineXRefCFCPlant(2) != "")
            {
                OpenLineMaintForm();
                return;
            }
            CheckStatus(sender,2);

            //8/23/19 - commented - Vitapro doesn't want to auto-open the export screen
            //LotInfo lot = new LotInfo();
            //lot = (LotInfo)grdSampleProductionView.GetFocusedRow();
            //if (lot != null)
            //{
            //    if (lot.ProductionStatusLine2 == (Int32)EnumProductionStatus.Current)
            //    {                    
            //        OpenExportForm(sender, 2, (Int32)((DevExpress.XtraEditors.LookUpEdit)(sender)).Properties.KeyValue);
            //    }
            //}
        }

        private void replueLine3_EditValueChanged(Object sender, EventArgs e)
        {
            if (CheckLineXRefCFCPlant(3) != "")
            {
                OpenLineMaintForm();
                return;
            }
            CheckStatus(sender,3);

            //8/23/19 - commented - Vitapro doesn't want to auto-open the export screen
            //LotInfo lot = new LotInfo();
            //lot = (LotInfo)grdSampleProductionView.GetFocusedRow();
            //if (lot != null)
            //{
            //    if (lot.ProductionStatusLine3 == (Int32)EnumProductionStatus.Current)
            //    {                    
            //        OpenExportForm(sender, 3, (Int32)((DevExpress.XtraEditors.LookUpEdit)(sender)).Properties.KeyValue);
            //    }
            //}
        }

        private void replueLine4_EditValueChanged(Object sender, EventArgs e)
        {
            //6/4/19 - commented
            //if (CheckLineXRefCFCPlant(4) != "")
            //{
            //    OpenLineMaintForm();
            //    return;
            //}
            //CheckStatus(sender,4);
            
        }

        private void SetStatus()
        {
            //8/27/19
            grdSampleProductionView.Columns.ColumnByName("colExport").Visible = false;
            gpExport.Visible = false;

            if (rbAvailable.Checked)
                _productionStatus = (Int32)EnumProductionStatus.Available;

            if (rbCurrent.Checked)
            {
                _productionStatus = (Int32)EnumProductionStatus.Current;
                //grdSampleProductionView.Columns.ColumnByName("colExport").Visible = true;

                ////8/27/19 - if user has permission to export to C5, show mass export checkbox
                //Int32 rtn = CFCAIM.AIMUtilities.GlobalFunctions.CheckUserPermissions(_cfcUserID, (Int32)EnumPermissionTable.AnalysisExport);
                //if (rtn == 0)
                //{
                //grdSampleProductionView.Columns.ColumnByName("colExport").Visible = true;
                //}

                //8/27/19 - if user has permission to export to C5, show mass export checkbox                              

                //9/9/20 - change to use AIMCoreGlobal
                //if (CFCAIM.AIMUtilities.GlobalFunctions.CheckUserPermissions(_cfcUserID, (Int32)EnumPermissionTable.AnalysisExport) == 0)                
                if (CFCAIMCore.Global.CheckUserPermissions(_cfcUserID, (Int32)EnumPermissionTable.AnalysisExport) == 0)
                {
                    grdSampleProductionView.Columns.ColumnByName("colExport").Visible = true;
                    gpExport.Visible = true;
                }

            }

            if (rbDepleted.Checked)
                _productionStatus = (Int32)EnumProductionStatus.Depleted;           
            if (rbAll.Checked)
                _productionStatus = (Int32)EnumProductionStatus.All;
        }
                
        private void rbAvailable_CheckedChanged(Object sender, EventArgs e)
        {
            SetStatus();
            if (rbAvailable.Checked == true)
            {
                _days = 0;
                pnlHistory.Visible = false;
                DataBind();

                DataBindLineLue();
                ////////////////////DataBindLineLueForAvailable();
            }
            
            
        }

        private void rbCurrent_CheckedChanged(Object sender, EventArgs e)
        {
            SetStatus();
            if (rbCurrent.Checked == true)
            {
                _days = 0;
                pnlHistory.Visible = false;
                DataBind();
                DataBindLineLue();
            }
            
        }
        
        private void rbDepleted_CheckedChanged(Object sender, EventArgs e)
        {
            SetStatus();
            if (rbDepleted.Checked == true)
            {
                pnlHistory.Visible = true;
                DataBind();
                DataBindLineLueForDepleted();
            }                       
        }
                

        private void rbAll_CheckedChanged(Object sender, EventArgs e)
        {
            SetStatus();
            if (rbAll.Checked == true)
            {
                pnlHistory.Visible = true;
                DataBind();
                DataBindLineLue();
            }
        }             
              

        private void btnCurrent_Click(Object sender, EventArgs e)
        {
            lblDays.Visible = true;
            txtDays.Visible = true;
            lblHistorical.Visible = false;
            btnCurrent.BackColor = SystemColors.GradientInactiveCaption;
            btnHistorical.BackColor = SystemColors.Control;
            CheckSystemSettings();
            SetStatus();
            DataBind();
            btnRefresh.PerformClick();

        }

        private void btnHistorical_Click(Object sender, EventArgs e)
        {
            lblDays.Visible = false;
            txtDays.Visible = false;
            lblHistorical.Visible = true;
            btnCurrent.BackColor = SystemColors.Control;
            btnHistorical.BackColor = SystemColors.GradientInactiveCaption;
            _days = 0;
            SetStatus();
            DataBind();
            btnRefresh.PerformClick();
        }
               

        private void CheckStatus(Object sender, Int32 lineNumber)
        {
            // added check for 99 -- to avoid going thru this code when the "grdSampleProductionView_ValidateRow" event is fired.             
            if (lineNumber == 99)
            {
                return;
            }

            String msg = "";

            DialogResult dr = new DialogResult();
            LotInfo lot = new LotInfo();
            Int32 status = 0;
            Int32 currentStatus = 0;
            Int32 aimMaterialID = 0;
            lot = (LotInfo)grdSampleProductionView.GetFocusedRow();

            if (lineNumber == 1)
                currentStatus = lot.ProductionStatusLine1;
            if (lineNumber == 2)
                currentStatus = lot.ProductionStatusLine2;
            if (lineNumber == 3)
                currentStatus = lot.ProductionStatusLine3;

            //6/4/19 - commented
            //if (lineNumber == 4)
            //    currentStatus = lot.ProductionStatusLine4;


            //10/1/20 - changed from obsolete "Properties.KeyValue" to ".EditValue"
            //status = (Int32)((DevExpress.XtraEditors.LookUpEdit)(sender)).Properties.KeyValue;
            status = (Int32)((DevExpress.XtraEditors.LookUpEdit)(sender)).EditValue;


            aimMaterialID = lot.AIMMaterialID;
            CustomManager.SaveProductionLineStatus(lot.SampleID, lineNumber, status, _cfcUserID);                       

            if (status == (Int32)EnumProductionStatus.Current)
            {
                // check to see if, for this material, line, there is already one that's current 
                List<LotInfo> checkPrevious = new List<LotInfo>();
                checkPrevious = CustomManager.GetLotMaterialLine(aimMaterialID, lineNumber, status, lot.SampleID);
                if (checkPrevious.Count > 0)
                {                   
                    msg = "Esta línea actualmente tiene otro lote asignado para este material. ¿Desea marcar el lote anterior (# " + checkPrevious[0].LotNumber + ") como agotada?";
                    msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    msg = msg + "(This line currently has another lot assigned for this material. Do you want to mark the previous lot (# " + checkPrevious[0].LotNumber + ") as depleted?)";
                    dr = MessageBox.Show(msg, "Advertencia...", MessageBoxButtons.YesNoCancel);
                    if (dr == DialogResult.Yes)
                    {
                        //9/30/19 - changed per Sergio -- to mark only the lot for the line that's being changed, instead of marking all the lots as depleted.
                        //mark PREVIOUS lot as depleted - all lines.                        
                        //CustomManager.SaveProductionLineStatus(checkPrevious[0].SampleID, 1, (Int32)EnumProductionStatus.Depleted, _cfcUserID);
                        //CustomManager.SaveProductionLineStatus(checkPrevious[0].SampleID, 2, (Int32)EnumProductionStatus.Depleted, _cfcUserID);
                        //CustomManager.SaveProductionLineStatus(checkPrevious[0].SampleID, 3, (Int32)EnumProductionStatus.Depleted, _cfcUserID);
                        CustomManager.SaveProductionLineStatus(checkPrevious[0].SampleID, lineNumber, (Int32)EnumProductionStatus.Depleted, _cfcUserID);
                        
                    }
                    else
                    {
                        //change this lot back to available.
                        CustomManager.SaveProductionLineStatus(lot.SampleID, lineNumber, (Int32)EnumProductionStatus.Available, _cfcUserID);
                    }
                }
            }
            else if (status == (Int32)EnumProductionStatus.Depleted)
            {
                //9/30/19 - commented per Sergio
                //////mark all lines depleted for that sample
                ////CustomManager.SaveProductionLineStatus(lot.SampleID, 1, status, _cfcUserID);
                ////CustomManager.SaveProductionLineStatus(lot.SampleID, 2, status, _cfcUserID);
                ////CustomManager.SaveProductionLineStatus(lot.SampleID, 3, status, _cfcUserID);

                //update depleted date on Lot table
                CustomManager.UpdateDepletedDate(lot.SampleID, _cfcUserID, true);

                //change export status so Lot hyperlink color is normal                      
                CustomManager.SaveExportStatus(lot.SampleID, false);


            }
            else if (status == (Int32)EnumProductionStatus.Available && currentStatus == (Int32)EnumProductionStatus.Depleted)
            {
                //mark all lines available for that sample
                CustomManager.SaveProductionLineStatus(lot.SampleID, 1, status, _cfcUserID);
                CustomManager.SaveProductionLineStatus(lot.SampleID, 2, status, _cfcUserID);
                CustomManager.SaveProductionLineStatus(lot.SampleID, 3, status, _cfcUserID);
                
                //update depleted date on Lot table
                CustomManager.UpdateDepletedDate(lot.SampleID, _cfcUserID, false);

                // change export status so Lot hyperlink color is normal                      
                CustomManager.SaveExportStatus(lot.SampleID, false);

            }
            else if (status == (Int32)EnumProductionStatus.Available)
            {
                // change export status so Lot hyperlink color is normal                      
                CustomManager.SaveExportStatus(lot.SampleID, false);
            }

            DataBind();
            ShowYellowHelpText();
            
            
        }

        private void btnRefresh_Click(Object sender, EventArgs e)
        {
            //ShowYellowHelpText();     '10/1/19 - made refresh button Visible and commented this line
            DataBind();
            //DataBindLineLue();        '10/1/19 - made refresh button Visible and commented this line
            //SetToolTips();            '10/1/19 - made refresh button Visible and commented this line
            //CheckSystemSettings();    '10/1/19 - made refresh button Visible and commented this line
        }

        private void repItemPictureEdit1_Click(Object sender, EventArgs e)
        {
            if (((DevExpress.XtraEditors.PictureEdit)sender).Image != null)
            {
                OpenExportForm(sender, 1, (Int32)EnumProductionStatus.Current);
            }            
        }

        private void repItemPictureEdit2_Click(Object sender, EventArgs e)
        {
            if (((DevExpress.XtraEditors.PictureEdit)sender).Image != null)
            {
                OpenExportForm(sender, 2, (Int32)EnumProductionStatus.Current);
            }
        }

        private void repItemPictureEdit3_Click(Object sender, EventArgs e)
        {
            if (((DevExpress.XtraEditors.PictureEdit)sender).Image != null)
            {
                OpenExportForm(sender, 3, (Int32)EnumProductionStatus.Current);
            }
        }

        private void repItemPictureEdit4_Click(Object sender, EventArgs e)
        {
            //6/4/19 - commented
            //if (((DevExpress.XtraEditors.PictureEdit)sender).Image != null)
            //{
            //    OpenExportForm(sender, 4, (Int32)EnumProductionStatus.Current);
            //}
        }


        
        private void grdSampleProductionView_CustomUnboundColumnData(Object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            // okay, this has nothing to do with columns moving

            if (e.Column.Name == "colLine1Dots")
            {
                e.Value = null;
                e.Column.ToolTip = "";
                if (((LotInfo)(e.Row)).ProductionStatusLine1 == (Int32)EnumProductionStatus.Current)
                {
                    e.Value = Properties.Resources.fwd_icon;
                    e.Column.ToolTip = "Pantalla de exportación abierta";       //"Open Export Screen"
                }
            }
            if (e.Column.Name == "colLine2Dots")
            {
                e.Value = null;
                e.Column.ToolTip = "";
                if (((LotInfo)(e.Row)).ProductionStatusLine2 == (Int32)EnumProductionStatus.Current)
                {
                    e.Value = Properties.Resources.fwd_icon;
                    e.Column.ToolTip = "Pantalla de exportación abierta";       //"Open Export Screen"
                }
            }
            if (e.Column.Name == "colLine3Dots")
            {
                e.Value = null;
                e.Column.ToolTip = "";
                if (((LotInfo)(e.Row)).ProductionStatusLine3 == (Int32)EnumProductionStatus.Current)
                {
                    e.Value = Properties.Resources.fwd_icon;
                    e.Column.ToolTip = "Pantalla de exportación abierta";       //"Open Export Screen"
                }
            }

        }

        private void repositoryItemTextEdit1_EditValueChanged(Object sender, EventArgs e)
        {
            LotInfo lot = new LotInfo();
            lot = (LotInfo)grdSampleProductionView.GetFocusedRow();
            if (lot != null)
            {                
                Int32 resultVal = 0;
                Int32.TryParse((String)((DevExpress.XtraEditors.TextEdit)(sender)).EditValue, out resultVal);
                lot.AmountAvailable = resultVal;

                //save amount available to lot table for sample
                CustomManager.SaveAmountAvailable(lot.SampleID, resultVal);
            }    
        }

        private void repositoryItemTextEdit1_Enter(Object sender, EventArgs e)
        {
            var edit = ((DevExpress.XtraEditors.TextEdit)sender);
            edit.SelectAll();
            
        }

        private void repositoryItemTextEdit1_Click(Object sender, EventArgs e)
        {
            var edit = ((DevExpress.XtraEditors.TextEdit)sender);
            edit.SelectAll();

        }

        private void repositoryItemTextEdit1_MouseEnter(Object sender, EventArgs e)
        {
            var edit = ((DevExpress.XtraEditors.TextEdit)sender);
            edit.SelectAll();

        }               

         
        //8/27/19    
        private void btnAcceptAll_Click(object sender, EventArgs e)
        {
            Boolean export;
            export = false;
            if (_export == false)
            {
                export = true;
            }


            List<LotInfo> lots = new List<LotInfo>();
            lots = (List<LotInfo>)grdSampleProduction.DataSource;
            if (lots != null)
            {
                foreach (var item in lots)
                {
                    item.Export = export;
                }
            }
            _export = export;
            grdSampleProduction.RefreshDataSource();
        }

        //8/27/19 - Mass Export
        private void btnExport_Click(object sender, EventArgs e)
        {            
            Int32 row = 0;            
            Boolean blnExport = false;
            String msg = "";
            Int32 totalCnt = 0;
            Int32 acceptCnt = 0;

            try
            {
                CFCAIM.Custom.Globals.Global.exportHeader.Clear();
                CFCAIM.Custom.Globals.Global.exportDetail.Clear();
                CFCAIM.Custom.Globals.Global.validationSuccess = false;
                _headerID = 1;
                _detailID = 1;

                List<LotInfo> lot = new List<LotInfo>();
                lot = (List<LotInfo>)grdSampleProductionView.DataSource;

                for (int i = 0; i < lot.Count; i++)
                {
                    totalCnt++;
                    if (lot[i].Export == true)
                    {
                        acceptCnt++;
                    }                    
                }
               
                if (acceptCnt == 0)
                {
                    msg = "Por favor acepte uno o más lotes. Nada para exportar.";
                    msg = msg + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    msg = msg + "(Please accept one or more lots. Nothing to export.)";
                    MessageBox.Show(msg, "Información...", MessageBoxButtons.OK);
                    return;
                }
            

                foreach (var item in lot)
                {
                    if (item.Export == true)
                    {
                        List<LineRefCFCPlantInfo> line = new List<LineRefCFCPlantInfo>();
                        line = CustomManager.GetLineRefCFCPlant();
                        foreach (var lineItem in line)
                        {
                            //------------------------------------------------------------------------------------------------------------------------

                            //if (Convert.ToInt32(grdSampleProductionView.GetRowCellValue(row, "colProductionStatusLine" + lineItem.LineID)) == (Int32)EnumProductionStatus.Current)
                            // Got error trying to pull column name using string (and not grid column type) -- so doing this not the best way ...

                            blnExport = false;
                            if (lineItem.LineID == 1 && lineItem.IsActive == true)
                            {
                                if (Convert.ToInt32(grdSampleProductionView.GetRowCellValue(row, colProductionStatusLine1)) == (Int32)EnumProductionStatus.Current)
                                {
                                    blnExport = true;
                                }
                            }
                            else if (lineItem.LineID == 2 && lineItem.IsActive == true)
                            {
                                if (Convert.ToInt32(grdSampleProductionView.GetRowCellValue(row, colProductionStatusLine2)) == (Int32)EnumProductionStatus.Current)
                                {
                                    blnExport = true;
                                }
                            }
                            else if (lineItem.LineID == 3 && lineItem.IsActive == true)
                            {
                                if (Convert.ToInt32(grdSampleProductionView.GetRowCellValue(row, colProductionStatusLine3)) == (Int32)EnumProductionStatus.Current)
                                {
                                    blnExport = true;
                                }
                            }
                            //------------------------------------------------------------------------------------------------------------------------

                            if (blnExport == true)
                            {                                
                                PopulateExportEntities(_headerID, item.SampleID, item.LotNumber, lineItem.LineID, true);
                            }
                        }
                    }
                    row++;
                }
                               
                if (CFCAIM.Custom.Globals.Global.validationSuccess == true)
                {
                    Close();
                    CFCAIM.Custom.Globals.Global.blnMassExport = true;
                    Export exportForm;
                    AIMGlobal.open_form(exportForm = new Export());
                }
                else
                {
                    // message ???
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //9/8/20
        private void rbNotExportedToConcept5_CheckedChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        //9/2/20
        private void rbExportedToConcept5_CheckedChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        //9/8/20
        private void rbAllRecs_CheckedChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        //9/2/20
        private void grdSampleProductionView_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {          
            
            Boolean concept5Status = (Boolean)grdSampleProductionView.GetListSourceRowCellValue(e.ListSourceRow, "Concept5ExportStatus");
                     
            if (rbExportedToConcept5.Checked == true)
            {
                if (concept5Status == true)
                {
                    e.Visible = true;
                    e.Handled = false;
                }
                else
                {
                    e.Visible = false;
                    e.Handled = true;
                }
            }
            else if (rbNotExportedToConcept5.Checked == true)
            {
                if (concept5Status == false)
                {
                    e.Visible = true;
                    e.Handled = false;
                }
                else
                {
                    e.Visible = false;
                    e.Handled = true;
                }
            }
            else
            {
                e.Visible = true;
                e.Handled = false;
            }            
        }      
    }

}
