using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using CFCUser.Data;
using CFCUser.Entity;
using CFCUser.Manager;
using Core.Library.DevExpress;
using Core.Library.SQLDatabaseConnection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Claims;
using CFCAIM;

//10/21/22 - commented
//Task991 using CFCAIM;

namespace CFCUser
{
    public partial class Logon : Form
    {
        Boolean _loadingForm = true;

        //3/15/23
        /////CFCUser.Global _userGlobalDefault = new CFCUser.Global();


        public Logon()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }
			

        #region private variables

        UserInfo _user = new UserInfo();
		Mutex mutex;
		Boolean _bypassCredentialsCheck = false;

        #endregion

        #region private functions

        private bool isInputValid()
        {
            try
            {
                if (txtUserName.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("You have to provide both username and password.", "Warning...");
                    txtUserName.Focus();
                    return false;
                }

                _user = UserManager.GetUser(txtUserName.Text, txtPassword.Text);
                if (_user == null)
                {
                    MessageBox.Show("Incorrect username or password! If you forgot your username or password contact administrator.", "Warning...");
                    txtUserName.Focus();
                    return false;
                }

                if (_user.IsActive == false)
                {
                    MessageBox.Show("This user has been deleted! If you want to re-activate this user contact administrator.", "Warning...");
                    txtUserName.Focus();
                    return false;
                }
                return true;
            }

            //10/17/23
            catch (Exception ex)   
            {
                MessageBox.Show(ex.Message + "Error calling isInputValid. Contact IT.", "Warning...", MessageBoxButtons.OK);
                return false;
            }
        
        }
        #endregion
        #region Form controls
        private void logon_Load(object sender, EventArgs e)
        {
            Global global = new Global();

			try       
			{
				
				txtUserName.Focus();

				if (global.ApplicationName == String.Empty)
				{
					MessageBox.Show("Ask CFCTech to set application name in CFC User global before running the application. Unable to run login app", "warning...");
					Close();
				}
				else
				{
					UserManager.CheckInsertDefaultRole(global.ApplicationName);
					UserManager.CheckInsertDefaultUser(global.ApplicationName);
				}

                //12/6/21
                LoadlueDatabase();

                mutex = Mutex.OpenExisting(global.ApplicationName);
                if (mutex != null)
				{
					MessageBox.Show("CFC AIM application is already running.", "warning...");
					_bypassCredentialsCheck = true;
					Application.Exit();
				}           
            }
			catch (WaitHandleCannotBeOpenedException ex)
			{
				mutex = new Mutex(true, "CFCAIM");
			}		
        }

