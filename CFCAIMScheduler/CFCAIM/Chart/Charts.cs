using CFCAIMCore;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using CFCUser.Entity;
using CFCUser.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;



namespace CFCAIM.Chart
{
    public partial class Charts : Form
	{
		#region  local variables		

		CFCUser.Global _userGlobal = new CFCUser.Global();
		MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];
		List<AIMMaterialInfo> _aimMaterial = new List<AIMMaterialInfo>();
		List<AIMNutrientInfo> _aimNutrient = new List<AIMNutrientInfo>();
		List<ChartInfo> _chart = new List<ChartInfo>();
		AnalysisQueryEnum _analysisQueryType = AnalysisQueryEnum.SampleDate;
		Int32 _queryTemplateID = 0;
		OutlierTypeEnum _outlierType = OutlierTypeEnum.NoOutlierExcluded;
		tpProdSpec _sample;
		SampleDetailFromAnalysis _sampleListUsingAnalysisQuery;        
        CFCAIM.Chart.ChartsSampleList _sampleList;
        List<GraphInfo> _details = new List<GraphInfo>();
        List<GraphProductControlInfo> _detailsProductControl = new List<GraphProductControlInfo>();
        List<AIMOwnerLocationInfo> _aimOwnerLocation = new List<AIMOwnerLocationInfo>();        
        AnalysisProductSampleDetail _analysisProductSampleDetail;                //1/4/23

        
        UserInfo _user = new UserInfo();                                         //10/17/23
        Int32 _ownerLocationGroupID = 0;                                         //10/17/23
        Int32 _supplierGroupID = 0;                                              //10/17/23
        Int32 _supplierLocationGroupID = 0;                                      //10/17/23

        //private object userGlobal;

        #endregion

