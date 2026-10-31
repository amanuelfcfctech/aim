using CFCAIM.AIMUtilities;
using CFCAIM.Chart;
using CFCAIMCore;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using CFCUser.Entity;
using CFCUser.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;



namespace CFCAIM
{
    public partial class AnalysisProduct : Form
    {

        CFCUser.Global _userGlobal = new CFCUser.Global();

        MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];
                
        TabPage PreviousTab;
        TabPage CurrentTab;

        Charts charts;
        AnalysisHelp analysisHelp;        
        
        //These two IDs are keeping the selected template same in all tabs
        Int32 _selectedViewTemplateID = 0;
        Int32 _selectedQueryTemplateID = 0;

        Boolean _saveViewTemplateAs = false;
        ReportPrintLayoutInfo _currentPrintLayout = null;   //task1175 FG 7/2/2026

        List<AIMOwnerLocationInfo> _aimOwnerLocation = new List<AIMOwnerLocationInfo>();
        List<AIMOwnerInfo> _aimOwner = new List<AIMOwnerInfo>();
        List<AIMMaterialInfo> _aimMaterial = new List<AIMMaterialInfo>();
        List<AIMSupplierInfo> _aimSupplier = new List<AIMSupplierInfo>();
        List<AIMSupplierLocationInfo> _aimSupplierLocation = new List<AIMSupplierLocationInfo>();
        
        List<CFCIngredientInfo> _cfcIngredient = new List<CFCIngredientInfo>();
        Boolean _isFormLoaded = false;
        Int32 _currentRow = 0;
        Boolean _isAnalysis = false;
        Boolean _saveAs = false;

        //12/20/22
        AnalysisProductSampleDetail _sampleDetail;

        //This is used to handle the current dataset
        DateTime _currentStartDate = DateTime.MinValue;
        DateTime _currentEndDate = DateTime.MaxValue;

        Int32 _currentQueryTemplateID = 0;
        List<AIMNutrientInfo> _aimNutrient = new List<AIMNutrientInfo>();                     
        List<UserDefaultsInfo> _userDefaults = new List<UserDefaultsInfo>();

        //-------------------------------------------------------
        //3/13/23 - added for 3 hide/show checkboxes. Depending on what is selected, the grid size and panel locations change.

        //Grid Size FormWindowState.Maximum)
        Size MaxShowBoth = new Size(1573, 520);
        Size MaxShowSummary = new Size(1573, 580);
        Size MaxShowGroupBy = new Size(1573, 580);
        Size MaxHideBoth = new Size(1573, 630);

        //Grid Size (FormWindowState.Normal)
        Size NormalShowBoth = new Size(1235, 329);
        Size NormalShowSummary = new Size(1235, 390);
        Size NormalShowGroupBy = new Size(1235, 390);
        Size NormalHideBoth = new Size(1235, 450);

        //Location of the panels
        Point ShowBoth = new Point(4, 131);
        Point ShowSummary = new Point(4, 70);       
        Point ShowGroupBy = new Point(4, 59);
        Point HideBoth = new Point(4, 0);
        Point TopLocation = new Point(0, 0);

        //-------------------------------------------------------

        //For User Groups
        Int32 _ownerLocationGroupID = 0;                                        //10/17/23
        Int32 _supplierGroupID = 0;                                             //10/17/23
        Int32 _supplierLocationGroupID = 0;                                     //10/17/23        
        UserInfo _user = new UserInfo();                                        //10/17/23

        public AnalysisProduct()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026

