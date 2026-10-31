//11/17/20 - added this form to CFCAIM (was in AIMCore.Global)
using CFCAIMCore;
using System;
using System.Windows.Forms;

namespace CFCAIM
{
    public partial class PDF : Form
    {
        public PDF()
        {
            InitializeComponent();
        }

        private void pdfViewer1_Load(object sender, System.EventArgs e)
        {
            pdfViewer1.LoadDocument(Global.PDFPathAndFileName);
        }

        private void btnExit_Click(object sender, System.EventArgs e)
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

        private void PDF_SizeChanged(object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void PDF_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }
    }
}
