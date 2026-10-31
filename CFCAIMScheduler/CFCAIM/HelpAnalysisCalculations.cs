using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CFCAIM
{
	public partial class HelpAnalysisCalculations : Form
	{
		public HelpAnalysisCalculations()
		{
			InitializeComponent();
		}

		private void HelpAnalysisDescriptions_Load(object sender, EventArgs e)
		{
			this.Icon = (Icon)Properties.Resources.CFCAIM;
		}
	}
}
