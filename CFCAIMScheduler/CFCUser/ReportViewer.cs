using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCUser.Entity;
using CFCUser.Manager;
using Microsoft.Reporting.WinForms;

namespace CFCUser
{
    public partial class ReportViewer : Form
    {
        #region variables
        List<UserInfo> _User = new List<UserInfo>();
        #endregion

        #region form controls
        public List<UserInfo> UserList
        {
            get
            {
                return _User;
            }

            set
            {
                _User = value;
            }
        }
        #endregion

        #region form controls
        public ReportViewer()
        {
            InitializeComponent();
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            List<UserInfo> User = new List<UserInfo>();
            User = UserManager.GetUser(true);
            Global global = new Global();
            String CompanyName = global.CompanyName;
            if (CompanyName == "")
            {
                CompanyName = "TRIAL VERSION";
            }
            rtpViewer.Visible = true;
            rtpViewer.LocalReport.DataSources.Clear();
            rtpViewer.LocalReport.ReportPath = @"Reports\UserList.rdlc";
            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("CompanyName", CompanyName, true));
            rtpViewer.LocalReport.SetParameters(paramList);
            rtpViewer.LocalReport.DataSources.Add(new ReportDataSource("dsReport", User));
            rtpViewer.RefreshReport();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void ReportViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ReportViewer.ActiveForm.Hide();
            //UserListing userListing = new UserListing();
            ////userListing.Opacity = 100;
            //UserListing.ActiveForm.Opacity = 100;
        }
    }
}
