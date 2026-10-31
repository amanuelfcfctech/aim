using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraEditors;
using System.Drawing;
using System.IO;
using System.Collections;
using DevExpress.XtraPivotGrid;

namespace CFCAIM.Report
{
    public partial class ReportView : Form
    {
        #region  local variables
        ReportQuery nreportView;
        List<AIMOwnerLocationInfo> _aimOwnerLocation = new List<AIMOwnerLocationInfo>();
        List<AIMOwnerInfo> _aimOwner = new List<AIMOwnerInfo>();
		List<AIMMaterialInfo> _aimMaterial = new List<AIMMaterialInfo>();
        List<AIMMaterialInfo> _aimMaterialProducts = new List<AIMMaterialInfo>();
        List<AIMSupplierInfo> _source = new List<AIMSupplierInfo>();
		List<AIMSupplierLocationInfo> _sourceLocation = new List<AIMSupplierLocationInfo>();
		List<AIMNutrientInfo> _aimNutrient = new List<AIMNutrientInfo>();
        List<ReportAlternateNamesInfo> _names = new List<ReportAlternateNamesInfo>();
        List<SampleTestTypeInfo> _sampleTestType = new List<SampleTestTypeInfo>();                      //8/16/23
        List<CFCUserInfo> _cfcUsers = new List<CFCUserInfo>();                                          //9/1/23

        Int32 _datasource1 = 0;
		Int32 _datasource2 = 0;
        //Int32 _datasourcetype1 = 0;                   //3/12/20 - types no longer used
        //Int32 _datasourcetype2 = 0;                   //3/12/20 - types no longer used
        Report.ReportHelp reportHelp;
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

        //6/20/23
        Int32 aimDatabaseID = 0;        
        String priorityValue = "";

        //9/6/23
        ReportComments reportComments;
        Boolean _noData = false;

        System.IO.Stream _LayoutStream = new System.IO.MemoryStream();

        #endregion

