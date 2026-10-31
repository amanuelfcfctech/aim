using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Reflection;

namespace CFCAIMCore.Data
{

    public class QADataAccess
    {
        // done
        #region Member Variables
        public static SqlDatabase _QADatabase;
        public static Boolean _isMessageDisplay = true;
        private static String _connectionString = String.Empty;
        #endregion 

        #region Constructors
        public QADataAccess()
        {
            //if (connectionString.Length > 0)
            //{
            //    _database = new SqlDatabase(connectionString);
            //}
            //else
            //{
            //    _database = (SqlDatabase)EnterpriseLibraryContainer.Current.GetInstance<Database>(connectionStringName);
            //}
        }
        #endregion

        #region Parameter

        public static String QADatabase
        {
            set
            {
                String connectionString = value;
                ConnectionString = connectionString;
                _QADatabase = new SqlDatabase(connectionString);
            }
        }
        public static Boolean IsMessageDisplay
        {
            get
            {
                return _isMessageDisplay;
            }

            set
            {
                _isMessageDisplay = value;
            }
        }

        public static String ConnectionString
        {
            get
            {
                return _connectionString;
            }

            set
            {
                _connectionString = value;
            }
        }

        #endregion

        #region API Calls

        #endregion

        #region GetData



        #endregion

    }
}
