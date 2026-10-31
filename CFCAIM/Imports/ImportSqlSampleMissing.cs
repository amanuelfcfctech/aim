using System;
using System.Windows.Forms;
using CFCAIMCore.Manager;

namespace CFCAIM.Imports
{
	public partial class ImportSqlSampleMissing : Form
	{
		public ImportSqlSampleMissing()
		{
			InitializeComponent();
		}

		private void ImportSqlSampleMissing_Load(Object sender, EventArgs e)
		{
			DataBindGrid();
			WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
		}

		private void DataBindGrid()
		{	  
			grdMissingSamples.DataSource = AIMManager.GetImportSQLSampleMissing();
			grdMissingSamplesView.FocusedRowHandle = 0;
		}

		private void btnExit_Click(Object sender, EventArgs e)
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

		private void btnResize_Click(Object sender, EventArgs e)
		{
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}

		private void ImportSqlSampleMissing_SizeChanged(Object sender, EventArgs e)
		{
			btnResize.Visible = true;
		}

		private void btnRefresh_Click(Object sender, EventArgs e)
		{
			DataBindGrid();
		}
	}
}
