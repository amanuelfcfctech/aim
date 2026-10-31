using System;

namespace CFCAIM
{
    public class Version
    {
        //CHANGE THIS DATE WITH EACH NEW VERSION
        private static String _programAppVersion = "2026-08-18";
        private static String _programDatabaseVersion = "2026-08-18";        
        #region Constructors
        public static String ProgramAppVersion
        {
            get
            {
                return _programAppVersion;
            }
        }
        public static String ProgramDatabaseVersion
        {
            get
            {
                return _programDatabaseVersion;
            }
        }
        #endregion
    }
}
