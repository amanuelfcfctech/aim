using System.Drawing;



namespace CFCAIM.ReportDevExpress
{
    public partial class SampleLabelReport : DevExpress.XtraReports.UI.XtraReport
    {
        public SampleLabelReport()
        {
            InitializeComponent();            
        }

        //6/30/21
        private void SampleLabelReport_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Line1
            //Task1085 DB250723 if (AIMGlobal.line1Bold == true)
            if (AIMGlobal.line1Bold == true && AIMGlobal.line1FontSize > 0) //Task1085 DB250723 
            {
                //Task1058 MW250515  xrLabelCaption1.Font = new Font(xrLabelCaption1.Font.Name, xrLabelCaption1.Font.Size, FontStyle.Bold); 
                //Task1058 MW250515  xrLabelField1.Font = new Font(xrLabelField1.Font.Name, xrLabelField1.Font.Size, FontStyle.Bold);
                xrLabelCaption1.Font = new Font(xrLabelCaption1.Font.Name, AIMGlobal.line1FontSize, FontStyle.Bold);        //Task1058 MW250515
                xrLabelField1.Font = new Font(xrLabelField1.Font.Name, AIMGlobal.line1FontSize, FontStyle.Bold);            //Task1058 MW250515
            }
            // Task1085 DB250813 else 
            else if (AIMGlobal.line1FontSize > 0)  // Task1085 DB250813
            {
                xrLabelCaption1.Font = new Font(xrLabelCaption1.Font.Name, AIMGlobal.line1FontSize);        //Task1058 MW250515
                xrLabelField1.Font = new Font(xrLabelField1.Font.Name, AIMGlobal.line1FontSize);            //Task1058 MW250515
            }

            //Line2
            //Task1085 DB250723 if (AIMGlobal.line2Bold == true)
            if (AIMGlobal.line2Bold == true && AIMGlobal.line2FontSize > 0) //Task1085 DB250723 
            {
                //Task1058 MW250515  xrLabelCaption2.Font = new Font(xrLabelCaption2.Font.Name, xrLabelCaption2.Font.Size, FontStyle.Bold);
                //Task1058 MW250515  xrLabelField2.Font = new Font(xrLabelField2.Font.Name, xrLabelField2.Font.Size, FontStyle.Bold);

                xrLabelCaption2.Font = new Font(xrLabelCaption2.Font.Name, AIMGlobal.line2FontSize, FontStyle.Bold);        //Task1058 MW250515
                xrLabelField2.Font = new Font(xrLabelField2.Font.Name, AIMGlobal.line2FontSize, FontStyle.Bold);            //Task1058 MW250515
            }
            // Task1085 DB250813 else 
            else if (AIMGlobal.line2FontSize > 0)  // Task1085 DB250813
            {
                xrLabelCaption2.Font = new Font(xrLabelCaption2.Font.Name, AIMGlobal.line2FontSize);        //Task1058 MW250515
                xrLabelField2.Font = new Font(xrLabelField2.Font.Name, AIMGlobal.line2FontSize);            //Task1058 MW250515
            }
            //Line3
            //Task1085 DB250723 if (AIMGlobal.line3Bold == true)
            if (AIMGlobal.line2Bold == true && AIMGlobal.line2FontSize > 0) //Task1085 DB250723 
            {
                //Task1058 MW250515  xrLabelCaption3.Font = new Font(xrLabelCaption3.Font.Name, xrLabelCaption3.Font.Size, FontStyle.Bold); 
                //Task1058 MW250515  xrLabelField3.Font = new Font(xrLabelField3.Font.Name, xrLabelField3.Font.Size, FontStyle.Bold);
                xrLabelCaption3.Font = new Font(xrLabelCaption3.Font.Name, AIMGlobal.line3FontSize, FontStyle.Bold);        //Task1058 MW250515
                xrLabelField3.Font = new Font(xrLabelField3.Font.Name, AIMGlobal.line3FontSize, FontStyle.Bold);            //Task1058 MW250515
            }
            // Task1085 DB250813 else 
            else if (AIMGlobal.line3FontSize > 0)  // Task1085 DB250813
            {
                xrLabelCaption3.Font = new Font(xrLabelCaption3.Font.Name, AIMGlobal.line3FontSize);        //Task1058 MW250515
                xrLabelField3.Font = new Font(xrLabelField3.Font.Name, AIMGlobal.line3FontSize);            //Task1058 MW250515
            }

            //Line4
            //Task1085 DB250723 if (AIMGlobal.line4Bold == true)
            if (AIMGlobal.line4Bold == true && AIMGlobal.line4FontSize > 0) //Task1085 DB250723 
            {
                //Task1058 MW250515  xrLabelCaption4.Font = new Font(xrLabelCaption4.Font.Name, xrLabelCaption4.Font.Size, FontStyle.Bold); 
                //Task1058 MW250515  xrLabelField4.Font = new Font(xrLabelField4.Font.Name, xrLabelField4.Font.Size, FontStyle.Bold);
                xrLabelCaption4.Font = new Font(xrLabelCaption4.Font.Name, AIMGlobal.line4FontSize, FontStyle.Bold);        //Task1058 MW250515
                xrLabelField4.Font = new Font(xrLabelField4.Font.Name, AIMGlobal.line4FontSize, FontStyle.Bold);            //Task1058 MW250515
            }
            // Task1085 DB250813 else 
            else if (AIMGlobal.line4FontSize > 0)  // Task1085 DB250813
            {
                xrLabelCaption4.Font = new Font(xrLabelCaption4.Font.Name, AIMGlobal.line4FontSize);        //Task1058 MW250515
                xrLabelField4.Font = new Font(xrLabelField4.Font.Name, AIMGlobal.line4FontSize);            //Task1058 MW250515
            }

            //Line5
            //Task1085 DB250723 if (AIMGlobal.line5Bold == true)
            if (AIMGlobal.line5Bold == true && AIMGlobal.line5FontSize > 0) //Task1085 DB250723 
            {
                //Task1058 MW250515  xrLabelCaption5.Font = new Font(xrLabelCaption5.Font.Name, xrLabelCaption5.Font.Size, FontStyle.Bold);
                //Task1058 MW250515  xrLabelField5.Font = new Font(xrLabelField5.Font.Name, xrLabelField5.Font.Size, FontStyle.Bold);
                xrLabelCaption5.Font = new Font(xrLabelCaption5.Font.Name, AIMGlobal.line5FontSize, FontStyle.Bold);        //Task1058 MW250515
                xrLabelField5.Font = new Font(xrLabelField5.Font.Name, AIMGlobal.line5FontSize, FontStyle.Bold);            //Task1058 MW250515
            }
            // Task1085 DB250813 else 
            else if (AIMGlobal.line5FontSize > 0)  // Task1085 DB250813
            {
                xrLabelCaption5.Font = new Font(xrLabelCaption5.Font.Name, AIMGlobal.line5FontSize);        //Task1058 MW250515
                xrLabelField5.Font = new Font(xrLabelField5.Font.Name, AIMGlobal.line5FontSize);            //Task1058 MW250515
            }
        }
    }
}
