using CFCAIM.Maintenance;
using CFCAIM.ReportDevExpress;
using CFCAIM.Sample;
using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Group;
using CFCAIMCore.Manager;
using CFCAPICore.Entity.Adisseo;
using CFCUser.Entity;
using CFCUser.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CFCAIM
{

    public partial class tpProdSpec : Form
    {
        MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];
        CFCUser.Global userGlobal = new CFCUser.Global();
        SampleInfo _selectedSample = new SampleInfo();
        AIMMaterialInfo _aimMaterial = new AIMMaterialInfo();
        UserInfo _user = new UserInfo();                                                                                //10/25/22
        SampleTabControlPageInfo _tabPage = new SampleTabControlPageInfo();                                             //8/23/23        

        List<AIMOwnerLocationInfo> _aimOwnerLocation = new List<AIMOwnerLocationInfo>();
        List<AIMOwnerInfo> _aimOwner = new List<AIMOwnerInfo>();
        List<DataSourceTypeInfo> _dataSourceType = new List<DataSourceTypeInfo>();
        List<DataSourceTypeInfo> _furtherAnalyses = new List<DataSourceTypeInfo>();
        List<AIMMaterialRefAnalyzerMaterialInfo> _aimMaterialXref = new List<AIMMaterialRefAnalyzerMaterialInfo>();
        List<AIMSupplierInfo> _supplier = new List<AIMSupplierInfo>();
        List<AIMSupplierLocationInfo> _supplierLocation = new List<AIMSupplierLocationInfo>();
        List<SampleResultInfo> _sampleResult = new List<SampleResultInfo>();
        List<SampleQualityInfo> _sampleQualityResults = new List<SampleQualityInfo>();                                  //1/9/18
        List<SampleResultPredictInfo> _sampleResultPredict = new List<SampleResultPredictInfo>();
        List<DataSourceTypeInfo> _dataSource = new List<DataSourceTypeInfo>();
        List<SampleResultTemplateInfo> _templateName = new List<SampleResultTemplateInfo>();
        List<SampleResultTemplateDetailInfo> _templateDetail = new List<SampleResultTemplateDetailInfo>();
        List<SampleQualityControlInfo> _qualityControl = new List<SampleQualityControlInfo>();
        List<SampleQualityControlMassEntryInfo> _qualityControlName = new List<SampleQualityControlMassEntryInfo>();    //1/5/18
        List<SampleQualityControlMassEntryDetailInfo> _qualityControlDetail = new List<SampleQualityControlMassEntryDetailInfo>();  //1/5/18
        List<AIMMaterialInfo> _aimMaterialForDropdown = new List<AIMMaterialInfo>();
        List<UserDefaultsInfo> _userDefaults = new List<UserDefaultsInfo>();                                            //1/12/18
        List<AIMOwnerSortInfo> _aimOwnerSort = new List<AIMOwnerSortInfo>();                                        //6/30/20
        List<AIMOwnerLocationSortInfo> _aimOwnerLocationSort = new List<AIMOwnerLocationSortInfo>();                //6/30/20
        List<AIMSupplierSortInfo> _aimSupplierSort = new List<AIMSupplierSortInfo>();                               //6/30/20
        List<AIMSupplierLocationSortInfo> _aimSupplierLocationSort = new List<AIMSupplierLocationSortInfo>();       //6/30/20
        List<SpecificIngredientsInfo> _specificIngredients = new List<SpecificIngredientsInfo>();                   //9/23/22
        List<SpecificProductsInfo> _specificProducts = new List<SpecificProductsInfo>();                            //5/19/23
        List<AIMOwnerLocationGroupDetailInfo> _locGroup = new List<AIMOwnerLocationGroupDetailInfo>();              //11/23/22
        List<SampleResultTemplateGridInfo> _templateDetailGrid = new List<SampleResultTemplateGridInfo>();          //Task1061 251030

        //11/16/23
        //List<SampleTestTypeInfo> _testTypeHeader = new List<SampleTestTypeInfo>();                                //8/11/23
        List<SampleTestTypeGroupInfo> _testTypeHeader = new List<SampleTestTypeGroupInfo>();                        //11/16/23                 

        List<SampleTabControlPageInfo> _pageOrder = new List<SampleTabControlPageInfo>();                           //8/18/23
        String _aimMaterialName = "";
        String _aimMaterialCode = "";
        Int32 _datasourcetypeID = 0;
        Int32 _datasourcetypeID_QC = 0;
        Int32 _lueTemplateID = 0;
        Int32 _days = 0;
        Int32 _selectedSampleNumber = 0;
        Int32 _lueQualityControlID = 0;                                                                                 //1/5/18
        Int32 _selectedDataSourceType = 0;
        Int32 _selectedDataSourceType_QualityControl = 0;
        Int32 _resultStatus = (Int32)EnumSampleResultStatus.Active;
        Int32 _resultStatus_QualityControl = (Int32)EnumSampleResultStatus.Active;
        Int32 _holdID = 0;
        Int32 _copies = 1;
        Int32 _quickAddMaterialID = 0;                          //4/15/22        
        Int32 _labelSize = 2;                                   //4/21/22   2 = default 2-inch label (5-lines); 1 = 1-inch (3-lines)
        Boolean _saveSample = true;
        Boolean _filterOn = false;
        // task 1155 FG 8/5/26 - controls flagged as required for the current material/location
        private List<Control> _requiredFieldControls = new List<Control>();
        private ErrorProvider _requiredFieldErrorProvider = null;
        // task 1155 FG 8/5/26 - each flagged box's normal colour, so the red fill can be undone
        private Dictionary<Control, Color> _requiredFieldOriginalBackColor = new Dictionary<Control, Color>();
        Boolean _showAnalyzerMaterials = false;
        Boolean _showMaterialNameFirst = false;                 //1/12/18  -- currently this is not used (until I can figure out how to show the name first in dropdown!)
        Boolean _showSpecific = false;                                                                  //9/23/22
        Boolean _showSpecificProducts = false;                                                          //5/19/23
        Boolean _IsSaveMassEntry = true;                                                                //Task1061 DB251031

        //10/19/23 - commented
        //Boolean _includeAllDatasources = false;                                                         //8/11/23

        Boolean _testTypeStatus = true;                         //8/17/23

        SampleExport sampleExport;
        AIM aimMaintenance;
        Analyzer analyzerMaintenance;                           //2/26/20        
        PDF pdf;                                                //11/17/20
        FormulationCrossReference formulationCrossRef;          //4/13/22
        Control _FirstTab;                                      //3/14/23        
        //SampleFurtherAnalysisView sampleFurtherAnalysisView;  //7/11/23 - commented
        Boolean _autoFillTestTypeIncludeBox = false;            //10/19/23
        AnalyzerCrossReference analyzerCrossReference;          //1/19/24

        Boolean _hideFormulationXrefMessage = false;            //11/14/24
        Boolean _hideFurtherAnalysisXrefMessage = false;        //11/14/24
        Boolean _prodXrefExists = false;                        //11/18/24
        Boolean _ingrXrefExists = false;                        //11/18/24
        private static Boolean _setSampleDateToToday = false;                  //Task1118 DB251205
        private static Boolean _clearSampleFieldsOnSave = false;               //Task1087 DB251205                              



        #region Properties
        public static Boolean SetSampleDateToToday          //Task1118 DB251205
        {
            get { return _setSampleDateToToday; }
            set { _setSampleDateToToday = value; }
        }
        public static Boolean ClearSampleFieldsOnSave       //Task1087 DB251205
        {
            get { return _clearSampleFieldsOnSave; }
            set { _clearSampleFieldsOnSave = value; }
        }



        #endregion

        //---------------------------------------------------------------------------------------------
        public tpProdSpec()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026

            //4/12/22
            lueMaterial.ProcessNewValue += lueMaterial_ProcessNewValue;
        }


        private void Sample_Load(object sender, EventArgs e)
        {
            try
            {
                SetPageOrder();


                //---------------------------------------------------------------------
                btnCurrent.BackColor = SystemColors.GradientInactiveCaption;
                btnHistorical.BackColor = SystemColors.Control;
                btnClearNewValues.Location = new Point(50, 60);            //Task1061  DB251031

                _user = UserManager.GetUser(userGlobal.CFCLoginUser.CFCUserID);             //10/25/22
                if (_user != null)
                {
                    if (_user.SpecificIngredientsGroupID > 0)
                    {
                        _showSpecific = true;
                    }
                    if (_user.SpecificProductsGroupID > 0)                                  //5/19/23
                    {
                        _showSpecificProducts = true;
                    }
                }

                DataBindOneTime();
                DataBindSample();
                DataBindSelectedSample();
                DataBindSampleResult();
                DataBindDataSourceFilter();                                                 //10/1/20
                DataBindSampleWarning();
                DataBindQualityControl();

                deSampleDate.DateTime = DateTime.Today;
                SetTabOrder();



                lueDataBindFilter();
                DataBindLueInstrument();
                DataBindLueTemplateName();
                DataBindProductVersion();
                DataBindLueQualityControlName();                                            //1/9/18
                DataBindQualityControlResults();                                            //1/9/18

                //todo remove
                rbActiveSample.TabStop = false;

                SetAlternateName();
                SetToolTips();
                formatDropdowns();

                if (AIMGlobal.chartScreen == true)
                {
                    tcSample.SelectTab(1);
                    AIMGlobal.chartScreen = false;
                }

                //Check user defaults for Sample screen
                DataBindDefaults();

                ShowCodeNameOrBothDropdowns("lblOwner");                                    //6/19/20 
                ShowCodeNameOrBothDropdowns("lblOwnerLocation");                            //6/19/20 
                ShowCodeNameOrBothDropdowns("lblSupplier");                                 //6/19/20 
                ShowCodeNameOrBothDropdowns("lblSupplierLocation");                         //6/19/20 

                DataBindSampleType();                                                       //1/22/21
                HideQuickAddPanel();                                                        //4/13/22
                CheckLabelSize();                                                           //4/19/22
                ShowOrHideFurtherAnalysisRerunButton();                                     //6/29/22
                DataBindGroundUnGround();                                                   //9/30/22
                CheckSystemSettings();


                if (CFCAIMCore.Global.sampleID != 0)
                {
                    lueSampleCode.EditValue = CFCAIMCore.Global.sampleID;
                }

                //10/27/22
                //----------------------------------------------------------------
                //If user is not allowed to see "Setup" menu item, disable hyperlinks                
                foreach (var item in _mainMenu.MainMenuStrip.Items)
                {
                    if (((ToolStripItem)item).Text == "Setup")
                    {
                        if (((ToolStripItem)item).Visible == false)
                        {
                            //Disable hyperlinks
                            lblOwner.Enabled = false;
                            lblOwnerLocation.Enabled = false;
                            lblSupplier.Enabled = false;
                            lblSupplierLocation.Enabled = false;
                            hyperMaterial.Enabled = false;
                            hyperMaterialCrossRef.Enabled = false;
                            hyperlinkLabelParameter.Enabled = false;
                        }
                    }
                }

                //10/27/22
                //----------------------------------------------------------------                
                //If user does not have permission to change the Sample Settings, disable button and 2 mass-entry hyperlinks                               
                List<CFCAIMCore.Entity.UserAccessInfo> userAccess = new List<CFCAIMCore.Entity.UserAccessInfo>();
                userAccess = AIMManager.GetUserAccess(userGlobal.CFCLoginUser.CFCUserID, (Int32)EnumPermissionTable.AccessToSampleSettings);
                if (userAccess.Count == 0)
                {
                    btnSetting.Enabled = false;
                    hyperlinkLabelControl1.Enabled = false;
                    hyperlinkQualityControl.Enabled = false;

                    //hyperlinkTestType.Enabled = false;                                //10/5/23
                    hyperlinkTestTypeGroupCode.Enabled = false;                         //11/16/23
                }

                DataBindTestTypeLue();                                                  //8/11/23

                //----------------------------------------------------------------
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
                //deSampleDate.TabStop = false;
                //deSampleDate.TabIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                AIMGlobal.SplashScreenHide();
            }
        }
        private void CheckSystemSettings()
        {
            //Check SystemSetting to show or hide Analyzer materials (for material-type = Ingredient)
            SystemSettingsInfo systemSettings = new SystemSettingsInfo();
            systemSettings = AIMManager.GetSystemSettings();
            if (systemSettings != null)
            {
                //----------------------------------------------------------------------------------------
                //12/30/22 - when 'Historical' is clicked, _days is set to 0, but was getting reset here.
                //_days = systemSettings.SampleNumberOfDays;
                //3/8/18                
                //if (_days == 0)
                //{
                //    _days = 60;
                //}
                if (lblHistorical.Visible == true)
                {
                    _days = 0;
                }
                else
                {
                    _days = systemSettings.SampleNumberOfDays;
                    if (_days == 0)
                    {
                        _days = 60;
                    }
                }
                //----------------------------------------------------------------------------------------

                txtDays.Text = _days.ToString() + " Days";

                _showAnalyzerMaterials = systemSettings.ShowAnalyzerMaterials;
                _showMaterialNameFirst = systemSettings.ShowNameFirstInDropdown;

                //5/26/20
                lblSourceFilter.Visible = true;
                lueDataSourceTypeSample.Visible = true;
                if (_showAnalyzerMaterials == false)
                {
                    lblSourceFilter.Visible = false;
                    lueDataSourceTypeSample.Visible = false;
                }

                //8/31/22
                lueFurtherAnalysis.EditValue = systemSettings.FurtherAnalysisDefaultID;

                //10/25/22 - commented
                ////////9/23/22
                //////_showSpecific = systemSettings.ShowSpecificIngredients;

                //11/21/22
                lueGroundUnGround.EditValue = systemSettings.GroundUnGroundDefaultID;

                //10/19/23
                _autoFillTestTypeIncludeBox = systemSettings.AutoFillSampleTestTypeIncludeBox;


                //Check SystemSetting to show or hide Further Analysis panel        //10/2/24 cm - begin
                if (systemSettings.ShowFurtherAnalysis == true)
                {
                    pnlFurtherAnalysis.Visible = true;

                    lblMissingFurtherAnalysisXref.Visible = false;              //11/14/24
                    hyperlinkAIMMaterialRefAnalyzerMaterial.Visible = false;    //11/14/24

                }
                else
                {
                    pnlFurtherAnalysis.Visible = false;
                }                                                                   //10/2/24 cm - end

                if (systemSettings.IsVisibleFormulationXrefMessage == false)        //11/14/24 - begin
                {
                    _hideFormulationXrefMessage = true;
                }
                else
                {
                    _hideFormulationXrefMessage = false;
                }
                if (systemSettings.IsVisibleFurtherAnalysisXrefMessage == false)
                {
                    _hideFurtherAnalysisXrefMessage = true;
                }
                else
                {
                    _hideFurtherAnalysisXrefMessage = false;
                }                                                                   //11/14/24 - end

                if (systemSettings.SetSampleDateToToday == true)        //Task1118 DB251205 
                {
                    _setSampleDateToToday = true;
                }
                else
                {
                    _setSampleDateToToday = false;
                }

                if (systemSettings.ClearFieldsOnSampleScreen == true)    //Task1087 DB251205 
                {
                    _clearSampleFieldsOnSave = true;
                }
                else
                {
                    _clearSampleFieldsOnSave = false;
                }

            }

        }
        private void CheckSystemSettingsFurtherAnalysis()
        {
            //Check SystemSetting to show or hide Analyzer materials (for material-type = Ingredient)
            SystemSettingsInfo systemSettings = new SystemSettingsInfo();
            systemSettings = AIMManager.GetSystemSettings();
            if (systemSettings != null)
            {
                lueFurtherAnalysis.EditValue = systemSettings.FurtherAnalysisDefaultID;
            }
        }

        //8/31/22        
        private void CheckSystemSettingsGroundUnGround()
        {
            SystemSettingsInfo systemSettings = new SystemSettingsInfo();
            systemSettings = AIMManager.GetSystemSettings();
            if (systemSettings != null)
            {
                lueGroundUnGround.EditValue = systemSettings.GroundUnGroundDefaultID;
            }
        }

        //11/21/22
        private void DataBindDefaults()
        {
            _userDefaults = AIMManager.GetUserDefaultsForCFCUserID(userGlobal.CFCLoginUser.CFCUserID);
            foreach (var defaultItem in _userDefaults)
            {
                if (defaultItem.ControlName == "rbLabelPrint2Yes")
                {
                    _copies = 2;
                }
            }
        }

        //---------------------------------------------------------------------------------------------
        #region DATABIND
        private void DataBindQualityControlResults()
        {


            if (_resultStatus_QualityControl == (Int32)EnumSampleResultStatus.Active)
                _sampleQualityResults = AIMManager.GetAllSampleResultQualityControl(_selectedSampleNumber, true);
            if (_resultStatus_QualityControl == (Int32)EnumSampleResultStatus.Inactive)
                _sampleQualityResults = AIMManager.GetAllSampleResultQualityControl(_selectedSampleNumber, false);
            if (_resultStatus_QualityControl == (Int32)EnumSampleResultStatus.Both)
                _sampleQualityResults = AIMManager.GetAllSampleResultQualityControl(_selectedSampleNumber);

            //added to clear out any orphaned data - (there were results for sampleID = 0)			
            if (_selectedSampleNumber == 0)
            {
                _sampleQualityResults = new List<SampleQualityInfo>();
            }

            grdSampleQualityResults.DataSource = _sampleQualityResults;

            List<DataSourceTypeInfo> dataSourceType = new List<DataSourceTypeInfo>();
            dataSourceType = AIMManager.GetDataSourceType(true);
            dataSourceType.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--Select--"
                }
            );

            if (_selectedDataSourceType_QualityControl == 0)
            {
                lueDataSourceTypeResultQualityControl.Properties.DataSource = dataSourceType;
                XtraEditors.LoadLookUpEdit(ref lueDataSourceTypeResultQualityControl, "DataSourceTypeID", "Name", "TypeDisplay");
            }

            //--------------------------------------------            
            if (dataSourceType.Count == 2)
            {
                //selecting value if there is only one
                lueDataSourceTypeResultQualityControl.EditValue = dataSourceType[1].DataSourceTypeID;
            }
            else
            {
                lueDataSourceTypeResultQualityControl.EditValue = _selectedDataSourceType_QualityControl;
            }
        }
        private void DataBindSampleResult()
        {

            Int32 type = (Int32)SampleMessageType.AnalyzerNutrient;
            if (_resultStatus == (Int32)EnumSampleResultStatus.Active)
                _sampleResult = AIMManager.GetAllSampleResult(_selectedSampleNumber, true, type);
            if (_resultStatus == (Int32)EnumSampleResultStatus.Inactive)
                _sampleResult = AIMManager.GetAllSampleResult(_selectedSampleNumber, false, type);
            if (_resultStatus == (Int32)EnumSampleResultStatus.Both)
                _sampleResult = AIMManager.GetAllSampleResult(_selectedSampleNumber);

            //added to clear out any orphaned data - (there were results for sampleID = 0)			
            if (_selectedSampleNumber == 0)
            {
                _sampleResult = new List<SampleResultInfo>();
            }
            grdSampleResults.DataSource = _sampleResult;

            List<DataSourceTypeInfo> dataSourceType = new List<DataSourceTypeInfo>();
            dataSourceType = AIMManager.GetDataSourceType(true);
            replueDataSourceType.DataSource = dataSourceType;
            XtraEditors.LoadGridLookUpEdit(ref replueDataSourceType, "DataSourceTypeID", "Name", "TypeDisplay");
            replueDataSourceType.Columns["TypeDisplay"].Caption = "Type";
            dataSourceType.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--Select--"
                }
            );

            if (_selectedDataSourceType == 0)
            {
                lueDataSourceTypeResult.Properties.DataSource = dataSourceType;
                XtraEditors.LoadLookUpEdit(ref lueDataSourceTypeResult, "DataSourceTypeID", "Name", "TypeDisplay");
            }

            if (dataSourceType.Count == 2)
            {
                //selecting value if there is only one
                lueDataSourceTypeResult.EditValue = dataSourceType[1].DataSourceTypeID;
            }
            else
            {
                lueDataSourceTypeResult.EditValue = _selectedDataSourceType;
            }
        }
        private void DataBindDataSourceFilter()
        {
            List<DataSourceTypeInfo> dataSourceType = new List<DataSourceTypeInfo>();
            dataSourceType = AIMManager.GetDataSourceType(true);
            dataSourceType.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--All--"
                }
            );
            if (_selectedDataSourceType == 0)
            {
                lueDataSourceFilter.Properties.DataSource = dataSourceType;
                XtraEditors.LoadLookUpEdit(ref lueDataSourceFilter, "DataSourceTypeID", "Name", "TypeDisplay");
            }

            if (dataSourceType.Count == 2)
            {
                //selecting value if there is only one
                lueDataSourceFilter.EditValue = dataSourceType[1].DataSourceTypeID;
            }
            else
            {
                lueDataSourceFilter.EditValue = _selectedDataSourceType;
            }
        }                                                   //10/1/20
        private void DataBindSampleResultForStatusAndDataSourceType(Int32 dataSourceID)
        {
            Int32 type = (Int32)SampleMessageType.AnalyzerNutrient;
            if ((Int32)lueDataSourceFilter.EditValue != 0)
            {
                //Task1136 DB251209 _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, dataSourceID, rbActiveResult.Checked, type);
                if (_resultStatus == (Int32)EnumSampleResultStatus.Active)
                    _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, dataSourceID, true, type);
                else if (_resultStatus == (Int32)EnumSampleResultStatus.Inactive)
                    _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, dataSourceID, false, type);
                else if (_resultStatus == (Int32)EnumSampleResultStatus.Both)
                    _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, dataSourceID, type);
                else
                {
                    _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, dataSourceID, rbActiveResult.Checked, type);   //Task1136 DB251209 
                }
                //Task1136 DB251209 if (_resultStatus == (Int32)EnumSampleResultStatus.Inactive)
                //Task1136 DB251209     _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, dataSourceID, false, type);
                //Task1136 DB251209 if (_resultStatus == (Int32)EnumSampleResultStatus.Both)
                //Task1136 DB251209   _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, dataSourceID, type);
                grdSampleResults.DataSource = _sampleResult;
            }
        }           //10/1/20

        private void DataBindMassEntry()
        {
            //Task1061 DB251031 Int32 sampleResultTemplateID = 0;
            if (lueTemplateName.EditValue != null)
            {
                //Task1061 DB251031 sampleResultTemplateID = (Int32)lueTemplateName.EditValue;
                _lueTemplateID = (Int32)lueTemplateName.EditValue;

                if (_lueTemplateID > 0)         //Task1061 DB251031
                {
                    //Task1062 DB251030 _templateDetail = AIMManager.GetSampleResultTemplateDetailTemplateID(_lueTemplateID, _selectedSampleNumber, _datasourcetypeID);
                    //Task1135 DB251209 _templateDetailGrid = AIMManager.GetSampleResultTemplateDetailTemplateID(_lueTemplateID, _selectedSampleNumber);     //Task1062 DB251031
                    _templateDetailGrid = AIMManager.GetSampleResultTemplateDetailTemplateID(_lueTemplateID, _selectedSampleNumber, (Int32)lueMaterial.EditValue);     //Task1135 DB251209 
                    //Task1061 DB251030 grdMassEntry.DataSource = _templateDetail;
                    grdMassEntry.DataSource = _templateDetailGrid;          //Task1061 DB251030
                }
            }
        }

        private void DataBindQualityControl()
        {
            Int32 ID = 0;
            if (lueQualityControlName.EditValue != null)
            {
                ID = (Int32)lueQualityControlName.EditValue;
                _lueQualityControlID = (Int32)lueQualityControlName.EditValue;
            }
            _qualityControlDetail = AIMManager.GetSampleQualityControlMassEntry(_lueQualityControlID, _selectedSampleNumber, _datasourcetypeID_QC);

            grdQualityControl.DataSource = _qualityControlDetail;


        }
        private void DataBindSampleWarning()
        {
            Int32 type = (Int32)SampleMessageType.ResultPredict;

            List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
            List<DataSourceTypeInfo> dataSourceType = new List<DataSourceTypeInfo>();
            _sampleResultPredict = AIMManager.GetAllSampleWarning(_selectedSampleNumber, type);
            analyzerNutrient = AIMManager.GetAllAnalyzerNutrient(true);
            dataSourceType = AIMManager.GetDataSourceType(true);

            grdSampleWarning.DataSource = _sampleResultPredict;

            analyzerNutrient.Insert
            (
                0,
                new AnalyzerNutrientInfo
                {
                    AnalyzerNutrientID = 0,
                    AnalyzerNutrientCode = "--Select--"
                }
            );

            dataSourceType.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--Select--"
                }
            );

            replueDataSourceTypeWarning.DataSource = dataSourceType;
            XtraEditors.LoadGridLookUpEdit(ref replueDataSourceTypeWarning, "DataSourceTypeID", "Name", "TypeDisplay");

        }
        private void DataBindFutherAnalyses()
        {
            //GetDataSourceFurtherAnalysisDataSourceTypeID
        }
        private void DataBindMaterial()
        {

            Int32 datasourceType = 0;
            Int32 materialtype = 0;
            Int32 ownerLocationID = 0;

            //6/17/20
            txtMaterialName_Sample.Text = "";

            txtMaterialCode_Sample.Text = ""; //3/4/25



            // -------------------------------------------
            //OWNERLOCATION 
            if (lueOwnerLocation.EditValue != null)
            {
                ownerLocationID = (Int32)lueOwnerLocation.EditValue;
            }

            //-------------------------------------------					
            //DATASOURCE TYPE		
            if (lueDataSourceTypeSample.EditValue != null)
            {
                datasourceType = (Int32)lueDataSourceTypeSample.EditValue;
            }

            //-------------------------------------------            
            //MATERIAL TYPE
            if (rbIngredients.Checked == true)
            {
                materialtype = (Int32)MaterialTypeEnum.Ingredient;
                txtMaterialType.Text = MaterialTypeEnum.Ingredient.ToString();  //4/1/19
            }
            else if (rbProducts.Checked == true)
            {
                materialtype = (Int32)MaterialTypeEnum.Product;
                txtMaterialType.Text = MaterialTypeEnum.Product.ToString();     //4/1/19
            }

            //9/23/22 - added because _showAnalyzerMaterials and _showSpecific (and maybe others) are used below and were not getting checked prior.            
            CheckSystemSettings();


            //----------------------------------------------------------------------
            //PRODUCTS
            #region Products

            if (rbProducts.Checked == true)
            {
                if (_showSpecificProducts == true)
                {
                    //added so not ALL products are brought in.  When user selects an Owner Location, the products will filter to that location
                    if (ownerLocationID > 0)
                    {
                        _specificProducts = AIMManager.GetSpecificProducts(_user.SpecificProductsGroupID, true, ownerLocationID);
                        lueMaterial.Properties.DataSource = _specificProducts;
                        XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "AIMOwnerLocationName");

                        if (_showMaterialNameFirst == true)
                        {
                            XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode", "AIMOwnerLocationName");
                            lueMaterial.Properties.Columns["AIMMaterialCode"].Visible = false;
                        }
                        if (_aimMaterialForDropdown.Count == 2)
                        {
                            lueMaterial.EditValue = _aimMaterialForDropdown[1].AIMMaterialID;
                        }
                    }
                    else
                    {
                        lueMaterial.Properties.DataSource = null;
                    }
                }
                else
                {
                    //added so not ALL products are brought in.  When user selects an Owner Location, the products will filter to that location
                    if (ownerLocationID > 0)
                    {
                        //4/13/22 - changed for products - they do not need to be cross-referenced to Analyzer
                        //_aimMaterialXref = AIMManager.GetAIMMaterialRefAnalyzerMaterial_DataSource_MaterialType_OwnerLocation(datasourceType, materialtype, ownerLocationID);
                        //lueMaterial.Properties.DataSource = _aimMaterialXref;
                        _aimMaterialForDropdown = AIMManager.GetAIMMaterial(true, ownerLocationID, (Int32)MaterialTypeEnum.Product);
                        lueMaterial.Properties.DataSource = _aimMaterialForDropdown;

                        //4/13/22
                        //XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "AIMLocationName");
                        XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "AIMOwnerLocationName");

                        if (_showMaterialNameFirst == true)
                        {
                            //4/13/22
                            //XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode", "AIMLocationName");
                            XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode", "AIMOwnerLocationName");

                            lueMaterial.Properties.Columns["AIMMaterialCode"].Visible = false;
                        }

                        //4/13/22 - changed for products - they do not need to be cross-referenced to Analyzer
                        //if (_aimMaterialXref.Count == 2)
                        //{
                        //    lueMaterial.EditValue = _aimMaterialXref[1].AIMMaterialID;      //selecting value if there is only one
                        //}
                        if (_aimMaterialForDropdown.Count == 2)
                        {
                            lueMaterial.EditValue = _aimMaterialForDropdown[1].AIMMaterialID;      //selecting value if there is only one
                        }
                    }

                    //7/16/21 - added so not ALL products are brought in.  When user selects an Owner Location, the products will filter to that location
                    else
                    {
                        lueMaterial.Properties.DataSource = null;
                    }
                }
            }
            #endregion

            else

            //----------------------------------------------------------------------
            //INGREDIENTS
            #region Ingredients
            {

                //----------------------------------------------------------------------
                //SPECIFIC LIST OF INGREDIENTS
                #region Specific items
                if (_showSpecific == true)
                {
                    if (_showAnalyzerMaterials == true)
                    {
                        _specificIngredients = AIMManager.GetSpecificIngredients_GroupID_DataSourceType_IsActive(_user.SpecificIngredientsGroupID, datasourceType, true);
                        lueMaterial.Properties.DataSource = _specificIngredients;

                        if (datasourceType == 0)
                        {
                            XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AnalyzerMaterialCode", "AIMMaterialCode", "AIMMaterialName", "DataSourceTypeName");
                            if (_showMaterialNameFirst == true)
                            {
                                XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AnalyzerMaterialCode", "AIMMaterialName", "AIMMaterialCode", "DataSourceTypeName");
                                lueMaterial.Properties.Columns["AIMMaterialCode"].Visible = false;
                            }
                            lueMaterial.Properties.Columns[9].Caption = "Source Name";
                        }
                        else
                        {
                            XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AnalyzerMaterialCode", "AIMMaterialCode", "AIMMaterialName");
                            if (_showMaterialNameFirst == true)
                            {
                                XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AnalyzerMaterialCode", "AIMMaterialName", "AIMMaterialCode");
                                lueMaterial.Properties.Columns["AIMMaterialCode"].Visible = false;
                            }

                        }
                        lueMaterial.Properties.Columns[8].Caption = "Analyzer Code";
                    }
                    else
                    {
                        _specificIngredients = AIMManager.GetSpecificIngredients(_user.SpecificIngredientsGroupID, true);
                        lueMaterial.Properties.DataSource = _specificIngredients;
                        XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
                        if (_showMaterialNameFirst == true)
                        {
                            XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");
                            lueMaterial.Properties.Columns["AIMMaterialCode"].Visible = false;
                        }
                    }
                }
                #endregion

                //----------------------------------------------------------------------
                //ALL INGREDIENTS
                #region All Ingredients
                else
                {
                    if (_showAnalyzerMaterials == true)
                    {

                        //when ingredient is selected, the data source is visible                
                        _aimMaterialXref = AIMManager.GetAIMMaterialRefAnalyzerMaterial_DataSource_MaterialType_OwnerLocation(datasourceType, materialtype, ownerLocationID);
                        lueMaterial.Properties.DataSource = _aimMaterialXref;


                        if (datasourceType == 0)
                        {
                            XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AnalyzerMaterialCode", "AIMMaterialCode", "AIMMaterialName", "DataSourceTypeName");
                            if (_showMaterialNameFirst == true)
                            {
                                XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AnalyzerMaterialCode", "AIMMaterialName", "AIMMaterialCode", "DataSourceTypeName");
                                lueMaterial.Properties.Columns["AIMMaterialCode"].Visible = false;
                            }
                            lueMaterial.Properties.Columns[14].Caption = "Source Name";
                        }
                        else
                        {

                            XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AnalyzerMaterialCode", "AIMMaterialCode", "AIMMaterialName", "AIMLocationName");
                            if (_showMaterialNameFirst == true)
                            {
                                XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AnalyzerMaterialCode", "AIMMaterialName", "AIMMaterialCode", "AIMLocationName");
                                lueMaterial.Properties.Columns["AIMMaterialCode"].Visible = false;
                            }
                            //4/13/22 - no reason for AIMLocationName to show for ingredients
                            lueMaterial.Properties.Columns["AIMLocationName"].Visible = false;


                        }
                        lueMaterial.Properties.Columns[10].Caption = "Analyzer Code";
                    }
                    else
                    {
                        //6/18/20               
                        //_aimMaterialForDropdown = AIMManager.GetAIMMaterial_Active_MaterialType_For_Sample(true, (Int32)MaterialTypeEnum.Ingredient);  
                        _aimMaterialForDropdown = AIMManager.GetAIMMaterial_Active_MaterialType_OwnerLocation_For_Sample(true, (Int32)MaterialTypeEnum.Ingredient, (Int32)lueOwnerLocation.EditValue);
                        lueMaterial.Properties.DataSource = _aimMaterialForDropdown;

                        XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
                        if (_showMaterialNameFirst == true)
                        {
                            XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");
                            lueMaterial.Properties.Columns["AIMMaterialCode"].Visible = false;
                        }

                    }
                }
                #endregion
            }

            #endregion

            //----------------------------------------------------------------------
            //4/15/22
            if (_quickAddMaterialID > 0)
            {
                lueMaterial.EditValue = _quickAddMaterialID;
            }
            lblVersionNote.Visible = false;
            checkSampleCrossRef();
            _quickAddMaterialID = 0;


        }
        private void lueDataBindFilter()
        {
            List<AlternateNameInfo> filter = new List<AlternateNameInfo>();
            filter = AIMManager.GetAlternateNameFilter((Int32)EnumFormID.Sample);
            filter.Insert
                (
                    0,
                    new AlternateNameInfo
                    {
                        ControlID = 0,
                        AlternateName = "--Select--"
                    }
                );

            lueFilter.Properties.DataSource = filter;
            XtraEditors.LoadLookUpEdit(ref lueFilter, "ControlID", "AlternateName");

        }
        private void DataBindProductVersion()
        {

            List<CFCProductArchiveInfo> version = new List<CFCProductArchiveInfo>();
            Int32 aimOwnerLocationID = 0;
            aimOwnerLocationID = (Int32)lueOwnerLocation.EditValue;

            version = AIMManager.GetCFCProductArchive((Int32)lueMaterial.EditValue, aimOwnerLocationID);

            lueVersion.Properties.DataSource = version;
            XtraEditors.LoadLookUpEdit(ref lueVersion, "CFCProductArchiveID", "ProductionVersion", "ProductionDate");
            lueVersion.Properties.Columns[1].Caption = "Production Version";
            lueVersion.Properties.Columns[2].Caption = "Stored Date";


            //12/5/22 - Versova wants first one to show
            ////autofill dropdown when there is only one value                        
            //if (version.Count == 1)
            //{
            //    lueVersion.EditValue = version[0].CFCProductArchiveID;
            //}
            //else
            //{
            //    lueVersion.EditValue = version;
            //}
            if (version.Count > 0)
            {
                lueVersion.EditValue = version[0].CFCProductArchiveID;
            }


        }
        private void DataBindCFCCrossRefDropdown()
        {
            Int32 plantID = 0;

            //get x-ref for ownerloc-cfcplantid
            CFCPlantRefAIMOwnerLocationInfo cfcPlant = new CFCPlantRefAIMOwnerLocationInfo();
            cfcPlant = AIMManager.GetCFCPlantAimOwnerLocationCrossReference((Int32)lueOwnerLocation.EditValue);
            if (cfcPlant != null)
            {
                plantID = cfcPlant.CFCPlantID;
            }


            List<CFCProductInfo> cfcProduct = new List<CFCProductInfo>();
            cfcProduct = AIMManager.GetAllCFCProductAimDatabaseIDCFCPlantID(true, (Int32)_mainMenu.lueDataMap.EditValue, plantID);
            cfcProduct.Insert
            (
                0,
                new CFCProductInfo
                {
                    CFCProductID = 0,
                    CFCProductCode = "--Select--",
                    CFCProductName = "--Select--"
                }
            );

            lueProductCode.Properties.DataSource = cfcProduct;
            XtraEditors.LoadLookUpEdit(ref lueProductCode, "CFCProductID", "CFCProductCode", "CFCProductName");
            lueProductCode.Properties.Columns["CFCProductCode"].Caption = "FRM Product Code";
            lueProductCode.Properties.Columns["CFCProductName"].Caption = "FRM Product Name";

        }                                               //4/13/22        
        private void DataBindSampleType()
        {
            List<SampleTypeInfo> sampleType = new List<SampleTypeInfo>();
            sampleType = AIMManager.GetSampleType(true);
            sampleType.Insert
            (
                0,
                new SampleTypeInfo
                {
                    SampleTypeID = 0,
                    SampleTypeName = ""
                }
            );

            lueSampleType.Properties.DataSource = sampleType;
            XtraEditors.LoadLookUpEdit(ref lueSampleType, "SampleTypeID", "SampleTypeName");
            lueVersion.Properties.Columns[1].Caption = "Name";
            if (sampleType.Count == 1)
            {
                lueSampleType.EditValue = sampleType[0].SampleTypeID;
            }
            else
            {
                if (AIMGlobal.fromAnalysisScreen == true)
                {
                    //12/21/22 - added check for null (got this coming from Analysis screen)
                    if (_selectedSample != null)
                    {
                        lueSampleType.EditValue = _selectedSample.SampleTypeID;
                    }
                    else
                    {
                        lueSampleType.EditValue = 0; // sampleType; //11/14/24
                    }

                }
                else
                {
                    lueSampleType.EditValue = 0; // sampleType; //11/14/24
                }
            }
        }

        //1/22/21        
        private void DataBindGroundUnGround()
        {
            List<GroundUnGroundInfo> groundUnGround = new List<GroundUnGroundInfo>();
            groundUnGround = AIMManager.GetGroundUnGround();
            lueGroundUnGround.Properties.DataSource = groundUnGround;
            XtraEditors.LoadLookUpEdit(ref lueGroundUnGround, "GroundUnGroundID", "GroundUnGroundString");
            lueGroundUnGround.Properties.Columns[1].Caption = "";
            if (groundUnGround.Count > 1)
            {
                lueGroundUnGround.EditValue = groundUnGround[0].GroundUnGroundID;
            }
        }

        //2/8/24
        private void DataBindGroundWholeSpecificType(Int32 groundUngroundID)
        {
            List<GroundUnGroundInfo> groundUnGround = new List<GroundUnGroundInfo>();
            groundUnGround = AIMManager.GetGroundUnGround(groundUngroundID);
            lueGroundUnGround.Properties.DataSource = groundUnGround;
            XtraEditors.LoadLookUpEdit(ref lueGroundUnGround, "GroundUnGroundID", "GroundUnGroundString");
            lueGroundUnGround.Properties.Columns[1].Caption = "";
            if (groundUnGround.Count == 1)
            {
                lueGroundUnGround.EditValue = groundUnGround[0].GroundUnGroundID;
            }
        }

        //9/30/22    
        private void DataBindOwner()
        {
            Int32 currentOwnerID = 0;
            if (lueOwner.EditValue != null)
            {
                Int32.TryParse(lueOwner.EditValue.ToString(), out currentOwnerID);
            }

            if (rbActiveSample.Checked == true)
            {
                //11/23/22 - added check of groupID (some users can see only certain owner/owner-locations, depending on group, which is set in User Listing)
                //_aimOwner = AIMManager.GetAIMOwner(true);
                _aimOwner = AIMManager.GetAIMOwner(_user.AIMOwnerLocationGroupID, true);
            }
            else
            {
                //11/23/22
                //_aimOwner = AIMManager.GetAIMOwnerAll();
                _aimOwner = AIMManager.GetAIMOwnerAll(_user.AIMOwnerLocationGroupID);
            }

            _aimOwner.Insert
            (
                0,
                new AIMOwnerInfo
                {
                    AIMOwnerID = 0,
                    OwnerCode = "--Select--",

                    //6/17/20
                    OwnerName = "--Select--",

                    IsActive = false
                }
            );
            lueOwner.Properties.DataSource = _aimOwner;
            XtraEditors.LoadLookUpEdit(ref lueOwner, "AIMOwnerID", "OwnerCode", "OwnerName");

            //---------------------------------------------------------------------------------------------------------------
            //6/30/20 - insert from aimOwnerInfo entity into aimOwnerSortInfo entity -- so Name shows before Code in dropdown
            _aimOwnerSort.Clear();
            foreach (var item in _aimOwner)
            {
                _aimOwnerSort.Add(new AIMOwnerSortInfo
                {
                    AIMOwnerID = item.AIMOwnerID,
                    OwnerName = item.OwnerName,
                    OwnerCode = item.OwnerCode,
                    IsActive = item.IsActive
                });
            }
            lueOwnerName.Properties.DataSource = _aimOwnerSort;
            XtraEditors.LoadLookUpEdit(ref lueOwnerName, "AIMOwnerID", "OwnerName", "OwnerCode");
            //---------------------------------------------------------------------------------------------------------------

            // task 1155 FG 8/5/26 - rebinding the datasource clears the lookup; put the owner back if still in the list
            if (currentOwnerID > 0)
            {
                Boolean ownerStillInList = false;
                for (Int32 i = 0; i < _aimOwner.Count; i++)
                {
                    if (_aimOwner[i].AIMOwnerID == currentOwnerID)
                    {
                        ownerStillInList = true;
                        break;
                    }
                }
                if (ownerStillInList)
                {
                    lueOwner.EditValue = currentOwnerID;
                    lueOwnerName.EditValue = currentOwnerID;
                }
            }

        }
        private void DataBindSupplier()
        {
            if (rbActiveSample.Checked == true)
            {
                //8/30/23
                //_source = AIMManager.GetSupplier(true);
                _supplier = AIMManager.GetSupplier(true, _user.AIMSupplierGroupID);
            }
            else
            {
                //_supplier = AIMManager.GetAIMSupplierAll();
                _supplier = AIMManager.GetAIMSupplierAllStatus(_user.AIMSupplierGroupID);
            }
            _supplier.Insert
            (
                0,
                new AIMSupplierInfo
                {
                    AIMSupplierID = 0,
                    AIMSupplierCode = "--Select--",
                    SupplierName = "--Select--",
                    IsActive = false
                }
            );
            lueSource.Properties.DataSource = _supplier;
            XtraEditors.LoadLookUpEdit(ref lueSource, "AIMSupplierID", "AIMSupplierCode", "SupplierName");

            //---------------------------------------------------------------------------------------------------------------
            //6/30/20 - insert from aimSupplierInfo entity into aimSupplierSortInfo entity -- so Name shows before Code in dropdown
            _aimSupplierSort.Clear();
            foreach (var item in _supplier)
            {
                _aimSupplierSort.Add(new AIMSupplierSortInfo
                {
                    AIMSupplierID = item.AIMSupplierID,
                    SupplierName = item.SupplierName,
                    AIMSupplierCode = item.AIMSupplierCode,
                    IsActive = item.IsActive
                });

                lueSupplierName.Properties.DataSource = _aimSupplierSort;
                XtraEditors.LoadLookUpEdit(ref lueSupplierName, "AIMSupplierID", "SupplierName", "AIMSupplierCode");
            }
            //---------------------------------------------------------------------------------------------------------------

        }
        private void DataBindOneTime()
        {
            DataBindOwner();
            DataBindSupplier();

            _furtherAnalyses = AIMManager.GetDataSourceFurtherAnalyses(true);
            _dataSource = AIMManager.GetDataSourceTypeRefExists(true);
            _furtherAnalyses.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--None--"
                }
            );
            _dataSource.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--All Sources--"

                }
            );


            lueFurtherAnalysis.Properties.DataSource = _furtherAnalyses;
            XtraEditors.LoadLookUpEdit(ref lueFurtherAnalysis, "DataSourceTypeID", "Name", "TypeDisplay");
            if (lueDataSourceTypeSample.EditValue == null)
            {
                DataBindlueDataSourceTypeSample();
            }
            else if ((Int32)lueDataSourceTypeSample.EditValue == 0)
            {
                DataBindlueDataSourceTypeSample();
            }

            Refresh();
        }
        private void DataBindlueDataSourceTypeSample()
        {
            lueDataSourceTypeSample.Properties.DataSource = _dataSource;
            XtraEditors.LoadLookUpEdit(ref lueDataSourceTypeSample, "DataSourceTypeID", "Name", "TypeDisplay");

            if (_dataSource.Count == 2)
            {
                //selecting value if there is only one
                lueDataSourceTypeSample.EditValue = _dataSource[1].DataSourceTypeID;
            }
            else
            {
                //10/1/24
                //lueDataSourceTypeSample.EditValue = _dataSource;
                lueDataSourceTypeSample.EditValue = 0; // _selectedDataSourceType;  //11/14/24
            }
        }
        private void DataBindSample()
        {
            try
            {

                List<SampleInfo> sample = new List<SampleInfo>();
                sample = AIMManager.GetAllSampleUseSystemDateSetting(rbActiveSample.Checked, _days);

                if (rbActiveSample.Checked == true)
                {
                    sample.Insert
                    (
                        0,
                        new SampleInfo
                        {
                            SampleID = 0,
                            ////SampleCode = "--Select--",      //Do NOT set the sample code.  Needs to be blank!
                            AlternateCode = "--Select--"
                        }
                    );
                }
                Int32 currentSampleNo = 0;
                if (lueSampleCode.EditValue != null && lueSampleCode.Text == String.Empty)
                {
                    /*
				     *we want keep the old value in case when user is trying to get record which is 
				     *inactive while cbActiveSample is true or vise versa. in these cases
				     *lueSampleCode.Text will be String.Empty
				     */
                    Int32.TryParse(lueSampleCode.EditValue.ToString(), out currentSampleNo);
                }
                else
                {

                }

                lueSampleCode.Properties.DataSource = sample;
                XtraEditors.LoadLookUpEdit(ref lueSampleCode, "SampleID", "SampleCode");
                lueSampleCode.Properties.Columns["SampleCode"].Visible = true;
                lueSampleCode.EditValue = currentSampleNo;

                lueAlternateCode.Properties.DataSource = sample;
                XtraEditors.LoadLookUpEdit(ref lueAlternateCode, "SampleID", "AlternateCode");
                lueAlternateCode.Properties.Columns["SampleCode"].Visible = true;
                lueAlternateCode.EditValue = currentSampleNo;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataBindSample_WithFiler()
        {
            List<SampleInfo> sample = new List<SampleInfo>();
            sample = AIMManager.GetAllSampleUseSystemDateSetting(rbActiveSample.Checked, _days);

            if (rbActiveSample.Checked == true)
            {
                sample.Insert
                (
                    0,
                    new SampleInfo
                    {
                        SampleID = 0,
                        AlternateCode = "--Select--"
                    }
                );
            }
            Int32 currentSampleNo = 0;
            if (lueSampleCode.EditValue != null && lueSampleCode.Text == String.Empty)
            {
                /*                                                                                              
				 *we want keep the old value in case when user is trying to get record which is 
				 *inactive while cbActiveSample is true or vise versa. in these cases
				 *lueSampleCode.Text will be String.Empty
				 */
                Int32.TryParse(lueSampleCode.EditValue.ToString(), out currentSampleNo);
            }
            lueSampleCode.Properties.DataSource = sample;

            XtraEditors.LoadLookUpEdit(ref lueSampleCode, "SampleID", "SampleCode");
            lueSampleCode.Properties.Columns["SampleCode"].Visible = true;
            lueSampleCode.EditValue = currentSampleNo;

            lueAlternateCode.Properties.DataSource = sample;
            XtraEditors.LoadLookUpEdit(ref lueAlternateCode, "SampleID", "AlternateCode");
            lueAlternateCode.Properties.Columns["SampleCode"].Visible = true;
            lueAlternateCode.EditValue = currentSampleNo;
        }
        private void DataBindSelectedSample()
        {
            Int32.TryParse(lueSampleCode.EditValue.ToString(), out _selectedSampleNumber);
            _selectedSample = AIMManager.GetSample(_selectedSampleNumber);
            if (_selectedSample != null)
            {
                if (_selectedSample != null)
                {
                    if (_selectedSample.MaterialTypeID == (Int32)MaterialTypeEnum.Product)
                    {
                        rbProducts.Checked = true;
                    }
                    else
                    {
                        rbIngredients.Checked = true;
                    }
                }

                lueOwner.EditValue = _selectedSample.AIMOwnerID;
                lueOwnerLocation.EditValue = _selectedSample.AIMOwnerLocationID;

                lueSource.EditValue = _selectedSample.AIMSupplierID;
                lueSourceLocation.EditValue = _selectedSample.AIMSupplierLocationID;

                deSampleDate.DateTime = _selectedSample.SampleDate.Date;
                txtSampleMaterial.Text = _selectedSample.AIMMaterialCode;
                txtAlternateCode.Text = _selectedSample.AlternateCode;
                txtSampleCode.Text = _selectedSample.SampleCode;
                txtContractNumber.Text = _selectedSample.ContractNumber;
                txtInvoiceNumber.Text = _selectedSample.InvoiceNumber;
                txtLoadNumber.Text = _selectedSample.LoadNumber;
                txtLotNumber.Text = _selectedSample.LotNumber;
                txtPONumber.Text = _selectedSample.PoNumber;
                txtTicketNumber.Text = _selectedSample.TicketNumber;
                txtVehicleID.Text = _selectedSample.VehicleNumber;
                txtBOL.Text = _selectedSample.BillOfLoad;
                txtUserName.Text = _selectedSample.UserName;
                txtUpdateStamp.Text = _selectedSample.UpdateStamp.ToString();
                txtNotes.Text = _selectedSample.Notes;
                deExpirationDate.DateTime = _selectedSample.ExpirationDate.Date;
                cbActive.Checked = _selectedSample.IsActive;
                cbActive.Enabled = true;

                lueDataSourceTypeSample.EditValue = _selectedSample.DataSourceTypeID;

                //bind lueMaterial using the datasourcetypeID (if one is selected)
                DataBindMaterial();
                lueMaterial.EditValue = _selectedSample.AIMMaterialID;
                lueFurtherAnalysis.EditValue = _selectedSample.FurtherAnalysisID;

                //9/14/20
                txtDocumentPath.Text = _selectedSample.SupportingDocumentPath;

                lblSupplier.Visible = true;
                lblSupplierLocation.Visible = true;
                lueSource.Visible = true;
                lueSourceLocation.Visible = true;

                //6/17/20
                lueSupplierName.Visible = true;
                lueSupplierLocationName.Visible = true;

                //1/22/21
                lueSampleType.EditValue = _selectedSample.SampleTypeID;

                //9/30/22
                if (_selectedSample.IsGround == true)
                {
                    lueGroundUnGround.EditValue = 1;
                }
                else
                {
                    //1/19/24
                    //lueGroundUnGround.EditValue = 0;
                    lueGroundUnGround.EditValue = 2;
                }

                //////DataBindGroundUnGround();   //1/17/24



                //----------------------------------------- 
                lblProductVersion.Visible = false;
                lueVersion.Visible = false;

                //4/13/22
                checkSampleCrossRef();

                txtMaterialType.Text = MaterialTypeEnum.Ingredient.ToString();

                if (_selectedSample.MaterialTypeID == (Int32)MaterialTypeEnum.Product)
                {
                    //11/18/24
                    //4/13/22
                    //if (lblXrefNote.Visible == true)
                    if (_prodXrefExists == false)

                    {
                        lblProductVersion.Visible = false;
                        lueVersion.Visible = false;
                        lblVersionNote.Visible = false;
                    }

                    else
                    {
                        lblProductVersion.Visible = true;
                        lueVersion.Visible = true;
                        lueVersion.EditValue = _selectedSample.CFCProductArchiveID;
                    }

                    txtMaterialType.Text = MaterialTypeEnum.Product.ToString();
                    ShowHideSupplier();
                }
                //-----------------------------------------

                //for existing sample, do not allow edit of Material or DataSource --> unless user has permission
                //4/1/19 - added permission check
                //lueMaterial.Enabled = false;
                Int32 rtn = CFCAIM.AIMUtilities.GlobalFunctions.CheckUserPermissions(userGlobal.CFCLoginUser.CFCUserID, (Int32)EnumPermissionTable.AllowChangeSampleMaterial);
                if (rtn == -1)
                {
                    lueMaterial.Enabled = false;
                    lblSourceFilter.Enabled = false;
                    lueDataSourceTypeSample.Enabled = false;
                    txtMaterialType.Enabled = false;
                    txtMaterialName_Sample.Enabled = false;
                    rbIngredients.Enabled = false;
                    rbProducts.Enabled = false;
                    lblProductVersion.Enabled = false;
                    lueVersion.Enabled = false;
                }
                else
                {
                    lueMaterial.Enabled = true;
                    lblSourceFilter.Enabled = true;
                    lueDataSourceTypeSample.Enabled = true;
                    txtMaterialType.Enabled = true;
                    txtMaterialName_Sample.Enabled = true;
                    rbIngredients.Enabled = true;
                    rbProducts.Enabled = true;
                    lblProductVersion.Enabled = true;
                    lueVersion.Enabled = true;
                }


                if ((Int32)lueMaterial.EditValue != 0)
                {
                    _aimMaterial = AIMManager.GetAIMMaterial((Int32)lueMaterial.EditValue, true);
                    if (_aimMaterial != null)
                    {
                        _aimMaterialName = _aimMaterial.AIMMaterialName;
                        _aimMaterialCode = _aimMaterial.AIMMaterialCode;
                    }
                    txtMaterialName.Text = _aimMaterialName;
                    txtMaterialName_Sample.Text = _aimMaterialName;

                    txtMaterialCode_Sample.Text = _aimMaterialCode;     //3/4/25

                    if (_aimMaterialCode != _aimMaterialName)
                    {
                        txtMaterial_MassEntry.Text = _aimMaterialCode + " - " + _aimMaterialName;
                    }
                    else
                    {
                        txtMaterial_MassEntry.Text = _aimMaterialName;
                    }
                }
            }
            else
            {
                txtSampleMaterial.Text = String.Empty;
                txtAlternateCode.Text = String.Empty;
                txtLoadNumber.Text = String.Empty;
                txtContractNumber.Text = String.Empty;
                txtInvoiceNumber.Text = String.Empty;
                txtLoadNumber.Text = String.Empty;
                txtLotNumber.Text = String.Empty;
                txtPONumber.Text = String.Empty;
                txtTicketNumber.Text = String.Empty;
                txtVehicleID.Text = String.Empty;
                txtBOL.Text = String.Empty;
                txtNotes.Text = String.Empty;
                deExpirationDate.DateTime = DateTime.Today;
                cbActive.Checked = true;
                cbActive.Enabled = false;
                //deSampleDate.EditValue = DateTime.Now.Date; customer don't want to reset it
                txtUserName.Text = String.Empty;
                txtUpdateStamp.Text = String.Empty;
                txtMaterialName.Text = String.Empty;
                txtMaterialName_Sample.Text = String.Empty;

                txtMaterialCode_Sample.Text = String.Empty;     //3/4/25

                // customer don't want them to reset				
                if (_aimOwner.Count == 2)
                {
                    //selecting value if there is only one
                    lueOwner.EditValue = _aimOwner[1].AIMOwnerID;
                }
                if (_supplier.Count == 2)
                {
                    lueSource.EditValue = _supplier[1].AIMSupplierID;
                }

                //for new samples, allow edit of Material and Data Sources
                lueMaterial.Enabled = true;
                lueDataSourceTypeSample.Enabled = true;
                txtMaterial_MassEntry.Text = String.Empty;

                //9/14/20
                txtDocumentPath.Text = String.Empty;

                //11/21/22 - changed to get value from SystemSettings
                //9/30/22
                //lueGroundUnGround.EditValue = EnumGroundUnGround.UnGround;
                CheckSystemSettingsGroundUnGround();


                //had to remove items from group because group box screws up tab order.
                txtMaterialType.Enabled = true;
                txtMaterialName_Sample.Enabled = true;
                lblSourceFilter.Enabled = true;
                rbIngredients.Enabled = true;
                rbProducts.Enabled = true;
                lblProductVersion.Enabled = true;
                lueVersion.Enabled = true;


                ////10/17/17 - for abvista
                //txtBOL.Text = "1250";
                //txtContractNumber.Text = "N/A";
                //txtInvoiceNumber.Text = "NIR";
                //txtLoadNumber.Text = "181";
                //txtLotNumber.Text = "7";
                //txtTicketNumber.Text = "RM";
                //txtAlternateCode.Text = "LRP0028";
                //txtSampleCode.Text = "P00-216";
                //lueOwner.Text = "ABVista";
                //lueOwnerLocation.Text = "ABVista";
                //lueSource.Text = "Foster Farms";
                //lueSourceLocation.Text = "Turlock";
                //lueAlternateCode.Text = "LRP0028";
            }
        }
        private void DataBindSourceLocation()
        {
            Int32 supplierLocationID = 0;
            Int32 supplierID = 0;
            if (lueSource.EditValue != null)
            {
                Int32.TryParse(lueSource.EditValue.ToString(), out supplierID);
            }

            if (rbActiveSample.Checked == true)
            {
                //8/30/23
                //_supplierLocation = AIMManager.GetSupplierLocationForSupplier(supplierID);
                _supplierLocation = AIMManager.GetSupplierLocationForSupplier(supplierID, _user.AIMSupplierGroupID, _user.AIMSupplierLocationGroupID);
            }
            else
            {
                //8/30/23
                //_supplierLocation = AIMManager.GetAIMSupplierLocationForSupplierAllStatus(supplierID);
                _supplierLocation = AIMManager.GetAIMSupplierLocationForSupplierAllStatus(supplierID, _user.AIMSupplierGroupID, _user.AIMSupplierLocationGroupID);
            }

            if (_selectedSample != null)
            {
                supplierLocationID = _selectedSample.AIMSupplierLocationID;
            }

            _supplierLocation.Insert
            (
                0,
                new AIMSupplierLocationInfo
                {
                    AIMSupplierLocationID = 0,
                    AIMSupplierLocationCode = "--Select--",
                    SupplierLocationName = "--Select--",
                    IsActive = false
                }
            );
            lueSourceLocation.Properties.DataSource = _supplierLocation;
            XtraEditors.LoadLookUpEdit(ref lueSourceLocation, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");

            //---------------------------------------------------------------------------------------------------------------
            //6/30/20
            _aimSupplierLocationSort.Clear();
            foreach (var item in _supplierLocation)
            {
                _aimSupplierLocationSort.Add(new AIMSupplierLocationSortInfo
                {
                    AIMSupplierLocationID = item.AIMSupplierLocationID,
                    SupplierLocationName = item.SupplierLocationName,
                    AIMSupplierLocationCode = item.AIMSupplierLocationCode,
                    IsActive = item.IsActive
                });
            }
            lueSupplierLocationName.Properties.DataSource = _aimSupplierLocationSort;
            XtraEditors.LoadLookUpEdit(ref lueSupplierLocationName, "AIMSupplierLocationID", "SupplierLocationName", "AIMSupplierLocationCode");
            //---------------------------------------------------------------------------------------------------------------

            if (_selectedSample == null && _supplierLocation.Count == 2)
            {
                //selecting value if there is only one
                lueSourceLocation.EditValue = _supplierLocation[1].AIMSupplierLocationID;

                //6/17/20
                lueSupplierLocationName.EditValue = _aimSupplierLocationSort[1].AIMSupplierLocationID;
            }
            else
            {
                lueSourceLocation.EditValue = 0; // supplierLocationID; //11/14/24

                //6/17/20
                lueSupplierLocationName.EditValue = 0; // supplierLocationID;   //11/14/24
            }
        }
        private void DataBindOwnerLocation()
        {
            Int32 ownerLocationID = 0;
            Int32 ownerID = 0;
            Int32 currentOwnerLocationID = 0;
            if (lueOwner.EditValue != null)
            {
                Int32.TryParse(lueOwner.EditValue.ToString(), out ownerID);
            }
            if (lueOwnerLocation.EditValue != null)
            {
                Int32.TryParse(lueOwnerLocation.EditValue.ToString(), out currentOwnerLocationID);
            }

            if (rbActiveSample.Checked == true)
            {
                //11/23/22
                //_aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(OwnerID);
                _aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(ownerID, _user.AIMOwnerLocationGroupID);
            }
            else
            {
                //11/23/22
                //_aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwnerAll(OwnerID);
                _aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwnerAll(ownerID, _user.AIMOwnerLocationGroupID);
            }

            if (_selectedSample != null)
            {
                ownerLocationID = _selectedSample.AIMOwnerLocationID;
            }

            _aimOwnerLocation.Insert
            (
                0,
                new AIMOwnerLocationInfo
                {
                    AIMOwnerLocationID = 0,
                    LocationCode = "--Select--",

                    //6/17/20
                    LocationName = "--Select--",

                    IsActive = false
                }
            );
            lueOwnerLocation.Properties.DataSource = _aimOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueOwnerLocation, "AIMOwnerLocationID", "LocationCode", "LocationName");
            lueOwnerLocation.Properties.Columns["LocationCode"].Visible = true;

            //---------------------------------------------------------------------------------------------------------------
            //6/30/20 - insert from aimOwnerLocationInfo entity into aimOwnerLocationSortInfo entity -- so Name shows before Code in dropdown
            _aimOwnerLocationSort.Clear();
            foreach (var item in _aimOwnerLocation)
            {
                _aimOwnerLocationSort.Add(new AIMOwnerLocationSortInfo
                {
                    AIMOwnerLocationID = item.AIMOwnerLocationID,
                    AIMOwnerID = item.AIMOwnerID,
                    LocationName = item.LocationName,
                    LocationCode = item.LocationCode,
                    IsActive = item.IsActive
                });
            }
            lueOwnerLocationName.Properties.DataSource = _aimOwnerLocationSort;
            XtraEditors.LoadLookUpEdit(ref lueOwnerLocationName, "AIMOwnerLocationID", "LocationName", "LocationCode");
            lueOwnerLocationName.Properties.Columns["LocationName"].Visible = true;
            //---------------------------------------------------------------------------------------------------------------

            // task 1155 FG 8/5/26 - keep the saved/current location after rebind instead of always clearing to 0
            Int32 locationToSelect = 0;
            if (ownerLocationID > 0 && OwnerLocationIsInList(ownerLocationID))
            {
                locationToSelect = ownerLocationID;
            }
            else if (currentOwnerLocationID > 0 && OwnerLocationIsInList(currentOwnerLocationID))
            {
                locationToSelect = currentOwnerLocationID;
            }
            else if (_selectedSample == null && _aimOwnerLocation.Count == 2)
            {
                //selecting value if there is only one
                locationToSelect = _aimOwnerLocation[1].AIMOwnerLocationID;
            }
            else if (_selectedSample == null)
            {
                locationToSelect = 0;
            }
            else
            {
                locationToSelect = 0;
            }

            lueOwnerLocation.EditValue = locationToSelect;
            lueOwnerLocationName.EditValue = locationToSelect;
        }

        private Boolean OwnerLocationIsInList(Int32 ownerLocationID)
        {
            for (Int32 i = 0; i < _aimOwnerLocation.Count; i++)
            {
                if (_aimOwnerLocation[i].AIMOwnerLocationID == ownerLocationID)
                {
                    return true;
                }
            }
            return false;
        }
        private void DataBindFurtherAnalyses()
        {
            if (lueDataSourceTypeSample.EditValue != null && lueDataSourceTypeSample.Text != String.Empty)
            {
                _furtherAnalyses = AIMManager.GetDataSourceFurtherAnalyses(true);

                if (_furtherAnalyses.Count == 0)
                {
                    lueFurtherAnalysis.ReadOnly = true;
                }
                else if (_furtherAnalyses.Count > 0)
                {
                    lueFurtherAnalysis.ReadOnly = false;
                    _furtherAnalyses.Insert
                    (
                        0,
                        new DataSourceTypeInfo
                        {
                            DataSourceTypeID = 0,
                            Name = "--Select--"
                        }
                    );
                }

                lueFurtherAnalysis.Properties.DataSource = _furtherAnalyses;
                XtraEditors.LoadLookUpEdit(ref lueFurtherAnalysis, "DataSourceTypeID", "Name", "TypeDisplay");

                //8/31/22
                CheckSystemSettingsFurtherAnalysis();

            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------
        #region MAIN FORM BUTTONS
        private void btnNew_Click(object sender, System.EventArgs e)
        {
            try
            {
                lueSampleCode.EditValue = 0;
                tcSample.SelectTab(0);

                DataBindOneTime();
                DataBindSelectedSample();
                DataBindMaterial();
                DataBindSampleResult();
                DataBindSampleWarning();
                DataBindQualityControl();
                DataBindLueTemplateName();
                DataBindLueQualityControlName();
                DataBindQualityControlResults();

                //6/17/20
                SetMaterialDisplayRowsVisible(false);

                //4/15/22
                lblXrefNote.Visible = false;
                lblVersionNote.Visible = false;
                _quickAddMaterialID = 0;

                //11/21/22 - changed to make value from system settings
                //9/30/22
                //lueGroundUnGround.EditValue = 0;                //0 = UnGrounded, 1 = Grounded
                CheckSystemSettingsGroundUnGround();


                //8/31/22
                CheckSystemSettingsFurtherAnalysis();

                // task 1155 FG 8/5/26 - clear leftover required-field errors from the previous sample
                ClearSampleRequiredFieldErrors();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // FG Task 1165 - capture sample ID before save to detect an existing-sample save
                Int32 sampleIDBeforeSave = 0;
                if (lueSampleCode.EditValue != null)
                {
                    Int32.TryParse(lueSampleCode.EditValue.ToString(), out sampleIDBeforeSave);
                }

                SaveSample();

                RefreshAfterSave(sampleIDBeforeSave);       // FG Task 1165

                //8/29/22 - commented
                //6/29/22
                //////SaveFurtherAnalysisSample();

                //5/10/23 - _FirstTab was null for some users -- added check for null
                //3/8/23 - added because Versova ran into problem with Enter-key getting stuck (and printing multiple labels)
                //lueOwner.Focus();
                //_FirstTab.Focus();
                if (_FirstTab != null)
                {
                    _FirstTab.Focus();
                }
                else
                {
                    lueOwner.Focus();
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
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {

                //6/6/19 - save sample code (added so Refresh doesn't move to very first sample and, instead, stays on the sample the user is on.)
                Int32 sampleCodeSave = 0;
                sampleCodeSave = (Int32)lueSampleCode.EditValue;

                DataBindSample();

                DataBindOneTime();
                DataBindSelectedSample();
                DataBindSampleResult();
                DataBindSampleWarning();
                DataBindQualityControl();
                DataBindLueTemplateName();
                DataBindLueQualityControlName();
                DataBindQualityControlResults();

                lueMaterial.EditValue = 0;
                rbIngredients.Checked = true;

                //these weren't getting reset when samples were refreshed
                deSampleDate.DateTime = DateTime.Today;
                lueDataSourceTypeSample.EditValue = 0;

                //4/15/22 - 
                ////lueOwner.EditValue = 0;

                ShowHideSupplier();

                //6/6/19
                lueSampleCode.EditValue = sampleCodeSave;

                //6/17/20
                SetMaterialDisplayRowsVisible(false);

                //6/19/20
                ShowCodeNameOrBothDropdowns("lblOwner");
                ShowCodeNameOrBothDropdowns("lblOwnerLocation");
                ShowCodeNameOrBothDropdowns("lblSupplier");
                ShowCodeNameOrBothDropdowns("lblSupplierLocation");

                //4/13/22
                HideQuickAddPanel();

                //4/21/22
                CheckLabelSize();

                //6/29/22
                ShowOrHideFurtherAnalysisRerunButton();

                //8/31/22 - commented and moved below, and databinding of fields
                CheckSystemSettings();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //3/25/25 -  * * * the OLD PrintPreview code is copied and commented at the bottom of this page
        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            List<SampleLabelFormatInfo> labels = new List<SampleLabelFormatInfo>();
            SampleLabelReport labelReport = new SampleLabelReport();
            SampleLabelReportOneInch labelReportOneInch = new SampleLabelReportOneInch();

            //LabelReportOneInch labelReportOneInch = new LabelReportOneInch();
            //PrintLabel();
            SetLabel(ref labels);
            Int32 sampleNumber = 0;
            try
            {
                if ((Int32)lueSampleCode.EditValue == 0)
                {
                    MessageBox.Show("Please select a sample.", "Warning...");
                    lueSampleCode.ShowPopup();
                    return;
                }

                //-------------------------------------------------------------------------
                //LABEL SIZE = 1
                Int32.TryParse(lueSampleCode.EditValue.ToString(), out sampleNumber);
                if (_labelSize == 1)
                {
                    if (rbProducts.Checked == true)
                    {
                        labelReportOneInch.DataSource = AIMManager.PrintLabel(sampleNumber, _showMaterialNameFirst, (Int32)MaterialTypeEnum.Product);
                    }
                    else
                    {
                        labelReportOneInch.DataSource = AIMManager.PrintLabel(sampleNumber, _showMaterialNameFirst, (Int32)MaterialTypeEnum.Ingredient);
                    }

                    //In GetPrinter below, a check is made for 'redirected' in printer-name. If exists, it replaces number after redirected with current number
                    labelReportOneInch.PrinterName = PrinterSetup.GetPrinter(PrinterTypeEnum.Label, userGlobal.CFCLoginUser.CFCUserID);
                    ReportPrintTool tool = new ReportPrintTool(labelReportOneInch);

                    //FG change 08/27/2026 - Hide()/Show() around a modal preview leaves WindowState out of sync with
                    //                          the real Size, so the anchored children get laid out for the other state and
                    //                          run past the form edge. ShowPreviewDialog is already modal, so the form does
                    //                          not need to be hidden. Same pair was already removed for the msgForm and
                    //                          sampleExport dialogs further down this file.
                    //this.Hide();
                    tool.ShowPreviewDialog();
                    //this.Show();
                }

                //-------------------------------------------------------------------------
                //LABEL SIZE = 2
                else
                {
                    if (rbProducts.Checked == true)
                    {
                        labelReport.DataSource = AIMManager.PrintLabel(sampleNumber, _showMaterialNameFirst, (Int32)MaterialTypeEnum.Product);
                    }
                    else
                    {
                        labelReport.DataSource = AIMManager.PrintLabel(sampleNumber, _showMaterialNameFirst, (Int32)MaterialTypeEnum.Ingredient);
                    }

                    //In GetPrinter below, a check is made for 'redirected' in printer-name. If exists, it replaces number after redirected with current number
                    labelReport.PrinterName = PrinterSetup.GetPrinter(PrinterTypeEnum.Label, userGlobal.CFCLoginUser.CFCUserID);

                    ReportPrintTool tool = new ReportPrintTool(labelReport);

                    //cursor change 08/27/2026 - see note in the label size 1 branch above
                    //this.Hide();
                    tool.ShowPreviewDialog();
                    //this.Show();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDeleteSample_Click(object sender, EventArgs e)

        {
            try
            {
                DialogResult dr = new DialogResult();
                ErrorInfo result = new ErrorInfo();

                if (lueSampleCode.EditValue == null || lueSampleCode.Text == String.Empty || lueSampleCode.Text == "0")
                {
                    dr = MessageBox.Show("Please enter a sample ID.", "Warning...");
                    lueSampleCode.Focus();
                    lueSampleCode.ShowPopup();
                    return;
                }

                dr = MessageBox.Show("This sample will be permanently deleted.  Do you want to delete this sample?", "Warning...", MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes)
                {
                    return;
                }

                AIMManager.DeleteSample((Int32)lueSampleCode.EditValue);
                dr = MessageBox.Show("Sample Deleted");

                lueSampleCode.EditValue = 0;
                lueSampleCode.Refresh();

                //6/24/22
                btnRefresh.PerformClick();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSampleNot_Click(object sender, EventArgs e)
        {

            SampleLog sampleLogForm = new SampleLog();
            sampleLogForm.Show();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            try
            {
                List<SampleInfo> sample = new List<SampleInfo>();

                //12/18/17
                //sample = AIMManager.GetAllSample(true);
                sample = AIMManager.GetAllSampleUseSystemDateSetting(true, _days);

                lueSampleCode.ItemIndex = sample.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            lueSampleCode.ItemIndex = 0;


            //if filter is on, skip refresh
            if (_filterOn == false)
            {
                btnRefresh.PerformClick();
            }


        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Int32 currentSampleIndex = 0;
            Int32.TryParse(lueSampleCode.ItemIndex.ToString(), out currentSampleIndex);
            lueSampleCode.ItemIndex = currentSampleIndex - 1;

            if (lueSampleCode.ItemIndex == 0)
            {
                lueSampleCode.EditValue = 0;
                tcSample.SelectTab(0);

                DataBindOneTime();
                DataBindSelectedSample();
                DataBindMaterial();
                DataBindSampleResult();
                DataBindSampleWarning();
                DataBindQualityControl();
                DataBindLueTemplateName();
                DataBindLueQualityControlName();
                DataBindQualityControlResults();

                //4/20/22
                if (txtMaterialName_Sample.Visible == false)
                {
                    lblXrefNote.Visible = false;
                }
            }

        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            Int32 currentSampleIndex = 0;
            Int32.TryParse(lueSampleCode.ItemIndex.ToString(), out currentSampleIndex);
            AIMGlobal.SplashScreenShow();
            lueSampleCode.ItemIndex = currentSampleIndex + 1;
            AIMGlobal.SplashScreenHide();
        }
        private void btnSetting_Click(object sender, EventArgs e)
        {
            try
            {
                TabOrder tabOrder = new TabOrder();
                tabOrder.ShowDialog();

                //8/23/23
                SetPageOrder();

                SetTabOrder();
                SetAlternateName();
                lueDataBindFilter();

                //6/19/20
                ShowCodeNameOrBothDropdowns("lblOwner");
                ShowCodeNameOrBothDropdowns("lblOwnerLocation");
                ShowCodeNameOrBothDropdowns("lblSupplier");
                ShowCodeNameOrBothDropdowns("lblSupplierLocation");

                //6/29/22
                ShowOrHideFurtherAnalysisRerunButton();

                //8/11/23
                DataBindTestTypeLue();

                CheckSystemSettings();                                                  //Task1054  MW250505

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion


        //---------------------------------------------------------------------------------------------
        #region SAMPLE tab
        private void lueAlternateCode_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueSampleCode.EditValue != lueAlternateCode.EditValue)
                {
                    lueSampleCode.EditValue = lueAlternateCode.EditValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueSampleCode_EditValueChanged(object sender, System.EventArgs e)
        {
            try
            {
                Boolean showMessage = false;
                Int32 sampleNumber = 0;
                Int32.TryParse(lueSampleCode.EditValue.ToString(), out sampleNumber);

                if (sampleNumber == 0)
                {
                    tbpResult.Enabled = false;
                    tbpMassEntry.Enabled = false;
                    tbpQualityControl.Enabled = false;

                    //11/16/23
                    //hyperlinkTestType.Visible = false;
                    hyperlinkTestTypeGroupCode.Visible = false;

                    //8/14/23
                    lueTestTypeGroupCode.Visible = false;
                    btnIncludeTestTypes.Visible = false;

                    //10/19/23
                    btnExcludeTestTypes.Visible = false;

                    btnAddTestType.Visible = false;

                }
                else
                {
                    tbpResult.Enabled = true;
                    tbpMassEntry.Enabled = true;
                    tbpQualityControl.Enabled = true;

                    //11/16/23
                    //hyperlinkTestType.Visible = true;
                    hyperlinkTestTypeGroupCode.Visible = true;

                    //8/14/23
                    lueTestTypeGroupCode.Visible = true;
                    btnIncludeTestTypes.Visible = true;

                    //10/19/23
                    btnExcludeTestTypes.Visible = true;

                    btnAddTestType.Visible = true;
                }

                if (lueAlternateCode.EditValue != lueSampleCode.EditValue)
                {
                    lueAlternateCode.EditValue = lueSampleCode.EditValue;
                }


                DataBindSelectedSample();
                DataBindSampleResult();
                DataBindSampleWarning();
                DataBindQualityControl();
                DataBindLueTemplateName();
                DataBindLueQualityControlName();
                DataBindQualityControlResults();

                lblDataSource_MassEntry.Visible = false;
                txtDataSource_MassEntry.Visible = false;

                lblDataSource_QualityControl.Visible = false;
                txtDataSource_QualityControl.Visible = false;


                if (showMessage == false)
                {
                    lblDataSource_MassEntry.Visible = true;
                    txtDataSource_MassEntry.Visible = true;
                    lblDataSource_QualityControl.Visible = true;
                    txtDataSource_QualityControl.Visible = true;
                }

                //5/26/20 - commented
                //lblSourceFilter.Visible = true;
                //lueDataSourceTypeSample.Visible = true;

                //6/17/20
                SetMaterialDisplayRowsVisible(true);

                if ((Int32)lueAlternateCode.EditValue == 0)
                {
                    txtSampleCode.Text = "New";

                    //6/17/20
                    SetMaterialDisplayRowsVisible(false);

                }
                else if (lueSampleCode.Text == "")
                {


                    //1/25/21
                    //txtSampleCode.Text = sampleNumber.ToString();
                    if (AIMGlobal.fromAnalysisScreen == true)
                        txtSampleCode.Text = AIMGlobal.sampleCode;
                    else
                    {
                        txtSampleCode.Text = sampleNumber.ToString();
                    }
                }
                else
                {
                    txtSampleCode.Text = lueSampleCode.Text.ToString();

                    //5/26/20 - commented
                    //lblSourceFilter.Visible = false;
                    //lueDataSourceTypeSample.Visible = false;
                }

                cbActive.BackColor = Color.Transparent;
                if (_filterOn == true && cbActive.Checked == false)
                {
                    cbActive.BackColor = Color.Yellow;
                }


                DataBindProductSpec();

                //4/20/22
                if (txtMaterialName_Sample.Visible == false)
                {
                    lblXrefNote.Visible = false;
                }

                //8/11/23
                DataBindTestType();

                ////10/17/17 - for abvista
                //txtBOL.Text = "1250";
                //txtContractNumber.Text = "N/A";
                //txtInvoiceNumber.Text = "NIR";
                //txtLoadNumber.Text = "181";
                //txtLotNumber.Text = "7";
                //txtTicketNumber.Text = "RM";
                //txtAlternateCode.Text = "LRP0028";
                //txtSampleCode.Text = "P00-216";
                //lueOwner.Text = "ABVista";
                //lueOwnerLocation.Text = "ABVista";
                //lueSource.Text = "Foster Farms";
                //lueSourceLocation.Text = "Turlock";				
                //lueAlternateCode.Text = "LRP0028";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------
        #region RESULTS tab
        private void btnAddSampleResult_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 nutrientID = 0;
                Int32.TryParse(lueAnalyzerNutrientCode.EditValue.ToString(), out nutrientID);
                Double resultvalue = 0;
                Double.TryParse(txtResultValue.Text, out resultvalue);
                if (_selectedSampleNumber == 0)
                {
                    MessageBox.Show("Please select a sample.", "warning...");
                    lueSampleCode.ShowPopup();
                    return;
                }
                if ((Int32)lueDataSourceTypeResult.EditValue == 0)
                {
                    MessageBox.Show("Please select a data source.", "Warning...", MessageBoxButtons.OK);
                    lueDataSourceTypeResult.Focus();
                    lueDataSourceTypeResult.ShowPopup();
                    return;
                }
                if (nutrientID < 1)
                {
                    MessageBox.Show("Must select a nutrient.", "warning...");
                    lueAnalyzerNutrientCode.Focus();
                    lueAnalyzerNutrientCode.ShowPopup();
                    return;
                }
                if (resultvalue <= 0)
                {
                    MessageBox.Show("Please enter a result value greater than zero.", "warning...");
                    txtResultValue.Focus();
                    return;
                }
                if ((Int32)lueDataSourceTypeResult.EditValue == 0)
                {
                    MessageBox.Show("Please select data source.", "warning...");
                    return;
                }

                SampleResultInfo sampleResult = new SampleResultInfo();
                Decimal resultValue = 0;
                Decimal.TryParse(txtResultValue.Text, out resultValue);
                sampleResult.ResultValue = resultValue;
                sampleResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                sampleResult.SampleID = _selectedSampleNumber;
                sampleResult.AnalyzerNutrientID = nutrientID;
                sampleResult.AnalyzerNutrientCode = lueAnalyzerNutrientCode.Text.Trim();
                sampleResult.DataSourceTypeID = (Int32)lueDataSourceTypeResult.EditValue;
                sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;
                sampleResult.InstrumentID = (Int32)lueInstrument.EditValue;
                sampleResult.AnalysisDate = DateTime.Now;

                AnalyzerNutrientInfo units = new AnalyzerNutrientInfo();
                units = AIMManager.GetAnalyzerNutrientUnits(nutrientID, (Int32)lueDataSourceTypeResult.EditValue);
                sampleResult.UnitType = units.UnitType;
                sampleResult.SqlResultID = String.Empty;

                //8/17/22
                //AIMManager.InsertSampleResult(sampleResult, null);
                AIMManager.InsertSampleResult(sampleResult, null, 0);

                //10/1/20
                //DataBindSampleResult();
                if ((Int32)lueDataSourceFilter.EditValue == 0)
                {
                    DataBindSampleResult();
                }
                else
                {
                    lueDataSourceFilter.EditValue = (Int32)lueDataSourceTypeResult.EditValue;
                    DataBindSampleResultForStatusAndDataSourceType((Int32)lueDataSourceTypeResult.EditValue);
                }

                txtResultValue.Text = String.Empty;

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
                SampleResultInfo sampleResult = new SampleResultInfo();
                sampleResult = (SampleResultInfo)grdSampleResultView.GetFocusedRow();
                sampleResult.IsActive = !sampleResult.IsActive;
                sampleResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                //its a soft delete
                AIMManager.DeleteSampleResult(ref sampleResult);
                DataBindSampleResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPreviewResultsGrid_Click(object sender, EventArgs e)
        {
            try
            {
                CFCAIMCore.Global.previewGridResults(grdSampleResults, "Results");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataBindLueInstrument()
        {
            List<AnalyzerInstrumentInfo> instrument = new List<AnalyzerInstrumentInfo>();
            instrument = AIMManager.GetAnalyzerInstrument(_selectedDataSourceType);
            instrument.Insert(0, new AnalyzerInstrumentInfo
            {
                InstrumentID = 0,
                Description = "---Select---"
            });
            lueInstrument.Properties.DataSource = instrument;
            XtraEditors.LoadLookUpEdit(ref lueInstrument, "InstrumentID", "Description");
            lueInstrument.EditValue = 0;
        }
        private void lueInstrument_KeyDown(object sender, KeyEventArgs e)
        {
            MoveCursorOnEnter(sender, e);
        }
        private void lueDataSourceTypeResult_Enter(object sender, EventArgs e)
        {
            lueDataSourceTypeResult.SelectAll();
        }
        private void lueDataSourceTypeResult_Click(object sender, EventArgs e)
        {
            lueDataSourceTypeResult.SelectAll();
        }
        private void lueDataSourceFilter_EditValueChanged(object sender, EventArgs e)
        {
            Int32 type = (Int32)SampleMessageType.AnalyzerNutrient;
            if ((Int32)lueDataSourceFilter.EditValue != 0)
            {
                //Task1134 DB251209 _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, (Int32)lueDataSourceFilter.EditValue, rbActiveResult.Checked, type);
                if (_resultStatus == (Int32)EnumSampleResultStatus.Active)
                    _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, (Int32)lueDataSourceFilter.EditValue, true, type);
                else if (_resultStatus == (Int32)EnumSampleResultStatus.Inactive)
                    _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, (Int32)lueDataSourceFilter.EditValue, false, type);
                else if (_resultStatus == (Int32)EnumSampleResultStatus.Both)
                    _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, (Int32)lueDataSourceFilter.EditValue, type);
                else
                {
                    _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, (Int32)lueDataSourceFilter.EditValue, rbActiveResult.Checked, type);       //Task1134 DB251209 
                }
                //Task1134 DB251209 if (_resultStatus == (Int32)EnumSampleResultStatus.Inactive)
                //Task1134 DB251209     _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, (Int32)lueDataSourceFilter.EditValue, false, type);
                //Task1134 DB251209 if (_resultStatus == (Int32)EnumSampleResultStatus.Both)
                //Task1134 DB251209     _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, (Int32)lueDataSourceFilter.EditValue, type);
                grdSampleResults.DataSource = _sampleResult;
            }
            else
            {
                DataBindSampleResult();
            }
        }               //10/1/20    
        private void lueAnalyzerNutrientCode_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataBindLueAnalyzerNutrient();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueAnalyzerNutrientCode_Enter(object sender, EventArgs e)
        {
            //10/1/20
            if ((Int32)lueAnalyzerNutrientCode.EditValue == 0 && (Int32)lueDataSourceTypeResult.EditValue == 0)
            {
                MessageBox.Show("Please select a Data Source first.");
                lueDataSourceTypeResult.ShowPopup();
            }

            lueAnalyzerNutrientCode.SelectAll();
        }
        private void lueAnalyzerNutrientCode_Click(object sender, EventArgs e)
        {
            lueAnalyzerNutrientCode.SelectAll();
        }
        private void txtResultValue_KeyDown(object sender, KeyEventArgs e)
        {
            MoveCursorOnEnter(sender, e);
        }
        private void rbActiveResult_CheckedChanged(object sender, EventArgs e)
        {
            SetResultStatus();


            if (rbActiveResult.Checked == true)
            {
                //10/1/20
                //DataBindSampleResult();
                if ((Int32)lueDataSourceFilter.EditValue != 0)
                {
                    DataBindSampleResultForStatusAndDataSourceType((Int32)lueDataSourceFilter.EditValue);
                }
                else
                {
                    DataBindSampleResult();
                }

            }
            //checkResultsDataSource();      //10/1/20 commented
        }
        private void rbInActiveResult_CheckedChanged(object sender, EventArgs e)
        {
            SetResultStatus();
            if (rbInActiveResult.Checked == true)
            {
                //10/1/20
                //DataBindSampleResult();
                if ((Int32)lueDataSourceFilter.EditValue != 0)
                {
                    DataBindSampleResultForStatusAndDataSourceType((Int32)lueDataSourceFilter.EditValue);
                }
                else
                {
                    DataBindSampleResult();
                }
            }



        }
        #endregion

        //---------------------------------------------------------------------------------------------
        #region HAND ENTRY tab
        private void DataBindLueQualityControlName()
        {
            _qualityControlName = AIMManager.GetSampleControlMassEntry();
            _qualityControlName.Insert
                (
                    0,
                    new SampleQualityControlMassEntryInfo
                    {
                        SampleQualityControlMassEntryID = 0,
                        SampleQualityControlMassEntryName = "--Select--"
                    }
                );
            lueQualityControlName.Properties.DataSource = _qualityControlName;
            XtraEditors.LoadLookUpEdit(ref lueQualityControlName, "SampleQualityControlMassEntryID", "SampleQualityControlMassEntryName");

            if (_qualityControlName.Count > 1)
            {
                lueQualityControlName.EditValue = _qualityControlName[1].SampleQualityControlMassEntryID;
            }

            _lueQualityControlID = (Int32)lueQualityControlName.EditValue;


        }                                                      //1/5/18
        #endregion

        //---------------------------------------------------------------------------------------------
        #region QC RESULTS tab
        private void hyperlinkLabelParameter_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.analyzerRequiredField = "EQUATIONPARAMETER";
                AIMGlobal.open_form(analyzerMaintenance = new Analyzer());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }                           //4/19/22
        #endregion

        //---------------------------------------------------------------------------------------------
        #region QC HAND ENTRY tab
        #endregion

        //---------------------------------------------------------------------------------------------
        #region ANALYSIS WARNING tab
        private void btnPreviewWarningGrid_Click(object sender, EventArgs e)
        {
            try
            {
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.previewGridResults(this.grdSampleWarning, "Warning");
                CFCAIMCore.Global.previewGridResults(this.grdSampleWarning, "Warning");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------
        #region TEST TYPES                                                                                  //8/11/23
        private void DataBindTestTypeLue()
        {
            //11/16/23
            //10/5/23
            //_testTypeHeader = AIMManager.GetSampleTestType();
            //_testTypeHeader = AIMManager.GetSampleTestTypeGroup(_user.SampleTestTypeGroupID);
            _testTypeHeader = AIMManager.GetTestTypeGroup_GroupID(_user.SampleTestTypeGroupID);



            _testTypeHeader.Insert
                (
                    0,
                    //new SampleTestTypeInfo
                    //{
                    //    SampleTestTypeID = 0,
                    //    SampleTestTypeName = "--Select--"
                    //}
                    new SampleTestTypeGroupInfo
                    {
                        SampleTestTypeGroupID = 0,
                        SampleTestTypeGroupCode = "--Select--"
                    }
                );
            lueTestTypeGroupCode.Properties.DataSource = _testTypeHeader;

            //11/16/23
            //XtraEditors.LoadLookUpEdit(ref lueTestTypeGroupCode, "SampleTestTypeID", "SampleTestTypeName");
            //lueTestTypeGroupCode.Properties.Columns[1].Caption = "Test Type Name";
            XtraEditors.LoadLookUpEdit(ref lueTestTypeGroupCode, "SampleTestTypeGroupID", "SampleTestTypeGroupCode");
            lueTestTypeGroupCode.Properties.Columns[1].Caption = "Group Type Code";
        }
        private void DataBindTestType()
        {
            List<SampleTestTypeResultInfo> testTypeResults = new List<SampleTestTypeResultInfo>();
            testTypeResults = AIMManager.GetTestTypeResults(_selectedSampleNumber, _testTypeStatus);
            Int32 currentRowNum = 0;
            currentRowNum = grdTestTypeView.FocusedRowHandle;
            grdTestType.DataSource = testTypeResults;
            grdTestTypeView.FocusedRowHandle = currentRowNum;
        }
        private void hyperlinkTestType_Click(object sender, EventArgs e)
        {
            try
            {
                //11/16/23
                //AIMGlobal.testTypeTab = true;
                //AIMGlobal.testTypeHeaderID = (Int32)lueTestType.EditValue;
                AIMGlobal.testTypeGroupID = (Int32)lueTestTypeGroupCode.EditValue;
                AIMGlobal.testTypeGroupTab = true;

                AIMGlobal.open_form(aimMaintenance = new AIM());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddTestType_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 testTypeID;

                //12/1/23
                //List<SampleTestTypeDetailInfo> testTypeDetail = new List<SampleTestTypeDetailInfo>();
                List<SampleTestTypeGroupDetailInfo> testTypeDetail = new List<SampleTestTypeGroupDetailInfo>();

                SampleTestTypeResultInfo testTypeResults = new SampleTestTypeResultInfo();
                List<SampleTestTypeResultInfo> datasource = new List<SampleTestTypeResultInfo>();

                //make sure test group has been selected
                if ((Int32)lueTestTypeGroupCode.EditValue == 0)
                {
                    MessageBox.Show("Please select a Test Group.", "Warning...", MessageBoxButtons.OK);
                    lueTestTypeGroupCode.Focus();
                    lueTestTypeGroupCode.ShowPopup();
                    return;
                }

                //get test type group list detail and add the records to the TestTypeResults table                
                testTypeID = (Int32)lueTestTypeGroupCode.EditValue;

                //11/16/23
                //testTypeDetail = AIMManager.GetSampleTestTypeDetail(testTypeID, true);
                testTypeDetail = AIMManager.GetSampleTestTypeGroupDetail(testTypeID, true);

                foreach (var item in testTypeDetail)
                {
                    testTypeResults.SampleTestTypeResultID = 0;
                    testTypeResults.SampleID = _selectedSampleNumber;
                    testTypeResults.SampleTestTypeDetailID = item.SampleTestTypeDetailID;
                    testTypeResults.IsIncluded = _autoFillTestTypeIncludeBox;
                    testTypeResults.IsActive = true;
                    testTypeResults.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                    AIMManager.SaveTestTypeResults(ref testTypeResults);
                }

                //databind the grid  
                DataBindTestType();

                //update Sample table with SampleTestTypeID
                AIMManager.SaveSampleTestType(_selectedSampleNumber, testTypeID, userGlobal.CFCLoginUser.CFCUserID);

                grdSampleResultView.RefreshData();
                lueTestTypeGroupCode.EditValue = 0;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void grdTestTypeView_ValidateRow(object sender, ValidateRowEventArgs e)
        {

            SampleTestTypeResultInfo type = new SampleTestTypeResultInfo();
            type = (SampleTestTypeResultInfo)grdTestTypeView.GetFocusedRow();

            //10/19/23
            //AIMManager.SaveTestTypeResults(type.SampleTestTypeResultID, type.IsIncluded, userGlobal.CFCLoginUser.CFCUserID);
            AIMManager.SaveTestTypeResults(type.SampleTestTypeResultID, type.IsIncluded, userGlobal.CFCLoginUser.CFCUserID);

        }
        private void btnIncludeTestType_Click(object sender, EventArgs e)
        {
            //10/19/23 - commented, instead added separate "Exclude" button
            //Boolean val = false;
            //if (_includeAllDatasources == false)
            //{
            //    _includeAllDatasources = true;
            //    val = true;
            //}
            //else
            //{
            //    _includeAllDatasources = false;
            //    val = false;
            //}
            List<SampleTestTypeResultInfo> type = new List<SampleTestTypeResultInfo>();
            type = (List<SampleTestTypeResultInfo>)grdTestType.DataSource;
            if (type != null)
            {
                foreach (var item in type)
                {
                    //10/19/23
                    //item.IsIncluded = val;
                    //AIMManager.SaveTestTypeResults(item.SampleTestTypeResultID, item.IsIncluded, userGlobal.CFCLoginUser.CFCUserID);                    
                    //AIMManager.SaveTestTypeResults(item.SampleTestTypeResultID, true, userGlobal.CFCLoginUser.CFCUserID);
                    AIMManager.SaveTestTypeResults(item.SampleTestTypeResultID, true, userGlobal.CFCLoginUser.CFCUserID);

                }
                //grdTestTypeView.RefreshData();
                DataBindTestType();
            }

        }
        private void SetTestTypeStatus()
        {
            if (rbActiveTestType.Checked)
                _testTypeStatus = true;
            if (rbInActiveTestType.Checked)
                _testTypeStatus = false;

        }
        private void rbActiveTestType_CheckedChanged(object sender, EventArgs e)
        {
            SetTestTypeStatus();

            if (rbActiveTestType.Checked == true)
            {
                DataBindTestType();
            }
        }
        private void rbInActiveTestType_CheckedChanged(object sender, EventArgs e)
        {
            SetTestTypeStatus();
            if (rbInActiveTestType.Checked == true)
            {
                DataBindTestType();
            }
        }
        private void grdTestTypeView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column == colImageTestType)
            {
                //get value in IsActive column for this row
                if (((SampleTestTypeResultInfo)(e.Row)).IsActive)
                {
                    e.Value = Properties.Resources.delete;
                }
                else
                {
                    e.Value = Properties.Resources.Undo_icon;
                }
            }
        }
        private void repItemPictureEditTestType_Click(object sender, EventArgs e)
        {
            try
            {
                SampleTestTypeResultInfo testType = new SampleTestTypeResultInfo();
                testType = (SampleTestTypeResultInfo)grdTestTypeView.GetFocusedRow();
                testType.IsActive = !testType.IsActive;
                AIMManager.SaveTestTypeResultsIsActive(testType.SampleTestTypeResultID, testType.IsActive, userGlobal.CFCLoginUser.CFCUserID);
                DataBindTestType();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPreviewGridTestType_Click(object sender, EventArgs e)
        {
            try
            {
                CFCAIMCore.Global.previewGridResults(grdTestType, "Test Types");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------

        private void DataBindLueTemplateName()
        {
            // Test this Dan
            Int32 aimMaterialID = 0;
            if (lueMaterial.EditValue != null)
            {
                aimMaterialID = (Int32)lueMaterial.EditValue;
            }
            _templateName = AIMManager.GetSampleResultTemplateAIMMaterialID(aimMaterialID);
            _templateName.Insert
           (
               0,
                   new SampleResultTemplateInfo
                   {
                       SampleResultTemplateID = 0,
                       SampleResultTemplateName = "--Select--"
                   }
               );
            lueTemplateName.Properties.DataSource = _templateName;
            XtraEditors.LoadLookUpEdit(ref lueTemplateName, "SampleResultTemplateID", "SampleResultTemplateName", "DataSourceTypeName");

            if (_templateName.Count > 1)
            {
                txtCurrentIteration.Visible = true;
                lblCurrentIteration.Visible = true;
                //Task1061 DB251103 txtCurrentIteration.Text = "";
                txtCurrentIteration.Text = _templateName[1].CurrentIteration.ToString();              //Task1061 DB251103
                //Task1061 DB251103 lueTemplateName.EditValue = _templateName[1].SampleResultTemplateID;
                lueTemplateName.EditValue = _templateName[1].SampleResultTemplateID_Default;                 //Task1061 DB251103 
            }
            _lueTemplateID = (Int32)lueTemplateName.EditValue;
        }

        private void DataBindMassEntryNutrient()            //Task1061 DB251031
        {
            if (_datasourcetypeID != 0)
            {
                List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
                analyzerNutrient = AIMManager.GetAnalyzerNutrient(_datasourcetypeID, true);
                analyzerNutrient.Insert
                (
                    0,
                    new AnalyzerNutrientInfo
                    {
                        AnalyzerNutrientID = 0,
                        AnalyzerNutrientCode = "--Select--"
                    }
                );
                lueMassEntryNutrient.Properties.DataSource = analyzerNutrient;
                XtraEditors.LoadLookUpEdit(ref lueMassEntryNutrient, "AnalyzerNutrientID", "AnalyzerNutrientCode");
            }
        }
        private void DataBindProductSpec()
        {
            String plantCode = "";
            String productCode = "";
            String productionVersion = "";

            AIMMaterialInfo material = new AIMMaterialInfo();
            material = AIMManager.GetAIMMaterial((Int32)lueMaterial.EditValue, true);
            if (material != null)
            {
                if (material.MaterialTypeID == (Int32)MaterialTypeEnum.Product)
                {
                    CFCPlantRefAIMOwnerLocationInfo cfcPlant = new CFCPlantRefAIMOwnerLocationInfo();
                    cfcPlant = AIMManager.GetCFCPlantAimOwnerLocationCrossReference((Int32)lueOwnerLocation.EditValue);
                    if (cfcPlant != null)
                    {
                        plantCode = cfcPlant.CFCPlantCode;
                    }

                    productCode = material.AIMMaterialCode;
                    productionVersion = lueVersion.Text;

                    List<CFCProductSpecInfo> prod = new List<CFCProductSpecInfo>();
                    prod = AIMManager.GetProductSpec(plantCode, productCode, productionVersion);
                    if (prod != null)
                    {
                        grdProductSpec.DataSource = prod;
                    }
                }
                else
                {
                    List<CFCProductSpecInfo> clearResults = new List<CFCProductSpecInfo>();
                    grdProductSpec.DataSource = clearResults;
                }
            }
        }
        private void lueSource_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //6/17/20
                SyncValueLue(ref lueSource, ref lueSupplierName);
                DataBindSourceLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueOwner_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //6/17/20
                SyncValueLue(ref lueOwner, ref lueOwnerName);

                DataBindOwnerLocation();
                DataBindMaterial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueMaterial_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataBindFurtherAnalyses();
                DataBindProductVersion();
                ShowHideSupplier();


                lblProductVersion.Visible = false;
                lueVersion.Visible = false;
                lblVersionNote.Visible = false;     //4/13/22

                //11/18/24 - moved below
                //checkSampleCrossRef();              //4/13/22

                AIMMaterialInfo materialType = new AIMMaterialInfo();
                materialType = AIMManager.GetAIMMaterial((Int32)lueMaterial.EditValue, true);

                if (materialType != null)
                {
                    //11/18/24 - moved from below
                    if (materialType.AIMMaterialName != null)           //3/28/22 - added check for null
                    {
                        txtMaterialName_Sample.Text = materialType.AIMMaterialName.ToString();
                    }

                    if (materialType.AIMMaterialCode != null)
                    {
                        txtMaterialCode_Sample.Text = materialType.AIMMaterialCode.ToString();
                    }

                    //11/18/24 - moved from above
                    checkSampleCrossRef();              //4/13/22


                    if (materialType.MaterialTypeID == (Int32)MaterialTypeEnum.Product)
                    {

                        if (_prodXrefExists == true)                        //11/18/24 
                        {
                            lblProductVersion.Visible = true;
                            lueVersion.Visible = true;

                            if ((Int32)lueVersion.EditValue == 0)           //6/24/22
                            {
                                lblVersionNote.Visible = true;              //6/24/22
                            }
                            else
                            {
                                lblVersionNote.Visible = false;             //6/24/22
                            }
                        }
                        else
                        {
                            //11/18/24
                            //if (lblXrefNote.Visible == true)                //4/13/22
                            //{                                               
                            //    lblProductVersion.Visible = false;          //4/13/22
                            //    lueVersion.Visible = false;                 //4/13/22
                            //    lblVersionNote.Visible = false;             //4/13/22    
                            //}                                              
                            lblProductVersion.Visible = false;
                            lueVersion.Visible = false;
                            lblVersionNote.Visible = false;
                        }



                        txtMaterialType.Text = MaterialTypeEnum.Product.ToString();
                        lblSupplier.Visible = false;
                        lblSupplierLocation.Visible = false;
                        lueSource.Visible = false;
                        lueSourceLocation.Visible = false;
                        lueSupplierName.Visible = false;                //6/17/20
                        lueSupplierLocationName.Visible = false;        //6/17/20

                        if (lueMaterial.GetColumnValue("AIMOwnerLocationID") != null)
                        {
                            lueOwnerLocation.EditValue = lueMaterial.GetColumnValue("AIMOwnerLocationID");
                        }
                    }

                    //11/18/24
                    else
                    {
                        if (_ingrXrefExists == true)
                        {
                            lblXrefNote.Visible = false;
                        }
                        else
                        {
                            if (_hideFormulationXrefMessage == true)
                            {
                                lblXrefNote.Visible = false;
                            }
                            else
                            {
                                lblXrefNote.Visible = true;
                            }
                        }
                    }


                    //11/18/24 - moved above
                    //if (materialType.AIMMaterialName != null)           //3/28/22 - added check for null
                    //{
                    //    txtMaterialName_Sample.Text = materialType.AIMMaterialName.ToString();
                    //}

                    if (pnlQuickAdd.Visible == true)                    //4/14/22
                    {
                        SetMaterialDisplayRowsVisible(false);
                        lblVersionNote.Visible = false;                 //4/14/22
                    }
                    else
                    {
                        SetMaterialDisplayRowsVisible(true);                     //6/17/20
                    }

                    //1/19/24
                    //Check to see if further analysis box should show up.
                    //Send AIMMaterialID, FurtherAnalysisID (datasourceid Addisseo = 11)
                    //If record exists in AIMMaterialRefAnalyzerMaterial table, show further analysis box; 
                    //Otherwise show label and [hyperlink to ref table] that says record needs to be added in order 
                    //to do further analysis.
                    List<AIMMaterialRefAnalyzerMaterialInfo> xref = new List<AIMMaterialRefAnalyzerMaterialInfo>();
                    xref = AIMManager.GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDAIMMaterialID((Int32)DataSourceTypeEnum.AdisseoAPI, (Int32)lueMaterial.EditValue);
                    if (xref.Count > 0)
                    {
                        pnlFurtherAnalysis.Visible = true;
                        lblMissingFurtherAnalysisXref.Visible = false;
                        hyperlinkAIMMaterialRefAnalyzerMaterial.Visible = false;

                        //2/8/24
                        //Check PNEMaterialType (to populate dropdown to specific type) 
                        List<PNEMaterialSampleTypeRefGroundUnGroundInfo> pne = new List<PNEMaterialSampleTypeRefGroundUnGroundInfo>();
                        pne = AIMManager.GetPNEMaterialSampleTypeRefGroundUnGround(materialType.AIMMaterialCode);
                        if (pne.Count == 1)
                        {
                            //populate dropdown with specific type
                            DataBindGroundWholeSpecificType(pne[0].GroundUnGroundID);
                        }
                        else
                        {
                            DataBindGroundUnGround();
                        }

                    }
                    else
                    {
                        pnlFurtherAnalysis.Visible = false;

                        //lblMissingFurtherAnalysisXref.Visible = true;                 //11/14/24 cm - begin
                        if (_hideFurtherAnalysisXrefMessage == true)
                        {
                            lblMissingFurtherAnalysisXref.Visible = false;
                        }
                        else
                        {
                            lblMissingFurtherAnalysisXref.Visible = true;
                        }                                                              //11/14/24 cm - end


                        hyperlinkAIMMaterialRefAnalyzerMaterial.Visible = true;
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueDataSourceType_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                _selectedDataSourceType = (Int32)lueDataSourceTypeResult.EditValue;
                DataBindLueAnalyzerNutrient();
                DataBindLueInstrument();
                checkResultsDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void checkResultsDataSource()
        {
            Int32 type = (Int32)SampleMessageType.AnalyzerNutrient;

            if (_selectedDataSourceType == 0)
            {
                if (_resultStatus == (Int32)EnumSampleResultStatus.Active)
                    _sampleResult = AIMManager.GetAllSampleResult(_selectedSampleNumber, true, type);
                if (_resultStatus == (Int32)EnumSampleResultStatus.Inactive)
                    _sampleResult = AIMManager.GetAllSampleResult(_selectedSampleNumber, false, type);
                if (_resultStatus == (Int32)EnumSampleResultStatus.Both)
                    _sampleResult = AIMManager.GetAllSampleResult(_selectedSampleNumber);

            }
            else
            {
                _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, (Int32)lueDataSourceTypeResult.EditValue, rbActiveResult.Checked, type);
                if (_resultStatus == (Int32)EnumSampleResultStatus.Active)
                    _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, (Int32)lueDataSourceTypeResult.EditValue, true, type);
                if (_resultStatus == (Int32)EnumSampleResultStatus.Inactive)
                    _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, (Int32)lueDataSourceTypeResult.EditValue, false, type);
                if (_resultStatus == (Int32)EnumSampleResultStatus.Both)
                    _sampleResult = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID(_selectedSampleNumber, (Int32)lueDataSourceTypeResult.EditValue, type);
            }
            grdSampleResults.DataSource = _sampleResult;

            //added to clear out any orphaned data - (there were results for sampleID = 0)
            if (_selectedSampleNumber == 0)
            {
                _sampleResult = new List<SampleResultInfo>();
            }


            if (_selectedDataSourceType != 0)
            {
                List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
                analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)lueDataSourceTypeResult.EditValue, true);
                analyzerNutrient.Insert
                (
                    0,
                    new AnalyzerNutrientInfo
                    {
                        AnalyzerNutrientID = 0,
                        AnalyzerNutrientCode = "--Select--"
                    }
                );
                lueAnalyzerNutrientCode.Properties.DataSource = analyzerNutrient;
                XtraEditors.LoadLookUpEdit(ref lueAnalyzerNutrientCode, "AnalyzerNutrientID", "AnalyzerNutrientCode");
            }
        }
        private void checkResultsDataSource_QualityControl()
        {

            if (_selectedDataSourceType_QualityControl == 0)
            {
                if (_resultStatus_QualityControl == (Int32)EnumSampleResultStatus.Active)
                    _sampleQualityResults = AIMManager.GetAllSampleResultQualityControl(_selectedSampleNumber, true);
                if (_resultStatus_QualityControl == (Int32)EnumSampleResultStatus.Inactive)
                    _sampleQualityResults = AIMManager.GetAllSampleResultQualityControl(_selectedSampleNumber, false);
                if (_resultStatus_QualityControl == (Int32)EnumSampleResultStatus.Both)
                    _sampleQualityResults = AIMManager.GetAllSampleResultQualityControl(_selectedSampleNumber);

            }
            else
            {
                _sampleQualityResults = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID_QualityControl(_selectedSampleNumber, (Int32)lueDataSourceTypeResultQualityControl.EditValue, rbActiveResult.Checked);
                if (_resultStatus_QualityControl == (Int32)EnumSampleResultStatus.Active)
                    _sampleQualityResults = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID_QualityControl(_selectedSampleNumber, (Int32)lueDataSourceTypeResultQualityControl.EditValue, true);
                if (_resultStatus_QualityControl == (Int32)EnumSampleResultStatus.Inactive)
                    _sampleQualityResults = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID_QualityControl(_selectedSampleNumber, (Int32)lueDataSourceTypeResultQualityControl.EditValue, false);
                if (_resultStatus_QualityControl == (Int32)EnumSampleResultStatus.Both)
                    _sampleQualityResults = AIMManager.GetAllSampleResultSampleIDDataSourceTypeID_QualityControl(_selectedSampleNumber, (Int32)lueDataSourceTypeResultQualityControl.EditValue);
            }
            grdSampleQualityResults.DataSource = _sampleQualityResults;

            //added to clear out any orphaned data - (there were results for sampleID = 0)
            if (_selectedSampleNumber == 0)
            {
                _sampleQualityResults = new List<SampleQualityInfo>();
            }


            if (_selectedDataSourceType_QualityControl != 0)
            {
                List<SampleQualityParameterInfo> param = new List<SampleQualityParameterInfo>();
                param = AIMManager.GetSampleQualityParameter((Int32)lueDataSourceTypeResultQualityControl.EditValue, true);
                param.Insert
                (
                    0,
                    new SampleQualityParameterInfo
                    {
                        SampleQualityParameterID = 0,
                        SampleQualityParameterName = "--Select--"
                    }
                );
                lueParameter.Properties.DataSource = param;
                XtraEditors.LoadLookUpEdit(ref lueParameter, "SampleQualityParameterID", "SampleQualityParameterName");
            }
        }                                           //1/9/18
        private void lueDataSourceTypeSample_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueDataSourceTypeSample.Enabled == true)
                {
                    DataBindMaterial();
                    DataBindFurtherAnalyses();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CheckColumnDescriptionOverrides()
        {
            List<ColumnDescriptionOverrideInfo> columnOverride = new List<ColumnDescriptionOverrideInfo>();
            columnOverride = AIMManager.GetColumnDescriptionOverride("Sample");
            if (columnOverride != null)
            {
                for (int i = 0; i < columnOverride.Count; i++)
                {
                    if (columnOverride[i].ColumnName == "SampleCode")
                    {
                        this.lblID.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "AlternateCode")
                    {
                        lblAlternateCode.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "SampleOwner")
                    {
                        lblOwner.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "OwnerLocation")
                    {
                        txtContractNumber.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "Source")
                    {
                        lblSupplier.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "SourceLocation")
                    {
                        lblSupplierLocation.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "ContractNumber")
                    {
                        lblContractNumber.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "PoNumber")
                    {
                        lblPONumber.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "InvoiceNumber")
                    {
                        lblInvoiceNumber.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "TicketNumber")
                    {
                        lblTicketNumber.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "LoadNumber")
                    {
                        lblLoadNumber.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "DataSource")
                    {
                        lblDataSource.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "Material")
                    {
                        this.lblProduct.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "BillOfLoad")
                    {
                        lblBOL.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "VehicleID")
                    {
                        lblVehicleID.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "LotNumber")
                    {
                        lblLotNumber.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "SampleDate")
                    {
                        this.lblDate.Text = columnOverride[i].OverrideName.ToString();
                    }
                    if (columnOverride[i].ColumnName == "FurtherAnalysis")
                    {
                        lblFurtherAnalysis.Text = columnOverride[i].OverrideName.ToString();
                    }

                }
            }

        }
        private void SetToolTips()
        {
            this.Icon = (Icon)Properties.Resources.CFCAIM;
            ToolTip ttip = new ToolTip();
            foreach (Control ctrl in pnlTopBtns.Controls)
            {
                ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
            }
            ttip.SetToolTip(btnDeleteSample, "Delete Sample");
            ttip.SetToolTip(btnAddSampleResult, "Add Record");
            ttip.SetToolTip(btnPreviewResultsGrid, "Preview Grid");
            ttip.SetToolTip(btnWarningPrintPreview, "Preview Grid");
            ttip.SetToolTip(btnSave, "Save Sample");
            ttip.SetToolTip(btnSampleSavePrintNew, "Save, Print and Create New Sample");
            ttip.SetToolTip(btnApplyFilter, "Apply Filter");
            ttip.SetToolTip(btnClearFilter, "Clear Filter");
            ttip.SetToolTip(btnPreviewMassEntry, "Preview Grid");
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
            ttip.SetToolTip(btnSampleSave, "Save and Create New Sample");
            ttip.SetToolTip(btnSaveMass, "Save Results");
            ttip.SetToolTip(lblOwner, "Open AIM Owner Maintenance");
            ttip.SetToolTip(lblOwnerLocation, "Open AIM Owner Location Maintenance");
            ttip.SetToolTip(lblSupplier, "Open AIM Supplier Maintenance");
            ttip.SetToolTip(lblSupplierLocation, "Open AIM Supplier Location Maintenance");
            ttip.SetToolTip(hyperlinkLabelControl1, "Edit Mass Entry Lists");
            ttip.SetToolTip(btnAddSampleResult_QualityControl, "Add Record");
            ttip.SetToolTip(btnPreviewResultsGrid_QualityControl, "Preview Grid");
            ttip.SetToolTip(btnQualityControlSave, "Save Results");
            ttip.SetToolTip(btnPreviewQualityControl, "Preview Grid");
            ttip.SetToolTip(hyperlinkQualityControl, "Edit Mass Entry Lists");
            ttip.SetToolTip(btnProductSpecPreview, "Preview Grid");

            //6/29/22
            ttip.SetToolTip(btnRerunFurtherAnalysis, "Rerun Further Analysis");

            //11/16/23
            //ttip.SetToolTip(hyperlinkTestType, "Edit Lab Lists");
            ttip.SetToolTip(hyperlinkTestTypeGroupCode, "Edit Group Codes");

            //8/14/23
            ttip.SetToolTip(btnIncludeTestTypes, "Include all datasources");

            //10/19/23
            ttip.SetToolTip(btnExcludeTestTypes, "Exclude all datasources");

            //11/16/23
            //ttip.SetToolTip(btnAddTestType, "Add Lab List");
            ttip.SetToolTip(btnAddTestType, "Add Group Test Types");

            ttip.SetToolTip(btnPreviewGridTestType, "Preview Grid");

        }
        private void formatDropdowns()
        {

            lueSampleCode.Properties.Columns[0].Alignment = DevExpress.Utils.HorzAlignment.Center;
            lueSampleCode.Properties.PopupWidth = 300;
            lueSampleCode.Properties.Columns[0].Width = 90;
            lueSampleCode.Properties.Columns[1].Width = 210;
            lueAlternateCode.Properties.Columns[0].Alignment = DevExpress.Utils.HorzAlignment.Near;
            lueAlternateCode.Properties.PopupWidth = 300;
            lueAlternateCode.Properties.Columns[0].Width = 90;
            lueAlternateCode.Properties.Columns[1].Width = 210;

        }
        private Boolean CheckForInactiveStatus()
        {
            Boolean rtn = false;

            List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
            owner = (List<AIMOwnerInfo>)lueOwner.Properties.DataSource;
            if (lueOwner.ItemIndex == -1)
            {
                MessageBox.Show("Owner is blank.", "warning...");
                rtn = true;
            }

            List<AIMOwnerLocationInfo> ownerLocation = new List<AIMOwnerLocationInfo>();
            ownerLocation = (List<AIMOwnerLocationInfo>)lueOwnerLocation.Properties.DataSource;
            if (lueOwnerLocation.ItemIndex == -1)
            {
                MessageBox.Show("Owner Location is blank.", "warning...");
                rtn = true;
            }

            List<AIMSupplierInfo> supplier = new List<AIMSupplierInfo>();
            supplier = (List<AIMSupplierInfo>)lueSource.Properties.DataSource;
            if (lueSource.ItemIndex == -1)
            {
                MessageBox.Show("Supplier is blank.", "warning...");
                rtn = true;
            }

            List<AIMSupplierLocationInfo> supplierLocation = new List<AIMSupplierLocationInfo>();
            supplierLocation = (List<AIMSupplierLocationInfo>)lueSourceLocation.Properties.DataSource;
            if (lueSourceLocation.ItemIndex == -1)
            {
                MessageBox.Show("Supplier Location is blank.", "warning...");
                rtn = true;
            }

            if (rtn == false)
            {

                Boolean activeOwner = owner[lueOwner.ItemIndex].IsActive;
                Boolean activeOwnerLocation = ownerLocation[lueOwnerLocation.ItemIndex].IsActive;
                Boolean activeSupplier = supplier[lueSource.ItemIndex].IsActive;
                Boolean activeSupplierLocation = supplierLocation[lueSourceLocation.ItemIndex].IsActive;

                if (cbActive.Checked == true && activeOwner == false && (Int32)lueOwner.EditValue != 0)
                {
                    MessageBox.Show("The Owner for this sample is inactive. Please activate the owner and try again.", "Warning...");
                    rtn = true;
                }
                else if (cbActive.Checked == true && activeOwnerLocation == false && (Int32)lueOwnerLocation.EditValue != 0)
                {
                    MessageBox.Show("The Owner Location for this sample is inactive. Please activate the owner location and try again.", "Warning...");
                    rtn = true;
                }
                else if (cbActive.Checked == true && activeSupplier == false && (Int32)lueSource.EditValue != 0)
                {
                    //if material is a product, the SupplierLocation is not required.
                    if (lueSource.Visible == true)
                    {
                        MessageBox.Show("The Supplier for this sample is inactive. Please activate the supplier and try again.", "Warning...");
                        rtn = true;
                    }
                }
                else if (cbActive.Checked == true && activeSupplierLocation == false && (Int32)lueSourceLocation.EditValue != 0)
                {
                    //if material is a product, the SupplierLocation is not required.
                    if (lueSourceLocation.Visible == true)
                    {
                        MessageBox.Show("The Supplier Location for this sample is inactive. Please activate the supplier location and try again.", "Warning...");
                        rtn = true;
                    }
                }
            }

            return rtn;

        }
        private void SaveSample()
        {

            //checking values in control
            _saveSample = true;


            if (CheckForInactiveStatus() == true)
            {
                _saveSample = false;
                cbActive.Checked = false;
                return;
            }

            if ((Int32)lueMaterial.EditValue == 0)
            {
                MessageBox.Show("Please select Material. Information is not saved", "Warning...");
                _saveSample = false;
                lueMaterial.Focus();
                lueMaterial.ShowPopup();
                return;
            }
            if ((Int32)lueOwner.EditValue == 0)
            {
                MessageBox.Show("Please select Owner. Information is not saved", "Warning...");
                _saveSample = false;
                lueOwner.Focus();
                lueOwner.ShowPopup();
                return;
            }

            if ((Int32)lueOwnerLocation.EditValue == 0)
            {
                MessageBox.Show("Please select Owner Location. Information is not saved", "Warning...");
                _saveSample = false;
                lueOwnerLocation.Focus();
                lueOwnerLocation.ShowPopup();
                return;
            }

            if ((Int32)lueSource.EditValue == 0)
            {
                //if material is a product, the SupplierLocation is not required.
                if (lueSource.Visible == true)
                {
                    MessageBox.Show("Please select Supplier. Information is not saved", "Warning...");
                    _saveSample = false;
                    lueSource.Focus();
                    lueSource.ShowPopup();
                    return;
                }
            }
            if ((Int32)lueSourceLocation.EditValue == 0)
            {
                //if material is a product, the SupplierLocation is not required.
                if (lueSourceLocation.Visible == true)
                {

                    MessageBox.Show("Please select Supplier Location. Information is not saved", "Warning...");
                    _saveSample = false;
                    lueSourceLocation.Focus();
                    lueSourceLocation.ShowPopup();
                    return;
                }
            }

            //4/14/22
            if (txtMaterialType.Text == MaterialTypeEnum.Product.ToString() && lueVersion.Visible == true && (Int32)lueVersion.EditValue == 0)
            //if (txtMaterialType.Text == MaterialTypeEnum.Product.ToString() && (Int32)lueVersion.EditValue == 0)
            {
                MessageBox.Show("Please select a product version. Information is not saved", "Warning...");
                lueVersion.Focus();
                lueVersion.ShowPopup();
                _saveSample = false;
                return;
            }

            //-------------------------------------------------------------------------

            // task 1155 FG 8/5/26 - admin-defined required fields for this material/location
            if (ValidateSampleRequiredFields() == false)
            {
                _saveSample = false;
                return;
            }

            //-------------------------------------------------------------------------

            //Task940 6/25/24       
            if (txtSampleCode.Text.ToUpper() == "NEW") { }

            else
            {
                //Task940 5/13/24
                Int32 currentSamp = 0;
                if (lueSampleCode.EditValue != null && lueSampleCode.Text == String.Empty)
                {
                    Int32.TryParse(lueSampleCode.EditValue.ToString(), out currentSamp);
                    lueSampleCode.Text = currentSamp.ToString();
                }
            }

            //-------------------------------------------------------------------------


            try
            {

                SampleInfo sampleInfo = new SampleInfo();
                AnalyzerPendingSampleInfo analyzer = new AnalyzerPendingSampleInfo();
                Int32 value = 0;
                Int32 savevalue = 0;
                savevalue = (Int32)lueSampleCode.EditValue;
                sampleInfo.AIMOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
                sampleInfo.AIMOwnerID = (Int32)lueOwner.EditValue;
                sampleInfo.SampleID = (Int32)lueSampleCode.EditValue;
                sampleInfo.SampleCode = lueSampleCode.Text;
                sampleInfo.AlternateCode = txtAlternateCode.Text;
                sampleInfo.AIMMaterialID = (Int32)lueMaterial.EditValue;
                sampleInfo.BillOfLoad = txtBOL.Text;
                sampleInfo.ContractNumber = txtContractNumber.Text;
                sampleInfo.InvoiceNumber = txtInvoiceNumber.Text;
                sampleInfo.IsActive = cbActive.Checked;
                sampleInfo.LoadNumber = txtLoadNumber.Text;
                sampleInfo.LotNumber = txtLotNumber.Text;
                sampleInfo.PoNumber = txtPONumber.Text;
                sampleInfo.AIMSupplierID = (Int32)lueSource.EditValue;
                sampleInfo.AIMSupplierLocationID = (Int32)lueSourceLocation.EditValue;
                sampleInfo.TicketNumber = txtTicketNumber.Text;
                sampleInfo.VehicleNumber = txtVehicleID.Text;
                sampleInfo.SampleDate = deSampleDate.DateTime.Date;
                sampleInfo.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                sampleInfo.FurtherAnalysisID = (Int32)lueFurtherAnalysis.EditValue;
                sampleInfo.DataSourceTypeID = (Int32)lueDataSourceTypeSample.EditValue;

                //---------------------------------------------------- 
                sampleInfo.MaterialTypeID = (Int32)MaterialTypeEnum.Ingredient;
                if (txtMaterialType.Text == "Product")
                {
                    sampleInfo.MaterialTypeID = (Int32)MaterialTypeEnum.Product;
                }
                sampleInfo.CFCProductArchiveID = (Int32)lueVersion.EditValue;
                //----------------------------------------------------

                sampleInfo.Notes = txtNotes.Text;
                sampleInfo.ExpirationDate = deExpirationDate.DateTime.Date;

                //9/14/20
                sampleInfo.SupportingDocumentPath = txtDocumentPath.Text.ToString();

                //1/22/21
                sampleInfo.SampleTypeID = (Int32)lueSampleType.EditValue;

                //9/30/22
                if ((Int32)lueGroundUnGround.EditValue == 1)
                {
                    sampleInfo.IsGround = true;
                }
                else
                {
                    sampleInfo.IsGround = false;
                }

                sampleInfo.SampleTestTypeID = 0;                                                //8/17/23

                if ((Int32)lueGroundUnGround.EditValue > 0)
                {
                    sampleInfo.GroundUnGroundID = (Int32)lueGroundUnGround.EditValue;           //1/19/24
                }


                ErrorInfo outPut = AIMManager.SaveSample(ref sampleInfo);

                //5/12/22
                ErrorInfo outputPendingSample = new ErrorInfo();

                if (outPut.ErrorNumber == 0)
                {
                    value = outPut.RecordID;
                    if (sampleInfo.FurtherAnalysisID > 0)
                    {
                        analyzer.SampleID = outPut.RecordID;

                        //5/12/22 - if FurtherAnalysis was used on sample screen, RecordID was getting changed!
                        //outPut = AIMManager.SaveAnalyzerPendingSample(ref analyzer);
                        outputPendingSample = AIMManager.SaveAnalyzerPendingSample(ref analyzer);

                    }
                    if (txtSampleCode.Text.ToUpper() == "NEW")
                    {
                        savevalue = outPut.RecordID;
                    }
                    else
                    {
                        savevalue = (Int32)lueSampleCode.EditValue;
                    }


                    AIMGlobal.SplashScreenShow();
                    DataBindSample();
                    AIMGlobal.SplashScreenHide();


                    //8/23/18 - object error?
                    lueSampleCode.EditValue = savevalue;

                    //5/3/22
                    lueAlternateCode.EditValue = savevalue;


                }

                //lblXrefNote.Visible = false;
                //lblVersionNote.Visible = false;
                //if ((Int32)lueMaterial.EditValue == 0)
                //{
                //    txtMaterialName_Sample.Text = "";
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving Sample - " + ex.Message, "Warning ...", MessageBoxButtons.OK);
            }
        }

        // FG Task 1165 - customer had to close and reopen the Sample screen to see saved changes.
        //Refreshes data that is otherwise only loaded in Sample_Load, and re-reads the saved sample when an
        //existing sample was saved (lueSampleCode_EditValueChanged does not fire when the sample ID did not change).
        private void RefreshAfterSave(Int32 sampleIDBeforeSave)
        {
            //SaveSample sets _saveSample = false when validation fails -- nothing was saved, so skip the refresh
            if (_saveSample == false)
            {
                return;
            }

            //Owner/Supplier/Further-Analysis/Data-Source dropdowns are normally only loaded in Sample_Load
            DataBindOneTime();

            Int32 currentSampleID = 0;
            if (lueSampleCode.EditValue != null)
            {
                Int32.TryParse(lueSampleCode.EditValue.ToString(), out currentSampleID);
            }

            //when an EXISTING sample is saved, SaveSample re-assigns the same value to lueSampleCode.EditValue,
            //so lueSampleCode_EditValueChanged never fires and the saved data is never re-read from the database.
            //DataBindSelectedSample also re-runs DataBindMaterial and restores the material display fields.
            if (sampleIDBeforeSave != 0 && sampleIDBeforeSave == currentSampleID)
            {
                DataBindSelectedSample();
                DataBindSampleResult();
                DataBindSampleWarning();
                DataBindQualityControl();
                DataBindQualityControlResults();
            }
        }

        private void SetLabel(ref List<SampleLabelFormatInfo> labels)
        {
            Int32 sampleNumber = 0;

            Int32.TryParse(lueSampleCode.EditValue.ToString(), out sampleNumber);

            if (rbProducts.Checked == true)
            {
                labels = AIMManager.PrintLabel(sampleNumber, _showMaterialNameFirst, (Int32)MaterialTypeEnum.Product);
            }
            else
            {
                labels = AIMManager.PrintLabel(sampleNumber, _showMaterialNameFirst, (Int32)MaterialTypeEnum.Ingredient);
            }

            if (labels.Count > 0)
            {
                AIMGlobal.line1Bold = labels[0].Line1Bold;
                AIMGlobal.line2Bold = labels[0].Line2Bold;
                AIMGlobal.line3Bold = labels[0].Line3Bold;
                AIMGlobal.line4Bold = labels[0].Line4Bold;
                AIMGlobal.line5Bold = labels[0].Line5Bold;
                AIMGlobal.line1FontSize = labels[0].Line1Size;                       // Task1058 MW250515
                AIMGlobal.line2FontSize = labels[0].Line2Size;                       // Task1058 MW250515
                AIMGlobal.line3FontSize = labels[0].Line3Size;                       // Task1058 MW250515
                AIMGlobal.line4FontSize = labels[0].Line4Size;                       // Task1058 MW250515
                AIMGlobal.line5FontSize = labels[0].Line5Size;                       // Task1058 MW250515
            }
        }

        //3/25/25 -  * * * the OLD PrintLabel code is copied and commented at the bottom of this page
        private void PrintLabel()
        {
            List<SampleLabelFormatInfo> labels = new List<SampleLabelFormatInfo>();
            SampleLabelReport labelReport = new SampleLabelReport();
            SampleLabelReportOneInch labelReportOneInch = new SampleLabelReportOneInch();

            // Int32 sampleNumber = 0;

            try
            {
                if ((Int32)lueSampleCode.EditValue == 0)
                {
                    MessageBox.Show("Please select a sample.", "Warning...");
                    lueSampleCode.ShowPopup();
                    return;
                }

                SetLabel(ref labels);

                //Int32.TryParse(lueSampleCode.EditValue.ToString(), out sampleNumber);


                //// Task1058 MW250515  NOT USED List<SampleLabelFormatInfo> printLabel = new List<SampleLabelFormatInfo>();
                //// Task1058 MW250515  NOT USED printLabel = AIMManager.PrintLabel(sampleNumber, _showMaterialNameFirst, (Int32)MaterialTypeEnum.Ingredient);

                //if (rbProducts.Checked == true)
                //{                    
                //    label = AIMManager.PrintLabel(sampleNumber, _showMaterialNameFirst, (Int32)MaterialTypeEnum.Product);
                //}
                //else
                //{   
                //    label = AIMManager.PrintLabel(sampleNumber, _showMaterialNameFirst, (Int32)MaterialTypeEnum.Ingredient);
                //}

                //if (label != null)
                //{
                //    AIMGlobal.line1Bold = label[0].Line1Bold;
                //    AIMGlobal.line2Bold = label[0].Line2Bold;
                //    AIMGlobal.line3Bold = label[0].Line3Bold;
                //    AIMGlobal.line4Bold = label[0].Line4Bold;
                //    AIMGlobal.line5Bold = label[0].Line5Bold;
                //    AIMGlobal.line1FontSize = label[0].Line1Size;                       // Task1058 MW250515
                //    AIMGlobal.line2FontSize = label[0].Line2Size;                       // Task1058 MW250515
                //    AIMGlobal.line3FontSize = label[0].Line3Size;                       // Task1058 MW250515
                //    AIMGlobal.line4FontSize = label[0].Line4Size;                       // Task1058 MW250515
                //    AIMGlobal.line5FontSize = label[0].Line5Size;                       // Task1058 MW250515
                //}

                labelReport.DataSource = labels;                 //default 2-inch label (5-lines)
                labelReportOneInch.DataSource = labels;          //1-inch label (3 lines) - added for Butterball

                if (_labelSize == 1)

                {
                    //6/9/23 - In GetPrinter below, a check is made for 'redirected' in printer-name. If exists, it replaces number after redirected with current number
                    labelReportOneInch.PrinterName = PrinterSetup.GetPrinter(PrinterTypeEnum.Label, userGlobal.CFCLoginUser.CFCUserID);
                    if (labelReportOneInch.PrinterName != "")
                    {
                        ReportPrintTool tool = new ReportPrintTool(labelReportOneInch);
                        tool.PrinterSettings.Copies = (short)_copies;  //ReportPrintTool copies is defined as short, so had to convert _copies to short; otherwise error pops
                        tool.Print();
                    }
                }
                else
                {
                    //6/9/23 - In GetPrinter below, a check is made for 'redirected' in printer-name. If exists, it replaces number after redirected with current number
                    labelReport.PrinterName = PrinterSetup.GetPrinter(PrinterTypeEnum.Label, userGlobal.CFCLoginUser.CFCUserID);
                    if (labelReport.PrinterName != "")
                    {
                        ReportPrintTool tool = new ReportPrintTool(labelReport);
                        tool.PrinterSettings.Copies = (short)_copies;  //ReportPrintTool copies is defined as short, so had to convert _copies to short; otherwise error pops                        
                        tool.Print();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ShowOrHideFurtherAnalysisRerunButton()
        {
            btnRerunFurtherAnalysis.Visible = false;
            List<AlternateNameInfo> alternateName = new List<AlternateNameInfo>();
            alternateName = AIMManager.GetAlternateName((Int32)EnumFormID.Sample, "lblFurtherAnalysis");
            if (alternateName != null)
            {
                if (alternateName[0].Visible == true)
                {
                    btnRerunFurtherAnalysis.Visible = true;
                }
            }
        }                                           //6/29/22        
        private void ShowCodeNameOrBothDropdowns(String controlName)
        {
            List<AlternateNameInfo> alternateName = new List<AlternateNameInfo>();
            alternateName = AIMManager.GetAlternateName((Int32)EnumFormID.Sample, controlName);
            if (alternateName != null)
            {

                switch (controlName)
                {
                    case "lblOwner":
                        lueOwner.Visible = true;
                        lueOwnerName.Visible = true;

                        //9/16/20
                        //lueOwner.Location = new Point(112, 55);           
                        //lueOwnerName.Location = new Point(293,55);
                        lueOwner.Location = new Point(158, 59);
                        lueOwnerName.Location = new Point(339, 59);

                        if (alternateName[0].ShowCodeNameOrBothID == (Int32)EnumShowCodeNameOrBoth.Both) { }
                        else if (alternateName[0].ShowCodeNameOrBothID == (Int32)EnumShowCodeNameOrBoth.Name)
                        {
                            lueOwner.Visible = false;

                            //9/16/20           
                            //lueOwnerName.Location = new Point(112,55);
                            lueOwnerName.Location = new Point(158, 59);

                        }
                        else { lueOwnerName.Visible = false; }
                        break;

                    case "lblOwnerLocation":
                        lueOwnerLocation.Visible = true;
                        lueOwnerLocationName.Visible = true;

                        //9/16/20
                        //lueOwnerLocation.Location = new Point(112, 78);
                        //lueOwnerLocationName.Location = new Point(293, 78);
                        lueOwnerLocation.Location = new Point(158, 82);
                        lueOwnerLocationName.Location = new Point(339, 82);

                        if (alternateName[0].ShowCodeNameOrBothID == (Int32)EnumShowCodeNameOrBoth.Both) { }
                        else if (alternateName[0].ShowCodeNameOrBothID == (Int32)EnumShowCodeNameOrBoth.Name)
                        {
                            lueOwnerLocation.Visible = false;

                            //9/16/20
                            //lueOwnerLocationName.Location = new Point(112, 78);
                            lueOwnerLocationName.Location = new Point(158, 82);

                        }
                        else { lueOwnerLocationName.Visible = false; }
                        break;

                    case "lblSupplier":
                        //cursor change 08/26/2026 - the whole Supplier row is hidden when Products is selected
                        //                          (see ShowHideSupplier). btnSetting_Click re-runs this method without
                        //                          re-running ShowHideSupplier, so without this guard the dropdowns come
                        //                          back on their own and leave an unlabeled row on the screen.
                        if (lblSupplier.Visible == false)
                        {
                            lueSource.Visible = false;
                            lueSupplierName.Visible = false;
                            break;
                        }

                        lueSource.Visible = true;
                        lueSupplierName.Visible = true;

                        //9/16/20
                        //lueSource.Location = new Point(112, 101);
                        //lueSupplierName.Location = new Point(293, 101);
                        lueSource.Location = new Point(158, 105);
                        lueSupplierName.Location = new Point(339, 105);

                        if (alternateName[0].ShowCodeNameOrBothID == (Int32)EnumShowCodeNameOrBoth.Both) { }
                        else if (alternateName[0].ShowCodeNameOrBothID == (Int32)EnumShowCodeNameOrBoth.Name)
                        {
                            lueSource.Visible = false;

                            //9/16/20
                            //lueSupplierName.Location = new Point(112, 101);
                            lueSupplierName.Location = new Point(158, 105);

                        }
                        else { lueSupplierName.Visible = false; }
                        break;

                    case "lblSupplierLocation":
                        //cursor change 08/26/2026 - same reason as the lblSupplier case above
                        if (lblSupplierLocation.Visible == false)
                        {
                            lueSourceLocation.Visible = false;
                            lueSupplierLocationName.Visible = false;
                            break;
                        }

                        lueSourceLocation.Visible = true;
                        lueSupplierLocationName.Visible = true;

                        //9/16/20
                        //lueSourceLocation.Location = new Point(112, 124);
                        //lueSupplierLocationName.Location = new Point(293, 124);
                        lueSourceLocation.Location = new Point(158, 128);
                        lueSupplierLocationName.Location = new Point(339, 128);

                        if (alternateName[0].ShowCodeNameOrBothID == (Int32)EnumShowCodeNameOrBoth.Both) { }
                        else if (alternateName[0].ShowCodeNameOrBothID == (Int32)EnumShowCodeNameOrBoth.Name)
                        {
                            lueSourceLocation.Visible = false;

                            //9/16/20
                            //lueSupplierLocationName.Location = new Point(112, 124);
                            lueSupplierLocationName.Location = new Point(158, 128);
                        }
                        else { lueSupplierLocationName.Visible = false; }
                        break;
                    default:
                        break;
                }

            }

        }                                  //6/19/20
        private void SetAlternateName()
        {
            String name = "";

            List<AlternateNameInfo> alternateName = new List<AlternateNameInfo>();
            alternateName = AIMManager.GetAlternateName((Int32)EnumFormID.Sample);

            //Task1042 FG 7/7/26 - Sample Screen Display group.
            //When the logged-in user is assigned a display group, that group's list of ControlIDs is the
            //definitive set of visible columns (required fields are always shown). When no group is assigned
            //(SampleScreenDisplayGroupID == 0) the original global AlternateName.Visible behavior is used.
            Int32 sampleScreenDisplayGroupID = userGlobal.CFCLoginUser.SampleScreenDisplayGroupID;
            Boolean useSampleScreenDisplayGroup = sampleScreenDisplayGroupID > 0;
            HashSet<Int32> visibleControlIDs = new HashSet<Int32>();
            if (useSampleScreenDisplayGroup)
            {
                List<SampleScreenDisplayGroupDetailInfo> displayDetail = AIMManager.GetSampleScreenDisplayDetail(sampleScreenDisplayGroupID);
                foreach (SampleScreenDisplayGroupDetailInfo detail in displayDetail)
                {
                    visibleControlIDs.Add(detail.ControlID);
                }
            }

            for (int i = 0; i < alternateName.Count; i++)
            {
                try
                {
                    Control[] control = this.Controls.Find(alternateName[i].ControlName, true);

                    List<ControlInfo> ctl = new List<ControlInfo>();
                    ctl = AIMManager.GetControl(alternateName[i].LinkToControlID);

                    if (alternateName[i].AlternateName != null)
                    {
                        name = Convert.ToString(alternateName[i].AlternateName);
                        if (name.Length > 0)
                        {
                            control[0].Text = name;
                        }

                        if (alternateName[i].RequiredField == false)
                        {
                            //Task1042 FG 7/7/26 - decide visibility from the display group (if assigned) or the global flag.
                            Boolean fieldVisible;
                            if (useSampleScreenDisplayGroup)
                            {
                                fieldVisible = visibleControlIDs.Contains(alternateName[i].ControlID);
                            }
                            else
                            {
                                fieldVisible = alternateName[i].Visible;
                            }

                            //8/14/23
                            if (ctl[0].ControlName == "lueFurtherAnalysis")
                            {
                                pnlFurtherAnalysis.Visible = true;
                            }

                            control[0].Visible = fieldVisible;
                            if (fieldVisible == false)
                            {
                                //-------------------------------------------------------------------------
                                //if this control has a LinkToControlID, using that id, get the control from the Control table
                                //and set that control.visible = false as well.
                                if (alternateName[i].LinkToControlID != 0)
                                {
                                    Control[] controlLink = this.Controls.Find(ctl[0].ControlName, true);
                                    controlLink[0].Visible = false;

                                    //8/14/23
                                    if (ctl[0].ControlName == "lueFurtherAnalysis")
                                    {
                                        pnlFurtherAnalysis.Visible = false;
                                    }
                                }
                                //-------------------------------------------------------------------------
                            }
                            else
                            {
                                if (alternateName[i].LinkToControlID != 0)
                                {
                                    Control[] controlLink = this.Controls.Find(ctl[0].ControlName, true);
                                    controlLink[0].Visible = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(alternateName[i].ControlName + " could not be found in the form. Please update the Control, Form, TabOrder and AlternateName tables in database.", "Warning...");

                }
            }
        }                                                               //6/19/20

        private void SetPageOrder()                                     //8/23/23
        {
            //---------------------------------------------------------------------
            AIMGlobal.loadingSampleScreen = true;

            //Reorder the pages according to the SampleTabControlPage table
            //Have to remove pages first, and then re-add

            tcSample.TabPages.Remove(tbpSample);
            tcSample.TabPages.Remove(tbpResult);
            tcSample.TabPages.Remove(tbpMassEntry);
            tcSample.TabPages.Remove(tpQualityControlResults);
            tcSample.TabPages.Remove(tbpQualityControl);
            tcSample.TabPages.Remove(tbpWarning);
            tcSample.TabPages.Remove(tpUserDefined);            //Cannot recall who/ why / what this tab is for
            tcSample.TabPages.Remove(tpProductSpec);
            tcSample.TabPages.Remove(tpTestType);
            _pageOrder = AIMManager.GetSampleTabControlPage();  //This sorts by tab pages, in SortOrder
            foreach (var item in _pageOrder)
            {
                if (item.IsHidden == false)
                {
                    //TabPage pageName;
                    //pageName = item.TabControlName;           //--> this won't work because string cannot be converted to pagename.
                    //tcSample.TabPages.Add(pageName);
                    switch (item.TabControlName)
                    {
                        case "tbpSample":
                            tcSample.TabPages.Add(tbpSample);
                            break;
                        case "tbpResult":
                            tcSample.TabPages.Add(tbpResult);
                            break;
                        case "tbpMassEntry":
                            tcSample.TabPages.Add(tbpMassEntry);
                            break;
                        case "tpQualityControlResults":
                            tcSample.TabPages.Add(tpQualityControlResults);
                            break;
                        case "tbpQualityControl":
                            tcSample.TabPages.Add(tbpQualityControl);
                            break;
                        case "tbpWarning":
                            tcSample.TabPages.Add(tbpWarning);
                            break;
                        case "tpUserDefined":
                            tcSample.TabPages.Add(tpUserDefined);
                            break;
                        case "tpProductSpec":
                            tcSample.TabPages.Add(tpProductSpec);
                            break;
                        case "tpTestType":
                            tcSample.TabPages.Add(tpTestType);
                            break;
                        default:
                            break;

                    }
                }
            }
            AIMGlobal.loadingSampleScreen = false;

        }
        private void SetTabOrder()
        {
            SetFilterTabStops(false);

            List<TabOrderInfo> tabOrder = new List<TabOrderInfo>();
            List<TabOrderGroupDetailInfo> tabOrderGroup = new List<TabOrderGroupDetailInfo>();

            //--------------------------------------------------------------------
            //3/14/23 
            //Get first tab control (for after clicking print buttons)
            //Couldn't figure out how to convert string to control so, setting string name, and then setting control as it loops thru below

            String firstTab = string.Empty;
            tabOrder = AIMManager.GetTabOrder(1, userGlobal.CFCLoginUser.CFCUserID, 1);
            if (tabOrder.Count == 0)
            {
                firstTab = "lueOwner";
            }

            else
            {
                firstTab = tabOrder[0].ControlName;
                List<AlternateNameInfo> alternateName = new List<AlternateNameInfo>();
                alternateName = AIMManager.GetAlternateName(1, tabOrder[0].ControlID);
                if (alternateName != null)
                {
                    if (alternateName.Count == 1)
                    {
                        if (alternateName[0].Visible == false)  //(If first tab is not visible, set to Owner field)
                        {
                            firstTab = "lueOwner";
                        }
                    }
                }
            }
            //--------------------------------------------------------------------

            tabOrderGroup = AIMManager.GetTabOrderGroupDetail(userGlobal.CFCLoginUser.TabOrderGroupID);

            if (tabOrderGroup.Count > 0)
            {
                for (int i = 0; i < tabOrderGroup.Count; i++)
                {
                    try
                    {
                        Control[] control = Controls.Find(tabOrderGroup[i].ControlName, true);
                        Boolean isTabStop = false;

                        //  Task1094 DB250903 control[0].TabIndex = tabOrderGroup[i].TabOrder;

                        //3/14/23

                        //  Task1094 DB250903 This was never working LOL
                        //  
                        ////if (tabOrderGroup[i].ControlName == firstTab)
                        ////{
                        ////    _FirstTab = control[0];
                        ////}

                        ////if (tabOrderGroup[i].TabOrder == 0)
                        ////{
                        ////    control[0].TabStop = false;
                        ////}
                        ////else
                        ////{
                        ////    control[0].TabStop = true;
                        ////}

                        //Task1094 DB250903 Start
                        if (tabOrderGroup[i].ControlName == firstTab)
                        {
                            _FirstTab = control[0];
                        }

                        if (tabOrderGroup[i].TabOrder > 0)
                        {
                            isTabStop = true;
                        }

                        if (control[0] is LookUpEdit lue)
                        {
                            lue.TabStop = isTabStop;
                            lue.TabIndex = tabOrderGroup[i].TabOrder;
                        }

                        if (control[0] is TextBox txt)
                        {
                            txt.TabStop = isTabStop;
                            txt.TabIndex = tabOrderGroup[i].TabOrder;
                        }

                        if (control[0] is CheckBox cb)
                        {
                            cb.TabStop = isTabStop;
                            cb.TabIndex = tabOrderGroup[i].TabOrder;
                        }

                        if (control[0] is RadioButton rb)
                        {
                            rb.TabStop = isTabStop;
                            rb.TabIndex = tabOrderGroup[i].TabOrder;
                        }

                        if (control[0] is DateEdit de)
                        {
                            de.TabStop = isTabStop;
                            de.TabIndex = tabOrderGroup[i].TabOrder;
                        }

                        if (control[0] is TabControl tc)
                        {
                            tc.TabStop = isTabStop;
                            tc.TabIndex = tabOrderGroup[i].TabOrder;
                        }
                        //Task1094 DB250903 End
                        //Task1094 FG 7/3/2026 Starts
                        if (control[0] is Button btn)   
                        {
                            btn.TabStop = isTabStop;
                            btn.TabIndex = tabOrderGroup[i].TabOrder;
                        }
                        //Task1094 End
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(tabOrder[i].ControlName + " could not be found in the form. Contact IT.", "Warning...");
                    }
                }
            }
            else
            {
                tabOrder = AIMManager.GetTabOrder(1, userGlobal.CFCLoginUser.CFCUserID);
                for (int i = 0; i < tabOrder.Count; i++)
                {
                    try
                    {
                        Control[] control = Controls.Find(tabOrder[i].ControlName, true);
                        //Task1094 DB250903  control[0].TabIndex = tabOrder[i].TabOrder;
                        Boolean isTabStop = false;

                        //3/14/23
                        if (tabOrder[i].ControlName == firstTab)
                        {
                            _FirstTab = control[0];
                        }

                        //Task1094 DB250903
                        //if (tabOrder[i].TabOrder == 0)
                        //{
                        //    control[0].TabStop = false;
                        //}
                        //else
                        //{
                        //    control[0].TabStop = true;
                        //}

                        //Task1094 DB250903 Start
                        if (tabOrder[i].ControlName == firstTab)
                        {
                            _FirstTab = control[0];
                        }

                        if (tabOrder[i].TabOrder > 0)
                        {
                            isTabStop = true;
                        }

                        if (control[0] is LookUpEdit lue)
                        {
                            lue.TabStop = isTabStop;
                            lue.TabIndex = tabOrder[i].TabOrder;
                        }

                        if (control[0] is TextBox txt)
                        {
                            txt.TabStop = isTabStop;
                            txt.TabIndex = tabOrder[i].TabOrder;
                        }

                        if (control[0] is CheckBox cb)
                        {
                            cb.TabStop = isTabStop;
                            cb.TabIndex = tabOrder[i].TabOrder;
                        }

                        if (control[0] is RadioButton rb)
                        {
                            rb.TabStop = isTabStop;
                            rb.TabIndex = tabOrder[i].TabOrder;
                        }

                        if (control[0] is DateEdit de)
                        {
                            de.TabStop = isTabStop;
                            de.TabIndex = tabOrder[i].TabOrder;
                        }

                        if (control[0] is TabControl tc)
                        {
                            tc.TabStop = isTabStop;
                            tc.TabIndex = tabOrder[i].TabOrder;
                        }
                        //Task1094 DB250903 End
                        // Modified in 2022 to fix a bug

                        //Task1094 FG 7/3/2026 Start
                        if (control[0] is Button btn)   
                        {
                            btn.TabStop = isTabStop;
                            btn.TabIndex = tabOrder[i].TabOrder;
                        }
                        //End

                    }
                    catch (Exception)
                    {
                        MessageBox.Show(tabOrder[i].ControlName + " could not be found in the form. Contact IT.", "Warning...");
                    }
                }
            }
        }

        private void txtScanner_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    Int32 val = 0;
                    Int32.TryParse(txtScanner.Text, out val);

                    if (val > 0)
                    {
                        lueSampleCode.EditValue = val;
                    }


                    txtScanner.Text = String.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Sample_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.GetNextControl(ActiveControl, true) != null)
                    {
                        e.SuppressKeyPress = true;
                        this.GetNextControl(ActiveControl, true).Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void grdSampleResultView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                SampleResultInfo sampleResult = new SampleResultInfo();
                sampleResult = (SampleResultInfo)grdSampleResultView.GetFocusedRow();
                sampleResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                //9/9/20 - change to use AIMCoreGlobal
                //if (AIMGlobal.aimDatabase != null)
                if (CFCAIMCore.Global.aimDatabase != null)

                {
                    //9/9/20 - change to use AIMCoreGlobal
                    //sampleResult.AimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
                    sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;

                }

                AIMManager.SaveSampleResultForResultValue(ref sampleResult);

                DataBindSampleResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void tcSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //8/23/23
                if (AIMGlobal.loadingSampleScreen == true)
                {
                    return;
                }

                Int32 sampleNumber = 0;
                Int32.TryParse(lueSampleCode.EditValue.ToString(), out sampleNumber);
                if (sampleNumber == 0)
                {
                    tbpResult.Enabled = false;
                    tbpMassEntry.Enabled = false;
                    tbpQualityControl.Enabled = false;

                    //11/16/23
                    //hyperlinkTestType.Visible = false;
                    hyperlinkTestTypeGroupCode.Visible = false;

                    //8/14/23                    
                    lueTestTypeGroupCode.Visible = false;
                    btnIncludeTestTypes.Visible = false;

                    //10/19/23
                    btnExcludeTestTypes.Visible = false;

                    btnAddTestType.Visible = false;

                }
                else
                {
                    tbpResult.Enabled = true;
                    tbpMassEntry.Enabled = true;
                    tbpQualityControl.Enabled = true;

                    //11/16/23
                    //hyperlinkTestType.Visible = true;
                    hyperlinkTestTypeGroupCode.Visible = true;

                    //8/14/23                    
                    lueTestTypeGroupCode.Visible = true;
                    btnIncludeTestTypes.Visible = true;

                    //10/19/23
                    btnExcludeTestTypes.Visible = true;

                    btnAddTestType.Visible = true;
                }

                rbActiveSample.Visible = true;
                rbInactiveSample.Visible = true;
                if (tcSample.SelectedIndex != 0)
                {
                    rbActiveSample.Visible = false;
                    rbInactiveSample.Visible = false;
                }

                AIMGlobal.massEntryTab = false;
                AIMGlobal.massEntryTemplateID = 0;

                if (tcSample.SelectedTab == tbpResult)
                {
                    lueDataSourceTypeResult.Focus();
                    lueDataSourceTypeResult.SelectAll();
                }
                if (tcSample.SelectedTab == tbpQualityControl)
                {
                    //1/5/18 - reworking quality-control form
                    //lueQualityAIMNutrient.Focus();
                    //lueQualityAIMNutrient.SelectAll();
                }
                if (tcSample.SelectedTab == tbpMassEntry)
                {
                    AIMGlobal.massEntryTab = true;
                    lueTemplateName.Focus();
                    lueTemplateName.SelectAll();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void rbInActiveSample_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //SetDeleteButtonImage();

                grdSampleResultView.Columns[5].OptionsColumn.AllowEdit = rbInactiveSample.Checked;


                DataBindOneTime();

                AIMGlobal.SplashScreenShow();
                DataBindSample();
                AIMGlobal.SplashScreenHide();

                DataBindSelectedSample();
                DataBindSampleResult();
                DataBindSampleWarning();
                DataBindQualityControl();
                DataBindLueTemplateName();

                //1/9/18
                DataBindLueQualityControlName();
                DataBindQualityControlResults();

                //move to first inactive sample
                tcSample.Enabled = true;
                if (rbInactiveSample.Checked == true)
                {
                    btnNext.PerformClick();
                    if (_selectedSampleNumber == 0)
                    {
                        tcSample.Enabled = false;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void txtScanner_Leave(object sender, EventArgs e)
        {
            try
            {

                Int32 val = 0;
                Int32.TryParse(txtScanner.Text, out val);
                if (val > 0)
                {
                    lueSampleCode.EditValue = val;
                }

                txtScanner.Text = String.Empty;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdSampleWarningView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == colMessageOutside)
                {
                    if (e.CellValue != null)
                    {
                        if (e.CellValue.ToString() == "0")
                        {
                            e.Appearance.ForeColor = e.Appearance.BackColor;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void replnkMessages_Click(object sender, EventArgs e)
        {
            /*
			try
			{			
				
				Int32 messageCount = 0;
				Int32 ID = 0;

				if (grdSampleWarningView.GetFocusedRowCellValue(colMessage) != null)
				{				
					Int32.TryParse(grdSampleWarningView.GetFocusedRowCellValue(colMessage).ToString(), out messageCount);
					Int32.TryParse(grdSampleWarningView.GetFocusedRowCellValue(colSampleResultPredictID).ToString(), out ID);
					AIMGlobal.sampleID = _selectedSampleNumber;
					AIMGlobal.sampleResultPredictID = ID;
				
					if (messageCount > 0)
					{
						MessageDetail msgForm = new MessageDetail();
						this.Hide();
						msgForm.WindowState = FormWindowState.Normal;
						msgForm.ShowDialog();
						this.Show();
						
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			*/

        }

        //Task1134 DB251211 
        //         Note: changed the behavior of the flag appearance. If value > max then ORANGE, if value < min then RED
        private void grdSampleResultView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name.ToString() == "colFlagInsideResults")
            {
                Int32 flagResult = (Int32)grdSampleResultView.GetRowCellValue(e.RowHandle, colResultValueFlag);

                // Choose image based on condition
                Image img = null;
                if (flagResult == 1)
                    img = Properties.Resources.Flags3_3;
                else if (flagResult == 2)
                    img = Properties.Resources.Flags3_2;

                if (img != null)
                {
                    e.DefaultDraw(); // draw normal cell
                    e.Graphics.DrawImage(img, e.Bounds.X + 4, e.Bounds.Y + 4, 16, 16);
                    e.Handled = true;   // prevent DevExpress from drawing over it
                }
            }
        }


        //1/5/18 - commented for now - reworking quality-control form
        //  private void DataBindLueQualityControlAimNutrient()
        //{ 
        //	List<AIMNutrientInfo> aimNutrient = new List<AIMNutrientInfo>();
        //	aimNutrient = AIMManager.GetAllAIMNutrient(true);
        //	aimNutrient.Insert
        //	(
        //		0,
        //		new AIMNutrientInfo
        //		{
        //			AIMNutrientID = 0,					
        //			AIMNutrientName = "--Select--",
        //			AIMNutrientCode = "--Select--"
        //		}
        //	);		
        //	lueQualityAIMNutrient.Properties.DataSource = aimNutrient;
        //	XtraEditors.LoadLookUpEdit(ref lueQualityAIMNutrient, "AIMNutrientID", "AIMNutrientName",  "AIMNutrientCode");

        //	replueAimNutrient.DataSource = aimNutrient;
        //	XtraEditors.LoadGridLookUpEdit(ref replueAimNutrient, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");
        //	replueAimNutrient.Columns["AIMNutrientCode"].Visible = true;
        //	replueAimNutrient.Columns["AIMNutrientName"].Visible = true;
        //}
        private void DataBindLueAnalyzerNutrient()
        {

            List<AnalyzerNutrientInfo> AnalyzerNutrient = new List<AnalyzerNutrientInfo>();
            Int32 dataType = 0;

            //if (lueDataSourceTypeResult.EditValue != null && lueDataSourceTypeResult.Text != String.Empty)
            //{
            dataType = (Int32)lueDataSourceTypeResult.EditValue;
            //}

            if (dataType == 0)
            {
                AnalyzerNutrient.Clear();
            }
            else
            {
                AnalyzerNutrient = AIMManager.GetAnalyzerNutrient(dataType, true);
            }

            AnalyzerNutrient.Insert
            (
                0,
                new AnalyzerNutrientInfo
                {
                    AnalyzerNutrientID = 0,
                    AnalyzerNutrientCode = "--Select--",
                    AnalyzerNutrientName = "--Select--"
                }
            );
            lueAnalyzerNutrientCode.Properties.DataSource = AnalyzerNutrient;
            XtraEditors.LoadLookUpEdit(ref lueAnalyzerNutrientCode, "AnalyzerNutrientID", "AnalyzerNutrientCode");
            lueAnalyzerNutrientCode.Properties.Columns["AnalyzerNutrientCode"].Visible = true;
            lueAnalyzerNutrientCode.Properties.Columns["AnalyzerNutrientName"].Visible = true;


        }
        private void DataBindLueParameter()
        {

            List<SampleQualityParameterInfo> param = new List<SampleQualityParameterInfo>();
            Int32 dataType = 0;

            dataType = (Int32)lueDataSourceTypeResultQualityControl.EditValue;
            if (dataType == 0)
            {
                param.Clear();
            }
            else
            {
                param = AIMManager.GetSampleQualityParameter(dataType, true);
            }

            param.Insert
            (
                0,
                new SampleQualityParameterInfo
                {
                    SampleQualityParameterID = 0,
                    SampleQualityParameterName = "--Select--"
                }
            );
            lueParameter.Properties.DataSource = param;
            XtraEditors.LoadLookUpEdit(ref lueParameter, "SampleQualityParameterID", "SampleQualityParameterName");


        }                                                               //1/9/18
        private void checkfilter()
        {
            if (lueDataSourceTypeResult.EditValue != null)
            {
                if ((Int32)lueDataSourceTypeResult.EditValue != 0)
                {
                    List<AIMNutrientRefAnalyzerNutrientInfo> xref = new List<AIMNutrientRefAnalyzerNutrientInfo>();
                    xref = AIMManager.GetAIMNutrientRefAnalyzerNutrientDataSourceTypeID((Int32)lueDataSourceTypeResult.EditValue);
                    Int32 currentRowNum = 0;
                    if (this.grdSampleResultView != null)
                    {
                        Int32.TryParse(grdSampleResultView.FocusedRowHandle.ToString(), out currentRowNum);
                    }
                    grdSampleResults.DataSource = xref;
                    grdSampleResultView.FocusedRowHandle = currentRowNum;

                }
            }
        }
        private void grdSampleWarningView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            Int32 messageCount = 0;
            Int32 ID = 0;

            if (e.Column.Name.ToString() == "colFlagOutside")
            {
                if (grdSampleWarningView.GetFocusedRowCellValue(colMessageOutside) != null)
                {
                    Int32.TryParse(grdSampleWarningView.GetFocusedRowCellValue(colMessageOutside).ToString(), out messageCount);
                    if (messageCount > 0)
                    {
                        Int32.TryParse(grdSampleWarningView.GetFocusedRowCellValue(colSampleResultPredictID).ToString(), out ID);

                        //9/9/20 - change to use AIMCoreGlobal
                        //AIMGlobal.sampleID = _selectedSampleNumber;
                        CFCAIMCore.Global.sampleID = _selectedSampleNumber;

                        AIMGlobal.sampleResultPredictID = ID;
                        AIMGlobal.analyzerNutrientID = 0;

                        MessageDetailOutside msgForm = new MessageDetailOutside();

                        //10/6/21 - changed for resize issue
                        //this.Hide();
                        //msgForm.WindowState = FormWindowState.Normal;                       
                        //msgForm.ShowDialog();
                        //this.Show();
                        msgForm.ShowDialog();
                    }
                }
            }
            else if (e.Column.Name.ToString() == "colFlagInside")
            {
                if (grdSampleWarningView.GetFocusedRowCellValue(colMessageInside) != null)
                {
                    Int32.TryParse(grdSampleWarningView.GetFocusedRowCellValue(colMessageInside).ToString(), out messageCount);
                    if (messageCount > 0)
                    {
                        Int32.TryParse(grdSampleWarningView.GetFocusedRowCellValue(colSampleResultPredictID).ToString(), out ID);

                        //9/9/20 - change to use AIMCoreGlobal
                        //AIMGlobal.sampleID = _selectedSampleNumber;
                        CFCAIMCore.Global.sampleID = _selectedSampleNumber;

                        AIMGlobal.sampleResultPredictID = ID;
                        AIMGlobal.analyzerNutrientID = 0;
                        AIMGlobal.sampleResultsIsActive = rbActiveResult.Checked;
                        MessageDetailInside msgForm = new MessageDetailInside();

                        //10/6/21 - changed for resize issue
                        //this.Hide();
                        //msgForm.WindowState = FormWindowState.Normal;                           
                        //msgForm.ShowDialog();
                        //this.Show();
                        msgForm.ShowDialog();
                    }
                }
            }

        }
        private void grdSampleResultView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            Int32 messageCount = 0;
            Int32 ID = 0;

            if (e.Column.Name.ToString() == "colFlagOutsideResults")
            {
                if (grdSampleResultView.GetFocusedRowCellValue(colMessageOutsideResult) != null)
                {
                    Int32.TryParse(grdSampleResultView.GetFocusedRowCellValue(colMessageOutsideResult).ToString(), out messageCount);
                    if (messageCount > 0)
                    {
                        Int32.TryParse(grdSampleResultView.GetFocusedRowCellValue(colAnalyzerNutrientID).ToString(), out ID);

                        //9/9/20 - change to use AIMCoreGlobal
                        //AIMGlobal.sampleID = _selectedSampleNumber;
                        CFCAIMCore.Global.sampleID = _selectedSampleNumber;

                        AIMGlobal.analyzerNutrientID = ID;
                        AIMGlobal.sampleResultPredictID = 0;
                        MessageDetailOutside msgForm = new MessageDetailOutside();

                        //10/6/21 - changed for resize issue
                        //this.Hide();
                        //msgForm.WindowState = FormWindowState.Normal;                       
                        //msgForm.ShowDialog();
                        //this.Show();
                        msgForm.ShowDialog();
                    }
                }
            }
            else if (e.Column.Name.ToString() == "colFlagInsideResults")
            {
                
                if (grdSampleResultView.GetFocusedRowCellValue(colMessageInsideResult) != null)
                {
                    //Task1134 DB25251211 Int32.TryParse(grdSampleResultView.GetFocusedRowCellValue(colMessageInsideResult).ToString(), out messageCount);                    
                    //Task1134 DB25251211 if (messageCount > 0)
                    //         Note: We modified the behavior of the grid to use ResultValueFlag for finding results that violate restrictions.
                    //               Before it was getting ALL nutrients when ONE nutrient was in violation. Now it gets only the violations.
                    Int32 flagResult = (Int32)grdSampleResultView.GetRowCellValue(e.RowHandle, colResultValueFlag);     //Task1134 DB25251211
                    if (flagResult > 0)                                                                                 //Task1134 DB25251211                              
                    {
                        Int32.TryParse(grdSampleResultView.GetFocusedRowCellValue(colAnalyzerNutrientID).ToString(), out ID);

                        //9/9/20 - change to use AIMCoreGlobal
                        //AIMGlobal.sampleID = _selectedSampleNumber;
                        CFCAIMCore.Global.sampleID = _selectedSampleNumber;

                        AIMGlobal.analyzerNutrientID = ID;
                        AIMGlobal.sampleResultPredictID = 0;
                        AIMGlobal.sampleResultsIsActive = rbActiveResult.Checked;
                        MessageDetailInside msgForm = new MessageDetailInside();

                        //10/6/21 - changed for resize issue
                        //this.Hide();
                        //msgForm.WindowState = FormWindowState.Normal;                       
                        //msgForm.ShowDialog();
                        //this.Show();
                        msgForm.ShowDialog();

                    }
                }
            }

        }
        private void lueSourceLocation_EditValueChanged(object sender, EventArgs e)
        {
            //6/17/20
            SyncValueLue(ref lueSourceLocation, ref lueSupplierLocationName);
        }
        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            AIMGlobal.SplashScreenShow();
            Sample_Load(sender, e);
            AIMGlobal.SplashScreenHide();
            txtFilter.Text = "";
            lueFilter.EditValue = 0;
            dtFilterBeg.Text = "";
            dtFilterEnd.Text = "";
            SetFilterTabStops(false);

            FilterSettings(false);
        }
        private void FilterSettings(Boolean setOnOff)
        {
            _filterOn = setOnOff;
            if (setOnOff == true)
            {
                pnlSearch.BackColor = Color.NavajoWhite;
                tbpSample.BackColor = Color.LightSteelBlue;
            }
            else
            {
                tbpSample.BackColor = Color.Transparent;
                pnlSearch.BackColor = Color.Transparent;
            }

        }
        private void btnApplyFilter_Click(object sender, EventArgs e)
        {

            if ((Int32)lueFilter.EditValue == 0 && dtFilterBeg.Text.Length == 0 && dtFilterEnd.Text.Length == 0)
            {
                MessageBox.Show("Please enter search criteria.", "Warning...");
                return;
            }

            AIMGlobal.SplashScreenShow();

            List<AlternateNameInfo> filter = new List<AlternateNameInfo>();
            filter = (List<AlternateNameInfo>)lueFilter.Properties.DataSource;

            Int32 controlID = filter[lueFilter.ItemIndex].ControlID;
            if (controlID == 0)
            {
                txtFilter.Text = "";
            }
            String value = txtFilter.Text;
            String dateBeg = dtFilterBeg.Text;
            String dateEnd = dtFilterEnd.Text;

            List<SampleInfo> sample = new List<SampleInfo>();
            sample = AIMManager.GetAllSampleFilter(controlID, value, dateBeg, dateEnd);

            if (sample.Count > 0)
            {
                lueSampleCode.Properties.DataSource = sample;

                XtraEditors.LoadLookUpEdit(ref lueSampleCode, "SampleID", "SampleCode");
                lueSampleCode.Properties.Columns["SampleCode"].Visible = true;
                if (sample[0].SampleCode != null)
                {
                    //3/11/25
                    //lueSampleCode.EditValue = sample[0].SampleCode;
                    //lueSampleCode.EditValue = 0;
                    lueSampleCode.EditValue = sample[0].SampleID;

                }

                lueAlternateCode.Properties.DataSource = sample;
                XtraEditors.LoadLookUpEdit(ref lueAlternateCode, "SampleID", "AlternateCode");
                lueAlternateCode.Properties.Columns["SampleCode"].Visible = true;
                if (sample[0].AlternateCode != null)
                {
                    //3/11/25
                    //lueAlternateCode.EditValue = sample[0].AlternateCode;
                    lueAlternateCode.EditValue = sample[0].SampleID;

                }

                FilterSettings(true);

                //11/15/22
                btnClearFilter.Focus();
                btnClearFilter.BackColor = SystemColors.ButtonHighlight;


            }
            else
            {
                FilterSettings(false);
                AIMGlobal.SplashScreenHide();
                MessageBox.Show("No samples found for search criteria.", "Warning...");
                return;
            }

            SetFilterTabStops(false);
            btnFirst.PerformClick();
            AIMGlobal.SplashScreenHide();

        }
        private void lueFilter_EditValueChanged(object sender, EventArgs e)
        {
            if ((Int32)lueFilter.EditValue != 0)
            {
                SetFilterTabStops(true);
                txtFilter.Focus();
            }

        }
        private void SetFilterTabStops(Boolean val)
        {
            txtFilter.TabStop = val;
            dtFilterBeg.TabStop = val;
            dtFilterEnd.TabStop = val;
            btnApplyFilter.TabStop = val;
        }
        private void dtFilterBeg_EditValueChanged(object sender, EventArgs e)
        {
            dtFilterEnd.Text = dtFilterBeg.Text;
            dtFilterEnd.Focus();
        }
        private void DrawGroupBox(GroupBox box, Graphics g, Color textColor, Color borderColor)
        {
            if (box != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                               box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               box.ClientRectangle.Width - 1,
                                               box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Clear text and border
                g.Clear(this.BackColor);

                // Draw text
                g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }
        private void btnClearFilter_Click_1(object sender, EventArgs e)
        {
            AIMGlobal.SplashScreenShow();

            lueMaterial.EditValue = 0;
            Sample_Load(sender, e);
            AIMGlobal.SplashScreenHide();
            txtFilter.Text = "";
            lueFilter.EditValue = 0;
            dtFilterBeg.Text = "";
            dtFilterEnd.Text = "";
            SetFilterTabStops(false);
            FilterSettings(false);

            //11/15/22
            btnClearFilter.BackColor = Color.Transparent;

            return;

        }
        private void dtFilterBeg_Leave(object sender, EventArgs e)
        {
            dtFilterEnd.Focus();
        }
        private void lueFilter_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void lueFilter_Enter(object sender, EventArgs e)
        {
            lueFilter.EditValue = 0;
            lueFilter.Focus();
        }
        private void lueFilter2_EditValueChanged(object sender, EventArgs e)
        {
            if ((Int32)lueFilter.EditValue != 0)
            {
                SetFilterTabStops(true);
                txtFilter.Focus();
            }
        }
        private void lueFilter2_Enter(object sender, EventArgs e)
        {
            lueFilter.SelectAll();
        }
        private void lueFilter_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void lueOwnerLocation_Enter(object sender, EventArgs e)
        {
            lueOwnerLocation.SelectAll();
        }
        private void lueOwnerLocation_Click(object sender, EventArgs e)
        {
            lueOwnerLocation.SelectAll();
        }
        private void lueDataSourceTypeSample_Click(object sender, EventArgs e)
        {
            lueDataSourceTypeSample.SelectAll();
        }
        private void lueDataSourceTypeSample_Enter(object sender, EventArgs e)
        {
            lueDataSourceTypeSample.SelectAll();
        }
        private void lueMaterial_Click(object sender, EventArgs e)
        {
            //7/19/21
            //lueMaterial.SelectAll();
            if (rbProducts.Checked == true && (Int32)lueOwnerLocation.EditValue == 0)
            {
                MessageBox.Show("Please select an Owner/Location first.");

                if ((Int32)lueOwner.EditValue > 0)
                {
                    lueOwnerLocation.ShowPopup();
                }
                else
                {
                    lueOwner.ShowPopup();
                }

            }
            else
            {
                lueMaterial.SelectAll();
            }


        }
        private void lueMaterial_Enter(object sender, EventArgs e)
        {
            //4/12/22
            pnlQuickAdd.Visible = false;
            pnlQuickAdd.Location = new Point(14, 260);

            lueMaterial.SelectAll();
        }
        private void lueOwner_Enter(object sender, EventArgs e)
        {
            lueOwner.SelectAll();
        }
        private void lueOwner_Click(object sender, EventArgs e)
        {
            lueOwner.SelectAll();
        }
        private void lueSource_Click(object sender, EventArgs e)
        {
            lueSource.SelectAll();
        }
        private void lueSource_Enter(object sender, EventArgs e)
        {
            lueSource.SelectAll();
        }
        private void lueSourceLocation_Click(object sender, EventArgs e)
        {
            lueSourceLocation.SelectAll();
        }
        private void lueSourceLocation_Enter(object sender, EventArgs e)
        {
            lueSourceLocation.SelectAll();
        }
        private void lueFurtherAnalysis_Click(object sender, EventArgs e)
        {
            lueFurtherAnalysis.SelectAll();
        }
        private void lueFurtherAnalysis_Enter(object sender, EventArgs e)
        {
            lueFurtherAnalysis.SelectAll();
        }
        private void lueFilter_Click(object sender, EventArgs e)
        {
            lueFilter.SelectAll();
        }


        private void btnExportSamples_Click(object sender, EventArgs e)
        {

            try
            {
                //10/6/21 - commented and added Close/open_form and set of Global.sampleID (because of resize issue)
                //sampleExport = new SampleExport();
                //this.Hide();
                //sampleExport.WindowState = FormWindowState.Normal;                  
                //sampleExport.ShowDialog();
                //this.Show();
                sampleExport = new SampleExport();
                sampleExport.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnPreviewMassEntry_Click(object sender, EventArgs e)
        {
            try
            {
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.previewGridResults(this.grdMassEntry, "Results Mass Entry");
                CFCAIMCore.Global.previewGridResults(this.grdMassEntry, "Results Mass Entry");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool IsLastVisibleRow(GridView view, int rowHandle)
        {
            GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
            if (viewInfo == null || viewInfo.RowsInfo == null || viewInfo.RowsInfo.Count == 0)
                return false;
            return viewInfo.RowsInfo[viewInfo.RowsInfo.Count - 1].RowHandle == rowHandle;
        }
        private void grdMassEntryView_ValidateRow(object sender, ValidateRowEventArgs e)                //Task1135 DB260418
        {

        }
        private void lueTemplateName_EditValueChanged(object sender, EventArgs e)
        {
            DataBindMassEntry();
            grdMassEntry.Focus();

            if ((Int32)lueTemplateName.EditValue != 0)
            {
                //1/29/21 - added check for null
                if (lueTemplateName.GetColumnValue("DataSourceTypeName") != null)
                {
                    txtDataSource_MassEntry.Text = lueTemplateName.GetColumnValue("DataSourceTypeName").ToString();
                    _datasourcetypeID = (Int32)lueTemplateName.GetColumnValue("DataSourceTypeID");
                }

            }
            _lueTemplateID = (Int32)lueTemplateName.EditValue;
        }
        private void SetResultStatus()
        {
            if (rbActiveResult.Checked)
                _resultStatus = (Int32)EnumSampleResultStatus.Active;
            if (rbInActiveResult.Checked)
                _resultStatus = (Int32)EnumSampleResultStatus.Inactive;
            if (rbBoth.Checked)
                _resultStatus = (Int32)EnumSampleResultStatus.Both;
        }

        private void rbBoth_CheckedChanged(object sender, EventArgs e)
        {
            SetResultStatus();
            if (rbBoth.Checked == true)
            {
                //10/1/20
                //DataBindSampleResult();
                if ((Int32)lueDataSourceFilter.EditValue != 0)
                {
                    DataBindSampleResultForStatusAndDataSourceType((Int32)lueDataSourceFilter.EditValue);
                }
                else
                {
                    DataBindSampleResult();
                }
            }
        }
        private void btnPreviewQualityControl_Click(object sender, EventArgs e)
        {
            try
            {
                CFCAIMCore.Global.previewGridResults(grdQualityControl, "Sample " + lueSampleCode.Text + " Quality Control");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //1/5/18 - commented for now, reworking quality-control form
        //      private void btnQualtyControlAdd_Click(object sender, EventArgs e)
        //{
        //	try
        //	{
        //		Int32 ID = 0;
        //		Int32 nutrientID = 0;
        //		Int32.TryParse(lueQualityAIMNutrient.EditValue.ToString(), out nutrientID);
        //		Decimal weightBeg = 0;
        //		Decimal weightEnd = 0;
        //		Decimal.TryParse(txtWeightBegin.Text, out weightBeg);
        //		Decimal.TryParse(txtWeightEnd.Text, out weightEnd);

        //		if (_selectedSampleNumber == 0)
        //		{
        //			MessageBox.Show("Please select a sample.", "warning...");
        //			lueSampleCode.ShowPopup();
        //			return;
        //		}
        //		if (nutrientID < 1)
        //		{
        //			MessageBox.Show("Please select a nutrient.", "warning...");
        //			lueQualityAIMNutrient.Focus();
        //			lueQualityAIMNutrient.ShowPopup();
        //			return;
        //		}
        //		if (weightBeg <= 0)
        //		{
        //			MessageBox.Show("Please enter a beginning weight greater than zero.", "warning...");
        //			txtWeightBegin.Focus();
        //			return;
        //		}
        //		if (weightEnd <= 0)
        //		{
        //			MessageBox.Show("Please enter an ending weight greater than zero.", "warning...");
        //			txtWeightEnd.Focus();
        //			return;
        //		}

        //		//Check to see if nutrient already exists on the grid; if so, pop message asking if it should be replaced
        //		DialogResult dr = new DialogResult();
        //		//List<SampleQualityControlInfo> check = new List<SampleQualityControlInfo>();
        //		SampleQualityControlInfo check = new SampleQualityControlInfo();
        //		check = AIMManager.GetSampleQualityControlSampleIDNutrientID(_selectedSampleNumber, nutrientID);
        //		if (check != null)
        //		{
        //			dr = MessageBox.Show("This nutrient already exists. Do you want to overwrite the values?", "Warning...", MessageBoxButtons.YesNo);
        //			if (dr != DialogResult.Yes)
        //			{
        //				return;
        //			}
        //			else
        //			{
        //				ID = check.SampleQualityControlID;
        //			}
        //		}

        //		SampleQualityControlInfo sampleControl = new SampleQualityControlInfo();
        //		sampleControl.SampleQualityControlID = ID;
        //		sampleControl.SampleID = _selectedSampleNumber;
        //		sampleControl.AIMNutrientID = nutrientID;
        //		sampleControl.SampleWeightBegin = weightBeg;
        //		sampleControl.SampleWeightEnd = weightEnd;
        //		sampleControl.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
        //		AIMManager.SaveSampleQualityControl(sampleControl);
        //		DataBindQualityControl();

        //		txtWeightBegin.Text = String.Empty;
        //		txtWeightEnd.Text = String.Empty;
        //		lueQualityAIMNutrient.EditValue = 0;
        //		lueQualityAIMNutrient.Focus();
        //		lueQualityAIMNutrient.SelectAll();

        //	}
        //	catch (Exception ex)
        //	{
        //		MessageBox.Show(ex.Message);
        //	}
        //}
        private void grdQualityControlView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            try
            {
                SampleQualityControlInfo sampleQuality = new SampleQualityControlInfo();
                sampleQuality = (SampleQualityControlInfo)grdQualityControlView.GetFocusedRow();
                sampleQuality.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SaveSampleQualityControl(sampleQuality);
                DataBindQualityControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void replueQualityControlDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SampleQualityControlInfo sampleQuality = new SampleQualityControlInfo();
                sampleQuality = (SampleQualityControlInfo)grdQualityControlView.GetFocusedRow();
                sampleQuality.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.DeleteSampleQualityControl(ref sampleQuality);
                DataBindQualityControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void grdSampleResultView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column == colImage)
            {
                //get value in IsActive column for this row
                if (((SampleResultInfo)(e.Row)).IsActive)
                {
                    e.Value = Properties.Resources.delete;
                }
                else
                {
                    e.Value = Properties.Resources.Undo_icon;
                }
            }

            /*
			//if (e.Column == colImage)
			if (e.Column.Caption == "Test")
			{
				GridView view = sender as GridView;
				if (e.Column.FieldName == "UnboundStatus" && e.IsGetData) 					
					e.Value =  getTotalValue(view, e.ListSourceRowIndex);



				Int32 test = Convert.ToInt32(grdSampleResultView.GetRowCellValue((Int32)e.Row, colIsActive));
				
				if ((String)e.Value == "1")
				{
					e.Value = Properties.Resources.delete;
					e.Column.AppearanceCell.Image = Properties.Resources.delete;				
				}
				else
				{
					e.Value = Properties.Resources.Undo_icon;
					e.Column.AppearanceCell.Image = Properties.Resources.Undo_icon;	
				}
			}
			 */
        }
        //1/5/18 - commented for now, reworking quality-control form
        ////private void txtWeightEnd_EditValueChanged(object sender, EventArgs e)
        ////{
        ////	CalculateDiff();		
        ////}

        ////private void CalculateDiff()
        ////{
        ////	Decimal diff = 0;
        ////	Decimal beg = 0;
        ////	Decimal end = 0;
        ////	if (txtWeightBegin.Text.Length > 0)
        ////	{
        ////		beg = Convert.ToDecimal(txtWeightBegin.Text);
        ////	}
        ////	if (txtWeightEnd.Text.Length > 0)
        ////	{
        ////		end = Convert.ToDecimal(txtWeightEnd.Text);
        ////	}

        ////	diff = Math.Round((end - beg), 3);
        ////	txtSampleWeightDifference.Text = Convert.ToString(diff);	
        ////}

        ////private void txtWeightBegin_EditValueChanged(object sender, EventArgs e)
        ////{
        ////	CalculateDiff();
        ////}

        ////private void lueQualityAIMNutrient_Enter(object sender, EventArgs e)
        ////{
        ////	lueQualityAIMNutrient.SelectAll();
        ////}

        ////private void lueQualityAIMNutrient_Click(object sender, EventArgs e)
        ////{
        ////	lueQualityAIMNutrient.SelectAll();
        ////}

        ////private void txtWeightBegin_Enter(object sender, EventArgs e)
        ////{
        ////	txtWeightBegin.SelectAll();
        ////}

        ////private void txtWeightEnd_Enter(object sender, EventArgs e)
        ////{
        ////	txtWeightEnd.SelectAll();
        ////}

        ////private void txtWeightBegin_Click(object sender, EventArgs e)
        ////{
        ////	txtWeightBegin.SelectAll();
        ////}
        ////private void txtWeightEnd_Click(object sender, EventArgs e)
        ////{
        ////	txtWeightEnd.SelectAll();
        ////}
        private void txtWeightBegin_KeyDown(object sender, KeyEventArgs e)
        {
            MoveCursorOnEnter(sender, e);
        }
        private void txtWeightEnd_KeyDown(object sender, KeyEventArgs e)
        {
            MoveCursorOnEnter(sender, e);
        }
        private void MoveCursorOnEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }
        private void lueQualityAIMNutrient_KeyDown(object sender, KeyEventArgs e)
        {
            MoveCursorOnEnter(sender, e);
        }
        private void lueDataSourceTypeResult_KeyDown(object sender, KeyEventArgs e)
        {
            MoveCursorOnEnter(sender, e);
        }
        private void lueAnalyzerNutrientCode_KeyDown(object sender, KeyEventArgs e)
        {
            MoveCursorOnEnter(sender, e);
        }


        private void repItemPictureEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                SampleResultInfo sampleResult = new SampleResultInfo();
                sampleResult = (SampleResultInfo)grdSampleResultView.GetFocusedRow();
                sampleResult.IsActive = !sampleResult.IsActive;
                sampleResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                //its a soft delete
                AIMManager.DeleteSampleResult(ref sampleResult);
                DataBindSampleResult();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void hyperlblOwner_Click(object sender, EventArgs e)
        {
            //open AIM maintenance, set tab to owner
            try
            {
                AIMGlobal.aimOwnerTab = true;
                if (lueOwner.EditValue != null)
                {
                    _holdID = (Int32)lueOwner.EditValue;
                }
                AIMGlobal.open_form(aimMaintenance = new AIM());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void hyperlblOwnerLocation_Click(object sender, EventArgs e)
        {
            //open AIM maintenance, set tab to owner location
            try
            {
                AIMGlobal.aimOwnerLocationTab = true;
                if (lueOwnerLocation.EditValue != null)
                {
                    _holdID = (Int32)lueOwnerLocation.EditValue;
                }
                AIMGlobal.open_form(aimMaintenance = new AIM());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void lblSupplier_Click(object sender, EventArgs e)
        {
            //open AIM maintenance, set tab to supplier
            try
            {
                AIMGlobal.aimSupplierTab = true;
                if (lueSource.EditValue != null)
                {
                    _holdID = (Int32)lueSource.EditValue;
                }
                AIMGlobal.open_form(aimMaintenance = new AIM());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void lblSupplierLocation_Click(object sender, EventArgs e)
        {
            //open AIM maintenance, set tab to supplier location
            try
            {
                AIMGlobal.aimSupplierLocationTab = true;
                if (lueSourceLocation.EditValue != null)
                {
                    _holdID = (Int32)lueSourceLocation.EditValue;
                }
                AIMGlobal.open_form(aimMaintenance = new AIM());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void lblSupplier_Leave(object sender, EventArgs e)
        {
            DataBindSupplier();
        }
        private void tbpSample_Enter(object sender, EventArgs e)
        {
            // This code is to make sure the owner, owner-loc, etc., is still valid, in case the
            // user did maintenance on the AIM tables from the Sample screen hyperlinks.


            // OWNER hyperlink --------------------------------------------
            if (AIMGlobal.aimOwnerTab == true)
            {
                DataBindOwner();
                var results = from g in _aimOwner
                              where g.AIMOwnerID == _holdID
                              select new
                              {
                                  g.AIMOwnerID
                              };
                if (results.Count() > 0)
                {
                    lueOwner.EditValue = _holdID;
                }
                else
                {
                    lueOwner.EditValue = 0;
                }
                _holdID = 0;
                AIMGlobal.aimOwnerTab = false;
            }

            // OWNER-LOCATION hyperlink -----------------------------------
            if (AIMGlobal.aimOwnerLocationTab == true)
            {
                DataBindOwnerLocation();
                var results = from g in _aimOwnerLocation
                              where g.AIMOwnerLocationID == _holdID
                              select new
                              {
                                  g.AIMOwnerLocationID
                              };
                if (results.Count() > 0)
                {
                    lueOwnerLocation.EditValue = _holdID;
                }
                else
                {
                    lueOwnerLocation.EditValue = 0;
                }
                _holdID = 0;
                AIMGlobal.aimOwnerLocationTab = false;
            }

            // SUPPLIER hyperlink --------------------------------------------
            if (AIMGlobal.aimSupplierTab == true)
            {
                DataBindSupplier();
                var results = from g in _supplier
                              where g.AIMSupplierID == _holdID
                              select new
                              {
                                  g.AIMSupplierID
                              };
                if (results.Count() > 0)
                {
                    lueSource.EditValue = _holdID;
                }
                else
                {
                    lueSource.EditValue = 0;
                }
                _holdID = 0;
                AIMGlobal.aimSupplierTab = false;
            }

            // SUPPLIER-LOCATION hyperlink -----------------------------------
            if (AIMGlobal.aimSupplierLocationTab == true)
            {
                DataBindSourceLocation();
                var results = from g in _supplierLocation
                              where g.AIMSupplierLocationID == _holdID
                              select new
                              {
                                  g.AIMSupplierLocationID
                              };
                if (results.Count() > 0)
                {
                    lueSourceLocation.EditValue = _holdID;
                }
                else
                {
                    lueSourceLocation.EditValue = 0;
                }
                _holdID = 0;
                AIMGlobal.aimSupplierLocationTab = false;
            }

        }
        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }
        private void Sample_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }
        private void rbIngredients_CheckedChanged(Object sender, EventArgs e)
        {
            DataBindMaterial();

            //4/13/22
            if (rbIngredients.Checked == true)
            {
                pnlDataSource.Visible = true;
                lblProductVersion.Visible = false;
                lueVersion.Visible = false;
                lblVersionNote.Visible = false;
            }
            else
            {
                pnlDataSource.Visible = false;
            }
        }
        private void rbProducts_CheckedChanged(Object sender, EventArgs e)
        {

            //4/13/22 
            if (rbProducts.Checked == true)
            {
                btnQuickAdd.Visible = true;
                pnlDataSource.Visible = false;
            }
            else
            {
                btnQuickAdd.Visible = false;
                pnlDataSource.Visible = true;
            }



            DataBindMaterial();
            ShowHideSupplier();

        }
        private void ShowHideSupplier()
        {
            lblSupplier.Visible = true;
            lblSupplierLocation.Visible = true;
            lueSource.Visible = true;
            lueSourceLocation.Visible = true;

            //6/17/20
            lueSupplierName.Visible = true;
            lueSupplierLocationName.Visible = true;

            if (rbProducts.Checked == true)
            {
                lblSupplier.Visible = false;
                lblSupplierLocation.Visible = false;
                lueSource.Visible = false;
                lueSourceLocation.Visible = false;
                lueSource.EditValue = 0;
                lueSourceLocation.EditValue = 0;

                //6/17/20
                lueSupplierName.Visible = false;
                lueSupplierLocationName.Visible = false;
                lueSupplierName.EditValue = 0;
                lueSupplierLocationName.EditValue = 0;
            }
        }
        private void lueOwnerLocation_EditValueChanged(Object sender, EventArgs e)
        {
            //6/17/20
            SyncValueLue(ref lueOwnerLocation, ref lueOwnerLocationName);

            DataBindMaterial();

            //4/13/22
            DataBindCFCCrossRefDropdown();

        }
        private void btnSaveMass_Click(Object sender, EventArgs e)
        {
            //loop thru grid, make sure all records are saved to SampleResult
            try
            {
                //DialogResult dr = new DialogResult();
                //Boolean allValuesAreZero = true;
                //List<SampleResultTemplateDetailInfo> checkAllZero = new List<SampleResultTemplateDetailInfo>();
                //checkAllZero = (List<SampleResultTemplateDetailInfo>)grdMassEntry.DataSource;
                //if (checkAllZero != null)
                //{
                //	foreach (var item in checkAllZero)
                //	{
                //		if (item.ResultValue != 0)
                //		{
                //			allValuesAreZero = false;
                //		}
                //	}
                //}
                //if (allValuesAreZero == true)
                //{
                //	dr = MessageBox.Show("All values are zero. Continue to Save Results?", "Warning...", MessageBoxButtons.YesNo);
                //	if (dr != DialogResult.Yes)
                //	{
                //		return;
                //	}
                //}


                Decimal resultVal = 0;
                Boolean allNumerics = true;
                Boolean missingAnalyzerNutrientCode = false;            //Task1061 DB251104
                                                                        //Task1061 DB251103 List<SampleResultTemplateDetailInfo> checkNumeric = new List<SampleResultTemplateDetailInfo>();
                                                                        //Task1061 DB251103 checkNumeric = (List<SampleResultTemplateDetailInfo>)grdMassEntry.DataSource;

                List<SampleResultTemplateGridInfo> checkNumeric = new List<SampleResultTemplateGridInfo>();      //Task1061 DB251103 
                checkNumeric = (List<SampleResultTemplateGridInfo>)grdMassEntry.DataSource;                      //Task1061 DB251103  
                if (checkNumeric != null)
                {
                    foreach (var item in checkNumeric)
                    {
                        resultVal = 0;
                        if (item.ResultValueString != null && item.ResultValueString != "")
                        {
                            if (Decimal.TryParse(item.ResultValueString, out resultVal))
                            { }
                            else
                            {
                                allNumerics = false;
                            }
                        }
                        if (item.AnalyzerNutrientCode == null)
                        {
                            missingAnalyzerNutrientCode = true;
                        }

                    }
                }

                if (allNumerics == false)
                {
                    MessageBox.Show("Some result values are invalid. Results not saved.", "Warning...");
                    return;
                }

                if (missingAnalyzerNutrientCode == true)            //Task1061 DB251104
                {
                    MessageBox.Show("Some results do not have analyzer nutrient cross refernces. Results not saved.", "Warning...");
                    return;
                }

                //Task1061 DB251103 List<SampleResultTemplateDetailInfo> result = new List<SampleResultTemplateDetailInfo>();
                //Task1061 DB251103 result = (List<SampleResultTemplateDetailInfo>)grdMassEntry.DataSource;

                List<SampleResultTemplateGridInfo> result = new List<SampleResultTemplateGridInfo>();       //Task1061 DB251103 
                result = (List<SampleResultTemplateGridInfo>)grdMassEntry.DataSource;                     //Task1061 DB251103 
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        //if ResultValueString = null, this means user has left the value blank, doesn't want to change.
                        if (item.ResultValueString != null && item.ResultValueString != "")
                        {
                            SampleResultInfo sampleResult = new SampleResultInfo();
                            sampleResult.SampleID = _selectedSampleNumber;
                            sampleResult.AnalyzerNutrientID = item.AnalyzerNutrientID;
                            sampleResult.AnalyzerNutrientCode = item.AnalyzerNutrientCode;

                            //sampleResult.ResultValue = item.ResultValue;
                            resultVal = 0;
                            Decimal.TryParse(item.ResultValueString, out resultVal);
                            sampleResult.ResultValue = resultVal;

                            sampleResult.IsActive = true;
                            sampleResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            sampleResult.UnitType = item.UnitType;
                            sampleResult.DataSourceTypeID = (Int32)lueDataSourceTypeResult.EditValue;

                            //9/9/20 - change to use AIMCoreGlobal
                            //sampleResult.AimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
                            sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;

                            //7/19/17 (Task 445)
                            sampleResult.SqlResultID = String.Empty;

                            //8/17/22
                            //AIMManager.InsertSampleResult(sampleResult, null);
                            AIMManager.InsertSampleResult(sampleResult, null, 0);
                        }
                    }

                    if ((Boolean)lueTemplateName.GetColumnValue("IsGroupTemplate") == true)                             //Task1061 DB250629 Start
                    {
                        AIMManager.UpdateSampleResultTemplateIDIteration((Int32)lueTemplateName.EditValue);
                    }                                                                                                   //Task1061 DB250629 End


                    DataBindSampleResult();


                    //4/24/17 - loop thru Mass Entry grid and zero-out result values
                    //Task1061 DB251103 result = (List<SampleResultTemplateDetailInfo>)grdMassEntry.DataSource;
                    result = (List<SampleResultTemplateGridInfo>)grdMassEntry.DataSource;       //Task1061 DB251103 
                    foreach (var item in result)
                    {
                        //Task1061 DB251103 item.ResultValue = 0;
                        item.ResultValueString = "";
                    }

                    //5/15/17
                    DataBindMassEntry();
                    DataBindLueTemplateName(); //Task1061 DB260627
                    grdMassEntryView.RefreshData();

                    MessageBox.Show("Results are Saved.", "Information...");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnSampleSavePrintNew_Click(Object sender, EventArgs e)
        {
            try
            {
                SaveSample();

                // FG Task 1165 - passing 0 because the screen resets to a new sample below,
                // so only the reference dropdowns (Owner/Supplier/etc.) need to be refreshed
                RefreshAfterSave(0);

                //8/29/22 - COMMENTED
                //6/29/22
                //SaveFurtherAnalysisSample();

                if (_saveSample == true)
                {
                    PrintLabel();
                }

                //Create a new Sample
                lueSampleCode.EditValue = 0;
                tcSample.SelectTab(0);

                //5/10/23 - _FirstTab was null for some users -- added check for null
                //3/8/23 - added because Versova ran into problem with Enter-key getting stuck (and printing multiple labels)
                //lueOwner.Focus();
                //_FirstTab.Focus();

                if (ClearSampleFieldsOnSave == true)                //Task1087 DB251205
                {
                    clearSampleScreenControl();
                }

                if (SetSampleDateToToday == true)                  //Task1118 DB251212
                {
                    deSampleDate.DateTime = DateTime.Today;
                }

                if (_FirstTab != null)
                {
                    _FirstTab.Focus();
                }
                else
                {
                    lueOwner.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSampleSave_Click(Object sender, EventArgs e)
        {
            try
            {
                SaveSample();

                // FG Task 1165 - passing 0 because the screen resets to a new sample below,
                // so only the reference dropdowns (Owner/Supplier/etc.) need to be refreshed
                RefreshAfterSave(0);

                //SaveFurtherAnalysisSample();    //8/29/22 - commented

                //Create a new Sample
                lueSampleCode.EditValue = 0;
                tcSample.SelectTab(0);

                //5/10/23 - _FirstTab was null for some users -- added check for null
                //3/8/23 - added because Versova ran into problem with Enter-key getting stuck (and printing multiple labels)
                //lueOwner.Focus();
                //_FirstTab.Focus();

                if (ClearSampleFieldsOnSave == true)                //Task1087 DB251205
                {
                    clearSampleScreenControl();
                }

                if (SetSampleDateToToday == true )                  //Task1118 DB251212
                {
                    deSampleDate.DateTime = DateTime.Today;
                }

                if (_FirstTab != null)
                {
                    _FirstTab.Focus();
                }
                else
                {
                    lueOwner.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void radioButton1_Click(Object sender, EventArgs e)
        {
            if (rbIngredients.Checked == true)
            {
                rbProducts.Checked = false;
            }
        }
        private void radioButton1_CheckedChanged(Object sender, EventArgs e)
        {
            DataBindMaterial();
        }
        private void radioButton2_CheckedChanged(Object sender, EventArgs e)
        {
            DataBindMaterial();
            ShowHideSupplier();
        }
        private void btnMassEntry_Click(Object sender, EventArgs e)
        {
            try
            {
                TabOrder tabOrder = new TabOrder();
                tabOrder.ShowDialog();
                SetTabOrder();
                SetAlternateName();
                lueDataBindFilter();
                DataBindLueTemplateName();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Sample_Activated(Object sender, EventArgs e)
        {
            //4/21/22
            CheckLabelSize();
        }
        private void btnClearNewValues_Click(Object sender, EventArgs e)
        {
            //Task1061 DB251103 List<SampleResultTemplateDetailInfo> clear = new List<SampleResultTemplateDetailInfo>();
            //Task1061 DB251103 clear = (List<SampleResultTemplateDetailInfo>)grdMassEntry.DataSource;

            List<SampleResultTemplateGridInfo> clear = new List<SampleResultTemplateGridInfo>();            //Task1061 DB251103
            clear = (List<SampleResultTemplateGridInfo>)grdMassEntry.DataSource;                            //Task1061 DB251103
            if (clear != null)
            {
                foreach (var item in clear)
                {
                    item.ResultValueString = null;
                }
                grdMassEntryView.RefreshData();
            }
        }
        private void hyperlinkLabelControl1_Click(Object sender, EventArgs e)
        {
            try
            {

                AIMGlobal.qualityControlMassEntryTab = false;
                AIMGlobal.massEntryTab = true;
                AIMGlobal.massEntryTemplateID = (Int32)lueTemplateName.EditValue;

                TabOrder tabOrder = new TabOrder();
                AIMGlobal.aimOwnerTab = true;
                //Task1103 DB251111 tabOrder.ShowDialog();
                tabOrder.Show();            //Task1103 DB251111
                SetTabOrder();
                SetAlternateName();
                lueDataBindFilter();
                DataBindLueTemplateName();
                DataBindLueQualityControlName();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        private Char Chr(Int32 v)
        {
            throw new NotImplementedException();
        }
        private void MakeEnterActLikeTab(Object sender, EventArgs e)
        {
            if (GetNextControl(ActiveControl, true) != null)
            {
                GetNextControl(ActiveControl, true).Focus();
            }
        }                                            //6/21/17 (Task 442)
        private void txtTicketNumber_KeyDown(Object sender, KeyEventArgs e)
        {
            //6/21/17 (Task 442)
            //if user hits Enter key, cursor tabs to next tabbed field
            if (e.KeyCode == Keys.Return)
            {
                MakeEnterActLikeTab(sender, e);
            }
        }
        private void txtLoadNumber_KeyDown(Object sender, KeyEventArgs e)
        {
            //6/21/17 (Task 442)
            //if user hits Enter key, cursor tabs to next tabbed field
            if (e.KeyCode == Keys.Return)
            {
                MakeEnterActLikeTab(sender, e);
            }
        }
        private void btnCurrent_Click(Object sender, EventArgs e)
        {
            lblDays.Visible = true;
            txtDays.Visible = true;
            btnCurrent.BackColor = SystemColors.GradientInactiveCaption;
            btnHistorical.BackColor = SystemColors.Control;


            //SystemColors.Control

            lblHistorical.Visible = false;

            try
            {
                CheckSystemSettings();

                DataBindOneTime();

                AIMGlobal.SplashScreenShow();
                DataBindSample();
                AIMGlobal.SplashScreenHide();

                DataBindSelectedSample();
                DataBindSampleResult();
                DataBindSampleWarning();
                DataBindQualityControl();
                DataBindLueTemplateName();

                //1/9/18
                DataBindLueQualityControlName();
                DataBindQualityControlResults();

                //move to first inactive sample
                tcSample.Enabled = true;
                if (rbInactiveSample.Checked == true)
                {
                    btnNext.PerformClick();
                    if (_selectedSampleNumber == 0)
                    {
                        tcSample.Enabled = false;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnHistorical_Click(Object sender, EventArgs e)
        {
            //12/27/17
            DialogResult dr = new DialogResult();

            dr = MessageBox.Show("This may take several minutes. Continue?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr != DialogResult.Yes)
            {
                return;
            }


            lblDays.Visible = false;
            txtDays.Visible = false;
            lblHistorical.Visible = true;
            btnCurrent.BackColor = SystemColors.Control;
            btnHistorical.BackColor = SystemColors.GradientInactiveCaption;

            try
            {
                _days = 0;

                DataBindOneTime();

                AIMGlobal.SplashScreenShow();
                DataBindSample();
                AIMGlobal.SplashScreenHide();

                DataBindSelectedSample();
                DataBindSampleResult();
                DataBindSampleWarning();
                DataBindQualityControl();
                DataBindLueTemplateName();

                //1/9/18
                DataBindLueQualityControlName();
                DataBindQualityControlResults();

                //move to first inactive sample
                tcSample.Enabled = true;
                if (rbInactiveSample.Checked == true)
                {
                    btnNext.PerformClick();
                    if (_selectedSampleNumber == 0)
                    {
                        tcSample.Enabled = false;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void hyperlinkQualityControl_Click(Object sender, EventArgs e)
        {
            try
            {

                AIMGlobal.massEntryTab = false;
                AIMGlobal.qualityControlMassEntryTab = true;
                AIMGlobal.qualityControlMassEntryID = (Int32)lueQualityControlName.EditValue;

                TabOrder tabOrder = new TabOrder();
                AIMGlobal.aimOwnerTab = true;
                tabOrder.ShowDialog();
                SetTabOrder();
                SetAlternateName();
                lueDataBindFilter();
                DataBindLueTemplateName();
                DataBindLueQualityControlName();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueQualityControlName_EditValueChanged(Object sender, EventArgs e)
        {
            DataBindQualityControl();
            grdQualityControl.Focus();

            if ((Int32)lueQualityControlName.EditValue != 0)
            {

                txtDataSource_QualityControl.Text = lueQualityControlName.GetColumnValue("DataSourceTypeName").ToString();
                _datasourcetypeID_QC = (Int32)lueQualityControlName.GetColumnValue("DataSourceTypeID");
            }

            _lueQualityControlID = (Int32)lueQualityControlName.EditValue;
        }
        private void btnQualityControlSave_Click(Object sender, EventArgs e)
        {
            //loop thru grid, make sure all records are saved to SampleResult
            try
            {
                Decimal resultVal = 0;
                Boolean allNumerics = true;
                List<SampleQualityControlMassEntryDetailInfo> checkNumeric = new List<SampleQualityControlMassEntryDetailInfo>();
                checkNumeric = (List<SampleQualityControlMassEntryDetailInfo>)grdQualityControl.DataSource;
                if (checkNumeric != null)
                {
                    foreach (var item in checkNumeric)
                    {
                        resultVal = 0;
                        if (item.ResultValueString != null && item.ResultValueString != "")
                        {
                            if (Decimal.TryParse(item.ResultValueString, out resultVal))
                            { }
                            else
                            {
                                allNumerics = false;
                            }
                        }
                    }
                }

                if (allNumerics == false)
                {
                    MessageBox.Show("Some result values are invalid. Results not saved.", "Warning...");
                    return;
                }

                DataBindQualityControlResults();

                List<SampleQualityControlMassEntryDetailInfo> result = new List<SampleQualityControlMassEntryDetailInfo>();
                result = (List<SampleQualityControlMassEntryDetailInfo>)grdQualityControl.DataSource;
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        //if ResultValueString = null, this means user has left the value blank, doesn't want to change.
                        if (item.ResultValueString != null && item.ResultValueString != "")
                        {

                            SampleQualityInfo qualityResult = new SampleQualityInfo();
                            qualityResult.SampleID = _selectedSampleNumber;
                            qualityResult.SampleQualityParameterID = item.SampleQualityParameterID;
                            resultVal = 0;
                            Decimal.TryParse(item.ResultValueString, out resultVal);
                            qualityResult.ResultValueNumeric = resultVal;
                            qualityResult.IsActive = true;
                            qualityResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            qualityResult.SampleQualityID = 0;

                            AIMManager.InsertSampleQuality(qualityResult);
                        }
                    }

                    DataBindQualityControlResults();

                    //loop thru Mass Entry grid and zero-out result values
                    result = (List<SampleQualityControlMassEntryDetailInfo>)grdQualityControl.DataSource;
                    foreach (var item in result)
                    {
                        item.ResultValue = 0;
                    }

                    DataBindQualityControl();
                    grdQualityControlView.RefreshData();
                }

                //3/7/18

                //9/9/20 - change to use AIMCoreGlobal
                //AIMManager.SaveSampleResultUsingEquation(_selectedSampleNumber, userGlobal.CFCLoginUser.CFCUserID, AIMGlobal.aimDatabase.AIMDatabaseID);
                AIMManager.SaveSampleResultUsingEquation(_selectedSampleNumber, userGlobal.CFCLoginUser.CFCUserID, CFCAIMCore.Global.aimDatabase.AIMDatabaseID);

                DataBindSampleResult();
                grdSampleResultView.RefreshData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueDataSourceTypeResult_QualityControl_EditValueChanged(Object sender, EventArgs e)
        {
        }           //1/9/18
        private void btnPreviewResultsGrid_QualityControl_Click(Object sender, EventArgs e)
        {
            try
            {
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.previewGridResults(grdSampleQualityResults, "Quality Control Results");
                CFCAIMCore.Global.previewGridResults(grdSampleQualityResults, "Quality Control Results");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddSampleResult_QualityControl_Click(Object sender, EventArgs e)
        {
            try
            {
                Int32 saveDataSourceTypeID = 0;
                Int32 paramID = 0;
                Int32.TryParse(lueParameter.EditValue.ToString(), out paramID);
                Double resultvalue = 0;
                Double.TryParse(txtResultValue_QualityControl.Text, out resultvalue);
                if (_selectedSampleNumber == 0)
                {
                    MessageBox.Show("Please select a sample.", "warning...");
                    lueSampleCode.ShowPopup();
                    return;
                }
                if ((Int32)lueDataSourceTypeResultQualityControl.EditValue == 0)
                {
                    MessageBox.Show("Please select a data source.", "Warning...", MessageBoxButtons.OK);
                    lueDataSourceTypeResultQualityControl.Focus();
                    lueDataSourceTypeResultQualityControl.ShowPopup();
                    return;
                }
                if (paramID < 1)
                {
                    MessageBox.Show("Must select a parameter.", "warning...");
                    lueParameter.Focus();
                    lueParameter.ShowPopup();
                    return;
                }
                if (resultvalue <= 0)
                {
                    MessageBox.Show("Please enter a result value greater than zero.", "warning...");
                    txtResultValue_QualityControl.Focus();
                    return;
                }

                saveDataSourceTypeID = (Int32)lueDataSourceTypeResultQualityControl.EditValue;

                SampleQualityInfo qualityResult = new SampleQualityInfo();
                Decimal resultValue = 0;
                Decimal.TryParse(txtResultValue_QualityControl.Text, out resultValue);
                qualityResult.ResultValueNumeric = resultValue;
                qualityResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                qualityResult.SampleID = _selectedSampleNumber;
                qualityResult.SampleQualityParameterID = paramID;

                AIMManager.InsertSampleQuality(qualityResult);
                DataBindQualityControlResults();
                txtResultValue_QualityControl.Text = String.Empty;
                lueDataSourceTypeResultQualityControl.EditValue = saveDataSourceTypeID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueDataSourceTypeResultQualityControl_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                _selectedDataSourceType_QualityControl = (Int32)lueDataSourceTypeResult.EditValue;
                DataBindLueParameter();
                checkResultsDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void grdSampleQualityResultsView_ValidateRow(Object sender, ValidateRowEventArgs e)
        {
            try
            {
                SampleQualityInfo result = new SampleQualityInfo();
                result = (SampleQualityInfo)grdSampleQualityResultsView.GetFocusedRow();
                result.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SaveSampleResultForResultValueQualityControl(ref result);
                DataBindQualityControlResults();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void grdSampleQualityResultsView_CustomUnboundColumnData(Object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column == colImage_QualityControl)
            {
                //get value in IsActive column for this row
                if (((SampleQualityInfo)(e.Row)).IsActive)
                {
                    e.Value = Properties.Resources.delete;
                }
                else
                {
                    e.Value = Properties.Resources.Undo_icon;
                }
            }
        }
        private void repItemPictureEdit_QualityControlResults_Click(Object sender, EventArgs e)
        {
            SampleQualityInfo qualityResult = new SampleQualityInfo();
            qualityResult = (SampleQualityInfo)grdSampleQualityResultsView.GetFocusedRow();
            qualityResult.IsActive = !qualityResult.IsActive;
            qualityResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

            //its a soft delete
            AIMManager.DeleteSampleQualityResult(ref qualityResult);
            DataBindQualityControlResults();
        }
        private void rbActive_QualityControl_CheckedChanged(Object sender, EventArgs e)
        {
            SetResultStatus_QualityControl();

            if (rbActive_QualityControl.Checked == true)
            {
                DataBindQualityControlResults();
            }
            checkResultsDataSource_QualityControl();
        }
        private void SetResultStatus_QualityControl()
        {
            if (rbActive_QualityControl.Checked)

                _resultStatus_QualityControl = (Int32)EnumSampleResultStatus.Active;
            if (rbInActive_QualityControl.Checked)
                _resultStatus_QualityControl = (Int32)EnumSampleResultStatus.Inactive;
            if (rbBoth_QualityControl.Checked)
                _resultStatus_QualityControl = (Int32)EnumSampleResultStatus.Both;
        }
        private void rbInActive_QualityControl_CheckedChanged(Object sender, EventArgs e)
        {
            SetResultStatus_QualityControl();
            if (rbInActive_QualityControl.Checked == true)
                DataBindQualityControlResults();
        }
        private void rbBoth_QualityControl_CheckedChanged(Object sender, EventArgs e)
        {
            SetResultStatus_QualityControl();
            if (rbBoth_QualityControl.Checked == true)
            {
                DataBindQualityControlResults();
            }
        }
        private void btnProductSpecPreview_Click(Object sender, EventArgs e)
        {
            try
            {
                CFCAIMCore.Global.previewGridResults(grdProductSpec, "Product Spec");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void hyperMaterial_Click(object sender, EventArgs e)
        {
            //open Analyzer maintenance
            if (_showAnalyzerMaterials == true)
            {
                try
                {
                    AIMGlobal.open_form(analyzerMaintenance = new Analyzer());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
                }
            }

            //3/4/25
            else
            {
                try
                {
                    AIMGlobal.open_form(aimMaintenance = new AIM());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
                }
            }
        }

        //2/26/20
        private void hyperMaterialCrossRef_Click(object sender, EventArgs e)
        {

            try
            {
                //4/15/22 - changed to formulation x-ref                
                //AIMGlobal.open_form(analyzerCrossRef = new AnalyzerCrossReference());                
                AIMGlobal.open_form(formulationCrossRef = new FormulationCrossReference());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void lueOwnerName_Click(object sender, EventArgs e)
        {
            lueOwnerName.SelectAll();
        }                                   //6/17/20        
        private void lueOwnerName_Enter(object sender, EventArgs e)
        {
            lueOwnerName.SelectAll();
        }                                   //6/17/20
        private void lueOwnerName_EditValueChanged(object sender, EventArgs e)
        {
            //6/17/20
            SyncValueLue(ref lueOwnerName, ref lueOwner);
        }
        private void SyncValueLue(ref DevExpress.XtraEditors.LookUpEdit from, ref LookUpEdit to)
        {
            if (from != null && to != null)
            {
                to.EditValue = from.EditValue;
            }
        }       //6/17/20
        private void lueOwnerLocationName_EditValueChanged(object sender, EventArgs e)
        {
            //6/17/20
            SyncValueLue(ref lueOwnerLocationName, ref lueOwnerLocation);
        }
        private void lueSupplierName_EditValueChanged(object sender, EventArgs e)
        {
            //6/17/20
            SyncValueLue(ref lueSupplierName, ref lueSource);
        }
        private void lueSupplierLocationName_EditValueChanged(object sender, EventArgs e)
        {
            //6/17/20
            SyncValueLue(ref lueSupplierLocationName, ref lueSourceLocation);
        }
        private void lueOwnerLocationName_Click(object sender, EventArgs e)
        {
            lueOwnerLocationName.SelectAll();

        }                           //6/17/20        
        private void lueOwnerLocationName_Enter(object sender, EventArgs e)
        {
            lueOwnerLocationName.SelectAll();
        }                           //6/17/20
        private void SelectText(object sender, EventArgs e)
        {
            LookUpEdit edit;
            edit = (LookUpEdit)sender;
            edit.SelectionStart = 0;
            edit.SelectionLength = edit.Text.Length;
        }
        private void btnBrowseDocument_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                String path = txtDocumentPath.ToString();
                if (System.IO.Directory.Exists(path))
                {
                    openFileDialog.InitialDirectory = path;
                }
                else
                {
                    openFileDialog.InitialDirectory = @"C:\";
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    txtDocumentPath.Text = openFileDialog.FileName;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnViewDocument_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            ErrorInfo result = new ErrorInfo();

            if (txtDocumentPath.Text == String.Empty)
            {
                dr = MessageBox.Show("Please enter a document path.", "Warning...");
                txtDocumentPath.Focus();
                return;
            }

            if ((txtDocumentPath.Text.Substring(txtDocumentPath.Text.Length - 5).ToUpper() == ".XLSX") || (txtDocumentPath.Text.Substring(txtDocumentPath.Text.Length - 4).ToUpper() == ".XLS"))
            {
                dr = MessageBox.Show("Unable to open Excel document. Please select another type of document (ex: TXT, CSV, etc.)", "Warning...");
                txtDocumentPath.Focus();
                return;
            }

            if (txtDocumentPath.Text.Substring(txtDocumentPath.Text.Length - 4).ToUpper() == ".PDF")
            {

                CFCAIMCore.Global.PDFPathAndFileName = txtDocumentPath.Text;
                AIMGlobal.open_form(pdf = new PDF());
            }
            else
            {
                RichTextBox rtb = new RichTextBox();
                rtb.LoadFile(txtDocumentPath.Text, RichTextBoxStreamType.PlainText);
                RichTextBoxLink rtbl = new RichTextBoxLink(new PrintingSystem());
                rtbl.RichTextBox = rtb;
                rtbl.ShowPreviewDialog();
            }
        }                               //9/14/20        
        private void button1_Click(object sender, EventArgs e)
        {
            txtDocumentPath.Text = String.Empty;
        }                                       //9/14/10
        private void lueDataSourceTypeResult_EditValueChanged(object sender, EventArgs e)
        {
            List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
            analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)lueDataSourceTypeResult.EditValue, true);
            analyzerNutrient.Insert
            (
                0,
                new AnalyzerNutrientInfo
                {
                    AnalyzerNutrientID = 0,
                    AnalyzerNutrientCode = "--Select--"
                }
            );
            lueAnalyzerNutrientCode.Properties.DataSource = analyzerNutrient;
            XtraEditors.LoadLookUpEdit(ref lueAnalyzerNutrientCode, "AnalyzerNutrientID", "AnalyzerNutrientCode");
        }

        SampleMaterialHelp samplematerialhelp;                                                          //10/2/20
        private void btnSampleMaterialHelp_Click(object sender, EventArgs e)                            //10/2/20
        {
            try
            {
                samplematerialhelp = new SampleMaterialHelp();
                samplematerialhelp.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueMaterial_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {
            if (rbProducts.Checked == true && lueMaterial.Text.Length > 0)
            {
                txtQuickAddCode.Text = lueMaterial.Text;
                txtQuickAddName.Text = "";
                txtQuickAddName.Text = GetCFCProductCodeDescription((Int32)lueOwnerLocation.EditValue, txtQuickAddCode.Text.Trim());
                ShowQuickAddPanel();
            }
        }   //4/12/22
        private void btnQuickAddSave_Click(object sender, EventArgs e)
        {
            try
            {
                AIMMaterialInfo aimMaterial = new AIMMaterialInfo();

                aimMaterial.AIMMaterialID = 0;
                aimMaterial.AIMMaterialCode = txtQuickAddCode.Text;
                aimMaterial.AIMMaterialName = txtQuickAddName.Text;
                aimMaterial.MaterialTypeID = (Int32)MaterialTypeEnum.Product;
                aimMaterial.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                aimMaterial.AIMOwnerLocationID = (Int32)lueOwnerLocation.EditValue;

                if (string.IsNullOrWhiteSpace(txtQuickAddCode.Text) == true)
                {
                    MessageBox.Show("Please enter a Material Code. ", "Warning...", MessageBoxButtons.OK);
                    txtQuickAddCode.Focus();
                    return;
                }
                if (txtQuickAddName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please enter a Material Name. ", "Warning...", MessageBoxButtons.OK);
                    txtQuickAddName.Focus();
                    return;
                }
                if ((Int32)lueOwnerLocation.EditValue == 0)
                {
                    MessageBox.Show("Please select an Owner Location. ", "Warning...", MessageBoxButtons.OK);
                    lueOwnerLocation.Focus();
                    lueOwnerLocation.ShowPopup();
                    return;
                }
                ErrorInfo error = new ErrorInfo();
                Int32 recID = 0;
                error = AIMManager.SaveAIMMaterial(ref aimMaterial, false);
                recID = error.RecordID;

                //4/15/22
                _quickAddMaterialID = recID;

                if (error.ErrorNumber > 0)
                {
                    return;
                }


                //Add Cross-Reference if it doesn't already exist

                //using aimownerlocationID, check plant xref to get cfcplantID
                CFCPlantRefAIMOwnerLocationInfo cfcPlant = new CFCPlantRefAIMOwnerLocationInfo();
                cfcPlant = AIMManager.GetCFCPlantAimOwnerLocationCrossReference((Int32)lueOwnerLocation.EditValue);
                if (cfcPlant != null)
                {
                    //using cfcplantID and entered product-code, check cfcproduct table for productID, if it exists.
                    List<CFCProductInfo> cfcProduct = new List<CFCProductInfo>();
                    cfcProduct = AIMManager.GetCFCProduct(cfcPlant.CFCPlantID, txtQuickAddCode.Text.Trim());

                    //using cfcProductID, check material xref to see if one exists - and create if it does not.
                    if (cfcProduct != null)
                    {
                        if (cfcProduct.Count > 0)
                        {
                            CFCProductRefAIMMaterialInfo xref = new CFCProductRefAIMMaterialInfo();
                            xref = AIMManager.GetCFCProductRefAIMMaterial(cfcProduct[0].CFCProductID);
                            if (xref == null)
                            {
                                //Create x-reference, using either the CFCProduct found above
                                CFCProductRefAIMMaterialInfo productRef = new CFCProductRefAIMMaterialInfo();
                                productRef.AIMMaterialID = recID;
                                productRef.CFCProductID = cfcProduct[0].CFCProductID;
                                productRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                                ErrorInfo xrefError = new ErrorInfo();
                                xrefError = AIMManager.SaveCFCProductRefAIMMaterial(ref productRef);
                                if (xrefError.ErrorNumber > 0)
                                {
                                    MessageBox.Show(xrefError.ErrorMessage, "Warning...");
                                    return;
                                }
                            }
                        }
                        else if ((Int32)lueProductCode.EditValue > 0)
                        {
                            //Create x-reference, using product selected in the quick-add dropdown 
                            CFCProductRefAIMMaterialInfo productRef = new CFCProductRefAIMMaterialInfo();
                            productRef.AIMMaterialID = recID;
                            productRef.CFCProductID = (Int32)lueProductCode.EditValue;
                            productRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            ErrorInfo xrefError = new ErrorInfo();
                            xrefError = AIMManager.SaveCFCProductRefAIMMaterial(ref productRef);
                            if (xrefError.ErrorNumber > 0)
                            {
                                MessageBox.Show(xrefError.ErrorMessage, "Warning...");
                                return;
                            }
                        }

                    }


                }

                DataBindMaterial();
                HideQuickAddPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }                                       //4/12/22
        private void btnCancelQuickAdd_Click(object sender, EventArgs e)
        {
            HideQuickAddPanel();
        }                                   //4/12/22
        private void pnlQuickAdd_Leave(object sender, EventArgs e)
        {
            pnlQuickAdd.Visible = false;
            pnlQuickAdd.Location = new Point(14, 260);
            if (rbProducts.Checked == true)
            {
                btnQuickAdd.Visible = true;
            }
        }                                           //4/12/22
        private void ShowQuickAddPanel()
        {
            //MessageBox.Show(e.DisplayValue.ToString());
            pnlQuickAdd.Location = new Point(10, 172);
            pnlQuickAdd.Visible = true;

            txtQuickAddName.TabIndex = 0;

            btnQuickAdd.Visible = false;

            SetMaterialDisplayRowsVisible(false);
            txtMaterialName_Sample.Text = "";
            txtMaterialCode_Sample.Text = "";



            lueVersion.Visible = false;
            lueMaterial.EditValue = 0;

            lblXrefNote.Visible = false;
            lblProductVersion.Visible = false;
            lblVersionNote.Visible = false;

        }                                                                   //4/12/22
        private void HideQuickAddPanel()
        {
            txtQuickAddCode.Text = String.Empty;
            txtQuickAddName.Text = String.Empty;
            pnlQuickAdd.Visible = false;
            pnlQuickAdd.Location = new Point(14, 260);

            //cursor change 08/26/2026 - only bring the read-only Material Code/Name rows back when a material is
            //                          actually selected. Sample_Load and the save refresh call this just to make sure
            //                          the Quick Add panel is closed, and showing the rows unconditionally re-displayed
            //                          two empty boxes on a new sample right after DataBindSelectedSample hid them.
            Int32 selectedMaterialID = 0;
            if (lueMaterial.EditValue != null)
            {
                Int32.TryParse(lueMaterial.EditValue.ToString(), out selectedMaterialID);
            }

            //cursor change 08/26/2026 SetMaterialDisplayRowsVisible(true);
            SetMaterialDisplayRowsVisible(selectedMaterialID > 0);

            btnQuickAdd.Visible = false;
            lblProductVersion.Visible = false;
            lueVersion.Visible = false;
            if (rbProducts.Checked == true)
            {
                btnQuickAdd.Visible = true;
            }



        }                                                                   //4/13/22
        private void btnQuickAdd_Click(object sender, EventArgs e)
        {
            txtQuickAddCode.Text = "";
            txtQuickAddName.Text = "";
            txtQuickAddName.Text = "";
            ShowQuickAddPanel();
            txtQuickAddCode.Focus();
        }
        private void txtQuickAddCode_Validated(object sender, EventArgs e)
        {
            txtQuickAddName.Text = GetCFCProductCodeDescription((Int32)lueOwnerLocation.EditValue, txtQuickAddCode.Text.Trim());
        }
        private void txtQuickAddCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txtQuickAddName.Text = GetCFCProductCodeDescription((Int32)lueOwnerLocation.EditValue, txtQuickAddCode.Text.Trim());
        }
        public static String GetCFCProductCodeDescription(Int32 ownerlocID, String prodCode)
        {
            String rtn = "";

            //using aimownerlocationID, check plant xref to get cfcplantID
            CFCPlantRefAIMOwnerLocationInfo cfcPlant = new CFCPlantRefAIMOwnerLocationInfo();
            cfcPlant = AIMManager.GetCFCPlantAimOwnerLocationCrossReference(ownerlocID);
            if (cfcPlant != null)
            {
                //using cfcplantID and entered product-code, check cfcproduct table for productID, if it exists.
                List<CFCProductInfo> cfcProduct = new List<CFCProductInfo>();
                cfcProduct = AIMManager.GetCFCProduct(cfcPlant.CFCPlantID, prodCode);
                if (cfcProduct != null)
                {
                    if (cfcProduct.Count > 0)
                    {
                        rtn = cfcProduct[0].CFCProductName;
                    }


                }
            }
            return rtn;
        }
        private void lueVersion_EditValueChanged(object sender, EventArgs e)
        {
            //4/13/22
            if ((Int32)lueVersion.EditValue > 0)
            {
                lblVersionNote.Visible = false;
            }

        }
        private void checkSampleCrossRef()
        {

            //------------------------------------------------------
            //Check for cross-reference; if none, show xref-note
            if (rbProducts.Checked == true)
            {
                _prodXrefExists = false;                                        //11/18/24 cm

                CFCProductRefAIMMaterialInfo xref = new CFCProductRefAIMMaterialInfo();
                xref = AIMManager.GetCFCProductRefAIMMaterialAimMaterialID((Int32)lueMaterial.EditValue);
                lblXrefNote.Visible = false;
                if (xref == null)
                {
                    if ((Int32)lueMaterial.EditValue > 0)
                    {
                        if (pnlQuickAdd.Visible == false)
                        {
                            //lblXrefNote.Visible = true;                       //11/14/24 - begin
                            if (txtMaterialName_Sample.Text == "")
                            {
                                lblXrefNote.Visible = false;
                            }
                            else
                            {
                                //lblXrefNote.Visible = true;                   
                                if (_hideFormulationXrefMessage == true)
                                {
                                    lblXrefNote.Visible = false;
                                }
                                else
                                {
                                    lblXrefNote.Visible = true;
                                }
                            }                                                   //11/14/24 - end
                        }
                    }
                }

                //11/18/24
                else
                {
                    _prodXrefExists = true;
                }

            }
            else
            {

                _ingrXrefExists = false;                                        //11/18/24 cm

                CFCIngredientRefAIMMaterialInfo xref = new CFCIngredientRefAIMMaterialInfo();
                xref = AIMManager.GetCFCIngredientRefAIMMaterialAimMaterialID((Int32)lueMaterial.EditValue);
                lblXrefNote.Visible = false;
                if (xref == null)
                {
                    if ((Int32)lueMaterial.EditValue > 0)
                    {
                        if (pnlQuickAdd.Visible == false)
                        {
                            //lblXrefNote.Visible = true;                       //11/14/24 - begin
                            if (txtMaterialName_Sample.Text == "")
                            {
                                lblXrefNote.Visible = false;
                            }
                            else
                            {
                                //lblXrefNote.Visible = true;
                                if (_hideFormulationXrefMessage == true)
                                {
                                    lblXrefNote.Visible = false;
                                }
                                else
                                {
                                    lblXrefNote.Visible = true;
                                }
                            }                                                   //11/14/24 - end

                        }
                    }
                }

                //11/18/24
                else
                {
                    _ingrXrefExists = true;
                }
            }
            //------------------------------------------------------
        }                                                                //4/13/22


        private void CheckLabelSize()
        {
            SystemSettingsInfo systemSettings = new SystemSettingsInfo();
            systemSettings = AIMManager.GetSystemSettings();
            if (systemSettings != null)
            {
                _labelSize = systemSettings.LabelSize;
            }
        }                                                                   //4/19/22        
        private void SaveFurtherAnalysisSample()
        {
            ErrorInfo furtherAnalysis = new ErrorInfo();

            //8/24/22
            List<SampleFurtherAnalysisInfo> sampleFurtherAnalysis = new List<SampleFurtherAnalysisInfo>();

            try
            {
                //8/1/22
                if ((Int32)lueSampleCode.EditValue == 0)
                {
                    MessageBox.Show("Please select a sample.", "Warning...");
                    lueSampleCode.ShowPopup();
                    return;
                }
                else if ((Int32)lueFurtherAnalysis.EditValue == 0)
                {
                    MessageBox.Show("Please select a Further Analysis datasource.", "Warning...");
                    lueFurtherAnalysis.ShowPopup();
                    return;
                }


                if ((Int32)lueSampleCode.EditValue > 0 && (Int32)lueFurtherAnalysis.EditValue > 0)
                {
                    //8/24/22 - check FurtherAnalysis table to see if sample is already in the queue to be processed.
                    sampleFurtherAnalysis = AIMManager.GetSampleFurtherAnalysis(_selectedSampleNumber);
                    if (sampleFurtherAnalysis.Count > 0)
                    {
                        MessageBox.Show("Sample is already in the queue to be processed.", "Warning...");
                        return;
                    }


                    //8/1/22 - added furtherAnalysisID (aka datasourcetypeID)
                    furtherAnalysis = AIMManager.SaveSampleFurtherAnalysis(0, (Int32)lueSampleCode.EditValue, (Int32)lueFurtherAnalysis.EditValue, userGlobal.CFCLoginUser.CFCUserID); ;
                    if (furtherAnalysis.ErrorNumber > 0)
                    {
                        //8/24/22
                        MessageBox.Show("Sample will be re-run.", "Message...", MessageBoxButtons.OK);

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //6/29/22        
        private void btnRerunFurtherAnalysis_Click(object sender, EventArgs e)
        {
            SaveFurtherAnalysisSample();
        }

        //6/29/22
        private void lueFurtherAnalysis_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void btnViewFurtherAnalysis_Click(object sender, EventArgs e)
        {
            //Opening form this way so it is not automatically set to maximized (in open_form)
            //AIMGlobal.open_form(sampleFurtherAnalysisView = new SampleFurtherAnalysisView());

            AIMGlobal.sampleCodeFurtherAnalysis = txtSampleCode.Text;                              //2/23/24


            SampleFurtherAnalysisView sampleFurtherAnalysisView = new SampleFurtherAnalysisView();
            sampleFurtherAnalysisView.Show();
        }

        //7/11/23        
        private void lueTestType_Click(object sender, EventArgs e)
        {
            lueTestTypeGroupCode.SelectAll();
        }

        private void lueTestType_Enter(object sender, EventArgs e)
        {
            lueTestTypeGroupCode.SelectAll();
        }

        //10/19/23
        private void btnExcludeTestTypes_Click(object sender, EventArgs e)
        {
            List<SampleTestTypeResultInfo> type = new List<SampleTestTypeResultInfo>();
            type = (List<SampleTestTypeResultInfo>)grdTestType.DataSource;
            if (type != null)
            {
                foreach (var item in type)
                {
                    //10/19/23
                    //AIMManager.SaveTestTypeResults(item.SampleTestTypeResultID, false, userGlobal.CFCLoginUser.CFCUserID);
                    AIMManager.SaveTestTypeResults(item.SampleTestTypeResultID, false, userGlobal.CFCLoginUser.CFCUserID);
                }

                //grdTestTypeView.RefreshData();
                DataBindTestType();
            }
        }


        //1/19/24
        private void hyperlinkAIMMaterialRefAnalyzerMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.PNEDataSourceTypeID = (Int32)DataSourceTypeEnum.AdisseoAPI;
                AIMGlobal.open_form(analyzerCrossReference = new AnalyzerCrossReference());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        //10/2/24 cm
        private void lueGroundUnGround_Click(object sender, EventArgs e)
        {
            lueGroundUnGround.SelectAll();
        }

        //10/2/24 cm
        private void lueGroundUnGround_Enter(object sender, EventArgs e)
        {
            lueGroundUnGround.SelectAll();
        }



        private void btnPrintPreview_Click_OLD(object sender, EventArgs e)
        {
            //SampleLabelReport labelReport = new SampleLabelReport();
            //SampleLabelReport sampleLabel = new SampleLabelReport();

            ////4/14/22
            //SampleLabelReportOneInch sampleLabelOneInch = new SampleLabelReportOneInch();

            //Int32 sampleNumber = 0;
            //try
            //{
            //    if ((Int32)lueSampleCode.EditValue == 0)
            //    {
            //        MessageBox.Show("Please select a sample.", "Warning...");
            //        lueSampleCode.ShowPopup();
            //        return;
            //    }
            //    Int32.TryParse(lueSampleCode.EditValue.ToString(), out sampleNumber);

            //    //4/14/22 - changed to check systemSettings to determine which label report to use
            //    //sampleLabel.DataSource = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst);
            //    //sampleLabel.PrinterName = PrinterSetup.GetPrinter(PrinterTypeEnum.Label, userGlobal.CFCLoginUser.CFCUserID);
            //    //ReportPrintTool tool = new ReportPrintTool(sampleLabel);

            //    ////////SystemSettingsInfo systemSettings = new SystemSettingsInfo();
            //    ////////systemSettings = AIMManager.GetSystemSettings();
            //    ////////if (systemSettings != null)
            //    ////////{
            //    ////////    if (systemSettings.LabelSize == 1)

            //    if (_labelSize == 1)
            //    {
            //        //5/5/23
            //        if (rbProducts.Checked == true)
            //        {
            //            //3/18/25
            //            //sampleLabelOneInch.DataSource = AIMManager.GetOneSampleProdLabel(sampleNumber, _showMaterialNameFirst);
            //            if (CFCAIMCore.Global.aimDatabase.Name == "Elmira")
            //            {
            //                sampleLabelOneInch.DataSource = AIMManager.GetOneSampleProdLabel(sampleNumber, _showMaterialNameFirst, true);
            //            }
            //            else
            //            {
            //                sampleLabelOneInch.DataSource = AIMManager.GetOneSampleProdLabel(sampleNumber, _showMaterialNameFirst, false);
            //            }
            //        }
            //        else
            //        {
            //            //3/18/25
            //            //sampleLabelOneInch.DataSource = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst);
            //            if (CFCAIMCore.Global.aimDatabase.Name == "Elmira")
            //            {
            //                sampleLabelOneInch.DataSource = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst, true);
            //            }
            //            else
            //            {
            //                sampleLabelOneInch.DataSource = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst, false);
            //            }
            //        }

            //        //5/5/23 - commented and moved above
            //        //sampleLabelOneInch.DataSource = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst);

            //        //6/9/23 - In GetPrinter below, a check is made for 'redirected' in printer-name. If exists, it replaces number after redirected with current number
            //        sampleLabelOneInch.PrinterName = PrinterSetup.GetPrinter(PrinterTypeEnum.Label, userGlobal.CFCLoginUser.CFCUserID);

            //        ReportPrintTool tool = new ReportPrintTool(sampleLabelOneInch);
            //        this.Hide();
            //        tool.ShowPreviewDialog();
            //        this.Show();
            //    }
            //    else
            //    {
            //        //5/5/23
            //        if (rbProducts.Checked == true)
            //        {
            //            //3/18/25
            //            //sampleLabel.DataSource = AIMManager.GetOneSampleProdLabel(sampleNumber, _showMaterialNameFirst);
            //            if (CFCAIMCore.Global.aimDatabase.Name == "Elmira")
            //            {
            //                sampleLabel.DataSource = AIMManager.GetOneSampleProdLabel(sampleNumber, _showMaterialNameFirst, true);
            //            }
            //            else
            //            {
            //                sampleLabel.DataSource = AIMManager.GetOneSampleProdLabel(sampleNumber, _showMaterialNameFirst, false);
            //            }
            //        }
            //        else
            //        {
            //            //3/18/25
            //            //sampleLabel.DataSource = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst);
            //            if (CFCAIMCore.Global.aimDatabase.Name == "Elmira")
            //            {
            //                sampleLabel.DataSource = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst, true);
            //            }
            //            else
            //            {            
            //                sampleLabel.DataSource = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst, false);
            //            }

            //        }

            //        //5/5/23 - commented and moved above
            //        //sampleLabel.DataSource = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst);

            //        //6/9/23 - In GetPrinter below, a check is made for 'redirected' in printer-name. If exists, it replaces number after redirected with current number
            //        sampleLabel.PrinterName = PrinterSetup.GetPrinter(PrinterTypeEnum.Label, userGlobal.CFCLoginUser.CFCUserID);

            //        ReportPrintTool tool = new ReportPrintTool(sampleLabel);
            //        this.Hide();
            //        tool.ShowPreviewDialog();
            //        this.Show();
            //    }
            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void PrintLabel_OLD()
        {
            SampleLabelReport sampleLabel = new SampleLabelReport();

            ////4/14/22
            //SampleLabelReportOneInch sampleLabelOneInch = new SampleLabelReportOneInch();

            //Int32 sampleNumber = 0;

            //try
            //{
            //    if ((Int32)lueSampleCode.EditValue == 0)
            //    {
            //        MessageBox.Show("Please select a sample.", "Warning...");
            //        lueSampleCode.ShowPopup();

            //        //5/2/23
            //        return;
            //    }

            //    //5/2/23
            //    //else
            //    Int32.TryParse(lueSampleCode.EditValue.ToString(), out sampleNumber);
            //    List<SampleLabelInfo> label = new List<SampleLabelInfo>();


            //    if (rbProducts.Checked == true)
            //    {
            //        //3/18/25
            //        //label = AIMManager.GetOneSampleProdLabel(sampleNumber, _showMaterialNameFirst);
            //        if (CFCAIMCore.Global.aimDatabase.Name == "Elmira")
            //        {
            //            label = AIMManager.GetOneSampleProdLabel(sampleNumber, _showMaterialNameFirst, true);
            //        }
            //        else
            //        {
            //            label = AIMManager.GetOneSampleProdLabel(sampleNumber, _showMaterialNameFirst, false);
            //        }
            //    }

            //    else
            //    {
            //        //5/2/23 - commented and moved above "if"
            //        //Int32.TryParse(lueSampleCode.EditValue.ToString(), out sampleNumber);                    
            //        //6/30/21
            //        //sampleLabel.DataSource = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst);
            //        //5/2/23 - commented and moved above "if"
            //        //List<SampleLabelInfo> label = new List<SampleLabelInfo>();

            //        //3/18/25
            //        //label = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst);
            //        if (CFCAIMCore.Global.aimDatabase.Name == "Elmira")
            //        {
            //            label = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst, true);
            //        }
            //        else
            //        {
            //            label = AIMManager.GetOneSampleLabel(sampleNumber, _showMaterialNameFirst, false);
            //        }
            //    }

            //    if (label != null)
            //    {
            //        AIMGlobal.line1Bold = label[0].Line1Bold;
            //        AIMGlobal.line2Bold = label[0].Line2Bold;
            //        AIMGlobal.line3Bold = label[0].Line3Bold;
            //        AIMGlobal.line4Bold = label[0].Line4Bold;
            //        AIMGlobal.line5Bold = label[0].Line5Bold;
            //    }

            //    sampleLabel.DataSource = label;                 //default 2-inch label (5-lines)

            //    //4/14/22
            //    sampleLabelOneInch.DataSource = label;          //1-inch label (3 lines) - added for Butterball

            //    //4/14/22 - check systemSettings to determine which label report to use
            //    //ReportPrintTool tool = new ReportPrintTool(sampleLabel);
            //    //////SystemSettingsInfo systemSettings = new SystemSettingsInfo();
            //    //////systemSettings = AIMManager.GetSystemSettings();
            //    //////if (systemSettings != null)
            //    //////{
            //    //////    if (systemSettings.LabelSize == 1)

            //    if (_labelSize == 1)

            //    {
            //        //6/9/23 - In GetPrinter below, a check is made for 'redirected' in printer-name. If exists, it replaces number after redirected with current number
            //        sampleLabelOneInch.PrinterName = PrinterSetup.GetPrinter(PrinterTypeEnum.Label, userGlobal.CFCLoginUser.CFCUserID);
            //        if (sampleLabelOneInch.PrinterName != "")
            //        {
            //            ReportPrintTool tool = new ReportPrintTool(sampleLabelOneInch);
            //            tool.PrinterSettings.Copies = (short)_copies;  //ReportPrintTool copies is defined as short, so had to convert _copies to short; otherwise error pops
            //            tool.Print();
            //        }
            //    }
            //    else
            //    {
            //        //6/9/23 - In GetPrinter below, a check is made for 'redirected' in printer-name. If exists, it replaces number after redirected with current number
            //        sampleLabel.PrinterName = PrinterSetup.GetPrinter(PrinterTypeEnum.Label, userGlobal.CFCLoginUser.CFCUserID);
            //        if (sampleLabel.PrinterName != "")
            //        {
            //            ReportPrintTool tool = new ReportPrintTool(sampleLabel);
            //            tool.PrinterSettings.Copies = (short)_copies;  //ReportPrintTool copies is defined as short, so had to convert _copies to short; otherwise error pops
            //            //tool.ShowPreview();
            //            tool.Print();
            //        }
            //    }

            //    //--------------------------------------------

            //    //////To Test - uncomment lines below and commented all printer lines above. Be sure to re-do after testing
            //    ////////SystemSettingsInfo systemSettings = new SystemSettingsInfo();
            //    ////////systemSettings = AIMManager.GetSystemSettings();
            //    ////////if (systemSettings != null)
            //    ////////{
            //    ////////    if (systemSettings.LabelSize == 1)
            //    ////////    {
            //    ////////        ReportPrintTool tool = new ReportPrintTool(sampleLabelOneInch);
            //    ////////        tool.PrinterSettings.Copies = (short)_copies;  //ReportPrintTool copies is defined as short, so had to convert _copies to short; otherwise error pops
            //    ////////        tool.ShowPreview();
            //    ////////    }
            //    ////////    else
            //    ////////    {
            //    ////////        ReportPrintTool tool = new ReportPrintTool(sampleLabel);
            //    ////////        tool.PrinterSettings.Copies = (short)_copies;  //ReportPrintTool copies is defined as short, so had to convert _copies to short; otherwise error pops
            //    ////////        tool.ShowPreview();
            //    ////////    }
            //    ////////}

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            tpProdSpec sample = new tpProdSpec();


            sample.Show();
        }

        private void grdMassEntryView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)               //Task1061 DB251031
        {
            String columnText = String.Empty;
            if (e.Column == colAnalyzerNutrientCode_Mass)
            {
                columnText = (String)(grdMassEntryView.GetRowCellValue(e.RowHandle, colAnalyzerNutrientCode_Mass));
                if (columnText == null)
                {
                    e.Appearance.BackColor = Color.Red;
                    _IsSaveMassEntry = false;
                }
            }

            if (e.Column.Name.ToString() == "colFlagInsideResultsMassEntry")                                                 //Task1135 DB260418
            {
                Int32 flagResult = (Int32)grdMassEntryView.GetRowCellValue(e.RowHandle, colResultValueFlagMassEntry);

                // Choose image based on condition
                Image img = null;
                if (flagResult == 1)
                    img = Properties.Resources.Flags3_3;
                else if (flagResult == 2)
                    img = Properties.Resources.Flags3_2;

                if (img != null)
                {
                    e.DefaultDraw(); // draw normal cell
                    e.Graphics.DrawImage(img, e.Bounds.X + 4, e.Bounds.Y + 4, 16, 16);
                    e.Handled = true;   // prevent DevExpress from drawing over it
                }
            }
        }

        private void txtDataSource_MassEntry_TextChanged(object sender, EventArgs e)                //Task1061 DB251031
        {
            _datasourcetypeID = (Int32)lueTemplateName.GetColumnValue("DataSourceTypeID");
            DataBindMassEntryNutrient();
        }


        private void clearSampleScreenControl()                       //Task1087 DB251205 Start 
        {
            SetFilterTabStops(false);

            List<TabOrderInfo> tabOrder = new List<TabOrderInfo>();
            List<TabOrderGroupDetailInfo> tabOrderGroup = new List<TabOrderGroupDetailInfo>();

            //--------------------------------------------------------------------

            tabOrderGroup = AIMManager.GetTabOrderGroupDetail(userGlobal.CFCLoginUser.TabOrderGroupID);

            if (tabOrderGroup.Count > 0)
            {
                for (int i = 0; i < tabOrderGroup.Count; i++)
                {
                    try
                    {
                        if (tabOrderGroup[i].TabOrder > 0)
                        {
                            Control[] control = Controls.Find(tabOrderGroup[i].ControlName, true);

                            if (control[0] is LookUpEdit lue)
                            {
                                LookUpEdit lookUpEditControl = (LookUpEdit)control[0];
                                lookUpEditControl.EditValue = 0;
                            }

                            if (control[0] is TextBox txt)
                            {
                                TextBox textBoxControl = (TextBox)control[0];
                                textBoxControl.Text = String.Empty;
                            }

                            if (control[0] is DateEdit de)
                            {
                                DateEdit dateEditControl = (DateEdit)control[0];
                                dateEditControl.EditValue = 0;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(tabOrder[i].ControlName + " could not be found in the form. Contact IT.", "Warning...");
                    }
                }
            }
            else
            {
                tabOrder = AIMManager.GetTabOrder(1, userGlobal.CFCLoginUser.CFCUserID);
                for (int i = 0; i < tabOrder.Count; i++)
                {
                    try
                    {
                        if (tabOrder[i].TabOrder > 0)
                        {
                            Control[] control = Controls.Find(tabOrder[i].ControlName, true);

                            if (control[0] is LookUpEdit lue)
                            {
                                LookUpEdit lookUpEditControl = (LookUpEdit)control[0];
                                lookUpEditControl.EditValue = 0;
                            }

                            if (control[0] is TextBox txt)
                            {
                                TextBox textBoxControl = (TextBox)control[0];
                                textBoxControl.Text = String.Empty;
                            }

                            if (control[0] is DateEdit de)
                            {
                                DateEdit dateEditControl = (DateEdit)control[0];
                                dateEditControl.EditValue = 0;
                            }
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show(tabOrder[i].ControlName + " could not be found in the form. Contact IT.", "Warning...");
                    }
                }
            }
        }                                                           //Task1087 DB251205 End


        private void btnAddNutrientMassEntry_Click(object sender, EventArgs e)                                                          //Task1061 DB251031
        {
            if ((Int32)lueMassEntryNutrient.EditValue > 0)
            {
                String analyzerNutrientNewValue = txtNutrientResultNewMassEntry.Text;
                Boolean isNumber = Int32.TryParse(analyzerNutrientNewValue, out Int32 nutrientValue);
                if (analyzerNutrientNewValue.Trim() == "")
                {
                    isNumber = true;
                    nutrientValue = 0;
                }

                if (isNumber == true)
                {
                    bool isNutrientExists = false;
                    Int32 analyzerNutrientIDNew = (Int32)lueMassEntryNutrient.GetColumnValue("AnalyzerNutrientID");
                    String analyzerNutrientCodeNew = (String)lueMassEntryNutrient.GetColumnValue("AnalyzerNutrientCode");

                    for (int i = 0; i < grdMassEntryView.RowCount; i++)
                    {
                        Int32 analyzerNutrientID = (Int32)grdMassEntryView.GetRowCellValue(i, grdMassEntryView.Columns["AnalyzerNutrientID"]);
                        if (analyzerNutrientID == analyzerNutrientIDNew)
                        {
                            isNutrientExists = true;
                            break;
                        }
                    }

                    if (isNutrientExists == false)
                    {
                        List<SampleResultTemplateGridInfo> templateGrid = (List<SampleResultTemplateGridInfo>)grdMassEntry.DataSource;
                        SampleResultTemplateGridInfo template = new SampleResultTemplateGridInfo();

                        AnalyzerNutrientInfo analyzerNutrient = AIMManager.GetAnalyzerNutrientUnits(analyzerNutrientIDNew, _datasourcetypeID);

                        if (analyzerNutrient != null)
                        {
                            template.SampleID = (Int32)lueSampleCode.EditValue;
                            template.AnalyzerNutrientCode = analyzerNutrientCodeNew;
                            template.UnitType = analyzerNutrient.UnitType;
                            template.ResultValueString = nutrientValue.ToString();
                            template.UserName = userGlobal.CFCLoginUser.UserName;
                            template.UpdateStamp = DateTime.Now;
                            template.AIMNutrientName = analyzerNutrient.AnalyzerNutrientName;
                            template.AnalyzerNutrientID = analyzerNutrientIDNew;
                            template.CurrentValue = 0;

                            templateGrid.Add(template);

                            grdMassEntry.DataSource = templateGrid;
                            grdMassEntryView.RefreshData();

                            txtNutrientResultNewMassEntry.Text = "";
                            lueMassEntryNutrient.EditValue = 0;

                        }
                        else
                        {
                            MessageBox.Show("Nutrient " + analyzerNutrientCodeNew + "Not found. Please select valid nutrient.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Nutrient " + analyzerNutrientCodeNew + " already exists in table. Please select unique nutrient.");
                    }
                }
                else
                {
                    MessageBox.Show("Nutrient value must be numeric, please enter valid value.");
                    txtNutrientResultNewMassEntry.Focus();
                }
            }
            else
            {
                MessageBox.Show("Analyzer Nutrient not selected.");
                lueMassEntryNutrient.Focus();
            }
        }

        private void grdMassEntryView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)   //Task1135 DB260418 todo delete maybe
        {
            //if (e.Column == colImage)
            //{
            //    //get value in IsActive column for this row
            //    if (((SampleResultTemplateGridInfo)(e.Row)).IsActive)
            //    {
            //        e.Value = Properties.Resources.delete;
            //    }
            //    else
            //    {
            //        e.Value = Properties.Resources.Undo_icon;
            //    }
            //}
        }

        private void grdMassEntryView_RowCellClick(object sender, RowCellClickEventArgs e)      //Task1135 DB260418
        {
            Int32 messageCount = 0;
            Int32 ID = 0;

            if (e.Column.Name.ToString() == "colFlagOutsideResultsMassEntry")
            {
                if (grdMassEntryView.GetFocusedRowCellValue(colMessageOutsideResultMassEntry) != null)
                {
                    Int32.TryParse(grdMassEntryView.GetFocusedRowCellValue(colMessageOutsideResultMassEntry).ToString(), out messageCount);
                    if (messageCount > 0)
                    {
                        Int32.TryParse(grdMassEntryView.GetFocusedRowCellValue(colAnalyzerNutrientID_Mass).ToString(), out ID);
                        CFCAIMCore.Global.sampleID = _selectedSampleNumber;

                        AIMGlobal.analyzerNutrientID = ID;
                        AIMGlobal.sampleResultPredictID = 0;
                        MessageDetailOutside msgForm = new MessageDetailOutside();

                        msgForm.ShowDialog();
                    }
                }
            }
            else if (e.Column.Name.ToString() == "colFlagInsideResultsMassEntry")
            {

                if (grdMassEntryView.GetFocusedRowCellValue(colMessageInsideResultMassEntry) != null)
                {

                    Int32 flagResult = (Int32)grdMassEntryView.GetRowCellValue(e.RowHandle, colResultValueFlagMassEntry);       
                    if (flagResult > 0)                                                                                                                      
                    {
                        Int32.TryParse(grdMassEntryView.GetFocusedRowCellValue(colAnalyzerNutrientID_Mass).ToString(), out ID);
                        CFCAIMCore.Global.sampleID = _selectedSampleNumber;

                        AIMGlobal.analyzerNutrientID = ID;
                        AIMGlobal.sampleResultPredictID = 0;
                        AIMGlobal.sampleResultsIsActive = rbActiveResult.Checked;
                        MessageDetailInside msgForm = new MessageDetailInside();

                        msgForm.ShowDialog();

                    }
                }
            }
        }

        // task 1155 FG 8/5/26 - show/hide the read-only material code and name rows together
        private void SetMaterialDisplayRowsVisible(Boolean visible)
        {
            lblMaterialCode.Visible = visible;
            txtMaterialCode_Sample.Visible = visible;
            lblMaterial.Visible = visible;
            txtMaterialName_Sample.Visible = visible;
        }

        // task 1155 FG 8/5/26
        #region REQUIRED FIELD VALIDATION -----------------------------------------------------------------
        //Admin-defined rules (AIM maintenance, Groups > Required Field) make extra Sample fields required
        //for the sample's material and owner location. Empty ones get a red error icon and block the save.

        private Boolean ValidateSampleRequiredFields()
        {
            ClearSampleRequiredFieldErrors();

            // task 1155 FG 8/5/26 - no group assigned to this user means the feature is off for
            //them, the same way the Tab Order and Sample Screen Display groups behave
            Int32 sampleRequiredFieldGroupID = userGlobal.CFCLoginUser.SampleRequiredFieldGroupID;
            if (sampleRequiredFieldGroupID == 0)
            {
                return true;
            }

            if (lueMaterial.EditValue == null || lueOwnerLocation.EditValue == null)
            {
                return true;
            }

            Int32 aimMaterialID = 0;
            Int32.TryParse(lueMaterial.EditValue.ToString(), out aimMaterialID);
            Int32 aimOwnerLocationID = 0;
            Int32.TryParse(lueOwnerLocation.EditValue.ToString(), out aimOwnerLocationID);
            if (aimMaterialID == 0)
            {
                return true;
            }

            List<SampleRequiredFieldGroupDetailInfo> rules = AIMManager.GetSampleRequiredFieldRuleForSample(aimMaterialID, aimOwnerLocationID, sampleRequiredFieldGroupID);

            List<String> missingFields = new List<String>();
            Control firstMissingControl = null;

            foreach (SampleRequiredFieldGroupDetailInfo rule in rules)
            {
                Control requiredControl = GetSampleRequiredFieldControl(rule.RequiredControlID, rule.ControlName);
                if (requiredControl == null || requiredControl.Visible == false)
                {
                    continue;
                }

                //two rules can require the same field (e.g. an all-material rule plus a group rule) - flag it once
                if (_requiredFieldControls.Contains(requiredControl))
                {
                    continue;
                }

                if (IsSampleRequiredFieldEmpty(requiredControl))
                {
                    SetSampleRequiredFieldError(requiredControl, "This field is required");
                    missingFields.Add(rule.FieldDisplayName);
                    if (firstMissingControl == null)
                    {
                        firstMissingControl = requiredControl;
                    }
                }
            }

            if (missingFields.Count > 0)
            {
                MessageBox.Show("Please fill in the required field(s): " + String.Join(", ", missingFields) + ". Information is not saved", "Warning...");
                firstMissingControl.Focus();
                return false;
            }

            return true;
        }

        // task 1155 FG 8/5/26 - the group stores the label (lblBOL); LinkToControlID gets us the
        //input the user actually types in (txtBOL), the same hop SetAlternateName() makes
        private Control GetSampleRequiredFieldControl(Int32 requiredControlID, String controlName)
        {
            String inputControlName = controlName;

            if (requiredControlID > 0)
            {
                List<ControlInfo> controlInfo = AIMManager.GetControl(requiredControlID);
                if (controlInfo != null && controlInfo.Count > 0 && controlInfo[0].LinkToControlID != 0)
                {
                    List<ControlInfo> linkedControlInfo = AIMManager.GetControl(controlInfo[0].LinkToControlID);
                    if (linkedControlInfo != null && linkedControlInfo.Count > 0 && String.IsNullOrEmpty(linkedControlInfo[0].ControlName) == false)
                    {
                        inputControlName = linkedControlInfo[0].ControlName;
                    }
                }
            }

            if (String.IsNullOrEmpty(inputControlName))
            {
                return null;
            }

            Control[] matches = this.Controls.Find(inputControlName, true);
            if (matches.Length > 0)
            {
                return matches[0];
            }
            return null;
        }

        private Boolean IsSampleRequiredFieldEmpty(Control requiredControl)
        {
            LookUpEdit lookUpEdit = requiredControl as LookUpEdit;
            if (lookUpEdit != null)
            {
                if (lookUpEdit.EditValue == null)
                {
                    return true;
                }

                Int32 selectedValue = 0;
                if (Int32.TryParse(lookUpEdit.EditValue.ToString(), out selectedValue))
                {
                    return selectedValue == 0;      //0 = the "--Select--" row
                }
                return String.IsNullOrWhiteSpace(lookUpEdit.EditValue.ToString());
            }

            DateEdit dateEdit = requiredControl as DateEdit;
            if (dateEdit != null)
            {
                return dateEdit.EditValue == null;
            }

            BaseEdit baseEdit = requiredControl as BaseEdit;
            if (baseEdit != null)
            {
                return baseEdit.EditValue == null || String.IsNullOrWhiteSpace(baseEdit.EditValue.ToString());
            }

            return String.IsNullOrWhiteSpace(requiredControl.Text);
        }

        private void SetSampleRequiredFieldError(Control requiredControl, String message)
        {
            BaseEdit baseEdit = requiredControl as BaseEdit;
            if (baseEdit != null)
            {
                //DevExpress editors draw their own red error icon with a tooltip
                baseEdit.ErrorText = message;

                // task 1155 FG 8/5/26 - red fill so the empty field stands out
                baseEdit.Properties.Appearance.BackColor = Color.MistyRose;
                baseEdit.Properties.Appearance.Options.UseBackColor = true;
            }
            else
            {
                if (_requiredFieldErrorProvider == null)
                {
                    _requiredFieldErrorProvider = new ErrorProvider(this);
                    _requiredFieldErrorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;      // task 1155 FG 8/5/26
                }

                // task 1155 FG 8/5/26 - negative padding keeps the icon inside the box instead
                //of pushing it out over the Save button, the fields are only 7px clear on the right
                _requiredFieldErrorProvider.SetIconAlignment(requiredControl, ErrorIconAlignment.MiddleRight);
                _requiredFieldErrorProvider.SetIconPadding(requiredControl, -20);
                _requiredFieldErrorProvider.SetError(requiredControl, message);

                // task 1155 FG 8/5/26 - plain text boxes have no ErrorText, so colour them instead
                if (_requiredFieldOriginalBackColor.ContainsKey(requiredControl) == false)
                {
                    _requiredFieldOriginalBackColor.Add(requiredControl, requiredControl.BackColor);
                }
                requiredControl.BackColor = Color.MistyRose;
            }

            _requiredFieldControls.Add(requiredControl);
        }

        private void ClearSampleRequiredFieldErrors()
        {
            foreach (Control requiredControl in _requiredFieldControls)
            {
                BaseEdit baseEdit = requiredControl as BaseEdit;
                if (baseEdit != null)
                {
                    baseEdit.ErrorText = String.Empty;
                    baseEdit.Properties.Appearance.Options.UseBackColor = false;     // task 1155 FG 8/5/26
                }
                else
                {
                    if (_requiredFieldErrorProvider != null)
                    {
                        _requiredFieldErrorProvider.SetError(requiredControl, String.Empty);
                    }

                    // task 1155 FG 8/5/26 - put the box back to its normal colour
                    Color originalBackColor;
                    if (_requiredFieldOriginalBackColor.TryGetValue(requiredControl, out originalBackColor))
                    {
                        requiredControl.BackColor = originalBackColor;
                    }
                }
            }
            _requiredFieldControls.Clear();
            _requiredFieldOriginalBackColor.Clear();        // task 1155 FG 8/5/26
        }
        #endregion
    }

}

