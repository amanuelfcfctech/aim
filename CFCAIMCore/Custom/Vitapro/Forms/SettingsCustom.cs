using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

//using CFCAIMCustom.Entity.Vitapro;
//using CFCAIMCustom.Manager;
using CFCAIM.Custom.Vitapro.Entity;
using CFCAIM.Custom.Vitapro.Manager;


//9/16/20
//namespace CFCAIMCustom.Vitapro
namespace CFCAIM.Custom.Vitapro.Forms

{
    public partial class SettingsCustom : Form
    {
        CFCUser.Global userGlobal = new CFCUser.Global();
        Int32 _SystemSettingsCustomID = 0;

        
        public SettingsCustom()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            DataBindlue();
            DataBind();            
            SetToolTips();
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void DataBind()
        {
            SystemSettingsCustomInfo settings = new SystemSettingsCustomInfo();
            settings = CustomManager.GetSystemSettingsCustom();
            if (settings != null)
            {
                _SystemSettingsCustomID = settings.SystemSettingsCustomID;
                lueAimNutrient1.EditValue = settings.AIMNutrientID1_ForDisplay;
                lueAimNutrient2.EditValue = settings.AIMNutrientID2_ForDisplay;
                lueAimNutrient3.EditValue = settings.AIMNutrientID3_ForDisplay;
                lueAimNutrient4.EditValue = settings.AIMNutrientID4_ForDisplay;
            }
        }
        private void DataBindlue()
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
            lueAimNutrient1.Properties.DataSource = aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueAimNutrient1, "AIMNutrientID", "AIMNutrientCode");
            lueAimNutrient1.Properties.Columns["AIMNutrientCode"].Visible = true;
            lueAimNutrient1.Properties.Columns["AIMNutrientName"].Visible = true;

            lueAimNutrient2.Properties.DataSource = aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueAimNutrient2, "AIMNutrientID", "AIMNutrientCode");
            lueAimNutrient2.Properties.Columns["AIMNutrientCode"].Visible = true;
            lueAimNutrient2.Properties.Columns["AIMNutrientName"].Visible = true;

            lueAimNutrient3.Properties.DataSource = aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueAimNutrient3, "AIMNutrientID", "AIMNutrientCode");
            lueAimNutrient3.Properties.Columns["AIMNutrientCode"].Visible = true;
            lueAimNutrient3.Properties.Columns["AIMNutrientName"].Visible = true;

            lueAimNutrient4.Properties.DataSource = aimNutrient;
            XtraEditors.LoadLookUpEdit(ref lueAimNutrient4, "AIMNutrientID", "AIMNutrientCode");
            lueAimNutrient4.Properties.Columns["AIMNutrientCode"].Visible = true;
            lueAimNutrient4.Properties.Columns["AIMNutrientName"].Visible = true;


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

            ToolTip ttip = new ToolTip();                       
            ttip.SetToolTip(btnResize, "Volver al tamaño normal de la pantalla");            
            ttip.SetToolTip(btnExit, "Salida");
            ttip.SetToolTip(btnRefresh, "Refrescar");
            ttip.SetToolTip(btnSave, "Salvar");



        }
               

        private void btnSave_Click(object sender, EventArgs e)
        {
            //String msg = "";

            //if ((Int32)lueAimNutrient1.EditValue == 0)
            //{
            //    msg = "Por favor seleccione el codigo de Proetin.";
            //    msg = msg + Environment.NewLine + Environment.NewLine;
            //    msg = msg + "(Please select Protein Code.)";
            //    MessageBox.Show(msg, "Advertencia...", MessageBoxButtons.OK);
                
            //    lueAimNutrient1.Focus();
            //    lueAimNutrient1.ShowPopup();
            //    return;
            //}

            SystemSettingsCustomInfo settings = new SystemSettingsCustomInfo();
            settings.SystemSettingsCustomID = _SystemSettingsCustomID;
            settings.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            settings.AIMNutrientID1_ForDisplay = (Int32)lueAimNutrient1.EditValue;
            settings.AIMNutrientID2_ForDisplay = (Int32)lueAimNutrient2.EditValue;  //9/27/19
            settings.AIMNutrientID3_ForDisplay = (Int32)lueAimNutrient3.EditValue;  //9/27/19
            settings.AIMNutrientID4_ForDisplay = (Int32)lueAimNutrient4.EditValue;  //9/27/19

            ErrorInfo error = new ErrorInfo();
            error = CustomManager.SaveSystemSettingsCustom(ref settings);
            if (error.ErrorNumber > 0)
            {
                return;
            }
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void SettingsCustom_SizeChanged(object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void lueAimNutrient1_EditValueChanged(object sender, EventArgs e)
        {
            if (lueAimNutrient1.GetColumnValue("AIMNutrientName") != null)
            {
                txtNutrientName1.Text = lueAimNutrient1.GetColumnValue("AIMNutrientName").ToString();
            }
            
            if ((Int32)lueAimNutrient1.EditValue != 0)
            {
                btnSave.PerformClick();
            }
            
        }

        //9/27/19
        private void lueAimNutrient2_EditValueChanged(object sender, EventArgs e)
        {
            if (lueAimNutrient2.GetColumnValue("AIMNutrientName") != null)
            {
                txtNutrientName2.Text = lueAimNutrient2.GetColumnValue("AIMNutrientName").ToString();
            }            
            if ((Int32)lueAimNutrient2.EditValue != 0)
            {
                btnSave.PerformClick();
            }
            
            
        }

        private void lueAimNutrient3_EditValueChanged(object sender, EventArgs e)
        {
            if (lueAimNutrient3.GetColumnValue("AIMNutrientName") != null)
            {
                txtNutrientName3.Text = lueAimNutrient3.GetColumnValue("AIMNutrientName").ToString();
            }
            if ((Int32)lueAimNutrient3.EditValue != 0)
            {
                btnSave.PerformClick();
            }
            
        }

        private void lueAimNutrient4_EditValueChanged(object sender, EventArgs e)
        {
            if (lueAimNutrient4.GetColumnValue("AIMNutrientName") != null)
            {
                txtNutrientName4.Text = lueAimNutrient4.GetColumnValue("AIMNutrientName").ToString();
            }
            if ((Int32)lueAimNutrient4.EditValue != 0)
            {
                btnSave.PerformClick();
            }
            
        }
    }
}