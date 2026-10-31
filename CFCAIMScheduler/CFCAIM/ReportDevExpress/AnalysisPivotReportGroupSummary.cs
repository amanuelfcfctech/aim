using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CFCAIM.ReportDevExpress
{
    public partial class AnalysisPivotReportGroupSummary : DevExpress.XtraReports.UI.XtraReport
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		static ArrayList medianList = new ArrayList();
		Int32 medianRow = 0;
		Int32 medianColumn = 0;
		Int32 lastRowIndex = 0;
		Boolean isGroup = true;

		public AnalysisPivotReportGroupSummary()
		{

			// ******* This report is Version 3 *******

			InitializeComponent();

            //4/28/21
            if (AIMGlobal.sortNutrientsBy == "Code")
            {
                pivotGridField1.Visible = true;
                pivotGridField1.AreaIndex = 0;			//nutrient code
                xrPivotGridField78.AreaIndex = 1;       //nutrient name
                xrPivotGridField79.AreaIndex = 2;       //unit size	
            }


            ////if no sub-totals are selected:
            //if (AIMGlobal.showMeanGroup == false)
            //         {
            //	///xrPivotGridField47.Visible = true;	//owner-location

            //	///xrPivotGridField47.AreaIndex = 3;   //owner-location
            //	xrPivotGridField13.AreaIndex = 1;   //supplier
            //	xrPivotGridField14.AreaIndex = 2;   //supplier location

            //	xrPivotGridField47.Width = 100;     //owner-location	
            //	xrPivotGridField13.Width = 100;     //supplier
            //	xrPivotGridField14.Width = 130;     //supplier location

            //}
            //else
            //{
            //	xrPivotGridField47.Visible = false; //owner-location

            //	xrPivotGridField13.AreaIndex = 1;   //supplier
            //	xrPivotGridField14.AreaIndex = 2;   //supplier location

            //	xrPivotGridField13.Width = 120;     //supplier
            //	xrPivotGridField14.Width = 130;     //supplier location
            //}

            //         xrPivotGridField18.Options.ShowValues = false;
            //         xrPivotGridField19.Options.ShowValues = false;
            //         xrPivotGridField22.Options.ShowValues = false;
            //         xrPivotGridField23.Options.ShowValues = false;
            //         xrPivotGridField24.Options.ShowValues = false;
            //         xrPivotGridField25.Options.ShowValues = false;
            //         xrPivotGridField42.Options.ShowValues = false;
            //         xrPivotGridField43.Options.ShowValues = false;
            //         xrPivotGridField26.Options.ShowValues = false;
            //         xrPivotGridField41.Options.ShowValues = false;

            //xrPivotGridField18.Options.ShowCustomTotals = false;
            //xrPivotGridField19.Options.ShowCustomTotals = false;
            //xrPivotGridField22.Options.ShowCustomTotals = false;
            //xrPivotGridField23.Options.ShowCustomTotals = false;
            //xrPivotGridField24.Options.ShowCustomTotals = false;
            //xrPivotGridField25.Options.ShowCustomTotals = false;
            //xrPivotGridField42.Options.ShowCustomTotals = false;
            //xrPivotGridField43.Options.ShowCustomTotals = false;
            //xrPivotGridField26.Options.ShowCustomTotals = false;
            //xrPivotGridField41.Options.ShowCustomTotals = false;

        }

		private void xrPivotGrid1_CustomCellValue(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCellValueEventArgs e)
		{
			Double value = 0;

			if (e.RowField != null)
			{

				if (e.RowField.ToString() == "Material")
				{
					if (isGroup == true)
					{
						isGroup = false;
						medianRow = 0;
						ArrayList medianList = new ArrayList();
						lastRowIndex = 0;
					}

					if (e.Value != null)
					{
						Double.TryParse(e.Value.ToString(), out value);
					}

					if (e.RowIndex == 0)
					{
						medianColumn += 1;
						medianList.Add(value);

						if (medianColumn == ((DevExpress.XtraReports.UI.XRPivotGrid)(sender)).ColumnCount)
						{
							if (((DevExpress.XtraReports.UI.XRPivotGrid)(sender)).RowCount == 1)
							{
								//kick off group totals
							}
						}
					}

					else
					{
						if (e.RowIndex != lastRowIndex)
						{
							medianRow += 1;
							lastRowIndex = e.RowIndex;
						}
						medianList.Add(value);
					}

				}
			}

		}

		private Double GetMedianValues(Int32 column)
		{
			// Returns a list of summary values against which a Custom Total will be calculated.
			Double value = 0;
			Int32 columnCount = 0;
			ArrayList values = new ArrayList();

			// Iterates through summary data source records and copies summary values to an array.
			if (medianList.Count == 0)
			{
				return value;
			}

			else if (medianList.Count <= medianColumn)
			{
				Double.TryParse(medianList[column].ToString(), out value);
				return value;
			}
			else
			{
				columnCount = column;
				for (int row = 0; row <= medianRow; row++)
				{
					Double.TryParse(medianList[columnCount].ToString(), out value);
					values.Add(value);
					columnCount += medianColumn;
				}
				// Sorts summary values.
				values.Sort();

				// Returns the summary values array.
				return GetMedian(values);
			}
		}

		private Double GetMedian(ArrayList values)
		{
			Int32 count = 0;
			if (values == null)
			{
				return 0;
			}
			else
			{
				if ((values.Count % 2) == 0)
				{
					count = values.Count / 2;
					return ((Double)(values[count - 1]) +
						(Double)(values[count])) / 2;
				}
				else
				{
					return (Double)values[values.Count / 2];
				}
			}
		}


		private void AnalysisPivotReport_BeforePrint(object sender, CancelEventArgs e)
		{

			//Populate report header text fields
			xrDryMatter.Text = AIMGlobal.dryMatterText;
			txtMaterial.Text = AIMGlobal.analyzerMaterialName;
			txtCompany.Text = CFCAIMCore.Global.CompanyName();
			

			xrDateFrom.Text = AIMGlobal.fromDate;
			xrDateTo.Text = AIMGlobal.toDate;
			if (AIMGlobal.showDate == false)
			{
				xrDateFrom.Visible = false;
				xrDateTo.Visible = false;
			}

		}

		private void xrPivotGrid1_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
		{

			if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
			{
				if (e.DisplayText == "Grand Total")
				{
					e.DisplayText = "Grand Totals";
				}
			}


		}

		private void xrPivotGrid1_CustomRowHeight(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomRowHeightEventArgs e)
		{
			if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
			{
				e.RowHeight = 25;
			}

			//1/18/21
			if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)
			{
				if (e.DataField.Caption == "Mean" && AIMGlobal.showMean == false) { e.RowHeight = 0; }
				if (e.DataField.Caption == "Median" && AIMGlobal.showMedian == false) { e.RowHeight = 0; }
				if (e.DataField.Caption == "StdDev" && AIMGlobal.showStdDev == false) { e.RowHeight = 0; }
				if (e.DataField.Caption == "CV %" && AIMGlobal.showCVpercent == false) { e.RowHeight = 0; }
				if (e.DataField.Caption == "Count" && AIMGlobal.showCount == false) { e.RowHeight = 0; }
				if (e.DataField.Caption == "Sum" && AIMGlobal.showSum == false) { e.RowHeight = 0; }
				if (e.DataField.Caption == "Mean-1/2SD" && AIMGlobal.showMeanMinus == false) { e.RowHeight = 0; }
				if (e.DataField.Caption == "Mean+1/2SD" && AIMGlobal.showMeanPlus == false) { e.RowHeight = 0; }
				if (e.DataField.Caption == "Min" && AIMGlobal.showMin == false) { e.RowHeight = 0; }
				if (e.DataField.Caption == "Max" && AIMGlobal.showMax == false) { e.RowHeight = 0; }
			}

            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Value)
            {
                if (e.DataField.Caption == "Mean" && AIMGlobal.showMeanGroup == false) { e.RowHeight = 0; }
                if (e.DataField.Caption == "Median" && AIMGlobal.showMedianGroup == false) { e.RowHeight = 0; }
                if (e.DataField.Caption == "StdDev" && AIMGlobal.showStdDevGroup == false) { e.RowHeight = 0; }
                if (e.DataField.Caption == "CV %" && AIMGlobal.showCVpercentGroup == false) { e.RowHeight = 0; }
                if (e.DataField.Caption == "Count" && AIMGlobal.showCountGroup == false) { e.RowHeight = 0; }
                if (e.DataField.Caption == "Sum" && AIMGlobal.showSumGroup == false) { e.RowHeight = 0; }
                if (e.DataField.Caption == "Mean-1/2SD" && AIMGlobal.showMeanMinusGroup == false) { e.RowHeight = 0; }
                if (e.DataField.Caption == "Mean+1/2SD" && AIMGlobal.showMeanPlusGroup == false) { e.RowHeight = 0; }
                if (e.DataField.Caption == "Min" && AIMGlobal.showMinGroup == false) { e.RowHeight = 0; }
                if (e.DataField.Caption == "Max" && AIMGlobal.showMaxGroup == false) { e.RowHeight = 0; }
            }
        }

		private const float CompareIsZeroTolerance = 0.000000000001f;
		private static double CoefficientOfVariance(double sd, double mean)
		{
			if (Math.Abs(mean - 0) < CompareIsZeroTolerance) return 0;
			return (sd / mean) * 100;
		}

		private void xrPivotGrid1_CustomCellDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCellDisplayTextEventArgs e)
		{

			if (e.DataField.Caption == "CV %" && e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total ||
				e.DataField.Caption == "CV %" && e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Value)
			{

				double stDev = Convert.ToDouble(e.GetCellValue(xrPivotGridField85));
				double mean = Convert.ToDouble(e.GetCellValue(xrPivotGridField81));
				double cv = CoefficientOfVariance(stDev, mean);

				if (cv == 0)
				{
					e.DisplayText = "";
				}
				else
				{
					e.DisplayText = string.Format("{0:f2}", cv);
				}


			}
						
			//-------------------------------------------------------------
			//1/15/21
			if (e.DataField.Caption == "Mean-1/2SD" && e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total ||
				e.DataField.Caption == "Mean-1/2SD" && e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Value)
			{
				double stDev = Convert.ToDouble(e.GetCellValue(xrPivotGridField85));
				double mean = Convert.ToDouble(e.GetCellValue(xrPivotGridField81));
				double calc = Convert.ToDouble(mean - (stDev * .5));

				if (calc == 0)
				{
					e.DisplayText = "";
				}
				else
				{
					e.DisplayText = string.Format("{0:f2}", calc);
				}

			}
			if (e.DataField.Caption == "Mean+1/2SD" && e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total ||
				e.DataField.Caption == "Mean+1/2SD" && e.RowValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Value)
			{
				double stDev = Convert.ToDouble(e.GetCellValue(xrPivotGridField85));
				double mean = Convert.ToDouble(e.GetCellValue(xrPivotGridField81));
				double calc = Convert.ToDouble(mean + (stDev * .5));
				if (calc == 0)
				{
					e.DisplayText = "";
				}
				else
				{
					e.DisplayText = string.Format("{0:f2}", calc);

				}
			}
			//-------------------------------------------------------------

		}


		private void xrPivotGrid1_BeforePrint(object sender, CancelEventArgs e)
		{
			medianList.Clear();
		}
    }
}