        public ReportView()
        {
            InitializeComponent();

            //6/7/22 - trying to figure out why date sort does not work for Butterball
            pivotGridControl1.CustomFieldSort += new DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventHandler(pivotGridControl1_CustomFieldSort);
        }
		private void reportView_Load(object sender, EventArgs e)
		{
			try
			{
                
                clear();
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
            lblSelectProduct.Visible = true;
            lueMaterial.Visible = true;

            rbExtremeOutlierInclude.Checked = true;
            rbMildOutlierInclude.Checked = true;
            
            pnlMaterialType.Visible = false;

            //4/15/22
            pnlAnalyzerOrFormulation.Visible = false;


            pnlGrid.Visible = false;
            grdReport.Visible = false;
            pnlTemplate.Visible = false;
            pnlSaveResults.Visible = false;

            //5/21/19
            lblTemplateHeader.Visible = false;
            lblSaveGridResults.Visible = false;

            pnlPivot.Visible = false;
            grdReport.DataSource = null;
            pivotGridControl1.DataSource = null;
            
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
            //200606CM - changed this back, until decision is made about showing products
            _aimMaterial = AIMManager.GetAIMMaterial(true);
            ////////////if (materialTypeID == (Int32)MaterialTypeEnum.Product)
            ////////////{                
            ////////////    _aimMaterial = AIMManager.GetAIMMaterial_Active_Products(true);
            ////////////}
            ////////////else
            ////////////{
            ////////////    _aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, materialTypeID);
            ////////////}
            
            _aimMaterial.Insert
            (
                0,
                new AIMMaterialInfo
                {
                    AIMMaterialID = 0,
                    AIMMaterialCode = "--All--"
                }
            );
            lueMaterial.Properties.DataSource = _aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");

        }
        private void dataBind()
		{

			_aimOwnerLocation = AIMManager.GetAIMOwnerLocation(true);
			_aimOwner = AIMManager.GetAIMOwner(true);						
			_aimNutrient = AIMManager.GetAIMNutrient(true);

            List<DataSourceAllTypesInfo> dataSource;
            dataSource = AIMManager.GetDataSourceAllTypes(true);

			_aimOwner.Insert
			(
				0,
				new AIMOwnerInfo
				{
					AIMOwnerID = 0,
					OwnerCode = "--All--"
				}
			);
			            
			_aimNutrient.Insert
			(
				0,
				new AIMNutrientInfo
				{
					AIMNutrientID = 0,
					AIMNutrientCode = "--Select--",
					AIMNutrientName = "--Select--"
				}
			);

			dataSource.Insert
			(
				0,
				new DataSourceAllTypesInfo
				{
					RowNumber = 0,
					Name = "--Select--"
				}
			);
            		

			lueOwner.Properties.DataSource = _aimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwner, "AIMOwnerID", "OwnerCode");
			if (_aimOwner.Count == 2)
			{
				//selecting value if there is only one
				lueOwner.EditValue = _aimOwner[1].AIMOwnerID;
			}
			else
			{
				lueOwner.EditValue = _aimOwner;
			}

            
			lueFirstDataSource.Properties.DataSource = dataSource;
			XtraEditors.LoadLookUpEdit(ref lueFirstDataSource, "RowNumber", "Name", "IsUserDefined","DataSourceTypeID");
			lueFirstDataSource.Properties.Columns["IsUserDefined"].Caption = "Type";
            lueFirstDataSource.Properties.Columns["DataSourceTypeID"].Visible = false;      //3/12/20

            lueSecondDataSource.Properties.DataSource = dataSource;
			XtraEditors.LoadLookUpEdit(ref lueSecondDataSource, "RowNumber", "Name", "IsUserDefined", "DataSourceTypeID");
			lueSecondDataSource.Properties.Columns["IsUserDefined"].Caption = "Type";
            lueSecondDataSource.Properties.Columns["DataSourceTypeID"].Visible = false;      //3/12/20

            //---------------------------------------------------------------------------   //8/16/23
            _sampleTestType = AIMManager.GetSampleTestType();                               
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
            if (_sampleTestType.Count == 2)
            {
                //selecting value if there is only one
                lueTestType.EditValue = _sampleTestType[1].SampleTestTypeID;
            }
            else
            {
                lueTestType.EditValue = _sampleTestType;
            }

            //---------------------------------------------------------------------------   //9/1/23            
            _cfcUsers = AIMManager.GetCFCUsersAllButAimSystem(true);
            _cfcUsers.Insert
                (
                0,
                new CFCUserInfo
                {
                    CFCUserID = 0,
                    UserName = "--All--"
                }
            );
            
            lueCFCUser.Properties.DataSource = _cfcUsers;
            XtraEditors.LoadLookUpEdit(ref lueCFCUser, "CFCUserID", "UserName");
            lueCFCUser.EditValue = userGlobal.CFCLoginUser.CFCUserID;

        }
        private void dataBindSupplier()
        {
            //6/3/20
            //_source = AIMManager.GetSupplier(true);

            //200606CM - changed this back, until decision is made about showing products
            //////////if (lueMaterial.EditValue == null)
            //////////{
            //////////    _source = AIMManager.GetSupplier(true);
            //////////}
            //////////else if (rbIngredients.Checked == true)
            //////////{
            //////////    _source = AIMManager.GetSupplierWithMaterials(true, (Int32)lueMaterial.EditValue);
            //////////}
            //////////else
            //////////{
            //////////    //For products, have to use material code; otherwise user has to select each material for every owner-location
            //////////    _source = AIMManager.GetSupplierWithMaterialCodes(true, lueMaterial.EditValue.ToString());
            //////////}
            if (lueMaterial.EditValue == null)
            {
                _source = AIMManager.GetSupplier(true);
            }
            else if (rbIngredients.Checked == true)
            {
                _source = AIMManager.GetSupplierWithMaterials(true, (Int32)lueMaterial.EditValue);
            }                        


            _source.Insert
            (
                0,
                new AIMSupplierInfo
                {
                    AIMSupplierID = 0,
                    AIMSupplierCode = "--All--",
                    SupplierName = "--All--"
                }
            );

            lueSupplier.Properties.DataSource = _source;
            XtraEditors.LoadLookUpEdit(ref lueSupplier, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
            lueSupplier.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

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

            reports.Insert
			(
				0,
				new ReportInfo
				{
					ReportID = 0,
					ReportName = "--Select--"
				}
			);
		  
			listboxReports.DataSource = reports;
			listboxReports.DisplayMember = "ReportName";
			listboxReports.ValueMember = "ReportID";

        }

		private void dataBindSourceLocation()
        {
			Int32 sourceLocationID = 0;
			Int32 sourceID;
			Int32.TryParse(lueSupplier.EditValue.ToString(), out sourceID);
			_sourceLocation = AIMManager.GetSupplierLocationForSupplier(sourceID);

			_sourceLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,
					AIMSupplierLocationCode = "--All--",
					SupplierLocationName = "--All--"
				}
			);
			lueSupplierLocation.Properties.DataSource = _sourceLocation;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocation, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
			lueSupplierLocation.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";
			

