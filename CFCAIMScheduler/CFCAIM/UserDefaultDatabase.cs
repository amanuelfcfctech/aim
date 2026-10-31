using CFCAIMCore.Manager;
using CFCUser.Data;
using CFCUser.Entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace CFCAIM
{
    class UserDefaultDatabase
    {
        public static void UpdateUserDefault(String userName, String connection)
        {


            //12/6/21 - Set user-default connection   
            String userConnection = String.Empty;
            List<UserDefaultFileInfo> userList = new List<UserDefaultFileInfo>();

            if (File.Exists(CFCUser.Data.UserDataAccess.CFCAIMUserDefaultFile))     //if xml exists ...
            {
                AIMManager.CloseAIMData();

                //Get connection name for user (from xml file)
                userConnection = UserDataAccess.GetUserDefaultDatabaseName(UserDataAccess.CFCAIMUserDefaultFile, userName);

                //-------------------------------------------------------------
                //CHANGE USER'S DATABASE ON XML
                if (userConnection != String.Empty)
                {
                    //Get connection string for lueDatabase.Text
                    UserDataAccess.LoadUserDefaultConnection(connection);

                    //Sets database connection
                    CFCUser.Manager.UserManager.IsMultipleDatabase = true;
                    CFCUser.Manager.UserManager.DatabaseConnection = AIMManager.DatabaseConnection;

                    //update user-default xml
                    UserDataAccess.LoadUserList(ref userList, CFCUser.Data.UserDataAccess.CFCAIMUserDefaultFile);
                    UserDataAccess.SaveUserDefaultXmlConnection(ref userList, CFCUser.Data.UserDataAccess.CFCAIMUserDefaultFile, userName, connection, false);

                }
                //-------------------------------------------------------------
                //ADD USER TO XML
                else
                {
                    UserDataAccess.LoadUserList(ref userList, CFCUser.Data.UserDataAccess.CFCAIMUserDefaultFile);
                    UserDataAccess.SaveUserDefaultXmlConnection(ref userList, CFCUser.Data.UserDataAccess.CFCAIMUserDefaultFile, userName, connection, true);
                }
            }
            else
            {
                //-------------------------------------------------------------
                //CREATE XML AND ADD USER
                UserDataAccess.SaveUserDefaultXmlConnection(ref userList, CFCUser.Data.UserDataAccess.CFCAIMUserDefaultFile, userName, connection, true);

            }
        }


    }
}
