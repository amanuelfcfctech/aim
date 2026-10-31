using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using CFCAIMCore.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Collections;


namespace CFCAIM
{
    public partial class DeveloperWindow : Form
    {
        public DeveloperWindow()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text.Trim() == "aaaa")
                {
                    tcDevTools.Enabled = true;
                }
                txtCode.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSwitchDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                String select = cboDatabase.SelectedItem.ToString();
                String dataBaseName = "";
                switch (select)
                {
                    case "CFC Tech":
                        dataBaseName = "cfcaim";
                        break;

                    case "Farm Service Elevator":
                        dataBaseName = "cfcaimfse";
                        break;

                    case "Nikki":
                        dataBaseName = "CFCAIMNickki";
                        break;

                    case "New":
						MessageBox.Show("This database will be used with the new applications (empty database). But its not setup yet.", "Warning...");
                        return;
                }

                XmlDocument xmlDoc = new XmlDocument();

                try
                {
                    String applicationPath = @AppDomain.CurrentDomain.BaseDirectory;
                    xmlDoc.Load(applicationPath + "\\CFCAIM.exe.config");
                    IEnumerator ie = xmlDoc.SelectNodes("/configuration/connectionStrings/add").GetEnumerator();

                    while (ie.MoveNext())
                    {
                        //String temp = (ie.Current as XmlNode).Attributes["name"].Value;
                        (ie.Current as XmlNode).Attributes["connectionString"].Value = @"Data Source=CFCServer\SQL2012;Initial Catalog=" + dataBaseName + ";User Id=saAdmin;Password=MsSql11!";                        
                    }

                    xmlDoc.Save(applicationPath + "\\CFCAIM.exe.config");
                    //AIMDataAccess._database = null;
                    //CFCAIMCore.Manager.AIMManager._AIMData = null;
					MessageBox.Show("Database has been switched successfully! Restart application to complete the process.", "Warning...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeveloperWindow_Load(object sender, EventArgs e)
        {
            try
            {
                System.Drawing.Icon ico = Properties.Resources.CFCAIM;
                this.Icon = ico;
                cboDatabase.SelectedIndex = 0;
                //string connectString = "Data Source=(ConnectionString);" + "Integrated Security=true";

                //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectString);

                ////Console.WriteLine("builder.InitialCatalog = " + builder.InitialCatalog);
                //lblDatabaseName.Text = "Database Name: " + builder.InitialCatalog;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}