using System;
using CFCUser.Entity;

namespace CFCUser
{
    public class Global
    {
        private static UserInfo _CFCLoginUser = new UserInfo();
        private static String _ApplicationName = String.Empty;
        private static string _CompanyName = String.Empty;

        //12/6/21
        private static Int32 _AIMDatabaseID;
        private static string _CFCLoginUserName = String.Empty;

        //10/21/22
        private static string _ProgramDatabaseVersion = String.Empty;

        //3/15/23
        ////private static string _CFCLoginUser_ForDefault = String.Empty;

        #region variables


        //10/21/22
        public string ProgramDatabaseVersion
        {
            get
            {
                return _ProgramDatabaseVersion;
            }
            set
            {
                _ProgramDatabaseVersion = value;
            }
        }


        public UserInfo CFCLoginUser
        {
            get
            {
                return _CFCLoginUser;
            }
            set
            {
                _CFCLoginUser = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                _CompanyName = value;
            }
        }
        public string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        //12/6/21
        public Int32 AIMDatabaseID
        {
            get
            {
                return _AIMDatabaseID;
            }
            set
            {
                _AIMDatabaseID = value;
            }
        }

        //12/6/21
        public string CFCLoginUserName
        {
            get
            {
                return _CFCLoginUserName;
            }
            set
            {
                _CFCLoginUserName = value;
            }
        }

        //3/15/23
        ////public string CFCLoginUser_ForDefault
        ////{
        ////    get
        ////    {
        ////        return _CFCLoginUser_ForDefault;
        ////    }
        ////    set
        ////    {
        ////        _CFCLoginUser_ForDefault = value;
        ////    }
        ////}

        #endregion
    }
}