            //------------------------------------------------------------
            //3/14/23 - added to allow user to change header row height
            grdViewAnalyst.MouseMove += grdViewAnalyst_MouseMove;
            grdViewAnalyst.MouseUp += (sender, e) => {
                isResizingColumnPanel = false;
            };
            //------------------------------------------------------------
        }

        private void analysis_Product_Load(object sender, EventArgs e)
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

                loadUserDefaults();
                setOutlierButtons(true);  //added because outliers can be changed on chart screen  

                //initializing this global to false (the global gets set in ExportBatchMaintenancePopUp,
                //                                              which is available only thru a button on the Analysis form.)
                AIMGlobal.exportComingFromAnalysis = false;

                //Query Tab lue's
                dataBindQueryTabLue();
                dataBindMaterial();
                dataBindOwnerLocationlue();               
                dataBindDefaults();

                //12/1/22
                DataBindTemplateLue();

                AIMCoreGlobal.LoadCustomEquation();
                WindowState = FormWindowState.Maximized;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region DataBind

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
            _isFormLoaded = false;

            //10/31/22
            //AnalysisSharedFunctions.populateViewTemplate(lueViewTemplate, lueViewTemplateInTab);
            //AnalysisSharedFunctions.populateQueryTemplate(lueQueryTemplate, lueQueryTemplateInTab);
            AnalysisSharedFunctions.populateViewTemplate(lueViewTemplate, lueViewTemplateInTab, "P");
            AnalysisSharedFunctions.populateQueryTemplate(lueQueryTemplate, lueQueryTemplateInTab, "P");


            _aimOwner = AnalysisSharedFunctions.populateAimOwner(_aimOwner, true);
            _aimOwnerLocation = AnalysisSharedFunctions.populateAimOwnerLocation(_aimOwnerLocation, true);
            _aimSupplier = AnalysisSharedFunctions.populateAimSupplier(_aimSupplier);
            _aimSupplierLocation = AnalysisSharedFunctions.populateAimSupplierLocation(_aimSupplierLocation);
            _aimMaterial = AnalysisSharedFunctions.populateAimMaterial(_aimMaterial, true);
            _aimNutrient = AnalysisSharedFunctions.populateAimNutrient(_aimNutrient, true);

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
        private void dataBindQueryTabLue()
        {
            AnalysisSharedFunctions.popOperatorLue(lueOperator);   //(Sample Date)
            AnalysisSharedFunctions.popMaterialNutrientLue(lueMaterialNutCode, lueMaterialNutName, true);
            AnalysisSharedFunctions.popNutrientLueAndReplue(lueNutrientCode, lueNutrientName, replueMaterialNutrientCode, replueMaterialNutrientName);

            //10/17/23
            //AnalysisSharedFunctions.popOwnerLue(lueOwnerCode, lueOwnerName, lueOwnerCode_Loc, lueOwnerName_Loc);
            AnalysisSharedFunctions.popOwnerLue(lueOwnerCode, lueOwnerName, lueOwnerCode_Loc, lueOwnerName_Loc, _ownerLocationGroupID);

            AnalysisSharedFunctions.popSampleTypeLue(lueSampleTypeName, true);

            //12/21/22
            AnalysisSharedFunctions.popProductTypeLue(lueProductTypeName, (Int32)_mainMenu.lueDataMap.EditValue);
        }
        private void dataBindDefaults()
        {
            try
            {                
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
        private void dataBindMaterial()
        {
            //MATERIAL --------------------------------------------------------------------
            List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();

            //3/4/21 - removed unnecessary assignment
            //List<AIMMaterialRefAnalyzerMaterialInfo> aimMaterialProducts = new List<AIMMaterialRefAnalyzerMaterialInfo>();
            List<AIMMaterialRefAnalyzerMaterialInfo> aimMaterialProducts;

            aimMaterialProducts = AIMManager.GetAIMMaterialRefAnalyzerMaterial_DataSource_MaterialType_OwnerLocation(0, (Int32)MaterialTypeEnum.Product, 0);
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
            lueMaterialCode.Properties.DataSource = aimMaterialProducts;
            XtraEditors.LoadLookUpEdit(ref lueMaterialCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "AIMLocationName");
            lueMaterialName.Properties.DataSource = aimMaterialProducts;
            XtraEditors.LoadLookUpEdit(ref lueMaterialName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode", "AIMLocationName");

            //10/9/23 - Beg/End Range dropdowns
            lueMaterialBeg.Properties.DataSource = aimMaterialProducts;
            XtraEditors.LoadLookUpEdit(ref lueMaterialBeg, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "AIMLocationName");
            lueMaterialEnd.Properties.DataSource = aimMaterialProducts;
            XtraEditors.LoadLookUpEdit(ref lueMaterialEnd, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "AIMLocationName");
        }
        private void dataBindOwnerLocationlue()
        {
            //10/17/23
            //AnalysisSharedFunctions.DataBindOwnerLocationLue(lueOwnerName_Loc, lueOwnerLocationName, lueOwnerLocationCode);
            AnalysisSharedFunctions.DataBindOwnerLocationLue(lueOwnerName_Loc, lueOwnerLocationName, lueOwnerLocationCode, _ownerLocationGroupID);
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
            grdViewAnalyst.Columns.ColumnByName("colCFCProductName").Visible = false;
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
                       
            if (AIMGlobal.exportbatchMissingIngredientOrNutrient == true)
            {
                grdViewAnalyst.Columns.ColumnByName("colCFCIngredientName").Visible = true;
                grdViewAnalyst.Columns.ColumnByName("colCFCNutrientName").Visible = true;
            }
        }
        private void dataBindSummary()
        {                        
            try
            {                
                txtProductCode.Text = (String)grdViewAnalyst.GetFocusedRowCellValue(colCFCProductCode);
                lblFrmPlant.Visible = true;
                txtFrmPlant.Visible = true;
                if (cbDate.Checked == true)
                {
                    lblFrmPlant.Visible = false;
                    txtFrmPlant.Visible = false;
                }
                
                txtArchiveVersion.Text = (String)grdViewAnalyst.GetFocusedRowCellValue(colArchiveVersion);
                txtProductionVersion.Text = (String)grdViewAnalyst.GetFocusedRowCellValue(colProductionVersion);
                txtCFCProductName.Text = (String)grdViewAnalyst.GetFocusedRowCellValue(colCFCProductName);
                txtNutrientCode.Text = (String)grdViewAnalyst.GetFocusedRowCellValue(colCFCNutrientCode);
                txtNutrientName.Text = (String)grdViewAnalyst.GetFocusedRowCellValue(colCFCNutrientName);
                txtFrmPlant.Text = (String)grdViewAnalyst.GetFocusedRowCellValue(colPlantCode);
                if (grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID) == null)
                {
                    txtProductID.Text = "";
                }
                else
                {
                    txtProductID.Text = grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID).ToString();
                }                
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
                                
                if (grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID) == null)
                {
                    txtAimMaterialID.Text = "";
                }
                else
                {
                    txtAimMaterialID.Text = grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID).ToString();
                }
                if (txtProductCode.Text == "")
                {
                    txtProductCode.Text = "No XRef";
                    txtProductID.Text = "0";
                }
                if (txtCFCProductName.Text == "")
                {
                    txtCFCProductName.Text = "No XRef";
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

                txtSampleValueSummaryLine.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colSampleValue)).ToString("N3");
                txtSpecNutrientMax.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colSpecNutrientMax)).ToString("N3");
                txtSpecNutrientMin.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colSpecNutrientMin)).ToString("N3");
                txtDifferenceBelowMin.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colDifferenceBelowMin)).ToString("N3");
                txtDifferenceAboveMax.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colDifferenceAboveMax)).ToString("N3");
                txtMean.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colTrimmedMean)).ToString("N3");
                txtStandardDeviation.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colTrimmedStandardDeviation)).ToString("N3");                
                txtNumberOfSamples.Text = Convert.ToDecimal(grdViewAnalyst.GetFocusedRowCellValue(colNumberOfSample)).ToString();
                txtTrimmedSampleSize.Text = Convert.ToInt32(grdViewAnalyst.GetFocusedRowCellValue(colTrimmedSampleSize)).ToString();
                txtTotalSampleSize.Text = Convert.ToInt32(grdViewAnalyst.GetFocusedRowCellValue(colTotalSampleSize)).ToString();
            }
            catch (Exception)
            {
                txtProductCode.Text = String.Empty;
                txtCFCProductName.Text = String.Empty;
                txtNutrientCode.Text = String.Empty;
                txtNutrientName.Text = String.Empty;
                txtSpecNutrientMax.Text = String.Empty;
                txtSpecNutrientMin.Text = String.Empty;
                txtDifferenceBelowMin.Text = String.Empty;
                txtDifferenceAboveMax.Text = String.Empty;
                txtMean.Text = String.Empty;
                txtStandardDeviation.Text = String.Empty;                
                txtNumberOfSamples.Text = String.Empty;
                txtTrimmedSampleSize.Text = String.Empty;
                txtTotalSampleSize.Text = String.Empty;
                txtArchiveVersion.Text = String.Empty;
                txtProductionVersion.Text = String.Empty;
                txtSampleValueSummaryLine.Text = String.Empty;
            }
        }
        private void dataBindTemplateGrids()
        {
            grdMaterial.DataSource = AIMManager.GetAIMMaterialForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Material);
            grdNutrient.DataSource = AIMManager.GetAIMNutrientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.NutrientCode);
            grdOwner.DataSource = AIMManager.GetAIMOwnerForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Owner);            
            grdOwnerLocation.DataSource = AIMManager.GetAIMOwnerLocationForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.OwnerLocation);            
            grdMaterialNutrient.DataSource = AIMManager.GetMaterialNutrientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.MaterialNutrient);            
            grdSampleType.DataSource = AIMManager.GetSampleTypeForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleType);

            //12/21/22
            grdProductType.DataSource = AIMManager.GetCFCProductTypeForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.ProductType);

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
                AnalysisSharedFunctions.SetDateTextField(txtDateOption, dtpValue, dtpSecondValue, (QueryOperationEnum)sampleDate[0].QueryOperationID, sampleDate[0].Value);
            }
        }
        private void dataBindIncludeExcludeBoxes()
        {
            AnalysisSharedFunctions.DataBindBox(grdMaterialView, rbMaterialInclude, rbMaterialExclude, "Material");
            AnalysisSharedFunctions.DataBindBox(grdNutrientView, rbNutrientInclude, rbNutrientExclude, "Nutrient");
            AnalysisSharedFunctions.DataBindBox(grdOwnerView, rbOwnerInclude, rbOwnerExclude, "Owner");
            AnalysisSharedFunctions.DataBindBox(grdOwnerLocationView, rbOwnerLocInclude, rbOwnerLocExclude, "OwnerLoc");            
            AnalysisSharedFunctions.DataBindBox(grdSampleTypeView, rbSampleTypeInclude, rbSampleTypeExclude, "SampleType");

            //12/13/22
            AnalysisSharedFunctions.DataBindBox(grdProductTypeView, rbProductTypeInclude, rbProductTypeExclude, "ProductType");
        }

        //3/4/21 commented, not used?
        //private void dataBindSelectedQueryDetail()
        //{            
        //    _isFormLoaded = false;
        //    Int32 fromAnalysisTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
        //    _isFormLoaded = true;
        //}

        #endregion region

        private void btnRecalculate_Click(object sender, EventArgs e)
        {
            {               

                try
                {
                    //12/1/22
                    if (rbNutrientTolerance.Checked == true && (Int32)lueNutrientTolerance.EditValue == 0)
                    {
                        MessageBox.Show("Please select a Nutrient Tolerance group.", "Warning...", MessageBoxButtons.OK);
                        lueNutrientTolerance.ShowPopup();
                        return;
                    }

                    dataBindGroupBy(false);
                    btnRecalculate.BackColor = SystemColors.Control;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }        
        public void SetbtnRecalculate()
        {
            btnRecalculate.BackColor = Color.Yellow;
        }

        private void backgroundWorkerDataBindGroupBy_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            AnalysisQueryEnum analysisQuery = AnalysisQueryEnum.NoGroup;
            OutlierTypeEnum outlierType = OutlierTypeEnum.NoOutlierExcluded;
            Int32 queryTemplateID;
            DateTime? startDate = null;
            DateTime? endDate = null;

            //12/1/22
            //Boolean groupMaterials = false;     //3/10/21
            Int32 groupMaterials = (Int32)EnumTolerance.None;
            Int32 nutrientToleranceHeaderID = 0;
            
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
                }

                //3/10/21
                if (rbProductTolerance.Checked == true)
                {
                    groupMaterials = (Int32)EnumTolerance.ProductTolerance;
                }
                else if(rbNutrientTolerance.Checked == true)
                {
                    groupMaterials = (Int32)EnumTolerance.NutrientTolerance;
                }

                nutrientToleranceHeaderID = (Int32)lueNutrientTolerance.EditValue;

                
                //12/1/22 
                //e.Result = AIMManager.GetAnalysisProduct(analysisQuery, queryTemplateID, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), groupMaterials, (Int32)_mainMenu.lueDataMap.EditValue, outlierType);
                e.Result = AIMManager.GetAnalysisProduct(analysisQuery, queryTemplateID, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), groupMaterials, nutrientToleranceHeaderID, (Int32)_mainMenu.lueDataMap.EditValue, outlierType);

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
                grdAnalysis.DataSource = e.Result;

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
                AIMGlobal.SplashScreenHide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
            _currentPrintLayout = LoadPrintLayoutFromTemplate();   //task1175 FG 7/2/2026
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
        #region Report Print Layout   //task1175 FG 7/2/2026

        private ReportPrintLayoutInfo LoadPrintLayoutFromTemplate()   //task1175 FG 7/2/2026
        {
            if ((Int32)lueViewTemplate.EditValue == 0)
            {
                return CFCAIMCore.Global.GetDefaultPrintLayout();
            }

            AnalysisViewTemplateInfo viewTemplate = (AnalysisViewTemplateInfo)lueViewTemplate.GetSelectedDataRow();
            if (viewTemplate == null)
            {
                return CFCAIMCore.Global.GetDefaultPrintLayout();
            }

            AnalysisViewTemplateInfo criteria = AIMManager.GetAnalysisViewTemplate(viewTemplate.AnalysisViewTemplateID);
            return CFCAIMCore.Global.DeserializePrintLayout(criteria.PrintLayoutXML);
        }

        private void SavePrintLayoutToTemplate(ReportPrintLayoutInfo layout)   //task1175 FG 7/2/2026
        {
            if ((Int32)lueViewTemplate.EditValue == 0)
            {
                return;
            }

            AnalysisViewTemplateInfo viewTemplate = (AnalysisViewTemplateInfo)lueViewTemplate.GetSelectedDataRow();
            if (viewTemplate == null)
            {
                return;
            }

            AnalysisViewTemplateInfo criteria = AIMManager.GetAnalysisViewTemplate(viewTemplate.AnalysisViewTemplateID);
            criteria.PrintLayoutXML = CFCAIMCore.Global.SerializePrintLayout(layout);
            criteria.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
            AIMManager.SaveAnalysisViewTemplate(ref criteria);
        }

        private void OnPreviewClosed(ReportPrintLayoutInfo layout)   //task1175 FG 7/2/2026
        {
            if ((Int32)lueViewTemplate.EditValue == 0)
            {
                return;
            }

            if (PrintLayoutChanged(_currentPrintLayout, layout) == false)   //task1175 FG 7/2/2026
            {
                return;
            }

            DialogResult dialogResult = MessageBox.Show(
                "Save print settings to " + lueViewTemplate.Text.Trim() + "?",
                "Save Print Layout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);   //task1175 FG 7/2/2026

            if (dialogResult == DialogResult.Yes)
            {
                _currentPrintLayout = layout;
                SavePrintLayoutToTemplate(layout);
            }
        }

        private Boolean PrintLayoutChanged(ReportPrintLayoutInfo oldLayout, ReportPrintLayoutInfo newLayout)   //task1175 FG 7/2/2026
        {
            if (oldLayout == null || newLayout == null)
            {
                return true;
            }

            return oldLayout.Landscape != newLayout.Landscape
                || oldLayout.MarginLeft != newLayout.MarginLeft
                || oldLayout.MarginTop != newLayout.MarginTop
                || oldLayout.MarginRight != newLayout.MarginRight
                || oldLayout.MarginBottom != newLayout.MarginBottom;
        }

        #endregion

        private void grdViewAnalyst_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                Double lowerMin = 0;
                Double lowerMax = 0;
                Double upperMin = 0;
                Double upperMax = 0;
                Double sampleValue = 0;
                String cfcIngredientCode = "";
                String cfcNutrientCode = "";
                Boolean isValidOutlier = true;
                Boolean isOverlay = true;

                Double specLower = 0;
                Double specUpper = 0;
                Double predictedLower = 0;
                Double predictedUpper = 0;

                if (e.RowHandle >= 0)
                {
                    isOverlay = (Boolean)(grdViewAnalyst.GetRowCellValue(e.RowHandle, colIsOverlay));
                }

                if (e.Column == colSampleValue)
                {
                    sampleValue = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colSampleValue));
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

                if (e.Column == colPredictedNutrientVal)
                {
                    e.Graphics.DrawLine(Pens.DarkBlue, e.Bounds.Left, e.Bounds.Top, e.Bounds.Left, e.Bounds.Bottom);
                    e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
                    e.Handled = true;
                }

                //---------------------------- TOLERANCE COLUMNS --------------------------------
                specLower = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colSpecLower));
                specUpper = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colSpecUpper));
                predictedLower = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colPredictedLower));
                predictedUpper = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colPredictedUpper));
                if (e.Column == colSpecLower)
                {                   
                    if (specLower > 0)
                    {
                        e.Appearance.BackColor = Color.Moccasin;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }                    
                }
                if (e.Column == colSpecUpper)
                {
                    if (specUpper > 0)
                    {
                        e.Appearance.BackColor = Color.Moccasin;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                }
                if (e.Column == colPredictedLower)
                {
                    if (predictedLower > 0)
                    {
                        e.Appearance.BackColor = Color.Lavender;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                }
                if (e.Column == colPredictedUpper)
                {
                    if (predictedUpper > 0)
                    {
                        e.Appearance.BackColor = Color.Lavender;
                        e.Appearance.FontStyleDelta = FontStyle.Bold;
                    }
                }

                
                //-------------------------------------------------------------------------------
                if (e.Column == colPredictedNutrientVal)
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

                //Is this needed for Product screen?
                if (e.Column == colCFCProductName)
                {
                    cfcIngredientCode = Convert.ToString(grdViewAnalyst.GetRowCellValue(e.RowHandle, colCFCProductCode));
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

                if (e.Column.Name.ToString() == "colFlagInsideWarningLimit")                            //Task1147 DB260422
                {
                    Int32 flagResult = (Int32)grdViewAnalyst.GetRowCellValue(e.RowHandle, colResultValueFlagAnalysis);

                    Image img = null;
                    if (flagResult == 1)
                        img = Properties.Resources.Flags3_3;
                    else if (flagResult == 2)
                        img = Properties.Resources.Flags3_2;

                    if (img != null)
                    {
                        e.DefaultDraw();
                        e.Graphics.DrawImage(img, e.Bounds.X + 4, e.Bounds.Y + 4, 16, 16);
                        e.Handled = true;
                    }
                }
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
        private void grdViewAnalyst_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
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

        
        #region Command Buttons
        private void btnPreview_Click(object sender, EventArgs e)
        {
            
            try
            {
                AIMGlobal.SplashScreenShow();
                if (_currentPrintLayout == null)   //task1175 FG 7/2/2026
                {
                    _currentPrintLayout = LoadPrintLayoutFromTemplate();
                }
                CFCAIMCore.Global.previewGridResults( grdAnalysis, "Full Screen Analysis - Products", _currentPrintLayout, OnPreviewClosed);   //task1175 FG 7/2/2026
                //CFCAIMCore.Global.previewGridResults(grdAnalysis, "Full Screen Analysis - Products"); //7/2/2026 replaced by previewGridResults
                AIMGlobal.SplashScreenHide();
            }
            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message);
            }

        }
        private void btnSaveQueryTemplateAs_Click(object sender, EventArgs e)
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
        private void btnSaveQueryTemplate_Click(object sender, EventArgs e)
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

                //10/31/22
                queryTemplate.IngredientOrProduct = "P";


                newAnalysisTemplateID = AIMManager.SaveQueryTemplate(ref queryTemplate).RecordID;
                if (newAnalysisTemplateID == 0)
                {
                    //couldn't save the template
                    pnlNewQueryTemplate.Visible = false;
                    pnlQueryButtons.Visible = true;
                    tabControlQuery.Visible = true;
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
                    pnlQueryButtons.Visible = true;
                    tabControlQuery.Visible = true;

                }

                txtNewQueryTemplateName.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancelQueryTemplateSave_Click(object sender, EventArgs e)
        {
            try
            {
                pnlNewQueryTemplate.Visible = false;
                pnlQueryButtons.Visible = true;
                txtNewQueryTemplateName.Text = String.Empty;
                txtDateOption.Text = String.Empty;
                tabControlQuery.Visible = true;
                if ((Int32)lueQueryTemplateInTab.EditValue == 0)
                {
                    tabControlQuery.Visible = false;

                }
                pnlNewQueryTemplate.Location = new Point(435, 60);  //moved out of the way
                pnlNewQueryTemplate.Size = new Size(609, 10);       //and made shorter
                pnlNewQueryTemplate.SendToBack();
                tabControlQuery.Visible = true;

                AnalysisSharedFunctions.CheckYellowBox(lblYellowBox, txtYellowBox, lueQueryTemplateInTab);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDeleteQueryTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isAnalysis == false)
                {
                    MessageBox.Show("This template was created in Reports and cannot be modified here. ", "Warning...");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Delete Template?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult != DialogResult.Yes)
                {
                    return;
                }
                AIMManager.DeleteQueryTemplate((Int32)lueQueryTemplateInTab.EditValue);

                //10/31/22
                //AnalysisSharedFunctions.DataBindTemplateDropdowns(lueQueryTemplate, lueQueryTemplateInTab);
                AnalysisSharedFunctions.populateQueryTemplate(lueQueryTemplate, lueQueryTemplateInTab, "P");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnUpdateCurrentViewTemplate_Click(object sender, EventArgs e)
        {
            AnalysisViewTemplateInfo verifyTemplate = new AnalysisViewTemplateInfo();               //Task1148 DB260414

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

                verifyTemplate = (AnalysisViewTemplateInfo)lueViewTemplate.GetSelectedDataRow();              //Task1148 DB260414 Start
                AnalysisViewTemplateInfo criteria = new AnalysisViewTemplateInfo();
                criteria = AIMManager.GetAnalysisViewTemplate(verifyTemplate.AnalysisViewTemplateID);

                if (criteria.CFCUserID != _userGlobal.CFCLoginUser.CFCUserID)
                {
                    DialogResult saveAsNewTeamplate = MessageBox.Show("Owner User " + criteria.UserName + " of this template does not match current user. Please have " + criteria.UserName + " edit current template or Save As New. Would you like to save as new?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (saveAsNewTeamplate == DialogResult.Yes)
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
                    }

                    return;
                }                                                                                               //Task1148 DB260414 End

                DialogResult dialogResult = MessageBox.Show("Overwrite " + lueViewTemplate.Text.ToString().TrimEnd() + "?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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
        private void btnDeleteViewTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                AnalysisViewTemplateInfo viewTemplate = (AnalysisViewTemplateInfo)lueViewTemplateInTab.GetSelectedDataRow();
                if (viewTemplate == null || viewTemplate.AnalysisViewTemplateID == 0)
                {
                    return;
                }

                if (viewTemplate.CFCUserID != _userGlobal.CFCLoginUser.CFCUserID)       //Task1148 DB260416 Start
                {
                    MessageBox.Show("Owner User " + viewTemplate.UserName + " of this template does not match current user. To delete template, please have " + viewTemplate.UserName + " make changes.", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    return;
                }                                                                       //Task1148 DB260416 End

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
        private void btnSaveViewTemplate_Click(object sender, EventArgs e)
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

                    //10/31/22
                    //viewTemplate.GridNumber = (Int32)EnumGridNumber.AnalysisIngredient;
                    viewTemplate.GridNumber = (Int32)EnumGridNumber.AnalysisProduct; 

                    //TemplateType 0 = none; TemplateType 1 = gridControl; TemplateType 2 = pivotControl
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
                        //Task1112 DB251015 viewTemplate.GridNumber = (Int32)EnumGridNumber.AnalysisIngredient;
                        viewTemplate.GridNumber = (Int32)EnumGridNumber.AnalysisProduct;        //Task1112 DB251015 

                        //TemplateType 0 = none; TemplateType 1 = gridControl; TemplateType 2 = pivotControl
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
                    viewTemplateForSaveAs.GridNumber = (Int32)EnumGridNumber.AnalysisIngredient;
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
        private void btnNewQueryTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                pnlQueryButtons.Visible = false;
                pnlNewQueryTemplate.Visible = true;
                lblYellowBox.Visible = false;
                txtYellowBox.Visible = false;
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

                AIMGlobal.comingFromAnalysis = true;

                //12/23/22
                AIMGlobal.fromAnalysisIngredientOrProduct = (Int32)EnumIngredientOrProduct.AnalysisProduct;
                AIMGlobal.nutrientToleranceHeaderID = (Int32)lueNutrientTolerance.EditValue;
                if (rbProductTolerance.Checked == true)
                {
                    AIMGlobal.groupMaterials = (Int32)EnumTolerance.ProductTolerance;
                }
                else if (rbNutrientTolerance.Checked == true)
                {
                    AIMGlobal.groupMaterials = (Int32)EnumTolerance.NutrientTolerance;
                }
                else
                {
                    AIMGlobal.groupMaterials = (Int32)EnumTolerance.None;
                }

                //1/4/23
                AIMGlobal.chartArchiveVersion = txtArchiveVersion.Text;
                
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

        }
        private void btnColumnCalcDescription_Click(object sender, EventArgs e)
        {
            AnalysisProductHelp helpDescriptions = new AnalysisProductHelp();
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
        private void btnRefreshMaterialNutrient_Click(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.RemoveZeroAimMaterialNutrientRecords(grdMaterialNutrient, _isAnalysis, lueQueryTemplateInTab);
            grdMaterialNutrient.Refresh();
            if (((List<TemplateMaterialNutrientInfo>)grdMaterialNutrient.DataSource).Count == 0)
            {
                lueMaterialNutCode.EditValue = 0;
                lueMaterialNutName.EditValue = 0;
            }

        }
        private void btnRemoveSampleDate_Click(object sender, EventArgs e)
        {
            AIMManager.DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleDate);
            dataBindSampleDate();
            txtDateOption.Text = "";

        }
        private void btnResize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                saveUserDefaults();     
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Tab Control functions
        private void tcoAnalysis_Selected(object sender, TabControlEventArgs e)
        {
            CurrentTab = e.TabPage;
        }
        private void tcoAnalysis_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbAllRecords.Checked = true;

            ToolTip ttip = new ToolTip();
            try
            {                

                switch (tcoAnalysis.SelectedIndex)
                {
                    case 0:

                        if (cbRecalculateRightAway.Checked == true)
                        {
                            dataBindGroupBy(false);
                        }

                        AnalysisSharedFunctions.RemoveZeroAimMaterialNutrientRecords(grdMaterialNutrient, _isAnalysis, lueQueryTemplateInTab);

                        lueQueryTemplate.EditValue = _selectedQueryTemplateID; //this will hit DatabindGroupBy
                        lueViewTemplate.EditValue = _selectedViewTemplateID;

                        //1/22/25
                        ckbHideColumnHeaders.Visible = true;
                        ckbHideGroupSummary.Visible = true;
                        ckbHideGroupBy.Visible = true;

                        break;
                    case 1:
                        lueQueryTemplateInTab.EditValue = _selectedQueryTemplateID;

                        //2/23/21
                        tabControlQuery.Visible = true;
                        if ((Int32)lueQueryTemplate.EditValue == 0)
                        {
                            tabControlQuery.Visible = false;
                        }

                        //1/22/25
                        ckbHideColumnHeaders.Visible = false;
                        ckbHideGroupSummary.Visible = false;
                        ckbHideGroupBy.Visible = false;

                        break;
                    case 2:
                        lueViewTemplateInTab.EditValue = _selectedViewTemplateID;

                        ttip.SetToolTip(btnNewViewTemplate, " Add New View");
                        ttip.SetToolTip(btnSaveViewTemplateAs, "Save View As");

                        //1/22/25
                        ckbHideColumnHeaders.Visible = false;
                        ckbHideGroupSummary.Visible = false;
                        ckbHideGroupBy.Visible = false;

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
        private void tcoAnalysis_Deselected(object sender, TabControlEventArgs e)
        {
            PreviousTab = e.TabPage;
        }
        #endregion

        #region LookUpEdits
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
                SetbtnRecalculate();
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
                }
                else
                {
                    gpbDate.Visible = true;
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
                    _currentPrintLayout = null;   //task1175 FG 7/2/2026
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
                        btnSaveQueryTemplateAs.Enabled = false;
                    }
                    else
                    {
                        btnSaveQueryTemplateAs.Enabled = true;
                    }
                }

                QueryTemplateInfo analysisTemplate = (QueryTemplateInfo)lueQueryTemplateInTab.GetSelectedDataRow();
                if (analysisTemplate != null && analysisTemplate.QueryTemplateID > 0)
                {
                    if (analysisTemplate.IsAnalysis == true)
                    {
                        _isAnalysis = true;
                        btnDeleteQueryTemplate.Visible = true;
                    }
                    else
                    {
                        _isAnalysis = false;
                        btnDeleteQueryTemplate.Visible = false;
                        lblCannotUpdate.Visible = true;
                        lblCannotUpdate.BringToFront();

                    }
                }

                dataBindTemplateGrids();
                dataBindSampleDate();
                AnalysisSharedFunctions.CheckYellowBox(lblYellowBox, txtYellowBox, lueQueryTemplateInTab);
                clearIncludeExclude();
                dataBindIncludeExcludeBoxes();

                tabControlQuery.SelectedIndex = 0;
                if (analysisTemplate != null)                   //Task1148 DB260416 Start
                {
                    if (analysisTemplate.CFCUserID != _userGlobal.CFCLoginUser.CFCUserID)
                    {
                        tpSampleDate.Enabled = false;
                        tpMaterial.Enabled = false;
                        tpNutrient.Enabled = false;
                        tpOwner.Enabled = false;
                        tpOwnerLocation.Enabled = false;
                        tpMaterialNutrient.Enabled = false;
                        tpSampleType.Enabled = false;
                        tpProductType.Enabled = false;
                        btnDeleteQueryTemplate.Enabled = false;
                    }
                    else
                    {
                        tpSampleDate.Enabled = true;
                        tpMaterial.Enabled = true;
                        tpNutrient.Enabled = true;
                        tpOwner.Enabled = true;
                        tpOwnerLocation.Enabled = true;
                        tpMaterialNutrient.Enabled = true;
                        tpProductType.Enabled = true;
                        btnDeleteQueryTemplate.Enabled = true;
                    }
                }                                               //Task1148 DB260416 Start

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void lueViewTemplateInTab_Click(object sender, EventArgs e)
        {
            lueViewTemplateInTab.SelectAll();
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
        private void lueOperator_EditValueChanged(object sender, EventArgs e)
        {
            QueryOperationEnum queryOperation;

            //3/5/21 - simplified
            //Int32 operation;
            //Int32.TryParse(lueOperator.EditValue.ToString(), out operation);
            Int32.TryParse(lueOperator.EditValue.ToString(), out Int32 operation);

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
        #endregion

        #region Radio Buttons                
        private void rbAllRecords_Click(object sender, EventArgs e)
        {
            btnRecalculate.PerformClick();
        }
        private void rbOutliersOnly_Click(object sender, EventArgs e)
        {
            checkOutliersOnly();
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

            SetbtnRecalculate();

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

            SetbtnRecalculate();

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

            SetbtnRecalculate();

        }
        private void rbMildOutlierExclude_CheckedChanged(object sender, EventArgs e)
        {
            SetbtnRecalculate();
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
        private void rbAllRecords_CheckedChanged(object sender, EventArgs e)
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
        private void rbOutliersOnly_CheckedChanged(object sender, EventArgs e)
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
        private void rbProducts_CheckedChanged(object sender, EventArgs e)
        {
            dataBindMaterial();
        }
        #endregion

        private void checkOutliersOnly()
        {

            Double lowerMin;
            Double lowerMax;
            Double upperMin;
            Double upperMax;
            Boolean isValidOutlier = true;


            if (rbOutliersOnly.Checked == true)
            {
                //3/4/21 - removed unnecessary assignment
                //List<AnalysisProductInfo> grid = new List<AnalysisProductInfo>();
                List<AnalysisProductInfo> grid;

                List<AnalysisProductInfo> temp = new List<AnalysisProductInfo>();                               

                if (grdViewAnalyst.DataSource != null)
                {
                    grid = (List<AnalysisProductInfo>)grdAnalysis.DataSource;
                    foreach (AnalysisProductInfo item in grid)
                    {
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
                    grdAnalysis.DataSource = temp;
                }
            }
        }
        private void setOutlierButtons(Boolean init)
        {
            //2/23/17 - added because outliers can be changed on chart screen 
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
                ttip.SetToolTip(btnSaveViewTemplate, "Save Template");
                ttip.SetToolTip(btnCancelViewTemplateSave, "Cancel");
                ttip.SetToolTip(btnDeleteViewTemplate, "Delete Template");
                ttip.SetToolTip(btnRecalculate, "Load/Recalculate Data");
                ttip.SetToolTip(btnUpdateCurrentViewTemplate, "Save Current Template/View");
                ttip.SetToolTip(btnColumnCalcDescription, "Calculation Descriptions");
                ttip.SetToolTip(this.btnPreview, "Preview Grid Results");
                ttip.SetToolTip(this.btnHelp, "Help");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
                ttip.SetToolTip(btnGraph, "Charts");
                ttip.SetToolTip(btnExit, "");
                ttip.SetToolTip(this.btnAddMaterial, "Add Material to Query");
                ttip.SetToolTip(this.btnAddNutrient, "Add Nutrient to Query");
                ttip.SetToolTip(this.btnAddOwner, "Add Owner to Query");
                ttip.SetToolTip(this.btnAddOwnerLocation, "Add Owner Location to Query");                
                ttip.SetToolTip(this.btnAddSampleDate, "Add Sample Date to Query");
                ttip.SetToolTip(this.btnAddMaterialNutrient, "Add Material to Query");                
                ttip.SetToolTip(this.btnRemoveSampleDate, "Remove Sample Date from Query");
                ttip.SetToolTip(this.btnAddSampleType, "Add Sample Type to Query");

                //12/13/22
                ttip.SetToolTip(this.btnAddProductType, "Add Product Type to Query");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clearIncludeExclude()
        {
            rbMaterialInclude.Checked = true;
            rbNutrientInclude.Checked = true;
            rbOwnerInclude.Checked = true;
            rbOwnerLocInclude.Checked = true;
            
        }
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
                    if (defaults.AllRecords == true) { rbAllRecords.Checked = true; } else { rbAllRecords.Checked = false; }
                    if (defaults.OutliersOnly == true) { rbOutliersOnly.Checked = true; } else { rbOutliersOnly.Checked = false; }
                    if (defaults.ExtremeOutlierInclude == true) { rbExtremeOutlierInclude.Checked = true; } else { rbExtremeOutlierInclude.Checked = false; }
                    if (defaults.ExtremeOutlierExclude == true) { rbExtremeOutlierExclude.Checked = true; } else { rbExtremeOutlierExclude.Checked = false; }
                    if (defaults.MildOutlierInclude == true) { rbMildOutlierInclude.Checked = true; } else { rbMildOutlierInclude.Checked = false; }
                    if (defaults.MildOutlierExclude == true) { rbMildOutlierExclude.Checked = true; } else { rbMildOutlierExclude.Checked = false; }

                    //12/13/22
                    //if (defaults.TemplateQuery > 0) { lueQueryTemplate.EditValue = defaults.TemplateQuery; } else { lueQueryTemplate.EditValue = 0; }
                    //if (defaults.TemplateView > 0) { lueViewTemplate.EditValue = defaults.TemplateView; } else { lueViewTemplate.EditValue = 0; }
                    if (defaults.TemplateQuery > 0 && defaults.IngredientOrProduct == "P") { lueQueryTemplate.EditValue = defaults.TemplateQuery; } else { lueQueryTemplate.EditValue = 0; }
                    if (defaults.TemplateView > 0 && defaults.IngredientOrProduct == "P") { lueViewTemplate.EditValue = defaults.TemplateView; } else { lueViewTemplate.EditValue = 0; }

                    if (defaults.RecalculateOnModify == true) { cbRecalculateRightAway.Checked = true; } else { cbRecalculateRightAway.Checked = false; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void saveUserDefaults()
        {
            try
            {
                AnalysisUserDefaultSettingsInfo defaults = new AnalysisUserDefaultSettingsInfo();
                defaults.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
                if (cbDate.Checked == true) { defaults.GroupByDate = true; } else { defaults.GroupByDate = false; }
                if (cbSampleID.Checked == true) { defaults.GroupBySampleID = true; } else { defaults.GroupBySampleID = false; }                
                if (rbAllRecords.Checked == true) { defaults.AllRecords = true; } else { defaults.AllRecords = false; }
                if (rbOutliersOnly.Checked == true) { defaults.OutliersOnly = true; } else { defaults.OutliersOnly = false; }
                if (rbExtremeOutlierInclude.Checked == true) { defaults.ExtremeOutlierInclude = true; } else { defaults.ExtremeOutlierInclude = false; }
                if (rbExtremeOutlierExclude.Checked == true) { defaults.ExtremeOutlierExclude = true; } else { defaults.ExtremeOutlierExclude = false; }
                if (rbMildOutlierInclude.Checked == true) { defaults.MildOutlierInclude = true; } else { defaults.MildOutlierInclude = false; }
                if (rbMildOutlierExclude.Checked == true) { defaults.MildOutlierExclude = true; } else { defaults.MildOutlierExclude = false; }
                if (lueQueryTemplate.EditValue != null) { defaults.TemplateQuery = (Int32)lueQueryTemplate.EditValue; } else { defaults.TemplateQuery = 0; }
                if (lueViewTemplate.EditValue != null) { defaults.TemplateView = (Int32)lueViewTemplate.EditValue; } else { defaults.TemplateView = 0; }
                if (cbRecalculateRightAway.Checked == true) { defaults.RecalculateOnModify = true; } else { defaults.RecalculateOnModify = false; }

                //12/13/22
                defaults.IngredientOrProduct = "P";

                ErrorInfo result = AIMManager.AnalysisUserDefaultSettingsSave(ref defaults);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void relinkNumberOfSamples()
        {
            try
            {
                OutlierTypeEnum outlierType = new OutlierTypeEnum();
                AnalysisQueryEnum analysisQuery = AnalysisQueryEnum.NoGroup;
                Int32 sampleID = 0;
                Int32 aimOwnerID = 0;
                Int32 aimOwnerLocationID = 0;
                String sampleCode = "";

                //Int32 cfcPlantID = 0;
                //Int32 cfcProductID = 0;

                DateTime sampleDate = new DateTime();                         
                DateTime startDate = deStart.DateTime;
                DateTime endDate = deEnd.DateTime;

                Int32.TryParse(lueQueryTemplate.EditValue.ToString(), out Int32 queryTemplateID);
                Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colAIMMaterialID).ToString(), out Int32 aimMaterialID);
                Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colAIMNutrientID).ToString(), out Int32 aimNutrientID);
                outlierType = OutlierTypeEnum.NoOutlierExcluded;

                //12/1/22                
                Int32 groupMaterials = (Int32)EnumTolerance.None;
                Int32 nutrientToleranceHeaderID = 0;

                //1/4/23
                String archiveVersion = String.Empty;

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
                }                
                
                if (cbDate.Checked == true && grdViewAnalyst.GetFocusedRowCellValue(colDate) != null)
                {
                    DateTime.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colDate).ToString(), out sampleDate);
                }
                if (cbSampleID.Checked == true && grdViewAnalyst.GetFocusedRowCellValue(colSampleID) != null)
                {
                    Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colSampleID).ToString(), out sampleID);
                }                

                analysisQuery = AnalysisQueryEnum.SampleID;

               
                if (rbProductTolerance.Checked == true)
                {
                    groupMaterials = (Int32)EnumTolerance.ProductTolerance;
                }

                //12/1/22
                if (rbNutrientTolerance.Checked == true)
                {
                    groupMaterials = (Int32)EnumTolerance.NutrientTolerance;                    
                }

                //12/1/22
                nutrientToleranceHeaderID = (Int32)lueNutrientTolerance.EditValue;

                //1/4/23
                archiveVersion = txtArchiveVersion.Text;

                AIMGlobal.sampleDetailProduct = AIMManager.GetAnalysisProduct(analysisQuery, queryTemplateID, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), groupMaterials, nutrientToleranceHeaderID, true, aimMaterialID, aimNutrientID, sampleDate, sampleID, aimOwnerID, aimOwnerLocationID, sampleCode, (Int32)_mainMenu.lueDataMap.EditValue, outlierType, archiveVersion);
                AIMGlobal.SplashScreenHide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void replnkNoOfSamples_Click(object sender, EventArgs e)
        {
            try
            {
                relinkNumberOfSamples();
                this.SendToBack();
                                
                //12/20/22
                _sampleDetail = new AnalysisProductSampleDetail();

                _sampleDetail.ShowDialog();

                relinkNumberOfSamples();
                rbOutliersOnly.Checked = AIMGlobal.analysisOutliersOnly;
                rbAllRecords.Checked = AIMGlobal.analysisAllRecords;

                rbExtremeOutlierExclude.Checked = AIMGlobal.analysisExcludeExtremeOutliers;
                rbExtremeOutlierInclude.Checked = AIMGlobal.analysisIncludeExtremeOutliers;
                rbMildOutlierExclude.Checked = AIMGlobal.analysisExcludeMildOutliers;
                rbMildOutlierInclude.Checked = AIMGlobal.analysisIncludeMildOutliers;

                SetbtnRecalculate();
                WindowState = FormWindowState.Maximized;
                this.BringToFront();
                WindowState = FormWindowState.Maximized;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }       

        private void grdViewAnalyst_DoubleClick(object sender, EventArgs e)
        {
            //3/4/21 commented, val and valSampleID not used
            //GridView view = (GridView)sender;
            //Point pt = view.GridControl.PointToClient(Control.MousePosition);            
            //object val = grdViewAnalyst.GetRowCellValue(grdViewAnalyst.FocusedRowHandle, colSampleValue);
            //object valSampleID = grdViewAnalyst.GetRowCellValue(grdViewAnalyst.FocusedRowHandle, colSampleID);

        }
        private void grdViewAnalyst_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            AIMGlobal.focusedRowGreen(sender, e);
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
                SetbtnRecalculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errror...", MessageBoxButtons.OK);
                throw;
            }

        }                      

        private void analysis_Product_Activated(object sender, EventArgs e)
        {
            setOutlierButtons(false);   //added because outliers can be changed on chart screen 
            CFCAIMCore.Global.sampleID = 0;
            AIMGlobal.sampleCode = "";
            AIMGlobal.fromAnalysisScreen = false;


        }

        private void tabControlQuery_DrawItem(object sender, DrawItemEventArgs e)
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
                
                case 5:         //Material-Nutrient
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
               
                case 6:         //SampleType
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
                    
                //12/21/22
                case 7:         //Productype
                    countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.ProductType);
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
                        
            AnalysisSharedFunctions.CheckYellowBox(lblYellowBox, txtYellowBox, lueQueryTemplateInTab);
        }                     

        private Int32 checksBeforeAdding(String type, DevExpress.XtraEditors.LookUpEdit lue)
        {
            Int32 rtn;            
            rtn = AnalysisSharedFunctions.ChecksBeforeAdding(type, lueQueryTemplateInTab, lue);
            return rtn;
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

        private void tabControlQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlQuery.SelectedIndex != 6)
            {
                AnalysisSharedFunctions.RemoveZeroAimMaterialNutrientRecords(grdMaterialNutrient, _isAnalysis, lueQueryTemplateInTab);
            }

        }

        private void replueMaterialNutrientName_Validating(object sender, CancelEventArgs e)
        {
            checkForMaterialNutrientDuplicates(sender, e);
        }
        private void replueMaterialNutrientCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
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

        private void grdMaterialNutrientView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
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
        private void grdMaterialNutrientView_ShownEditor(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            if (view.ActiveEditor is LookUpEdit)
            {
                LookUpEdit lookUp = ((LookUpEdit)view.ActiveEditor);

                if (!lookUp.IsPopupOpen)
                    lookUp.ShowPopup();
            }

        }
        private void grdMaterialNutrientView_GotFocus(object sender, EventArgs e)
        {
            grdMaterialNutrientView.ShowEditor();
        }

        private void updateQueryOperation(Int32 field, RadioButton rb, Int32 operationID, Boolean isAnd)
        {
            AIMManager.UpdateQueryOperationIDIsAnd((Int32)lueQueryTemplateInTab.EditValue, field, operationID, isAnd, _userGlobal.CFCLoginUser.CFCUserID);
        }


        #region Query Add Buttons
        private void btnAddSampleDate_Click(object sender, EventArgs e)
        {
            QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
            Int32 days = 0;
            QueryOperationEnum queryOperation;
            Int32 operation;

            if (AnalysisSharedFunctions.CreatedInReportsCheck(lueQueryTemplateInTab) != 0)
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
            AnalysisSharedFunctions.SetDateTextField(txtDateOption, dtpValue, dtpSecondValue, queryOperation, days);

            lueOperator.EditValue = 0;
            txtValue.Text = "";

        }
        private void btnAddMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbSingleMaterial.Checked == true)                                               //10/9/23
                {                                                                                   //10/9/23
                    if (checksBeforeAdding("Material", lueMaterialCode) != 0)
                    {
                        return;
                    }
                    AnalysisSharedFunctions.SaveQueryTemplateDetail((Int32)QueryFeildEnum.Material, lueQueryTemplateInTab, rbMaterialExclude, lueMaterialCode, _userGlobal.CFCLoginUser.CFCUserID);
                }

                //-------------------------------------------------------------
                else if (rbRangeMaterial.Checked == true)
                {
                    if ((Int32)lueMaterialBeg.EditValue == 0)
                    {
                        MessageBox.Show("Please select Range Begin value.", "Warning...");
                        lueMaterialBeg.ShowPopup();
                        return;
                    }
                    else if ((Int32)lueMaterialEnd.EditValue == 0)
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddNutrient_Click(object sender, EventArgs e)
        {
            try
            {
                if (checksBeforeAdding("Nutrient", lueNutrientCode) != 0)
                {
                    return;
                }
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
        private void btnAddOwner_Click(object sender, EventArgs e)
        {
            try
            {
                if (checksBeforeAdding("Owner", lueOwnerCode) != 0)
                {
                    return;
                }                
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
        private void btnAddOwnerLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (checksBeforeAdding("Owner Location", lueOwnerLocationCode) != 0)
                {
                    return;
                }
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
        
        private void btnAddMaterialNutrient_Click(object sender, EventArgs e)
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
        private void btnAddSampleType_Click(object sender, EventArgs e)
        {
            try
            {
                if (checksBeforeAdding("SampleType", lueSampleTypeName) != 0)
                {
                    return;
                }
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
        #endregion

        #region Query Delete Buttons
        private void repbtnDeleteMaterial_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                AIMMaterialInfo aimMaterial = new AIMMaterialInfo();
                aimMaterial = (AIMMaterialInfo)grdMaterialView.GetFocusedRow();
                if (aimMaterial != null)
                {
                    AnalysisSharedFunctions.DeleteQueryTemplateDetail(_isAnalysis, lueQueryTemplateInTab, aimMaterial.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();
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
        private void repbtnDeleteOwner_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                AIMOwnerInfo aimOwner = new AIMOwnerInfo();
                aimOwner = (AIMOwnerInfo)grdOwnerView.GetFocusedRow();
                if (aimOwner != null)
                {                    
                    AnalysisSharedFunctions.DeleteQueryTemplateDetail(_isAnalysis, lueQueryTemplateInTab, aimOwner.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();
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
        private void repbtnDeleteOwnerLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                AIMOwnerLocationInfo aimOwnerLocation = new AIMOwnerLocationInfo();
                aimOwnerLocation = (AIMOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();
                if (aimOwnerLocation != null)
                {                    
                    AnalysisSharedFunctions.DeleteQueryTemplateDetail(_isAnalysis, lueQueryTemplateInTab, aimOwnerLocation.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

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
        
        private void repbtnDeleteNutrient_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                AIMNutrientInfo aimNutrient = new AIMNutrientInfo();
                aimNutrient = (AIMNutrientInfo)grdNutrientView.GetFocusedRow();
                if (aimNutrient != null)
                {                    
                    AnalysisSharedFunctions.DeleteQueryTemplateDetail(_isAnalysis, lueQueryTemplateInTab, aimNutrient.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();
                
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
        private void repbtnDeleteMultiple_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                TemplateMaterialNutrientInfo materialNutrient = new TemplateMaterialNutrientInfo();
                materialNutrient = (TemplateMaterialNutrientInfo)grdMaterialNutrientView.GetFocusedRow();
                if (materialNutrient != null)
                {                    
                    AnalysisSharedFunctions.DeleteQueryTemplateDetail(_isAnalysis, lueQueryTemplateInTab, materialNutrient.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

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
                    AnalysisSharedFunctions.DeleteQueryTemplateDetail(_isAnalysis, lueQueryTemplateInTab, sampleType.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

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

        #endregion              

        #region Sync Values
        private void lueMaterialCode_EditValueChanged(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.SyncValueLue(lueMaterialCode, lueMaterialName);
        }
        private void lueMaterialName_EditValueChanged(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.SyncValueLue(lueMaterialName, lueMaterialCode);
        }
        private void lueOwnerCode_EditValueChanged(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.SyncValueLue(lueOwnerCode, lueOwnerName);
        }
        private void lueOwnerName_EditValueChanged(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.SyncValueLue(lueOwnerName, lueOwnerCode);
        }
        private void lueOwnerCode_Loc_EditValueChanged(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.SyncValueLue(lueOwnerCode_Loc, lueOwnerName_Loc);
            dataBindOwnerLocationlue();
        }
        private void lueOwnerName_Loc_EditValueChanged(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.SyncValueLue(lueOwnerName_Loc, lueOwnerCode_Loc);
            dataBindOwnerLocationlue();
        }
        private void lueOwnerLocationCode_EditValueChanged(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.SyncValueLue(lueOwnerLocationCode, lueOwnerLocationName);
        }
        private void lueOwnerLocationName_EditValueChanged(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.SyncValueLue(lueOwnerLocationName, lueOwnerLocationCode);
        }        
        private void lueMaterialNutCode_EditValueChanged(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.SyncValueLue(lueMaterialNutCode, lueMaterialNutName);
        }
        private void lueMaterialNutName_EditValueChanged(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.SyncValueLue(lueMaterialNutName, lueMaterialNutCode);
        }              
        private void lueNutrientCode_EditValueChanged(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.SyncValueLue(lueNutrientCode, lueNutrientName);
        }
        private void lueNutrientName_EditValueChanged(object sender, EventArgs e)
        {
            AnalysisSharedFunctions.SyncValueLue(lueNutrientName, lueNutrientCode);
        }        
        #endregion

        #region Include/Exclude
        private void rbMaterialInclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 4 (include), IsAnd s/b 0           
            updateQueryOperation((Int32)QueryFeildEnum.Material, rbMaterialInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbMaterialExclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 9 (exclude), IsAnd s/b 1           
            updateQueryOperation((Int32)QueryFeildEnum.Material, rbMaterialExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }
        private void rbNutrientInclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 4 (include), IsAnd s/b 0            
            updateQueryOperation((Int32)QueryFeildEnum.NutrientCode, rbNutrientInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbNutrientExclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 9 (exclude), IsAnd s/b 1
            updateQueryOperation((Int32)QueryFeildEnum.NutrientCode, rbNutrientExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }
        private void rbOwnerInclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 4 (include), IsAnd s/b 0            
            updateQueryOperation((Int32)QueryFeildEnum.Owner, rbOwnerInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbOwnerExclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 9 (exclude), IsAnd s/b 1            
            updateQueryOperation((Int32)QueryFeildEnum.Owner, rbOwnerExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }
        private void rbOwnerLocInclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 4 (include), IsAnd s/b 0            
            updateQueryOperation((Int32)QueryFeildEnum.OwnerLocation, rbOwnerLocInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbOwnerLocExclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 9 (exclude), IsAnd s/b 1            
            updateQueryOperation((Int32)QueryFeildEnum.OwnerLocation, rbOwnerLocExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }
                        
        private void rbSampleTypeInclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 4 (include), IsAnd s/b 0            
            updateQueryOperation((Int32)QueryFeildEnum.SampleType, rbSampleTypeInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbSampleTypeExclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 9 (exclude), IsAnd s/b 1            
            updateQueryOperation((Int32)QueryFeildEnum.SampleType, rbSampleTypeExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }

        //12/13/22
        private void rbProductTypeInclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 4 (include), IsAnd s/b 0            
            updateQueryOperation((Int32)QueryFeildEnum.ProductType, rbProductTypeInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        //12/13/22
        private void rbProductTypeExclude_Click(object sender, EventArgs e)
        {
            //QueryOperationID = 9 (exclude), IsAnd s/b 1            
            updateQueryOperation((Int32)QueryFeildEnum.ProductType, rbProductTypeExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }

        #endregion

        #region SelectAll        
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
        private void lueQueryTemplateInTab_Click(object sender, EventArgs e)
        {
            lueQueryTemplateInTab.SelectAll();
        }
        private void lueQueryTemplateInTab_Enter(object sender, EventArgs e)
        {
            lueQueryTemplateInTab.SelectAll();
        }
        private void lueMaterialCode_Click(object sender, EventArgs e)
        {
            lueMaterialCode.SelectAll();
        }
        private void lueMaterialCode_Enter(object sender, EventArgs e)
        {
            lueMaterialCode.SelectAll();
        }
        private void lueMaterialName_Click(object sender, EventArgs e)
        {
            lueMaterialName.SelectAll();
        }
        private void lueMaterialName_Enter(object sender, EventArgs e)
        {
            lueMaterialName.SelectAll();
        }
        private void lueNutrientCode_Click(object sender, EventArgs e)
        {
            lueNutrientCode.SelectAll();
        }
        private void lueNutrientName_Enter(object sender, EventArgs e)
        {
            lueNutrientCode.SelectAll();
        }
        private void lueOwnerCode_Click(object sender, EventArgs e)
        {
            lueOwnerCode.SelectAll();
        }
        private void lueOwnerCode_Enter(object sender, EventArgs e)
        {
            lueOwnerCode.SelectAll();
        }
        private void lueOwnerName_Click(object sender, EventArgs e)
        {
            lueOwnerName.SelectAll();
        }
        private void lueOwnerName_Enter(object sender, EventArgs e)
        {
            lueOwnerName.SelectAll();
        }
        private void lueOwnerCode_Loc_Click(object sender, EventArgs e)
        {
            lueOwnerCode_Loc.SelectAll();
        }
        private void lueOwnerCode_Loc_Enter(object sender, EventArgs e)
        {
            lueOwnerCode_Loc.SelectAll();
        }
        private void lueOwnerName_Loc_Click(object sender, EventArgs e)
        {
            lueOwnerName_Loc.SelectAll();
        }
        private void lueOwnerName_Loc_Enter(object sender, EventArgs e)
        {
            lueOwnerName_Loc.SelectAll();
        }
        private void lueOwnerLocationCode_Click(object sender, EventArgs e)
        {
            lueOwnerLocationCode.SelectAll();
        }
        private void lueOwnerLocationCode_Enter(object sender, EventArgs e)
        {
            lueOwnerLocationCode.SelectAll();
        }
        private void lueOwnerLocationName_Click(object sender, EventArgs e)
        {
            lueOwnerLocationName.SelectAll();
        }
        private void lueOwnerLocationName_Enter(object sender, EventArgs e)
        {
            lueOwnerLocationName.SelectAll();
        }       
        private void lueMaterialNutCode_Click(object sender, EventArgs e)
        {
            lueMaterialNutCode.SelectAll();
        }
        private void lueMaterialNutCode_Enter(object sender, EventArgs e)
        {
            lueMaterialNutCode.SelectAll();
        }
        private void lueMaterialNutName_Click(object sender, EventArgs e)
        {
            lueMaterialNutName.SelectAll();
        }
        private void lueMaterialNutName_Enter(object sender, EventArgs e)
        {
            lueMaterialNutName.SelectAll();
        }                             
        private void lueSampleTypeName_Click(object sender, EventArgs e)
        {
            lueSampleTypeName.SelectAll();
        }
        private void lueSampleTypeName_Enter(object sender, EventArgs e)
        {
            lueSampleTypeName.SelectAll();
        }
        #endregion

        tpProdSpec sample;
        private void repHyperOwnerLocationCode_Click(object sender, EventArgs e)
        {

        }

        private void repHyperLinkEditSampleCode_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.SplashScreenShow();

                //3/5/21 - simplified
                //Int32 sampleID;
                //Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colSampleID).ToString(), out sampleID);               
                Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colSampleID).ToString(), out Int32 sampleID);

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

                    //10/6/21 - commented, not necessary
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

        

        private void deStart_EditValueChanged(object sender, EventArgs e)
        {
            DateTime startDate = deStart.DateTime;
            DateTime endDate = deEnd.DateTime;
            AnalysisSharedFunctions.GetDateDuration(txtDuration, startDate, endDate);
            if (AIMGlobal.blnChangeAnalysisDate == true)
            {
                deStart.EditValue = Convert.ToDateTime("1/1/1980");
            }
            SetbtnRecalculate();
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
            SetbtnRecalculate();
        }

        //12/1/22
        private void DataBindTemplateLue()
        {
            List<NutrientToleranceHeaderInfo> nutTolerance = new List<NutrientToleranceHeaderInfo>();
            nutTolerance = AIMManager.GetNutrientToleranceHeader();
            nutTolerance.Insert
                (
                    0,
                    new NutrientToleranceHeaderInfo
                    {
                        NutrientToleranceHeaderID = 0,
                        NutrientToleranceHeaderName = "--Select--"
                    }
                );
            lueNutrientTolerance.Properties.DataSource = nutTolerance;
            XtraEditors.LoadLookUpEdit(ref lueNutrientTolerance, "NutrientToleranceHeaderID", "NutrientToleranceHeaderName");
            lueNutrientTolerance.Properties.Columns[1].Caption = "Tolerance Name";

            if (nutTolerance.Count == 2)
            {
                lueNutrientTolerance.EditValue = nutTolerance[1].NutrientToleranceHeaderID;
            }
            else
            {
                lueNutrientTolerance.EditValue = 0;

            }
        }

        //12/9/22
        private void rbToleranceNone_CheckedChanged(object sender, EventArgs e)
        {
            lueNutrientTolerance.Visible = false;
            lueProductTolerance.Visible = false;        //Task1125 DB251202
        }
        private void rbNutrientTolerance_CheckedChanged(object sender, EventArgs e)
        {
            lueNutrientTolerance.Visible = true;
            lueProductTolerance.Visible = false;        //Task1125 DB251202
        }
        private void rbProductTolerance_CheckedChanged(object sender, EventArgs e)
        {
            lueNutrientTolerance.Visible = false;
            lueProductTolerance.Visible = true;         //Task1125 DB251202
        }

        //12/13/22
        #region Product Type
        
        private void btnAddProductType_Click(object sender, EventArgs e)
        {
            try
            {
                if (checksBeforeAdding("ProductType", lueProductTypeName) != 0)
                {
                    return;
                }
                AnalysisSharedFunctions.SaveQueryTemplateDetail((Int32)QueryFeildEnum.ProductType, lueQueryTemplateInTab, rbProductTypeExclude, lueProductTypeName, _userGlobal.CFCLoginUser.CFCUserID);
                dataBindTemplateGrids();
                lueProductTypeName.EditValue = 0;
                lueProductTypeName.Focus();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueProductType_Click(object sender, EventArgs e)
        {
            lueProductTypeName.SelectAll();
        }

        private void lueProductType_Enter(object sender, EventArgs e)
        {
            lueProductTypeName.SelectAll();
        }

       
        //12/21/22
        private void repbtnDeleteProductType_Click(object sender, EventArgs e)
        {            
            try
            {
                CFCProductTypeInfo productType = new CFCProductTypeInfo();
                productType = (CFCProductTypeInfo)grdProductTypeView.GetFocusedRow();
                if (productType != null)
                {
                    AnalysisSharedFunctions.DeleteQueryTemplateDetail(_isAnalysis, lueQueryTemplateInTab, productType.QueryTemplateDetailID);
                }
                dataBindTemplateGrids();

                if (((List<CFCProductTypeInfo>)grdProductType.DataSource).Count == 0)
                {
                    rbProductTypeInclude.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        
        private void cbOutsideSpecsOnly_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxOptions();
        }
        private void cbOutsidePredictedOnly_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxOptions();
        }

        private void checkBoxOptions()
        {
            Double belowMinSpec;
            Double aboveMaxSpec;
            Double belowMinPredict;
            Double aboveMaxPredict;

            List<AnalysisProductInfo> grid;
            List<AnalysisProductInfo> temp = new List<AnalysisProductInfo>();

            if (cbOutsideSpecsOnly.Checked == true && cbOutsidePredictedOnly.Checked == true) 
            {               

                if (grdViewAnalyst.DataSource != null)
                {
                    grid = (List<AnalysisProductInfo>)grdAnalysis.DataSource;
                    foreach (AnalysisProductInfo item in grid)
                    {
                        belowMinSpec = item.MinSpecViolation;
                        aboveMaxSpec = item.MaxSpecViolation;
                        belowMinPredict = item.MinPredictedViolation;
                        aboveMaxPredict = item.MaxPredictedViolation;
                        if (belowMinSpec > 0)
                        {
                            temp.Add(item);
                        }
                        else if (aboveMaxSpec > 0)
                        {
                            temp.Add(item);
                        }
                        else if (belowMinPredict > 0)
                        {
                            temp.Add(item);
                        }
                        else if (aboveMaxPredict > 0)
                        {
                            temp.Add(item);
                        }
                    }
                    grdAnalysis.DataSource = temp;
                }
            }
            else if (cbOutsideSpecsOnly.Checked == true)
            {              

                if (grdViewAnalyst.DataSource != null)
                {
                    grid = (List<AnalysisProductInfo>)grdAnalysis.DataSource;
                    foreach (AnalysisProductInfo item in grid)
                    {
                        belowMinSpec = item.MinSpecViolation;
                        aboveMaxSpec = item.MaxSpecViolation;
                        if (belowMinSpec > 0)
                        {
                            temp.Add(item);
                        }
                        else if (aboveMaxSpec > 0)
                        {
                            temp.Add(item);
                        }
                    }
                    grdAnalysis.DataSource = temp;
                }
            }
            else if (cbOutsidePredictedOnly.Checked == true)
            {                
                if (grdViewAnalyst.DataSource != null)
                {
                    grid = (List<AnalysisProductInfo>)grdAnalysis.DataSource;
                    foreach (AnalysisProductInfo item in grid)
                    {
                        belowMinPredict = item.MinPredictedViolation;
                        aboveMaxPredict = item.MaxPredictedViolation;
                        if (belowMinPredict > 0)
                        {
                            temp.Add(item);
                        }
                        else if (aboveMaxPredict > 0)
                        {
                            temp.Add(item);
                        }
                    }
                    grdAnalysis.DataSource = temp;
                }
            }
            else
            {
                btnRecalculate.PerformClick();
            }
        }

        private void btnToleranceHelp_Click(object sender, EventArgs e)
        {
            AnalysisProductToleranceHelp helpTolerance = new AnalysisProductToleranceHelp();
            helpTolerance.ShowDialog();
        }

        //3/13/23        
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
                if (pnlGroupByToleranceOutliersResetPrintHelp.Visible == true)
                {
                    pnlGroupByToleranceOutliersResetPrintHelp.Location = TopLocation;
                    grdAnalysis.Location = ShowGroupBy;
                    //Task1167 DB260408 Grid height now calculated dynamically
                    //if (WindowState == FormWindowState.Normal)
                    //{
                    //    grdAnalysis.Size = NormalShowGroupBy;
                    //}
                    //else
                    //{
                    //    grdAnalysis.Size = MaxShowGroupBy;
                    //}
                }
                else
                {
                    //Both Summary and Group-By are hidden                                        
                    grdAnalysis.Location = HideBoth;
                    //Task1167 DB260408 Grid height now calculated dynamically
                    //if (WindowState == FormWindowState.Normal)
                    //{
                    //    grdAnalysis.Size = NormalHideBoth;
                    //}
                    //else
                    //{
                    //    grdAnalysis.Size = MaxHideBoth;
                    //}
                }

                //Show Summary
            }
            else
            {
                gbGroupSummary.Visible = true;

                if (pnlGroupByToleranceOutliersResetPrintHelp.Visible == true)
                {
                    pnlGroupByToleranceOutliersResetPrintHelp.Location = TopLocation;

                    gbGroupSummary.Location = ShowGroupBy;
                    ////gbGroupSummary.Location = ShowSummary;

                    grdAnalysis.Location = ShowBoth;
                    //Task1167 DB260408 Grid height now calculated dynamically
                    //if (WindowState == FormWindowState.Normal)
                    //{
                    //    grdAnalysis.Size = NormalShowBoth;
                    //}
                    //else
                    //{
                    //    grdAnalysis.Size = MaxShowBoth;
                    //}
                }
                else
                {
                    gbGroupSummary.Location = TopLocation;                    
                    grdAnalysis.Location = ShowSummary;
                    //Task1167 DB260408 Grid height now calculated dynamically
                    //if (WindowState == FormWindowState.Normal)
                    //{
                    //    grdAnalysis.Size = NormalShowSummary;
                    //}
                    //else
                    //{
                    //    grdAnalysis.Size = MaxShowSummary;
                    //}
                }
            }

            grdAnalysis.Height = tpaData.ClientSize.Height - grdAnalysis.Top - grdAnalysis.Margin.Bottom; //Task1167 DB260408
        }

        private void ckbHideGroupBy_CheckedChanged(object sender, EventArgs e)
        {

            //Hide GroupBy
            if (pnlGroupByToleranceOutliersResetPrintHelp.Visible == true)
            {
                pnlGroupByToleranceOutliersResetPrintHelp.Visible = false;
                if (gbGroupSummary.Visible == true)
                {
                    gbGroupSummary.Location = TopLocation;
                    grdAnalysis.Location = ShowSummary;
                    //Task1167 DB260408 Grid height now calculated dynamically
                    //if (WindowState == FormWindowState.Normal)
                    //{
                    //    grdAnalysis.Size = NormalShowSummary;
                    //}
                    //else
                    //{
                    //    grdAnalysis.Size = MaxShowSummary;
                    //}
                }
                else
                {
                    //Both Summary and Group-By are hidden                    
                    grdAnalysis.Location = HideBoth;
                    //Task1167 DB260408 Grid height now calculated dynamically
                    //if (WindowState == FormWindowState.Normal)
                    //{
                    //    grdAnalysis.Size = NormalHideBoth;
                    //}
                    //else
                    //{
                    //    grdAnalysis.Size = MaxHideBoth;
                    //}
                }
            }
            else
            {
                pnlGroupByToleranceOutliersResetPrintHelp.Visible = true;
                if (gbGroupSummary.Visible == true)
                {
                    pnlGroupByToleranceOutliersResetPrintHelp.Location = TopLocation;
                    gbGroupSummary.Location = ShowGroupBy;
                    grdAnalysis.Location = ShowBoth;
                    //Task1167 DB260408 Grid height now calculated dynamically
                    //if (WindowState == FormWindowState.Normal)
                    //{
                    //    grdAnalysis.Size = NormalShowBoth;
                    //}
                    //else
                    //{
                    //    grdAnalysis.Size = MaxShowBoth;
                    //}
                }
                else
                {
                    pnlGroupByToleranceOutliersResetPrintHelp.Location = TopLocation;
                    grdAnalysis.Location = ShowGroupBy;
                    //Task1167 DB260408 Grid height now calculated dynamically
                    //if (WindowState == FormWindowState.Normal)
                    //{
                    //    grdAnalysis.Size = NormalShowGroupBy;
                    //}
                    //else
                    //{
                    //    grdAnalysis.Size = MaxShowGroupBy;
                    //}
                }
            }

            grdAnalysis.Height = tpaData.ClientSize.Height - grdAnalysis.Top - grdAnalysis.Margin.Bottom; //Task1167 DB260408
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
                grdAnalysis.Cursor = Cursors.Default;
                return;
            }
            grdAnalysis.Cursor = Cursors.SizeNS;

            if (!isResizingColumnPanel && isCursorAboveColumnPanel && isLeftMouseButton)
            {
                isResizingColumnPanel = true;
                return;
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

        //10/9/23
        private void rbRangeMaterial_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRangeMaterial.Checked == true)
            {
                gbMaterialRange.Enabled = true;
                gbMaterialSingle.Enabled = false;
            }
        }

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


        //------------------------------------------------------------

    }
}

