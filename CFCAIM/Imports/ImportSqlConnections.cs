using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Core.Library.DevExpress;
using Core.Library.SQLDatabaseConnection;
using System.Collections;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;

namespace CFCAIM.Imports
{
    public partial class ImportSqlConnections : Form
	{
		Boolean _skipMsg = false;
		Boolean _change = false;
		Boolean _saved = false;
		private List<SQLDatabaseInfo> _databases = new List<SQLDatabaseInfo>();
		//Boolean _isGoodConnection = false;
        CFCUser.Global _UserGlobal = new CFCUser.Global();
		MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];

		public ImportSqlConnections()
		{
			InitializeComponent();
			CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
		}

		private void ImportSqlConnections_Load(Object sender, EventArgs e)
		{                                  
			SetToolTips();
			SQLDatabaseConnection.LoadConnection(ref _databases, false, SQLDatabaseConnection.CFCAIMDatabaseFile);
			lueDatabase.Properties.DataSource = String.Empty;                        
            DataBindLueConnectName();
            Int32 rtn = 9999;
            if (AIMGlobal.sqlDatabaseConnectionName != "")
			{
				rtn = SearchConnectNameForID(AIMGlobal.sqlDatabaseConnectionName);
				if (rtn != 9999)
				{
                    lueConnectName.EditValue = rtn;                    
				}			   
			}
            _change = false;
            _saved = false;
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
		}

