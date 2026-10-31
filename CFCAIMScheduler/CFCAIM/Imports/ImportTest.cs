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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using System.Collections;
using System.Collections.ObjectModel;


namespace CFCAIM.Imports
{
	public partial class ImportTest : Form
	{
		public ImportTest()
		{
			InitializeComponent();				
		}

		private void ImportTest_Load(object sender, EventArgs e)
		{			
			List<SampleInfo> _sample = new List<SampleInfo>();
			_sample = AIMManager.GetAllSample(true);
			gridControl1.DataSource = _sample;
		}

		private void advBandedGridView1_MasterRowEmpty(object sender, MasterRowEmptyEventArgs e)
		{
			SampleInfo s = new SampleInfo();
			SampleResultInfo sr = new SampleResultInfo();
			s = (SampleInfo)this.advBandedGridView1.GetRow(e.RowHandle);		

		}

		private void advBandedGridView1_MasterRowGetRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
		{
			e.RelationCount = 1;
		}

		private void advBandedGridView1_MasterRowGetRelationName(object sender, MasterRowGetRelationNameEventArgs e)
		{
			// remove this and an error pops
			//e.RelationName = "SampleResults";
			e.RelationName = "";
		}

		private void advBandedGridView1_MasterRowGetChildList(object sender, MasterRowGetChildListEventArgs e)
		{
			SampleInfo s = new SampleInfo();
			List<SampleResultInfo> sr = new List<SampleResultInfo>();
			s = (SampleInfo)advBandedGridView1.GetRow(e.RowHandle);
			sr = AIMManager.GetAllSampleResultForImport(s.SampleID);							
			e.ChildList = sr;
			
		}

		private void gridControl1_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
		{
			GridView view;
			view = e.View as GridView;

			//format visible columns
			view.Columns["ResultValue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			view.Columns["ResultValue"].DisplayFormat.FormatString = "n2";
			view.Columns["ResultValue"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			view.Columns["ResultValue"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			
			view.Columns["ResultValue"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);
			view.Columns["AnalyzerNutrientCode"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);
			view.Columns["AIMNutrientName"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);			
			view.Columns["Unit"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);

			view.Columns["ResultValue"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);
			view.Columns["AnalyzerNutrientCode"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);
			view.Columns["AIMNutrientName"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);
			view.Columns["Unit"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);

			view.Columns["ResultValue"].OptionsColumn.AllowEdit = false;
			view.Columns["AnalyzerNutrientCode"].OptionsColumn.AllowEdit = false;
			view.Columns["AIMNutrientName"].OptionsColumn.AllowEdit = false;
			view.Columns["Unit"].OptionsColumn.AllowEdit = false;			
			

			//Hide un-used columns
			view.Columns["SampleID"].Visible = false;		
			view.Columns["SampleResultID"].Visible = false;
			view.Columns["IsActive"].Visible = false;
			view.Columns["AnalysisDate"].Visible = false;
			view.Columns["AnalyzerNutrientID"].Visible = false;
			view.Columns["UpdateStamp"].Visible = false;
			view.Columns["IsCalculate"].Visible = false;
			view.Columns["AimDatabaseID"].Visible = false;
			view.Columns["MessageInside"].Visible = false;
			view.Columns["MessageOutside"].Visible = false;
			view.Columns["DataSourceTypeName"].Visible = false;
			view.Columns["DataSourceTypeID"].Visible = false;
			view.Columns["UnitType"].Visible = false;
			view.Columns["UserName"].Visible = false;
			view.Columns["CFCUserID"].Visible = false;

			
			
			
		}

		private void btnExpand_Click(object sender, EventArgs e)
		{
			Boolean blnExpand = false;
			GridView view = advBandedGridView1;
			if (btnExpand.Text == "Expand All")
			{
				blnExpand = true;
				btnExpand.Text = "Collapse All";
			}
			else
			{
				btnExpand.Text = "Expand All";
			}

			view.BeginUpdate();
			try
			{
				int dataRowCount = view.DataRowCount;
				dataRowCount = 100;   //hardcoded because test is using sample results -- too many!
				AIMGlobal.SplashScreenShow();
				for (int rHandle = 0; rHandle < dataRowCount; rHandle++)
					view.SetMasterRowExpanded(rHandle, blnExpand);
				AIMGlobal.SplashScreenHide();
			}
			finally
			{
				view.EndUpdate();			
			}

		}
			
	}
}
