using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using CFCAIM.Maintenance;
using CFCAIMCore;
using DevExpress.XtraEditors;
using System.Linq;
using CFCAIM.AIMUtilities;
using CFCAIM.Chart;
using System.Collections;
using System.Data;
using DevExpress.XtraGrid.Views.Base;
using CFCUser.Manager;
using CFCUser.Entity;

namespace CFCAIM
{
    public partial class Analysis : Form
    {

        CFCUser.Global _userGlobal = new CFCUser.Global();

        MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];
        TabPage PreviousTab;
        TabPage CurrentTab;
        Charts charts;
        AnalysisHelp analysisHelp;
        AnalysisHelpTarget analysisHelpTarget;
        FormulationCrossReference formulationCrossReference;

        //These two IDs are keeping the selected template same in all tabs
        Int32 _selectedViewTemplateID = 0;
        Int32 _selectedQueryTemplateID = 0;

        Boolean _saveViewTemplateAs = false;

        List<AIMOwnerLocationInfo> _aimOwnerLocation = new List<AIMOwnerLocationInfo>();
        List<AIMOwnerInfo> _aimOwner = new List<AIMOwnerInfo>();
        List<AIMMaterialInfo> _aimMaterial = new List<AIMMaterialInfo>();
        List<AIMSupplierInfo> _aimSupplier = new List<AIMSupplierInfo>();
        List<AIMSupplierLocationInfo> _aimSupplierLocation = new List<AIMSupplierLocationInfo>();
        List<CFCPlantInfo> _cfcPlant = new List<CFCPlantInfo>();

        //10/10/23
        List<CFCPlantInfo> _cfcPlantCheckbox = new List<CFCPlantInfo>();

        List<CFCIngredientInfo> _cfcIngredient = new List<CFCIngredientInfo>();
        Boolean _isFormLoaded = false;
        Int32 _currentRow = 0;
        Boolean _isAnalysis = false;
        Boolean _saveAs = false;
        SampleDetailFromAnalysis _sampleDetail;
        //This is used to handle the current dataset
        DateTime _currentStartDate = DateTime.MinValue;
        DateTime _currentEndDate = DateTime.MaxValue;
        Int32 _currentQueryTemplateID = 0;
        List<AIMNutrientInfo> _aimNutrient = new List<AIMNutrientInfo>();
        Boolean _skipReLoad = false;        
        List<CFCPlantRefAIMOwnerLocationInfo> _aimCFCPlantCrossReference = new List<CFCPlantRefAIMOwnerLocationInfo>();
        Boolean _missingOwnerLocXRef = false;
        List<UserDefaultsInfo> _userDefaults = new List<UserDefaultsInfo>();
        List<ControlInfo> _controlEntity = new List<ControlInfo>();

        //10/9/20
        //Boolean blnOutliersOnlyFilterOn = false;

        //2/19/21 - commented, not used
        //List<SampleInfo> _sample = new List<SampleInfo>();

        //-------------------------------------------------------
        //3/13/23 - added for 3 hide/show checkboxes. Depending on what is selected, the grid size and panel locations change.

        //Grid Size FormWindowState.Maximum)
        Size MaxShowBoth = new Size(1573, 515);
        Size MaxShowSummary = new Size(1573, 580);
        Size MaxShowGroupBy = new Size(1573, 630);
        Size MaxHideBoth = new Size(1573, 630);

        //Grid Size (FormWindowState.Normal)
        Size NormalShowBoth = new Size(1203, 355);
        Size NormalShowSummary = new Size(1203, 420);
        Size NormalShowGroupBy = new Size(1203, 420);
        Size NormalHideBoth = new Size(1203, 520);

        //Location of the panels
        Point ShowBoth = new Point(3, 111);
        Point ShowSummary = new Point(3, 70);
        Point ShowGroupBy = new Point(3, 46);
        Point HideBoth = new Point(3, 0);
        Point TopLocation = new Point(2, 0);
        //-------------------------------------------------------

        //For User Groups
        Int32 _ownerLocationGroupID = 0;                                        //10/17/23
        Int32 _supplierGroupID = 0;                                             //10/17/23
        Int32 _supplierLocationGroupID = 0;                                     //10/17/23        
        UserInfo _user = new UserInfo();                                        //10/17/23

        //11/22/23
        Boolean _blnAllowExport = false;
        

        #region form controls
        public Analysis()
        {
            try
            {
                InitializeComponent();

                //------------------------------------------------------------
                //3/14/23 - added to allow user to change header row height
                grdViewAnalyst.MouseMove += grdViewAnalyst_MouseMove;
                grdViewAnalyst.MouseUp += (sender, e) => {
                    isResizingColumnPanel = false;
                };
                //------------------------------------------------------------

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...");
            }
        }