		private Int32 SearchConnectNameForID(String name)
		{
					
			Int32 rtn = 9999;
			Int32 counter = 0;
			foreach (var item in (lueConnectName.Properties.DataSource as IList))
			{
				if (((SQLDatabaseInfo)item).ConnectName == name)
				{
					rtn = counter;					
				}
				else
				{
					counter++;
				}				
			}			
			return rtn;

		}
		private void DataBindLueConnectName()
		{
			List<SQLDatabaseInfo> dbase = new List<SQLDatabaseInfo>();
			for (int i = 0; i < _databases.Count; i++)
			{
				if (_databases[i].ConnectionType == 2 && _databases[i].ConnectName != "New")
				{
					dbase.Insert
					(
						0,
						new SQLDatabaseInfo
						{							
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

			lueConnectName.Properties.DataSource = dbase;
			XtraEditors.LoadLookUpEdit(ref lueConnectName, "DatabaseID", "ConnectName");
			lueConnectName.Properties.Columns["DatabaseID"].Caption = "Connection Name";
		}			

		private void btnSaveConnection_Click(Object sender, EventArgs e)
		{
			String userName = txtUserName.Text;
			String password = txtPassword.Text;
			Int32 id = 0;

            timer1.Stop();
                        
			if (lueDatabase.Text == "")
			{
				MessageBox.Show("Please enter a database name.", "Warning...");
				lueDatabase.Focus();
				return;
			}
            if (txtConnectName.Text == "")
            {
                MessageBox.Show("Please enter a connection name.", "Warning...");
                txtConnectName.Focus();
                return;
            }


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

			Int32.TryParse(lueConnectName.EditValue.ToString(), out id);
			if (SQLDatabaseConnection.ValidSqlConnection(id, txtConnectName.Text, txtServerName.Text, chkSQLServerAuthentication.Checked, ref userName, ref password, lueDatabase.Text, _databases))

			{
				if (id == 0)
				{
					id = _databases.Count;
					_databases.Insert(id, new SQLDatabaseInfo
					{
						DatabaseID = id,
						ConnectName = txtConnectName.Text,
						ServerName = txtServerName.Text,
						IsSQLAuthentication = chkSQLServerAuthentication.Checked,
						IsDefault = false,
						Database = lueDatabase.Text,
						User = userName,
						Password = password,
						ConnectionType = 2,

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
					_databases[id].ConnectionType = 2;

					_databases[id].ProductionOrNon = productionOrNon;
				}

				SQLDatabaseConnection.SaveXmlDatabaseData(ref _databases, SQLDatabaseConnection.CFCAIMDatabaseFile);
                			       

				//Save template 
				ImportConfigInfo importConfig = new ImportConfigInfo();
				CFCUser.Global global = new CFCUser.Global();
				if (AIMGlobal.sqlNewOrEdit == "New")
                {	                                                           
                    importConfig.ImportConfigID = 0;
                    importConfig.Name = AIMGlobal.systemTemplateName;                    
                    importConfig.FileTypeID = (Int32)FileTypeEnum.SQL;
                    importConfig.DataSourceTypeID = AIMGlobal.systemTemplateDataSourceTypeID;                                        
                    importConfig.IsCreateSample = true;                                    
                    importConfig.ConnectName = txtConnectName.Text;
                    importConfig.CFCUserID = global.CFCLoginUser.CFCUserID;		  
                    importConfig.ServerName = txtServerName.Text;
                    importConfig.SqlUserName = txtUserName.Text;
                    importConfig.SqlPassword = txtPassword.Text;
                    importConfig.IsSqlAuthentication = chkSQLServerAuthentication.Checked;
                    importConfig.DatabaseName = lueDatabase.Text;		
                    AIMManager.SaveImportConfig(ref importConfig);                

                    //save file type xref
                    DataSourceTypeRefFileTypeInfo def = new DataSourceTypeRefFileTypeInfo();
                    def.DataSourceTypeRefFileTypeID = 0;
                    def.DataSourceTypeID = AIMGlobal.systemTemplateDataSourceTypeID;
                    def.FileTypeID = (Int32)FileTypeEnum.SQL;
                    def.CFCUserID = _UserGlobal.CFCLoginUser.CFCUserID;
                    ErrorInfo error = new ErrorInfo();
                    error = AIMManager.SaveDataSourceTypeRefFileType(ref def);
                    if (error.ErrorNumber > 0)
                    {
                        return;
                    }
                }
				else
				{
					//else save just the connection name
					AIMManager.SaveImportConfigConnectName(AIMGlobal.importConfigID, txtConnectName.Text, global.CFCLoginUser.CFCUserID);					
				}

				lueConnectName.EditValue = id;
				if (_skipMsg == false)
				{
					MessageBox.Show("Save was successful.", "Message...", MessageBoxButtons.OK);
                    btnSaveConnection.BackColor = SystemColors.Control;
                    _saved = true;					
				}

			}
		}

		private void lueConnectName_EditValueChanged(Object sender, EventArgs e)
		{				
			List<String> databasesList = new List<string>();
			Int32 id = 0;
			pnlSqlAuthentication.Visible = false;
			pnlDatabaseName.Visible = false;
			pnlDatabaseName.Visible = false;
			btnSaveConnection.Visible = false;
			btnDelete.Visible = false;

			Int32.TryParse(lueConnectName.EditValue.ToString(), out id);
			if (id == 0)
			{
				txtServerName.Text = String.Empty;
				chkSQLServerAuthentication.Checked = true;
				txtUserName.Text = String.Empty;
				txtPassword.Text = String.Empty;
				lueDatabase.EditValue = 0;
				txtConnectName.Text = String.Empty;
			}
			else
			{

				btnSaveConnection.Visible = true;
				btnDelete.Visible = true;
				pnlSQLConnection.Visible = true;
				txtServerName.Text = _databases[id].ServerName;
				chkSQLServerAuthentication.Checked = _databases[id].IsSQLAuthentication;
				txtUserName.Text = _databases[id].User;
				txtPassword.Text = _databases[id].Password;	 
				lueDatabase.EditValue = _databases[id].Database;
				txtConnectName.Text = _databases[id].ConnectName;

				if (txtConnectName.Text != "")
				{
					pnlDatabaseName.Visible = true;
				}
				if (_databases[id].IsSQLAuthentication == true)
				{
					pnlSqlAuthentication.Visible = true;
				}														

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

			}
           
		}

		private void TestLoadDropDown(Boolean showMsg)
		{
			List<String> databasesList = new List<string>();
			String userName = txtUserName.Text;
			String password = txtPassword.Text;
            //_isGoodConnection = false;

            if (txtServerName.Text.Length == 0)
			{
				MessageBox.Show("Please enter a server name.", "Warning...", MessageBoxButtons.OK);
				return;
			}

			if (chkSQLServerAuthentication.Checked == true)
			{
				if (txtUserName.Text.Length == 0)
				{
					MessageBox.Show("Please enter a user name.", "Warning...", MessageBoxButtons.OK);
					return;
				}
				else if (txtPassword.Text.Length == 0)
				{
					MessageBox.Show("Please enter a password.", "Warning...", MessageBoxButtons.OK);
					return;
				}
			}

			if (SQLDatabaseConnection.ValidSqlConnection(null, "TEST", txtServerName.Text, chkSQLServerAuthentication.Checked, ref userName, ref password, null, _databases))
			{
				lblConnection.Visible = true;
				if (SQLDatabaseConnection.TestSqlConnection(txtServerName.Text, chkSQLServerAuthentication.Checked, txtUserName.Text, txtPassword.Text, "", showMsg))

				{
					SQLDatabaseConnection.GetUserDatabases(txtServerName.Text, chkSQLServerAuthentication.Checked, txtUserName.Text, txtPassword.Text, "", ref databasesList);
					lueDatabase.Properties.DataSource = databasesList;
					//_isGoodConnection = true;
					pnlDatabaseName.Visible = true;
					lblConnection.Text = "Connected";
					lblConnection.ForeColor = Color.RoyalBlue;
                    btnSaveConnection.Visible = true;
                    btnDelete.Visible = true;

                }
				else
				{
					lblConnection.Text = "Connection Failed";
					lblConnection.ForeColor = Color.Red;                    

                }
			}
		}

		private void btnDelete_Click(Object sender, EventArgs e)
		{
			SQLDatabaseInfo database = new SQLDatabaseInfo();
			Int32 id = 0;

			if (MessageBox.Show("Are you sure you want to delete this connection.", "Message...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				Int32.TryParse(lueConnectName.EditValue.ToString(), out id);
				database = _databases[id];
				_databases.Remove(database);
				SQLDatabaseConnection.SaveXmlDatabaseData(ref _databases, SQLDatabaseConnection.CFCAIMDatabaseFile);
				_databases = new List<SQLDatabaseInfo>();

				SQLDatabaseConnection.LoadConnection(ref _databases, false, SQLDatabaseConnection.CFCAIMDatabaseFile);
				lueConnectName.EditValue = 0;
			}
		}

		private void SetToolTips()
		{
			try
			{
				Icon ico = Properties.Resources.CFCAIM;
				this.Icon = ico;
				ToolTip ttip = new ToolTip();
				foreach (Control ctrl in pnlTopBtns.Controls)
				{
					ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
				}				
				ttip.SetToolTip(btnResize, "Back to Normal Screen Size");	 
				ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
				ttip.SetToolTip(btnSaveConnection, "Save Database Connection");
				ttip.SetToolTip(btnDelete, "Delete Database Connection");

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}			   
		private void btnExit_Click(Object sender, EventArgs e)
		{
			try
			{
				if (_change == true && _saved == false)
				{
					DialogResult dr;
                    
					dr = MessageBox.Show("WARNING - Changes have NOT been saved. Close form without saving?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (dr == DialogResult.No)
					{
						return;
					}
				}

                timer1.Stop();
                Close();
				ImportData importData;
				AIMGlobal.open_form(importData = new ImportData());


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
		private void ImportSqlConnections_SizeChanged(Object sender, EventArgs e)
		{
			btnResize.Visible = true;
		}

		private void btnNew_Click(Object sender, EventArgs e)
		{
            pnlSQLConnection.Visible = true;
            panelServer.Visible = true;

			//7/6/17 - fill in these 4 with what is currently being used - to help out user						
			Int32 id = 0;
			Int32.TryParse(_mainMenu.lueDatabase.EditValue.ToString(), out id);
			id++;   //add 1 to id because _mainMenu.lueDatabase has added id of 0(New)		

            //8/23/17 - commented
            //txtServerName.Text = _databases[id].ServerName;
            //chkSQLServerAuthentication.Checked = _databases[id].IsSQLAuthentication;
            //txtUserName.Text = _databases[id].User;
            //txtPassword.Text = _databases[id].Password;
            chkSQLServerAuthentication.Checked = false;


            lueDatabase.EditValue = 0;
            txtConnectName.Text = String.Empty;
            txtServerName.Focus();

        }

        private void txtServerName_TextChanged(Object sender, EventArgs e)
        {
            _change = true;
        }

        private void txtUserName_TextChanged(Object sender, EventArgs e)
        {
            _change = true;
        }

        private void txtPassword_TextChanged(Object sender, EventArgs e)
        {
            _change = true;
        }

        private void lueDatabase_EditValueChanged(Object sender, EventArgs e)
        {
            _change = true;
            timer1.Start();
        }

        private void txtConnectName_TextChanged(Object sender, EventArgs e)
        {
            _change = true;
            timer1.Start();
        }

        private void chkSQLServerAuthentication_CheckedChanged(Object sender, EventArgs e)
        {
            pnlSqlAuthentication.Visible = false;
            if (chkSQLServerAuthentication.Checked == true)
            {
                pnlSqlAuthentication.Visible = true;
            }
        }

        private void btnTestConnection_Click(Object sender, EventArgs e)
        {
            TestLoadDropDown(true);
        }

        private void timer1_Tick(Object sender, EventArgs e)
        {
            if (btnSaveConnection.BackColor == SystemColors.Control)
            {
                btnSaveConnection.BackColor = SystemColors.Info;
            }
            else
            {
                btnSaveConnection.BackColor = SystemColors.Control;
            }
        }
    }
}
