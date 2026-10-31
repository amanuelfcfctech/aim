using CFCAIMCore.Manager;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace CFCAIM
{
    public partial class LanguageTest : Form
    {
        public LanguageTest()
        {
            InitializeComponent();
        }

        private void LanguageTest_Load(Object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CL");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-CL");

            DateTime dt = DateTime.Now;
            CultureInfo ci = new CultureInfo("es-CL");
            var strDate = "ddd MMM dd,yyyy"; //please use yours format
            MessageBox.Show(dt.ToString(strDate, ci));


            Int32 rowNum = 0;
            Int32.TryParse(grdTestView.FocusedRowHandle.ToString(), out rowNum);
            grdTest.DataSource = AIMManager.GetMathTypeSorted(true);
            grdTestView.FocusedRowHandle = rowNum;
        }

        private void radioButton1_CheckedChanged(Object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CL");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-CL");
            }            
            MessageBox.Show(LanguageResource.anotherText);            

            button1.Text = LanguageResource.button1Text;
            grdTestView.Columns["MathTypeID"].Caption = LanguageResource.colMathTypeID;
            grdTestView.Columns["MathTypeName"].Caption = LanguageResource.colMathTypeName;
            grdTestView.Columns["IsActive"].Caption = LanguageResource.colIsActive;

        }

        private void radioButton2_CheckedChanged(Object sender, EventArgs e)
        {

        }
    }
    
}
