using Core.Library.SQLDatabaseConnection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CFCAIMWorker
{
	public partial class DatabaseConnection : Form
	{
		private List<SQLDatabaseInfo> _databases = new List<SQLDatabaseInfo>();
		private List<String> _databasesList = new List<string>();
		private String _databaseXML = Application.StartupPath + @"\" + "CFCAIMWorkerDatabase.xml";
		private Boolean _isLoad = true;

		CFCUser.Global userGlobal = new CFCUser.Global();

		public DatabaseConnection()
		{
			InitializeComponent();
		}

		private void SwitchType()
		{
			Int32 index = 99;

			if (_isLoad == false)
			{
				_databases = new List<SQLDatabaseInfo>();
				SQLDatabaseConnection.LoadConnection(ref _databases, true, _databaseXML);

				txtID.Text = String.Empty;
				txtServerName.Text = String.Empty;
				txtUserName.Text = String.Empty;
				txtPassword.Text = String.Empty;
				lueDatabase.Text = String.Empty;
				lblConnection.Text = String.Empty;
				groupBox2.Visible = false;

				if (rbNir.Checked == true)
				{
					for (int i = 0; i < _databases.Count; i++)
					{
						if (_databases[i].ConnectName == "NIR")
						{
							index = i;
							break;
						}						
					}

					if (index != 99)
					{
						txtID.Text = index.ToString();
						txtServerName.Text = _databases[index].ServerName;
						txtUserName.Text = _databases[index].User ;
						txtPassword.Text = _databases[index].Password;
						chkSQLServerAuthentication.Checked = _databases[index].IsSQLAuthentication;
						lblConnection.Text = String.Empty;
						TestLoadDropDown(true);
						lueDatabase.EditValue = _databases[index].Database;
					}
				}
				else
				{
					for (int i = 0; i < _databases.Count; i++)
					{
						if (_databases[i].ConnectName == "AIM")
						{
							index = i;
							break;
						}
					}

					if (index != 99)
					{
						txtID.Text = index.ToString();
						txtServerName.Text = _databases[index].ServerName;
						txtUserName.Text = _databases[index].User;
						txtPassword.Text = _databases[index].Password;
						chkSQLServerAuthentication.Checked = _databases[index].IsSQLAuthentication;
						lblConnection.Text = String.Empty;
						TestLoadDropDown(true);
						lueDatabase.EditValue = _databases[index].Database;
					}
				}
			}
		}

		private void SetToolTips()
		{
			ToolTip ttip = new ToolTip();
			ttip.SetToolTip(btnSave, "Save");
			ttip.SetToolTip(btnExit, "Exit");
		}

		private void TestLoadDropDown(Boolean showMsg)
		{
			List<String> database = new List<String>();
			String userName = txtUserName.Text;
			String password = txtPassword.Text;


			if (SQLDatabaseConnection.ValidSqlConnection(null, "CFCAIMWorker", txtServerName.Text, chkSQLServerAuthentication.Checked, ref userName, ref password, null, null))
			{
				lblConnection.Visible = true;
				if (SQLDatabaseConnection.TestSqlConnection(txtServerName.Text, chkSQLServerAuthentication.Checked, txtUserName.Text, txtPassword.Text, "", showMsg))
				{
					_databasesList = new List<string>();
					SQLDatabaseConnection.GetUserDatabases(txtServerName.Text, chkSQLServerAuthentication.Checked, txtUserName.Text, txtPassword.Text, "", ref _databasesList);

					lueDatabase.Properties.DataSource = _databasesList;
					lueDatabase.Properties.NullText = "";
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

		private void DatabaseConnection_Load(object sender, EventArgs e)
		{
			SQLDatabaseConnection.LoadConnection(ref _databases, true, _databaseXML);

			if (_databases.Count == 0)
			{
				txtServerName.Text = String.Empty;
				txtUserName.Text = String.Empty;
				txtPassword.Text = String.Empty;
			}
			else
			{
				txtID.Text = _databases[0].DatabaseID.ToString();
				txtServerName.Text = _databases[0].ServerName;
				txtUserName.Text = _databases[0].User;
				txtPassword.Text = _databases[0].Password;

				if (_databases[0].IsSQLAuthentication == true)
				{
					chkSQLServerAuthentication.Checked = true;
				}
				else
				{
					chkSQLServerAuthentication.Checked = false;
				}

				if (_databases[0].ConnectName == "AIM")
				{
					rbAIM.Checked = true;
				}
				else
				{
					rbNir.Checked = true;
				}

				lueDatabase.Text = String.Empty;
				TestLoadDropDown(false);

				if (_databasesList.Count > 0)
				{
					for (int i = 0; i < _databasesList.Count; i++)
					{
						if (_databases[0].Database == _databasesList[i])
						{
							lueDatabase.EditValue = _databases[0].Database;
							break;
						}
					}
				}
			}

			SetToolTips();
			_isLoad = false;			
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

		private void btnSave_Click(object sender, EventArgs e)
		{
			String databaseFile = String.Empty;
			String connectName = String.Empty;
			String userName = txtUserName.Text;
			String password = txtPassword.Text;
			Int32 id = 0;

			if (rbAIM.Checked == false && rbNir.Checked == false)
			{
				MessageBox.Show("Please pick what type of SQL connection this is.", "Save Failed...", MessageBoxButtons.OK);
			}
			else
			{
				if (rbAIM.Checked == true)
				{
					connectName = "AIM";
				}
				else
				{
					connectName = "NIR";
				}

				if (SQLDatabaseConnection.ValidSqlConnection(null, connectName, txtServerName.Text, chkSQLServerAuthentication.Checked, ref userName, ref password, lueDatabase.Text, null))
				{
					if (txtID.Text.Length == 0)
					{
						id = _databases.Count;
						txtID.Text = id.ToString();
						_databases.Insert(id, new SQLDatabaseInfo
						{
							DatabaseID = id,
							ConnectName = connectName,
							ServerName = txtServerName.Text,
							IsSQLAuthentication = chkSQLServerAuthentication.Checked,
							IsDefault = true,
							Database = lueDatabase.Text,
							User = userName,
							Password = password,
							ConnectionType = 1,
							ProductionOrNon = "P"
						}); 

						SQLDatabaseConnection.SaveXmlDatabaseData(ref _databases, _databaseXML);

						MessageBox.Show("Save was successful.", "Message...", MessageBoxButtons.OK);
					}
					else
					{
						for (int i = 0; i < _databases.Count ; i++)
						{
							if (_databases[i].ConnectName == connectName)
							{
								_databases[i].ServerName = txtServerName.Text;
								_databases[i].IsSQLAuthentication = chkSQLServerAuthentication.Checked;
								_databases[i].IsDefault = true;
								_databases[i].Database = lueDatabase.Text;
								_databases[i].User = userName;
								_databases[i].Password = password;
								_databases[i].ConnectionType = 1;
							}
							SQLDatabaseConnection.SaveXmlDatabaseData(ref _databases, _databaseXML);

							MessageBox.Show("Save was successful.", "Message...", MessageBoxButtons.OK);
						}
					}
				}
			}
		}

		private void btnTest_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtServerName.Text.Length == 0)
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
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void rbNir_CheckedChanged(object sender, EventArgs e)
		{
			if (rbNir.Checked == true )
			{
				SwitchType();
			}
		}

		private void rbAIM_CheckedChanged(object sender, EventArgs e)
		{
			if (rbAIM.Checked == true )
			{
				SwitchType();
			}
		}
	}
}