        //12/6/21
        private void LoadlueDatabase()
        {
            List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
            Int32 databaseID = 0;
            
            if (File.Exists(SQLDatabaseConnection.CFCAIMDatabaseFile))
            {                
                SQLDatabaseConnection.LoadConnection(ref databases, true, SQLDatabaseConnection.CFCAIMDatabaseFile);
                databaseID = SQLDatabaseConnection.GetDefaultConnection(ref databases);
                lueDatabase.Properties.DataSource = databases;
                XtraEditors.LoadLookUpEdit(ref lueDatabase, "DatabaseID", "ConnectName");
                lueDatabase.EditValue = databaseID;
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSignIn.Focus();
                btnSignIn_Click(sender, e);
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            Global global = new Global();

            try
            {

                //Task991 Global global = new Global();

                //12/6/21
                //if (isInputValid() == true)
                //{
                //    _user.LastLoginDate = DateTime.Now;
                //    UserManager.SaveUser(_user);
                //    global.CFCLoginUser = _user;
                //    Close();
                //}
                if (isInputValid() == true)
                {
                    global.CFCLoginUserName = txtUserName.Text;                     //Task991
                }
                global.AIMDatabaseID = (Int32)lueDatabase.EditValue;                //Task991


                //11/3/22
                AIMManager.CloseAIMData();
                UserManager.CloseUserData();



                if (Check_Version() == true)
                {
                    //---------------------------------------------------------------                                         
                    if (isInputValid() == true)
                    {

                        ErrorInfo result = new ErrorInfo();         //Task1115 DB251105
                        String databaseName = String.Empty;         //Task1115 DB251105
                        databaseName = lueDatabase.Text;            //Task1115 DB251105      

                        result = AIMManager.InsertCFCUserLoginLog(_user.CFCUserID, databaseName);       //Task1115 DB251105 
                        _user.LastLoginDate = DateTime.Now;
                        UserManager.SaveUser(_user);
                        global.CFCLoginUser = _user;                                //Task991
                        Close();
                    }
                }
                else
                {
                    global.CFCLoginUser.UserName = txtUserName.Text;                //Task991
                }
            }

            //10/17/23
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error calling btnSignIn_Click. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
        }

        //12/6/21
        private Boolean Check_Version()
        {
            Global global = new Global();
            String msg = String.Empty;
            String saveConnection = String.Empty;
            Boolean isGood = false;

            //---------------------------------------------------------------                                
            //Check SQL version table and program-database version. They must be the same, else exit.  Added 2/20/17				                                                                
            VersionInfo version = AIMManager.GetVersion();

            saveConnection = "";

            //////10/21/22
            ////Global global = new Global();

            if (version == null || version.Version == null)
            {
                MessageBox.Show("The SQL Version table is empty. Please contact IT.", "Warning ...", MessageBoxButtons.OK);
            }

            //10/21/22
            //Task991  else if (version.Version.ToString() != CFCAIM.Version.ProgramDatabaseVersion.ToString())            

            //why isn't global set?!
            else if (version.Version.ToString() != global.ProgramDatabaseVersion.ToString())
            


                    ////else if (version.Version.ToString() != global.ProgramDatabaseVersion.ToString()) 

                    {
                msg = "The SQL database version:    " + version.Version.ToString() + "    does not match ";

                //10/21/22
                //Task991  msg += Environment.NewLine + "the Program database:          " + CFCAIM.Version.ProgramDatabaseVersion.ToString();
                msg += Environment.NewLine + "the Program database:          " + global.ProgramDatabaseVersion.ToString();

                ////msg += Environment.NewLine + "the Program database:          " + global.ProgramDatabaseVersion.ToString();

                msg += Environment.NewLine + Environment.NewLine + Environment.NewLine;
                MessageBox.Show(msg, "Warning ...", MessageBoxButtons.OK);

                //1/25/18
                saveConnection = AIMManager.DatabaseConnection;

                //1/25/18
                DialogResult dr;
                dr = MessageBox.Show("Do you want to select/create a new connection?", "Warning...", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    DatabaseConnection databaseConnection = new DatabaseConnection();
                    databaseConnection.ShowDialog();
                    if (AIMManager.DatabaseConnection == String.Empty)
                    {
                        Application.Exit();

                    }
                    else if (saveConnection == AIMManager.DatabaseConnection)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        isGood = true;
                    }
                }
                else
                {
                    Application.Exit();
                }

            }
            else
            {
                isGood = true;
            }

            return isGood;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            txtUserName.SelectAll();
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            txtUserName.SelectAll();
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
        }

        #endregion

        private void logon_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global global = new Global();

