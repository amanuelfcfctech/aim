using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFCFormsControlLibrary
{
    public partial class ExportBatchStatusButton : UserControl
    {
        public ExportBatchStatusButton()
        {
            InitializeComponent();
        }

        //private int _StatusNumber;
        /// <summary>
        /// It can be either 0,1 or 2
        /// </summary>
        /// <value>0</value>
        public int StatusNumber;
        //{
        //    get
        //    {
        //        return _StatusNumber;
        //    }
        //    set
        //    {

        //    }
        //}

        private void btnGreen_Click(object sender, EventArgs e)
        {
            this.StatusNumber = 2;
            ProcessStatus();
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            this.StatusNumber = 1;
            ProcessStatus();
        }

        public void ProcessStatus()
        {
            switch (this.StatusNumber)
            {
                case 0:
                    btnGreen.BackColor = Color.Lime;
                    btnRed.BackColor = Color.Red;
                    break;
                case 1:
                    btnGreen.BackColor = Color.Gray;
                    btnRed.BackColor = Color.Red;
                    break;
                case 2:
                    btnGreen.BackColor = Color.Lime;
                    btnRed.BackColor = Color.Gray;
                    break;
                default:
                    break;
            }
        }
    }
}
