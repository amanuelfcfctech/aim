using Core.Library.SQLDatabaseConnection;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;


namespace CFCAIMWorkerTest
{
    internal class BrukerTest
    {
        #region Member Variables
        private static String _databaseXML;
        private static List<SQLDatabaseInfo> _databases;
        private static String _path;
        
        #endregion

        #region Parameters
        public static String DatabaseXML
        {
            get
            {
                return _databaseXML;
            }
            set
            {
                _databaseXML = value;
            }
        }
        public static List<SQLDatabaseInfo> Databases
        {
            get
            {
                return _databases;
            }
            set
            {
                _databases = value;
            }
        }
        public static String Path
        {
            set
            {
                _path = System.IO.Directory.GetCurrentDirectory();
            }
        }
        #endregion

        public static void MakeSQLConnection()
        {
            

            _databases = new List<SQLDatabaseInfo>();
            SQLDatabaseConnection.LoadConnection(ref _databases, true, _databaseXML);

        }


        public void GetBrukerSample_Test()
        {


            //SendTangoSampleToAPI()
        }


    }
}
