using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraReports.UI;



namespace CFCAIM
{
    public partial class Utilities : Form
    {
        CFCUser.Global userGlobal = new CFCUser.Global();
		List<AIMSupplierInfo> _source = new List<AIMSupplierInfo>();
		List<AIMSupplierLocationInfo> _sourceLocation = new List<AIMSupplierLocationInfo>();
        List<AnalyzerMaterialInfo> _AnalyzerMaterial = new List<AnalyzerMaterialInfo>();
        List<DataSourceTypeInfo> _dataSource = new List<DataSourceTypeInfo>();
        

        public Utilities()
        {
            InitializeComponent();
        }
                
        private void Utilities_Load(object sender, EventArgs e)
        {
            try
            {

                tabControl2.TabPages.Remove(tpIngredient);

                DataBindSupplierlue();
                DataBindSupplierLocationlue();
                DataBindSupplierLocationlueNew();
                DataBindlueDataSource();
                DataBindSampleCodelue();                    //11/11/22              
                SetToolTips();
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataBindlueDataSource()
        {

            _dataSource = AIMManager.GetDataSourceTypeRefExists(true);            
            _dataSource.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--All Sources--"

                }
            );

            lueDataSource.Properties.DataSource = _dataSource;
            XtraEditors.LoadLookUpEdit(ref lueDataSource, "DataSourceTypeID", "Name", "TypeDisplay");

            if (_dataSource.Count == 2)
            {
                //selecting value if there is only one
                lueDataSource.EditValue = _dataSource[1].DataSourceTypeID;
            }
            else
            {
                lueDataSource.EditValue = _dataSource;
            }
        }
        private void DataBindSupplierlue()
        {
            
            List<AIMSupplierInfo> aimSupplier = new List<AIMSupplierInfo>();
            aimSupplier = AIMManager.GetSupplierForUtility(true);
            aimSupplier.Insert
            (
                0,
                new AIMSupplierInfo
                {
                    AIMSupplierID = 0,
                    AIMSupplierCode = "--Select--",
                    SupplierName = "--Select--"
                }
            );
            
            lueSupplier.Properties.DataSource = aimSupplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplier, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
            lueSupplier.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

            lueNewSupplier.Properties.DataSource = aimSupplier;
            XtraEditors.LoadLookUpEdit(ref lueNewSupplier, "AIMSupplierID", "AIMSupplierCode", "SupplierName");            
            lueNewSupplier.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";
        }

        //11/11/22 
        private void DataBindSampleCodelue()
        {
            List<SampleInfo> sample = new List<SampleInfo>();
            sample = AIMManager.GetAllSamples(true);
            sample.Insert
            (
                0,
                new SampleInfo
                {
                    SampleID = 0,
                    SampleCode = "--Select--"                   
                }
            );
            lueSampleCode.Properties.DataSource = sample;
            XtraEditors.LoadLookUpEdit(ref lueSampleCode, "SampleID", "SampleCode", "SampleDate");
            
        }

