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
    public partial class ProductTolerance : Form
    {
        CFCUser.Global userGlobal = new CFCUser.Global();
        Int32 _headerID = 0;
        Boolean _blnSkipProductCheck = false;
        List<ProductToleranceHeaderInfo> _header = new List<ProductToleranceHeaderInfo>();
        List<ProductToleranceDetailInfo> _detail = new List<ProductToleranceDetailInfo>();

        public ProductTolerance()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }

        private void ProductTolerance_Load(object sender, EventArgs e)
        {
            System.Drawing.Icon ico = Properties.Resources.CFCAIM;
            this.Icon = ico;
            DataBindGroupLue();
            DataBindSpecUsingFormulationLue();
            DataBindAimMaterialLue(0);
            PopulateReplue();
            SetToolTips();

            tabControl1.TabPages.Remove(tpNutrients_Formulation);
            tabControl1.TabPages.Remove(tpNutrients_Aim);
            DataBindNutrientlue();

            WindowState = FormWindowState.Maximized;
        }

        private void PopulateReplue()
        {
            //---------------------------------------------------
            List<PlusMinusInfo> plusMinus = new List<PlusMinusInfo>();

            //12/1/22
            //plusMinus = AIMManager.GetPlusMinus();
            plusMinus = AIMManager.GetPlusMinus(false);     // false = 'None' will not be included in datasource

            repluePlusMinus.DataSource = plusMinus;
            XtraEditors.LoadGridLookUpEdit(ref repluePlusMinus, "PlusMinusID", "PlusMinusName");
            repluePlusMinus.Columns["PlusMinusName"].Caption = "Plus/Minus";
            repluePlusMinus.PopupWidth = 10;
        }

        private void DataBindGroupLue()
        {
            _header = AIMManager.GetProductToleranceHeader();
            _header.Insert
                (
                    0,
                    new ProductToleranceHeaderInfo
                    {
                        ProductToleranceHeaderID = 0,
                        ProductToleranceHeaderName = "--Select--"
                    }
                );
            lueProductToleranceGroupName.Properties.DataSource = _header;
            XtraEditors.LoadLookUpEdit(ref lueProductToleranceGroupName, "ProductToleranceHeaderID", "ProductToleranceHeaderName");
            lueProductToleranceGroupName.Properties.Columns[1].Caption = "Product Tolerance Group Name";

        }
        private void DataBindSpecUsingFormulationLue()
        {
            List<CFCProductRefAIMMaterialInfo> aimMaterial = new List<CFCProductRefAIMMaterialInfo>();
            aimMaterial = AIMManager.GetCFCProductRefAIMMaterial();
            aimMaterial.Insert
            (
                0,
                new CFCProductRefAIMMaterialInfo
                {
                    AIMMaterialID = 0,
                    AIMMaterialCode = "--Select--",
                    AIMMaterialName = "--Select--",
                    ArchiveVersion = "--Select--"
                   
                }
            ) ;
            lueSpecCodeNew.Properties.DataSource = aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueSpecCodeNew, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "ArchiveVersion", "CFCProductArchiveID");

            lueSpecCode.Properties.DataSource = aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueSpecCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "ArchiveVersion", "CFCProductArchiveID");
        }

        private void DataBindSpecUsingAimLue()
        {
            List<ProductToleranceProductCodesInfo> aimMaterial = new List<ProductToleranceProductCodesInfo>();
            aimMaterial = AIMManager.GetAIMMaterialProductCodes();
            aimMaterial.Insert
            (
                0,
                new ProductToleranceProductCodesInfo
                {
                    AIMMaterialID = 0,
                    AIMMaterialCode = "--Select--",
                    AIMMaterialName = "--Select--"
                }
            );
            lueSpecCodeNew_UsingAim.Properties.DataSource = aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueSpecCodeNew_UsingAim, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");

            lueSpecCode_UsingAim.Properties.DataSource = aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueSpecCode_UsingAim, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");

        }

        private void DataBindAimMaterialLue(Int32 materialType)
        {
            List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();

            if (materialType == 0)
            {
                aimMaterial = AIMManager.GetAIMMaterial(true);
            }
            else
            {
                //10/13/23
                //aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, materialType);
                aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, materialType, (Int32)EnumOrderBy.Name);
            }
            aimMaterial.Insert
            (
                0,
                new AIMMaterialInfo
                {
                    AIMMaterialID = 0,
                    AIMMaterialCode = "--Select--",
                    AIMMaterialName = "--Select--",
                    AIMOwnerLocationName = "--Select--"
                }
            );

            lueAIMMaterial.Properties.DataSource = aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueAIMMaterial, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "AIMOwnerLocationName");

            //11/15/22
            lueAIMMaterialName.Properties.DataSource = aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueAIMMaterialName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode", "AIMOwnerLocationName");


        }


        private void DataBindMaterialGrid()
        {
            Int32 rowNum = 0;
            Int32.TryParse(grdProductView.FocusedRowHandle.ToString(), out rowNum);
            grdProduct.DataSource = AIMManager.GetProductToleranceDetail(_headerID);
            grdProductView.FocusedRowHandle = rowNum;

        }

        private void SetToolTips()
        {
            ToolTip ttip = new ToolTip();
            foreach (Control ctrl in pnlTopBtns.Controls)
            {
                ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
            }
            ttip.SetToolTip(btnAddHeader, "Add Group");
            ttip.SetToolTip(btnDelete, "Delete Group");
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
            ttip.SetToolTip(btnAddMaterial, "Add Material to Group");
            ttip.SetToolTip(btnSaveHeader, "Save Group");
            ttip.SetToolTip(btnAddNutrient, "Add Nutrient to Group");

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
        
        private void repbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ProductToleranceDetailInfo detail = new ProductToleranceDetailInfo();
                detail = (ProductToleranceDetailInfo)grdProductView.GetFocusedRow();
                detail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.DeleteProductToleranceDetail(_headerID, detail.ProductToleranceDetailID);
                DataBindMaterialGrid();
                lueAIMMaterial.EditValue = 0;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            pnlNew.Visible = true;
            pnlNew.Location = new Point(8, 58);
            pnlExisting.Visible = false;
            
            gbAddMaterial.Visible = false;
            tabControl1.Visible = false;
            pnlUsingSpecsFrom.Visible = true;

            txtProductToleranceNameNew.Text = "";
            txtProductToleranceNameNew.Focus();
            lueAIMMaterial.EditValue = 0;

            lueSpecCodeNew.EditValue = 0;
            lueSpecCodeNew_UsingAim.EditValue = 0;

            tabControl1.TabPages.Remove(tpNutrients_Formulation);
            tabControl1.TabPages.Remove(tpNutrients_Aim);


        }

        private void hideShowEditFields(Boolean val, Int32 specFrom)
        {            
            lblSpecCodeEdit.Visible = val;            
            btnDelete.Visible = val;            

            if (specFrom == (Int32)EnumSpecFrom.Aim)
            {
                lblProductionVersion.Visible = false;
                txtProdVersion.Visible = false;
                lblArchiveVersion.Visible = false;
                txtArchiveVersion.Visible = false;                
                lueSpecCode.Visible = false;
            }

            else
            {
                lblProductionVersion.Visible = val;
                txtProdVersion.Visible = val;
                lblArchiveVersion.Visible = val;
                txtArchiveVersion.Visible = val;
                lueSpecCode.Visible = val;
                txtSpecName.Visible = val;                              
            }
        }

        //////private void btnSelectExisting_Click(object sender, EventArgs e)
        //////{
        //////    pnlExisting.Visible = true;
        //////    pnlExisting.Location = new Point(11, 106);
        //////    pnlNew.Visible = false;
        //////    pnlNew.Location = new Point(521, 143);
        //////    tabControl1.TabPages.Remove(tpNutrients_Formulation);
        //////    tabControl1.TabPages.Remove(tpNutrients_Aim);

        //////    pnlUsingSpecsFrom.Visible = false;

        //////    hideShowEditFields(false, (Int32)EnumSpecFrom.Formulation);

        //////    if (_header.Count == 2)
        //////    {
        //////        lueProductToleranceGroupName.EditValue = _header[1].ProductToleranceHeaderID;
        //////        hideShowEditFields(true, (Int32)EnumSpecFrom.Formulation);
        //////    }
        //////    else
        //////    {
        //////        lueProductToleranceGroupName.EditValue = 0;               
        //////    }
        //////}

        private void btnAddHeader_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductToleranceNameNew.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Material Group Name.");
                    txtProductToleranceNameNew.Focus();
                    return;
                }                                
                if (rbUseAim.Checked == true)
                {
                    if ((Int32)lueSpecCodeNew_UsingAim.EditValue == 0)
                    {
                        MessageBox.Show("Please select Spec Product Code.");
                        lueSpecCodeNew_UsingAim.Focus();
                        lueSpecCodeNew_UsingAim.ShowPopup();
                        return;
                    }
                }
                else
                {
                    if ((Int32)lueSpecCodeNew.EditValue == 0)
                    {
                        MessageBox.Show("Please select Spec Product Code.");
                        lueSpecCodeNew.Focus();
                        lueSpecCodeNew.ShowPopup();
                        return;
                    }
                }

                List<ProductToleranceHeaderInfo> header = new List<ProductToleranceHeaderInfo>();
                header = AIMManager.GetProductToleranceHeaderName(txtProductToleranceNameNew.Text);
                if (header == null) { }
                else if (header.Count > 0)
                {
                    MessageBox.Show("This Material Group Name already exists. ", "Warning...", MessageBoxButtons.OK);
                    txtProductToleranceNameNew.Focus();
                    txtProductToleranceNameNew.SelectAll();
                    return;
                }

                //add product tolerance
                ProductToleranceHeaderInfo result = new ProductToleranceHeaderInfo();
                result.ProductToleranceHeaderID = 0;
                result.ProductToleranceHeaderName = txtProductToleranceNameNew.Text;
                                
                if (rbUseAim.Checked == true)
                {                    
                    result.AIMMaterialID = (Int32)lueSpecCodeNew_UsingAim.EditValue;
                    result.AIMMaterialCode = lueSpecCodeNew_UsingAim.Text;
                    result.SpecFrom = (Int32)EnumSpecFrom.Aim;
                    tabControl1.TabPages.Remove(tpNutrients_Formulation);
                    tabControl1.TabPages.Add(tpNutrients_Aim);
                }
                else
                {
                    result.AIMMaterialID = (Int32)lueSpecCodeNew.EditValue;
                    result.AIMMaterialCode = lueSpecCodeNew.Text;
                    result.SpecFrom = (Int32)EnumSpecFrom.Formulation;
                    tabControl1.TabPages.Remove(tpNutrients_Aim);
                    tabControl1.TabPages.Add(tpNutrients_Formulation);
                }

                result.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                ErrorInfo outPut = AIMManager.SaveProductToleranceHeader(ref result);
                if (outPut.ErrorNumber != 0)
                {
                    MessageBox.Show("Unable to save the product tolerance group.", "Critical...", MessageBoxButtons.OK);
                    return;
                }
                ProductToleranceDetailInfo clearResults = new ProductToleranceDetailInfo();
                grdProduct.DataSource = clearResults;
                DataBindMaterialGrid();
                _headerID = outPut.RecordID;

                //For formulation, get archiveID and populate nutrients
                if (rbUseFormulation.Checked == true)
                {
                    var item = lueSpecCodeNew.GetSelectedDataRow();
                    Int32 archiveID = ((CFCProductRefAIMMaterialInfo)item).CFCProductArchiveID;                    
                    if ((Int32)lueSpecCodeNew.EditValue != 0)
                    {                       
                        InsertNutrients(archiveID);
                    }

                    //Populate nutrients grid                    
                    DataBindNutrients(_headerID);
                }

                //refresh groupname dropdown and pull up newly-added group                                       
                DataBindGroupLue();
                DataBindAimMaterialLue(2);
                lueProductToleranceGroupName.EditValue = _headerID;
                pnlExisting.Visible = true;                
                pnlNew.Visible = false;
                pnlNew.Location = new Point(521, 143);
                lueAIMMaterial.Focus();
                lueAIMMaterial.SelectAll();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertNutrients(Int32 headerID)
        {
            try
            {
                ErrorInfo nutrientResult = new ErrorInfo();
                List<CFCProductArchiveSpecInfo> spec = new List<CFCProductArchiveSpecInfo>();
                spec = AIMManager.GetAIMNutrientCFCProductArchiveSpec(headerID);
                foreach (var item in spec)
                {
                    ProductToleranceNutrientInfo nutrients = new ProductToleranceNutrientInfo();
                    nutrients.ProductToleranceNutrientID = 0;
                    nutrients.ProductToleranceHeaderID = _headerID;
                    nutrients.AIMNutrientID = item.AIMNutrientID;
                    nutrients.NutrientValue = (Double)item.NutrientValue;

                    nutrients.NutrientMin = item.NutrientMin;
                    nutrients.MinPlusMinus = (Int32)EnumPlusMinus.Minus;
                    nutrients.MinAmount = 0;
                    nutrients.MinPercent = 0;

                    nutrients.NutrientMax = item.NutrientMax;
                    nutrients.MaxPlusMinus = (Int32)EnumPlusMinus.Plus;
                    nutrients.MaxAmount = 0;
                    nutrients.MaxPercent = 0;
                    nutrientResult = AIMManager.SaveProductToleranceNutrient(ref nutrients);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
               
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if ((Int32)lueProductToleranceGroupName.EditValue == 0)
            {
                MessageBox.Show("Please select Material Group to delete.");
                lueProductToleranceGroupName.Focus();
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Delete Material Group '" + lueProductToleranceGroupName.Text + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                Int32 headerID = 0;
                headerID = (Int32)lueProductToleranceGroupName.EditValue;

                AIMManager.DeleteProductToleranceDetail(headerID);
                AIMManager.DeleteProductToleranceHeader(headerID);

                //delete from cfcuser (set GroupID = 0)
                AIMManager.UpdateCFCUsersSpecificIngredientsGroup(headerID, 0);

                DataBindMaterialGrid();
                DataBindGroupLue();
                txtProductToleranceNameNew.Text = String.Empty;
                
                lueAIMMaterial.EditValue = 0;
                lueSpecCodeNew.EditValue = 0;

                tabControl1.Visible = false;
                pnlExisting.Visible = false;
                txtSpecName.Text = String.Empty;               
                txtProdVersion.Text = String.Empty;
                txtArchiveVersion.Text = String.Empty;
                _blnSkipProductCheck = true;
                lueSpecCode.EditValue = 0;
                _blnSkipProductCheck = false;
            }

        }

        private void btnAddMaterial_Click(object sender, EventArgs e)
        {
            if ((Int32)lueProductToleranceGroupName.EditValue == 0)
            {
                MessageBox.Show("Please select Material Group.");
                lueProductToleranceGroupName.Focus();
                lueProductToleranceGroupName.ShowPopup();
                return;
            }
            if ((Int32)lueAIMMaterial.EditValue == 0)
            {
                MessageBox.Show("Please select Material.");
                lueAIMMaterial.Focus();
                lueAIMMaterial.ShowPopup();
                return;
            }

            //Check to see if material is part of an existing group already -- if it is, do not allow add
            List<ProductToleranceDetailInfo> detail = new List<ProductToleranceDetailInfo>();
            detail = AIMManager.GetProductToleranceDetailMaterial((Int32)lueAIMMaterial.EditValue);
            if (detail == null) { }
            else if (detail.Count > 0)
            {
                if (detail[0].ProductToleranceHeaderID == _headerID)
                {
                    MessageBox.Show("This material already exists in the group. ", "Warning...", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("This material exists in another group. ", "Warning...", MessageBoxButtons.OK);
                }
                lueAIMMaterial.Focus();
                lueAIMMaterial.ShowPopup();
                return;
            }
            else
            {
                ProductToleranceDetailInfo groupDetail = new ProductToleranceDetailInfo();
                groupDetail.ProductToleranceDetailID = 0;
                groupDetail.ProductToleranceHeaderID = _headerID;
                groupDetail.AIMMaterialID = (Int32)lueAIMMaterial.EditValue;
                groupDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                groupDetail.UpdateStamp = DateTime.Now;
                ErrorInfo outPut = AIMManager.SaveProductToleranceDetail(ref groupDetail);
            }
            DataBindMaterialGrid();

            lueAIMMaterial.Focus();
            lueAIMMaterial.SelectAll();
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataBindGroupLue();
            DataBindAimMaterialLue(0);
            txtProductToleranceNameNew.Text = String.Empty;           
            txtSpecName.Text = String.Empty;           
        }
               

        private void lueSpecCodeNew_Enter(object sender, EventArgs e)
        {
            lueSpecCodeNew.SelectAll();
        }

        private void lueSpecCodeNew_Click(object sender, EventArgs e)
        {
            lueSpecCodeNew.SelectAll();
        }

        private void lueAIMMaterial_PopupFilter(object sender, DevExpress.XtraEditors.Controls.PopupFilterEventArgs e)
        {
            //this will remove it from the dropdown -- but still need to have something in place for when user deletes record 
            //-- need to put it back in the list
            //LookUpEdit lookUpEdit = sender as LookUpEdit;
            //if (lookUpEdit.EditValue == null) return;
            //e.Criteria = CriteriaOperator.Parse($"AIMMaterialID != '{lookUpEdit.EditValue}'");
        }
                
        private void DataBindNutrients(Int32 headerID)
        {
            //Populate nutrients grid
            List<ProductToleranceNutrientInfo> aimNuts = new List<ProductToleranceNutrientInfo>();
            aimNuts = AIMManager.GetProductToleranceNutrient(headerID);
            grdNutrient.DataSource = aimNuts;
        }

        private void grdNutrientView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                ProductToleranceNutrientInfo nutrient = new ProductToleranceNutrientInfo();
                nutrient = (ProductToleranceNutrientInfo)grdNutrientView.GetFocusedRow();
                nutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SaveProductToleranceNutrient(ref nutrient);
                DataBindNutrients(nutrient.ProductToleranceHeaderID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveHeader_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewName.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Material Group Name.");
                    txtNewName.Focus();
                    return;
                }

                List<ProductToleranceHeaderInfo> header = new List<ProductToleranceHeaderInfo>();
                header = AIMManager.GetProductToleranceHeaderName(txtNewName.Text);
                if (header == null) { }
                else if (header.Count > 0)
                {
                    MessageBox.Show("This Product Tolerance Name already exists. ", "Warning...", MessageBoxButtons.OK);
                    txtNewName.Focus();
                    txtNewName.SelectAll();
                    return;
                }

                //update product tolerance header
                ProductToleranceHeaderInfo result = new ProductToleranceHeaderInfo();
                result.ProductToleranceHeaderID = (Int32)lueProductToleranceGroupName.EditValue;
                result.ProductToleranceHeaderName = txtNewName.Text;

                if (rbUseAim.Checked == true)
                {
                    result.AIMMaterialID = (Int32)lueSpecCode_UsingAim.EditValue;
                }
                else
                {
                    result.AIMMaterialID = (Int32)lueSpecCode.EditValue;               
                }
                
                result.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                ErrorInfo outPut = AIMManager.SaveProductToleranceHeader(ref result);
                if (outPut.ErrorNumber != 0)
                {
                    MessageBox.Show("Unable to save changes to material group.", "Critical...", MessageBoxButtons.OK);
                    return;
                }

                //refresh groupname dropdown and pull up newly-added group                                       
                DataBindGroupLue();                               
                lueProductToleranceGroupName.EditValue = _headerID;

                //This will hide/show/move buttons where they need to be
                btnCancelEdit.PerformClick();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PopulateVersionFields()
        {
            List<CFCProductRefAIMMaterialInfo> prod = new List<CFCProductRefAIMMaterialInfo>();
            prod = AIMManager.GetCFCProductRefAIMMaterial_AIMMaterialID((Int32)lueSpecCode.EditValue);
            if (prod.Count > 0)
            {
                txtProdVersion.Text = prod[0].ProductionVersion.ToString();
                txtArchiveVersion.Text = prod[0].ArchiveVersion.ToString();
            }
        }

        private void lueSpecCode_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            Boolean cancel = false;

            if (lueSpecCode.EditValue != null)
            {               
                if ((Int32)lueSpecCode.EditValue != 0)
                {
                    //-------------------------------------------------------------
                    //This is so when the groupName is changed, this code below doesn't get hit -- only gets run when the spec prod code is changed.
                    if (_blnSkipProductCheck == false)
                    {
                        if (e.NewValue != e.OldValue && (Int32)e.NewValue != 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Changing the Spec Product Code will clear any existing nutrient records for this group.  Continue changing?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (dialogResult == DialogResult.Yes)
                            {
                                //update tolerance 
                                ProductToleranceHeaderInfo result = new ProductToleranceHeaderInfo();
                                result.ProductToleranceHeaderID = (Int32)lueProductToleranceGroupName.EditValue;                                
                                result.AIMMaterialID = (Int32)e.NewValue;
                                result.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                                ErrorInfo outPut = AIMManager.SaveProductToleranceHeader(ref result);
                                if (outPut.ErrorNumber != 0)
                                {
                                    MessageBox.Show("Unable to save changes to material group.", "Critical...", MessageBoxButtons.OK);
                                    return;
                                }

                                //Delete nutrient vals for header-id before populating with new
                                AIMManager.DeleteProductToleranceNutrient((Int32)lueProductToleranceGroupName.EditValue);

                                //Insert Nutrients into table
                                InsertNutrients((Int32)lueSpecCode.EditValue);

                                //Populate nutrients grid                    
                                DataBindNutrients((Int32)lueProductToleranceGroupName.EditValue);

                                //Refresh the txtSpecName
                                List<ProductToleranceHeaderInfo> hdr = new List<ProductToleranceHeaderInfo>();
                                hdr = AIMManager.GetProductToleranceHeaderID(_headerID);   //for material-groups using FORMULATION for the spec, stp may return multiple rows (for archive versions), but this form uses the "TOP" version
                                if (hdr.Count > 0)
                                {
                                    txtSpecName.Text = hdr[0].AIMMaterialName;
                                }
                            }
                            else
                            {
                                cancel = true;
                            }
                        }
                    }
                    //-------------------------------------------------------------
                                  
                }
                e.Cancel = cancel;
            }
        }

        private void btnEditName_Click(object sender, EventArgs e)
        {
            if ((Int32)lueProductToleranceGroupName.EditValue == 0)
            {
                lueProductToleranceGroupName.ShowPopup();
                return;
            }

            lueProductToleranceGroupName.Visible = false;

            txtNewName.Size = new Size(279, 21);
            txtNewName.Location = new Point(195, 4);
            txtNewName.Visible = true;
            txtNewName.Text = lueProductToleranceGroupName.Text;
            txtNewName.Focus();
            txtNewName.SelectAll();

            btnCancelEdit.Location = new Point(475, 3);
            btnCancelEdit.Visible = true;

            btnSaveHeader.Location = new Point(546, 3);
            btnSaveHeader.Size = new Size(41, 44);
            btnSaveHeader.Visible = true;
                        
            btnAddNew.Enabled = false;

            lueSpecCode.Enabled = false;
            tabControl1.Enabled = false;

        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            lueProductToleranceGroupName.Visible = true;

            txtNewName.Location = new Point(8, 58);
            txtNewName.Visible = false;
            txtNewName.Text = String.Empty;

            btnCancelEdit.Location = new Point(425, 26);
            btnCancelEdit.Visible = false;

            btnSaveHeader.Location = new Point(496, 47);
            btnSaveHeader.Visible = false;
                        
            btnAddNew.Enabled = true;

            lueSpecCode.Enabled = true;
            tabControl1.Enabled = true;

        }

        private void grdNutrientView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            Double amt = 0;
            String fieldName = String.Empty;

            if (e.Column.FieldName == "NutrientMinAmount" ||
                e.Column.FieldName == "NutrientMinPercent" ||
                e.Column.FieldName == "NutrientMaxAmount" ||
                e.Column.FieldName == "NutrientMaxPercent")
            {
                fieldName = e.Column.FieldName;
                amt = Convert.ToDouble(grdNutrientView.GetRowCellValue(e.RowHandle, grdNutrientView.Columns[fieldName]));
                if (amt != 0)
                {
                    e.Appearance.ForeColor = Color.Blue;
                    e.Appearance.FontStyleDelta = FontStyle.Bold;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.FontStyleDelta = FontStyle.Regular;
                }
            }
        }
               

        private void lueSpecCode_EditValueChanged(object sender, EventArgs e)
        {
            PopulateVersionFields();
        }

        private void lueAIMMaterial_Click(object sender, EventArgs e)
        {
            lueAIMMaterial.SelectAll();
        }

        private void lueAIMMaterial_Enter(object sender, EventArgs e)
        {
            lueAIMMaterial.SelectAll();
        }

        private void grdNutrientView_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            GridView gv = sender as GridView;
            Double amtMin = 0;
            Double pctMin = 0;
            Double amtMax = 0;
            Double pctMax = 0;
            amtMin = Convert.ToDouble(gv.GetRowCellValue(e.RowHandle, gv.Columns["NutrientMinAmount"]));
            pctMin = Convert.ToDouble(gv.GetRowCellValue(e.RowHandle, gv.Columns["NutrientMinPercent"]));
            amtMax = Convert.ToDouble(gv.GetRowCellValue(e.RowHandle, gv.Columns["NutrientMaxAmount"]));
            pctMax = Convert.ToDouble(gv.GetRowCellValue(e.RowHandle, gv.Columns["NutrientMaxPercent"]));

            if (gv.FocusedColumn.FieldName == "NutrientMinAmount")
            {
                if (amtMin > 0)
                {
                    gv.SetRowCellValue(e.RowHandle, gv.Columns["NutrientMinPercent"], 0);
                }
            }
            if (gv.FocusedColumn.FieldName == "NutrientMinPercent")
            {
                if (pctMin > 0)
                {
                    gv.SetRowCellValue(e.RowHandle, gv.Columns["NutrientMinAmount"], 0);
                }
            }
            if (gv.FocusedColumn.FieldName == "NutrientMaxAmount")
            {
                if (amtMax > 0)
                {
                    gv.SetRowCellValue(e.RowHandle, gv.Columns["NutrientMaxPercent"], 0);
                }
            }
            if (gv.FocusedColumn.FieldName == "NutrientMaxPercent")
            {
                if (pctMax > 0)
                {
                    gv.SetRowCellValue(e.RowHandle, gv.Columns["NutrientMaxAmount"], 0);
                }
            }

        }

        private void grdNutrientView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.FieldName == "NutrientMax")
            //{
            //    e.Graphics.DrawLine(Pens.Red, e.Bounds.Left, e.Bounds.Top, e.Bounds.Left, e.Bounds.Bottom);
            //    e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
            //    e.Handled = true;
            //}
        }

        //4/29/22
        private void rbUseFormulation_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUseFormulation.Checked == true)
            {
                lueSpecCodeNew.Visible = true;
                lueSpecCodeNew_UsingAim.Visible = false;
                DataBindSpecUsingFormulationLue();
                lueSpecCodeNew.ShowPopup();
            }
        }

        //4/29/22
        private void rbUseAim_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUseAim.Checked == true)
            {
                lueSpecCodeNew.Visible = false;
                lueSpecCodeNew_UsingAim.Visible = true;
                lueSpecCodeNew_UsingAim.Location = new Point(145, 51);
                lueSpecCodeNew_UsingAim.Size = new Size(202, 22);

                DataBindSpecUsingAimLue();
                lueSpecCodeNew_UsingAim.ShowPopup();
            }
        }

        //4/29/22
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
        }

        
        //4/29/22
        private void btnAddNutrient_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Int32)lueAIMCodeNutrient.EditValue == 0)
                {
                    MessageBox.Show("Please select AIM Nutrient.", "Warning...");
                    lueAIMCodeNutrient.ShowPopup();
                    return;
                }
                
                List<ProductToleranceNutrientInfo> aimNuts = new List<ProductToleranceNutrientInfo>();
                aimNuts = AIMManager.GetProductToleranceNutrient(_headerID, (Int32)lueAIMCodeNutrient.EditValue);
                if (aimNuts.Count > 0)
                {
                    MessageBox.Show("This Nutrient has already been added.", "Warning...");
                    lueAIMCodeNutrient.ShowPopup();
                    return;
                }

                Double resultMin = 0;
                Double.TryParse(txtMin.Text, out resultMin);
                Double resultMax = 0;
                Double.TryParse(txtMax.Text, out resultMax);

                //Save Nutrient to ProductToleranceNutrient table
                ErrorInfo nutrientResult = new ErrorInfo();
                ProductToleranceNutrientInfo nutrients = new ProductToleranceNutrientInfo();
                nutrients.ProductToleranceNutrientID = 0;
                nutrients.ProductToleranceHeaderID = _headerID;
                nutrients.AIMNutrientID = (Int32)lueAIMCodeNutrient.EditValue;
                nutrients.NutrientMin = resultMin;
                nutrients.NutrientMax = resultMax;
                nutrients.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                nutrientResult = AIMManager.SaveProductToleranceNutrient(ref nutrients);
                if (nutrientResult.ErrorNumber > 0)
                {
                    return;
                }

                //Databind Nutrients
                DataBindNutrientGrid(_headerID);

                lueAIMCodeNutrient.EditValue = 0;                
                lueAIMCodeNutrient.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //4/29/22
        private void DataBindNutrientGrid(Int32 headerID)
        {
            Int32 rowNum = 0;
            Int32.TryParse(grdNutrientRefView.FocusedRowHandle.ToString(), out rowNum);
            grdNutrientUsingAim.DataSource = AIMManager.GetProductToleranceNutrient(headerID);
            grdNutrientRefView.FocusedRowHandle = rowNum;           
        }

        //4/29/22
        private void repbtnDeleteNutrient_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 hdrID = 0;
                ProductToleranceNutrientInfo nutrientRef = new ProductToleranceNutrientInfo();
                nutrientRef = (ProductToleranceNutrientInfo)grdNutrientRefView.GetFocusedRow();
                hdrID = nutrientRef.ProductToleranceHeaderID;
                AIMManager.DeleteProductToleranceNutrient_NutrientID(nutrientRef.ProductToleranceNutrientID);             
                DataBindNutrientGrid(hdrID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //4/29/22
        private void grdNutrientRefView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                ProductToleranceNutrientInfo nutrient = new ProductToleranceNutrientInfo();
                nutrient = (ProductToleranceNutrientInfo)grdNutrientRefView.GetFocusedRow();
                nutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SaveProductToleranceNutrient(ref nutrient);
                DataBindNutrientGrid(nutrient.ProductToleranceHeaderID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //4/29/22
        private void lueSpecCodeNew_UsingAim_Click(object sender, EventArgs e)
        {
            lueSpecCodeNew_UsingAim.SelectAll();
        }

        //4/29/22
        private void lueSpecCodeNew_UsingAim_Enter(object sender, EventArgs e)
        {
            lueSpecCodeNew_UsingAim.SelectAll();
        }

        //4/29/22
        private void lueSpecCode_UsingAim_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            Boolean cancel = false;

            if (lueSpecCode_UsingAim.EditValue != null)
            {
                if ((Int32)lueSpecCode_UsingAim.EditValue != 0)
                {
                    //-------------------------------------------------------------
                    //This is so when the groupName is changed, this code below doesn't get hit -- only gets run when the spec prod code is changed.
                    if (_blnSkipProductCheck == false)
                    {
                        if (e.NewValue != e.OldValue && (Int32)e.NewValue != 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Changing the Spec Product Code will clear any existing nutrient records for this group.  Continue changing?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (dialogResult == DialogResult.Yes)
                            {
                                //update material group
                                ProductToleranceHeaderInfo result = new ProductToleranceHeaderInfo();
                                result.ProductToleranceHeaderID = (Int32)lueProductToleranceGroupName.EditValue;
                                result.AIMMaterialID = (Int32)e.NewValue;
                                result.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                                ErrorInfo outPut = AIMManager.SaveProductToleranceHeader(ref result);
                                if (outPut.ErrorNumber != 0)
                                {
                                    MessageBox.Show("Unable to save changes to product tolerance group.", "Critical...", MessageBoxButtons.OK);
                                    return;
                                }

                                //Delete nutrient vals for header-id before populating with new
                                AIMManager.DeleteProductToleranceNutrient((Int32)lueProductToleranceGroupName.EditValue);

                                DataBindNutrientGrid((Int32)lueProductToleranceGroupName.EditValue);

                                //Refresh the txtSpecName
                                List<ProductToleranceHeaderInfo> hdr = new List<ProductToleranceHeaderInfo>();
                                hdr = AIMManager.GetProductToleranceHeaderID(_headerID);   //For AIM, this comes back with 12 duplicate records (?)
                                if (hdr.Count > 0)
                                {
                                    txtSpecName.Text = hdr[0].AIMMaterialName;
                                }

                            }
                            else
                            {
                                cancel = true;
                            }
                        }
                    }
                    //-------------------------------------------------------------
                }
                e.Cancel = cancel;
            }
        }

        
        

        //11/15/22
        private void lueAIMMaterialName_Click(object sender, EventArgs e)
        {
            lueAIMMaterialName.SelectAll();
        }

        //11/15/22
        private void lueAIMMaterialName_Enter(object sender, EventArgs e)
        {
            lueAIMMaterialName.SelectAll();
        }

		//4/29/22
		#region Sync values
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
        private void lueAIMMaterialName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SyncValueLue(ref lueAIMMaterialName, ref lueAIMMaterial);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lueAIMMaterial_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SyncValueLue(ref lueAIMMaterial, ref lueAIMMaterialName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        private void lueProductToleranceGroupName_EditValueChanged(object sender, EventArgs e)
        {
            Int32 materialID = 0;
            lblSpecCodeEdit.Visible = false;

            tabControl1.TabPages.Remove(tpNutrients_Formulation);
            tabControl1.TabPages.Remove(tpNutrients_Aim);

            if ((Int32)lueProductToleranceGroupName.EditValue != 0)
            {
                gbAddMaterial.Visible = true;
                tabControl1.Visible = true;
                rbUsingAim.Checked = false;
                rbUsingFormulation.Checked = false;
                pnlUsingSpecsFrom.Visible = true;

                _headerID = (Int32)lueProductToleranceGroupName.EditValue;

                //populate AIM material dropdown 
                List<ProductToleranceHeaderInfo> hdr = new List<ProductToleranceHeaderInfo>();
                hdr = AIMManager.GetProductToleranceHeaderID(_headerID);   //for tolerance-groups using FORMULATION for the spec, stp may return multiple rows (for archive versions), but this form uses the "TOP" version
                if (hdr.Count > 0)
                {
                    lueAIMMaterial.Properties.PopupWidth = 600;  //widen, to show Owner Location Name

                    //11/15/22
                    lueAIMMaterialName.Properties.PopupWidth = 600;

                    lblSpecCodeEdit.Visible = true;
                    txtSpecName.Visible = true;
                    txtSpecName.Text = hdr[0].AIMMaterialName;

                    if (hdr[0].SpecFrom == (Int32)EnumSpecFrom.Aim)
                    {
                        lueSpecCode.Visible = false;
                        lueSpecCode_UsingAim.Size = new Size(89, 22);
                        lueSpecCode_UsingAim.Visible = true;
                        lueSpecCode_UsingAim.Location = new Point(195, 27);
                        lueSpecCode_UsingAim.Visible = true;

                        _blnSkipProductCheck = true;
                        DataBindSpecUsingAimLue();

                        lueSpecCode_UsingAim.EditValue = hdr[0].AIMMaterialID;

                        tabControl1.TabPages.Add(tpNutrients_Aim);
                        rbUsingAim.Checked = true;
                        _blnSkipProductCheck = false;

                    }
                    else
                    {
                        _blnSkipProductCheck = true;
                        txtSpecName.Visible = true;
                        lueSpecCode_UsingAim.Visible = false;
                        lueSpecCode.EditValue = hdr[0].AIMMaterialID;
                        _blnSkipProductCheck = false;
                        PopulateVersionFields();
                        tabControl1.TabPages.Add(tpNutrients_Formulation);
                        rbUsingFormulation.Checked = true;
                    }

                    hideShowEditFields(true, hdr[0].SpecFrom);
                }


                DataBindAimMaterialLue(materialID);
                DataBindNutrients(_headerID);
                DataBindNutrientGrid(_headerID);
            }
            else
            {
                gbAddMaterial.Visible = false;
                tabControl1.Visible = false;
            }

            //Populate grid with detail
            Int32 rowNum = 0;
            Int32.TryParse(grdProductView.FocusedRowHandle.ToString(), out rowNum);
            if (lueProductToleranceGroupName.EditValue != null)
            {
                grdProduct.DataSource = AIMManager.GetProductToleranceDetail((Int32)lueProductToleranceGroupName.EditValue);
            }
            grdProductView.FocusedRowHandle = rowNum;

        }

        private void lueProductToleranceGroupName_Click(object sender, EventArgs e)
        {
            lueProductToleranceGroupName.SelectAll();
        }

        private void lueProductToleranceGroupName_Enter(object sender, EventArgs e)
        {
            lueProductToleranceGroupName.SelectAll();
        }

        private void grdProductView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            AIMGlobal.focusedRowGreen(sender, e);
        }      

        private void ProductTolerance_SizeChanged(object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnCancelAddNew_Click(object sender, EventArgs e)
        {
            pnlExisting.Visible = true;            
            pnlNew.Visible = false;
            pnlNew.Location = new Point(521, 143);
            btnAddNew.Visible = true;
        }
    }
}
