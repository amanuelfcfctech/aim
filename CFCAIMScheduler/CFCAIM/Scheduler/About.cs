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
	public partial class About : Form
	{
		public About()
		{
			InitializeComponent();
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

		private void About_Load(object sender, EventArgs e)
		{


			string version = System.Reflection.Assembly.GetExecutingAssembly()
										   .GetName()
										   .Version
										   .ToString();

			string fileVersion = Application.ProductVersion;
			txtVersion.Text = version;
            SetToolTips();


            //VersionUpdater - command line tool??

            //AssemblyInfoUtil.exe -inc:4 AssemblyInfo.cs
            //AssemblyInfoUtil.exe -set:3.1.7.53 "C:\CFCTech13\Source\CFCAIM\Properties\AssemblyInfo.cs";

            //6/19/17
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

        }

        private void label10_Click(Object sender, EventArgs e)
        {

        }

        private void SetToolTips()
        {
            Icon = (Icon)Properties.Resources.CFCAIM;
            ToolTip ttip = new ToolTip();
            foreach (Control ctrl in pnlTopBtns.Controls)
            {
                ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
            }            
            ttip.SetToolTip(btnExit, "Exit");
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
        }

        private void btnResize_Click(Object sender, EventArgs e)
        {
            //6/19/17
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void About_SizeChanged(Object sender, EventArgs e)
        {
            //6/19/17
            btnResize.Visible = true;
        }
    }
}
