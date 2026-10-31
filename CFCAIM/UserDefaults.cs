using System;
using System.Windows.Forms;
using CFCUser.Entity;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using CFCUser.Manager;
using System.Collections.Generic;
using CFCAIM.AIMUtilities;

namespace CFCAIM
{
    public partial class UserDefaults : Form
    {

        UserInfo _loginUser = new UserInfo();
        CFCUser.Global _userGlobal = new CFCUser.Global();
        UserInfo _user = new UserInfo();
        List<UserDefaultsInfo> _userDefaults = new List<UserDefaultsInfo>();
        List<ControlInfo> _controlEntity = new List<ControlInfo>();
        Boolean _needToSave = false;

        Int32 _cfcUserID = 0;

        public UserDefaults()
        {
            InitializeComponent();
        }

        private void UserDefaults_Load(Object sender, EventArgs e)
        {
            _cfcUserID = _userGlobal.CFCLoginUser.CFCUserID;
            _user = new UserInfo();            
            _user = UserManager.GetUser(_cfcUserID);            
            lblName.Text = _user.UserName;
            ClearButtons();
            DataBindDefaults();            
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        
        private void DataBindDefaults()
        {
            try
            {
                //on the Analysis screen, the following objects are turned ON:
                //  rbExtremeOutlierInclude, rbMildOutlierInclude, rbAllRecords, rdbBase, cbDate and cbRecalcNo
                
                _controlEntity = AIMManager.GetControlUsingAllowUserDefault(true);
                _userDefaults = AIMManager.GetUserDefaultsForCFCUserID(_userGlobal.CFCLoginUser.CFCUserID);                
                foreach (var defaultItem in _userDefaults)
                {
                    foreach (var controlName in Controls.All())
                    {
                        if (controlName.Name == defaultItem.ControlName)
                        {
                            if (controlName.AccessibilityObject.Role == AccessibleRole.RadioButton)
                            {
                                ((RadioButton)controlName).Checked = true;
                            }
                            else if (controlName.AccessibilityObject.Role == AccessibleRole.CheckButton)
                            {
                                ((CheckBox)controlName).Checked = true;
                            }

                        }
                        else if (defaultItem.ControlName == "rbRecalcNo")
                        {
                            rbRecalcNo.Checked = true;
                        }
                        else if (defaultItem.ControlName == "rbRecalcYes")
                        {
                            rbRecalcYes.Checked = true;
                        }
                    }
                }
                _needToSave = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
               

        private void ClearButtons()
        {
            rbExtremeOutlierInclude.Checked = false;
            rbExtremeOutlierExclude.Checked = false;
            rbMildOutlierInclude.Checked = false;
            rbMildOutlierExclude.Checked = false;
            rbAllRecords.Checked = false;
            rbOutliersOnly.Checked = false;
            rdbBase.Checked = false;
            rdbDefaultOverlay.Checked = false;
            rdbSpecificOverlay.Checked = false;

            rbRecalcYes.Checked = false;
            rbRecalcNo.Checked = false;

            cbDate.Checked = false;
            cbSampleID.Checked = false;
            cbOwner.Checked = false;
            cbOwnerLocation.Checked = false;
            cbSupplier.Checked = false;
            cbSupplierLocation.Checked = false;
        }

        private void btnExit_Click(Object sender, EventArgs e)
        {
            try
            {
                if (_needToSave == true)
                {
                    DialogResult dr;
                    dr = MessageBox.Show("Changes have not been saved. Close form without saving?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                }
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
            foreach (Control ctrl in pnlTopBtns.Controls)
            {
                ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
            }
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
            ttip.SetToolTip(btnRefresh, "Refresh");
            ttip.SetToolTip(btnSave, "Save Settings");
        }

        private void UserDefaults_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void saveSettings(Int32 controlID)
        {
            try
            {
                            
                UserDefaultsInfo def = new UserDefaultsInfo();
                def.UserDefaultsID = 0;
                def.CFCUserID = _cfcUserID;
                def.FormID = (Int32)EnumFormID.Analysis;
                def.ControlID = controlID;
                def.ValueOnOff = true;
                def.UpdateUserID = _cfcUserID;
                ErrorInfo error = new ErrorInfo();
                error = AIMManager.SaveUserDefaults(ref def);
                if (error.ErrorNumber > 0)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(Object sender, EventArgs e)
        {
            try
            {
           
                Boolean saveRec = false;

                //clear out defaults for user first -- then add new.
                AIMManager.DeleteUserDefaults((Int32)EnumFormID.Analysis, _cfcUserID);
                AIMManager.DeleteUserDefaults((Int32)EnumFormID.Sample, _cfcUserID);

                foreach (var item in _controlEntity)
                {
                    foreach (var controlName in Controls.All())
                    {
                        if (controlName.Name == item.ControlName)
                        {
                            if (controlName.AccessibilityObject.Role == AccessibleRole.RadioButton && ((RadioButton)controlName).Checked == true)
                            {
                                saveRec = true;
                            }
                            else if (controlName.AccessibilityObject.Role == AccessibleRole.CheckButton && ((CheckBox)controlName).Checked == true)
                            {
                                saveRec = true;
                            }
                            if (saveRec == true)
                            {                                
                                saveSettings(item.ControlID);
                            }
                            saveRec = false;
                        }

                        else if (item.ControlName == "rbRecalcNo" || item.ControlName == "rbRecalcYes")
                        {
                            if (item.ControlName == "rbRecalcNo" && rbRecalcNo.Checked == true)
                            {
                                saveRec = true;
                            }

                            if (item.ControlName == "rbRecalcYes" && rbRecalcYes.Checked == true)
                            {
                                saveRec = true;
                            }
                            if (saveRec == true)
                            {
                                saveSettings(item.ControlID);
                            }
                            saveRec = false;
                        }
                    }
                }
                _needToSave = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        

        private void rbExtremeOutlierInclude_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void rbExtremeOutlierExclude_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void rbMildOutlierInclude_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void rbMildOutlierExclude_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void rbAllRecords_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void rbOutliersOnly_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void rbRecalcYes_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void rbRecalcNo_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void rdbBase_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void rdbDefaultOverlay_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void rdbSpecificOverlay_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void cbDate_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void cbSampleID_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void cbOwner_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void cbOwnerLocation_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void cbSupplier_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void cbSupplierLocation_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void btnClearAnalysis_Click(Object sender, EventArgs e)
        {
            ClearButtons();
            _needToSave = true;
        }         

        private void rbLabelPrint2Yes_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }

        private void rbLabelPrint2No_CheckedChanged(Object sender, EventArgs e)
        {
            _needToSave = true;
        }
    }
}
