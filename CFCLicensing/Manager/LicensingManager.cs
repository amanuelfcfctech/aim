using CFCLicensing.Data;
using CFCLicensing.Entity;
using System;


namespace CFCLicensing.Manager
{
    public class LicensingManager
    {
        #region  Member Variables

        public static string _Host;
        public static LicensingDataAccess _licensingData;

        //1/19/17
        private static String _databaseConnection;
        private static Boolean _isMultipleDatabase = false;

        #endregion

        #region  Properties
        protected static LicensingDataAccess LicensingData
        {
            get
            {
                if (DatabaseConnection != null)
                {
                    _licensingData = new LicensingDataAccess(String.Empty, DatabaseConnection);
                }
                else
                { 
                    if (IsMultipleDatabase)
                    {
                        if (DatabaseConnection != null)
                        {
                            _licensingData = new LicensingDataAccess(String.Empty, DatabaseConnection);
                        }
                    }
                    else
                    {
                        if (_Host == "localhost")
                        {
                            _licensingData = new LicensingDataAccess("ConnectionStringTest", String.Empty);
                        }
                        else
                        {
                            _licensingData = new Data.LicensingDataAccess("ConnectionString", String.Empty);
                        }
                    }
                }
                return _licensingData;
            }
        }
        public static void CloseLicensingData()
        {
            _licensingData = null;
        }
        //1/19/17 - added
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

        //1/19/17 - added
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

        #region Get System Option
        public static VersionInfo GetVersion()
        {
            return LicensingData.GetVersion();
        }
        public static ConfigInfo GetConfig()
        {
            return LicensingData.GetConfig();
        }
        #endregion

        #region Save System Option

        public static void SaveConfig(ref ConfigInfo config)
        {
            Boolean isSuccessful = LicensingData.SaveConfig(ref config);
        }

        public static Boolean SaveConfigWithSuccess(ref ConfigInfo config)
        {
            return LicensingData.SaveConfig(ref config);
        }
        #endregion

    }
}