			if (_bypassCredentialsCheck == false)
			{
				if (global.CFCLoginUser.CFCUserID == 0)                      //Task991
                {
					MessageBox.Show("User credentials are needed to use this program. Closing application!", "Warning...");
					Application.Exit();
				}
			}
        }


        private void txtPassword_Leave(object sender, EventArgs e)
        {
            checkUserDefaults();           
        }

        private void checkUserDefaults()
        {
            //12/6/21
            //Check User-Default XML first. 
            //If user exists in the xml, set database to the User-Default. 
            //If user does not exist in the xml, then use the CFCAIMDabase.xml for the connection    
            
            //3/13/23
            //Change this to check the "Users" folder for a folder with the user's name. (In that folder will be the user's default database).

            Int32 databaseID = 0;
            lblDatabase.Visible = false;
            lueDatabase.Visible = false;

            Global global = new Global();

            if (txtUserName.Text == String.Empty || txtPassword.Text == String.Empty)
            {
                return;
            }

            //3/15/23
            // DB 230315 Needed to comment out to compile. global.CFCLoginUser_ForDefault = txtUserName.Text;



            //Load list of databases for dropdownZ
            if (lueDatabase.Properties.DataSource == null)
            {
                List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
                SQLDatabaseConnection.LoadConnection(ref databases, true, SQLDatabaseConnection.CFCAIMDatabaseFile);
                databaseID = SQLDatabaseConnection.GetDefaultConnection(ref databases);
                lueDatabase.Properties.DataSource = databases;
                XtraEditors.LoadLookUpEdit(ref lueDatabase, "DatabaseID", "ConnectName");
                lueDatabase.EditValue = databaseID;
            }

            String userConnection = String.Empty;
            if (File.Exists(Data.UserDataAccess.CFCAIMUserDefaultFile))     //if xml exists ...
            {
                //Get the user's default connection
                userConnection = UserDataAccess.GetUserDefaultDatabaseName(Data.UserDataAccess.CFCAIMUserDefaultFile, txtUserName.Text);
                if (userConnection != String.Empty)
                {
                    //Get ID for default-user database
                    databaseID = UserDataAccess.GetUserDefaultDatabaseID(userConnection);
                    lueDatabase.EditValue = databaseID;

                    AIMManager.CloseAIMData();

                    //Returns connection string (AIMManager.DatabaseConnection) for user's default database
                    UserDataAccess.LoadUserDefaultConnection(userConnection);

                    //Sets database connection                    
                    CFCUser.Manager.UserManager.IsMultipleDatabase = true;
                    CFCUser.Manager.UserManager.DatabaseConnection = AIMManager.DatabaseConnection;
                    _loadingForm = false;
                }
            }

            lblDatabase.Visible = true;
            lueDatabase.Visible = true;
        }
               
        private void txtUserName_Leave(object sender, EventArgs e)
        {
            checkUserDefaults();
        }

        private void lueDatabase_EditValueChanged(object sender, EventArgs e)
        {
            //12/6/21 - Set user-default connection                   
            if (_loadingForm == false)
            {                  
                String userConnection = String.Empty;
                List<UserDefaultFileInfo> userList = new List<UserDefaultFileInfo>();

                if (File.Exists(CFCUser.Data.UserDataAccess.CFCAIMUserDefaultFile))     //if xml exists ...
                {
                    AIMManager.CloseAIMData();

                    //Get connection name for user (from xml file)
                    userConnection = UserDataAccess.GetUserDefaultDatabaseName(UserDataAccess.CFCAIMUserDefaultFile, txtUserName.Text);

                    //-------------------------------------------------------------
                    //CHANGE USER'S DATABASE ON XML
                    if (userConnection != String.Empty)
                    {
                        //Get connection string for lueDatabase.Text
                        UserDataAccess.LoadUserDefaultConnection(lueDatabase.Text);

                        //Sets database connection
                        CFCUser.Manager.UserManager.IsMultipleDatabase = true;
                        CFCUser.Manager.UserManager.DatabaseConnection = AIMManager.DatabaseConnection;

                        //update user-default xml
                        UserDataAccess.LoadUserList(ref userList, CFCUser.Data.UserDataAccess.CFCAIMUserDefaultFile);
                        
                        UserDataAccess.SaveUserDefaultXmlConnection(ref userList, CFCUser.Data.UserDataAccess.CFCAIMUserDefaultFile, txtUserName.Text, lueDatabase.Text, false);
                        
                    }
                    //-------------------------------------------------------------
                    //ADD USER TO XML
                    else
                    {
                        UserDataAccess.LoadUserList(ref userList, CFCUser.Data.UserDataAccess.CFCAIMUserDefaultFile);
                        UserDataAccess.SaveUserDefaultXmlConnection(ref userList, CFCUser.Data.UserDataAccess.CFCAIMUserDefaultFile, txtUserName.Text, lueDatabase.Text, true);
                    }
                }
                else
                {
                    //-------------------------------------------------------------
                    //CREATE XML AND ADD USER

                    AIMManager.CloseAIMData();

                    UserDataAccess.SaveUserDefaultXmlConnection(ref userList, CFCUser.Data.UserDataAccess.CFCAIMUserDefaultFile, txtUserName.Text, lueDatabase.Text, true);

                    //Get connection string for lueDatabase.Text
                    UserDataAccess.LoadUserDefaultConnection(lueDatabase.Text);

                    //Sets database connection
                    CFCUser.Manager.UserManager.IsMultipleDatabase = true;
                    CFCUser.Manager.UserManager.DatabaseConnection = AIMManager.DatabaseConnection;
                    
                }

                
            }
            _loadingForm = false;
        }

        private void lblDatabase_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
