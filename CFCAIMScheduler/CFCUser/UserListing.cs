using CFCUser.Entity;
using CFCUser.Manager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCUser;
using Core.Library.DevExpress;

using Core.Library.SQLDatabaseConnection;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using CFCAIM;
using CFCAIM.Maintenance;
using CFCAIMCore;

namespace CFCUser
{
    public partial class UserListing : Form
    {

        Global _global = new Global();
        UserInfo _loginUser = new UserInfo();
        List<RolesInfo> _loginUserRoles = new List<RolesInfo>();
        Int32 _selectedUserNumber = 0;
        UserInfo _selectedUser = new UserInfo();
        List<RolesInfo> _SelectedUserRoles = new List<RolesInfo>();
        List<RolesInfo> _roles = new List<RolesInfo>();
        List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
        Int32 databaseID = 0;
        Boolean addUserButtonClicked = false;                                               //10/26/22
        List<UserMenuAccessInfo> _header = new List<UserMenuAccessInfo>();        
        AIM aimMaintenance;                                                                 //11/21/22

        //10/6/23 - commented
        //TabOrder tabOrder;                                                                  //10/4/23

        //-----------------------------------------------------------------------------------------

        public UserListing()
        {
            InitializeComponent();
        }

        private void userListing_Load(object sender, EventArgs e)
        {
            SQLDatabaseConnection.LoadConnection(ref databases, true, SQLDatabaseConnection.CFCAIMDatabaseFile);
            databaseID = SQLDatabaseConnection.GetDefaultConnection(ref databases);
            UserManager.IsMultipleDatabase = true;
            UserManager.DatabaseConnection = SQLDatabaseConnection.GetSQLConnection(ref databases, databaseID);

            _loginUser = _global.CFCLoginUser;
            txtLogInUserHeading.Text = "Log In User: " + _loginUser.FirstName + " " + _loginUser.LastName + " (" + _loginUser.UserName + ")";

            _loginUserRoles = UserManager.GetUserRoles(_loginUser.UserName, _loginUser.ApplicationName);

            _roles = UserManager.GetRoles(_global.ApplicationName); //all roles exist            
            if (_roles.Count == 0)
            {
                //CreateDefaultUserRole "Administrator";
                UserManager.CheckInsertDefaultRole(_global.ApplicationName);
                UserManager.CheckInsertDefaultRole(_global.ApplicationName);
                _roles = UserManager.GetRoles(_global.ApplicationName);
            }

            cmbRoles.DataSource = _roles;
            cmbRoles.ValueMember = "RoleName";
            cmbRoles.SelectedIndex = 1;
            
            DataBindSpecificIngredientsGroup();             //10/20/22
            DataBindOwnerLocationGroup();                   //11/22/22
            DataBindSpecificProductsGroup();                //5/18/23
            dataBindGrd();
            dataBindUser();
            dataBindcmbPermissions();            
            setToolTips();                                  //6/19/17
            dataBindUserAccess();                           //8/16/23
            insertUserMenuAccess();                         //8/16/23
            dataBindUserMenuAccess();                       //8/16/23
            DataBindSupplierGroup();                        //8/30/23            
            DataBindTestTypeGroup();                        //10/4/23
            
        }
        private void setControls()
        {
            if (!isAdmin())
            {
                //only admin can update the roles
                grpAdmin.Enabled = false;
            }
            if (_selectedUser != null)
            {
                if (!isAdmin() && _loginUser.CFCUserID != _selectedUser.CFCUserID || !rbActiveRecords.Checked)
                {
                    //user(not administrator) can NOT change the name
                    //and password of other users
                    grpNamePwd.Enabled = false;
                }
                else
                {
                    grpNamePwd.Enabled = true;
                }
            }

            if (_selectedUser != null)
            {
                //each user can see others name, username, roles
                txtNewPwd.Text = "";
                txtCnfrmPwd.Text = "";
                txtUserName.Enabled = false;
                btnSave.Enabled = true;
            }
            else
            {
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtEmailAddress.Text = "";
                txtNewPwd.Text = "";
                txtCnfrmPwd.Text = "";
                txtUserName.Text = "";
                txtC5UserName.Text = "";                            //10/4/23 cm
                grdAdmin.DataSource = null;
                txtUserName.Enabled = true;
                btnSave.Enabled = false;
            }
        }
        private Boolean checkUserInput()
        {
            if (txtFirstName.Text == "" || txtLastName.Text == "")
            {
                MessageBox.Show("Please enter both first name and last name.", "Warning...");
                txtFirstName.Focus();
                return false;
            }
            if (_selectedUserNumber == 0 && txtNewPwd.Text == "")
            {
                MessageBox.Show("Please enter a password.", "User Maintenance");
                txtNewPwd.Focus();
                return false;
            }
            if (txtNewPwd.Text != txtCnfrmPwd.Text)
            {
                MessageBox.Show("New password and confirm password do not match. They must be the same.", "User Maintenance");
                txtNewPwd.Text = "";
                txtCnfrmPwd.Text = "";
                txtNewPwd.Focus();
                return false;
            }
            txtUserName.Text = txtUserName.Text.Trim();
            if (_selectedUserNumber == 0 && txtUserName.Text == "")
            {
                MessageBox.Show("Please enter a username.", "User Maintenance");
                txtUserName.Focus();
                return false;
            }
            if (_selectedUserNumber == 0 && txtUserName.Text.Contains(" "))
            {
                MessageBox.Show("Username cannot not contain spaces.", "User Maintenance");
                txtUserName.Focus();
                return false;
            }
            if (txtEmailAddress.ToString().Length > 0 && txtEmailAddress.Text.Contains(" "))
            {
                MessageBox.Show("Email address cannot contain spaces.", "User Maintenance");
                txtEmailAddress.Focus();
                return false;
            }

            //10/4/23
            if (txtC5UserName.ToString().Length > 0 && txtC5UserName.Text.Contains(" "))
            {
                MessageBox.Show("Concept5 User Name cannot contain spaces.", "User Maintenance");
                txtEmailAddress.Focus();
                return false;
            }

            if (_selectedUserNumber == 0)
            {
                //UserInfo TempUser = new UserInfo();                
                UserInfo TempUser;

                TempUser = UserManager.GetUser(txtUserName.Text);
                if (TempUser != null)
                {
                    //8/25/18
                    //MessageBox.Show("User Name already exists. Please enter a different User Name.", "User Maintenance");
                    if (TempUser.IsActive == false)
                    {
                        MessageBox.Show("This User Name is assigned to an inactive user and cannot be reused. Please enter a different User Name.", "User Maintenance");
                    }
                    else
                    {
                        MessageBox.Show("User Name already exists. Please enter a different User Name.", "User Maintenance");
                    }


                    txtUserName.Focus();
                    return false;
                }
            }
            return true;
        }
        private bool saveUser()
        {
            if (checkUserInput())
            {
                if (_selectedUserNumber == 0)
                {
                    _selectedUser = new UserInfo();
                }
                _selectedUser.CFCUserID = _selectedUserNumber;
                _selectedUser.ApplicationName = _global.ApplicationName;
                _selectedUser.UserName = txtUserName.Text;
                _selectedUser.FirstName = txtFirstName.Text;
                _selectedUser.LastName = txtLastName.Text;
                _selectedUser.EmailAddress = txtEmailAddress.Text;
                _selectedUser.C5UserName = txtC5UserName.Text;                  //10/4/23 cm

                if ((txtCnfrmPwd.Text != "" && txtNewPwd.Text != "") || _selectedUserNumber == 0)
                {
                    _selectedUser.Password = txtNewPwd.Text;
                }
                                
                _selectedUser.SpecificIngredientsGroupID = (Int32)lueGroupCode.EditValue;                           //10/25/22
                _selectedUser.AIMOwnerLocationGroupID = (Int32)lueOwnerLocationGroupCode.EditValue;                 //11/22/22
                _selectedUser.SpecificProductsGroupID = (Int32)lueProductGroupCode.EditValue;                       //5/18/23                
                _selectedUser.AIMSupplierGroupID = (Int32)lueSupplierGroupCode.EditValue;                           //8/30/23
                _selectedUser.AIMSupplierLocationGroupID = (Int32)lueSupplierLocationGroupCode.EditValue;           //8/30/23
                _selectedUser.SampleTestTypeGroupID = (Int32)lueTestTypeGroupCode.EditValue;                        //10/4/23                

                bool isSaved = UserManager.SaveUser(_selectedUser);
                if (isSaved)
                {
                    if (_selectedUserNumber == 0)
                    {
                        dataBindGrd();
                        grdViewUser.FocusedRowHandle = grdViewUser.RowCount - 1;
                    }
                    return true;
                }
                MessageBox.Show("Information could NOT be saved!", "User Maintenance");
            }
            return false;
        }
        private void saveUserAccess()
        {
            if (_selectedUserNumber != 0 || (_selectedUserNumber == 0 && saveUser()))
            {
                //true if: changing role of old user || true if: new user is saved
                UserManager.SaveUserRole(_selectedUser.UserName, _selectedUser.ApplicationName, cmbRoles.Text);
                dataBindUser();
            }
        }
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (isAdmin())
            {
                _selectedUserNumber = 0;
                txtFirstName.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtEmailAddress.Text = string.Empty;
                txtUserName.Text = string.Empty;
                lblLastLoginDate.Text = string.Empty;
                txtC5UserName.Text = string.Empty;                  //10/4/23
                grdAdmin.DataSource = null;
                txtUserName.Enabled = true;
                txtFirstName.Focus();

                //10/26/22
                lueGroupCode.EditValue = 0;
                grdUserAccess.DataSource = null;
                grdMenuAccess.DataSource = null;
                addUserButtonClicked = true;
                
                lueOwnerLocationGroupCode.EditValue = 0;            //11/22/22  
                lueProductGroupCode.EditValue = 0;                  //5/18/23
                lueSupplierGroupCode.EditValue = 0;                 //8/30/23
                lueSupplierLocationGroupCode.EditValue = 0;         //8/30/23
                lueTestTypeGroupCode.EditValue = 0;                 //10/4/23
                
            }
            else
            {
                MessageBox.Show("Only administrator can add a user!", "Warning...");
            }

        }
        private void rbActiveRecords_CheckedChanged(object sender, EventArgs e)
        {
            dataBindGrd();
            dataBindUser();
            grpAdmin.Enabled = true;
            grpNamePwd.Enabled = true;
            setControls();
        }
        private void rbDeletedRecords_CheckedChanged(object sender, EventArgs e)
        {
            dataBindGrd();
            dataBindUser();
            grpAdmin.Enabled = false;
            grpNamePwd.Enabled = false;
        }
        private void repbtnDelete_Click(object sender, EventArgs e)
        {
            if (isAdmin())

            {   //3/4/21
                //UserInfo user = new UserInfo();
                UserInfo user;

                user = (UserInfo)grdViewUser.GetFocusedRow();
                UserManager.DeleteUser(user.CFCUserID);

                //6/18/18 - delete record from PrinterUserDefault (if one exists)
                UserManager.DeletePrinterUserDefault(user.CFCUserID);

                //10/26/22 - delete records from UserMenuAccess table
                UserManager.DeleteUserMenuAccess(user.CFCUserID);

                //10/26/22 - delete froms from UserAccess table
                UserManager.DeleteUserAccess_CFCUserID(user.CFCUserID);

                dataBindGrd();
            }
            else
            {
                MessageBox.Show("Only administrator can delete a user!", "Warning...");
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataBindGrd();
            dataBindUser();

            
            DataBindSpecificIngredientsGroup();         //12/13/22
            DataBindOwnerLocationGroup();               //12/13/22
            DataBindSpecificProductsGroup();            //5/18/23
            DataBindSupplierGroup();                    //8/30/23
            DataBindTestTypeGroup();                    //10/4/23            

        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ShowDialog();
            this.Visible = true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveUser())
            {
                saveRole();
                dataBindUser();

                //10/26/22
                if (addUserButtonClicked == true)
                {
                    //insert records for this user into UserMenuAccess table
                    addUserButtonClicked = false;
                    UserManager.InsertUserMenuAccess(_selectedUserNumber);
                    dataBindUserMenuAccess();
                }
            }
        }
        private void setToolTips()
        {
            ToolTip ttip = new ToolTip();
            ttip.SetToolTip(btnExit, "Exit");
            ttip.SetToolTip(btnRefresh, "Refresh");
            ttip.SetToolTip(btnPrint, "Preview");
            ttip.SetToolTip(btnAddUser, "Add User");
            ttip.SetToolTip(btnSave, "Save User");
            ttip.SetToolTip(btnSaveUserAccess, "Save User Permission");
            ttip.SetToolTip(btnAddRoles, "Add Role");

            //10/24/22
            ttip.SetToolTip(btnSaveGroupCode, "Save Group Codes");

        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            UserMaintenanceHelp help = new UserMaintenanceHelp();
            help.ShowDialog();
        }                                   //10/18/22        


