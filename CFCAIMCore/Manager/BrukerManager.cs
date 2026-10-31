using System;
using System.Collections.Generic;
using CFCAIMCore.Data;
using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Bruker;

namespace CFCAIMCore.Manager
{
	public class BrukerManager
	{
		#region  Member Variables
		private static BrukerDataAccess _brukerData;
		#endregion

		#region  Properties
		protected static BrukerDataAccess BrukerData
		{
			get
			{
				//    if (_aimData == null)
				//    {
				//        if (IsMultipleDatabase)
				//        {
				//            _aimData = new Data.AIMDataAccess(String.Empty, DatabaseConnection);
				//        }
				//        else
				//        {
				//            if (_host == "CFCAIMDEMO")
				//            {
				//                _aimData = new Data.AIMDataAccess("DemoConnectionString", String.Empty);
				//            }
				//            else
				//            {
				_brukerData = new Data.BrukerDataAccess();
				//            }
				//        }

				//    }
				return _brukerData;
			}
		}

		public static Boolean IsMessageDisplay
		{
			get
			{
				return Data.BrukerDataAccess.IsMessageDisplay;
			}

			set
			{
				Data.BrukerDataAccess.IsMessageDisplay = value;
			}
		}
		#endregion

		#region Funcation
		public static void CreateBrukerConnection(String serverName, String databaseName, Boolean isSqlAuthentication, String userName, String password)
		{
			if (isSqlAuthentication == true)
			{
				BrukerDataAccess.BrukerDatabase = String.Format("Data Source = {0}; Initial Catalog = {1}; User Id = {2}; Password = {3}", serverName, databaseName, userName, password);
			}
		}
		public static void CreateBrukerConnection(String connectionName)
		{
			BrukerDataAccess.BrukerDatabase = connectionName;
		}
		public static void SQLCommand(String sqlCommand)
		{
			BrukerData.SQLCommand(sqlCommand);
		}
		public static void ExecuteNonQuerySQLCommand(String sqlCommand)
		{
			BrukerData.ExecuteNonQuerySQLCommand(sqlCommand);
		}
        #endregion

        #region Insert
        public static void InsertCFCLogResult(BrukerCFCLogResultInfo brukerCFCLog)
		{
			BrukerData.InsertCFCLogResult(brukerCFCLog);
		}
		public static void InsertCFCLogSpectrum(String path, String fileName)
		{
			String errorMessage = String.Empty;
			Boolean isError = false;
			BrukerData.InsertCFCLogSpectrum(path, fileName, isError, errorMessage);
		}
		public static void InsertCFCLogSpectrum(String path, String fileName, Boolean isError, String errorMessage)
		{
			BrukerData.InsertCFCLogSpectrum(path, fileName, isError, errorMessage);
		}
		#endregion

		#region Get
		public static List<BrukerSQLInfo> GetBrukerSqlData(String sampleCode, DateTime startDate, DateTime endDate)
		{
			return BrukerData.GetBrukerSqlData(sampleCode, startDate, endDate);
		}
		public static List<BrukerSQLInfo> GetBrukerSqlData()
		{
			return BrukerData.GetBrukerSqlData();
		}
		public static List<BrukerSampleInfo> GetBrukerSpectrum()
		{
			return BrukerData.GetBrukerSpectrum();
		}
		public static List<BrukerSampleInfo> GetBrukerSample()
		{
			return BrukerData.GetBrukerSample();
		}
		public static List<BrukerSampleInfo> GetBrukerSampleAll()
		{
			return BrukerData.GetBrukerSampleAll();
		}
		public static List<BrukerSampleResultInfo> GetBrukerSampleResultNotSent(String spectrumPath, String spectrumName)
		{
			return BrukerData.GetBrukerSampleResultNotSent(spectrumPath, spectrumName);
		}
		public static List<BrukerSampleResultInfo> GetBrukerSampleResult(String spectrumPath, String spectrumName)
		{
			return BrukerData.GetBrukerSampleResult(spectrumPath, spectrumName);
		}
		public static Boolean GetSysObject(String name)
		{
			return BrukerData.GetSysObject(name);
		}
		public static BrukerFileListInfo GetBrukerFileList(String path, String fileName)
		{
			return BrukerData.GetBrukerFileList(path, fileName);
		}
		public static String GetCurrentVersion()
		{
			return BrukerData.GetCurrentVersion();
		}
		#endregion

		#region Delete
		public static ErrorInfo DeleteCFCLogResult(String path, String fileName)
		{
			return BrukerData.DeleteCFCLogResult(path, fileName);
		}
		public static ErrorInfo DeleteCFCLogSpectrum(String path, String fileName)
		{
			return BrukerData.DeleteCFCLogSpectrum(path, fileName);
		}
		#endregion
	}
}
