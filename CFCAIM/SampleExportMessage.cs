using CFCAIMCore.Entity;
using System;
using System.Windows.Forms;

namespace CFCAIM
{
    public partial class SampleExportMessage : Form
	{

		public SampleExportMessage()
		{
			InitializeComponent();

        }

		
		private void SampleExportMessage_Leave(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAppend_Click(object sender, EventArgs e)
		{
			CFCAIM.AIMGlobal.exportFileVal = (Int32)EnumSampleExport.Append;
			Close();			
		}

		private void btnOverwrite_Click(object sender, EventArgs e)
		{
			CFCAIM.AIMGlobal.exportFileVal = (Int32)EnumSampleExport.Overwrite;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			CFCAIM.AIMGlobal.exportFileVal = (Int32)EnumSampleExport.Cancel;
			Close();
		}

    }
}