        public Charts()
		{
			InitializeComponent();			
		}
		private void charts_Load(object sender, EventArgs e)
		{
			try
			{

                //10/17/23
                SetUserGroups();


                //7/27/22 - moved from databind of material
                //This sets the dates if coming from Analysis
                if (AIMGlobal.chartBegDate.ToString() != "")
                {
                    deFromDate.EditValue = AIMGlobal.chartBegDate;
                }
                if (AIMGlobal.chartEndDate.ToString() != "")
                {
                    deToDate.EditValue = AIMGlobal.chartEndDate;
                }

                //7/22/21
                DataBindMaterial();
                DataBindNutrient();

                dataBind();

                //12/27/22                
                if (AIMGlobal.comingFromAnalysis == true)
                {
                    gbIngredientOrProduct.Visible = false;
                    if (AIMGlobal.fromAnalysisIngredientOrProduct == (Int32)EnumIngredientOrProduct.AnalysisProduct)
                    {
                        rbControlProduct.Checked = true;
                    }
                    else
                    {
                        rbControlIngredient.Checked = true;
                    }
                }
                else
                {
                    rbControlIngredient.Checked = true;                    
                }

                //12/29/22
                if (AIMGlobal.groupMaterials == (Int32)EnumTolerance.ProductTolerance)
                {
                    rbProductTolerance.Checked = true;
                }
                else if (AIMGlobal.groupMaterials == (Int32)EnumTolerance.NutrientTolerance)
                {
                    rbNutrientTolerance.Checked = true;
                }
                else
                {
                    rbToleranceNone.Checked = true;
                    lueNutrientTolerance.EditValue = 0;
                }

                //2/4/20 - added supplier locations
                dataBindSupplierLocationlue(lueSupplier, lueSupplierLocation);
                dataBindSupplierLocationlue(lueSupplier2, lueSupplierLocation2);
                dataBindSupplierLocationlue(lueSupplier3, lueSupplierLocation3);
                dataBindSupplierLocationlue(lueSupplier4, lueSupplierLocation4);

                //7/9/21 - added owner and owner locations
                dataBindOwnerLocationlue(lueOwner, lueOwnerLocation);                               
                dataBindOwnerLocationlue(lueOwner2, lueOwnerLocation2);
                dataBindOwnerLocationlue(lueOwner3, lueOwnerLocation3);
                dataBindOwnerLocationlue(lueOwner4, lueOwnerLocation4);

                //12/29/22
                dataBindTolerancelue();

                hideCharts();               
				checkGlobals();
				setToolTips();
                              
                
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
        private void btnGetChartData_Click(object sender, EventArgs e)
        {
            
            #region Set Criteria
                       

            _analysisQueryType = AnalysisQueryEnum.SampleDate;
            if (rbSampleNumber.Checked == true)
            {
                _analysisQueryType = AnalysisQueryEnum.SampleID;
            }

            _queryTemplateID = AIMGlobal.chartQueryTemplateID;

            if ((Int32)lueMaterial.EditValue == 0)
            {
                MessageBox.Show("Please select Material.", "Warning...");
                lueMaterial.ShowPopup();
                return;
            }
            if ((Int32)lueNutrient.EditValue == 0)
            {
                MessageBox.Show("Please select Nutrient.", "Warning...");
                lueNutrient.ShowPopup();
                return;
            }
            if (rbAdvancedAnalysis.Checked == true && (Int32)lueNutrient2.EditValue == 0)
            {
                MessageBox.Show("Please select second Nutrient.", "Warning...");
                lueNutrient2.ShowPopup();
                return;
            }
            if (rbSupplierCompare.Checked == true && (Int32)lueSupplier.EditValue == 0)
            {
                MessageBox.Show("Please select Supplier.", "Warning...");

                //5/21/20                
                ckbSupp1.Checked = true;

                lueSupplier.ShowPopup();

                return;
            }

            //5/21/20
            //if (rbSupplierCompare.Checked == true && (Int32)lueSupplier2.EditValue == 0)
            if (rbSupplierCompare.Checked == true && ((Int32)lueSupplier2.EditValue == 0 && (Int32)lueSupplier3.EditValue == 0 && (Int32)lueSupplier4.EditValue == 0))

            {

                //5/21/20
                //MessageBox.Show("Please select second Supplier.", "Warning...");
                MessageBox.Show("Please select at least one more Supplier.", "Warning...");
                ckbSupp2.Checked = true;

                lueSupplier2.ShowPopup();

                return;
            }

            if (rbDataSourceCompare.Checked == true && (Int32)lueDataSource.EditValue == 0)
            {
                MessageBox.Show("Please select Data Source.", "Warning...");
                ckDataSource1.Checked = true;
                lueDataSource.ShowPopup();
                return;
            }

            //5/22/20
            if (rbDataSourceCompare.Checked == true && ((Int32)lueDataSource2.EditValue == 0 && (Int32)lueDataSource3.EditValue == 0 && (Int32)lueDataSource4.EditValue == 0))
            {
                MessageBox.Show("Please select at least one more Data Source.", "Warning...");
                ckDataSource2.Checked = true;
                lueDataSource2.ShowPopup();
                return;
            }

            if (deToDate.Text == "")
            {
                deToDate.EditValue = DateTime.Now;
            }
            if (deFromDate.Text == "")
            {
                deFromDate.EditValue = "1/1/2000";
            }
                       
            
            clearDataSources();

            ChartInfo chart = new ChartInfo();
            Int32 sampleID;
            chartTrend.Visible = false;
            chartTrendByDate.Visible = false;                        //2/5/20
            chartGraphControl.Visible = false;
            chartGraphControlBySampleCode.Visible = false;
            chartAdvancedAnalysis.Visible = false;

            //12/23/22            
            chartProductControl.Visible = false;
            chartProductControlByDate.Visible = false;

            Int32 aimMaterialID = (Int32)lueMaterial.EditValue;
            Int32 aimNutrientID = (Int32)lueNutrient.EditValue;
            Int32 aimNutrientID2 = (Int32)lueNutrient2.EditValue;
            Int32 aimSupplierID = (Int32)lueSupplier.EditValue;
            Int32 aimSupplierID2 = (Int32)lueSupplier2.EditValue;
            Int32 aimSupplierID3 = (Int32)lueSupplier3.EditValue;
            Int32 aimSupplierID4 = (Int32)lueSupplier4.EditValue;

            Int32 aimSupplierLocationID = (Int32)lueSupplierLocation.EditValue;
            Int32 aimSupplierLocationID2 = (Int32)lueSupplierLocation2.EditValue;
            Int32 aimSupplierLocationID3 = (Int32)lueSupplierLocation3.EditValue;
            Int32 aimSupplierLocationID4 = (Int32)lueSupplierLocation4.EditValue;

            //7/9/21
            Int32 aimOwnerID = (Int32)lueOwner.EditValue;
            Int32 aimOwnerID2 = (Int32)lueOwner2.EditValue;
            Int32 aimOwnerID3 = (Int32)lueOwner3.EditValue;
            Int32 aimOwnerID4 = (Int32)lueOwner4.EditValue;
            Int32 aimOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
            Int32 aimOwnerLocationID2 = (Int32)lueOwnerLocation2.EditValue;
            Int32 aimOwnerLocationID3 = (Int32)lueOwnerLocation3.EditValue;
            Int32 aimOwnerLocationID4 = (Int32)lueOwnerLocation4.EditValue;

            //9/8/21
            Int32 chartOption = 0;
            if (rbSupplierOnly.Checked == true)
            {
                chartOption = 1;
            }
            if (rbOwnerOnly.Checked == true)
            {
                chartOption = 2;
            }
            else
            {
                chartOption = 3;
            }


            //9/1/21 ------------------------------
            if (ckbOwn1.Checked == false)
            {
                aimOwnerID = 0;
                aimOwnerLocationID = 0;
            }
            if (ckbOwn2.Checked == false)
            {
                aimOwnerID2 = 0;
                aimOwnerLocationID2 = 0;
            }
            if (ckbOwn3.Checked == false)
            {
                aimOwnerID3 = 0;
                aimOwnerLocationID3 = 0;
            }
            if (ckbOwn4.Checked == false)
            {
                aimOwnerID4 = 0;
                aimOwnerLocationID4 = 0;
            }


            DateTime startDt = Convert.ToDateTime(deFromDate.Text);
            DateTime endDt = Convert.ToDateTime(deToDate.Text);


            //Outliers
            _outlierType = OutlierTypeEnum.NoOutlierExcluded;
            if (rbMildOutlierExclude.Checked == true)
            {
                _outlierType = OutlierTypeEnum.ExtremeAndMildExcluded;
            }
            else if (rbExtremeOutlierExclude.Checked == true)
            {
                _outlierType = OutlierTypeEnum.ExtremeExcluded;
            }


            #endregion

            #region Control Chart INGREDIENT ------------------------------------------
            //Control Chart
            if (rbControlChart.Checked == true)
            {
				
				//12/23/22				
				if (rbControlIngredient.Checked == true)
                {               

                    AIMGlobal.SplashScreenShow();
                    List<AnalysisInfo> analysis = new List<AnalysisInfo>();
                    if (rbSampleNumber.Checked == true)
                    {
                        _analysisQueryType = AnalysisQueryEnum.SampleID;
                    }

                    DateTime sampleDate = DateTime.MinValue;

                    //7/28/21
                    AIMCoreGlobal.LoadCustomEquation();

                    //8/2/21 - another change for Randy at Perdue -- to be able to run chart for Supplier, for Owner, or for both Supplier and Owner
                    //6/28/21 - changed for Randy at Perdue
                    //analysis = AIMManager.GetAnalysis(_analysisQueryType, _queryTemplateID, startDt, endDt, false, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDate, 0, 0, 0, 0, 0, 1, 1, 0, "", 0, _outlierType, 0, 0);
                    //analysis = AIMManager.GetAnalysis(_analysisQueryType, _queryTemplateID, startDt, endDt, false, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDate, 0, (Int32)lueOwner.EditValue, (Int32)lueOwnerLocation.EditValue, (Int32)lueSupplier.EditValue, (Int32)lueSupplierLocation.EditValue, 1, 1, 0, "", 0, _outlierType, 0, 0, true,(Int32)lueSupplier2.EditValue, (Int32)lueSupplier3.EditValue, (Int32)lueSupplier4.EditValue, (Int32)lueSupplierLocation2.EditValue, (Int32)lueSupplierLocation3.EditValue, (Int32)lueSupplierLocation4.EditValue);                
                    ////analysis = AIMManager.GetAnalysis(_analysisQueryType, _queryTemplateID, startDt, endDt, false, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDate, 0, (Int32)lueOwner.EditValue, (Int32)lueOwnerLocation.EditValue, (Int32)lueSupplier.EditValue, (Int32)lueSupplierLocation.EditValue, 1, 1, 0, "", 0, _outlierType, 0, 0, true, (Int32)lueSupplier2.EditValue, (Int32)lueSupplier3.EditValue, (Int32)lueSupplier4.EditValue, (Int32)lueSupplierLocation2.EditValue, (Int32)lueSupplierLocation3.EditValue, (Int32)lueSupplierLocation4.EditValue, (Int32)lueOwner2.EditValue, (Int32)lueOwner3.EditValue, (Int32)lueOwner4.EditValue, (Int32)lueOwnerLocation2.EditValue, (Int32)lueOwnerLocation3.EditValue, (Int32)lueOwnerLocation4.EditValue);
                    if (rbSupplierOnly.Checked == true)
                    {     
                        //1/9/23 - aimDataBaseID not getting set
                        //analysis = AIMManager.GetAnalysis(_analysisQueryType, _queryTemplateID, startDt, endDt, false, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDate, 0, 0, 0, (Int32)lueSupplier.EditValue, (Int32)lueSupplierLocation.EditValue, 1, 1, 0, "", 0, _outlierType, 0, 0, true, (Int32)lueSupplier2.EditValue, (Int32)lueSupplier3.EditValue, (Int32)lueSupplier4.EditValue, (Int32)lueSupplierLocation2.EditValue, (Int32)lueSupplierLocation3.EditValue, (Int32)lueSupplierLocation4.EditValue, 0, 0, 0, 0, 0, 0, chartOption);
                        analysis = AIMManager.GetAnalysis(_analysisQueryType, _queryTemplateID, startDt, endDt, false, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDate, 0, 0, 0, (Int32)lueSupplier.EditValue, (Int32)lueSupplierLocation.EditValue, 1, 1, 0, "", (Int32)_mainMenu.lueDataMap.EditValue, _outlierType, 0, 0, true, (Int32)lueSupplier2.EditValue, (Int32)lueSupplier3.EditValue, (Int32)lueSupplier4.EditValue, (Int32)lueSupplierLocation2.EditValue, (Int32)lueSupplierLocation3.EditValue, (Int32)lueSupplierLocation4.EditValue, 0, 0, 0, 0, 0, 0, chartOption);
                    }
                    else if (rbOwnerOnly.Checked == true)
                    {
                        //1/9/23 - aimDataBaseID not getting set                        
                        //analysis = AIMManager.GetAnalysis(_analysisQueryType, _queryTemplateID, startDt, endDt, false, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDate, 0, aimOwnerID, aimOwnerLocationID, 0, 0, 1, 1, 0, "", 0, _outlierType, 0, 0, true, 0, 0, 0, 0, 0, 0, aimOwnerID2, aimOwnerID3, aimOwnerID4, aimOwnerLocationID2, aimOwnerLocationID3, aimOwnerLocationID4, chartOption);
                        analysis = AIMManager.GetAnalysis(_analysisQueryType, _queryTemplateID, startDt, endDt, false, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDate, 0, aimOwnerID, aimOwnerLocationID, 0, 0, 1, 1, 0, "", (Int32)_mainMenu.lueDataMap.EditValue, _outlierType, 0, 0, true, 0, 0, 0, 0, 0, 0, aimOwnerID2, aimOwnerID3, aimOwnerID4, aimOwnerLocationID2, aimOwnerLocationID3, aimOwnerLocationID4, chartOption);
                    }
                    else
                    {
                        //1/9/23 - aimDataBaseID not getting set                        
                        //analysis = AIMManager.GetAnalysis(_analysisQueryType, _queryTemplateID, startDt, endDt, false, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDate, 0, aimOwnerID, aimOwnerLocationID, (Int32)lueSupplier.EditValue, (Int32)lueSupplierLocation.EditValue, 1, 1, 0, "", 0, _outlierType, 0, 0, true, (Int32)lueSupplier2.EditValue, (Int32)lueSupplier3.EditValue, (Int32)lueSupplier4.EditValue, (Int32)lueSupplierLocation2.EditValue, (Int32)lueSupplierLocation3.EditValue, (Int32)lueSupplierLocation4.EditValue, aimOwnerID2, aimOwnerID3, aimOwnerID4, aimOwnerLocationID2, aimOwnerLocationID3, aimOwnerLocationID4, chartOption);
                        analysis = AIMManager.GetAnalysis(_analysisQueryType, _queryTemplateID, startDt, endDt, false, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDate, 0, aimOwnerID, aimOwnerLocationID, (Int32)lueSupplier.EditValue, (Int32)lueSupplierLocation.EditValue, 1, 1, 0, "", (Int32)_mainMenu.lueDataMap.EditValue, _outlierType, 0, 0, true, (Int32)lueSupplier2.EditValue, (Int32)lueSupplier3.EditValue, (Int32)lueSupplier4.EditValue, (Int32)lueSupplierLocation2.EditValue, (Int32)lueSupplierLocation3.EditValue, (Int32)lueSupplierLocation4.EditValue, aimOwnerID2, aimOwnerID3, aimOwnerID4, aimOwnerLocationID2, aimOwnerLocationID3, aimOwnerLocationID4, chartOption);
                    }

                    foreach (AnalysisInfo item in analysis)
                    {
                        if (item.IsInclude == true)
                        {
                            if (aimMaterialID == item.AIMMaterialID && aimNutrientID == item.AIMNutrientID)
                            {
                            
                                Int32.TryParse(item.SampleID.ToString(), out sampleID);
                                fillDetail(ref _details, item.AIMNutrientName, item.SampleValue, aimMaterialID, aimNutrientID, item.SampleDate, sampleID, item.SampleCode);
                                fillDetail(ref _details, "Mean", item.TrimmedMean, aimMaterialID, aimNutrientID, item.SampleDate, sampleID, item.SampleCode);
                                fillDetail(ref _details, "Extreme Upper Outlier", item.TrimmedOutlierUpperExtreme, aimMaterialID, aimNutrientID, item.SampleDate, sampleID, item.SampleCode);
                                fillDetail(ref _details, "Extreme Lower Outlier", item.TrimmedOutlierLowerExtreme, aimMaterialID, aimNutrientID, item.SampleDate, sampleID, item.SampleCode);
                                fillDetail(ref _details, "Proposed Value", item.ProposedNutrientValue, aimMaterialID, aimNutrientID, item.SampleDate, sampleID, item.SampleCode);

                                //10/26/22
                                ////fillDetail(ref _details, "Current Nutrient Value", item.CurrentNutrientVal, aimMaterialID, aimNutrientID, item.SampleDate, sampleID, item.SampleCode);
                            }
                        }
                    }

                    //9/21/18 - commented - this was not working.  So created a separate chart for SampleCode
                    //////XYDiagram diagram = new XYDiagram();
                    //////diagram = (XYDiagram)chartGraphControl.Diagram;
                    //////if (_analysisQueryType == AnalysisQueryEnum.SampleID)
                    //////{                    
                    //////    chartGraphControl.SeriesTemplate.ArgumentDataMember = "SampleID";
                    //////    diagram.AxisX.Title.Text = "Sample Code";
                    //////}
                    //////else
                    //////{
                    //////    chartGraphControl.SeriesTemplate.ArgumentDataMember = "SampleDate";
                    //////    diagram.AxisX.Title.Text = "Sample Date";
                    //////}

                    if (_analysisQueryType == AnalysisQueryEnum.SampleDate)
                    {
                        changeSizeAndLocation(chartGraphControl);
                        chartGraphControl.DataSource = _details;
                    }
                    else
                    {
                        changeSizeAndLocation(chartGraphControlBySampleCode);
                        chartGraphControlBySampleCode.DataSource = _details;
                    }

                    //-----------------------------------------------------------------------------
                    //1/31/20 - added this so the cursor moves down, within the chart area, to remove the extra dots on all lines except the Nutrient line.
                    //          (The dots show up on all the lines until the cursor moves within the chart area!? -- so manually moving the cursor.)
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X - 100, Cursor.Position.Y + 200);
                    //-----------------------------------------------------------------------------

                    AIMGlobal.SplashScreenHide();
                    return;
                }

            }
            #endregion

            #region Control Chart PRODUCT ---------------------------------------------
            if (rbControlChart.Checked == true)
            {
                if (rbControlProduct.Checked == true)
                {
                    Double resultValue = 0;
                    AIMGlobal.SplashScreenShow();
                    List<AnalysisProductInfo> analysis = new List<AnalysisProductInfo>();
                    Int32 groupMaterials = AIMGlobal.groupMaterials;
                    Int32 nutrientToleranceHeaderID = AIMGlobal.nutrientToleranceHeaderID;
                    Double saveResultValueHigh = 0;
                    Double saveResultValueLow = 0;
                    Double min = 0;
                    Double max = 0;

                    //if (rbSampleNumber.Checked == true)
                    //{
                    //    _analysisQueryType = AnalysisQueryEnum.SampleID;
                    //}
                    _analysisQueryType = AnalysisQueryEnum.SampleID;

                    //------------------------------------                
                    if (rbProductTolerance.Checked == true)
                    {
                        groupMaterials = (Int32)EnumTolerance.ProductTolerance;
                    }
                    if (rbNutrientTolerance.Checked == true)
                    {
                        groupMaterials = (Int32)EnumTolerance.NutrientTolerance;
                        nutrientToleranceHeaderID = (Int32)lueNutrientTolerance.EditValue;
                    }
                    //------------------------------------

                    DateTime sampleDate = DateTime.MinValue;
                    
                  
                    AIMCoreGlobal.LoadCustomEquation();                    
                    
                    analysis = AIMManager.GetAnalysisProduct(_analysisQueryType, _queryTemplateID, startDt, endDt, groupMaterials, nutrientToleranceHeaderID, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDate, 0, 0, 0, "", (Int32)_mainMenu.lueDataMap.EditValue, _outlierType, AIMGlobal.chartArchiveVersion);
                   

                    chartProductControl.Series[0].Name = "Sample Value";
                    chartProductControlByDate.Series[0].Name = "Sample Date";

                    foreach (AnalysisProductInfo item in analysis)
                    {
                        if (item.IsInclude == true)
                        {
                            if (aimMaterialID == item.AIMMaterialID && aimNutrientID == item.AIMNutrientID && item.ArchiveVersion == AIMGlobal.chartArchiveVersion)
                            {
                                Int32.TryParse(item.SampleID.ToString(), out sampleID);                                
                                resultValue = (item.PredictedValue - item.SampleValue) * -1;                                
                                fillDetail(ref _details, item.AIMNutrientName, resultValue, aimMaterialID, aimNutrientID, item.SampleDate, sampleID, item.SampleCode);
                                min = item.PredictedMinAmount * -1;
                                max = item.PredictedMaxAmount;

                                
                                if (resultValue > saveResultValueHigh)
                                {
                                    saveResultValueHigh = resultValue;
                                }
                                if (resultValue < saveResultValueLow)
                                {
                                    saveResultValueLow = resultValue;
                                }
                            }
                        }
                    }

                    XYDiagram diagram;


                    //if (_analysisQueryType == AnalysisQueryEnum.SampleDate)
                    if (rbDate.Checked == true)
                    {
                        changeSizeAndLocation(chartProductControlByDate);
                        chartProductControlByDate.DataSource = _details;                        
                        diagram = chartProductControlByDate.Diagram as XYDiagram;
                    }
                    else
                    {
                        changeSizeAndLocation(chartProductControl);
                        chartProductControl.DataSource = _details;                        
                        diagram = chartProductControl.Diagram as XYDiagram;
                    }                                      

                    diagram.AxisY.ConstantLines.Clear();

                    // Create constant lines.
                    ConstantLine constantLine1 = new ConstantLine();
                    ConstantLine constantLine2 = new ConstantLine();
                    ConstantLine constantLine3 = new ConstantLine();
                    diagram.AxisY.ConstantLines.Add(constantLine1);
                    diagram.AxisY.ConstantLines.Add(constantLine2);
                    diagram.AxisY.ConstantLines.Add(constantLine3);

                    // Define their axis values -- 
                    constantLine1.AxisValue = max;
                    constantLine2.AxisValue = min;
                    constantLine3.AxisValue = 0;

                    // Customize the behavior of the constant line.
                    constantLine1.Visible = true;
                    constantLine1.ShowInLegend = true;
                    constantLine1.LegendText = "Predicted Max Amount";
                    constantLine1.ShowBehind = false;

                    constantLine2.Visible = true;
                    constantLine2.ShowInLegend = true;
                    constantLine2.LegendText = "Predicted Min Amount";
                    constantLine2.ShowBehind = false;

                    constantLine3.ShowInLegend = false;

                    diagram.AxisY.WholeRange.AutoSideMargins = false;
                    diagram.AxisY.WholeRange.SideMarginsValue = 0;
                    diagram.AxisY.WholeRange.Auto = false;

                    if (saveResultValueHigh > max)
                    {
                        max = saveResultValueHigh;
                    }
                    if (saveResultValueLow < min)
                    {
                        min = saveResultValueLow;
                    }
                    diagram.AxisY.WholeRange.SetMinMaxValues(min - .05, max + .05);

                    // Customize the constant line's title.
                    //constantLine1.Title.Visible = true;
                    //constantLine1.Title.Text = "Constant Line Title";
                    //constantLine1.Title.TextColor = Color.Red;
                    //constantLine1.Title.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
                    ////constantLine1.Title.DXFont = new DXFont("Tahoma", 14, DXFontStyle.Bold);
                    //constantLine1.Title.ShowBelowLine = true;
                    //constantLine1.Title.Alignment = ConstantLineTitleAlignment.Far;

                    // Customize the appearance of the constant line.
                    constantLine1.Color = Color.GreenYellow;
                    constantLine2.Color = Color.Orange;
                    constantLine3.Color = Color.Red;
                    constantLine1.LineStyle.Thickness = 5;
                    constantLine2.LineStyle.Thickness = 5;
                    constantLine3.LineStyle.Thickness = 5;
                    //constantLine1.LineStyle.DashStyle = DashStyle.Dash;


                    //-----------------------------------------------------------------------------
                    //This moves the cursor down, within the chart area, to remove the extra dots on all lines except the Nutrient line.
                    //          (The dots show up on all the lines until the cursor moves within the chart area!? -- so manually moving the cursor.)
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X - 100, Cursor.Position.Y + 200);
                    //-----------------------------------------------------------------------------

                    AIMGlobal.SplashScreenHide();
                    return;

                }
            }
            #endregion
                       
            #region Trend By SampleCode -----------------------------------------------
            //Trend By SampleCode

            //9/20/22 - split into two sections: Trend by SampleCode and Trend by Date (because of problem where series was getting
            //          cleared out (see 7/27/22 comment below) and never getting re-set). Splitting these up seemed to solve the problem.

            if (rbTrend.Checked == true && rbSampleNumber.Checked == true)
            {
                AIMGlobal.SplashScreenShow();

                //9/20/22 - commented and split the Trend into two (by samplecode and by date -- see comment above.)
                //7/27/22 - removes any existing series/data from the charts - for now, clearing the series for just this chart, because
                //          have not had problems with any of the other charts. Smithfield uses Trend and found this.                
                //chartTrend.Series.Clear();
                
                chart.AIMMaterialID = aimMaterialID;
                chart.AIMNutrientID = aimNutrientID;
                chart.StartDate = startDt;
                chart.EndDate = endDt;

                //8/2/21 - added for Randy at Perdue so chart can be run for Supplier, Owner, or both Supplier and Owner
                if (rbSupplierOnly.Checked == true || rbSupplierAndOwner.Checked == true)
                {
                    //2/4/20 - added suppliers and supplier-locations for Randy at Perdue                
                    chart.AIMSupplierID = aimSupplierID;
                    chart.AIMSupplierID2 = aimSupplierID2;
                    chart.AIMSupplierID3 = aimSupplierID3;
                    chart.AIMSupplierID4 = aimSupplierID4;
                    chart.AIMSupplierLocationID = aimSupplierLocationID;
                    chart.AIMSupplierLocationID2 = aimSupplierLocationID2;
                    chart.AIMSupplierLocationID3 = aimSupplierLocationID3;
                    chart.AIMSupplierLocationID4 = aimSupplierLocationID4;
                    chart.SupplierOwnerOption = chartOption;                            //9/8/21


                }
                if (rbOwnerOnly.Checked == true || rbSupplierAndOwner.Checked == true)
                {
                    //6/28/21 - added owner and owner-location for Randy at Perdue
                    chart.AIMOwnerID = aimOwnerID;
                    chart.AIMOwnerID2 = aimOwnerID2;
                    chart.AIMOwnerID3 = aimOwnerID3;
                    chart.AIMOwnerID4 = aimOwnerID4;
                    chart.AIMOwnerLocationID = aimOwnerLocationID;
                    chart.AIMOwnerLocationID2 = aimOwnerLocationID2;
                    chart.AIMOwnerLocationID3 = aimOwnerLocationID3;
                    chart.AIMOwnerLocationID4 = aimOwnerLocationID4;
                    chart.SupplierOwnerOption = chartOption;                            //9/8/21
                }
                _chart = AIMManager.GetTrendChartData(ref chart);
                changeSizeAndLocation(chartTrend);
                setLegendNames_Trend(chartTrend);                                 //2/10/10
                hideShowLegendNames_Trend(chartTrend);                            //2/10/20                    
                chartTrend.DataSource = _chart;

                AIMGlobal.SplashScreenHide();
                return;
            }
            #endregion

            #region Trend By Date -----------------------------------------------------            
            //Trend By Date
            if (rbTrend.Checked == true && rbDate.Checked == true)
            {
                AIMGlobal.SplashScreenShow();                

                chart.AIMMaterialID = aimMaterialID;
                chart.AIMNutrientID = aimNutrientID;
                chart.StartDate = startDt;
                chart.EndDate = endDt;

                //8/2/21 - added for Randy at Perdue so chart can be run for Supplier, Owner, or both Supplier and Owner
                if (rbSupplierOnly.Checked == true || rbSupplierAndOwner.Checked == true)
                {
                    //2/4/20 - added suppliers and supplier-locations for Randy at Perdue                
                    chart.AIMSupplierID = aimSupplierID;
                    chart.AIMSupplierID2 = aimSupplierID2;
                    chart.AIMSupplierID3 = aimSupplierID3;
                    chart.AIMSupplierID4 = aimSupplierID4;
                    chart.AIMSupplierLocationID = aimSupplierLocationID;
                    chart.AIMSupplierLocationID2 = aimSupplierLocationID2;
                    chart.AIMSupplierLocationID3 = aimSupplierLocationID3;
                    chart.AIMSupplierLocationID4 = aimSupplierLocationID4;
                    chart.SupplierOwnerOption = chartOption;                            //9/8/21


                }
                if (rbOwnerOnly.Checked == true || rbSupplierAndOwner.Checked == true)
                {
                    //6/28/21 - added owner and owner-location for Randy at Perdue
                    chart.AIMOwnerID = aimOwnerID;
                    chart.AIMOwnerID2 = aimOwnerID2;
                    chart.AIMOwnerID3 = aimOwnerID3;
                    chart.AIMOwnerID4 = aimOwnerID4;
                    chart.AIMOwnerLocationID = aimOwnerLocationID;
                    chart.AIMOwnerLocationID2 = aimOwnerLocationID2;
                    chart.AIMOwnerLocationID3 = aimOwnerLocationID3;
                    chart.AIMOwnerLocationID4 = aimOwnerLocationID4;
                    chart.SupplierOwnerOption = chartOption;                            //9/8/21
                }
                _chart = AIMManager.GetTrendChartData(ref chart);
                changeSizeAndLocation(chartTrendByDate);
                setLegendNames_Trend(chartTrendByDate);                           //2/10/20
                hideShowLegendNames_Trend(chartTrendByDate);                      //2/10/20                    
                chartTrendByDate.DataSource = _chart;                

                AIMGlobal.SplashScreenHide();
                return;
            }
            #endregion

            #region AdvancedAnalysis --------------------------------------------------            
            //AdvancedAnalysis
            if (rbAdvancedAnalysis.Checked == true)
            {
                AIMGlobal.SplashScreenShow();
                changeSizeAndLocation(chartAdvancedAnalysis);

                chart.AIMMaterialID = aimMaterialID;
                chart.AIMNutrientID = aimNutrientID;
                chart.AIMNutrientID2 = aimNutrientID2;
                chart.StartDate = startDt;
                chart.EndDate = endDt;

                //8/2/21 - added for Randy at Perdue so chart can be run for Supplier, Owner, or both Supplier and Owner
                if (rbSupplierOnly.Checked == true || rbSupplierAndOwner.Checked == true)
                {
                    //6/28/21 - added for Randy at Perdue  
                    chart.AIMSupplierID = aimSupplierID;
                    chart.AIMSupplierID2 = aimSupplierID2;
                    chart.AIMSupplierID3 = aimSupplierID3;
                    chart.AIMSupplierID4 = aimSupplierID4;
                    chart.AIMSupplierLocationID = aimSupplierLocationID;
                    chart.AIMSupplierLocationID2 = aimSupplierLocationID2;
                    chart.AIMSupplierLocationID3 = aimSupplierLocationID3;
                    chart.AIMSupplierLocationID4 = aimSupplierLocationID4;
                    chart.AIMOwnerID = aimOwnerID;
                    chart.AIMOwnerLocationID = aimOwnerLocationID;
                    chart.SupplierOwnerOption = chartOption;                                //9/8/21

                }
                if (rbOwnerOnly.Checked == true || rbSupplierAndOwner.Checked == true)
                {
                    //7/12/21 - added owner and owner-location for Randy at Perdue
                    chart.AIMOwnerID = aimOwnerID;
                    chart.AIMOwnerID2 = aimOwnerID2;
                    chart.AIMOwnerID3 = aimOwnerID3;
                    chart.AIMOwnerID4 = aimOwnerID4;
                    chart.AIMOwnerLocationID = aimOwnerLocationID;
                    chart.AIMOwnerLocationID2 = aimOwnerLocationID2;
                    chart.AIMOwnerLocationID3 = aimOwnerLocationID3;
                    chart.AIMOwnerLocationID4 = aimOwnerLocationID4;
                    chart.SupplierOwnerOption = chartOption;                                //9/8/21
                }

                _chart = AIMManager.GetAdvancedAnalysisChartData(ref chart);
                chartAdvancedAnalysis.DataSource = _chart;

                //7/14/21
                //chartAdvancedAnalysis.Series[0].Name = lueNutrient.Text.ToString();
                //chartAdvancedAnalysis.Series[1].Name = lueNutrient2.Text.ToString();
                chartAdvancedAnalysis.Series[0].Name = lueNutrient.GetColumnValue("AIMNutrientName").ToString();
                chartAdvancedAnalysis.Series[1].Name = lueNutrient2.GetColumnValue("AIMNutrientName").ToString();

                AIMGlobal.SplashScreenHide();


                return;
            }
            #endregion ----------------------------------------------------------                    

            #region Supplier Compare --------------------------------------------------            
            //Supplier Compare
            if (rbSupplierCompare.Checked == true)
            {
                AIMGlobal.SplashScreenShow();
                changeSizeAndLocation(chartSupplierCompare);

                chart.AIMMaterialID = aimMaterialID;
                chart.AIMNutrientID = aimNutrientID;
                chart.AIMSupplierID = aimSupplierID;
                chart.AIMSupplierID2 = aimSupplierID2;
                chart.AIMSupplierID3 = aimSupplierID3;                                  // added 5/21/20
                chart.AIMSupplierID4 = aimSupplierID4;                                  // added 5/21/20

                //6/28/21 - added for Randy at Perdue
                chart.AIMSupplierLocationID = aimSupplierLocationID;
                chart.AIMSupplierLocationID2 = aimSupplierLocationID2;
                chart.AIMSupplierLocationID3 = aimSupplierLocationID3;
                chart.AIMSupplierLocationID4 = aimSupplierLocationID4;
                chart.StartDate = startDt;
                chart.EndDate = endDt;
                _chart = AIMManager.GetSupplierCompareChartData(ref chart);
                chartSupplierCompare.DataSource = _chart;
                //chartSupplierCompare.Series[0].Name = lueSupplier.Text.ToString();    //commented 6/4/20 (handled below)
                //chartSupplierCompare.Series[1].Name = lueSupplier2.Text.ToString();   //commented 6/4/20 (handled below)
                AIMGlobal.SplashScreenHide();

                setLegendNames_SupplierComparison(chartSupplierCompare);                // added 5/21/20
                hideUnusedLegendNames_Suppliers(chartSupplierCompare);           // added 5/21/20


                return;
            }
            #endregion

            #region DataSource Comparison ---------------------------------------------            
            if (rbDataSourceCompare.Checked == true)
            {
                AIMGlobal.SplashScreenShow();

                chart.AIMMaterialID = aimMaterialID;
                chart.AIMNutrientID = aimNutrientID;
                chart.StartDate = startDt;
                chart.EndDate = endDt;
                chart.DataSourceTypeID = (Int32)lueDataSource.EditValue;
                chart.DataSourceTypeID2 = (Int32)lueDataSource2.EditValue;
                chart.DataSourceTypeID3 = (Int32)lueDataSource3.EditValue;
                chart.DataSourceTypeID4 = (Int32)lueDataSource4.EditValue;
                _chart = AIMManager.GetDataSourceComparisonChartData(ref chart);
                chartDataSourceCompare.DataSource = _chart;
                AIMGlobal.SplashScreenHide();
                setLegendNames_DataSourceComparison(chartDataSourceCompare);
                hideUnusedLegendNames_DataSource(chartDataSourceCompare);
                changeSizeAndLocation(chartDataSourceCompare);


                return;
            }
            #endregion---------------------------------
                      
        }

