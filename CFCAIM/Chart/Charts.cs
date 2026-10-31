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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CFCUser;
using System.Globalization;

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

        //task 1133 FG 7/21/2026 - maps between the existing Name dropdowns and the new Code dropdowns (supplier, supplier location, owner, owner location)
        Dictionary<LookUpEdit, LookUpEdit> _codeLueForNameLue = new Dictionary<LookUpEdit, LookUpEdit>();
        Dictionary<LookUpEdit, LookUpEdit> _nameLueForCodeLue = new Dictionary<LookUpEdit, LookUpEdit>();

        //task 1035 FG 8/7/2026 - rows added from the supplier/owner picker rows (Pick --> Add --> Grid) -- no limit on how many
        BindingList<ChartsSupplierSelectionRow> _supplierSelections = new BindingList<ChartsSupplierSelectionRow>();
        BindingList<ChartsOwnerSelectionRow> _ownerSelections = new BindingList<ChartsOwnerSelectionRow>();

        //task 1035 FG 8/7/2026 - colors for the dynamically-created chart series (repeats if more rows than colors)
        static readonly Color[] _seriesColors = new Color[]
        {
            Color.CornflowerBlue, Color.DarkOrange, Color.Red, Color.LimeGreen,
            Color.MediumPurple, Color.Teal, Color.Sienna, Color.Magenta,
            Color.Olive, Color.Navy, Color.Crimson, Color.DarkCyan
        };

        //private object userGlobal;

        #endregion

        public Charts()
		{
			InitializeComponent();
			CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026

            BuildCodeNameDropdownPairs();       //task 1133 FG 7/21/2026

            //task 1035 FG 8/7/2026 - bind the supplier/owner selection grids to the selection lists
            grdSupplierSel.DataSource = _supplierSelections;
            grdOwnerSel.DataSource = _ownerSelections;
		}

        //task 1133 FG 7/21/2026 - pairs each existing Name dropdown with its new Code dropdown and wires the sync/visibility events
        private void BuildCodeNameDropdownPairs()
        {
            _codeLueForNameLue.Add(lueSupplier, lueSupplierCode);
            _codeLueForNameLue.Add(lueSupplierLocation, lueSupplierLocationCode);
            _codeLueForNameLue.Add(lueOwner, lueOwnerCode);
            _codeLueForNameLue.Add(lueOwnerLocation, lueOwnerLocationCode);

            foreach (KeyValuePair<LookUpEdit, LookUpEdit> pair in _codeLueForNameLue)
            {
                LookUpEdit nameLue = pair.Key;
                LookUpEdit codeLue = pair.Value;

                _nameLueForCodeLue.Add(codeLue, nameLue);

                //keep Code and Name dropdowns showing the same record
                nameLue.EditValueChanged += new EventHandler(lueName_EditValueChanged);
                codeLue.EditValueChanged += new EventHandler(lueCode_EditValueChanged);

                //Code dropdown follows the Name dropdown whenever the form shows/hides or enables/disables it
                nameLue.VisibleChanged += new EventHandler(lueName_VisibleEnabledChanged);
                nameLue.EnabledChanged += new EventHandler(lueName_VisibleEnabledChanged);

                //select-all on click/enter, same behavior as the existing dropdowns
                codeLue.Click += new EventHandler(lueCode_ClickEnter);
                codeLue.Enter += new EventHandler(lueCode_ClickEnter);
            }
        }

        //task 1133 FG 7/21/2026 - Name dropdown changed --> copy the selected record to its Code dropdown
        private void lueName_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit nameLue = (LookUpEdit)sender;
            LookUpEdit codeLue = _codeLueForNameLue[nameLue];
            SyncValueLue(ref nameLue, ref codeLue);
        }

        //task 1133 FG 7/21/2026 - Code dropdown changed --> copy the selected record to its Name dropdown
        private void lueCode_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit codeLue = (LookUpEdit)sender;
            LookUpEdit nameLue = _nameLueForCodeLue[codeLue];
            SyncValueLue(ref codeLue, ref nameLue);
        }

        //task 1133 FG 7/21/2026 - mirror Visible/Enabled from the Name dropdown to its Code dropdown
        private void lueName_VisibleEnabledChanged(object sender, EventArgs e)
        {
            LookUpEdit nameLue = (LookUpEdit)sender;
            LookUpEdit codeLue = _codeLueForNameLue[nameLue];
            codeLue.Visible = nameLue.Visible;
            codeLue.Enabled = nameLue.Enabled;
        }

        //task 1133 FG 7/21/2026
        private void lueCode_ClickEnter(object sender, EventArgs e)
        {
            ((LookUpEdit)sender).SelectAll();
        }

        //task 1133 FG 7/21/2026 - column header labels above the supplier Code/Name dropdowns, shown/hidden together with lblSupplier
        private void ShowHideSupplierColumnHeaders(Boolean blnShow)
        {
            lblSupplierCodeHdr.Visible = blnShow;
            lblSupplierNameHdr.Visible = blnShow;
            lblSupplierLocationCodeHdr.Visible = blnShow;
            lblSupplierLocationNameHdr.Visible = blnShow;
        }

        //task 1133 FG 7/21/2026 - column header labels above the owner Code/Name dropdowns, shown/hidden together with lblOwner
        private void ShowHideOwnerColumnHeaders(Boolean blnShow)
        {
            lblOwnerCodeHdr.Visible = blnShow;
            lblOwnerNameHdr.Visible = blnShow;
            lblOwnerLocationCodeHdr.Visible = blnShow;
            lblOwnerLocationNameHdr.Visible = blnShow;
        }

        #region task 1035 FG 8/7/2026 - supplier/owner selection grids (Pick --> Add --> Grid, any number of rows)

        //task 1035 FG 8/7/2026 - adds the supplier/supplier-location currently picked in the dropdowns to the supplier grid
        private void btnSupplierAdd_Click(object sender, EventArgs e)
        {
            Int32 supplierID = 0;
            Int32 supplierLocationID = 0;

            if (lueSupplier.EditValue != null)
            {
                Int32.TryParse(lueSupplier.EditValue.ToString(), out supplierID);
            }
            if (lueSupplierLocation.EditValue != null)
            {
                Int32.TryParse(lueSupplierLocation.EditValue.ToString(), out supplierLocationID);
            }

            if (supplierID == 0)
            {
                MessageBox.Show("Please select a Supplier to add.", "Warning...");
                lueSupplier.ShowPopup();
                return;
            }

            foreach (ChartsSupplierSelectionRow row in _supplierSelections)
            {
                if (row.SupplierID == supplierID && row.SupplierLocationID == supplierLocationID)
                {
                    MessageBox.Show("This Supplier/Location is already in the list.", "Warning...");
                    return;
                }
            }

            ChartsSupplierSelectionRow newRow = new ChartsSupplierSelectionRow();
            newRow.SupplierID = supplierID;
            newRow.SupplierCode = lueSupplierCode.Text;
            newRow.SupplierName = lueSupplier.Text;
            newRow.SupplierLocationID = supplierLocationID;
            newRow.SupplierLocationCode = supplierLocationID > 0 ? lueSupplierLocationCode.Text : "";
            newRow.SupplierLocationName = supplierLocationID > 0 ? lueSupplierLocation.Text : "";
            _supplierSelections.Add(newRow);
            RenumberSelectionRows();

            //reset the picker for the next entry
            lueSupplier.EditValue = 0;
            lueSupplierLocation.EditValue = 0;
        }

        //task 1035 FG 8/7/2026 - removes the highlighted row from the supplier grid
        //Cursor change 06/10/2026 task 1035 - called from inline grid delete button (replaced external Remove button)
        private void RemoveSupplierSelectionRow()
        {
            ChartsSupplierSelectionRow focusedRow = gvSupplierSel.GetFocusedRow() as ChartsSupplierSelectionRow;
            if (focusedRow == null)
            {
                MessageBox.Show("Please select a Supplier row to remove.", "Warning...");
                return;
            }
            _supplierSelections.Remove(focusedRow);
            RenumberSelectionRows();
        }

        //Cursor change 06/10/2026 task 1035 - inline delete column click handler for supplier grid
        private void repbtnSupSelDelete_Click(object sender, EventArgs e)
        {
            RemoveSupplierSelectionRow();
        }

        //task 1035 FG 8/7/2026 - adds the owner/owner-location currently picked in the dropdowns to the owner grid
        private void btnOwnerAdd_Click(object sender, EventArgs e)
        {
            Int32 ownerID = 0;
            Int32 ownerLocationID = 0;

            if (lueOwner.EditValue != null)
            {
                Int32.TryParse(lueOwner.EditValue.ToString(), out ownerID);
            }
            if (lueOwnerLocation.EditValue != null)
            {
                Int32.TryParse(lueOwnerLocation.EditValue.ToString(), out ownerLocationID);
            }

            if (ownerID == 0)
            {
                MessageBox.Show("Please select an Owner to add.", "Warning...");
                lueOwner.ShowPopup();
                return;
            }

            foreach (ChartsOwnerSelectionRow row in _ownerSelections)
            {
                if (row.OwnerID == ownerID && row.OwnerLocationID == ownerLocationID)
                {
                    MessageBox.Show("This Owner/Location is already in the list.", "Warning...");
                    return;
                }
            }

            ChartsOwnerSelectionRow newRow = new ChartsOwnerSelectionRow();
            newRow.OwnerID = ownerID;
            newRow.OwnerCode = lueOwnerCode.Text;
            newRow.OwnerName = lueOwner.Text;
            newRow.OwnerLocationID = ownerLocationID;
            newRow.OwnerLocationCode = ownerLocationID > 0 ? lueOwnerLocationCode.Text : "";
            newRow.OwnerLocationName = ownerLocationID > 0 ? lueOwnerLocation.Text : "";
            _ownerSelections.Add(newRow);
            RenumberSelectionRows();

            //reset the picker for the next entry
            lueOwner.EditValue = 0;
            lueOwnerLocation.EditValue = 0;
        }

        //task 1035 FG 8/7/2026 - removes the highlighted row from the owner grid
        //Cursor change 06/10/2026 task 1035 - called from inline grid delete button (replaced external Remove button)
        private void RemoveOwnerSelectionRow()
        {
            ChartsOwnerSelectionRow focusedRow = gvOwnerSel.GetFocusedRow() as ChartsOwnerSelectionRow;
            if (focusedRow == null)
            {
                MessageBox.Show("Please select an Owner row to remove.", "Warning...");
                return;
            }
            _ownerSelections.Remove(focusedRow);
            RenumberSelectionRows();
        }

        //Cursor change 06/10/2026 task 1035 - inline delete column click handler for owner grid
        private void repbtnOwnSelDelete_Click(object sender, EventArgs e)
        {
            RemoveOwnerSelectionRow();
        }

        //task 1035 FG 8/7/2026 - re-numbers the # column in both grids (rows are paired by number: supplier row 1 runs with owner row 1)
        private void RenumberSelectionRows()
        {
            for (Int32 i = 0; i < _supplierSelections.Count; i++)
            {
                _supplierSelections[i].RowNumber = i + 1;
            }
            for (Int32 i = 0; i < _ownerSelections.Count; i++)
            {
                _ownerSelections[i].RowNumber = i + 1;
            }
            grdSupplierSel.RefreshDataSource();
            grdOwnerSel.RefreshDataSource();
        }

        //task 1035 FG 8/7/2026 - combines the supplier and owner grid rows into one list of pairs, matched by row position
        //                        (same AND relationship as the old screen where supplier row 1 ran with owner row 1).
        //                        Which grids are used depends on the Supplier only / Owner only / Supplier and Owner radios.
        //                        If a grid is empty but the picker dropdowns have a value, the picker value is used as one row,
        //                        so the screen still works like before for a single supplier/owner without pressing Add.
        private List<ChartsSupplierOwnerPair> BuildSupplierOwnerPairs()
        {
            List<ChartsSupplierOwnerPair> pairs = new List<ChartsSupplierOwnerPair>();

            Boolean useSuppliers = rbOwnerOnly.Checked == false;
            Boolean useOwners = rbSupplierOnly.Checked == false && rbSupplierCompare.Checked == false && rbDataSourceCompare.Checked == false;

            List<ChartsSupplierSelectionRow> supplierRows = new List<ChartsSupplierSelectionRow>(_supplierSelections);
            List<ChartsOwnerSelectionRow> ownerRows = new List<ChartsOwnerSelectionRow>(_ownerSelections);

            //picker fallback -- run with the dropdown selection when nothing was added to the grid
            if (useSuppliers == true && supplierRows.Count == 0 && lueSupplier.EditValue != null && (Int32)lueSupplier.EditValue > 0)
            {
                ChartsSupplierSelectionRow pickerRow = new ChartsSupplierSelectionRow();
                pickerRow.SupplierID = (Int32)lueSupplier.EditValue;
                pickerRow.SupplierName = lueSupplier.Text;
                if (lueSupplierLocation.EditValue != null && (Int32)lueSupplierLocation.EditValue > 0)
                {
                    pickerRow.SupplierLocationID = (Int32)lueSupplierLocation.EditValue;
                    pickerRow.SupplierLocationName = lueSupplierLocation.Text;
                }
                supplierRows.Add(pickerRow);
            }
            if (useOwners == true && ownerRows.Count == 0 && lueOwner.EditValue != null && (Int32)lueOwner.EditValue > 0)
            {
                ChartsOwnerSelectionRow pickerRow = new ChartsOwnerSelectionRow();
                pickerRow.OwnerID = (Int32)lueOwner.EditValue;
                pickerRow.OwnerName = lueOwner.Text;
                if (lueOwnerLocation.EditValue != null && (Int32)lueOwnerLocation.EditValue > 0)
                {
                    pickerRow.OwnerLocationID = (Int32)lueOwnerLocation.EditValue;
                    pickerRow.OwnerLocationName = lueOwnerLocation.Text;
                }
                ownerRows.Add(pickerRow);
            }

            if (useSuppliers == false)
            {
                supplierRows.Clear();
            }
            if (useOwners == false)
            {
                ownerRows.Clear();
            }

            Int32 pairCount = Math.Max(supplierRows.Count, ownerRows.Count);
            for (Int32 i = 0; i < pairCount; i++)
            {
                ChartsSupplierOwnerPair pair = new ChartsSupplierOwnerPair();

                String supplierPart = "";
                String ownerPart = "";

                if (i < supplierRows.Count)
                {
                    pair.SupplierID = supplierRows[i].SupplierID;
                    pair.SupplierLocationID = supplierRows[i].SupplierLocationID;
                    supplierPart = pair.SupplierLocationID > 0 ? supplierRows[i].SupplierLocationName : supplierRows[i].SupplierName;
                }
                if (i < ownerRows.Count)
                {
                    pair.OwnerID = ownerRows[i].OwnerID;
                    pair.OwnerLocationID = ownerRows[i].OwnerLocationID;
                    ownerPart = pair.OwnerLocationID > 0 ? ownerRows[i].OwnerLocationName : ownerRows[i].OwnerName;
                }

                //legend text, same style as the old screen (supplier-location, owner-location, or "location-owner" for both)
                if (supplierPart != "" && ownerPart != "")
                {
                    pair.DisplayName = supplierPart + "-" + ownerPart;
                }
                else
                {
                    pair.DisplayName = supplierPart + ownerPart;
                }

                if (pair.SupplierID > 0 || pair.OwnerID > 0)
                {
                    pairs.Add(pair);
                }
            }

            return pairs;
        }

        //task 1035 8/10/26 cursor change - old version kept for reference:  one combined list
        //                                  'SupplierID,SupplierLocationID,OwnerID,OwnerLocationID;...'
        //                                  replaced by BuildChartsSupplierPairList / BuildChartsOwnerPairList below
        //private String BuildChartsSupplierOwnerPairList(List<ChartsSupplierOwnerPair> pairs)
        //{
        //    String pairList = "";
        //    foreach (ChartsSupplierOwnerPair pair in pairs)
        //    {
        //        if (pairList != "")
        //        {
        //            pairList += ";";
        //        }
        //        pairList += pair.SupplierID + "," + pair.SupplierLocationID + "," + pair.OwnerID + "," + pair.OwnerLocationID;
        //    }
        //    return pairList;
        //}

        //task 1035 8/10/26 cursor change - supplier rows and owner rows are now sent as two separate delimited lists
        //                                  (supplier and owner are different selections on the screen).  Both lists are
        //                                  built from the same pair list, so entry 1 of each list is the same row --
        //                                  that keeps 'Both (Supplier and Owner)' paired by position in SQL.
        //                                  Supplier list:  'SupplierID,SupplierLocationID;...'  (empty when no supplier is selected)
        private String BuildChartsSupplierPairList(List<ChartsSupplierOwnerPair> pairs)
        {
            String pairList = "";
            Boolean hasValue = false;
            foreach (ChartsSupplierOwnerPair pair in pairs)
            {
                if (pairList != "")
                {
                    pairList += ";";
                }
                pairList += pair.SupplierID + "," + pair.SupplierLocationID;
                if (pair.SupplierID > 0)
                {
                    hasValue = true;
                }
            }
            return hasValue == true ? pairList : "";
        }

        //task 1035 8/10/26 cursor change - owner list:  'OwnerID,OwnerLocationID;...'  (entry positions match the
        //                                  supplier list; empty when no owner is selected)
        private String BuildChartsOwnerPairList(List<ChartsSupplierOwnerPair> pairs)
        {
            String pairList = "";
            Boolean hasValue = false;
            foreach (ChartsSupplierOwnerPair pair in pairs)
            {
                if (pairList != "")
                {
                    pairList += ";";
                }
                pairList += pair.OwnerID + "," + pair.OwnerLocationID;
                if (pair.OwnerID > 0)
                {
                    hasValue = true;
                }
            }
            return hasValue == true ? pairList : "";
        }

        //task 1035 FG 8/7/2026 - series colors repeat when there are more grid rows than colors
        private Color GetSeriesColor(Int32 seriesIndex)
        {
            return _seriesColors[seriesIndex % _seriesColors.Length];
        }

        //task 1035 FG 8/7/2026 - @SupplierOwnerOption for one pair:  1=Supplier only;  2=Owner only;  3=Both
        private Int32 GetPairSupplierOwnerOption(ChartsSupplierOwnerPair pair)
        {
            if (pair.SupplierID == 0 && pair.OwnerID > 0)
            {
                return 2;
            }
            if (pair.SupplierID > 0 && pair.OwnerID > 0)
            {
                return 3;
            }
            return 1;
        }

        //task 1035 FG 8/7/2026 - loads a Trend chart (by sample code or by date) with one dynamically-created series per
        //                        supplier/owner grid row, so the chart is no longer limited to 4 rows.  Each series is a
        //                        point series with a regression line, same look as the old designer-defined series.
        private void LoadTrendChart(ChartControl chart, List<ChartsSupplierOwnerPair> supplierOwnerPairs, Int32 aimMaterialID, Int32 aimNutrientID, DateTime startDt, DateTime endDt, Int32 chartOption, Boolean byDate)
        {
            chart.DataSource = null;
            chart.Series.Clear();
            _chart = new List<ChartInfo>();

            List<ChartsSupplierOwnerPair> pairsToRun = new List<ChartsSupplierOwnerPair>(supplierOwnerPairs);
            Boolean unfiltered = false;
            if (pairsToRun.Count == 0)
            {
                //nothing selected -- run once, unfiltered, and hide the legend (same as the old screen with nothing selected)
                pairsToRun.Add(new ChartsSupplierOwnerPair());
                unfiltered = true;
            }

            Int32 seriesIndex = 0;
            foreach (ChartsSupplierOwnerPair pair in pairsToRun)
            {
                ChartInfo pairCriteria = new ChartInfo();
                pairCriteria.AIMMaterialID = aimMaterialID;
                pairCriteria.AIMNutrientID = aimNutrientID;
                pairCriteria.StartDate = startDt;
                pairCriteria.EndDate = endDt;
                pairCriteria.AIMSupplierID = pair.SupplierID;
                pairCriteria.AIMSupplierLocationID = pair.SupplierLocationID;
                pairCriteria.AIMOwnerID = pair.OwnerID;
                pairCriteria.AIMOwnerLocationID = pair.OwnerLocationID;
                pairCriteria.SupplierOwnerOption = GetPairSupplierOwnerOption(pair);

                List<ChartInfo> pairData = AIMManager.GetTrendChartData(ref pairCriteria);
                _chart.AddRange(pairData);

                String seriesName = unfiltered == true ? "Result Value" : pair.DisplayName;
                Series series = new Series(seriesName, ViewType.Point);
                series.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
                series.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.True;
                series.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
                series.CrosshairLabelPattern = byDate == true ? "Sample Date: {A:MM/dd/yyyy}" : "Sample Code: {A}, Result: {V}";
                series.Tag = pair;

                Color seriesColor = GetSeriesColor(seriesIndex);
                PointSeriesView pointView = (PointSeriesView)series.View;
                pointView.Color = seriesColor;
                pointView.PointMarkerOptions.Size = 10;

                RegressionLine regressionLine = new RegressionLine("Regression Line " + (seriesIndex + 1));
                regressionLine.Color = seriesColor;
                regressionLine.LineStyle.Thickness = 3;
                pointView.Indicators.Add(regressionLine);

                chart.Series.Add(series);

                foreach (ChartInfo dataRow in pairData)
                {
                    //same filter as the old designer series ([ResultValue] <> 0.0)
                    if (dataRow.ResultValue != 0)
                    {
                        if (byDate == true)
                        {
                            series.Points.Add(new SeriesPoint(dataRow.SampleDate, dataRow.ResultValue));
                        }
                        else
                        {
                            series.Points.Add(new SeriesPoint(dataRow.SampleCode, dataRow.ResultValue));
                        }
                    }
                }
                seriesIndex++;
            }

            chart.Legend.Visibility = unfiltered == true ? DevExpress.Utils.DefaultBoolean.False : DevExpress.Utils.DefaultBoolean.True;
        }

        #endregion

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
                //task 1035 FG 8/7/2026 - only one picker row now; selections go into the grids
                dataBindSupplierLocationlue(lueSupplier, lueSupplierLocation);

                //7/9/21 - added owner and owner locations
                dataBindOwnerLocationlue(lueOwner, lueOwnerLocation);

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
            //task 1035 FG 8/7/2026 - suppliers now come from the supplier selection grid (Pick --> Add --> Grid)
            if (rbSupplierCompare.Checked == true && _supplierSelections.Count < 2)
            {
                MessageBox.Show("Please add at least two Suppliers to the supplier list.", "Warning...");
                lueSupplier.ShowPopup();
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

            //task 1035 FG 8/7/2026 - supplier/owner selections now come from the two grids (any number of rows),
            //                        combined by row position (supplier row 1 with owner row 1, row 2 with row 2, etc.)
            List<ChartsSupplierOwnerPair> supplierOwnerPairs = BuildSupplierOwnerPairs();

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

            DateTime date;
            DateTime.TryParseExact(deFromDate.Text, "dd-MM-yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None,out date);

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
                    //task 1035 8/10/26 cursor change - old version kept for reference:  one combined list
                    //String chartsSupplierOwnerPairList = BuildChartsSupplierOwnerPairList(supplierOwnerPairs);
                    //analysis = AIMManager.GetAnalysis(_analysisQueryType, _queryTemplateID, startDt, endDt, false, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDate, 0, 0, 0, 0, 0, 1, 1, 0, "", (Int32)_mainMenu.lueDataMap.EditValue, _outlierType, 0, 0, true, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, chartOption, chartsSupplierOwnerPairList);

                    //task 1035 8/10/26 cursor change - supplier rows and owner rows are now sent as two separate delimited
                    //                                  lists (matched by position for 'Both'), no limit on the number of rows
                    String chartsSupplierPairList = BuildChartsSupplierPairList(supplierOwnerPairs);
                    String chartsOwnerPairList = BuildChartsOwnerPairList(supplierOwnerPairs);
                    analysis = AIMManager.GetAnalysis(_analysisQueryType, _queryTemplateID, startDt, endDt, false, true, (Int32)lueMaterial.EditValue, (Int32)lueNutrient.EditValue, sampleDate, 0, 0, 0, 0, 0, 1, 1, 0, "", (Int32)_mainMenu.lueDataMap.EditValue, _outlierType, 0, 0, true, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, chartOption, chartsSupplierPairList, chartsOwnerPairList);

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

                //task 1035 FG 8/7/2026 - one series per supplier/owner grid row (no 4-row limit) -- created dynamically
                changeSizeAndLocation(chartTrend);
                LoadTrendChart(chartTrend, supplierOwnerPairs, aimMaterialID, aimNutrientID, startDt, endDt, chartOption, false);

                AIMGlobal.SplashScreenHide();
                return;
            }
            #endregion

            #region Trend By Date -----------------------------------------------------            
            //Trend By Date
            if (rbTrend.Checked == true && rbDate.Checked == true)
            {
                AIMGlobal.SplashScreenShow();

                //task 1035 FG 8/7/2026 - one series per supplier/owner grid row (no 4-row limit) -- created dynamically
                changeSizeAndLocation(chartTrendByDate);
                LoadTrendChart(chartTrendByDate, supplierOwnerPairs, aimMaterialID, aimNutrientID, startDt, endDt, chartOption, true);

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

                //task 1035 FG 8/7/2026 - run the query once per supplier/owner grid row (no 4-row limit) and merge the results,
                //                        skipping samples already returned by an earlier row (the rows are OR'd, same as before)
                List<ChartsSupplierOwnerPair> pairsToRun = new List<ChartsSupplierOwnerPair>(supplierOwnerPairs);
                if (pairsToRun.Count == 0)
                {
                    //nothing selected -- run once, unfiltered (same as the old screen with nothing selected)
                    pairsToRun.Add(new ChartsSupplierOwnerPair());
                }

                _chart = new List<ChartInfo>();
                Dictionary<Int32, Boolean> includedSampleIDs = new Dictionary<Int32, Boolean>();
                foreach (ChartsSupplierOwnerPair pair in pairsToRun)
                {
                    ChartInfo pairCriteria = new ChartInfo();
                    pairCriteria.AIMMaterialID = aimMaterialID;
                    pairCriteria.AIMNutrientID = aimNutrientID;
                    pairCriteria.AIMNutrientID2 = aimNutrientID2;
                    pairCriteria.StartDate = startDt;
                    pairCriteria.EndDate = endDt;
                    pairCriteria.AIMSupplierID = pair.SupplierID;
                    pairCriteria.AIMSupplierLocationID = pair.SupplierLocationID;
                    pairCriteria.AIMOwnerID = pair.OwnerID;
                    pairCriteria.AIMOwnerLocationID = pair.OwnerLocationID;
                    pairCriteria.SupplierOwnerOption = GetPairSupplierOwnerOption(pair);

                    List<ChartInfo> pairData = AIMManager.GetAdvancedAnalysisChartData(ref pairCriteria);
                    foreach (ChartInfo dataRow in pairData)
                    {
                        if (dataRow.SampleID == 0 || includedSampleIDs.ContainsKey(dataRow.SampleID) == false)
                        {
                            if (dataRow.SampleID != 0)
                            {
                                includedSampleIDs.Add(dataRow.SampleID, true);
                            }
                            _chart.Add(dataRow);
                        }
                    }
                }

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

                //task 1035 FG 8/7/2026 - run the query once per supplier grid row (no 4-row limit) and create one line series per row
                chartSupplierCompare.DataSource = null;
                chartSupplierCompare.Series.Clear();
                _chart = new List<ChartInfo>();

                Int32 seriesIndex = 0;
                foreach (ChartsSupplierSelectionRow supplierRow in _supplierSelections)
                {
                    ChartInfo pairCriteria = new ChartInfo();
                    pairCriteria.AIMMaterialID = aimMaterialID;
                    pairCriteria.AIMNutrientID = aimNutrientID;
                    pairCriteria.AIMSupplierID = supplierRow.SupplierID;
                    pairCriteria.AIMSupplierLocationID = supplierRow.SupplierLocationID;
                    pairCriteria.StartDate = startDt;
                    pairCriteria.EndDate = endDt;

                    List<ChartInfo> pairData = AIMManager.GetSupplierCompareChartData(ref pairCriteria);
                    _chart.AddRange(pairData);

                    String seriesName = supplierRow.SupplierName;
                    if (supplierRow.SupplierLocationID > 0)
                    {
                        seriesName = supplierRow.SupplierName + " " + supplierRow.SupplierLocationName;
                    }

                    Series series = new Series(seriesName, ViewType.Line);
                    chartSupplierCompare.Series.Add(series);
                    LineSeriesView lineView = (LineSeriesView)series.View;
                    lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                    lineView.Color = GetSeriesColor(seriesIndex);
                    series.Tag = supplierRow;

                    foreach (ChartInfo dataRow in pairData)
                    {
                        //same filter as the old designer series ([ResultValue] <> 0.0)
                        if (dataRow.ResultValue != 0)
                        {
                            series.Points.Add(new SeriesPoint(dataRow.SampleDate, dataRow.ResultValue));
                        }
                    }
                    seriesIndex++;
                }

                chartSupplierCompare.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                AIMGlobal.SplashScreenHide();

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
                    SupplierName = "--Select--",
                    AIMSupplierCode = "--Select--"      //task 1133 FG 7/21/2026
                }
            );
            lueSupplier.Properties.DataSource = supplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplier, "AIMSupplierID", "SupplierName");
            lueSupplier.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

            //task 1133 FG 7/21/2026 - bind the Supplier Code dropdown to the same list as the Name dropdown
            //task 1035 FG 8/7/2026 - suppliers 2-4 removed -- extra suppliers are now added to the supplier selection grid instead
            lueSupplierCode.Properties.DataSource = supplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplierCode, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
            lueSupplierCode.Properties.Columns["AIMSupplierCode"].Caption = "AIM Supplier Code";
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
                    OwnerName = "--Select--",
                    OwnerCode = "--Select--"        //task 1133 FG 7/21/2026
                }
            );
            lueOwner.Properties.DataSource = owner;
            XtraEditors.LoadLookUpEdit(ref lueOwner, "AIMOwnerID", "OwnerName");
            lueOwner.Properties.Columns["OwnerName"].Caption = "AIM Owner Name";                    

            //task 1133 FG 7/21/2026 - bind the Owner Code dropdown to the same list as the Name dropdown
            //task 1035 FG 8/7/2026 - owners 2-4 removed -- extra owners are now added to the owner selection grid instead
            lueOwnerCode.Properties.DataSource = owner;
            XtraEditors.LoadLookUpEdit(ref lueOwnerCode, "AIMOwnerID", "OwnerCode", "OwnerName");
            lueOwnerCode.Properties.Columns["OwnerCode"].Caption = "AIM Owner Code";

            if (owner.Count == 2)
            {
                lueOwner.EditValue = owner[1].AIMOwnerID;
            }
            else
            {
                lueOwner.EditValue = 0; // owner;       //11/14/24
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
                    SupplierLocationName = "--Select--",
                    AIMSupplierLocationCode = "--Select--"      //task 1133 FG 7/21/2026
                }
            );

            lueSuppLoc.Properties.DataSource = aimSupplierLocation;
            XtraEditors.LoadLookUpEdit(ref lueSuppLoc, "AIMSupplierLocationID", "SupplierLocationName");
            lueSuppLoc.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

            //task 1133 FG 7/21/2026 - bind the matching Supplier Location Code dropdown to the same list
            LookUpEdit lueSuppLocCode = _codeLueForNameLue[lueSuppLoc];
            lueSuppLocCode.Properties.DataSource = aimSupplierLocation;
            XtraEditors.LoadLookUpEdit(ref lueSuppLocCode, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
            lueSuppLocCode.Properties.Columns["AIMSupplierLocationCode"].Caption = "AIM Supplier Location Code";

            if (aimSupplierLocation.Count == 2)
            {
                //selecting value if there is only one
                lueSuppLoc.EditValue = aimSupplierLocation[1].AIMSupplierLocationID;
            }
            else
            {
                lueSuppLoc.EditValue = 0; // aimSupplierLocation;   11/14/24
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
                    LocationName = "--Select--",
                    LocationCode = "--Select--"     //task 1133 FG 7/21/2026
                }
            );

            lueOwnLoc.Properties.DataSource = aimOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueOwnLoc, "AIMOwnerLocationID", "LocationName");
            lueOwnLoc.Properties.Columns["LocationName"].Caption = "AIM Owner Location Name";

            //task 1133 FG 7/21/2026 - bind the matching Owner Location Code dropdown to the same list
            LookUpEdit lueOwnLocCode = _codeLueForNameLue[lueOwnLoc];
            lueOwnLocCode.Properties.DataSource = aimOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueOwnLocCode, "AIMOwnerLocationID", "LocationCode", "LocationName");
            lueOwnLocCode.Properties.Columns["LocationCode"].Caption = "AIM Owner Location Code";

            if (aimOwnerLocation.Count == 2)
            {
                //selecting value if there is only one
                lueOwnLoc.EditValue = aimOwnerLocation[1].AIMOwnerLocationID;
            }
            else
            {
                lueOwnLoc.EditValue = 0; // aimOwnerLocation; 11/14/24
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
                lueOwnerLocation.EditValue = 0; // _aimOwnerLocation; 11/14/24
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

                        //task 1035 FG 8/7/2026 - series are created dynamically (one per supplier/owner grid row), so the
                        //                        supplier is carried on the series Tag instead of being looked up by series position
                        ChartsSupplierOwnerPair seriesPair = ((Series)sampleHit.HitObject).Tag as ChartsSupplierOwnerPair;
                        if (seriesPair != null)
                        {
                            supplierID = seriesPair.SupplierID;
                            supplierLocationID = seriesPair.SupplierLocationID;
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
        



        //task 1035 FG 8/7/2026 - the old fixed-4-series legend helpers for Trend and Supplier Compare were removed.
        //                        Series (and their legend names) are now created dynamically, one per supplier/owner grid row.

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

            //Task1098 DB250912 deFromDate.Text = AIMGlobal.chartBegDate.ToString("M/dd/yyyy");
            deFromDate.Text = AIMGlobal.chartBegDate.ToString("d");         //Task1098 DB250912 
            if (deFromDate.Text == "1/01/0001")
			{
                //1/15/21
                //deFromDate.EditValue = "1/01/2015";
                deFromDate.EditValue = "1/01/" + DateTime.Now.Year.ToString();

            }
            //Task1098 DB250912 deToDate.Text = AIMGlobal.chartEndDate.ToString("M/dd/yyyy");
            deToDate.Text = AIMGlobal.chartEndDate.ToString("d");           //Task1098 DB250912 
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
        //task 1035 FG 8/7/2026 - the ckbSupp/ckbOwn checkbox handlers and the 2-4 dropdown handlers were removed.
        //                        Extra suppliers/owners are now added to the selection grids (Pick --> Add --> Grid).

        //7/14/21
        private void clearSuppliers()
        {
            lueSupplier.EditValue = 0;
            lueSupplierLocation.EditValue = 0;
            _supplierSelections.Clear();        //task 1035 FG 8/7/2026
        }

        //7/14/21
        private void clearOwners()
        {
            lueOwner.EditValue = 0;
            lueOwnerLocation.EditValue = 0;
            _ownerSelections.Clear();           //task 1035 FG 8/7/2026
        }

        private void showHideSuppliers(Boolean blnShow)
        {
            //task 1035 FG 8/7/2026 - the supplier area is now one picker row (Code/Name dropdowns), Add button and a grid
            //Cursor change 06/10/2026 task 1035 - removed external Remove button; delete is now inside the grid
            lblSupplier.Visible = blnShow;
            ShowHideSupplierColumnHeaders(blnShow);
            lueSupplier.Visible = blnShow;
            lueSupplierLocation.Visible = blnShow;
            btnSupplierAdd.Visible = blnShow;
            grdSupplierSel.Visible = blnShow;
        }

        //6/25/21
        private void showHideOwners(Boolean blnShow)
        {
            //task 1035 FG 8/7/2026 - the owner area is now one picker row (Code/Name dropdowns), Add button and a grid
            //Cursor change 06/10/2026 task 1035 - removed external Remove button; delete is now inside the grid
            lblOwner.Visible = blnShow;
            ShowHideOwnerColumnHeaders(blnShow);
            lueOwner.Visible = blnShow;
            lueOwnerLocation.Visible = blnShow;
            btnOwnerAdd.Visible = blnShow;
            grdOwnerSel.Visible = blnShow;
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
                
        //task 1035 FG 8/7/2026 - the ckbOwn checkbox handlers and the owner 2-4 dropdown handlers were removed.
        //                        Extra owners are now added to the owner selection grid (Pick --> Add --> Grid).

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
        private void lueSupplierLocation_Click(object sender, EventArgs e)
        {
            lueSupplierLocation.SelectAll();
        }
        private void lueSupplierLocation_Enter(object sender, EventArgs e)
        {
            lueSupplierLocation.SelectAll();
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

    //task 1035 FG 8/7/2026 - one row in the supplier selection grid on the Charts screen (Pick --> Add --> Grid)
    public class ChartsSupplierSelectionRow
    {
        public Int32 RowNumber { get; set; }
        public Int32 SupplierID { get; set; }
        public String SupplierCode { get; set; } = "";
        public String SupplierName { get; set; } = "";
        public Int32 SupplierLocationID { get; set; }
        public String SupplierLocationCode { get; set; } = "";
        public String SupplierLocationName { get; set; } = "";
    }

    //task 1035 FG 8/7/2026 - one row in the owner selection grid on the Charts screen (Pick --> Add --> Grid)
    public class ChartsOwnerSelectionRow
    {
        public Int32 RowNumber { get; set; }
        public Int32 OwnerID { get; set; }
        public String OwnerCode { get; set; } = "";
        public String OwnerName { get; set; } = "";
        public Int32 OwnerLocationID { get; set; }
        public String OwnerLocationCode { get; set; } = "";
        public String OwnerLocationName { get; set; } = "";
    }

    //task 1035 FG 8/7/2026 - one supplier/owner combination sent to the chart queries
    //                        (supplier grid row and owner grid row matched by row position)
    public class ChartsSupplierOwnerPair
    {
        public Int32 SupplierID { get; set; }
        public Int32 SupplierLocationID { get; set; }
        public Int32 OwnerID { get; set; }
        public Int32 OwnerLocationID { get; set; }
        public String DisplayName { get; set; } = "";
    }
}

