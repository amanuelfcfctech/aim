using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;


namespace CFCAIM
{
	public partial class MessageDetail_Test : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();	
		public MessageDetail_Test()
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

		private void MessageDetail_Load(object sender, EventArgs e)
		{
			DataBindGrid();
			SetToolTips();
		}

		public void DataBindGrid()
		{
			List<SampleMessageDetailInfo> inside = new List<SampleMessageDetailInfo>();

            if (AIMGlobal.sampleResultPredictID == 0)           
            {
				//9/9/20 - change to use AIMCoreGlobal
				//inside = AIMManager.GetSampleMessageDetailInsideAnalyzerNutrientID(AIMGlobal.sampleID, AIMGlobal.analyzerNutrientID, (Int32)SampleMessageType.Detail, AIMGlobal.sampleResultsIsActive);
				inside = AIMManager.GetSampleMessageDetailInsideAnalyzerNutrientID(CFCAIMCore.Global.sampleID, AIMGlobal.analyzerNutrientID, (Int32)SampleMessageType.Detail, AIMGlobal.sampleResultsIsActive);
            }
			else
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMManager.GetSampleMessageDetailInside(AIMGlobal.sampleID, AIMGlobal.sampleResultPredictID, (Int32)SampleMessageType.Detail);
				inside = AIMManager.GetSampleMessageDetailInside(CFCAIMCore.Global.sampleID, AIMGlobal.sampleResultPredictID, (Int32)SampleMessageType.Detail);
            }
			Int32 currentRowNum = 0;
			Int32.TryParse(this.grdMessageInsideView.FocusedRowHandle.ToString(), out currentRowNum);
			grdMessageInside.DataSource = inside;
			grdMessageInsideView.FocusedRowHandle = currentRowNum;

			List<SampleMessageDetailInfo> outside = new List<SampleMessageDetailInfo>();
			if (AIMGlobal.sampleResultPredictID == 0)
			{
				//9/9/20 - change to use AIMCoreGlobal
				//outside = AIMManager.GetSampleMessageDetailOutsideAnalyzerNutrientID(AIMGlobal.sampleID, AIMGlobal.analyzerNutrientID, (Int32)SampleMessageType.Detail);
				outside = AIMManager.GetSampleMessageDetailOutsideAnalyzerNutrientID(CFCAIMCore.Global.sampleID, AIMGlobal.analyzerNutrientID, (Int32)SampleMessageType.Detail);
            }
			else
			{
				//9/9/20 - change to use AIMCoreGlobal
				//outside = AIMManager.GetSampleMessageDetailOutside(AIMGlobal.sampleID, AIMGlobal.sampleResultPredictID, (Int32)SampleMessageType.Detail);
				outside = AIMManager.GetSampleMessageDetailOutside(CFCAIMCore.Global.sampleID, AIMGlobal.sampleResultPredictID, (Int32)SampleMessageType.Detail);
            }
			
			Int32.TryParse(this.grdMessageOutsideView.FocusedRowHandle.ToString(), out currentRowNum);
			grdMessageOutside.DataSource = outside;
			grdMessageOutsideView.FocusedRowHandle = currentRowNum;



		}

		private void SetToolTips()
		{
			try
			{
				this.Icon = (Icon)Properties.Resources.CFCAIM;
				ToolTip ttip = new ToolTip();
				foreach (Control ctrl in pnlTopBtns.Controls)
				{
					ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
