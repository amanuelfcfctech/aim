using System;
using System.Drawing;
using System.Windows.Forms;

namespace CFCAIM.Chart
{
    public partial class ChartsSampleList : Form
    {        
        public ChartsSampleList()
        {
            InitializeComponent();
        }


        private void grdSampleList_Load(object sender, EventArgs e)
        {
            try
            {               
                DataBind();               
                SetToolTips();
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataBind()
        {            
            Int32 _currentRow = grdSampleListView.FocusedRowHandle;
            grdSampleList.DataSource = AIMGlobal.chartSampleList;
        }


        tpProdSpec sample;
        private void replnkSampleID_Click(object sender, System.EventArgs e)
        {
            try
            {

                AIMGlobal.SplashScreenShow();
                Int32 sampleID;
                Int32.TryParse(grdSampleListView.GetFocusedRowCellValue(colSampleID).ToString(), out sampleID);

                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.sampleID = sampleID;
                CFCAIMCore.Global.sampleID = sampleID;

                if (sample == null || sample.Visible == false)
                {
                    sample = new tpProdSpec();
                    sample.MdiParent = this.MdiParent;

                    //10/6/21 - commented, not needed
                    //sample.WindowState = FormWindowState.Normal;                    

                    sample.Show();
                }
                else
                {
                    //10/6/21
                    //sample.WindowState = FormWindowState.Normal;                    
                    sample.WindowState = FormWindowState.Maximized;                   

                    sample.Activate();
                }

                AIMGlobal.SplashScreenHide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SetToolTips()
        {
            this.Icon = (Icon)Properties.Resources.CFCAIM;
            ToolTip ttip = new ToolTip();
            foreach (Control ctrl in pnlTopBtns.Controls)
            {
                ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
            }         
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");


        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void ChartsSampleList_SizeChanged(object sender, EventArgs e)
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
