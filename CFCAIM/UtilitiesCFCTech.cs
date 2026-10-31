using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Manager;
using System.Data.SqlClient;
using Core.Library.SQLDatabaseConnection;
using System.Data;
using Core.Library.DevExpress;
using System.Linq;

namespace CFCAIM
{
    public partial class UtilitiesCFCTech : Form
	{

		MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];
        private List<SQLDatabaseInfo> _databases = new List<SQLDatabaseInfo>();
        List<SQLDatabaseInfo> _dbase = new List<SQLDatabaseInfo>();
        String _strConn = "";

        public UtilitiesCFCTech()
		{
			InitializeComponent();
			CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
		}

		private void UtilitiesCFCTech_Load(Object sender, EventArgs e)
		{			
            DataBindLueConnectName();
            SetToolTips();
            txtPassword.Text = "";
            txtScript.BackColor = System.Drawing.Color.LightGray;
            btnRun.Visible = false;
            btnClear.Visible = false;
            lblDatabase.Visible = false;
            lueConnectName.Visible = false;
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }        

        private void DataBindLueConnectName()
        {
            Int32 id = 0;            
            SQLDatabaseConnection.LoadConnection(ref _databases, true, SQLDatabaseConnection.CFCAIMDatabaseFile);
            id = SQLDatabaseConnection.GetDefaultConnection(ref _databases);
            lueConnectName.Properties.DataSource = _databases;
            XtraEditors.LoadLookUpEdit(ref lueConnectName, "DatabaseID", "ConnectName");
            lueConnectName.EditValue = id;            
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

        private void SetToolTips()
        {
            System.Drawing.Icon ico = Properties.Resources.CFCAIM;
            this.Icon = ico;
            ToolTip ttip = new ToolTip();
            foreach (Control ctrl in pnlTopBtns.Controls)
            {
                ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
            }
            ttip.SetToolTip(btnExit, "Exit");
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
        }

        private void txtPassword_Leave(Object sender, EventArgs e)
		{           
			if (txtPassword.Text == "CFCTech")
			{
				txtScript.BackColor = System.Drawing.Color.White;
				btnRun.Visible = true;
                btnClear.Visible = true;
                lblDatabase.Visible = true;
                lueConnectName.Visible = true;
                lblPassword.Visible = false;
				txtPassword.Visible = false;	
                			
			}			
		}

        private void btnRun_Click(Object sender, EventArgs e)
        {
			try
			{                
                if (txtScript.Text.Length == 0)
                {
                    return;
                }
                if (lueConnectName.Text.Length == 0)
                {
                    MessageBox.Show("Please select a Database Connection.", "Message...", MessageBoxButtons.OK);
                    lueConnectName.Focus();
                    return;
                }


                //CHANGE TO SQL DATABASE IN DROPDOWN						
                ChangeSqlDatabaseToDropdown(lueConnectName.Text);                

                //RUN SELECT SCRIPT
                if (txtScript.Text.Substring(0, 6).ToUpper() == "SELECT")
				{					
					txtResults.Visible = true;                  

					//SEND SELECT SCRIPT TO DATABASE										
					SqlConnection conn = new SqlConnection(_strConn);
					SqlCommand cmd = new System.Data.SqlClient.SqlCommand();					
					cmd.CommandText = txtScript.Text;
					cmd.CommandType = System.Data.CommandType.Text;					
					DataTable table = new DataTable();			   
					cmd.Connection = conn;	

					//CLEAR GRID COLUMNS
					grdResultsView.Columns.Clear();	
					try
					{	 						
						conn.Open();
						table.Load(cmd.ExecuteReader(), LoadOption.OverwriteChanges);																																													
						grdResults.DataSource = table;	
						conn.Close();  
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
					}			 					
				}

                //---------------------------------------------------------------------
                //RUN UPDATE/INSERT/DELETE SCRIPT
                else
                {				  
					txtResults.Visible = false;

                    //REMOVE CARRIAGE RETURNS, TABS
                    txtScript.Text = txtScript.Text.Replace("\t", " ");
                    txtScript.Text = txtScript.Text.Replace("\n", " ");                    
                    txtScript.Text = txtScript.Text.Replace("\r",Environment.NewLine);
                    txtScript.Text = txtScript.Text.Replace("\r\n", " ");
                    
                    //RUN SQL SCRIPT
                    AIMManager.RunSqlScript(txtScript.Text);				   
					MessageBox.Show("Complete", "Information...");
					txtResults.Visible = true;
				}

                //CHANGE BACK TO ORIGINAL DATABASE CONNECTION                
                ///DataBindLueConnectName();
                //ChangeBackToOriginalSqlConn();

                //lueConnectName.Text = ""; //hide connection name to force user to select (even though it really is getting set to the default)

            }
            catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}

        //7/25/17
        private void ChangeSqlDatabaseToDropdown(String connectionName)
        {
            String serverName = "";
            Boolean isSqlAuth = false;
            String user = "";
            String password = "";
            String database = "";

            //----------------------------------------------------------------------------
            //CHANGE TO SQL DATABASE IN DROPDOWN	
            AIMManager.CloseAIMData();
            List<SQLDatabaseInfo> dbase = new List<SQLDatabaseInfo>();
            SQLDatabaseConnection.LoadConnection(ref dbase, true, SQLDatabaseConnection.CFCAIMDatabaseFile);
            var connections = from g in dbase
                              where g.ConnectName == connectionName
                              select g;
            foreach (var item in connections)
            {
                serverName = item.ServerName;
                isSqlAuth = item.IsSQLAuthentication;
                user = item.User;
                password = item.Password;
                database = item.Database;
            }
            AIMManager.IsMultipleDatabase = true;
            _strConn = AIMManager.DatabaseConnection = SQLDatabaseConnection.GetSQLConnection(serverName, isSqlAuth, user, password, database);
        }

        //7/25/17
        private void ChangeBackToOriginalSqlConn()
        {
                        
            Int32 id = 0;
            SQLDatabaseConnection.LoadConnection(ref _databases, true, SQLDatabaseConnection.CFCAIMDatabaseFile);
            id = SQLDatabaseConnection.GetDefaultConnection(ref _databases);
            lueConnectName.Properties.DataSource = _databases;
            XtraEditors.LoadLookUpEdit(ref lueConnectName, "DatabaseID", "ConnectName");
            lueConnectName.EditValue = id;
            
            //List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
            //Int32 databaseID = 0;
            //Int32.TryParse(lueConnectName.EditValue.ToString(), out databaseID);
            //databases = (List<SQLDatabaseInfo>)lueConnectName.Properties.DataSource;
            //SQLDatabaseConnection.SetDefaultConnection(ref _databases, _databases[0].DatabaseID, SQLDatabaseConnection.CFCAIMDatabaseFile);

            //Need to do this so, when you go back to the other forms, the correct connection is used.
            //AIMManager.IsMultipleDatabase = true;
            //AIMManager.DatabaseConnection = SQLDatabaseConnection.GetSQLConnection(databases[databaseID].ServerName, databases[databaseID].IsSQLAuthentication, databases[databaseID].User, databases[databaseID].Password, databases[databaseID].Database);
            //AIMManager.CloseAIMData();
        }
                
        private void btnResize_Click(Object sender, EventArgs e)
        {            
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void UtilitiesCFCTech_SizeChanged(Object sender, EventArgs e)
        {          
            btnResize.Visible = true;
        }

		private void button1_Click(Object sender, EventArgs e)
		{
			this.Refresh();
		}

		private void btnLogin_Click(Object sender, EventArgs e)
		{
			if (txtPassword.Text == "CFCTech")
			{
				txtScript.BackColor = System.Drawing.Color.White;
				btnRun.Visible = true;
                btnClear.Visible = true;
                lblDatabase.Visible = true;
                lueConnectName.Visible = true;
                lblPassword.Visible = false;
				txtPassword.Visible = false;
				btnLogin.Visible = false;
                ////lueConnectName.Text = "";  //hide connection name to force user to select (even though it really is getting set to the default)

            }
		}

        private void btnClear_Click(Object sender, EventArgs e)
        {
            txtScript.Text = String.Empty;            
            grdResultsView.Columns.Clear();
            txtScript.Focus();
        }
       
    }
}
