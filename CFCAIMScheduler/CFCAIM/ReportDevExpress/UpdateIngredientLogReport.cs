using System;

namespace CFCAIM.ReportDevExpress
{
    public partial class UpdateIngredientLogReport : DevExpress.XtraReports.UI.XtraReport
    {
        public UpdateIngredientLogReport()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {

            xrCheck.Visible = false;
            if (Convert.ToDouble(GetCurrentColumnValue("Used")) != Convert.ToDouble(GetCurrentColumnValue("AfterNutrientValue")))
            {
                //((DetailBand)sender).BackColor = Color.Yellow;       --> couldn't get this to work so added label to report instead and hide or show
                xrCheck.Visible = true;
            }            
        }
    }
}
