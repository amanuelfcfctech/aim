using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid;
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
    public partial class UsersLoggedIn : Form
    {
        public UsersLoggedIn()
        {
            InitializeComponent();
        }              

        private void UsersLoggedIn_Load(Object sender, EventArgs e)
        {
            DataBind();
            SetToolTips();
            //WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void DataBind()
        {            
            grdUsers.DataSource = AIMManager.GetCFCUsersLoggedInStatus(true);
            
        }

        private void SetToolTips()
        {
            try
            {
                Icon ico = Properties.Resources.CFCAIM;
                this.Icon = ico;
                ToolTip ttip = new ToolTip();
                foreach (Control ctrl in pnlTopBtns.Controls)
                {
                    ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
                }                
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(Object sender, EventArgs e)
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

        private void UsersLoggedIn_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void repbtnRemove_Click(Object sender, EventArgs e)
        {
            try
            {


                CFCUserInfo user = new CFCUserInfo();
                user = (CFCUserInfo)grdUsersView.GetFocusedRow();               
                AIMManager.UpdateCFCUsersLoggedInStatus(user.CFCUserID, false);
                DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
