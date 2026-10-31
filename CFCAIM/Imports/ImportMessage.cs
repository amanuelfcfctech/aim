using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFCAIM.Imports
{
	public partial class ImportMessage : Form
	{
		public ImportMessage()
		{
			InitializeComponent();
		}

		private void ImportMessage_Load(object sender, EventArgs e)
		{
			

			richMessage1.Text = CFCAIM.AIMGlobal.importMessage1;
			richMessage2.Text = CFCAIM.AIMGlobal.importMessage2;
			richMessage1.SelectionAlignment = HorizontalAlignment.Center;
			richMessage2.SelectionAlignment = HorizontalAlignment.Center;
			richMessage3.SelectionAlignment = HorizontalAlignment.Center;
			richMessage4.SelectionAlignment = HorizontalAlignment.Center;
			richMessage5.SelectionAlignment = HorizontalAlignment.Center;
					
		}

		private void ImportMessage_Leave(object sender, EventArgs e)
		{
			CFCAIM.AIMGlobal.importMessage1 = "";
			CFCAIM.AIMGlobal.importMessage2 = "";
			CFCAIM.AIMGlobal.importMessage3 = "";
			CFCAIM.AIMGlobal.importMessage4 = "";
			CFCAIM.AIMGlobal.importMessage5 = "";
		}
	}
}
