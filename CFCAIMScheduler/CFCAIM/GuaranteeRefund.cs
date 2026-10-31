using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Core.Library.DevExpress;
using CFCAIM.Report;
using System.Linq;

namespace CFCAIM
{
	public partial class GuaranteeRefund : Form
	{

		List<AIMMaterialInfo> _aimMaterial = new List<AIMMaterialInfo>();
		List<AIMSupplierInfo> _aimSupplier = new List<AIMSupplierInfo>();
		List<AIMSupplierLocationInfo> _aimSupplierLocation = new List<AIMSupplierLocationInfo>();
		AimSourceLocationGuaranteeReportInfo _supplierLocationGuaranteeParams = new AimSourceLocationGuaranteeReportInfo();
		List<AimSourceLocationGuaranteeReportInfo> _supplierGuaranteeReport = new List<AimSourceLocationGuaranteeReportInfo>();
		List<GuaranteeRefundHeaderInfo> _batch = new List<GuaranteeRefundHeaderInfo>();
		List<GuaranteeRefundDetailInfo> _sampleCode = new List<GuaranteeRefundDetailInfo>();
		CFCUser.Global userGlobal = new CFCUser.Global();

		public GuaranteeRefund()
		{
			InitializeComponent();
		}

		private void GuaranteeRefund_Load(Object sender, EventArgs e)
		{

			DataBindLue();
			//DataBindGrid();
			SetToolTips();
            WindowState = FormWindowState.Maximized;
        }



