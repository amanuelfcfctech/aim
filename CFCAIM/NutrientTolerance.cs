using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CFCAIM
{
    public partial class NutrientTolerance : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		Int32 _headerID = 0;
		List<NutrientToleranceHeaderInfo> _header = new List<NutrientToleranceHeaderInfo>();
		List<NutrientToleranceDetailInfo> _detail = new List<NutrientToleranceDetailInfo>();


		public NutrientTolerance()
		{
			InitializeComponent();
			CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
		}

        private void NutrientTolerance_Load(object sender, EventArgs e)
        {
            System.Drawing.Icon ico = Properties.Resources.CFCAIM;
            this.Icon = ico;
            DataBindToleranceLue();
            DataBindNutrientlue();
            SetToolTips();
            WindowState = FormWindowState.Maximized;
        }

        private void SetToolTips()
		{
			ToolTip ttip = new ToolTip();
			foreach (Control ctrl in pnlTopBtns.Controls)
			{
				ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
			}
            ttip.SetToolTip(btnAddHeader, "Add Template");
            ttip.SetToolTip(btnDelete, "Delete Template");
			ttip.SetToolTip(btnResize, "Back to Normal Screen Size");			
			ttip.SetToolTip(btnSaveHeader, "Save Template");
            ttip.SetToolTip(btnAddSpec, "Add Nutrient Spec to Template");
            ttip.SetToolTip(btnAddTarget, "Add Nutrient Target to Template");


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
		private void btnResize_Click(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}
        private void NutrientTolerance_SizeChanged(object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
		{
			pnlNew.Visible = true;
			pnlNew.Location = new Point(11, 58);
			pnlExisting.Visible = false;
			pnlExisting.Location = new Point(521, 143);            			
			tabControl1.Visible = false;
			txtTemplateNameNew.Text = "";
			txtTemplateNameNew.Focus();
           
        }

		private void DataBindToleranceLue()
		{
			_header = AIMManager.GetNutrientToleranceHeader();
			_header.Insert
				(
					0,
					new NutrientToleranceHeaderInfo
                    {
						NutrientToleranceHeaderID = 0,
						NutrientToleranceHeaderName = "--Select--"
					}
				);
			lueTemplateName.Properties.DataSource = _header;
			XtraEditors.LoadLookUpEdit(ref lueTemplateName, "NutrientToleranceHeaderID", "NutrientToleranceHeaderName");
			lueTemplateName.Properties.Columns[1].Caption = "Template Name";

            if (_header.Count == 2)
            {
                //selecting value if there is only one
                lueTemplateName.EditValue = _header[1].NutrientToleranceHeaderID;
            }
            else
            {
                lueTemplateName.EditValue = 0; // _header;  //11/14/24
            }

        }
        		

		private void btnAddHeader_Click(object sender, EventArgs e)
		{
            try
            {
                if (txtTemplateNameNew.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Set Name.");
                    txtTemplateNameNew.Focus();
                    return;
                }
               
                List<NutrientToleranceHeaderInfo> header = new List<NutrientToleranceHeaderInfo>();
                header = AIMManager.GetNutrientToleranceHeaderName(txtTemplateNameNew.Text);
                if (header == null) { }
                else if (header.Count > 0)
                {
                    MessageBox.Show("This Set Name already exists. ", "Warning...", MessageBoxButtons.OK);
                    txtTemplateNameNew.Focus();
                    txtTemplateNameNew.SelectAll();
                    return;
                }

                //add tolerance group
                NutrientToleranceHeaderInfo result = new NutrientToleranceHeaderInfo();
                result.NutrientToleranceHeaderID = 0;
                result.NutrientToleranceHeaderName = txtTemplateNameNew.Text;                               
                result.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                ErrorInfo outPut = AIMManager.SaveNutrientToleranceHeader(ref result);
                if (outPut.ErrorNumber != 0)
                {
                    MessageBox.Show("Unable to save the tolerance.", "Critical...", MessageBoxButtons.OK);
                    return;
                }
                NutrientToleranceDetailInfo clearResults = new NutrientToleranceDetailInfo();
                              

                _headerID = outPut.RecordID;				             

                //Populate nutrients grid                                   
                DataBindNutrientGrid(_headerID);

                //refresh groupname dropdown and pull up newly-added group                                       
                DataBindToleranceLue();               

                lueTemplateName.EditValue = _headerID;
                pnlExisting.Visible = true;
                pnlExisting.Location = new Point(11, 58);
                pnlNew.Visible = false;
                pnlNew.Location = new Point(521, 143);
                btnAddNew.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		private void btnSelectExisting_Click(object sender, EventArgs e)
		{
			pnlExisting.Visible = true;
			pnlExisting.Location = new Point(11, 106);
			pnlNew.Visible = false;
			pnlNew.Location = new Point(521, 143);
			
			if (_header.Count == 2)
			{
				lueTemplateName.EditValue = _header[1].NutrientToleranceHeaderID;				
			}
			else
			{
				lueTemplateName.EditValue = 0;
				lueTemplateName.ShowPopup();
			}
		}

        private void lueTemplateName_EditValueChanged(object sender, EventArgs e)
        {
            if ((Int32)lueTemplateName.EditValue != 0)
            {                
                tabControl1.Visible = true;

                _headerID = (Int32)lueTemplateName.EditValue;
               
                DataBindNutrientGrid(_headerID);
                DataBindNutrientGridTarget(_headerID);

            }
            else
            {            
                tabControl1.Visible = false;
            }
        }

       
        private void btnDelete_Click(object sender, EventArgs e)
        {

            pnlExisting.Visible = true;
            pnlExisting.Location = new Point(11, 58);
            pnlNew.Visible = false;
            pnlNew.Location = new Point(521, 143);

            if ((Int32)lueTemplateName.EditValue == 0)
            {
                MessageBox.Show("Please select Set Name to delete.");
                lueTemplateName.Focus();
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Delete Tolerance '" + lueTemplateName.Text + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                Int32 headerID = 0;
                headerID = (Int32)lueTemplateName.EditValue;

                AIMManager.DeleteNutrientToleranceDetail(headerID);
                AIMManager.DeleteNutrientToleranceHeader(headerID);
                AIMManager.DeleteNutrientToleranceDetail(headerID);
                AIMManager.DeleteNutrientToleranceDetailTarget(headerID);
                
                DataBindToleranceLue();
                txtTemplateNameNew.Text = String.Empty;

                tabControl1.Visible = false;
                

            }

        }

        private void btnEditName_Click(object sender, EventArgs e)
        {
            if ((Int32)lueTemplateName.EditValue == 0)
            {
                lueTemplateName.ShowPopup();
                return;
            }

            lueTemplateName.Visible = false;

            txtNewName.Size = new Size(279, 22);
            txtNewName.Location = new Point(78, 4);
            txtNewName.Visible = true;
            txtNewName.Text = lueTemplateName.Text;
            txtNewName.Focus();
            txtNewName.SelectAll();

            btnCancelEdit.Location = new Point(358, 3);
            btnCancelEdit.Visible = true;
                        
            btnSaveHeader.Visible = true;
                       
            btnAddNew.Enabled = false;
                        
            tabControl1.Enabled = false;
        }

        private void btnSaveHeader_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewName.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Set Name.");
                    txtNewName.Focus();
                    return;
                }

                List<NutrientToleranceHeaderInfo> header = new List<NutrientToleranceHeaderInfo>();
                header = AIMManager.GetNutrientToleranceHeaderName(txtNewName.Text);
                if (header == null) { }
                else if (header.Count > 0)
                {
                    MessageBox.Show("This Set Name already exists. ", "Warning...", MessageBoxButtons.OK);
                    txtNewName.Focus();
                    txtNewName.SelectAll();
                    return;
                }

                //update tolerance header
                NutrientToleranceHeaderInfo result = new NutrientToleranceHeaderInfo();
                result.NutrientToleranceHeaderID = (Int32)lueTemplateName.EditValue;
                result.NutrientToleranceHeaderName = txtNewName.Text;
                result.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                ErrorInfo outPut = AIMManager.SaveNutrientToleranceHeader(ref result);
                if (outPut.ErrorNumber != 0)
                {
                    MessageBox.Show("Unable to save changes to tolerance.", "Critical...", MessageBoxButtons.OK);
                    return;
                }

                //refresh groupname dropdown and pull up newly-added group                                       
                DataBindToleranceLue();
                lueTemplateName.EditValue = _headerID;

                //This will hide/show/move buttons where they need to be
                btnCancelEdit.PerformClick();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            lueTemplateName.Visible = true;

            txtNewName.Location = new Point(10, 72);
            txtNewName.Visible = false;
            txtNewName.Text = String.Empty;

            btnCancelEdit.Location = new Point(425, 26);
            btnCancelEdit.Visible = false;
                        
            btnSaveHeader.Visible = false;

            btnAddNew.Enabled = true;
                       
            tabControl1.Enabled = true;
        }

        private void SyncValueLue(ref LookUpEdit from, ref LookUpEdit to)
        {
            if (from == null || to == null)
            {
                return;
            }
            if (to.EditValue == from.EditValue)
            {
                return;
            }
            to.EditValue = from.EditValue;
        }
                
               

        private void lueTemplateName_Click(object sender, EventArgs e)
        {
            lueTemplateName.SelectAll();
        }

        private void lueTemplateName_Enter(object sender, EventArgs e)
        {
            lueTemplateName.SelectAll();
        }

        private void DataBindNutrientlue()
        {
            List<AIMNutrientInfo> aimNutrient = new List<AIMNutrientInfo>();
            aimNutrient = AIMManager.GetAllAIMNutrient(true);
            aimNutrient.Insert
            (
                0,
                new AIMNutrientInfo
                {
                    AIMNutrientID = 0,
                    AIMNutrientCode = "--Select--",
                    AIMNutrientName = "--Select--"
                }
            );
            lueAIMCodeNutrient.Properties.DataSource = aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueAIMCodeNutrient, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");

            lueAIMNameNutrient.Properties.DataSource = aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueAIMNameNutrient, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

            txtMin.Text = "0";
            txtMinPct.Text = "0";
            txtMax.Text = "0";
            txtMaxPct.Text = "0";

            //---------------------------------------------------
            //Target

            lueAimNutrientCodeTarget.Properties.DataSource = aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueAimNutrientCodeTarget, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");

            lueAimNutrientNameTarget.Properties.DataSource = aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueAimNutrientNameTarget, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

            txtMinTarget.Text = "0";
            txtMinPctTarget.Text = "0";
            txtMaxTarget.Text = "0";
            txtMaxPctTarget.Text = "0";



        }

        private void lueAIMCodeNutrient_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SyncValueLue(ref lueAIMCodeNutrient, ref lueAIMNameNutrient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueAIMNameNutrient_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SyncValueLue(ref lueAIMNameNutrient, ref lueAIMCodeNutrient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueAIMCodeNutrient_Click(object sender, EventArgs e)
        {
            lueAIMCodeNutrient.SelectAll();
        }

        private void lueAIMCodeNutrient_Enter(object sender, EventArgs e)
        {
            lueAIMCodeNutrient.SelectAll();
        }

        private void lueAIMNameNutrient_Click(object sender, EventArgs e)
        {
            lueAIMNameNutrient.SelectAll();
        }

        private void lueAIMNameNutrient_Enter(object sender, EventArgs e)
        {
            lueAIMNameNutrient.SelectAll();
        }

        private void btnAddNutrient_Click(object sender, EventArgs e)
        {
            try
            {
                String msg = String.Empty;

                if ((Int32)lueAIMCodeNutrient.EditValue == 0)
                {
                    MessageBox.Show("Please select AIM Nutrient.", "Warning...");
                    lueAIMCodeNutrient.ShowPopup();
                    return;
                }

                //---------------------------------------------
                //Check nutrient detail TARGET table - make sure nutrient hasn't already been added there
                List<NutrientToleranceDetailTargetInfo> target = new List<NutrientToleranceDetailTargetInfo>();
                target = AIMManager.GetNutrientToleranceDetailTarget(_headerID, (Int32)lueAIMCodeNutrient.EditValue);
                if (target.Count > 0)
                {
                    msg = "This Nutrient has been added to the Predicted Value list.";
                    msg += Environment.NewLine + Environment.NewLine + "Please remove it from that list or select another nutrient.";
                    MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);                                    
                    return;
                }

                //---------------------------------------------
                List<NutrientToleranceDetailInfo> aimNuts = new List<NutrientToleranceDetailInfo>();
                aimNuts = AIMManager.GetNutrientToleranceDetail(_headerID, (Int32)lueAIMCodeNutrient.EditValue);
                if (aimNuts.Count > 0)
                {
                    MessageBox.Show("This Nutrient has already been added.", "Warning...");
                    lueAIMCodeNutrient.ShowPopup();
                    return;
                }

                //---------------------------------------------                
                if (txtMin.Text == String.Empty)
                {
                    MessageBox.Show("Please enter Minimum Amount.", "Warning...");
                    txtMin.Focus();
                    return;
                }
                else if (txtMinPct.Text == String.Empty)
                {
                    MessageBox.Show("Please enter Minimum Percent.", "Warning...");
                    txtMinPct.Focus();
                    return;
                }

                //---------------------------------------------                
                else if (txtMax.Text == String.Empty)
                {
                    MessageBox.Show("Please enter Maximum Amount.", "Warning...");
                    txtMax.Focus();
                    return;
                }
                else if (txtMaxPct.Text == String.Empty)
                {
                    MessageBox.Show("Please enter Maximum Percent.", "Warning...");
                    txtMaxPct.Focus();
                    return;
                }

                //---------------------------------------------
                Double resultMin = 0;
                Double resultMinPct = 0;
                Double.TryParse(txtMin.Text, out resultMin);
                Double.TryParse(txtMinPct.Text, out resultMinPct);
                Double resultMax = 0;
                Double resultMaxPct = 0;
                Double.TryParse(txtMax.Text, out resultMax);
                Double.TryParse(txtMaxPct.Text, out resultMaxPct);

                //Save Nutrient to NutrientToleranceDetail table
                ErrorInfo nutrientResult = new ErrorInfo();
                NutrientToleranceDetailInfo detail = new NutrientToleranceDetailInfo();
                detail.NutrientToleranceDetailID = 0;
                detail.NutrientToleranceHeaderID = _headerID;
                detail.AIMNutrientID = (Int32)lueAIMCodeNutrient.EditValue;


                detail.MinAmount = resultMin;
                detail.MinPercent = resultMinPct;

                detail.MaxAmount = resultMax;
                detail.MaxPercent = resultMaxPct;

                detail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                nutrientResult = AIMManager.SaveNutrientToleranceDetail(ref detail);
                if (nutrientResult.ErrorNumber > 0)
                {
                    return;
                }

                //Databind Nutrients
                DataBindNutrientGrid(_headerID);

                lueAIMCodeNutrient.EditValue = 0;
                lueAIMCodeNutrient.Focus();

                txtMin.Text = "0";
                txtMinPct.Text = "0";
                txtMax.Text = "0";
                txtMaxPct.Text = "0";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataBindNutrientGrid(Int32 headerID)
        {
            Int32 rowNum = 0;
            Int32.TryParse(grdNutrientView.FocusedRowHandle.ToString(), out rowNum);
            grdNutrient.DataSource = AIMManager.GetNutrientToleranceDetail(headerID);
            grdNutrientView.FocusedRowHandle = rowNum;
        }

        private void DataBindNutrientGridTarget(Int32 headerID)
        {
            Int32 rowNum = 0;
            Int32.TryParse(grdNutrientTargetView.FocusedRowHandle.ToString(), out rowNum);
            grdNutrientTarget.DataSource = AIMManager.GetNutrientToleranceDetailTarget(headerID);
            grdNutrientTargetView.FocusedRowHandle = rowNum;
        }

        
        private void repbtnDelNutrient_Click(object sender, EventArgs e)
        {
            try
            {
                NutrientToleranceDetailInfo detail = new NutrientToleranceDetailInfo();
                detail = (NutrientToleranceDetailInfo)grdNutrientView.GetFocusedRow();
                detail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                //Delete Nutrient Tolerance Detail rec
                AIMManager.DeleteNutrientToleranceDetail(_headerID, detail.AIMNutrientID);
                DataBindNutrientGrid(_headerID);
                lueAIMCodeNutrient.EditValue = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

       
        private void txtMin_EditValueChanged(object sender, EventArgs e)
        {
            txtMinPct.Text = "0";
        }

        private void txtMinPct_EditValueChanged(object sender, EventArgs e)
        {
            txtMin.Text = "0";
        }

        private void txtMax_EditValueChanged(object sender, EventArgs e)
        {
            txtMaxPct.Text = "0";
        }

        private void txtMaxPct_EditValueChanged(object sender, EventArgs e)
        {
            txtMax.Text = "0";
        }

        private void grdNutrientView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            Double amtMin = 0;
            Double pctMin = 0;
            Double amtMax = 0;
            Double pctMax = 0;
            amtMin = Convert.ToDouble(gv.GetRowCellValue(e.RowHandle, gv.Columns["MinAmount"]));
            pctMin = Convert.ToDouble(gv.GetRowCellValue(e.RowHandle, gv.Columns["MinPercent"]));
            amtMax = Convert.ToDouble(gv.GetRowCellValue(e.RowHandle, gv.Columns["MaxAmount"]));
            pctMax = Convert.ToDouble(gv.GetRowCellValue(e.RowHandle, gv.Columns["MaxPercent"]));

            //------------------------------------------------------------------------------  MIN
            if (gv.FocusedColumn.FieldName == "MinAmount")
            {
                if (amtMin > 0 && pctMin > 0)
                {                    
                    gv.SetRowCellValue(e.RowHandle, gv.Columns["MinPercent"], 0);
                }                
            }
            if (gv.FocusedColumn.FieldName == "MinPercent")
            {
                if (amtMin > 0 && pctMin > 0)
                {
                    gv.SetRowCellValue(e.RowHandle, gv.Columns["MinAmount"], 0);
                }               
            }


            //------------------------------------------------------------------------------  MAX
            if (gv.FocusedColumn.FieldName == "MaxAmount")
            {
                if (amtMax > 0 && pctMax > 0)
                {
                    gv.SetRowCellValue(e.RowHandle, gv.Columns["MaxPercent"], 0);
                }                                
            }
            if (gv.FocusedColumn.FieldName == "MaxPercent")
            {
                if (amtMax > 0 && pctMax > 0)
                {
                    gv.SetRowCellValue(e.RowHandle, gv.Columns["MaxAmount"], 0);
                }                
            }
            
        }

        private void lueAimNutrientCodeTarget_Click(object sender, EventArgs e)
        {
            lueAimNutrientCodeTarget.SelectAll();
        }

        private void lueAimNutrientCodeTarget_Enter(object sender, EventArgs e)
        {
            lueAimNutrientCodeTarget.SelectAll();
        }

        private void lueAimNutrientNameTarget_Click(object sender, EventArgs e)
        {
            lueAimNutrientNameTarget.SelectAll();
        }

        private void lueAimNutrientNameTarget_Enter(object sender, EventArgs e)
        {
            lueAimNutrientNameTarget.SelectAll();
        }

        private void lueAimNutrientCodeTarget_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SyncValueLue(ref lueAimNutrientCodeTarget, ref lueAimNutrientNameTarget);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueAimNutrientNameTarget_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SyncValueLue(ref lueAimNutrientNameTarget, ref lueAimNutrientCodeTarget);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtMinTarget_EditValueChanged(object sender, EventArgs e)
        {
            txtMinPctTarget.Text = "0";
        }

        private void txtMinPctTarget_EditValueChanged(object sender, EventArgs e)
        {
            txtMinTarget.Text = "0";
        }

        private void txtMaxTarget_EditValueChanged(object sender, EventArgs e)
        {
            txtMaxPctTarget.Text = "0";
        }

        private void txtMaxPctTarget_EditValueChanged(object sender, EventArgs e)
        {
            txtMaxTarget.Text = "0";
        }

        private void btnAddTarget_Click(object sender, EventArgs e)
        {
            try
            {
                String msg = string.Empty;

                if ((Int32)lueAimNutrientCodeTarget.EditValue == 0)
                {
                    MessageBox.Show("Please select AIM Nutrient.", "Warning...");
                    lueAimNutrientCodeTarget.ShowPopup();
                    return;
                }

                //---------------------------------------------
                //Check nutrient-tolerance table - make sure nutrient hasn't already been added there
                List<NutrientToleranceDetailInfo> nutrients = new List<NutrientToleranceDetailInfo>();
                nutrients = AIMManager.GetNutrientToleranceDetail(_headerID, (Int32)lueAimNutrientCodeTarget.EditValue);
                if (nutrients.Count > 0)
                {
                    msg = "This Nutrient has been added to the Product Specs list.";
                    msg += Environment.NewLine + Environment.NewLine + "Please remove it from that list or select another nutrient.";
                    MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
                    return;
                }

                //---------------------------------------------
                List<NutrientToleranceDetailTargetInfo> aimNuts = new List<NutrientToleranceDetailTargetInfo>();
                aimNuts = AIMManager.GetNutrientToleranceDetailTarget(_headerID, (Int32)lueAimNutrientCodeTarget.EditValue);
                if (aimNuts.Count > 0)
                {
                    MessageBox.Show("This Nutrient has already been added.", "Warning...");
                    lueAimNutrientCodeTarget.ShowPopup();
                    return;
                }

                //---------------------------------------------                
                if (txtMinTarget.Text == String.Empty)
                {
                    MessageBox.Show("Please enter Minimum Amount.", "Warning...");
                    txtMinTarget.Focus();
                    return;
                }
                else if (txtMinPctTarget.Text == String.Empty)
                {
                    MessageBox.Show("Please enter Minimum Percent.", "Warning...");
                    txtMinPctTarget.Focus();
                    return;
                }

                //---------------------------------------------                
                else if (txtMaxTarget.Text == String.Empty)
                {
                    MessageBox.Show("Please enter Maximum Amount.", "Warning...");
                    txtMaxTarget.Focus();
                    return;
                }
                else if (txtMaxPctTarget.Text == String.Empty)
                {
                    MessageBox.Show("Please enter Maximum Percent.", "Warning...");
                    txtMaxPctTarget.Focus();
                    return;
                }

                //---------------------------------------------
                Double resultMin = 0;
                Double resultMinPct = 0;
                Double.TryParse(txtMinTarget.Text, out resultMin);
                Double.TryParse(txtMinPctTarget.Text, out resultMinPct);
                Double resultMax = 0;
                Double resultMaxPct = 0;
                Double.TryParse(txtMaxTarget.Text, out resultMax);
                Double.TryParse(txtMaxPctTarget.Text, out resultMaxPct);

                //Save Nutrient to Target table
                ErrorInfo nutrientResult = new ErrorInfo();
                NutrientToleranceDetailTargetInfo target = new NutrientToleranceDetailTargetInfo();
                target.NutrientToleranceDetailTargetID = 0;
                target.NutrientToleranceHeaderID = _headerID;
                target.AIMNutrientID = (Int32)lueAimNutrientCodeTarget.EditValue;
                target.MinAmount = resultMin;
                target.MinPercent = resultMinPct;

                target.MaxAmount = resultMax;
                target.MaxPercent = resultMaxPct;

                target.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                nutrientResult = AIMManager.SaveNutrientToleranceDetailTarget(ref target);
                if (nutrientResult.ErrorNumber > 0)
                {
                    return;
                }

                //Databind Nutrients
                DataBindNutrientGridTarget(_headerID);


                lueAimNutrientCodeTarget.EditValue = 0;
                lueAimNutrientCodeTarget.Focus();

                txtMinTarget.Text = "0";
                txtMinPctTarget.Text = "0";
                txtMaxTarget.Text = "0";
                txtMaxPctTarget.Text = "0";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repDeleteTargetNutrient_Click(object sender, EventArgs e)
        {
            try
            {
                NutrientToleranceDetailTargetInfo detail = new NutrientToleranceDetailTargetInfo();
                detail = (NutrientToleranceDetailTargetInfo)grdNutrientTargetView.GetFocusedRow();
                detail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                                
                AIMManager.DeleteNutrientToleranceDetailTarget(_headerID, detail.AIMNutrientID);
                DataBindNutrientGridTarget(_headerID);
                lueAimNutrientCodeTarget.EditValue = 0;
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdNutrientTargetView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            Double amtMin = 0;
            Double pctMin = 0;
            Double amtMax = 0;
            Double pctMax = 0;
            amtMin = Convert.ToDouble(gv.GetRowCellValue(e.RowHandle, gv.Columns["MinAmount"]));
            pctMin = Convert.ToDouble(gv.GetRowCellValue(e.RowHandle, gv.Columns["MinPercent"]));
            amtMax = Convert.ToDouble(gv.GetRowCellValue(e.RowHandle, gv.Columns["MaxAmount"]));
            pctMax = Convert.ToDouble(gv.GetRowCellValue(e.RowHandle, gv.Columns["MaxPercent"]));

            //------------------------------------------------------------------------------  MIN
            if (gv.FocusedColumn.FieldName == "MinAmount")
            {
                if (amtMin > 0 && pctMin > 0)
                {
                    gv.SetRowCellValue(e.RowHandle, gv.Columns["MinPercent"], 0);
                }
               
            }
            if (gv.FocusedColumn.FieldName == "MinPercent")
            {
                if (amtMin > 0 && pctMin > 0)
                {
                    gv.SetRowCellValue(e.RowHandle, gv.Columns["MinAmount"], 0);
                }
            }


            //------------------------------------------------------------------------------  MAX
            if (gv.FocusedColumn.FieldName == "MaxAmount")
            {
                if (amtMax > 0 && pctMax > 0)
                {
                    gv.SetRowCellValue(e.RowHandle, gv.Columns["MaxPercent"], 0);
                }
            }
            if (gv.FocusedColumn.FieldName == "MaxPercent")
            {
                if (amtMax > 0 && pctMax > 0)
                {
                    gv.SetRowCellValue(e.RowHandle, gv.Columns["MaxAmount"], 0);
                }
            }

        }

        private void btnCancelAddNew_Click(object sender, EventArgs e)
        {
            pnlExisting.Visible = true;
            pnlExisting.Location = new Point(11, 58);
            pnlNew.Visible = false;
            pnlNew.Location = new Point(521, 143);
            btnAddNew.Visible = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            pnlExisting.Visible = true;
            pnlExisting.Location = new Point(11, 58);
            pnlNew.Visible = false;
            pnlNew.Location = new Point(521, 143);
        }

        private void grdNutrientView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                NutrientToleranceDetailInfo nutrient = new NutrientToleranceDetailInfo();
                nutrient = (NutrientToleranceDetailInfo)grdNutrientView.GetFocusedRow();
                nutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SaveNutrientToleranceDetail(ref nutrient);                
                DataBindNutrientGrid(nutrient.NutrientToleranceHeaderID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdNutrientTargetView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            NutrientToleranceDetailTargetInfo nutrient = new NutrientToleranceDetailTargetInfo();
            nutrient = (NutrientToleranceDetailTargetInfo)grdNutrientTargetView.GetFocusedRow();
            nutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            AIMManager.SaveNutrientToleranceDetailTarget(ref nutrient);
            DataBindNutrientGridTarget(nutrient.NutrientToleranceHeaderID);
            
        }
    }
}
