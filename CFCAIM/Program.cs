using CFCAIM.Maintenance;
using CFCAIMCore;
using CFCAIMCore.Manager;
using CFCFormsControlLibrary;
using Core.Library.SQLDatabaseConnection;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CFCAIM
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //MessageBox.Show("Step1.  ", "Msg...", MessageBoxButtons.OK);


            String[] arguments = Environment.GetCommandLineArgs();
            List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
            String demoInstanceName = Properties.Settings.Default.DemoInstanceName;
            String demoDatabaseName = Properties.Settings.Default.DemoDatabaseName;
            String output = String.Empty;
            String source;
            String destination;
            Boolean isFatal = false;

            //12/6/21 - commented, moved to CFCUser project, Logon screen, under "Sign In" button
            //Boolean isGood = false;
            //String msg;
            //String saveConnection = "";

            //MainMenu mainmenu;

            //MessageBox.Show("Step2.  ", "Msg...", MessageBoxButtons.OK);

            //This is for when you run it from the schedule task
            if (arguments.Length > 1 && arguments[1].ToString().ToUpper() == "FOSS")
            {
                Application.Run(new MockNIR());
            }
            else
            {
                if (demoInstanceName.Length > 9)
                {
                    if (demoInstanceName.Substring(6, 4) == "DEMO")
                    {
                        try
                        {
                            if (SqlLocalDB("info") == false)   //Check to see if SQLLocalDB is installed 
                            {
                                CFCAIMCore.Manager.AIMManager._host = demoInstanceName;
                                if (SqlLocalDB("info", demoInstanceName, ref output) == false)
                                {
                                    output = String.Empty;
                                    if (SqlLocalDB("create", demoInstanceName, ref output) == true)
                                    {
                                        isFatal = true;
                                    }
                                    else
                                    {
                                        isFatal = true;
                                        SqlDatabase database = (SqlDatabase)EnterpriseLibraryContainer.Current.GetInstance<Database>("DemoConnectionMasterString");
                                        using (DbCommand cmd = database.GetSqlStringCommand("Select * from SysDatabases"))
                                        {
                                            using (IDataReader reader = database.ExecuteReader(cmd))
                                            {
                                                while (reader.Read())
                                                {
                                                    if (reader.GetValue(0).ToString().ToUpper().IndexOf(demoDatabaseName) > 0)
                                                    {
                                                        isFatal = false;
                                                    }
                                                }
                                            }
                                        }

                                        if (isFatal == true)
                                        {
                                            isFatal = false;
                                            using (DbCommand cmd = database.GetSqlStringCommand("ALTER LOGIN [sa] WITH PASSWORD=N'Sql2012!!'"))
                                            {
                                                Int32 result = database.ExecuteNonQuery(cmd);
                                            }

                                            using (DbCommand cmd = database.GetSqlStringCommand("ALTER LOGIN [sa] ENABLE"))
                                            {
                                                Int32 result = database.ExecuteNonQuery(cmd);
                                            }

                                            source = Application.StartupPath + @"\Database\" + demoDatabaseName + ".mdf";
                                            destination = Application.StartupPath + @"\" + demoDatabaseName + ".mdf";
                                            if (File.Exists(source))
                                            {
                                                if (File.Exists(destination))
                                                {
                                                    File.Delete(destination);
                                                }
                                                File.Copy(source, destination);

                                                destination = Application.StartupPath + @"\" + demoDatabaseName + "_log.ldf";
                                                if (File.Exists(destination))
                                                {
                                                    File.Delete(destination);
                                                }

                                                database = (SqlDatabase)EnterpriseLibraryContainer.Current.GetInstance<Database>("DemoConnectionString");
                                                using (DbCommand cmd = database.GetSqlStringCommand("dbo.usp_Sample_UPD_StampDate"))
                                                {
                                                    Int32 result = database.ExecuteNonQuery(cmd);
                                                }
                                            }
                                            else
                                            {
                                                isFatal = true;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                isFatal = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Creating demo database fail.  " + ex.Message, "Error...", MessageBoxButtons.OK);
                            isFatal = true;
                        }
                    }
                }

                //--------------------------------------------------------------------------------------------------------------------
                if (isFatal == false)
                {
                    Boolean continu = false;								//added 2/23/17
                    Boolean validUser = false;                              //added 2/23/17


                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    if (File.Exists(SQLDatabaseConnection.CFCAIMDatabaseFile) == false)
                    {
                        Application.Run(new DatabaseConnection());
                    }


                    //Use XML to get database					
                    if (File.Exists(SQLDatabaseConnection.CFCAIMDatabaseFile))
                    {
                        //------------------------------------------------------- added (2/23/17)                       
                        do
                        {
                            continu = TestConnectionForUser(ref validUser);
                        } while (continu == false);

                        if (continu == true && validUser == true)
                        {
                            //-------------------------------------------------------

                            //                  //11/3/22
                            //                  global::CFCLicensing.Manager.LicensingManager.CloseLicensingData();                            

                            //                  global::CFCLicensing.Manager.LicensingManager.IsMultipleDatabase = true;
                            //global::CFCLicensing.Manager.LicensingManager.DatabaseConnection = AIMManager.DatabaseConnection;
                            //                  global::CFCLicensing.Object.Activation.ProductType = global::CFCLicensing.Object.CFCProductType.CFCAIM;
                            //                  CFCLicensing.LicensingChecker CFCLicensing = new CFCLicensing.LicensingChecker();
                            //                  CFCLicensing.Main();

                            //if (global::CFCLicensing.Object.Activation.IsError == false)
                            //{
                            //12/6/21 - moved the Version Check portion to CFCUser Project, Logon screen, under "Sign In" button
                            //////---------------------------------------------------------------                                
                            //////Check SQL version table and program-database version. They must be the same, else exit.  Added 2/20/17				                                                    

                            //////3/4/21 
                            //////VersionInfo version;
                            //////version = AIMManager.GetVersion();
                            //////VersionInfo version = AIMManager.GetVersion();
                            //////1/25/18
                            ////isGood = false;
                            ////saveConnection = "";
                            ////if (version == null)
                            ////{
                            ////    MessageBox.Show("The SQL Version table is empty. Please contact IT.", "Warning ...", MessageBoxButtons.OK);
                            ////}
                            ////else if (version.Version.ToString() != Version.ProgramDatabaseVersion.ToString())
                            ////{
                            ////    msg = "The SQL database version:    " + version.Version.ToString() + "    does not match ";
                            ////    msg += Environment.NewLine + "the Program database:          " + CFCAIM.Version.ProgramDatabaseVersion.ToString();
                            ////    msg += Environment.NewLine + Environment.NewLine + Environment.NewLine;
                            ////    MessageBox.Show(msg, "Warning ...", MessageBoxButtons.OK);
                            ////    //1/25/18
                            ////    saveConnection = AIMManager.DatabaseConnection;
                            ////    //1/25/18
                            ////    DialogResult dr;
                            ////    dr = MessageBox.Show("Do you want to select/create a new connection?", "Warning...", MessageBoxButtons.YesNo);
                            ////    if (dr == DialogResult.Yes)
                            ////    {
                            ////        DatabaseConnection databaseConnection = new DatabaseConnection();
                            ////        databaseConnection.ShowDialog();
                            ////        if (AIMManager.DatabaseConnection == String.Empty)
                            ////        {
                            ////            Application.Exit();
                            ////        }
                            ////        else if (saveConnection == AIMManager.DatabaseConnection)
                            ////        {
                            ////            Application.Exit();
                            ////        }
                            ////        else
                            ////        {
                            ////            isGood = true;
                            ////        }
                            ////    }
                            ////    else
                            ////    {
                            ////        Application.Exit();
                            ////    }
                            ////}
                            ////else
                            ////{
                            ////    isGood = true;
                            ////}
                            //////---------------------------------------------------------------
                            ////if (isGood == true)
                            ////{

                            CFCUser.Manager.UserManager.IsMultipleDatabase = true;
                            CFCUser.Manager.UserManager.DatabaseConnection = AIMManager.DatabaseConnection;
                            CFCUser.Global global = new CFCUser.Global();
                            global.ApplicationName = "CFCAIM";

                            //10/21/22
                            //global.ProgramAppVersion = Version.ProgramAppVersion;

                            //Version.ProgramDatabaseVersion = '24.10.05';
                            CFCUser.Logon logon = new CFCUser.Logon();
                            
                            System.Drawing.Icon ico = Properties.Resources.CFCAIM;
                            logon.Icon = ico;
                            logon.ShowDialog();

                            //4/25/18 - update User's Logged-In Status
                            AIMManager.SaveCFCUsersLoggedInStatus(global.CFCLoginUser.CFCUserID, true);

                            AIMCoreGlobal.CFCUserID = global.CFCLoginUser.CFCUserID;
                            


                            //11/3/22
                            global::CFCLicensing.Manager.LicensingManager.CloseLicensingData();

                            global::CFCLicensing.Manager.LicensingManager.IsMultipleDatabase = true;
                            global::CFCLicensing.Manager.LicensingManager.DatabaseConnection = AIMManager.DatabaseConnection;
                            global::CFCLicensing.Object.Activation.ProductType = global::CFCLicensing.Object.CFCProductType.CFCAIM;
                            CFCLicensing.LicensingChecker CFCLicensing = new CFCLicensing.LicensingChecker();
                            CFCLicensing.Main();

                            if (global::CFCLicensing.Object.Activation.IsError == false)
                            {

                              Application.Run(new MainMenu());
                                
                            }

                            

                            // This doesn't do anything !
                            ////////--------------------------------
                            //////// Create a new object, representing the German culture.  
                            //////CultureInfo culture = CultureInfo.CreateSpecificCulture("de-DE");

                            //////// The following line provides localization for the application's user interface.  
                            //////Thread.CurrentThread.CurrentUICulture = culture;

                            //////// The following line provides localization for data formats.  
                            //////Thread.CurrentThread.CurrentCulture = culture;

                            //////// Set this culture as the default culture for all threads in this application.  
                            //////// Note: The following properties are supported in the .NET Framework 4.5+ 
                            //////CultureInfo.DefaultThreadCurrentCulture = culture;
                            //////CultureInfo.DefaultThreadCurrentUICulture = culture;
                            ////////--------------------------------

                            ////}
                            //  }                            
                        }
                    }
                }
            }
        }


        public static Boolean TestConnectionForUser(ref Boolean validUser)	//added 2/23/17
        {
            List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
            Int32 databaseID;
            Boolean continu = false;

            SQLDatabaseConnection.LoadConnection(ref databases, true, SQLDatabaseConnection.CFCAIMDatabaseFile);
            databaseID = SQLDatabaseConnection.GetDefaultConnection(ref databases);
            AIMManager.IsMultipleDatabase = true;
            AIMManager.DatabaseConnection = SQLDatabaseConnection.GetSQLConnection(ref databases, databaseID);

            //11/30/21 - added check for database count -- ran into error when xml didn't exist
            if (databases.Count > 0)
            {

                //Check if user is valid for this connection
                String connectName = databases[databaseID].Database;
                String serverName = databases[databaseID].ServerName;
                Boolean sqlAuthen = databases[databaseID].IsSQLAuthentication;
                String userName = databases[databaseID].User;
                String password = databases[databaseID].Password;
                validUser = SQLDatabaseConnection.TestSqlConnection(serverName, sqlAuthen, userName, password, connectName, false);
            }


            if (validUser == false)
            {
                DialogResult dr;
                dr = MessageBox.Show("Do you want to select/create a new connection?", "Warning...", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    DatabaseConnection databaseConnection = new DatabaseConnection();
                    databaseConnection.ShowDialog();
                }
                else
                {
                    continu = true;
                    Application.Exit();
                }
            }
            else
            {
                continu = true;
            }

            return continu;

        }

        public static Boolean SqlLocalDB(String activity)
        {
            String output = String.Empty;

            return SqlLocalDB(activity, "", ref output);
        }


        public static Boolean SqlLocalDB(String activity, String instanceName)
        {
            String output = String.Empty;

            return SqlLocalDB(activity, instanceName, ref output);
        }

        public static Boolean SqlLocalDB(String activity, String instanceName, ref String output)
        {
            Boolean isError = false;
            try
            {
                String error = String.Empty;
                String command = "SqlLocalDB.exe";
                ProcessStartInfo info = new ProcessStartInfo(command, " " + activity + " " + instanceName); // Create a new ProcessStartInfo object
                Process p = new Process();                  // Create a new Process
                p.StartInfo = info;                         // Specify the StartInfo
                p.StartInfo.UseShellExecute = false;        // Do not use the OS shell
                p.StartInfo.RedirectStandardOutput = true;  // Allow writing output to the standard output
                p.StartInfo.RedirectStandardError = true;   // Allow writing error to the standard error
                p.StartInfo.CreateNoWindow = true;          // So the windows does not show
                p.Start();                                  // Start the process
                p.WaitForExit();                            // Wait for the process to exit
                StreamReader reader = p.StandardOutput;     // Get Standard Output Stream
                output = reader.ReadToEnd();                // Read Standard Output
                reader.Close();                             // Close stream
                if (output.Length == 0)                     // If no output then display standard error
                {
                    reader = p.StandardError;
                    error = reader.ReadToEnd();
                    MessageBox.Show(error, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    isError = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isError = true;
            }
            return isError;
        }
    }
}