        //-----------------------------------------------------------------------------------------------
        #region Users Panel
        private void dataBindGrd()
        {
            Int32 rowInGrid = 0;
            try
            {
                rowInGrid = grdViewUser.FocusedRowHandle;
            }
            catch (Exception)
            {
            }
            //all users in grid
            grdUser.DataSource = UserManager.GetUser(rbActiveRecords.Checked);
            grdViewUser.FocusedRowHandle = rowInGrid;
        }
        private void grdUsers_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataBindUser();
            dataBindUserAccess();

            //11/1/22 
            insertUserMenuAccess();

            //10/25/22
            dataBindUserMenuAccess();

        }
        #endregion

        //-----------------------------------------------------------------------------------------------
        #region Admin Panel
        private Boolean isAdmin()
        {
            List<RolesInfo> loginUserRole = new List<RolesInfo>();
            loginUserRole = UserManager.GetUserRoles(_loginUser.UserName, _loginUser.ApplicationName);
            for (int i = 0; i < loginUserRole.Count; i++)
            {
                if (loginUserRole[i].RoleName == "Administrator")
                {
                    return true;
                }
            }
            return false;
        }
        private void saveRole()
        {
            if (_selectedUserNumber != 0 || (_selectedUserNumber == 0 && saveUser()))
            {
                //true if: changing role of old user || true if: new user is saved
                UserManager.SaveUserRole(_selectedUser.UserName, _selectedUser.ApplicationName, cmbRoles.Text);
                dataBindUser();
            }
        }
        private void btnAddRoles_Click(object sender, EventArgs e)
        {
            saveRole();
        }
        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            //this button is under role grid
            string RoleName = grdViewRole.GetFocusedRowCellDisplayText(colRoles);
            UserManager.DeleteUserRole(_selectedUser.ApplicationName, _selectedUser.UserName, RoleName);
            dataBindUser();
        }
        #endregion

        //-----------------------------------------------------------------------------------------------
        #region Menu Access
        private void dataBindUser()
        {
            _selectedUserNumber = 0;
            _selectedUser = new UserInfo();
            try
            {                
                if (grdViewUser.RowCount > 0)
                {
                    _selectedUserNumber = (Int32)grdViewUser.GetFocusedRowCellValue(colCFCUserID);
                }
            }
            catch (Exception)
            {
            }
            _selectedUser = UserManager.GetUser(_selectedUserNumber);
            if (_selectedUser == null)
            {
                txtFirstName.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtEmailAddress.Text = string.Empty;
                txtUserName.Text = string.Empty;
                txtC5UserName.Text = string.Empty;                  //10/4/23
                lblLastLoginDate.Text = string.Empty;
                grdAdmin.DataSource = null;
                
                lueGroupCode.EditValue = 0;                         //10/25/22
                lueOwnerLocationGroupCode.EditValue = 0;            //11/11/22
                lueProductGroupCode.EditValue = 0;                  //5/18/23
                lueSupplierGroupCode.EditValue = 0;                 //8/30/23
                lueSupplierLocationGroupCode.EditValue = 0;         //8/30/23
                lueTestTypeGroupCode.EditValue = 0;                 //10/4/23
                

            }
            else
            {
                txtFirstName.Text = _selectedUser.FirstName;
                txtLastName.Text = _selectedUser.LastName;
                txtEmailAddress.Text = _selectedUser.EmailAddress;
                _SelectedUserRoles = UserManager.GetUserRoles(_selectedUser.UserName, _selectedUser.ApplicationName);
                grdAdmin.DataSource = _SelectedUserRoles;
                txtUserName.Text = _selectedUser.UserName;
                txtC5UserName.Text = _selectedUser.C5UserName;                                              //10/4/23

                lblLastLoginDate.Text = "Last Login Date: " + _selectedUser.LastLoginDate.ToString();                
                lueGroupCode.EditValue = _selectedUser.SpecificIngredientsGroupID;                          //10/25/22                
                lueOwnerLocationGroupCode.EditValue = _selectedUser.AIMOwnerLocationGroupID;                //11/22/22                
                lueProductGroupCode.EditValue = _selectedUser.SpecificProductsGroupID;                      //5/18/23                
                lueSupplierGroupCode.EditValue = _selectedUser.AIMSupplierGroupID;                          //8/30/23
                lueSupplierLocationGroupCode.EditValue = _selectedUser.AIMSupplierLocationGroupID;          //8/30/23                
                lueTestTypeGroupCode.EditValue = _selectedUser.SampleTestTypeGroupID;                       //10/4/23
               
            }
            setControls();
        }
        private void dataBindUserAccess()
        {
            List<UserAccessInfo> userAccess;
            {
                userAccess = UserManager.GetUserAccessCFCUserID(_selectedUserNumber);
                grdUserAccess.DataSource = userAccess;
            }
        }
        private void dataBindUserMenuAccess()
        {
            List<UserMenuAccessInfo> userMenuAccess;
            {
                userMenuAccess = UserManager.GetUserMenuAccessCFCUserID(_selectedUserNumber);
                grdMenuAccess.DataSource = userMenuAccess;

            }
        }                                           //10/25/22
        private void insertUserMenuAccess()
        {
            List<UserMenuAccessInfo> userMenuAccess;
            {
                userMenuAccess = UserManager.InsertUserMenuAccess(_selectedUserNumber);
                grdMenuAccess.DataSource = userMenuAccess;
            }
        }                                             //11/1/22
        private void btnSelectAll_Click(object sender, EventArgs e)
        {

            //loop thru grid
            List<UserMenuAccessInfo> menuItem = new List<UserMenuAccessInfo>();
            menuItem = (List<UserMenuAccessInfo>)grdMenuAccess.DataSource;
            if (menuItem != null)
            {

                foreach (var item in menuItem)
                {
                    UserManager.SaveUserMenuAccess(item.UserMenuAccessID, item.CFCUserID, item.MenuItemsID, _loginUser.CFCUserID, true);
                }
                dataBindUserMenuAccess();
            }
        }
        private void btnUnselectAll_Click(object sender, EventArgs e)
        {

            //loop thru grid
            List<UserMenuAccessInfo> menuItem = new List<UserMenuAccessInfo>();
            menuItem = (List<UserMenuAccessInfo>)grdMenuAccess.DataSource;
            if (menuItem != null)
            {

                foreach (var item in menuItem)
                {
                    UserManager.SaveUserMenuAccess(item.UserMenuAccessID, item.CFCUserID, item.MenuItemsID, _loginUser.CFCUserID, false);
                }
                dataBindUserMenuAccess();
            }
        }                           //11/21/22
        private void grdMenuAccessView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                UserMenuAccessInfo access = new UserMenuAccessInfo();
                access = (UserMenuAccessInfo)grdMenuAccessView.GetFocusedRow();
                UserManager.SaveUserMenuAccess(access.UserMenuAccessID, access.CFCUserID, access.MenuItemsID, _loginUser.CFCUserID, access.IsNotAllowed);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        //11/1/22

        //testing        
        private void grdMenuAccessView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            String description = string.Empty;
            if (e.Column == colMenuItemsDescription)
            {
                description = Convert.ToString(grdMenuAccessView.GetRowCellValue(e.RowHandle, colMenuItemsDescription));
                if (description == "Analysis" ||
                    description == "Sample" ||
                    description == "Data Exchange" ||
                    description == "Reports" ||
                    description == "Setup" ||
                    description == "Users" ||
                    description == "Utilities" ||
                    description == "Database")
                {
                    e.Appearance.FontStyleDelta = FontStyle.Bold;
                    e.Appearance.ForeColor = Color.DarkBlue;
                }
            }
        }
        private void grdMenuAccessView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == "colLevel1")
            {

            }
        }
        #endregion

        //-----------------------------------------------------------------------------------------------
        #region Permissions
        private void dataBindcmbPermissions()
        {
            List<PermissionInfo> permissions = new List<PermissionInfo>();
            permissions = UserManager.GetPermissions();
            permissions.Insert
                (
                    0,
                    new PermissionInfo
                    {
                        PermissionID = 0,
                        Description = "--Select--"
                    }
                );

            cmbPermissions.DataSource = permissions;
            cmbPermissions.ValueMember = "Description";
            cmbPermissions.SelectedIndex = 0;


        }
        private void repItemDeleteUserPermission_Click(object sender, EventArgs e)
        {
            //3/4/21
            //UserAccessInfo user = new UserAccessInfo();
            UserAccessInfo user;

            user = (UserAccessInfo)grdUserAccessView.GetFocusedRow();
            UserManager.DeleteUserAccess(user.UserAccessID);

            dataBindUserAccess();

        }
        #endregion

        //-----------------------------------------------------------------------------------------------
        #region Other Information

        private void btnSaveGroupCode_Click(object sender, EventArgs e)
        {
            //7/10/23
            //if ((Int32)lueGroupCode.EditValue > 0)
            //{  
            //    saveUser();                
            //}
            saveUser();
        }                       

        //-----------------------------------------------------------------------------------------------
        #region Specific Ingredients                                                            //10/1/22        
        private void DataBindSpecificIngredientsGroup()
        {
            List<CFCAIMCore.Entity.SpecificIngredientsGroupInfo> group = new List<CFCAIMCore.Entity.SpecificIngredientsGroupInfo>();
            group = CFCAIMCore.Manager.AIMManager.GetSpecificIngredientsGroup(true);
            group.Insert
                (
                0,
                new CFCAIMCore.Entity.SpecificIngredientsGroupInfo
                {
                    SpecificIngredientsGroupID = 0,
                    SpecificIngredientsGroupCode = "--Select--"

                }
                );
            lueGroupCode.Properties.DataSource = group;
            XtraEditors.LoadLookUpEdit(ref lueGroupCode, "SpecificIngredientsGroupID", "SpecificIngredientsGroupCode");
        }
        private void btnSaveUserAccess_Click(object sender, EventArgs e)
        {
            if (cmbPermissions.SelectedIndex != 0)
            {
                //Insert into UserAccess table
                //Int32 test = cmbPermissions.Items[2].PermissionID;
                //not getting correct permission id from cmbPermissions
                // ((CFCUser.Entity.PermissionInfo)(cmbPermissions.SelectedItem)).PermissionID

                Int32 permID = ((CFCUser.Entity.PermissionInfo)(cmbPermissions.SelectedItem)).PermissionID;

                UserManager.SaveUserAccess(0, _selectedUserNumber, permID, _loginUser.CFCUserID);
                dataBindUserAccess();
            }
        }
        private void hyperSpecific_Click(object sender, EventArgs e)
        {
            //open AIM maintenance, set tab to specific ingredients
            try
            {
                AIMGlobal.aimSpecificIngredientsTab = true;
                AIMGlobal.open_form(aimMaintenance = new AIM());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        #endregion

        //-----------------------------------------------------------------------------------------------
        #region Location Groups                                                                 //11/22/22        
        private void DataBindOwnerLocationGroup()
        {
            List<CFCAIMCore.Entity.AIMOwnerLocationGroupInfo> group = new List<CFCAIMCore.Entity.AIMOwnerLocationGroupInfo>();
            group = CFCAIMCore.Manager.AIMManager.GetOwnerLocationGroup(true);
            group.Insert
                (
                0,
                new CFCAIMCore.Entity.AIMOwnerLocationGroupInfo
                {
                    AIMOwnerLocationGroupID = 0,
                    AIMOwnerLocationGroupCode = "--Select--"

                }
                );
            lueOwnerLocationGroupCode.Properties.DataSource = group;
            XtraEditors.LoadLookUpEdit(ref lueOwnerLocationGroupCode, "AIMOwnerLocationGroupID", "AIMOwnerLocationGroupCode");
        }
        private void hyperLocationGroup_Click(object sender, EventArgs e)
        {
            //open AIM maintenance, set tab to owner-location-groups
            try
            {
                AIMGlobal.aimOwnerLocationGroupTab = true;
                AIMGlobal.open_form(aimMaintenance = new AIM());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        #endregion

        //-----------------------------------------------------------------------------------------------
        #region Specific Product                                                                //5/18/23        
        private void DataBindSpecificProductsGroup()
        {
            List<CFCAIMCore.Entity.SpecificProductsGroupInfo> group = new List<CFCAIMCore.Entity.SpecificProductsGroupInfo>();
            group = CFCAIMCore.Manager.AIMManager.GetSpecificProductsGroup(true);
            group.Insert
                (
                0,
                new CFCAIMCore.Entity.SpecificProductsGroupInfo
                {
                    SpecificProductsGroupID = 0,
                    SpecificProductsGroupCode = "--Select--"

                }
                );
            lueProductGroupCode.Properties.DataSource = group;
            XtraEditors.LoadLookUpEdit(ref lueProductGroupCode, "SpecificProductsGroupID", "SpecificProductsGroupCode");
        }
        private void hyperProductSpecific_Click(object sender, EventArgs e)
        {
            //open AIM maintenance, set tab to specific products
            try
            {
                AIMGlobal.aimSpecificProductsTab = true;
                AIMGlobal.open_form(aimMaintenance = new AIM());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        #endregion

        //----------------------------------------------------------------------------------------------
        #region Supplier and SupplierLocation Groups                                            //8/30/23
        private void DataBindSupplierGroup()
        {
            List<CFCAIMCore.Entity.AIMSupplierGroupInfo> group = new List<CFCAIMCore.Entity.AIMSupplierGroupInfo>();
            group = CFCAIMCore.Manager.AIMManager.GetSupplierGroup(true);
            group.Insert
                (
                0,
                new CFCAIMCore.Entity.AIMSupplierGroupInfo
                {
                    AIMSupplierGroupID = 0,
                    AIMSupplierGroupCode = "--Select--"

                }
                );
            lueSupplierGroupCode.Properties.DataSource = group;
            XtraEditors.LoadLookUpEdit(ref lueSupplierGroupCode, "AIMSupplierGroupID", "AIMSupplierGroupCode");
        }
        private void hyperSupplierGroup_Click(object sender, EventArgs e)
        {
            //open AIM maintenance, set tab to supplier group
            try
            {
                AIMGlobal.aimSupplierGroupTab = true;
                AIMGlobal.open_form(aimMaintenance = new AIM());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void hyperSupplierLocationGroup_Click(object sender, EventArgs e)
        {
            //open AIM maintenance, set tab to supplier-location group
            try
            {
                AIMGlobal.aimSupplierLocationGroupTab = true;
                AIMGlobal.open_form(aimMaintenance = new AIM());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        private void lueSupplierGroupCode_EditValueChanged(object sender, EventArgs e)
        {
            List<CFCAIMCore.Entity.AIMSupplierLocationGroupInfo> loc = new List<CFCAIMCore.Entity.AIMSupplierLocationGroupInfo>();
            loc = CFCAIMCore.Manager.AIMManager.GetSupplierLocationGroup_GroupID((Int32)lueSupplierGroupCode.EditValue);
            if (loc.Count > 0)
            {
                DataBindlueSupplierLocationGroupCode(loc[0].AIMSupplierGroupID);
            }
            else
            {
                DataBindlueSupplierLocationGroupCode(0);
            }


        }
        private void DataBindlueSupplierLocationGroupCode(Int32 supplierGroupID)
        {
            List<CFCAIMCore.Entity.AIMSupplierLocationGroupInfo> group = new List<CFCAIMCore.Entity.AIMSupplierLocationGroupInfo>();

            if (supplierGroupID > 0)
            {
                group = CFCAIMCore.Manager.AIMManager.GetSupplierLocationGroupUserListing(true, supplierGroupID);
            }
            group.Insert
                (
                0,
                new CFCAIMCore.Entity.AIMSupplierLocationGroupInfo
                {
                    AIMSupplierLocationGroupID = 0,
                    AIMSupplierGroupCode = "--Select--"

                }
                );
            lueSupplierLocationGroupCode.Properties.DataSource = group;
            XtraEditors.LoadLookUpEdit(ref lueSupplierLocationGroupCode, "AIMSupplierLocationGroupID", "AIMSupplierGroupCode");

        }
        #endregion

        //-----------------------------------------------------------------------------------------------
        #region Test Type Groups                                                                //10/4/23
        private void DataBindTestTypeGroup()
        {
            List<CFCAIMCore.Entity.SampleTestTypeGroupInfo> group = new List<CFCAIMCore.Entity.SampleTestTypeGroupInfo>();
            group = CFCAIMCore.Manager.AIMManager.GetTestTypeGroup(true);
            group.Insert
                (
                0,
                new CFCAIMCore.Entity.SampleTestTypeGroupInfo
                {
                    SampleTestTypeGroupID = 0,
                    SampleTestTypeGroupCode = "--Select--"
                }
                );

            lueTestTypeGroupCode.Properties.DataSource = group;
            XtraEditors.LoadLookUpEdit(ref lueTestTypeGroupCode, "SampleTestTypeGroupID", "SampleTestTypeGroupCode");
        }
        private void hyperTestType_Click(object sender, EventArgs e)
        {            
            try
            {
                AIMGlobal.testTypeGroupID = (Int32)lueTestTypeGroupCode.EditValue;
                AIMGlobal.testTypeGroupTab = true;

                //10/6/23 - moved TestTypeGroup maintenance to AIM Maintenance screen
                //AIMGlobal.open_form(tabOrder = new TabOrder());
                AIMGlobal.open_form(aimMaintenance = new AIM());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
        #endregion
              

        #endregion


    }
}