        private void DataBindSupplierLocationlue()
        {
            Int32 supplier = 0;
            
            //------------------------------------------------------
            List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
            if (lueSupplier.EditValue != null)
            {
                Int32.TryParse(lueSupplier.EditValue.ToString(), out supplier);
            }
            aimSupplierLocation = AIMManager.GetSupplierLocationForSupplierForUtility(supplier);
            aimSupplierLocation.Insert
            (
                0,
                new AIMSupplierLocationInfo
                {
                    AIMSupplierLocationID = 0,
                    AIMSupplierLocationCode = "--Select--",
                    SupplierLocationName = "--Select--"
                }
            );
            lueSupplierLocation.Properties.DataSource = aimSupplierLocation;
            XtraEditors.LoadLookUpEdit(ref lueSupplierLocation, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
            lueSupplierLocation.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

            if (aimSupplierLocation.Count == 2)
            {
                //selecting value if there is only one
                lueSupplierLocation.EditValue = aimSupplierLocation[1].AIMSupplierLocationID;
            }
            else
            {
                lueSupplierLocation.EditValue = aimSupplierLocation;
            }           

        }

        private void DataBindSupplierLocationlueNew()
        {
            Int32 supplier = 0;
                       
            List<AIMSupplierLocationInfo> aimNewSupplierLocation = new List<AIMSupplierLocationInfo>();
            if (lueNewSupplier.EditValue != null)
            {
                Int32.TryParse(lueNewSupplier.EditValue.ToString(), out supplier);
            }
            aimNewSupplierLocation = AIMManager.GetSupplierLocationForSupplierForUtility(supplier);
            aimNewSupplierLocation.Insert
            (
                0,
                new AIMSupplierLocationInfo
                {
                    AIMSupplierLocationID = 0,
                    AIMSupplierLocationCode = "--Select--",
                    SupplierLocationName = "--Select--"
                }
            );
            lueNewSupplierLocation.Properties.DataSource = aimNewSupplierLocation;
            XtraEditors.LoadLookUpEdit(ref lueNewSupplierLocation, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
            lueNewSupplierLocation.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

            if (aimNewSupplierLocation.Count == 2)
            {
                //selecting value if there is only one
                lueNewSupplierLocation.EditValue = aimNewSupplierLocation[1].AIMSupplierLocationID;
            }
            else
            {
                lueNewSupplierLocation.EditValue = aimNewSupplierLocation;
            }


        }

        private void DataBindMateriallue()
        {

            Int32 dataType = 0;
            Int32 materialtype = 0;
            Int32 ownerLocationID = 0;

            if (lueDataSource.EditValue != null)
            {
                dataType = (Int32)lueDataSource.EditValue;
            }

            List<AIMMaterialRefAnalyzerMaterialInfo> _aimMaterialXref = new List<AIMMaterialRefAnalyzerMaterialInfo>();
            materialtype = (Int32)MaterialTypeEnum.Ingredient;           
            _aimMaterialXref = AIMManager.GetAnalyzerMaterialsForUtility(dataType, materialtype, ownerLocationID);
            lueIngredient.Properties.DataSource = _aimMaterialXref;                          
            XtraEditors.LoadLookUpEdit(ref lueIngredient, "AnalyzerMaterialID", "AnalyzerMaterialCode", "AIMMaterialCode", "AIMMaterialName", "DataSourceTypeName");
            lueIngredient.Properties.Columns[12].Caption = "Source Name";                                   
            lueIngredient.Properties.Columns[10].Caption = "Analyzer Code";

            lueNewIngredient.Properties.DataSource = _aimMaterialXref;
            XtraEditors.LoadLookUpEdit(ref lueNewIngredient, "AnalyzerMaterialID", "AnalyzerMaterialCode", "AIMMaterialCode", "AIMMaterialName", "DataSourceTypeName");
            lueNewIngredient.Properties.Columns[12].Caption = "Source Name";
            lueNewIngredient.Properties.Columns[10].Caption = "Analyzer Code";

        }

        

		private void SetToolTips()
		{
			try
			{
				System.Drawing.Icon ico = Properties.Resources.CFCAIM;
                this.Icon = ico;
                ToolTip ttip = new ToolTip();
                foreach (Control ctrl in pnlTopBtns.Controls)
                {
                    ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
                }
				ttip.SetToolTip(btnSaveMaterial, "Save");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
                ttip.SetToolTip(btnSaveSupplierLocation, "Replace Sample Supplier/Supplier Location and update Cross-Reference");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
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

		
        private void btnResize_Click(Object sender, EventArgs e)
        {
            //6/19/17
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void Utilities_SizeChanged(Object sender, EventArgs e)
        {
            //6/19/17
            btnResize.Visible = true;
        }

        private void lueSupplier_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {                
                DataBindSupplierLocationlue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueNewSupplier_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {               
                DataBindSupplierLocationlueNew();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //11/11/22 - commented, rearranged form to use TabControl instead
        //private void ShowHidePanelsAndDatabindLues()        
        //{

        //    pnlSupplierLocation.Visible = false;
        //    pnlIngredient.Visible = false;        

        //    if (rbSupplierLocation.Checked == true)
        //    {
        //        DataBindSupplierLocationlue();
        //        DataBindSupplierLocationlueNew();
        //        pnlSupplierLocation.Visible = true;
        //    }
        //    else if (rbIngredient.Checked == true)
        //    {
        //        DataBindMateriallue();
        //        pnlIngredient.Visible = true;
        //    }        
        //}

        //11/11/22 - commented, rearranged form to use TabControl instead
        //private void rbSupplier_CheckedChanged(Object sender, EventArgs e)
        //{
        //    ShowHidePanelsAndDatabindLues();
        //}

        //11/11/22 - commented, rearranged form to use TabControl instead
        //private void rbSupplierLocation_CheckedChanged(Object sender, EventArgs e)
        //{
        //    ShowHidePanelsAndDatabindLues();
        //}

        //11/11/22 - commented, rearranged form to use TabControl instead
        //private void rbIngredient_CheckedChanged(Object sender, EventArgs e)
        //{
        //    ShowHidePanelsAndDatabindLues();
        //}


        private void lueSupplier_Click(Object sender, EventArgs e)
        {
            lueSupplier.SelectAll();
        }

        private void lueSupplier_Enter(Object sender, EventArgs e)
        {
            lueSupplier.SelectAll();
        }

        private void lueSupplierLocation_Click(Object sender, EventArgs e)
        {
            lueSupplierLocation.SelectAll();
        }

        private void lueSupplierLocation_Enter(Object sender, EventArgs e)
        {
            lueSupplierLocation.SelectAll();
        }

        private void lueNewSupplier_Click(Object sender, EventArgs e)
        {
            lueNewSupplier.SelectAll();
        }

        private void lueNewSupplier_Enter(Object sender, EventArgs e)
        {
            lueNewSupplier.SelectAll();
        }

        private void lueNewSupplierLocation_Click(Object sender, EventArgs e)
        {
            lueNewSupplierLocation.SelectAll();
        }

        private void lueNewSupplierLocation_Enter(Object sender, EventArgs e)
        {
            lueNewSupplierLocation.SelectAll();
        }

        private void lueIngredient_Click(Object sender, EventArgs e)
        {
            lueIngredient.SelectAll();
        }

        private void lueIngredient_Enter(Object sender, EventArgs e)
        {
            lueIngredient.SelectAll();
        }

        private void lueNewIngredient_Click(Object sender, EventArgs e)
        {
            lueNewIngredient.SelectAll();
        }

        private void lueNewIngredient_Enter(Object sender, EventArgs e)
        {
            lueNewIngredient.SelectAll();
        }

        private void btnRefresh_Click(Object sender, EventArgs e)
        {            
            //rbSupplierLocation.Checked = false;
            //rbIngredient.Checked = false;

            DataBindSupplierlue();
            DataBindSupplierLocationlue();
            DataBindSupplierLocationlueNew();
            DataBindlueDataSource();            
            DataBindSampleCodelue();                    //11/11/22
            tabControl2.SelectedIndex = 0;              //11/11/22

        }

        private void btnSaveSupplierLocation_Click(Object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void btnSaveSupplier_Click(Object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void btnSaveMaterial_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void SaveChanges()
        {
            try
            {
                //----------------------------------------------------------------------------------
                
                //if (pnlSupplierLocation.Visible == true)
                if (tabControl2.SelectedIndex == 0)
                    {
                    if ((Int32)lueSupplier.EditValue == 0)
                    {
                        MessageBox.Show("Please select AIM supplier.", "Warning...");
                        lueSupplier.Focus();
                        lueSupplier.ShowPopup();
                        return;
                    }
                    if ((Int32)lueSupplierLocation.EditValue == 0)
                    {
                        MessageBox.Show("Please select AIM supplier location.", "Warning...");
                        lueSupplierLocation.Focus();
                        lueSupplierLocation.ShowPopup();
                        return;
                    }
                    if ((Int32)lueNewSupplier.EditValue == 0)
                    {
                        MessageBox.Show("Please select replacement AIM supplier.", "Warning...");
                        lueNewSupplier.Focus();
                        lueNewSupplier.ShowPopup();
                        return;
                    }                    
                    if ((Int32)lueNewSupplierLocation.EditValue == 0)
                    {
                        MessageBox.Show("Please select replacement AIM supplier location.", "Warning...");
                        lueNewSupplierLocation.Focus();
                        lueNewSupplierLocation.ShowPopup();
                        return;
                    }
                }

                //----------------------------------------------------------------------------------
                //11/11/22
                else if (tabControl2.SelectedIndex == 1)
                {
                    if ((Int32)lueSampleCode.EditValue == 0)
                    {
                        MessageBox.Show("Please select a Sample Code.", "Warning...");
                        lueSampleCode.Focus();
                        lueSampleCode.ShowPopup();
                        return;
                    }
                    if (txtNewSampleCode.Text == String.Empty)
                    {
                        MessageBox.Show("Please enter a new Sample Code.", "Warning...");
                        txtNewSampleCode.Focus();
                        return;
                    }
                }

                //----------------------------------------------------------------------------------
                //else if (pnlIngredient.Visible == true)
                else if (tabControl2.SelectedIndex == 2)
                    {
                    if ((Int32)lueIngredient.EditValue == 0)
                    {
                        MessageBox.Show("Please select Analyzer material.", "Warning...");
                        lueIngredient.Focus();
                        lueIngredient.ShowPopup();
                        return;
                    }
                    if ((Int32)lueNewIngredient.EditValue == 0)
                    {
                        MessageBox.Show("Please select replacement Analyzer material.", "Warning...");
                        lueNewIngredient.Focus();
                        lueNewIngredient.ShowPopup();
                        return;
                    }
                }

                

                //----------------------------------------------------------------------------------
                DialogResult dialogR;
                String msg = String.Empty;

                if (tabControl2.SelectedIndex == 1)     //SampleCode tab
                {
                    //check to see if there are existing samples with this new sampleCode, and inform user before updating                    
                    List<SampleInfo> selectedSampleCode = new List<SampleInfo>();                      
                    selectedSampleCode = AIMManager.GetSampleForUtility(txtNewSampleCode.Text);
                    if (selectedSampleCode.Count > 0)
                    {
                        msg = "This will update " + selectedSampleCode.Count + " existing sample record(s). Continue with update?";
                    }
                    else
                    {
                        msg = "This will update all sample records that match the condition. Continue with update?";
                    }
                }
                else
                {
                    msg = "This will update all sample records that match the condition. Continue with update?";
                }

                dialogR = MessageBox.Show(msg, "Warning...", MessageBoxButtons.YesNo);
                if (dialogR == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                //saving...
                SampleInfo sampleToBeReplaced = new SampleInfo();
                SampleInfo sample = new SampleInfo();
                Int32 val = 0;
                sampleToBeReplaced.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                sample.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                //----------------------------------------------------------------------------------
                
                //if (pnlIngredient.Visible == true)
                if (tabControl2.SelectedIndex == 2)

                    {
                    Int32.TryParse(lueIngredient.EditValue.ToString(), out val);
                    if (val > 0)
                    {
                        sampleToBeReplaced.AnalyzerMaterialID = val;
                    }
                    val = 0;
                    Int32.TryParse(lueNewIngredient.EditValue.ToString(), out val);
                    if (val > 0)
                    {
                        sample.AnalyzerMaterialID = val;
                    }
                }
                
                //else if (pnlSupplierLocation.Visible == true)
                else if (tabControl2.SelectedIndex == 0)
                {
                    Int32.TryParse(lueSupplier.EditValue.ToString(), out val);
                    if (val > 0)
                    {
                        sampleToBeReplaced.AIMSupplierID = val;
                    }
                    val = 0;
                    Int32.TryParse(lueNewSupplier.EditValue.ToString(), out val);
                    if (val > 0)
                    {
                        sample.AIMSupplierID = val;
                    }

                    Int32.TryParse(lueSupplierLocation.EditValue.ToString(), out val);
                    if (val > 0)
                    {
                        sampleToBeReplaced.AIMSupplierLocationID = val;
                    }
                    val = 0;
                    Int32.TryParse(lueNewSupplierLocation.EditValue.ToString(), out val);
                    if (val > 0)
                    {
                        sample.AIMSupplierLocationID = val;
                    }
                }

                //11/11/22
                else if (tabControl2.SelectedIndex == 1)
                {
                    sampleToBeReplaced.SampleCode = lueSampleCode.Text;
                    sample.SampleCode = txtNewSampleCode.Text;
                }

                //----------------------------------------------------------------------------------
                //UPDATE SAMPLE 
                AIMManager.SaveSample(ref sampleToBeReplaced, ref sample);

                //----------------------------------------------------------------------------------
                //UPDATE CROSS-REFERENCE
                ErrorInfo error = new ErrorInfo();

                //if (pnlIngredient.Visible == true)
                if (tabControl2.SelectedIndex == 2)
                {
                    error = AIMManager.UpdateAIMMaterialRefAnalyzerMaterial(ref sampleToBeReplaced, ref sample, ckRemoveMaterial.Checked);
                    if (error.ErrorNumber > 0)
                    {
                        return;
                    }
                }

                //else if (pnlSupplierLocation.Visible == true)
                else if (tabControl2.SelectedIndex == 0)
                {                    
                    error = AIMManager.UpdateAIMSupplierRefAnalyzerSupplier(ref sampleToBeReplaced, ref sample, ckRemoveSupplier.Checked);
                    if (error.ErrorNumber > 0)
                    {
                        return;
                    }
                }

                DataBindSupplierlue();
                DataBindSupplierLocationlue();
                DataBindSupplierLocationlueNew();
                DataBindlueDataSource();               
                DataBindSampleCodelue();                        //11/11/22
                txtNewSampleCode.Text = String.Empty;           //11/11/22
                MessageBox.Show("Update Complete");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueDataSource_EditValueChanged(Object sender, EventArgs e)
        {
            try
            {
                DataBindMateriallue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //11/11/22
        private void btnSaveSampleCode_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }        
    }
}