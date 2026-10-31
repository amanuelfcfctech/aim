using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFCAIM
{
    public partial class UserLoginLog : Form
    {
        public UserLoginLog()
        {
            InitializeComponent();
        }

        private void UserLoginLog_Load(object sender, EventArgs e)
        {
            DataBindUserLog();
        }

        private void DataBindUserLog()
        {
            List<CFCUserLoginLogInfo> userLogin = new List<CFCUserLoginLogInfo>();
            userLogin = AIMManager.GetCFCUsersLoginLog();
            grdUsers.DataSource = userLogin;
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

        private void btnResize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }
    }
}