        #region DataBind

        //7/22/21 - moved from dataBind to its own function
        private void DataBindMaterial()
        {
            Int32 materialTypeID = 1;

            //7/27/22 - moved to load event
            ////2/18/22
            //if (AIMGlobal.chartBegDate.ToString() != "")
            //{
            //    deFromDate.EditValue = AIMGlobal.chartBegDate;
            //}
            //if (AIMGlobal.chartEndDate.ToString() != "")
            //{
            //    deToDate.EditValue = AIMGlobal.chartEndDate;
            //}

            if (deToDate.Text == "")
            {
                deToDate.EditValue = DateTime.Now;
            }
            if (deFromDate.Text == "")
            {
                //7/27/22
                //deFromDate.EditValue = "1/1/2000";
                deFromDate.EditValue = Convert.ToDateTime("1/1/2000");
            }

            //1/6/23
            if (rbControlProduct.Checked == true)
            {
                materialTypeID = 2;
            }

            //7/23/21 - Filter materials for those only on samples between start/end dates
            //_aimMaterial = AIMManager.GetAIMMaterial(true);

            //1/6/23
            //_aimMaterial = AIMManager.GetAIMMaterialForSampleDates(true, Convert.ToDateTime(deFromDate.EditValue), Convert.ToDateTime(deToDate.EditValue));
            _aimMaterial = AIMManager.GetAIMMaterialForSampleDates(true, Convert.ToDateTime(deFromDate.EditValue), Convert.ToDateTime(deToDate.EditValue), materialTypeID);

            _aimMaterial.Insert
            (
                0,
            new AIMMaterialInfo
            {
                AIMMaterialID = 0,
                AIMMaterialCode = "--Select--",
                AIMMaterialName = "--Select--"
            }
            );

            lueMaterial.Properties.DataSource = _aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");

            //7/22/21
            lueMaterial_Name.Properties.DataSource = _aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueMaterial_Name, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");

            //1/6/23
            //2/18/22
            ////if (AIMGlobal.chartAimMaterialID != 0)
            if (rbControlProduct.Checked == true && AIMGlobal.chartAimMaterialID != 0)
            {
                lueMaterial.EditValue = AIMGlobal.chartAimMaterialID;
            }

            //1/6/23
            if (AIMGlobal.chartAimMaterialID != 0)
            {
                lueMaterial.EditValue = AIMGlobal.chartAimMaterialID;
            }                

            //1/6/23
            if (rbControlProduct.Checked == true && AIMGlobal.chartArchiveVersion != "")
            {
                lueVersion.Text = AIMGlobal.chartArchiveVersion;
            }

        }

        //7/22/21 - moved from dataBind to its own function
        private void DataBindNutrient()
        {            
            _aimNutrient = AIMManager.GetAIMNutrient(true);
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
            lueNutrient.Properties.DataSource = _aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueNutrient, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");

            //7/22/21
            lueNutrient_Name.Properties.DataSource = _aimNutrient;            
            XtraEditors.LoadLookUpEdit(ref lueNutrient_Name, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");


            lueNutrient2.Properties.DataSource = _aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueNutrient2, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");

            //7/22/21
            lueNutrient2_Name.Properties.DataSource = _aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueNutrient2_Name, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

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

        private void dataBind()
		{                        
            dataBindSupplier();
            dataBindOwner();            
            dataBindDataSources();            
        }

        private void dataBindSupplier()
        {
            //3/4/21
            //List<AIMSupplierInfo> supplier = new List<AIMSupplierInfo>();
            List<AIMSupplierInfo> supplier;

            //10/17/23 - added check of groupID (some users can see only certain suppliers, depending on group, which is set in User Listing)
            //supplier = AIMManager.GetSupplierWithMaterials(true, (Int32)lueMaterial.EditValue);
            supplier = AIMManager.GetSupplierWithMaterials(true, _supplierGroupID, (Int32)lueMaterial.EditValue);

            supplier.Insert
            (
                0,
                new AIMSupplierInfo
                {
                    AIMSupplierID = 0,                    
                    SupplierName = "--Select--"
                }
            );
            lueSupplier.Properties.DataSource = supplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplier, "AIMSupplierID", "SupplierName");
            lueSupplier.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

            lueSupplier2.Properties.DataSource = supplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplier2, "AIMSupplierID", "SupplierName");
            lueSupplier2.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

            lueSupplier3.Properties.DataSource = supplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplier3, "AIMSupplierID", "SupplierName");
            lueSupplier3.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

