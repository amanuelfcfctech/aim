using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CFCAIM
{
	public partial class AnalysisProductSampleDetail : Form
	{
		public Boolean _isFormLoaded;
		CFCUser.Global _userGlobal = new CFCUser.Global();

		public AnalysisProductSampleDetail()
		{
			InitializeComponent();
		}

		private void AnalysisProductSampleDetail_Load(object sender, EventArgs e)
		{
			_isFormLoaded = false;
			DataBind();
			SetToolTips();
			_isFormLoaded = true;
		}

		public void DataBind()
		{
			try
			{
				_isFormLoaded = false;
				Int32 _currentRow = grdViewAnalyst.FocusedRowHandle;
				grdViewAnalyst.Columns.ColumnByName("colAIMOwnerLocationCode").Visible = false;
				grdViewAnalyst.Columns.ColumnByName("colNumberOfSample").Visible = false;
				grdViewAnalyst.Columns.ColumnByName("colDate").Visible = false;				
				grdAnalyst.DataSource = AIMGlobal.sampleDetailProduct;
				txtCount.Text = AIMGlobal.sampleDetailProduct.Count.ToString();
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

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

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		tpProdSpec sample;
		private void replnkSampleID_Click(object sender, EventArgs e)
		{
			try
			{

				AIMGlobal.SplashScreenShow();
				Int32 sampleID;
				Int32.TryParse(grdViewAnalyst.GetFocusedRowCellValue(colSampleID).ToString(), out sampleID);
				CFCAIMCore.Global.sampleID = sampleID;
				if (grdViewAnalyst.GetFocusedRowCellValue(colSampleCode) != null)
				{
					AIMGlobal.sampleCode = grdViewAnalyst.GetFocusedRowCellValue(colSampleCode).ToString();
					AIMGlobal.fromAnalysisScreen = true;
				}
				if (sample == null || sample.Visible == false)
				{
					sample = new tpProdSpec();
					sample.MdiParent = this.MdiParent;
					sample.Show();
				}
				else
				{
					sample.WindowState = FormWindowState.Maximized;
					sample.Activate();
				}

				AIMGlobal.SplashScreenHide();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void grdViewAnalyst_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
		{
			Double lowerMin = 0;
			Double lowerMax = 0;
			Double upperMin = 0;
			Double upperMax = 0;
			Boolean isValidOutlier = true;

			if (e.Column == colSampleValue)
			{
				isValidOutlier = Convert.ToBoolean(grdViewAnalyst.GetRowCellValue(e.RowHandle, colTrimmedIsValidOutlier));
				lowerMin = Convert.ToInt32(grdViewAnalyst.GetRowCellValue(e.RowHandle, colLMin));
				lowerMax = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colLMax));
				upperMin = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colUMin));
				upperMax = Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colUMax));

				if (Convert.ToDouble(grdViewAnalyst.GetRowCellValue(e.RowHandle, colSampleSize)) > 0)
				{
					if (isValidOutlier == true)
					{
						if (lowerMax + upperMax > 0)
						{
							e.Appearance.BackColor = Color.Red;
						}
						else if (lowerMin + upperMin > 0)
						{
							e.Appearance.BackColor = Color.Orange;
						}
					}
				}
			}
		}

		private void reprbIsIncludeModified_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SampleAIMRefCFCUserInfo sampleAIMRefCFCUser = new SampleAIMRefCFCUserInfo();
				Int32 sampleAIMID = (Int32)grdViewAnalyst.GetRowCellValue(grdViewAnalyst.FocusedRowHandle, colSampleAIMID);
				grdViewAnalyst.PostEditor();
				Boolean isInclude = (Boolean)grdViewAnalyst.GetRowCellValue(grdViewAnalyst.FocusedRowHandle, colIsInclude);

				if (isInclude == true)
				{
					AIMManager.DeleteSampleAIMRefCFCUser(sampleAIMID, SampleAIMRefCFCUserEnum.SampleAIMID_CFCUserID, _userGlobal.CFCLoginUser.CFCUserID);
				}
				else
				{
					sampleAIMRefCFCUser.SampleAIMRefCFCUserID = 0;
					sampleAIMRefCFCUser.SampleAIMID = sampleAIMID;
					sampleAIMRefCFCUser.IsUserFlag = true;
					sampleAIMRefCFCUser.CFCUserID = _userGlobal.CFCLoginUser.CFCUserID;
					AIMManager.SaveSampleAIMRefCFCUser(ref sampleAIMRefCFCUser);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Errror...", MessageBoxButtons.OK);
				throw;
			}
		}
	}
}
