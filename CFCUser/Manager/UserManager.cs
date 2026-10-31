using System;
using System.Collections.Generic;
using CFCUser.Data;
using CFCUser.Entity;

namespace CFCUser.Manager
{
    public class UserManager
    {
        #region  Member Variables

        public static string _Host;
        private static UserDataAccess _UserData;
        
        //1/19/17
        private static String _databaseConnection;
        private static Boolean _isMultipleDatabase = false;

        #endregion

        #region  Properties
        protected static UserDataAccess UserData
        {
            get
            {
                if (_UserData == null)
                {
                    if (IsMultipleDatabase)
                    {
                        _UserData = new UserDataAccess(String.Empty, DatabaseConnection);
                    }
                    else
                    {
                        if (_Host == "localhost")
                        {
                            _UserData = new UserDataAccess("ConnectionStringTest", String.Empty);
                        }
                        else
                        {
                            _UserData = new UserDataAccess("ConnectionString", String.Empty);

                        }
                    }
                }
                return _UserData;
            }
        }

        //11/3/22
        public static void CloseUserData()
        {
            _UserData = null;
        }

        public static Boolean IsMultipleDatabase
        {
            get
            {
                return _isMultipleDatabase;
            }

            set
            {
                _isMultipleDatabase = value;
            }
        }
        public static String DatabaseConnection
        {
            get
            {
                return _databaseConnection;
            }

            set
            {
                _databaseConnection = value;
            }
        }
        #endregion

        #region Save User
        public static Boolean SaveUser(UserInfo user)
        {
            return UserData.SaveUser(user);
        }

        
        #endregion

        #region Get User
        public static List<UserInfo> GetUser(Boolean IsActive)
        {
            return UserData.GetUser(IsActive);
        }
        public static UserInfo GetUser(int CFCUserID)
        {
            return UserData.GetUser(CFCUserID);
        }
        public static UserInfo GetUser(string UserName, string Password)
        {
            return UserData.GetUser(UserName, Password);
        }
        public static UserInfo GetUser(string UserName)
        {
            return UserData.GetUser(UserName);
        }
        #endregion

        #region Delete User
        public static Boolean DeleteUser(int CFCUserID)
        {
            return UserData.DeleteUser(CFCUserID);
        }

        //6/18/18
        public static Boolean DeletePrinterUserDefault(Int32 cfcUserID)
        {
            return UserData.DeletePrinterUserDefault(cfcUserID);
        }

        ////10/26/22
        //public static Boolean InsertUserMenuAccess(Int32 cfcUserID)
        //{
        //    return UserData.InsertUserMenuAccess(cfcUserID);
        //}
          
        //10/26/22
        public static Boolean DeleteUserMenuAccess(Int32 cfcUserID)
        {
            return UserData.DeleteUserMenuAccess(cfcUserID);
        }

        //10/26/22
        public static Boolean DeleteUserAccess_CFCUserID(Int32 cfcUserID)
        {
            return UserData.DeleteUserAccess_CFCUserID(cfcUserID);
        }

        #endregion

        #region Get Roles
        public static List<RolesInfo> GetUserRoles(string UserName, string ApplicationName)
        {
            return UserData.GetUserRoles(UserName, ApplicationName);
        }

        public static List<RolesInfo> GetRoles(string ApplicationName)
        {
            return UserData.GetRoles(ApplicationName);
        }
        #endregion

        #region Save User Role
        public static Boolean SaveUserRole(string UserName, string ApplicationName, string RoleName)
        {
            return UserData.SaveUserRole(UserName, ApplicationName, RoleName);
        }
        #endregion

        #region Delete User Role
        public static Boolean DeleteUserRole(string ApplicationName, string UserName, string RoleName)
        {
            return UserData.DeleteUserRole(ApplicationName, UserName, RoleName);
        }
        #endregion

        #region Insert Role and Default User
        public static Boolean CheckInsertDefaultRole(string ApplicationName)
        {
            return UserData.CheckInsertDefaultRole(ApplicationName);
        }
        public static Boolean CheckInsertDefaultUser(string ApplicationName)
        {
            return UserData.CheckInsertDefaultUser(ApplicationName);
        }
        #endregion

        #region Permissions/UserAccess

        public static List<UserAccessInfo> GetUserAccessCFCUserID(Int32 cfcUserID)
        {
            return UserData.GetUserAccessCFCUserID(cfcUserID);
        }

        //10/25/22
        public static List<UserMenuAccessInfo> GetUserMenuAccessCFCUserID(Int32 cfcUserID)
        {
            return UserData.GetUserMenuAccessCFCUserID(cfcUserID);
        }


        //11/1/22
        public static List<UserMenuAccessInfo> InsertUserMenuAccess(Int32 cfcUserID)
        {
            return UserData.InsertUserMenuAccess(cfcUserID);
        }

        //10/26/22
        public static UserMenuAccessInfo GetUserMenuAccessCFCUserID(Int32 cfcUserID, String description)
        {
            return UserData.GetUserMenuAccessCFCUserID(cfcUserID, description);
        }
        
        public static List<PermissionInfo> GetPermissions()
        {
            return UserData.GetPermissions();
        }

        public static Boolean SaveUserAccess(Int32 userAccessID, Int32 cfcUserID, Int32 permissionID, Int32 updateUserID)
        {
            return UserData.SaveUserAccess(userAccessID, cfcUserID, permissionID, updateUserID);
        }

        //10/26/22
        public static Boolean SaveUserMenuAccess(Int32 userMenuAccessID, Int32 cfcUserID, Int32 menuItemsID, Int32 updateUserID, Boolean isNotAllowed)
        {
            return UserData.SaveUserMenuAccess(userMenuAccessID, cfcUserID, menuItemsID, updateUserID, isNotAllowed);
        }

        public static Boolean DeleteUserAccess(Int32 ID)
        {
            return UserData.DeleteUserAccess(ID);
        }

        
        #endregion


    }
}