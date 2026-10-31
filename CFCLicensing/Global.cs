using System;

namespace CFCLicensing.Varible
{
    public static class Global
	{
		#region variables
		static String _sqlConnection = "ConnectionString";

        private static TimeSpan _LicensingDaysLeft = new TimeSpan();
        private static String _AppStartupPathNDatabaseDirectory = "";//being used in Fertilizer main
        
        #endregion
         
        #region Properties
        public static String SqlConnection
		{
			get
			{
				return _sqlConnection;
			}
			set
			{
				_sqlConnection = value;
			}
		}
        public static TimeSpan LicensingDaysLeft
        {
            get
            {
                return _LicensingDaysLeft;
            }
            set
            {
                _LicensingDaysLeft = value;
            }
        }
        public static String AppStartupPathNDatabaseDirectory
        {
            get
            {
                return _AppStartupPathNDatabaseDirectory;
            }
            set
            {
                _AppStartupPathNDatabaseDirectory = value;
            }
        }
        #endregion  
    }
}
