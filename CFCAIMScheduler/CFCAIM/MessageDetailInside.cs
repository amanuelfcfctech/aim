using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;

namespace CFCAIM
{
	public partial class MessageDetailInside : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();	

		public MessageDetailInside()
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

		private void MessageDetailInside_Load(object sender, EventArgs e)
		{
			DataBindGrid();
			SetToolTips();
		}

		public void DataBindGrid()
		{
			List<SampleMessageDetailInfo> messageDetail = new List<SampleMessageDetailInfo>();			
			if (AIMGlobal.sampleResultPredictID == 0)
			{
				//9/9/20 - change to use AIMCoreGlobal
				//messageDetail = AIMManager.GetSampleMessageDetailInsideAnalyzerNutrientID(AIMGlobal.sampleID, AIMGlobal.analyzerNutrientID, (Int32)SampleMessageType.Detail, AIMGlobal.sampleResultsIsActive);
				messageDetail = AIMManager.GetSampleMessageDetailInsideAnalyzerNutrientID(CFCAIMCore.Global.sampleID, AIMGlobal.analyzerNutrientID, (Int32)SampleMessageType.Detail, AIMGlobal.sampleResultsIsActive);
            }
			else
			{
				//9/9/20 - change to use AIMCoreGlobal
				//messageDetail = AIMManager.GetSampleMessageDetailInside(AIMGlobal.sampleID, AIMGlobal.sampleResultPredictID, (Int32)SampleMessageType.Detail);
				messageDetail = AIMManager.GetSampleMessageDetailInside(CFCAIMCore.Global.sampleID, AIMGlobal.sampleResultPredictID, (Int32)SampleMessageType.Detail);
            }			
			Int32 currentRowNum = 0;
			Int32.TryParse(this.grdMessageView.FocusedRowHandle.ToString(), out currentRowNum);
			grdMessage.DataSource = messageDetail;
			grdMessageView.FocusedRowHandle = currentRowNum;

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
