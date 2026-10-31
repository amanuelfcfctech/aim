using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using CFCAIM.ReportDevExpress;
using Core.Library.DevExpress;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;




namespace CFCAIM
{
	public partial class TestIndexProb : Form
	{
		public TestIndexProb()
		{
			InitializeComponent();
		}

		private void TestIndexProb_Load(object sender, EventArgs e)
		{
			List<SampleInfo> sample = new List<SampleInfo>();
			sample = AIMManager.GetAllSample(true);

			sample.Insert
			(
				0,
				new SampleInfo
				{
					SampleID = 0,
					AlternateCode = "--Select--"
				}
			);
			Int32 currentSampleNo = 0;
			if (lueSampleNumber.EditValue != null && lueSampleNumber.Text == String.Empty)
			{			
				Int32.TryParse(lueSampleNumber.EditValue.ToString(), out currentSampleNo);
			}
			lueSampleNumber.Properties.DataSource = sample;
			XtraEditors.LoadLookUpEdit(ref lueSampleNumber, "SampleID", "SampleID");
			lueSampleNumber.Properties.Columns["SampleCode"].Visible = true;
			lueSampleNumber.EditValue = currentSampleNo;
			
		}

		private void btnLast_Click(object sender, EventArgs e)
		{
			List<SampleInfo> sample = new List<SampleInfo>();
			sample = AIMManager.GetAllSample(true);
			lueSampleNumber.ItemIndex = sample.Count;
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			Int32 currentSampleIndex = 0;
			Int32.TryParse(lueSampleNumber.ItemIndex.ToString(), out currentSampleIndex);			 
			lueSampleNumber.ItemIndex = currentSampleIndex + 1;
		}

		private void btnPrevious_Click(object sender, EventArgs e)
		{
			Int32 currentSampleIndex = 0;
			Int32.TryParse(lueSampleNumber.ItemIndex.ToString(), out currentSampleIndex);
			lueSampleNumber.ItemIndex = currentSampleIndex - 1;
		}

		private void btnFirst_Click(object sender, EventArgs e)
		{
			lueSampleNumber.ItemIndex = 0;
		}
	}
}
