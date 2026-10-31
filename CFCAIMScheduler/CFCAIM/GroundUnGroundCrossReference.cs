using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CFCAIMCore.Entity.Adisseo;
using CFCAIMCore.Entity;
using Core.Library.DevExpress;

namespace CFCAIM
{
    public partial class GroundUnGroundCrossReference : Form
    {
        CFCUser.Global userGlobal = new CFCUser.Global();

        public GroundUnGroundCrossReference()
        {
            InitializeComponent();
        }

        private void GroundUnGroundCrossReference_Load(object sender, EventArgs e)
        {
            DatabindGrid();
            DataBindLues();
            SetToolTips();
            ClearFields();
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void DatabindGrid()
        {                    
            grdGround.DataSource = AIMManager.GetPNEMaterialSampleTypeRefGroundUnGround();           
            
        }

        private void DataBindLues()
        {
            List<GroundUnGroundInfo> ground = new List<GroundUnGroundInfo>();
            ground = AIMManager.GetGroundUnGround();
            ground.Insert
            (
                0,
                new GroundUnGroundInfo
                {
                    GroundUnGroundID = 0,
                    GroundUnGroundString = "--Select--"
                }
            );
            lueGround.Properties.DataSource = ground;
            XtraEditors.LoadLookUpEdit(ref lueGround, "GroundUnGroundID", "GroundUnGroundString");

            List<PNEMaterialSampleTypeInfo> type = new List<PNEMaterialSampleTypeInfo>();
            type = AIMManager.GetPNEMaterialSampleType();
            type.Insert
            (
                0,
                new PNEMaterialSampleTypeInfo
                {
                    PNEMaterialSampleTypeID = 0,
                    PNEMaterialSampleTypeCode = "--Select--"
                }
            );
            lueMaterialSampleType.Properties.DataSource = type;
            XtraEditors.LoadLookUpEdit(ref lueMaterialSampleType, "PNEMaterialSampleTypeID", "PNEMaterialSampleTypeCode");
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
                ttip.SetToolTip(btnAdd, "Add Record");
                ttip.SetToolTip(btnPreview, "Preview Grid Results");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearFields()
        {
            lueMaterialSampleType.EditValue = 0;
            lueGround.EditValue = 0;

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

        private void GroundUnGroundCrossReference_SizeChanged(object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                CFCAIMCore.Global.previewGridResults(grdGround, "Ground/Whole Cross Reference");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Int32)lueMaterialSampleType.EditValue == 0)
                {
                    MessageBox.Show("Please select a Sample Type. ", "Warning...", MessageBoxButtons.OK);
                    lueMaterialSampleType.Focus();
                    lueMaterialSampleType.ShowPopup();
                    return;
                }
                if ((Int32)lueGround.EditValue == 0)
                {
                    MessageBox.Show("Please select a Ground/Whole Description. ", "Warning...", MessageBoxButtons.OK);
                    lueGround.Focus();
                    lueGround.ShowPopup();
                    return;
                }

                List<PNEMaterialSampleTypeRefGroundUnGroundInfo> xref = new List<PNEMaterialSampleTypeRefGroundUnGroundInfo>();
                xref = AIMManager.GetPNEMaterialSampleTypeRefGroundUnGround((Int32)lueMaterialSampleType.EditValue);
                if (xref.Count != 0)
                {
                    MessageBox.Show("A cross reference already exists for this Sample Type. ", "Warning...", MessageBoxButtons.OK);
                    lueMaterialSampleType.Focus();
                    return;
                }

                PNEMaterialSampleTypeRefGroundUnGroundInfo def = new PNEMaterialSampleTypeRefGroundUnGroundInfo();
                def.PNEMaterialSampleTypeRefGroundUnGroundID = 0;
                def.PNEMaterialSampleTypeID = (Int32)lueMaterialSampleType.EditValue;
                def.GroundUnGroundID = (Int32)lueGround.EditValue;
                def.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                ErrorInfo error = new ErrorInfo();
                error = AIMManager.SavePNEMaterialSampleTypeRefGroundUnGround(ref def);
                if (error.ErrorNumber > 0)
                {
                    return;
                }
                DatabindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repbtnDeleteGroundUnGroundXRef_Click(object sender, EventArgs e)
        {
            try
            {
                PNEMaterialSampleTypeRefGroundUnGroundInfo xref = new PNEMaterialSampleTypeRefGroundUnGroundInfo();
                xref = (PNEMaterialSampleTypeRefGroundUnGroundInfo)grdGroundView.GetFocusedRow();
                xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.DeletePNEMaterialSampleTypeRefGroundUnGround(xref.PNEMaterialSampleTypeRefGroundUnGroundID);
                DatabindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
