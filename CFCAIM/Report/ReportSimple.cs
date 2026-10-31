using CFCAIMCore;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using CFCUser.Entity;
using CFCUser.Manager;
using Core.Library.DevExpress;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CFCAIMCore.Entity.Test;
using DevExpress.Office.Utils;

namespace CFCAIM.Report
{
    public partial class ReportSimple : Form
    {
        #region  local variables
        ReportAdvanced nreportView;
        List<AIMOwnerLocationInfo> _aimOwnerLocation = new List<AIMOwnerLocationInfo>();
        List<AIMOwnerInfo> _aimOwner = new List<AIMOwnerInfo>();
        List<AIMMaterialInfo> _aimMaterial = new List<AIMMaterialInfo>();
        List<AIMMaterialInfo> _aimMaterialProducts = new List<AIMMaterialInfo>();
        List<AIMSupplierInfo> _supplier = new List<AIMSupplierInfo>();
        List<AIMSupplierLocationInfo> _supplierLocation = new List<AIMSupplierLocationInfo>();

        //10/17/23 commented, not used
        //List<AIMNutrientInfo> _aimNutrient = new List<AIMNutrientInfo>();

        List<ReportAlternateNamesInfo> _names = new List<ReportAlternateNamesInfo>();
        List<SampleTestTypeInfo> _sampleTestType = new List<SampleTestTypeInfo>();                      //8/16/23
        List<CFCUserInfo> _cfcUsers = new List<CFCUserInfo>();                                          //9/1/23
        UserInfo _user = new UserInfo();                                                                //10/5/23


        Int32 _datasource1 = 0;
        Int32 _datasource2 = 0;
        Report.ReportHelpDryMatter reportDryMatterHelp;
        String _materialIDList = "";
        String _materialIDList2 = "";
        String _materialIDList3 = "";
        MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];

        //3/4/21 commented, not used - BUT MAY BE IN THE FUTURE
        //Int32 _nutrientID1 = 0;
        //Int32 _nutrientID2 = 0;

        ReportDesigner reportDesigner;
        CFCUser.Global userGlobal = new CFCUser.Global();
        Int32 _selectedViewTemplateID = 0;
        Int32 _selectedTemplateType = 0;

        //2/16/21
        Boolean _groupProducts = false;

        Int32 aimDatabaseID = 0;                                                //6/20/23
        String priorityValue = "";                                              //6/20/23
        ReportComments reportComments;                                          //9/6/23
        Boolean _noData = false;                                                //9/6/23


        Int32 _sampleTestTypeGroupID = 0;                                       //10/5/23
        Int32 _ownerLocationGroupID = 0;                                        //10/17/23
        Int32 _supplierGroupID = 0;                                             //10/17/23
        Int32 _supplierLocationGroupID = 0;                                     //10/17/23
        CFCUser.Global _userGlobal = new CFCUser.Global();                      //10/17/23

        Report.ReportHelp reportHelp;                                           //10/26/23
        Int32 currentRowNum = 0;
        Int32 panelWidth;
        Int32 panelHeight;
        Int32 gridWidth;                                                        //TASK951 6/4/24 
        Int32 gridHeight;                                                       //TASK951 6/4/24
        Int32 gridWidthExport;                                                  //TASK951 6/4/24 
        Int32 gridHeightExport;                                                 //TASK951 6/4/24



        System.IO.Stream _LayoutStream = new System.IO.MemoryStream();
        ReportPrintLayoutInfo _currentPrintLayout = null;   //1175FG 7/1/2026

        #endregion

        public ReportSimple()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026

            //6/7/22 - trying to figure out why date sort does not work for Butterball
            pivotGridControl1.CustomFieldSort += new DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventHandler(pivotGridControl1_CustomFieldSort);

