using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFCAIM.Sample
{
	public partial class xSampleFurtherAnalysisPending : Form
	{
        CFCUser.Global userGlobal = new CFCUser.Global();

        public xSampleFurtherAnalysisPending()
		{
			InitializeComponent();
		}

        private void SampleFurtherAnalysisPending_Load(object sender, EventArgs e)
        {
            SetToolTips();
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void btnResize_Click(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}

        private void SetToolTips()
        {
            try
            {
                this.Icon = Properties.Resources.CFCAIM;
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

        private void SampleFurtherAnalysisPending_SizeChanged(object sender, EventArgs e)
        {
            btnResize.Visible = true;
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
    }
}
