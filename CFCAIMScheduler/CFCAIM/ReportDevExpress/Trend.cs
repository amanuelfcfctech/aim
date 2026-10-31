using System;
using DevExpress.XtraCharts;



namespace CFCAIM.ReportDevExpress
{
	public partial class Trend : DevExpress.XtraReports.UI.XtraReport
	{
		public Trend()
		{
			InitializeComponent();
		}


		private void AddIndicator(Series ser, FinancialIndicator kind)
		{
			/*
			// Get a collection of indicators.
			IndicatorCollection indicators =
				((StockSeriesView)ser.View).Indicators;

			// Clear it.
			indicators.Clear();

			// Add a new indicator to it.
			indicators.Add(CreateIndicator(kind, new DateTime(2007, 12, 27),
				new DateTime(2008, 1, 17), ValueLevel.High));
			 */
		}


		private void xrChart1_BoundDataChanged(object sender, EventArgs e)
		{
			

		}

		private void xrChart1_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e)
		{			
			//foreach (CrosshairElement element in e.CrosshairElements)
			//{
			//	{
			//		SeriesPoint currentPoint = element.SeriesPoint;

			//		if (currentPoint.Tag.GetType() == typeof(Series))
			//		{
			//			Series rowView = (Series)currentPoint.Tag;
			//			string s = "Result Value = ";
			//			//+ rowView["UnitPrice"].ToString() +
			//			//	"\r\nUnits in stock = " + rowView["UnitsInStock"].ToString() +
			//			//	"\r\nQuantity per unit = " + rowView["QuantityPerUnit"].ToString();

			//		//	if (currentPoint.Tag.GetType() == typeof(DataRowView))
			//		//{
			//			//DataRowView rowView = (DataRowView)currentPoint.Tag;
			//			//string s = "Unit price = " + rowView["UnitPrice"].ToString() +
			//			//	"\r\nUnits in stock = " + rowView["UnitsInStock"].ToString() +
			//			//	"\r\nQuantity per unit = " + rowView["QuantityPerUnit"].ToString();

			//			element.LabelElement.Text = s;

			//		}
			//	}
			//}

		}

		private void xrChart1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
		{
			//// this doesnt error off, but it also doesn't show up when hovering/clicking dot
			//String val = e.SeriesPoint[0].ToString();
			//Int32 pointID = e.SeriesPoint.SeriesPointID;
			//e.SeriesPoint.ToolTipHint = val;

			/*
			foreach (Series element in e.Series)
			{
				{
					SeriesPoint currentPoint = element.SeriesPoint;

					if (currentPoint.Tag.GetType() == typeof(Series))
					{
						Series rowView = (Series)currentPoint.Tag;
						string s = "Result Value = ";
						//+ rowView["UnitPrice"].ToString() +
						//	"\r\nUnits in stock = " + rowView["UnitsInStock"].ToString() +
						//	"\r\nQuantity per unit = " + rowView["QuantityPerUnit"].ToString();

						//	if (currentPoint.Tag.GetType() == typeof(DataRowView))
						//{
						//DataRowView rowView = (DataRowView)currentPoint.Tag;
						//string s = "Unit price = " + rowView["UnitPrice"].ToString() +
						//	"\r\nUnits in stock = " + rowView["UnitsInStock"].ToString() +
						//	"\r\nQuantity per unit = " + rowView["QuantityPerUnit"].ToString();

						element.LabelElement.Text = s;

					}
				}
			}
			*/ 


		}

		private void TopMargin_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//9/10/20 - change to use AIMCoreGlobal
			//txtCompany.Text = AIMGlobal.CompanyName();
			txtCompany.Text = CFCAIMCore.Global.CompanyName();

            //Populate report header text fields
            xrMaterial.Text = AIMGlobal.aimMaterialName.ToString();
			xrNutrient.Text = AIMGlobal.aimNutrientName.ToString();
			xrDateFrom.Text = AIMGlobal.fromDate;
			xrDateTo.Text = AIMGlobal.toDate;
			xrSampleFrom.Text = AIMGlobal.sampleIDFrom.ToString();
			xrSampleTo.Text = AIMGlobal.sampleIDTo.ToString();
			if (AIMGlobal.fromDate == "" && AIMGlobal.toDate == "")
			{
				lblDateRange.Visible = false;
				lblDateThru.Visible = false;
				xrDateFrom.Visible = false;
				xrDateTo.Visible = false;			
			}
			if (AIMGlobal.sampleIDFrom == "" && AIMGlobal.sampleIDTo == "")
			{
				lblSampleRange.Visible = false;
				lblSampleThru.Visible = false;
				xrSampleFrom.Visible = false;
				xrSampleTo.Visible = false;
			}

			//Set X-axis intervals (sample-number interval set to 25 here)
			((DevExpress.XtraCharts.AxisXBase)(((DevExpress.XtraCharts.XYDiagram)(xrChart1.Diagram)).AxisX)).NumericScaleOptions.GridSpacing = 25;
			
		}


	}
}
