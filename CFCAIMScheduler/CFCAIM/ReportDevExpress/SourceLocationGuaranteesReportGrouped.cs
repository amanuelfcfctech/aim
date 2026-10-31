using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CFCAIM.ReportDevExpress
{
    public partial class SourceLocationGuaranteesReportGrouped : DevExpress.XtraReports.UI.XtraReport
    {
        public SourceLocationGuaranteesReportGrouped()
        {
            InitializeComponent();
        }

        private void SourceLocationGuaranteesReportGrouped_BeforePrint(object sender, CancelEventArgs e)
        {
            string resultformat = string.Empty;

            //4/20/21
            if (AIMGlobal.sortNutrientsBy == "Code")
            {
                Detail.SortFields.Add(new GroupField("SortNutrientCode", XRColumnSortOrder.Ascending));
            }
            else
            {
                Detail.SortFields.Add(new GroupField("AimNutrientName", XRColumnSortOrder.Ascending));
            }

            //11/21/22 - set TextFormatString = system settings for number of decimals
            if (AIMGlobal.numberOfDecimals > 0)
            {
                for (int i = 0; i < AIMGlobal.numberOfDecimals; i++)
                {
                    resultformat = resultformat + "0";
                }
                xrResultValue.TextFormatString = "{0:0." + resultformat + "}";
            }
        }

        //1/19/23
        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {
            double result = 0;
            double adjAmt = 0;
            double min = 0;
            double max = 0;

            result = Convert.ToDouble(GetCurrentColumnValue("ResultValue"));
            adjAmt = Convert.ToDouble(GetCurrentColumnValue("AdjustedNutrientAmount"));
            min = Convert.ToDouble(GetCurrentColumnValue("Min"));
            max = Convert.ToDouble(GetCurrentColumnValue("Max"));

            if (AIMGlobal.checkOutOfRange == true)
            {              
                if (AIMGlobal.resultOrAdjustedValue == "A")
                {
                    if ((adjAmt < min) || (adjAmt > max && max != 0))
                    {
                        xrFlagAdjusted.Visible = true;          //flag for result is set in stp; flag for adjusted is set here.
                    }                         //print line
                    else
                    {
                        e.Cancel = true;        //don't print line
                    }
                }
                else
                {
                    if ((result < min) || (result > max && max != 0))
                    { }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            else
            {
                //flag for result is set in stp; flag for adjusted is set here.
                if ((adjAmt < min) || (adjAmt > max && max != 0))
                {
                    xrFlagAdjusted.Visible = true;
                }
                else
                {
                    xrFlagAdjusted.Visible = false;
                }
            }
        }

        //1/19/23
        private void groupHeaderBand4_BeforePrint(object sender, CancelEventArgs e)
        {
            double result = 0;
            double adjAmt = 0;
            double min = 0;
            double max = 0;

            result = Convert.ToDouble(GetCurrentColumnValue("ResultValue"));
            adjAmt = Convert.ToDouble(GetCurrentColumnValue("AdjustedNutrientAmount"));
            min = Convert.ToDouble(GetCurrentColumnValue("Min"));
            max = Convert.ToDouble(GetCurrentColumnValue("Max"));

            if (AIMGlobal.checkOutOfRange == true)
            {               
                if (AIMGlobal.resultOrAdjustedValue == "A")
                {
                    if ((adjAmt < min) || (adjAmt > max && max != 0))
                    {
                        xrFlagAdjusted.Visible = true;          //flag for result is set in stp; flag for adjusted is set here.
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    if ((result < min) || (result > max && max != 0))
                    { }
                    else
                    {
                        e.Cancel = true;
                    }
                }

            }
            else
            {
                //flag for result is set in stp; flag for adjusted is set here.
                if ((adjAmt < min) || (adjAmt > max && max != 0))
                {
                    xrFlagAdjusted.Visible = true;
                }
                else
                {
                    xrFlagAdjusted.Visible = false;
                }
            }
        }
    }
}
