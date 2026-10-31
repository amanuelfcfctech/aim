using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIM.Maintenance;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System.Threading;

namespace CFCAIM
{
    public partial class SampleInfoForProcessResult : Form
    {
        #region  local variables
        CFCUser.Global userGlobal = new CFCUser.Global();
        List<AIMOwnerLocationInfo> _aimOwnerLocation = new List<AIMOwnerLocationInfo>();
        List<AIMOwnerInfo> _aimOwner = new List<AIMOwnerInfo>();
		List<AIMSupplierInfo> _source = new List<AIMSupplierInfo>();
		List<AIMSupplierLocationInfo> _sourceLocation = new List<AIMSupplierLocationInfo>();			
		List<AIMOwnerLocationInfo> _plant = new List<AIMOwnerLocationInfo>();

        public Int32 aimOwnerID = 0;
        public Int32 aimOwnerLocationID = 0;
        public Int32 sourceID = 0;
        public Int32 sourceLocationID = 0;
		public Int32 plantID = 0;
        public String AnalyzerMaterialType = "";
        public Boolean allValueAreGood = false;

        #endregion

        #region private functions
        private void DataBindOneTime()
        {
            cmbMaterial.SelectedIndex = 0;
            _aimOwnerLocation = AIMManager.GetAIMOwnerLocation(true);
            _aimOwner = AIMManager.GetAIMOwner(true);
			_source = AIMManager.GetSupplier(true);
			

            _aimOwner.Insert
            (
                0,
                new AIMOwnerInfo
                {
                    AIMOwnerID = 0,
					OwnerCode = "--Select--"
                }
            );

            _source.Insert
            (
                0,
				new AIMSupplierInfo
                {
					AIMSupplierID = 0,
					SupplierName = "--Select--"
                }
            );
			
            lueOwner.Properties.DataSource = _aimOwner;
            XtraEditors.LoadLookUpEdit(ref lueOwner, "AIMOwnerID", "OwnerCode");
            lueOwner.Properties.Columns["OwnerCode"].Visible = true;

            lueSource.Properties.DataSource = _source;
			XtraEditors.LoadLookUpEdit(ref lueSource, "AIMSupplierID", "SupplierName");
			lueSource.Properties.Columns["SupplierName"].Visible = true;

			if (_aimOwner.Count == 2)
            {
                //selecting value if there is only one
                lueOwner.EditValue = _aimOwner[1].AIMOwnerID;
            }
            if (_source.Count == 2)
            {
				lueSource.EditValue = _source[1].AIMSupplierID;
            }
        }
        private void DataBindSourceLocation()
        {
            //Int32 sourceLocationID = 0;
            Int32 sourceID = 0;
            if (lueSource.EditValue != null)
            {
                Int32.TryParse(lueSource.EditValue.ToString(), out sourceID);
            }
			_sourceLocation = AIMManager.GetSupplierLocationForSupplier(sourceID);

            _sourceLocation.Insert
            (
                0,
				new AIMSupplierLocationInfo
                {
					AIMSupplierLocationID = 0,
					SupplierLocationName = "--Select--"
                }
            );
            lueSourceLocation.Properties.DataSource = _sourceLocation;
			XtraEditors.LoadLookUpEdit(ref lueSourceLocation, "AIMSupplierLocationID", "SupplierLocationName");
			lueSourceLocation.Properties.Columns["SupplierLocationName"].Visible = true;
            if (_sourceLocation.Count == 2)
            {
                //selecting value if there is only one
				lueSourceLocation.EditValue = _sourceLocation[1].AIMSupplierLocationID;
            }
        }
        private void DataBindOwnerLocation()
        {
            Int32 OwnerID = 0;
            if (lueOwner.EditValue != null)
            {
                Int32.TryParse(lueOwner.EditValue.ToString(), out OwnerID);
            }

            _aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(OwnerID);

            _aimOwnerLocation.Insert
            (
                0,
                new AIMOwnerLocationInfo
                {
                    AIMOwnerLocationID = 0,
					LocationCode = "--Select--"
                }
            );
            lueOwnerLocation.Properties.DataSource = _aimOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueOwnerLocation, "AIMOwnerLocationID", "LocationCode");
            lueOwnerLocation.Properties.Columns["LocationCode"].Visible = true;
            if (_aimOwnerLocation.Count == 2)
            {
                //selecting value if there is only one
                lueOwnerLocation.EditValue = _aimOwnerLocation[1].AIMOwnerLocationID;
            }
        }
        #endregion

        public SampleInfoForProcessResult()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }
		
		private void SampleInfoForProcessResult_Load(object sender, EventArgs e)
		{
			try
			{

				DataBindOneTime();
				SetToolTips();

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
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
        
		private void lueOwner_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataBindOwnerLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueSource_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataBindSourceLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            try
            {
                Int32.TryParse(lueOwner.EditValue.ToString(), out aimOwnerID);
                Int32.TryParse(lueOwnerLocation.EditValue.ToString(), out aimOwnerLocationID);
                Int32.TryParse(lueSource.EditValue.ToString(), out sourceID);
                Int32.TryParse(lueSourceLocation.EditValue.ToString(), out sourceLocationID);				
                AnalyzerMaterialType = cmbMaterial.SelectedItem.ToString();

                if (aimOwnerID == 0)
                {
                    MessageBox.Show("Please select owner.", "warning...");
					lueOwner.Focus();
					lueOwner.ShowPopup();
                    return;
                }
                if (aimOwnerLocationID == 0)
                {
                    MessageBox.Show("Please select owner location.", "warning...");
					lueOwnerLocation.Focus();
					lueOwnerLocation.ShowPopup();
                    return;
                }
                if (sourceID == 0)
                {
                    MessageBox.Show("Please select supplier.", "warning...");
					lueSource.Focus();
					lueSource.ShowPopup();
                    return;
                }
                if (sourceLocationID == 0)
                {
                    MessageBox.Show("Please select supplier location.", "warning...");
					lueSourceLocation.Focus();
					lueSourceLocation.ShowPopup();
                    return;
                }				
                if (AnalyzerMaterialType.Length == 0)
                {
                    MessageBox.Show("Please select material type.", "warning...");					
					cmbMaterial.Focus();				
                    return;
                }

                allValueAreGood = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}