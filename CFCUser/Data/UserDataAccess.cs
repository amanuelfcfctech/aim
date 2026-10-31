using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using CFCUser.Entity;
using Core.Library.Data;
using Core.Library.SQLDatabaseConnection;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using UserAccessInfo = CFCUser.Entity.UserAccessInfo;

namespace CFCUser.Data
{
    public class UserDataAccess
    {
        #region Member Variables
        SqlDatabase _database;
        public static Boolean _isMessageDisplay = true;
        #endregion

        #region Constructors

        //1/19/17
        //public UserDataAccess(string connectionStringName)
        public UserDataAccess(string connectionStringName, String connectionString)
        {
            //1/19/17
            //_database = (SqlDatabase)EnterpriseLibraryContainer.Current.GetInstance<Database>(connectionStringName);
            if (connectionString.Length > 0)
            {
                _database = new SqlDatabase(connectionString);
            }
            else
            {
                _database = (SqlDatabase)EnterpriseLibraryContainer.Current.GetInstance<Database>(connectionStringName);
            }

        }
        #endregion

        #region Get User
        //overloaded
        public List<UserInfo> GetUser(Boolean IsActive)
        {
            List<UserInfo> users = new List<UserInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("cfcUser.usp_CFCUser_SEL_IsActive"))
                {
                    _database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, IsActive);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<UserInfo>.GetGenericCollection(ref users, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to get User. Calling GetUser.  Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return users;
        }
        public UserInfo GetUser(int CFCUserID)
        {
            UserInfo user = new UserInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("cfcUser.usp_CFCUser_SEL_CFCUserID"))
                {
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        user = ObjectFiller<UserInfo>.GetScalar(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to get User. Calling GetUser.  Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return user;
        }
        public UserInfo GetUser(string UserName, string Password)
        {
            UserInfo user = new UserInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("cfcUser.usp_CFCUser_SEL_UserName_Password"))
                {
                    _database.AddInParameter(cmd, "@UserName", SqlDbType.VarChar, UserName);
                    _database.AddInParameter(cmd, "@Password", SqlDbType.VarChar, Password);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        user = ObjectFiller<UserInfo>.GetScalar(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to get User. Calling GetUser. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return user;
        }
        public UserInfo GetUser(string UserName)
        {
            UserInfo user = new UserInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("cfcUser.usp_CFCUser_SEL_UserName"))
                {
                    _database.AddInParameter(cmd, "@UserName", SqlDbType.VarChar, UserName);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        user = ObjectFiller<UserInfo>.GetScalar(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to get User. Calling GetUser. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return user;
        }
        #endregion

        #region Save User
        public Boolean SaveUser(UserInfo user)
        {
            Boolean isSuccessful = true;
            try
            {
                if (user.LastLoginDate == DateTime.MinValue)
                {
                    //this is SQL minimum date
                    user.LastLoginDate = Convert.ToDateTime("1/1/1900 1:00:00 PM");
                }

                using (DbCommand cmd = _database.GetStoredProcCommand("cfcUser.usp_CFCUser_SAV"))
                {
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, user.CFCUserID);
                    _database.AddInParameter(cmd, "@ApplicationName", SqlDbType.VarChar, user.ApplicationName);
                    _database.AddInParameter(cmd, "@UserName", SqlDbType.VarChar, user.UserName);
                    _database.AddInParameter(cmd, "@Password", SqlDbType.VarChar, user.Password);
                    _database.AddInParameter(cmd, "@LastLoginDate", SqlDbType.DateTime, user.LastLoginDate);
                    //passing Password as OldPassword. no idea how procs are dealing with it.
                    _database.AddInParameter(cmd, "@OldPassword", SqlDbType.VarChar, user.Password);
                    _database.AddInParameter(cmd, "@FirstName", SqlDbType.VarChar, user.FirstName);
                    _database.AddInParameter(cmd, "@LastName", SqlDbType.VarChar, user.LastName);
                    _database.AddInParameter(cmd, "@EmailAddress", SqlDbType.VarChar, user.EmailAddress);

                    //10/25/22
                    _database.AddInParameter(cmd, "@SpecificIngredientsGroupID", SqlDbType.Int, user.SpecificIngredientsGroupID);

                    //11/22/22
                    _database.AddInParameter(cmd, "@AIMOwnerLocationGroupID", SqlDbType.Int, user.AIMOwnerLocationGroupID);

                    //5/18/23
                    _database.AddInParameter(cmd, "@SpecificProductsGroupID", SqlDbType.Int, user.SpecificProductsGroupID);

                    //8/30/23
                    _database.AddInParameter(cmd, "@AIMSupplierGroupID", SqlDbType.Int, user.AIMSupplierGroupID);
                    _database.AddInParameter(cmd, "@AIMSupplierLocationGroupID", SqlDbType.Int, user.AIMSupplierLocationGroupID);

                    //10/4/23
                    _database.AddInParameter(cmd, "@SampleTestTypeGroupID", SqlDbType.Int, user.SampleTestTypeGroupID);
                    _database.AddInParameter(cmd, "@C5UserName", SqlDbType.NVarChar, user.C5UserName);

                    //29/05/29          Task1041 DB
                    _database.AddInParameter(cmd, "@TabOrderGroupID", SqlDbType.Int, user.TabOrderGroupID);

                    //Task1042 FG 7/7/26
                    _database.AddInParameter(cmd, "@SampleScreenDisplayGroupID", SqlDbType.Int, user.SampleScreenDisplayGroupID);

                    // task 1155 FG 8/5/26
                    _database.AddInParameter(cmd, "@SampleRequiredFieldGroupID", SqlDbType.Int, user.SampleRequiredFieldGroupID);

                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                isSuccessful = false;
                MessageBox.Show(ex.Message + "\n\nUnable to save User. Calling SaveUser. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return isSuccessful;
        }
        #endregion

        #region Delete User
        public Boolean DeleteUser(int CFCUserID)
        {
            Boolean isSuccessful = true;
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("cfcUser.usp_CFCUser_DEL_CFCUserID"))
                {
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                isSuccessful = false;
                MessageBox.Show(ex.Message + "\n\nUnable to Delete User. Calling DeleteUser. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return isSuccessful;
        }

        //6/18/18
        public Boolean DeletePrinterUserDefault(Int32 cfcUserID)
        {
            Boolean isSuccessful = true;
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterUserDefault_DEL_CFCUserID"))
                {
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                isSuccessful = false;
                MessageBox.Show(ex.Message + "\n\nUnable to delete user printer. Calling DeletePrinterUserDefault. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return isSuccessful;
        }

        //10/26/22
        public Boolean DeleteUserMenuAccess(Int32 cfcUserID)
        {
            Boolean isSuccessful = true;
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_UserMenuAccess_DEL_CFCUserID"))
                {
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                isSuccessful = false;
                MessageBox.Show(ex.Message + "\n\nUnable to user menu access. Calling DeleteUserMenuAccess. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return isSuccessful;
        }

        //10/26/22
        public Boolean DeleteUserAccess_CFCUserID(Int32 cfcUserID)
        {
            Boolean isSuccessful = true;
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_UserAccess_DEL_CFCUserID"))
                {
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                isSuccessful = false;
                MessageBox.Show(ex.Message + "\n\nUnable to user menu access. Calling DeleteUserMenuAccess. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return isSuccessful;
        }


        ////10/26/22
        //public Boolean InsertUserMenuAccess(Int32 cfcUserID)
        //{
        //    Boolean isSuccessful = true;
        //    try
        //    {
        //        using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_UserMenuAccess_INS_CFCUserID"))
        //        {
        //            _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
        //            _database.ExecuteNonQuery(cmd);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        isSuccessful = false;
        //        MessageBox.Show(ex.Message + "\n\nUnable to insert user menu access. Calling DeletePrinterUserDefault. Contact IT.", "Warning...", MessageBoxButtons.OK);
        //    }
        //    return isSuccessful;
        //}


        #endregion

        #region get User UserRoles
        public List<RolesInfo> GetUserRoles(string UserName, string applicationName)
        {
            List<RolesInfo> UserRoles = new List<RolesInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("dbo.aspnet_UsersInRoles_GetRolesForUser"))
                {
                    _database.AddInParameter(cmd, "@UserName", SqlDbType.VarChar, UserName);
                    _database.AddInParameter(cmd, "@ApplicationName", SqlDbType.VarChar, applicationName);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<RolesInfo>.GetGenericCollection(ref UserRoles, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to get UserRoles. Calling GetUserRoles. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return UserRoles;
        }
        #endregion

        #region save UserRole
        public Boolean SaveUserRole(string UserName, string ApplicationName, string RoleName)
        {
            Boolean IsSuccessful = true;
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("dbo.aspnet_UsersInRoles_AddUsersToRoles"))
                {
                    _database.AddInParameter(cmd, "@ApplicationName", SqlDbType.VarChar, ApplicationName);
                    _database.AddInParameter(cmd, "@UserNames", SqlDbType.VarChar, UserName);
                    _database.AddInParameter(cmd, "@RoleNames", SqlDbType.VarChar, RoleName);
                    _database.AddInParameter(cmd, "@CurrentTimeUtc", SqlDbType.DateTime, DateTime.Now);
                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                IsSuccessful = false;
                MessageBox.Show(ex.Message + "\n\nUnable to Save UserRoles. Calling SaveUserRoles. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return IsSuccessful;
        }
        #endregion

        #region Get All Roles
        public List<RolesInfo> GetRoles(string ApplicationName)
        {
            List<RolesInfo> Roles = new List<RolesInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("dbo.aspnet_Roles_GetAllRoles"))
                {
                    _database.AddInParameter(cmd, "@ApplicationName", SqlDbType.VarChar, ApplicationName);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<RolesInfo>.GetGenericCollection(ref Roles, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to get Roles. Calling GetRoles. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return Roles;
        }
        #endregion

        #region delete UserRole
        public bool DeleteUserRole(string ApplicationName, string UserName, string RoleName)
        {
            Boolean IsSuccessful = true;
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("dbo.aspnet_UsersInRoles_RemoveUsersFromRoles"))
                {
                    _database.AddInParameter(cmd, "@ApplicationName", SqlDbType.VarChar, ApplicationName);
                    _database.AddInParameter(cmd, "@UserNames", SqlDbType.VarChar, UserName);
                    _database.AddInParameter(cmd, "@RoleNames", SqlDbType.VarChar, RoleName);
                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                IsSuccessful = false;
                MessageBox.Show(ex.Message + "\n\nUnable to Delete UserRole. Calling DeleteUserRole. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return IsSuccessful;
        }
        #endregion

        #region creating default application role and user
        public Boolean CheckInsertDefaultRole(string ApplicationName)
        {
            Boolean IsSuccessful = true;
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("cfcUser.usp_Roles_INS_DefaultRole"))
                {
                    _database.AddInParameter(cmd, "@ApplicationName", SqlDbType.VarChar, ApplicationName);
                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                IsSuccessful = false;
                MessageBox.Show(ex.Message + "\n\nUnable to Insert RoleName. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return IsSuccessful;
        }
        public Boolean CheckInsertDefaultUser(string ApplicationName)
        {
            Boolean IsSuccessful = true;
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("cfcUser.usp_CFCUser_INS_DefaultUser"))
                {
                    _database.AddInParameter(cmd, "@ApplicationName", SqlDbType.VarChar, ApplicationName);
                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                IsSuccessful = false;
                MessageBox.Show(ex.Message + "\n\nUnable to Insert DefaultUser. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return IsSuccessful;
        }
        #endregion

        #region Permissions/UserAccess

        public List<PermissionInfo> GetPermissions()
        {
            List<PermissionInfo> result = new List<PermissionInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("cfcUser.usp_Permission_SEL"))
                {
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<PermissionInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to get Permissions. Calling GetPermissions stp.  Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return result;
        }
        public List<UserAccessInfo> GetUserAccessCFCUserID(Int32 cfcUserID)
        {
            List<UserAccessInfo> result = new List<UserAccessInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_UserAccess_SEL_CFCUserID"))
                {
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<UserAccessInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to get UserAccess. Calling GetUserAccessCFCUserID. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return result;
        }

        //10/25/22
        public List<UserMenuAccessInfo> GetUserMenuAccessCFCUserID(Int32 cfcUserID)
        {
            List<UserMenuAccessInfo> result = new List<UserMenuAccessInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_UserMenuAccess_SEL_CFCUserID"))
                {
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<UserMenuAccessInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to get UserAccess. Calling GetMenuUserAccessCFCUserID. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return result;
        }

        //11/1/22
        public List<UserMenuAccessInfo> InsertUserMenuAccess(Int32 cfcUserID)
        {
            List<UserMenuAccessInfo> result = new List<UserMenuAccessInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_UserMenuAccess_INS_CFCUserID"))
                {
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<UserMenuAccessInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to get UserAccess. Calling GetMenuUserAccessCFCUserID. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return result;
        }

        //10/26/22
        public UserMenuAccessInfo GetUserMenuAccessCFCUserID(Int32 cfcUserID, String description)
        {
            UserMenuAccessInfo result = new UserMenuAccessInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_UserMenuAccess_SEL_CFCUserID_MenuDescription"))
                {
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    _database.AddInParameter(cmd, "@Description", SqlDbType.NVarChar, description);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<UserMenuAccessInfo>.GetScalar(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to get UserAccess. Calling GetMenuUserAccessCFCUserID. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return result;
        }

        public Boolean SaveUserAccess(Int32 userAccessID, Int32 cfcUserID, Int32 permissionID, Int32 updateUserID)
        {
            Boolean IsSuccessful = true;
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_UserAccess_SAV_CFCUserID_PermissionID"))
                {
                    _database.AddInParameter(cmd, "@UserAccessID", SqlDbType.Int, userAccessID);
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    _database.AddInParameter(cmd, "@PermissionID", SqlDbType.Int, permissionID);

                    //8/22/18
                    //_database.AddInParameter(cmd, "@UpdateStamp", SqlDbType.VarChar, DateTime.Now);                    

                    _database.AddInParameter(cmd, "@UpdateUserID", SqlDbType.Int, updateUserID);
                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                IsSuccessful = false;
                MessageBox.Show(ex.Message + "\n\nUnable to Save UserRoles. Calling SaveUserRoles. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return IsSuccessful;
        }

        //10/26/22               
        public Boolean SaveUserMenuAccess(Int32 userMenuAccessID, Int32 cfcUserID, Int32 menuItemsID, Int32 updateUserID, Boolean isNotAllowed)
        {
            Boolean IsSuccessful = true;
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_UserMenuAccess_SAV_CFCUserID_MenuItemsID"))
                {
                    _database.AddInParameter(cmd, "@UserMenuAccessID", SqlDbType.Int, userMenuAccessID);
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    _database.AddInParameter(cmd, "@MenuItemsID", SqlDbType.Int, menuItemsID);
                    _database.AddInParameter(cmd, "@UpdateUserID", SqlDbType.Int, updateUserID);
                    _database.AddInParameter(cmd, "@IsNotAllowed", SqlDbType.Bit, isNotAllowed);
                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                IsSuccessful = false;
                MessageBox.Show(ex.Message + "\n\nUnable to Save UserRoles. Calling SaveUserRoles. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return IsSuccessful;
        }

        public Boolean DeleteUserAccess(Int32 userAccessID)
        {
            Boolean IsSuccessful = true;
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_UserAccess_DEL_UserAccessID"))
                {
                    _database.AddInParameter(cmd, "@ID", SqlDbType.Int, userAccessID);
                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                IsSuccessful = false;
                MessageBox.Show(ex.Message + "\n\nUnable to Delete UserAccess permission. Calling DeleteUserAccess. Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return IsSuccessful;
        }


        #endregion

        #region User Defaults


        //12/6/21

        private static String _CFCAIMUserDefaultFile = Application.StartupPath + @"\CFCAIMUserDefault.xml";

        /////private static String _CFCAIMUserDefaultFile = Application.StartupPath + "\\User\\" + Global._CFCLoginUser_ForDefault + @"\CFCAIMUserDefault.xml";        
        //3/15/23 - TODO - this should be changed to add "Users" folder and user-name to above file
        //Application.StartupPath + "\\Users\\admin"


        //12/6/21
        public static String CFCAIMUserDefaultFile
        {
            get
            {
                return _CFCAIMUserDefaultFile;
            }

            set
            {
                _CFCAIMUserDefaultFile = value;
            }
        }


        //12/6/21
        public static void LoadUserList(ref List<UserDefaultFileInfo> databases, String userDefaultFile)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlNodeList xmlNode;
                Int32 count = 0;

                if (File.Exists(userDefaultFile))
                {
                    using (FileStream fs = new FileStream(userDefaultFile, FileMode.Open, FileAccess.Read))
                    {
                        if (fs.Length > 0)
                        {
                            xmlDoc.Load(fs);
                            xmlNode = xmlDoc.GetElementsByTagName("UserDefault");
                            for (Int32 i = 0; i <= xmlNode.Count - 1; i++)
                            {
                                databases.Insert(count, new UserDefaultFileInfo
                                {
                                    UserName = xmlNode[i].ChildNodes.Item(0).InnerText.Trim(),
                                    ConnectName = xmlNode[i].ChildNodes.Item(1).InnerText.Trim()
                                });
                                count += 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
            }
        }

        //12/6/21 - Gets the connection string for the user's default database
        public static void LoadUserDefaultConnection(String userConnection)
        {

            Int32 databaseID = 0;
            String serverName = "";
            Boolean isSqlAuth = false;
            String user = "";
            String password = "";
            String database = "";
            List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();


            //This loads all the database connections from cfcaimDatabase.xml
            SQLDatabaseConnection.LoadConnection(ref databases, true, SQLDatabaseConnection.CFCAIMDatabaseFile);

            //--------------------------------------------------------
            //Get User's default connection information
            //var connections = from g in databases
            //                  where g.ConnectName == userConnection
            //                    select g;
            foreach (SQLDatabaseInfo sqlDatabase in databases)
            {
                if (sqlDatabase.ConnectName == userConnection)
                {
                    databaseID = sqlDatabase.DatabaseID;
                    serverName = sqlDatabase.ServerName;
                    isSqlAuth = sqlDatabase.IsSQLAuthentication;
                    user = sqlDatabase.User;
                    password = sqlDatabase.Password;
                    database = sqlDatabase.Database;
                    break;
                }
            }

            AIMManager.IsMultipleDatabase = true;
            AIMManager.DatabaseConnection = SQLDatabaseConnection.GetSQLConnection(serverName, isSqlAuth, user, password, database);

        }

        //12/6/21
        public static Int32 GetUserDefaultDatabaseID(String userConnection)
        {
            try
            {
                Int32 databaseID = 0;
                List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();

                //This loads all the database connections from cfcaimDatabase.xml
                SQLDatabaseConnection.LoadConnection(ref databases, true, SQLDatabaseConnection.CFCAIMDatabaseFile);

                //--------------------------------------------------------
                //Get User's default connection information
                var connections = from g in databases
                                  where g.ConnectName == userConnection
                                  select g;
                foreach (var item in connections)
                {
                    databaseID = item.DatabaseID;
                }

                return databaseID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
                return 0;
            }

        }

        //12/6/21 - Reads the user-default xml file and returns the connection-name for the user
        public static String GetUserDefaultDatabaseName(String userDefaultFile, String userName)
        {
            try
            {
                String defaultConnection = String.Empty;

                XmlDocument xmlDoc = new XmlDocument();
                XmlNodeList xmlNode;

                if (File.Exists(userDefaultFile))
                {
                    using (FileStream fs = new FileStream(userDefaultFile, FileMode.Open, FileAccess.Read))
                    {
                        if (fs.Length > 0)
                        {
                            xmlDoc.Load(fs);
                            xmlNode = xmlDoc.GetElementsByTagName("UserDefault");
                            for (Int32 i = 0; i <= xmlNode.Count - 1; i++)
                            {
                                if (userName != null)
                                {
                                    if (xmlNode[i].ChildNodes.Item(0).InnerText.Trim().ToUpper() == userName.ToUpper())
                                    {
                                        defaultConnection = xmlNode[i].ChildNodes.Item(1).InnerText.Trim();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                return defaultConnection;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
                return String.Empty;
            }
        }

        //12/6/21
        public static void SaveUserDefaultXmlConnection(ref List<UserDefaultFileInfo> UserList, String userDefaultFile, String userName, String connectName, Boolean newUser)
        {
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                Boolean isData = false;
                settings.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(userDefaultFile, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteComment("UserDefault");
                    writer.WriteStartElement("UserDefaults");
                    isData = SaveUserDefaultXmlDatabaseDetailData(writer, ref UserList, userName, userDefaultFile, connectName, newUser);
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                }

                if (isData == false && File.Exists(userDefaultFile))
                {
                    File.Delete(userDefaultFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
            }
        }

        //12/6/21        
        public static Boolean SaveUserDefaultXmlDatabaseDetailData(XmlWriter writer, ref List<UserDefaultFileInfo> userList, String userName, String userDefaultFile, String connectName, Boolean newUser)
        {
            try
            {
                Boolean isData = false;
                foreach (UserDefaultFileInfo user in userList)
                {

                    writer.WriteStartElement("UserDefault");
                    writer.WriteElementString("UserName", user.UserName);

                    if (userName != null)
                    {
                        if (user.UserName.ToUpper() == userName.ToUpper())
                        {
                            writer.WriteElementString("ConnectName", connectName);
                        }
                        else
                        {
                            writer.WriteElementString("ConnectName", user.ConnectName);
                        }
                    }


                    writer.WriteEndElement();
                    isData = true;
                }
                if (newUser == true)
                {
                    writer.WriteStartElement("UserDefault");
                    writer.WriteElementString("UserName", userName);
                    writer.WriteElementString("ConnectName", connectName);
                    writer.WriteEndElement();
                    isData = true;
                }
                return isData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK);
                return false;
            }
        }


        #endregion

    }
}