			if (_sourceLocation.Count == 2)
			{
				//selecting value if there is only one
				lueSupplierLocation.EditValue = _sourceLocation[1].AIMSupplierLocationID;
			}
			else
			{
				lueSupplierLocation.EditValue = sourceLocationID;
			}
						
        }

        private void dataBindOwnerLocation()
        {            
            Int32 OwnerID = 0;
            if (lueOwner.EditValue != null)
            {
                Int32.TryParse(lueOwner.EditValue.ToString(), out OwnerID);
            }

            _aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(OwnerID);

            _aimOwnerLocation.Insert
            (
                0,
                new AIMOwnerLocationInfo
                {
                    AIMOwnerLocationID = 0,
                    LocationCode = "--All--"
                }
            );
            lueOwnerLocation.Properties.DataSource = _aimOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueOwnerLocation, "AIMOwnerLocationID", "LocationCode");
            lueOwnerLocation.Properties.Columns["LocationCode"].Visible = true;
            lueOwnerLocation.Properties.Columns["LocationName"].Visible = true;
			if (_aimOwnerLocation.Count == 2)
			{
				//selecting value if there is only one
				lueOwnerLocation.EditValue = _aimOwnerLocation[1].AIMOwnerLocationID;
			}
			else
			{
				lueOwnerLocation.EditValue = _aimOwnerLocation;
			}
        }

		private void lueOwner_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
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
                dataBindSourceLocation();
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
                //3/4/21 - removed unnecessary assignment
				//SystemSettingsInfo systemSettings = new SystemSettingsInfo();
                SystemSettingsInfo systemSettings;

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


		private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            
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
			moveCursorOnEnter(sender, e);
		}

		private void deToDate_KeyDown(object sender, KeyEventArgs e)
		{
			moveCursorOnEnter(sender, e);
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
                reportHelp = new ReportHelp();
                reportHelp.ShowDialog();
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

            //--------------------------------------------------------------
            lblSelectProduct.Visible = true;
			lueMaterial.Visible = true;
			lueMaterial.Enabled = true;

            //200606CM - changed this back, until decision is made about showing products
            pnlMaterialType.Visible = false;
            ////////pnlMaterialType.Visible = true;

            //4/15/22
            pnlAnalyzerOrFormulation.Visible = false;


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
            
            //9/5/23 - commented
            //deFromDate.Text = "";                     
            //deToDate.Text = "";                       

            //-------------------------------------------------------------------------
            // 3 Main Panels:  
            //      panelCriteria   - includes pnlDryMatter, pnlSupplierLocationCriteria, pnlDataSourcesTCompare and pnlMaterialPick
            //      pnlPivot        - includes PivotGridControl1    
            //      pnlGrid         - includes grdReport

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
            
            pnlMaterialGroup.Visible = false;                   //2/16/21
            rbGroupMaterialsNo.Checked = true;                  //2/16/21            
            pnlQualityControl.Visible = false;                  //4/26/22
            rbQCNo.Checked = true;                              //4/26/22
            
            panelShowOnly.Visible = false;                      //6/20/23
            pnlTestType.Visible = false;                        //8/16/23
            panelDateSampleOrCreated.Visible = false;           //8/22/23
            panelCFCUser.Visible = false;                       //9/1/23
            hyperReportComments.Visible = false;                //9/6/23



            //-------------------------------------------------------------------------
            //RESIZE PANEL CRITERIA (based on size of main group)
            panelCriteria.Visible = true;
            
            //9/5/23
            //panelCriteria.Location = new Point(248, 210);
            panelCriteria.Location = new Point(279, 210);

            Int32 panelWidth;
            Int32 panelHeight;
            panelWidth = grpMain.Width - 270;
            panelHeight = grpMain.Height - 230;                                   
            panelCriteria.Size = new Size(panelWidth, panelHeight);

            //-------------------------------------------------------------------------
            //ANALYSIS SUMMARY
            #region Analysis Summary
            //4/20/21
            if (reportID == (Int32)ReportEnum.AnalysisSummary)
            {                
                pnlSortOnNutrientCode.Visible = true;
                pnlSortOnNutrientCode.Location = new Point(1,97);
                pnlSortOnNutrientCode.Size = new Size(132,61);
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


            //-------------------------------------------------------------------------
            // ANALYSIS PIVOT SUMMARY
            #region Analysis Pivot Summary
            if (reportID == (Int32)ReportEnum.AnalysisPivotSummary)
			{
                
                pnlDryMatter.Visible = true;                
                pnlDryMatter.Location = new Point(10, 5);
                pnlDryMatter.Size = new Size(232, 67);

                lblSelectProduct.Visible = false;
				lueMaterial.Visible = false;

                //200606CM - changed this back, until decision is made about showing products
                //6/5/20
                ///////pnlMaterialType.Visible = false;

                pnlMaterialPick.Visible = true;
                pnlMaterialPick.Location = new Point(15, 80);                                
                pnlMaterialPick.Size = new Size(850, 250);

                //4/28/21
                pnlSortOnNutrientCode.Visible = true;
                pnlSortOnNutrientCode.Location = new Point(1, 97);
                pnlSortOnNutrientCode.Size = new Size(132, 61);


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

                lblSelectProduct.Visible = false;
                lueMaterial.Visible = false;

                //200606CM - changed this back, until decision is made about showing products
                //6/5/20
                ////////pnlMaterialType.Visible = false;

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
            if (reportID == (Int32) ReportEnum.SampleActivityReport)
            {
                deFromDate.Text = DateTime.Today.ToString();                
                chkShowViolations.Visible = false;
                lblSelectProduct.Visible = false;
                lueMaterial.Visible = false;

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

                //3/20/18
                panelTime.Visible = true;

                //6/20/23
                //pnlQualityControl.Location = new Point(114, 97);
                pnlQualityControl.Location = new Point(140, 92);

                pnlQualityControl.Visible = true;

                PopupContainerControl container = new PopupContainerControl();
                XtraUserControl uc = new XtraUserControl();
                uc.Dock = DockStyle.Fill;
                container.Controls.Add(dropDownButton1);
                popupContainerEdit1.Properties.PopupControl = container;

            }
            #endregion


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
                deToDate.EditValue = DateTime.Now;
                deFromDate.EditValue = DateTime.Now.AddDays(-30);
            }
            #endregion
            

            //-------------------------------------------------------------------------
            // SAMPLE RESULTS BY SUPPLIER
            #region Sample Results by Supplier
            //2/12/18
            if (reportID == (Int32)ReportEnum.SampleResultsBySupplier)
            {
                lblSelectProduct.Visible = false;
                lueMaterial.Visible = false;

                //200606CM - changed this back, until decision is made about showing products
                //6/5/20
                /////////pnlMaterialType.Visible = false;

                lblTicket.Visible = false;
                txtTicket.Visible = false;
            }
            #endregion


            //-------------------------------------------------------------------------
            // SUPPLIER LOCATION GUARANTEES
            #region Supplier Location Guarantees
            if (reportID == (Int32)ReportEnum.SupplierLocationGuarantees)
            {

                //4/20/21          
                pnlSortOnNutrientCode.Visible = true;
                pnlSortOnNutrientCode.Location = new Point(1, 97);
                pnlSortOnNutrientCode.Size = new Size(132, 61);

                txtTicket.Enabled = false;
                lueOwner.Enabled = false;
                lueOwnerLocation.Enabled = false;

                pnlSupplierLocationCriteria.Visible = true;
                pnlSupplierLocationCriteria.Location = new Point(10, 5);

                //1/18/23
                //pnlSupplierLocationCriteria.Size = new Size(232, 67);
                pnlSupplierLocationCriteria.Size = new Size(232, 103);
                gpUseResultOrAdjusted.Visible = true;


            }
            #endregion


            //-------------------------------------------------------------------------
            // USER-DEFINED SAMPLE DATA
            #region User-Defined Sample Data
            //4/4/18
            if (reportID == (Int32)ReportEnum.UserDefinedSampleData)
            {
                //6/4/20
                panelCriteria.Visible = false;

                chkShowViolations.Visible = false;
                lblSelectProduct.Visible = false;
                lueMaterial.Visible = false;
                pnlMaterialType.Visible = true;
                lblSource.Visible = false;
                lblSourceLocation.Visible = false;
                lblTicket.Visible = false;               
                lueSupplier.Visible = false;
                lueSupplierLocation.Visible = false;
                txtTicket.Visible = false;                

                pnlGrid.Visible = true;

                //9/5/23
                //pnlGrid.Location = new Point(248, 210);
                pnlGrid.Location = new Point(279, 210);

                pnlGrid.Size = new Size(panelWidth, panelHeight);

                //2/16/21
                pnlMaterialGroup.Visible = true;
                
                grdReport.Visible = true;
                pnlTemplate.Visible = true;
                pnlSaveResults.Visible = true;
                lblTemplateHeader.Visible = true;
                lblSaveGridResults.Visible = true;
            }
            #endregion


            //-------------------------------------------------------------------------
            // USER-DEFINED SAMPLE PIVOT DATA
            #region User-Defined Sample Pivot Data
            //4/12/18
            if (reportID == (Int32)ReportEnum.UserDefinedSamplePivotData)
            {
                //6/3/20
                panelCriteria.Visible = false;

                pnlPivot.Visible = true;

                //9/5/23
                //pnlPivot.Location = new Point(254, 272);
                pnlPivot.Location = new Point(276, 272);

                pnlPivot.Size = new Size(panelWidth, panelHeight);

                //2/16/21
                pnlMaterialGroup.Visible = true;

                pnlTemplate.Visible = true;
                pnlSaveResults.Visible = true;
                lblTemplateHeader.Visible = true;
                lblSaveGridResults.Visible = true;
                pnlMaterialType.Visible = true;

                //6/26/23
                panelShowOnly.Visible = true;
                
                // Save the default layout to a stream. 
                //5/22/19
                _LayoutStream = new System.IO.MemoryStream();

                pivotGridControl1.SaveLayoutToStream(_LayoutStream);
                            
            }
            #endregion

            //-------------------------------------------------------------------------
            // SUBMITTED SAMPLES REPORT
            #region Submitted Samples Report
            if (reportID == (Int32)ReportEnum.SamplesSubmittedReport)
            {
                lblDate.Text = "Sample Created Date";                                                       //8/16/23
                deFromDate.Text = DateTime.Today.ToString();
                chkShowViolations.Visible = false;
                lblSelectProduct.Visible = true;
                lueMaterial.Visible = true;
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
                panelTime.Visible = true;

                pnlTestType.Visible = true;
                pnlTestType.Location = new Point(3,7);
                pnlTestType.Size = new Size(286, 140);

                panelDateSampleOrCreated.Visible = true;
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
                //deFromDate.Text = DateTime.Today.ToString();
                chkShowViolations.Visible = false;
                lblSelectProduct.Visible = true;
                lueMaterial.Visible = true;
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
                panelTime.Visible = false;
                panelDateSampleOrCreated.Visible = false;
                panelCFCUser.Visible = true;
                hyperReportComments.Visible = true;
            }
            #endregion

            //-------------------------------------------------------------------------

            //200606CM - changed this back, until decision is made about showing products
            //6/5/20 - commented, because now we want these reports to have the option of selecting ingredients or products            
            if (reportID == (Int32)ReportEnum.AnalysisPivotReport ||
                reportID == (Int32)ReportEnum.AnalysisPivotSummary ||
                reportID == (Int32)ReportEnum.ManufacturedProductComplianceReport ||
                reportID == (Int32)ReportEnum.SampleResultsBySupplier ||

                //4/15/22 - COMMENTED
                //reportID == (Int32)ReportEnum.AimMaterialRefAnalyzerMaterialOphans ||

                reportID == (Int32)ReportEnum.AimNutrientRefAnalyzerNutrientOphans ||
                reportID == (Int32)ReportEnum.ExportBatchDetail ||
                reportID == (Int32)ReportEnum.AnalysisSupplierComparison ||
                reportID == (Int32)ReportEnum.AnalysisDateComparisonReport ||
                reportID == (Int32)ReportEnum.SupplierLocationGuarantees ||
                reportID == (Int32)ReportEnum.AnalysisDetail ||
                reportID == (Int32)ReportEnum.AnalysisSummary ||
                reportID == (Int32)ReportEnum.SampleComparisonReport)
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
                
        private void btnSimpleView_Click(Object sender, EventArgs e)
        {
            reportDesigner = new ReportDesigner();               
            reportDesigner.Show();            
        }

        private void btnPreviewGrid_Click(Object sender, EventArgs e)
        {
            {
                try
                {

                    if (_selectedTemplateType == (Int32)EnumReportDisplayType.GridControl)
                    {
                        if (AIMGlobal.templateReportID == (Int32)ReportEnum.UserDefinedSampleData)
                        {
                            //9/9/20 - change to use AIMCoreGlobal
                            //AIMGlobal.skipCompanyDateHeading = true;                                        //5/22/19 - added skip because Company and Date were showing up oddly on report

                            //9/9/20 - change to use AIMCoreGlobal
                            //AIMGlobal.previewGridResults(grdReport, "User-Defined Sample Data");

                            //9/9/20 - change to use AIMCoreGlobal
                            //AIMGlobal.skipCompanyDateHeading = false;
                            CFCAIMCore.Global.skipCompanyDateHeading = true;                                        //5/22/19 - added skip because Company and Date were showing up oddly on report                            
                            CFCAIMCore.Global.previewGridResults(grdReport, "User-Defined Sample Data");                            
                            CFCAIMCore.Global.skipCompanyDateHeading = false;
                        }                        
                    }
                    else if (_selectedTemplateType == (Int32)EnumReportDisplayType.PivotControl)
                    {
                        if (AIMGlobal.templateReportID == (Int32)ReportEnum.UserDefinedSamplePivotData)
                        {

                            //9/9/20 - change to use AIMCoreGlobal
                            //AIMGlobal.skipCompanyDateHeading = true;                                        //5/22/19 - added skip because Company and Date were showing up oddly on report
                            //AIMGlobal.previewPivotResults(pivotGridControl1, "User-Defined Sample Pivot Data");
                            //AIMGlobal.skipCompanyDateHeading = false;
                            CFCAIMCore.Global.skipCompanyDateHeading = true;                                        //5/22/19 - added skip because Company and Date were showing up oddly on report
                            CFCAIMCore.Global.previewPivotResults(pivotGridControl1, "User-Defined Sample Pivot Data");
                            CFCAIMCore.Global.skipCompanyDateHeading = false;
                        }                        
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
                //3/8/23
                txtUserName.Visible = false;
                txtUpdateStamp.Visible = false;
                lblUserName.Visible = false;
                lblUpdateStamp.Visible = false;


                if (_selectedTemplateType == (Int32)EnumReportDisplayType.GridControl)
                {
                    //5/22/19
                    grdReport.DataSource = null;
                    grdReportView.RefreshData();

                    foreach (var column in grdReportView.Columns)
                    {
                        ((DevExpress.XtraGrid.Columns.GridColumn)(column)).GroupIndex = -1;

                        //5/22/19
                        ((DevExpress.XtraGrid.Columns.GridColumn)column).Visible = true;
                    }                   
                    
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

                //5/21/19 - clear criteria
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

                    //3/8/23
                    txtUserName.Text = criteria.UserName;
                    txtUpdateStamp.Text = criteria.UpdateStamp.ToString();


                }

                //5/21/19                
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
            try
            {
                //No query was selected so open view template screen 
                if ((Int32)lueTemplate.EditValue == 0)
                {

                    Stream streamNew = new System.IO.MemoryStream();

                    //4/12/18
                    //grdReportView.SaveLayoutToStream(streamNew);
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
                
                DialogResult dialogResult = MessageBox.Show("Overwrite " + lueTemplate.Text.ToString().TrimEnd() + "?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        //getting layout in stream
                        Stream stream = new System.IO.MemoryStream();

                        //------------------------------------------
                        //5/21/19
                        //grdReportView.SaveLayoutToStream(stream);
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
            AIMGlobal.templateReportID = 0;
        }

        private void btnClearMaterials_Click(Object sender, EventArgs e)
        {
            clearMaterials();
        }
               
        private void rbProducts_CheckedChanged(Object sender, EventArgs e)
        {

            //200606CM - changed this back, until decision is made about showing products      
            lblSelectProduct.Visible = true;
            lueMaterial.Visible = true;
            if (rbProducts.Checked == true)
            {                
                lblSelectProduct.Visible = false;
                lueMaterial.Visible = false;
                                

            }
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
            dataBindSourceLocation();
        }
               

        private void rbIngredients_CheckedChanged(object sender, EventArgs e)
        {
            //200606CM - changed this back, until decision is made about showing products
            //////////6/5/20
            //////////if (rbIngredients.Checked == true)
            //////////{
            //////////    DataBindMaterial((Int32)MaterialTypeEnum.Ingredient);
            //////////}
            
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
                    reportID == (Int32)ReportEnum.SamplesSubmittedReport)                           //8/16/23
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
                        dr = MessageBox.Show("Running this report for ALL materials may take a few minutes. Continue running report?", "Warning ...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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
                    //ANALYSIS DATE COMPARISON REPORT
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
                    //ANALYSIS DETAIL
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
                    //ANALYSIS SUMMARY
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

                        //9/9/20 - change to use AIMCoreGlobal
                        //ReportFunctions.DataSetExportBatchDetail = exportBatch;
                        CFCAIMCore.Global.DataSetExportBatchDetail = exportBatch;

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

                        AIMGlobal.showDate = true;
                        Int32 currentRowNum = 0;
                        List<SampleCustomReportInfo> sampleData = new List<SampleCustomReportInfo>();
                        if (rbProducts.Checked == true)
                        {
                            //2/16/21
                            //sampleData = AIMManager.GetSampleCustomProduct((DateTime)deFromDate.DateTime, (DateTime)deToDate.DateTime, (Int32)MaterialTypeEnum.Product, (Int32)lueOwnerLocation.EditValue);
                            sampleData = AIMManager.GetSampleCustomProduct((DateTime)deFromDate.DateTime, (DateTime)deToDate.DateTime, (Int32)MaterialTypeEnum.Product, (Int32)lueOwnerLocation.EditValue, _groupProducts);
                        }
                        else
                        {
                            //2/16/21
                            //sampleData = AIMManager.GetSampleCustom((DateTime)deFromDate.DateTime, (DateTime)deToDate.DateTime, (Int32)MaterialTypeEnum.Ingredient, (Int32)lueOwnerLocation.EditValue);
                            sampleData = AIMManager.GetSampleCustom((DateTime)deFromDate.DateTime, (DateTime)deToDate.DateTime, (Int32)MaterialTypeEnum.Ingredient, (Int32)lueOwnerLocation.EditValue, _groupProducts);
                        }

                        if (grdReportView != null)
                        {
                            Int32.TryParse(grdReportView.FocusedRowHandle.ToString(), out currentRowNum);
                        }
                        grdReport.DataSource = sampleData;
                        grdReportView.FocusedRowHandle = currentRowNum;

                        //Get alternate column names                        
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
                                                
                        //---------------------------
                        txtdt = deFromDate.DateTime.ToString("yyyy-MM-dd ");
                        txtdt += teFromTime.Time.ToString("HH:mm:ss");
                        amPM = ".000";
                        if (teFromTime.Time.ToString("tt") == "PM")
                        {
                            amPM = ".999";
                        }
                        txtdt += amPM;
                        submitted.FromDateString = txtdt;

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
                            submitted.ToDateString = txtdt;
                        }

                        if ((Int32)lueMaterial.EditValue != 0)
                        {
                            submitted.AIMMaterialID = (Int32)lueMaterial.EditValue;
                        }

                        if (rbProducts.Checked == true)
                        {
                            submitted.MaterialTypeID = 2;
                        }
                        else
                        {
                            submitted.MaterialTypeID = 1;
                        }

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

                        submitted.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                        sampleTestType = AIMManager.GetSampleSubmitted(ref submitted);                        
                        ReportFunctions.DataSetSampleSubmitted = sampleTestType;

                        break;
                    #endregion

                    //------------------------------------------------------            //9/1/23
                    // SAMPLE ANALYSIS REQUEST FORM 
                    #region Sample Analysis Request Form
                    case (Int32)ReportEnum.SampleAnalysisRequestFormEvonik:
                    case (Int32)ReportEnum.SampleAnalysisRequestFormAjinomoto:
                    case (Int32)ReportEnum.SampleAnalysisRequestFormUnitedAnimalHealth:

                        SampleAnalysisRequestFormInfo form = new SampleAnalysisRequestFormInfo();

                        //---------------------------
                        txtdt = deFromDate.DateTime.ToString("yyyy-MM-dd ");
                        txtdt += teFromTime.Time.ToString("HH:mm:ss");
                        amPM = ".000";
                        if (teFromTime.Time.ToString("tt") == "PM")
                        {
                            amPM = ".999";
                        }
                        txtdt += amPM;
                        form.FromDateString = txtdt;

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
                            form.ToDateString = txtdt;
                        }
                                               
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
                        if (rbProducts.Checked == true)
                        {
                            form.MaterialTypeID = 2;
                        }
                        else
                        {
                            form.MaterialTypeID = 1;
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
                    ReportFunctions.LoadReport(reportID);
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

        ////6/7/22 - trying to figure out why date sort does not work for Butterball      
        //private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        //{
        //    pivotGridField8.SortMode = ((CheckEdit)sender).Checked ? PivotSortMode.Custom : PivotSortMode.Default;
        //}

    }
}