            lueSupplier4.Properties.DataSource = supplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplier4, "AIMSupplierID", "SupplierName");
            lueSupplier4.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

        }

        //7/9/21
        private void dataBindOwner()
        {
            List<AIMOwnerInfo> owner;

            //10/17/23 - added check of groupID (some users can see only certain owner-locations, depending on group, which is set in User Listing)
            //owner = AIMManager.GetAIMOwner(true);
            owner = AIMManager.GetAIMOwner(_ownerLocationGroupID, true);

            owner.Insert
            (
                0,
                new AIMOwnerInfo
                {
                    AIMOwnerID = 0,                    
                    OwnerName = "--Select--"
                }
            );
            lueOwner.Properties.DataSource = owner;
            XtraEditors.LoadLookUpEdit(ref lueOwner, "AIMOwnerID", "OwnerName");
            lueOwner.Properties.Columns["OwnerName"].Caption = "AIM Owner Name";                    
           
            lueOwner2.Properties.DataSource = owner;
            XtraEditors.LoadLookUpEdit(ref lueOwner2, "AIMOwnerID", "OwnerName");
            lueOwner2.Properties.Columns["OwnerName"].Caption = "AIM Owner Name";

            lueOwner3.Properties.DataSource = owner;
            XtraEditors.LoadLookUpEdit(ref lueOwner3, "AIMOwnerID", "OwnerName");
            lueOwner3.Properties.Columns["OwnerName"].Caption = "AIM Owner Name";

            lueOwner4.Properties.DataSource = owner;
            XtraEditors.LoadLookUpEdit(ref lueOwner4, "AIMOwnerID", "OwnerName");
            lueOwner4.Properties.Columns["OwnerName"].Caption = "AIM Owner Name";

            if (owner.Count == 2)
            {
                lueOwner.EditValue = owner[1].AIMOwnerID;
                lueOwner2.EditValue = owner[1].AIMOwnerID;
                lueOwner3.EditValue = owner[1].AIMOwnerID;
                lueOwner4.EditValue = owner[1].AIMOwnerID;
            }
            else
            {
                lueOwner.EditValue = owner;
                lueOwner2.EditValue = owner;
                lueOwner3.EditValue = owner;
                lueOwner4.EditValue = owner;
            }
        }

        //5/22/20
        private void dataBindDataSources()
        {
            List<DataSourceTypeInfo> datasource = new List<DataSourceTypeInfo>();
            datasource = AIMManager.GetDataSourceType(true);
            datasource.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--Select--"
                }                
            );
            lueDataSource.Properties.DataSource = datasource;
            XtraEditors.LoadLookUpEdit(ref lueDataSource, "DataSourceTypeID", "Name");
            lueDataSource.Properties.Columns["Name"].Caption = "DataSource Name";

            lueDataSource2.Properties.DataSource = datasource;
            XtraEditors.LoadLookUpEdit(ref lueDataSource2, "DataSourceTypeID", "Name");
            lueDataSource2.Properties.Columns["Name"].Caption = "DataSource Name";
            
            lueDataSource3.Properties.DataSource = datasource;
            XtraEditors.LoadLookUpEdit(ref lueDataSource3, "DataSourceTypeID", "Name");
            lueDataSource3.Properties.Columns["Name"].Caption = "DataSource Name";

            lueDataSource4.Properties.DataSource = datasource;
            XtraEditors.LoadLookUpEdit(ref lueDataSource4, "DataSourceTypeID", "Name");
            lueDataSource4.Properties.Columns["Name"].Caption = "DataSource Name";

        }

        private void dataBindSupplierLocationlue(DevExpress.XtraEditors.LookUpEdit lueSupp, DevExpress.XtraEditors.LookUpEdit lueSuppLoc)
        {
            Int32 supplier = 0;

            //------------------------------------------------------
            List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
            if (lueSupp.EditValue != null)
            {
                Int32.TryParse(lueSupp.EditValue.ToString(), out supplier);
            }

            //10/17/23 - added check of groupID (some users can see only certain suppliers/supplier-locations, depending on group, which is set in User Listing)
            //aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplier);
            aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplier, _supplierGroupID, _supplierLocationGroupID);

            aimSupplierLocation.Insert
            (
                0,
                new AIMSupplierLocationInfo
                {
                    AIMSupplierLocationID = 0,                    
                    SupplierLocationName = "--Select--"
                }
            );

            lueSuppLoc.Properties.DataSource = aimSupplierLocation;
            XtraEditors.LoadLookUpEdit(ref lueSuppLoc, "AIMSupplierLocationID", "SupplierLocationName");
            lueSuppLoc.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

            if (aimSupplierLocation.Count == 2)
            {
                //selecting value if there is only one
                lueSuppLoc.EditValue = aimSupplierLocation[1].AIMSupplierLocationID;
            }
            else
            {
                lueSuppLoc.EditValue = aimSupplierLocation;
            }

        }

        //6/25/21
        private void dataBindOwnerLocationlue(DevExpress.XtraEditors.LookUpEdit lueOwn, DevExpress.XtraEditors.LookUpEdit lueOwnLoc)
        {
            Int32 owner = 0;


            //------------------------------------------------------
            List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
            if (lueOwn.EditValue != null)
            {
                Int32.TryParse(lueOwn.EditValue.ToString(), out owner);
            }
            aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(owner);
            aimOwnerLocation.Insert
            (
                0,
                new AIMOwnerLocationInfo
                {
                    AIMOwnerLocationID = 0,                    
                    LocationName = "--Select--"
                }
            );

            lueOwnLoc.Properties.DataSource = aimOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueOwnLoc, "AIMOwnerLocationID", "LocationName");
            lueOwnLoc.Properties.Columns["LocationName"].Caption = "AIM Owner Location Name";

            if (aimOwnerLocation.Count == 2)
            {
                //selecting value if there is only one
                lueOwnLoc.EditValue = aimOwnerLocation[1].AIMOwnerLocationID;
            }
            else
            {
                lueOwnLoc.EditValue = aimOwnerLocation;
            }

        }
        private void dataBindOwnerLocation()
        {
            Int32 OwnerID = 0;
            if (lueOwner.EditValue != null)
            {
                Int32.TryParse(lueOwner.EditValue.ToString(), out OwnerID);
            }

            //10/17/23 - added check of groupID (some users can see only certain owner-locations, depending on group, which is set in User Listing)
            //_aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(OwnerID);
            _aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(OwnerID, _ownerLocationGroupID);

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
        #endregion

        #region Radio buttons

        private void rbTrend_CheckedChanged(object sender, EventArgs e)
        {
            rbReportCheckChanged(rbTrend);
        }
        private void rbSampleNumber_CheckedChanged(object sender, EventArgs e)
        {

            clearDataSources();
            if (rbSampleNumber.Checked == true)
            {
                _analysisQueryType = AnalysisQueryEnum.SampleID;
            }
            else
            {
                _analysisQueryType = AnalysisQueryEnum.SampleDate;
            }
        }
        private void rbDate_CheckedChanged(object sender, EventArgs e)
        {
            clearDataSources();
            if (rbSampleNumber.Checked == true)
            {
                _analysisQueryType = AnalysisQueryEnum.SampleID;
            }
            else
            {
                _analysisQueryType = AnalysisQueryEnum.SampleDate;
            }
        }
        private void rbSupplierCompare_CheckedChanged(object sender, EventArgs e)
        {
            rbReportCheckChanged(rbSupplierCompare);
        }
        private void rbControlChart_CheckedChanged(object sender, EventArgs e)
        {
            rbReportCheckChanged(rbControlChart);
        }
        private void rbAdvancedAnalysis_CheckedChanged(object sender, EventArgs e)
        {
            rbReportCheckChanged(rbAdvancedAnalysis);
        }

        #endregion

        #region Object Selected
        private void chartAdvancedAnalysis_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            ChartHitInfo sampleHit = chartAdvancedAnalysis.CalcHitInfo(e.HitInfo.HitPoint.X, e.HitInfo.HitPoint.Y);
            openSample(sampleHit, "chartAdvancedAnalysis");
        }
        private void chartGraphControl_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            ChartHitInfo sampleHit = chartGraphControl.CalcHitInfo(e.HitInfo.HitPoint.X, e.HitInfo.HitPoint.Y);

            if (rbSampleNumber.Checked == true)
            {
                openSample(sampleHit, "chartGraphControl");
            }
            else
            {
                openSampleList_UsingAnalysisQuery(sampleHit, "chartGraphControl");
            }
        }
        private void chartTrend_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            try
            {
                ChartHitInfo sampleHit = chartTrend.CalcHitInfo(e.HitInfo.HitPoint.X, e.HitInfo.HitPoint.Y);
                openSample(sampleHit, "chartTrend");
            }
            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message);
            }

        }

        //2/6/20
        private void chartTrendSuppliersByDate_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            try
            {
                ChartHitInfo sampleHit = chartTrendByDate.CalcHitInfo(e.HitInfo.HitPoint.X, e.HitInfo.HitPoint.Y);
                openSampleList(sampleHit, "chartTrendSuppliersByDate");
            }
            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message);
            }
        }

        private void chartSupplierCompare_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            ChartHitInfo sampleHit = chartSupplierCompare.CalcHitInfo(e.HitInfo.HitPoint.X, e.HitInfo.HitPoint.Y);
            if (rbSampleNumber.Checked == true)
            {
                openSample(sampleHit, "chartSupplierCompare");
            }
            else
            {
                openSampleList_UsingAnalysisQuery(sampleHit, "chartSupplierCompare");
            }
        }

        #endregion

        #region Open Sample

        private void openSampleList(ChartHitInfo sampleHit, String chartName)
        {
            try
            {
                AIMGlobal.SplashScreenShow();
                String sampleDate = "";
                DateTime startDt = Convert.ToDateTime(deFromDate.Text);
                DateTime endDt = Convert.ToDateTime(deToDate.Text);
                Int32 supplierID = 0;
                Int32 supplierLocationID = 0;

                //2/12/20 - added this to avoid error -- when user clicks on point that happens to be on a regression line.
                if (sampleHit.HitObject is RegressionLine)
                {
                    AIMGlobal.SplashScreenHide();
                    return;
                }

                if (sampleHit.SeriesPoint != null)
                {
                    sampleDate = Convert.ToString(sampleHit.SeriesPoint.Argument);

                    if (sampleDate != "")
                    {
                        List<ChartSampleListInfo> samples = new List<ChartSampleListInfo>();

                        DateTime sampleDt = Convert.ToDateTime(sampleDate);

                        switch (((Series)sampleHit.HitObject).SeriesID)
                        {
                            case 0:
                                supplierID = (Int32)lueSupplier.EditValue;
                                supplierLocationID = (Int32)lueSupplierLocation.EditValue;
                                break;
                            case 1:
                                supplierID = (Int32)lueSupplier2.EditValue;
                                supplierLocationID = (Int32)lueSupplierLocation2.EditValue;
                                break;
                            case 2:
                                supplierID = (Int32)lueSupplier3.EditValue;
                                supplierLocationID = (Int32)lueSupplierLocation3.EditValue;
                                break;
                            case 3:
                                supplierID = (Int32)lueSupplier4.EditValue;
                                supplierLocationID = (Int32)lueSupplierLocation4.EditValue;
                                break;
                            default:
                                break;
                        }

                        samples = AIMManager.GetChartSampleList(sampleDt, sampleDt, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, supplierID, supplierLocationID);
                        AIMGlobal.chartSampleList = samples;

                        _sampleList = new CFCAIM.Chart.ChartsSampleList();
                        this.Hide();
                        AIMGlobal.chartScreen = true;
                        _sampleList.WindowState = FormWindowState.Maximized;
                        AIMGlobal.SplashScreenHide();
                        _sampleList.ShowDialog();

                        //2/12/20 - added this because Charts screen was not coming back maximized (like it just skips the command below)
                        for (int i = 0; i < 50; i++)
                        {
                            Application.DoEvents();
                            i++;
                        }

                        this.Show();
                        this.WindowState = FormWindowState.Maximized;


                    }
                }
                AIMGlobal.SplashScreenHide();

            }
            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void openSampleList_UsingAnalysisQuery(ChartHitInfo sampleHit, String chartName)
        {
            try
            {
                AIMGlobal.SplashScreenShow();
                String sampleDate = "";
                DateTime startDt = Convert.ToDateTime(deFromDate.Text);
                DateTime endDt = Convert.ToDateTime(deToDate.Text);

                if (sampleHit.SeriesPoint != null)
                {
                    sampleDate = Convert.ToString(sampleHit.SeriesPoint.Argument);

                    if (sampleDate != "")
                    {
                        DateTime sampleDt = Convert.ToDateTime(sampleDate);
                        _analysisQueryType = AnalysisQueryEnum.SampleID;                        
                        AIMGlobal.sampleDetail = AIMManager.GetAnalysis(_analysisQueryType, _queryTemplateID, startDt, endDt, false, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDt, 0, 0, 0, 0, 0, 0, 0, 0, null, (Int32)_mainMenu.lueDataMap.EditValue, OutlierTypeEnum.NoOutlierExcluded, 0, 0, false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);                       
                        
                        _sampleListUsingAnalysisQuery = new SampleDetailFromAnalysis();
                        _sampleListUsingAnalysisQuery.MdiParent = this.MdiParent;
                        AIMGlobal.chartScreen = true;
                        _sampleListUsingAnalysisQuery.Show();

                        //should this be changed to ShowDialog? -- so code knows to come back to this point (so datasource can be refreshed).						
                        //////_sampleListUsingAnalysisQuery.Show();
                        ////_sampleList.ShowDialog();
                    }
                }
                AIMGlobal.SplashScreenHide();

            }
            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message);
            }
        }

        //1/5/23
        private void openSampleList_UsingAnalysisProductQuery(ChartHitInfo sampleHit, String chartName)
        {
            try
            {
                AIMGlobal.SplashScreenShow();
                String sampleDate = "";
                DateTime startDt = Convert.ToDateTime(deFromDate.Text);
                DateTime endDt = Convert.ToDateTime(deToDate.Text);
                Int32 groupMaterials = AIMGlobal.groupMaterials;
                Int32 nutrientToleranceHeaderID = AIMGlobal.nutrientToleranceHeaderID;

                if (sampleHit.SeriesPoint != null)
                {
                    sampleDate = Convert.ToString(sampleHit.SeriesPoint.Argument);
                    
                    if (sampleDate != "")
                    {
                        DateTime sampleDt = Convert.ToDateTime(sampleDate);
                        _analysisQueryType = AnalysisQueryEnum.SampleID;

                        if (rbProductTolerance.Checked == true)
                        {
                            groupMaterials = (Int32)EnumTolerance.ProductTolerance;
                        }
                        if (rbNutrientTolerance.Checked == true)
                        {
                            groupMaterials = (Int32)EnumTolerance.NutrientTolerance;
                            nutrientToleranceHeaderID = (Int32)lueNutrientTolerance.EditValue;
                        }
                                                                        
                        AIMGlobal.sampleDetailProduct = AIMManager.GetAnalysisProduct(_analysisQueryType, _queryTemplateID, startDt, endDt, groupMaterials, nutrientToleranceHeaderID, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDt, 0, 0, 0, "", (Int32)_mainMenu.lueDataMap.EditValue, _outlierType, AIMGlobal.chartArchiveVersion);

                        _analysisProductSampleDetail = new AnalysisProductSampleDetail();
                        _analysisProductSampleDetail.MdiParent = this.MdiParent;
                        AIMGlobal.chartScreen = true;
                        _analysisProductSampleDetail.Show();
                    }
                }
                AIMGlobal.SplashScreenHide();

            }
            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message);
            }
        }
        private void openSample(ChartHitInfo sampleHit, String chartName)
        {
            Int32 sampleID = 0;
            String sampleCode = "";

            //3/4/21
            //SampleInfo sampleInfo = new SampleInfo();
            SampleInfo sampleInfo;

            if (sampleHit.SeriesPoint != null)
            {
                //TODO this chart needs to use SampleCode instead of SampleID.
                // When that's done, remove 'chartName' from this function 

                //9/24/20 - commented
                //if (chartName == "chartGraphControl" || chartName == "chartGraphControlBySampleCode")
                //{
                //    sampleID = Convert.ToInt32(sampleHit.SeriesPoint.Argument);
                //}                
                //else
                //{
                sampleCode = sampleHit.SeriesPoint.Argument;

                //get sampleID
                sampleInfo = AIMManager.GetSampleForCharts(sampleCode);


                if (sampleInfo != null)
                {
                    sampleID = sampleInfo.SampleID;
                }
                //}
            }

            if (sampleID > 0)
            {
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.sampleID = sampleID;				
                CFCAIMCore.Global.sampleID = sampleID;
                {
                    AIMGlobal.SplashScreenShow();
                    _sample = new tpProdSpec();
                    _sample.MdiParent = this.MdiParent;

                    //10/6/21 - commented, not needed
                    //_sample.WindowState = FormWindowState.Normal;                  

                    AIMGlobal.chartScreen = true;
                    _sample.Show();
                    AIMGlobal.SplashScreenHide();
                }

            }
        }
        #endregion

        #region Legend      
        



        //2/10/20
        private void setLegendNames_Trend(ChartControl chart)
        {
            chart.Series[0].Name = "Location 1";
            chart.Series[1].Name = "Location 2";
            chart.Series[2].Name = "Location 3";
            chart.Series[3].Name = "Location 4";

            //8/3/21 - check to see if supplier, owner or combination of both should be displayed in legend
            #region Supplier Only
            if (rbSupplierOnly.Checked == true)
            {
                //Show Supplier or Supplier-Location name in legend
                if ((Int32)lueSupplier.EditValue != 0 && lueSupplier.Text != "")
                {
                    chart.Series[0].Name = lueSupplier.Text;
                    if ((Int32)lueSupplierLocation.EditValue != 0 && lueSupplierLocation.Text != "")
                    {
                        chart.Series[0].Name = lueSupplierLocation.Text;
                    }
                }
                if ((Int32)lueSupplier2.EditValue != 0 && lueSupplier2.Text != "")
                {
                    chart.Series[1].Name = lueSupplier2.Text;
                    if ((Int32)lueSupplierLocation2.EditValue != 0 && lueSupplierLocation2.Text != "")
                    {
                        chart.Series[1].Name = lueSupplierLocation2.Text;
                    }
                }
                if ((Int32)lueSupplier3.EditValue != 0 && lueSupplier3.Text != "")
                {
                    chart.Series[2].Name = lueSupplier3.Text;
                    if ((Int32)lueSupplierLocation3.EditValue != 0 && lueSupplierLocation3.Text != "")
                    {
                        chart.Series[2].Name = lueSupplierLocation3.Text;
                    }
                }
                if ((Int32)lueSupplier4.EditValue != 0 && lueSupplier4.Text != "")
                {
                    chart.Series[3].Name = lueSupplier4.Text;
                    if ((Int32)lueSupplierLocation4.EditValue != 0 && lueSupplierLocation4.Text != "")
                    {
                        chart.Series[3].Name = lueSupplierLocation4.Text;
                    }
                }
            }
            #endregion
            #region Owner Only
            else if (rbOwnerOnly.Checked == true)
            {
                //Show Owner or Owner-Location name in legend
                if ((Int32)lueOwner.EditValue != 0 && lueOwner.Text != "" && ckbOwn1.Checked == true)
                {
                    chart.Series[0].Name = lueOwner.Text;
                    if ((Int32)lueOwnerLocation.EditValue != 0 && lueOwnerLocation.Text != "")
                    {
                        chart.Series[0].Name = lueOwnerLocation.Text;
                    }
                }
                else
                {
                    chart.Series[0].Name = "";
                }
                if ((Int32)lueOwner2.EditValue != 0 && lueOwner2.Text != "" && ckbOwn2.Checked == true)
                {
                    chart.Series[1].Name = lueOwner2.Text;
                    if ((Int32)lueOwnerLocation2.EditValue != 0 && lueOwnerLocation2.Text != "")
                    {
                        chart.Series[1].Name = lueOwnerLocation2.Text;
                    }
                }
                else
                {
                    chart.Series[1].Name = "";
                }
                if ((Int32)lueOwner3.EditValue != 0 && lueOwner3.Text != "" && ckbOwn3.Checked == true)
                {
                    chart.Series[2].Name = lueOwner3.Text;
                    if ((Int32)lueOwnerLocation3.EditValue != 0 && lueOwnerLocation3.Text != "")
                    {
                        chart.Series[2].Name = lueOwnerLocation3.Text;
                    }
                }
                else
                {
                    chart.Series[2].Name = "";
                }
                if ((Int32)lueOwner4.EditValue != 0 && lueOwner4.Text != "" && ckbOwn4.Checked == true)
                {
                    chart.Series[3].Name = lueOwner4.Text;
                    if ((Int32)lueOwnerLocation4.EditValue != 0 && lueOwnerLocation4.Text != "")
                    {
                        chart.Series[3].Name = lueOwnerLocation4.Text;
                    }
                }
                else
                {
                    chart.Series[3].Name = "";
                }
            }
            #endregion
            #region Both Supplier and Owner
            else
            {
                if ((Int32)lueSupplier.EditValue != 0 && lueSupplier.Text != "" && ckbSupp1.Checked == true)
                {
                    chart.Series[0].Name = lueSupplier.Text;
                    if ((Int32)lueSupplierLocation.EditValue != 0 && lueSupplierLocation.Text != "")
                    {
                        chart.Series[0].Name = lueSupplierLocation.Text;
                        if ((Int32)lueOwner.EditValue != 0 && lueOwner.Text != "")
                        {
                            chart.Series[0].Name = lueSupplierLocation.Text + "-" + lueOwner.Text;
                            if ((Int32)lueOwnerLocation.EditValue != 0 && lueOwnerLocation.Text != "")
                            {
                                chart.Series[0].Name = lueSupplierLocation.Text + "-" + lueOwnerLocation.Text;
                            }
                        }
                    }
                }
                else
                {
                    chart.Series[0].Name = "";
                }

                if ((Int32)lueSupplier2.EditValue != 0 && lueSupplier2.Text != "" && ckbSupp2.Checked == true)
                {
                    chart.Series[1].Name = lueSupplier2.Text;
                    if ((Int32)lueSupplierLocation2.EditValue != 0 && lueSupplierLocation2.Text != "")
                    {
                        chart.Series[1].Name = lueSupplierLocation2.Text;
                        if ((Int32)lueOwner2.EditValue != 0 && lueOwner2.Text != "")
                        {
                            chart.Series[1].Name = lueSupplierLocation2.Text + "-" + lueOwner2.Text;
                            if ((Int32)lueOwnerLocation2.EditValue != 0 && lueOwnerLocation2.Text != "")
                            {
                                chart.Series[1].Name = lueSupplierLocation2.Text + "-" + lueOwnerLocation2.Text;
                            }
                        }
                    }
                }
                else
                {
                    chart.Series[1].Name = "";
                }

                if ((Int32)lueSupplier3.EditValue != 0 && lueSupplier3.Text != "" && ckbSupp3.Checked == true)
                {
                    chart.Series[2].Name = lueSupplier3.Text;
                    if ((Int32)lueSupplierLocation3.EditValue != 0 && lueSupplierLocation3.Text != "")
                    {
                        chart.Series[2].Name = lueSupplierLocation3.Text;
                        if ((Int32)lueOwner3.EditValue != 0 && lueOwner3.Text != "")
                        {
                            chart.Series[2].Name = lueSupplierLocation3.Text + "-" + lueOwner3.Text;
                            if ((Int32)lueOwnerLocation3.EditValue != 0 && lueOwnerLocation3.Text != "")
                            {
                                chart.Series[2].Name = lueSupplierLocation3.Text + "-" + lueOwnerLocation3.Text;
                            }
                        }
                    }
                }
                else
                {
                    chart.Series[2].Name = "";
                }

                if ((Int32)lueSupplier4.EditValue != 0 && lueSupplier4.Text != "" && ckbSupp4.Checked == true)
                {
                    chart.Series[3].Name = lueSupplier4.Text;
                    if ((Int32)lueSupplierLocation4.EditValue != 0 && lueSupplierLocation4.Text != "")
                    {
                        chart.Series[3].Name = lueSupplierLocation4.Text;
                        if ((Int32)lueOwner4.EditValue != 0 && lueOwner4.Text != "")
                        {
                            chart.Series[3].Name = lueSupplierLocation4.Text + "-" + lueOwner4.Text;
                            if ((Int32)lueOwnerLocation4.EditValue != 0 && lueOwnerLocation4.Text != "")
                            {
                                chart.Series[3].Name = lueSupplierLocation4.Text + "-" + lueOwnerLocation4.Text;
                            }
                        }
                    }
                }
                else
                {
                    chart.Series[3].Name = "";
                }
            }
            #endregion
        }

        //2/10/20
        private void hideShowLegendNames_Trend(ChartControl chart)
        {
            chart.Series[0].Visible = true;
            chart.Series[1].Visible = true;
            chart.Series[2].Visible = true;
            chart.Series[3].Visible = true;
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;


            //8/3/21 - check to see if supplier, owner or combination of both should be hidden/shown
            #region Supplier Only
            if (rbSupplierOnly.Checked == true)
            {
                //Series0 is used for both the versions of the Trend report (with or without suppliers)
                if ((Int32)lueSupplier.EditValue == 0 && ((Int32)lueSupplier2.EditValue != 0 || (Int32)lueSupplier3.EditValue != 0 || (Int32)lueSupplier4.EditValue != 0))
                {
                    chart.Series[0].Visible = false;
                }

                //Hide legend if not running with suppliers
                else if ((Int32)lueSupplier.EditValue == 0 && (Int32)lueSupplier2.EditValue == 0 && (Int32)lueSupplier3.EditValue == 0 && (Int32)lueSupplier4.EditValue == 0)
                {
                    chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                }

                if ((Int32)lueSupplier2.EditValue == 0)
                {
                    chart.Series[1].Visible = false;
                }
                if ((Int32)lueSupplier3.EditValue == 0)
                {
                    chart.Series[2].Visible = false;
                }
                if ((Int32)lueSupplier4.EditValue == 0)
                {
                    chart.Series[3].Visible = false;
                }
            }
            #endregion
            #region Owner Only
            else if (rbOwnerOnly.Checked == true)
            {
                //Series0 is used for both the versions of the Trend report (with or without owners)
                if ((Int32)lueOwner.EditValue == 0 && ((Int32)lueOwner2.EditValue != 0 || (Int32)lueOwner3.EditValue != 0 || (Int32)lueOwner4.EditValue != 0))
                {
                    chart.Series[0].Visible = false;
                }

                //Hide legend if not running with owners
                else if ((Int32)lueSupplier.EditValue == 0 && (Int32)lueSupplier2.EditValue == 0 && (Int32)lueSupplier3.EditValue == 0 && (Int32)lueSupplier4.EditValue == 0 && (Int32)lueOwner.EditValue == 0 && (Int32)lueOwner2.EditValue == 0 && (Int32)lueOwner3.EditValue == 0 && (Int32)lueOwner4.EditValue == 0)
                {
                    chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                }

                if ((Int32)lueOwner2.EditValue == 0)
                {
                    chart.Series[1].Visible = false;
                }
                if ((Int32)lueOwner3.EditValue == 0)
                {
                    chart.Series[2].Visible = false;
                }
                if ((Int32)lueOwner4.EditValue == 0)
                {
                    chart.Series[3].Visible = false;
                }
            }
            #endregion
            #region Both Supplier and Owner
            else
            {
                //Series0 is used for both the versions of the Trend report (with or without suppliers/owners)
                if ((Int32)lueSupplier.EditValue == 0 && ((Int32)lueSupplier2.EditValue != 0 || (Int32)lueSupplier3.EditValue != 0 || (Int32)lueSupplier4.EditValue != 0) && (Int32)lueSupplier.EditValue == 0 && ((Int32)lueSupplier2.EditValue != 0 || (Int32)lueSupplier3.EditValue != 0 || (Int32)lueSupplier4.EditValue != 0))
                {
                    chart.Series[0].Visible = false;
                }

                //Hide legend if not running with supplier or owners
                else if ((Int32)lueOwner.EditValue == 0 && (Int32)lueOwner2.EditValue == 0 && (Int32)lueOwner3.EditValue == 0 && (Int32)lueOwner4.EditValue == 0)
                {
                    chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                }



                if ((Int32)lueSupplier2.EditValue == 0 && (Int32)lueOwner2.EditValue == 0 && (Int32)lueOwnerLocation2.EditValue == 0)
                {
                    chart.Series[1].Visible = false;
                }
                if ((Int32)lueSupplier3.EditValue == 0 && (Int32)lueOwner3.EditValue == 0 && (Int32)lueOwnerLocation3.EditValue == 0)
                {
                    chart.Series[2].Visible = false;
                }
                if ((Int32)lueSupplier4.EditValue == 0 && (Int32)lueOwner4.EditValue == 0 && (Int32)lueOwnerLocation4.EditValue == 0)
                {
                    chart.Series[3].Visible = false;
                }
            }
            #endregion
        }
        //5/21/20
        private void setLegendNames_SupplierComparison(ChartControl chart)
        {
            chart.Series[0].Name = "Supplier 1";
            chart.Series[1].Name = "Supplier 2";
            chart.Series[2].Name = "Supplier 3";
            chart.Series[3].Name = "Supplier 4";

            //Show Supplier name in legend
            if ((Int32)lueSupplier.EditValue != 0 && lueSupplier.Text != "")
            {
                //7/12/21
                //chart.Series[0].Name = lueSupplier.Text;
                chart.Series[0].Name = lueSupplier.Text + " " + lueSupplierLocation.Text;
            }
            if ((Int32)lueSupplier2.EditValue != 0 && lueSupplier2.Text != "")
            {
                //7/12/21
                //chart.Series[1].Name = lueSupplier2.Text;
                chart.Series[1].Name = lueSupplier2.Text + " " + lueSupplierLocation2.Text;
            }
            if ((Int32)lueSupplier3.EditValue != 0 && lueSupplier3.Text != "")
            {
                //7/12/21
                //chart.Series[2].Name = lueSupplier3.Text;
                chart.Series[2].Name = lueSupplier3.Text + " " + lueSupplierLocation3.Text;
            }

            if ((Int32)lueSupplier4.EditValue != 0 && lueSupplier4.Text != "")
            {
                //7/12/21
                //chart.Series[3].Name = lueSupplier4.Text;
                chart.Series[3].Name = lueSupplier4.Text + " " + lueSupplierLocation4.Text;
            }
        }

        //5/21/20
        private void hideUnusedLegendNames_Suppliers(ChartControl chart)
        {
            chart.Series[0].Visible = true;
            chart.Series[1].Visible = true;
            chart.Series[2].Visible = true;
            chart.Series[3].Visible = true;
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            if ((Int32)lueSupplier2.EditValue == 0)
            {
                chart.Series[1].Visible = false;
            }
            if ((Int32)lueSupplier3.EditValue == 0)
            {
                chart.Series[2].Visible = false;
            }
            if ((Int32)lueSupplier4.EditValue == 0)
            {
                chart.Series[3].Visible = false;
            }
        }

        //5/21/20
        private void setLegendNames_DataSourceComparison(ChartControl chart)
        {
            chart.Series[0].Name = "DataSource 1";
            chart.Series[1].Name = "DataSource 2";
            chart.Series[2].Name = "DataSource 3";
            chart.Series[3].Name = "DataSource 4";

            //Show DataSource name in legend
            if ((Int32)lueDataSource.EditValue != 0 && lueDataSource.Text != "")
            {
                chart.Series[0].Name = lueDataSource.Text;
            }
            if ((Int32)lueDataSource2.EditValue != 0 && lueDataSource2.Text != "")
            {
                chart.Series[1].Name = lueDataSource2.Text;
            }
            if ((Int32)lueDataSource3.EditValue != 0 && lueDataSource3.Text != "")
            {
                chart.Series[2].Name = lueDataSource3.Text;
            }

            if ((Int32)lueDataSource4.EditValue != 0 && lueDataSource4.Text != "")
            {
                chart.Series[3].Name = lueDataSource4.Text;
            }
        }

        //5/21/20
        private void hideUnusedLegendNames_DataSource(ChartControl chart)
        {
            chart.Series[0].Visible = true;
            chart.Series[1].Visible = true;
            chart.Series[2].Visible = true;
            chart.Series[3].Visible = true;
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            if ((Int32)lueDataSource2.EditValue == 0)
            {
                chart.Series[1].Visible = false;
            }
            if ((Int32)lueDataSource3.EditValue == 0)
            {
                chart.Series[2].Visible = false;
            }
            if ((Int32)lueDataSource4.EditValue == 0)
            {
                chart.Series[3].Visible = false;
            }
        }
        #endregion

        private void setOutlierButtons(Boolean init)    //2/23/17 - added because outliers can be changed on chart screen 
        {
            if (init == true)
            {
                AIMGlobal.analysisExcludeExtremeOutliers = false;
                AIMGlobal.analysisIncludeExtremeOutliers = true;
                AIMGlobal.analysisExcludeMildOutliers = false;
                AIMGlobal.analysisIncludeMildOutliers = true;
                AIMGlobal.analysisAllRecords = true;
                AIMGlobal.analysisOutliersOnly = false;

            }
            else
            {
                rbExtremeOutlierExclude.Checked = AIMGlobal.analysisExcludeExtremeOutliers;
                rbExtremeOutlierInclude.Checked = AIMGlobal.analysisIncludeExtremeOutliers;
                rbMildOutlierExclude.Checked = AIMGlobal.analysisExcludeMildOutliers;
                rbMildOutlierInclude.Checked = AIMGlobal.analysisIncludeMildOutliers;
                rbAllRecords.Checked = AIMGlobal.analysisAllRecords;
                rbOutliersOnly.Checked = AIMGlobal.analysisOutliersOnly;
            }
        }
        private void setToolTips()
		{
			this.Icon = (Icon)Properties.Resources.CFCAIM;
			ToolTip ttip = new ToolTip();
			foreach (Control ctrl in pnlTopBtns.Controls)
			{
				ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
			}
			ttip.SetToolTip(btnGetChartData, "Generate Chart");
			ttip.SetToolTip(btnPreview, "Preview Chart");
			ttip.SetToolTip(btnPrint, "Print Chart");
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");


        }
		private void hideCharts()
		{            
            rbControlChart.Checked = true;

            //12/23/22
            if (rbControlIngredient.Checked == true)
            {            
                chartProductControl.Visible = false;
                chartProductControlByDate.Visible = false;
            }
            else
            {
                chartProductControl.Visible = true;
                chartProductControlByDate.Visible = true;
            }

                        
			rbTrend.Checked = false;
			rbAdvancedAnalysis.Checked = false;
			rbSupplierCompare.Checked = false;
            rbDataSourceCompare.Checked = false;                //5/22/20

			chartTrend.Visible = false;            
            chartTrendByDate.Visible = false;                   //2/5/20
            chartGraphControl.Visible = false;
            chartGraphControlBySampleCode.Visible = false;
            chartAdvancedAnalysis.Visible = false;
			chartSupplierCompare.Visible = false;
            chartDataSourceCompare.Visible = false;             //5/22/20

            grpData.Visible = true;
			rbSampleNumber.Checked = true;
		}
		private void clearDataSources()
		{
			_details = new List<GraphInfo>();
			_chart = new List<ChartInfo>();

			chartGraphControl.DataSource = _details;
            chartGraphControlBySampleCode.DataSource = _details;

            chartTrend.DataSource = _chart;            
            chartTrendByDate.DataSource = _chart;            //2/5/20
            chartAdvancedAnalysis.DataSource = _chart;
			chartSupplierCompare.DataSource = _chart;            

            chartGraphControl.Visible = false;
            chartGraphControlBySampleCode.Visible = false;
            chartTrend.Visible = false;
            
            chartTrendByDate.Visible = false;                //2/5/20
            chartAdvancedAnalysis.Visible = false;
			chartSupplierCompare.Visible = false;

            chartDataSourceCompare.DataSource = _chart;     //5/22/20
            chartDataSourceCompare.Visible = false;         //5/22/20
                        
            //12/23/22                        
            chartProductControl.Visible = false;
            chartProductControlByDate.Visible = false;

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
            		
            //7/22/21
            DataBindMaterial();
            DataBindNutrient();

            dataBind();

            hideCharts();
			checkGlobals();

			lueNutrient.EditValue = 0;
			lueMaterial.EditValue = 0;			
			deFromDate.Text = "";
			deToDate.Text = "";

            //8/3/21            
            rbControlChart.Checked = true;            

        }
        private void btnPrint_Click(object sender, EventArgs e)
		{

			try
			{
				if (rbAdvancedAnalysis.Checked == true)
				{
					grdChart.DataSource = _chart;
                    CFCAIMCore.Global.printChartResults(chartAdvancedAnalysis, "Advanced Analysis");
                }

				if (rbControlChart.Checked == true)
				{
                    grdChart.DataSource = _details;
                    if (_analysisQueryType == AnalysisQueryEnum.SampleDate)
                    {
                        CFCAIMCore.Global.printChartResults(chartGraphControl, "Control Chart");
                    }
                    else
                    {
                        CFCAIMCore.Global.printChartResults(chartGraphControlBySampleCode, "Control Chart");
                    }
				}

				if (rbTrend.Checked == true)
				{
					grdChart.DataSource = _chart;               
                    if (rbDate.Checked == true)
                    {                        
                        CFCAIMCore.Global.printChartResults(chartTrendByDate, "Trend");
                    }
                    else
	                {
                        CFCAIMCore.Global.printChartResults(chartTrend, "Trend");
                    }
                }

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void checkGlobals()
		{
			if (AIMGlobal.chartAimNutrientID != 0)
			{
				lueNutrient.EditValue = AIMGlobal.chartAimNutrientID;
			}
			if (AIMGlobal.chartAimMaterialID != 0)
			{
				lueMaterial.EditValue = AIMGlobal.chartAimMaterialID;
			}
						
			_queryTemplateID = AIMGlobal.chartQueryTemplateID;
			_outlierType = AIMGlobal.chartOutlierType;

			_analysisQueryType = AnalysisQueryEnum.SampleDate;
			rbDate.Checked = true;
			rbSampleNumber.Checked = false;

			deFromDate.Text = AIMGlobal.chartBegDate.ToString("M/dd/yyyy");
			if (deFromDate.Text == "1/01/0001")
			{
                //1/15/21
                //deFromDate.EditValue = "1/01/2015";
                deFromDate.EditValue = "1/01/" + DateTime.Now.Year.ToString();

            }
			deToDate.Text = AIMGlobal.chartEndDate.ToString("M/dd/yyyy");
			if (deToDate.Text == "1/01/0001")
			{
				deToDate.EditValue = DateTime.Now;
			}

            if (AIMGlobal.comingFromAnalysis == false)
            {
                setOutlierButtons(true);
            }
            else
            {
                setOutlierButtons(false);
            }                        
        }	
                
        //5/27/20
        private void changeSizeAndLocation(ChartControl chart)
        {            
            chartTrend.Visible = false;
            chartTrendByDate.Visible = false;
            chartAdvancedAnalysis.Visible = false;
            chartGraphControl.Visible = false;
            chartDataSourceCompare.Visible = false;
            chartSupplierCompare.Visible = false;

            //12/23/22            
            chartProductControl.Visible = false;
            chartProductControlByDate.Visible = false;
            
            chart.Visible = true;

            //chart.Location = new Point(0, 167);
            //chart.Location = new Point(0, 206);
            chart.Location = new Point(0, 245);
            
            //Had to add this because anchoring was not working when Location was changed -- and charts were not filling up full screen.
            //Instead of 100%, changed to 95% and 70% so there is a buffer around the edges.                        
            chart.Width = (Int32)Math.Truncate(this.Width * .95);

            //7/14/21 - added check of WindowState for Anel at Perdue - chart was not fitting correctly on his screen.
            //chart.Height = (Int32)Math.Truncate(this.Height * .7);
            if (WindowState == FormWindowState.Maximized)
            {
                //7/22/21
                //chart.Height = (Int32)Math.Truncate(this.Height * .7);
                chart.Height = (Int32)Math.Truncate(this.Height * .6);

            }
            else if(WindowState == FormWindowState.Normal)
            {   
                //7/22/21
                chart.Height = (Int32)Math.Truncate(this.Height * .6);
                chart.Height = (Int32)Math.Truncate(this.Height * .5);
            }                                                                
        }
                
        private void fillDetail(ref List<GraphInfo> details, String valueName, Double value, Int32 aimMaterID,
						Int32 aimNutrientID, DateTime sampleDate, Int32 sampleID, String sampleCode)
		{

            String samplecode = "";
            if (rbSampleNumber.Checked == true)
            {
                samplecode = sampleCode;
            }

            GraphInfo detail = new GraphInfo();
            detail.IngrendientName = lueMaterial.Text.ToString();
            detail.UserName = _userGlobal.CFCLoginUser.UserName;
            detail.StartDate = (DateTime)deFromDate.DateTime;
            detail.EndDate = (DateTime)deToDate.DateTime;
            detail.AIMMaterialID = aimMaterID;
            detail.AIMNutrientID = aimNutrientID;
            detail.SampleDate = sampleDate;
            detail.ValueName = valueName;
            detail.Value = Math.Round(value, 3);
            detail.SampleID = sampleID;
            detail.SampleCode = samplecode;
            details.Add(detail);
        }

        //12/30/22
        private void fillDetailProductControl(ref List<GraphProductControlInfo> details, String valueName, Double value, Int32 aimMaterID,
                       Int32 aimNutrientID, DateTime sampleDate, Int32 sampleID, String sampleCode, Double min, Double max)
        {

            String samplecode = "";
            if (rbSampleNumber.Checked == true)
            {
                samplecode = sampleCode;
            }

            GraphProductControlInfo detail = new GraphProductControlInfo();
            detail.IngrendientName = lueMaterial.Text.ToString();
            detail.UserName = _userGlobal.CFCLoginUser.UserName;
            detail.StartDate = (DateTime)deFromDate.DateTime;
            detail.EndDate = (DateTime)deToDate.DateTime;
            detail.AIMMaterialID = aimMaterID;
            detail.AIMNutrientID = aimNutrientID;
            detail.SampleDate = sampleDate;
            detail.ValueName = valueName;
            detail.Value = Math.Round(value, 3);
            detail.SampleID = sampleID;
            detail.SampleCode = samplecode;
            detail.PredictedMin = min;
            detail.PredictedMax = max;
            details.Add(detail);
        }

        private void btnPreview_Click(Object sender, EventArgs e)
		{
			try
			{
				if (rbAdvancedAnalysis.Checked == true)
				{
					grdChart.DataSource = _chart;
                    CFCAIMCore.Global.previewGridResults(grdChart, "Advanced Analysis - Raw Data");
                    CFCAIMCore.Global.previewChartResults(chartAdvancedAnalysis, "Advanced Analysis");
                }

				if (rbControlChart.Checked == true)
				{
					grdChart.DataSource = _details;
                    CFCAIMCore. Global.previewGridResults(grdChart, "Control Chart - Raw Data");

                    if (_analysisQueryType == AnalysisQueryEnum.SampleDate)
                    {                    
                        CFCAIMCore.Global.previewChartResults(chartGraphControl, "Control Chart");
                    }
                    else
                    {
                        CFCAIMCore.Global.previewChartResults(chartGraphControlBySampleCode, "Control Chart");
                    }
				}

				if (rbTrend.Checked == true)
				{
                    grdChart.DataSource = _chart;
                    CFCAIMCore. Global.previewGridResults(grdChart, "Trend - Raw Data");

                    if (rbDate.Checked == true)
                    {                    
                        CFCAIMCore.Global.previewChartResults(chartTrendByDate, "Trend");
                    }
                    else
                    {                        
                        CFCAIMCore.Global.previewChartResults(chartTrend, "Trend");
                    }
                }

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
        
        private void rbExtremeOutlierExclude_CheckedChanged(Object sender, EventArgs e)
        {           
            AIMGlobal.analysisExcludeExtremeOutliers = rbExtremeOutlierExclude.Checked;
        }
        private void rbExtremeOutlierInclude_CheckedChanged(Object sender, EventArgs e)
        {         
            AIMGlobal.analysisIncludeExtremeOutliers = rbExtremeOutlierInclude.Checked;            
        }
        private void rbMildOutlierInclude_CheckedChanged(Object sender, EventArgs e)
        {         
            AIMGlobal.analysisIncludeMildOutliers = rbMildOutlierInclude.Checked;                       
        }
        private void rbMildOutlierExclude_CheckedChanged(Object sender, EventArgs e)
        {         
            AIMGlobal.analysisExcludeMildOutliers = rbMildOutlierExclude.Checked;
        }        
              
        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }
        private void charts_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }
        private void rbOutliersOnly_CheckedChanged(Object sender, EventArgs e)
        {
            AIMGlobal.analysisOutliersOnly = rbOutliersOnly.Checked;
        }
        private void rbAllRecords_CheckedChanged(Object sender, EventArgs e)
        {
            AIMGlobal.analysisAllRecords = rbAllRecords.Checked;
        }
        private void lueOwner_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                dataBindOwnerLocationlue(lueOwner, lueOwnerLocation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void chartGraphControl_CustomDrawCrosshair(Object sender, CustomDrawCrosshairEventArgs e)
        {

            foreach (CrosshairElement ce in e.CrosshairElements)
            {

                LineSeriesView v;
                v = ce.Series.View as LineSeriesView;
                if (ce.Series.Name == "Mean" || ce.Series.Name == "Extreme Upper Outlier" || ce.Series.Name == "Extreme Lower Outlier" || ce.Series.Name == "Proposed Value")
                {
                    (ce.Series.View as LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
                }
                else
                {
                    (ce.Series.View as LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                }
            }
        }
        private void chartGraphControlBySampleCode_CustomDrawCrosshair(Object sender, CustomDrawCrosshairEventArgs e)
        {
            foreach (CrosshairElement ce in e.CrosshairElements)            
            {
                LineSeriesView v;
                

                v = ce.Series.View as LineSeriesView;
                if (ce.Series.Name == "Mean" || ce.Series.Name == "Extreme Upper Outlier" || ce.Series.Name == "Extreme Lower Outlier" || ce.Series.Name == "Proposed Value")
                {
                    (ce.Series.View as LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
                }
                else
                {
                    (ce.Series.View as LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                }
            }
        }
        private void chartGraphControlBySampleCode_ObjectSelected(Object sender, HotTrackEventArgs e)
        {
            ChartHitInfo sampleHit = chartGraphControlBySampleCode.CalcHitInfo(e.HitInfo.HitPoint.X, e.HitInfo.HitPoint.Y);
            openSample(sampleHit, "chartGraphControlBySampleCode");

        }
        private void chartGraphControlBySampleCode_CustomDrawSeriesPoint(Object sender, CustomDrawSeriesPointEventArgs e)
        {
            //9/21/18 - commented.  If this gets uncommented, when mousing over the chart, the chart, legend, titles will disappear.
            //if (e.Series.Name == "Mean"
            //    || e.Series.Name == "Extreme Upper Outlier"
            //    || e.Series.Name == "Extreme Lower Outlier"
            //    || e.Series.Name == "Proposed Value")
            //{
            //    e.Series.Points.Remove(e.SeriesPoint);
            //}

        }
        private void chartGraphControl_CustomDrawSeriesPoint(Object sender, CustomDrawSeriesPointEventArgs e)
        {
            //9/21/18 - commented.  If this gets uncommented, when mousing over the chart, the chart, legend, titles will disappear.
            //if (e.Series.Name == "Mean"
            //    || e.Series.Name == "Extreme Upper Outlier"
            //    || e.Series.Name == "Extreme Lower Outlier"
            //    || e.Series.Name == "Proposed Value")
            //{
            //    e.Series.Points.Remove(e.SeriesPoint);
            //}
        }
        
        private void lueSupplier_EditValueChanged(object sender, EventArgs e)
        {
            try
            {              
                dataBindSupplierLocationlue(lueSupplier, lueSupplierLocation);                               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        
        private void lueSupplier2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dataBindSupplierLocationlue(lueSupplier2, lueSupplierLocation2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueSupplier3_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dataBindSupplierLocationlue(lueSupplier3, lueSupplierLocation3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueSupplier4_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dataBindSupplierLocationlue(lueSupplier4, lueSupplierLocation4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                
        private void ckbSupp1_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSupp1.CheckState == CheckState.Checked)
            {
                lueSupplier.Visible = true;

                //lueSupplierLocation.Visible = true;       //5/21/20 - added additional Suppliers for Comparison, but only Trend should see Supplier Locations
                //6/25/21 - changed to allow ALL charts, except DataSource comparison, to see supplier and supplier-locations
                //if (rbTrend.Checked == true)                    
                if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true || rbSupplierCompare.Checked == true)

                {
                    lueSupplierLocation.Visible = true;
                }
                
            }
            else
            {
                lueSupplier.Visible = false;
                lueSupplierLocation.Visible = false;                
                lueSupplier.EditValue = 0;
                lueSupplierLocation.EditValue = 0;

                //7/14/21
                ckbOwn1.CheckState = CheckState.Unchecked;
                lueOwner.Visible = false;
                lueOwnerLocation.Visible = false;
                lueOwner.EditValue = 0;
                lueOwnerLocation.EditValue = 0;

            }
        }
        private void ckbSupp2_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSupp2.CheckState == CheckState.Checked)
            {                
                lueSupplier2.Visible = true;

                //lueSupplierLocation2.Visible = true;      //5/21/20 - added additional Suppliers for Comparison, but only Trent should see Supplier Locations
                //6/25/21 - changed to allow ALL charts, except DataSource comparison, to see supplier and supplier-locations
                //if (rbTrend.Checked == true)                    
                if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true || rbSupplierCompare.Checked == true)
                    
                {
                    lueSupplierLocation2.Visible = true;
                }
            }
            else
            {
                lueSupplier2.Visible = false;
                lueSupplierLocation2.Visible = false;                
                lueSupplier2.EditValue = 0;
                lueSupplierLocation2.EditValue = 0;

                //7/14/21
                ckbOwn2.CheckState = CheckState.Unchecked;
                lueOwner2.Visible = false;
                lueOwnerLocation2.Visible = false;
                lueOwner2.EditValue = 0;
                lueOwnerLocation2.EditValue = 0;
            }
        }
        private void ckbSupp3_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSupp3.CheckState == CheckState.Checked)
            {                
                lueSupplier3.Visible = true;

                //lueSupplierLocation3.Visible = true;      //5/21/20 - added additional Suppliers for Comparison, but only Trent should see Supplier Locations
                //6/25/21 - changed to allow ALL charts, except DataSource comparison, to see supplier and supplier-locations
                //if (rbTrend.Checked == true)                    
                if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true || rbSupplierCompare.Checked == true)

                {
                    lueSupplierLocation3.Visible = true;
                }
            }
            else
            {
                lueSupplier3.Visible = false;
                lueSupplierLocation3.Visible = false;                
                lueSupplier3.EditValue = 0;
                lueSupplierLocation3.EditValue = 0;

                //7/14/21
                ckbOwn3.CheckState = CheckState.Unchecked;
                lueOwner3.Visible = false;
                lueOwnerLocation3.Visible = false;
                lueOwner3.EditValue = 0;
                lueOwnerLocation3.EditValue = 0;
            }
        }
        private void ckbSupp4_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSupp4.CheckState == CheckState.Checked)
            {
                lueSupplier4.Visible = true;

                //lueSupplierLocation4.Visible = true;      //5/21/20 - added additional Suppliers for Comparison, but only Trent should see Supplier Locations
                //6/25/21 - changed to allow ALL charts, except DataSource comparison, to see supplier and supplier-locations
                //if (rbTrend.Checked == true)                    
                if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true || rbSupplierCompare.Checked == true)

                {
                    lueSupplierLocation4.Visible = true;
                }
            }
            else
            {
                lueSupplier4.Visible = false;
                lueSupplierLocation4.Visible = false;                
                lueSupplier4.EditValue = 0;
                lueSupplierLocation4.EditValue = 0;

                //7/14/21
                ckbOwn4.CheckState = CheckState.Unchecked;
                lueOwner4.Visible = false;
                lueOwnerLocation4.Visible = false;
                lueOwner4.EditValue = 0;
                lueOwnerLocation4.EditValue = 0;
            }
        }

        //7/14/21
        private void clearSuppliers()
        {
            ckbSupp1.Visible = false;
            ckbSupp2.Visible = false;
            ckbSupp3.Visible = false;
            ckbSupp4.Visible = false;
            ckbSupp1.CheckState = CheckState.Unchecked;
            ckbSupp2.CheckState = CheckState.Unchecked;
            ckbSupp3.CheckState = CheckState.Unchecked;
            ckbSupp4.CheckState = CheckState.Unchecked;
            lueSupplier.Visible = false;
            lueSupplier2.Visible = false;
            lueSupplier3.Visible = false;
            lueSupplier4.Visible = false;
            lueSupplierLocation.Visible = false;
            lueSupplierLocation2.Visible = false;
            lueSupplierLocation3.Visible = false;
            lueSupplierLocation4.Visible = false;
        }

        //7/14/21
        private void clearOwners()
        {
            ckbOwn1.Visible = false;
            ckbOwn2.Visible = false;
            ckbOwn3.Visible = false;
            ckbOwn4.Visible = false;
            ckbOwn1.CheckState = CheckState.Unchecked;
            ckbOwn2.CheckState = CheckState.Unchecked;
            ckbOwn3.CheckState = CheckState.Unchecked;
            ckbOwn4.CheckState = CheckState.Unchecked;
            lueOwner.Visible = false;
            lueOwner2.Visible = false;
            lueOwner3.Visible = false;
            lueOwner4.Visible = false;
            lueOwnerLocation.Visible = false;
            lueOwnerLocation2.Visible = false;
            lueOwnerLocation3.Visible = false;
            lueOwnerLocation4.Visible = false;
        }

        private void showHideSuppliers(Boolean blnShow)
        {

            ckbSuppLabel.Visible = false;
            lblSupplier.Visible = false;
          
            //----------------------------------------------------------
            //7/14/21
            //ckbSupp1.Visible = false;
            //ckbSupp2.Visible = false;
            //ckbSupp3.Visible = false;
            //ckbSupp4.Visible = false;
            //ckbSupp1.CheckState = CheckState.Unchecked;
            //ckbSupp2.CheckState = CheckState.Unchecked;
            //ckbSupp3.CheckState = CheckState.Unchecked;
            //ckbSupp4.CheckState = CheckState.Unchecked;
            //lueSupplier.Visible = false;
            //lueSupplier2.Visible = false;
            //lueSupplier3.Visible = false;
            //lueSupplier4.Visible = false;
            //lueSupplierLocation.Visible = false;
            //lueSupplierLocation2.Visible = false;
            //lueSupplierLocation3.Visible = false;
            //lueSupplierLocation4.Visible = false;
            if ((Int32)lueSupplier.EditValue == 0)
            {
                ckbSupp1.Visible = false;
                ckbSupp1.CheckState = CheckState.Unchecked;
                lueSupplier.Visible = false;
                lueSupplierLocation.Visible = false;
            }
            if ((Int32)lueSupplier2.EditValue == 0)
            {
                ckbSupp2.Visible = false;
                ckbSupp2.CheckState = CheckState.Unchecked;
                lueSupplier2.Visible = false;
                lueSupplierLocation2.Visible = false;
            }
            if ((Int32)lueSupplier3.EditValue == 0)
            {
                ckbSupp3.Visible = false;
                ckbSupp3.CheckState = CheckState.Unchecked;
                lueSupplier3.Visible = false;
                lueSupplierLocation3.Visible = false;
            }
            if ((Int32)lueSupplier4.EditValue == 0)
            {
                ckbSupp4.Visible = false;
                ckbSupp4.CheckState = CheckState.Unchecked;
                lueSupplier4.Visible = false;
                lueSupplierLocation4.Visible = false;
            }
            //----------------------------------------------------------

            if (blnShow == true)
            {
                ckbSuppLabel.Visible = true;
                lblSupplier.Visible = true;               
                ckbSupp1.Visible = true;
                ckbSupp2.Visible = true;
                ckbSupp3.Visible = true;
                ckbSupp4.Visible = true;
                
            }
            //12/28/22
            else
            {
                ckbSuppLabel.Visible = false;
                lblSupplier.Visible = false;
                ckbSupp1.Visible = false;
                ckbSupp2.Visible = false;
                ckbSupp3.Visible = false;
                ckbSupp4.Visible = false;

            }

        }

        //6/25/21
        private void showHideOwners(Boolean blnShow)
        {

            ckbOwnLabel.Visible = false;
            lblOwner.Visible = false;
            
            //----------------------------------------------------------
            //7/14/21
            //ckbOwn1.Visible = false;
            //ckbOwn2.Visible = false;
            //ckbOwn3.Visible = false;
            //ckbOwn4.Visible = false;
            //ckbOwn1.CheckState = CheckState.Unchecked;
            //ckbOwn2.CheckState = CheckState.Unchecked;
            //ckbOwn3.CheckState = CheckState.Unchecked;
            //ckbOwn4.CheckState = CheckState.Unchecked;
            //lueOwner.Visible = false;
            //lueOwner2.Visible = false;
            //lueOwner3.Visible = false;
            //lueOwner4.Visible = false;
            //lueOwnerLocation.Visible = false;
            //lueOwnerLocation2.Visible = false;
            //lueOwnerLocation3.Visible = false;
            //lueOwnerLocation4.Visible = false;
            if ((Int32)lueOwner.EditValue == 0)
            {
                ckbOwn1.Visible = false;
                ckbOwn1.CheckState = CheckState.Unchecked;
                lueOwner.Visible = false;
                lueOwnerLocation.Visible = false;
            }
            if ((Int32)lueOwner2.EditValue == 0)
            {
                ckbOwn2.Visible = false;
                ckbOwn2.CheckState = CheckState.Unchecked;
                lueOwner2.Visible = false;
                lueOwnerLocation2.Visible = false;
            }
            if ((Int32)lueOwner3.EditValue == 0)
            {
                ckbOwn3.Visible = false;
                ckbOwn3.CheckState = CheckState.Unchecked;
                lueOwner3.Visible = false;
                lueOwnerLocation3.Visible = false;
            }
            if ((Int32)lueOwner4.EditValue == 0)
            {
                ckbOwn4.Visible = false;
                ckbOwn4.CheckState = CheckState.Unchecked;
                lueOwner4.Visible = false;
                lueOwnerLocation4.Visible = false;
            }


            if (blnShow == true)
            {
                ckbOwnLabel.Visible = true;
                lblOwner.Visible = true;
               
                ckbOwn1.Visible = true;
                ckbOwn2.Visible = true;
                ckbOwn3.Visible = true;
                ckbOwn4.Visible = true;

            }

            //12/28/22
            else
            {
                ckbOwnLabel.Visible = false;
                lblOwner.Visible = false;
                ckbOwn1.Visible = false;
                ckbOwn2.Visible = false;
                ckbOwn3.Visible = false;
                ckbOwn4.Visible = false;
            }

        }

        //5/20/20
        private void lueMaterial_EditValueChanged(object sender, EventArgs e)
        {
            //Filter suppliers for selected material -- show only suppliers that have this material
            dataBindSupplier();

            //1/5/23
            DataBindProductVersion();

            //7/22/21
            try
            {
                SyncValueLue(ref lueMaterial, ref lueMaterial_Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //5/22/20
        private void rbDataSourceCompare_CheckedChanged(object sender, EventArgs e)
        {
            rbReportCheckChanged(rbDataSourceCompare);      
        }

        private void rbReportCheckChanged(RadioButton button)

        {

            //8/2/21
            showHideOwnerSupplierGroupBox();

            pnlDataSource.Visible = false;

            //8/2/21 - commented
            //pnlOwner.Visible = false;

            //12/29/22
            gbTolerance.Visible = false;

            //1/5/23
            gbProductVersion.Visible = false;

            #region ControlChart
            if (button == rbControlChart)
            {
                //12/27/21
                if (rbControlProduct.Checked == true)
                {
                    grpSupplierAndOrOwner.Visible = false;
                    grpData.Visible = true;
                    clearDataSources();
                    panelOutlier.Visible = true;                    
                    showHideSuppliers(false);
                    showHideOwners(false);
                    if (AIMGlobal.comingFromAnalysis == false)
                    {
                        /////gbIngredientOrProduct.Visible = true;
                    }                    
                    gbTolerance.Visible = true;
                    gbTolerance.Location = new Point(323, 76);

                    //1/5/23
                    gbProductVersion.Visible = true;
                    gbProductVersion.Location = new Point(323, 14);

                    //1/4/23
                    if (AIMGlobal.comingFromAnalysis == true)
                    {
                        /////gbIngredientOrProduct.Visible = true;
                    }
                    else
                    {
                        gbIngredientOrProduct.Visible = false;
                    }
                }
                else
                {

                    //8/2/21
                    grpSupplierAndOrOwner.Visible = true;

                    grpData.Visible = true;
                    clearDataSources();
                    panelOutlier.Visible = true;

                    showHideSuppliers(true);
                    showHideOwners(true);

                    //1/4/23 - commented - do not allow product charts from "Charts" menu item
                    ////////12/23/22
                    //////if (AIMGlobal.comingFromAnalysis == false)
                    //////{                        
                    //////    gbIngredientOrProduct.Visible = true;
                    //////}
                    if (AIMGlobal.comingFromAnalysis == true)
                    {
                        /////gbIngredientOrProduct.Visible = true;
                    }
                    else
                    {
                        gbIngredientOrProduct.Visible = false;
                    }
                }



            }
            #endregion
            #region Trend
            if (button == rbTrend)
            {
                //8/2/21
                grpSupplierAndOrOwner.Visible = true;

                //2/3/20 - added sample-date option to Trend chart
                
                grpData.Visible = true;
                showHideSuppliers(false);
                if (rbTrend.Checked == true)
                {                    
                    showHideSuppliers(true);                    
                    showHideOwners(true);
                }
                clearDataSources();
                panelOutlier.Visible = false;

                //8/2/21 - commented
                //6/25/21
                //pnlOwner.Visible = true;

                //12/23/22
                gbIngredientOrProduct.Visible = false;
            }
            #endregion
            #region Advanced Analysis
            if (button == rbAdvancedAnalysis)
            {
                //8/2/21
                grpSupplierAndOrOwner.Visible = true;

                panelOutlier.Visible = false;
                clearDataSources();
                grpData.Visible = false;
                lblNutrient2.Visible = false;
                lueNutrient2.Visible = false;

                //7/22/21
                lueNutrient2_Name.Visible = false;

                if (rbAdvancedAnalysis.Checked == true)
                {
                    lblNutrient2.Visible = true;
                    lueNutrient2.Visible = true;

                    //7/22/21
                    lueNutrient2_Name.Visible = true;

                }

                //6/25/21
                //showHideSuppliers(false);
                showHideSuppliers(true);

                //7/12/21               
                showHideOwners(true);

                //8/2/21 - commented
                //6/25/21
                ////pnlOwner.Visible = true;

                //12/23/22
                gbIngredientOrProduct.Visible = false;

            }
            #endregion
            #region SupplierCompare
            if (button == rbSupplierCompare)
            {
                pnlSupplier.Visible = true;
                pnlOwner.Visible = false;
                panelOutlier.Visible = false;
                clearDataSources();
                grpData.Visible = false;
                showHideSuppliers(false);
                if (rbSupplierCompare.Checked == true)
                {
                    showHideSuppliers(true);

                    //7/14/21
                    clearOwners();

                    //lblSupplier.Visible = true;                       //5/21/20 - commented          
                    //lueSupplier.Visible = true;
                    //lueSupplier2.Visible = true;

                    //6/25/21 - commented
                    //lblSupplierLocation.Visible = false;
                    //lueSupplierLocation.Visible = false;
                    //lueSupplierLocation2.Visible = false;
                    //lueSupplierLocation3.Visible = false;
                    //lueSupplierLocation4.Visible = false;
                }

                //12/23/22
                gbIngredientOrProduct.Visible = false;
            }
            #endregion                       
            #region Datasource Compare
            if (button == rbDataSourceCompare)
            {
                pnlOwner.Visible = false;
                panelOutlier.Visible = false;
                clearDataSources();
                grpData.Visible = false;

                //7/22/21 - changed to panel
                pnlDataSource.Visible = true;

                ckDataSource1.Visible = true;
                ckDataSource2.Visible = true;
                ckDataSource3.Visible = true;
                ckDataSource4.Visible = true;

                //7/14/21
                clearSuppliers();
                clearOwners();

                //12/23/22
                gbIngredientOrProduct.Visible = false;
            }
            #endregion

        }
        
        private void ckDataSource1_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDataSource1.CheckState == CheckState.Checked)
            {
                lueDataSource.Visible = true;
                lueDataSource.ShowPopup();
            }
            else
            {
                lueDataSource.Visible = false;                
                lueDataSource.EditValue = 0;                
            }
        }
        private void ckDataSource2_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDataSource2.CheckState == CheckState.Checked)
            {
                lueDataSource2.Visible = true;
                lueDataSource2.ShowPopup();
            }
            else
            {
                lueDataSource2.Visible = false;
                lueDataSource2.EditValue = 0;
            }
        }
        private void ckDataSource3_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDataSource3.CheckState == CheckState.Checked)
            {
                lueDataSource3.Visible = true;
                lueDataSource3.ShowPopup();
            }
            else
            {
                lueDataSource3.Visible = false;
                lueDataSource3.EditValue = 0;
            }
        }
        private void ckDataSource4_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDataSource4.CheckState == CheckState.Checked)
            {
                lueDataSource4.Visible = true;
                lueDataSource4.ShowPopup();
            }
            else
            {
                lueDataSource4.Visible = false;
                lueDataSource4.EditValue = 0;
            }
        }
               
        private void chartDataSourceCompare_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            try
            {
                ChartHitInfo sampleHit = chartDataSourceCompare.CalcHitInfo(e.HitInfo.HitPoint.X, e.HitInfo.HitPoint.Y);
                openSample(sampleHit, "chartDataSourceCompare");
            }
            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message);
            }
        }
                
        //6/25/21
        private void ckbOwn1_CheckedChanged(object sender, EventArgs e)
        {
            Int32 count = 0;

            //7/12/21
            //if (ckbOwn1.CheckState == CheckState.Checked)
            if (ckbOwn1.CheckState == CheckState.Checked && ((Int32)lueSupplier.EditValue > 0 && (Int32)lueSupplierLocation.EditValue > 0) )
            {
                lueOwner.Visible = true;

                count = (lueOwner.Properties.DataSource as IList).Count;
                if (count == 2)
                {
                    lueOwner.ItemIndex = 1;
                }

                if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true)
                {
                    lueOwnerLocation.Visible = true;
                }
            }

            //8/2/21
            else if (ckbOwn1.CheckState == CheckState.Checked && rbOwnerOnly.Checked == true)
            {
                lueOwner.Visible = true;

                count = (lueOwner.Properties.DataSource as IList).Count;
                if (count == 2)
                {
                    lueOwner.ItemIndex = 1;
                }

                if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true)
                {
                    lueOwnerLocation.Visible = true;
                }
            }

            else
            {                
                lueOwner.Visible = false;
                lueOwnerLocation.Visible = false;
                lueOwner.EditValue = 0;
                lueOwnerLocation.EditValue = 0;

                //7/12/21
                ckbOwn1.CheckState = CheckState.Unchecked;
            }
        }
                
        //7/9/21
        private void ckbOwn2_CheckedChanged(object sender, EventArgs e)
        {
            Int32 count = 0;

            //7/12/21
            //if (ckbOwn2.CheckState == CheckState.Checked)
            if (ckbOwn2.CheckState == CheckState.Checked && ((Int32)lueSupplier2.EditValue > 0 && (Int32)lueSupplierLocation2.EditValue > 0))
            {
                lueOwner2.Visible = true;

                count = (lueOwner2.Properties.DataSource as IList).Count;
                if (count == 2)
                {
                    lueOwner2.ItemIndex = 1;
                }

                if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true)
                {
                    lueOwnerLocation2.Visible = true;
                }
            }

            //8/2/21
            else if (ckbOwn2.CheckState == CheckState.Checked && rbOwnerOnly.Checked == true)
            {
                lueOwner2.Visible = true;

                count = (lueOwner2.Properties.DataSource as IList).Count;
                if (count == 2)
                {
                    lueOwner2.ItemIndex = 1;
                }

                if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true)
                {
                    lueOwnerLocation2.Visible = true;
                }
            }

            else
            {
                lueOwner2.Visible = false;
                lueOwnerLocation2.Visible = false;
                lueOwner2.EditValue = 0;
                lueOwnerLocation2.EditValue = 0;

                //7/12/21
                ckbOwn2.CheckState = CheckState.Unchecked;

            }
        }

        //7/9/21
        private void ckbOwn3_CheckedChanged(object sender, EventArgs e)
        {
            Int32 count = 0;

            //if (ckbOwn3.CheckState == CheckState.Checked)
            if (ckbOwn3.CheckState == CheckState.Checked && ((Int32)lueSupplier3.EditValue > 0 && (Int32)lueSupplierLocation3.EditValue > 0))
            {
                lueOwner3.Visible = true;

                count = (lueOwner3.Properties.DataSource as IList).Count;
                if (count == 2)
                {
                    lueOwner3.ItemIndex = 1;
                }

                if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true)
                {
                    lueOwnerLocation3.Visible = true;
                }
            }

            //8/2/21
            else if (ckbOwn3.CheckState == CheckState.Checked && rbOwnerOnly.Checked == true)
            {
                lueOwner3.Visible = true;

                count = (lueOwner3.Properties.DataSource as IList).Count;
                if (count == 2)
                {
                    lueOwner3.ItemIndex = 1;
                }

                if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true)
                {
                    lueOwnerLocation3.Visible = true;
                }
            }

            else
            {
                lueOwner3.Visible = false;
                lueOwnerLocation3.Visible = false;
                lueOwner3.EditValue = 0;
                lueOwnerLocation3.EditValue = 0;

                //7/12/21
                ckbOwn3.CheckState = CheckState.Unchecked;
            }
        }

        //7/9/21
        private void ckbOwn4_CheckedChanged(object sender, EventArgs e)
        {
            Int32 count = 0;

            //7/12/21
            //if (ckbOwn4.CheckState == CheckState.Checked)
            if (ckbOwn4.CheckState == CheckState.Checked && ((Int32)lueSupplier4.EditValue > 0 && (Int32)lueSupplierLocation4.EditValue > 0))
            {
                lueOwner4.Visible = true;

                count = (lueOwner4.Properties.DataSource as IList).Count;
                if (count == 2)
                {
                    lueOwner4.ItemIndex = 1;
                }

                if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true)
                {
                    lueOwnerLocation4.Visible = true;
                }
            }

            //8/2/21
            else if (ckbOwn4.CheckState == CheckState.Checked && rbOwnerOnly.Checked == true)
            {
                lueOwner4.Visible = true;

                count = (lueOwner4.Properties.DataSource as IList).Count;
                if (count == 2)
                {
                    lueOwner4.ItemIndex = 1;
                }

                if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true)
                {
                    lueOwnerLocation4.Visible = true;
                }
            }

            else
            {
                lueOwner4.Visible = false;
                lueOwnerLocation4.Visible = false;
                lueOwner4.EditValue = 0;
                lueOwnerLocation4.EditValue = 0;

                //7/12/21
                ckbOwn4.CheckState = CheckState.Unchecked;
            }
        }

        //7/9/21
        private void lueOwner2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dataBindOwnerLocationlue(lueOwner2, lueOwnerLocation2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //7/9/21
        private void lueOwner3_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dataBindOwnerLocationlue(lueOwner3, lueOwnerLocation3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //7/9/21
        private void lueOwner4_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dataBindOwnerLocationlue(lueOwner4, lueOwnerLocation4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //7/22/21
        private void SyncValueLue(ref LookUpEdit from, ref LookUpEdit to)
        {
            if (from != null && to != null)
            {
                to.EditValue = from.EditValue;
            }
        }

        //7/22/21
        private void lueMaterial_Name_EditValueChanged(object sender, EventArgs e)
        {
            //7/22/21
            //Filter suppliers for selected material -- show only suppliers that have this material
            dataBindSupplier();

            //7/22/21
            try
            {
                SyncValueLue(ref lueMaterial_Name, ref lueMaterial);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                
        //7/22/21
        private void lueNutrient_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueNutrient, ref lueNutrient_Name);
        }

        //7/22/21
        private void lueNutrient_Name_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueNutrient_Name, ref lueNutrient);
        }

        //7/22/21
        private void lueNutrient2_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueNutrient2, ref lueNutrient2_Name);
        }

        //7/22/21
        private void lueNutrient2_Name_EditValueChanged(object sender, EventArgs e)
        {
            SyncValueLue(ref lueNutrient2_Name, ref lueNutrient2);
        }

        //7/23/21
        private void deFromDate_EditValueChanged(object sender, EventArgs e)
        {
            DataBindMaterial();
        }

        //7/23/21
        private void deToDate_EditValueChanged(object sender, EventArgs e)
        {
            DataBindMaterial();
        }

        //8/2/21
        private void rbSupplierOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSupplierOnly.Checked == true)
            {
                pnlSupplier.Visible = true;
                pnlOwner.Visible = false;
                lblInfo.Visible = false;
            }
        }

        //8/2/21
        private void rbOwnerOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOwnerOnly.Checked == true)
            {
                pnlSupplier.Visible = false;
                pnlOwner.Visible = true;
                lblInfo.Visible = false;
            }
        }

        //8/2/21
        private void rbSupplierAndOwner_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSupplierAndOwner.Checked == true)
            {
                pnlSupplier.Visible = true;
                pnlOwner.Visible = true;
                lblInfo.Visible = true;

                //8/3/21
                lueOwner.Visible = false;
                lueOwnerLocation.Visible = false;
                lueOwner2.Visible = false;
                lueOwnerLocation2.Visible = false;
                lueOwner3.Visible = false;
                lueOwnerLocation3.Visible = false;
                lueOwner4.Visible = false;
                lueOwnerLocation4.Visible = false;
                ckbOwn1.CheckState = CheckState.Unchecked;
                ckbOwn2.CheckState = CheckState.Unchecked;
                ckbOwn3.CheckState = CheckState.Unchecked;
                ckbOwn4.CheckState = CheckState.Unchecked;

                if (ckbOwn1.CheckState == CheckState.Checked && ((Int32)lueSupplier.EditValue > 0 && (Int32)lueSupplierLocation.EditValue > 0))
                {
                    ckbOwn1.CheckState = CheckState.Checked;
                    lueOwner.Visible = true;
                    if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true)
                    {
                        lueOwnerLocation.Visible = true;
                    }
                }
                if (ckbOwn2.CheckState == CheckState.Checked && ((Int32)lueSupplier2.EditValue > 0 && (Int32)lueSupplierLocation2.EditValue > 0))
                {
                    ckbOwn2.CheckState = CheckState.Checked;
                    lueOwner2.Visible = true;
                    if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true)
                    {
                        lueOwnerLocation2.Visible = true;
                    }
                }
                if (ckbOwn3.CheckState == CheckState.Checked && ((Int32)lueSupplier3.EditValue > 0 && (Int32)lueSupplierLocation3.EditValue > 0))
                {
                    ckbOwn3.CheckState = CheckState.Checked;
                    lueOwner3.Visible = true;
                    if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true)
                    {
                        lueOwnerLocation3.Visible = true;
                    }
                }
                if (ckbOwn4.CheckState == CheckState.Checked && ((Int32)lueSupplier4.EditValue > 0 && (Int32)lueSupplierLocation4.EditValue > 0))
                {
                    ckbOwn4.CheckState = CheckState.Checked;
                    lueOwner4.Visible = true;
                    if (rbTrend.Checked == true || rbControlChart.Checked == true || rbAdvancedAnalysis.Checked == true)
                    {
                        lueOwnerLocation4.Visible = true;
                    }
                }

            }
        }

        //8/2/21
        private void showHideOwnerSupplierGroupBox()
        {
            grpSupplierAndOrOwner.Visible = false;
            if (rbControlChart.Checked == true || rbTrend.Checked == true || rbAdvancedAnalysis.Checked == true) 
            {
                grpSupplierAndOrOwner.Visible = true;

                if (rbSupplierOnly.Checked == true)
                {
                    pnlSupplier.Visible = true;
                    pnlOwner.Visible = false;
                }
                else if(rbOwnerOnly.Checked == true)
                {
                    pnlSupplier.Visible = false;
                    pnlOwner.Visible = true;
                }
                else
                {
                    pnlSupplier.Visible = true;
                    pnlOwner.Visible = true;
                }
            }
        }

        #region lue's Enter and Click
        private void lueMaterial_Enter(object sender, EventArgs e)
        {
            lueMaterial.SelectAll();
        }
        private void lueMaterial_Click(object sender, EventArgs e)
        {
            lueMaterial.SelectAll();
        }
        private void lueNutrient_Click(object sender, EventArgs e)
        {
            lueNutrient.SelectAll();
        }
        private void lueNutrient_Enter(object sender, EventArgs e)
        {
            lueNutrient.SelectAll();
        }
        private void lueNutrient2_Enter(object sender, EventArgs e)
        {
            lueNutrient2.SelectAll();
        }
        private void lueNutrient2_Click(object sender, EventArgs e)
        {
            lueNutrient2.SelectAll();
        }
        private void lueSupplier_Click(object sender, EventArgs e)
        {
            lueSupplier.SelectAll();
        }
        private void lueSupplier_Enter(object sender, EventArgs e)
        {
            lueSupplier.SelectAll();
        }
        private void lueSupplier2_Click(object sender, EventArgs e)
        {
            lueSupplier2.SelectAll();
        }
        private void lueSupplier2_Enter(object sender, EventArgs e)
        {
            lueSupplier2.SelectAll();
        }
        private void lueSupplier3_Click(object sender, EventArgs e)
        {
            lueSupplier3.SelectAll();
        }
        private void lueSupplier3_Enter(object sender, EventArgs e)
        {
            lueSupplier3.SelectAll();
        }
        private void lueSupplier4_Click(object sender, EventArgs e)
        {
            lueSupplier4.SelectAll();
        }
        private void lueSupplier4_Enter(object sender, EventArgs e)
        {
            lueSupplier4.SelectAll();
        }
        private void lueSupplierLocation_Click(object sender, EventArgs e)
        {
            lueSupplierLocation.SelectAll();
        }
        private void lueSupplierLocation_Enter(object sender, EventArgs e)
        {
            lueSupplierLocation.SelectAll();
        }
        private void lueSupplierLocation2_Click(object sender, EventArgs e)
        {
            lueSupplierLocation2.SelectAll();
        }
        private void lueSupplierLocation2_Enter(object sender, EventArgs e)
        {
            lueSupplierLocation2.SelectAll();
        }
        private void lueSupplierLocation3_Click(object sender, EventArgs e)
        {
            lueSupplierLocation3.SelectAll();
        }
        private void lueSupplierLocation3_Enter(object sender, EventArgs e)
        {
            lueSupplierLocation3.SelectAll();
        }
        private void lueSupplierLocation4_Click(object sender, EventArgs e)
        {
            lueSupplierLocation4.SelectAll();
        }
        private void lueSupplierLocation4_Enter(object sender, EventArgs e)
        {
            lueSupplierLocation4.SelectAll();
        }
        
        //7/22/21
        private void lueMaterial_Name_Click(object sender, EventArgs e)
        {
            lueMaterial_Name.SelectAll();
        }

        //7/22/21
        private void lueMaterial_Name_Enter(object sender, EventArgs e)
        {
            lueMaterial_Name.SelectAll();
        }

        //7/22/21
        private void lueNutrient_Name_Click(object sender, EventArgs e)
        {
            lueNutrient_Name.SelectAll();
        }

        //7/22/21
        private void lueNutrient_Name_Enter(object sender, EventArgs e)
        {
            lueNutrient_Name.SelectAll();
        }

        //7/22/21
        private void lueNutrient2_Name_Click(object sender, EventArgs e)
        {
            lueNutrient2_Name.SelectAll();
        }

        //7/22/21
        private void lueNutrient2_Name_Enter(object sender, EventArgs e)
        {
            lueNutrient2_Name.SelectAll();
        }
        #endregion

        //11/15/22
        private void grpSupplierAndOrOwner_VisibleChanged(object sender, EventArgs e)
        {
            if (grpSupplierAndOrOwner.Visible == true)
            {
                lblInfo.Visible = true;
            }
            else
            {
                lblInfo.Visible = false;
            }
        }


        private void chartProductControlByDate_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e)
        {
            foreach (CrosshairElement ce in e.CrosshairElements)
            {

                LineSeriesView v;
                v = ce.Series.View as LineSeriesView;
                if (ce.Series.Name == "Predicted Realized High Tolerance Value" || ce.Series.Name == "Predicted Realized Low Tolerance Value")
                {
                    (ce.Series.View as LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
                }
                else
                {
                    (ce.Series.View as LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                }
            }
        }

        //12/27/22
        private void rbControlIngredient_CheckedChanged(object sender, EventArgs e)
        {
            rbReportCheckChanged(rbControlChart);

            //1/6/23
            DataBindMaterial();
        }

        //12/27/22
        private void rbControlProduct_CheckedChanged(object sender, EventArgs e)
        {
            rbReportCheckChanged(rbControlChart);

            //1/6/23
            DataBindMaterial();
        }

        //12/29/22
        private void rbToleranceNone_CheckedChanged(object sender, EventArgs e)
        {
            lueNutrientTolerance.Visible = false;
        }
        private void rbNutrientTolerance_CheckedChanged(object sender, EventArgs e)
        {
            lueNutrientTolerance.Visible = true;
        }
        private void rbProductTolerance_CheckedChanged(object sender, EventArgs e)
        {
            lueNutrientTolerance.Visible = false;
        }

        //12/29/22
        private void dataBindTolerancelue()
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

        //12/29/22
        private void btnToleranceHelp_Click(object sender, EventArgs e)
        {
            AnalysisProductToleranceHelp helpTolerance = new AnalysisProductToleranceHelp();
            helpTolerance.ShowDialog();
        }

        private void chartProductControl_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e)
        {
            foreach (CrosshairElement ce in e.CrosshairElements)
            {

                LineSeriesView v;
                v = ce.Series.View as LineSeriesView;

                if (v != null)
                {
                    if (ce.Series.Name == "Value")
                    {
                        (ce.Series.View as LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
                    }
                    else
                    {
                        (ce.Series.View as LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                    }
                }
                
            }
        }

        private void chartProductControl_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            try
            {
                ChartHitInfo sampleHit = chartProductControl.CalcHitInfo(e.HitInfo.HitPoint.X, e.HitInfo.HitPoint.Y);
                openSample(sampleHit, "chartProductControl");
            }
            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message);
            }
        }

        private void chartProductControlByDate_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            try
            {
                ChartHitInfo sampleHit = chartProductControlByDate.CalcHitInfo(e.HitInfo.HitPoint.X, e.HitInfo.HitPoint.Y);
                openSampleList_UsingAnalysisProductQuery(sampleHit, "chartProductControlByDate");
            }
            catch (Exception ex)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message);
            }
        }

        //1/5/23
        private void DataBindProductVersion()
        {

            List<CFCProductArchiveInfo> version = new List<CFCProductArchiveInfo>();
            version = AIMManager.GetCFCProductArchive((Int32)lueMaterial.EditValue);

            lueVersion.Properties.DataSource = version;
            XtraEditors.LoadLookUpEdit(ref lueVersion, "CFCProductArchiveID", "ArchiveVersion", "ProductionDate");
            lueVersion.Properties.Columns[1].Caption = "Archive Version";
            lueVersion.Properties.Columns[2].Caption = "Stored Date";
            if (version.Count > 0)
            {
                lueVersion.EditValue = version[0].CFCProductArchiveID;
            }


        }


    }
}

