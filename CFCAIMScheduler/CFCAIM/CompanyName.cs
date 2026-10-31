using CFCAIMCore.Entity;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CFCAIM
{
    public partial class CompanyName : Form
    {

        SystemSettings systemSettings;

        public CompanyName()
        {
            InitializeComponent();
        }

        private void CompanyName_Load(object sender, EventArgs e)
        {

            btnSave.Enabled = false;
            txtCompanyName.Text = CFCAIMCore.Global.CompanyName();
            SetToolTips();
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
                AIMGlobal.open_form(systemSettings = new SystemSettings());
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

        private void CompanyName_SizeChanged(object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }
               

        private void SetToolTips()
        {
            this.Icon = (Icon)Properties.Resources.CFCAIM;
            ToolTip ttip = new ToolTip();
            ttip.SetToolTip(btnSave, "Save");            
            ttip.SetToolTip(btnExit, "Exit");            
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtCompanyName.Text.Trim().Length == 0)
            {
                MessageBox.Show ("Please enter a Company Name.", "Warning...");
                txtCompanyName.Focus();
                return;
            }

            if (txtCompanyName.Text == CFCAIMCore.Global.CompanyName())
            {
                MessageBox.Show("Company Name has not been changed.", "Warning...");
                txtCompanyName.Focus();
                return;
            }

            DialogResult dr = new DialogResult();
            dr = MessageBox.Show("Changing the Company Name will require a new license (you will have 30 days to renew the license). Continue saving the new name?", "Warning...", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                LicensingConfigInfo config = new LicensingConfigInfo();
                config = CFCAIMCore.Manager.AIMManager.SaveCompanyName(1, txtCompanyName.Text);
                MessageBox.Show("Company Name has been changed.", "Information...");
            }




        }

        private void txtCompanyName_Enter(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            txtCompanyName.Text = CFCAIMCore.Global.CompanyName();
        }
    }
}