            //task 1133 FG 7/21/2026 - keep the Name dropdowns' Visible/Enabled state in step with their Code dropdowns
            lueOwner.VisibleChanged += new EventHandler(lueOwner_VisibleEnabledChanged);
            lueOwner.EnabledChanged += new EventHandler(lueOwner_VisibleEnabledChanged);
            lueOwnerLocation.VisibleChanged += new EventHandler(lueOwnerLocation_VisibleEnabledChanged);
            lueOwnerLocation.EnabledChanged += new EventHandler(lueOwnerLocation_VisibleEnabledChanged);
            lueSupplier.VisibleChanged += new EventHandler(lueSupplier_VisibleEnabledChanged);
            lueSupplier.EnabledChanged += new EventHandler(lueSupplier_VisibleEnabledChanged);
            lueSupplierLocation.VisibleChanged += new EventHandler(lueSupplierLocation_VisibleEnabledChanged);
            lueSupplierLocation.EnabledChanged += new EventHandler(lueSupplierLocation_VisibleEnabledChanged);
        }
        private void reportView_Load(object sender, EventArgs e)
        {
            try
            {

                clear();

                SetUserGroups();                                                    //10/17/23

                //---------------------------------------------------------------
                dataBind();
                
                dataBindMaterial((Int32)MaterialTypeEnum.Ingredient);               //6/3/20
                dataBindSupplier();                                                 //6/3/20
                dataBindReports();
                populateMaterialCheckBox();

                //2/27/17 panelOutlier is currently not used on the Reports screen (only on Charts).
                //2/20/17 commented 2/22/17, not used currently
                //PopulateNutrientCheckBox();

                dataBindTemplate();                                                 //4/5/18




                if (AIMGlobal.templateReportID == (Int32)ReportEnum.UserDefinedSampleData ||
                    AIMGlobal.templateReportID == (Int32)ReportEnum.UserDefinedSamplePivotData)
                {

                    //5/22/19 - load default xml
                    //_LayoutStream = new System.IO.MemoryStream();

                    getTemplateGlobals();

                    //4/15/22
                    btnGenerateReport.Image = CFCAIM.Properties.Resources.next_32x321;
                }

                //4/15/22
                else
                {
                    btnGenerateReport.Image = CFCAIM.Properties.Resources.preview_32x32;
                }

                loadUserDefaults();                                          //Task1075 DB250715

                ckAll.Checked = true;                                               //1/18/21
                ckAllGroup.Checked = true;                                          //1/18/21

                //6/20/23 - get AIM Priority Used Value
                AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
                aimDatabaseID = (Int32)_mainMenu.lueDataMap.EditValue;
                aimDatabase = AIMManager.GetAIMDatabase(aimDatabaseID);
                List<AIMPriorityUsedValueInfo> priority = new List<AIMPriorityUsedValueInfo>();
                priority = AIMManager.GetAIMPriorityUsedValue(aimDatabase.AIMPriorityUsedValueID);
                if (priority.Count > 0)
                {
                    priorityValue = priority[0].Description;
                    if (aimDatabase.AIMPriorityUsedValueID == (Int32)PriorityUsedValue.DataSource)
                    {
                        DataSourceTypeInfo type = new DataSourceTypeInfo();
                        type = AIMManager.GetDataSourceType_DataSourceTypeID(aimDatabase.DataSourceTypeID);
                        if (type != null)
                        {
                            lblShowOnlyType.Text = type.Name;
                        }
                        else
                        {
                            lblShowOnlyType.Text = "";
                        }
                    }
                    else
                    {
                        lblShowOnlyType.Text = priorityValue;
                    }
                }
                else
                {
                    lblShowOnlyType.Text = "";
                }

                //10/5/23
                dataBindTestType();



                setToolTips();
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //5/21/19
        private void btnRefresh_Click(object sender, EventArgs e)
        {


            clear();
            dataBind();

            //6/5/20
            dataBindMaterial((Int32)MaterialTypeEnum.Ingredient);
            dataBindSupplier();

            dataBindReports();
            populateMaterialCheckBox();

            if (AIMGlobal.templateReportID == (Int32)ReportEnum.UserDefinedSampleData ||
                AIMGlobal.templateReportID == (Int32)ReportEnum.UserDefinedSamplePivotData)
            {
                return;
            }

            dataBindTemplate();
            dataBindTestType();                     //10/5/23

            AIMGlobal.SplashScreenHide();           //6/5/20
        }

        private void clear()
        {
            lueMaterial.EditValue = 0;
            deFromDate.Text = "";
            deToDate.Text = "";
            txtTicket.Text = "";
            lueOwner.EditValue = 0;
            lueOwnerLocation.EditValue = 0;
            lueSupplier.EditValue = 0;
            lueSupplierLocation.EditValue = 0;
            panelCriteria.Visible = false;
            lueFirstDataSource.EditValue = 0;
            lueSecondDataSource.EditValue = 0;
            lblAimMaterial.Visible = true;                      //1/15/24
            lblMaterialCode.Visible = true;
            lblMaterialName.Visible = true;                     //1/15/24
            lueMaterial.Visible = true;
            lueMaterialName.Visible = true;                     //1/15/24

            rbExtremeOutlierInclude.Checked = true;
            rbMildOutlierInclude.Checked = true;

            pnlMaterialType.Visible = false;

            //4/15/22
            pnlAnalyzerOrFormulation.Visible = false;


            pnlGrid.Visible = false;
            grdReport.Visible = false;
            grdExportLog.Visible = false;                       //TASK951 6/4/24
            pnlTemplate.Visible = false;
            pnlSaveResults.Visible = false;

            //5/21/19
            lblTemplateHeader.Visible = false;
            lblSaveGridResults.Visible = false;

            pnlPivot.Visible = false;
            grdReport.DataSource = null;
            grdExportLog.DataSource = null;                     //TASK951 6/4/24
            pivotGridControl1.DataSource = null;

            grdCostAnalysis.Visible = false;            //Task1150 DB250109
            grdCostAnalysis.DataSource = null;          //Task1150 DB250109

            clearMaterials();
            dataBindReports();
            listboxReports.Focus();

        }

        //4/12/18
        private void dataBindTemplate_TemplateType()
        {
            //3/4/21 - removed unnecessary assignment
            //List<AnalysisViewTemplateInfo> viewTemplate = new List<AnalysisViewTemplateInfo>();
            List<AnalysisViewTemplateInfo> viewTemplate;

            viewTemplate = AIMManager.GetAnalysisViewTemplate_TemplateType(_selectedTemplateType);
            viewTemplate.Insert
            (
                0,
                new AnalysisViewTemplateInfo
                {
                    AnalysisViewTemplateID = 0,
                    AnalysisViewTemplateName = "--Select--"
                }
            );
            lueTemplate.Properties.DataSource = viewTemplate;
            XtraEditors.LoadLookUpEdit(ref lueTemplate, "AnalysisViewTemplateID", "AnalysisViewTemplateName");
        }

        private void dataBindTemplate()
        {
            //3/4/21 - removed unnecessary assignment
            //List<AnalysisViewTemplateInfo> viewTemplate = new List<AnalysisViewTemplateInfo>();
            List<AnalysisViewTemplateInfo> viewTemplate;

            //viewTemplate = AIMManager.GetAllAnalysisViewTemplate(true, (Int32)EnumGridNumber.ReportSampleCustom);
            viewTemplate = AIMManager.GetAllAnalysisViewTemplate(true, (Int32)EnumGridNumber.UserDefinedSampleReportData, _selectedTemplateType);

            viewTemplate.Insert
            (
                0,
                new AnalysisViewTemplateInfo
                {
                    AnalysisViewTemplateID = 0,
                    AnalysisViewTemplateName = "--Select--"
                }
            );
            lueTemplate.Properties.DataSource = viewTemplate;
            XtraEditors.LoadLookUpEdit(ref lueTemplate, "AnalysisViewTemplateID", "AnalysisViewTemplateName");
        }

        private void populateMaterialCheckBox()
        {
            //3/4/21 - removed unnecessary assignment
            //List<AIMMaterialInfo> material = new List<AIMMaterialInfo>();
            List<AIMMaterialInfo> material;

            material = AIMManager.GetAIMMaterialOrderByMaterialName(true);
            checkedListBoxControl1.DataSource = material;
        }

        //3/4/21 commented, not used -- BUT MAY BE IN THE FUTURE
        //private void populateNutrientCheckBox()
        //{
        //    //3/4/21 - removed unnecessary assignment
        //    //List<AIMNutrientInfo> nutrient = new List<AIMNutrientInfo>();
        //    List<AIMNutrientInfo> nutrient;

        //    nutrient = AIMManager.GetAIMNutrient(true);
        //    checkedListBoxControl2.DataSource = nutrient;        
        //}

        //6/5/20
        private void dataBindProducts()
        {
            _aimMaterialProducts = AIMManager.GetAIMMaterial_Active_Products(true);
            _aimMaterialProducts.Insert
            (
                0,
                new AIMMaterialInfo
                {
                    AIMMaterialCode = "--All--",
                    AIMMaterialName = "--All--"
                }
            );
            lueMaterial.Properties.DataSource = _aimMaterialProducts;
            XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialCode", "AIMMaterialCode", "AIMMaterialName");
            lueMaterial.EditValue = "--All--";

        }

        private void dataBindMaterial(Int32 materialTypeID)
        {
            _aimMaterial = AIMManager.GetAIMMaterial(true);
            _aimMaterial.Insert
            (
                0,
                new AIMMaterialInfo
                {
                    AIMMaterialID = 0,
                    AIMMaterialCode = "--All--",
                    AIMMaterialName = "--All--"         //1/15/24
                }
            );
            lueMaterial.Properties.DataSource = _aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");

            //1/15/24
            lueMaterialName.Properties.DataSource = _aimMaterial;
            //XtraEditors.LoadLookUpEdit(ref lueMaterialName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");
            XtraEditors.LoadLookUpEdit(ref lueMaterialName, "AIMMaterialID", "AIMMaterialName");
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

        //Task1075 DB250715
        private void saveUserDefaults()
        {
            try
            {
                ReportUserDefaultSettingInfo defaults = new ReportUserDefaultSettingInfo();
                defaults.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;

                if (listboxReports.SelectedValue != null)
                    defaults.ReportType = Convert.ToInt32(listboxReports.SelectedValue);

                if (deFromDate.EditValue != null && deFromDate.EditValue != "")
                    defaults.FromDate = Convert.ToDateTime(deFromDate.EditValue);
                //else
                //    defaults.FromDate = null;

                if (deToDate.EditValue != null && deFromDate.EditValue != "")
                    defaults.ToDate = Convert.ToDateTime(deToDate.EditValue);
                //else
                //    defaults.ToDate = null;

                if (rbIngredients.Checked) defaults.MaterialType = 1;
                else if (rbProducts.Checked) defaults.MaterialType = 2;
                else if (rbAllIngredientsProducts.Checked) defaults.MaterialType = 3;

                if (rbExtremeOutlierInclude.Checked) defaults.ExtremeOutlier = 1;
                else if (rbExtremeOutlierExclude.Checked) defaults.ExtremeOutlier = 2;

                if (rbGroupMaterialsYes.Checked) defaults.MaterialGroup = 1;
                else if (rbGroupMaterialsNo.Checked) defaults.MaterialGroup = 2;

                if (rbMildOutlierInclude.Checked) defaults.MildOutlier = 1;
                else if (rbMildOutlierExclude.Checked) defaults.MildOutlier = 2;

                if (rbSortNutrientCode.Checked) defaults.SortOnNutrientCode = 1;
                else if (rbSortNutrientName.Checked) defaults.SortOnNutrientCode = 2;

                if (btnSampleWithResults.Checked) defaults.SampleResult = 1;
                else if (btnSampleWithoutResults.Checked) defaults.SampleResult = 2;
                else if (btnSampleWithBoth.Checked) defaults.SampleResult = 3;

                if (rbAnalyzer.Checked) defaults.AnalyzerOrFormulation = 1;
                else if (rbFormulation.Checked) defaults.AnalyzerOrFormulation = 2;

                if (rbQCNo.Checked) defaults.QualityControl = 1;
                else if (rbQCYes.Checked) defaults.QualityControl = 2;

                if (rbUseSampleDate.Checked) defaults.DateSampleOrCreated = 1;
                else if (rbUseCreatedDate.Checked) defaults.DateSampleOrCreated = 2;

                if (lueOwner.EditValue != null) { defaults.OwnerID = (Int32)lueOwner.EditValue; } else { defaults.OwnerID = 0; }
                if (lueOwnerLocation.EditValue != null) { defaults.OwnerLocationID = (Int32)lueOwnerLocation.EditValue; } else { defaults.OwnerLocationID = 0; }
                if (lueSupplier.EditValue != null) { defaults.SupplierID = (Int32)lueSupplier.EditValue; } else { defaults.SupplierID = 0; }
                if (lueSupplierLocation.EditValue != null) { defaults.SupplierLocationID = (Int32)lueSupplierLocation.EditValue; } else { defaults.SupplierLocationID = 0; }
                if (lueMaterial.EditValue != null) { defaults.MaterialID = (Int32)lueMaterial.EditValue; } else { defaults.MaterialID = 0; }
                if (lueMaterialName.EditValue != null) { defaults.MaterialNameID = (Int32)lueMaterialName.EditValue; } else { defaults.MaterialNameID = 0; }
                if (lueTestType.EditValue != null) { defaults.TestTypeID = (Int32)lueTestType.EditValue; } else { defaults.TestTypeID = 0; }

                defaults.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;

                defaults.TicketNumber = txtTicket.Text;
                if (lueTemplate.EditValue != null) { defaults.TemplateID = (Int32)lueTemplate.EditValue; } else { defaults.TemplateID = 0; }
                if (lueCFCUser.EditValue != null) { defaults.CFCUserFormID = (Int32)lueCFCUser.EditValue; } else { defaults.CFCUserFormID = 0; }
                defaults.OutOfRangeOnly = chkOutOfRange.Checked;
                defaults.ShowViolations = chkShowViolations.Checked;


                //Intelliscence got mad when not specified.
                CFCAIMCore.Entity.ErrorInfo result = AIMManager.SaveReportUserDefaultSetting(ref defaults);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Task1075 DB250715
        private void loadUserDefaults()
        {
            try
            {
                ReportUserDefaultSettingInfo reportUserDefaultSetting = new ReportUserDefaultSettingInfo();
                reportUserDefaultSetting = AIMManager.GetReportUserDefaultSetting(_userGlobal.CFCLoginUser.CFCUserID);

                if (reportUserDefaultSetting != null)
                {
                    if (reportUserDefaultSetting.ReportType > 0) { listboxReports.SelectedValue = reportUserDefaultSetting.ReportType; }

                    if (reportUserDefaultSetting.FromDate != null) { deFromDate.DateTime = reportUserDefaultSetting.FromDate; } else { deFromDate.EditValue = null; }
                    if (reportUserDefaultSetting.ToDate != null) { deToDate.DateTime = reportUserDefaultSetting.ToDate; } else { deToDate.EditValue = null; }



                    if (reportUserDefaultSetting.MaterialType == 1) { rbIngredients.Checked = true; }
                    else if (reportUserDefaultSetting.MaterialType == 2) { rbProducts.Checked = true; }
                    else if (reportUserDefaultSetting.MaterialType == 3) { rbAllIngredientsProducts.Checked = true; }


                    if (reportUserDefaultSetting.ExtremeOutlier == 1) { rbExtremeOutlierInclude.Checked = true; }
                    else if (reportUserDefaultSetting.ExtremeOutlier == 2) { rbExtremeOutlierExclude.Checked = true; }


                    if (reportUserDefaultSetting.MaterialGroup == 1) { rbGroupMaterialsYes.Checked = true; }
                    else if (reportUserDefaultSetting.MaterialGroup == 2) { rbGroupMaterialsNo.Checked = true; }


                    if (reportUserDefaultSetting.MildOutlier == 1) { rbMildOutlierInclude.Checked = true; }
                    else if (reportUserDefaultSetting.MildOutlier == 2) { rbMildOutlierExclude.Checked = true; }

                    if (reportUserDefaultSetting.SortOnNutrientCode == 1) { rbSortNutrientCode.Checked = true; }
                    else if (reportUserDefaultSetting.SortOnNutrientCode == 2) { rbSortNutrientName.Checked = true; }

                    if (reportUserDefaultSetting.SampleResult == 1) { btnSampleWithResults.Checked = true; }
                    else if (reportUserDefaultSetting.SampleResult == 2) { btnSampleWithoutResults.Checked = true; }
                    else if (reportUserDefaultSetting.SampleResult == 3) { btnSampleWithBoth.Checked = true; }


                    if (reportUserDefaultSetting.AnalyzerOrFormulation == 1) { rbAnalyzer.Checked = true; }
                    else if (reportUserDefaultSetting.AnalyzerOrFormulation == 2) { rbFormulation.Checked = true; }


                    if (reportUserDefaultSetting.QualityControl == 1) { rbQCYes.Checked = true; }
                    else if (reportUserDefaultSetting.QualityControl == 2) { rbQCNo.Checked = true; }


                    if (reportUserDefaultSetting.DateSampleOrCreated == 1) { rbUseSampleDate.Checked = true; }
                    else if (reportUserDefaultSetting.DateSampleOrCreated == 2) { rbUseCreatedDate.Checked = true; }


                    if (reportUserDefaultSetting.OwnerID > 0) { lueOwner.EditValue = reportUserDefaultSetting.OwnerID; } else { lueOwner.EditValue = 0; }
                    if (reportUserDefaultSetting.OwnerLocationID > 0) { lueOwnerLocation.EditValue = reportUserDefaultSetting.OwnerLocationID; } else { lueOwnerLocation.EditValue = 0; }
                    if (reportUserDefaultSetting.SupplierID > 0) { lueSupplier.EditValue = reportUserDefaultSetting.SupplierID; } else { lueSupplier.EditValue = 0; }
                    if (reportUserDefaultSetting.SupplierLocationID > 0) { lueSupplierLocation.EditValue = reportUserDefaultSetting.SupplierLocationID; } else { lueSupplierLocation.EditValue = 0; }
                    if (reportUserDefaultSetting.MaterialID > 0) { lueMaterial.EditValue = reportUserDefaultSetting.MaterialID; } else { lueMaterial.EditValue = 0; }
                    if (reportUserDefaultSetting.MaterialNameID > 0) { lueMaterialName.EditValue = reportUserDefaultSetting.MaterialNameID; } else { lueMaterialName.EditValue = 0; }
                    if (reportUserDefaultSetting.TestTypeID > 0) { lueTestType.EditValue = reportUserDefaultSetting.TestTypeID; } else { lueTestType.EditValue = 0; }

                    if (!string.IsNullOrEmpty(reportUserDefaultSetting.TicketNumber)) { txtTicket.Text = reportUserDefaultSetting.TicketNumber; } else { txtTicket.Text = string.Empty; }
                    if (reportUserDefaultSetting.TemplateID > 0) { lueTemplate.EditValue = reportUserDefaultSetting.TemplateID; } else { lueTemplate.EditValue = 0; }
                    if (reportUserDefaultSetting.CFCUserFormID > 0) { lueCFCUser.EditValue = reportUserDefaultSetting.CFCUserFormID; } else { lueCFCUser.EditValue = 0; }
                    chkOutOfRange.Checked = reportUserDefaultSetting.OutOfRangeOnly;
                    chkShowViolations.Checked = reportUserDefaultSetting.ShowViolations;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataBind()
        {
            //-----------------------------------------------
            //10/17/23 - added check of groupID (some users can see only certain owners/owner-locations, depending on group, which is set in User Listing)
            //_aimOwner = AIMManager.GetAIMOwner(true);                          
            _aimOwner = AIMManager.GetAIMOwner(_ownerLocationGroupID, true);
            _aimOwner.Insert
            (
                0,
                new AIMOwnerInfo
                {
                    AIMOwnerID = 0,
                    OwnerCode = "--All--",
                    OwnerName = "--All--"       //task 1133 FG 7/21/2026
                }
            );
            lueOwner.Properties.DataSource = _aimOwner;
            XtraEditors.LoadLookUpEdit(ref lueOwner, "AIMOwnerID", "OwnerCode");

            //task 1133 FG 7/21/2026 - bind the Owner Name dropdown to the same list as the Code dropdown (same pattern as Sample.cs)
            lueOwnerName.Properties.DataSource = _aimOwner;
            XtraEditors.LoadLookUpEdit(ref lueOwnerName, "AIMOwnerID", "OwnerName", "OwnerCode");

            if (_aimOwner.Count == 2)
            {
                //selecting value if there is only one
                lueOwner.EditValue = _aimOwner[1].AIMOwnerID;
            }
            else
            {
                lueOwner.EditValue = 0; // _aimOwner;   //11/14/24
            }

            //-----------------------------------------------
            //10/17/23 commented, not used
            //_aimOwnerLocation = AIMManager.GetAIMOwnerLocation(true);

            //-----------------------------------------------
            //10/17/23 commented, not used            
            //_aimNutrient = AIMManager.GetAIMNutrient(true);
            //_aimNutrient.Insert
            //(
            //	0,
            //	new AIMNutrientInfo
            //	{
            //		AIMNutrientID = 0,
            //		AIMNutrientCode = "--Select--",
            //		AIMNutrientName = "--Select--"
            //	}
            //);

            //-----------------------------------------------
            List<DataSourceAllTypesInfo> dataSource;
            dataSource = AIMManager.GetDataSourceAllTypes(true);
            dataSource.Insert
            (
                0,
                new DataSourceAllTypesInfo
                {
                    RowNumber = 0,
                    Name = "--Select--"
                }
            );
            lueFirstDataSource.Properties.DataSource = dataSource;
            XtraEditors.LoadLookUpEdit(ref lueFirstDataSource, "RowNumber", "Name", "IsUserDefined", "DataSourceTypeID");
            lueFirstDataSource.Properties.Columns["IsUserDefined"].Caption = "Type";
            lueFirstDataSource.Properties.Columns["DataSourceTypeID"].Visible = false;      //3/12/20

            lueSecondDataSource.Properties.DataSource = dataSource;
            XtraEditors.LoadLookUpEdit(ref lueSecondDataSource, "RowNumber", "Name", "IsUserDefined", "DataSourceTypeID");
            lueSecondDataSource.Properties.Columns["IsUserDefined"].Caption = "Type";
            lueSecondDataSource.Properties.Columns["DataSourceTypeID"].Visible = false;      //3/12/20


            //-----------------------------------------------                                /9/1/23            
            _cfcUsers = AIMManager.GetCFCUsersAllButAimSystem(true);
            _cfcUsers.Insert
                (
                0,
                new CFCUserInfo
                {
                    CFCUserID = 0,
                    UserName = "--All--",
                    FirstName = "--All--",      //task 1133 FG 7/21/2026 - CFCUserName is computed from FirstName + LastName
                    LastName = ""
                }
            );
            lueCFCUser.Properties.DataSource = _cfcUsers;
            XtraEditors.LoadLookUpEdit(ref lueCFCUser, "CFCUserID", "UserName");

            //task 1133 FG 7/21/2026 - bind the User Name dropdown (FirstName + LastName) to the same list as the login-name dropdown
            lueCFCUserName.Properties.DataSource = _cfcUsers;
            XtraEditors.LoadLookUpEdit(ref lueCFCUserName, "CFCUserID", "CFCUserName", "UserName");

            lueCFCUser.EditValue = userGlobal.CFCLoginUser.CFCUserID;

        }

        private void dataBindTestType()
        {
            //---------------------------------------------------------------------------   //8/16/23                                  
            _sampleTestType = AIMManager.GetSampleTestTypeGroup(_sampleTestTypeGroupID);
            _sampleTestType.Insert
            (
                0,
                new SampleTestTypeInfo
                {
                    SampleTestTypeID = 0,
                    SampleTestTypeName = "--All--"
                }
            );
            lueTestType.Properties.DataSource = _sampleTestType;
            XtraEditors.LoadLookUpEdit(ref lueTestType, "SampleTestTypeID", "SampleTestTypeName");
            lueTestType.Properties.Columns["SampleTestTypeName"].Caption = "Type Name";

        }

        private void dataBindSupplier()
        {

            if (lueMaterial.EditValue == null)
            {
                //10/17/23 - added check of groupID (some users can see only certain suppliers, depending on group, which is set in User Listing)
                //_source = AIMManager.GetSupplier(true);
                _supplier = AIMManager.GetSupplier(true, _supplierGroupID);
            }
            else if (rbIngredients.Checked == true)
            {
                //10/17/23 - added check of groupID (some users can see only certain suppliers, depending on group, which is set in User Listing)
                //_supplier = AIMManager.GetSupplierWithMaterials(true, (Int32)lueMaterial.EditValue);
                _supplier = AIMManager.GetSupplierWithMaterials(true, _supplierGroupID, (Int32)lueMaterial.EditValue);

            }
            if (_supplier[0].SupplierName != "--All--")
            {
                _supplier.Insert
                (
                    0,
                    new AIMSupplierInfo
                    {
                        AIMSupplierID = 0,
                        AIMSupplierCode = "--All--",
                        SupplierName = "--All--"
                    }
                );
            }

            lueSupplier.Properties.DataSource = _supplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplier, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
            lueSupplier.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

            //task 1133 FG 7/21/2026 - bind the Supplier Name dropdown to the same list as the Code dropdown (same pattern as Sample.cs)
            lueSupplierName.Properties.DataSource = _supplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplierName, "AIMSupplierID", "SupplierName", "AIMSupplierCode");
            lueSupplierName.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

        }

        private void dataBindReports()
        {
            //initializing report listbox

            //3/4/21 - removed unnecessary assignment
            //List<ReportInfo> reports = new List<ReportInfo>();
            List<ReportInfo> reports;

            //5/31/22
            //reports = AIMManager.GetAllReport();
            reports = AIMManager.GetAllReport(true, false);

            //10/25/23 commented
            //reports.Insert
            //(
            //	0,
            //	new ReportInfo
            //	{
            //		ReportID = 0,
            //		ReportName = "--Select--"
            //	}
            //);

            listboxReports.DataSource = reports;
            listboxReports.DisplayMember = "ReportName";
            listboxReports.ValueMember = "ReportID";

        }

        private void databindSupplierLocation()
        {
            Int32 supplierLocationID = 0;
            Int32 supplierID;
            Int32.TryParse(lueSupplier.EditValue.ToString(), out supplierID);

            //10/17/23 - added check of groupID (some users can see only certain suppliers/supplier-locations, depending on group, which is set in User Listing)
            //_supplierLocation = AIMManager.GetSupplierLocationForSupplier(supplierID);
            _supplierLocation = AIMManager.GetSupplierLocationForSupplier(supplierID, _supplierGroupID, _supplierLocationGroupID);

            _supplierLocation.Insert
            (
                0,
                new AIMSupplierLocationInfo
                {
                    AIMSupplierLocationID = 0,
                    AIMSupplierLocationCode = "--All--",
                    SupplierLocationName = "--All--"
                }
            );
            lueSupplierLocation.Properties.DataSource = _supplierLocation;
            XtraEditors.LoadLookUpEdit(ref lueSupplierLocation, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
            lueSupplierLocation.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

            //task 1133 FG 7/21/2026 - bind the Supplier Location Name dropdown to the same list as the Code dropdown (same pattern as Sample.cs)
            lueSupplierLocationName.Properties.DataSource = _supplierLocation;
            XtraEditors.LoadLookUpEdit(ref lueSupplierLocationName, "AIMSupplierLocationID", "SupplierLocationName", "AIMSupplierLocationCode");
            lueSupplierLocationName.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";


            if (_supplierLocation.Count == 2)
            {
                //selecting value if there is only one
                lueSupplierLocation.EditValue = _supplierLocation[1].AIMSupplierLocationID;
            }
            else
            {
                lueSupplierLocation.EditValue = 0; // supplierLocationID; //11/14/24
            }

        }

        private void dataBindOwnerLocation()
        {
            Int32 OwnerID = 0;
            if (lueOwner.EditValue != null)
            {
                Int32.TryParse(lueOwner.EditValue.ToString(), out OwnerID);
            }

            //10/17/23 - added check of groupID (some users can see only certain owners/owner-locations, depending on group, which is set in User Listing)
            //_aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(OwnerID);
            _aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(OwnerID, _ownerLocationGroupID);

            _aimOwnerLocation.Insert
            (
                0,
                new AIMOwnerLocationInfo
                {
                    AIMOwnerLocationID = 0,
                    LocationCode = "--All--",
                    LocationName = "--All--"    //task 1133 FG 7/21/2026
                }
            );
            lueOwnerLocation.Properties.DataSource = _aimOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueOwnerLocation, "AIMOwnerLocationID", "LocationCode");
            lueOwnerLocation.Properties.Columns["LocationCode"].Visible = true;
            lueOwnerLocation.Properties.Columns["LocationName"].Visible = true;

            //task 1133 FG 7/21/2026 - bind the Owner Location Name dropdown to the same list as the Code dropdown (same pattern as Sample.cs)
            lueOwnerLocationName.Properties.DataSource = _aimOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueOwnerLocationName, "AIMOwnerLocationID", "LocationName", "LocationCode");

            if (_aimOwnerLocation.Count == 2)
            {
                //selecting value if there is only one
                lueOwnerLocation.EditValue = _aimOwnerLocation[1].AIMOwnerLocationID;
            }
            else
            {
                lueOwnerLocation.EditValue = 0; // _aimOwnerLocation;   //11/14/24
            }
        }

        private void lueOwner_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SyncValueLue(ref lueOwner, ref lueOwnerName);       //task 1133 FG 7/21/2026
                dataBindOwnerLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueSource_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SyncValueLue(ref lueSupplier, ref lueSupplierName);     //task 1133 FG 7/21/2026
                databindSupplierLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHelpDryMatter_Click(object sender, EventArgs e)
        {
            try
            {
                //10/6/21 - changed for resize issue
                //reportHelp = new ReportHelp();
                //this.Hide();
                //reportHelp.WindowState = FormWindowState.Normal;                                
                //reportHelp.ShowDialog();
                //this.Show();
                reportDryMatterHelp = new ReportHelpDryMatter();
                reportDryMatterHelp.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void populateMaterialList()
        {

            Int32 id;

            //3/4/21 - commented, not used
            //AIMMaterialInfo material = new AIMMaterialInfo();

            foreach (AIMMaterialInfo item in checkedListBoxControl1.CheckedItems)
            {
                id = item.AIMMaterialID;

                if (_materialIDList.Length > 4000)
                {
                    if (_materialIDList2.Length > 4000)
                    {
                        _materialIDList3 = _materialIDList3 + id.ToString() + ", ";
                    }
                    else
                    {
                        _materialIDList2 = _materialIDList2 + id.ToString() + ", ";
                    }
                }
                else
                {
                    _materialIDList = _materialIDList + id.ToString() + ", ";
                }
            }
        }

        //3/4/21 commented, not used -- BUT MAY BE IN THE FUTURE
        //private void populateNutrientList()
        //{
        //    _nutrientID1 = 0;
        //    _nutrientID2 = 0;            

        //    //3/4/21 - commented, not used
        //    //AIMNutrientInfo nutrient = new AIMNutrientInfo();

        //    foreach (AIMNutrientInfo item in checkedListBoxControl2.CheckedItems)
        //    {
        //        if (_nutrientID1 == 0)
        //        {
        //            _nutrientID1 = item.AIMNutrientID;
        //        }
        //        else
        //        {
        //            _nutrientID2 = item.AIMNutrientID;
        //        }            
        //    }           
        //}

        private Int32 checkCriteria(Int32 reportID)
        {
            String msg;
            Int32 rtn = 0;

            if (reportID == (Int32)ReportEnum.AnalysisPivotSummary)
            {
                if (checkedListBoxControl1.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select Material(s)", "Warning...");
                    rtn = -1;
                }
            }
            if (reportID == (Int32)ReportEnum.SampleComparisonReport)
            {
                if ((Int32)lueFirstDataSource.EditValue == 0)
                {
                    MessageBox.Show("Please select First Datasource", "Warning...");
                    lueFirstDataSource.ShowPopup();
                    rtn = -1;
                }
                else if ((Int32)lueSecondDataSource.EditValue == 0)
                {
                    MessageBox.Show("Please select Second Datasource", "Warning...");
                    lueSecondDataSource.ShowPopup();
                    rtn = -1;
                }
            }

            //Check system setting for Moisture Nutrient. If not set up, do not allow dry-matter version of pivot report 
            if ((reportID == (Int32)ReportEnum.AnalysisPivotReport || reportID == (Int32)ReportEnum.AnalysisPivotSummary) && rbDryMatter.Checked == true)
            {
                //10/24/23 put back in
                //3/4/21 - removed unnecessary assignment
                SystemSettingsInfo systemSettings = new SystemSettingsInfo();
                //SystemSettingsInfo systemSettings;

                systemSettings = AIMManager.GetSystemSettings();
                if (systemSettings == null || systemSettings.AIMNutrientID == 0)
                {
                    msg = "The pivot/dry-matter base report will not run until a Moisture Nutrient Code is assigned.";
                    msg += Environment.NewLine + Environment.NewLine + "To assign a Moisture Nutrient Code:  ";
                    msg += Environment.NewLine + Environment.NewLine + "     From the Main Menu, go to Setup > System Settings > AIM Settings ";
                    msg += Environment.NewLine + "     and select Moisture Nutrient Code from the dropdown. ";

                    MessageBox.Show(msg, "Warning...");
                    rtn = -1;
                }

            }

            //1/3/23
            //Check system setting for DryMatter Nutrient. If not set up, do not allow, message user.
            if (reportID == (Int32)ReportEnum.SupplierLocationGuarantees)
            {
                SystemSettingsInfo systemSettings;
                systemSettings = AIMManager.GetSystemSettings();
                if (systemSettings == null || systemSettings.DryMatterAIMNutrientID == 0)
                {
                    msg = "This report requires a Dry Matter nutrient code to be assigned.";
                    msg += Environment.NewLine + Environment.NewLine + "To assign a Dry Matter Nutrient Code:  ";
                    msg += Environment.NewLine + Environment.NewLine + "     From the Main Menu, go to Setup > System Settings > AIM Settings ";
                    msg += Environment.NewLine + "     and select Dry Matter Nutrient Code from the dropdown. ";

                    MessageBox.Show(msg, "Warning...");
                    rtn = -1;
                }

            }
            return rtn;
        }

        private void setAnalysisVariables(AnalysisReportsInfo analysisSummaryToGet)
        {
            analysisSummaryToGet.SampleOwnerID = (Int32)lueOwner.EditValue;
            analysisSummaryToGet.AIMOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
            analysisSummaryToGet.AIMSupplierID = (Int32)lueSupplier.EditValue;
            analysisSummaryToGet.AIMSupplierLocationID = (Int32)lueSupplierLocation.EditValue;
            analysisSummaryToGet.ToDate = (DateTime)deToDate.DateTime;
            analysisSummaryToGet.FromDate = (DateTime)deFromDate.DateTime;
            analysisSummaryToGet.TicketNumber = txtTicket.Text.Trim();
            if (lueMaterial.Text != "--All--")
            {
                analysisSummaryToGet.AIMMaterialID = (Int32)lueMaterial.EditValue;
            }

            //-----------------------------------------------------            
            analysisSummaryToGet.SwOutliersOnly = false;
            analysisSummaryToGet.SwExtremeOutlierExclude = false;
            if (rbExtremeOutlierExclude.Checked == true)
            {
                analysisSummaryToGet.SwExtremeOutlierExclude = true;
            }
            analysisSummaryToGet.SwMildOutlierExclude = false;
            if (rbMildOutlierExclude.Checked == true)
            {
                analysisSummaryToGet.SwMildOutlierExclude = true;
            }
            //-----------------------------------------------------

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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                ttip.SetToolTip(btnGenerateReport, "Generate Report");
                ttip.SetToolTip(btnSaveTemplate, "Save Template");
                ttip.SetToolTip(btnPreviewGrid, "Preview Grid Results");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //10/6/21 - commented - this button no longer exists (and hasn't for quite some time)
        //private void btnAdvance_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.Hide();
        //        if (nreportView == null || nreportView.Visible == false)
        //        {
        //            nreportView = new ReportQuery();
        //            nreportView.MdiParent = this.MdiParent;
        //            nreportView.Show();
        //        }
        //        else
        //        {
        //            nreportView.WindowState = FormWindowState.Normal;                   
        //            nreportView.Activate();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}


        private void lueOwner_Click(object sender, EventArgs e)
        {
            lueOwner.SelectAll();
        }

        private void lueOwner_Enter(object sender, EventArgs e)
        {
            lueOwner.SelectAll();
        }

        private void lueOwnerLocation_Enter(object sender, EventArgs e)
        {
            lueOwnerLocation.SelectAll();
        }

        private void lueOwnerLocation_Click(object sender, EventArgs e)
        {
            lueOwnerLocation.SelectAll();
        }

        private void lueSupplier_Enter(object sender, EventArgs e)
        {
            lueSupplier.SelectAll();
        }

        private void lueSupplier_Click(object sender, EventArgs e)
        {
            lueSupplier.SelectAll();
        }

        private void lueSupplierLocation_Enter(object sender, EventArgs e)
        {
            lueSupplierLocation.SelectAll();
        }

        private void lueSupplierLocation_Click(object sender, EventArgs e)
        {
            lueSupplierLocation.SelectAll();
        }

        private void lueMaterial_Enter(object sender, EventArgs e)
        {
            lueMaterial.SelectAll();
        }

        private void lueMaterial_Click(object sender, EventArgs e)
        {
            lueMaterial.SelectAll();
        }

        private void moveCursorOnEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void lueMaterial_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void deFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            //10/17/23 commented, causing part of time to show in box
            //moveCursorOnEnter(sender, e);
        }

        private void deToDate_KeyDown(object sender, KeyEventArgs e)
        {
            //10/17/23 commented, causing part of time to show in box
            //moveCursorOnEnter(sender, e);
        }

        private void txtTicket_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void lueOwner_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void lueOwnerLocation_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void lueSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void lueSupplierLocation_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void lueNutrient_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void txtSampleIDFrom_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void txtSampleIDTo_KeyDown(object sender, KeyEventArgs e)
        {
            moveCursorOnEnter(sender, e);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                //10/6/21 - changed for resize issue
                //reportHelp = new ReportHelp();
                //this.Hide();
                //reportHelp.WindowState = FormWindowState.Normal;                                
                //reportHelp.ShowDialog();
                //this.Show();
                reportDryMatterHelp = new ReportHelpDryMatter();
                reportDryMatterHelp.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAllMaterials_Click(Object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxControl1.ItemCount; i++)
            {
                checkedListBoxControl1.SetItemChecked(i, true);
            }
        }

        private void clearMaterials()
        {
            for (int i = 0; i < checkedListBoxControl1.ItemCount; i++)
            {
                checkedListBoxControl1.SetItemChecked(i, false);
            }
        }

        private void listboxReports_SelectedIndexChanged(Object sender, EventArgs e)
        {
            ReportInfo reportInfo;

            Int32 reportID;
            Int32.TryParse(listboxReports.SelectedValue.ToString(), out reportID);
            reportInfo = AIMManager.GetReportReportID(reportID);
            if (reportInfo == null) return;

            AIMGlobal.templateReportID = reportID;
            _selectedTemplateType = reportInfo.TemplateType;
            dataBindTemplate_TemplateType();

            lblDate.Text = "Sample Date";                                                       //8/16/23


            //--------------------------------------------------------------
            //4/15/22
            btnGenerateReport.Image = CFCAIM.Properties.Resources.preview_32x32;
            System.Drawing.Icon ico = Properties.Resources.CFCAIM;
            this.Icon = ico;
            ToolTip ttip = new ToolTip();

            ttip.SetToolTip(btnGenerateReport, String.Empty);
            ttip.SetToolTip(btnGenerateReport, "Generate Report         ");         //added spaces because I couldn't figure out (quickly) how to overwrite tooltip
            if (reportID == (Int32)ReportEnum.UserDefinedSampleData ||
                reportID == (Int32)ReportEnum.UserDefinedSamplePivotData)
            {
                btnGenerateReport.Image = CFCAIM.Properties.Resources.next_32x321;
                ttip.SetToolTip(btnGenerateReport, String.Empty);
                ttip.SetToolTip(btnGenerateReport, "Generate Report Data");
            }
            Int32 testVar = (Int32)lueMaterial.EditValue;
            //--------------------------------------------------------------
            lblAimMaterial.Visible = true;                      //1/15/24
            lblMaterialCode.Visible = true;
            lblMaterialName.Visible = true;                     //1/15/24
            lueMaterial.Visible = true;
            lueMaterial.Enabled = true;
            lueMaterialName.Visible = true;                     //1/15/24
            lueMaterialName.Enabled = true;                     //1/15/24
            pnlMaterialType.Visible = false;
            pnlAnalyzerOrFormulation.Visible = false;           //4/15/22
            txtTicket.Enabled = true;
            lueOwner.Enabled = true;
            lueOwnerLocation.Enabled = true;
            lblOwner.Visible = true;
            lblOwnerLocation.Visible = true;
            lblSource.Visible = true;
            lblSourceLocation.Visible = true;
            lblTicket.Visible = true;
            lueOwner.Visible = true;
            lueOwnerLocation.Visible = true;
            lueSupplier.Visible = true;
            lueSupplierLocation.Visible = true;
            txtTicket.Visible = true;
            chkShowViolations.Visible = false;
            panelTime.Visible = false;
            pnlTemplate.Visible = false;
            pnlSaveResults.Visible = false;
            lblTemplateHeader.Visible = false;
            lblSaveGridResults.Visible = false;

            //9/5/23 - commented
            //rbIngredients.Checked = true;            
            //deFromDate.Text = "";                     
            //deToDate.Text = "";                       

            //-------------------------------------------------------------------------
            // 3 Main Panels:  
            //      panelCriteria   - includes pnlDryMatter, pnlSupplierLocationCriteria, pnlDataSourcesTCompare and pnlMaterialPick
            //      pnlPivot        - includes PivotGridControl1    
            //      pnlGrid         - includes grdReport and grdExportLog

            // panelCriteria is visible for most reports (even if they don't have criteria) -- but it's made invisible for the reports
            //      that use pnlPivot and pnlGrid

            panelCriteria.Visible = false;
            pnlDryMatter.Visible = false;
            pnlSortOnNutrientCode.Visible = false;              //4/20/21            
            pnlGroupBy.Visible = false;                         //1/18/21
            pnlShowHideTotalFields.Visible = false;             //1/18/21

            pnlSupplierLocationCriteria.Visible = false;
            pnlDataSourcesToCompare.Visible = false;
            pnlMaterialPick.Visible = false;
            pnlPivot.Visible = false;
            pnlGrid.Visible = false;
            grdReport.Visible = false;
            grdExportLog.Visible = false;                       //TASK951 6/4/24

            pnlMaterialGroup.Visible = false;                   //2/16/21
            rbGroupMaterialsNo.Checked = true;                  //2/16/21            
            pnlQualityControl.Visible = false;                  //4/26/22
            rbQCNo.Checked = true;                              //4/26/22

            panelShowOnly.Visible = false;                      //6/20/23
            pnlTestType.Visible = false;                        //8/16/23
            panelDateSampleOrCreated.Visible = false;           //8/22/23
            panelCFCUser.Visible = false;                       //9/1/23
            hyperReportComments.Visible = false;                //9/6/23

            btnHelpReports.Visible = false;                     //10/26/23
            pnlSampleResultOption.Visible = false;              //Task1076 DB250711
            grdCostAnalysis.Visible = false;                    //Task1150 DB250109

            //-------------------------------------------------------------------------
            //RESIZE PANEL CRITERIA (based on size of main group)
            panelCriteria.Visible = true;

            //9/5/23
            //panelCriteria.Location = new Point(248, 210);
            panelCriteria.Location = new Point(279, 210);

            panelWidth = grpMain.Width - 270;
            panelHeight = grpMain.Height - 230;
            panelCriteria.Size = new Size(panelWidth, panelHeight);

            gridWidth = grpMain.Width - 310;                            //TASK951 6/4/24
            gridHeight = grpMain.Height - 235;                          //TASK951 6/4/24    
            gridWidthExport = grpMain.Width - 310;                      //TASK951 6/4/24
            gridHeightExport = grpMain.Height - 235;                    //TASK951 6/4/24


            pnlMaterialType.Size = new Size(112, 35);  //(w/h)          //1/15/24
            rbAllIngredientsProducts.Checked = true;                    //1/15/24
            lblAimMaterial.Visible = false;                             //1/15/24
            lblMaterialCode.Visible = false;                            //1/15/24
            lblMaterialName.Visible = false;                            //1/15/24
            lueMaterial.Visible = false;                                //1/15/24
            lueMaterialName.Visible = false;                            //1/15/24


            //-------------------------------------------------------------------------
            //ANALYSIS SUMMARY
            #region Analysis Summary
            //4/20/21
            if (reportID == (Int32)ReportEnum.AnalysisSummary)
            {
                pnlSortOnNutrientCode.Visible = true;
                pnlSortOnNutrientCode.Location = new Point(1, 97);
                pnlSortOnNutrientCode.Size = new Size(132, 61);
            }
            #endregion

            //-------------------------------------------------------------------------
            // ANALYSIS PIVOT REPORT
            #region Analysis Pivot Report
            if (reportID == (Int32)ReportEnum.AnalysisPivotReport)
            {
                pnlDryMatter.Visible = true;
                pnlDryMatter.Location = new Point(10, 8);
                pnlDryMatter.Size = new Size(232, 67);

                //1/18/21
                pnlGroupBy.Visible = true;
                pnlGroupBy.Location = new Point(270, 9);
                pnlGroupBy.Size = new Size(370, 145);

                pnlShowHideTotalFields.Visible = true;
                pnlShowHideTotalFields.Location = new Point(10, 90);
                pnlShowHideTotalFields.Size = new Size(333, 185);
                lblGrandTotal.Visible = false;

                //4/28/21
                pnlSortOnNutrientCode.Visible = true;
                pnlSortOnNutrientCode.Location = new Point(1, 97);
                pnlSortOnNutrientCode.Size = new Size(132, 61);


            }
            #endregion

            testVar = (Int32)lueMaterial.EditValue;
            //-------------------------------------------------------------------------
            // ANALYSIS PIVOT SUMMARY
            #region Analysis Pivot Summary
            if (reportID == (Int32)ReportEnum.AnalysisPivotSummary)
            {
                pnlDryMatter.Visible = true;
                pnlDryMatter.Location = new Point(10, 5);
                pnlDryMatter.Size = new Size(232, 67);
                lblAimMaterial.Visible = false;                     //1/15/24
                lblMaterialCode.Visible = false;
                lblMaterialName.Visible = false;                    //1/15/24
                lueMaterial.Visible = false;
                lueMaterialName.Visible = false;                    //1/15/24
                pnlMaterialPick.Visible = true;
                pnlMaterialPick.Location = new Point(15, 80);
                pnlMaterialPick.Size = new Size(850, 250);
                pnlSortOnNutrientCode.Visible = true;               //4/28/21
                pnlSortOnNutrientCode.Location = new Point(1, 97);  //4/28/21
                pnlSortOnNutrientCode.Size = new Size(132, 61);     //4/28/21
            }

            //4/15/22
            if (reportID > 0)
            {
                timer1.Start();
            }

            #endregion

            //-------------------------------------------------------------------------
            //EXPORT PENDING BATCH DETAIL
            #region Export Pending Batch Detail
            //4/20/21
            if (reportID == (Int32)ReportEnum.ExportBatchDetail)
            {
                pnlSortOnNutrientCode.Visible = true;
                pnlSortOnNutrientCode.Location = new Point(1, 97);
                pnlSortOnNutrientCode.Size = new Size(132, 61);
            }
            #endregion



            //-------------------------------------------------------------------------
            // MANUFACTURED PRODUCT COMPLIANCE REPORT
            #region Mfg Product Compliance Report
            if (reportID == (Int32)ReportEnum.ManufacturedProductComplianceReport)
            {
                chkShowViolations.Visible = true;

                lblAimMaterial.Visible = false;                         //1/15/24
                lblMaterialCode.Visible = false;
                lblMaterialName.Visible = false;                        //1/15/24
                lueMaterial.Visible = false;
                lueMaterialName.Visible = false;                        //1/15/24
                lblOwner.Visible = false;
                lblOwnerLocation.Visible = false;
                lblSource.Visible = false;
                lblSourceLocation.Visible = false;
                lblTicket.Visible = false;
                lueOwner.Visible = false;
                lueOwnerLocation.Visible = false;
                lueSupplier.Visible = false;
                lueSupplierLocation.Visible = false;
                txtTicket.Visible = false;
            }
            #endregion


            //-------------------------------------------------------------------------
            //MATERIAL CROSS-REFERENCE ORPHANS
            #region Material Cross-Reference Orphans
            //4/20/21
            if (reportID == (Int32)ReportEnum.AimMaterialRefAnalyzerMaterialOphans)
            {

                //4/15/22                
                pnlMaterialType.Visible = true;
                rbProducts.Checked = true;
                pnlAnalyzerOrFormulation.Visible = true;

                //6/20/23
                //pnlAnalyzerOrFormulation.Location = new Point(114, 97);
                pnlAnalyzerOrFormulation.Location = new Point(140, 92);


            }
            #endregion

            //-------------------------------------------------------------------------
            //NUTRIENT CROSS-REFERENCE ORPHANS
            #region Nutrient Cross-Reference Orphans
            //4/20/21
            if (reportID == (Int32)ReportEnum.AimNutrientRefAnalyzerNutrientOphans)
            {
                pnlSortOnNutrientCode.Visible = true;
                pnlSortOnNutrientCode.Location = new Point(1, 97);
                pnlSortOnNutrientCode.Size = new Size(132, 61);
            }
            #endregion

            //-------------------------------------------------------------------------
            // SAMPLE ACTIVITY REPORT
            #region Sample Activity Report
            //12/28/17
            if (reportID == (Int32)ReportEnum.SampleActivityReport)
            {

                //Task1075 DB250716 deFromDate.Text = DateTime.Today.ToString();                
                chkShowViolations.Visible = false;
                lblAimMaterial.Visible = false;                         //1/15/24
                lblMaterialCode.Visible = false;
                lblMaterialName.Visible = false;                        //1/15/24
                lueMaterial.Visible = false;
                lueMaterialName.Visible = false;                        //1/15/24
                pnlMaterialType.Visible = true;
                lblOwner.Visible = false;
                lblOwnerLocation.Visible = false;
                lblSource.Visible = false;
                lblSourceLocation.Visible = false;
                lblTicket.Visible = false;
                lueOwner.Visible = false;
                lueOwnerLocation.Visible = false;
                lueSupplier.Visible = false;
                lueSupplierLocation.Visible = false;
                txtTicket.Visible = false;
                panelTime.Visible = true;                               //3/20/18
                //pnlQualityControl.Location = new Point(114, 97);      //6/20/23
                pnlQualityControl.Location = new Point(140, 92);        //6/20/23
                pnlQualityControl.Visible = true;
                rbIngredients.Checked = true;                           //2/12/24

                PopupContainerControl container = new PopupContainerControl();
                XtraUserControl uc = new XtraUserControl();
                uc.Dock = DockStyle.Fill;
                container.Controls.Add(dropDownButton1);
                popupContainerEdit1.Properties.PopupControl = container;
            }
            #endregion
            testVar = (Int32)lueMaterial.EditValue;
            //-------------------------------------------------------------------------
            // SAMPLE COMPARISON REPORT 
            #region Sample Comparison Report
            if (reportID == (Int32)ReportEnum.SampleComparisonReport)
            {
                //4/20/21                
                pnlSortOnNutrientCode.Visible = true;
                pnlSortOnNutrientCode.Location = new Point(1, 97);
                pnlSortOnNutrientCode.Size = new Size(132, 61);

                pnlDataSourcesToCompare.Visible = true;
                pnlDataSourcesToCompare.Location = new Point(3, 7);
                pnlDataSourcesToCompare.Size = new Size(286, 140);

                //Default to last 30 days                    

                if (deToDate.EditValue == "" || deToDate.EditValue is null)             //Task1075 DB250716
                {
                    deToDate.EditValue = DateTime.Now;
                }
                if (deFromDate.EditValue == "" || deFromDate.EditValue is null)         //Task1075 DB250716
                {
                    deFromDate.EditValue = DateTime.Now.AddDays(-30);
                }

            }
            #endregion


            //-------------------------------------------------------------------------
            // SAMPLE RESULTS BY SUPPLIER
            #region Sample Results by Supplier
            //2/12/18
            if (reportID == (Int32)ReportEnum.SampleResultsBySupplier)
            {
                lblAimMaterial.Visible = false;                             //1/15/24
                lblMaterialCode.Visible = false;
                lblMaterialName.Visible = false;                            //1/15/24
                lueMaterial.Visible = false;
                lueMaterialName.Visible = false;                            //1/15/24
                lblTicket.Visible = false;
                txtTicket.Visible = false;
            }
            #endregion


            //-------------------------------------------------------------------------
            // SUPPLIER LOCATION GUARANTEES
            #region Supplier Location Guarantees
            if (reportID == (Int32)ReportEnum.SupplierLocationGuarantees)
            {
                pnlSortOnNutrientCode.Visible = true;                       //4/20/21 
                pnlSortOnNutrientCode.Location = new Point(1, 97);          //4/20/21 
                pnlSortOnNutrientCode.Size = new Size(132, 61);             //4/20/21 
                txtTicket.Enabled = false;
                lueOwner.Enabled = false;
                lueOwnerLocation.Enabled = false;
                pnlSupplierLocationCriteria.Visible = true;
                pnlSupplierLocationCriteria.Location = new Point(10, 5);
                //pnlSupplierLocationCriteria.Size = new Size(232, 67);     //1/18/23
                pnlSupplierLocationCriteria.Size = new Size(232, 103);      //1/18/23
                gpUseResultOrAdjusted.Visible = true;
            }
            #endregion


            //-------------------------------------------------------------------------
            // USER-DEFINED SAMPLE DATA
            #region User-Defined Sample Data
            //4/4/18
            if (reportID == (Int32)ReportEnum.UserDefinedSampleData)
            {
                panelCriteria.Visible = false;                      //6/4/20
                chkShowViolations.Visible = false;
                lblMaterialCode.Visible = false;
                lblMaterialName.Visible = false;                    //1/15/24
                lueMaterial.Visible = false;
                lueMaterialName.Visible = false;                    //1/15/24
                lblSource.Visible = false;
                lblSourceLocation.Visible = false;
                lblTicket.Visible = false;
                lueSupplier.Visible = false;
                lueSupplierLocation.Visible = false;
                txtTicket.Visible = false;
                lblAimMaterial.Visible = true;                      //1/15/24
                pnlMaterialType.Visible = true;
                pnlMaterialGroup.Visible = true;                    //2/16/21
                pnlSampleResultOption.Visible = true;               //Task1076 DB250711

                pnlGrid.Visible = true;
                pnlGrid.Location = new Point(279, 210);             //9/5/23
                pnlGrid.Size = new Size(panelWidth, panelHeight);
                grdReport.Visible = true;
                grdReport.Size = new Size(gridWidth, gridHeight);   //TASK951 6/4/24 
                grdReport.Location = new Point(3, -2);              //TASK951 6/4/24           

                pnlTemplate.Visible = true;
                pnlSaveResults.Visible = true;
                lblTemplateHeader.Visible = true;
                lblSaveGridResults.Visible = true;
                rbIngredients.Checked = true;                       //2/12/24
                grdExportLog.Visible = false;                       //TASK951 6/4/24
            }
            #endregion


            //-------------------------------------------------------------------------
            // USER-DEFINED SAMPLE PIVOT DATA
            #region User-Defined Sample Pivot Data
            //4/12/18
            if (reportID == (Int32)ReportEnum.UserDefinedSamplePivotData)
            {
                panelCriteria.Visible = false;                      //6/3/20
                pnlPivot.Visible = true;
                //pnlPivot.Location = new Point(254, 272);          //9/5/23
                pnlPivot.Location = new Point(276, 272);            //9/5/23
                pnlPivot.Size = new Size(panelWidth, panelHeight);
                pnlMaterialGroup.Visible = true;                    //2/16/21
                pnlTemplate.Visible = true;
                pnlSaveResults.Visible = true;
                lblTemplateHeader.Visible = true;
                lblSaveGridResults.Visible = true;
                pnlMaterialType.Visible = true;
                panelShowOnly.Visible = true;                       //6/26/23
                lblAimMaterial.Visible = true;                      //1/15/24
                rbIngredients.Checked = true;                       //2/12/24
                _LayoutStream = new System.IO.MemoryStream();       //5/22/19 - Save the default layout to a stream
                pnlSampleResultOption.Visible = false;              //Task1076 DB250711
                pivotGridControl1.SaveLayoutToStream(_LayoutStream);
            }
            #endregion
            testVar = (Int32)lueMaterial.EditValue;
            //-------------------------------------------------------------------------
            // SUBMITTED SAMPLES REPORT
            #region Submitted Samples Report
            if (reportID == (Int32)ReportEnum.SamplesSubmittedReport)
            {
                lblDate.Text = "Sample Created Date";               //8/16/23                
                if (deFromDate.EditValue == "" || deFromDate.EditValue is null)             //Task1075 DB250716
                {
                    deFromDate.EditValue = DateTime.Now;                //10/9/23
                }
                if (deToDate.EditValue == "" || deToDate.EditValue is null)             //Task1075 DB250716
                {
                    deToDate.EditValue = DateTime.Now;                  //10/9/23
                }
                chkShowViolations.Visible = false;
                lblAimMaterial.Visible = true;                      //1/15/24
                lblMaterialCode.Visible = true;
                lblMaterialName.Visible = true;                     //1/15/24
                lueMaterial.Visible = true;
                lueMaterialName.Visible = true;                     //1/15/24
                lblOwner.Visible = true;
                lblOwnerLocation.Visible = true;
                lblSource.Visible = false;
                lblSourceLocation.Visible = false;
                lblTicket.Visible = false;
                lueSupplier.Visible = false;
                lueSupplierLocation.Visible = false;
                txtTicket.Visible = false;
                pnlTestType.Visible = true;
                pnlTestType.Location = new Point(3, 7);
                pnlTestType.Size = new Size(286, 140);
                panelDateSampleOrCreated.Visible = true;
                panelCFCUser.Visible = true;                        //10/16/23
                btnHelpReports.Visible = true;                      //10/26/23
                pnlMaterialType.Visible = true;                     //1/15/24
                pnlMaterialType.Size = new Size(112, 53);  //(w/h)  //1/15/24
                lblMaterialCode.Visible = false;                    //1/15/24
                lblMaterialName.Visible = false;                    //1/15/24
                lueMaterial.Visible = false;                        //1/15/24
                lueMaterialName.Visible = false;                    //1/15/24
            }
            #endregion

            //-------------------------------------------------------------------------                   //9/1/23
            // SAMPLE ANALYSIS REQUEST FORM
            #region Sample Analysis Request Form
            if (reportID == (Int32)ReportEnum.SampleAnalysisRequestFormEvonik ||
                reportID == (Int32)ReportEnum.SampleAnalysisRequestFormAjinomoto ||
                reportID == (Int32)ReportEnum.SampleAnalysisRequestFormUnitedAnimalHealth)
            {
                lblDate.Text = "Sample Created Date";                                                       //8/16/23

                //deFromDate.Text = DateTime.Today.ToString();                                                //10/9/23
                //deToDate.Text = DateTime.Today.ToString();                                                  //10/9/23
                if (deFromDate.EditValue == "" || deFromDate.EditValue is null)             //Task1075 DB250716
                {
                    deFromDate.EditValue = DateTime.Now;                //10/9/23
                }
                if (deToDate.EditValue == "" || deToDate.EditValue is null)             //Task1075 DB250716
                {
                    deToDate.EditValue = DateTime.Now;                  //10/9/23
                }
                chkShowViolations.Visible = false;
                lblAimMaterial.Visible = true;                      //1/15/24
                lblMaterialCode.Visible = true;
                lblMaterialName.Visible = true;                 //1/15/24
                lueMaterial.Visible = true;
                lueMaterialName.Visible = true;                 //1/15/24
                lblOwner.Visible = false;
                lblOwnerLocation.Visible = false;
                lblSource.Visible = false;
                lblSourceLocation.Visible = false;
                lblTicket.Visible = false;
                lueOwner.Visible = false;
                lueOwnerLocation.Visible = false;
                lueSupplier.Visible = false;
                lueSupplierLocation.Visible = false;
                txtTicket.Visible = false;
                panelDateSampleOrCreated.Visible = false;
                panelCFCUser.Visible = true;
                hyperReportComments.Visible = true;

                btnHelpReports.Visible = true;                                                              //10/26/23

                if (reportID == (Int32)ReportEnum.SampleAnalysisRequestFormUnitedAnimalHealth) //Task1053 250814
                {
                    lblSource.Visible = true;
                    lblSourceLocation.Visible = true;
                    lueSupplier.Visible = true;
                    lueSupplierLocation.Visible = true;
                }
            }
            #endregion
            testVar = (Int32)lueMaterial.EditValue;
            //-------------------------------------------------------------------------
            //EXPORT LOG DETAIL
            #region Export Log Detail                                                       //Task951 6/4/24 
            if (reportID == (Int32)ReportEnum.ExportLog)
            {
                panelCriteria.Visible = false;
                chkShowViolations.Visible = false;
                lblAimMaterial.Visible = false;
                lblMaterialCode.Visible = false;
                lblMaterialName.Visible = false;
                lueMaterial.Visible = false;
                lueMaterialName.Visible = false;
                pnlMaterialType.Visible = false;
                lblSource.Visible = false;
                lblSourceLocation.Visible = false;
                lblTicket.Visible = false;
                lueSupplier.Visible = false;
                lueSupplierLocation.Visible = false;
                txtTicket.Visible = false;
                pnlMaterialGroup.Visible = false;
                lblOwner.Visible = false;
                lblOwnerLocation.Visible = false;
                lueOwner.Visible = false;
                lueOwnerLocation.Visible = false;
                grdReport.Visible = false;
                pnlTemplate.Visible = false;
                lblTemplateHeader.Visible = false;
                pnlSampleResultOption.Visible = false;              //Task1076 DB250711
                grdCostAnalysis.Visible = false;            //Task1150 DB250109

                pnlGrid.Visible = true;
                pnlGrid.Location = new Point(279, 210);
                pnlGrid.Size = new Size(panelWidth, panelHeight);
                grdExportLog.Visible = true;
                grdExportLog.Size = new Size(gridWidthExport, gridHeightExport);
                grdExportLog.Location = new Point(3, -2);
                pnlSaveResults.Visible = true;
                lblSaveGridResults.Visible = true;

            }


            #endregion

            //-------------------------------------------------------------------------

            if (reportID == (Int32)ReportEnum.AnalysisCost)         //Task1150 DB260108
            {
                panelCriteria.Visible = false;                      //6/3/20
                //pnlPivot.Visible = true;
                ////pnlPivot.Location = new Point(254, 272);          //9/5/23
                pnlGrid.Location = new Point(276, 272);            //9/5/23
                pnlGrid.Size = new Size(panelWidth, panelHeight);
                pnlMaterialGroup.Visible = true;                      //1/15/24
                _LayoutStream = new System.IO.MemoryStream();       //5/22/19 - Save the default layout to a stream
                pnlSampleResultOption.Visible = false;              //Task1076 DB250711
                //pivotGridControl1.SaveLayoutToStream(_LayoutStream);
                grdCostAnalysis.Visible = true;            //Task1150 DB250109
                grdCostAnalysis.Size = new Size(gridWidthExport, gridHeightExport);
                grdCostAnalysis.Location = new Point(3, -2);
                lblSource.Visible = false;
                lblSourceLocation.Visible = false;
                lblTicket.Visible = false;
                lueSupplier.Visible = false;
                lueSupplierLocation.Visible = false;
                txtTicket.Visible = false;

                lueOwner.Visible = true;
                lueOwnerLocation.Visible = true;
                lblOwner.Visible = true;
                lblOwnerLocation.Visible = true;
            }                                                       //Task1150 DB260108

            //200606CM - changed this back, until decision is made about showing products
            //6/5/20 - commented, because now we want these reports to have the option of selecting ingredients or products            
            if (reportID == (Int32)ReportEnum.AnalysisPivotReport ||
                reportID == (Int32)ReportEnum.AnalysisPivotSummary ||
                reportID == (Int32)ReportEnum.ManufacturedProductComplianceReport ||
                reportID == (Int32)ReportEnum.SampleResultsBySupplier ||
                //reportID == (Int32)ReportEnum.AimMaterialRefAnalyzerMaterialOphans ||     //4/15/22 - COMMENTED
                reportID == (Int32)ReportEnum.AimNutrientRefAnalyzerNutrientOphans ||
                reportID == (Int32)ReportEnum.ExportBatchDetail ||
                reportID == (Int32)ReportEnum.AnalysisSupplierComparison ||
                reportID == (Int32)ReportEnum.AnalysisDateComparisonReport ||
                reportID == (Int32)ReportEnum.SupplierLocationGuarantees ||
                reportID == (Int32)ReportEnum.AnalysisDetail ||
                reportID == (Int32)ReportEnum.AnalysisSummary ||
                reportID == (Int32)ReportEnum.SampleComparisonReport ||
                //reportID == (Int32)ReportEnum.SamplesSubmittedReport ||                   //commented 1/15/24
                reportID == (Int32)ReportEnum.SampleAnalysisRequestFormEvonik ||            //10/5/23
                reportID == (Int32)ReportEnum.SampleAnalysisRequestFormAjinomoto ||         //10/5/23
                reportID == (Int32)ReportEnum.SampleAnalysisRequestFormUnitedAnimalHealth)  //10/5/23
            {
                pnlMaterialType.Visible = false;
            }


            deFromDate.Focus();

        }

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void reportView_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnClearNutrients_Click(Object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxControl2.ItemCount; i++)
            {
                checkedListBoxControl2.SetItemChecked(i, false);
            }
        }

        private void checkedListBoxControl2_ItemCheck(Object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (checkedListBoxControl2.CheckedItems.Count > 2)
            {
                MessageBox.Show("You may select only two nutrients for this report.", "Warning...");
                {
                    if (checkedListBoxControl2.GetItemCheckState(checkedListBoxControl2.SelectedIndex) == CheckState.Checked)
                    {
                        checkedListBoxControl2.SetItemCheckState(checkedListBoxControl2.SelectedIndex, CheckState.Unchecked);
                    }
                }
            }
        }

        private void btnPreviewGrid_Click(Object sender, EventArgs e)
        {
            Int32 reportID;                                                                 //Task1150 DB260113                                                
            Int32.TryParse(listboxReports.SelectedValue.ToString(), out reportID);          //Task1150 DB260113
            {          
                try
                {

                    if (_selectedTemplateType == (Int32)EnumReportDisplayType.GridControl)
                    {
                        if (AIMGlobal.templateReportID == (Int32)ReportEnum.UserDefinedSampleData)
                        {

                            //AIMGlobal.skipCompanyDateHeading = false;
                            CFCAIMCore.Global.skipCompanyDateHeading = true;                                        //5/22/19 - added skip because Company and Date were showing up oddly on report                            
                            //CFCAIMCore.Global.previewGridResults(grdReport, "User-Defined Sample Data");          //7/2/2026 - added skip because method replaced by previewGridResults
                            if (_currentPrintLayout == null)   //1175FG 7/1/2026
                            {
                                _currentPrintLayout = LoadPrintLayoutFromTemplate();
                            }
                            CFCAIMCore.Global.previewGridResults(
                                grdReport,
                                "User-Defined Sample Data",
                                _currentPrintLayout,
                                OnPreviewClosed);
                            CFCAIMCore.Global.skipCompanyDateHeading = false;
                        }

                        else if (AIMGlobal.templateReportID == (Int32)ReportEnum.ExportLog)                         //TASK951 6/4/24 - begin
                        {
                            CFCAIMCore.Global.skipCompanyDateHeading = true;
                            CFCAIMCore.Global.previewGridResults(grdExportLog, "Export Log Detail");
                            CFCAIMCore.Global.skipCompanyDateHeading = false;
                        }                                                                                           //TASK951 6/4/24 - end
                    }
                    else if (_selectedTemplateType == (Int32)EnumReportDisplayType.PivotControl)
                    {
                        if (AIMGlobal.templateReportID == (Int32)ReportEnum.UserDefinedSamplePivotData)
                        {
                            //AIMGlobal.skipCompanyDateHeading = true;                                              //5/22/19 - added skip because Company and Date were showing up oddly on report
                            //AIMGlobal.previewPivotResults(pivotGridControl1, "User-Defined Sample Pivot Data");
                            //AIMGlobal.skipCompanyDateHeading = false;
                            CFCAIMCore.Global.skipCompanyDateHeading = true;                                        //5/22/19 - added skip because Company and Date were showing up oddly on report
                            //CFCAIMCore.Global.previewPivotResults(pivotGridControl1, "User-Defined Sample Pivot Data"); //7/2/2026 - added skip beccase method is replaced by previewPivotResults
                            
                            if (_currentPrintLayout == null)   //Task-1175 FG 7/1/2026
                            {
                                _currentPrintLayout = LoadPrintLayoutFromTemplate();
                            }
                            CFCAIMCore.Global.previewPivotResults(
                                pivotGridControl1,
                                "User-Defined Sample Pivot Data",
                                _currentPrintLayout,
                                OnPreviewClosed);
                            CFCAIMCore.Global.skipCompanyDateHeading = false;
                        }
                    }

                    if (reportID == (Int32)ReportEnum.AnalysisCost)
                    {
                        ReportFunctions.fromDate = deFromDate.DateTime;
                        ReportFunctions.toDate = deToDate.DateTime;
                        ReportFunctions.locationName = lueOwnerLocation.GetColumnValue("LocationName").ToString();
                        ReportFunctions.DataSetTestGrid = (List<TestGridInfo>)grdCostAnalysis.DataSource;
                        ReportFunctions.LoadReport((Int32)ReportEnum.AnalysisCost);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void lueTemplate_EditValueChanged(Object sender, EventArgs e)
        {
            _selectedViewTemplateID = (Int32)lueTemplate.EditValue;
            AIMGlobal.templateID = (Int32)lueTemplate.EditValue;

            //If "--Select--" is picked, remove any column groupings
            if ((Int32)_selectedViewTemplateID == 0)
            {
                _currentPrintLayout = null;   //task1175 FG 7/2/2026
                //3/8/23
                txtUserName.Visible = false;
                txtUpdateStamp.Visible = false;
                lblUserName.Visible = false;
                lblUpdateStamp.Visible = false;


                if (_selectedTemplateType == (Int32)EnumReportDisplayType.GridControl)
                {
                    grdReport.DataSource = null;
                    grdReportView.RefreshData();
                }
                else if (_selectedTemplateType == (Int32)EnumReportDisplayType.PivotControl)
                {

                    //5/22/19
                    pivotGridControl1.DataSource = null;
                    pivotGridControl1.RefreshData();

                    // Load the layout from the stream.
                    if (_LayoutStream.Length > 0)
                    {
                        _LayoutStream.Position = 0;
                        _LayoutStream.Flush();
                        _LayoutStream.Seek(0, SeekOrigin.Begin);
                        pivotGridControl1.RestoreLayoutFromStream(_LayoutStream);
                    }
                }

                //Clear criteria
                lueMaterial.EditValue = 0;
                lueOwner.EditValue = 0;
                lueOwnerLocation.EditValue = 0;
                lueSupplier.EditValue = 0;
                lueSupplierLocation.EditValue = 0;
                rbIngredients.Checked = true;

            }
            else
            {
                //3/8/23
                txtUserName.Visible = true;
                txtUpdateStamp.Visible = true;
                lblUserName.Visible = true;
                lblUpdateStamp.Visible = true;                  

                applyViewTemplate();
            }

            //5/21/19
            deFromDate.Focus();

        }

        private void applyViewTemplate()
        {
            //3/4/21 - removed unnecessary assignment
            //AnalysisViewTemplateInfo viewTemplate = new AnalysisViewTemplateInfo();
            AnalysisViewTemplateInfo viewTemplate;

            viewTemplate = (AnalysisViewTemplateInfo)lueTemplate.GetSelectedDataRow();
            if (viewTemplate == null || viewTemplate.AnalysisViewTemplateID == 0 || viewTemplate.AnalysisViewTemplateXML == null)
            {
                return;
            }
            if (viewTemplate.AnalysisViewTemplateXML.Trim().Length == 0)
            {
                return;
            }
            try
            {
                //5/20/19 - get criteria                
                AnalysisViewTemplateInfo criteria = new AnalysisViewTemplateInfo();
                criteria = AIMManager.GetAnalysisViewTemplate(viewTemplate.AnalysisViewTemplateID);

                if (_selectedTemplateType == (Int32)EnumReportDisplayType.GridControl)
                {
                    // convert string to stream
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(viewTemplate.AnalysisViewTemplateXML.Trim());
                    MemoryStream stream = new MemoryStream(byteArray);
                    grdReportView.RestoreLayoutFromStream(stream);
                }

                //4/12/18
                else if (_selectedTemplateType == (Int32)EnumReportDisplayType.PivotControl)
                {
                    System.IO.Stream streamPivot;
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(viewTemplate.AnalysisViewTemplateXML.Trim());
                    streamPivot = new System.IO.MemoryStream(byteArray);
                    pivotGridControl1.RestoreLayoutFromStream(streamPivot);
                    streamPivot.Seek(0, System.IO.SeekOrigin.Begin);

                    //5/21/19
                    //Task1075 DB250723 lueMaterial.EditValue = criteria.templateAIMMaterialID;
                    //Task1075 DB250723 lueOwner.EditValue = criteria.templateAIMOwnerID;
                    //Task1075 DB250723 lueOwnerLocation.EditValue = criteria.templateAIMOwnerLocationID;
                    //Task1075 DB250723 lueSupplier.EditValue = criteria.templateAIMSupplierID;
                    //Task1075 DB250723 lueSupplierLocation.EditValue = criteria.templateAIMSupplierLocationID;
                    //Task1075 DB250723 rbIngredients.Checked = true;
                    //Task1075 DB250723 if (criteria.templateMaterialTypeID == (Int32)MaterialTypeEnum.Product)
                    //Task1075 DB250723 {
                    //Task1075 DB250723     rbProducts.Checked = true;
                    //Task1075 DB250723 }

                    ////3/8/23
                    //txtUserName.Text = criteria.UserName;
                    //txtUpdateStamp.Text = criteria.UpdateStamp.ToString();


                }

                //Task1075 DB250723
                lueMaterial.EditValue = criteria.templateAIMMaterialID;
                lueOwner.EditValue = criteria.templateAIMOwnerID;
                lueOwnerLocation.EditValue = criteria.templateAIMOwnerLocationID;
                lueSupplier.EditValue = criteria.templateAIMSupplierID;
                lueSupplierLocation.EditValue = criteria.templateAIMSupplierLocationID;
                rbIngredients.Checked = true;
                if (criteria.templateMaterialTypeID == (Int32)MaterialTypeEnum.Product)
                {
                    rbProducts.Checked = true;
                }

                txtUserName.Text = criteria.UserName;
                txtUpdateStamp.Text = criteria.UpdateStamp.ToString();

                if (criteria.CFCUserID != _userGlobal.CFCLoginUser.CFCUserID)              //Task1148 DB260416 Start
                {
                    txtUserName.BackColor = SystemColors.Control;
                    txtUserName.ForeColor = Color.Red;
                }
                else
                {
                    txtUserName.BackColor = SystemColors.Control;
                    txtUserName.ForeColor = Color.Black;
                }                                                                           //Task1148 DB260416 End

                _currentPrintLayout = LoadPrintLayoutFromTemplate();   //1175FG 7/1/2026

                if (deFromDate.Text != "")
                {
                    btnGenerateReport.PerformClick();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region Report Print Layout //1175FG 7/1/2026
        private ReportPrintLayoutInfo LoadPrintLayoutFromTemplate()   //1175FG 7/1/2026
        {
            if ((Int32)lueTemplate.EditValue == 0)
            {
                return CFCAIMCore.Global.GetDefaultPrintLayout();
            }
            AnalysisViewTemplateInfo viewTemplate = (AnalysisViewTemplateInfo)lueTemplate.GetSelectedDataRow();
            if (viewTemplate == null)
            {
                return CFCAIMCore.Global.GetDefaultPrintLayout();
            }
            AnalysisViewTemplateInfo criteria = AIMManager.GetAnalysisViewTemplate(viewTemplate.AnalysisViewTemplateID);
            return CFCAIMCore.Global.DeserializePrintLayout(criteria.PrintLayoutXML);
        }
        private void SavePrintLayoutToTemplate(ReportPrintLayoutInfo layout)   //1175FG 7/1/2026
        {
            if ((Int32)lueTemplate.EditValue == 0)
            {
                return;
            }
            AnalysisViewTemplateInfo viewTemplate = (AnalysisViewTemplateInfo)lueTemplate.GetSelectedDataRow();
            if (viewTemplate == null)
            {
                return;
            }
            AnalysisViewTemplateInfo criteria = AIMManager.GetAnalysisViewTemplate(viewTemplate.AnalysisViewTemplateID);
            criteria.PrintLayoutXML = CFCAIMCore.Global.SerializePrintLayout(layout);
            criteria.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            AIMManager.SaveAnalysisViewTemplate(ref criteria);
        }
        private void OnPreviewClosed(ReportPrintLayoutInfo layout)   //1175FG 7/1/2026
        {
            if ((Int32)lueTemplate.EditValue == 0)
            {
                return;
            }
            if (PrintLayoutChanged(_currentPrintLayout, layout) == false)   //task1175 FG 7/2/2026
            {
                return;
            }
            DialogResult dialogResult = MessageBox.Show(
                "Save print settings to " + lueTemplate.Text.Trim() + "?",
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


        //4/5/18
        private void setTemplateGlobals()
        {
            AIMGlobal.templateReportID = (Int32)listboxReports.SelectedValue;
            AIMGlobal.templateFromDate = deFromDate.DateTime;
            AIMGlobal.templateToDate = deToDate.DateTime;
            AIMGlobal.templateMaterialType = 1;


            ////////if (rbProducts.Checked == true)
            ////////{
            ////////    AIMGlobal.templateMaterialType = 2;
            ////////}
            ////////AIMGlobal.templateOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
            ////////AIMGlobal.templateID = (Int32)lueTemplate.EditValue;


            //-----------------------------------------------------------------------------
            //5/20/19 - send criteria as part of template-save
            AIMGlobal.templateAIMMaterialID = (Int32)lueMaterial.EditValue;
            AIMGlobal.templateAIMOwnerID = (Int32)lueOwner.EditValue;
            AIMGlobal.templateAIMOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
            AIMGlobal.templateAIMSupplierID = (Int32)lueSupplier.EditValue;
            AIMGlobal.templateAIMSupplierLocationID = (Int32)lueSupplierLocation.EditValue;
            AIMGlobal.templateMaterialTypeID = (Int32)MaterialTypeEnum.Ingredient;
            if (rbProducts.Checked == true)
            {
                AIMGlobal.templateMaterialTypeID = (Int32)MaterialTypeEnum.Product;
            }
            //-----------------------------------------------------------------------------


        }

        //4/5/18
        private void getTemplateGlobals()
        {
            listboxReports.SelectedValue = AIMGlobal.templateReportID;
            deFromDate.DateTime = AIMGlobal.templateFromDate;
            deToDate.DateTime = AIMGlobal.templateToDate;

            ////////rbIngredients.Checked = true;
            ////////if (AIMGlobal.templateMaterialType == 2)
            ////////{
            ////////    rbProducts.Checked = true;
            ////////}

            ////////lueOwnerLocation.EditValue = AIMGlobal.templateOwnerLocationID;
            ////////lueTemplate.EditValue = AIMGlobal.templateID;

            //-----------------------------------------------------------------------------
            //5/20/19
            lueMaterial.EditValue = AIMGlobal.templateAIMMaterialID;
            lueOwner.EditValue = AIMGlobal.templateAIMOwnerID;
            lueOwnerLocation.EditValue = AIMGlobal.templateAIMOwnerLocationID;
            lueSupplier.EditValue = AIMGlobal.templateAIMSupplierID;
            lueSupplierLocation.EditValue = AIMGlobal.templateAIMSupplierLocationID;


            if (AIMGlobal.templateMaterialTypeID == (Int32)MaterialTypeEnum.Ingredient)
            {
                rbIngredients.Checked = true;
            }
            else
            {
                rbProducts.Checked = true;
            }
            //-----------------------------------------------------------------------------

        }


        //5/21/19
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        //4/5/18
        private void btnEditTemplate_Click(Object sender, EventArgs e)
        {
            setTemplateGlobals();
            Close();

            AIMGlobal.saveNewViewTemplate = false;
            AIMGlobal.templateType = _selectedTemplateType;
            ReportTemplate reportTemplate;
            AIMGlobal.open_form(reportTemplate = new ReportTemplate());

        }

        //4/5/18
        private void btnSaveTemplate_Click(Object sender, EventArgs e)
        {
            Int32 reportID = 0;
            AnalysisViewTemplateInfo verifyTemplate;                  //Task1148 DB260414

            try
            {
                //No query was selected so open view template screen 
                if ((Int32)lueTemplate.EditValue == 0)
                {

                    Stream streamNew = new System.IO.MemoryStream();

                    if (_selectedTemplateType == (Int32)EnumReportDisplayType.GridControl)
                    {
                        grdReportView.SaveLayoutToStream(streamNew);
                    }
                    else if (_selectedTemplateType == (Int32)EnumReportDisplayType.PivotControl)
                    {
                        pivotGridControl1.SaveLayoutToStream(streamNew);
                    }

                    AIMGlobal.streamNew = streamNew;
                    AIMGlobal.saveNewViewTemplate = true;
                    AIMGlobal.saveReportTemplateAs = false;
                    AIMGlobal.templateType = _selectedTemplateType;

                    //-----------------------------------------------------------------------------
                    //5/20/19 - send criteria as part of template-save
                    AIMGlobal.templateAIMMaterialID = (Int32)lueMaterial.EditValue;
                    AIMGlobal.templateAIMOwnerID = (Int32)lueOwner.EditValue;
                    AIMGlobal.templateAIMOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
                    AIMGlobal.templateAIMSupplierID = (Int32)lueSupplier.EditValue;
                    AIMGlobal.templateAIMSupplierLocationID = (Int32)lueSupplierLocation.EditValue;
                    AIMGlobal.templateMaterialTypeID = (Int32)MaterialTypeEnum.Ingredient;
                    if (rbProducts.Checked == true)
                    {
                        AIMGlobal.templateMaterialTypeID = (Int32)MaterialTypeEnum.Product;
                    }
                    //-----------------------------------------------------------------------------

                    Close();
                    ReportTemplate reportTemplate;
                    AIMGlobal.open_form(reportTemplate = new ReportTemplate());
                    return;
                }

                // Get current template here        db260414

                verifyTemplate = (AnalysisViewTemplateInfo)lueTemplate.GetSelectedDataRow();              //Task1148 DB260414 Start
                AnalysisViewTemplateInfo criteria = new AnalysisViewTemplateInfo();
                criteria = AIMManager.GetAnalysisViewTemplate(verifyTemplate.AnalysisViewTemplateID);

                if (criteria.CFCUserID != _userGlobal.CFCLoginUser.CFCUserID)
                {
                    DialogResult saveAsNewTeamplate = MessageBox.Show("Owner User " + criteria.UserName + " of this template does not match current user. Please have " + criteria.UserName + " edit current template or Save As New. Would you like to save as new?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (saveAsNewTeamplate == DialogResult.Yes)
                    {
                        btnEditTemplate_Click(btnSaveTemplate, EventArgs.Empty);
                    }
                    
                    return;
                }                                                                                       //Task1148 DB260414 End

                DialogResult dialogResult = MessageBox.Show("Overwrite " + lueTemplate.Text.ToString().TrimEnd() + "?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        //getting layout in stream
                        Stream stream = new System.IO.MemoryStream();

                        //------------------------------------------                        
                        if (_selectedTemplateType == (Int32)EnumReportDisplayType.GridControl)
                        {
                            grdReportView.SaveLayoutToStream(stream);
                        }
                        else if (_selectedTemplateType == (Int32)EnumReportDisplayType.PivotControl)
                        {
                            pivotGridControl1.SaveLayoutToStream(stream);
                        }
                        //------------------------------------------


                        stream.Position = 0;

                        //saving layout
                        AnalysisViewTemplateInfo viewTemplate = new AnalysisViewTemplateInfo();
                        viewTemplate = (AnalysisViewTemplateInfo)lueTemplate.GetSelectedDataRow();
                        StreamReader streamReader = new StreamReader(stream);
                        viewTemplate.AnalysisViewTemplateXML = streamReader.ReadToEnd();
                        viewTemplate.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                        //5/21/19
                        viewTemplate.templateAIMMaterialID = (Int32)lueMaterial.EditValue;
                        viewTemplate.templateAIMOwnerID = (Int32)lueOwner.EditValue;
                        viewTemplate.templateAIMOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
                        viewTemplate.templateAIMSupplierID = (Int32)lueSupplier.EditValue;
                        viewTemplate.templateAIMSupplierLocationID = (Int32)lueSupplierLocation.EditValue;
                        viewTemplate.templateMaterialTypeID = (Int32)MaterialTypeEnum.Ingredient;
                        if (rbProducts.Checked == true)
                        {
                            viewTemplate.templateMaterialTypeID = (Int32)MaterialTypeEnum.Product;
                        }

                        stream.Dispose();
                        AIMManager.SaveAnalysisViewTemplate(ref viewTemplate);
                        _mainMenu.MessageBoxCFCshow("View template has been updated.", true);

                        //5/21/19
                        deFromDate.Focus();
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
                    AIMGlobal.saveReportTemplateAs = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                _mainMenu.MessageBoxCFCshow("View template did not get updated because of an internal problem.", false);
                MessageBox.Show(ex.Message);
            }
        }

        //4/12/18
        private void reportView_FormClosed(Object sender, FormClosedEventArgs e)
        {
            saveUserDefaults();                                          //Task1075 DB250715
            AIMGlobal.templateReportID = 0;
        }

        private void btnClearMaterials_Click(Object sender, EventArgs e)
        {
            clearMaterials();
        }

        private void rbProducts_CheckedChanged(Object sender, EventArgs e)
        {

            //200606CM - changed this back, until decision is made about showing products  
            lblAimMaterial.Visible = true;                      //1/15/24
            lblMaterialCode.Visible = true;
            lblMaterialName.Visible = true;         //1/15/24
            lueMaterial.Visible = true;
            lueMaterialName.Visible = true;         //1/15/24
            if (rbProducts.Checked == true)
            {
                lblMaterialCode.Visible = false;
                lblMaterialName.Visible = false;        //1/15/24
                lueMaterial.Visible = false;
                lueMaterialName.Visible = false;
            }
            lueMaterial.EditValue = 0;                          //1/15/24            

            ////////////Int32 reportID = 0;
            ////////////Int32.TryParse(listboxReports.SelectedValue.ToString(), out reportID);
            ////////////lblSelectProduct.Visible = true;
            ////////////lueMaterial.Visible = true;
            ////////////if (rbProducts.Checked == true)
            ////////////{
            ////////////    DataBindProducts();
            ////////////    if (reportID == (Int32)ReportEnum.UserDefinedSamplePivotData || reportID == (Int32)ReportEnum.UserDefinedSampleData)
            ////////////    {
            ////////////        lblSelectProduct.Visible = false;
            ////////////        lueMaterial.Visible = false;
            ////////////    }
            ////////////}

        }

        //5/22/19
        private void btnResetToDefault_Click(object sender, EventArgs e)
        {

            lueMaterial.EditValue = 0;
            lueOwner.EditValue = 0;
            lueOwnerLocation.EditValue = 0;
            lueSupplier.EditValue = 0;
            lueSupplierLocation.EditValue = 0;
            rbIngredients.Checked = true;

            lueTemplate.EditValue = 0;

            if (_selectedTemplateType == (Int32)EnumReportDisplayType.GridControl)
            {

                grdReport.DataSource = null;
                grdReportView.RefreshData();
                foreach (var item in grdReportView.Columns)
                {
                    ((DevExpress.XtraGrid.Columns.GridColumn)item).Visible = true;
                }



            }
            else if (_selectedTemplateType == (Int32)EnumReportDisplayType.PivotControl)
            {
                pivotGridControl1.DataSource = null;
                pivotGridControl1.RefreshData();
            }


        }

        //6/3/20
        private void lueMaterial_EditValueChanged(object sender, EventArgs e)
        {
            dataBindSupplier();
            databindSupplierLocation();

            //1/15/24
            SyncValueLue(ref lueMaterial, ref lueMaterialName);
        }


        private void rbIngredients_CheckedChanged(object sender, EventArgs e)
        {
            //200606CM - changed this back, until decision is made about showing products
            //////////6/5/20
            //////////if (rbIngredients.Checked == true)
            //////////{
            //////////    DataBindMaterial((Int32)MaterialTypeEnum.Ingredient);
            //////////}

            //1/15/24
            if (rbIngredients.Checked == true)
            {
                lblAimMaterial.Visible = false;                      //1/15/24
                lblMaterialCode.Visible = false;
                lblMaterialName.Visible = false;            //1/15/24
                lueMaterial.Visible = false;
                lueMaterial.EditValue = 0;
                lueMaterialName.Visible = false;            //1/15/24                
                Int32 reportID;
                Int32.TryParse(listboxReports.SelectedValue.ToString(), out reportID);
                if (reportID == (Int32)ReportEnum.SamplesSubmittedReport)
                {
                    lblAimMaterial.Visible = true;                      //1/15/24
                    lblMaterialCode.Visible = true;
                    lblMaterialName.Visible = true;         //1/15/24
                    lueMaterial.Visible = true;
                    lueMaterialName.Visible = true;         //1/15/24
                }
            }


        }


        //1/18/20
        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAll.Checked == true)
            {
                ckMean.Checked = true;
                ckMedian.Checked = true;
                ckStdDev.Checked = true;
                ckCVpercent.Checked = true;
                ckCount.Checked = true;
                ckSum.Checked = true;
                ckMeanMinus.Checked = true;
                ckMeanPlus.Checked = true;
                ckMin.Checked = true;
                ckMax.Checked = true;
            }
            if (ckAll.Checked == false)
            {
                ckMean.Checked = false;
                ckMedian.Checked = false;
                ckStdDev.Checked = false;
                ckCVpercent.Checked = false;
                ckCount.Checked = false;
                ckSum.Checked = false;
                ckMeanMinus.Checked = false;
                ckMeanPlus.Checked = false;
                ckMin.Checked = false;
                ckMax.Checked = false;
            }
        }

        private void ckMean_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMean.Checked == false)
            {
                ckAll.Checked = false;
            }
        }

        private void ckMedian_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMedian.Checked == false)
            {
                ckAll.Checked = false;
            }
        }

        private void ckStdDev_CheckedChanged(object sender, EventArgs e)
        {
            if (ckStdDev.Checked == false)
            {
                ckAll.Checked = false;
            }
        }

        private void ckCVpercent_CheckedChanged(object sender, EventArgs e)
        {
            if (ckCVpercent.Checked == false)
            {
                ckAll.Checked = false;
            }
        }

        private void ckCount_CheckedChanged(object sender, EventArgs e)
        {
            if (ckCount.Checked == false)
            {
                ckAll.Checked = false;
            }
        }

        private void ckMeanMinus_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMeanMinus.Checked == false)
            {
                ckAll.Checked = false;
            }
        }

        private void ckMeanPlus_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMeanPlus.Checked == false)
            {
                ckAll.Checked = false;
            }
        }

        private void ckMin_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMin.Checked == false)
            {
                ckAll.Checked = false;
            }
        }

        private void ckMax_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMax.Checked == false)
            {
                ckAll.Checked = false;
            }
        }

        private void ckSum_CheckedChanged(object sender, EventArgs e)
        {
            if (ckSum.Checked == false)
            {
                ckAll.Checked = false;
            }
        }


        private void ckAllGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAllGroup.Checked == true)
            {
                ckMeanGroup.Checked = true;
                ckMedianGroup.Checked = true;
                ckStdDevGroup.Checked = true;
                ckCVpercentGroup.Checked = true;
                ckCountGroup.Checked = true;
                ckSumGroup.Checked = true;
                ckMeanMinusGroup.Checked = true;
                ckMeanPlusGroup.Checked = true;
                ckMinGroup.Checked = true;
                ckMaxGroup.Checked = true;
            }

            if (ckAllGroup.Checked == false)
            {
                ckMeanGroup.Checked = false;
                ckMedianGroup.Checked = false;
                ckStdDevGroup.Checked = false;
                ckCVpercentGroup.Checked = false;
                ckCountGroup.Checked = false;
                ckSumGroup.Checked = false;
                ckMeanMinusGroup.Checked = false;
                ckMeanPlusGroup.Checked = false;
                ckMinGroup.Checked = false;
                ckMaxGroup.Checked = false;
            }
        }


        private void ckMeanGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMeanGroup.Checked == false)
            {
                ckAllGroup.Checked = false;
            }
        }

        private void ckMedianGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMedianGroup.Checked == false)
            {
                ckAllGroup.Checked = false;
            }
        }

        private void ckStdDevGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (ckStdDevGroup.Checked == false)
            {
                ckAllGroup.Checked = false;
            }
        }

        private void ckCVpercentGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (ckCVpercentGroup.Checked == false)
            {
                ckAllGroup.Checked = false;
            }
        }

        private void ckCountGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (ckCountGroup.Checked == false)
            {
                ckAllGroup.Checked = false;
            }
        }

        private void ckSumGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (ckSumGroup.Checked == false)
            {
                ckAllGroup.Checked = false;
            }
        }

        private void ckMeanMinusGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMeanMinusGroup.Checked == false)
            {
                ckAllGroup.Checked = false;
            }
        }

        private void ckMeanPlusGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMeanPlusGroup.Checked == false)
            {
                ckAllGroup.Checked = false;
            }
        }

        private void ckMinGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMinGroup.Checked == false)
            {
                ckAllGroup.Checked = false;
            }
        }

        private void ckMaxGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMaxGroup.Checked == false)
            {
                ckAllGroup.Checked = false;
            }
        }

        private void rbVersion1GroupByMaterial_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVersion1GroupByMaterial.Checked == true)
            {
                pnlShowHideTotalFields.Size = new Size(333, 185);
            }
        }

        //1/19/21
        private void rbVersion2GroupByMaterialOwnerLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVersion2GroupByMaterialOwnerLocation.Checked == true)
            {
                pnlShowHideTotalFields.Size = new Size(333, 185);
            }
        }

        //1/19/21
        private void rbVersion3GroupByMaterialSupplierLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVersion3GroupByMaterialSupplierLocation.Checked == true)
            {
                pnlShowHideTotalFields.Size = new Size(333, 366);
                lblGrandTotal.Visible = true;
            }
            else
            {
                pnlShowHideTotalFields.Size = new Size(333, 185);
                lblGrandTotal.Visible = false;
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                AnalysisReportsInfo analysisSummaryToGet = new AnalysisReportsInfo();
                List<AnalysisReportsInfo> analysisSummaryToSet = new List<AnalysisReportsInfo>();
                List<AimNutrientRefAnalyzerNutrientOrphanInfo> nutrientOrphans = new List<AimNutrientRefAnalyzerNutrientOrphanInfo>();
                List<AimMaterialRefAnalyzerMaterialOrphanInfo> materialOrphans = new List<AimMaterialRefAnalyzerMaterialOrphanInfo>();
                List<ExportBatchMaintenanceReportInfo> exportBatch = new List<ExportBatchMaintenanceReportInfo>();
                List<AimSourceLocationGuaranteeReportInfo> sourceLocationGuarantee = new List<AimSourceLocationGuaranteeReportInfo>();
                AimSourceLocationGuaranteeReportInfo sourceLocationGuaranteeParams = new AimSourceLocationGuaranteeReportInfo();
                List<SampleComparisonReportInfo> sampleResult = new List<SampleComparisonReportInfo>();
                List<ChartInfo> trend = new List<ChartInfo>();
                List<ManufacturedProductComplianceReportInfo> mfgProductCompliance = new List<ManufacturedProductComplianceReportInfo>();
                List<SampleActivityReportInfo> sampleActivity = new List<SampleActivityReportInfo>();
                List<SampleResultsBySupplierInfo> sampleResults = new List<SampleResultsBySupplierInfo>();
                List<SampleHeaderInfo> sampleOwnerData = new List<SampleHeaderInfo>();              //6/27/22
                List<SampleSubmittedSamplesReportInfo> sampleTestType = new List<SampleSubmittedSamplesReportInfo>();           //8/16/23
                List<SampleAnalysisRequestFormInfo> sampleRequestForm = new List<SampleAnalysisRequestFormInfo>();           //9/1/23
                List<ExportBatchLogInfo> exportLog = new List<ExportBatchLogInfo>();                //TASK951 6/3/24
                List<TestGridInfo> testGrid = new List<TestGridInfo>();                                 //Task1150 DB260108
                String startDate = String.Empty;
                String endDate = String.Empty;
                Int32 sampleResultOption = 0;                 //Task1076 DB250711
                Boolean isGrouped = false;                      //Task1150 DB260109
                //Test - remove next 3 rows
                //deFromDate.EditValue = "12/01/2020";
                //deToDate.EditValue = "2/24/2021";
                //lueMaterial.EditValue = 305;
                //lueSupplier.EditValue = 42;
                //lueSupplierLocation.EditValue = 93;

                //4/15/22
                timer1.Stop();

                if ((Int32)listboxReports.SelectedValue == 0)
                {
                    MessageBox.Show("Please select Report", "Warning...");
                    return;
                }
                Int32 reportID = 0;
                Int32.TryParse(listboxReports.SelectedValue.ToString(), out reportID);
                if (checkCriteria(reportID) != 0)
                {
                    return;
                }

                if (reportID == (Int32)ReportEnum.SampleActivityReport ||
                    reportID == (Int32)ReportEnum.SampleResultsBySupplier ||
                    reportID == (Int32)ReportEnum.UserDefinedSampleData ||
                    reportID == (Int32)ReportEnum.UserDefinedSamplePivotData ||
                    reportID == (Int32)ReportEnum.SamplesSubmittedReport ||          //8/16/23
                    reportID == (Int32)ReportEnum.AnalysisCost)                     //Task1150 db260108                   

                {
                    if (deFromDate.Text == "")
                    {
                        MessageBox.Show("Please enter a From date", "Warning...");
                        deFromDate.Focus();
                        return;
                    }
                }

                //3/16/20 - added this because, for Smithfield, report can take a while to run for all materials
                if (reportID == (Int32)ReportEnum.SampleComparisonReport && lueMaterial.Text == "--All--")
                {
                    DialogResult dr = new DialogResult();
                    {
                        dr = MessageBox.Show("Running this report for ALL materials may take a few minutes. Continue running report?", "Warning ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr != DialogResult.Yes)
                        {
                            lueMaterial.Focus();
                            return;
                        }
                    }
                }

                AIMGlobal.SplashScreenShow();
                AIMGlobal.showDate = true;
                if (deFromDate.Text == "" || deToDate.Text == "")
                {
                    AIMGlobal.showDate = false;

                    if (deFromDate.Text == "")
                    {
                        deFromDate.EditValue = "1/1/2000";
                    }
                    if (deToDate.Text == "")
                    {
                        deToDate.EditValue = DateTime.Now;
                    }
                }

                //2/16/21
                _groupProducts = false;

                //9/6/23
                _noData = false;

                //-----------------------------------------------
                //4/20/21 - currently, the nutrients can be sorted by name or code in the following reports:  
                // Analysis Summary, Export Pending Batch Detail, Nutrient Cross-Reference Orphans, Sample Comparison Report, Supplier Location Guarantees,
                // AnalysisPivotReport, 
                AIMGlobal.sortNutrientsBy = "Code";
                if (rbSortNutrientName.Checked == true)
                {
                    AIMGlobal.sortNutrientsBy = "Name";
                }
                //-----------------------------------------------


                //9/1/23
                AIMGlobal.reportID = reportID;

                //9/1/23
                if (pnlMaterialType.Visible == true)
                {
                    if (rbIngredients.Checked == true)
                    {
                        AIMGlobal.materialTypeID = (Int32)MaterialTypeEnum.Ingredient;
                    }
                    else
                    {
                        AIMGlobal.materialTypeID = (Int32)MaterialTypeEnum.Product;
                    }
                }


                switch (reportID)
                {

                    //------------------------------------------------------
                    // ANALYSIS DATE COMPARISON REPORT
                    #region Analysis Date Comparison Report
                    case (Int32)ReportEnum.AnalysisDateComparisonReport:
                        setAnalysisVariables(analysisSummaryToGet);
                        if (rbProducts.Checked == true)
                        {
                            analysisSummaryToSet = AIMManager.GetAnalysisSummaryForAnalysisDateComparison_Product(ref analysisSummaryToGet);
                        }
                        else
                        {
                            analysisSummaryToSet = AIMManager.GetAnalysisSummaryForAnalysisDateComparison(ref analysisSummaryToGet);
                        }
                        ReportFunctions.DataSetAnalysisSummary = analysisSummaryToSet;
                        break;
                    #endregion

                    //------------------------------------------------------
                    // ANALYSIS DETAIL
                    #region Analysis Detail
                    case (Int32)ReportEnum.AnalysisDetail:
                        setAnalysisVariables(analysisSummaryToGet);
                        if (rbProducts.Checked == true)
                        {
                            analysisSummaryToSet = AIMManager.GetAnalysisDetail_Product(ref analysisSummaryToGet, AIMCoreGlobal.AIMDatabaseID);
                        }
                        else
                        {
                            analysisSummaryToSet = AIMManager.GetAnalysisDetail(ref analysisSummaryToGet, AIMCoreGlobal.AIMDatabaseID);
                        }
                        ReportFunctions.DataSetAnalysisSummary = analysisSummaryToSet;
                        break;
                    #endregion

                    //------------------------------------------------------
                    // ANALYSIS SUPPLIER COMPARISON
                    #region Analysis Supplier Comparison
                    case (Int32)ReportEnum.AnalysisSupplierComparison:
                        setAnalysisVariables(analysisSummaryToGet);
                        analysisSummaryToSet = AIMManager.GetAnalysisSourceComparison(ref analysisSummaryToGet);
                        ReportFunctions.DataSetAnalysisSummary = analysisSummaryToSet;
                        break;
                    #endregion

                    //------------------------------------------------------
                    // ANALYSIS PIVOT REPORT and ANALYSIS PIVOT SUMMARY
                    #region Analysis Pivot Report and Analysis Pivot Summary
                    case (Int32)ReportEnum.AnalysisPivotReport:
                    case (Int32)ReportEnum.AnalysisPivotSummary:

                    //1/19/21
                    case (Int32)ReportEnum.AnalysisPivotReportGroupSummary:

                        //4/28/21 - added so dates are not cleared after report is run
                        AIMGlobal.showDate = true;

                        AIMGlobal.fromDate = deFromDate.Text;
                        AIMGlobal.toDate = deToDate.Text;

                        AIMGlobal.dryMatterText = "(Raw Data, As-Is Basis)";
                        if (rbDryMatter.Checked == true)
                        {
                            AIMGlobal.dryMatterText = "(Dry Matter Base)";
                        }

                        if (lueMaterial.Text != "--All--")
                        {
                            AIMGlobal.analyzerMaterialName = " - " + lueMaterial.Text;
                        }
                        else
                        {
                            AIMGlobal.analyzerMaterialName = "";
                        }

                        _materialIDList = "";
                        _materialIDList2 = "";
                        _materialIDList3 = "";
                        if (reportID == (Int32)ReportEnum.AnalysisPivotSummary)
                        {
                            populateMaterialList();
                        }


                        //--------------------------------------------------------
                        //1/18/21 - set globals depending on what user selects

                        // These are for the end-of-report summary totals
                        AIMGlobal.showMean = true;
                        AIMGlobal.showMedian = true;
                        AIMGlobal.showStdDev = true;
                        AIMGlobal.showCVpercent = true;
                        AIMGlobal.showCount = true;
                        AIMGlobal.showSum = true;
                        AIMGlobal.showMeanMinus = true;
                        AIMGlobal.showMeanPlus = true;
                        AIMGlobal.showMin = true;
                        AIMGlobal.showMax = true;
                        if (ckMean.Checked == false) { AIMGlobal.showMean = false; }
                        if (ckMedian.Checked == false) { AIMGlobal.showMedian = false; }
                        if (ckStdDev.Checked == false) { AIMGlobal.showStdDev = false; }
                        if (ckCVpercent.Checked == false) { AIMGlobal.showCVpercent = false; }
                        if (ckCount.Checked == false) { AIMGlobal.showCount = false; }
                        if (ckSum.Checked == false) { AIMGlobal.showSum = false; }
                        if (ckMeanMinus.Checked == false) { AIMGlobal.showMeanMinus = false; }
                        if (ckMeanPlus.Checked == false) { AIMGlobal.showMeanPlus = false; }
                        if (ckMin.Checked == false) { AIMGlobal.showMin = false; }
                        if (ckMax.Checked == false) { AIMGlobal.showMax = false; }

                        // These are for the sub-group summary totals
                        AIMGlobal.showMeanGroup = true;
                        AIMGlobal.showMedianGroup = true;
                        AIMGlobal.showStdDevGroup = true;
                        AIMGlobal.showCVpercentGroup = true;
                        AIMGlobal.showCountGroup = true;
                        AIMGlobal.showSumGroup = true;
                        AIMGlobal.showMeanMinusGroup = true;
                        AIMGlobal.showMeanPlusGroup = true;
                        AIMGlobal.showMinGroup = true;
                        AIMGlobal.showMaxGroup = true;
                        if (ckMeanGroup.Checked == false) { AIMGlobal.showMeanGroup = false; }
                        if (ckMedianGroup.Checked == false) { AIMGlobal.showMedianGroup = false; }
                        if (ckStdDevGroup.Checked == false) { AIMGlobal.showStdDevGroup = false; }
                        if (ckCVpercentGroup.Checked == false) { AIMGlobal.showCVpercentGroup = false; }
                        if (ckCountGroup.Checked == false) { AIMGlobal.showCountGroup = false; }
                        if (ckSumGroup.Checked == false) { AIMGlobal.showSumGroup = false; }
                        if (ckMeanMinusGroup.Checked == false) { AIMGlobal.showMeanMinusGroup = false; }
                        if (ckMeanPlusGroup.Checked == false) { AIMGlobal.showMeanPlusGroup = false; }
                        if (ckMinGroup.Checked == false) { AIMGlobal.showMinGroup = false; }
                        if (ckMaxGroup.Checked == false) { AIMGlobal.showMaxGroup = false; }


                        //--------------------------------------------------------
                        setAnalysisVariables(analysisSummaryToGet);


                        //1/19/21
                        //analysisSummaryToSet = AIMManager.GetAnalysisPivot(ref analysisSummaryToGet, rbDryMatter.Checked, _materialIDList, _materialIDList2, _materialIDList3);
                        if (rbVersion3GroupByMaterialSupplierLocation.Checked == true)
                        {
                            AIMGlobal.groupbyOption = 3;

                            //2/24/21
                            //analysisSummaryToSet = AIMManager.GetAnalysisPivotSupplierLocation(ref analysisSummaryToGet, rbDryMatter.Checked, _materialIDList, _materialIDList2, _materialIDList3);
                            analysisSummaryToSet = AIMManager.GetAnalysisPivot(ref analysisSummaryToGet, rbDryMatter.Checked, _materialIDList, _materialIDList2, _materialIDList3);

                            if (ckMeanGroup.Checked == false && ckMedianGroup.Checked == false &&
                                ckStdDevGroup.Checked == false && ckCVpercentGroup.Checked == false &&
                                ckCountGroup.Checked == false && ckSumGroup.Checked == false &&
                                ckMeanMinusGroup.Checked == false && ckMeanPlusGroup.Checked == false &&
                                ckMinGroup.Checked == false && ckMaxGroup.Checked == false)
                            {
                                reportID = (Int32)ReportEnum.AnalysisPivotReportGroupSummaryWithNoSubTotals;
                            }
                            else
                            {
                                reportID = (Int32)ReportEnum.AnalysisPivotReportGroupSummary;
                            }

                        }
                        else if (rbVersion2GroupByMaterialOwnerLocation.Checked == true)
                        {
                            AIMGlobal.groupbyOption = 2;
                            analysisSummaryToSet = AIMManager.GetAnalysisPivotOwnerLocation(ref analysisSummaryToGet, rbDryMatter.Checked, _materialIDList, _materialIDList2, _materialIDList3);
                        }
                        else
                        {
                            AIMGlobal.groupbyOption = 1;
                            analysisSummaryToSet = AIMManager.GetAnalysisPivot(ref analysisSummaryToGet, rbDryMatter.Checked, _materialIDList, _materialIDList2, _materialIDList3);


                        }

                        ReportFunctions.DataSetAnalysisSummary = analysisSummaryToSet;
                        break;
                    #endregion

                    //------------------------------------------------------
                    // ANALYSIS SUMMARY
                    #region Analysis Summary
                    case (Int32)ReportEnum.AnalysisSummary:
                        setAnalysisVariables(analysisSummaryToGet);
                        if (rbProducts.Checked == true)
                        {
                            analysisSummaryToSet = AIMManager.GetAnalysisSummary_Product(ref analysisSummaryToGet, AIMCoreGlobal.AIMDatabaseID);
                        }
                        else
                        {
                            analysisSummaryToSet = AIMManager.GetAnalysisSummary(ref analysisSummaryToGet, AIMCoreGlobal.AIMDatabaseID);
                        }
                        ReportFunctions.DataSetAnalysisSummary = analysisSummaryToSet;
                        break;
                    #endregion

                    //------------------------------------------------------
                    // EXPORT PENDING BATCH DETAIL
                    #region Export Pending Batch Detail
                    case (Int32)ReportEnum.ExportBatchDetail:
                        exportBatch = AIMManager.GetExportBatchDetailReportData();
                        CFCAIMCore.Global.DataSetExportBatchDetail = exportBatch;
                        break;
                    #endregion

                    //------------------------------------------------------
                    // EXPORT LOG BATCH DETAIL                                              //TASK951 6/3/24
                    #region Export Log
                    case (Int32)ReportEnum.ExportLog:

                        currentRowNum = 0;
                        exportLog = AIMManager.GetExportBatchLog((DateTime)deFromDate.DateTime, (DateTime)deToDate.DateTime);
                        if (grdExportLogView != null)
                        {
                            Int32.TryParse(grdExportLogView.FocusedRowHandle.ToString(), out currentRowNum);
                        }
                        pnlGrid.Visible = true;
                        pnlGrid.Location = new Point(279, 210);
                        pnlGrid.Size = new Size(panelWidth, panelHeight);
                        grdExportLog.Visible = true;
                        grdExportLog.DataSource = exportLog;
                        grdExportLogView.FocusedRowHandle = currentRowNum;
                        break;
                    #endregion                        

                    //------------------------------------------------------
                    // MFG PRODUCT COMPLIANCE REPORT
                    #region Mfg Product Compliance Report
                    case (Int32)ReportEnum.ManufacturedProductComplianceReport:

                        DateTime fromDt = DateTime.MinValue;
                        DateTime toDt = DateTime.MinValue;
                        DateTime.TryParse(deFromDate.EditValue.ToString(), out fromDt);
                        DateTime.TryParse(deToDate.EditValue.ToString(), out toDt);

                        if (chkShowViolations.Checked == true)
                        {
                            mfgProductCompliance = AIMManager.GetManufacturedProductComplianceViolations((Int32)_mainMenu.lueDatabase.EditValue, toDt, fromDt);
                        }
                        else
                        {
                            mfgProductCompliance = AIMManager.GetManufacturedProductComplianceReportData((Int32)_mainMenu.lueDatabase.EditValue, toDt, fromDt);
                        }

                        ReportFunctions.DataSetMfgProductCompliance = mfgProductCompliance;
                        break;
                    #endregion

                    //------------------------------------------------------
                    // MATERIAL CROSS-REFERENCE ORPHANS
                    #region Material Cross-Reference Orphas                    
                    case (Int32)ReportEnum.AimMaterialRefAnalyzerMaterialOphans:

                        //4/15/22
                        //materialOrphans = AIMManager.GetAimAnalyzerMaterialRefReportData();
                        if (rbAnalyzer.Checked == true)
                        {
                            if (rbIngredients.Checked == true)
                            {
                                AIMGlobal.orphanCrossRefReport = 1;
                                materialOrphans = AIMManager.GetMaterialOrphanReportData((Int32)EnumOrphanXRefReport.AnalyzerIngredient);
                            }
                            else
                            {
                                AIMGlobal.orphanCrossRefReport = 2;
                                materialOrphans = AIMManager.GetMaterialOrphanReportData((Int32)EnumOrphanXRefReport.AnalyzerProduct);
                            }
                        }
                        else
                        {
                            if (rbIngredients.Checked == true)
                            {
                                AIMGlobal.orphanCrossRefReport = 3;
                                materialOrphans = AIMManager.GetMaterialOrphanReportData((Int32)EnumOrphanXRefReport.FormulationIngredient);
                            }
                            else
                            {
                                AIMGlobal.orphanCrossRefReport = 4;
                                materialOrphans = AIMManager.GetMaterialOrphanReportData((Int32)EnumOrphanXRefReport.FormulationProduct);
                            }
                        }

                        ReportFunctions.DataSetAimAnalyzerMaterialRef = materialOrphans;
                        break;
                    #endregion

                    //------------------------------------------------------
                    // NUTRIENT CROSS-REFERENCE ORPHANS
                    #region Nutrient Cross-Reference Orphans
                    case (Int32)ReportEnum.AimNutrientRefAnalyzerNutrientOphans:
                        nutrientOrphans = AIMManager.GetAimAnalyzerNutrientRefReportData();
                        ReportFunctions.DataSetAimAnalyzerNutrientRef = nutrientOrphans;
                        break;
                    #endregion

                    //------------------------------------------------------
                    // SAMPLE ACTIVITY REPORT
                    #region Sample Activity Report
                    case (Int32)ReportEnum.SampleActivityReport:

                        SampleActivityReportInfo activity = new SampleActivityReportInfo();

                        String txtdt = "";
                        String amPM = "";

                        //---------------------------
                        txtdt = deFromDate.DateTime.ToString("yyyy-MM-dd ");
                        txtdt += teFromTime.Time.ToString("HH:mm:ss");
                        amPM = ".000";
                        if (teFromTime.Time.ToString("tt") == "PM")
                        {
                            amPM = ".999";
                        }
                        txtdt += amPM;
                        activity.FromDateString = txtdt;

                        //---------------------------
                        if (deToDate.Text != "")
                        {
                            txtdt = deToDate.DateTime.ToString("yyyy-MM-dd ");
                            txtdt += teToTime.Time.ToString("HH:mm:ss");
                            amPM = ".000";
                            if (teToTime.Time.ToString("tt") == "PM")
                            {
                                amPM = ".999";
                            }
                            txtdt += amPM;
                            activity.ToDateString = txtdt;
                        }

                        //4/26/22 - option was showing on screen but code never added to report
                        if (rbProducts.Checked == true)
                        {
                            activity.MaterialTypeID = 2;
                        }
                        else
                        {
                            activity.MaterialTypeID = 1;
                        }

                        activity.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                        //4/26/22
                        //sampleActivity = AIMManager.GetSampleActivity(ref activity);
                        if (rbQCYes.Checked == true)
                        {
                            AIMGlobal.sampleActivityReportOption = 2;
                            sampleActivity = AIMManager.GetSampleActivityQualityControl(ref activity);
                        }
                        else
                        {
                            AIMGlobal.sampleActivityReportOption = 1;
                            sampleActivity = AIMManager.GetSampleActivity(ref activity);
                        }
                        ReportFunctions.DataSetSampleActivity = sampleActivity;

                        break;
                    #endregion

                    //------------------------------------------------------
                    // SAMPLE COMPARISON REPORT
                    #region Sample Comparison Report
                    case (Int32)ReportEnum.SampleComparisonReport:

                        AIMGlobal.dataSourceName1 = lueFirstDataSource.Text.ToString();
                        AIMGlobal.dataSourceName2 = lueSecondDataSource.Text.ToString();

                        //3/12/20
                        _datasource1 = (Int32)lueFirstDataSource.GetColumnValue("DataSourceTypeID");
                        _datasource2 = (Int32)lueSecondDataSource.GetColumnValue("DataSourceTypeID");


                        SampleComparisonReportInfo compare = new SampleComparisonReportInfo();
                        compare.DataSource1 = _datasource1;
                        compare.DataSource2 = _datasource2;
                        //compare.Type1 = _datasourcetype1;             //3/12/20 - types no longer used
                        //compare.Type2 = _datasourcetype2;             //3/12/20 - types no longer used

                        //compare.AimMaterialID = (Int32)lueMaterial.EditValue;
                        if (rbIngredients.Checked == true)
                        {
                            compare.AimMaterialID = (Int32)lueMaterial.EditValue;
                        }
                        else if (rbProducts.Checked == true && lueMaterial.EditValue.ToString() == "--All--")
                        {

                        }
                        else
                        {
                            compare.AimMaterialID = (Int32)lueMaterial.EditValue;
                        }

                        compare.AimMaterialID = (Int32)lueMaterial.EditValue;
                        compare.SampleOwnerID = (Int32)lueOwner.EditValue;
                        compare.AIMOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
                        compare.AIMSourceID = (Int32)lueSupplier.EditValue;
                        compare.AIMSourceLocationID = (Int32)lueSupplierLocation.EditValue;
                        compare.ToDate = (DateTime)deToDate.DateTime;
                        compare.FromDate = (DateTime)deFromDate.DateTime;
                        compare.TicketNumber = txtTicket.Text.Trim();

                        compare.BothMustHaveValues = false;
                        if (rbBothHaveValues.Checked == true)
                        {
                            compare.BothMustHaveValues = true;
                        }

                        sampleResult = AIMManager.GetSampleComparison(ref compare);
                        ReportFunctions.DataSetSampleComparison = sampleResult;
                        break;
                    #endregion

                    //------------------------------------------------------
                    // SAMPLE RESULTS BY SUPPLIER
                    #region Sample Results by Supplier
                    //2/12/19
                    case (Int32)ReportEnum.SampleResultsBySupplier:

                        AIMGlobal.showDate = true;
                        SampleResultsBySupplierInfo results = new SampleResultsBySupplierInfo();
                        results.ToDate = (DateTime)deToDate.DateTime;
                        results.FromDate = (DateTime)deFromDate.DateTime;
                        if ((Int32)lueOwner.EditValue != 0)
                        {
                            results.AIMOwnerID = (Int32)lueOwner.EditValue;
                        }
                        if ((Int32)lueOwnerLocation.EditValue != 0)
                        {
                            results.AIMOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
                        }
                        if ((Int32)lueSupplier.EditValue != 0)
                        {
                            results.AIMSourceID = (Int32)lueSupplier.EditValue;
                        }
                        if ((Int32)lueSupplierLocation.EditValue != 0)
                        {
                            results.AIMSourceLocationID = (Int32)lueSupplierLocation.EditValue;
                        }
                        results.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                        sampleResults = AIMManager.GetSampleResultsBySupplier(ref results);
                        if (sampleResults.Count == 0)
                        {
                            AIMGlobal.SplashScreenHide();
                            MessageBox.Show("There are no samples for this criteria.", "Information...");
                            return;
                        }
                        ReportFunctions.DataSetSampleResultsBySupplier = sampleResults;
                        break;
                    #endregion                                     

                    //------------------------------------------------------
                    // SUPPLIER LOCATION GUARANTEES
                    #region Supplier Location Guarantees
                    case (Int32)ReportEnum.SupplierLocationGuarantees:
                        sourceLocationGuaranteeParams.ToDate = (DateTime)deToDate.DateTime;
                        sourceLocationGuaranteeParams.FromDate = (DateTime)deFromDate.DateTime;


                        if ((Int32)lueMaterial.EditValue != 0)
                        {
                            sourceLocationGuaranteeParams.AIMMaterialID = (Int32)lueMaterial.EditValue;
                        }
                        if ((Int32)lueSupplier.EditValue != 0)
                        {
                            sourceLocationGuaranteeParams.AimSupplierID = (Int32)lueSupplier.EditValue;
                        }
                        if ((Int32)lueSupplierLocation.EditValue != 0)
                        {
                            sourceLocationGuaranteeParams.AimSupplierLocationID = (Int32)lueSupplierLocation.EditValue;
                        }

                        sourceLocationGuaranteeParams.Flag = 0;
                        if (chkOutOfRange.Checked == true)
                        {
                            sourceLocationGuaranteeParams.Flag = 1;
                        }

                        AIMGlobal.groupByLoadNumber = false;
                        if (chkGroupByLoadNumber.Checked == true)
                        {
                            AIMGlobal.groupByLoadNumber = true;
                        }

                        //1/18/23
                        AIMGlobal.checkOutOfRange = false;
                        if (chkOutOfRange.Checked == true)
                        {
                            AIMGlobal.checkOutOfRange = true;
                            if (rbAdjustedValue.Checked == true)
                            {
                                AIMGlobal.resultOrAdjustedValue = "A";
                            }
                            else
                            {
                                AIMGlobal.resultOrAdjustedValue = "R";
                            }
                        }

                        //6/26/23
                        sourceLocationGuaranteeParams.AdjustedOrResult = AIMGlobal.resultOrAdjustedValue;


                        //1/18/23 - commented "product" part -- not used (currently)
                        ////4/20/18                        
                        //if (rbProducts.Checked == true)
                        //{
                        //    sourceLocationGuarantee = AIMManager.GetSourceLocationGuaranteeReportData_Product(ref sourceLocationGuaranteeParams);
                        //}
                        //else
                        //{
                        //    sourceLocationGuarantee = AIMManager.GetSourceLocationGuaranteeReportData(ref sourceLocationGuaranteeParams);                            
                        //}
                        sourceLocationGuarantee = AIMManager.GetSourceLocationGuaranteeReportData(ref sourceLocationGuaranteeParams);


                        //2/21/17 - commented, not using nutrient checkboxlist
                        ////sourceLocationGuarantee = AIMManager.GetSourceLocationGuaranteeReportData(ref sourceLocationGuaranteeParams);
                        //AIMGlobal.specificNutrients = false;
                        //populateNutrientList();
                        //if (_nutrientID1 == 0 && _nutrientID2 == 0)
                        //{
                        //    sourceLocationGuarantee = AIMManager.GetSourceLocationGuaranteeReportData(ref sourceLocationGuaranteeParams);
                        //}
                        //else
                        //{
                        //    AIMGlobal.specificNutrients = true;
                        //    sourceLocationGuaranteeParams.AIMNutrientID1 = _nutrientID1;
                        //    sourceLocationGuaranteeParams.AIMNutrientID2 = _nutrientID2;
                        //    sourceLocationGuarantee = AIMManager.GetSourceLocationGuaranteeSpecificNutrientsReportData(ref sourceLocationGuaranteeParams);
                        //}		

                        //11/21/22 - get number of decimals for resultvalue from system settings
                        SystemSettingsInfo systemSettings;
                        systemSettings = AIMManager.GetSystemSettings();
                        if (systemSettings != null)
                        {
                            AIMGlobal.numberOfDecimals = systemSettings.GuaranteeReportDecimals;
                        }



                        ReportFunctions.DataSetSourceLocationGuarantee = sourceLocationGuarantee;
                        break;
                    #endregion

                    //------------------------------------------------------
                    // USER-DEFINED SAMPLE DATA                    
                    #region User-Defined Sample Data

                    case (Int32)ReportEnum.UserDefinedSampleData:

                        //2/16/21
                        if (rbGroupMaterialsYes.Checked == true)
                        {
                            _groupProducts = true;
                        }

                        if (btnSampleWithoutResults.Checked == true)                       //Task1076 DB250711 Start
                        {
                            sampleResultOption = 1;
                        }
                        else if (btnSampleWithBoth.Checked == true)
                        {
                            sampleResultOption = 2;
                        }
                        else
                        {
                            sampleResultOption = 0;                                         //Default is 0, but we will set it anyways to be safe
                        }                                                                   //Task1076 DB250711 End





                        AIMGlobal.showDate = true;
                        currentRowNum = 0;
                        List<SampleCustomReportInfo> sampleData = new List<SampleCustomReportInfo>();
                        if (rbProducts.Checked == true)
                        {
                            //2/16/21
                            //sampleData = AIMManager.GetSampleCustomProduct((DateTime)deFromDate.DateTime, (DateTime)deToDate.DateTime, (Int32)MaterialTypeEnum.Product, (Int32)lueOwnerLocation.EditValue);
                            //Task1086 DB250711 sampleData = AIMManager.GetSampleCustomProduct((DateTime)deFromDate.DateTime, (DateTime)deToDate.DateTime, (Int32)MaterialTypeEnum.Product, (Int32)lueOwnerLocation.EditValue, _groupProducts);
                            sampleData = AIMManager.GetSampleCustomProduct((DateTime)deFromDate.DateTime, (DateTime)deToDate.DateTime, (Int32)MaterialTypeEnum.Product, (Int32)lueOwnerLocation.EditValue, _groupProducts, sampleResultOption);     //Task1086 DB250711 
                        }
                        else
                        {
                            //2/16/21
                            //sampleData = AIMManager.GetSampleCustom((DateTime)deFromDate.DateTime, (DateTime)deToDate.DateTime, (Int32)MaterialTypeEnum.Ingredient, (Int32)lueOwnerLocation.EditValue);
                            //Task1086 DB250711  sampleData = AIMManager.GetSampleCustom((DateTime)deFromDate.DateTime, (DateTime)deToDate.DateTime, (Int32)MaterialTypeEnum.Ingredient, (Int32)lueOwnerLocation.EditValue, _groupProducts);
                            sampleData = AIMManager.GetSampleCustom((DateTime)deFromDate.DateTime, (DateTime)deToDate.DateTime, (Int32)MaterialTypeEnum.Ingredient, (Int32)lueOwnerLocation.EditValue, _groupProducts, sampleResultOption);     //Task1086 DB250711  
                        }


                        if (grdReportView != null)
                        {
                            Int32.TryParse(grdReportView.FocusedRowHandle.ToString(), out currentRowNum);
                        }
                        grdReport.DataSource = sampleData;
                        grdReportView.FocusedRowHandle = currentRowNum;

                        //Get alternate column names                        +
                        _names = AIMManager.GetReportAlternateNames();
                        foreach (var item in _names)
                        {
                            if (item.ControlName == "lblBOL")
                            {
                                //grdReportView.Columns["BillOfLoad"].Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblContractNumber")
                            {
                                grdReportView.Columns["ContractNumber"].Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblInvoiceNumber")
                            {
                                grdReportView.Columns["InvoiceNumber"].Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblLoadNumber")
                            {
                                grdReportView.Columns["LoadNumber"].Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblLotNumber")
                            {
                                grdReportView.Columns["LotNumber"].Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblPONumber")
                            {
                                grdReportView.Columns["PoNumber"].Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblTicketNumber")
                            {
                                grdReportView.Columns["TicketNumber"].Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblVehicleID")
                            {
                                grdReportView.Columns["VehicleNumber"].Caption = item.AlternateName;
                            }

                            //6/15/23
                            if (item.ControlName == "lblDataSource")
                            {
                                grdReportView.Columns["DataSource"].Caption = item.AlternateName;
                            }
                        }


                        break;
                    #endregion

                    //------------------------------------------------------
                    // USER-DEFINED SAMPLE PIVOT DATA
                    #region User-Defined Sample Pivot Data
                    //4/12/18
                    case (Int32)ReportEnum.UserDefinedSamplePivotData:

                        //2/16/21
                        AIMGlobal.showDate = true;

                        //2/16/21
                        if (rbGroupMaterialsYes.Checked == true)
                        {
                            _groupProducts = true;
                        }

                        setAnalysisVariables(analysisSummaryToGet);

                        //------------------------------------------------
                        //6/20/23 - added check of ShowOnly radio-buttons
                        if (rbShowOnlyNo.Checked == true)
                        {
                            if (rbProducts.Checked == true)
                            {
                                analysisSummaryToSet = AIMManager.GetAnalysisPivotData_ProductWithoutSampleAIM(ref analysisSummaryToGet, AIMCoreGlobal.AIMDatabaseID, _groupProducts);
                            }
                            else
                            {
                                analysisSummaryToSet = AIMManager.GetAnalysisPivotDataWithoutSampleAIM(ref analysisSummaryToGet, AIMCoreGlobal.AIMDatabaseID, _groupProducts);
                            }
                        }
                        else
                        {
                            //------------------------------------------------


                            if (rbProducts.Checked == true)
                            {
                                analysisSummaryToSet = AIMManager.GetAnalysisPivotData_Product(ref analysisSummaryToGet, AIMCoreGlobal.AIMDatabaseID, _groupProducts);
                            }
                            else
                            {
                                analysisSummaryToSet = AIMManager.GetAnalysisPivotData(ref analysisSummaryToGet, AIMCoreGlobal.AIMDatabaseID, _groupProducts);
                            }

                        }
                        pivotGridControl1.DataSource = analysisSummaryToSet;

                        //10/10/23 - from DevExpress support
                        pivotGridControl1.OptionsCustomization.AllowDrag = true;
                        pivotGridField10.Options.AllowDrag = DefaultBoolean.True;
                        pivotGridField20.Options.AllowDrag = DefaultBoolean.True;


                        //4/25/18
                        //Get alternate column names                        

                        //6/15/23 - added DataSourceTypeCode to stp (for Mountaire)
                        //_names = AIMManager.GetReportAlternateNames();
                        _names = AIMManager.GetReportAlternateNamesForUserDefinedSamplePivotReport();
                        foreach (var item in _names)
                        {
                            if (item.ControlName == "lblBOL")
                            {
                                pivotGridField17.Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblContractNumber")
                            {
                                pivotGridField12.Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblInvoiceNumber")
                            {
                                pivotGridField14.Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblLoadNumber")
                            {
                                pivotGridField16.Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblLotNumber")
                            {
                                pivotGridField19.Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblPONumber")
                            {
                                pivotGridField13.Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblTicketNumber")
                            {
                                pivotGridField15.Caption = item.AlternateName;
                            }
                            if (item.ControlName == "lblVehicleID")
                            {
                                pivotGridField18.Caption = item.AlternateName;
                            }

                            //6/15/23
                            if (item.ControlName == "lblDataSource")
                            {
                                pivotGridField23.Caption = item.AlternateName;
                            }
                        }



                        //5/18/18
                        if (rbIngredients.Checked == true)
                        {
                            pivotGridField20.Visible = false;
                            pivotGridField21.Visible = false;
                        }

                        break;
                    #endregion

                    //------------------------------------------------------
                    // SAMPLES BY OWNER LOCATION
                    #region Samples by Owner Location
                    //6/27/22
                    case (Int32)ReportEnum.SamplesByOwnerLocation:

                        AIMGlobal.showDate = true;
                        SampleHeaderInfo ownerData = new SampleHeaderInfo();
                        ownerData.ToDate = (DateTime)deToDate.DateTime;
                        ownerData.FromDate = (DateTime)deFromDate.DateTime;
                        if ((Int32)lueOwner.EditValue != 0)
                        {
                            ownerData.AIMOwnerID = (Int32)lueOwner.EditValue;
                        }
                        if ((Int32)lueOwnerLocation.EditValue != 0)
                        {
                            ownerData.AIMOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
                        }
                        if ((Int32)lueSupplier.EditValue != 0)
                        {
                            ownerData.AIMSourceID = (Int32)lueSupplier.EditValue;
                        }
                        if ((Int32)lueSupplierLocation.EditValue != 0)
                        {
                            ownerData.AIMSourceLocationID = (Int32)lueSupplierLocation.EditValue;
                        }
                        if ((Int32)lueMaterial.EditValue != 0)
                        {
                            ownerData.AIMMaterialID = (Int32)lueMaterial.EditValue;
                        }

                        ownerData.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                        sampleOwnerData = AIMManager.GetSamplesByOwnerLoc(ref ownerData);
                        if (sampleOwnerData.Count == 0)
                        {
                            AIMGlobal.SplashScreenHide();
                            MessageBox.Show("There are no samples for this criteria.", "Information...");
                            return;
                        }
                        ReportFunctions.DataSetSamplesByOwnerLoc = sampleOwnerData;
                        break;
                    #endregion

                    //------------------------------------------------------
                    // SUBMITTED SAMPLES REPORT
                    #region Submitted Samples Report
                    case (Int32)ReportEnum.SamplesSubmittedReport:

                        SampleSubmittedSamplesReportInfo submitted = new SampleSubmittedSamplesReportInfo();

                        ////---------------------------
                        //txtdt = deFromDate.DateTime.ToString("yyyy-MM-dd ");
                        //txtdt += teFromTime.Time.ToString("HH:mm:ss");
                        //amPM = ".000";
                        //if (teFromTime.Time.ToString("tt") == "PM")
                        //{
                        //    amPM = ".999";
                        //}
                        //txtdt += amPM;
                        //submitted.FromDateString = txtdt;

                        ////---------------------------
                        //if (deToDate.Text != "")
                        //{
                        //    txtdt = deToDate.DateTime.ToString("yyyy-MM-dd ");
                        //    txtdt += teToTime.Time.ToString("HH:mm:ss");
                        //    amPM = ".000";
                        //    if (teToTime.Time.ToString("tt") == "PM")
                        //    {
                        //        amPM = ".999";
                        //    }
                        //    txtdt += amPM;
                        //    submitted.ToDateString = txtdt;
                        //}
                        startDate = deFromDate.DateTime.ToString("yyyy-MM-dd ");
                        startDate += "00:00:00.000";
                        submitted.FromDateString = startDate;

                        endDate = deToDate.DateTime.ToString("yyyy-MM-dd ");
                        endDate += "23:59:59.999";
                        submitted.ToDateString = endDate;

                        if ((Int32)lueMaterial.EditValue != 0)
                        {
                            submitted.AIMMaterialID = (Int32)lueMaterial.EditValue;
                        }

                        //1/15/24 - uncommented
                        if (rbProducts.Checked == true)
                        {
                            submitted.MaterialTypeID = 2;
                        }
                        else if (rbIngredients.Checked == true)
                        {
                            submitted.MaterialTypeID = 1;
                        }
                        else  //All Products                                              
                        {                                               //1/15/24
                            submitted.MaterialTypeID = 0;               //1/15/24
                        }                                               //1/15/24

                        if ((Int32)lueTestType.EditValue != 0)
                        {
                            submitted.SampleTestTypeID = (Int32)lueTestType.EditValue;
                        }

                        if (rbUseSampleDate.Checked == true)
                        {
                            submitted.UseSampleDate = true;
                            AIMGlobal.useSampleDate = true;
                        }
                        else
                        {
                            submitted.UseSampleDate = false;
                            AIMGlobal.useSampleDate = false;
                        }

                        //submitted.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;                  //10/16/23
                        submitted.CFCUserID = (Int32)lueCFCUser.EditValue;                          //10/16/23
                        submitted.AIMOwnerID = (Int32)lueOwner.EditValue;                           //10/16/23
                        submitted.AIMOwnerLocationID = (Int32)lueOwnerLocation.EditValue;           //10/16/23

                        sampleTestType = AIMManager.GetSampleSubmitted(ref submitted);
                        ReportFunctions.DataSetSampleSubmitted = sampleTestType;

                        break;
                    #endregion

                    // ANALYSIS COST
                    case (Int32)ReportEnum.AnalysisCost:            //Task1150 DB260102

                        if (rbGroupMaterialsYes.Checked == true)
                        {
                            isGrouped = true;
                        }
                        else
                        {
                            isGrouped = false;
                        }

                        testGrid = AIMManager.GetTestGrid(isGrouped, deFromDate.DateTime, deToDate.DateTime, (Int32)lueOwnerLocation.EditValue);
                        grdCostAnalysis.DataSource = testGrid;
                        pnlGrid.Visible = true;
                        
                        grdCostAnalysis.Visible = true;
                        pnlSaveResults.Visible = true;
                        break;
                    //------------------------------------------------------            //9/1/23
                    // SAMPLE ANALYSIS REQUEST FORM 
                    #region Sample Analysis Request Form
                    case (Int32)ReportEnum.SampleAnalysisRequestFormEvonik:
                    case (Int32)ReportEnum.SampleAnalysisRequestFormAjinomoto:
                    case (Int32)ReportEnum.SampleAnalysisRequestFormUnitedAnimalHealth:

                        SampleAnalysisRequestFormInfo form = new SampleAnalysisRequestFormInfo();

                        startDate = deFromDate.DateTime.ToString("yyyy-MM-dd ");
                        startDate += "00:00:00.000";
                        form.FromDateString = startDate;

                        endDate = deToDate.DateTime.ToString("yyyy-MM-dd ");
                        endDate += "23:59:59.999";
                        form.ToDateString = endDate;

                        //----------------------------------------------------------------
                        if (reportID == (Int32)ReportEnum.SampleAnalysisRequestFormEvonik)
                        {
                            form.DataSourceTypeID = 3;
                        }
                        else if (reportID == (Int32)ReportEnum.SampleAnalysisRequestFormAjinomoto)
                        {
                            form.DataSourceTypeID = 14;
                        }
                        else if (reportID == (Int32)ReportEnum.SampleAnalysisRequestFormUnitedAnimalHealth)
                        {
                            form.DataSourceTypeID = 15;
                        }

                        //----------------------------------------------------------------
                        if ((Int32)lueMaterial.EditValue != 0)
                        {
                            form.AIMMaterialID = (Int32)lueMaterial.EditValue;
                        }

                        //----------------------------------------------------------------
                        //10/5/23 - commented
                        //if (rbProducts.Checked == true)
                        //{
                        //    form.MaterialTypeID = 2;
                        //}
                        //else
                        //{
                        //    form.MaterialTypeID = 1;
                        //}

                        if (reportID == (Int32)ReportEnum.SampleAnalysisRequestFormUnitedAnimalHealth)      //Task1053 250814
                        {
                            if ((Int32)lueSupplier.EditValue != 0)
                            {
                                form.SupplierID = (Int32)lueSupplier.EditValue;
                            }
                            
                        }

                            //----------------------------------------------------------------
                        form.CFCUserID = (Int32)lueCFCUser.EditValue;
                        sampleRequestForm = AIMManager.GetSampleAnalysisRequestForm(ref form);
                        ReportFunctions.DataSetSampleAnalysisRequestForm = sampleRequestForm;

                        if (sampleRequestForm.Count == 0)
                        {
                            _noData = true;
                        }


                        

                        break;





                    #endregion
                    



                }

                //Possible change for Mountaire:  add dropdown with "summary types" for user to choose from.                
                //Field 10 is the result field. The following line sets the "summary type" = SUM  
                //(Other summary types:  Average, Count, Min, Max, StdDev, StdDevp)
                pivotGridControl1.Fields[10].SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum;

                AIMGlobal.SplashScreenHide();

                //9/6/23
                if (_noData == true)
                {
                    MessageBox.Show("No data exists for the selected report/parameters.", "Warning...");
                }
                else
                {
                    //ReportFunctions.LoadReport(reportID);
                    this.Show();
                }


                //4/26/22 - date keeps getting wiped out - very irritating
                //if (AIMGlobal.showDate == false)
                //{
                //    deFromDate.EditValue = "";
                //    deToDate.EditValue = "";
                //}
                AIMGlobal.showDate = true;
                if (deFromDate.Text == "" || deToDate.Text == "")
                {
                    AIMGlobal.showDate = false;

                    if (deFromDate.Text == "")
                    {
                        deFromDate.EditValue = "1/1/2000";
                    }
                    if (deToDate.Text == "")
                    {
                        deToDate.EditValue = DateTime.Now;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //4/15/22
        private void timer1_Tick(Object sender, EventArgs e)
        {
            if (btnGenerateReport.BackColor == SystemColors.Control)
            {
                //btnGenerateReport.BackColor = SystemColors.Info;
                btnGenerateReport.BackColor = Color.CornflowerBlue;
            }
            else
            {
                btnGenerateReport.BackColor = SystemColors.Control;
            }
        }

        //6/7/22 - trying to figure out why date sort does not work for Butterball - this isn't getting hit -- 
        private void pivotGridControl1_CustomFieldSort(object sender, DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "StringSampleDate")   //pivotGridField8
            {
                object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "SampleDate"),
                        orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "SampleDate");
                e.Result = Comparer.Default.Compare(orderValue1, orderValue2);
            }
        }

        //1/18/23
        private void chkOutOfRange_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOutOfRange.Checked == true)
            {
                gpUseResultOrAdjusted.Visible = true;
            }
            else
            {
                gpUseResultOrAdjusted.Visible = false;
            }
        }

        private void rbShowOnlyYes_CheckedChanged(object sender, EventArgs e)
        {
            //6/26/23            
            if (rbShowOnlyYes.Checked == true)
            {
                lblShowOnly.Text = "Results: show only";
                lblShowOnlyType.Visible = true;
            }
            else
            {
                lblShowOnly.Text = "Results: show  ALL datasources";
                lblShowOnlyType.Visible = false;
            }
        }

        //8/22/23
        private void rbUseCreatedDate_CheckedChanged(object sender, EventArgs e)
        {
            lblDate.Text = "Sample Created Date";
        }

        //8/22/23
        private void rbUseSampleDate_CheckedChanged(object sender, EventArgs e)
        {
            lblDate.Text = "Sample Date";
        }

        //9/6/23
        private void hyperReportComments_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.open_form(reportComments = new ReportComments());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void listboxReports_SelectedValueChanged(object sender, EventArgs e)
        {
            //Tak1075 DB250716     Why were we doing this? deFromDate.Text = "";                                                               //10/5/23
        }

        private void lueCFCUser_Click(object sender, EventArgs e)
        {
            lueCFCUser.SelectAll();
        }

        private void lueCFCUser_Enter(object sender, EventArgs e)
        {
            lueCFCUser.SelectAll();
        }

        private void lueTemplate_Click(object sender, EventArgs e)
        {
            lueTemplate.SelectAll();
        }

        private void lueTemplate_Enter(object sender, EventArgs e)
        {
            lueTemplate.SelectAll();
        }

        private void lueTestType_Click(object sender, EventArgs e)
        {
            lueTestType.SelectAll();
        }

        private void lueTestType_Enter(object sender, EventArgs e)
        {
            lueTestType.SelectAll();
        }

        private void lueSecondDataSource_Click(object sender, EventArgs e)
        {
            lueSecondDataSource.SelectAll();
        }

        private void lueSecondDataSource_Enter(object sender, EventArgs e)
        {
            lueSecondDataSource.SelectAll();
        }

        private void lueFirstDataSource_Enter(object sender, EventArgs e)
        {
            lueFirstDataSource.SelectAll();
        }

        private void lueFirstDataSource_Click(object sender, EventArgs e)
        {
            lueFirstDataSource.SelectAll();
        }

        //10/26/23
        private void btnHelpReports_Click(object sender, EventArgs e)
        {
            reportHelp = new ReportHelp();
            reportHelp.ShowDialog();
        }

        //1/15/24
        private void rbAllIngredientsProducts_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAllIngredientsProducts.Checked == true)
            {
                lblMaterialCode.Visible = false;
                lblMaterialName.Visible = false;                    //1/15/24
                lueMaterial.Visible = false;
                lueMaterialName.Visible = false;
                lueMaterial.EditValue = 0;
            }
        }

        //1/15/24
        private void SyncValueLue(ref LookUpEdit from, ref LookUpEdit to)
        {
            if (from == null || to == null)
            {
                return;
            }
            if (to.EditValue == from.EditValue)
            {
                return;
            }
            to.EditValue = from.EditValue;
        }

        //1/15/24
        private void lueMaterialName_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueMaterialName, ref lueMaterial);
        }

        //1/15/24
        private void lueMaterialName_Click(object sender, EventArgs e)
        {
            lueMaterialName.SelectAll();
        }

        //1/15/24
        private void lueMaterialName_Enter(object sender, EventArgs e)
        {
            lueMaterialName.SelectAll();
        }

        //task 1133 FG 7/21/2026 - Name dropdowns for Owner, Owner Location, Supplier and CFC User (same pattern as Sample.cs / lueMaterialName)
        #region Code-Name dropdown sync
        private void lueOwnerName_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueOwnerName, ref lueOwner);
        }
        private void lueOwnerName_Click(object sender, EventArgs e)
        {
            lueOwnerName.SelectAll();
        }
        private void lueOwnerName_Enter(object sender, EventArgs e)
        {
            lueOwnerName.SelectAll();
        }
        private void lueOwnerLocation_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueOwnerLocation, ref lueOwnerLocationName);
        }
        private void lueOwnerLocationName_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueOwnerLocationName, ref lueOwnerLocation);
        }
        private void lueOwnerLocationName_Click(object sender, EventArgs e)
        {
            lueOwnerLocationName.SelectAll();
        }
        private void lueOwnerLocationName_Enter(object sender, EventArgs e)
        {
            lueOwnerLocationName.SelectAll();
        }
        private void lueSupplierName_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueSupplierName, ref lueSupplier);
        }
        private void lueSupplierLocation_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueSupplierLocation, ref lueSupplierLocationName);
        }
        private void lueSupplierLocationName_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueSupplierLocationName, ref lueSupplierLocation);
        }
        private void lueSupplierLocationName_Click(object sender, EventArgs e)
        {
            lueSupplierLocationName.SelectAll();
        }
        private void lueSupplierLocationName_Enter(object sender, EventArgs e)
        {
            lueSupplierLocationName.SelectAll();
        }
        private void lueSupplierName_Click(object sender, EventArgs e)
        {
            lueSupplierName.SelectAll();
        }
        private void lueSupplierName_Enter(object sender, EventArgs e)
        {
            lueSupplierName.SelectAll();
        }
        private void lueCFCUser_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueCFCUser, ref lueCFCUserName);
        }
        private void lueCFCUserName_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueCFCUserName, ref lueCFCUser);
        }
        private void lueCFCUserName_Click(object sender, EventArgs e)
        {
            lueCFCUserName.SelectAll();
        }
        private void lueCFCUserName_Enter(object sender, EventArgs e)
        {
            lueCFCUserName.SelectAll();
        }

        //The report-type logic shows/hides/disables the Code dropdowns in many places;
        //these handlers keep each Name dropdown in the same state instead of repeating that logic everywhere.
        private void lueOwner_VisibleEnabledChanged(object sender, EventArgs e)
        {
            lueOwnerName.Visible = lueOwner.Visible;
            lueOwnerName.Enabled = lueOwner.Enabled;
        }
        private void lueOwnerLocation_VisibleEnabledChanged(object sender, EventArgs e)
        {
            lueOwnerLocationName.Visible = lueOwnerLocation.Visible;
            lueOwnerLocationName.Enabled = lueOwnerLocation.Enabled;
        }
        private void lueSupplier_VisibleEnabledChanged(object sender, EventArgs e)
        {
            lueSupplierName.Visible = lueSupplier.Visible;
            lueSupplierName.Enabled = lueSupplier.Enabled;
        }
        private void lueSupplierLocation_VisibleEnabledChanged(object sender, EventArgs e)
        {
            lueSupplierLocationName.Visible = lueSupplierLocation.Visible;
            lueSupplierLocationName.Enabled = lueSupplierLocation.Enabled;
        }
        #endregion

        private void pnlAnalyzerOrFormulation_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grdReportView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)   //Task1147 DB260428
        {
            if (e.Column.Name.ToString() == "colFlagInsideWarningLimit")                            
            {
                Int32 flagResult = (Int32)grdReportView.GetRowCellValue(e.RowHandle, colResultValueFlagAnalysis);

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
        //Task1176 FG 7/3/2026 Start
        private void btnHelpCriteria_Click(object sender, EventArgs e)
        {
            ReportPanelHelp.ShowNextTo((Control)sender, "Criteria", HelpText.Criteria);
        }

        private void btnHelpTemplates_Click(object sender, EventArgs e)
        {
            ReportPanelHelp.ShowNextTo((Control)sender, "Templates", HelpText.Templates);
        }

        private void btnHelpSaveResults_Click(object sender, EventArgs e)
        {
            ReportPanelHelp.ShowNextTo((Control)sender, "Save Results", HelpText.SaveResults);
        }

        private static class HelpText
        {
            public const string Criteria =
                "Filters which samples are included in the report. Set these before you Generate or Preview.\r\n\r\n" +
                "\u2022 Sample Date From / To: limit results to samples in this date range (uses the Sample Date or the Sample Created Date, per the date option).\r\n\r\n" +
                "\u2022 Ingredients / Products: choose which material type to report on. Ingredients hides the material picker; Products reports on finished products.\r\n\r\n" +
                "\u2022 Group Materials: Yes combines matching materials into one grouped result; No reports each material separately.\r\n\r\n" +
                "\u2022 Ticket: limit results to one ticket number (blank = all tickets).\r\n\r\n" +
                "\u2022 Sample Owner / Owner Location: restrict results to that company / location.\r\n\r\n" +
                "\u2022 Supplier / Supplier Location: restrict results to that supplier and its location.\r\n\r\n" +
                "\u2022 Results (show only / show ALL datasources): \"show only\" keeps just the priority value per sample (e.g. Last In); the other option returns every data source.";

            public const string Templates =
                "Save and reuse a report's column layout together with its criteria.\r\n\r\n" +
                "\u2022 Template list: pick a saved template to load its layout and saved criteria. \"--Select--\" clears the layout and criteria.\r\n\r\n" +
                "\u2022 Edit Templates...: open the template maintenance screen to rename or manage templates.\r\n\r\n" +
                "\u2022 Reset: clear the current criteria and template selection, switch back to Ingredients, and empty the results.\r\n\r\n" +
                "\u2022 Save (disk icon): save the current layout and criteria to the selected template (confirms overwrite). If none is selected, opens Save-As-New. You can only overwrite templates you own.\r\n\r\n" +
                "\u2022 Updated By / Update Stamp: shows who last saved the selected template and when.";

            public const string SaveResults =
                "Opens a print preview of the current results (grid or pivot, depending on the template).\r\n\r\n" +
                "From the preview you can print the report and/or save it to Excel.";
        }
        //Task1176 FG 7/3/2026 End

        ////6/7/22 - trying to figure out why date sort does not work for Butterball      
        //private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        //{
        //    pivotGridField8.SortMode = ((CheckEdit)sender).Checked ? PivotSortMode.Custom : PivotSortMode.Default;
        //}

    }
}


