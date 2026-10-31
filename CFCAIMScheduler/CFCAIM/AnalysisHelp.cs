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
	public partial class AnalysisHelp : Form
	{
		public AnalysisHelp()
		{
			InitializeComponent();
		}

		private void AnalysisHelp_Leave(object sender, EventArgs e)
		{		
			Close();		
		}
	
	}
}
