using CFCLicensing.Manager;
using Core.Library.DevExpress;
using Core.Library.SQLDatabaseConnection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace CFCFormsControlLibrary
{
    public partial class DatabaseConnection : Form
    {
        #region  local variables
        private List<SQLDatabaseInfo> _databases = new List<SQLDatabaseInfo>();
        Boolean _skipMsg = false;
        #endregion

        #region local Functions
        private void TestLoadDropDown(Boolean showMsg)
        {
            List<String> databasesList = new List<string>();
            String userName = txtUserName.Text;
            String password = txtPassword.Text;


            if (SQLDatabaseConnection.ValidSqlConnection(null, txtConnectName.Text, txtServerName.Text, chkSQLServerAuthentication.Checked, ref userName, ref password, null, _databases))
            ////if (SQLDatabaseConnection.ValidSqlConnection(null, txtConnectName.Text, txtServerName.Text, chkSQLServerAuthentication.Checked, ref userName, ref password, null, _databases, "1"))

            {
                lblConnection.Visible = true;
                //12/22/17 -- ADDED FALSE TO END OF THIS -- WAS NOT THERE PRIOR TO VIRUS!
                if (SQLDatabaseConnection.TestSqlConnection(txtServerName.Text, chkSQLServerAuthentication.Checked, txtUserName.Text, txtPassword.Text, "", showMsg))
                {
                    SQLDatabaseConnection.GetUserDatabases(txtServerName.Text, chkSQLServerAuthentication.Checked, txtUserName.Text, txtPassword.Text, "", ref databasesList);
                    lueDatabase.Properties.DataSource = databasesList;
                    pnlSaveDelete.Visible = true;
                    groupBox2.Visible = true;
                    lblConnection.Text = "Connected";
                    lblConnection.ForeColor = Color.RoyalBlue;
                }
                else
                {
                    lblConnection.Text = "Connection Failed";
                    lblConnection.ForeColor = Color.Red;
                }
            }
        }
        private void LoadlueDatabase()
        {
            List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
            Int32 databaseID = 0;

            lueDatabase.Visible = false;
            lblDatabase.Visible = false;

            if (File.Exists(FormsControl.CFCDatabaseFile))
            {
                lueDatabase.Visible = true;
                lblDatabase.Visible = true;
                SQLDatabaseConnection.LoadConnection(ref databases, true, FormsControl.CFCDatabaseFile);
                databaseID = SQLDatabaseConnection.GetDefaultConnection(ref databases);
                lueDatabase.Properties.DataSource = databases;
                XtraEditors.LoadLookUpEdit(ref lueDatabase, "DatabaseID", "ConnectName");
                lueDatabase.EditValue = databaseID;

            }
        }
        #endregion

        #region Forms Functions
        public DatabaseConnection()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
            this.Icon = FormsControl.FormIcon;
        }

        private void btnTest_Click(Object sender, EventArgs e)
        {
            try
            {

                if (txtConnectName.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Database Connection Name or select from the Search dropdown.", "Message...", MessageBoxButtons.OK);
                    txtConnectName.Focus();
                    return;
                }
                else if (txtServerName.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Server Name.", "Message...", MessageBoxButtons.OK);
                    txtServerName.Focus();
                    return;
                }
                else if (chkSQLServerAuthentication.Checked == true && (txtUserName.Text.Length == 0 || txtPassword.Text.Length == 0))
                {
                    MessageBox.Show("Please enter both User-name and Password.", "Message...", MessageBoxButtons.OK);
                    txtUserName.Focus();
                    return;
                }

                TestLoadDropDown(true);
                lueConnectName.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(Object sender, EventArgs e)
        {
            String userName = txtUserName.Text;
            String password = txtPassword.Text;
            Int32 id = 0;

            Int32.TryParse(lueConnectName.EditValue.ToString(), out id);

            //11/30/21
            String productionOrNon = String.Empty;
            if (rbProduction.Checked == true)
            {
                productionOrNon = "P";
            }
            else
            {
                productionOrNon = "N";
            }


            if (SQLDatabaseConnection.ValidSqlConnection(id, txtConnectName.Text, txtServerName.Text, chkSQLServerAuthentication.Checked, ref userName, ref password, lueDatabase.Text, _databases))
            ////if (SQLDatabaseConnection.ValidSqlConnection(id, txtConnectName.Text, txtServerName.Text, chkSQLServerAuthentication.Checked, ref userName, ref password, lueDatabase.Text, _databases, "1"))

            {
                foreach (var item in _databases)
                {
                    item.IsDefault = false;
                }
                if (id == 0)
                {
                    id = _databases.Count;
                    _databases.Insert(id, new SQLDatabaseInfo
                    {
                        DatabaseID = id,
                        ConnectName = txtConnectName.Text,
                        ServerName = txtServerName.Text,
                        IsSQLAuthentication = chkSQLServerAuthentication.Checked,
                        IsDefault = true,
                        Database = lueDatabase.Text,
                        User = userName,
                        Password = password,
                        ConnectionType = 1,

                        //11/30/21
                        ProductionOrNon = "P"

                    });
                }
                else
                {
                    _databases[id].DatabaseID = id;
                    _databases[id].ConnectName = txtConnectName.Text;
                    _databases[id].ServerName = txtServerName.Text;
                    _databases[id].IsSQLAuthentication = chkSQLServerAuthentication.Checked;
                    _databases[id].IsDefault = true;
                    _databases[id].Database = lueDatabase.Text;
                    _databases[id].User = txtUserName.Text;
                    _databases[id].Password = txtPassword.Text;
                    _databases[id].ConnectionType = 1;

                    //11/30/21
                    _databases[id].ProductionOrNon = productionOrNon;

                }

                SQLDatabaseConnection.SaveXmlDatabaseData(ref _databases, FormsControl.CFCDatabaseFile);
                lueConnectName.EditValue = id;
                if (_skipMsg == false)
                {
                    MessageBox.Show("Save was successful.", "Message...", MessageBoxButtons.OK);
                }
            }
        }
        private void DatabaseConnection_Load(Object sender, EventArgs e)
        {
            this.Icon = FormsControl.FormIcon;
            SQLDatabaseConnection.LoadConnection(ref _databases, false, FormsControl.CFCDatabaseFile);
            pnlSaveDelete.Visible = false;
            lueDatabase.Properties.DataSource = String.Empty;
            DataBindLueConnectName();
            //lueConnectName.Properties.DataSource = _databases;
           // MessageBox.Show("here" + _databases.Count.ToString());
           // XtraEditors.LoadLookUpEdit(ref lueConnectName, "DatabaseID", "ConnectName");
            lueDatabase.EditValue = 0;
            //lueConnectName.EditValue = 0;
            lblConnection.Visible = false;
            lueConnectName.Focus();
            SetToolTips();

        }
        private void DataBindLueConnectName()
        {
            List<SQLDatabaseInfo> dbase = new List<SQLDatabaseInfo>();
            for (int i = 0; i < _databases.Count; i++)
            {
                if (_databases[i].ConnectionType == 1 && _databases[i].ConnectName != "New")
                {
                    dbase.Insert
                    (
                        0,
                        new SQLDatabaseInfo
                        {
                            //DatabaseID = _databases[i].DatabaseID,
                            //ConnectName = _databases[i].ConnectName
                            ConnectName = _databases[i].ConnectName,
                            ConnectionType = _databases[i].ConnectionType,
                            Database = _databases[i].Database,
                            DatabaseID = _databases[i].DatabaseID,
                            IsDefault = _databases[i].IsDefault,
                            IsSQLAuthentication = _databases[i].IsSQLAuthentication,
                            Password = _databases[i].Password,
                            ServerName = _databases[i].ServerName,
                            User = _databases[i].User,

                            //11/30/21
                            ProductionOrNon = _databases[i].ProductionOrNon

                        }
                    );
                }
            }

            //6/27/17
            //lueConnectName.Properties.DataSource = _databases;
            lueConnectName.Properties.DataSource = dbase;

            XtraEditors.LoadLookUpEdit(ref lueConnectName, "DatabaseID", "ConnectName");
            lueConnectName.Properties.Columns["DatabaseID"].Caption = "Connection Name";
        }
        private void chkSQLServerAuthentication_CheckedChanged(Object sender, EventArgs e)
        {
            pnlSqlAuthentication.Visible = chkSQLServerAuthentication.Checked;
        }
        private void btnDelete_Click(Object sender, EventArgs e)
        {
            SQLDatabaseInfo database = new SQLDatabaseInfo();
            Int32 id = 0;

            if (MessageBox.Show("Are you sure you want to delete this connection.", "Message...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Int32.TryParse(lueConnectName.EditValue.ToString(), out id);
                database = _databases[id];
                _databases.Remove(database);
                SQLDatabaseConnection.SaveXmlDatabaseData(ref _databases, FormsControl.CFCDatabaseFile);
                _databases = new List<SQLDatabaseInfo>();
                SQLDatabaseConnection.LoadConnection(ref _databases, false, FormsControl.CFCDatabaseFile);
                lueConnectName.EditValue = 0;
            }
        }
        private void btnNew_Click(Object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            lueConnectName.EditValue = 0;
        }
        private void DatabaseConnection_FormClosing(Object sender, FormClosingEventArgs e)
        {
            String msg = "";
            DialogResult dr = new DialogResult();
            if (File.Exists(FormsControl.CFCDatabaseFile) == false)
            {
                dr = MessageBox.Show("Are you sure you want to exit? A database connection must be selected/added before running the program.", "Warning...", MessageBoxButtons.YesNoCancel);
                if (dr != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
            if ((Int32)lueConnectName.EditValue == 0)
            {
                return;
            }

            Int32 databaseID = 0;
            Int32.TryParse(lueConnectName.EditValue.ToString(), out databaseID);
            SQLDatabaseConnection.SetDefaultConnection(ref _databases, databaseID, SQLDatabaseConnection.CFCAIMDatabaseFile);
            LicensingManager.IsMultipleDatabase = true;
            LicensingManager.DatabaseConnection = SQLDatabaseConnection.GetSQLConnection(_databases[databaseID].ServerName, _databases[databaseID].IsSQLAuthentication, _databases[databaseID].User, _databases[databaseID].Password, _databases[databaseID].Database);

            //1/25/18
            //Check SQL version table and program-database version. They must be the same, else exit.  Added 2/20/17				                                                    
            CFCLicensing.Entity.VersionInfo version = new CFCLicensing.Entity.VersionInfo();
            version = LicensingManager.GetVersion();

            if (version == null)
            {
                MessageBox.Show("The SQL Version table is empty. Please contact IT.", "Warning ...", MessageBoxButtons.OK);
                Application.Exit();
            }
            else if (version.Version != FormsControl.ProgramVersion)
            {
                msg = "The SQL database version:    " + version.Version.ToString() + "    does not match ";
                msg += Environment.NewLine + "the Program database:          " + FormsControl.ProgramVersion;
                msg += Environment.NewLine + Environment.NewLine + Environment.NewLine;
                MessageBox.Show(msg, "Warning ...", MessageBoxButtons.OK);

                DialogResult dResult;
                dResult = MessageBox.Show("Do you want to select/create a new connection? If not, the application will close.", "Warning...", MessageBoxButtons.YesNo);
                if (dResult != DialogResult.Yes)
                {
                    FormsControl.DatabaseConnection = String.Empty;
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void lueConnectName_EditValueChanged(Object sender, EventArgs e)
        {
            lblConnection.Visible = false;
            List<String> databasesList = new List<string>();
            Int32 id = 0;

            Int32.TryParse(lueConnectName.EditValue.ToString(), out id);
            if (id == 0)
            {
                txtConnectName.Text = String.Empty;
                txtServerName.Text = String.Empty;
                txtUserName.Text = String.Empty;
                txtPassword.Text = String.Empty;
                chkSQLServerAuthentication.Checked = true;
                pnlSaveDelete.Visible = false;
                groupBox2.Visible = false;
            }
            else
            {
                groupBox1.Visible = true;
                txtConnectName.Text = _databases[id].ConnectName;
                txtServerName.Text = _databases[id].ServerName;
                txtUserName.Text = _databases[id].User;
                txtPassword.Text = _databases[id].Password;
                chkSQLServerAuthentication.Checked = _databases[id].IsSQLAuthentication;
                TestLoadDropDown(false);

                //11/30/21
                if (_databases[id].ProductionOrNon == "P")
                {
                    rbProduction.Checked = true;
                }
                else
                {
                    rbNonProduction.Checked = true;
                }

                lueDatabase.EditValue = _databases[id].Database;
                lueDatabase.Refresh();
                this.Refresh();
                pnlSaveDelete.Visible = true;
                groupBox2.Visible = true;

                _skipMsg = true;
                btnSave.PerformClick();
                _skipMsg = false;
            }
        }

        #endregion
        private void SetToolTips()
        {
            ToolTip ttip = new ToolTip();
            ttip.SetToolTip(btnSave, "Save");
            ttip.SetToolTip(btnDelete, "Delete");
            ttip.SetToolTip(btnExit, "Exit");
            ttip.SetToolTip(btnNew, "New");
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");

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

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }
    }
}