		private void DataBindGrid()
		{
			List<GuaranteeRefundDetailInfo> guarantee = new List<GuaranteeRefundDetailInfo>();

			Int32 batchID = 0;			
			if (txtBatch.Text.Length > 0)
			{
				Int32.TryParse(txtBatch.Text, out batchID);
			}			
            guarantee = AIMManager.GetGuaranteeRefundDetail(batchID, txtCode.Text);
            

            Int32 currentRowNum = 0;
			Int32.TryParse(grdGuaranteeRefundView.FocusedRowHandle.ToString(), out currentRowNum);
			grdGuarantee.DataSource = guarantee;
			grdGuaranteeRefundView.FocusedRowHandle = currentRowNum;

		}
		public void PopulateGrid()
		{
			try
			{
				//get max batchid and add 1
				Int32 batchID = 0;
				List<GuaranteeRefundHeaderInfo> batch = new List<GuaranteeRefundHeaderInfo>();
				batch = AIMManager.GetGuaranteeHeaderLastBatchID();
				if (batch != null && batch.Count != 0)
				{
					batchID = batch[0].BatchID + 1;
				}			   

				Int32 headerID = 0;
				ErrorInfo result = new ErrorInfo();		
				GuaranteeRefundHeaderInfo header = new GuaranteeRefundHeaderInfo();	  		
				for (int i = 0; i < _supplierGuaranteeReport.Count; i++)
				{
					header.GuaranteeRefundHeaderID = 0;
					header.BatchID = batchID;
                    header.AIMSupplierID = _supplierGuaranteeReport[i].AimSupplierID;
                    header.AIMSupplierLocationID = _supplierGuaranteeReport[i].AimSupplierLocationID;
                    header.AIMMaterialID = _supplierGuaranteeReport[i].AimMaterialID;
                    result = AIMManager.SaveGuaranteeRefundHeader(ref header);
					if (result.ErrorNumber != 0)
					{
						MessageBox.Show("Unable to generate Guarantee ID.", "Critical...", MessageBoxButtons.OK);
						Close();
						return;
					}

					txtBatch.Text = header.BatchID.ToString();
					txtMaterial.Text = header.AIMMaterialName;
					txtSupplier.Text = header.SourceName;
					txtSupplierLocation.Text = header.SourceLocationName;


					headerID = result.RecordID;  	 		
					GuaranteeRefundDetailInfo detail = new GuaranteeRefundDetailInfo();
					detail.GuaranteeRefundHeaderID = headerID;
					detail.GuaranteeRefundDetailID = 0;
					detail.SampleID = _supplierGuaranteeReport[i].SampleID;                                      
					detail.AIMNutrientID = _supplierGuaranteeReport[i].AimNutrientID;
                    detail.ResultValue = _supplierGuaranteeReport[i].ResultValue;
                    detail.GuaranteeMin = _supplierGuaranteeReport[i].Min;
					detail.GuaranteeMax = _supplierGuaranteeReport[i].Max;
                    detail.SampleCode = _supplierGuaranteeReport[i].SampleID.ToString();
					detail.LoadNumber = _supplierGuaranteeReport[i].LoadNumber;
					detail.SampleDate = _supplierGuaranteeReport[i].SampleDate;
                    detail.CalculatedValue = 0;
                    detail.Status = 0;
                    detail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					detail.UpdateStamp = DateTime.Now;
					result = AIMManager.SaveGuaranteeRefundDetail(ref detail);

				}
                //List<GuaranteeRefundHeaderInfo> grdResults = new List<GuaranteeRefundHeaderInfo>();			
                //grdGuaranteeRefund.DataSource = grdResults;                
                DataBindGrid();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
				
		public void DataBindLue()
		{
			_aimSupplier = AIMManager.GetSupplier(true);
			_aimMaterial = AIMManager.GetAIMMaterial(true);

			////Show only distinct batch#'s
			//List<GuaranteeRefundHeaderInfo> batchLue = new List<GuaranteeRefundHeaderInfo>();
			//_batch = AIMManager.GetGuaranteeRefundHeader();						
			//var distinctBatch = from c in _batch
			//					 group c by c.BatchID into uniqueIds
			//					 select uniqueIds.FirstOrDefault();
			//foreach (var item in distinctBatch)
			//{
			//	batchLue.Insert
			//	(
			//		0,
			//		new GuaranteeRefundHeaderInfo
			//		{
			//			BatchID = item.BatchID,
			//			SourceName = item.SourceName
			//		}
			//	);
			//}
			//lueBatch.Properties.DataSource = batchLue;			
			//XtraEditors.LoadLookUpEdit(ref lueBatch, "BatchID", "SourceName");
			//lueBatch.Properties.Columns[0].Caption = "Batch ID";
			//lueBatch.Properties.Columns[0].Visible = true;
			//lueBatch.Properties.Columns[1].Visible = false;

			//show sampleCodes
			//_sampleCode = AIMManager.GetGuaranteeRefundDetail();		

			_aimMaterial.Insert
			(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialCode = "--All--"
				}
			);
			_aimSupplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					SupplierName = "--All--"
				}
			);
			
			lueMaterial.Properties.DataSource = _aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");

