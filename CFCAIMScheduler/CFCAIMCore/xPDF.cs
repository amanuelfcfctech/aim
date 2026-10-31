using System;

using System.Windows.Forms;


//9/14/20
namespace CFCAIMCore
{
    public partial class xPDF : Form
    {
        public xPDF()
        {
            InitializeComponent();
        }
        
        private void pdfViewer1_Load(object sender, EventArgs e)
        {
            pdfViewer1.LoadDocument(Global.PDFPathAndFileName);        
        }
    }
}
