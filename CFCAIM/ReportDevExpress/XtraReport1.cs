using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;

namespace CFCAIM.ReportDevExpress
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
               
        public XtraReport1()
        {
            InitializeComponent();
        }

        private void XtraReport1_DesignerLoaded(Object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {            
            AIMGlobal.SplashScreenHide();
        }
    }
}