			lueSupplier.Properties.DataSource = _aimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplier, "AIMSupplierID", "SupplierName");
					

		}

		private void DataBindSupplierLocation()
		{
			Int32 supplierLocationID = 0;
			Int32 supplierID = 0;
			Int32.TryParse(lueSupplier.EditValue.ToString(), out supplierID);
			_aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplierID);

			_aimSupplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,
					SupplierLocationName = "--All--"
				}
			);
			lueSupplierLocation.Properties.DataSource = _aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocation, "AIMSupplierLocationID", "SupplierLocationName");
			lueSupplierLocation.Properties.Columns["SupplierLocationName"].Visible = true;
			if (_aimSupplierLocation.Count == 2)
			{
				//selecting value if there is only one
				lueSupplierLocation.EditValue = _aimSupplierLocation[1].AIMSupplierLocationID;
			}
			else
			{
				lueSupplierLocation.EditValue = supplierLocationID;
			}

		}
			
		private void SetToolTips()
		{
			try
			{
				this.Icon = Properties.Resources.CFCAIM;
				ToolTip ttip = new ToolTip();
				foreach (Control ctrl in pnlTopBtns.Controls)
				{
					ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
				}
				ttip.SetToolTip(btnReportPreview, "Preview Report");
                ttip.SetToolTip(btnPreviewGrid, "Preview Grid Data");
                ttip.SetToolTip(btnSearchBatch, "Search");
				ttip.SetToolTip(btnAcceptAll, "Claim All");
				ttip.SetToolTip(btnCancelAll, "Ignore All");
                ttip.SetToolTip(btnSearchBatch, "Search For Specific Batch Number or Sample Code");               
                ttip.SetToolTip(btnDeleteBatch, "Delete Batch");                
				ttip.SetToolTip(btnAdd, "Create Batch");

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ClearFields()
		{				
			deFromDate.Text = "";
			deToDate.Text = "";
			lueMaterial.EditValue = 0;
			lueSupplier.EditValue = 0;
			lueSupplierLocation.EditValue = 0;

			txtMaterial.Text = "";
			txtSupplier.Text = "";
			txtSupplierLocation.Text = "";
			
								
		}

        private Boolean setReportParams()
        {
            Boolean rtn = false;
			_supplierLocationGuaranteeParams = new AimSourceLocationGuaranteeReportInfo();

			if (deFromDate.Text == "")
            {
                deFromDate.EditValue = "1/1/2000";
            }
            if (deToDate.Text == "")
            {
                deToDate.EditValue = DateTime.Now;
            }
            _supplierLocationGuaranteeParams.ToDate = (DateTime)deToDate.DateTime;
            _supplierLocationGuaranteeParams.FromDate = (DateTime)deFromDate.DateTime;

            if ((Int32)lueMaterial.EditValue != 0)
            {
                _supplierLocationGuaranteeParams.AIMMaterialID = (Int32)lueMaterial.EditValue;
                rtn = true;
            }
            if ((Int32)lueSupplier.EditValue != 0)
            {
                _supplierLocationGuaranteeParams.AimSupplierID = (Int32)lueSupplier.EditValue;
                rtn = true;
            }
            if ((Int32)lueSupplierLocation.EditValue != 0)
            {
                _supplierLocationGuaranteeParams.AimSupplierLocationID = (Int32)lueSupplierLocation.EditValue;
                rtn = true;
            }
						
			_supplierLocationGuaranteeParams.Flag = 0;		
            if (chkOutOfRange.Checked == true)
            {				
				_supplierLocationGuaranteeParams.Flag = 1;			
			}

            return rtn;
        }

		private void btnReportPreview_Click(Object sender, EventArgs e)
		{
			Int32 batchID = 0;
			String sampleCode = "";
			Int32 status = 1;			
			Int32 flag = 1;
			

			//Make sure there are "claimed" records				
			//////ErrorInfo result = new ErrorInfo();
			//////AnalysisExportBatchMaintenance exportBatch = new AnalysisExportBatchMaintenance();
			//////exportBatch = AIMManager.GetUpdateIngredientHeader(AIMGlobal.exportbatchmaintenanceHeaderID);
			//////result = AIMManager.GetIngredientDetailStatusCount(ref exportBatch);
			//////if (result.ErrorNumber == (Int32)AcceptReject.Reject)
			//////{
			//////	MessageBox.Show("No items have been claimed.", "Information ...", MessageBoxButtons.OK);
			//////	return;
			//////}




			if (txtBatch.Text.Length > 0)
			{
				Int32.TryParse(txtBatch.Text, out batchID);
			}
			if (txtSampleCode.Text.Length > 0)
			{
				sampleCode = txtSampleCode.Text;
			}

			setReportParams();
			AIMGlobal.groupByLoadNumber = true;		 

			_supplierGuaranteeReport = AIMManager.GetSourceLocationGuaranteeReportData(batchID, sampleCode, status, flag);
		   		   

			//DialogResult dr = new DialogResult();           
			//dr = MessageBox.Show("Do you want to populate the grid?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			//if (dr == DialogResult.Yes)
			//{
			//    PopulateGrid();
			//}

			ReportFunctions.DataSetSourceLocationGuarantee = _supplierGuaranteeReport;
			AIMGlobal.SplashScreenHide();
			ReportFunctions.LoadReport((Int32)ReportEnum.SupplierLocationGuarantees);
			Show();
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

		private void btnRefresh_Click(Object sender, EventArgs e)
		{
			DataBindLue();
			SetToolTips();
			ClearFields();
		}

		private void lueSupplier_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				DataBindSupplierLocation();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnSearch_Click(Object sender, EventArgs e)
		{
            pnlNew.Visible = false;
            pnlSearch.Visible = true;
            pnlDelete.Visible = false;
			ClearGridandFields();

			
		}

        private void AcceptRejectAll(Int32 acceptReject)
        {
            Int32 batchID = 0;
            String sampleCode = "";
            if (txtBatchNo != null && txtSampleCode != null)
            {
                Int32.TryParse(txtBatchNo.Text.ToString(), out batchID);
                sampleCode = txtSampleCode.Text;
            }
            else if (txtBatchNo != null)
            {
                Int32.TryParse(txtBatchNo.Text, out batchID);
            }
            else if (txtSampleCode != null)
            {
                sampleCode = txtSampleCode.Text;
            }
            ErrorInfo hdr = new ErrorInfo();
            hdr = AIMManager.SaveGuaranteeRefundDetail(batchID, sampleCode, acceptReject);
            DataBindGrid();
        }
		private void btnAcceptAll_Click(Object sender, EventArgs e)
		{
            AcceptRejectAll((Int32)AcceptReject.Accept);           
		}

        private void btnCancelAll_Click(Object sender, EventArgs e)
        {
            AcceptRejectAll((Int32)AcceptReject.Reject);
        }

        private void btnApplyFilter_Click(Object sender, EventArgs e)
        {            
            Int32 batchID = 0;
            String sampleCode = "";
            if (txtBatchNo != null && txtSampleCode != null)
            {
                Int32.TryParse(txtBatchNo.Text.ToString(), out batchID);
                sampleCode = txtSampleCode.Text;
            }
            else if (txtBatchNo != null)
            {
                Int32.TryParse(txtBatchNo.Text, out batchID);
            }
            else if (txtSampleCode != null)
            {
                sampleCode = txtSampleCode.Text;
            }
            List<GuaranteeRefundDetailInfo> hdr = new List<GuaranteeRefundDetailInfo>();
            hdr = AIMManager.GetGuaranteeRefundDetail(batchID, sampleCode);

            if (hdr.Count > 0)
            {
                grdGuarantee.DataSource = hdr;
				txtMaterial.Text = hdr[0].AIMMaterialName;
				txtSupplier.Text = hdr[0].SourceName;
				txtSupplierLocation.Text = hdr[0].SourceLocationName;
			}

			
			

        }

        private void btnClearFilter_Click(Object sender, EventArgs e)
        {
            txtBatchNo.Text = "";
            txtSampleCode.Text = "";
			txtBatch.Text = "";
			grdGuarantee.Text = "";

            DataBindGrid();
        }

        private void btnPreviewGrid_Click(Object sender, EventArgs e)
        {
            try
            {
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdGuarantee, "Guarantee Reimbursements");
				CFCAIMCore.Global.previewGridResults(grdGuarantee, "Guarantee Reimbursements");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repbtnAccept_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //ValidateRow();
            ErrorInfo result = new ErrorInfo();
            result = AIMManager.SaveGuaranteeRefundDetail((Int32)grdGuaranteeRefundView.GetRowCellValue(grdGuaranteeRefundView.FocusedRowHandle, "GuaranteeRefundDetailID"), (Int32)AcceptReject.Accept);
            DataBindGrid();
            
        }

        private void repbtnReject_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //ValidateRow();
            ErrorInfo result = new ErrorInfo();
            result = AIMManager.SaveGuaranteeRefundDetail((Int32)grdGuaranteeRefundView.GetRowCellValue(grdGuaranteeRefundView.FocusedRowHandle, "GuaranteeRefundDetailID"), (Int32)AcceptReject.Reject);
            DataBindGrid();
        }

        private void ValidateRow()
        {
            GuaranteeRefundHeaderInfo guarantee = new GuaranteeRefundHeaderInfo();
            guarantee = (GuaranteeRefundHeaderInfo)grdGuaranteeRefundView.GetFocusedRow();
            guarantee.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            AIMManager.SaveGuaranteeRefundHeader(ref guarantee);
            DataBindGrid();
        }

        private void btnAdd_Click(Object sender, EventArgs e)
        {
            Boolean rtn = false;
            rtn = setReportParams();
            if (rtn == false)
            {
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("No parameters were selected, is this correct?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr != DialogResult.Yes)
                {
                    return;
                }
            }
            _supplierGuaranteeReport = AIMManager.GetSourceLocationGuaranteeReportData(ref _supplierLocationGuaranteeParams);            
            PopulateGrid();
			PopulateTextFields();

		}

		private void PopulateTextFields()
		{
			txtMaterial.Text = "All";
			txtSupplier.Text = "All";
			txtSupplierLocation.Text = "All";
			if ((Int32)lueMaterial.EditValue != 0 && lueMaterial.Text != "")
			{
				txtMaterial.Text = lueMaterial.Text;				
			}
			if ((Int32)lueSupplier.EditValue != 0 && lueSupplier.Text != "")
			{
				txtSupplier.Text = lueSupplier.Text;			
			}
			if ((Int32)lueSupplierLocation.EditValue != 0 && lueSupplierLocation.Text != "")
			{
				txtSupplierLocation.Text = lueSupplierLocation.Text;				
			}
		}
        private void btnDeleteBatch_Click(Object sender, EventArgs e)
        {

            if (txtDeleteBatchNo.Text == "")
            {
                return;
            }
            Int32 batchID = 0;
            DialogResult dr = new DialogResult();
            dr = MessageBox.Show("Do you want to delete Batch No " + txtDeleteBatchNo.Text + "?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr != DialogResult.Yes)                
            {
                txtDeleteBatchNo.Text = "";
                return;
                
            }                                           
            Int32.TryParse(txtDeleteBatchNo.Text.ToString(), out batchID);
            AIMManager.DeleteGuaranteeBatch(batchID);
            DataBindGrid();
            txtDeleteBatchNo.Text = "";
            MessageBox.Show("Batch Deleted");
		   	pnlDelete.Visible = false;
			

		}

        private void btnClearDelete_Click(Object sender, EventArgs e)
        {
            txtDeleteBatchNo.Text = "";
        }

		private void ClearGridandFields()
		{
			txtCode.Text = "";
			txtBatch.Text = "";
			txtMaterial.Text = "";
			txtSupplier.Text = "";
			txtSupplierLocation.Text = "";
			DataBindGrid();
		}

        private void btnNew_Click(Object sender, EventArgs e)
        {
            pnlNew.Visible = true;
            pnlSearch.Visible = false;
            pnlDelete.Visible = false;
			ClearGridandFields();

		}

        private void btnDelete_Click(Object sender, EventArgs e)
        {
            pnlNew.Visible = false;
            pnlSearch.Visible = false;
            pnlDelete.Visible = true;
			ClearGridandFields();
		}

		private void txtBatchNo_TextChanged(Object sender, EventArgs e)
		{
			txtBatch = txtBatchNo;
			
		}

		private void txtSampleCode_TextChanged(Object sender, EventArgs e)
		{
			txtCode = txtSampleCode;
		}

		private void btnShowAll_Click(Object sender, EventArgs e)
		{
			List<GuaranteeRefundDetailInfo> hdr = new List<GuaranteeRefundDetailInfo>();
			hdr = AIMManager.GetGuaranteeRefund();

			if (hdr.Count > 0)
			{
				grdGuarantee.DataSource = hdr;
				txtMaterial.Text = "";
				txtSupplier.Text = "";
				txtSupplierLocation.Text = "";
			}
			
		}
	}
}