        private void analyst_Load(object sender, EventArgs e)
        {
            try
            {

                AIMManager.DeleteSampleAIMRefCFCUser(_userGlobal.CFCLoginUser.CFCUserID, SampleAIMRefCFCUserEnum.CFCUserID);
                _isFormLoaded = false;

                //10/26/21 - changed date fields from DateTimePicker to DevExpress DateEdit (resize issue)
                //dtpStart.Value = DateTime.Today.AddDays(-29);                
                //dtpEnd.Value = DateTime.Today;                                
                deStart.EditValue = DateTime.Now.AddDays(-29);
                deEnd.EditValue = DateTime.Now;

                //10/17/23
                SetUserGroups();


                dataBindLue();
                setToolTips();
                _isFormLoaded = true;

                //12/16/20               
                loadUserDefaults();

                setOutlierButtons(true);  //added because outliers can be changed on chart screen  

                //initializing this global to false (the global gets set in ExportBatchMaintenancePopUp,
                //                                              which is available only thru a button on the Analysis form.)
                AIMGlobal.exportComingFromAnalysis = false;

                //hide these tab pages for now
                tcoAnalysis.TabPages.Remove(tpaLog);
                tcoAnalysis.TabPages.Remove(tpa_queryOLD);

                //Query Tab lue's
                dataBindQueryTabLue();
                dataBindMaterial();
                dataBindOwnerLocationlue();
                dataBindSupplierLocationlue();

                //1/12/18
                dataBindDefaults();

                //10/13/23
                dataBindExportGroups();

                AIMCoreGlobal.LoadCustomEquation();
                WindowState = FormWindowState.Maximized;


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataBindExportGroups()
        {
            List<ExportIngredientGroupInfo> group = new List<ExportIngredientGroupInfo>();
            group = AIMManager.GetExportIngredientGroup(true);
            group.Insert
                (
                0,
                new ExportIngredientGroupInfo
                {
                    ExportIngredientGroupID = 0,
                    ExportIngredientGroupCode = "--Select--"
                }
                );
            lueExpandGroup.Properties.DataSource = group;
            XtraEditors.LoadLookUpEdit(ref lueExpandGroup, "ExportIngredientGroupID", "ExportIngredientGroupCode");
            lueQueryBaseGroup.Properties.DataSource = group;
            XtraEditors.LoadLookUpEdit(ref lueQueryBaseGroup, "ExportIngredientGroupID", "ExportIngredientGroupCode");

            //1/9/24
            if (group.Count == 2)
            {
                lueExpandGroup.EditValue = group[1].ExportIngredientGroupID;
            }

            //1/11/24
            List<ExportPlantGroupInfo> groupPlant = new List<ExportPlantGroupInfo>();
            groupPlant = AIMManager.GetExportPlantGroup(true);
            groupPlant.Insert
                (
                0,
                new ExportPlantGroupInfo
                {
                    ExportPlantGroupID = 0,
                    ExportPlantGroupCode = "--Select--"
                }
                );
            lueCFCPlantGroup.Properties.DataSource = groupPlant;
            XtraEditors.LoadLookUpEdit(ref lueCFCPlantGroup, "ExportPlantGroupID", "ExportPlantGroupCode");
                        
            //lueQueryBaseGroup.Properties.DataSource = group;
            //XtraEditors.LoadLookUpEdit(ref lueQueryBaseGroup, "ExportIngredientGroupID", "ExportIngredientGroupCode");
            if (groupPlant.Count == 2)
            {
                lueCFCPlantGroup.EditValue = groupPlant[1].ExportPlantGroupID;
            }
        }

        //1/12/18
        private void dataBindDefaults()
        {
            try
            {
                _controlEntity = AIMManager.GetControlUsingAllowUserDefault(true);
                _userDefaults = AIMManager.GetUserDefaultsForCFCUserID(_userGlobal.CFCLoginUser.CFCUserID);
                foreach (var defaultItem in _userDefaults)
                {
                    foreach (var controlName in Controls.All())
                    {
                        if (controlName.Name == defaultItem.ControlName)
                        {
                            if (controlName.AccessibilityObject.Role == AccessibleRole.RadioButton)
                            {
                                ((RadioButton)controlName).Checked = true;
                            }
                            else if (controlName.AccessibilityObject.Role == AccessibleRole.CheckButton)
                            {
                                ((CheckBox)controlName).Checked = true;
                            }

                        }
                        else if (defaultItem.ControlName == "rbRecalcNo")
                        {
                            cbRecalculateRightAway.Checked = false;
                        }
                        else if (defaultItem.ControlName == "rbRecalcYes")
                        {
                            cbRecalculateRightAway.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private Image getImageFromResource(string fileName)
        {
            Stream resource = typeof(Analysis).Assembly.GetManifestResourceStream("ImagesInCells.Resources." + fileName);
            return Image.FromStream(resource);
        }
        private byte[] getImageData(string fileName)
        {
            Image img = getImageFromResource(fileName);
            MemoryStream mem = new MemoryStream();
            img.Save(mem, System.Drawing.Imaging.ImageFormat.Bmp);
            return mem.GetBuffer();
        }


        private void setToolTips()
        {
            try
            {
                System.Drawing.Icon ico = Properties.Resources.CFCAIM;
                this.Icon = ico;

                ToolTip ttip = new ToolTip();
                foreach (Control ctrl in pnlTopBtns.Controls)
                {
                    ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
                }
                ttip.SetToolTip(btnPreview, "Preview Grid");
                ttip.SetToolTip(btnNewQueryTemplate, "New Template");
                ttip.SetToolTip(btnNewQueryTemplate, "Add New Query Template");
                ttip.SetToolTip(btnSaveQueryTemplateAs, "Save Query Template As");
                ttip.SetToolTip(btnSaveQueryTemplate, "Save Template");
                ttip.SetToolTip(btnCancelQueryTemplateSave, "Cancel ");
                ttip.SetToolTip(btnDeleteQueryTemplate, "Delete Template");
                ttip.SetToolTip(btnAdd, "Add Parameter");
                ttip.SetToolTip(btnSaveViewTemplate, "Save Template");
                ttip.SetToolTip(btnCancelViewTemplateSave, "Cancel");
                ttip.SetToolTip(btnDeleteViewTemplate, "Delete Template");
                ttip.SetToolTip(btnRecalculate, "Load/Recalculate Data");
                ttip.SetToolTip(btnUpdateCurrentViewTemplate, "Save Current Template/View");
                ttip.SetToolTip(btnExportBatchMaintenance, "Export Batch Maintenance");
                ttip.SetToolTip(btnColumnCalcDescription, "Calculation Descriptions");
                ttip.SetToolTip(this.btnPreview, "Preview Grid Results");
                ttip.SetToolTip(this.btnHelp, "Help");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
                ttip.SetToolTip(btnGraph, "Charts");
                ttip.SetToolTip(btnExit, "");
                ttip.SetToolTip(this.btnExportTargetHelp, "Export Target Definitions");

                ttip.SetToolTip(this.btnAddMaterial, "Add Material to Query");
                ttip.SetToolTip(this.btnAddNutrient, "Add Nutrient to Query");
                ttip.SetToolTip(this.btnAddOwner, "Add Owner to Query");
                ttip.SetToolTip(this.btnAddOwnerLocation, "Add Owner Location to Query");
                ttip.SetToolTip(this.btnAddSupplier, "Add Supplier to Query");
                ttip.SetToolTip(this.btnAddSupplierLocation, "Add Supplier Location to Query");
                ttip.SetToolTip(this.btnAddSampleDate, "Add Sample Date to Query");
                ttip.SetToolTip(this.btnAddMaterialNutrient, "Add Material to Query");
                ttip.SetToolTip(this.btnAddIngredient, "Add Ingredient to Query");
                ttip.SetToolTip(this.btnRemoveSampleDate, "Remove Sample Date from Query");



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueQueryTemplate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isFormLoaded == false)
                {
                    return;
                }
                tabControlQuery.Visible = false;


                _mainMenu.MessageBoxCFCshow("Query template is not yet connected with the grid. We are working on it.");
                AnalysisTemplateInfo analysisTemplate = new AnalysisTemplateInfo();
                SetBtnRecalculate();
                _selectedQueryTemplateID = (Int32)lueQueryTemplate.EditValue;
                if ((Int32)lueQueryTemplate.EditValue > 0)
                {
                    tabControlQuery.Visible = true;
                    gpbDate.Visible = false;
                    analysisTemplate = AIMManager.GetOneAnalysisTemplate((Int32)lueQueryTemplate.EditValue);

                    if (analysisTemplate != null)
                    {
                        AIMGlobal.AnalysisMath = analysisTemplate.MathType;
                    }

                    if (cbRecalculateRightAway.Checked == true)
                    {
                        dataBindGroupBy(false);
                    }
                    bindExportTarget();
                }
                else
                {

                    gpbDate.Visible = true;
                    rdbBase.Checked = true;
                    lueCFCPlant.EditValue = 0;
                    if (cbRecalculateRightAway.Checked == true)
                    {
                        dataBindGroupBy(false);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueViewTemplate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isFormLoaded == false)
                {
                    return;
                }
                _selectedViewTemplateID = (Int32)lueViewTemplate.EditValue;

                //If "--Select--" is picked, remove any column groupings
                if ((Int32)_selectedViewTemplateID == 0)
                {
                    foreach (var column in grdViewAnalyst.Columns)
                    {
                        ((DevExpress.XtraGrid.Columns.GridColumn)(column)).GroupIndex = -1;
                    }
                }
                else
                {
                    applyViewTemplate();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdViewAnalyst_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            try
            {
                Double lowerMin = 0;
                Double lowerMax = 0;
                Double upperMin = 0;
                Double upperMax = 0;
                Double sampleValue = 0;
                Int32 cfcPlantID = 0;
                String cfcIngredientCode = "";
                String cfcNutrientCode = "";
                Boolean isValidOutlier = true;
                Boolean isOverlay = true;

                if (e.RowHandle >= 0)
                {
                    isOverlay = (Boolean)(grdViewAnalyst.GetRowCellValue(e.RowHandle, colIsOverlay));
                }

                //commented all - not completely tested
                //Int32 sampleCount = 0;
                //Boolean modified = false;
                //Int32 excludedSample = 0;

                if (e.Column == colSampleValue)
                {
                    sampleValue = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colSampleValue));

                    //6/24/20
                    //lowerMin = Convert.ToInt32(grdViewAnalyst.GetRowCellValue(e.RowHandle, colLMin));
                    lowerMin = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colLMin));

                    lowerMax = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colLMax));
                    upperMin = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colUMin));
                    upperMax = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colUMax));
                    isValidOutlier = Convert.ToBoolean(grdViewAnalyst.GetRowCellValue(e.RowHandle, colTrimmedIsValidOutlier));

                    if (Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colSampleSize)) > 0)
                    {
                        if (isValidOutlier == true)
                        {
                            if (lowerMax + upperMax > 0)
                            {
                                e.Appearance.BackColor = Color.Red;
                            }
                            else if (lowerMin + upperMin > 0)
                            {
                                e.Appearance.BackColor = Color.Orange;
                            }
                        }
                    }
                }

                if (e.Column == colCurrentNutrientVal)
                {
                    if (isOverlay == true)
                    {
                        e.Appearance.ForeColor = Color.Blue;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                }

                if (e.Column == colOwnerLocationCode)
                {

                    if (rdbSpecificOverlay.Checked == false)    //don't change background to yellow if specific overlay is selected
                    {
                        cfcPlantID = (Int32)(grdViewAnalyst.GetRowCellValue(e.RowHandle, colCFCPlantID));
                        if (cfcPlantID == 0)
                        {
                            if (cbOwnerLocation.Checked == false)
                            {
                                //1/19/21
                                //e.DisplayText = "Add Grouping";
                                e.DisplayText = "Add FRM x-ref";
                            }
                            e.Appearance.BackColor = Color.Yellow;
                        }
                    }
                }

                if (e.Column == colCFCIngredientName)
                {
                    cfcIngredientCode = Convert.ToString(grdViewAnalyst.GetRowCellValue(e.RowHandle, colCFCIngredientCode));
                    if (cfcIngredientCode == "")
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                }

                if (e.Column == colCFCNutrientName)
                {
                    cfcNutrientCode = Convert.ToString(grdViewAnalyst.GetRowCellValue(e.RowHandle, colCFCNutrientCode));
                    if (cfcNutrientCode == "")
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                }

                
                // CHECK THIS OUT:  https://documentation.devexpress.com/#WindowsForms/CustomDocument17691								
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void rbPlantYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbRecalculateRightAway.Checked == false)
                {
                    dataBindGroupBy(true);
                }
                else
                {
                    dataBindGroupBy(_skipReLoad);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbDateYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbRecalculateRightAway.Checked == false)
                {
                    dataBindGroupBy(true);
                }
                else
                {
                    dataBindGroupBy(_skipReLoad);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //this is the SampleID grouping
        private void rbMaterialYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbRecalculateRightAway.Checked == true)
                {
                    dataBindGroupBy(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                //12/17/20
                saveUserDefaults();

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdViewAnalyst_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                dataBindSummary();
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
                //11/22/23                
                if (_blnAllowExport == false)                
                {
                    MessageBox.Show("Data must be reloaded before exporting. ", "Warning...", MessageBoxButtons.OK);
                    return;
                }

                Int32 count = 0;

                if (grdViewAnalyst.RowCount > 0)
                {
                    this.Hide();
                    this.SendToBack();

                    //10/26/21 - changed date fields from DateTimePicker to DevExpress DateEdit (resize issue)
                    //CFCAIMCore.Global.ExportStartDate = dtpStart.Value.Date;
                    //CFCAIMCore.Global.ExportEndDate = dtpEnd.Value.Date;
                    CFCAIMCore.Global.ExportStartDate = deStart.DateTime;
                    CFCAIMCore.Global.ExportEndDate = deEnd.DateTime;

                    //-----------------------------------------------------------------
                    //CHECK FILTER - 10/5/23
                    AIMGlobal.analysisDataListFiltered = null;
                    if (grdViewAnalyst.ActiveFilter.Count > 0) {

                        List<AnalysisInfo> filteredList = new List<AnalysisInfo>();
                        for (int i = 0; i < grdViewAnalyst.DataRowCount; i++)
                        {
                            filteredList.Add(grdViewAnalyst.GetRow(i) as AnalysisInfo);
                        }
                        AIMGlobal.analysisDataListFiltered = filteredList;
                    }

                    //-----------------------------------------------------------------
                    AIMGlobal.analysisDataList = (List<AnalysisInfo>)grdAnalyst.DataSource;
                    AIMGlobal.analysisWarningList = new List<AnalysisInfo>();
                    AIMGlobal.exportbatchMissingIngredientOrNutrient = false;

                    //make sure FRM/AIM ingredient and nutrient are there					
                    foreach (AnalysisInfo item in AIMGlobal.analysisDataList)
                    {
                        if (item.CFCIngredientID == 0 || item.CFCNutrientID == 0)
                        {
                            AIMGlobal.analysisWarningList.Add(
                                new AnalysisInfo
                                {
                                    AIMMaterialID = item.AIMMaterialID,
                                    AIMMaterialCode = item.AIMMaterialCode,
                                    AIMMaterialName = item.AIMMaterialName,
                                    AIMNutrientID = item.AIMNutrientID,
                                    AIMNutrientCode = item.AIMNutrientCode,
                                    AIMNutrientName = item.AIMNutrientName
                                }
                                );
                            AIMGlobal.exportbatchMissingIngredientOrNutrient = true;
                        }

                        if (item.CFCPlantID == 0)
                        {
                            count++;
                        }
                    }

                    //-----------------------------------------------------------------
                    //SPECIFIC OVERLAY
                    if (rdbSpecificOverlay.Checked == true)
                    {
                        CFCAIMCore.Global.exportbatchTargetID = (Int32)ExportTargetEnum.SpecificOverlay;

                        //Single Plant
                        if (rbPlantSingle.Checked == true)
                        {
                            if ((Int32)lueCFCPlant.EditValue == 0)
                            {                                
                                MessageForGroup(lueCFCPlant, "Plant");
                                return;
                            }
                            else
                            {
                                AIMGlobal.exportbatchPlantSingleOrGroup = "Single";
                                AIMGlobal.exportbatchAIMOwnerLocationID = (Int32)lueCFCPlant.EditValue;
                                AIMGlobal.exportbatchCFCPlantName = lueCFCPlant.Text.ToString();
                            }
                        }

                        //Plant Group
                        else
                        {
                            if ((Int32)lueCFCPlantGroup.EditValue == 0)
                            {
                                MessageForGroup(lueCFCPlantGroup, "PlantGroup");
                                return;
                            }
                            else
                            {
                                AIMGlobal.exportbatchPlantSingleOrGroup = "Group";
                                AIMGlobal.exportbatchPlantGroupID = (Int32)lueCFCPlantGroup.EditValue;                   //Plant Group ID
                                AIMGlobal.exportbatchPlantGroupName = lueCFCPlantGroup.Text.ToString();                  //Plant Group Name
                            }
                        }                       
                    }

                    //-----------------------------------------------------------------
                    //DEFAULT OVERLAY
                    else if (rdbDefaultOverlay.Checked == true)
                    {
                        CFCAIMCore.Global.exportbatchTargetID = (Int32)ExportTargetEnum.DefaultOverlay;                        
                        DialogResult dr = new DialogResult();
                        if (count > 0)
                        {
                            dr = MessageBox.Show("Some records (highlighted) are missing a Formulation/AIM plant cross-reference. The export will skip these records.", "Warning ...", MessageBoxButtons.OKCancel);
                            if (dr != DialogResult.OK)
                            {
                                Show();
                                BringToFront();
                                return;
                            }
                        }
                    }

                    //-----------------------------------------------------------------
                    //BASE                   
                    else
                    {
                        CFCAIMCore.Global.exportbatchTargetID = (Int32)ExportTargetEnum.Base;
                    }
                                        
                    //-----------------------------------------------------------------
                    //CHECK EXPAND
                    if (chkExpand.Checked == true && (Int32)lueExpandGroup.EditValue == 0)
                    {
                        MessageForGroup(lueExpandGroup, "Ingredient");
                        return;
                    }
                    if (chkExpand.Checked == true)
                    {

                        if (rdbSpecificOverlay.Checked == true) { CFCAIMCore.Global.exportbatchTargetID = (Int32)ExportTargetEnum.SpecificOverlayExpand; }
                        else if (rdbDefaultOverlay.Checked == true) { CFCAIMCore.Global.exportbatchTargetID = (Int32)ExportTargetEnum.DefaultOverlayExpand; }
                        else { CFCAIMCore.Global.exportbatchTargetID = (Int32)ExportTargetEnum.BaseExpand; }

                        AIMGlobal.exportbatchIngredientGroupID = (Int32)lueExpandGroup.EditValue;                   //Expand ID
                        AIMGlobal.exportbatchIngredientGroupName = lueExpandGroup.Text.ToString();                  //Expand Name
                    }



                    ExportBatchMaintenancePopUp exportForm = new ExportBatchMaintenancePopUp();
                    exportForm.ShowDialog();
                    this.Show();
                                       
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //1/8/24
        private void MessageForGroup(LookUpEdit lue, String type)
        {
            String msg;
            if (type == "Ingredient")
            {
                msg = "Please select an Ingredient Group. ";
            }
            else if (type == "PlantGroup")
            {                
                msg = "Please select a Plant Group. ";
            }
            else
            {
                msg = "Please select a CFC Plant Name.";
            }
            MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
            Show();
            BringToFront();
            lue.ShowPopup();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    //9/9/20 - change to use AIMCoreGlobal
                    //AIMGlobal.previewGridResults(grdAnalyst, "Full Screen Analysis");
                    AIMGlobal.SplashScreenShow();
                    CFCAIMCore.Global.previewGridResults(grdAnalyst, "Full Screen Analysis - Ingredients");
                    AIMGlobal.SplashScreenHide();
                }
                catch (Exception ex)
                {
                    AIMGlobal.SplashScreenHide();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void grdViewAnalyst_PrintInitialize(object sender, PrintInitializeEventArgs e)
        {
            try
            {
                PrintingSystemBase printSystem = e.PrintingSystem as PrintingSystemBase;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnRecalculate_Click(object sender, EventArgs e)
        {
            try
            {
                dataBindGroupBy(false);
                btnRecalculate.BackColor = SystemColors.Control;

                //11/22/23 
                _blnAllowExport = true;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //does btnLoadData exist?
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                dataBindGroupBy(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region QueryTab form functions

        private void btnSaveTemplateAs_Click(object sender, EventArgs e)
        {
            try
            {
                _saveAs = true;
                QueryTemplateInfo temp = new QueryTemplateInfo();
                temp = (QueryTemplateInfo)lueQueryTemplateInTab.GetSelectedDataRow();

                if (temp == null)
                {
                    return;
                }

                if (temp.QueryTemplateID == 0)
                {
                    MessageBox.Show("Please select the template first.", "Warning...", MessageBoxButtons.OK);
                    lueQueryTemplateInTab.ShowPopup();
                    return;
                }

                tabControlQuery.Visible = false;
                lblYellowBox.Visible = false;
                txtYellowBox.Visible = false;
                pnlExistingQueryTemplate.Visible = false;
                pnlQueryButtons.Visible = false;
                pnlNewQueryTemplate.Visible = true;
                pnlNewQueryTemplate.Location = new Point(10, 65);
                pnlNewQueryTemplate.Size = new Size(506, 65);
                pnlNewQueryTemplate.BringToFront();
                txtNewQueryTemplateName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            QueryTemplateInfo queryTemplate = new QueryTemplateInfo();
            Int32 newAnalysisTemplateID;

            try
            {
                /* create new template with just a sample query in it
				 * or
				 * create new template with existing sample query in it
				 * 
				 * depending upon the switch
				 */


                if (txtNewQueryTemplateName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please provide a template name.", "Warning...", MessageBoxButtons.OK);
                    return;
                }
                if (txtNewQueryTemplateName.Text.Trim().Length > 30)
                {
                    MessageBox.Show("Template name is too long. It cannot be more than 30 characters.", "Warning...", MessageBoxButtons.OK);
                    return;
                }

                QueryTemplateInfo templatename = new QueryTemplateInfo();
                templatename = AIMManager.GetQueryTemplateName(txtNewQueryTemplateName.Text);
                if (templatename != null && templatename.QueryTemplateID != 0)
                {
                    MessageBox.Show("This template name already exists. Please enter another name.", "Warning...", MessageBoxButtons.OK);
                    txtNewQueryTemplateName.Text = "";
                    txtNewQueryTemplateName.Focus();
                    return;
                }

                queryTemplate.QueryTemplateID = 0;
                queryTemplate.Name = txtNewQueryTemplateName.Text;
                queryTemplate.IsAnalysis = true;
                queryTemplate.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;


                if (rdbQuerySpecificOverlay.Checked == true)
                {
                    queryTemplate.ExportTargetID = ExportTargetEnum.SpecificOverlay;
                    if (lueQueryAIMOwnerLocation.ItemIndex != 0)
                    {
                        queryTemplate.AIMOwnerLocationID = lueQueryAIMOwnerLocation.ItemIndex;
                    }
                }
                else if (rdbQueryDefaultOverlay.Checked == true)
                {
                    queryTemplate.ExportTargetID = ExportTargetEnum.DefaultOverlay;
                }

                //10/18/23
                if (rdbQueryBaseGroup.Checked == true)
                {
                    queryTemplate.ExportTargetID = ExportTargetEnum.BaseExpand;                    
                }

                else
                {
                    queryTemplate.ExportTargetID = ExportTargetEnum.Base;
                }

                //10/31/22
                queryTemplate.IngredientOrProduct = "I";

                newAnalysisTemplateID = AIMManager.SaveQueryTemplate(ref queryTemplate).RecordID;
                if (newAnalysisTemplateID == 0)
                {
                    //couldn't save the template
                    pnlNewQueryTemplate.Visible = false;
                    pnlExistingQueryTemplate.Visible = true;
                    pnlQueryButtons.Visible = true;
                    tabControlQuery.Visible = true;
                    grpQueryExportTarget.Visible = true;
                    pnlNewQueryTemplate.Location = new Point(435, 60);  //moved out of the way
                    pnlNewQueryTemplate.Size = new Size(609, 10);       //and made shorter
                    pnlNewQueryTemplate.SendToBack();
                }
                else
                {
                    if (_saveAs == true)
                    {
                        AIMManager.CopyQueryTemplateDetail(newAnalysisTemplateID, (Int32)lueQueryTemplateInTab.EditValue, _userGlobal.CFCLoginUser.CFCUserID);
                    }
                    _saveAs = false;
                    dataBindLue();
                    lueQueryTemplateInTab.EditValue = newAnalysisTemplateID;
                    pnlNewQueryTemplate.Visible = false;
                    pnlExistingQueryTemplate.Visible = true;
                    pnlQueryButtons.Visible = true;
                    tabControlQuery.Visible = true;
                    grpQueryExportTarget.Visible = true;
                }

                txtNewQueryTemplateName.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void btnCancelTemplateSave_Click(object sender, EventArgs e)
        {
            try
            {
                pnlNewQueryTemplate.Visible = false;
                pnlExistingQueryTemplate.Visible = true;
                pnlQueryButtons.Visible = true;
                txtNewQueryTemplateName.Text = String.Empty;
                txtDateOption.Text = String.Empty;
                tabControlQuery.Visible = true;

                //2/7/18
                grpQueryExportTarget.Visible = true;

                if ((Int32)lueQueryTemplateInTab.EditValue == 0)
                {
                    tabControlQuery.Visible = false;

                    //2/7/18
                    grpQueryExportTarget.Visible = false;
                }



                pnlNewQueryTemplate.Location = new Point(435, 60);  //moved out of the way
                pnlNewQueryTemplate.Size = new Size(609, 10);       //and made shorter
                pnlNewQueryTemplate.SendToBack();
                tabControlQuery.Visible = true;
                checkYellowBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueQueryTemplateInTab_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblCannotUpdate.Visible = false;
                lblCannotUpdate.SendToBack();
                btnDeleteQueryTemplate.Visible = false;

                dtpValue.Text = "";
                dtpSecondValue.Text = "";
                txtDateOption.Text = "";

                _saveAs = false;
                if (lueQueryTemplateInTab.EditValue != null)
                {
                    _selectedQueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;  //to change the selected template in data tab.

                    lueQueryTemplate.EditValue = _selectedQueryTemplateID;

                    if ((Int32)lueQueryTemplateInTab.EditValue == 0)
                    {
                        grpQueryExportTarget.Visible = false;
                        btnSaveQueryTemplateAs.Enabled = false;

                        pnlNewLine.Enabled = false;
                        pnlNewLine.Visible = false;
                        grdQuery.Visible = false;

                    }
                    else
                    {
                        grpQueryExportTarget.Visible = true;
                        btnSaveQueryTemplateAs.Enabled = true;

                        pnlNewLine.Enabled = true;
                        pnlNewLine.Visible = true;
                        grdQuery.Visible = true;

                        bindExportTarget();
                    }
                }

                QueryTemplateInfo analysisTemplate = (QueryTemplateInfo)lueQueryTemplateInTab.GetSelectedDataRow();
                if (analysisTemplate != null && analysisTemplate.QueryTemplateID > 0)
                {
                    if (analysisTemplate.IsAnalysis == true)
                    {
                        _isAnalysis = true;
                        pnlNewLine.Enabled = true;
                        pnlNewLine.Visible = true;
                        btnDeleteQueryTemplate.Visible = true;
                    }
                    else
                    {
                        _isAnalysis = false;
                        pnlNewLine.Enabled = false;
                        pnlNewLine.Visible = false;
                        btnDeleteQueryTemplate.Visible = false;
                        lblCannotUpdate.Visible = true;
                        lblCannotUpdate.BringToFront();

                    }

                    showLastUpdate(analysisTemplate.UserName, analysisTemplate.UpdateStamp.ToString());

                }
                else
                {
                    showLastUpdate("", "");
                }


                dataBindSelectedQueryDetail();
                dataBindTemplateLue();
                dataBindTemplateGrids();
                dataBindSampleDate();
                checkYellowBox();

                //2/7/18
                clearIncludeExclude();
                dataBindIncludeExcludeBoxes();

                tabControlQuery.SelectedIndex = 0;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showLastUpdate(String userID, String updateDate)
        {
            try
            {
                if (userID.Length > 0)
                {
                    lblLastUpdated.Visible = true;
                    lblBy.Visible = true;
                    txtLastUpdated.Visible = true;
                    txtBy.Visible = true;
                    txtLastUpdated.Text = updateDate;
                    txtBy.Text = userID;
                }
                else
                {
                    lblLastUpdated.Visible = false;
                    lblBy.Visible = false;
                    txtLastUpdated.Visible = false;
                    txtBy.Visible = false;
                    txtLastUpdated.Text = "";
                    txtBy.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void saveExportTarget()
        {

            QueryTemplateInfo queryTemplate = new QueryTemplateInfo();
            if (tcoAnalysis.SelectedIndex == 0)   //data tab
            {
                //10/18/21 - added check for null
                if (lueQueryTemplate.EditValue != null)

                {
                    if ((Int32)lueQueryTemplate.EditValue == 0) return;
                    queryTemplate = AIMManager.GetQueryTemplate((Int32)lueQueryTemplate.EditValue);
                    if (rdbSpecificOverlay.Checked == true)
                    {                        
                        queryTemplate.ExportTargetID = ExportTargetEnum.SpecificOverlay;
                        
                        queryTemplate.AIMOwnerLocationID = (Int32)lueCFCPlant.EditValue;

                        //10/11/23 wait on this
                        //if (checkedComboBoxEdit1.Text == "Multiple Plants")
                        //{
                        //    MessageBox.Show("Unable to save template when multiple plants are selected.", "Warning...");
                        //    return;
                        //}
                        //else
                        //{
                        //    queryTemplate.AIMOwnerLocationID = Convert.ToInt32(checkedComboBoxEdit1.EditValue);
                        //}
                        
                        
                    }
                    else if (rdbDefaultOverlay.Checked == true)
                    {
                        queryTemplate.ExportTargetID = ExportTargetEnum.DefaultOverlay;
                        queryTemplate.AIMOwnerLocationID = 0;
                    }
                    else
                    {
                        queryTemplate.ExportTargetID = ExportTargetEnum.Base;
                        queryTemplate.AIMOwnerLocationID = 0;
                    }
                    AIMManager.SaveQueryTemplate(ref queryTemplate);
                }
            }
            else if (tcoAnalysis.SelectedIndex == 1)  //query tab
            {
                if ((Int32)lueQueryTemplateInTab.EditValue == 0) return;

                queryTemplate = AIMManager.GetQueryTemplate(_selectedQueryTemplateID);
                if (rdbQuerySpecificOverlay.Checked == true)
                {
                    queryTemplate.ExportTargetID = ExportTargetEnum.SpecificOverlay;
                    queryTemplate.AIMOwnerLocationID = (Int32)lueQueryAIMOwnerLocation.EditValue;
                }
                else if (rdbQueryDefaultOverlay.Checked == true)
                {
                    queryTemplate.ExportTargetID = ExportTargetEnum.DefaultOverlay;
                    queryTemplate.AIMOwnerLocationID = 0;
                }

                //10/18/23
                else if (rdbQueryBaseGroup.Checked == true)
                {
                    queryTemplate.ExportTargetID = ExportTargetEnum.BaseExpand;
                    queryTemplate.AIMOwnerLocationID = 0;
                }

                else
                {
                    queryTemplate.ExportTargetID = ExportTargetEnum.Base;
                    queryTemplate.AIMOwnerLocationID = 0;
                }
                AIMManager.SaveQueryTemplate(ref queryTemplate);
            }

        }
        private void bindExportTarget()
        {
            try
            {
                QueryTemplateInfo temp = new QueryTemplateInfo();
                temp = AIMManager.GetQueryTemplate(_selectedQueryTemplateID);

                if (temp == null)
                {
                    rdbQueryBase.Checked = true;
                    lueQueryAIMOwnerLocation.EditValue = 0;

                    rdbBase.Checked = true;
                    lueCFCPlant.EditValue = 0;

                }

                else if (temp.ExportTargetID == ExportTargetEnum.SpecificOverlay)
                {
                    rdbQuerySpecificOverlay.Checked = true;
                    lueQueryAIMOwnerLocation.EditValue = temp.AIMOwnerLocationID;


                    rdbSpecificOverlay.Checked = true;
                    lueCFCPlant.EditValue = temp.AIMOwnerLocationID;
                }
                else if (temp.ExportTargetID == ExportTargetEnum.DefaultOverlay)
                {
                    rdbQueryDefaultOverlay.Checked = true;
                    lueQueryAIMOwnerLocation.EditValue = 0;

                    rdbDefaultOverlay.Checked = true;
                    lueCFCPlant.EditValue = 0;
                }

                //10/18/23
                else if (temp.ExportTargetID == ExportTargetEnum.BaseExpand)
                {
                    rdbQueryBaseGroup.Checked = true;
                    lueQueryAIMOwnerLocation.EditValue = 0;
                    chkExpand.Checked = true;
                    lueCFCPlant.EditValue = 0;
                }

                else
                {
                    rdbQueryBase.Checked = true;
                    lueQueryAIMOwnerLocation.EditValue = 0;

                    rdbBase.Checked = true;
                    lueCFCPlant.EditValue = 0;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void btnAdd_Click(object sender, EventArgs e)
        {            
        }

        private void grdQueryView_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            try
            {
                GridView gv = sender as GridView;

                QueryFeildEnum fieldName;
                Enum.TryParse(gv.GetRowCellValue(e.RowHandle, gv.Columns["QueryFieldID"]).ToString().Trim(), out fieldName);

                QueryOperationEnum operatorName;
                Enum.TryParse(gv.GetRowCellValue(e.RowHandle, gv.Columns["QueryOperationID"]).ToString().Trim(), out operatorName);

                if (e.Column.FieldName == "QueryOperationID")
                {
                    if (gv.GetRowCellValue(e.RowHandle, gv.Columns["QueryOperationID"]) != null)
                    {
                        List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
                        queryOperation = AIMManager.GetQueryOperation(fieldName);
                        queryOperation.Insert
                        (
                            0,
                            new QueryOperationInfo
                            {
                                QueryOperationID = 0,
                                Operation = "--Select--"
                            }
                        );
                        lueQueryOperator.Properties.DataSource = queryOperation;
                        XtraEditors.LoadLookUpEdit(ref lueQueryOperator, "QueryOperationID", "Operation");
                        if (queryOperation.Count > 1)
                        {
                            lueQueryOperator.EditValue = queryOperation[1].QueryOperationID;
                        }
                    }
                }
                if (e.Column.FieldName == "Value")
                {

                    //TODO - ask Mark
                    if (gv.GetRowCellValue(e.RowHandle, gv.Columns["StringDate"]).ToString() != "")
                    {
                        e.RepositoryItem = null;
                    }
                    else
                    {


                        if (gv.GetRowCellValue(e.RowHandle, gv.Columns["Value"]) != null)
                        {
                            switch (fieldName)
                            {
                                case QueryFeildEnum.Owner:

                                    if (e.RowHandle == grdQueryView.FocusedRowHandle)
                                    {
                                        replueValue.DataSource = _aimOwner;
                                        XtraEditors.LoadGridLookUpEdit(ref replueValue, "AIMOwnerID", "OwnerCode");
                                        replueValue.Columns["OwnerName"].Visible = true;
                                        e.RepositoryItem = replueValue;
                                    }
                                    else
                                    {
                                        replueTempValue.DataSource = _aimOwner;
                                        XtraEditors.LoadGridLookUpEdit(ref replueTempValue, "AIMOwnerID", "OwnerCode");
                                        replueTempValue.Columns["OwnerName"].Visible = true;
                                        e.RepositoryItem = replueTempValue;
                                    }
                                    break;
                                case QueryFeildEnum.OwnerLocation:

                                    if (e.RowHandle == grdQueryView.FocusedRowHandle)
                                    {
                                        replueValue.DataSource = _aimOwnerLocation;
                                        XtraEditors.LoadGridLookUpEdit(ref replueValue, "AIMOwnerLocationID", "LocationCode");
                                        replueValue.Columns["LocationName"].Visible = true;
                                        e.RepositoryItem = replueValue;
                                    }
                                    else
                                    {
                                        replueTempValue.DataSource = _aimOwnerLocation;
                                        XtraEditors.LoadGridLookUpEdit(ref replueTempValue, "AIMOwnerLocationID", "LocationCode");
                                        replueTempValue.Columns["LocationName"].Visible = true;
                                        e.RepositoryItem = replueTempValue;
                                    }
                                    break;
                                case QueryFeildEnum.Supplier:

                                    if (e.RowHandle == grdQueryView.FocusedRowHandle)
                                    {
                                        replueValue.DataSource = _aimSupplier;
                                        XtraEditors.LoadGridLookUpEdit(ref replueValue, "AIMSupplierID", "SupplierName");
                                        e.RepositoryItem = replueValue;
                                    }
                                    else
                                    {
                                        replueTempValue.DataSource = _aimSupplier;
                                        XtraEditors.LoadGridLookUpEdit(ref replueTempValue, "AIMSupplierID", "SupplierName");
                                        e.RepositoryItem = replueTempValue;
                                    }
                                    break;
                                case QueryFeildEnum.SupplierLocation:

                                    if (e.RowHandle == grdQueryView.FocusedRowHandle)
                                    {
                                        replueValue.DataSource = _aimSupplierLocation;
                                        XtraEditors.LoadGridLookUpEdit(ref replueValue, "AIMSupplierLocationID", "SupplierLocationName");
                                        e.RepositoryItem = replueValue;
                                    }
                                    else
                                    {
                                        replueTempValue.DataSource = _aimSupplierLocation;
                                        XtraEditors.LoadGridLookUpEdit(ref replueTempValue, "AIMSupplierLocationID", "SupplierLocationName");
                                        e.RepositoryItem = replueTempValue;
                                    }
                                    break;
                                case QueryFeildEnum.Material:

                                    if (e.RowHandle == grdQueryView.FocusedRowHandle)
                                    {
                                        replueValue.DataSource = _aimMaterial;
                                        XtraEditors.LoadGridLookUpEdit(ref replueValue, "AIMMaterialID", "AIMMaterialCode");
                                        e.RepositoryItem = replueValue;
                                    }
                                    else
                                    {
                                        replueTempValue.DataSource = _aimMaterial;
                                        XtraEditors.LoadGridLookUpEdit(ref replueTempValue, "AIMMaterialID", "AIMMaterialCode");
                                        e.RepositoryItem = replueTempValue;
                                    }
                                    break;
                                case QueryFeildEnum.SampleDate:
                                    if (e.RowHandle == grdQueryView.FocusedRowHandle)
                                    {
                                        e.RepositoryItem = reptxtValue;
                                    }
                                    else
                                    {
                                        e.RepositoryItem = reptxtTempValue;
                                    }
                                    break;
                                case QueryFeildEnum.IngredientCode:

                                    if (e.RowHandle == grdQueryView.FocusedRowHandle)
                                    {
                                        replueValue.DataSource = _cfcIngredient;
                                        XtraEditors.LoadGridLookUpEdit(ref replueValue, "CFCIngredientID", "CFCIngredientName");
                                        e.RepositoryItem = replueValue;
                                    }
                                    else
                                    {
                                        replueTempValue.DataSource = _cfcIngredient;
                                        XtraEditors.LoadGridLookUpEdit(ref replueTempValue, "CFCIngredientID", "CFCIngredientName");
                                        e.RepositoryItem = replueTempValue;
                                    }
                                    break;
                                case QueryFeildEnum.NutrientCode:
                                    if (e.RowHandle == grdQueryView.FocusedRowHandle)
                                    {
                                        replueValue.DataSource = _aimNutrient;
                                        XtraEditors.LoadGridLookUpEdit(ref replueValue, "AIMNutrientID", "AIMNutrientCode");
                                        e.RepositoryItem = replueValue;
                                    }
                                    else
                                    {
                                        replueTempValue.DataSource = _aimNutrient;
                                        XtraEditors.LoadGridLookUpEdit(ref replueTempValue, "AIMNutrientID", "AIMNutrientCode");
                                        e.RepositoryItem = replueTempValue;
                                    }
                                    break;

                            }
                        }
                    }
                }
                if (e.Column.FieldName == "DateValue")
                {
                    //if (gv.GetRowCellValue(e.RowHandle, gv.Columns["DateValue"]) != null)
                    //{
                    switch (fieldName)
                    {
                        case QueryFeildEnum.SampleDate:
                            if (
                                  operatorName == QueryOperationEnum.Equal
                                || operatorName == QueryOperationEnum.GreaterThan
                                || operatorName == QueryOperationEnum.GreaterThanEqual
                                || operatorName == QueryOperationEnum.LessThan
                                || operatorName == QueryOperationEnum.LessThanEqual
                                )
                            {
                                if (e.RowHandle == grdQueryView.FocusedRowHandle)
                                {
                                    e.RepositoryItem = repdateValue;

                                }
                                else
                                {
                                    e.RepositoryItem = repdateTempValue;
                                }
                            }
                            else
                            {
                                e.RepositoryItem = null;
                            }
                            break;
                        default:
                            e.RepositoryItem = null;
                            break;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("we are inside cell edit! " + ex.Message + "Data: " + ex.Data);
            }
        }

        private void grdQueryView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            try
            {
                if (_isFormLoaded == true)
                {
                    QueryTemplateDetailInfo queryDetail = new QueryTemplateDetailInfo();
                    Int32 rowHandle = 0;

                    queryDetail = (QueryTemplateDetailInfo)grdQueryView.GetFocusedRow();
                    rowHandle = grdQueryView.FocusedRowHandle;
                    AIMManager.SaveQueryTemplateDetail(ref queryDetail);
                    dataBindSelectedQueryDetail();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isAnalysis == false)
                {
                    MessageBox.Show("This template was created in Reports and cannot be modified here. ", "Warning...");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Delete Template?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult != DialogResult.Yes)
                {
                    return;
                }
                AIMManager.DeleteQueryTemplate((Int32)lueQueryTemplateInTab.EditValue);
                dataBindTemplateDropdowns();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteSetting_Click(object sender, EventArgs e)
        {
            try
            {
                QueryTemplateInfo analysisTemplate = (QueryTemplateInfo)lueQueryTemplateInTab.GetSelectedDataRow();
                if (analysisTemplate != null && analysisTemplate.QueryTemplateID > 0)
                {
                    if (analysisTemplate.IsAnalysis == true)
                    {
                        AIMManager.DeleteQueryTemplate(analysisTemplate.QueryTemplateID);
                        showLastUpdate("", "");
                    }
                    else
                    {

                        MessageBox.Show("This template was created in Reports and cannot be deleted here. ", "Warning...");
                        return;
                    }

                    dataBindLue();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateCurrentViewTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isFormLoaded == false)
                {
                    return;
                }

                //No query was selected so open view template screen 
                if ((Int32)lueViewTemplate.EditValue == 0)
                {
                    AIMGlobal.saveNewViewTemplate = true;
                    _saveViewTemplateAs = false;
                    pnlExistingViewTemplate.Visible = false;
                    pnlViewButtons.Visible = false;
                    pnlNewViewTemplate.Visible = true;

                    tcoAnalysis.SelectedIndex = 2;
                    tcoAnalysis_SelectedIndexChanged(sender, e);

                    txtNewViewTemplateName.Focus();
                    txtNewViewTemplateName.Select();

                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Overwrite " + lueViewTemplate.Text.ToString().TrimEnd() + "?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        //getting layout in stream
                        Stream stream = new System.IO.MemoryStream();
                        grdViewAnalyst.SaveLayoutToStream(stream);
                        stream.Position = 0;

                        //saving layout
                        AnalysisViewTemplateInfo viewTemplate = new AnalysisViewTemplateInfo();
                        viewTemplate = (AnalysisViewTemplateInfo)lueViewTemplate.GetSelectedDataRow();
                        StreamReader streamReader = new StreamReader(stream);
                        viewTemplate.AnalysisViewTemplateXML = streamReader.ReadToEnd();
                        viewTemplate.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                        stream.Dispose();
                        AIMManager.SaveAnalysisViewTemplate(ref viewTemplate);
                        _mainMenu.MessageBoxCFCshow("View template has been updated.", true);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    AIMGlobal.saveNewViewTemplate = true;
                    _saveViewTemplateAs = false;
                    pnlExistingViewTemplate.Visible = false;
                    pnlViewButtons.Visible = false;
                    pnlNewViewTemplate.Visible = true;
                    tcoAnalysis.SelectedIndex = 2;
                    tcoAnalysis_SelectedIndexChanged(sender, e);

                    txtNewViewTemplateName.Focus();
                    txtNewViewTemplateName.Select();

                    return;
                }
            }
            catch (Exception ex)
            {
                _mainMenu.MessageBoxCFCshow("View template did not get updated because of an internal problem.", false);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNewViewTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                _saveViewTemplateAs = false;
                pnlExistingViewTemplate.Visible = false;
                pnlViewButtons.Visible = false;
                pnlNewViewTemplate.Visible = true;

                txtNewViewTemplateName.Focus();
                txtNewViewTemplateName.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveViewTemplateAs_Click(object sender, EventArgs e)
        {
            try
            {
                AnalysisViewTemplateInfo temp = new AnalysisViewTemplateInfo();
                temp = (AnalysisViewTemplateInfo)lueViewTemplateInTab.GetSelectedDataRow();
                if (temp == null)
                {
                    return;
                }
                if (temp.AnalysisViewTemplateID == 0)
                {
                    MessageBox.Show("Please select the template first.", "Warning...", MessageBoxButtons.OK);
                    return;
                }
                _saveViewTemplateAs = true;
                pnlExistingViewTemplate.Visible = false;
                pnlViewButtons.Visible = false;
                pnlNewViewTemplate.Visible = true;

                txtNewViewTemplateName.Focus();
                txtNewViewTemplateName.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnViewTemplateSave_Click(object sender, EventArgs e)
        {
            try
            {
                int newRecID = 0;
                if (txtNewViewTemplateName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please provide a template name.", "Warning...", MessageBoxButtons.OK);
                    return;
                }
                if (txtNewViewTemplateName.Text.Trim().Length > 30)
                {
                    MessageBox.Show("Template name must be 30 characters or less.", "Warning...", MessageBoxButtons.OK);
                    return;
                }

                //verify template name does not already exist 
                List<AnalysisViewTemplateNameInfo> viewTemplateName = new List<AnalysisViewTemplateNameInfo>();

                //4/12/18
                //viewTemplateName = AIMManager.GetTemplateName(txtNewViewTemplateName.Text.Trim());
                viewTemplateName = AIMManager.GetTemplateName(txtNewViewTemplateName.Text.Trim(), (Int32)EnumReportDisplayType.None);

                if (viewTemplateName.Count != 0)
                {
                    MessageBox.Show("This template name already exists.  Please pick another name.", "Warning...", MessageBoxButtons.OK);
                    lueViewTemplate.Text = String.Empty;
                    lueViewTemplate.Focus();
                    return;
                }

                if (_saveViewTemplateAs == false)
                {
                    AnalysisViewTemplateInfo viewTemplate = new AnalysisViewTemplateInfo();
                    viewTemplate.AnalysisViewTemplateID = 0;
                    viewTemplate.AnalysisViewTemplateName = txtNewViewTemplateName.Text.Trim();
                    viewTemplate.AnalysisViewTemplateXML = "";
                    viewTemplate.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;

                    //4/4/18
                    viewTemplate.GridNumber = (Int32)EnumGridNumber.AnalysisIngredient;

                    //4/12/18  TemplateType 0 = none; TemplateType 1 = gridControl; TemplateType 2 = pivotControl
                    viewTemplate.TemplateType = 0;

                    newRecID = AIMManager.SaveAnalysisViewTemplate(ref viewTemplate).RecordID;

                    if (newRecID == 0)
                    {
                        //couldn't save the template, error message will be in the data layer
                        pnlNewViewTemplate.Visible = false;
                        pnlExistingViewTemplate.Visible = true;
                        pnlViewButtons.Visible = true;
                        return;
                    }


                    if (AIMGlobal.saveNewViewTemplate == true)
                    {
                        //getting layout in stream
                        Stream stream = new System.IO.MemoryStream();
                        grdViewAnalyst.SaveLayoutToStream(stream);
                        stream.Position = 0;

                        StreamReader streamReader = new StreamReader(stream);
                        viewTemplate.AnalysisViewTemplateID = newRecID;
                        viewTemplate.AnalysisViewTemplateName = txtNewViewTemplateName.Text;
                        viewTemplate.AnalysisViewTemplateXML = streamReader.ReadToEnd();
                        viewTemplate.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;

                        //4/4/18
                        viewTemplate.GridNumber = (Int32)EnumGridNumber.AnalysisIngredient;

                        //4/12/18  TemplateType 0 = none; TemplateType 1 = gridControl; TemplateType 2 = pivotControl
                        viewTemplate.TemplateType = 0;

                        viewTemplate.IsActive = true;
                        stream.Dispose();
                        AIMManager.SaveAnalysisViewTemplate(ref viewTemplate);
                    }

                }
                if (_saveViewTemplateAs == true)
                {
                    AnalysisViewTemplateInfo viewTemplateForSaveAs = new AnalysisViewTemplateInfo();
                    viewTemplateForSaveAs = (AnalysisViewTemplateInfo)lueViewTemplateInTab.GetSelectedDataRow();
                    viewTemplateForSaveAs.AnalysisViewTemplateID = 0;
                    viewTemplateForSaveAs.AnalysisViewTemplateName = txtNewViewTemplateName.Text.Trim();
                    viewTemplateForSaveAs.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;

                    //4/4/18
                    viewTemplateForSaveAs.GridNumber = (Int32)EnumGridNumber.AnalysisIngredient;

                    //4/12/18  TemplateType 0 = none; TemplateType 1 = gridControl; TemplateType 2 = pivotControl
                    viewTemplateForSaveAs.TemplateType = 0;

                    newRecID = AIMManager.SaveAnalysisViewTemplate(ref viewTemplateForSaveAs).RecordID;
                }
                dataBindLue();

                lueViewTemplateInTab.EditValue = newRecID;
                pnlNewViewTemplate.Visible = false;
                pnlExistingViewTemplate.Visible = true;
                pnlViewButtons.Visible = true;
                txtNewViewTemplateName.Text = "";
                tcoAnalysis.SelectedIndex = 0;   //data tab

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteViewTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                AnalysisViewTemplateInfo viewTemplate = (AnalysisViewTemplateInfo)lueViewTemplateInTab.GetSelectedDataRow();
                if (viewTemplate == null || viewTemplate.AnalysisViewTemplateID == 0)
                {
                    return;
                }
                AIMManager.DeleteAnalysisViewTemplate(ref viewTemplate);
                dataBindLue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelViewTemplateSave_Click(object sender, EventArgs e)
        {
            try
            {
                pnlNewViewTemplate.Visible = false;
                pnlExistingViewTemplate.Visible = true;
                pnlViewButtons.Visible = true;
                txtNewViewTemplateName.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tcoAnalysis_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbAllRecords.Checked = true;

            ToolTip ttip = new ToolTip();
            try
            {
                if (_selectedQueryTemplateID != 0 && PreviousTab.Text.ToString() == "Query Template" && rdbQuerySpecificOverlay.Checked == true && (Int32)lueQueryAIMOwnerLocation.EditValue == 0)
                {
                    MessageBox.Show("Please select specific location.", "Warning...");
                    tcoAnalysis.SelectedIndex = 1;
                    lueQueryTemplateInTab.EditValue = _selectedQueryTemplateID;
                    if (_selectedQueryTemplateID > 0)
                    {
                        pnlNewLine.Visible = true;
                        grdQuery.Visible = true;
                    }
                    else
                    {
                        pnlNewLine.Visible = false;
                        grdQuery.Visible = false;
                    }
                }

                switch (tcoAnalysis.SelectedIndex)
                {
                    case 0:

                        if (cbRecalculateRightAway.Checked == true)
                        {
                            dataBindGroupBy(false);
                        }

                        removeZeroAimMaterialNutrientRecords();

                        lueQueryTemplate.EditValue = _selectedQueryTemplateID; //this will hit DatabindGroupBy
                        lueViewTemplate.EditValue = _selectedViewTemplateID;

                        break;
                    case 1:
                        lueQueryTemplateInTab.EditValue = _selectedQueryTemplateID;

                        if (_selectedQueryTemplateID > 0)
                        {
                            pnlNewLine.Visible = true;
                            grdQuery.Visible = true;
                        }
                        else
                        {
                            pnlNewLine.Visible = false;
                            grdQuery.Visible = false;
                        }

                        //2/23/21
                        tabControlQuery.Visible = true;
                        if ((Int32)lueQueryTemplate.EditValue == 0)
                        {
                            tabControlQuery.Visible = false;
                        }



                        break;
                    case 2:
                        lueViewTemplateInTab.EditValue = _selectedViewTemplateID;

                        ttip.SetToolTip(btnNewViewTemplate, " Add New View");
                        ttip.SetToolTip(btnSaveViewTemplateAs, "Save View As");

                        break;
                    case 3:
                        dataBindLog();
                        break;

                }
                if (tcoAnalysis.SelectedIndex == 0)
                {
                    pnlTop.Visible = true;
                }
                else
                {
                    pnlTop.Visible = false;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueViewTemplateInTab_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueViewTemplateInTab.EditValue != null)
                {
                    _selectedViewTemplateID = (Int32)lueViewTemplateInTab.EditValue;
                    lueViewTemplate.EditValue = _selectedViewTemplateID;
                    applyViewTemplate();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion



        #region private functions
        private void backgroundWorkerDataBindGroupBy_DoWork(object sender, DoWorkEventArgs e)
        {
            AnalysisQueryEnum analysisQuery = AnalysisQueryEnum.NoGroup;
            OutlierTypeEnum outlierType = OutlierTypeEnum.NoOutlierExcluded;
            Int32 queryTemplateID;

            DateTime? startDate = null;
            DateTime? endDate = null;

            try
            {
                queryTemplateID = _selectedQueryTemplateID;

                //10/26/21 - changed date fields from DateTimePicker to DevExpress DateEdit (resize issue)
                //startDate = dtpStart.Value;
                //endDate = dtpEnd.Value;
                startDate = deStart.DateTime;
                endDate = deEnd.DateTime;

                if (_currentStartDate != startDate || _currentEndDate != endDate || _currentQueryTemplateID != queryTemplateID)
                {
                    AIMManager.DeleteSampleAIMRefCFCUser(_userGlobal.CFCLoginUser.CFCUserID, SampleAIMRefCFCUserEnum.CFCUserID);
                    _currentStartDate = Convert.ToDateTime(startDate);
                    _currentEndDate = Convert.ToDateTime(endDate);
                    _currentQueryTemplateID = queryTemplateID;
                }

                if (rbMildOutlierInclude.Checked == false)
                {
                    outlierType = OutlierTypeEnum.ExtremeAndMildExcluded;
                }
                else if (rbExtremeOutlierExclude.Checked == true)
                {
                    outlierType = OutlierTypeEnum.ExtremeExcluded;
                }

                if (cbSampleID.Checked == true)
                {
                    analysisQuery = AnalysisQueryEnum.SampleID;
                }
                else if (cbDate.Checked == true)
                {
                    analysisQuery = AnalysisQueryEnum.SampleDate;

                    if (cbOwnerLocation.Checked == true)
                    {
                        if (cbSupplierLocation.Checked == true)
                        {
                            analysisQuery = AnalysisQueryEnum.SampleDateOwnerLocationSupplierLocation;
                        }
                        else if (cbSupplier.Checked == true)
                        {
                            analysisQuery = AnalysisQueryEnum.SampleDateOwnerLocationSupplier;
                        }
                        else
                        {
                            analysisQuery = AnalysisQueryEnum.SampleDateOwnerLocation;
                        }
                    }
                    else if (cbOwner.Checked == true)
                    {
                        if (cbSupplierLocation.Checked == true)
                        {
                            analysisQuery = AnalysisQueryEnum.SampleDateOwnerSupplierLocation;
                        }
                        else if (cbSupplier.Checked == true)
                        {
                            analysisQuery = AnalysisQueryEnum.SampleDateOwnerSupplier;
                        }
                        else
                        {
                            analysisQuery = AnalysisQueryEnum.SampleDateOwner;
                        }
                    }
                    else if (cbSupplierLocation.Checked == true)
                    {
                        analysisQuery = AnalysisQueryEnum.SampleDateSupplierLocation;
                    }
                    else if (cbSupplier.Checked == true)
                    {
                        analysisQuery = AnalysisQueryEnum.SampleDateSupplier;
                    }
                }
                else if (cbOwnerLocation.Checked == true)
                {
                    if (cbSupplierLocation.Checked == true)
                    {
                        analysisQuery = AnalysisQueryEnum.OwnerLocationSupplierLocation;
                    }
                    else if (cbSupplier.Checked == true)
                    {
                        analysisQuery = AnalysisQueryEnum.OwnerLocationSupplier;
                    }
                    else
                    {
                        analysisQuery = AnalysisQueryEnum.OwnerLocation;
                    }
                }
                else if (cbOwner.Checked == true)
                {
                    if (cbSupplierLocation.Checked == true)
                    {
                        analysisQuery = AnalysisQueryEnum.OwnerSupplierLocation;
                    }
                    else if (cbSupplier.Checked == true)
                    {
                        analysisQuery = AnalysisQueryEnum.OwnerSupplier;
                    }
                    else
                    {
                        analysisQuery = AnalysisQueryEnum.Owner;
                    }
                }
                else if (cbSupplierLocation.Checked == true)
                {
                    analysisQuery = AnalysisQueryEnum.SupplierLocation;
                }
                else if (cbSupplier.Checked == true)
                {
                    analysisQuery = AnalysisQueryEnum.Supplier;
                }

                Int32 plant;

                Int32.TryParse(lueCFCPlant.EditValue.ToString(), out plant);
                e.Result = AIMManager.GetAnalysis(analysisQuery, queryTemplateID, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), rdbDefaultOverlay.Checked, (Int32)_mainMenu.lueDataMap.EditValue, outlierType, plant);


                _isFormLoaded = true;

                AIMGlobal.chartAnalysisQueryType = analysisQuery;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundWorkerDataBindGroupBy_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                grdAnalyst.DataSource = e.Result;

                //7/21/21 - hide sampleID and sampleResultID on grid
                foreach (var column in grdViewAnalyst.Columns)
                {
                    if (((DevExpress.XtraGrid.Columns.GridColumn)column).FieldName == "SampleID")
                    {
                        ((DevExpress.XtraGrid.Columns.GridColumn)column).Visible = false;
                    }
                    else if (((DevExpress.XtraGrid.Columns.GridColumn)column).FieldName == "SampleResultID")
                    {
                        ((DevExpress.XtraGrid.Columns.GridColumn)column).Visible = false;
                    }
                }


                checkOutliersOnly();

                grdViewAnalyst.FocusedRowHandle = _currentRow;
                gbGroupBy.Enabled = true;
                grdViewAnalyst.OptionsBehavior.Editable = true;
                grdViewAnalyst.ExpandGroupRow(_currentRow);
                //removelblRecordCount.Text = grdViewAnalyst.DataRowCount.ToString() + " Records Found";

                //picFlagRed.Visible = true;
                //lblFlagRed.Visible = true;
                //picFlagYellow.Visible = true;
                //lblFlagYellow.Visible = true;

                AIMGlobal.SplashScreenHide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataBindTemplateLue()
        {

            //2/19/21 commented, no longer used
            //List<QueryFieldInfo> queryField = new List<QueryFieldInfo>();
            //queryField = AIMManager.GetQueryField(true);
            //queryField.Insert
            //(
            //    0,
            //    new QueryFieldInfo
            //    {
            //        QueryFieldID = 0,
            //        Field = "--Select--"
            //    }
            //);
            //lueQueryField.Properties.DataSource = queryField;
            //XtraEditors.LoadLookUpEdit(ref lueQueryField, "QueryFieldID", "Field");

            //2/22/21 commented, no longer used
            //List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
            //queryOperation = AIMManager.GetQueryOperation(true);
            //queryOperation.Insert
            //(
            //    0,
            //    new QueryOperationInfo
            //    {
            //        QueryOperationID = 0,
            //        Operation = "--Select--"
            //    }
            //);
            //lueQueryOperator.Properties.DataSource = queryOperation;
            //XtraEditors.LoadLookUpEdit(ref lueQueryOperator, "QueryOperationID", "Operation");
        }

        //10/17/23
        private void SetUserGroups()
        {
            _user = UserManager.GetUser(_userGlobal.CFCLoginUser.CFCUserID);
            if (_user != null)
            {
                _ownerLocationGroupID = _user.AIMOwnerLocationGroupID;
                _supplierGroupID = _user.AIMSupplierGroupID;
                _supplierLocationGroupID = _user.AIMSupplierLocationGroupID;
            }
        }

        private void dataBindLue()
        {            
                        
            AnalysisSharedFunctions.populateViewTemplate(lueViewTemplate, lueViewTemplateInTab, "I");
            AnalysisSharedFunctions.populateQueryTemplate(lueQueryTemplate, lueQueryTemplateInTab, "I");

            _isFormLoaded = false;

            //-------------------------------------------------------------------------------------            
            _aimOwner = AnalysisSharedFunctions.populateAimOwner(_aimOwner, true);
            _aimOwnerLocation = AnalysisSharedFunctions.populateAimOwnerLocation(_aimOwnerLocation, true);
            _aimSupplier = AnalysisSharedFunctions.populateAimSupplier(_aimSupplier);
            _aimSupplierLocation = AnalysisSharedFunctions.populateAimSupplierLocation(_aimSupplierLocation);
            _aimMaterial = AnalysisSharedFunctions.populateAimMaterial(_aimMaterial, true);
            _aimNutrient = AnalysisSharedFunctions.populateAimNutrient(_aimNutrient, true);

            _aimCFCPlantCrossReference = AnalysisSharedFunctions.populateCFCPlantCrossReference(_aimCFCPlantCrossReference, lueQueryAIMOwnerLocation);
            _missingOwnerLocXRef = false;
            lblMissingOwnerXref.Visible = false;            
            if (_aimCFCPlantCrossReference.Count <= 1)
            {
                _missingOwnerLocXRef = true;
            }
                        
            _cfcPlant = AnalysisSharedFunctions.populateCFCPlant(_cfcPlant, true, lueCFCPlant);

            

                        

            //--------------------------------------------------------------------------
            _cfcIngredient = AIMManager.GetAllCFCIngredientAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);
            _cfcIngredient.Insert
            (
                0,
                new CFCIngredientInfo
                {
                    CFCIngredientID = 0,
                    CFCIngredientCode = "--Select--"
                }
            );

            //--------------------------------------------------------------------------
            btnDeleteQueryTemplate.Visible = false;
            AIMGlobal.SplashScreenHide();
            _isFormLoaded = true;


        }

        private void dataBindTemplateDropdowns()
        {
            //10/31/22
            //AnalysisSharedFunctions.DataBindTemplateDropdowns(lueQueryTemplate, lueQueryTemplateInTab);
            AnalysisSharedFunctions.populateQueryTemplate(lueQueryTemplate, lueQueryTemplateInTab, "I");
        }
        private void dataBindSummary()
        {                        
            try
            {
                //3/2/21
                txtIngredientCode.Text = (String)grdViewAnalyst.GetFocusedRowCellValue(colCFCIngredientCode);

                txtCFCIngredientName.Text = (String)grdViewAnalyst.GetFocusedRowCellValue(colCFCIngredientName);
                txtNutrientCode.Text = (String)grdViewAnalyst.GetFocusedRowCellValue(colCFCNutrientCode);
                txtNutrientName.Text = (String)grdViewAnalyst.GetFocusedRowCellValue(colCFCNutrientName);
                txtFrmPlant.Text = (String)grdViewAnalyst.GetFocusedRowCellValue(colPlantCode);

                //1/25/21
                //txtIngredientID.Text = grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID).ToString();
                if (grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID) == null)
                {
                    txtIngredientID.Text = "";
                }
                else
                {
                    txtIngredientID.Text = grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID).ToString();
                }

                //1/25/21
                //txtNutrientID.Text = grdViewAnalyst.GetFocusedRowCellValue(colAIMNutrientID).ToString();
                if (grdViewAnalyst.GetFocusedRowCellValue(colAIMNutrientID) == null)
                {
                    txtNutrientID.Text = "";
                }
                else
                {
                    txtNutrientID.Text = grdViewAnalyst.GetFocusedRowCellValue(colAIMNutrientID).ToString();
                }


                //10/20/20 - added for charts; ingredientid gets changed below (not sure why), so added separate column for Aim ID
                txtAimMaterialID.Text = String.Empty;

                //1/25/21
                //txtAimMaterialID.Text = grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID).ToString();
                if (grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID) == null)
                {
                    txtAimMaterialID.Text = "";
                }
                else
                {
                    txtAimMaterialID.Text = grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID).ToString();
                }



                if (txtIngredientCode.Text == "")
                {
                    txtIngredientCode.Text = "No XRef";
                    txtIngredientID.Text = "0";
                }
                if (txtCFCIngredientName.Text == "")
                {
                    txtCFCIngredientName.Text = "No XRef";
                }
                if (txtNutrientCode.Text == "")
                {
                    txtNutrientCode.Text = "No XRef";
                    txtNutrientID.Text = "0";
                }
                if (txtNutrientName.Text == "")
                {
                    txtNutrientName.Text = "No XRef";
                }


                txtCurrentValue.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colCurrentNutrientVal)).ToString("N3");
                txtProposeNutrientValue.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colProposedNutrientValue)).ToString("N3");
                txtDifference.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colDifference)).ToString("N3");

                //5/3/16 - change to use proposed/trimmed
                //txtMean.Text = grdViewAnalyst.GetFocusedRowCellValue(colMean).ToString().ToString();				
                txtMean.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colTrimmedMean)).ToString("N3");

                txtStandardDeviation.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colTrimmedStandardDeviation)).ToString("N3");
                txtSafetyMargin.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colSafetyMargin)).ToString("N3");
                txtNumberOfSamples.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colNumberOfSample)).ToString();
                txtTrimmedSampleSize.Text = Convert.ToInt32(grdViewAnalyst.GetFocusedRowCellValue(colTrimmedSampleSize)).ToString();

                //10/2/20
                txtTotalSampleSize.Text = Convert.ToInt32(grdViewAnalyst.GetFocusedRowCellValue(colTotalSampleSize)).ToString();


            }
            catch (Exception)
            {
                txtIngredientCode.Text = String.Empty;
                txtCFCIngredientName.Text = String.Empty;
                txtNutrientCode.Text = String.Empty;
                txtNutrientName.Text = String.Empty;
                txtCurrentValue.Text = String.Empty;
                txtProposeNutrientValue.Text = String.Empty;
                txtDifference.Text = String.Empty;
                txtMean.Text = String.Empty;
                txtStandardDeviation.Text = String.Empty;
                txtSafetyMargin.Text = String.Empty;
                txtNumberOfSamples.Text = String.Empty;
                txtTrimmedSampleSize.Text = String.Empty;

                //10/2/20
                txtTotalSampleSize.Text = String.Empty;
            }

            //3/2/21 - decided to change this back to separate functions for Ingredient and Product
            ////AnalysisSharedFunctions.DataBindSummary(true, grdViewAnalyst, txtAimMaterialID, txtNutrientID, txtNutrientCode, txtNutrientName,
            ////    txtCurrentValue, txtProposeNutrientValue, txtDifference, txtMean, txtStandardDeviation, txtSafetyMargin, txtNumberOfSamples,
            ////    txtTrimmedSampleSize, txtTotalSampleSize, txtIngredientID, txtIngredientCode, txtCFCIngredientName, txtFrmPlant,
            ////    colAIMMaterialID, colAIMNutrientID, colCFCNutrientCode, colCFCNutrientName, colCurrentNutrientVal, colProposedNutrientValue,
            ////    colDifference, colTrimmedMean, colTrimmedStandardDeviation, colSafetyMargin, colNumberOfSample, colTrimmedSampleSize,
            ////    colTotalSampleSize, colCFCIngredientName, colPlantCode);

        }
        private void dataBindGroupBy(Boolean skipLoad)
        {
            if (skipLoad == true)
            {
                return;
            }


            _isFormLoaded = false;
            _currentRow = grdViewAnalyst.FocusedRowHandle;
            AIMGlobal.SplashScreenShow();
            gbGroupBy.Enabled = false;
            grdViewAnalyst.OptionsBehavior.Editable = false;
                        
            //Making sure all are false
            grdViewAnalyst.Columns.ColumnByName("colSampleCode").Visible = false;
            grdViewAnalyst.Columns.ColumnByName("colDate").Visible = false;
            grdViewAnalyst.Columns.ColumnByName("colOwnerCode").Visible = false;
            grdViewAnalyst.Columns.ColumnByName("colOwnerLocationCode").Visible = false;
            grdViewAnalyst.Columns.ColumnByName("colSupplierName").Visible = false;
            grdViewAnalyst.Columns.ColumnByName("colSupplierLocationName").Visible = false;
            grdViewAnalyst.Columns.ColumnByName("colCFCIngredientName").Visible = false;
            grdViewAnalyst.Columns.ColumnByName("colCFCNutrientName").Visible = false;

            //1/28/21
            grdViewAnalyst.Columns.ColumnByName("colOwnerLocationName").Visible = false;


            if (!backgroundWorkerDataBindGroupBy.IsBusy)
            {
                backgroundWorkerDataBindGroupBy.RunWorkerAsync();
            }

            if (cbSampleID.Checked == true)
            {
                grdViewAnalyst.Columns.ColumnByName("colSampleCode").Visible = true;
                grdViewAnalyst.Columns.ColumnByName("colIsInclude").Visible = true;
            }

            if (cbDate.Checked == true)
            {
                grdViewAnalyst.Columns.ColumnByName("colDate").Visible = true;
            }

            if (cbOwner.Checked == true)
            {
                grdViewAnalyst.Columns.ColumnByName("colOwnerCode").Visible = true;
            }

            if (cbOwnerLocation.Checked == true)
            {
                grdViewAnalyst.Columns.ColumnByName("colOwnerCode").Visible = true;
                grdViewAnalyst.Columns.ColumnByName("colOwnerLocationCode").Visible = true;

                //1/28/21
                grdViewAnalyst.Columns.ColumnByName("colOwnerLocationName").Visible = true;
            }

            if (cbSupplier.Checked == true)
            {
                grdViewAnalyst.Columns.ColumnByName("colSupplierName").Visible = true;
            }

            if (cbSupplierLocation.Checked == true)
            {
                grdViewAnalyst.Columns.ColumnByName("colSupplierName").Visible = true;
                grdViewAnalyst.Columns.ColumnByName("colSupplierLocationName").Visible = true;
            }

            if (AIMGlobal.exportbatchMissingIngredientOrNutrient == true)
            {
                grdViewAnalyst.Columns.ColumnByName("colCFCIngredientName").Visible = true;
                grdViewAnalyst.Columns.ColumnByName("colCFCNutrientName").Visible = true;
            }
        }
        private void dataBindLog()
        {
            grdLog.DataSource = AIMManager.GetAllAnalysisLog(true);
        }

        private void dataBindTemplateGrids()
        {
            grdMaterial.DataSource = AIMManager.GetAIMMaterialForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Material);
            grdNutrient.DataSource = AIMManager.GetAIMNutrientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.NutrientCode);
            grdOwner.DataSource = AIMManager.GetAIMOwnerForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Owner);
            grdSupplier.DataSource = AIMManager.GetAIMSupplierForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Supplier);
            grdOwnerLocation.DataSource = AIMManager.GetAIMOwnerLocationForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.OwnerLocation);
            grdSupplierLocation.DataSource = AIMManager.GetAIMSupplierLocationForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SupplierLocation);
            grdMaterialNutrient.DataSource = AIMManager.GetMaterialNutrientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.MaterialNutrient);
            grdIngredient.DataSource = AIMManager.GetCFCIngredientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.IngredientCode);

            //1/25/21
            grdSampleType.DataSource = AIMManager.GetSampleTypeForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleType);
        }

        private void dataBindSampleDate()
        {
            //3/4/21 - removed unnecessary assignment
            //List<QueryTemplateDetailInfo> sampleDate = new List<QueryTemplateDetailInfo>();
            List<QueryTemplateDetailInfo> sampleDate;

            sampleDate = AIMManager.GetAIMSampleDateForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleDate);
            btnRemoveSampleDate.Visible = false;
            if (sampleDate.Count > 0)
            {

                if (sampleDate[0].StringDate != "")
                {
                    dtpValue.Text = sampleDate[0].DateValue.ToString();
                }

                if (sampleDate[0].StringSecondDate != "")
                {
                    dtpSecondValue.Text = sampleDate[0].SecondDate.ToString();
                }

                btnRemoveSampleDate.Visible = true;

                //3/4/21 - removed unnecessary assignment
                //List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
                List<QueryOperationInfo> queryOperation;

                queryOperation = AIMManager.GetQueryOperation(QueryFeildEnum.SampleDate);

                String dateText = "";
                dateText = queryOperation[sampleDate[0].QueryOperationID - 1].Operation;
                setDateTextField((QueryOperationEnum)sampleDate[0].QueryOperationID, sampleDate[0].Value);
            }
        }

        private void dataBindSelectedQueryDetail()
        {
            Int32 fromAnalysisTemplateID;
            _isFormLoaded = false;
            fromAnalysisTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
            grdQuery.DataSource = AIMManager.GetQueryTemplateDetail(fromAnalysisTemplateID);
            _isFormLoaded = true;

            grdQueryView.Columns.ColumnByName("colDelete").Visible = true;
            if (_isAnalysis == false)
            {
                grdQueryView.Columns.ColumnByName("colDelete").Visible = false;
            }

            cboeAndOr.Visible = false;
            lblAndOr.Visible = false;
            if (grdQueryView.DataRowCount != 0)
            {
                cboeAndOr.Visible = true;
                lblAndOr.Visible = true;
            }
        }

        public void SetBtnRecalculate()
        {
            btnRecalculate.BackColor = Color.Yellow;
        }

        //10/26/21 - commented and moved call to GetDateDuration to events under date fields (deStart and deEnd) because of resize issue
        //////10/19/21 - changed to public (to see if this made a difference for Springfield resize problem)
        //////private void validateDates()
        ////public void validateDates()
        ////{
        ////    //2/22/21 - changed to use AnalysisSharedFunctions.ValidateDates below 
        ////    //DateTime startDate = dtpStart.Value;
        ////    //DateTime endDate = dtpEnd.Value;
        ////    //if (startDate.Year < 1980)
        ////    //{
        ////    //    MessageBox.Show("Start date cannot be earlier than 1980. We are changing it for you.", "Warning...");
        ////    //    startDate = Convert.ToDateTime("1/1/1980");
        ////    //    dtpStart.Value = startDate;
        ////    //}
        ////    //txtDuration.Text = "Duration:        " + ((endDate - startDate).Days + 1).ToString() + " Days";

        ////    //10/19/21 - changed to send datetime (instead of DateTimePicker value for start/end dates - because of resize problem in Springfield)
        ////    //DateTime startDate = dtpStart.Value;
        ////    //DateTime endDate = dtpEnd.Value;
        ////    DateTime startDate = deStart.DateTime;
        ////    DateTime endDate = deEnd.DateTime;
        ////    AnalysisSharedFunctions.GetDateDuration(txtDuration, startDate, endDate);
        ////    //10/20/21 - caused looping error
        ////    //dtpStart.Value = startDate;
        ////    //deStart.DateTime = startDate;
        ////}

        private void link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

            TextBrick pageNumberHeading = e.Graph.DrawString("FRM " + this.Text, Color.Black,
               new RectangleF(0, 0, 200, 20), BorderSide.None);

            pageNumberHeading.Location = new Point(800, 100);
        }


        //3/4/21 commented
        //private Boolean saveAnalysisQueryDetail(FunctionCallFromEnum callFrom)
        //{
            //QueryTemplateDetailInfo queryDetail = new QueryTemplateDetailInfo();
            //Int32 queryFieldID = 0;
            //Int32 queryTemplateID = 0;
            //Int32 value = 0;
            //Int32 rowHandle = 0;
            //if (callFrom == FunctionCallFromEnum.Grid)
            //{
            //	queryDetail = (QueryTemplateDetailInfo)grdQueryView.GetFocusedRow();
            //	rowHandle = grdQueryView.FocusedRowHandle;
            //}
            //else if (callFrom == FunctionCallFromEnum.Button)
            //{
            //	if (cboeAndOr.SelectedItem.ToString() == "And")
            //	{
            //		queryDetail.IsAnd = true;
            //	}
            //	else
            //	{
            //		queryDetail.IsAnd = false;
            //	}

            //	queryDetail.QueryTemplateDetailID = 0;
            //	Int32.TryParse(lueQueryField.EditValue.ToString(), out queryTemplateID);
            //	queryDetail.QueryTemplateID = queryTemplateID;
            //	Int32.TryParse(lueQueryField.EditValue.ToString(), out queryFieldID);
            //	queryDetail.QueryFieldID = (QueryFeildEnum)queryFieldID;

            //	switch (queryDetail.QueryFieldID)
            //	{
            //		case QueryFeildEnum.SampleDate:
            //			Int32.TryParse(txtValue.Text, out value);
            //			queryDetail.Value = value;
            //			break;
            //		case QueryFeildEnum.SampleCode:
            //			break;
            //		case QueryFeildEnum.SampleOwner:
            //			break;
            //		case QueryFeildEnum.OwnerLocation:
            //			break;
            //		case QueryFeildEnum.Supplier:
            //			break;
            //		case QueryFeildEnum.SupplierLocation:
            //			break;
            //		case QueryFeildEnum.Ingredient:
            //			break;
            //		case QueryFeildEnum.Product:
            //			break;
            //		case QueryFeildEnum.AnalysisDate:
            //			break;
            //		case QueryFeildEnum.Material:
            //			break;
            //		default:
            //			break;
            //	}
            //}
            //else

            //if (queryDetail.AnalysisQueryDetailAndOr.Trim().Length == 0)
            //{
            //	if (callFrom == FunctionCallFromEnum.Grid && rowHandle != 0)
            //	{
            //		MessageBox.Show("Please, select 'and' or 'or'", "Warning...");
            //		return false;
            //	}
            //}

            //if (queryDetail.AnalysisQueryDetailField.ToLower().Contains("select") == true || queryDetail.AnalysisQueryDetailField.Trim().Length == 0)
            //{
            //	MessageBox.Show("Please select the field.", "Warning...");
            //	return false;
            //}
            //if (queryDetail.AnalysisQueryDetailOperator.Trim().Length == 0)
            //{
            //	MessageBox.Show("Please select an operator.", "Warning...");
            //	return false;
            //}
            //if (queryDetail.AnalysisQueryDetailValue.Trim().Length == 0)
            //{
            //	MessageBox.Show("Please provide a value.", "Warning...");
            //	return false;
            //}
            //Int32 temp = 0;
            //Int32.TryParse(lueQueryTemplateInTab.EditValue.ToString(), out temp);
            //if (temp == 0)
            //{
            //	MessageBox.Show("Please select a query name.");
            //	return false;
            //}
            //queryDetail.AnalysisTemplateID = temp;
            //queryDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            //AIMManager.SaveAnalysisQueryDetail(ref queryDetail);
            //DataBindSelectedQueryDetail();
        //    return true;
        //}

        private void applyViewTemplate()
        {
            if (_isFormLoaded == false)
            {
                return;
            }

            //3/4/21 - removed unnecessary assignment
            //AnalysisViewTemplateInfo viewTemplate = new AnalysisViewTemplateInfo();
            AnalysisViewTemplateInfo viewTemplate;

            viewTemplate = (AnalysisViewTemplateInfo)lueViewTemplate.GetSelectedDataRow();
            if (viewTemplate == null || viewTemplate.AnalysisViewTemplateID == 0)
            {
                return;
            }
            if (viewTemplate.AnalysisViewTemplateXML.Trim().Length == 0)
            {
                return;
            }
            try
            {
                // convert string to stream
                byte[] byteArray = Encoding.UTF8.GetBytes(viewTemplate.AnalysisViewTemplateXML.Trim());
                MemoryStream stream = new MemoryStream(byteArray);
                grdViewAnalyst.RestoreLayoutFromStream(stream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void fillDetail(ref List<GraphInfo> details, String valueName, Double value, Int32 aimMaterID,
                        Int32 aimNutrientID, DateTime sampleDate, Int32 sampleID)
        {
            GraphInfo detail = new GraphInfo();

            detail.IngrendientName = txtCFCIngredientName.Text;
            detail.UserName = _userGlobal.CFCLoginUser.UserName;

            //10/26/21 - changed date fields from DateTimePicker to DevExpress DateEdit (resize issue)
            //detail.StartDate = dtpStart.Value;
            //detail.EndDate = dtpEnd.Value;
            detail.StartDate = deStart.DateTime;
            detail.EndDate = deEnd.DateTime;

            detail.AIMMaterialID = aimMaterID;
            detail.AIMNutrientID = aimNutrientID;
            detail.SampleDate = sampleDate;
            detail.ValueName = valueName;
            detail.Value = Math.Round(value, 3);
            detail.SampleID = sampleID;
            details.Add(detail);
        }
        #endregion

        private void btnGraph_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.chartAimNutrientID = 0;
                AIMGlobal.chartAimMaterialID = 0;
                Int32.TryParse(txtNutrientID.Text.ToString(), out AIMGlobal.chartAimNutrientID);
                Int32.TryParse(txtAimMaterialID.Text.ToString(), out AIMGlobal.chartAimMaterialID);
                Int32.TryParse(lueQueryTemplate.EditValue.ToString(), out AIMGlobal.chartQueryTemplateID);
                AIMGlobal.chartBegDate = deStart.DateTime;
                AIMGlobal.chartEndDate = deEnd.DateTime;

                //2/18/21
                //-------------------------------------------------------------------------------------------
                // - when query template is used (gpbDate.visible = false), date fields were not getting set correctly (or at all)
                if (gpbDate.Visible == false)
                {
                    AIMGlobal.chartBegDate = Convert.ToDateTime("1/1/2000");
                    AIMGlobal.chartEndDate = DateTime.Now;

                    if (AIMGlobal.chartQueryTemplateID > 0)
                    {
                        //call stp to return begin/end dates
                        QueryOperationSampleDateInfo dates = new QueryOperationSampleDateInfo();
                        dates = AIMManager.GetTemplateSampleDates(AIMGlobal.chartQueryTemplateID);

                        if (dates != null)
                        {
                            AIMGlobal.chartBegDate = dates.DateValue;
                            AIMGlobal.chartEndDate = dates.SecondDateValue;
                        }                                            
                    }                                      
                }
                //-------------------------------------------------------------------------------------------

                AIMGlobal.comingFromAnalysis = true;
                                
                //12/23/22
                AIMGlobal.fromAnalysisIngredientOrProduct = (Int32)EnumIngredientOrProduct.AnalysisIngredient;

                AIMGlobal.analysisAllRecords = rbAllRecords.Checked;
                AIMGlobal.analysisOutliersOnly = rbOutliersOnly.Checked;
                AIMGlobal.analysisExcludeExtremeOutliers = rbExtremeOutlierExclude.Checked;
                AIMGlobal.analysisIncludeExtremeOutliers = rbExtremeOutlierInclude.Checked;
                AIMGlobal.analysisExcludeMildOutliers = rbMildOutlierExclude.Checked;
                AIMGlobal.analysisIncludeMildOutliers = rbMildOutlierInclude.Checked;

                if (cbSampleID.Checked == true)
                {
                    AIMGlobal.chartAnalysisQueryType = AnalysisQueryEnum.SampleID;
                }
                else
                {
                    AIMGlobal.chartAnalysisQueryType = AnalysisQueryEnum.SampleDate;
                }                               
                AIMGlobal.open_form(charts = new Charts());
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ////TODO Rename report
            //TestMark devDate = new TestMark();
            //SampleLineReport devSample = new SampleLineReport();
            //List<AnalysisInfo> analysis = new List<AnalysisInfo>();
            //GraphHeaderInfo header = new GraphHeaderInfo();
            //List<GraphInfo> details = new List<GraphInfo>();
            //GraphInfo detail = new GraphInfo();
            //Int32 aimMaterID = 0;
            //Int32 aimNutrientID = 0;
            //Int32 sampleID = 0;

            //try
            //{
            //	if (grdViewAnalyst.DataSource != null)
            //	{
            //		Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID).ToString(), out aimMaterID);
            //		Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colAIMNutrientID).ToString(), out aimNutrientID);
            //		analysis = (List<AnalysisInfo>)grdAnalyst.DataSource;
            //		foreach (AnalysisInfo item in analysis)
            //		{
            //			if (aimMaterID == item.AIMMaterialID && aimNutrientID == item.AIMNutrientID)
            //			{
            //				Int32.TryParse(item.SampleID.ToString(), out sampleID);
            //				FillDetail(ref details, item.AIMNutrientName, item.SampleValue, aimMaterID, aimNutrientID, item.SampleDate, sampleID);
            //				FillDetail(ref details, "Mean", item.ProposedMean, aimMaterID, aimNutrientID, item.SampleDate, sampleID);
            //				FillDetail(ref details, "Upper Outlier", item.OutlierUpperExtreme, aimMaterID, aimNutrientID, item.SampleDate, sampleID);
            //				FillDetail(ref details, "Lower Outlier", item.OutlierLowerExtreme, aimMaterID, aimNutrientID, item.SampleDate, sampleID);
            //				FillDetail(ref details, "Proposed Value", item.ProposedNutrientValue, aimMaterID, aimNutrientID, item.SampleDate, sampleID);
            //			}
            //		}

            //		if (cbSampleID.Checked)
            //		{
            //			devSample.DataSource = details;
            //			devSample.PrinterName = PrinterSetup.GetPrinter(PrinterTypeEnum.Other);
            //			ReportPrintTool tool = new ReportPrintTool(devSample);
            //			tool.ShowPreviewDialog();
            //		}
            //		else
            //		{
            //			devDate.DataSource = details;
            //			devDate.PrinterName = PrinterSetup.GetPrinter(PrinterTypeEnum.Other);
            //			ReportPrintTool tool = new ReportPrintTool(devDate);
            //			tool.ShowPreviewDialog();
            //		}
            //	}
            //}
            //catch (Exception ex)
            //{
            //	MessageBox.Show(ex.Message);
            //}
        }

        private void relinkNumberOfSamples()
        {
            try
            {
                OutlierTypeEnum outlierType = new OutlierTypeEnum();
                AnalysisQueryEnum analysisQuery = AnalysisQueryEnum.NoGroup;
                Int32 queryTemplateID = 0;
                Int32 aimMaterialID = 0;
                Int32 aimNutrientID = 0;                
                Int32 sampleID = 0;
                Int32 aimOwnerID = 0;
                Int32 aimOwnerLocationID = 0;
                Int32 aimSupplierID = 0;
                Int32 aimSupplierLocationID = 0;
                Int32 cfcPlantID = 0;
                Int32 gWarning = 0;
                Int32 limitWarning = 0;
                DateTime sampleDate = new DateTime();
                String sampleCode = "";

                //10/26/21 - changed date fields from DateTimePicker to DevExpress DateEdit (resize issue)
                //DateTime startDate = dtpStart.Value;
                //DateTime endDate = dtpEnd.Value;
                DateTime startDate = deStart.DateTime;
                DateTime endDate = deEnd.DateTime;

                Int32.TryParse(lueQueryTemplate.EditValue.ToString(), out queryTemplateID);
                Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID).ToString(), out aimMaterialID);
                Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colAIMNutrientID).ToString(), out aimNutrientID);
                outlierType = OutlierTypeEnum.NoOutlierExcluded;
                Int32 cfcIngredientID = 0;


                AIMGlobal.SplashScreenShow();

                if (rbMildOutlierExclude.Checked == true)
                {
                    outlierType = OutlierTypeEnum.ExtremeAndMildExcluded;
                }
                else if (rbExtremeOutlierExclude.Checked == true)
                {
                    outlierType = OutlierTypeEnum.ExtremeExcluded;
                }
                AIMGlobal.chartOutlierType = outlierType;

                if (cbSampleID.Checked == true)
                {
                    analysisQuery = AnalysisQueryEnum.SampleID;
                }
                else if (cbDate.Checked == true)
                {
                    analysisQuery = AnalysisQueryEnum.SampleDate;

                    if (cbOwnerLocation.Checked == true)
                    {
                        if (cbSupplierLocation.Checked == true)
                        {
                            analysisQuery = AnalysisQueryEnum.SampleDateOwnerLocationSupplierLocation;
                        }
                        else if (cbSupplier.Checked == true)
                        {
                            analysisQuery = AnalysisQueryEnum.SampleDateOwnerLocationSupplier;
                        }
                        else
                        {
                            analysisQuery = AnalysisQueryEnum.SampleDateOwnerLocation;
                        }
                    }
                    else if (cbOwner.Checked == true)
                    {
                        if (cbSupplierLocation.Checked == true)
                        {
                            analysisQuery = AnalysisQueryEnum.SampleDateOwnerSupplierLocation;
                        }
                        else if (cbSupplier.Checked == true)
                        {
                            analysisQuery = AnalysisQueryEnum.SampleDateOwnerSupplier;
                        }
                        else
                        {
                            analysisQuery = AnalysisQueryEnum.SampleDateOwner;
                        }
                    }
                    else if (cbSupplierLocation.Checked == true)
                    {
                        analysisQuery = AnalysisQueryEnum.SampleDateSupplierLocation;
                    }
                    else if (cbSupplier.Checked == true)
                    {
                        analysisQuery = AnalysisQueryEnum.SampleDateSupplier;
                    }


                }
                else if (cbOwnerLocation.Checked == true)
                {
                    if (cbSupplierLocation.Checked == true)
                    {
                        analysisQuery = AnalysisQueryEnum.OwnerLocationSupplierLocation;
                    }
                    else if (cbSupplier.Checked == true)
                    {
                        analysisQuery = AnalysisQueryEnum.OwnerLocationSupplier;
                    }
                    else
                    {
                        analysisQuery = AnalysisQueryEnum.OwnerLocation;
                    }
                }
                else if (cbOwner.Checked == true)
                {
                    if (cbSupplierLocation.Checked == true)
                    {
                        analysisQuery = AnalysisQueryEnum.OwnerSupplierLocation;
                    }
                    else if (cbSupplier.Checked == true)
                    {
                        analysisQuery = AnalysisQueryEnum.OwnerSupplier;
                    }
                    else
                    {
                        analysisQuery = AnalysisQueryEnum.Owner;
                    }
                }
                else if (cbSupplierLocation.Checked == true)
                {
                    analysisQuery = AnalysisQueryEnum.SupplierLocation;
                }
                else if (cbSupplier.Checked == true)
                {
                    analysisQuery = AnalysisQueryEnum.Supplier;
                }

                if (cbDate.Checked == true && grdViewAnalyst.GetFocusedRowCellValue(colDate) != null)
                {
                    DateTime.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colDate).ToString(), out sampleDate);
                }
                if (cbSampleID.Checked == true && grdViewAnalyst.GetFocusedRowCellValue(colSampleID) != null)
                {                    
                    Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colSampleID).ToString(), out sampleID);
                }
                if (cbOwner.Checked == true && grdViewAnalyst.GetFocusedRowCellValue(colAIMOwnerID) != null)
                {
                    Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colAIMOwnerID).ToString(), out aimOwnerID);
                }
                if (cbOwnerLocation.Checked == true && grdViewAnalyst.GetFocusedRowCellValue(colAIMOwnerLocationID) != null)
                {
                    Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colAIMOwnerLocationID).ToString(), out aimOwnerLocationID);
                }
                if (cbSupplier.Checked == true && grdViewAnalyst.GetFocusedRowCellValue(colAIMSupplierID) != null)
                {
                    Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colAIMSupplierID).ToString(), out aimSupplierID);
                }
                if (cbSupplierLocation.Checked == true && grdViewAnalyst.GetFocusedRowCellValue(colAIMSupplierLocationID) != null)
                {
                    Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colAIMSupplierLocationID).ToString(), out aimSupplierLocationID);
                }
                if (cbOwnerLocation.Checked == true && grdViewAnalyst.GetFocusedRowCellValue(colCFCPlantID) != null)
                {
                    Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colCFCPlantID).ToString(), out cfcPlantID);
                }


                analysisQuery = AnalysisQueryEnum.SampleID;

                Int32 plant;
                Int32.TryParse(lueCFCPlant.EditValue.ToString(), out plant);

                //6/28/21
                //AIMGlobal.sampleDetail = AIMManager.GetAnalysis(analysisQuery, queryTemplateID, startDate, endDate, rdbDefaultOverlay.Checked, true, aimMaterialID, aimNutrientID, sampleDate, sampleID, aimOwnerID, aimOwnerLocationID, aimSupplierID, aimSupplierLocationID, cfcPlantID, gWarning, limitWarning, sampleCode, (Int32)_mainMenu.lueDataMap.EditValue, outlierType, cfcIngredientID, plant);
                AIMGlobal.sampleDetail = AIMManager.GetAnalysis(analysisQuery, queryTemplateID, startDate, endDate, rdbDefaultOverlay.Checked, true, aimMaterialID, aimNutrientID, sampleDate, sampleID, aimOwnerID, aimOwnerLocationID, aimSupplierID, aimSupplierLocationID, cfcPlantID, gWarning, limitWarning, sampleCode, (Int32)_mainMenu.lueDataMap.EditValue, outlierType, cfcIngredientID, plant, false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                AIMGlobal.SplashScreenHide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void replnkNoOfSample_Click(object sender, EventArgs e)
        {
            try
            {
                relinkNumberOfSamples();

                //4/29/19 - changed from Hide to BringToBack (Analysis form was not staying maximized)
                //this.Hide();
                this.SendToBack();

                //12/20/22 - no longer necessary
                //3/1/21
                ////AIMGlobal.FromAnalysisProduct = false;
                                
                _sampleDetail = new SampleDetailFromAnalysis();  
                
                //10/6/21 - commented, not needed
                //_sampleDetail.WindowState = FormWindowState.Normal;                    

                _sampleDetail.ShowDialog();

                relinkNumberOfSamples();
                rbOutliersOnly.Checked = AIMGlobal.analysisOutliersOnly;
                rbAllRecords.Checked = AIMGlobal.analysisAllRecords;

                rbExtremeOutlierExclude.Checked = AIMGlobal.analysisExcludeExtremeOutliers;
                rbExtremeOutlierInclude.Checked = AIMGlobal.analysisIncludeExtremeOutliers;
                rbMildOutlierExclude.Checked = AIMGlobal.analysisExcludeMildOutliers;
                rbMildOutlierInclude.Checked = AIMGlobal.analysisIncludeMildOutliers;

                SetBtnRecalculate();
                WindowState = FormWindowState.Maximized;

                //4/29/19 - changed from Show to BringToFront (Analysis form was not getting maximized)
                //this.Show();
                this.BringToFront();

                WindowState = FormWindowState.Maximized;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        
        private void rdbBase_CheckedChanged(object sender, EventArgs e)
        {
            try
            {                
                _skipReLoad = true;
                _blnAllowExport = false;                    //2/7/24                

                if (rdbBase.Checked == true)
                {

                    //1/8/24
                    pnlExpand.Visible = true;
                    pnlSingleOrGroup.Visible = false;
                    rbPlantSingle.Checked = true;

                    lueCFCPlant.Visible = false;
                                                           
                    cbOwnerLocation.Checked = false;
                    cbOwnerLocation.Enabled = true;

                    cbOwner.Checked = false;
                    cbOwner.Enabled = true;
                    lueCFCPlant.EditValue = 0;

                    lblFrmPlant.Visible = false;
                    txtFrmPlant.Visible = false;

                }

                _skipReLoad = false;
                SetBtnRecalculate();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void rdbDefaultOverlay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {                
                _skipReLoad = true;

                _blnAllowExport = false;                    //2/7/24  

                if (rdbDefaultOverlay.Checked == true)
                {

                    //1/8/24
                    pnlExpand.Visible = true;
                    pnlSingleOrGroup.Visible = false;
                    rbPlantSingle.Checked = true;
                                        
                    lueCFCPlant.Visible = false;
                    cbOwnerLocation.Checked = true;
                    cbOwnerLocation.Enabled = false;

                    cbOwner.Checked = true;
                    cbOwner.Enabled = false;
                    lueCFCPlant.EditValue = 0;

                    lblFrmPlant.Visible = true;
                    txtFrmPlant.Visible = true;
                                        
                }

                _skipReLoad = false;
                SetBtnRecalculate();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void rdbSpecificOverlay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {                
                _skipReLoad = true;

                _blnAllowExport = false;                    //2/7/24  

                if (rdbSpecificOverlay.Checked == true)
                {
                    //1/8/24
                    pnlExpand.Visible = true;
                    pnlSingleOrGroup.Visible = true;
                    
                    lueCFCPlant.Visible = true;
                    cbOwnerLocation.Checked = true;
                    cbOwnerLocation.Enabled = false;

                    cbOwner.Checked = true;
                    cbOwner.Enabled = false;

                    lblFrmPlant.Visible = false;
                    txtFrmPlant.Visible = false;

                    
                }

                //2/7/24
                _skipReLoad = false;
                SetBtnRecalculate();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void rdbQueryBase_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbQueryBase.Checked == true)
                {
                    lueQueryAIMOwnerLocation.Visible = false;

                    //10/18/23
                    lueQueryBaseGroup.Visible = false;
                                        
                    lblMissingOwnerXref.Visible = false;
                    savebindTarget();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void rdbQueryDefaultOverlay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbQueryDefaultOverlay.Checked == true)
                {
                    lueQueryAIMOwnerLocation.Visible = false;

                    //10/18/23
                    lueQueryBaseGroup.Visible = false;

                    lblMissingOwnerXref.Visible = false;
                    savebindTarget();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void rdbQuerySpecificOverlay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbQuerySpecificOverlay.Checked == true)
                {
                    lueQueryAIMOwnerLocation.Visible = true;

                    //10/18/23
                    lueQueryBaseGroup.Visible = false;

                    if (_missingOwnerLocXRef == true)
                    {
                        lblMissingOwnerXref.Visible = true;
                    }
                    savebindTarget();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //2/19/21 commented, no longer used
        //private void replueField_EditValueChanged(object sender, EventArgs e)
        //{
        //    List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
        //    String feildNameString = String.Empty;
        //    QueryFeildEnum queryField;

        //    try
        //    {
        //        if (sender != null)
        //        {
        //            Int32 itemindex = 0;
        //            itemindex = ((DevExpress.XtraEditors.LookUpEdit)(sender)).ItemIndex;
        //            QueryFeildEnum.TryParse(itemindex.ToString(), out queryField);
        //            queryOperation = AIMManager.GetQueryOperation(queryField);
        //            queryOperation.Insert
        //            (
        //                0,
        //                new QueryOperationInfo
        //                {
        //                    QueryOperationID = 0,
        //                    Operation = "--Select--"
        //                }
        //            );
        //            replueOperator.DataSource = queryOperation;

        //            switch (queryField)
        //            {
        //                case QueryFeildEnum.Owner:
        //                    replueValue.DataSource = _aimOwner;
        //                    XtraEditors.LoadGridLookUpEdit(ref replueValue, "AIMOwnerID", "OwnerCode");
        //                    replueValue.Columns["OwnerName"].Visible = true;
        //                    break;
        //                case QueryFeildEnum.OwnerLocation:
        //                    replueValue.DataSource = _aimOwnerLocation;
        //                    XtraEditors.LoadGridLookUpEdit(ref replueValue, "AIMOwnerLocationID", "LocationCode");
        //                    replueValue.Columns["LocationName"].Visible = true;
        //                    break;
        //                case QueryFeildEnum.Supplier:
        //                    replueValue.DataSource = _aimSupplier;
        //                    XtraEditors.LoadGridLookUpEdit(ref replueValue, "AIMSupplierID", "SupplierName");
        //                    break;
        //                case QueryFeildEnum.SupplierLocation:
        //                    replueValue.DataSource = _aimSupplierLocation;
        //                    XtraEditors.LoadGridLookUpEdit(ref replueValue, "AIMSupplierLocationID", "SupplierLocationName");
        //                    break;
        //                case QueryFeildEnum.Material:
        //                    replueValue.DataSource = _aimMaterial;
        //                    XtraEditors.LoadGridLookUpEdit(ref replueValue, "AIMMaterialID", "AIMMaterialCode");
        //                    break;
        //                case QueryFeildEnum.NutrientCode:
        //                    replueValue.DataSource = _aimNutrient;
        //                    XtraEditors.LoadGridLookUpEdit(ref replueValue, "AIMNutrientID", "AIMNutrientCode");
        //                    break;

        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
        //    }
        //}


        private void cbOwner_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbRecalculateRightAway.Checked == false)
                {
                    dataBindGroupBy(true);
                }
                else
                {
                    dataBindGroupBy(_skipReLoad);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueQueryAIMOwnerLocation_EditValueChanged(object sender, EventArgs e)
        {
            saveExportTarget();
            bindExportTarget();
        }

        private void btnNewQueryTemplate_Click(object sender, EventArgs e)
        {

            try
            {
                pnlExistingQueryTemplate.Visible = false;
                pnlQueryButtons.Visible = false;
                pnlNewQueryTemplate.Visible = true;
                rdbQueryBase.Checked = true;
                lblYellowBox.Visible = false;
                txtYellowBox.Visible = false;

                //2/7/18
                grpQueryExportTarget.Visible = false;

                dtpValue.Text = "";
                dtpSecondValue.Text = "";
                txtDateOption.Text = "";
                tabControlQuery.Visible = false;
                pnlNewQueryTemplate.Location = new Point(12, 78);
                pnlNewQueryTemplate.Size = new Size(609, 46);
                pnlNewQueryTemplate.BringToFront();
                txtNewQueryTemplateName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void savebindTarget()
        {
            if (tcoAnalysis.SelectedIndex == 1)
            {
                saveExportTarget();
            }
            bindExportTarget();
        }

        private void tcoAnalysis_Deselected(object sender, TabControlEventArgs e)
        {
            PreviousTab = e.TabPage;
        }
        private void tcoAnalysis_Selected(object sender, TabControlEventArgs e)
        {
            CurrentTab = e.TabPage;
        }

        private void grdViewAnalyst_DoubleClick(object sender, EventArgs e)
        {
            //3/4/21 commented, not used
            //GridView view = (GridView)sender;
            //Point pt = view.GridControl.PointToClient(Control.MousePosition);            
            //object val = grdViewAnalyst.GetRowCellValue(grdViewAnalyst.FocusedRowHandle, colSampleValue);
            //object valSampleID = grdViewAnalyst.GetRowCellValue(grdViewAnalyst.FocusedRowHandle, colSampleID);
        }

        private static void doRowDoubleClick(GridView view, Point pt)
        {

            GridHitInfo info = view.CalcHitInfo(pt);

            if (info.Column.Name != "colSampleValue")
            {
                return;
            }


            if (info.InRow || info.InRowCell)
            {
                string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption));
            }

        }

        private void rbOutliersOnly_Click(object sender, EventArgs e)
        {            
            checkOutliersOnly();            
        }

        private void rbAllRecords_Click(object sender, EventArgs e)
        {
            btnRecalculate.PerformClick();
        }

        //3/4/21 commented, not used
        //private void checkOutliersOnlyNew()
        //{

            //RowCellCustomDrawEventArgs e;
            //e = grdViewAnalyst.RowCellStyle;
            //Int32 currow = 0;
            //GridViewAppearances ga;
            //System.Drawing.Color bkColor;

            //for (int i = 0; i < grdViewAnalyst.RowCount; i++)
            //{
            //    if (e.Appearance.BackColor == Color.Red || e.Appearance.BackColor == Color.Orange)
            //    {

            //    }
            //    //ga = grdViewAnalyst.Appearance;
            //    //bkColor = grdViewAnalyst.GetRowCellDisplayText.Appearance.FocusedCell.BackColor;
            //    //bkColor = grdViewAnalyst.GetRowCellValue(grdViewAnalyst.FocusedRowHandle, colSampleID);
            //}

            //List<AnalysisInfo> grid = new List<AnalysisInfo>();


            //grid = (List<AnalysisInfo>)grdAnalyst.DataSource;
            ////foreach (AnalysisInfo item in grid)

            //foreach (var item in grdAnalyst.)
            //{

            //}
            //{
            //    bkColor = grdViewAnalyst.Appearance.FocusedCell.BackColor;
            //}

            //for (int i = 0; i < grdViewAnalyst.RowCount; i++)
            //{

            //    if (grdViewAnalyst.CustomDrawCell[i].Cells[2].Text == "")
            //    {
            //        grdViewAnalyst.Row[i].Cells[2].BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        grdViewAnalyst.Row[i].Cells[2].BackColor = Color.Green;
            //    }
            //}

        //}

        private void checkOutliersOnly()
        {

            //9/18/20 - commented, not used
            //Double lower = 0; 
            //Double upper = 0;
            
            //10/9/20 - added these
            Double lowerMin;
            Double lowerMax;
            Double upperMin;
            Double upperMax;
            Boolean isValidOutlier = true;
            

            if (rbOutliersOnly.Checked == true)
            {
                //3/4/21 - removed unnecessary assignment
                //List<AnalysisInfo> grid = new List<AnalysisInfo>();
                List<AnalysisInfo> grid;

                List<AnalysisInfo> temp = new List<AnalysisInfo>();

                if (grdViewAnalyst.DataSource != null)
                {
                    grid = (List<AnalysisInfo>)grdAnalyst.DataSource;
                    foreach (AnalysisInfo item in grid)
                    {
                        //6/24/20
                        //if (Math.Round(item.SampleValue, 4) < Math.Round(item.UntrimmedOutlierLowerMild, 4) ||
                        //    Math.Round(item.SampleValue, 4) > Math.Round(item.UntrimmedOutlierUpperMild, 4))
                        //{
                        //    temp.Add(item);
                        //}

                        //10/9/20 -- commented and changed to code below.  When "Outliers Only" was clicked, this code was not working.
                        //           Changed it to use the code/column values from CustomDrawCellj
                        //if (item.SampleValue > 0)
                        //{
                        //    if (Math.Round(item.SampleValue, 4) < Math.Round(item.UntrimmedOutlierLowerMild, 4) ||
                        //        Math.Round(item.SampleValue, 4) > Math.Round(item.UntrimmedOutlierUpperMild, 4))
                        //    {
                        //        temp.Add(item);
                        //    }
                        //}
                        //else
                        //{
                        //    if (Math.Round(item.SampleValue, 4) > Math.Round(item.UntrimmedOutlierLowerMild, 4) ||
                        //        Math.Round(item.SampleValue, 4) < Math.Round(item.UntrimmedOutlierUpperMild, 4))
                        //    {
                        //        temp.Add(item);
                        //    }
                        //}
                        lowerMin = item.LMin;
                        lowerMax = item.LMax;
                        upperMin = item.UMin;
                        upperMax = item.UMax;
                        isValidOutlier = item.IsTrimmedValidOutlier;
                        if (isValidOutlier == true)
                        {
                            if (lowerMax + upperMax > 0)
                            {
                                temp.Add(item);
                            }
                            else if (lowerMin + upperMin > 0)
                            {
                                temp.Add(item);
                            }
                        }

                    }
                    grdAnalyst.DataSource = temp;
                }                             
            }
        }

        private void btnDescriptions_Click(object sender, EventArgs e)
        {
            HelpAnalysisCalculations helpDescriptions = new HelpAnalysisCalculations();
            helpDescriptions.ShowDialog();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                AIMManager.DeleteSampleAIMRefCFCUser(_userGlobal.CFCLoginUser.CFCUserID, SampleAIMRefCFCUserEnum.CFCUserID);
                if (cbRecalculateRightAway.Checked == true)
                {
                    dataBindGroupBy(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repchkIsInclude_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SampleAIMRefCFCUserInfo sampleAIMRefCFCUser = new SampleAIMRefCFCUserInfo();
                Int32 sampleAIMID = (Int32)grdViewAnalyst.GetRowCellValue(grdViewAnalyst.FocusedRowHandle, colSampleAIMID);
                grdViewAnalyst.PostEditor();
                Boolean isInclude = (Boolean)grdViewAnalyst.GetRowCellValue(grdViewAnalyst.FocusedRowHandle, colIsInclude);

                if (isInclude == true)
                {
                    AIMManager.DeleteSampleAIMRefCFCUser(sampleAIMID, SampleAIMRefCFCUserEnum.SampleAIMID_CFCUserID, _userGlobal.CFCLoginUser.CFCUserID);
                }
                else
                {
                    sampleAIMRefCFCUser.SampleAIMRefCFCUserID = 0;
                    sampleAIMRefCFCUser.SampleAIMID = sampleAIMID;
                    sampleAIMRefCFCUser.IsUserFlag = true;
                    sampleAIMRefCFCUser.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                    AIMManager.SaveSampleAIMRefCFCUser(ref sampleAIMRefCFCUser);
                }
                SetBtnRecalculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errror...", MessageBoxButtons.OK);
                throw;
            }
        }

        private void rbExtremeOutlierInclude_CheckedChanged(object sender, EventArgs e)
        {

            if (rbExtremeOutlierInclude.Checked == true)
            {
                AIMGlobal.analysisIncludeExtremeOutliers = true;
            }
            else
            {
                AIMGlobal.analysisIncludeExtremeOutliers = false;
            }

            SetBtnRecalculate();
        }
        private void rbExtremeOutlierExclude_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExtremeOutlierExclude.Checked == true)
            {
                AIMGlobal.analysisExcludeExtremeOutliers = true;
            }
            else
            {
                AIMGlobal.analysisExcludeExtremeOutliers = false;
            }

            SetBtnRecalculate();
        }
        private void rbMildOutlierInclude_CheckedChanged(object sender, EventArgs e)
        {

            if (rbMildOutlierInclude.Checked == true)
            {
                AIMGlobal.analysisIncludeMildOutliers = true;
            }
            else
            {
                AIMGlobal.analysisIncludeMildOutliers = false;
            }

            SetBtnRecalculate();
        }
        private void rbMildOutlierExclude_CheckedChanged(object sender, EventArgs e)
        {
            SetBtnRecalculate();
            if (rbMildOutlierExclude.Checked == true)
            {
                rbExtremeOutlierExclude.Checked = true;
            }

            if (rbMildOutlierExclude.Checked == true)
            {
                AIMGlobal.analysisExcludeMildOutliers = true;
            }
            else
            {
                AIMGlobal.analysisExcludeMildOutliers = false;
            }
        }
        private void cbSupplierLocation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbRecalculateRightAway.Checked == false)
                {
                    dataBindGroupBy(true);
                }
                else

                {
                    dataBindGroupBy(_skipReLoad);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cbSupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbRecalculateRightAway.Checked == false)
                {
                    dataBindGroupBy(true);
                }
                else

                {
                    dataBindGroupBy(_skipReLoad);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueQueryTemplate_Click(object sender, EventArgs e)
        {
            lueQueryTemplate.SelectAll();
        }
        private void lueQueryTemplate_Enter(object sender, EventArgs e)
        {
            lueQueryTemplate.SelectAll();
        }
        private void lueViewTemplate_Click(object sender, EventArgs e)
        {
            lueViewTemplate.SelectAll();
        }
        private void lueViewTemplate_Enter(object sender, EventArgs e)
        {
            lueViewTemplate.SelectAll();
        }
        private void lueCFCPlant_Click(object sender, EventArgs e)
        {
            lueCFCPlant.SelectAll();
        }
        private void lueCFCPlant_Enter(object sender, EventArgs e)
        {
            lueCFCPlant.SelectAll();
        }
        private void lueQueryTemplateInTab_Enter(object sender, EventArgs e)
        {
            lueQueryTemplateInTab.SelectAll();
        }
        private void lueQueryTemplateInTab_Click(object sender, EventArgs e)
        {
            lueQueryTemplateInTab.SelectAll();
        }
        private void lueQueryAIMOwnerLocation_Click(object sender, EventArgs e)
        {
            lueQueryAIMOwnerLocation.SelectAll();
        }
        private void lueQueryAIMOwnerLocation_Enter(object sender, EventArgs e)
        {
            lueQueryAIMOwnerLocation.SelectAll();
        }

        //2/19/21 - commented, no longer used
        //private void cboeAndOr_Click(object sender, EventArgs e)
        //{
        //    cboeAndOr.SelectAll();
        //}
        //private void cboeAndOr_Enter(object sender, EventArgs e)
        //{
        //    cboeAndOr.SelectAll();
        //}

        //2/19/21 - commented, no longer used
        //private void lueQueryField_Enter(object sender, EventArgs e)
        //{
        //    lueQueryField.SelectAll();
        //}
        //private void lueQueryField_Click(object sender, EventArgs e)
        //{
        //    lueQueryField.SelectAll();
        //}

        private void lueQueryOperator_Click(object sender, EventArgs e)
        {
            lueQueryOperator.SelectAll();
        }
        private void lueQueryOperator_Enter(object sender, EventArgs e)
        {
            lueQueryOperator.SelectAll();
        }
        private void lueViewTemplateInTab_Enter(object sender, EventArgs e)
        {
            lueViewTemplateInTab.SelectAll();
        }
        private void lueViewTemplateInTab_Click(object sender, EventArgs e)
        {
            lueViewTemplateInTab.SelectAll();
        }
        private void lueValue_Click(object sender, EventArgs e)
        {
            lueValue2.SelectAll();
        }
        private void lueValue_Enter(object sender, EventArgs e)
        {
            lueValue2.SelectAll();
        }
        private void lueSupplierCode_Enter(Object sender, EventArgs e)
        {
            lueSupplierCode.SelectAll();
        }

        private void lueSupplierCode_Click(Object sender, EventArgs e)
        {
            lueSupplierCode.SelectAll();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                analysisHelp = new AnalysisHelp();
                analysisHelp.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repHyperOwnerLocationCode_Click(object sender, EventArgs e)
        {
            //open formulation cross-reference , set tab to plant
            try
            {
                AIMGlobal.ownerLocationTab = true;
                AIMGlobal.open_form(formulationCrossReference = new FormulationCrossReference());               
                AIMGlobal.ownerLocationTab = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }


        #region move cursor
        private void moveCursorOnEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void cboeAndOr_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        //2/19/21 - commented, no longer used
        //private void lueQueryField_KeyDown(object sender, KeyEventArgs e)
        //{
        //    MoveCursorOnEnter(sender, e);
        //}

        private void lueQueryOperator_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void dtpValue_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void lueValue_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void lueQueryTemplateInTab_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }
        #endregion

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void analysis_Activated(Object sender, EventArgs e)
        {

            setOutlierButtons(false);   //added because outliers can be changed on chart screen 

            //1/25/21
            CFCAIMCore.Global.sampleID = 0;
            AIMGlobal.sampleCode = "";
            AIMGlobal.fromAnalysisScreen = false;

            //1/10/24
            if (AIMGlobal.exportComingFromAnalysis == true)
            {
                WindowState = FormWindowState.Maximized;
            }



        }

        private void setOutlierButtons(Boolean init)    //added because outliers can be changed on chart screen 
        {
            if (init == true)
            {                
                AIMGlobal.analysisExcludeExtremeOutliers = rbExtremeOutlierExclude.Checked;
                AIMGlobal.analysisIncludeExtremeOutliers = rbExtremeOutlierInclude.Checked;
                AIMGlobal.analysisExcludeMildOutliers = rbMildOutlierExclude.Checked;
                AIMGlobal.analysisIncludeMildOutliers = rbMildOutlierInclude.Checked;
                AIMGlobal.analysisAllRecords = rbAllRecords.Checked;
                AIMGlobal.analysisOutliersOnly = rbOutliersOnly.Checked;
                AIMGlobal.comingFromAnalysis = false;
            }
            else
            {
                rbExtremeOutlierExclude.Checked = AIMGlobal.analysisExcludeExtremeOutliers;
                rbExtremeOutlierInclude.Checked = AIMGlobal.analysisIncludeExtremeOutliers;
                rbMildOutlierExclude.Checked = AIMGlobal.analysisExcludeMildOutliers;
                rbMildOutlierInclude.Checked = AIMGlobal.analysisIncludeMildOutliers;
                rbAllRecords.Checked = AIMGlobal.analysisAllRecords;
                rbOutliersOnly.Checked = AIMGlobal.analysisOutliersOnly;
                AIMGlobal.comingFromAnalysis = true;
            }
        }

        private void lueCFCPlant_EditValueChanged(Object sender, EventArgs e)
        {
            SetBtnRecalculate();
        }

        private void btnExportTargetHelp_Click(Object sender, EventArgs e)
        {
            try
            {
                analysisHelpTarget = new AnalysisHelpTarget();
                analysisHelpTarget.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdViewAnalyst_ShowCustomizationForm(Object sender, EventArgs e)
        {
            (sender as GridView).CustomizationForm.ActiveListBox.MeasureItem -= activeListBox_MeasureItem;
            (sender as GridView).CustomizationForm.ActiveListBox.MeasureItem += activeListBox_MeasureItem;
            (sender as GridView).CustomizationForm.ActiveListBox.ItemHeight = 26;
        }

        private void activeListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 26;
        }

        private void rbAllRecords_CheckedChanged(Object sender, EventArgs e)
        {

            if (rbAllRecords.Checked == true)
            {
                AIMGlobal.analysisAllRecords = true;
            }
            else
            {
                AIMGlobal.analysisAllRecords = false;
            }
        }
        private void rbOutliersOnly_CheckedChanged(Object sender, EventArgs e)
        {

            
            if (rbOutliersOnly.Checked == true)
            {
                AIMGlobal.analysisOutliersOnly = true;              
            }
            else
            {
                AIMGlobal.analysisOutliersOnly = false;
            }
        }

              

        private void checkYellowBox()
        {
            //2/22/21
            ////If template detail exists, show yellow box help text	   
            //lblYellowBox.Visible = false;
            //txtYellowBox.Visible = false;
            //ErrorInfo countField = new ErrorInfo();
            //countField = AIMManager.CountQueryTemplateTotal((Int32)lueQueryTemplateInTab.EditValue);
            //if (countField.RecordID > 0)
            //{
            //    lblYellowBox.Visible = true;
            //    txtYellowBox.Visible = true;
            //}
            AnalysisSharedFunctions.CheckYellowBox(lblYellowBox, txtYellowBox, lueQueryTemplateInTab);
        }

        #region Template

        private void dataBindQueryTabLue()
        {
            
            //SAMPLE DATE -----------------------------------------------------------------            
            AnalysisSharedFunctions.popOperatorLue(lueOperator);
                        
            //MATERIAL/NUTRIENT ----------------------------------------------------------
            AnalysisSharedFunctions.popMaterialNutrientLue(lueMaterialNutCode, lueMaterialNutName, true);
                        
            //NUTRIENT --------------------------------------------------------------------
            AnalysisSharedFunctions.popNutrientLueAndReplue(lueNutrientCode, lueNutrientName, replueMaterialNutrientCode, replueMaterialNutrientName);
            
            //OWNER -----------------------------------------------------------------------

            //10/17/23
            //AnalysisSharedFunctions.popOwnerLue(lueOwnerCode, lueOwnerName, lueOwnerCode_Loc, lueOwnerName_Loc);
            AnalysisSharedFunctions.popOwnerLue(lueOwnerCode, lueOwnerName, lueOwnerCode_Loc, lueOwnerName_Loc, _ownerLocationGroupID);


            //SUPPLIER --------------------------------------------------------------------
            
            //10/17/23
            //AnalysisSharedFunctions.popSupplierLue(lueSupplierCode, lueSupplierName, lueSupplierCode_Loc, lueSupplierName_Loc);
            AnalysisSharedFunctions.popSupplierLue(lueSupplierCode, lueSupplierName, lueSupplierCode_Loc, lueSupplierName_Loc, _supplierGroupID);


            //INGREDIENT --------------------------------------------------------------------
            List<CFCIngredientInfo> cfcIngredient;
            cfcIngredient = AIMManager.GetAllCFCIngredientAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);
            cfcIngredient.Insert
            (
                0,
                new CFCIngredientInfo
                {
                    CFCIngredientID = 0,
                    CFCIngredientCode = "--Select--",
                    CFCIngredientName = "--Select--"
                }
            );
            lueIngredientCode.Properties.DataSource = cfcIngredient;
            XtraEditors.LoadLookUpEdit(ref lueIngredientCode, "CFCIngredientID", "CFCIngredientCode", "CFCIngredientName");
            lueIngredientName.Properties.DataSource = cfcIngredient;
            XtraEditors.LoadLookUpEdit(ref lueIngredientName, "CFCIngredientID", "CFCIngredientName", "CFCIngredientCode");

            
            //SAMPLE TYPE --------------------------------------------------------------
            AnalysisSharedFunctions.popSampleTypeLue(lueSampleTypeName, true);

        }
        
        private void dataBindOwnerLocationlue()

        {            
            //OWNER LOCATION -------------------------------------------------------------
            
            //10/17/23
            //AnalysisSharedFunctions.DataBindOwnerLocationLue(lueOwnerName_Loc, lueOwnerLocationName, lueOwnerLocationCode);
            AnalysisSharedFunctions.DataBindOwnerLocationLue(lueOwnerName_Loc, lueOwnerLocationName, lueOwnerLocationCode, _ownerLocationGroupID);

        }
        private void dataBindSupplierLocationlue()
        {            
            //SUPPLIER LOCATION -------------------------------------------------------------

            //10/17/23
            //AnalysisSharedFunctions.DataBindSupplierLocationLue(lueSupplierName_Loc, lueSupplierLocationCode, lueSupplierLocationName);
            AnalysisSharedFunctions.DataBindSupplierLocationLue(lueSupplierName_Loc, lueSupplierLocationCode, lueSupplierLocationName, _supplierGroupID, _supplierLocationGroupID);

        }

        private Int32 checksBeforeAdding(String type, DevExpress.XtraEditors.LookUpEdit lue)
        {
            Int32 rtn;            
            rtn = AnalysisSharedFunctions.ChecksBeforeAdding(type, lueQueryTemplateInTab, lue);
            return rtn;
        }

        private Int32 createdInReportsCheck()
        {
            Int32 rtn;
            //2/23/21
            //QueryTemplateInfo analysisTemplate = (QueryTemplateInfo)lueQueryTemplateInTab.GetSelectedDataRow();
            //if (analysisTemplate != null && analysisTemplate.QueryTemplateID > 0)
            //{
            //    if (analysisTemplate.IsAnalysis == false && (Int32)lueQueryTemplateInTab.EditValue != 0)
            //    {
            //        MessageBox.Show("This template was created in Reports and cannot be modified here. ", "Warning...");
            //        rtn = -1;
            //    }
            //}            
            rtn = AnalysisSharedFunctions.CreatedInReportsCheck(lueQueryTemplateInTab);
            return rtn;

        }

        private void deleteQueryTemplateDetail(Int32 queryTemplateDetailID)
        {
            //2/23/21
            //try
            //{
            //    if (_isAnalysis == false && (Int32)lueQueryTemplateInTab.EditValue != 0)
            //    {
            //        MessageBox.Show("This template was created in Reports and cannot be modified here. ", "Warning...");
            //        return;
            //    }
            //    AIMManager.DeleteQueryTemplateDetail(queryTemplateDetailID);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            AnalysisSharedFunctions.DeleteQueryTemplateDetail(_isAnalysis, lueQueryTemplateInTab, queryTemplateDetailID);
        }

        private void syncValueLue(ref LookUpEdit from, ref LookUpEdit to)
        {
            //2/23/21
            //if (from == null || to == null)
            //{
            //    return;
            //}
            //if (to.EditValue == from.EditValue)
            //{
            //    return;
            //}
            //to.EditValue = from.EditValue;
            AnalysisSharedFunctions.SyncValueLue(from, to);
        }

        private void btnAddMaterial_Click(Object sender, EventArgs e)
        {
            try
            {
                //-------------------------------------------------------------
                if (rbSingleMaterial.Checked == true)                                               //10/9/23
                {                                                                                   //10/9/23
                    if (checksBeforeAdding("Material", lueMaterialCode) != 0)
                    {
                        return;
                    }
                    AnalysisSharedFunctions.SaveQueryTemplateDetail((Int32)QueryFeildEnum.Material, lueQueryTemplateInTab, rbMaterialExclude, lueMaterialCode, _userGlobal.CFCLoginUser.CFCUserID);
                }                                                                                   //10/9/23

                //-------------------------------------------------------------
                else if (rbRangeMaterial.Checked == true)
                {
                    if ((Int32)lueMaterialBeg.EditValue == 0)
                    {
                        MessageBox.Show("Please select Range Begin value.", "Warning...");
                        lueMaterialBeg.ShowPopup();
                        return;
                    }
                    else if ((Int32) lueMaterialEnd.EditValue == 0)
                    {
                        MessageBox.Show("Please select Range End value.", "Warning...");
                        lueMaterialEnd.ShowPopup();
                        return;
                    }                    

                    //Get all materials in the range
                    List<AIMMaterialInfo> range = new List<AIMMaterialInfo>();
                    List<AIMMaterialInfo> material = new List<AIMMaterialInfo>();
                    range = AIMManager.GetAIMMaterialAIMMaterialCodeRange(lueMaterialBeg.Text, lueMaterialEnd.Text);

                    //Send each material to this stp (checks to see if material is already added)
                    if (range != null)
                    {
                        foreach (var item in range)
                        {
                            material = AIMManager.GetAIMMaterialForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Material, item.AIMMaterialID);
                            if (material.Count == 0)
                            {                            
                                AnalysisSharedFunctions.SaveQueryTemplateDetailValue((Int32)QueryFeildEnum.Material, lueQueryTemplateInTab, rbMaterialExclude, item.AIMMaterialID, _userGlobal.CFCLoginUser.CFCUserID);
                            }
                            
                        }
                    }                    
                }
                
                
                
                dataBindTemplateGrids();

                lueMaterialCode.EditValue = 0;
                lueMaterialName.EditValue = 0;

                lueMaterialBeg.EditValue = 0;                   //10/9/23
                lueMaterialEnd.EditValue = 0;                   //10/9/23

                if (rbSingleMaterial.Checked == true)           //10/9/23
                {                                               //10/9/23
                    lueMaterialCode.Focus();
                    lueMaterialCode.ShowPopup();
                }                                               //10/9/23
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void repbtnDeleteMaterial_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                AIMMaterialInfo aimMaterial = new AIMMaterialInfo();
                aimMaterial = (AIMMaterialInfo)grdMaterialView.GetFocusedRow();
                if (aimMaterial != null)
                {
                    deleteQueryTemplateDetail(aimMaterial.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

                //2/7/18
                if (((List<AIMMaterialInfo>)grdMaterial.DataSource).Count == 0)
                {
                    rbMaterialInclude.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddOwner_Click(Object sender, EventArgs e)
        {
            try
            {
                if (checksBeforeAdding("Owner", lueOwnerCode) != 0)
                {
                    return;
                }

                //2/23/21
                //QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
                //queryTemplateDetail.QueryTemplateDetailID = 0;
                //queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
                //queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.Owner;
                ////3/6/18
                ////queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                //queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
                ////2/7/18
                //if (rbOwnerExclude.Checked == true)
                //{
                //    //3/6/18 - If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0
                //    //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                //    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                //    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                //}
                //queryTemplateDetail.Value = (Int32)lueOwnerCode.EditValue;
                //queryTemplateDetail.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                //AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
                AnalysisSharedFunctions.SaveQueryTemplateDetail((Int32)QueryFeildEnum.Owner, lueQueryTemplateInTab, rbOwnerExclude, lueOwnerCode, _userGlobal.CFCLoginUser.CFCUserID);

                dataBindTemplateGrids();
                lueOwnerCode.EditValue = 0;
                lueOwnerName.EditValue = 0;

                lueOwnerCode.Focus();
                lueOwnerCode.ShowPopup();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void repbtnDeleteOwner_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                AIMOwnerInfo aimOwner = new AIMOwnerInfo();
                aimOwner = (AIMOwnerInfo)grdOwnerView.GetFocusedRow();
                if (aimOwner != null)
                {
                    deleteQueryTemplateDetail(aimOwner.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

                //2/7/18
                if (((List<AIMOwnerInfo>)grdOwner.DataSource).Count == 0)
                {
                    rbOwnerInclude.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddOwnerLocation_Click(Object sender, EventArgs e)
        {
            try
            {

                if (checksBeforeAdding("Owner Location", lueOwnerLocationCode) != 0)
                {
                    return;
                }

                //2/23/21
                //QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
                //queryTemplateDetail.QueryTemplateDetailID = 0;
                //queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
                //queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.OwnerLocation;
                //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                
                //queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
                ////2/7/18
                //if (rbOwnerLocExclude.Checked == true)
                //{
                //    //3/6/18 - If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0
                //    //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                //    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                //    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                //}
                //queryTemplateDetail.Value = (Int32)lueOwnerLocationCode.EditValue;
                //queryTemplateDetail.SecondValue = (Int32)lueOwnerCode_Loc.EditValue;
                //queryTemplateDetail.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                //AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
                AnalysisSharedFunctions.SaveQueryTemplateDetail((Int32)QueryFeildEnum.OwnerLocation, lueQueryTemplateInTab, rbOwnerLocExclude, lueOwnerLocationCode, _userGlobal.CFCLoginUser.CFCUserID);

                dataBindTemplateGrids();

                //7/27/22 - commented
                //lueOwnerCode_Loc.EditValue = 0;
                //lueOwnerName_Loc.EditValue = 0;

                lueOwnerLocationCode.EditValue = 0;
                lueOwnerLocationName.EditValue = 0;

                //7/27/22 - commented
                //lueOwnerCode_Loc.Focus();
                //lueOwnerCode_Loc.ShowPopup();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void repbtnDeleteOwnerLocation_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                AIMOwnerLocationInfo aimOwnerLocation = new AIMOwnerLocationInfo();
                aimOwnerLocation = (AIMOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();
                if (aimOwnerLocation != null)
                {
                    deleteQueryTemplateDetail(aimOwnerLocation.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

                //2/7/18
                if (((List<AIMOwnerLocationInfo>)grdOwnerLocation.DataSource).Count == 0)
                {
                    rbOwnerLocInclude.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddSupplier_Click(Object sender, EventArgs e)
        {
            try
            {

                if (checksBeforeAdding("Supplier", lueSupplierCode) != 0)
                {
                    return;
                }

                //2/23/21
                //QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
                //queryTemplateDetail.QueryTemplateDetailID = 0;
                //queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
                //queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.Supplier;
                ////3/6/18
                ////queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND                
                //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                
                //queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
                ////2/7/18
                //if (rbSupplierExclude.Checked == true)
                //{
                //    //3/6/18 - If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0
                //    //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                //    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                //    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                //}
                //queryTemplateDetail.Value = (Int32)lueSupplierCode.EditValue;
                //queryTemplateDetail.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                //AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
                AnalysisSharedFunctions.SaveQueryTemplateDetail((Int32)QueryFeildEnum.Supplier, lueQueryTemplateInTab, rbSupplierExclude, lueSupplierCode, _userGlobal.CFCLoginUser.CFCUserID);

                dataBindTemplateGrids();

                lueSupplierCode.EditValue = 0;
                lueSupplierName.EditValue = 0;
                lueSupplierCode.Focus();
                lueSupplierCode.ShowPopup();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void repbtnDeleteSupplier_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                AIMSupplierInfo aimSupplier = new AIMSupplierInfo();
                aimSupplier = (AIMSupplierInfo)grdSupplierView.GetFocusedRow();
                if (aimSupplier != null)
                {
                    deleteQueryTemplateDetail(aimSupplier.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

                //2/7/18
                if (((List<AIMSupplierInfo>)grdSupplier.DataSource).Count == 0)
                {
                    rbSupplierInclude.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddSupplierLocation_Click(Object sender, EventArgs e)
        {
            try
            {

                if (checksBeforeAdding("Supplier Location", lueSupplierLocationName) != 0)
                {
                    return;
                }

                //2/23/21
                //QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
                //queryTemplateDetail.QueryTemplateDetailID = 0;
                //queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
                //queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.SupplierLocation;
                ////3/6/18
                ////queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND                
                //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                
                //queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
                ////2/7/18
                //if (rbSupplierLocExclude.Checked == true)
                //{
                //    //3/6/18 - If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0
                //    //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                //    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                //    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                //}
                //queryTemplateDetail.Value = (Int32)lueSupplierLocationCode.EditValue;
                //queryTemplateDetail.SecondValue = (Int32)lueSupplierCode_Loc.EditValue;
                //queryTemplateDetail.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                //AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
                AnalysisSharedFunctions.SaveQueryTemplateDetail((Int32)QueryFeildEnum.SupplierLocation, lueQueryTemplateInTab, rbSupplierLocExclude, lueSupplierLocationCode, _userGlobal.CFCLoginUser.CFCUserID);

                dataBindTemplateGrids();

                lueSupplierCode_Loc.EditValue = 0;
                lueSupplierName_Loc.EditValue = 0;
                lueSupplierLocationCode.EditValue = 0;
                lueSupplierLocationName.EditValue = 0;

                lueSupplierCode_Loc.Focus();
                lueSupplierCode_Loc.ShowPopup();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void repbtnDeleteSupplierLocation_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                AIMSupplierLocationInfo aimSupplierLocation = new AIMSupplierLocationInfo();
                aimSupplierLocation = (AIMSupplierLocationInfo)grdSupplierLocationView.GetFocusedRow();
                if (aimSupplierLocation != null)
                {
                    deleteQueryTemplateDetail(aimSupplierLocation.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

                //2/7/18
                if (((List<AIMSupplierLocationInfo>)grdSupplierLocation.DataSource).Count == 0)
                {
                    rbSupplierLocInclude.Checked = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddNutrient_Click(Object sender, EventArgs e)
        {
            try
            {
                if (checksBeforeAdding("Nutrient", lueNutrientCode) != 0)
                {
                    return;
                }

                //2/23/21
                //QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
                //queryTemplateDetail.QueryTemplateDetailID = 0;
                //queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
                //queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.NutrientCode;
                ////3/6/18
                ////queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND                
                //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                
                //queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
                ////2/7/18
                //if (rbNutrientExclude.Checked == true)
                //{
                //    //3/6/18 - If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0
                //    //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                //    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND

                //    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                //}
                //queryTemplateDetail.Value = (Int32)lueNutrientCode.EditValue;
                //queryTemplateDetail.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                //AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
                AnalysisSharedFunctions.SaveQueryTemplateDetail((Int32)QueryFeildEnum.NutrientCode, lueQueryTemplateInTab, rbNutrientExclude, lueNutrientCode, _userGlobal.CFCLoginUser.CFCUserID);

                dataBindTemplateGrids();

                lueNutrientCode.EditValue = 0;
                lueNutrientName.EditValue = 0;
                lueNutrientCode.Focus();
                lueNutrientCode.ShowPopup();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void repbtnDeleteNutrient_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                AIMNutrientInfo aimNutrient = new AIMNutrientInfo();
                aimNutrient = (AIMNutrientInfo)grdNutrientView.GetFocusedRow();
                if (aimNutrient != null)
                {
                    deleteQueryTemplateDetail(aimNutrient.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

                //2/7/18
                if (((List<AIMNutrientInfo>)grdNutrient.DataSource).Count == 0)
                {
                    rbNutrientInclude.Checked = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddMaterialMulti_Click(Object sender, EventArgs e)
        {
            try
            {
                if ((Int32)lueMaterialNutCode.EditValue == 0)
                {
                    MessageBox.Show("Please select AIM Material.", "Warning...");
                    lueMaterialNutCode.ShowPopup();
                    return;
                }

                if (checksBeforeAdding("MaterialNutrient", lueMaterialNutCode) != 0)
                {
                    return;
                }


                QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
                queryTemplateDetail.QueryTemplateDetailID = 0;
                queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
                queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.MaterialNutrient;
                queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND                
                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
                queryTemplateDetail.Value = (Int32)lueMaterialNutCode.EditValue;
                queryTemplateDetail.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
                dataBindTemplateGrids();

                //set row and column, and then ShowEditor opens the Nutrient dropdown
                Int32 row = grdMaterialNutrientView.RowCount - 1;
                grdMaterialNutrientView.FocusedRowHandle = row;
                grdMaterialNutrientView.FocusedColumn = grdMaterialNutrientView.VisibleColumns[2];
                grdMaterialNutrientView.ShowEditor();

                lueMaterialNutCode.Focus();
                lueMaterialNutCode.ShowPopup();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void repbtnDeleteMultiple_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                TemplateMaterialNutrientInfo materialNutrient = new TemplateMaterialNutrientInfo();
                materialNutrient = (TemplateMaterialNutrientInfo)grdMaterialNutrientView.GetFocusedRow();
                if (materialNutrient != null)
                {
                    deleteQueryTemplateDetail(materialNutrient.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddIngredient_Click(Object sender, EventArgs e)
        {
            try
            {

                if (checksBeforeAdding("Ingredient", lueIngredientCode) != 0)
                {
                    return;
                }

                //2/23/21
                //QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
                //queryTemplateDetail.QueryTemplateDetailID = 0;
                //queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
                //queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.IngredientCode;
                ////3/6/18
                ////queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND                
                //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                
                //queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
                ////2/7/18
                //if (rbIngredientExclude.Checked == true)
                //{
                //    //3/6/18 - If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0
                //    //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                //    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                //    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                //}
                //queryTemplateDetail.Value = (Int32)lueIngredientCode.EditValue;
                //queryTemplateDetail.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                //AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
                AnalysisSharedFunctions.SaveQueryTemplateDetail((Int32)QueryFeildEnum.IngredientCode, lueQueryTemplateInTab, rbIngredientExclude, lueIngredientCode, _userGlobal.CFCLoginUser.CFCUserID);

                dataBindTemplateGrids();

                lueIngredientCode.EditValue = 0;
                lueIngredientName.EditValue = 0;
                lueIngredientCode.Focus();
                lueIngredientCode.ShowPopup();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void repbtnDeleteIngredient_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                CFCIngredientInfo cfcIngredient = new CFCIngredientInfo();
                cfcIngredient = (CFCIngredientInfo)grdIngredientView.GetFocusedRow();
                if (cfcIngredient != null)
                {
                    deleteQueryTemplateDetail(cfcIngredient.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

                //2/7/18
                if (((List<CFCIngredientInfo>)grdIngredient.DataSource).Count == 0)
                {
                    rbIngredientInclude.Checked = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueMaterialCode_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueMaterialCode, ref lueMaterialName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueMaterialName_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueMaterialName, ref lueMaterialCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueMaterialCode_Click(Object sender, EventArgs e)
        {
            lueMaterialCode.SelectAll();
        }
        private void lueMaterialCode_Enter(Object sender, EventArgs e)
        {
            lueMaterialCode.SelectAll();
        }
        private void lueMaterialName_Click(Object sender, EventArgs e)
        {
            lueMaterialName.SelectAll();
        }
        private void lueMaterialName_Enter(Object sender, EventArgs e)
        {
            lueMaterialName.SelectAll();
        }
        private void lueOwnerCode_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueOwnerCode, ref lueOwnerName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueOwnerName_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueOwnerName, ref lueOwnerCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueOwnerCode_Enter(Object sender, EventArgs e)
        {
            lueOwnerCode.SelectAll();
        }
        private void lueOwnerCode_Click(Object sender, EventArgs e)
        {
            lueOwnerCode.SelectAll();
        }
        private void lueOwnerName_Click(Object sender, EventArgs e)
        {
            lueOwnerName.SelectAll();
        }
        private void lueOwnerName_Enter(Object sender, EventArgs e)
        {
            lueOwnerName.SelectAll();
        }
        private void lueOwnerCode_Loc_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueOwnerCode_Loc, ref lueOwnerName_Loc);
                dataBindOwnerLocationlue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueOwnerName_Loc_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueOwnerName_Loc, ref lueOwnerCode_Loc);
                dataBindOwnerLocationlue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueOwnerLocationCode_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueOwnerLocationCode, ref lueOwnerLocationName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueOwnerLocationName_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueOwnerLocationName, ref lueOwnerLocationCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueOwnerCode_Loc_Enter(Object sender, EventArgs e)
        {
            lueOwnerCode_Loc.SelectAll();
        }
        private void lueOwnerCode_Loc_Click(Object sender, EventArgs e)
        {
            lueOwnerCode_Loc.SelectAll();
        }
        private void lueOwnerName_Loc_Click(Object sender, EventArgs e)
        {
            lueOwnerName_Loc.SelectAll();
        }
        private void lueOwnerName_Loc_Enter(Object sender, EventArgs e)
        {
            lueOwnerName_Loc.SelectAll();
        }
        private void lueOwnerLocationCode_Enter(Object sender, EventArgs e)
        {
            lueOwnerLocationCode.SelectAll();
        }
        private void lueOwnerLocationCode_Click(Object sender, EventArgs e)
        {
            lueOwnerLocationCode.SelectAll();
        }
        private void lueOwnerLocationName_Click(Object sender, EventArgs e)
        {
            lueOwnerLocationName.SelectAll();
        }
        private void lueOwnerLocationName_Enter(Object sender, EventArgs e)
        {
            lueOwnerLocationName.SelectAll();
        }
        private void lueSupplierName_Enter(Object sender, EventArgs e)
        {
            lueSupplierName.SelectAll();
        }
        private void lueSupplierName_Click(Object sender, EventArgs e)
        {
            lueSupplierName.SelectAll();
        }
        private void lueSupplierName_Loc_Enter(Object sender, EventArgs e)
        {
            lueSupplierName.SelectAll();
        }
        private void lueSupplierName_Loc_Click(Object sender, EventArgs e)
        {
            lueSupplierName.SelectAll();
        }
        private void lueSupplierLocationName_Click(Object sender, EventArgs e)
        {
            lueSupplierLocationName.SelectAll();
        }
        private void lueSupplierLocationName_Enter(Object sender, EventArgs e)
        {
            lueSupplierLocationName.SelectAll();
        }
        private void lueSupplierName_Loc_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueSupplierName_Loc, ref lueSupplierCode_Loc);
                dataBindSupplierLocationlue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueMaterialMultiCode_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueMaterialNutCode, ref lueMaterialNutName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueMaterialMultiName_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueMaterialNutName, ref lueMaterialNutCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueIngredientCode_Click(Object sender, EventArgs e)
        {
            lueIngredientCode.SelectAll();
        }
        private void lueIngredientCode_Enter(Object sender, EventArgs e)
        {
            lueIngredientCode.SelectAll();
        }
        private void lueIngredientName_Enter(Object sender, EventArgs e)
        {
            lueIngredientName.SelectAll();
        }
        private void lueIngredientName_Click(Object sender, EventArgs e)
        {
            lueIngredientName.SelectAll();
        }
        private void lueIngredientCode_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueIngredientCode, ref lueIngredientName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueIngredientName_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueIngredientName, ref lueIngredientCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueNutrientCode_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueNutrientCode, ref lueNutrientName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueNutrientName_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueNutrientName, ref lueNutrientCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueNutrientCode_Enter(Object sender, EventArgs e)
        {
            lueNutrientCode.SelectAll();
        }
        private void lueNutrientCode_Click(Object sender, EventArgs e)
        {
            lueNutrientCode.SelectAll();
        }
        private void lueNutrientName_Enter(Object sender, EventArgs e)
        {
            lueNutrientName.SelectAll();
        }
        private void lueNutrientName_Click(Object sender, EventArgs e)
        {
            lueNutrientName.SelectAll();
        }
        private void lueMaterialMultiCode_Enter(Object sender, EventArgs e)
        {
            lueMaterialNutCode.SelectAll();
        }
        private void lueMaterialMultiCode_Click(Object sender, EventArgs e)
        {
            lueMaterialNutCode.SelectAll();
        }
        private void lueMaterialMultiName_Click(Object sender, EventArgs e)
        {
            lueMaterialNutName.SelectAll();
        }
        private void lueMaterialMultiName_Enter(Object sender, EventArgs e)
        {
            lueMaterialNutName.SelectAll();
        }
        private void lueSupplierCode_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueSupplierCode, ref lueSupplierName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void replueMaterialNutrientName_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void grdMultipleView_ValidateRow(Object sender, ValidateRowEventArgs e)
        {
            try
            {

                TemplateMaterialNutrientInfo nutRef = new TemplateMaterialNutrientInfo();
                nutRef = (TemplateMaterialNutrientInfo)grdMaterialNutrientView.GetFocusedRow();

                nutRef.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SaveQueryTemplateDetailNutrient(0, nutRef.QueryTemplateDetailID, nutRef.AIMNutrientID, _userGlobal.CFCLoginUser.CFCUserID);
                grdMaterialNutrient.DataSource = AIMManager.GetMaterialNutrientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.MaterialNutrient);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnRefreshMaterialNutrient_Click(Object sender, EventArgs e)
        {
            removeZeroAimMaterialNutrientRecords();
            grdMaterialNutrient.Refresh();
            if (((List<TemplateMaterialNutrientInfo>)grdMaterialNutrient.DataSource).Count == 0)
            {
                lueMaterialNutCode.EditValue = 0;
                lueMaterialNutName.EditValue = 0;
            }
        }
        private void removeZeroAimMaterialNutrientRecords()
        {
            //2/22/21
            ////loop thru grid - delete any records with AIMNutrientID = 0
            //List<TemplateMaterialNutrientInfo> multi = new List<TemplateMaterialNutrientInfo>();
            //multi = (List<TemplateMaterialNutrientInfo>)grdMaterialNutrient.DataSource;
            //if (multi != null)
            //{
            //    foreach (var item in multi)
            //    {
            //        if (item.AIMNutrientID == 0)
            //        {
            //            DeleteQueryTemplateDetail(item.QueryTemplateDetailID);
            //        }
            //    }
            //}
            //grdMaterialNutrient.DataSource = AIMManager.GetMaterialNutrientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.MaterialNutrient);
            AnalysisSharedFunctions.RemoveZeroAimMaterialNutrientRecords(grdMaterialNutrient, _isAnalysis, lueQueryTemplateInTab);
        }
        private void tabControlQuery_DrawItem(Object sender, DrawItemEventArgs e)
        {
            // This event is called once for each tab button in your tab control

            //3/4/21 - removed unnecessary assignment
            //ErrorInfo countField = new ErrorInfo();
            ErrorInfo countField;


            // First paint the background with a color based on the current tab		

            #region Tab BACKGROUND           
            switch (e.Index)    // e.Index is the index of the tab in the TabPages collection.
            {
                case 0:         //SampleDate
                    countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleDate);
                    if (countField.RecordID != 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
                    }
                    break;

                case 1:         //Material

                    countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Material);
                    if (countField.RecordID != 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
                    }

                    break;
                case 2:         //Nutrient
                    countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.NutrientCode);
                    if (countField.RecordID != 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
                    }
                    break;
                case 3:         //Owner
                    countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Owner);
                    if (countField.RecordID != 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
                    }
                    break;
                case 4:         //OwnerLocation
                    countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.OwnerLocation);
                    if (countField.RecordID != 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
                    }
                    break;
                case 5:         //Supplier			
                    countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Supplier);
                    if (countField.RecordID != 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
                    }
                    break;
                case 6:         //SupplierLocation
                    countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SupplierLocation);
                    if (countField.RecordID != 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
                    }
                    break;
                case 7:         //Material-Nutrient
                    countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.MaterialNutrient);
                    if (countField.RecordID != 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
                    }
                    break;
                case 8:         //Ingredient
                    countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.IngredientCode);
                    if (countField.RecordID != 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
                    }
                    break;

                case 9:         //SampleType
                    countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleType);
                    if (countField.RecordID != 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
                    }
                    break;

                default:
                    e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
                    break;
            }
            #endregion

            #region Tab TEXT
            // Then draw the current tab button text 
            Rectangle paddedBounds = e.Bounds;

            paddedBounds.Inflate(1, -2);
            //paddedBounds.Inflate(1, -1);

            //e.Graphics.DrawString(tabControlQuery.TabPages[e.Index].Text, this.Font, SystemBrushes.HighlightText, paddedBounds);
            //e.Graphics.DrawString(tabControlQuery.TabPages[e.Index].Text, new Font(tabControlQuery.Font, FontStyle.Bold), Brushes.Black, e.Bounds);
            e.Graphics.DrawString(tabControlQuery.TabPages[e.Index].Text, new Font(tabControlQuery.Font, FontStyle.Bold), Brushes.Black, paddedBounds);
            #endregion

            checkYellowBox();

        }
        private void replueMaterialNutrientCode_Validating(Object sender, CancelEventArgs e)
        {
            try
            {
                checkForMaterialNutrientDuplicates(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void checkForMaterialNutrientDuplicates(Object sender, CancelEventArgs e)
        {
            try
            {
                //check to see if this material-nutrient combination already exists.
                Int32 aimNutrientID = 0;
                TemplateMaterialNutrientInfo matNut = new TemplateMaterialNutrientInfo();
                QueryTemplateDetailNutrientInfo matNutrient = new QueryTemplateDetailNutrientInfo();
                matNut = (TemplateMaterialNutrientInfo)grdMaterialNutrientView.GetFocusedRow();
                aimNutrientID = (Int32)((LookUpEdit)(sender)).EditValue;

                matNutrient = AIMManager.GetQueryTemplateDetailNutrient(matNut.QueryTemplateID, matNut.AIMMaterialID, aimNutrientID);
                if (matNutrient != null)
                {
                    MessageBox.Show("This material/nutrient combination has already been selected.", "Information...", MessageBoxButtons.OK);
                    e.Cancel = true;
                    SendKeys.Send("{ESC}");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void tabControlQuery_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (tabControlQuery.SelectedIndex != 6)
            {                
                removeZeroAimMaterialNutrientRecords();
            }
        }
        private void replueMaterialNutrientName_Validating(Object sender, CancelEventArgs e)
        {
            checkForMaterialNutrientDuplicates(sender, e);
        }

        private void lueOperator_EditValueChanged(Object sender, EventArgs e)
        {
            QueryOperationEnum queryOperation;
            Int32 operation;
            lblValue.Visible = true;
            Int32.TryParse(lueOperator.EditValue.ToString(), out operation);
            queryOperation = (QueryOperationEnum)operation;

            switch (queryOperation)
            {
                case QueryOperationEnum.PastNDaysToDate:
                    lblDays.Visible = true;
                    txtValue.Visible = true;
                    dtpValue.Visible = false;
                    dtpSecondValue.Visible = false;
                    break;

                case QueryOperationEnum.MonthToDate:
                case QueryOperationEnum.YearToDate:
                    lblDays.Visible = false;
                    txtValue.Visible = false;
                    dtpValue.Visible = false;
                    dtpSecondValue.Visible = false;
                    break;

                case QueryOperationEnum.Equal:
                case QueryOperationEnum.NotEqual:
                case QueryOperationEnum.GreaterThan:
                case QueryOperationEnum.LessThan:
                case QueryOperationEnum.GreaterThanEqual:
                case QueryOperationEnum.LessThanEqual:
                    lblDays.Visible = false;
                    txtValue.Visible = false;
                    dtpValue.Visible = true;
                    dtpSecondValue.Visible = false;
                    break;
                case QueryOperationEnum.Between:
                    lblDays.Visible = false;
                    txtValue.Visible = false;
                    dtpValue.Visible = true;
                    dtpSecondValue.Visible = true;
                    break;
                default:
                    lblDays.Visible = false;
                    txtValue.Visible = false;
                    dtpValue.Visible = false;
                    dtpSecondValue.Visible = false;

                    break;
            }

        }

        private void btnSaveDateForQuery_Click(Object sender, EventArgs e)
        {
            QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
            Int32 days = 0;
            QueryOperationEnum queryOperation;
            Int32 operation;

            if (createdInReportsCheck() != 0)
            {
                return;
            }
            if ((Int32)lueOperator.EditValue == 0)
            {
                MessageBox.Show("Please select a Date Option.", "Message...", MessageBoxButtons.OK);
                lueOperator.Focus();
                lueOperator.ShowPopup();
                return;
            }
            if (dtpValue.Visible == true && dtpValue.Text.Length == 0)
            {
                MessageBox.Show("Please select a Date.", "Message...", MessageBoxButtons.OK);
                dtpValue.Focus();
                return;
            }
            if (txtValue.Visible == true)
            {
                Int32.TryParse(txtValue.Text, out days);
                if (days == 0)
                {
                    MessageBox.Show("Please enter Number of Days (number must be greater than zero).", "Warning...");
                    txtValue.Focus();
                    txtValue.SelectAll();
                    return;
                }
                else if (days < 0)
                {
                    MessageBox.Show("Number of Days must be greater than zero.", "Warning...");
                    txtValue.Focus();
                    txtValue.SelectAll();
                    return;
                }
            }

            //If SampleDate option exists for this query, delete it before adding a new sampleDate
            AIMManager.DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleDate);

            if (dtpValue.Visible == true)
            {
                queryTemplateDetail.DateValue = dtpValue.Value;
                if (dtpSecondValue.Visible == true)
                {
                    queryTemplateDetail.SecondDate = dtpSecondValue.Value;
                }
            }
            else if (txtValue.Visible == true)
            {
                queryTemplateDetail.Value = days;
            }

            queryTemplateDetail.QueryTemplateDetailID = 0;
            queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
            queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.SampleDate;
            Int32.TryParse(lueOperator.EditValue.ToString(), out operation);
            queryOperation = (QueryOperationEnum)operation;
            queryTemplateDetail.QueryOperationID = (Int32)queryOperation;
            queryTemplateDetail.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
            AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

            dataBindSampleDate();
            setDateTextField(queryOperation, days);

            lueOperator.EditValue = 0;
            txtValue.Text = "";
        }
        private void setDateTextField(QueryOperationEnum queryOperation, Int32 days)
        {
            //2/22/21
            //switch (queryOperation)
            //{
            //    case QueryOperationEnum.PastNDaysToDate:
            //        txtDateOption.Text = days + " Days Past Current Date";
            //        break;
            //    case QueryOperationEnum.MonthToDate:
            //        txtDateOption.Text = "Month-To-Date";
            //        break;
            //    case QueryOperationEnum.YearToDate:
            //        txtDateOption.Text = "Year-To-Date";
            //        break;
            //    case QueryOperationEnum.Equal:
            //        txtDateOption.Text = "Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
            //        break;
            //    case QueryOperationEnum.GreaterThan:
            //        txtDateOption.Text = "Greater Than " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
            //        break;
            //    case QueryOperationEnum.LessThan:
            //        txtDateOption.Text = "Less Than " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
            //        break;
            //    case QueryOperationEnum.GreaterThanEqual:
            //        txtDateOption.Text = "Greater Than or Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
            //        break;
            //    case QueryOperationEnum.LessThanEqual:
            //        txtDateOption.Text = "Less Than or Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
            //        break;
            //    case QueryOperationEnum.NotEqual:
            //        txtDateOption.Text = "Not Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
            //        break;
            //    case QueryOperationEnum.Between:
            //        txtDateOption.Text = queryOperation.ToString() + " " + dtpValue.Value.Date.ToString("MM/dd/yyyy") + " and " + dtpSecondValue.Value.Date.ToString("MM/dd/yyyy");
            //        break;

            //    default:
            //        txtDateOption.Text = queryOperation.ToString();

            //        break;
            //}
            AnalysisSharedFunctions.SetDateTextField(txtDateOption, dtpValue, dtpSecondValue, queryOperation, days);
        }
        private void btnRemoveSampleDate_Click(Object sender, EventArgs e)
        {
            AIMManager.DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleDate);
            dataBindSampleDate();
            txtDateOption.Text = "";
        }
        private void grdMaterialNutrientView_ShownEditor(Object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            if (view.ActiveEditor is LookUpEdit)
            {
                LookUpEdit lookUp = ((LookUpEdit)view.ActiveEditor);

                if (!lookUp.IsPopupOpen)
                    lookUp.ShowPopup();
            }
        }
        private void grdMaterialNutrientView_GotFocus(Object sender, EventArgs e)
        {
            grdMaterialNutrientView.ShowEditor();
        }






        #endregion

        #region Master
        //private void grdMultipleView_MasterRowEmpty(Object sender, MasterRowEmptyEventArgs e)
        //      {

        //      }

        //      private void grdMultipleView_MasterRowGetRelationCount(Object sender, MasterRowGetRelationCountEventArgs e)
        //      {
        //          e.RelationCount = 1;
        //      }

        //      private void grdMultipleView_MasterRowGetRelationName(Object sender, MasterRowGetRelationNameEventArgs e)
        //      {
        //          e.RelationName = "";
        //      }

        //      private void grdMultiple_ViewRegistered(Object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        //      {
        //          GridView view;
        //          view = e.View as GridView;

        //      }
        //      private void grdMultipleView_MasterRowExpanded(Object sender, CustomMasterRowEventArgs e)
        //      {
        //          //GridView masterView = (GridView)sender;
        //          //GridView detailView = masterView.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
        //          //detailView.BestFitColumns();
        //      }

        //      private void grdMultipleView_MasterRowGetChildList(Object sender, MasterRowGetChildListEventArgs e)
        //      {
        //          TemplateMaterialNutrientInfo header = new TemplateMaterialNutrientInfo();
        //          header = (TemplateMaterialNutrientInfo)grdMaterialNutrientView.GetRow(e.RowHandle);
        //          e.ChildList = header.NutrientDetail;


        //      }


        #endregion

        private void lueSupplierName_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueSupplierName, ref lueSupplierCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueSupplierCode_Loc_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueSupplierCode_Loc, ref lueSupplierName_Loc);
                dataBindSupplierLocationlue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueSupplierLocationCode_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueSupplierLocationCode, ref lueSupplierLocationName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueSupplierLocationName_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                syncValueLue(ref lueSupplierLocationName, ref lueSupplierLocationCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueSupplierCode_Loc_Click(Object sender, EventArgs e)
        {
            lueSupplierCode_Loc.SelectAll();
        }

        private void lueSupplierCode_Loc_Enter(Object sender, EventArgs e)
        {
            lueSupplierCode_Loc.SelectAll();
        }

        private void lueSupplierLocationCode_Click(Object sender, EventArgs e)
        {
            lueSupplierLocationCode.SelectAll();
        }

        private void lueSupplierLocationCode_Enter(Object sender, EventArgs e)
        {
            lueSupplierLocationCode.SelectAll();
        }

        //2/7/18
        private void updateQueryOperation(Int32 field, RadioButton rb, Int32 operationID, Boolean isAnd)
        {
            AIMManager.UpdateQueryOperationIDIsAnd((Int32)lueQueryTemplateInTab.EditValue, field, operationID, isAnd, _userGlobal.CFCLoginUser.CFCUserID);
        }

        //2/7/18
        private void clearIncludeExclude()
        {
            rbMaterialInclude.Checked = true;
            rbNutrientInclude.Checked = true;
            rbOwnerInclude.Checked = true;
            rbOwnerLocInclude.Checked = true;
            rbSupplierInclude.Checked = true;
            rbSupplierLocInclude.Checked = true;
            rbIngredientInclude.Checked = true;
        }

        //2/7/18
        private void dataBindIncludeExcludeBoxes()
        {
            dataBindBox(grdMaterialView, rbMaterialInclude, rbMaterialExclude, "Material");
            dataBindBox(grdNutrientView, rbNutrientInclude, rbNutrientExclude, "Nutrient");
            dataBindBox(grdOwnerView, rbOwnerInclude, rbOwnerExclude, "Owner");
            dataBindBox(grdOwnerLocationView, rbOwnerLocInclude, rbOwnerLocExclude, "OwnerLoc");
            dataBindBox(grdSupplierView, rbSupplierInclude, rbSupplierExclude, "Supplier");
            dataBindBox(grdSupplierLocationView, rbSupplierLocInclude, rbSupplierLocExclude, "SupplierLoc");
            dataBindBox(grdIngredientView, rbIngredientInclude, rbIngredientExclude, "Ingredient");

            //1/25/21
            dataBindBox(grdSampleTypeView, rbSampleTypeInclude, rbSampleTypeExclude, "SampleType");
        }

        //2/7/18
        private void dataBindBox(GridView view, RadioButton rbInclude, RadioButton rbExclude, String type)
        {
            //2/22/21
            //if (view.DataRowCount != 0)
            //{
            //    rbInclude.Checked = true;
            //    Int32 queryOperationID = (Int32)QueryOperationEnum.Equal;
            //    switch (type)
            //    {
            //        case "Material":
            //            queryOperationID = ((List<AIMMaterialInfo>)view.DataSource)[0].QueryOperationID;
            //            break;
            //        case "Nutrient":
            //            queryOperationID = ((List<AIMNutrientInfo>)view.DataSource)[0].QueryOperationID;
            //            break;
            //        case "Owner":
            //            queryOperationID = ((List<AIMOwnerInfo>)view.DataSource)[0].QueryOperationID;
            //            break;
            //        case "OwnerLoc":
            //            queryOperationID = ((List<AIMOwnerLocationInfo>)view.DataSource)[0].QueryOperationID;
            //            break;
            //        case "Supplier":
            //            queryOperationID = ((List<AIMSupplierInfo>)view.DataSource)[0].QueryOperationID;
            //            break;
            //        case "SupplierLoc":
            //            queryOperationID = ((List<AIMSupplierLocationInfo>)view.DataSource)[0].QueryOperationID;
            //            break;
            //        case "MaterialNutrient":
            //            queryOperationID = ((List<TemplateMaterialNutrientInfo>)view.DataSource)[0].QueryOperationID;
            //            break;
            //        case "Ingredient":
            //            queryOperationID = ((List<CFCIngredientInfo>)view.DataSource)[0].QueryOperationID;
            //            break;
            //    }
            //    if (queryOperationID == (Int32)QueryOperationEnum.NotEqual)
            //    {
            //        rbExclude.Checked = true;
            //    }
            //}
            AnalysisSharedFunctions.DataBindBox(view, rbInclude, rbExclude, type);
        }

        #region Radio Buttons
        private void rbMaterialInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.Material, rbMaterialInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.Material, rbMaterialInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbMaterialExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.Material, rbMaterialExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.Material, rbMaterialExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }
        private void rbNutrientInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.NutrientCode, rbNutrientInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.NutrientCode, rbNutrientInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbNutrientExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.NutrientCode, rbNutrientExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.NutrientCode, rbNutrientExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }
        private void rbOwnerInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.Owner, rbOwnerInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.Owner, rbOwnerInclude, (Int32)QueryOperationEnum.Equal, false);

        }
        private void rbOwnerExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.Owner, rbOwnerExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.Owner, rbOwnerExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }
        private void rbOwnerLocInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.OwnerLocation, rbOwnerLocInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.OwnerLocation, rbOwnerLocInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbOwnerLocExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.OwnerLocation, rbOwnerLocExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.OwnerLocation, rbOwnerLocExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }
        private void rbSupplierInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.Supplier, rbSupplierInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.Supplier, rbSupplierInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbSupplierExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.Supplier, rbSupplierExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.Supplier, rbSupplierExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }
        private void rbSupplierLocInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.SupplierLocation, rbSupplierLocInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.SupplierLocation, rbSupplierLocInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbSupplierLocExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.SupplierLocation, rbSupplierLocExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.SupplierLocation, rbSupplierLocExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }
        private void rbIngredientInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.IngredientCode, rbIngredientInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.IngredientCode, rbIngredientInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbIngredientExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.IngredientCode, rbIngredientExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.IngredientCode, rbIngredientExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }

        //10/9/23 commented, not used
        //private void rbProducts_CheckedChanged(Object sender, EventArgs e)
        //{
        //    dataBindMaterial();
        //}

        //1/25/21
        private void rbSampleTypeInclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 4 (include), IsAnd s/b 0            
            updateQueryOperation((Int32)QueryFeildEnum.SampleType, rbSampleTypeInclude, (Int32)QueryOperationEnum.Equal, false);
        }

        //1/25/21
        private void rbSampleTypeExclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 9 (exclude), IsAnd s/b 1            
            updateQueryOperation((Int32)QueryFeildEnum.SampleType, rbSampleTypeExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }
        #endregion
                

        private void dataBindMaterial()
        {
            //MATERIAL --------------------------------------------------------------------
            List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
            List<AIMMaterialInfo> aimMaterialRange = new List<AIMMaterialInfo>();                       //10/13/23
            List<AIMMaterialRefAnalyzerMaterialInfo> aimMaterialProducts = new List<AIMMaterialRefAnalyzerMaterialInfo>();

            //10/9/23 commented, not used
            //if (rbProducts.Checked == true)
            //{
            //    aimMaterialProducts = AIMManager.GetAIMMaterialRefAnalyzerMaterial_DataSource_MaterialType_OwnerLocation(0, (Int32)MaterialTypeEnum.Product, 0);
            //}
            //else
            //{
                //10/13/23
                //aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient);
                aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient, (Int32)EnumOrderBy.Name);
            //}

            aimMaterial.Insert
            (
                0,
                new AIMMaterialInfo
                {
                    AIMMaterialID = 0,
                    AIMMaterialCode = "--Select--",
                    AIMMaterialName = "--Select--"
                }
            );

            //10/9/23 commented, not used
            //if (rbProducts.Checked == true)
            //{
            //    lueMaterialCode.Properties.DataSource = aimMaterialProducts;
            //    XtraEditors.LoadLookUpEdit(ref lueMaterialCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "AIMLocationName");
            //    lueMaterialName.Properties.DataSource = aimMaterialProducts;
            //    XtraEditors.LoadLookUpEdit(ref lueMaterialName, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "AIMLocationName");
            //}
            //else
            //{
            lueMaterialCode.Properties.DataSource = aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueMaterialCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
            lueMaterialName.Properties.DataSource = aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueMaterialName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");
            //}

            //10/9/23 - Beg/End Range dropdowns
            aimMaterialRange = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient, (Int32)EnumOrderBy.Code);
            aimMaterialRange.Insert
            (
                0,
                new AIMMaterialInfo
                {
                    AIMMaterialID = 0,
                    AIMMaterialCode = "--Select--",
                    AIMMaterialName = "--Select--"
                }
            );
            lueMaterialBeg.Properties.DataSource = aimMaterialRange;
            XtraEditors.LoadLookUpEdit(ref lueMaterialBeg, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
            lueMaterialEnd.Properties.DataSource = aimMaterialRange;
            XtraEditors.LoadLookUpEdit(ref lueMaterialEnd, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");

        }

        //5/29/19
        private void grdViewAnalyst_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            AIMGlobal.focusedRowGreen(sender, e);
        }

        //12/16/20 - save user-default settings   
        private void saveUserDefaults()
        {
            try
            {                                           
                AnalysisUserDefaultSettingsInfo defaults = new AnalysisUserDefaultSettingsInfo();
                defaults.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                if (cbDate.Checked == true) { defaults.GroupByDate = true; } else { defaults.GroupByDate = false; }
                if (cbSampleID.Checked == true) { defaults.GroupBySampleID = true; } else { defaults.GroupBySampleID = false; }
                if (cbOwner.Checked == true) { defaults.GroupByOwner = true; } else { defaults.GroupByOwner = false; }
                if (cbOwnerLocation.Checked == true) { defaults.GroupByOwnerLocation = true; } else { defaults.GroupByOwnerLocation = false; }
                if (cbSupplier.Checked == true) { defaults.GroupBySupplier = true; } else { defaults.GroupBySupplier = false; }
                if (cbSupplierLocation.Checked == true) { defaults.GroupBySupplierLocation = true; } else { defaults.GroupBySupplierLocation = false; }
                if (rbAllRecords.Checked == true) { defaults.AllRecords = true; } else { defaults.AllRecords = false; }
                if (rbOutliersOnly.Checked == true) { defaults.OutliersOnly = true; } else { defaults.OutliersOnly = false; }
                if (rbExtremeOutlierInclude.Checked == true) { defaults.ExtremeOutlierInclude = true; } else { defaults.ExtremeOutlierInclude = false; }
                if (rbExtremeOutlierExclude.Checked == true) { defaults.ExtremeOutlierExclude = true; } else { defaults.ExtremeOutlierExclude = false; }
                if (rbMildOutlierInclude.Checked == true) { defaults.MildOutlierInclude = true; } else { defaults.MildOutlierInclude = false; }
                if (rbMildOutlierExclude.Checked == true) { defaults.MildOutlierExclude = true; } else { defaults.MildOutlierExclude = false; }
                if (rdbBase.Checked == true) { defaults.TargetBase = true; } else { defaults.TargetBase = false; }
                if (rdbDefaultOverlay.Checked == true) { defaults.TargetDefaultOverlay = true; } else { defaults.TargetDefaultOverlay = false; }
                if (rdbSpecificOverlay.Checked == true) { defaults.TargetSpecificOverlay = true; } else { defaults.TargetSpecificOverlay = false; }
                if (lueCFCPlant.EditValue != null) { defaults.TargetSpecificOverlayPlantID = (Int32)lueCFCPlant.EditValue; } else { defaults.TargetSpecificOverlayPlantID = 0; }
                if (lueQueryTemplate.EditValue != null) { defaults.TemplateQuery = (Int32)lueQueryTemplate.EditValue; } else { defaults.TemplateQuery = 0; }
                if (lueViewTemplate.EditValue != null) { defaults.TemplateView = (Int32)lueViewTemplate.EditValue; } else { defaults.TemplateView = 0; }
                if (cbRecalculateRightAway.Checked == true) { defaults.RecalculateOnModify = true; } else { defaults.RecalculateOnModify = false; }

                //12/13/22
                defaults.IngredientOrProduct = "I";

                ErrorInfo result = AIMManager.AnalysisUserDefaultSettingsSave(ref defaults);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //12/17/20
        private void loadUserDefaults()
        {
            try
            {
                AnalysisUserDefaultSettingsInfo defaults = new AnalysisUserDefaultSettingsInfo();
                defaults = AIMManager.GetAnalysisUserDefaultSettings(_userGlobal.CFCLoginUser.CFCUserID);
                if (defaults != null)
                {
                    if (defaults.GroupByDate == true) { cbDate.Checked = true; } else { cbDate.Checked = false; }
                    if (defaults.GroupBySampleID == true) { cbSampleID.Checked = true; } else { cbSampleID.Checked = false; }
                    if (defaults.GroupByOwner == true) { cbOwner.Checked = true; } else { cbOwner.Checked = false; }
                    if (defaults.GroupByOwnerLocation == true) { cbOwnerLocation.Checked = true; } else { cbOwnerLocation.Checked = false; }
                    if (defaults.GroupBySupplier == true) { cbSupplier.Checked = true; } else { cbSupplier.Checked = false; }
                    if (defaults.GroupBySupplierLocation == true) { cbSupplierLocation.Checked = true; } else { cbSupplierLocation.Checked = false; }
                    if (defaults.AllRecords == true) { rbAllRecords.Checked = true; } else { rbAllRecords.Checked = false; }
                    if (defaults.OutliersOnly == true) { rbOutliersOnly.Checked = true; } else { rbOutliersOnly.Checked = false; }
                    if (defaults.ExtremeOutlierInclude == true) { rbExtremeOutlierInclude.Checked = true; } else { rbExtremeOutlierInclude.Checked = false; }
                    if (defaults.ExtremeOutlierExclude == true) { rbExtremeOutlierExclude.Checked = true; } else { rbExtremeOutlierExclude.Checked = false; }
                    if (defaults.MildOutlierInclude == true) { rbMildOutlierInclude.Checked = true; } else { rbMildOutlierInclude.Checked = false; }
                    if (defaults.MildOutlierExclude == true) { rbMildOutlierExclude.Checked = true; } else { rbMildOutlierExclude.Checked = false; }
                    if (defaults.TargetBase == true) { rdbBase.Checked = true; } else { rdbBase.Checked = false; }
                    if (defaults.TargetDefaultOverlay == true) { rdbDefaultOverlay.Checked = true; } else { rdbDefaultOverlay.Checked = false; }
                    if (defaults.TargetSpecificOverlay == true) { rdbSpecificOverlay.Checked = true; } else { rdbSpecificOverlay.Checked = false; }                    
                    if (defaults.TargetSpecificOverlayPlantID > 0) { lueCFCPlant.EditValue = defaults.TargetSpecificOverlayPlantID; } else { lueCFCPlant.EditValue = 0; }

                    //12/13/22
                    //if (defaults.TemplateQuery > 0) { lueQueryTemplate.EditValue = defaults.TemplateQuery; } else { lueQueryTemplate.EditValue = 0; }
                    //if (defaults.TemplateView > 0) { lueViewTemplate.EditValue = defaults.TemplateView; } else { lueViewTemplate.EditValue = 0; }
                    if (defaults.TemplateQuery > 0 && defaults.IngredientOrProduct == "I") { lueQueryTemplate.EditValue = defaults.TemplateQuery; } else { lueQueryTemplate.EditValue = 0; }
                    if (defaults.TemplateView > 0 && defaults.IngredientOrProduct == "I") { lueViewTemplate.EditValue = defaults.TemplateView; } else { lueViewTemplate.EditValue = 0; }

                    if (defaults.RecalculateOnModify == true) { cbRecalculateRightAway.Checked = true; } else { cbRecalculateRightAway.Checked = false; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        //1/20/21
        tpProdSpec sample;
        private void repHyperLinkEditSampleCode_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.SplashScreenShow();
                Int32 sampleID;
                Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colSampleID).ToString(), out sampleID);

                //1/25/21 -----------------------------
                CFCAIMCore.Global.sampleID = sampleID;
                if (grdViewAnalyst.GetFocusedRowCellValue(colSampleCode) != null)
                {
                    AIMGlobal.sampleCode = grdViewAnalyst.GetFocusedRowCellValue(colSampleCode).ToString();
                    AIMGlobal.fromAnalysisScreen = true;
                }
                
                //-------------------------------------
                if (sample == null || sample.Visible == false)
                {
                    sample = new tpProdSpec();
                    sample.MdiParent = this.MdiParent;

                    //10/6/21 - commented (not necessary)
                    //sample.WindowState = FormWindowState.Normal;                    

                    sample.Show();
                }
                else
                {
                    //10/6/21 - changed to maximized for resize issue (not certain this code ever gets hit)
                    //sample.WindowState = FormWindowState.Normal;                    
                    sample.WindowState = FormWindowState.Maximized;                   

                    sample.Activate();
                }

                AIMGlobal.SplashScreenHide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        //1/25/21
        private void btnAddSampleType_Click(object sender, EventArgs e)
        {
            try
            {
                if (checksBeforeAdding("SampleType", lueSampleTypeName) != 0)
                {
                    return;
                }

                //2/23/21
                //QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
                //queryTemplateDetail.QueryTemplateDetailID = 0;
                //queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
                //queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.SampleType;                
                //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                //queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;                
                //if (rbSampleTypeExclude.Checked == true)
                //{                
                //    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                //    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                //}
                //queryTemplateDetail.Value = (Int32)lueSampleTypeName.EditValue;
                //queryTemplateDetail.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                //AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
                AnalysisSharedFunctions.SaveQueryTemplateDetail((Int32)QueryFeildEnum.SampleType, lueQueryTemplateInTab, rbSampleTypeExclude, lueSampleTypeName, _userGlobal.CFCLoginUser.CFCUserID);

                dataBindTemplateGrids();
                                
                lueSampleTypeName.EditValue = 0;
                lueSampleTypeName.Focus();
                lueSampleTypeName.ShowPopup();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repbtnDeleteSampleType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                SampleTypeInfo sampleType = new SampleTypeInfo();
                sampleType = (SampleTypeInfo)grdSampleTypeView.GetFocusedRow();
                if (sampleType != null)
                {
                    deleteQueryTemplateDetail(sampleType.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

                //2/7/18
                if (((List<SampleTypeInfo>)grdSampleType.DataSource).Count == 0)
                {
                    rbSampleTypeInclude.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //1/25/21
        private void lueSampleTypeName_Click(object sender, EventArgs e)
        {
            lueSampleTypeName.SelectAll();
        }

        //1/25/21
        private void lueSampleTypeName_Enter(object sender, EventArgs e)
        {
            lueSampleTypeName.SelectAll();
        }               

        private void deStart_EditValueChanged(object sender, EventArgs e)
        {            
            DateTime startDate = deStart.DateTime;
            DateTime endDate = deEnd.DateTime;
            AnalysisSharedFunctions.GetDateDuration(txtDuration, startDate, endDate);
            if (AIMGlobal.blnChangeAnalysisDate == true)
            {
                deStart.EditValue = Convert.ToDateTime("1/1/1980");
            }
            SetBtnRecalculate();
        }

        private void deEnd_EditValueChanged(object sender, EventArgs e)
        {            
            DateTime startDate = deStart.DateTime;
            DateTime endDate = deEnd.DateTime;
            AnalysisSharedFunctions.GetDateDuration(txtDuration, startDate, endDate);
            if (AIMGlobal.blnChangeAnalysisDate == true)
            {
                deStart.EditValue = Convert.ToDateTime("1/1/1980");
            }
            SetBtnRecalculate();
        }                      

        private void ckbHideColumnHeaders_CheckedChanged(object sender, EventArgs e)
        {
            if (grdViewAnalyst.OptionsView.ShowColumnHeaders == true)
            {
                grdViewAnalyst.OptionsView.ShowColumnHeaders = false;
            }
            else
            {
                grdViewAnalyst.OptionsView.ShowColumnHeaders = true;
            }
        }
        

        private void ckbHideGroupSummary_CheckedChanged(object sender, EventArgs e)
        {
            //Hide Summary
            if (gbGroupSummary.Visible == true)
            {
                gbGroupSummary.Visible = false;

                //Show GroupBy
                if (pnlGroupByPlusOutliersResetPrintHelp.Visible == true)
                {                    
                    pnlGroupByPlusOutliersResetPrintHelp.Location = TopLocation;
                    grdAnalyst.Location = ShowGroupBy;
                    if (WindowState == FormWindowState.Normal)
                    {                     
                        grdAnalyst.Size = NormalShowGroupBy;
                    }
                    else
                    {                     
                        grdAnalyst.Size = MaxShowGroupBy;
                    }
                }
                else
                {
                    //Both Summary and Group-By are hidden                                        
                    grdAnalyst.Location = HideBoth;
                    if (WindowState == FormWindowState.Normal)
                    {                    
                        grdAnalyst.Size = NormalHideBoth;
                    }
                    else
                    {                     
                        grdAnalyst.Size = MaxHideBoth;
                    }
                }              
            
            //Show Summary
            }
            else
            {
                gbGroupSummary.Visible = true;

                if (pnlGroupByPlusOutliersResetPrintHelp.Visible == true)
                {                    
                    pnlGroupByPlusOutliersResetPrintHelp.Location = TopLocation;
                    gbGroupSummary.Location = ShowGroupBy;                    
                    grdAnalyst.Location = ShowBoth;
                    if (WindowState == FormWindowState.Normal)
                    {                     
                        grdAnalyst.Size = NormalShowBoth;
                    }
                    else
                    {                    
                        grdAnalyst.Size = MaxShowBoth;
                    }
                }
                else
                {                    
                    gbGroupSummary.Location = TopLocation;
                    grdAnalyst.Location = ShowSummary;
                    if (WindowState == FormWindowState.Normal)
                    {                    
                        grdAnalyst.Size = NormalShowSummary;
                    }
                    else
                    {                     
                        grdAnalyst.Size = MaxShowSummary;
                    }
                }
            }
        }

        private void ckbHideGroupBy_CheckedChanged(object sender, EventArgs e)
        {
            
            //Hide GroupBy
            if (pnlGroupByPlusOutliersResetPrintHelp.Visible == true)
            {
                pnlGroupByPlusOutliersResetPrintHelp.Visible = false;
                if (gbGroupSummary.Visible == true)
                {                    
                    gbGroupSummary.Location = TopLocation;
                    grdAnalyst.Location = ShowSummary;
                    if (WindowState == FormWindowState.Normal)
                    {                        
                        grdAnalyst.Size = NormalShowSummary;
                    }
                    else
                    {                     
                        grdAnalyst.Size = MaxShowSummary;
                    }
                }
                else
                {
                    //Both Summary and Group-By are hidden                    
                    grdAnalyst.Location = HideBoth;
                    if (WindowState == FormWindowState.Normal)
                    {                       
                        grdAnalyst.Size = NormalHideBoth;
                    }
                    else
                    {                     
                        grdAnalyst.Size = MaxHideBoth;
                    }
                }
            }
            else
            {
                pnlGroupByPlusOutliersResetPrintHelp.Visible = true;                
                if (gbGroupSummary.Visible == true)
                {                    
                    pnlGroupByPlusOutliersResetPrintHelp.Location = TopLocation;
                    gbGroupSummary.Location = ShowGroupBy;
                    grdAnalyst.Location = ShowBoth;

                    if (WindowState == FormWindowState.Normal)
                    {                        
                        grdAnalyst.Size = NormalShowBoth;
                    }
                    else
                    {                     
                        grdAnalyst.Size = MaxShowBoth;
                    }
                }
                else
                {
                    pnlGroupByPlusOutliersResetPrintHelp.Location = TopLocation;
                    grdAnalyst.Location = ShowGroupBy;
                    if (WindowState == FormWindowState.Normal)
                    {                     
                        grdAnalyst.Size = NormalShowGroupBy;
                    }
                    else
                    {                    
                        grdAnalyst.Size = MaxShowGroupBy;
                    }
                }
            }
        }


        //------------------------------------------------------------
        //3/14/23 - added to allow user to change header row height
        bool isResizingColumnPanel = false;
        private void grdViewAnalyst_MouseMove(object sender, MouseEventArgs e)
        {
            var columnPanelBounds = ((GridViewInfo)grdViewAnalyst.GetViewInfo()).ViewRects.ColumnPanel;

            if (isResizingColumnPanel)
            {
                grdViewAnalyst.ColumnPanelRowHeight = e.Y - columnPanelBounds.Top;
            }

            var isCursorAboveColumnPanel = Math.Abs(e.Y - columnPanelBounds.Bottom) < 5;
            var isLeftMouseButton = (e.Button & MouseButtons.Left) == MouseButtons.Left;

            if (!isCursorAboveColumnPanel)
            {
                grdAnalyst.Cursor = Cursors.Default;
                return;
            }
            grdAnalyst.Cursor = Cursors.SizeNS;

            if (!isResizingColumnPanel && isCursorAboveColumnPanel && isLeftMouseButton)
            {
                isResizingColumnPanel = true;
                return;
            }
        }

        //10/9/23
        private void rbRangeMaterial_CheckedChanged(object sender, EventArgs e)
        {            
            if (rbRangeMaterial.Checked == true)
            {
                gbMaterialRange.Enabled = true;
                gbMaterialSingle.Enabled = false;
            }
        }

        //10/9/23
        private void rbSingleMaterial_CheckedChanged(object sender, EventArgs e)
        {            
            if (rbSingleMaterial.Checked == true)
            {
                gbMaterialSingle.Enabled = true;
                gbMaterialRange.Enabled = false;
                
            }
        }
                

        //10/13/23
        //////////private void rbBaseGroup_CheckedChanged(object sender, EventArgs e)
        //////////{
        //////////    try
        //////////    {

        //////////        _skipReLoad = true;

        //////////        if (rbBaseGroup.Checked == true)
        //////////        {
        //////////            lueCFCPlant.Visible = false;
        //////////            lueBaseGroup.Visible = true;
        //////////            cbOwnerLocation.Checked = false;
        //////////            cbOwnerLocation.Enabled = true;
        //////////            cbOwner.Checked = false;
        //////////            cbOwner.Enabled = true;
        //////////            lueCFCPlant.EditValue = 0;
        //////////            lblFrmPlant.Visible = false;
        //////////            txtFrmPlant.Visible = false;
        //////////        }
        //////////        _skipReLoad = false;
        //////////        SetBtnRecalculate();

        //////////    }
        //////////    catch (Exception ex)
        //////////    {
        //////////        MessageBox.Show(ex.Message);
        //////////    }
        //////////}

        private void lueMaterialBeg_Click(object sender, EventArgs e)
        {
            lueMaterialBeg.SelectAll();
        }

        private void lueMaterialBeg_Enter(object sender, EventArgs e)
        {
            lueMaterialBeg.SelectAll();
        }

        private void lueMaterialEnd_Click(object sender, EventArgs e)
        {
            lueMaterialEnd.SelectAll();
        }

        private void lueMaterialEnd_Enter(object sender, EventArgs e)
        {
            lueMaterialEnd.SelectAll();
        }

        //10/18/23
        private void rdbQueryBaseGroup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbQueryBaseGroup.Checked == true)
                {   
                    lueQueryBaseGroup.Visible = true;
                    lueQueryAIMOwnerLocation.Visible = false;                    
                    savebindTarget();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //10/18/23
        private void lueExplandGroup_Click(object sender, EventArgs e)
        {
            lueExpandGroup.SelectAll();
        }

        private void lueExpandGroup_Enter(object sender, EventArgs e)
        {
            lueExpandGroup.SelectAll();
        }

        private void lueQueryBaseGroup_Click(object sender, EventArgs e)
        {
            lueQueryBaseGroup.SelectAll();
        }

        private void lueQueryBaseGroup_Enter(object sender, EventArgs e)
        {
            lueQueryBaseGroup.SelectAll();
        }

        //1/8/24
        private void chkExpand_CheckedChanged(object sender, EventArgs e)
        {
            _skipReLoad = true;
            _blnAllowExport = false;                    //2/7/24  

            if (chkExpand.Checked)
            {
                lueExpandGroup.Visible = true;
                                
                if (rdbBase.Checked == true)
                {
                    lueCFCPlant.Visible = false;                    
                    cbOwnerLocation.Checked = false;
                    cbOwnerLocation.Enabled = true;
                    cbOwner.Checked = false;
                    cbOwner.Enabled = true;
                    lueCFCPlant.EditValue = 0;
                    lblFrmPlant.Visible = false;
                    txtFrmPlant.Visible = false;
                }                            
            }
            else
            {
                lueExpandGroup.Visible = false;
            }

            _skipReLoad = false;                        
            SetBtnRecalculate();
        }

        //1/8/24
        private void rbPlantGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPlantGroup.Checked == true)
            {
                lueCFCPlantGroup.Visible = true;
            }
            else
            {
                lueCFCPlantGroup.Visible = false;
            }
            
        }

        private void rbPlantSingle_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPlantSingle.Checked == true)
            {
                lueCFCPlant.Visible = true;
            }
            else
            {
                lueCFCPlant.Visible = false;
            }
            
        }

        //------------------------------------------------------------

        private void lueQueryOperator_EditValueChanged(object sender, EventArgs e)
        {
            //2/19/21 commented, no longer used
            //QueryFeildEnum queryFeild;
            //Int32 feild = 0;
            //QueryOperationEnum queryOperation;
            //Int32 operation = 0;
            //lblValue.Visible = true;

            //Int32.TryParse(lueQueryField.EditValue.ToString(), out feild);
            //queryFeild = (QueryFeildEnum)feild;

            //Int32.TryParse(lueQueryOperator.EditValue.ToString(), out operation);
            //queryOperation = (QueryOperationEnum)operation;

            //if (queryFeild == QueryFeildEnum.SampleDate)
            //{
            //    switch (queryOperation)
            //    {
            //        case QueryOperationEnum.PastNDaysToDate:
            //            txtValue2.Visible = true;
            //            dtpValue2.Visible = false;
            //            lueValue2.Visible = false;

            //            break;
            //        case QueryOperationEnum.MonthToDate:
            //            txtValue2.Visible = false;
            //            dtpValue2.Visible = false;
            //            lueValue2.Visible = false;
            //            lblValue.Visible = false;
            //            break;
            //        case QueryOperationEnum.YearToDate:
            //            txtValue2.Visible = false;
            //            dtpValue2.Visible = false;
            //            lueValue2.Visible = false;
            //            lblValue.Visible = false;
            //            break;
            //        case QueryOperationEnum.Equal:
            //            txtValue2.Visible = false;
            //            dtpValue2.Visible = true;
            //            lueValue2.Visible = false;
            //            break;
            //        case QueryOperationEnum.NotEqual:
            //            txtValue2.Visible = false;
            //            dtpValue2.Visible = true;
            //            lueValue2.Visible = false;
            //            break;
            //        case QueryOperationEnum.GreaterThan:
            //            txtValue2.Visible = false;
            //            dtpValue2.Visible = true;
            //            lueValue2.Visible = false;

            //            break;
            //        case QueryOperationEnum.LessThan:
            //            txtValue2.Visible = false;
            //            dtpValue2.Visible = true;
            //            lueValue2.Visible = false;

            //            break;
            //        case QueryOperationEnum.GreaterThanEqual:
            //            txtValue2.Visible = false;
            //            dtpValue2.Visible = true;
            //            lueValue2.Visible = false;

            //            break;
            //        case QueryOperationEnum.LessThanEqual:
            //            txtValue2.Visible = false;
            //            dtpValue2.Visible = true;
            //            lueValue2.Visible = false;

            //            break;
            //        default:
            //            txtValue2.Visible = false;
            //            dtpValue2.Visible = false;
            //            lueValue2.Visible = false;

            //            break;
            //    }
            //}
            //else
            //{
            //    txtValue2.Visible = false;
            //    dtpValue2.Visible = false;
            //    lueValue2.Visible = true;
            //}
        }       

        private void Analysis_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveUserDefaults();
        }


        //10/26/21 - no longer used - changed date fields from DateTimePicker to DevExpress DateEdit (resize issue)  
        //private void dtpStart_ValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {        
        //        validateDates();        
        //        SetBtnRecalculate();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //10/26/21 - no longer used - changed date fields from DateTimePicker to DevExpress DateEdit (resize issue)  
        //private void dtpEnd_ValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {                        
        //        validateDates();        
        //        SetBtnRecalculate();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}
