using CFCAIMWorkerTest.Entity;
using Core.Library.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using CFCAIMCore.Manager;

namespace CFCAIMWorkerTest.Data
{

    public class QADataAccess
    {
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

        public CFCAPIManager CFCAPIManager;

        #endregion

        #region API Calls

        #region

        #region GetData
        public QAUnitTestInfo GetQAError()
        {
            try
            {
                Initializer();
                RestRequest request = new RestRequest("/Bruker", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(brukerSample);
                IRestResponse<ErrorInfo> response = _client.Execute<ErrorInfo>(request);
                return (ErrorInfo)response.Data;
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(ex.Message + " Unable to " + MethodInfo.GetCurrentMethod().Name + ". Contact IT.", EventLogEntryType.Error);
                throw;
            }
        }


        #endregion

    }
}
