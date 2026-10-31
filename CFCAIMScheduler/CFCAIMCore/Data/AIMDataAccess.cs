using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Adisseo;
using CFCAIMCore.Entity.API;
using CFCAIMCore.Entity.CFCAPI;
using Core.Library.Data;
using Core.Library.Notification;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace CFCAIMCore.Data
{
	public class AIMDataAccess
	{
		#region Member Variables
		public static SqlDatabase _database;
		public static SqlDatabase _brukerDatabase;
		public static Boolean _isMessageDisplay = true;
		#endregion

		#region Constructors
		public AIMDataAccess(String connectionStringName, String connectionString)
		{
			if (connectionString.Length > 0)
			{
				_database = new SqlDatabase(connectionString);
			}
			else
			{
				_database = (SqlDatabase)EnterpriseLibraryContainer.Current.GetInstance<Database>(connectionStringName);
			}
		}
		#endregion

		#region Parameter
		public static String BrukerDatabase
		{
			set
			{
				String connectionString = value;

				_brukerDatabase = new SqlDatabase(connectionString);
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
		#endregion

		#region Funcation
		private static void HandleMessage(Exception ex, String methodName)
		{
			//3/4/21 - removed unnecessary assignment
			//String message = String.Empty;
			//message = ex.Message + " Unable to " + methodName + ". Contact IT.";
			String message = ex.Message + " Unable to " + methodName + ". Contact IT.";

			if (_isMessageDisplay)
			{
				MessageBox.Show(message, "Warning...", MessageBoxButtons.OK);
			}
			else
			{
				ApplicationLog.WriteLog(message, EventLogEntryType.Warning);
			}
		}

		private static void HandleMessage(ErrorInfo message, String methodName)
		{
			if (_isMessageDisplay)
			{
				MessageBox.Show(message.ErrorMessage, "Warning...", MessageBoxButtons.OK);
			}
			else
			{
				ApplicationLog.WriteLog(message.ErrorMessage, EventLogEntryType.Warning);
			}
		}
		#endregion

		#region Get Data
		public List<PNECreditInfo> GetPNECredit()
		{
			List<PNECreditInfo> result = new List<PNECreditInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNECredit_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PNECreditInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);

			}
			return result;
		}

		//8/1/22
		public List<APISprectrumLogInfo> GetAPISprectrumLog(Int32 sampleID, Int32 apiSpectrumID)
		{
			List<APISprectrumLogInfo> result = new List<APISprectrumLogInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_APISprectrumLog_SEL_SampleIDAPISpectrumID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@APISpectrumID", SqlDbType.Int, apiSpectrumID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<APISprectrumLogInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);

			}
			return result;
		}

		//2/20/20
		public Int32 BackupDbase(String bkStr)
		{
			Int32 result = 0;

			try
			{
				using (DbCommand cmd = _database.GetSqlStringCommand(bkStr))
				{
					result = _database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = -99;
			}
			return result;
		}

		//2/25/20
		public ErrorInfo GetNewID()
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GetNewID_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//1/4/24
		public List<SystemLogInfo> GetSystemLog()
		{
			List<SystemLogInfo> result = new List<SystemLogInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SystemLog_SEL"))
				{					
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SystemLogInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//4/11/18
		public List<PivotGridTestInfo> GetPivotGrid()
		{
			List<PivotGridTestInfo> result = new List<PivotGridTestInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PivotGrid_Test"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PivotGridTestInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//11/30/22
		public List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysisPending()
		{
			List<SampleFurtherAnalysisInfo> result = new List<SampleFurtherAnalysisInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleFurtherAnalysis_SEL_Pending"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleFurtherAnalysisInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/28/22
		public List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysis()
		{
			List<SampleFurtherAnalysisInfo> result = new List<SampleFurtherAnalysisInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleFurtherAnalysis_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleFurtherAnalysisInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/18/24
		public List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysis(DateTime begDate, DateTime endDate, Int32 dateField, Int32 dataSourceTypeID)
		{
			List<SampleFurtherAnalysisInfo> result = new List<SampleFurtherAnalysisInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleFurtherAnalysis_SEL_BeginDate_EndDate_DateField_DateSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@BegDate", SqlDbType.Date, begDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, endDate);
					_database.AddInParameter(cmd, "@DateField", SqlDbType.Int, dateField);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleFurtherAnalysisInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/6/24
		//7/11/23		
		//public List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysis(DateTime begDate, DateTime endDate)
		public List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysis(DateTime begDate, DateTime endDate, Boolean isAll)
		{
			List<SampleFurtherAnalysisInfo> result = new List<SampleFurtherAnalysisInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleFurtherAnalysis_SEL_UpdateStamp"))
				{
					_database.AddInParameter(cmd, "@BegDate", SqlDbType.Date, begDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, endDate);
					if (isAll == true)
					{
						_database.AddInParameter(cmd, "@IsAll", SqlDbType.Bit, 1);
					}
					else
					{
						_database.AddInParameter(cmd, "@IsAll", SqlDbType.Bit, 0);
					}
					
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleFurtherAnalysisInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysis(DateTime begDate, DateTime endDate, Boolean isAll, Int32 sampleID)
		{
			List<SampleFurtherAnalysisInfo> result = new List<SampleFurtherAnalysisInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleFurtherAnalysis_SEL_UpdateStamp_SampleID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@BegDate", SqlDbType.Date, begDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, endDate);
					if (isAll == true)
					{
						_database.AddInParameter(cmd, "@IsAll", SqlDbType.Bit, 1);
					}
					else
					{
						_database.AddInParameter(cmd, "@IsAll", SqlDbType.Bit, 0);
					}

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleFurtherAnalysisInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//8/24/22
		public List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysis(Int32 sampleID)
		{
			List<SampleFurtherAnalysisInfo> result = new List<SampleFurtherAnalysisInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleFurtherAnalysis_SEL_SampleID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleFurtherAnalysisInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/28/22		
		public ErrorInfo UpdateSampleFurtherAnalysis(Int32 sampleFurtherAnalysisID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleFurtherAnalysis_UPD_SampleFurtherAnalysisID"))
				{
					_database.AddInParameter(cmd, "@SampleFurtherAnalysisID", SqlDbType.Int, sampleFurtherAnalysisID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}





		//5/13/19
		public List<UpdateIngredientLogInfo> GetUpdateIngredientLog(Int32 updateIngredientHeaderID)
		{
			List<UpdateIngredientLogInfo> result = new List<UpdateIngredientLogInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientLog_SEL_UpdateIngredientHeaderID"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientHeaderID", SqlDbType.Int, updateIngredientHeaderID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<UpdateIngredientLogInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ColumnDescriptionOverrideInfo> GetColumnDescriptionOverride(String tableName)
		{
			List<ColumnDescriptionOverrideInfo> result = new List<ColumnDescriptionOverrideInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ColumnDescriptionOverride_SEL_TableName"))
				{
					_database.AddInParameter(cmd, "@TableName", SqlDbType.NVarChar, tableName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ColumnDescriptionOverrideInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public VersionInfo GetVersion()
		{
			VersionInfo result = new VersionInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Version_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<VersionInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ApplicationInfo> GetAppID(String appName)
		{
			List<ApplicationInfo> result = new List<ApplicationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Application_SEL_AppName"))
				{
					_database.AddInParameter(cmd, "@AppName", SqlDbType.NVarChar, appName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ApplicationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<EventDayInfo> GetEventDay(Int32 eventID)
		{
			List<EventDayInfo> result = new List<EventDayInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EventDay_SEL_EventID"))
				{
					_database.AddInParameter(cmd, "@EventID", SqlDbType.Int, eventID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<EventDayInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<DayInfo> GetDay()
		{
			List<DayInfo> result = new List<DayInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Days_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DayInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<WeekInfo> GetWeek()
		{
			List<WeekInfo> result = new List<WeekInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Weeks_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<WeekInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/2/19
		public List<EventWeeklyInfo> GetEventWeekly(Int32 eventID)
		{
			List<EventWeeklyInfo> result = new List<EventWeeklyInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EventWeekly_SEL_EventID"))
				{
					_database.AddInParameter(cmd, "@EventID", SqlDbType.Int, eventID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<EventWeeklyInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/2/19
		public List<EventMonthlyInfo> GetEventMonthly(Int32 eventID)
		{
			List<EventMonthlyInfo> result = new List<EventMonthlyInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EventMonthly_SEL_EventID"))
				{
					_database.AddInParameter(cmd, "@EventID", SqlDbType.Int, eventID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<EventMonthlyInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<MonthInfo> GetMonth()
		{
			List<MonthInfo> result = new List<MonthInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Months_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<MonthInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//3/15/19
		public List<EventsInfo> GetEvents()
		{
			List<EventsInfo> result = new List<EventsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Event_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<EventsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/3/19
		public List<EventsInfo> GetEvents(Boolean eventStatus)
		{
			List<EventsInfo> result = new List<EventsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Event_SEL_EventStatus"))
				{
					_database.AddInParameter(cmd, "@EventStatus", SqlDbType.Bit, eventStatus);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<EventsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//3/15/19
		public List<EventRunTypeInfo> GetEventRunType()
		{
			List<EventRunTypeInfo> result = new List<EventRunTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EventRunType_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<EventRunTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//3/15/19
		public List<EventTypeInfo> GetEventType()
		{
			List<EventTypeInfo> result = new List<EventTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EventType_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<EventTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//12/1/22
		//public List<PlusMinusInfo> GetPlusMinus()
		public List<PlusMinusInfo> GetPlusMinus(Boolean includeNone)
		{
			List<PlusMinusInfo> result = new List<PlusMinusInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PlusMinus_SEL_IncludeExcludeNone"))
				{
					_database.AddInParameter(cmd, "@IncludeNone", SqlDbType.Bit, includeNone);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PlusMinusInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/28/21
		public List<YesNoInfo> GetYesNo()
		{
			List<YesNoInfo> result = new List<YesNoInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_YesNo_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<YesNoInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		
		public List<GroundUnGroundInfo> GetGroundUnGround()
		{
			List<GroundUnGroundInfo> result = new List<GroundUnGroundInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GroundUnGround_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<GroundUnGroundInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/8/24
		public List<GroundUnGroundInfo> GetGroundUnGround(Int32 groundUngroundID)
		{
			List<GroundUnGroundInfo> result = new List<GroundUnGroundInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GroundUnGround_SEL_GroundUnGroundID"))
				{
					_database.AddInParameter(cmd, "@GroundUnGroundID", SqlDbType.Int, groundUngroundID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<GroundUnGroundInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
			

		//1/17/24
		public List<GroundUnGroundInfo> GetGroundUnGroundPNE()
		{
			List<GroundUnGroundInfo> result = new List<GroundUnGroundInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GroundUnGround_SEL_isPNE"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<GroundUnGroundInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		//3/15/19
		public ErrorInfo SaveEvent(ref EventsInfo eventsInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Events_SAV"))
				{
					cmd.Parameters.AddRange(eventsInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//3/15/19
		public ErrorInfo SaveEventWeekly(Int32 eventWeeklyID, Int32 eventID, Int32 weekID, Int32 recurEvery)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EventWeekly_SAV"))
				{
					_database.AddInParameter(cmd, "@EventWeeklyID", SqlDbType.Int, eventWeeklyID);
					_database.AddInParameter(cmd, "@EventID", SqlDbType.Int, eventID);
					_database.AddInParameter(cmd, "@WeekID", SqlDbType.Int, weekID);
					_database.AddInParameter(cmd, "@RecurEvery", SqlDbType.Int, recurEvery);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//3/15/19
		public ErrorInfo SaveEventDaily(Int32 eventDailyID, Int32 eventID, Int32 dayID, Int32 recurEvery)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EventDaily_SAV"))
				{
					_database.AddInParameter(cmd, "@EventDailyID", SqlDbType.Int, eventDailyID);
					_database.AddInParameter(cmd, "@EventID", SqlDbType.Int, eventID);
					_database.AddInParameter(cmd, "@DayID", SqlDbType.Int, dayID);
					_database.AddInParameter(cmd, "@RecurEvery", SqlDbType.Int, recurEvery);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//3/19/19        
		public ErrorInfo SaveEventMonthlySpecificDays(Int32 ID, Int32 eventMonthlyID, Int32 dayID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EventMonthlySpecificDays_SAV"))
				{
					_database.AddInParameter(cmd, "@EventMonthlySpecificDaysID", SqlDbType.Int, ID);
					_database.AddInParameter(cmd, "@EventMonthlyID", SqlDbType.Int, eventMonthlyID);
					_database.AddInParameter(cmd, "@DayID", SqlDbType.Int, dayID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//3/19/19        
		public ErrorInfo SaveEventMonthlySpecificWeekDays(Int32 ID, Int32 eventMonthlyID, Int32 weekID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EventMonthlySpecificWeekDays_SAV"))
				{
					_database.AddInParameter(cmd, "@EventMonthlySpecificWeekDaysID", SqlDbType.Int, ID);
					_database.AddInParameter(cmd, "@EventMonthlyID", SqlDbType.Int, eventMonthlyID);
					_database.AddInParameter(cmd, "@WeekID", SqlDbType.Int, weekID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//3/19/19        
		public ErrorInfo SaveEventMonthlySpecificWeekNumber(Int32 ID, Int32 eventMonthlyID, Int32 weekNumberID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EventMonthlySpecificWeekNumber_SAV"))
				{
					_database.AddInParameter(cmd, "@EventMonthlySpecificWeekNumberID", SqlDbType.Int, ID);
					_database.AddInParameter(cmd, "@EventMonthlyID", SqlDbType.Int, eventMonthlyID);
					_database.AddInParameter(cmd, "@WeekNumberID", SqlDbType.Int, weekNumberID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveEventMonthly(Int32 eventMonthlyID, Int32 eventID, Int32 monthID, Boolean specificDays, Boolean specificWeek)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EventMonthly_SAV"))
				{
					_database.AddInParameter(cmd, "@EventMonthlyID", SqlDbType.Int, eventMonthlyID);
					_database.AddInParameter(cmd, "@EventID", SqlDbType.Int, eventID);
					_database.AddInParameter(cmd, "@MonthID", SqlDbType.Int, monthID);
					_database.AddInParameter(cmd, "@SpecificDays", SqlDbType.Bit, specificDays);
					_database.AddInParameter(cmd, "@SpecificWeek", SqlDbType.Bit, specificWeek);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public List<UserAccessInfo> GetUserAccess(Int32 cfcUserID, Int32 permissionID)
		{
			List<UserAccessInfo> result = new List<UserAccessInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserAccess_SEL_CFCUserID_PermissionID"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.AddInParameter(cmd, "@PermissionID", SqlDbType.Int, permissionID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<UserAccessInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleLogInfo> GetSampleLog(Int32 sampleID)
		{
			List<SampleLogInfo> result = new List<SampleLogInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleLog_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleLogInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleLogInfo> GetSampleLog(Boolean isActive)
		{
			List<SampleLogInfo> result = new List<SampleLogInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleLog_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleLogInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierInfo> GetAnalyzerSupplier(Int32 analyzerSupplierID, Int32 datasourceTypeID, Int32 isActive)
		{
			List<AnalyzerSupplierInfo> result = new List<AnalyzerSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplier_SEL_SupplierID_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, analyzerSupplierID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/18/22
		public QueryOperationSampleDateInfo GetTemplateSampleDates(Int32 queryOperationID)
		{
			QueryOperationSampleDateInfo result = new QueryOperationSampleDateInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryOperationID"))
				{
					_database.AddInParameter(cmd, "@QueryOperationID", SqlDbType.Int, queryOperationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<QueryOperationSampleDateInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		public AnalyzerSupplierInfo GetAnalyzerSupplier(Int32 analyzerSupplierID, Int32 datasourceTypeID)
		{
			AnalyzerSupplierInfo result = new AnalyzerSupplierInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplier_SEL_AnalyzerSupplierID_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, analyzerSupplierID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerSupplierInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AnalyzerSupplierLocationInfo GetAnalyzerSupplierLocation(Int32 datasourceTypeID, String analyzerSupplierLocationCode)
		{
			AnalyzerSupplierLocationInfo result = new AnalyzerSupplierLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SEL_AnalyzerSupplierLocationCode_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationCode", SqlDbType.NVarChar, analyzerSupplierLocationCode);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerSupplierLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AnalyzerOwnerLocationInfo GetAnalyzerOwnerLocationDataSourceTypeOwnerCode(Int32 datasourceTypeID, String analyzerOwnerLocationCode)
		{
			AnalyzerOwnerLocationInfo result = new AnalyzerOwnerLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_SEL_DataSourceTypeID_OwnerLocationCode"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerOwnerLocationCode", SqlDbType.NVarChar, analyzerOwnerLocationCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerOwnerLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocation(Int32 analyzerSupplierLocationID, Int32 datasourceTypeID, Int32 isActive)
		{
			List<AnalyzerSupplierLocationInfo> result = new List<AnalyzerSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SEL_SupplierLocationID_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationID", SqlDbType.Int, analyzerSupplierLocationID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AnalyzerSupplierLocationInfo GetAnalyzerSupplierLocation(Int32 datasourceTypeID, Int32 analyzerSupplierID, String analyzerSupplierLocationCode)
		{
			AnalyzerSupplierLocationInfo result = new AnalyzerSupplierLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SEL_DataSourceTypeID_SupplierID_SupplierLocationCode"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationCode", SqlDbType.NVarChar, analyzerSupplierLocationCode);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, analyzerSupplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerSupplierLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/17/18
		public List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocationNameList(Int32 datasourceTypeID, Int32 analyzerSupplierID, String analyzerSupplierLocationName)
		{
			List<AnalyzerSupplierLocationInfo> result = new List<AnalyzerSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SEL_DataSourceTypeID_SupplierID_SupplierLocationName"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, analyzerSupplierID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationName", SqlDbType.NVarChar, analyzerSupplierLocationName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public AnalyzerSupplierLocationInfo GetAnalyzerSupplierLocationName(Int32 datasourceTypeID, Int32 analyzerSupplierID, String analyzerSupplierLocationName)
		{
			AnalyzerSupplierLocationInfo result = new AnalyzerSupplierLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SEL_DataSourceTypeID_SupplierID_SupplierLocationName"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, analyzerSupplierID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationName", SqlDbType.NVarChar, analyzerSupplierLocationName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerSupplierLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AnalyzerNutrientInfo GetAnalyzerNutrientUnits(Int32 analyzerNutrientID, Int32 datasourceTypeID)
		{
			AnalyzerNutrientInfo result = new AnalyzerNutrientInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerNutrient_SEL_AnalyzerNutrientID_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerNutrientID", SqlDbType.Int, analyzerNutrientID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerNutrientInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AnalyzerNutrientInfo GetAnalyzerNutrient(Int32 datasourceTypeID, String analyzerNutrientCode)
		{
			AnalyzerNutrientInfo result = new AnalyzerNutrientInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerNutrient_SEL_AnalyzerNutrientID_DataSourceTypeID_AnalyzerNutrientCode"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerNutrientCode", SqlDbType.VarChar, analyzerNutrientCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerNutrientInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/4/20
		public List<ChartSampleListInfo> GetChartSampleList(DateTime startDate, DateTime endDate, Int32 materialID, Int32 nutrientID, Int32 supplierID, Int32 supplierLocationID)
		{
			List<ChartSampleListInfo> result = new List<ChartSampleListInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_Chart_SampleList"))
				{
					_database.AddInParameter(cmd, "@StartDate", SqlDbType.DateTime, startDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.DateTime, endDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, materialID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, nutrientID);
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, supplierID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, supplierLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ChartSampleListInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<GraphInfo> GetSampleForGraphTest()
		{
			List<GraphInfo> result = new List<GraphInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_Chart"))
				{
					cmd.CommandTimeout = 60;
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<GraphInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuarantee(Boolean isActive)
		{
			List<AIMSupplierLocationGuaranteeInfo> result = new List<AIMSupplierLocationGuaranteeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocationGuarantee_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGuaranteeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/28/20
		public List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuarantee(Boolean isActive, Int32 aimSupplierID)
		{
			List<AIMSupplierLocationGuaranteeInfo> result = new List<AIMSupplierLocationGuaranteeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocationGuarantee_SEL_IsActive_AIMSourceID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, aimSupplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGuaranteeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/28/20
		public List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuarantee(Boolean isActive, Int32 aimSupplierID, Int32 aimMaterialID, Int32 aimNutrientID)
		{
			List<AIMSupplierLocationGuaranteeInfo> result = new List<AIMSupplierLocationGuaranteeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocationGuarantee_SEL_IsActive_AIMSourceID_AIMMaterialID_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, aimSupplierID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGuaranteeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/28/20
		public List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuarantee(Int32 aimSupplierID, Int32 aimSupplierLocationID, Int32 aimMaterialID, Int32 aimNutrientID)
		{
			List<AIMSupplierLocationGuaranteeInfo> result = new List<AIMSupplierLocationGuaranteeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocationGuarantee_SEL_AIMSourceID_AIMSourceLocationID_AIMMaterialID_AIMNutrientID_AllStatus"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, aimSupplierID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, aimSupplierLocationID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGuaranteeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/28/20
		public List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuarantee(Boolean isActive, Int32 aimSupplierID, Int32 aimSupplierLocationID, Int32 aimMaterialID, Int32 aimNutrientID)
		{
			List<AIMSupplierLocationGuaranteeInfo> result = new List<AIMSupplierLocationGuaranteeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocationGuarantee_SEL_IsActive_AIMSourceID_AIMSourceLocationID_AIMMaterialID_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, aimSupplierID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, aimSupplierLocationID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGuaranteeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/28/20
		public ErrorInfo DeleteAIMSupplierLocationGuarantee(Int32 aimSupplierLocationGuaranteeID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationGuarantee_DEL_AIMSupplierLocationGuaranteeID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierLocationGuaranteeID", SqlDbType.Int, aimSupplierLocationGuaranteeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//2/23/24
		public ErrorInfo DeletePNECredit(Int32 pneCreditID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNECredit_DEL"))
				{
					_database.AddInParameter(cmd, "@PNECreditID", SqlDbType.Int, pneCreditID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}					
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//7/2/20
		public List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuarantee(Int32 aimMaterialID)
		{
			List<AIMSupplierLocationGuaranteeInfo> result = new List<AIMSupplierLocationGuaranteeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocationGuarantee_SEL_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGuaranteeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/2/20
		public List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuaranteeNutrient(Int32 aimNutrientID, Boolean isActive)
		{
			List<AIMSupplierLocationGuaranteeInfo> result = new List<AIMSupplierLocationGuaranteeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocationGuarantee_SEL_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGuaranteeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//12/3/20  -- one stp to handle all the variations
		public List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuaranteesFiltered(Boolean isActive, Int32 aimSupplierID, Int32 aimSupplierLocationID, Int32 aimMaterialID, Int32 aimNutrientID)
		{
			List<AIMSupplierLocationGuaranteeInfo> result = new List<AIMSupplierLocationGuaranteeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocationGuarantees_SEL_IsActive_AIMSourceID_AIMSourceLocationID_AIMMaterialID_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, aimSupplierID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, aimSupplierLocationID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGuaranteeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//testing
		//public ErrorInfo SendSqlStatement(String sql)
		//{
		//	ErrorInfo result = new ErrorInfo();
		//	try
		//	{
		//		//CommandType txt = CommandType.Text;				
		//		//using (DbCommand cmd = _database.ExecuteNonQuery(txt, sql));
		//		//{					
		//		//	using (IDataReader reader = _database.ExecuteReader(cmd))
		//		//	{
		//		//		result = ObjectFiller<ErrorInfo>.GetScalar(reader);
		//		//	}
		//		//}
		//	}
		//	catch (Exception ex)
		//	{
		//		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//		result.ErrorNumber = -1;
		//	}
		//	return result;
		//}


		public ErrorInfo GetCFCIngredientNutrientOverlayCount()
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCIngredientNutrientOverlay_SEL_COUNT"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo GetSampleOldestSampleDate()
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_TOP1_SampleDate"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}



		public ErrorInfo GetUserDefinedDataSourceTemplateCount(Int32 defID, String colName)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_SEL_COUNT_DataSourceDefinitionID"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, defID);
					_database.AddInParameter(cmd, "@MapColumnName", SqlDbType.NVarChar, colName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo GetSampleImportLogNextBatchID()
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleImportLog_SEL_NEXT_BatchID"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveImportSQLSampleMissing(Int32 logID, String sampleCode, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportSQLSampleMissing_SAV"))
				{
					_database.AddInParameter(cmd, "@ImportSQLSampleLogID", SqlDbType.Int, logID);
					_database.AddInParameter(cmd, "@MissingSampleCode", SqlDbType.NVarChar, sampleCode);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveImportSQLSampleLog(ref ImportSQLSampleLogInfo log)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportSQLSampleLog_SAV_ConfigID"))
				{
					cmd.Parameters.AddRange(log.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public List<ImportSQLSampleLogInfo> GetImportSQLSampleLog(Int32 importConfigID)
		{
			List<ImportSQLSampleLogInfo> result = new List<ImportSQLSampleLogInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportSQLSampleLog_SEL_ImportConfigID"))
				{
					_database.AddInParameter(cmd, "@ImportConfigID", SqlDbType.Int, importConfigID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ImportSQLSampleLogInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ImportSQLSampleMissingInfo> GetImportSQLSampleMissing()
		{
			List<ImportSQLSampleMissingInfo> result = new List<ImportSQLSampleMissingInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportSQLSampleMissing_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ImportSQLSampleMissingInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public ErrorInfo GetUserDefinedDataSourceTemplateMapColumnName(Int32 defID, String mapColumnName)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_SEL_UserDefinedSourceDefinitionID_MapColumnName"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, defID);
					_database.AddInParameter(cmd, "@MapColumnName", SqlDbType.NVarChar, mapColumnName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//12/23/20
		public UserDefinedDataSourceTemplateInfo GetUserDefinedDataSourceTemplateMapColumnNameTop(Int32 defID, String mapColumnName)
		{
			UserDefinedDataSourceTemplateInfo result = new UserDefinedDataSourceTemplateInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_SEL_Top_DefinitionID_MapColumnName"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, defID);
					_database.AddInParameter(cmd, "@MapColumnName", SqlDbType.NVarChar, mapColumnName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<UserDefinedDataSourceTemplateInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public ErrorInfo GetDataSourceHeaderDetail(String analyzerDataSourceTypeName)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceHeaderDetail_SEL_AnalyzerDataSourceTypeName"))
				{
					_database.AddInParameter(cmd, "@AnalyzerDataSourceTypeName", SqlDbType.VarChar, analyzerDataSourceTypeName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo GetNextGeneratedSampleNumber(Int32 dataSourceTypeID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_AutoGenerated"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public List<MathTypeInfo> GetMathType(Boolean isActive)
		{
			List<MathTypeInfo> result = new List<MathTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_MathType_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<MathTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/3/18        
		public List<SampleQualityEquationInfo> GetEquationID(Int32 ID)
		{
			List<SampleQualityEquationInfo> result = new List<SampleQualityEquationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityEquation_SEL_SampleQualityEquationID"))
				{
					_database.AddInParameter(cmd, "SampleQualityEquationID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityEquationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleQualityEquationBuildInfo> GetSampleEquationBuild_SampleEquationBuildID(Int32 ID)
		{
			List<SampleQualityEquationBuildInfo> result = new List<SampleQualityEquationBuildInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityEquationBuild_SEL_SampleQualityEquationID"))
				{
					_database.AddInParameter(cmd, "SampleQualityEquationID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityEquationBuildInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<MathTypeInfo> GetMathTypeID(Int32 mathTypeID)
		{
			List<MathTypeInfo> result = new List<MathTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_MathType_SEL_MathTypeID"))
				{
					_database.AddInParameter(cmd, "@IMathTypeID", SqlDbType.Int, mathTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<MathTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<MathTypeInfo> GetMathTypeSorted(Boolean isActive)
		{
			List<MathTypeInfo> result = new List<MathTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_MathType_SEL_IsActive_SortBy_Equation"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<MathTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/3/18
		public List<SampleQualityEquationInfo> GetQualityControlEquations(Boolean isActive)
		{
			List<SampleQualityEquationInfo> result = new List<SampleQualityEquationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityEquation_SEL_IsActive_SortBy_Equation"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityEquationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/5/22  
		public List<AIMMaterialInfo> GetAIMMaterialWithOrMissingXref(Boolean isActive, Boolean IsXref)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_CFCIngredientRefAIMMaterial_SEL_IsActive_IsXref"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@IsXref", SqlDbType.Bit, IsXref);
					cmd.CommandTimeout = 0;

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/5/22
		public List<CFCIngredientInfo> GetAllCFCIngredientAimDatabaseIDWithOrMissingXref(Boolean isActive, Int32 aimDatabaseID, Boolean IsXref)
		{
			List<CFCIngredientInfo> result = new List<CFCIngredientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCIngredient_CFCIngredientRefAIMMaterial_SEL_IsActive_AimDatabaseID_IsXref"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					_database.AddInParameter(cmd, "@IsXref", SqlDbType.Bit, IsXref);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCIngredientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/5/24
		public List<AIMMaterialInfo> GetAIMMaterialProductWithOrMissingXref(Boolean isActive, Boolean IsXref)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_CFCProductRefAIMMaterial_SEL_IsActive_IsXref"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@IsXref", SqlDbType.Bit, IsXref);
					cmd.CommandTimeout = 0;

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMMaterialInfo> GetAIMMaterial(Boolean isActive)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);

					cmd.CommandTimeout = 0;

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//10/13/23
		//public List<AIMMaterialInfo> GetAIMMaterial_MaterialTypeID_IsActive(Int32 materialTypeID, Boolean isActive)
		public List<AIMMaterialInfo> GetAIMMaterial_MaterialTypeID_IsActive(Int32 materialTypeID, Boolean isActive, Int32 orderBy)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_IsActive_MaterialTypeID"))
				{
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, materialTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);

					_database.AddInParameter(cmd, "@OrderBy", SqlDbType.Int, orderBy);                          //10/13/23

					cmd.CommandTimeout = 0;

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/23/21  
		public List<AIMMaterialInfo> GetAIMMaterialForSampleDates(Boolean isActive, DateTime startDate, DateTime endDate, Int32 materialTypeID)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_IsActive_SampleDate"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@StartDate", SqlDbType.Date, startDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, endDate);

					//1/6/23
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, materialTypeID);

					cmd.CommandTimeout = 0;

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMMaterialInfo> GetAIMMaterialOrderByMaterialName(Boolean isActive)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_IsActive_OrderByMaterialName"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMMaterialInfo> GetAIMMaterial(Boolean isActive, Int32 aimOwnerLocationID, Int32 materialTypeID)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_IsActive_OwnerLocationID_MaterialTypeID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, materialTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//5/19/23
		public List<SpecificProductsInfo> GetSpecificProducts(Int32 groupID, Boolean isActive, Int32 aimOwnerLocationID)
		{
			List<SpecificProductsInfo> result = new List<SpecificProductsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_SpecificProductsGroupID_IsActive_OwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@SpecificProductsGroupID", SqlDbType.Int, groupID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SpecificProductsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//10/13/23
		//public List<AIMMaterialInfo> GetAIMMaterial_Active_MaterialType(Boolean isActive, Int32 materialTypeID)
		public List<AIMMaterialInfo> GetAIMMaterial_Active_MaterialType(Boolean isActive, Int32 materialTypeID, Int32 orderBy)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_IsActive_MaterialTypeID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, materialTypeID);
					_database.AddInParameter(cmd, "@OrderBy", SqlDbType.Int, orderBy);							//10/13/23
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//9/23/22
		public List<SpecificIngredientsInfo> GetSpecificIngredients()
		{
			List<SpecificIngredientsInfo> result = new List<SpecificIngredientsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredients_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SpecificIngredientsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//9/23/22
		public List<SpecificIngredientsInfo> GetSpecificIngredients(Int32 groupID, Boolean isActive)
		{
			List<SpecificIngredientsInfo> result = new List<SpecificIngredientsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredients_SEL_GroupID_IsActive"))
				{
					_database.AddInParameter(cmd, "@SpecificIngredientsGroupID", SqlDbType.Int, groupID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SpecificIngredientsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//10/19/22		
		public List<SpecificIngredientsGroupInfo> GetSpecificIngredientsGroup(Boolean isActive)
		{
			List<SpecificIngredientsGroupInfo> result = new List<SpecificIngredientsGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredientsGroup_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SpecificIngredientsGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//5/18/23
		public List<SpecificProductsGroupInfo> GetSpecificProductsGroup(Boolean isActive)
		{
			List<SpecificProductsGroupInfo> result = new List<SpecificProductsGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificProductsGroup_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SpecificProductsGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//9/23/22
		public List<SpecificIngredientsInfo> GetSpecificIngredients(Int32 aimMaterialID)
		{
			List<SpecificIngredientsInfo> result = new List<SpecificIngredientsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredients_SEL_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SpecificIngredientsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//5/18/23
		public List<SpecificProductsInfo> GetSpecificProducts(Int32 aimMaterialID)
		{
			List<SpecificProductsInfo> result = new List<SpecificProductsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificProducts_SEL_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SpecificProductsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//10/19/22
		public List<SpecificIngredientsInfo> GetSpecificIngredients_GroupID(Int32 groupID)
		{
			List<SpecificIngredientsInfo> result = new List<SpecificIngredientsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredients_SEL_SpecificIngredientsGroupID"))
				{
					_database.AddInParameter(cmd, "@SpecificIngredientsGroupID", SqlDbType.Int, groupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SpecificIngredientsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//5/18/23
		public List<SpecificProductsInfo> GetSpecificProducts_GroupID(Int32 groupID)
		{
			List<SpecificProductsInfo> result = new List<SpecificProductsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificProducts_SEL_SpecificProductsGroupID"))
				{
					_database.AddInParameter(cmd, "@SpecificProductsGroupID", SqlDbType.Int, groupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SpecificProductsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/18/20
		public List<AIMMaterialStockStatusInfo> GetAIMMaterialStockStatus(Boolean isActive)
		{
			List<AIMMaterialStockStatusInfo> result = new List<AIMMaterialStockStatusInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialStockStatus_SEL"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialStockStatusInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//6/5/20
		public List<AIMMaterialInfo> GetAIMMaterial_Active_Products(Boolean isActive)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_Products_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//10/7/20 - copied, commented and renamed below
		//public List<AIMMaterialInfo> GetAIMMaterial_Active_MaterialType_For_Sample(Boolean isActive, Int32 materialTypeID, Int32 ownerLocationID)
		//{
		//    List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
		//    try
		//    {
		//        using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_IsActive_MaterialTypeID_For_Sample"))
		//        {
		//            _database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
		//            _database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, materialTypeID);
		//
		//            //6/18/20
		//            _database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, ownerLocationID);
		//
		//            using (IDataReader reader = _database.ExecuteReader(cmd))
		//            {
		//                ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
		//            }
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//    }
		//    return result;
		//}
		public List<AIMMaterialInfo> GetAIMMaterial_Active_MaterialType_OwnerLocation_For_Sample(Boolean isActive, Int32 materialTypeID, Int32 ownerLocationID)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_IsActive_MaterialTypeID_For_Sample"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, materialTypeID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, ownerLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMMaterialInfo> GetAIMMaterial(String aimMaterialCode, Int32 aimOwnerLocationID)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_AIMMaterialCode_AIMOwnerLocationID"))
				{

					_database.AddInParameter(cmd, "@AIMMaterialCode", SqlDbType.NVarChar, aimMaterialCode);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMMaterialInfo> GetAIMMaterialAIMMaterialCode(String aimMaterialCode)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_AIMMaterialCode"))
				{

					_database.AddInParameter(cmd, "@AIMMaterialCode", SqlDbType.NVarChar, aimMaterialCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//10/9/23
		public List<AIMMaterialInfo> GetAIMMaterialAIMMaterialCodeRange(String aimMaterialCodeBeg, String aimMaterialCodeEnd)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_AIMMaterialCodeBeg_AIMMaterialCodeEnd"))
				{

					_database.AddInParameter(cmd, "@AIMMaterialCodeBeg", SqlDbType.NVarChar, aimMaterialCodeBeg);
					_database.AddInParameter(cmd, "@AIMMaterialCodeEnd", SqlDbType.NVarChar, aimMaterialCodeEnd);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<WarningLimitsInfo> GetAllWarningLimits()
		{
			List<WarningLimitsInfo> result = new List<WarningLimitsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_WarningLimits_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<WarningLimitsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/2/20
		public List<WarningLimitsInfo> GetWarningLimitsForMaterial(Int32 aimMaterialID)
		{
			List<WarningLimitsInfo> result = new List<WarningLimitsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_WarningLimits_SEL_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<WarningLimitsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/2/20
		public List<WarningLimitsInfo> GetWarningLimitsForNutrient(Int32 aimNutrientID)
		{
			List<WarningLimitsInfo> result = new List<WarningLimitsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_WarningLimits_SEL_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<WarningLimitsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/2/18
		public List<SampleQualityParameterInfo> GetAllQualityParameters()
		{
			List<SampleQualityParameterInfo> result = new List<SampleQualityParameterInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityParameter_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityParameterInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//3/22/18 - added dataSourceTypeID
		//3/15/18
		public SampleQualityParameterInfo GetQualityParameterName(String name, Int32 dataSourceTypeID)
		{
			SampleQualityParameterInfo result = new SampleQualityParameterInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityParameter_SEL_SampleQualityParameterName"))
				{
					_database.AddInParameter(cmd, "@SampleQualityParameterName", SqlDbType.NVarChar, name);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<SampleQualityParameterInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/4/18
		public SampleQualityParameterInfo GetQualityParameterID(Int32 id)
		{
			SampleQualityParameterInfo result = new SampleQualityParameterInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityParameter_SEL_SampleQualityParameterID"))
				{
					_database.AddInParameter(cmd, "@SampleQualityParameterID", SqlDbType.Int, id);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<SampleQualityParameterInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/25/20
		public SystemMasterAndDatabaseFilesInfo GetMasterAndDatabaseInfo(String databaseName, String fileType)
		{
			SystemMasterAndDatabaseFilesInfo result = new SystemMasterAndDatabaseFilesInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SystemMasterDatabaseInfo_SEL"))
				{
					_database.AddInParameter(cmd, "@DatabaseName", SqlDbType.VarChar, databaseName);
					_database.AddInParameter(cmd, "@FileType", SqlDbType.VarChar, fileType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<SystemMasterAndDatabaseFilesInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<UserDefinedDataSourceDefinitionInfo> GetAllUserDefinedDataSourceDefinition()
		{
			List<UserDefinedDataSourceDefinitionInfo> result = new List<UserDefinedDataSourceDefinitionInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceDefinition_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<UserDefinedDataSourceDefinitionInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//3/20/19
		public List<UserDefinedDataSourceDefinitionInfo> GetAllUserDefinedDataSourceDefinition_ForScheduler()
		{
			List<UserDefinedDataSourceDefinitionInfo> result = new List<UserDefinedDataSourceDefinitionInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceDefinition_SEL_ForScheduler"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<UserDefinedDataSourceDefinitionInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		public List<UserDefinedDataSourceTemplateInfo> GetAllUserDefinedDataSourceTemplate()
		{
			List<UserDefinedDataSourceTemplateInfo> result = new List<UserDefinedDataSourceTemplateInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<UserDefinedDataSourceTemplateInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<UserDefinedDataSourceTemplateInfo> GetUserDefinedDataSourceTemplate(Int32 ID)
		{
			List<UserDefinedDataSourceTemplateInfo> result = new List<UserDefinedDataSourceTemplateInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_SEL_DataSourceDefinitionID"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<UserDefinedDataSourceTemplateInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<EmailAddressXRefMessageInfo> GetAllEmailMessageXrefMessage()
		{
			List<EmailAddressXRefMessageInfo> result = new List<EmailAddressXRefMessageInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EmailAddressXRefMessage_SEL_ALL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<EmailAddressXRefMessageInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<WebServiceInfo> GetAllWebService()
		{
			List<WebServiceInfo> result = new List<WebServiceInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_WebService_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<WebServiceInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<MaterialTypeInfo> GetMaterialType(Boolean isActive)
		{
			List<MaterialTypeInfo> result = new List<MaterialTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_MaterialType_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<MaterialTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public AIMConfigInfo GetAIMConfig(Int32 CFCUserID)
		{
			AIMConfigInfo result = new AIMConfigInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMConfig_SEL_CFCUserID"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMConfigInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerAnalyseInfo> GetAnalyzerAnalyse(Boolean isActive)
		{
			List<AnalyzerAnalyseInfo> result = new List<AnalyzerAnalyseInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerAnalyse_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerAnalyseInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{

				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerAnalyseInfo> GetAnalyzerAnalyse(Int32 analyzerMaterialID)
		{
			List<AnalyzerAnalyseInfo> result = new List<AnalyzerAnalyseInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerAnalyse_SEL_AnalyzerMaterialID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, analyzerMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerAnalyseInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerAnalyseInfo> GetAnalyzerAnalyse()
		{
			List<AnalyzerAnalyseInfo> result = new List<AnalyzerAnalyseInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerAnalyse_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerAnalyseInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<UnitTypeInfo> GetAllUnitType()
		{
			List<UnitTypeInfo> result = new List<UnitTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UnitType_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<UnitTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalysisLogInfo> GetAllAnalysisLog(Boolean isActive)
		{
			List<AnalysisLogInfo> result = new List<AnalysisLogInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisLog_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisLogInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalysisViewTemplateInfo> GetAllAnalysisViewTemplate(Boolean isActive, Int32 gridNumber, Int32 templateType)
		{
			List<AnalysisViewTemplateInfo> result = new List<AnalysisViewTemplateInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisViewTemplate_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@GridNumber", SqlDbType.Int, gridNumber);
					_database.AddInParameter(cmd, "@TemplateType", SqlDbType.Int, templateType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisViewTemplateInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//4/12/18
		public List<AnalysisViewTemplateInfo> GetAnalysisViewTemplate_TemplateType(Int32 templateType)

		{
			List<AnalysisViewTemplateInfo> result = new List<AnalysisViewTemplateInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisViewTemplate_SEL_TemplateType"))
				{
					_database.AddInParameter(cmd, "@TemplateType", SqlDbType.Int, templateType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisViewTemplateInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ReportQueryInfo> GetAllReportQuery(Boolean isActive)
		{
			List<ReportQueryInfo> result = new List<ReportQueryInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ReportQuery_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ReportQueryInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AnalysisReportsInfo> GetDataThroughReportQuery(String query)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetSqlStringCommand(query))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AnalysisInfo> GetDataThroughAnalysisQuery(String query)
		{
			List<AnalysisInfo> result = new List<AnalysisInfo>();
			try
			{
				using (DbCommand cmd = _database.GetSqlStringCommand(query))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<ReportQueryDetailInfo> GetAllReportQueryDetail(Boolean isActive)
		{
			List<ReportQueryDetailInfo> result = new List<ReportQueryDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ReportQueryDetail_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ReportQueryDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ReportQueryDetailInfo> GetOneReportQueryDetail(Int32 reportQueryID, Boolean isActive)
		{
			List<ReportQueryDetailInfo> result = new List<ReportQueryDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ReportQueryDetail_SEL_ReportQueryID"))
				{
					_database.AddInParameter(cmd, "@ReportQueryID", SqlDbType.Int, reportQueryID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ReportQueryDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<TableColumnInfo> GetAllTablesColumnsFromDatabse()
		{
			List<TableColumnInfo> result = new List<TableColumnInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SEL_AllTableColumnNames"))
				{
					//_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<TableColumnInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/6/24
		public ErrorInfo UpdateCFCProductAimMaterialWithMissingXref(Int32 cfcProductID, String cfcProductCode, Int32 aimOwnerLocationID, Int32 cfcUserID)		
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductRefAIMMaterial_INS_Missing_Xref"))
				{
					_database.AddInParameter(cmd, "@CFCProductID", SqlDbType.Int, cfcProductID);
					_database.AddInParameter(cmd, "@CFCProductCode", SqlDbType.VarChar, cfcProductCode);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//6/4/24
		public List<CFCProductInfo> GetAllCFCProductAimDatabaseIDWithOrMissingXref(Boolean isActive, Int32 aimDatabaseID, Boolean isXref)
		{
			List<CFCProductInfo> result = new List<CFCProductInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProduct_CFCProductRefAIMMaterial_SEL_IsActive_AimDatabaseID_IsXref"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					_database.AddInParameter(cmd, "@IsXref", SqlDbType.Bit, isXref);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
			

		public List<CFCProductRefAIMMaterialInfo> GetAllCFCProductRefAIMMaterial()
		{
			List<CFCProductRefAIMMaterialInfo> result = new List<CFCProductRefAIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductRefAIMMaterial_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductRefAIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/5/24
		public List<CFCProductRefAIMMaterialInfo> GetMissingProductXRefs()
		{
			List<CFCProductRefAIMMaterialInfo> result = new List<CFCProductRefAIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductRefAIMMaterial_SEL_Missing_Xref"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductRefAIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/5/24
		public List<AIMMaterialInfo> GetAllAIMMaterialWithOrMissingXref(Int32 materialTypeID, Boolean isActive, Boolean isXref)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrient_CFCNutrientRefAIMNutrient_SEL_IsActive_IsXref"))
				{
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, isActive);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@IsXref", SqlDbType.Bit, isXref);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<CFCNutrientInfo> GetAllCFCNutrient(Boolean isActive)
		{
			List<CFCNutrientInfo> result = new List<CFCNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrient_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/12/24    
		public List<CFCNutrientInfo> GetCFCNutrientUpdateIngredientDetail(Int32 updateIngredientDetailID)
		{
			List<CFCNutrientInfo> result = new List<CFCNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrient_SEL_UpdateIngredientDetailID"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientDetailID", SqlDbType.Int, updateIngredientDetailID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//5/31/18
		public List<CFCNutrientInfo> GetCFCNutrientID(Int32 cfcNutrientID)
		{
			List<CFCNutrientInfo> result = new List<CFCNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrient_SEL_CFCNutrientID"))
				{
					_database.AddInParameter(cmd, "@CFCNutrientID", SqlDbType.Int, cfcNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCNutrientInfo> GetAllCFCNutrientInitialLoadAimDatabaseID(Boolean isActive, Int32 aimDatabaseID)
		{
			List<CFCNutrientInfo> result = new List<CFCNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrientInitialLoad_SEL_IsActive_AimDatabaseID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/5/22
		public List<CFCNutrientInfo> GetAllCFCNutrientAimDatabaseIDWithOrMissingXref(Boolean isActive, Int32 aimDatabaseID, Boolean isXref)
		{
			List<CFCNutrientInfo> result = new List<CFCNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrient_CFCNutrientRefAIMNutrient_SEL_IsActive_AimDatabaseID_IsXref"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					_database.AddInParameter(cmd, "@IsXref", SqlDbType.Bit, isXref);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCNutrientInfo> GetAllCFCNutrientAimDatabaseID(Boolean isActive, Int32 aimDatabaseID)
		{
			List<CFCNutrientInfo> result = new List<CFCNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrient_SEL_IsActive_AimDatabaseID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/5/21
		public List<AIMNutrientInfo> GetAllAIMNutrientWithOrMissingXref(Boolean isActive, Boolean isXref)
		{
			List<AIMNutrientInfo> result = new List<AIMNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrient_CFCNutrientRefAIMNutrient_SEL_IsActive_IsXref"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@IsXref", SqlDbType.Bit, isXref);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMNutrientInfo> GetAllAIMNutrient(Boolean isActive)
		{
			List<AIMNutrientInfo> result = new List<AIMNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrient_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMNutrientRefAnalyzerNutrientInfo> GetAllAIMNutrientRefAnalyzerNutrient()
		{
			List<AIMNutrientRefAnalyzerNutrientInfo> result = new List<AIMNutrientRefAnalyzerNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrientRefAnalyzerNutrient_SEL_All"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMNutrientRefAnalyzerNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMSupplierRefAnalyzerSupplierInfo> GetAllAIMSupplierRefAnalyzerSupplier()
		{
			List<AIMSupplierRefAnalyzerSupplierInfo> result = new List<AIMSupplierRefAnalyzerSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierRefAnalyzerSupplier_SEL_All"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierRefAnalyzerSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> GetAllAIMSupplierLocationRefAnalyzerSupplierLocation()
		{
			List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> result = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationRefAnalyzerSupplierLocation_SEL_All"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMNutrientRefAnalyzerNutrientInfo> GetAIMNutrientRefAnalyzerNutrientDataSourceTypeID(Int32 datasourceTypeID)
		{
			List<AIMNutrientRefAnalyzerNutrientInfo> result = new List<AIMNutrientRefAnalyzerNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrientRefAnalyzerNutrient_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMNutrientRefAnalyzerNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/18/18
		public List<AIMNutrientRefAnalyzerNutrientInfo> GetAIMNutrientRefAnalyzerNutrientDataSourceTypeIDAIMNutrientIDAnalyzerNutrientID(Int32 datasourceTypeID, Int32 aimNutrientID, Int32 analyzerNutrientID)
		{
			List<AIMNutrientRefAnalyzerNutrientInfo> result = new List<AIMNutrientRefAnalyzerNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrientRefAnalyzerNutrient_SEL_DataSourceTypeID_AIMNutrientID_AnalyzerNutrientID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					_database.AddInParameter(cmd, "@AnalyzerNutrientID", SqlDbType.Int, analyzerNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMNutrientRefAnalyzerNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMNutrientRefAnalyzerNutrientInfo> GetAIMNutrientRefAnalyzerNutrientDataSourceTypeIDAIMNutrientID(Int32 datasourceTypeID, Int32 aimNutrientID)
		{
			List<AIMNutrientRefAnalyzerNutrientInfo> result = new List<AIMNutrientRefAnalyzerNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrientRefAnalyzerNutrient_SEL_DataSourceTypeID_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMNutrientRefAnalyzerNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMSupplierRefAnalyzerSupplierInfo> GetAIMSupplierRefAnalyzerSupplierDataSourceTypeID(Int32 datasourceTypeID)
		{
			List<AIMSupplierRefAnalyzerSupplierInfo> result = new List<AIMSupplierRefAnalyzerSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierRefAnalyzerSupplier_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierRefAnalyzerSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMSupplierRefAnalyzerSupplierInfo> GetAIMSupplierRefAnalyzerSupplierDataSourceTypeIDAnalyzerSupplierID(Int32 datasourceTypeID, Int32 analyzerSupplierID)
		{
			List<AIMSupplierRefAnalyzerSupplierInfo> result = new List<AIMSupplierRefAnalyzerSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierRefAnalyzerSupplier_SEL_DataSourceTypeID_AnalyzerSupplierID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, analyzerSupplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierRefAnalyzerSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterialDataSourceTypeID(Int32 datasourceTypeID)
		{
			List<AIMMaterialRefAnalyzerMaterialInfo> result = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialRefAnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/11/23
		public List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDMaterialTypeID(Int32 datasourceTypeID, Int32 materialTypeID)
		{
			List<AIMMaterialRefAnalyzerMaterialInfo> result = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID_MaterialTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, materialTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialRefAnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDAnalyzerMaterialID(Int32 datasourceTypeID, Int32 analyzerMaterialID)
		{
			List<AIMMaterialRefAnalyzerMaterialInfo> result = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID_AnalyzerMaterialID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, analyzerMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialRefAnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDAIMMaterialID(Int32 datasourceTypeID, Int32 aimMaterialID)
		{
			List<AIMMaterialRefAnalyzerMaterialInfo> result = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialRefAnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMMaterialRefAnalyzerMaterialInfo> GetAllAIMMaterialRefAnalyzerMaterial()
		{
			List<AIMMaterialRefAnalyzerMaterialInfo> result = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialRefAnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCIngredientRefAIMMaterialInfo> GetCFCIngredientRefAIMMaterial()
		{
			List<CFCIngredientRefAIMMaterialInfo> result = new List<CFCIngredientRefAIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCIngredientRefAIMMaterial_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCIngredientRefAIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//11/30/23
		public List<CFCIngredientNutrientOverlayInfo> GetCFCIngredientNutrientOverlay()
		{
			List<CFCIngredientNutrientOverlayInfo> result = new List<CFCIngredientNutrientOverlayInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCIngredientNutrientOverlay_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCIngredientNutrientOverlayInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/4/24
		public List<CFCProductArchiveInfo> GetCFCProductArchive_ProductID(Int32 cfcProductID)
		{
			List<CFCProductArchiveInfo> result = new List<CFCProductArchiveInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductArchive_SEL_CFCProductID"))
				{
					_database.AddInParameter(cmd, "@CFCProductID", SqlDbType.Int, cfcProductID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductArchiveInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public CFCIngredientRefAIMMaterialInfo GetCFCIngredientRefAIMMaterial(Int32 cfcIngredientID)
		{
			CFCIngredientRefAIMMaterialInfo result = new CFCIngredientRefAIMMaterialInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCIngredientRefAIMMaterial_SEL_CFCIngredientID"))
				{
					_database.AddInParameter(cmd, "@CFCIngredientID", SqlDbType.Int, cfcIngredientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCIngredientRefAIMMaterialInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public CFCIngredientRefAIMMaterialInfo GetCFCIngredientRefAIMMaterialAimMaterialID(Int32 aimMaterialID)
		{
			CFCIngredientRefAIMMaterialInfo result = new CFCIngredientRefAIMMaterialInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCIngredientRefAIMMaterial_SEL_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCIngredientRefAIMMaterialInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		public CFCProductRefAIMMaterialInfo GetCFCProductRefAIMMaterial(Int32 cfcProductID)
		{
			CFCProductRefAIMMaterialInfo result = new CFCProductRefAIMMaterialInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductRefAIMMaterial_SEL_CFCProductID"))
				{
					_database.AddInParameter(cmd, "@CFCProductID", SqlDbType.Int, cfcProductID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCProductRefAIMMaterialInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//3/15/21  
		public CFCProductRefAIMMaterialInfo GetCFCProductRefAIMMaterialXRefID(Int32 ID)
		{
			CFCProductRefAIMMaterialInfo result = new CFCProductRefAIMMaterialInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductRefAIMMaterial_SEL_CFCProductRefAIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@CFCProductRefAIMMaterialID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCProductRefAIMMaterialInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public CFCProductRefAIMMaterialInfo GetCFCProductRefAIMMaterialAimMaterialID(Int32 aimMaterialID)
		{
			CFCProductRefAIMMaterialInfo result = new CFCProductRefAIMMaterialInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductRefAIMMaterial_SEL_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCProductRefAIMMaterialInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public CFCPlantRefAIMOwnerLocationInfo GetCFCPlantRefAIMOwnerLocation(Int32 cfcPlantID)
		{
			CFCPlantRefAIMOwnerLocationInfo result = new CFCPlantRefAIMOwnerLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlantRefAIMOwnerLocation_SEL_CFCPlantID"))
				{
					_database.AddInParameter(cmd, "@CFCPlantID", SqlDbType.Int, cfcPlantID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCPlantRefAIMOwnerLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public CFCNutrientRefAIMNutrientInfo GetCFCNutrientRefAIMNutrient(Int32 cfcNutrientID)
		{
			CFCNutrientRefAIMNutrientInfo result = new CFCNutrientRefAIMNutrientInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrientRefAIMNutrient_SEL_CFCNutrientID"))
				{
					_database.AddInParameter(cmd, "@CFCNutrientID", SqlDbType.Int, cfcNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCNutrientRefAIMNutrientInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/23/18
		public ErrorInfo UserDefinedDataSourceTemplate_UpdateMap(Int32 defID, Int32 templateID, Int32 mapColumnID, String mapColumnName)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_UPD_MapColumnForDataSourceType"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, defID);
					_database.AddInParameter(cmd, "@UserDefinedDataSourceTemplateID", SqlDbType.Int, templateID);
					_database.AddInParameter(cmd, "@MapColumnID", SqlDbType.Int, mapColumnID);
					_database.AddInParameter(cmd, "@MapColumnName", SqlDbType.NVarChar, mapColumnName);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/6/18
		public ErrorInfo UpdateQueryOperationIDIsAnd(Int32 queryTemplateID, Int32 queryFieldID, Int32 queryOperationID, Boolean isAnd, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_UPD_QueryOperationID_IsAnd"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.AddInParameter(cmd, "@QueryOperationID", SqlDbType.Int, queryOperationID);
					_database.AddInParameter(cmd, "@IsAnd", SqlDbType.Bit, isAnd);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//10/14/22		
		public ErrorInfo UpdateQueryTemplateDetailNutrient(Int32 queryTemplateID, Int32 driverAIMNutrientID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetailNutrient_UPD_DriverAIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@DriverAIMNutrientID", SqlDbType.Int, driverAIMNutrientID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/1/22		
		public ErrorInfo UpdateQueryTemplateDetailOrderBy(Int32 queryTemplateDetailID, Int32 orderBy, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_UPD_OrderBy"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateDetailID", SqlDbType.Int, queryTemplateDetailID);
					_database.AddInParameter(cmd, "@OrderBy", SqlDbType.Int, orderBy);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/8/22
		public ErrorInfo UpdateTemplateDetailQueryFieldID(Int32 queryTemplateDetailID, Int32 queryFieldID, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_UPD_QueryFieldID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateDetailID", SqlDbType.Int, queryTemplateDetailID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public CFCNutrientRefAIMNutrientInfo GetCFCNutrientRefAIMNutrientAIMID(Int32 aimNutrientID)
		{
			CFCNutrientRefAIMNutrientInfo result = new CFCNutrientRefAIMNutrientInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrientRefAIMNutrient_SEL_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCNutrientRefAIMNutrientInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<CFCNutrientRefAIMNutrientInfo> GetAllCFCNutrientRefAIMNutrient()
		{
			List<CFCNutrientRefAIMNutrientInfo> result = new List<CFCNutrientRefAIMNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrientRefAIMNutrient_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCNutrientRefAIMNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCNutrientRefAIMNutrientInfo> GetAllCFCNutrientRefAIMNutrientAimDatabase(Int32 aimDatabaseID)
		{
			List<CFCNutrientRefAIMNutrientInfo> result = new List<CFCNutrientRefAIMNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrientRefAIMNutrient_SEL_AimDatabaseID"))
				{
					_database.AddInParameter(cmd, "@AIMDatabaseID", SqlDbType.Int, aimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCNutrientRefAIMNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ReportInfo> GetAllReport()
		{
			List<ReportInfo> result = new List<ReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SEL_IsActive"))
				{
					//_database.AddInParameter(cmd, "@PrinterTypeID", SqlDbType.Int, printerTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//9/1/23
		public List<ReportCommentsInfo> GetReportComments(Int32 reportID)
		{
			List<ReportCommentsInfo> result = new List<ReportCommentsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ReportComments_SEL_ReportID"))
				{
					_database.AddInParameter(cmd, "@ReportID", SqlDbType.Int, reportID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ReportCommentsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ReportInfo> GetAllReport(Boolean isActive, Boolean isAdvanced)
		{
			List<ReportInfo> result = new List<ReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SEL_IsActive_IsAdvanced"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@IsAdvanced", SqlDbType.Bit, isAdvanced);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//9/5/23
		public List<ReportInfo> GetAllReportsAllowingComments(Boolean isActive, Boolean hasComments)
		{
			List<ReportInfo> result = new List<ReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SEL_IsActive_HasComments"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@HasComments", SqlDbType.Bit, hasComments);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ReportInfo> GetAllReportAnalysis()
		{
			List<ReportInfo> result = new List<ReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SEL_IsActive_IsAnalysis"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public ReportQueryInfo GetOneReportQuery(Int32 reportQueryID)
		{
			ReportQueryInfo result = new ReportQueryInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ReportQuery_SEL_ReportQueryID"))
				{
					_database.AddInParameter(cmd, "@ReportQueryID", SqlDbType.Int, reportQueryID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ReportQueryInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/15/18        
		public PrinterUserDefaultInfo GetUserPrinterDefault(Int32 cfcUserID, Int32 printerTypeID)
		{
			PrinterUserDefaultInfo result = new PrinterUserDefaultInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterUserDefault_SEL_CFCUserID_PrinterTypeID"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.AddInParameter(cmd, "@PrinterTypeID", SqlDbType.Int, printerTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<PrinterUserDefaultInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//6/15/18        
		public PrinterUserDefaultInfo GetUserPrinterDefault(Int32 printerTypeID)
		{
			PrinterUserDefaultInfo result = new PrinterUserDefaultInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterUserDefault_SEL_PrinterTypeID"))
				{
					_database.AddInParameter(cmd, "@PrinterTypeID", SqlDbType.Int, printerTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<PrinterUserDefaultInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//6/15/18
		public PrinterDefaultInfo GetPrinterDefault(Int32 printerTypeID)
		{
			PrinterDefaultInfo result = new PrinterDefaultInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterDefault_SEL_PrinterTypeID"))
				{
					_database.AddInParameter(cmd, "@PrinterTypeID", SqlDbType.Int, printerTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<PrinterDefaultInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//8/23/18
		public static String DefaultPrinterName()
		{
			String rtn;
			System.Drawing.Printing.PrinterSettings defaultPrinter = new System.Drawing.Printing.PrinterSettings();
			rtn = defaultPrinter.PrinterName;
			return rtn;

		}

		//6/15/18
		//public PrinterTypeInfo GetOnePrinterType(Int32 printerTypeID)
		public PrinterTypeInfo GetOnePrinterType(Int32 printerTypeID, Int32 cfcUserID)

		{
			PrinterTypeInfo result = new PrinterTypeInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterType_SEL_PrinterTypeID_CFCUserID"))
				{
					_database.AddInParameter(cmd, "@PrinterTypeID", SqlDbType.Int, printerTypeID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<PrinterTypeInfo>.GetScalar(reader);

						//8/8/23
						if (result != null)
						{
							//8/23/18
							if (result.PrinterName == "Default")
							{
								result.PrinterName = DefaultPrinterName();
							}
						}

					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public ReportInfo GetReportReportID(Int32 reportID)
		{
			ReportInfo result = new ReportInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SEL_ReportID"))
				{
					_database.AddInParameter(cmd, "@ReportID", SqlDbType.Int, reportID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ReportInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/15/18        
		public List<PrinterTypeInfo> GetPrinterTypeFromDefaultPrinters()
		{
			List<PrinterTypeInfo> result = new List<PrinterTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterDefault_PrinterType_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PrinterTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<PrinterTypeInfo> GetPrinterType()
		{
			List<PrinterTypeInfo> result = new List<PrinterTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterType_SEL_All"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PrinterTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/15/18        
		public List<PrinterUserDefaultInfo> GetPrinterUserDefaultAll()
		{
			List<PrinterUserDefaultInfo> result = new List<PrinterUserDefaultInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterUserDefault_SEL_All"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PrinterUserDefaultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//6/15/18
		public List<PrinterDefaultInfo> GetPrinterDefaultAll()
		{
			List<PrinterDefaultInfo> result = new List<PrinterDefaultInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterDefault_SEL_All"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PrinterDefaultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<EmailAddressInfo> GetEmailAddress()
		{
			List<EmailAddressInfo> result = new List<EmailAddressInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EmailAddress_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<EmailAddressInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalysisReportsInfo> GetAnalysisSummary(AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID)   //TODO - (Int32)_mainMenu.lueDataMap.EditValue = aimDataBaseID  
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisSummary"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDataBaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/20/18
		public List<AnalysisReportsInfo> GetAnalysisSummary_Product(AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID)   //TODO - (Int32)_mainMenu.lueDataMap.EditValue = aimDataBaseID  
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisSummary_Product"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDataBaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalysisReportsInfo> GetAnalysisDetail(AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID)  //TODO - (Int32)_mainMenu.lueDataMap.EditValue = aimDataBaseID
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisDetail"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDataBaseID);

					cmd.CommandTimeout = 0;

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/16/21
		//public List<AnalysisReportsInfo> GetAnalysisPivotData(AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID)  //TODO - (Int32)_mainMenu.lueDataMap.EditValue = aimDataBaseID
		public List<AnalysisReportsInfo> GetAnalysisPivotData(AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID, Boolean groupMaterials)  //TODO - (Int32)_mainMenu.lueDataMap.EditValue = aimDataBaseID
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_Analysis_Pivot_Data"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDataBaseID);

					//2/16/21					
					_database.AddInParameter(cmd, "@GroupMaterials", SqlDbType.Bit, groupMaterials);

					cmd.CommandTimeout = 0;

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/19/18
		public List<AnalysisReportsInfo> GetAnalysisDetail_Product(AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID)  //TODO - (Int32)_mainMenu.lueDataMap.EditValue = aimDataBaseID
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisDetail_Product"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDataBaseID);

					cmd.CommandTimeout = 0;

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/16/21
		//public List<AnalysisReportsInfo> GetAnalysisPivotData_Product(AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID)  //TODO - (Int32)_mainMenu.lueDataMap.EditValue = aimDataBaseID
		public List<AnalysisReportsInfo> GetAnalysisPivotData_Product(AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID, Boolean groupMaterials)  //TODO - (Int32)_mainMenu.lueDataMap.EditValue = aimDataBaseID
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_Analysis_Pivot_Data_Product"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDataBaseID);

					//2/16/21
					_database.AddInParameter(cmd, "@GroupMaterials", SqlDbType.Bit, groupMaterials);

					cmd.CommandTimeout = 0;

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/20/23
		public List<AnalysisReportsInfo> GetAnalysisPivotDataWithoutSampleAIM(AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID, Boolean groupMaterials)  //TODO - (Int32)_mainMenu.lueDataMap.EditValue = aimDataBaseID
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_Analysis_Pivot_Data_WithoutSampleAIM"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDataBaseID);
					_database.AddInParameter(cmd, "@GroupMaterials", SqlDbType.Bit, groupMaterials);

					cmd.CommandTimeout = 0;

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/26/23
		public List<AnalysisReportsInfo> GetAnalysisPivotData_ProductWithoutSampleAIM(AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID, Boolean groupMaterials)  //TODO - (Int32)_mainMenu.lueDataMap.EditValue = aimDataBaseID
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_Analysis_Pivot_Data_Product_WithoutSampleAIM"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDataBaseID);
					_database.AddInParameter(cmd, "@GroupMaterials", SqlDbType.Bit, groupMaterials);

					cmd.CommandTimeout = 0;

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AnalysisReportsInfo> GetAnalysisDetail(Int32 queryTemplateID, Int32 aimDatabaseID)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisDetail"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDatabaseID);
					cmd.CommandTimeout = 0;
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/20/18
		public List<AnalysisReportsInfo> GetAnalysisDetail_Product(Int32 queryTemplateID, Int32 aimDatabaseID)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisDetail_Product"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDatabaseID);
					cmd.CommandTimeout = 0;
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		// Version 1 of report 
		public List<AnalysisReportsInfo> GetAnalysisPivot(AnalysisReportsInfo analysisSummary, Boolean dryMatterBase, String aimMaterialList, String aimMaterialList2, String aimMaterialList3)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisPivot"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);
					_database.AddInParameter(cmd, "@DryMatterBase", SqlDbType.Bit, dryMatterBase);
					_database.AddInParameter(cmd, "@AIMMaterialList", SqlDbType.VarChar, aimMaterialList);
					_database.AddInParameter(cmd, "@AIMMaterialList2", SqlDbType.VarChar, aimMaterialList2);
					_database.AddInParameter(cmd, "@AIMMaterialList3", SqlDbType.VarChar, aimMaterialList3);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/19/21 - Version 2 of report 
		public List<AnalysisReportsInfo> GetAnalysisPivotOwnerLocation(AnalysisReportsInfo analysisSummary, Boolean dryMatterBase, String aimMaterialList, String aimMaterialList2, String aimMaterialList3)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisPivot_GroupByMaterialOwnerLocation"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);
					_database.AddInParameter(cmd, "@DryMatterBase", SqlDbType.Bit, dryMatterBase);
					_database.AddInParameter(cmd, "@AIMMaterialList", SqlDbType.VarChar, aimMaterialList);
					_database.AddInParameter(cmd, "@AIMMaterialList2", SqlDbType.VarChar, aimMaterialList2);
					_database.AddInParameter(cmd, "@AIMMaterialList3", SqlDbType.VarChar, aimMaterialList3);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/18/21 - Version 3 of report
		public List<AnalysisReportsInfo> GetAnalysisPivotSupplierLocation(AnalysisReportsInfo analysisSummary, Boolean dryMatterBase, String aimMaterialList, String aimMaterialList2, String aimMaterialList3)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisPivot_GroupByMaterialSupplierLocation"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);
					_database.AddInParameter(cmd, "@DryMatterBase", SqlDbType.Bit, dryMatterBase);
					_database.AddInParameter(cmd, "@AIMMaterialList", SqlDbType.VarChar, aimMaterialList);
					_database.AddInParameter(cmd, "@AIMMaterialList2", SqlDbType.VarChar, aimMaterialList2);
					_database.AddInParameter(cmd, "@AIMMaterialList3", SqlDbType.VarChar, aimMaterialList3);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalysisReportsInfo> GetAnalysisSourceComparison(AnalysisReportsInfo analysisSummary)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisSourceComparison"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		public List<AnalysisReportsInfo> GetAnalysisSourceComparison(Int32 queryTemplateID)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisSourceComparison"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//5/20/22
		public List<SampleResultsComparisonReportInfo> GetSampleResultsComparison(Int32 queryTemplateID, Int32 datasourcetype1, Int32 datasourcetype2, Boolean useAnalysisDate)
		{
			List<SampleResultsComparisonReportInfo> result = new List<SampleResultsComparisonReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SampleResultsComparison"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@DataSourceTypeID1", SqlDbType.Int, datasourcetype1);
					_database.AddInParameter(cmd, "@DataSourceTypeID2", SqlDbType.Int, datasourcetype2);
					_database.AddInParameter(cmd, "@UseAnalysisDate", SqlDbType.Bit, useAnalysisDate);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultsComparisonReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/22/22		
		public List<DataSourceComparisonInfo> GetDataSourceComparison(Int32 queryTemplateID, Int32 datasourcetype1, Int32 datasourcetype2, Boolean useAnalysisDate
			, Int32 DS1_Nut1, Int32 DS1_Nut2, Int32 DS1_Nut3
			, Int32 DS2_Nut1, Int32 DS2_Nut2, Int32 DS2_Nut3
			, Int32 driverAIMNutrientID)
		{
			List<DataSourceComparisonInfo> result = new List<DataSourceComparisonInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_DataSourceComparison"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@DataSourceTypeID1", SqlDbType.Int, datasourcetype1);
					_database.AddInParameter(cmd, "@DataSourceTypeID2", SqlDbType.Int, datasourcetype2);
					_database.AddInParameter(cmd, "@UseAnalysisDate", SqlDbType.Bit, useAnalysisDate);
					_database.AddInParameter(cmd, "@Datasource1AIMNutrientID1", SqlDbType.Int, DS1_Nut1);
					_database.AddInParameter(cmd, "@Datasource1AIMNutrientID2", SqlDbType.Int, DS1_Nut2);
					_database.AddInParameter(cmd, "@Datasource1AIMNutrientID3", SqlDbType.Int, DS1_Nut3);
					_database.AddInParameter(cmd, "@Datasource2AIMNutrientID1", SqlDbType.Int, DS2_Nut1);
					_database.AddInParameter(cmd, "@Datasource2AIMNutrientID2", SqlDbType.Int, DS2_Nut2);
					_database.AddInParameter(cmd, "@Datasource2AIMNutrientID3", SqlDbType.Int, DS2_Nut3);

					//10/14/22
					_database.AddInParameter(cmd, "@DriverAIMNutrientID", SqlDbType.Int, driverAIMNutrientID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceComparisonInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AimNutrientRefAnalyzerNutrientOrphanInfo> GetAimAnalyzerNutrientRefReportData()
		{
			List<AimNutrientRefAnalyzerNutrientOrphanInfo> result = new List<AimNutrientRefAnalyzerNutrientOrphanInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AIMNutrientRefAnalyzerNutrient_Orphans"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AimNutrientRefAnalyzerNutrientOrphanInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ChartInfo> GetTrendChartData(ChartInfo chart)
		{
			List<ChartInfo> result = new List<ChartInfo>();
			try
			{
				//2/5/20
				//using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Chart_Trend"))
				//{
				//	_database.AddInParameter(cmd, "@AIMMaterialID ", SqlDbType.Int, chart.AIMMaterialID);
				//	_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, chart.AIMNutrientID);
				//	_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, chart.AIMOwnerID);
				//	_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, chart.AIMOwnerLocationID);
				//	_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, chart.AIMSupplierID);
				//	_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, chart.AIMSupplierLocationID);
				//	_database.AddInParameter(cmd, "@TicketNumber", SqlDbType.NVarChar, chart.TicketNumber);
				//	_database.AddInParameter(cmd, "@SampleCodeFrom", SqlDbType.NVarChar, chart.SampleCodeFrom);
				//	_database.AddInParameter(cmd, "@SampleCodeTo", SqlDbType.NVarChar, chart.SampleCodeTo);
				//	_database.AddInParameter(cmd, "@StartDate", SqlDbType.Date, chart.StartDate);
				//	_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, chart.EndDate);
				//	using (IDataReader reader = _database.ExecuteReader(cmd))
				//	{
				//		ObjectFiller<ChartInfo>.GetGenericCollection(ref result, reader);
				//	}
				//}
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Chart_Trend"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID ", SqlDbType.Int, chart.AIMMaterialID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, chart.AIMNutrientID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, chart.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, chart.AIMSupplierLocationID);

					_database.AddInParameter(cmd, "@AIMSourceID2", SqlDbType.Int, chart.AIMSupplierID2);
					_database.AddInParameter(cmd, "@AIMSourceLocationID2", SqlDbType.Int, chart.AIMSupplierLocationID2);

					_database.AddInParameter(cmd, "@AIMSourceID3", SqlDbType.Int, chart.AIMSupplierID3);
					_database.AddInParameter(cmd, "@AIMSourceLocationID3", SqlDbType.Int, chart.AIMSupplierLocationID3);

					_database.AddInParameter(cmd, "@AIMSourceID4", SqlDbType.Int, chart.AIMSupplierID4);
					_database.AddInParameter(cmd, "@AIMSourceLocationID4", SqlDbType.Int, chart.AIMSupplierLocationID4);

					_database.AddInParameter(cmd, "@SampleCodeFrom", SqlDbType.NVarChar, chart.SampleCodeFrom);
					_database.AddInParameter(cmd, "@SampleCodeTo", SqlDbType.NVarChar, chart.SampleCodeTo);
					_database.AddInParameter(cmd, "@StartDate", SqlDbType.Date, chart.StartDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, chart.EndDate);

					//6/28/21
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, chart.AIMOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, chart.AIMOwnerLocationID);

					//7/9/21
					_database.AddInParameter(cmd, "@AIMOwnerID2", SqlDbType.Int, chart.AIMOwnerID2);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID2", SqlDbType.Int, chart.AIMOwnerLocationID2);
					_database.AddInParameter(cmd, "@AIMOwnerID3", SqlDbType.Int, chart.AIMOwnerID3);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID3", SqlDbType.Int, chart.AIMOwnerLocationID3);
					_database.AddInParameter(cmd, "@AIMOwnerID4", SqlDbType.Int, chart.AIMOwnerID4);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID4", SqlDbType.Int, chart.AIMOwnerLocationID4);

					//9/8/21
					_database.AddInParameter(cmd, "@SupplierOwnerOption", SqlDbType.Int, chart.SupplierOwnerOption);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ChartInfo>.GetGenericCollection(ref result, reader);
					}
				}


			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<ChartInfo> GetDataSourceComparisonChartData(ChartInfo chart)
		{
			List<ChartInfo> result = new List<ChartInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Chart_DataSourceCompare"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID ", SqlDbType.Int, chart.AIMMaterialID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, chart.AIMNutrientID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, chart.DataSourceTypeID);
					_database.AddInParameter(cmd, "@DataSourceTypeID2", SqlDbType.Int, chart.DataSourceTypeID2);
					_database.AddInParameter(cmd, "@DataSourceTypeID3", SqlDbType.Int, chart.DataSourceTypeID3);
					_database.AddInParameter(cmd, "@DataSourceTypeID4", SqlDbType.Int, chart.DataSourceTypeID4);
					_database.AddInParameter(cmd, "@SampleCodeFrom", SqlDbType.NVarChar, chart.SampleCodeFrom);
					_database.AddInParameter(cmd, "@SampleCodeTo", SqlDbType.NVarChar, chart.SampleCodeTo);
					_database.AddInParameter(cmd, "@StartDate", SqlDbType.Date, chart.StartDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, chart.EndDate);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ChartInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<ChartInfo> GetAdvancedAnalysisChartData(ChartInfo chart)
		{
			List<ChartInfo> result = new List<ChartInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Chart_AdvancedAnalysis"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID ", SqlDbType.Int, chart.AIMMaterialID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, chart.AIMNutrientID);
					_database.AddInParameter(cmd, "@AIMNutrientID2", SqlDbType.Int, chart.AIMNutrientID2);
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, chart.AIMOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, chart.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, chart.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, chart.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@TicketNumber", SqlDbType.NVarChar, chart.TicketNumber);
					_database.AddInParameter(cmd, "@SampleCodeFrom", SqlDbType.NVarChar, chart.SampleCodeFrom);
					_database.AddInParameter(cmd, "@SampleCodeTo", SqlDbType.NVarChar, chart.SampleCodeTo);
					_database.AddInParameter(cmd, "@StartDate", SqlDbType.Date, chart.StartDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, chart.EndDate);

					//6/28/21					
					_database.AddInParameter(cmd, "@AIMSourceID2", SqlDbType.Int, chart.AIMSupplierID2);
					_database.AddInParameter(cmd, "@AIMSourceLocationID2", SqlDbType.Int, chart.AIMSupplierLocationID2);
					_database.AddInParameter(cmd, "@AIMSourceID3", SqlDbType.Int, chart.AIMSupplierID3);
					_database.AddInParameter(cmd, "@AIMSourceLocationID3", SqlDbType.Int, chart.AIMSupplierLocationID3);
					_database.AddInParameter(cmd, "@AIMSourceID4", SqlDbType.Int, chart.AIMSupplierID4);
					_database.AddInParameter(cmd, "@AIMSourceLocationID4", SqlDbType.Int, chart.AIMSupplierLocationID4);

					//7/12/21
					_database.AddInParameter(cmd, "@AIMOwnerID2", SqlDbType.Int, chart.AIMOwnerID2);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID2", SqlDbType.Int, chart.AIMOwnerLocationID2);
					_database.AddInParameter(cmd, "@AIMOwnerID3", SqlDbType.Int, chart.AIMOwnerID3);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID3", SqlDbType.Int, chart.AIMOwnerLocationID3);
					_database.AddInParameter(cmd, "@AIMOwnerID4", SqlDbType.Int, chart.AIMOwnerID4);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID4", SqlDbType.Int, chart.AIMOwnerLocationID4);

					//9/8/21
					_database.AddInParameter(cmd, "@SupplierOwnerOption", SqlDbType.Int, chart.SupplierOwnerOption);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ChartInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ChartInfo> GetSupplierCompareChartData(ChartInfo chart)
		{
			List<ChartInfo> result = new List<ChartInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Chart_SupplierCompare"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID ", SqlDbType.Int, chart.AIMMaterialID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, chart.AIMNutrientID);
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, chart.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSupplierID2", SqlDbType.Int, chart.AIMSupplierID2);
					_database.AddInParameter(cmd, "@AIMSupplierID3", SqlDbType.Int, chart.AIMSupplierID3);      // added 5/21/20
					_database.AddInParameter(cmd, "@AIMSupplierID4", SqlDbType.Int, chart.AIMSupplierID4);      // added 5/21/20
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, chart.AIMOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, chart.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, chart.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@TicketNumber", SqlDbType.NVarChar, chart.TicketNumber);
					_database.AddInParameter(cmd, "@SampleCodeFrom", SqlDbType.NVarChar, chart.SampleCodeFrom);
					_database.AddInParameter(cmd, "@SampleCodeTo", SqlDbType.NVarChar, chart.SampleCodeTo);
					_database.AddInParameter(cmd, "@StartDate", SqlDbType.Date, chart.StartDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, chart.EndDate);

					//6/28/21 - added supplier-locations					
					_database.AddInParameter(cmd, "@AIMSupplierLocationID2", SqlDbType.Int, chart.AIMSupplierLocationID2);
					_database.AddInParameter(cmd, "@AIMSupplierLocationID3", SqlDbType.Int, chart.AIMSupplierLocationID3);
					_database.AddInParameter(cmd, "@AIMSupplierLocationID4", SqlDbType.Int, chart.AIMSupplierLocationID4);



					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ChartInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/19/23 - commented, not used currently. *** In the future, if it is used, the stp needs to be changed to match logic in GetSourceLocationGuaranteeReportData
		//public List<AimSourceLocationGuaranteeReportInfo> GetSourceLocationGuaranteeSpecificNutrientsReportData(AimSourceLocationGuaranteeReportInfo guarantee)
		//{
		//	List<AimSourceLocationGuaranteeReportInfo> result = new List<AimSourceLocationGuaranteeReportInfo>();
		//	try
		//	{
		//		using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AIMSourceLocationGuaranteesSpecificNutrients"))
		//		{
		//			_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, guarantee.ToDate);
		//			_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, guarantee.FromDate);

		//			if (guarantee.AIMMaterialID != 0)
		//			{
		//				_database.AddInParameter(cmd, "@AimMaterialID", SqlDbType.Int, guarantee.AIMMaterialID);
		//			}

		//			if (guarantee.AimSupplierID != 0)
		//			{
		//				_database.AddInParameter(cmd, "@Source", SqlDbType.Int, guarantee.AimSupplierID);
		//			}
		//			if (guarantee.AimSupplierLocationID != 0)
		//			{
		//				_database.AddInParameter(cmd, "@SourceLocation", SqlDbType.Int, guarantee.AimSupplierLocationID);
		//			}
		//			if (guarantee.Flag == true)
		//			{
		//				_database.AddInParameter(cmd, "@Flag", SqlDbType.Int, guarantee.Flag);
		//			}
		//			_database.AddInParameter(cmd, "@AIMNutrientID1", SqlDbType.Int, guarantee.AIMNutrientID1);
		//			_database.AddInParameter(cmd, "@AIMNutrientID2", SqlDbType.Int, guarantee.AIMNutrientID2);

		//			using (IDataReader reader = _database.ExecuteReader(cmd))
		//			{
		//				ObjectFiller<AimSourceLocationGuaranteeReportInfo>.GetGenericCollection(ref result, reader);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//	}
		//	return result;
		//}


		public List<AimSourceLocationGuaranteeReportInfo> GetSourceLocationGuaranteeReportData(Int32 batchID, String sampleCode, Int32 status, Int32 flag)
		{
			List<AimSourceLocationGuaranteeReportInfo> result = new List<AimSourceLocationGuaranteeReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AIMSourceLocationGuarantees_BatchID_SampleCode"))
				{
					_database.AddInParameter(cmd, "@BatchID", SqlDbType.Int, batchID);
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.NVarChar, sampleCode);
					_database.AddInParameter(cmd, "@Status", SqlDbType.Int, status);
					_database.AddInParameter(cmd, "@Flag", SqlDbType.Int, flag);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AimSourceLocationGuaranteeReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AimSourceLocationGuaranteeReportInfo> GetSourceLocationGuaranteeReportData(AimSourceLocationGuaranteeReportInfo guarantee)
		{
			List<AimSourceLocationGuaranteeReportInfo> result = new List<AimSourceLocationGuaranteeReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AIMSourceLocationGuarantees"))
				{
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, guarantee.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, guarantee.FromDate);

					if (guarantee.AIMMaterialID != 0)
					{
						_database.AddInParameter(cmd, "@AimMaterialID", SqlDbType.Int, guarantee.AIMMaterialID);
					}

					if (guarantee.AimSupplierID != 0)
					{
						_database.AddInParameter(cmd, "@Source", SqlDbType.Int, guarantee.AimSupplierID);
					}
					if (guarantee.AimSupplierLocationID != 0)
					{
						_database.AddInParameter(cmd, "@SourceLocation", SqlDbType.Int, guarantee.AimSupplierLocationID);
					}

					_database.AddInParameter(cmd, "@Flag", SqlDbType.Int, guarantee.Flag);

					//6/26/23
					_database.AddInParameter(cmd, "@AdjustedOrResult", SqlDbType.NVarChar, guarantee.AdjustedOrResult);


					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AimSourceLocationGuaranteeReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/19/23 - commented, not used
		////4/20/18
		//public List<AimSourceLocationGuaranteeReportInfo> GetSourceLocationGuaranteeReportData_Product(AimSourceLocationGuaranteeReportInfo guarantee)
		//{
		//    List<AimSourceLocationGuaranteeReportInfo> result = new List<AimSourceLocationGuaranteeReportInfo>();
		//    try
		//    {
		//        using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AIMSourceLocationGuarantees_Product"))
		//        {
		//            _database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, guarantee.ToDate);
		//            _database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, guarantee.FromDate);

		//            if (guarantee.AIMMaterialID != 0)
		//            {
		//                _database.AddInParameter(cmd, "@AimMaterialID", SqlDbType.Int, guarantee.AIMMaterialID);
		//            }

		//            if (guarantee.AimSupplierID != 0)
		//            {
		//                _database.AddInParameter(cmd, "@Source", SqlDbType.Int, guarantee.AimSupplierID);
		//            }
		//            if (guarantee.AimSupplierLocationID != 0)
		//            {
		//                _database.AddInParameter(cmd, "@SourceLocation", SqlDbType.Int, guarantee.AimSupplierLocationID);
		//            }
		//            if (guarantee.Flag == true)
		//            {
		//                _database.AddInParameter(cmd, "@Flag", SqlDbType.Int, guarantee.Flag);
		//            }
		//            using (IDataReader reader = _database.ExecuteReader(cmd))
		//            {
		//                ObjectFiller<AimSourceLocationGuaranteeReportInfo>.GetGenericCollection(ref result, reader);
		//            }
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//    }
		//    return result;
		//}

		public List<ManufacturedProductComplianceReportInfo> GetManufacturedProductComplianceReportData(Int32 dataSourceTypeID, DateTime toDate, DateTime fromDate)
		{
			List<ManufacturedProductComplianceReportInfo> result = new List<ManufacturedProductComplianceReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_ProductCompliance"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, toDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, fromDate);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ManufacturedProductComplianceReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ManufacturedProductComplianceReportInfo> GetManufacturedProductComplianceViolations(Int32 dataSourceTypeID, DateTime toDate, DateTime fromDate)
		{
			List<ManufacturedProductComplianceReportInfo> result = new List<ManufacturedProductComplianceReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_ProductCompliance_Violations"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, toDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, fromDate);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ManufacturedProductComplianceReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AimMaterialRefAnalyzerMaterialOrphanInfo> GetAimAnalyzerMaterialRefReportData()
		{
			List<AimMaterialRefAnalyzerMaterialOrphanInfo> result = new List<AimMaterialRefAnalyzerMaterialOrphanInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AIMMaterialRefAnalyzerMaterial_Orphans"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AimMaterialRefAnalyzerMaterialOrphanInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/15/22
		//AnalyzerIngredient = 1,
		//AnalyzerProduct = 2,
		//FormulationIngredient = 3,
		//FormulationProduct = 4

		public List<AimMaterialRefAnalyzerMaterialOrphanInfo> GetMaterialOrphanReportData(Int32 reportNumber)
		{
			List<AimMaterialRefAnalyzerMaterialOrphanInfo> result = new List<AimMaterialRefAnalyzerMaterialOrphanInfo>();
			try
			{
				String stp = "";
				if (reportNumber == (Int32)EnumOrphanXRefReport.AnalyzerIngredient)
				{
					stp = "dbo.usp_Report_AIMMaterialRefAnalyzerMaterial_Orphans";
				}
				else if (reportNumber == (Int32)EnumOrphanXRefReport.AnalyzerProduct)
				{
					stp = "dbo.usp_Report_AIMMaterialRefAnalyzerMaterial_Orphans_Products";
				}
				else if (reportNumber == (Int32)EnumOrphanXRefReport.FormulationIngredient)
				{
					stp = "dbo.usp_Report_CFCIngredientRefAIMMaterial_Orphans";
				}
				else if (reportNumber == (Int32)EnumOrphanXRefReport.FormulationProduct)
				{
					stp = "dbo.usp_Report_CFCProductRefAIMMaterial_Orphans";
				}

				using (DbCommand cmd = _database.GetStoredProcCommand(stp))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AimMaterialRefAnalyzerMaterialOrphanInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<SampleComparisonReportInfo> GetSampleComparison(SampleComparisonReportInfo compare)
		{
			List<SampleComparisonReportInfo> result = new List<SampleComparisonReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SampleComparison"))
				{
					_database.AddInParameter(cmd, "@DataSource1", SqlDbType.Int, compare.DataSource1);
					_database.AddInParameter(cmd, "@DataSource2", SqlDbType.Int, compare.DataSource2);
					//_database.AddInParameter(cmd, "@Type1", SqlDbType.Int, compare.Type1);                        //3/12/20 - commented
					//_database.AddInParameter(cmd, "@Type2", SqlDbType.Int, compare.Type2);                        //3/12/20 - commented
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, compare.AimMaterialID);
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, compare.SampleOwnerID);                  //3/16/20 - added
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, compare.AIMOwnerLocationID);        //3/16/20 - added
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, compare.AIMSourceID);                    //3/16/20 - added    
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, compare.AIMSourceLocationID);    //3/16/20 - added
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, compare.ToDate);                               //3/12/20 - added
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, compare.FromDate);                           //3/12/20 - added
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.NVarChar, compare.TicketNumber);                 //3/16/20 - added

					_database.AddInParameter(cmd, "@BothMustHaveValues", SqlDbType.Bit, compare.BothMustHaveValues);        //6/5/30 - added

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleComparisonReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/16/21
		//public List<SampleCustomReportInfo> GetSampleCustom(DateTime fromDate, DateTime toDate, Int32 materialTypeID, Int32 aimOwnerLocationID)
		public List<SampleCustomReportInfo> GetSampleCustom(DateTime fromDate, DateTime toDate, Int32 materialTypeID, Int32 aimOwnerLocationID, Boolean groupMaterials)
		{
			List<SampleCustomReportInfo> result = new List<SampleCustomReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SampleCustom"))
				{
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, fromDate);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, toDate);
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, materialTypeID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);

					//2/16/21
					_database.AddInParameter(cmd, "@GroupMaterials", SqlDbType.Bit, groupMaterials);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleCustomReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/16/21
		//public List<SampleCustomReportInfo> GetSampleCustomProduct(DateTime fromDate, DateTime toDate, Int32 materialTypeID, Int32 aimOwnerLocationID)
		public List<SampleCustomReportInfo> GetSampleCustomProduct(DateTime fromDate, DateTime toDate, Int32 materialTypeID, Int32 aimOwnerLocationID, Boolean groupMaterials)
		{
			List<SampleCustomReportInfo> result = new List<SampleCustomReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SampleCustom_Product"))
				{
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, fromDate);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, toDate);
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, materialTypeID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);

					//2/16/21
					_database.AddInParameter(cmd, "@GroupMaterials", SqlDbType.Bit, groupMaterials);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleCustomReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//TASK951 6/3/24
		public List<ExportBatchLogInfo> GetExportBatchLog(DateTime fromDate, DateTime toDate)
		{
			List<ExportBatchLogInfo> result = new List<ExportBatchLogInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_ExportLogDetail"))
				{
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, fromDate);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, toDate);
				
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExportBatchLogInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<SampleActivityReportInfo> GetSampleActivity(SampleActivityReportInfo activity)
		{
			List<SampleActivityReportInfo> result = new List<SampleActivityReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SampleActivity"))
				{
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.VarChar, activity.FromDateString);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.VarChar, activity.ToDateString);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, activity.CFCUserID);

					//4/26/22
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, activity.MaterialTypeID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleActivityReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/26/22
		public List<SampleActivityReportInfo> GetSampleActivityQualityControl(SampleActivityReportInfo activity)
		{
			List<SampleActivityReportInfo> result = new List<SampleActivityReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SampleActivity_QualityControl"))
				{
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.VarChar, activity.FromDateString);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.VarChar, activity.ToDateString);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, activity.CFCUserID);
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, activity.MaterialTypeID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleActivityReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/25/18
		public List<ReportAlternateNamesInfo> GetReportAlternateNames()
		{
			List<ReportAlternateNamesInfo> result = new List<ReportAlternateNamesInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AlternateNames"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ReportAlternateNamesInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/6/20
		public List<ReportAlternateNamesInfo> GetReportAlternateNamesForImport()
		{
			List<ReportAlternateNamesInfo> result = new List<ReportAlternateNamesInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AlternateName_For_Import"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ReportAlternateNamesInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/15/23
		public List<ReportAlternateNamesInfo> GetReportAlternateNamesForUserDefinedSamplePivotReport()
		{
			List<ReportAlternateNamesInfo> result = new List<ReportAlternateNamesInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AlternateNames_UserDefinedSamplePivot"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ReportAlternateNamesInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//2/12/19
		public List<SampleResultsBySupplierInfo> GetSampleResultsBySupplier(SampleResultsBySupplierInfo results)
		{
			List<SampleResultsBySupplierInfo> result = new List<SampleResultsBySupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SampleResultsBySupplier"))
				{
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.VarChar, results.FromDate);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.VarChar, results.ToDate);
					_database.AddInParameter(cmd, "@OwnerID", SqlDbType.Int, results.AIMOwnerID);

					//6/27/22
					//_database.AddInParameter(cmd, "@OwnerLocationID", SqlDbType.Int, results.AIMOwnerID);
					_database.AddInParameter(cmd, "@OwnerLocationID", SqlDbType.Int, results.AIMOwnerLocationID);

					_database.AddInParameter(cmd, "@SupplierID", SqlDbType.Int, results.AIMSourceID);
					_database.AddInParameter(cmd, "@SupplierLocationID", SqlDbType.Int, results.AIMSourceLocationID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, results.CFCUserID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultsBySupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/27/22
		public List<SampleHeaderInfo> GetSamplesByOwnerLoc(SampleHeaderInfo results)
		{
			List<SampleHeaderInfo> result = new List<SampleHeaderInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SamplesByOwnerLocation"))
				{
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.VarChar, results.FromDate);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.VarChar, results.ToDate);
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, results.AIMOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, results.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, results.AIMSourceID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, results.AIMSourceLocationID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, results.AIMMaterialID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, results.CFCUserID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleHeaderInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ExportBatchMaintenanceReportInfo> GetExportBatchDetailReportData(Int32 updateIngredientHeaderID, Int32 targetID)
		{
			List<ExportBatchMaintenanceReportInfo> result = new List<ExportBatchMaintenanceReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_ExportBatchDetail_UpdateIngredientHeaderID"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientHeaderID", SqlDbType.Int, updateIngredientHeaderID);
					_database.AddInParameter(cmd, "@TargetID", SqlDbType.Int, targetID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExportBatchMaintenanceReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ExportBatchMaintenanceReportInfo> GetExportBatchDetailReportData()
		{
			List<ExportBatchMaintenanceReportInfo> result = new List<ExportBatchMaintenanceReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_ExportBatchDetail"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExportBatchMaintenanceReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalysisReportsInfo> GetAnalysisSummary(Int32 queryTemplateID, Int32 aimDatabaseID)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisSummary"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDatabaseID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/20/18
		public List<AnalysisReportsInfo> GetAnalysisSummary_Product(Int32 queryTemplateID, Int32 aimDatabaseID)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisSummary_Product"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDatabaseID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalysisReportsInfo> GetAnalysisSummaryForAnalysisDateComparison(AnalysisReportsInfo analysisSummary)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisDateComparison"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);


					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/20/18
		public List<AnalysisReportsInfo> GetAnalysisSummaryForAnalysisDateComparison_Product(AnalysisReportsInfo analysisSummary)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisDateComparison_Product"))
				{
					_database.AddInParameter(cmd, "@SampleOwnerID", SqlDbType.Int, analysisSummary.SampleOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, analysisSummary.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, analysisSummary.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, analysisSummary.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.Date, analysisSummary.ToDate);
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.Date, analysisSummary.FromDate);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, analysisSummary.AIMMaterialID);
					_database.AddInParameter(cmd, "@Ticket", SqlDbType.NVarChar, analysisSummary.TicketNumber);


					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalysisReportsInfo> GetAnalysisSummaryForAnalysisDateComparison(Int32 queryTemplateID)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisDateComparison"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/20/18
		public List<AnalysisReportsInfo> GetAnalysisSummaryForAnalysisDateComparison_Product(Int32 queryTemplateID)
		{
			List<AnalysisReportsInfo> result = new List<AnalysisReportsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_AnalysisDateComparison_Product"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisReportsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleResultInfo> GetSampleResultForSQLResultID(Int32 sampleID, String sqlResultID)
		{
			List<SampleResultInfo> result = new List<SampleResultInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_SampleID_SQLResultID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@SQLResultID", SqlDbType.NVarChar, sqlResultID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<SampleResultInfo> GetAllSampleResult(Int32 sampleID)
		{
			List<SampleResultInfo> result = new List<SampleResultInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_SampleID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/9/18 
		public List<SampleQualityInfo> GetAllSampleResultQualityControl(Int32 sampleID)
		{
			List<SampleQualityInfo> result = new List<SampleQualityInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQuality_SEL_SampleID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleResultInfo> GetAllSampleResult(Int32 sampleID, Boolean isActive, Int32 sampleResultMessageType)
		{
			List<SampleResultInfo> result = new List<SampleResultInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_SampleID_IsActive"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@SampleResultMessageType", SqlDbType.Int, sampleResultMessageType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleResultInfo> GetAllSampleResult(Boolean isActive)
		{
			List<SampleResultInfo> result = new List<SampleResultInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/9/18
		public List<SampleQualityInfo> GetAllSampleResultQualityControl(Int32 sampleID, Boolean isActive)
		{
			List<SampleQualityInfo> result = new List<SampleQualityInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQuality_SEL_SampleID_IsActive"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/4/18        
		public List<SampleQualityControlMassEntryDetailInfo> GetSampleQualityControlMassEntryDetail(Int32 ID)
		{
			List<SampleQualityControlMassEntryDetailInfo> result = new List<SampleQualityControlMassEntryDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControlMassEntryDetail_SEL_SampleQualityControlMassEntryID"))
				{
					_database.AddInParameter(cmd, "@SampleQualityControlMassEntryID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityControlMassEntryDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleResultTemplateDetailInfo> GetSampleResultTemplateDetail(Int32 templateID)
		{
			List<SampleResultTemplateDetailInfo> result = new List<SampleResultTemplateDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultTemplateDetail_SEL_SampleResultTemplateID"))
				{
					_database.AddInParameter(cmd, "@SampleResultTemplateID", SqlDbType.Int, templateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultTemplateDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}





		//4/19/22
		public ErrorInfo GetQualityControlMassEntryDetailSortOrder(Int32 sampleQualityControlMassEntryID, Int32 sortOrder)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControlMassEntryDetail_SEL_SampleQualityControlMassEntryID_SortOrder"))
				{
					_database.AddInParameter(cmd, "@SampleQualityControlMassEntryID", SqlDbType.Int, sampleQualityControlMassEntryID);
					_database.AddInParameter(cmd, "@SortOrder", SqlDbType.Int, sortOrder);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//1/4/18
		public ErrorInfo GetSampleQualityControlDetailMaxSortNo(Int32 massEntryID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControlMassEntryDetail_SEL_MaxSortOrder"))
				{
					_database.AddInParameter(cmd, "@SampleQualityControlMassEntryID", SqlDbType.Int, massEntryID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public ErrorInfo GetSampleResultTemplateDetailMaxSortNo(Int32 templateID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultTemplateDetail_SEL_MaxSortOrder"))
				{
					_database.AddInParameter(cmd, "@SampleResultTemplateID", SqlDbType.Int, templateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public ErrorInfo RunSqlScript(String str)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_RunSqlScript"))
				{
					_database.AddInParameter(cmd, "@SqlString", SqlDbType.NVarChar, str);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/15/22		          
		public ErrorInfo RomerImportInsertTemplateRecords(Int32 defID, Boolean isComposite)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_INS_RomerLabsDef"))
				{
					cmd.CommandTimeout = 0;
					_database.AddInParameter(cmd, "@DefID", SqlDbType.Int, defID);
					_database.AddInParameter(cmd, "@IsComposite", SqlDbType.Bit, isComposite);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//5/9/18          
		public ErrorInfo ReCalculateEquations(Int32 progressBarID, Int32 cfcUserID, Int32 aimDatabaseID, String rebuildDate)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_INS_UnitiyForSampleQualityEquation"))
				{
					cmd.CommandTimeout = 0;
					_database.AddInParameter(cmd, "@ProgressBarID", SqlDbType.Int, progressBarID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.AddInParameter(cmd, "@AIMDatabaseID", SqlDbType.Int, aimDatabaseID);

					//11/4/22
					_database.AddInParameter(cmd, "@RebuildDate", SqlDbType.VarChar, rebuildDate);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/27/19 
		//public ErrorInfo RePopulateSampleAIMTable(Int32 progressBarID, Int32 cfcUserID)
		public ErrorInfo RePopulateSampleAIMTable(Int32 progressBarID, Int32 cfcUserID, String rebuildDate)

		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleAIM_INS_Utility"))

				{
					cmd.CommandTimeout = 0;
					_database.AddInParameter(cmd, "@ProgressBarID", SqlDbType.Int, progressBarID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);

					//6/27/19
					_database.AddInParameter(cmd, "@RebuildDate", SqlDbType.VarChar, rebuildDate);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleQualityControlInfo> GetSampleQualityControl(Int32 sampleID)
		{
			List<SampleQualityControlInfo> result = new List<SampleQualityControlInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControl_SEL_SampleID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityControlInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public SampleQualityControlInfo GetSampleQualityControlSampleIDNutrientID(Int32 sampleID, Int32 aimNutrientID)
		{
			SampleQualityControlInfo result = new SampleQualityControlInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControl_SEL_SampleID_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						//ObjectFiller<SampleQualityControlInfo>.GetGenericCollection(ref result, reader);
						result = ObjectFiller<SampleQualityControlInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleResultInfo> GetAllSampleResultForImport(Int32 sampleID)
		{
			List<SampleResultInfo> result = new List<SampleResultInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultForImport_SEL_SampleID_test"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleResultInfo> GetAllSampleResultSampleIDDataSourceTypeID(Int32 sampleID, Int32 dataSourceTypeID, Int32 sampleResultMessageType)
		{
			List<SampleResultInfo> result = new List<SampleResultInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_SampleID_DataSourceTypeID_AllStatus"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@SampleResultMessageType", SqlDbType.Int, sampleResultMessageType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<SampleResultInfo> GetAllSampleResultSampleIDDataSourceTypeID(Int32 sampleID, Int32 dataSourceTypeID, Boolean isActive, Int32 sampleResultMessageType)
		{
			List<SampleResultInfo> result = new List<SampleResultInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_SampleID_DataSourceTypeID"))
				{

					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@SampleResultMessageType", SqlDbType.Int, sampleResultMessageType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/9/18
		public List<SampleQualityInfo> GetAllSampleResultSampleIDDataSourceTypeID_QualityControl(Int32 sampleID, Int32 dataSourceTypeID, Boolean isActive)
		{
			List<SampleQualityInfo> result = new List<SampleQualityInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQuality_SEL_SampleID_DataSourceTypeID_IsActive"))
				{

					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/9/18
		public List<SampleQualityInfo> GetAllSampleResultSampleIDDataSourceTypeID_QualityControl(Int32 sampleID, Int32 dataSourceTypeID)
		{
			List<SampleQualityInfo> result = new List<SampleQualityInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQuality_SEL_SampleID_DataSourceTypeID_IsActive"))
				{

					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<SampleResultInfo> GetAllSampleResultDataSourceTypeID(Int32 dataSourceTypeID, Boolean isActive)
		{
			List<SampleResultInfo> result = new List<SampleResultInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		

		public List<SampleResultPredictInfo> GetAllSampleWarning(Int32 sampleID, Int32 sampleResultMessageType)
		{
			List<SampleResultPredictInfo> result = new List<SampleResultPredictInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultPredict_SEL_SampleID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@SampleResultMessageType", SqlDbType.Int, sampleResultMessageType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultPredictInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleMessageDetailInfo> GetSampleMessageDetailOutside(Int32 sampleID, Int32 sampleResultPredictID, Int32 sampleResultMessageType)
		{
			List<SampleMessageDetailInfo> result = new List<SampleMessageDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_MessageOutside_SampleID_SampleResultPredictID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@SampleResultPredictID", SqlDbType.Int, sampleResultPredictID);
					_database.AddInParameter(cmd, "@SampleResultMessageType", SqlDbType.Int, sampleResultMessageType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleMessageDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleMessageDetailInfo> GetSampleMessageDetailOutsideAnalyzerNutrientID(Int32 sampleID, Int32 analyzerNutrientID, Int32 sampleResultMessageType)
		{
			List<SampleMessageDetailInfo> result = new List<SampleMessageDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_MessageOutside_SampleID_AnalyzerNutrientID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@AnalyzerNutrientID", SqlDbType.Int, analyzerNutrientID);
					_database.AddInParameter(cmd, "@SampleResultMessageType", SqlDbType.Int, sampleResultMessageType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleMessageDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<SampleMessageDetailInfo> GetSampleMessageDetailInside(Int32 sampleID, Int32 sampleResultPredictID, Int32 sampleResultMessageType)
		{
			List<SampleMessageDetailInfo> result = new List<SampleMessageDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_MessageInside_SampleID_SampleResultPredictID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@SampleResultPredictID", SqlDbType.Int, sampleResultPredictID);
					_database.AddInParameter(cmd, "@SampleResultMessageType", SqlDbType.Int, sampleResultMessageType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleMessageDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<SampleMessageDetailInfo> GetSampleMessageDetailInsideAnalyzerNutrientID(Int32 sampleID, Int32 analyzerNutrientID, Int32 sampleResultMessageType, Boolean isActive)
		{
			List<SampleMessageDetailInfo> result = new List<SampleMessageDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_MessageInside_SampleID_AnalyzerNutrientID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@AnalyzerNutrientID", SqlDbType.Int, analyzerNutrientID);
					_database.AddInParameter(cmd, "@SampleResultMessageType", SqlDbType.Int, sampleResultMessageType);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleMessageDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMSupplierInfo> GetSupplier(Boolean isActive)
		{
			List<AIMSupplierInfo> result = new List<AIMSupplierInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		//8/30/23
		public List<AIMSupplierInfo> GetSupplier(Boolean isActive, Int32 aimSupplierGroupID)
		{
			List<AIMSupplierInfo> result = new List<AIMSupplierInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplier_SEL_AIMSupplierGroupID_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AIMSupplierGroupID", SqlDbType.Int, aimSupplierGroupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMSupplierInfo> GetSupplierWithMaterials(Boolean isActive, Int32 groupID, Int32 aimMaterialID)
		{
			List<AIMSupplierInfo> result = new List<AIMSupplierInfo>();
			try
			{
				//10/17/23
				//using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_SEL_IsActive_AimMaterialID"))
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplier_SEL_IsActive_AimSupplierGroupID_AimMaterialID"))					
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AIMSupplierGroupID", SqlDbType.Int, groupID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/5/20
		public List<AIMSupplierInfo> GetSupplierWithMaterialCodes(Boolean isActive, String aimMaterialCode)
		{
			List<AIMSupplierInfo> result = new List<AIMSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_SEL_IsActive_AimMaterialCode"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AimMaterialCode", SqlDbType.VarChar, aimMaterialCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//12/19/18
		public List<AIMSupplierInfo> GetSupplierForUtility(Boolean isActive)
		{
			List<AIMSupplierInfo> result = new List<AIMSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_SEL_IsActive_For_Utility"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/30/23
		public List<AIMSupplierInfo> GetAimSupplier(Int32 aimSourceID)
		{
			List<AIMSupplierInfo> result = new List<AIMSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_SEL_AIMSourceID"))
				{
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Bit, aimSourceID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMSupplierInfo GetOneSupplier(Int32 aimSupplierID)
		{
			AIMSupplierInfo result = new AIMSupplierInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_SEL_AIMSourceID"))
				{
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, aimSupplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMSupplierInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMSupplierInfo GetOneSupplier(String partialName, Int32 num)
		{
			AIMSupplierInfo result = new AIMSupplierInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_SEL_PARTIAL_SourceName"))
				{
					_database.AddInParameter(cmd, "@PartialName", SqlDbType.NVarChar, partialName);
					_database.AddInParameter(cmd, "@Num", SqlDbType.Int, num);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMSupplierInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMSupplierLocationInfo GetSupplierLocationSupplierIDLocationName(Int32 aimSupplierID, String supplierLocationName)
		{
			AIMSupplierLocationInfo result = new AIMSupplierLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_SourceID_SourceName"))
				{
					_database.AddInParameter(cmd, "@SupplierID", SqlDbType.Int, aimSupplierID);
					_database.AddInParameter(cmd, "@SupplierLocationName", SqlDbType.NVarChar, supplierLocationName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMSupplierLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMSupplierLocationInfo GetSupplierLocationSupplierIDLocationCode(Int32 aimSupplierID, String supplierLocationCode)
		{
			AIMSupplierLocationInfo result = new AIMSupplierLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocation_SEL_SupplierID_SupplierLocationCode"))
				{
					_database.AddInParameter(cmd, "@SupplierID", SqlDbType.Int, aimSupplierID);
					_database.AddInParameter(cmd, "@SupplierLocationCode", SqlDbType.NVarChar, supplierLocationCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMSupplierLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMOwnerLocationInfo GetOwnerLocationOwnerIDLocationName(Int32 aimOwnerID, String ownerLocationName)
		{
			AIMOwnerLocationInfo result = new AIMOwnerLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SEL_OwnerID_OwnerLocationName"))
				{
					_database.AddInParameter(cmd, "@OwnerID", SqlDbType.Int, aimOwnerID);
					_database.AddInParameter(cmd, "@OwnerLocationName", SqlDbType.NVarChar, ownerLocationName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMOwnerLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMOwnerLocationInfo GetOwnerLocationOwnerIDLocationCode(Int32 aimOwnerID, String ownerLocationCode)
		{
			AIMOwnerLocationInfo result = new AIMOwnerLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SEL_OwnerID_OwnerLocationCode"))
				{
					_database.AddInParameter(cmd, "@OwnerID", SqlDbType.Int, aimOwnerID);
					_database.AddInParameter(cmd, "@OwnerLocationCode", SqlDbType.NVarChar, ownerLocationCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMOwnerLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public AIMSupplierInfo GetSupplierSupplierName(String supplierName)
		{
			AIMSupplierInfo result = new AIMSupplierInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_SEL_SourceName"))
				{
					_database.AddInParameter(cmd, "@SupplierName", SqlDbType.NVarChar, supplierName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMSupplierInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMSupplierInfo GetSupplierSupplierCode(String supplierCode)
		{
			AIMSupplierInfo result = new AIMSupplierInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_SEL_SourceCode"))
				{
					_database.AddInParameter(cmd, "@SupplierCode", SqlDbType.NVarChar, supplierCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMSupplierInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/18/20
		public AIMMaterialStockStatusInfo GetAIMMaterialStockStatus(Int32 materialID, Int32 ownerLocationID)
		{
			AIMMaterialStockStatusInfo result = new AIMMaterialStockStatusInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialStockStatus_SEL_AIMMaterialID_AIMOwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, materialID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, ownerLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMMaterialStockStatusInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/2/20
		public AIMMaterialStockStatusInfo GetAIMMaterialStockStatus(Int32 materialID)
		{
			AIMMaterialStockStatusInfo result = new AIMMaterialStockStatusInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialStockStatus_SEL_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, materialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMMaterialStockStatusInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMOwnerInfo GetOwnerName(String ownerName)
		{
			AIMOwnerInfo result = new AIMOwnerInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwner_SEL_OwnerName"))
				{
					_database.AddInParameter(cmd, "@OwnerName", SqlDbType.NVarChar, ownerName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMOwnerInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMOwnerInfo GetAIMOwnerCode(String ownerCode)
		{
			AIMOwnerInfo result = new AIMOwnerInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwner_SEL_OwnerCode"))
				{
					_database.AddInParameter(cmd, "@OwnerCode", SqlDbType.NVarChar, ownerCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMOwnerInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMMaterialInfo GetAIMMaterialName(String materialName)
		{
			AIMMaterialInfo result = new AIMMaterialInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_MaterialName"))
				{
					_database.AddInParameter(cmd, "@MaterialName", SqlDbType.NVarChar, materialName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMMaterialInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//public AIMMaterialInfo GetAIMMaterial(Int32 materialID)
		public AIMMaterialInfo GetAIMMaterial(Int32 materialID, Boolean isActive)

		{
			AIMMaterialInfo result = new AIMMaterialInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, materialID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMMaterialInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMSupplierLocationInfo GetOneSupplierLocation(String partialName, Int32 num)
		{
			AIMSupplierLocationInfo result = new AIMSupplierLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_PARTIAL_SourceLocationName"))
				{
					_database.AddInParameter(cmd, "@PartialName", SqlDbType.NVarChar, partialName);
					_database.AddInParameter(cmd, "@Num", SqlDbType.Int, num);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMSupplierLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMOwnerInfo GetOneOwner(String partialName, Int32 num)
		{
			AIMOwnerInfo result = new AIMOwnerInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwner_SEL_PARTIAL_OwnerName"))
				{
					_database.AddInParameter(cmd, "@PartialName", SqlDbType.NVarChar, partialName);
					_database.AddInParameter(cmd, "@Num", SqlDbType.Int, num);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMOwnerInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMOwnerLocationInfo GetOneOwnerLocation(String partialName, Int32 num)
		{
			AIMOwnerLocationInfo result = new AIMOwnerLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SEL_PARTIAL_LocationName"))
				{
					_database.AddInParameter(cmd, "@PartialName", SqlDbType.NVarChar, partialName);
					_database.AddInParameter(cmd, "@Num", SqlDbType.Int, num);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMOwnerLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMSupplierInfo> GetAllSupplier()
		{
			List<AIMSupplierInfo> result = new List<AIMSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMSupplierLocationInfo> GetAllSupplierLocation(Boolean isActive)
		{
			List<AIMSupplierLocationInfo> result = new List<AIMSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/26/19
		public List<AIMSupplierLocationGroupedInfo> GetAllSupplierLocationGrouped(Boolean isActive)
		{
			List<AIMSupplierLocationGroupedInfo> result = new List<AIMSupplierLocationGroupedInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_ALL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGroupedInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public UserDefinedDataSourceTemplateInfo GetMapColumnName(Int32 userDefinedDataSourceDefinitionID, String sourceColumnName)
		{
			UserDefinedDataSourceTemplateInfo result = new UserDefinedDataSourceTemplateInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_SEL_DataSourceDefinitionID_SourceColumnName"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, userDefinedDataSourceDefinitionID);
					_database.AddInParameter(cmd, "@SourceColumnName", SqlDbType.NVarChar, sourceColumnName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<UserDefinedDataSourceTemplateInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMSupplierLocationInfo> GetSupplierLocationForSupplier(Int32 aimSupplierID)
		{
			List<AIMSupplierLocationInfo> result = new List<AIMSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_AIMSourceID"))
				{
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, aimSupplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/30/23		
		public List<AIMSupplierLocationInfo> GetSupplierLocationForSupplier(Int32 aimSupplierID, Int32 aimSupplierGroupID, Int32 aimSupplierLocationGroupID)
		{
			List<AIMSupplierLocationInfo> result = new List<AIMSupplierLocationInfo>();
			try
			{
				//using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_AIMSourceID_AIMSupplierLocationGroupID"))
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_AIMSourceID_AIMSupplierGroupID_AIMSupplierLocationGroupID"))
				{
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, aimSupplierID);
					_database.AddInParameter(cmd, "@AIMSupplierGroupID", SqlDbType.Int, aimSupplierGroupID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationGroupID", SqlDbType.Int, aimSupplierLocationGroupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/30/23
		public List<AIMSupplierLocationInfo> GetSupplierLocationForSupplierWithGroup(Int32 aimSupplierID, Int32 aimSupplierGroupID)
		{
			List<AIMSupplierLocationInfo> result = new List<AIMSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_AIMSourceID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, aimSupplierID);
					_database.AddInParameter(cmd, "@AIMSupplierGroupID", SqlDbType.Int, aimSupplierGroupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//12/19/18
		public List<AIMSupplierLocationInfo> GetSupplierLocationForSupplierForUtility(Int32 aimSupplierID)
		{
			List<AIMSupplierLocationInfo> result = new List<AIMSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_AIMSourceID_For_Utility"))
				{
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, aimSupplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//12/5/19
		public List<AIMOwnerLocationInfo> GetOwnerLocationForOwnerForUtility(Int32 aimOwnerID)
		{
			List<AIMOwnerLocationInfo> result = new List<AIMOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SEL_OwnerID_For_Utility"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, aimOwnerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocation(Int32 datasourceTypeID, Int32 analyzerSupplierID)
		{
			List<AnalyzerSupplierLocationInfo> result = new List<AnalyzerSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SEL_DataSourceTypeID_SupplierID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, analyzerSupplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public AIMSupplierLocationInfo GetOneSupplierLocation(Int32 aimSupplierLocationID)
		{
			AIMSupplierLocationInfo result = new AIMSupplierLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_AIMSourceLocationID"))
				{
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, aimSupplierLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMSupplierLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMDatabaseInfo> GetAIMDatabase(Boolean isActive)
		{
			List<AIMDatabaseInfo> result = new List<AIMDatabaseInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMDatabase_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMDatabaseInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public AIMDatabaseInfo GetAIMDatabase(Int32 aimDatabaseID)
		{
			AIMDatabaseInfo result = new AIMDatabaseInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMDatabase_SEL_AIMDatabaseID"))
				{
					_database.AddInParameter(cmd, "@AIMDatabaseID", SqlDbType.Int, aimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMDatabaseInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public AIMDatabaseInfo GetAIMDatabase(String name)
		{
			AIMDatabaseInfo result = new AIMDatabaseInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMDatabase_SEL_Name"))
				{
					_database.AddInParameter(cmd, "@Name", SqlDbType.VarChar, name);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMDatabaseInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalysisViewTemplateNameInfo> GetTemplateName(String name)
		{
			List<AnalysisViewTemplateNameInfo> result = new List<AnalysisViewTemplateNameInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisViewTemplate_SEL_AnalysisViewTemplateName_IsActive"))
				{
					_database.AddInParameter(cmd, "@AnalysisViewTemplateName", SqlDbType.VarChar, name);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisViewTemplateNameInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/12/18
		public List<AnalysisViewTemplateNameInfo> GetTemplateName(String name, Int32 templateType)
		{
			List<AnalysisViewTemplateNameInfo> result = new List<AnalysisViewTemplateNameInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisViewTemplate_SEL_AnalysisViewTemplateName_IsActive_TemplateType"))
				{
					_database.AddInParameter(cmd, "@AnalysisViewTemplateName", SqlDbType.VarChar, name);
					_database.AddInParameter(cmd, "@TemplateType", SqlDbType.Int, templateType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisViewTemplateNameInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/13/18
		public List<AnalysisViewTemplateNameInfo> GetTemplateName(String name, Int32 gridNumber, Int32 templateType)
		{
			List<AnalysisViewTemplateNameInfo> result = new List<AnalysisViewTemplateNameInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisViewTemplate_SEL_AnalysisViewTemplateName_GridNumber_TemplateType"))
				{
					_database.AddInParameter(cmd, "@AnalysisViewTemplateName", SqlDbType.VarChar, name);
					_database.AddInParameter(cmd, "@GridNumber", SqlDbType.Int, gridNumber);
					_database.AddInParameter(cmd, "@TemplateType", SqlDbType.Int, templateType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisViewTemplateNameInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCPlantRefAIMOwnerLocationInfo> GetAllCFCPlantAIMOwnerLocation()
		{
			List<CFCPlantRefAIMOwnerLocationInfo> result = new List<CFCPlantRefAIMOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlantRefAIMOwnerLocation_SEL_All"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCPlantRefAIMOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//3/19/18
		//public List<CFCProductArchiveInfo> GetCFCProductArchive(Int32 aimMaterialID)
		public List<CFCProductArchiveInfo> GetCFCProductArchive(Int32 aimMaterialID, Int32 aimOwnerLocationID)
		{
			List<CFCProductArchiveInfo> result = new List<CFCProductArchiveInfo>();
			try
			{
				//3/19/18
				//using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductArchive_SEL_AIMMaterial"))
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductArchive_SEL_AIMMaterial_AIMOwnerLocation"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductArchiveInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/5/23
		public List<CFCProductArchiveInfo> GetCFCProductArchive(Int32 aimMaterialID)
		{
			List<CFCProductArchiveInfo> result = new List<CFCProductArchiveInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductArchive_SEL_AIMMaterial"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductArchiveInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		//5/17/18
		public List<CFCProductSpecInfo> GetProductSpec(String plantCode, String productCode, String productionVersion)
		{
			List<CFCProductSpecInfo> result = new List<CFCProductSpecInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProduct_SEL_PlantCodeProductCodeProductionVersion"))
				{
					_database.AddInParameter(cmd, "@PlantCode", SqlDbType.VarChar, plantCode);
					_database.AddInParameter(cmd, "@ProductCode", SqlDbType.VarChar, productCode);
					_database.AddInParameter(cmd, "@ProductionVersion", SqlDbType.VarChar, productionVersion);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductSpecInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCProductInfo> GetAllCFCProduct(Boolean isActive)
		{
			List<CFCProductInfo> result = new List<CFCProductInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProduct_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<CFCProductInfo> GetAllCFCProductAimDatabaseID(Boolean isActive, Int32 aimDatabaseID)
		{
			List<CFCProductInfo> result = new List<CFCProductInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProduct_SEL_IsActive_AimDatabaseID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/13/22
		public List<CFCProductInfo> GetCFCProduct(Int32 plantID, String productCode)
		{
			List<CFCProductInfo> result = new List<CFCProductInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProduct_SEL_PlantID_ProductCode"))
				{
					_database.AddInParameter(cmd, "@PlantID", SqlDbType.Int, plantID);
					_database.AddInParameter(cmd, "@ProductCode", SqlDbType.VarChar, productCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCProductInfo> GetAllCFCProductAimDatabaseIDCFCPlantID(Boolean isActive, Int32 aimDatabaseID, Int32 cfcPlantID)
		{
			List<CFCProductInfo> result = new List<CFCProductInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProduct_SEL_IsActive_AimDatabaseID_CFCPlantID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					_database.AddInParameter(cmd, "@CFCPlantID", SqlDbType.Int, cfcPlantID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCIngredientProductCombinedInfo> GetAllCFCProductInitialLoadAimDatabaseID(Boolean isActive, Int32 aimDatabaseID)
		{
			List<CFCIngredientProductCombinedInfo> result = new List<CFCIngredientProductCombinedInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductInitialLoad_SEL_IsActive_AimDatabaseID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCIngredientProductCombinedInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCIngredientInfo> GetAllCFCIngredient(Boolean isActive)
		{
			List<CFCIngredientInfo> result = new List<CFCIngredientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCIngredient_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCIngredientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCIngredientInfo> GetAllCFCIngredientAimDatabaseID(Boolean isActive, Int32 aimDatabaseID)
		{
			List<CFCIngredientInfo> result = new List<CFCIngredientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCIngredient_SEL_IsActive_AimDatabaseID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCIngredientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public CFCPlantRefAIMOwnerLocationInfo GetOneCFCPlantAIMOwnerLocation(Int32 CFCPlantID)
		{
			CFCPlantRefAIMOwnerLocationInfo result = new CFCPlantRefAIMOwnerLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlantRefAIMOwnerLocation_SEL_CFCPlantID"))
				{
					_database.AddInParameter(cmd, "@CFCPlantID", SqlDbType.Int, CFCPlantID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCPlantRefAIMOwnerLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public CFCPlantRefAIMOwnerLocationInfo GetOneCFCPlantAIMOwnerLocation(Int32 cfcPlantID, Int32 aimOwnerLocationID)
		{
			CFCPlantRefAIMOwnerLocationInfo result = new CFCPlantRefAIMOwnerLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlantRefAIMOwnerLocation_SEL_CFCPlantID_AIMOwnerLocationID"))
				{
					if (cfcPlantID > 0)
					{
						_database.AddInParameter(cmd, "@CFCPlantID", SqlDbType.Int, cfcPlantID);
					}

					if (aimOwnerLocationID > 0)
					{
						_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					}

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCPlantRefAIMOwnerLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<CFCPlantInfo> GetCFCPlant(Boolean isActive)
		{
			List<CFCPlantInfo> result = new List<CFCPlantInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlant_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCPlantInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/5/22 
		public List<CFCPlantInfo> GetCFCPlantAimDatabaseIDWithOrMissingXref(Boolean isActive, Int32 aimDatabaseID, Boolean isXref)
		{
			List<CFCPlantInfo> result = new List<CFCPlantInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlant_CFCPlantRefAIMOwnerLocation_SEL_IsActive_AimDatabaseID_IsXref"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					_database.AddInParameter(cmd, "@IsXref", SqlDbType.Bit, isXref);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCPlantInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCPlantInfo> GetCFCPlantAimDatabaseID(Boolean isActive, Int32 aimDatabaseID)
		{
			List<CFCPlantInfo> result = new List<CFCPlantInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlant_SEL_IsActive_AimDatabaseID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCPlantInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/27/22
		public List<ProductToleranceProductCodesInfo> GetAIMMaterialProductCodes()
		{
			List<ProductToleranceProductCodesInfo> result = new List<ProductToleranceProductCodesInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SEL_ProductCodes"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ProductToleranceProductCodesInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//3/15/21
		public List<CFCProductRefAIMMaterialInfo> GetCFCProductRefAIMMaterial()
		{
			List<CFCProductRefAIMMaterialInfo> result = new List<CFCProductRefAIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductArchive_SEL_CFCProductCFCPlantCFCProductRefAIMMaterialAIMMaterial"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductRefAIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//3/16/21
		public List<CFCProductRefAIMMaterialInfo> GetCFCProductRefAIMMaterial_AIMMaterialID(Int32 aimMaterialID)
		{
			List<CFCProductRefAIMMaterialInfo> result = new List<CFCProductRefAIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductArchive_SEL_CFCProductCFCPlantCFCProductRefAIMMaterialAIMMaterialAIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductRefAIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//3/15/21
		public List<CFCProductArchiveSpecInfo> GetAIMNutrientCFCProductArchiveSpec(Int32 cfcProductArchiveID)
		{
			List<CFCProductArchiveSpecInfo> result = new List<CFCProductArchiveSpecInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrientCFCProductArchiveSpec_SEL_CFCProductArchiveID"))
				{
					_database.AddInParameter(cmd, "@CFCProductArchiveID", SqlDbType.Int, cfcProductArchiveID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductArchiveSpecInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<CFCPlantInfo> GetCFCPlant(Int32 aimDatabaseID)
		{
			List<CFCPlantInfo> result = new List<CFCPlantInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlant_SEL_AIMDatabaseID"))
				{
					_database.AddInParameter(cmd, "@AIMDatabaseID", SqlDbType.Int, aimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCPlantInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public CFCPlantInfo Get_CFCPlant(Int32 aimDatabaseID, Int32 cfcplantID)
		{
			CFCPlantInfo result = new CFCPlantInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlant_SEL_CFCPlantID"))
				{
					_database.AddInParameter(cmd, "@CFCPlantID", SqlDbType.Int, cfcplantID);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCPlantInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public CFCPlantInfo GetCFCPlant(String CFCPlantCode)
		{
			CFCPlantInfo result = new CFCPlantInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlant_SEL_CFCPlantCode"))
				{
					_database.AddInParameter(cmd, "@CFCPlantCode", SqlDbType.VarChar, CFCPlantCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCPlantInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public CFCPlantInfo GetCFCPlant(String CFCPlantCode, Int32 AimDatabaseID)
		{
			CFCPlantInfo result = new CFCPlantInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlant_SEL_CFCPlantCode_AimDatabaseID"))
				{
					_database.AddInParameter(cmd, "@CFCPlantCode", SqlDbType.VarChar, CFCPlantCode);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, AimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCPlantInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierInfo> GetAllAnalyzerSupplier(Boolean isActive)
		{
			List<AnalyzerSupplierInfo> result = new List<AnalyzerSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplier_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierLocationInfo> GetAllAnalyzerSupplierLocation(Boolean isActive)
		{
			List<AnalyzerSupplierLocationInfo> result = new List<AnalyzerSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierInfo> GetAllAnalyzerSupplier()
		{
			List<AnalyzerSupplierInfo> result = new List<AnalyzerSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplier_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierLocationInfo> GetAllAnalyzerSupplierLocation()
		{
			List<AnalyzerSupplierLocationInfo> result = new List<AnalyzerSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerNutrientInfo> GetAllAnalyzerNutrient(Boolean isActive)
		{
			List<AnalyzerNutrientInfo> result = new List<AnalyzerNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerNutrient_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerNutrientInfo> GetAllAnalyzerNutrient()
		{
			List<AnalyzerNutrientInfo> result = new List<AnalyzerNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerNutrient_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerOwnerInfo> GetAllAnalyzerOwner(Boolean isActive)
		{
			List<AnalyzerOwnerInfo> result = new List<AnalyzerOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwner_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerOwnerInfo> GetAllAnalyzerOwner()
		{
			List<AnalyzerOwnerInfo> result = new List<AnalyzerOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwner_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerOwnerLocationInfo> GetAllAnalyzerOwnerLocation(Boolean isActive)
		{
			List<AnalyzerOwnerLocationInfo> result = new List<AnalyzerOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerOwnerLocationInfo> GetAllAnalyzerOwnerLocation()
		{
			List<AnalyzerOwnerLocationInfo> result = new List<AnalyzerOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierInfo> GetAnalyzerSupplier(Int32 DataSourceTypeID, Boolean isActive)
		{
			List<AnalyzerSupplierInfo> result = new List<AnalyzerSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplier_SEL_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierInfo> GetAnalyzerSupplier(Int32 DataSourceTypeID)
		{
			List<AnalyzerSupplierInfo> result = new List<AnalyzerSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplier_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocation(Int32 DataSourceTypeID, Boolean isActive)
		{
			List<AnalyzerSupplierLocationInfo> result = new List<AnalyzerSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SEL_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocation(Int32 DataSourceTypeID)
		{
			List<AnalyzerSupplierLocationInfo> result = new List<AnalyzerSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerNutrientInfo> GetAllAnalyzerNutrient(Int32 DataSourceTypeID, Boolean isActive)
		{
			List<AnalyzerNutrientInfo> result = new List<AnalyzerNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerNutrient_SEL_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerNutrientInfo> GetAnalyzerNutrient(Int32 DataSourceTypeID)
		{
			List<AnalyzerNutrientInfo> result = new List<AnalyzerNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerNutrient_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/4/18 
		public List<SampleQualityParameterInfo> GetSampleQualityParameterDetail(Int32 dataSourceTypeID, Int32 sampleQualityControlMassEntryID, Boolean isActive)
		{
			List<SampleQualityParameterInfo> result = new List<SampleQualityParameterInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityParameter_SEL_SampleQualityControlMassEntryDetail"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@SampleQualityControlMassEntryID", SqlDbType.Int, sampleQualityControlMassEntryID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityParameterInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerNutrientInfo> GetAnalyzerNutrientTemplateDetail(Int32 dataSourceTypeID, Int32 sampleResultTemplateID, Boolean isActive)
		{
			List<AnalyzerNutrientInfo> result = new List<AnalyzerNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerNutrient_SEL_SampleResultTemplateDetail"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@SampleResultTemplateID", SqlDbType.Int, sampleResultTemplateID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		public List<AnalyzerMaterialInfo> GetAllAnalyzerMaterial(Boolean isActive)
		{
			List<AnalyzerMaterialInfo> result = new List<AnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterial_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/17/24   
		public List<AnalyzerMaterialInfo> GetPNEAnalyzerMaterial(Int32 aimMaterialID, Int32 dataSourceTypeID)
		{
			List<AnalyzerMaterialInfo> result = new List<AnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEForAnalyzerMaterial_SEL_AIMMaterialID_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMNutrientInfo> GetAIMNutrient(Boolean isActive)
		{
			List<AIMNutrientInfo> result = new List<AIMNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrient_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//10/13/22
		public List<AIMNutrientDriverInfo> GetAIMNutrientDriver(Int32 queryTemplateID)
		{
			List<AIMNutrientDriverInfo> result = new List<AIMNutrientDriverInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrientDriver_SEL_QueryTemplateID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMNutrientDriverInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ImportConfigInfo> GetImportConfig()
		{
			List<ImportConfigInfo> result = new List<ImportConfigInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportConfig_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ImportConfigInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ImportConfigInfo> GetImportConfig(DataSourceTypeEnum dataSourceType)
		{
			List<ImportConfigInfo> result = new List<ImportConfigInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportConfig_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, (Int32)dataSourceType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ImportConfigInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public ImportConfigInfo GetImportConfig(String name)
		{
			ImportConfigInfo result = new ImportConfigInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportConfig_SEL_Name"))
				{
					_database.AddInParameter(cmd, "@Name", SqlDbType.VarChar, name);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ImportConfigInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/8/18 
		public List<DataSourceTypeInfo> GetDataSourceTypeHavingAnalyzerNutrients(Boolean isActive)
		{
			List<DataSourceTypeInfo> result = new List<DataSourceTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceType_SEL_IsActive_AnalyzerNutrientExists"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/10/18 
		public List<DataSourceTypeInfo> GetDataSourceTypeHavingQualityControlParameters(Boolean isActive)
		{
			List<DataSourceTypeInfo> result = new List<DataSourceTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceType_SEL_IsActive_QualityControlParametersExists"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<DataSourceTypeInfo> GetDataSourceType(Boolean isActive)
		{
			List<DataSourceTypeInfo> result = new List<DataSourceTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceType_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/22/21
		public List<SampleTypeInfo> GetSampleType(Boolean isActive)
		{
			List<SampleTypeInfo> result = new List<SampleTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleType_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//12/21/22
		public List<CFCProductTypeInfo> GetCFCProductType(Int32 aimDatabaseID)
		{
			List<CFCProductTypeInfo> result = new List<CFCProductTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductType_SEL_AIMDatabaseID"))
				{
					_database.AddInParameter(cmd, "@AIMDatabaseID", SqlDbType.Int, aimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<DataSourceTypeInfo> GetDataSourceTypeRefExists(Boolean isActive)
		{
			List<DataSourceTypeInfo> result = new List<DataSourceTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceType_SEL_IsActive_RefExists"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<DataSourceTypeInfo> GetDataSourceTypeByUserDefinedSwitch(Boolean isUserDefined)
		{
			List<DataSourceTypeInfo> result = new List<DataSourceTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceType_SEL_IsUserDefined"))
				{
					_database.AddInParameter(cmd, "@IsUserDefined", SqlDbType.Bit, isUserDefined);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<DataSourceAllTypesInfo> GetDataSourceAllTypes(Boolean isActive)
		{
			List<DataSourceAllTypesInfo> result = new List<DataSourceAllTypesInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceAllTypes_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceAllTypesInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<DataSourceTypeInfo> GetDataSourceTypeUserDefined(Boolean isActive)
		{
			List<DataSourceTypeInfo> result = new List<DataSourceTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceType_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<DataSourceTypeRefFileTypeInfo> GetDataSourceTypeRefFileType()
		{
			List<DataSourceTypeRefFileTypeInfo> result = new List<DataSourceTypeRefFileTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceTypeRefFileType_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeRefFileTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		public List<AIMPriorityUsedValueInfo> GetAIMPriorityUsedValue(Int32 aimPriorityUsedValueID)
		{
			List<AIMPriorityUsedValueInfo> result = new List<AIMPriorityUsedValueInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMPriorityUsedValue_SEL_ID"))
				{
					_database.AddInParameter(cmd, "@AIMPriorityUsedValueID", SqlDbType.Int, aimPriorityUsedValueID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMPriorityUsedValueInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<ShowCodeNameOrBothInfo> GetCodeNameOrBothValues()
		{
			List<ShowCodeNameOrBothInfo> result = new List<ShowCodeNameOrBothInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ShowCodeNameOrBoth_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ShowCodeNameOrBothInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMPriorityUsedValueInfo> GetAIMPriorityUsedValue()
		{
			List<AIMPriorityUsedValueInfo> result = new List<AIMPriorityUsedValueInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMPriorityUsedValue_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMPriorityUsedValueInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMPriorityUsedValueInfo> GetAIMPriorityUsedValueExcludeDefault()
		{
			List<AIMPriorityUsedValueInfo> result = new List<AIMPriorityUsedValueInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMPriorityUsedValue_SEL_ExcludeDefault"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMPriorityUsedValueInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public DataSourceTypeInfo GetDataSourceType_DataSourceTypeID(Int32 dataSourceTypeID)
		{
			DataSourceTypeInfo result = new DataSourceTypeInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceType_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<DataSourceTypeInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<DataSourceTypeInfo> GetDataSourceFurtherAnalyses(Boolean isFurtherAnalyses)
		{
			List<DataSourceTypeInfo> result = new List<DataSourceTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceType_SEL_IsFurtherAnalyses"))
				{
					_database.AddInParameter(cmd, "@IsFurtherAnalyses", SqlDbType.Bit, isFurtherAnalyses);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<FileTypeInfo> GetFileType(Boolean isActive)
		{
			List<FileTypeInfo> result = new List<FileTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_FileType_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<FileTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<FileTypeInfo> GetFileType(Int32 fileTypeID)
		{
			List<FileTypeInfo> result = new List<FileTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_FileType_SEL_FileTypeID"))
				{
					_database.AddInParameter(cmd, "@FileTypeID", SqlDbType.Int, fileTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<FileTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/24/21	
		public ErrorInfo GetLabelDefault(String controlName)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Label_SEL_Default"))
				{
					_database.AddInParameter(cmd, "@ControlName", SqlDbType.VarChar, controlName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}



		public List<ExcelFileTypeInfo> GetExcelFileType(Boolean isActive)
		{
			List<ExcelFileTypeInfo> result = new List<ExcelFileTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExcelFileType_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExcelFileTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ExcelFileTypeInfo> GetExcelFileTypeName(String name)
		{
			List<ExcelFileTypeInfo> result = new List<ExcelFileTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExcelFileType_SEL_Name"))
				{
					_database.AddInParameter(cmd, "@Name", SqlDbType.NVarChar, name);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExcelFileTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//TODO - Is ImportFileTypeInfo used???? --> FileTypeInfo is used but I don't think ImportFileTypeInfo is used
		public List<ImportFileTypeInfo> GetImportFileType(Boolean isActive)
		{
			List<ImportFileTypeInfo> result = new List<ImportFileTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportFileType_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ImportFileTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<DelimiterTypeInfo> GetDelimiterType(Boolean isActive)
		{
			List<DelimiterTypeInfo> result = new List<DelimiterTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DelimiterType_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DelimiterTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/1/19
		public List<DecimalSeparatorInfo> GetDecimalSeparator(Boolean isActive)
		{
			List<DecimalSeparatorInfo> result = new List<DecimalSeparatorInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DecimalSeparator_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DecimalSeparatorInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/3/19        
		public DecimalSeparatorInfo GetDecimalSeparator(Int32 decimalSeparatorID)
		{
			DecimalSeparatorInfo result = new DecimalSeparatorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DecimalSeparator_SEL_DecimalSeparatorID"))
				{
					_database.AddInParameter(cmd, "@DecimalSeparatorID", SqlDbType.Int, decimalSeparatorID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<DecimalSeparatorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		//------------------------------------

		public List<DateFormatInfo> GetDateFormat()
		{
			List<DateFormatInfo> result = new List<DateFormatInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DateFormat_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DateFormatInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/11/18
		public List<UserDefaultsInfo> GetUserDefaultsForCFCUserID(Int32 cfcUserID)
		{
			List<UserDefaultsInfo> result = new List<UserDefaultsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefaults_SEL_CFCUserID"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<UserDefaultsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public DateFormatInfo GetDateFormat(Int32 dateFormatID)
		{
			DateFormatInfo result = new DateFormatInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DateFormat_SEL_DateFormatID"))
				{
					_database.AddInParameter(cmd, "@DateFormatID", SqlDbType.Int, dateFormatID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<DateFormatInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//12/7/18
		public List<DataSourceTypeRefFileTypeInfo> GetDataSourceTypeRefFileType(Int32 dataSourceTypeID, Boolean isSystem)
		{
			List<DataSourceTypeRefFileTypeInfo> result = new List<DataSourceTypeRefFileTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceTypeRefFileType_SEL_DataSourceTypeID_IsSystem"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@IsSystem", SqlDbType.Bit, isSystem);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeRefFileTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<DataSourceTypeRefFileTypeInfo> GetDataSourceTypeRefFileType(Int32 dataSourceTypeID)
		{
			List<DataSourceTypeRefFileTypeInfo> result = new List<DataSourceTypeRefFileTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceTypeRefFileType_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeRefFileTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<DataSourceTypeRefFileTypeInfo> GetDataSourceTypeRefFileType(Int32 dataSourceTypeID, Int32 fileTypeID)
		{
			List<DataSourceTypeRefFileTypeInfo> result = new List<DataSourceTypeRefFileTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceTypeRefFileType_SEL_DataSourceTypeID_FileTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@FileTypeID", SqlDbType.Int, fileTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeRefFileTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<FileTypeRefDelimiterTypeInfo> GetFileTypeRefDelimiterType(Int32 fileTypeID)
		{
			List<FileTypeRefDelimiterTypeInfo> result = new List<FileTypeRefDelimiterTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_FileTypeRefDelimiterType_SEL_FileTypeID"))
				{
					_database.AddInParameter(cmd, "@FileTypeID", SqlDbType.Int, fileTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<FileTypeRefDelimiterTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<UpdateIngredientDetail> GetExportBatchMaintenance(Int32 UpdateIngredientHeaderID)
		{
			List<UpdateIngredientDetail> result = new List<UpdateIngredientDetail>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientDetail_SEL_UpdateIngredientHeaderID"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientHeaderID", SqlDbType.Int, UpdateIngredientHeaderID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						{
							ObjectFiller<UpdateIngredientDetail>.GetGenericCollection(ref result, reader);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/12/24
		public List<UpdateIngredientDetail> GetExportBatchMaintenance(Int32 updateIngredientHeaderID, Int32 updateIngredientDetailID)
		{
			List<UpdateIngredientDetail> result = new List<UpdateIngredientDetail>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientDetail_SEL_UpdateIngredientHeaderID_UpdateIngredientDetailID"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientHeaderID", SqlDbType.Int, updateIngredientHeaderID);
					_database.AddInParameter(cmd, "@UpdateIngredientDetailID", SqlDbType.Int, updateIngredientDetailID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						{
							ObjectFiller<UpdateIngredientDetail>.GetGenericCollection(ref result, reader);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/9/18
		public ErrorInfo GetExportBatchMaintenanceCheckIsOverlay(Int32 updateIngredientHeaderID, Int32 status, Boolean isOverlay)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientDetail_SEL_UpdateIngredientHeaderIDStatusIsOverlay"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientHeaderID", SqlDbType.Int, updateIngredientHeaderID);
					_database.AddInParameter(cmd, "@Status", SqlDbType.Int, status);
					_database.AddInParameter(cmd, "@IsOverlay", SqlDbType.Bit, isOverlay);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name); result.ErrorNumber = -1;
			}
			return result;
		}


		public List<UpdateIngredientDetail> GetExportBatchMaintenanceDefault(Int32 UpdateIngredientHeaderID)
		{
			List<UpdateIngredientDetail> result = new List<UpdateIngredientDetail>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientDetail_SEL_UpdateIngredientHeaderID_Default"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientHeaderID", SqlDbType.Int, UpdateIngredientHeaderID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						{
							ObjectFiller<UpdateIngredientDetail>.GetGenericCollection(ref result, reader);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<UpdateIngredientDetail> GetExportBatchMaintenanceSpecific(Int32 UpdateIngredientHeaderID)
		{
			List<UpdateIngredientDetail> result = new List<UpdateIngredientDetail>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientDetail_SEL_UpdateIngredientHeaderID_Specific"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientHeaderID", SqlDbType.Int, UpdateIngredientHeaderID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						{
							ObjectFiller<UpdateIngredientDetail>.GetGenericCollection(ref result, reader);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalysisExportBatchMaintenance> GetUpdateIngredient(Int32 Status)
		{
			List<AnalysisExportBatchMaintenance> result = new List<AnalysisExportBatchMaintenance>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientHeader_SEL_Status"))
				{
					_database.AddInParameter(cmd, "@Status", SqlDbType.Int, Status);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						{
							ObjectFiller<AnalysisExportBatchMaintenance>.GetGenericCollection(ref result, reader);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AnalysisExportBatchMaintenance> GetExportBatchMaintenance()
		{
			List<AnalysisExportBatchMaintenance> result = new List<AnalysisExportBatchMaintenance>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientHeader_SEL_ALL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						{
							ObjectFiller<AnalysisExportBatchMaintenance>.GetGenericCollection(ref result, reader);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public AIMNutrientInfo GetAIMNutrient(String aimNutrientCode)
		{
			AIMNutrientInfo result = new AIMNutrientInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrient_SEL_AIMNutrientCode"))
				{
					_database.AddInParameter(cmd, "@AnalyzerNutrientCode", SqlDbType.VarChar, aimNutrientCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMNutrientInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/2/20
		//public AIMNutrientInfo GetAIMNutrient(Int32 aimNutrientID)
		public AIMNutrientInfo GetAIMNutrientIsActive(Int32 aimNutrientID, Boolean isActive)

		{
			AIMNutrientInfo result = new AIMNutrientInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrient_SEL_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMNutrientInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public AnalysisExportBatchMaintenance GetUpdateIngredientHeader(Int32 UpdateIngredientHeaderID)
		{
			AnalysisExportBatchMaintenance result = new AnalysisExportBatchMaintenance();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientHeader_SEL_UpdateIngredientHeaderID"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientHeaderID", SqlDbType.Int, UpdateIngredientHeaderID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalysisExportBatchMaintenance>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AnalyzerMaterialInfo> GetAnalyzerMaterial(Boolean isActive)
		{
			List<AnalyzerMaterialInfo> result = new List<AnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterial_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<DefaultSettingsInfo> GetDefaultSettings(Int32 CFCUserID)
		{
			List<DefaultSettingsInfo> result = new List<DefaultSettingsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DefaultSettings_SEL_CFCUserID"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DefaultSettingsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//12/16/20
		public AnalysisUserDefaultSettingsInfo GetAnalysisUserDefaultSettings(Int32 CFCUserID)
		{
			AnalysisUserDefaultSettingsInfo result = new AnalysisUserDefaultSettingsInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisUserDefaultSettings_SEL_CFCUserID"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalysisUserDefaultSettingsInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public SystemSettingsInfo GetSystemSettings()
		{
			SystemSettingsInfo result = new SystemSettingsInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SystemSettings_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<SystemSettingsInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AnalyzerInstrumentInfo> GetAllAnalyzerInstrument()
		{
			List<AnalyzerInstrumentInfo> result = new List<AnalyzerInstrumentInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Instrument_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerInstrumentInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerInstrumentInfo> GetAnalyzerInstrument(Int32 dataSourceTypeID)
		{
			List<AnalyzerInstrumentInfo> result = new List<AnalyzerInstrumentInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Instrument_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerInstrumentInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//public void ReturnProcMessage(String message, String appName, Int32 CFCUserID)
		//{
		//	if (appName == "CFCAIMWorker")
		//	{
		//		AIMManager.InsertSystemLog((Int32)SystemLogType.Info, message, (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, CFCUserID);
		//	}
		//	else
		//	{
		//		HandleMessage(message, MethodInfo.GetCurrentMethod().Name);
		//	}
		//}

		public List<AnalyzerMaterialInfo> GetAnalyzerMaterial(Int32 DataSourceTypeID, Boolean isActive)
		{
			List<AnalyzerMaterialInfo> result = new List<AnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterial_SEL_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMNutrientInfo> GetAIMNutrient(Int32 DataSourceTypeID, Boolean isActive)
		{
			List<AIMNutrientInfo> result = new List<AIMNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrient_SEL_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerMaterialInfo> GetAnalyzerMaterial(DataSourceTypeEnum dataSourceType, Int32 aimMaterialID)
		{
			List<AnalyzerMaterialInfo> result = new List<AnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterial_SEL_DataSourceTypeIDAIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, (Int32)dataSourceType);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AnalyzerMaterialInfo GetAnalyzerMaterial(Int32 dataSourceTypeID, String analyzerMaterialCode)
		{
			AnalyzerMaterialInfo result = new AnalyzerMaterialInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterial_SEL_DataSourceTypeIDAnalyzerMaterialCode"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerMaterialCode", SqlDbType.VarChar, analyzerMaterialCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerMaterialInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AnalyzerSupplierInfo GetAnalyzerSupplier(DataSourceTypeEnum dataSourceType, String analyzerSupplierCode)
		{
			AnalyzerSupplierInfo result = new AnalyzerSupplierInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplier_SEL_AnalyzerSupplierCode_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.VarChar, (Int32)dataSourceType);
					_database.AddInParameter(cmd, "@AnalyzerSupplierCode", SqlDbType.VarChar, analyzerSupplierCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerSupplierInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AnalyzerOwnerInfo GetAnalyzerOwnerDataSourceTypeOwnerCode(Int32 dataSourceTypeID, String analyzerOwnerCode)
		{
			AnalyzerOwnerInfo result = new AnalyzerOwnerInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwner_SEL_DataSourceTypeID_OwnerCode"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.VarChar, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerOwnerCode", SqlDbType.VarChar, analyzerOwnerCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerOwnerInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AnalyzerSupplierInfo GetAnalyzerSupplier(Int32 dataSourceTypeID, String analyzerSupplierCode)
		{
			AnalyzerSupplierInfo result = new AnalyzerSupplierInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplier_SEL_AnalyzerSupplierCode_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierCode", SqlDbType.VarChar, analyzerSupplierCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerSupplierInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMMaterialRefAnalyzerMaterialInfo> GetAnalyzerMaterialsForUtility(Int32 dataSourceType, Int32 materialTypeID, Int32 aimOwnerLocationID)
		{
			List<AIMMaterialRefAnalyzerMaterialInfo> result = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID_MaterialTypeID_AIMOwnerLocationID_For_Utility"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceType);
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, materialTypeID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialRefAnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterial_DataSource_MaterialType_OwnerLocation(Int32 dataSourceType, Int32 materialTypeID, Int32 aimOwnerLocationID)
		{
			List<AIMMaterialRefAnalyzerMaterialInfo> result = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID_MaterialTypeID_AIMOwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceType);
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, materialTypeID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialRefAnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//9/23/22
		public List<SpecificIngredientsInfo> GetSpecificIngredients_GroupID_DataSourceType_IsActive(Int32 groupID, Int32 dataSourceType, Boolean isActive)
		{
			List<SpecificIngredientsInfo> result = new List<SpecificIngredientsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredients_SEL_GroupID_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@SpecificIngredientsGroupID", SqlDbType.Int, groupID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceType);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SpecificIngredientsInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialWithAnalyzerMaterial(Int32 dataSourceType)
		{
			List<AIMMaterialRefAnalyzerMaterialInfo> result = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialWithAnalyzerMaterial_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialRefAnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AnalyzerMaterialInfo> GetAnalyzerMaterial_AnalyzerMaterialID(Int32 analyzerMaterialID)
		{
			List<AnalyzerMaterialInfo> result = new List<AnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterial_SEL_AnalyzerMaterialID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, analyzerMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerMaterialInfo> GetAnalyzerMaterial_MaterialID_DataSourceTypeID_IsActive(Int32 analyzerMaterialID, Int32 datasourceTypeID, Int32 isActive)
		{
			List<AnalyzerMaterialInfo> result = new List<AnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterial_SEL_MaterialID_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, analyzerMaterialID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerMaterialInfo> GetAnalyzerMaterial()
		{
			List<AnalyzerMaterialInfo> result = new List<AnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterial_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AnalyzerPendingSampleInfo> GetAnalyzerPendingSample(AnalyzerStatusEnum status)
		{
			List<AnalyzerPendingSampleInfo> result = new List<AnalyzerPendingSampleInfo>();
			String proc = String.Empty;

			try
			{
				switch (status)
				{
					case AnalyzerStatusEnum.NotSent:
						proc = "dbo.usp_AnalyzerPendingSample_SEL_SentStampIsNull";
						break;
					case AnalyzerStatusEnum.NotReceived:
						proc = "dbo.usp_AnalyzerPendingSample_SEL_ReceivedStampIsNull";
						break;
					case AnalyzerStatusEnum.NotSentOrNotReceived:
						proc = "dbo.usp_AnalyzerPendingSample_SEL_SentStampOrReceivedStampIsNull";
						break;
				}

				using (DbCommand cmd = _database.GetStoredProcCommand(proc))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerPendingSampleInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public AnalyzerPendingSampleInfo GetAnalyzerPendingSample(Int32 sampleID)
		{
			AnalyzerPendingSampleInfo result = new AnalyzerPendingSampleInfo();

			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerPendingSample_SEL_SampleID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerPendingSampleInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/16/24
		public List<AnalyzerPendingSampleInfo> GetAnalyzerPendingSample(DateTime begDate, DateTime endDate, Int32 dateField)
		{
			List<AnalyzerPendingSampleInfo> result = new List<AnalyzerPendingSampleInfo>();

			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerPendingSample_SEL_BeginDate_EndDate_DateField"))
				{
					_database.AddInParameter(cmd, "@BegDate", SqlDbType.Date, begDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, endDate);
					_database.AddInParameter(cmd, "@DateField", SqlDbType.Int, dateField);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerPendingSampleInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerPendingSampleInfo> GetAnalyzerPendingSampleList(Int32 sampleID)
		{
			List<AnalyzerPendingSampleInfo> result = new List<AnalyzerPendingSampleInfo>();

			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerPendingSample_SEL_SampleID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerPendingSampleInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerMaterialInfo> GetAnalyzerMaterial(Int32 dataSourceTypeID)
		{
			List<AnalyzerMaterialInfo> result = new List<AnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterial_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<PNEMaterialSampleTypeInfo> GetPNEMaterialSampleType()
		{
			List<PNEMaterialSampleTypeInfo> result = new List<PNEMaterialSampleTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEMaterialSampleType_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PNEMaterialSampleTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<PNEMaterialTypeInfo> GetPNEMaterialType(Int32 analyzerMaterialID)
		{
			List<PNEMaterialTypeInfo> result = new List<PNEMaterialTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEMaterialType_SEL_AnalyzerMaterialID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, analyzerMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PNEMaterialTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<PNEForAnalyzerMaterialRefAnalyzerNutrientInfo> GetPNEForAnalyzerMaterialRefAnalyzerNutrient(Int32 analyzerMaterialID)
		{
			List<PNEForAnalyzerMaterialRefAnalyzerNutrientInfo> result = new List<PNEForAnalyzerMaterialRefAnalyzerNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEForAnalyzerMaterialRefAnalyzerNutrient_SEL_AnalyzerMaterialID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, analyzerMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PNEForAnalyzerMaterialRefAnalyzerNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<PNEMaterialTypeInfo> GetPNEMaterialType()
		{
			List<PNEMaterialTypeInfo> result = new List<PNEMaterialTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEMaterialType_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PNEMaterialTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AnalyzerMaterialRefAnalyzerAnalyseInfo> GetAnalyzerMaterialAnalyse(Int32 analyzerMaterialID, Int32 dataSourceTypeID)
		{
			List<AnalyzerMaterialRefAnalyzerAnalyseInfo> result = new List<AnalyzerMaterialRefAnalyzerAnalyseInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterialRefAnalyzerAnalyse_SEL"))
				{
					_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, analyzerMaterialID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerMaterialRefAnalyzerAnalyseInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMOwnerLocationInfo> GetAllAIMOwnerLocation(Boolean isActive)
		{
			List<AIMOwnerLocationInfo> result = new List<AIMOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		////////////11/30/22
		//////////public List<AIMOwnerLocationInfo> GetAIMOwnerLocationMaterialTemplate()
		//////////{
		//////////	List<AIMOwnerLocationInfo> result = new List<AIMOwnerLocationInfo>();
		//////////	try
		//////////	{
		//////////		using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SEL_MaterialLocations"))
		//////////		{					
		//////////			using (IDataReader reader = _database.ExecuteReader(cmd))
		//////////			{
		//////////				ObjectFiller<AIMOwnerLocationInfo>.GetGenericCollection(ref result, reader);
		//////////			}
		//////////		}
		//////////	}
		//////////	catch (Exception ex)
		//////////	{
		//////////		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//////////	}
		//////////	return result;
		//////////}

		//3/4/21
		//public List<AnalysisInfo> GetAnalysis(Boolean IsActive)
		public List<AnalysisInfo> GetAnalysis()
		{
			List<AnalysisInfo> info = new List<AnalysisInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_ALL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}
		public List<AnalysisTemplateInfo> GetAnalysisTemplate(Boolean isActive)
		{
			List<AnalysisTemplateInfo> info = new List<AnalysisTemplateInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalystTemplate_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisTemplateInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<QueryFieldInfo> GetQueryField(Boolean isActive)
		{
			List<QueryFieldInfo> result = new List<QueryFieldInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryField_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<QueryFieldInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<QueryOperationInfo> GetQueryOperation(Boolean isActive)
		{
			List<QueryOperationInfo> result = new List<QueryOperationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryOperation_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<QueryOperationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<QueryOperationInfo> GetQueryOperation(QueryFeildEnum queryField)
		{
			List<QueryOperationInfo> result = new List<QueryOperationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryOperation_SEL_QueryFieldID"))
				{
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, (Int32)queryField);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<QueryOperationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public QueryTemplateInfo GetQueryTemplate(Int32 queryTemplateID)
		{
			QueryTemplateInfo result = new QueryTemplateInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplate_SEL_QueryTemplateID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<QueryTemplateInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public QueryTemplateDetailNutrientInfo GetQueryTemplateDetailNutrient(Int32 queryTemplateID, Int32 aimMaterialID, Int32 aimNutrientID)
		{
			QueryTemplateDetailNutrientInfo result = new QueryTemplateDetailNutrientInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetailNutrient_SEL_QueryTemplateID_AIMMaterialID_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<QueryTemplateDetailNutrientInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//10/14/22
		public QueryTemplateDetailNutrientInfo GetQueryTemplateDetailNutrient(Int32 queryTemplateID)
		{
			QueryTemplateDetailNutrientInfo result = new QueryTemplateDetailNutrientInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetailNutrient_SEL_QueryTemplateID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<QueryTemplateDetailNutrientInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/15/22
		public QueryTemplateDetailNutrientInfo GetQueryTemplateDetailNutrientDataSource(Int32 queryTemplateID, Int32 dataSourceTypeID, Int32 aimNutrientID)

		{
			QueryTemplateDetailNutrientInfo result = new QueryTemplateDetailNutrientInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetailNutrient_SEL_QueryTemplateID_DataSourceTypeID_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<QueryTemplateDetailNutrientInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public QueryTemplateInfo GetQueryTemplateName(String name)
		{
			QueryTemplateInfo result = new QueryTemplateInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplate_SEL_Name"))
				{
					_database.AddInParameter(cmd, "@Name", SqlDbType.VarChar, name);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<QueryTemplateInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public DataSourceTypeInfo GetDataSourceUserName(String name)
		{
			DataSourceTypeInfo result = new DataSourceTypeInfo();
			try
			{
				//3/6/18 - this stp was renamed (same code in stp, just different name, to keep with naming convention)
				//using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceUser_SEL_Name"))
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceType_SEL_Name"))

				{
					_database.AddInParameter(cmd, "@Name", SqlDbType.VarChar, name);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<DataSourceTypeInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/22/21
		public SampleTypeInfo GetSampleTypeName(String name)
		{
			SampleTypeInfo result = new SampleTypeInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleType_SEL_SampleTypeName"))

				{
					_database.AddInParameter(cmd, "@SampleTypeName", SqlDbType.VarChar, name);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<SampleTypeInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public QueryTemplateDetailInfo GetQueryTemplateDetailForSaveAs(Int32 queryTemplateID)
		{
			QueryTemplateDetailInfo result = new QueryTemplateDetailInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<QueryTemplateDetailInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//10/31/22
		//public List<QueryTemplateInfo> GetQueryTemplate()
		public List<QueryTemplateInfo> GetQueryTemplate(String ingredientOrProduct)
		{
			List<QueryTemplateInfo> result = new List<QueryTemplateInfo>();
			try
			{
				//10/31/22
				//using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplate_SEL"))
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplate_SEL_IngredientOrProduct"))
				{
					_database.AddInParameter(cmd, "@IngredientOrProduct", SqlDbType.NVarChar, ingredientOrProduct);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<QueryTemplateInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/22/22
		public List<QueryTemplateInfo> GetQueryTemplateForReportsScreen()
		{
			List<QueryTemplateInfo> result = new List<QueryTemplateInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplate_SEL_ReportsScreen"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<QueryTemplateInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<QueryTemplateDetailInfo> GetQueryTemplateDetail(Int32 queryTemplateID)
		{
			List<QueryTemplateDetailInfo> result = new List<QueryTemplateDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<QueryTemplateDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<QueryTemplateDetailInfo> GetAIMSampleDateForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<QueryTemplateDetailInfo> result = new List<QueryTemplateDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<QueryTemplateDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/13/22
		public List<QueryTemplateDetailInfo> GetAIMAnalysisDateForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<QueryTemplateDetailInfo> result = new List<QueryTemplateDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<QueryTemplateDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMMaterialInfo> GetAIMMaterialForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<CFCIngredientInfo> GetCFCIngredientForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<CFCIngredientInfo> result = new List<CFCIngredientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCIngredientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/25/21
		public List<SampleTypeInfo> GetSampleTypeForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			List<SampleTypeInfo> result = new List<SampleTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID_Value"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//12/21/22
		public List<CFCProductTypeInfo> GetCFCProductTypeForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			List<CFCProductTypeInfo> result = new List<CFCProductTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID_Value"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/25/21
		public List<SampleTypeInfo> GetSampleTypeForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<SampleTypeInfo> result = new List<SampleTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//12/21/22
		public List<CFCProductTypeInfo> GetCFCProductTypeForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<CFCProductTypeInfo> result = new List<CFCProductTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCProductTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//5/26/22
		public List<DataSourceTypeInfo> GetDataSourceTypeForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			List<DataSourceTypeInfo> result = new List<DataSourceTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID_Value"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//5/26/22
		public List<DataSourceTypeInfo> GetDataSourceTypeForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<DataSourceTypeInfo> result = new List<DataSourceTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//6/14/22		
		public List<DataSourceComparisonInfo> GetDataSourceComparisonForTemplate(Int32 queryTemplateID, Int32 queryFieldID)

		{
			List<DataSourceComparisonInfo> result = new List<DataSourceComparisonInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_DataSourceComparison"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceComparisonInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCIngredientInfo> GetCFCIngredientForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			List<CFCIngredientInfo> result = new List<CFCIngredientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID_Value"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCIngredientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMNutrientInfo> GetAIMNutrientForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<AIMNutrientInfo> result = new List<AIMNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<TemplateMaterialNutrientInfo> GetMaterialNutrientForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<TemplateMaterialNutrientInfo> result = new List<TemplateMaterialNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<TemplateMaterialNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMMaterialInfo> GetAIMMaterialForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			List<AIMMaterialInfo> result = new List<AIMMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID_Value"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMNutrientInfo> GetAIMNutrientForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			List<AIMNutrientInfo> result = new List<AIMNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID_Value"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMOwnerInfo> GetAIMOwnerForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<AIMOwnerInfo> result = new List<AIMOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMOwnerInfo> GetAIMOwnerForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			List<AIMOwnerInfo> result = new List<AIMOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID_Value"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMSupplierInfo> GetAIMSupplierForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<AIMSupplierInfo> result = new List<AIMSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMSupplierInfo> GetAIMSupplierForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			List<AIMSupplierInfo> result = new List<AIMSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID_Value"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMOwnerLocationInfo> GetAIMOwnerLocationForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<AIMOwnerLocationInfo> result = new List<AIMOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMOwnerLocationInfo> GetAIMOwnerLocationForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			List<AIMOwnerLocationInfo> result = new List<AIMOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID_Value"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMSupplierLocationInfo> GetAIMSupplierLocationForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			List<AIMSupplierLocationInfo> result = new List<AIMSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMSupplierLocationInfo> GetAIMSupplierLocationForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			List<AIMSupplierLocationInfo> result = new List<AIMSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID_Value"))
				{

					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public CFCPlantRefAIMOwnerLocationInfo GetCFCPlantAimOwnerLocationCrossReference(Int32 aimOwnerLocationID)
		{
			CFCPlantRefAIMOwnerLocationInfo result = new CFCPlantRefAIMOwnerLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlantRefAimOwnerLocation_SEL_AimOwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<CFCPlantRefAIMOwnerLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AnalysisTemplateInfo GetOneAnalysisTemplate(Int32 analystTemplateID)
		{
			AnalysisTemplateInfo result = new AnalysisTemplateInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalystTemplate_SEL_AnalystTemplateID"))
				{
					_database.AddInParameter(cmd, "@AnalystTemplateID", SqlDbType.Int, analystTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalysisTemplateInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AnalysisInfo> GetAnalysisByDate(DateTime startDate, DateTime endDate)
		{
			List<AnalysisInfo> info = new List<AnalysisInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_GroupBy_Date"))
				{
					_database.AddInParameter(cmd, "@StartDate", SqlDbType.Date, startDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, endDate);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<AnalysisInfo> GetAnalysisByNoOne(DateTime startDate, DateTime endDate)
		{
			List<AnalysisInfo> info = new List<AnalysisInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_GroupBy_NoOne"))
				{
					_database.AddInParameter(cmd, "@StartDate", SqlDbType.Date, startDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, endDate);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		#region GetAnalysis - INGREDIENT
		public List<AnalysisInfo> GetAnalysis(
						AnalysisQueryEnum analysisQuery,
						Int32 queryTemplateID,
						DateTime startDate,
						DateTime endDate,
						Boolean isDefaultLocation,
						Boolean isSubSet,
						Int32 aimMaterialID,
						Int32 aimNutrientID,
						DateTime sampleDate,
						Int32 sampleID,
						Int32 aimOwnerID,
						Int32 aimOwnerLocationID,
						Int32 aimSupplierID,
						Int32 aimSupplierLocationID,
						Int32 cfcPlantID,
						Int32 warningGlobalH,
						Int32 warningMinMaxLimit,
						String sampleCode,
						Int32 aimDataBaseID,
						OutlierTypeEnum outlierType,
						Int32 cfcIngredientID,
						Int32 specificCFCPlantID,

						//6/28/21
						Boolean fromCharts,
						Int32 aimSupplierID2,
						Int32 aimSupplierID3,
						Int32 aimSupplierID4,
						Int32 aimSupplierLocationID2,
						Int32 aimSupplierLocationID3,
						Int32 aimSupplierLocationID4,
						Int32 aimOwnerID2,
						Int32 aimOwnerID3,
						Int32 aimOwnerID4,
						Int32 aimOwnerLocationID2,
						Int32 aimOwnerLocationID3,
						Int32 aimOwnerLocationID4,
						Int32 supplierOwnerOption               //9/8/21


					)
		{

			List<AnalysisInfo> info = new List<AnalysisInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_Analysis"))

				{
					_database.AddInParameter(cmd, "@AnalysisQuery", SqlDbType.Int, (Int32)analysisQuery);
					_database.AddInParameter(cmd, "@StartDate", SqlDbType.Date, startDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, endDate);
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@IsDefaultLocation", SqlDbType.Bit, isDefaultLocation);
					_database.AddInParameter(cmd, "@IsSubSet", SqlDbType.Bit, isSubSet);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDataBaseID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
					_database.AddInParameter(cmd, "@OutlierType", SqlDbType.Int, (Int32)outlierType);
					if (specificCFCPlantID > 0)
					{
						_database.AddInParameter(cmd, "@SpecificCFCPlantID", SqlDbType.Int, specificCFCPlantID);
					}

					{
						if (isSubSet == true)
						{
							_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
							_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
							if (sampleDate != DateTime.MinValue)
							{
								_database.AddInParameter(cmd, "@SampleDate", SqlDbType.Date, sampleDate);
							}
							if (sampleID != 0)
							{
								_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
							}
							if (aimOwnerID != 0)
							{
								_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, aimOwnerID);
							}
							if (aimOwnerLocationID != 0)
							{
								_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
							}
							if (aimSupplierID != 0)
							{
								_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, aimSupplierID);
							}
							if (aimSupplierLocationID != 0)
							{
								_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, aimSupplierLocationID);
							}
							if (cfcPlantID != 0)
							{
								_database.AddInParameter(cmd, "@CFCPlantID", SqlDbType.Int, cfcPlantID);
							}
							if (cfcIngredientID != 0)
							{
								_database.AddInParameter(cmd, "@CFCIngredientID", SqlDbType.Int, cfcIngredientID);
							}

							//6/28/21 ---------------------------------------------------------------
							_database.AddInParameter(cmd, "@FromCharts", SqlDbType.Int, fromCharts);
							if (aimSupplierID2 != 0)
							{
								_database.AddInParameter(cmd, "@AIMSourceID2", SqlDbType.Int, aimSupplierID2);
							}
							if (aimSupplierLocationID2 != 0)
							{
								_database.AddInParameter(cmd, "@AIMSourceLocationID2", SqlDbType.Int, aimSupplierLocationID2);
							}
							if (aimSupplierID3 != 0)
							{
								_database.AddInParameter(cmd, "@AIMSourceID3", SqlDbType.Int, aimSupplierID3);
							}
							if (aimSupplierLocationID3 != 0)
							{
								_database.AddInParameter(cmd, "@AIMSourceLocationID3", SqlDbType.Int, aimSupplierLocationID3);
							}
							if (aimSupplierID4 != 0)
							{
								_database.AddInParameter(cmd, "@AIMSourceID4", SqlDbType.Int, aimSupplierID4);
							}
							if (aimSupplierLocationID4 != 0)
							{
								_database.AddInParameter(cmd, "@AIMSourceLocationID4", SqlDbType.Int, aimSupplierLocationID4);
							}

							//7/12/21 ---------------------------------------------------------------							
							if (aimOwnerID2 != 0)
							{
								_database.AddInParameter(cmd, "@AIMOwnerID2", SqlDbType.Int, aimOwnerID2);
							}
							if (aimOwnerLocationID2 != 0)
							{
								_database.AddInParameter(cmd, "@AIMOwnerLocationID2", SqlDbType.Int, aimOwnerLocationID2);
							}
							if (aimOwnerID3 != 0)
							{
								_database.AddInParameter(cmd, "@AIMOwnerID3", SqlDbType.Int, aimOwnerID3);
							}
							if (aimOwnerLocationID3 != 0)
							{
								_database.AddInParameter(cmd, "@AIMOwnerLocationID3", SqlDbType.Int, aimOwnerLocationID3);
							}
							if (aimOwnerID4 != 0)
							{
								_database.AddInParameter(cmd, "@AIMOwnerID4", SqlDbType.Int, aimOwnerID4);
							}
							if (aimOwnerLocationID4 != 0)
							{
								_database.AddInParameter(cmd, "@AIMOwnerLocationID4", SqlDbType.Int, aimOwnerLocationID4);
							}

							//9/8/21
							if (supplierOwnerOption != 0)
							{
								_database.AddInParameter(cmd, "@SupplierOwnerOption", SqlDbType.Int, supplierOwnerOption);
							}

						}
					}

					String testSql = "";
					testSql = "exec usp_Sample_SEL_Analysis " + (Int32)analysisQuery + ", ";
					testSql += "'" + startDate + "', ";
					testSql += "'" + endDate + "', ";
					testSql += queryTemplateID + ", ";
					testSql += isDefaultLocation + ", ";
					testSql += isSubSet + ", ";
					testSql += aimDataBaseID + ", ";
					testSql += aimMaterialID + ", ";
					testSql += aimNutrientID + ", ";

					if (sampleDate != DateTime.MinValue)
					{
						testSql += "'" + sampleDate + "', ";
					}
					else
					{
						//9/21/18
						//testSql += "'', ";
						testSql += "NULL, ";

					}


					if (sampleID == 0)
					{
						testSql += "NULL, ";
					}
					else
					{
						testSql += sampleID + ", ";
					}
					if (aimOwnerID == 0)
					{
						testSql += "NULL, ";
					}
					else
					{
						testSql += aimOwnerID + ", ";
					}
					if (aimOwnerLocationID == 0)
					{
						testSql += "NULL, ";
					}
					else
					{
						testSql += aimOwnerLocationID + ", ";
					}
					if (aimSupplierID == 0)
					{
						testSql += "NULL, ";
					}
					else
					{
						testSql += aimSupplierID + ", ";
					}
					if (aimSupplierLocationID == 0)
					{
						testSql += "NULL, ";
					}
					else
					{
						testSql += aimSupplierLocationID + ", ";
					}
					if (cfcPlantID == 0)
					{
						testSql += "NULL, ";
					}
					else
					{
						testSql += cfcPlantID + ", ";
					}
					testSql += AIMCoreGlobal.CFCUserID + ", ";
					testSql += (Int32)outlierType + ", ";
					testSql += cfcIngredientID + ", ";
					testSql += specificCFCPlantID + ", ";

					//6/28/21
					testSql += fromCharts + ", ";
					testSql += aimSupplierID2 + ", ";
					testSql += aimSupplierLocationID2 + ", ";
					testSql += aimSupplierID3 + ", ";
					testSql += aimSupplierLocationID3 + ", ";
					testSql += aimSupplierID4 + ", ";
					testSql += aimSupplierLocationID4 + ", ";

					//7/12/21
					testSql += aimOwnerID2 + ", ";
					testSql += aimOwnerLocationID2 + ", ";
					testSql += aimOwnerID3 + ", ";
					testSql += aimOwnerLocationID3 + ", ";
					testSql += aimOwnerID4 + ", ";
					testSql += aimOwnerLocationID4 + ", ";

					//9/8/21
					testSql += supplierOwnerOption;

					//9/14/20
					cmd.CommandTimeout = 0;

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}
		#endregion


		public List<AnalysisProductInfo> GetAnalysisProduct(
				AnalysisQueryEnum analysisQuery,
				Int32 queryTemplateID,
				DateTime startDate,
				DateTime endDate,
				Int32 groupMaterials,
				Int32 nutrientToleranceHeaderID,
				Boolean isSubSet,
				Int32 aimMaterialID,
				Int32 aimNutrientID,
				DateTime sampleDate,
				Int32 sampleID,
				Int32 aimOwnerID,
				Int32 aimOwnerLocationID,
				String sampleCode,
				Int32 aimDataBaseID,
				OutlierTypeEnum outlierType

				//1/4/23
				, String archiveVersion

			)
		{

			List<AnalysisProductInfo> info = new List<AnalysisProductInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_AnalysisProduct"))
				{
					_database.AddInParameter(cmd, "@AnalysisQuery", SqlDbType.Int, (Int32)analysisQuery);
					_database.AddInParameter(cmd, "@StartDate", SqlDbType.Date, startDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Date, endDate);
					_database.AddInParameter(cmd, "@ApplyToleranceGroup", SqlDbType.Int, groupMaterials);
					_database.AddInParameter(cmd, "@NutrientToleranceHeaderID", SqlDbType.Int, nutrientToleranceHeaderID);
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@IsSubSet", SqlDbType.Bit, isSubSet);
					_database.AddInParameter(cmd, "@AimDataBaseID", SqlDbType.Int, aimDataBaseID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
					_database.AddInParameter(cmd, "@OutlierType", SqlDbType.Int, (Int32)outlierType);

					{
						if (isSubSet == true)
						{
							_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
							_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
							if (sampleDate != DateTime.MinValue)
							{
								_database.AddInParameter(cmd, "@SampleDate", SqlDbType.Date, sampleDate);
							}
							if (sampleID != 0)
							{
								_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
							}
							if (aimOwnerID != 0)
							{
								_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, aimOwnerID);
							}
							if (aimOwnerLocationID != 0)
							{
								_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
							}

							if (archiveVersion != null)
							{
								_database.AddInParameter(cmd, "@ArchiveVersion", SqlDbType.VarChar, archiveVersion);
							}

						}
					}

					String testSql = "";
					testSql = "exec usp_Sample_SEL_AnalysisProduct '', " + (Int32)analysisQuery + ", ";
					testSql += "'" + startDate + "', ";
					testSql += "'" + endDate + "', ";
					testSql += queryTemplateID + ", ";

					testSql += groupMaterials + ", ";
					testSql += nutrientToleranceHeaderID + ", ";

					testSql += isSubSet + ", ";
					testSql += aimDataBaseID + ", ";
					testSql += aimMaterialID + ", ";
					testSql += aimNutrientID + ", ";
					testSql += "'', ";
					testSql += sampleID + ", ";
					testSql += aimOwnerID + ", ";
					testSql += aimOwnerLocationID + ", ";
					testSql += AIMCoreGlobal.CFCUserID + ", ";
					testSql += (Int32)outlierType;

					//1/4/23
					testSql += ",'" + archiveVersion + "'";

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalysisProductInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<SampleInfo> GetAllSample(Boolean IsActive)
		{
			List<SampleInfo> info = new List<SampleInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}


		public List<SampleInfo> GetAllSampleUseSystemDateSetting(Boolean IsActive, Int32 days)
		{
			List<SampleInfo> info = new List<SampleInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_IsActive_SystemSettingDays"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, IsActive);
					_database.AddInParameter(cmd, "@Days", SqlDbType.Int, days);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//11/11/22
		public List<SampleInfo> GetAllSamples(Boolean IsActive)
		{
			List<SampleInfo> info = new List<SampleInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_IsActive_AllSamples"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}
		public List<SampleInfo> GetAllSampleFilter(Int32 filterControlID, String value, String dateBeg, String dateEnd)
		{
			List<SampleInfo> info = new List<SampleInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_Filter"))
				{
					_database.AddInParameter(cmd, "@FilterControlID", SqlDbType.Int, filterControlID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.NVarChar, value);
					_database.AddInParameter(cmd, "@DateBeg", SqlDbType.NVarChar, dateBeg);
					_database.AddInParameter(cmd, "@DateEnd", SqlDbType.NVarChar, dateEnd);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}


		public List<SampleTableStructureInfo> GetTableNames(String tableName)
		{
			List<SampleTableStructureInfo> info = new List<SampleTableStructureInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_ColumnNames"))
				{
					_database.AddInParameter(cmd, "@TableName", SqlDbType.VarChar, tableName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTableStructureInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<SampleExportHeaderInfo> GetAllSampleFilterCSVExport(Int32 exportTemplateID)
		{
			List<SampleExportHeaderInfo> info = new List<SampleExportHeaderInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_Export_CSV"))
				{
					_database.AddInParameter(cmd, "@ExportTemplateID", SqlDbType.Int, exportTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleExportHeaderInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}


		public List<SampleExportDetailInfo> GetAllSampleFilterCSVExportDetail(Int32 exportTemplateID)
		{
			List<SampleExportDetailInfo> info = new List<SampleExportDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_Export_CSV"))
				{
					_database.AddInParameter(cmd, "@ExportTemplateID", SqlDbType.Int, exportTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleExportDetailInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<SampleExportDetailInfo> GetAllSampleForExport(String sampleCodeBeg, String sampleCodeEnd, String dateBeg, String dateEnd)
		{
			List<SampleExportDetailInfo> info = new List<SampleExportDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_Export"))
				{
					_database.AddInParameter(cmd, "@SampleCodeBeg", SqlDbType.NVarChar, sampleCodeBeg);
					_database.AddInParameter(cmd, "@SampleCodeEnd", SqlDbType.NVarChar, sampleCodeEnd);
					_database.AddInParameter(cmd, "@DateBeg", SqlDbType.NVarChar, dateBeg);
					_database.AddInParameter(cmd, "@DateEnd", SqlDbType.NVarChar, dateEnd);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleExportDetailInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//11/11/22		
		public List<SampleInfo> GetSampleForUtility(String SampleCode)
		{
			List<SampleInfo> info = new List<SampleInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_SampleCode_ForUtility"))
				{
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.NVarChar, SampleCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public SampleInfo GetSample(String SampleCode)
		{
			SampleInfo info = new SampleInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_SampleCode"))
				{
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.VarChar, SampleCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<SampleInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public SampleInfo GetSampleForCharts(String SampleCode)
		{
			SampleInfo info = new SampleInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_SampleCode_For_Charts"))
				{
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.VarChar, SampleCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<SampleInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}


		//------------------------------------------------------------------
		//7/13/21 - Question: Datasource on Sample is used with auto-generated samples, to get the next available sample# for the datasource,
		//			because there may be multiple datasources on an import file -- so shouldn't datasourcetypeid be included?

		//3/12/21 - changed this back, because datasourcetypeid on the sample header shouldn't be used -- because
		//			one sample can have several datasourcetypes(which are on the AnalyzerNutrient and part of the results)
		//2/15/21
		//public SampleInfo GetSampleCodeForImports(String SampleCode, Int32 datasourceTypeID)
		public SampleInfo GetSampleCodeForImports(String SampleCode)
		{
			SampleInfo info = new SampleInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_SampleCode_ForImports"))
				{
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.VarChar, SampleCode);
					//_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<SampleInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}
		//------------------------------------------------------------------


		public SampleInfo GetSampleCodeForRomerImports(String SQLResultID, Int32 OwnerID, Int32 OwnerLocID, Int32 SupplierID, Int32 SupplierLocID)
		{
			SampleInfo info = new SampleInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SampleResult_SEL_SQLResultID_OwnerID_OwnerLocID_SupplierID_SupplierLocID"))
				{
					_database.AddInParameter(cmd, "@SQLResultID", SqlDbType.VarChar, SQLResultID);
					_database.AddInParameter(cmd, "@OwnerID", SqlDbType.Int, OwnerID);
					_database.AddInParameter(cmd, "@OwnerLocID", SqlDbType.Int, OwnerLocID);
					_database.AddInParameter(cmd, "@SupplierID", SqlDbType.Int, SupplierID);
					_database.AddInParameter(cmd, "@SupplierLocID", SqlDbType.Int, SupplierLocID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<SampleInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}


		public SampleInfo GetSample(String sampleCode, Int32 dataSourceTypeID)
		{
			SampleInfo info = new SampleInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_SampleCode_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.VarChar, sampleCode);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<SampleInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}


		public SampleInfo GetSampleAlternateCode(String alternateCode)
		{
			SampleInfo info = new SampleInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_AlternateCode"))
				{
					_database.AddInParameter(cmd, "@AlternateCode", SqlDbType.VarChar, alternateCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<SampleInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public SampleInfo GetSample(Int32 SampleID)
		{
			SampleInfo info = new SampleInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_SampleID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, SampleID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<SampleInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<SampleInfo> GetSampleMissingFurtherAnalysis(Int32 furtherAnalysisID, MaterialTypeEnum materialTypeID, DateTime createdStamp)
		{
			List<SampleInfo> result = new List<SampleInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_FurtherAnalysisIDMaterialTypeIDCreatedStampMissingFurtherAnalysis"))
				{
					_database.AddInParameter(cmd, "@FurtherAnalysisID", SqlDbType.Int, furtherAnalysisID);
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, (Int32)materialTypeID);
					_database.AddInParameter(cmd, "@CreatedStamp", SqlDbType.DateTime, createdStamp);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<SampleInfo> GetSampleMissingFurtherAnalysisResult(Int32 furtherAnalysisID, MaterialTypeEnum materialTypeID, DateTime createdStamp, String sqlResultID)
		{
			List<SampleInfo> result = new List<SampleInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_FurtherAnalysisIDMaterialTypeIDCreatedStampSQLResultIDMissingResult"))
				{
					_database.AddInParameter(cmd, "@FurtherAnalysisID", SqlDbType.Int, furtherAnalysisID);
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, (Int32)materialTypeID);
					_database.AddInParameter(cmd, "@CreatedStamp", SqlDbType.DateTime, createdStamp);
					_database.AddInParameter(cmd, "@SQLResultID", SqlDbType.NVarChar, sqlResultID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		//1/22/21
		public SampleInfo GetSampleUsingSampleType(Int32 sampleTypeID)
		{
			SampleInfo info = new SampleInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_SampleTypeID"))
				{
					_database.AddInParameter(cmd, "@SampleTypeID", SqlDbType.Int, sampleTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<SampleInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}


		public UserDefinedDataSourceDefinitionInfo GetDataSourceUserDefinition(Int32 UserDefinedDataSourceDefinitionID)
		{
			UserDefinedDataSourceDefinitionInfo info = new UserDefinedDataSourceDefinitionInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceDefinition_SEL_UserDefinedDataSourceDefinitionID"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, UserDefinedDataSourceDefinitionID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<UserDefinedDataSourceDefinitionInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public UserDefinedDataSourceDefinitionInfo GetDataSourceTypeID(Int32 DataSourceTypeID)
		{
			UserDefinedDataSourceDefinitionInfo info = new UserDefinedDataSourceDefinitionInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceDefinition_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<UserDefinedDataSourceDefinitionInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//12/16/20
		public List<UserDefinedDataSourceDefinitionInfo> GetImportsForRequiredField(Int32 ID, String fieldName)
		{
			List<UserDefinedDataSourceDefinitionInfo> result = new List<UserDefinedDataSourceDefinitionInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceDefinition_SEL_AimRequiredFieldID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, ID);
					_database.AddInParameter(cmd, "@FieldName", SqlDbType.VarChar, fieldName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<UserDefinedDataSourceDefinitionInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<UserDefinedDataSourceDefinitionInfo> GetUserDefinedDefinitionImportFileName(String importFileName)
		{
			List<UserDefinedDataSourceDefinitionInfo> result = new List<UserDefinedDataSourceDefinitionInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceDefinition_SEL_ImportFileName"))
				{
					_database.AddInParameter(cmd, "@ImportFileName", SqlDbType.NVarChar, importFileName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<UserDefinedDataSourceDefinitionInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public UserDefinedDataSourceDefinitionInfo GetUserDefinedDataSourceDefinitionDataSourceUserIDFileTypeID(Int32 dataSourceTypeID, Int32 fileTypeID)
		{
			UserDefinedDataSourceDefinitionInfo info = new UserDefinedDataSourceDefinitionInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceDefinition_SEL_DataSourceTypeID_FileTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@FileTypeID", SqlDbType.Int, fileTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<UserDefinedDataSourceDefinitionInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//1/19/24
		public List<PNEMaterialSampleTypeRefGroundUnGroundInfo> GetPNEMaterialSampleTypeRefGroundUnGround()
		{
			List<PNEMaterialSampleTypeRefGroundUnGroundInfo> result = new List<PNEMaterialSampleTypeRefGroundUnGroundInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEMaterialSampleTypeRefGroundUnGround_SEL"))
				{					
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PNEMaterialSampleTypeRefGroundUnGroundInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//2/8/24
		public List<PNEMaterialSampleTypeRefGroundUnGroundInfo> GetPNEMaterialSampleTypeRefGroundUnGround(String aimMaterialCode)
		{
			List<PNEMaterialSampleTypeRefGroundUnGroundInfo> result = new List<PNEMaterialSampleTypeRefGroundUnGroundInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEMaterialSampleTypeRefGroundUnGround_SEL_AIMMaterialCode"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialCode", SqlDbType.NVarChar, aimMaterialCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PNEMaterialSampleTypeRefGroundUnGroundInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//1/19/24
		public List<PNEMaterialSampleTypeRefGroundUnGroundInfo> GetPNEMaterialSampleTypeRefGroundUnGround(Int32 PNEMaterialSampleTypeID)
		{
			List<PNEMaterialSampleTypeRefGroundUnGroundInfo> result = new List<PNEMaterialSampleTypeRefGroundUnGroundInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEMaterialSampleTypeRefGroundUnGround_SEL_PNEMaterialSampleTypeID"))
				{
					_database.AddInParameter(cmd, "@PNEMaterialSampleTypeID", SqlDbType.Int, PNEMaterialSampleTypeID);				
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PNEMaterialSampleTypeRefGroundUnGroundInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/19/24
		public ErrorInfo SavePNEMaterialSampleTypeRefGroundUnGround(ref PNEMaterialSampleTypeRefGroundUnGroundInfo def)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEMaterialSampleTypeRefGroundUnGround_SAV"))
				{
					cmd.Parameters.AddRange(def.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//1/19/24
		public void DeletePNEMaterialSampleTypeRefGroundUnGround(Int32 id)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEMaterialSampleTypeRefGroundUnGround_DEL"))
				{
					_database.AddInParameter(cmd, "@PNEMaterialSampleTypeRefGroundUnGroundID", SqlDbType.Int, id);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public List<DataSourceTypeRefFileTypeInfo> GetDataSourceTypeIDFileID(Int32 DataSourceTypeID, Int32 FileTypeID)
		{
			List<DataSourceTypeRefFileTypeInfo> result = new List<DataSourceTypeRefFileTypeInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceTypeRefFileType_SEL_DataSourceTypeID_FileTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					_database.AddInParameter(cmd, "@FileTypeID", SqlDbType.Int, FileTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeRefFileTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public DataSourceTypeInfo GetUserDefinedDataSourceTypeID(Int32 DataSourceTypeID)
		{
			DataSourceTypeInfo info = new DataSourceTypeInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSource_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<DataSourceTypeInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<SampleLabelInfo> GetOneSampleLabel(Int32 SampleID, Boolean printNameFirst)
		{
			List<SampleLabelInfo> info = new List<SampleLabelInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_Label"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, SampleID);
					_database.AddInParameter(cmd, "@PrintNameFirst", SqlDbType.Bit, printNameFirst);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleLabelInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//5/2/23
		public List<SampleLabelInfo> GetOneSampleProdLabel(Int32 SampleID, Boolean printNameFirst)
		{
			List<SampleLabelInfo> info = new List<SampleLabelInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_ProdLabel"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, SampleID);
					_database.AddInParameter(cmd, "@PrintNameFirst", SqlDbType.Bit, printNameFirst);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleLabelInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}



		public List<PertenSQLInfo> GetPertenSqlData(String sampleNumber, DateTime startDate, DateTime endDate)
		{
			List<PertenSQLInfo> info = new List<PertenSQLInfo>();
			try
			{
				if (sampleNumber.Length == 0)
				{
					sampleNumber = null;
				}

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EstimateEvent_SEL_AnalysisEndTimeSampleNumber"))
				{
					_database.AddInParameter(cmd, "@SampleNumber", SqlDbType.NVarChar, sampleNumber);
					_database.AddInParameter(cmd, "@AnalysisStartTime", SqlDbType.DateTime, startDate.ToString("yyyy-MM-dd") + " 00:00:00.000");
					_database.AddInParameter(cmd, "@AnalysisEndTime", SqlDbType.DateTime, endDate.ToString("yyyy-MM-dd") + " 23:59:59.999");
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<PertenSQLInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//2/26/20
		public List<BrukerSQLInfo> GetBrukerSqlData(String sampleCode, DateTime startDate, DateTime endDate)
		{
			List<BrukerSQLInfo> info = new List<BrukerSQLInfo>();
			try
			{
				if (sampleCode.Length == 0)
				{
					sampleCode = null;
				}

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_T1_Quant2_Results_SEL_DateTimeAnalysisSampleCode"))
				{
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.NVarChar, sampleCode);
					_database.AddInParameter(cmd, "@AnalysisStartTime", SqlDbType.DateTime, startDate.ToString("yyyy-MM-dd") + " 00:00:00.000");
					_database.AddInParameter(cmd, "@AnalysisEndTime", SqlDbType.DateTime, endDate.ToString("yyyy-MM-dd") + " 23:59:59.999");
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<BrukerSQLInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//11/23/22
		public List<AIMOwnerInfo> GetAIMOwner(Int32 OwnerLocationGroupID, Boolean isActive)
		{
			List<AIMOwnerInfo> info = new List<AIMOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwner_SEL_IsActive_GroupID_IsActive"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationGroupID", SqlDbType.Int, OwnerLocationGroupID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<AIMOwnerInfo> GetAIMOwner(Boolean isActive)
		{
			List<AIMOwnerInfo> info = new List<AIMOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwner_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<AIMOwnerInfo> GetAIMOwnerWithValidOwnerLocations(Boolean isActive)
		{
			List<AIMOwnerInfo> info = new List<AIMOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwner_SEL_IsActive_HasOwnerLocations"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<AIMSupplierInfo> GetAIMSupplierWithValidSupplierLocations(Boolean isActive)
		{
			List<AIMSupplierInfo> info = new List<AIMSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_SEL_IsActive_HasSourceLocations"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//11/23/22
		//public List<AIMOwnerInfo> GetAIMOwnerAll()
		public List<AIMOwnerInfo> GetAIMOwnerAll(Int32 aimOwnerLocationGroupID)
		{
			List<AIMOwnerInfo> info = new List<AIMOwnerInfo>();
			try
			{
				//11/23/22
				//using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwner_SEL"))
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwner_SEL_AIMOwnerLocationGroupID"))

				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationGroupID", SqlDbType.Int, aimOwnerLocationGroupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//8/30/23
		//public List<AIMSupplierInfo> GetAIMSupplierAll()
		public List<AIMSupplierInfo> GetAIMSupplierAllStatus(Int32 aimSupplierGroupID)
		{
			List<AIMSupplierInfo> info = new List<AIMSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplier_SEL_aimSupplierGroupID"))
				{
					//8/30/23
					_database.AddInParameter(cmd, "@AIMSupplierGroupID", SqlDbType.Int, aimSupplierGroupID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}


		public List<SampleImportUserDefinedMapSelectionInfo> GetMapSelection(Boolean isActive)
		{
			List<SampleImportUserDefinedMapSelectionInfo> info = new List<SampleImportUserDefinedMapSelectionInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_MapSelection_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleImportUserDefinedMapSelectionInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//8/19/22 - added for Romer imports
		public List<SampleImportUserDefinedMapSelectionInfo> GetMapSelectionSkipUseMapping(Boolean isActive)
		{
			List<SampleImportUserDefinedMapSelectionInfo> info = new List<SampleImportUserDefinedMapSelectionInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_MapSelection_SEL_IsActive_Skip_UseMapping"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleImportUserDefinedMapSelectionInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//12/3/19
		//public List<SampleImportColumnNamesInfo> GetSampleColumnNames(Boolean swIncludeDataSourceColumn)
		public List<SampleImportColumnNamesInfo> GetSampleColumnNames(Boolean swIncludeDataSourceColumn, Boolean swUseSystemSampleCodes)
		{
			List<SampleImportColumnNamesInfo> info = new List<SampleImportColumnNamesInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleColumnNames_SEL"))
				{
					_database.AddInParameter(cmd, "@IncludeDataSource", SqlDbType.Bit, swIncludeDataSourceColumn);
					_database.AddInParameter(cmd, "@UseSystemSampleCodes", SqlDbType.Bit, swUseSystemSampleCodes);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleImportColumnNamesInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//12/4/19
		//4/1/19
		//public List<SampleImportColumnNamesInfo> GetSampleColumnNames_CanAlter(Boolean swIncludeDataSourceColumn)
		public List<SampleImportColumnNamesInfo> GetSampleColumnNames_CanAlter(Boolean swIncludeDataSourceColumn, Boolean swUseSystemSampleCodes)
		{
			List<SampleImportColumnNamesInfo> info = new List<SampleImportColumnNamesInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleColumnNames_SEL_CanAlter"))
				{
					_database.AddInParameter(cmd, "@IncludeDataSource", SqlDbType.Bit, swIncludeDataSourceColumn);
					_database.AddInParameter(cmd, "@UseSystemSampleCodes", SqlDbType.Bit, swUseSystemSampleCodes);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleImportColumnNamesInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//7/29/19
		public List<SampleImportColumnNamesInfo> GetSampleColumnNames_HeaderOnly()
		{
			List<SampleImportColumnNamesInfo> info = new List<SampleImportColumnNamesInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleColumnNames_SEL_HeaderOnly"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleImportColumnNamesInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//TODO - 4/3/18 - this stp is not used anymore -- can be deleted when ImportTextFile.cs is removed.
		public SampleImportColumnNamesInfo GetSampleColumnNames(String mapColumnName)
		{
			SampleImportColumnNamesInfo info = new SampleImportColumnNamesInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleColumnNames_SEL_MapColumnName"))
				{

					_database.AddInParameter(cmd, "@MapColumnName", SqlDbType.NVarChar, mapColumnName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{

						info = ObjectFiller<SampleImportColumnNamesInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{

				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}


		public List<UnitTypeInfo> GetUnitType(Boolean isActive)
		{
			List<UnitTypeInfo> info = new List<UnitTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UnitType_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<UnitTypeInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}
		public AIMOwnerInfo GetOneAIMOwner(Int32 ownerID)
		{
			AIMOwnerInfo result = new AIMOwnerInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwner_SEL_AIMOwnerID"))
				{
					_database.AddInParameter(cmd, "@OwnerID", SqlDbType.Int, ownerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMOwnerInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMOwnerLocationInfo> GetAIMOwnerLocation(Boolean isActive)
		{
			List<AIMOwnerLocationInfo> info = new List<AIMOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}
		public AIMOwnerLocationInfo GetOneAIMOwnerLocation(Int32 ownerLocationID)
		{
			AIMOwnerLocationInfo result = new AIMOwnerLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SEL_OwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@OwnerLocationID", SqlDbType.Int, ownerLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMOwnerLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/5/22
		public List<AIMOwnerLocationInfo> GetAIMOwnerLocationForOwnerWithOrMissingXref(Int32 ownerID, Boolean isXref)
		{
			List<AIMOwnerLocationInfo> info = new List<AIMOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_CFCPlantRefAIMOwnerLocation_SEL_OwnerID_IsXref"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, ownerID);
					_database.AddInParameter(cmd, "@IsXRef", SqlDbType.Bit, isXref);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<AIMOwnerLocationInfo> GetAIMOwnerLocationForOwner(Int32 ownerID)
		{
			List<AIMOwnerLocationInfo> info = new List<AIMOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SEL_OwnerID"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, ownerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//11/23/22
		public List<AIMOwnerLocationInfo> GetAIMOwnerLocationForOwner(Int32 ownerID, Int32 aimOwnerLocationGroupID)
		{
			List<AIMOwnerLocationInfo> info = new List<AIMOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SEL_OwnerID_AIMOwnerLocationGroupID"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, ownerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationGroupID", SqlDbType.Int, aimOwnerLocationGroupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//11/23/22
		//public List<AIMOwnerLocationInfo> GetAIMOwnerLocationForOwnerAll(Int32 ownerID)
		public List<AIMOwnerLocationInfo> GetAIMOwnerLocationForOwnerAll(Int32 ownerID, Int32 aimOwnerLocationGroupID)

		{
			List<AIMOwnerLocationInfo> info = new List<AIMOwnerLocationInfo>();
			try
			{
				//11/23/22
				//using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SEL_OwnerID_ALL"))
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SEL_ALL_OwnerID_AIMOwnerLocationID"))

				{
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, ownerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationGroupID", SqlDbType.Int, aimOwnerLocationGroupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}



		public List<AIMSupplierLocationInfo> GetAIMSupplierLocationForSupplierAllStatus(Int32 supplierID)
		{
			List<AIMSupplierLocationInfo> info = new List<AIMSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_AIMSourceID_ALL"))
				{
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, supplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//8/30/23
		public List<AIMSupplierLocationInfo> GetAIMSupplierLocationForSupplierAllStatus(Int32 supplierID, Int32 aimSupplierGroupID, Int32 aimSupplierLocationGroupID)
		{
			List<AIMSupplierLocationInfo> info = new List<AIMSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_AIMSourceID_AIMSupplierGroupID_AIMSupplierLocationGroupID_ALL"))
				{
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, supplierID);
					_database.AddInParameter(cmd, "@AIMSupplierGroupID", SqlDbType.Int, aimSupplierGroupID);
					_database.AddInParameter(cmd, "@AIMSupplierSupplierGroupID", SqlDbType.Int, aimSupplierLocationGroupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<AIMSupplierLocationInfo> GetAIMSupplierLocationForSupplier(Int32 supplierID)
		{
			List<AIMSupplierLocationInfo> info = new List<AIMSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SEL_SourceID"))
				{
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, supplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}




		public List<TabOrderInfo> GetTabOrder(Int32 formID, Int32 userID)
		{
			List<TabOrderInfo> info = new List<TabOrderInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_TabOrder_SEL_FormID"))
				{
					_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
					_database.AddInParameter(cmd, "@UserID", SqlDbType.Int, userID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<TabOrderInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//3/14/23
		public List<TabOrderInfo> GetTabOrder(Int32 formID, Int32 userID, Int32 tabOrder)
		{
			List<TabOrderInfo> info = new List<TabOrderInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_TabOrder_SEL_FormID_TabOrder"))
				{
					_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
					_database.AddInParameter(cmd, "@UserID", SqlDbType.Int, userID);
					_database.AddInParameter(cmd, "@TabOrder", SqlDbType.Int, tabOrder);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<TabOrderInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<ControlInfo> GetControl(Int32 controlID)
		{
			List<ControlInfo> info = new List<ControlInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Control_SEL_ControlID"))
				{
					_database.AddInParameter(cmd, "@ControlID", SqlDbType.Int, controlID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ControlInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//1/11/18
		public List<ControlInfo> GetControlUsingAllowUserDefault(Boolean allowUserDefault)
		{
			List<ControlInfo> info = new List<ControlInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Control_SEL_AllowUserDefault"))
				{
					_database.AddInParameter(cmd, "@AllowUserDefault", SqlDbType.Bit, allowUserDefault);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ControlInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<AlternateNameInfo> GetAlternateName(Int32 formID)
		{
			List<AlternateNameInfo> info = new List<AlternateNameInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AlternateName_SEL_FormID"))
				{
					_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AlternateNameInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//5/2/23
		public List<LabelsInfo> GetLabels_IngrOrProd(String ingrOrProd)
		{
			List<LabelsInfo> info = new List<LabelsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Labels_SEL_IngrOrProd"))
				{
					_database.AddInParameter(cmd, "@IngrOrProd", SqlDbType.NVarChar, ingrOrProd);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<LabelsInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//6/26/20
		public List<LabelsInfo> GetLabels()
		{
			List<LabelsInfo> info = new List<LabelsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Labels_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<LabelsInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//8/21/23
		public List<SampleTabControlPageInfo> GetSampleTabControlPage()
		{
			List<SampleTabControlPageInfo> info = new List<SampleTabControlPageInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTabControlPage_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTabControlPageInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}



		//8/21/23
		public List<SampleTabControlPageInfo> GetSampleTabControlPageSortOrder(Int32 SortOrder)
		{
			List<SampleTabControlPageInfo> info = new List<SampleTabControlPageInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTabControlPage_SEL_SortOrder"))
				{
					_database.AddInParameter(cmd, "@SortOrder", SqlDbType.Int, SortOrder);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTabControlPageInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//8/23/23	
		public SampleTabControlPageInfo GetSampleTabControlPageName(String pageName)
		{
			SampleTabControlPageInfo result = new SampleTabControlPageInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTabControlPage_SEL_SampleTabControlPageName"))
				{
					_database.AddInParameter(cmd, "@SampleTabControlPageName", SqlDbType.NVarChar, pageName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<SampleTabControlPageInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		//8/21/23
		public void SaveSampleTabControlPage(Int32 SampleTabControlPageID, Int32 SortOrder, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTabControlPage_SAV_SampleTabControlPageID_SortOrder"))
				{
					_database.AddInParameter(cmd, "@SampleTabControlPageID", SqlDbType.Int, SampleTabControlPageID);
					_database.AddInParameter(cmd, "@SortOrder", SqlDbType.Int, SortOrder);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				//result.ErrorNumber = -1;
			}
			return;
		}

		//6/19/20
		public List<AlternateNameInfo> GetAlternateName(Int32 formID, String controlName)
		{
			List<AlternateNameInfo> info = new List<AlternateNameInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AlternateName_SEL_FormID_ControlName"))
				{
					_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
					_database.AddInParameter(cmd, "@ControlName", SqlDbType.VarChar, controlName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AlternateNameInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//3/14/23
		public List<AlternateNameInfo> GetAlternateName(Int32 formID, Int32 controlID)
		{
			List<AlternateNameInfo> info = new List<AlternateNameInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AlternateName_SEL_FormID_ControlID"))
				{
					_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
					_database.AddInParameter(cmd, "@ControlID", SqlDbType.Int, controlID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AlternateNameInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//6/26/20 - commented, label fields moved from AlternateName table to Labels table
		//public List<AlternateNameInfo> GetAlternateNameLabelFieldFirst(Int32 formID)
		//{
		//	List<AlternateNameInfo> info = new List<AlternateNameInfo>();
		//	try
		//	{
		//		using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AlternateName_SEL_LabelFieldFirst"))
		//		{
		//			_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
		//			using (IDataReader reader = _database.ExecuteReader(cmd))
		//			{
		//				ObjectFiller<AlternateNameInfo>.GetGenericCollection(ref info, reader);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//	}
		//	return info;
		//}


		//6/26/20 - commented, label fields moved from AlternateName table to Labels table
		//public List<AlternateNameInfo> GetAlternateNameLabelFieldLast(Int32 formID)
		//{
		//	List<AlternateNameInfo> info = new List<AlternateNameInfo>();
		//	try
		//	{
		//		using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AlternateName_SEL_LabelFieldLast"))
		//		{
		//			_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
		//			using (IDataReader reader = _database.ExecuteReader(cmd))
		//			{
		//				ObjectFiller<AlternateNameInfo>.GetGenericCollection(ref info, reader);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//	}
		//	return info;
		//}

		//6/2/20
		public List<LabelsInfo> GetLabelFieldFirst()
		{
			List<LabelsInfo> info = new List<LabelsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Labels_SEL_LabelFieldFirst"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<LabelsInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//6/26/20
		public List<LabelsInfo> GetLabelFieldLast()
		{
			List<LabelsInfo> info = new List<LabelsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Labels_SEL_LabelFieldLast"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<LabelsInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//6/23/21
		public List<LabelsInfo> GetLabelFieldLine2()
		{
			List<LabelsInfo> info = new List<LabelsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Labels_SEL_LabelFieldLine2"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<LabelsInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//6/23/21
		public List<LabelsInfo> GetLabelFieldLine3()
		{
			List<LabelsInfo> info = new List<LabelsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Labels_SEL_LabelFieldLine3"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<LabelsInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//6/23/21
		public List<LabelsInfo> GetLabelFieldLine4()
		{
			List<LabelsInfo> info = new List<LabelsInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Labels_SEL_LabelFieldLine4"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<LabelsInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}


		//12/7/18 
		////public List<AlternateNameInfo> GetUseAsSampleCode(Int32 templateID)
		////{
		////    List<AlternateNameInfo> info = new List<AlternateNameInfo>();
		////    try
		////    {
		////        using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AlternateName_SEL_LabelFieldFirst"))
		////        {
		////            _database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
		////            using (IDataReader reader = _database.ExecuteReader(cmd))
		////            {
		////                ObjectFiller<AlternateNameInfo>.GetGenericCollection(ref info, reader);
		////            }
		////        }
		////    }
		////    catch (Exception ex)
		////    {
		////        HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		////    }
		////    return info;
		////}


		public List<AlternateNameInfo> GetAlternateNameFilter(Int32 formID)
		{
			List<AlternateNameInfo> info = new List<AlternateNameInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AlternateName_FilterControl_SEL"))
				{
					_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AlternateNameInfo>.GetGenericCollection(ref info, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public APIPendingAIMMaterialInfo GetAPIPendingAIMMaterial(Int32 aimMaterialID, Int32 dataSourceTypeID, Boolean isActive)
		{
			APIPendingAIMMaterialInfo result = new APIPendingAIMMaterialInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_APIPendingAIMMaterial_SEL_AIMMaterialIDDataSourceTypeIDIsActive"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<APIPendingAIMMaterialInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo GetSampleResultMessageCount(Int32 type, Int32 sampleID, DateTime startDate, DateTime endDate)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_SEL_Messages"))
				{
					_database.AddInParameter(cmd, "@Type", SqlDbType.Int, type);
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@StartDate", SqlDbType.Int, startDate);
					_database.AddInParameter(cmd, "@EndDate", SqlDbType.Int, endDate);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name); result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo GetImportHeaderStatusCount()
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportHeader_CheckStatus"))
				{
					//cmd.Parameters.AddRange(importBatch.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//1/25/24
		//public ErrorInfo GetIngredientDetailStatusCount(ref AnalysisExportBatchMaintenance exportbatch)
		//{
		//	ErrorInfo result = new ErrorInfo();
		//	try
		//	{
		//		using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientDetail_CheckStatus"))
		//		{
		//			cmd.Parameters.AddRange(exportbatch.SerializeAsSqlParameter());
		//			using (IDataReader reader = _database.ExecuteReader(cmd))
		//			{
		//				result = ObjectFiller<ErrorInfo>.GetScalar(reader);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//		result.ErrorNumber = -1;
		//	}
		//	return result;
		//}
		public ErrorInfo GetIngredientDetailStatusCount(Int32 updateIngredientHeaderID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientDetail_CheckStatus"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientHeaderID", SqlDbType.Int, updateIngredientHeaderID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo GetSampleImportStatusCount(Int32 SampleImportLogID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleImportLog_CheckStatus"))
				{
					_database.AddInParameter(cmd, "@SampleImportLogID", SqlDbType.Int, SampleImportLogID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public List<SampleExportTemplateInfo> GetSampleExportTemplate()
		{
			List<SampleExportTemplateInfo> result = new List<SampleExportTemplateInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleExportTemplate_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleExportTemplateInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMUpdateLogInfo> GetAimUpdateLog(Int32 batchID)
		{
			List<AIMUpdateLogInfo> result = new List<AIMUpdateLogInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMUpdateLog_SEL_BatchID"))
				{
					_database.AddInParameter(cmd, "@BatchID", SqlDbType.Int, batchID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMUpdateLogInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<GuaranteeRefundHeaderInfo> GetGuaranteeHeaderLastBatchID()
		{
			List<GuaranteeRefundHeaderInfo> result = new List<GuaranteeRefundHeaderInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GuaranteeRefundHeader_SEL_LastBatchID"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<GuaranteeRefundHeaderInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMUpdateLogInfo> GetAimUpdateLogLastBatchID()
		{
			List<AIMUpdateLogInfo> result = new List<AIMUpdateLogInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMUpdateLog_SEL_LastBatchID"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMUpdateLogInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleResultTemplateInfo> GetSampleResultTemplate()
		{
			List<SampleResultTemplateInfo> result = new List<SampleResultTemplateInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultTemplate_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultTemplateInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/4/18
		public List<SampleQualityControlMassEntryInfo> GetSampleControlMassEntry()
		{
			List<SampleQualityControlMassEntryInfo> result = new List<SampleQualityControlMassEntryInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControlMassEntry_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityControlMassEntryInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleResultTemplateInfo> GetSampleResultTemplate(String name)
		{
			List<SampleResultTemplateInfo> result = new List<SampleResultTemplateInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultTemplate_SEL_SampleResultTemplateName"))
				{
					_database.AddInParameter(cmd, "@SampleResultTemplateName", SqlDbType.VarChar, name);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultTemplateInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/5/18
		public List<SampleQualityControlMassEntryInfo> GetSampleQualityControlMassEntryName(String name)
		{
			List<SampleQualityControlMassEntryInfo> result = new List<SampleQualityControlMassEntryInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControlMassEntry_SEL_Name"))
				{
					_database.AddInParameter(cmd, "@SampleQualityControlMassEntryName", SqlDbType.VarChar, name);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityControlMassEntryInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<SampleResultTemplateInfo> GetSampleResultTemplateAIMMaterialID(Int32 aimMaterialID)
		{
			List<SampleResultTemplateInfo> result = new List<SampleResultTemplateInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultTemplate_SEL_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultTemplateInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<SampleResultTemplateInfo> GetSampleResultTemplateTemplateID(Int32 templateID)
		{
			List<SampleResultTemplateInfo> result = new List<SampleResultTemplateInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultTemplate_SEL_SampleResultTemplateID"))
				{
					_database.AddInParameter(cmd, "@SampleResultTemplateID", SqlDbType.Int, templateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultTemplateInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}





		//11/23/22
		public List<SampleResultTemplateDetailInfo> GetSampleResultTemplateDetailTemplateID(Int32 templateID, Int32 sampleID, Int32 datasourceTypeID)
		{
			List<SampleResultTemplateDetailInfo> result = new List<SampleResultTemplateDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultTemplateDetail_SEL_SampleResultTemplateID_SampleID_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@SampleResultTemplateID", SqlDbType.Int, templateID);
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@DatasourceTypeID", SqlDbType.Int, datasourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleResultTemplateDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		//11/28/22		
		public List<CFCUserInfo> UpdateCFCUsersSpecificIngredientsGroup(Int32 specificIngredientsGroupID, Int32 value)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_CFCUser_UPD_SpecificIngredientsGroupID"))
				{
					_database.AddInParameter(cmd, "@SpecificIngredientsGroupID", SqlDbType.Int, specificIngredientsGroupID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//5/18/23
		public List<CFCUserInfo> UpdateCFCUsersSpecificProductsGroup(Int32 specificProductsGroupID, Int32 value)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_CFCUser_UPD_SpecificProductsGroupID"))
				{
					_database.AddInParameter(cmd, "@SpecificProductsGroupID", SqlDbType.Int, specificProductsGroupID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//11/28/22		
		public List<CFCUserInfo> UpdateCFCUsersAIMOwnerLocationGroup(Int32 aimOwnerLocationGroupID, Int32 value)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_CFCUser_UPD_AIMOwnerLocationGroupID"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationGroupID", SqlDbType.Int, aimOwnerLocationGroupID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//11/28/22
		public void DeleteOwnerLocationGroupDetail_GroupID(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationGroupDetail_DEL_AIMOwnerLocationGroupID"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//11/28/22
		public List<AIMOwnerLocationGroupDetailInfo> GetOwnerLocationGroupDetail_AIMOwnerLocationID(Int32 aimOwnerLocationID)
		{
			List<AIMOwnerLocationGroupDetailInfo> result = new List<AIMOwnerLocationGroupDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationGroupDetail_SEL_AIMOwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationGroupDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//11/28/22  
		public ErrorInfo UpdateOwnerLocationGroupDetailIsActive(Int32 aimOwnerLocationID, Boolean isActive, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationGroupDetail_UPD_IsActive"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//1/5/18
		public List<SampleQualityControlMassEntryDetailInfo> GetSampleQualityControlMassEntry(Int32 massEntryID, Int32 sampleID, Int32 datasourceTypeID)
		{
			List<SampleQualityControlMassEntryDetailInfo> result = new List<SampleQualityControlMassEntryDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControlMassEntryDetail_SEL_SampleQualityControlMassEntryID_SampleID_DataSourceTypeID"))

				{
					_database.AddInParameter(cmd, "@SampleQualityControlMassEntryID", SqlDbType.Int, massEntryID);
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityControlMassEntryDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCUserInfo> GetCFCUserDataMap(Int32 CFCUserID)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCUser_SEL_LastActiveDataMap"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCUserInfo> GetCFCUsers(Boolean IsActive)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_CFCUser_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//9/7/23
		public List<CFCUserInfo> GetCFCUsersAllButAimSystem(Boolean IsActive)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_CFCUser_SEL_IsActive_All_But_AIMSystem"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<CFCUserInfo> GetCFCUser(Int32 cfcUserID)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_CFCUser_SEL_CFCUserID"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/25/18
		public List<CFCUserInfo> GetCFCUsersLoggedInStatus(Boolean loggedInStatus)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_CFCUser_SEL_LoggedInStatus"))
				{
					_database.AddInParameter(cmd, "@LoggedInStatus", SqlDbType.Bit, loggedInStatus);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/27/18        
		public List<CFCUserInfo> UpdateCFCUsersLoggedInStatus(Int32 cfcUserID, Boolean loggedInStatus)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_CFCUser_UPD_CFCUserID_LoggedInStatus"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.AddInParameter(cmd, "@LoggedInStatus", SqlDbType.Bit, loggedInStatus);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/25/18
		public List<CFCUserInfo> SaveCFCUsersLoggedInStatus(Int32 cfcUserID, Boolean loggedInStatus)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_CFCUser_SAV_CFCUserID_LoggedInStatus"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.AddInParameter(cmd, "@LoggedInStatus", SqlDbType.Bit, loggedInStatus);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/28/20
		public List<AlternateNameInfo> GetAlternateImportFieldName(Int32 defID, String mapColumnName)
		{
			List<AlternateNameInfo> result = new List<AlternateNameInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AlternateName_SEL_DefID_MapColumnName"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, defID);
					_database.AddInParameter(cmd, "@MapColumnName", SqlDbType.VarChar, mapColumnName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AlternateNameInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//12/22/20
		public MapColumnNamesInfo GetMapColumnName(Int32 mapID)
		{
			MapColumnNamesInfo info = new MapColumnNamesInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_MapColumnNames_SEL_MapColumnID"))
				{
					_database.AddInParameter(cmd, "@MapColumnID", SqlDbType.Int, mapID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<MapColumnNamesInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//1/28/20
		public MapColumnNamesInfo GetOriginalMapColumnName(Int32 defID, String altName)
		{
			MapColumnNamesInfo info = new MapColumnNamesInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_MapColumnNames_SEL_DefID_AlternateName"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, defID);
					_database.AddInParameter(cmd, "@AlternateName", SqlDbType.VarChar, altName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<MapColumnNamesInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		#endregion

		#region Insert

		//01/18/23
		public ErrorInfo InsertAPISprectrumLog(Int32 sampleID, Int32 apiSpectrumID, Int32 apiNIRType)
		{
			ErrorInfo result = new ErrorInfo();

			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_APISprectrumLog_INS"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@APISpectrumID", SqlDbType.Int, apiSpectrumID);
					_database.AddInParameter(cmd, "@APINIRType", SqlDbType.Int, apiNIRType);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);

			}
			return result;
		}

		public void InsertSystemLog(Int32 typeID, String message, Int32 lineNumber, String methodName, Int32 CFCUserID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SystemLog_INS"))
				{
					_database.AddInParameter(cmd, "@TypeID", SqlDbType.Int, typeID);
					_database.AddInParameter(cmd, "@Message", SqlDbType.VarChar, message);
					_database.AddInParameter(cmd, "@SourceLineNumber", SqlDbType.Int, lineNumber);
					_database.AddInParameter(cmd, "@MethodName", SqlDbType.VarChar, methodName);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public ErrorInfo InsertAIMMessage(Int32 typeID, String message, Int32 CFCUserID)
		{
			ErrorInfo result = new ErrorInfo();

			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMessage_INS"))
				{
					_database.AddInParameter(cmd, "@TypeID", SqlDbType.Int, typeID);
					_database.AddInParameter(cmd, "@Message", SqlDbType.VarChar, message);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}

			return result;
		}

		public void InsertTabOrder(Int32 formID, Int32 userID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_TabOrder_INS"))
				{
					_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
					_database.AddInParameter(cmd, "@UserID", SqlDbType.Int, userID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void InsertAlternateName(Int32 formID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AlternateName_INS"))
				{
					_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void InsertCFCPlantRefAIMOwnerLocation(ref CFCPlantRefAIMOwnerLocationInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlantRefAIMOwnerLocation_INS"))
				{
					_database.AddInParameter(cmd, "@CFCPlantID", SqlDbType.Int, refInfo.CFCPlantID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, refInfo.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, refInfo.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		//10/20/20 - commented. AnalyzerMaterialRefAnalyse table does not exist
		//public void InsertAnalyzerMaterialRefAnalyse(Int32 AnalyzerMaterialID, DataSourceTypeEnum dataSourceTypeID, String analyzerAnalyseCode)
		//{
		//	try
		//	{
		//		using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterialRefAnalyzerAnalyse_INS"))
		//		{
		//			_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, AnalyzerMaterialID);
		//			_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, (Int32)dataSourceTypeID);
		//			_database.AddInParameter(cmd, "@AnalyzerAnalyseCode", SqlDbType.NVarChar, analyzerAnalyseCode);
		//			_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
		//			_database.ExecuteNonQuery(cmd);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//	}
		//}

		public void InsertCFCPlant(ref CFCPlantInfo plant)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlant_INS"))
				{
					_database.AddInParameter(cmd, "@CFCPlantCode", SqlDbType.VarChar, plant.CFCPlantCode);
					_database.AddInParameter(cmd, "@CFCPlantName", SqlDbType.VarChar, plant.CFCPlantName);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, plant.CFCUserID);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, plant.AimDatabaseID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		#endregion

		#region Delete
		public Int32 DeleteAnalysisViewTemplate(ref AnalysisViewTemplateInfo viewTemplate)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisViewTemplate_DEL"))
				{
					_database.AddInParameter(cmd, "@AnalysisViewTemplateID", SqlDbType.Int, viewTemplate.AnalysisViewTemplateID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}


		public Int32 DeleteSampleQualityControl(ref SampleQualityControlInfo sampleQuality)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControl_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleQualityControlID", SqlDbType.Int, sampleQuality.SampleQualityControlID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		public Int32 DeleteSampleAIMRefCFCUser(Int32 sampleAIMRefCFCUserID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleAIMRefCFCUser_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleAIMRefCFCUserID", SqlDbType.Int, sampleAIMRefCFCUserID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		//1/11/18
		public Int32 DeleteUserDefaults(Int32 formID, Int32 cfcUserID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefaults_DEL"))
				{
					_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		public Int32 DeleteSampleExportTemplate(Int32 ID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleExportTemplate_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleExportTemplateID", SqlDbType.Int, ID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		public Int32 DeleteSampleResultTemplate(Int32 ID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultTemplate_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleResultTemplateID", SqlDbType.Int, ID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		//1/5/18        
		public Int32 DeleteSampleQualityControlMassEntry(Int32 ID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControlMassEntry_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleQualityControlMassEntryID", SqlDbType.Int, ID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		public void DeleteSystemLog()
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SystemLog_DEL"))
				{
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteSampleResult(ref SampleResultInfo sr)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleResultID", SqlDbType.Int, sr.SampleResultID);
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sr.SampleID);
					_database.AddInParameter(cmd, "@AnalyzerNutrientID", SqlDbType.Int, sr.AnalyzerNutrientID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, sr.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, sr.IsActive);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//1/10/18
		public void DeleteSampleQualityResult(ref SampleQualityInfo sq)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQuality_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleQualityID", SqlDbType.Int, sq.SampleQualityID);
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sq.SampleID);
					_database.AddInParameter(cmd, "@SampleQualityParameterID", SqlDbType.Int, sq.SampleQualityParameterID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, sq.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, sq.IsActive);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//4/17/18 - added cfcUserID
		public void DeleteCFCProductRefAIMMaterial(Int32 CFCProductRefAimMaterialID, Int32 cfcUserID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductRefAIMMaterial_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, CFCProductRefAimMaterialID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public void DeleteQueryField(Int32 queryFieldID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryField_DEL"))
				{
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public void DeleteQueryOperation(Int32 queryOperationID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryOperation_DEL"))
				{
					_database.AddInParameter(cmd, "@QueryOperationID", SqlDbType.Int, queryOperationID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public void DeleteQueryTemplate(Int32 queryTemplateID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplate_DEL"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}


		public void DeleteGuaranteeBatch(Int32 batchID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GuaranteeRefundHeader_DEL_BatchID"))
				{
					_database.AddInParameter(cmd, "@BatchID", SqlDbType.Int, batchID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}


		public void DeleteQueryTemplateDetail(Int32 queryTemplateDetailID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_DEL"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateDetailID", SqlDbType.Int, queryTemplateDetailID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}


		public void DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID(Int32 queryTemplateID, Int32 queryFieldID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_DEL_QueryTemplateID_QueryFieldID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteSample(Int32 SampleID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, SampleID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteSampleAIMRefCFCUser(Int32 value, SampleAIMRefCFCUserEnum sampleAIMRefCFCUser)
		{
			Int32 cfcUserID = 0;
			DeleteSampleAIMRefCFCUser(value, sampleAIMRefCFCUser, cfcUserID);
		}

		public void DeleteSampleAIMRefCFCUser(Int32 value, SampleAIMRefCFCUserEnum sampleAIMRefCFCUser, Int32 cfcUserID)
		{

			String query = String.Empty;
			String parameter = String.Empty;
			String parameter2 = String.Empty;

			switch (sampleAIMRefCFCUser)
			{
				case SampleAIMRefCFCUserEnum.SampleAIMRefCFCUserID:
					query = "dbo.usp_SampleAIMRefCFCUser_DEL_SampleAIMRefCFCUserID";
					parameter = "@SampleAIMRefCFCUserID";
					break;
				case SampleAIMRefCFCUserEnum.SampleAIMID_CFCUserID:
					query = "dbo.usp_SampleAIMRefCFCUser_DEL_SampleAIMIDCFCUserID";
					parameter = "@SampleAIMID";
					parameter2 = "@CFCUserID";
					break;
				case SampleAIMRefCFCUserEnum.CFCUserID:
					query = "dbo.usp_SampleAIMRefCFCUser_DEL_CFCUserID";
					parameter = "@CFCUserID";
					break;
				default:
					break;
			}
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand(query))
				{
					_database.AddInParameter(cmd, parameter, SqlDbType.Int, value);
					if (parameter2 != String.Empty && sampleAIMRefCFCUser == SampleAIMRefCFCUserEnum.SampleAIMID_CFCUserID)
					{
						_database.AddInParameter(cmd, parameter2, SqlDbType.Int, cfcUserID);
					}
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//1/4/18        
		public void DeleteSampleQualityControlMassEntryDetail(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControlMassEntryDetail_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@SampleQualityControlMassEntryDetailID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteSampleResultTemplateDetail(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultTemplateDetail_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@SampleResultTemplateDetailID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//4/17/18 - added cfcUserID
		public void DeleteCFCIngredientRefAIMMaterial(Int32 cfcIngredientRefAIMMaterialID, Int32 cfcUserID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCIngredientRefAIMMaterial_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, cfcIngredientRefAIMMaterialID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteWarningLimits(Int32 warningLimitsID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_WarningLimits_DEL"))
				{
					_database.AddInParameter(cmd, "@WarningLimitsID", SqlDbType.Int, warningLimitsID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//9/23/22
		public void DeleteSpecificIngredients(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredients_DEL"))
				{
					_database.AddInParameter(cmd, "@SpecificIngredientsID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//5/18/23
		public void DeleteSpecificProducts(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificProducts_DEL"))
				{
					_database.AddInParameter(cmd, "@SpecificProductsID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}


		//9/23/22
		public void DeleteSpecificIngredients_GroupID(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredients_DEL_SpecificIngredientsGroupID"))
				{
					_database.AddInParameter(cmd, "@SpecificIngredientsGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//5/18/23
		public void DeleteSpecificProducts_GroupID(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificProducts_DEL_SpecificProductsGroupID"))
				{
					_database.AddInParameter(cmd, "@SpecificProductsGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//10/19/22
		public void DeleteSpecificIngredientsGroup(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredientsGroup_DEL"))
				{
					_database.AddInParameter(cmd, "@SpecificIngredientsGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//5/18/23
		public void DeleteSpecificProductsGroup(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificProductsGroup_DEL"))
				{
					_database.AddInParameter(cmd, "@SpecificProductsGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//1/2/18
		public void DeleteSampleQualityParameter(Int32 sampleQualityParameterID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityParameter_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleQualityParameterID", SqlDbType.Int, sampleQualityParameterID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteMathType(Int32 mathTypeID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_MathType_DEL"))
				{
					_database.AddInParameter(cmd, "@MathTypeID", SqlDbType.Int, mathTypeID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//1/3/18
		public void DeleteSampleQualityEquation(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityEquation_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleQualityEquationID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//1/4/18
		public void DeleteSampleQualityEquationInactive()
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityEquation_DEL_Inactive"))
				{
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//1/4/18
		public void DeleteSampleQualityEquationBuild(Int32 equationID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityEquationBuild_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleQualityEquationID", SqlDbType.Int, equationID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//1/4/18
		public void DeleteSampleQualityEquationBuild_Inactive(Int32 equationID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityEquationBuild_DEL_EquationID_Inactive"))
				{
					_database.AddInParameter(cmd, "@SampleQualityEquationID", SqlDbType.Int, equationID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}


		public void DeleteImportHeaderDetailStatus(Int32 statusMap)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportHeaderDetail_DEL_StatusMap"))
				{
					_database.AddInParameter(cmd, "@StatusMap", SqlDbType.Int, statusMap);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteEmailAddressXRefMessage(Int32 emailAddressXRefMessageID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EmailAddressXRefMessage_DEL"))
				{
					_database.AddInParameter(cmd, "@EmailAddressXRefMessageID", SqlDbType.Int, emailAddressXRefMessageID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}


		public void DeleteDataSourceUserTemplate(Int32 dataDefID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_DEL"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceTemplateID", SqlDbType.Int, dataDefID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteDataSourceUserDefinitionAndTemplate(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceDefinition_DEL"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteDataSourceUserTemplateUsingDefinitionID(Int32 defID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_DEL_DefinitionID"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, defID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteDataSourceUserTemplateZeroSourceColumnNumber(Int32 defID, Int32 sourceColumnNumber)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_DEL_DefinitionID_SourceColumnNumber"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, defID);
					_database.AddInParameter(cmd, "@SourceColumnNumber", SqlDbType.Int, sourceColumnNumber);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}


		public ErrorInfo DataSourceUserTemplateCountSourceColumnNumberDuplicates(Int32 defID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_SEL_DataSourceDefinitionID_DuplicateSourceColumnNumbers"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, defID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo DataSourceUserTemplateCheckSourceColumnName(Int32 defID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_SEL_DataSourceDefinitionID_BlankSourceColumnName"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, defID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public void DeleteDataSourceUser(Int32 dataSourceID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceType_DEL"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//1/22/21
		public void DeleteSampleType(Int32 sampleTypeID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleType_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleTypeID", SqlDbType.Int, sampleTypeID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteDataSourceTypeRefFileType(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceTypeRefFileType_DEL"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeRefFileTypeID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}


		public void DeleteDataSourceTypeRefFileTypeUsingDataSourceTypeID(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceTypeRefFileType_DEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteEmailAddress(Int32 emailAddressID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EmailAddress_DEL"))
				{
					_database.AddInParameter(cmd, "@EmailAddressID", SqlDbType.Int, emailAddressID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteEvent(Int32 eventID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Event_DEL"))
				{
					_database.AddInParameter(cmd, "@EventID", SqlDbType.Int, eventID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeletePrinterDefault(Int32 printerDefaultID)
		{
			try
			{
				//6/15/18
				//using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterType_DEL"))
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterDefault_DEL"))

				{
					_database.AddInParameter(cmd, "@PrinterDefaultID", SqlDbType.Int, printerDefaultID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//6/15/18
		public void DeletePrinterUserDefault(Int32 printerUserDefaultID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterUserDefault_DEL"))
				{
					_database.AddInParameter(cmd, "@PrinterUserDefaultID", SqlDbType.Int, printerUserDefaultID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteWebService(Int32 webServiceID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_WebService_DEL"))
				{
					_database.AddInParameter(cmd, "@WebServiceID", SqlDbType.Int, webServiceID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//4/17/18 - added cfcUserID
		public void DeleteCFCNutrientRefAIMNutrient(Int32 CFCNutrientRefAnalyzerNutrientID, Int32 cfcUserID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrientRefAIMNutrient_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, CFCNutrientRefAnalyzerNutrientID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//4/17/18 - added cfcUserID		
		public void DeleteAIMNutrientRefAnalyzerNutrient(Int32 AIMNutrientRefAnalyzerNutrientID, Int32 cfcUserID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrientRefAnalyzerNutrient_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, AIMNutrientRefAnalyzerNutrientID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//4/17/18 - added cfcUserID
		public void DeleteAIMSupplierRefAnalyzerSupplier(Int32 AIMSupplierRefAnalyzerSupplierID, Int32 cfcUserID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierRefAnalyzerSupplier_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, AIMSupplierRefAnalyzerSupplierID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//4/17/18 - added cfcUserID
		public void DeleteAIMSupplierLocationRefAnalyzerSupplierLocation(Int32 AIMSupplierLocationRefAnalyzerSupplierLocationID, Int32 cfcUserID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationRefAnalyzerSupplierLocation_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, AIMSupplierLocationRefAnalyzerSupplierLocationID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteSampleAim(Int32 aimNutrientID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleAIM_DEL_AimNutrientID"))
				{
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}


		public void DeleteDataSourceDetail(Int32 DataSourceDetailID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceDetail_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, DataSourceDetailID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}


		public void DeleteDataSourceHeader(Int32 DataSourceHeaderID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceHeader_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, DataSourceHeaderID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//4/17/18
		//public void DeleteAIMMaterialRefAnalyzerMaterial(Int32 AIMMaterialRefAnalyzerMaterialID)
		public void DeleteAIMMaterialRefAnalyzerMaterial(Int32 AIMMaterialRefAnalyzerMaterialID, Int32 cfcUserID)

		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, AIMMaterialRefAnalyzerMaterialID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteAIMMaterialStockStatus(Int32 materialID, Int32 ownerLocationID)

		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialStockStatus_DEL"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, materialID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, ownerLocationID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//10/14/22
		public void DeleteAIMNutrientDriver(Int32 queryTemplateID, Int32 aimNutrientID)

		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrientDriver_DEL"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteAIMMaterialStockStatus(Int32 ownerLocationID)

		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialStockStatus_DEL_AIMOwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, ownerLocationID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//10/20/20 - commented. AnalyzerMaterialRefAnalyse table does not exist
		//public void DeleteAnalyzerMaterialRefAnalyse(Int32 analyzerMaterialRefAnalyzerAnalyseID)
		//{
		//	try
		//	{
		//		using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterialRefAnalyzerAnalyse_DEL"))
		//		{
		//			_database.AddInParameter(cmd, "@AnalyzerMaterialRefAnalyzerAnalyseID", SqlDbType.Int, analyzerMaterialRefAnalyzerAnalyseID);
		//			_database.ExecuteNonQuery(cmd);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//	}
		//}
		public void DeleteAnalyzerAnalyse(Int32 analyzerAnalyseID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerAnalyse_DEL"))
				{
					_database.AddInParameter(cmd, "@AnalyzerAnalyseID", SqlDbType.Int, analyzerAnalyseID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//4/17/18 - added cfcUserID
		public void DeleteCFCPlantRefAIMOwnerLocation(Int32 CFCPlantRefAIMOwnerLocationID, Int32 cfcUserID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlantRefAIMOwnerLocation_DEL_CFCPlantRefAIMOwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@CFCPlantRefAIMOwnerLocationID", SqlDbType.Int, CFCPlantRefAIMOwnerLocationID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteImportConfig(Int32 importConfigID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportConfig_DEL"))
				{
					_database.AddInParameter(cmd, "@ImportConfigID", SqlDbType.Int, importConfigID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public void DeleteInstrument(Int32 instrumentID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Instrument_DEL"))
				{
					_database.AddInParameter(cmd, "@InstrumentID", SqlDbType.Int, instrumentID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		#endregion

		#region Save
		public ErrorInfo SavePNEMaterialSampleType(ref PNEMaterialSampleTypeInfo pneMaterialSampleType)
		{
			ErrorInfo result = new ErrorInfo();

			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEMaterialSampleType_SAV"))
				{
					cmd.Parameters.AddRange(pneMaterialSampleType.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);

			}
			return result;
		}
		public ErrorInfo SavePNEForAnalyzerMaterialRefAnalyzerNutrient(ref PNEForAnalyzerMaterialRefAnalyzerNutrientInfo pneForAnalyzerMaterialRefAnalyzerNutrient)
		{
			ErrorInfo result = new ErrorInfo();

			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEForAnalyzerMaterialRefAnalyzerNutrient_SAV"))
				{
					cmd.Parameters.AddRange(pneForAnalyzerMaterialRefAnalyzerNutrient.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);

			}
			return result;
		}
		public ErrorInfo SavePNEMaterialType(ref PNEMaterialTypeInfo pneMaterialType)
		{
			ErrorInfo result = new ErrorInfo();

			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNEMaterialType_SAV"))
				{
					cmd.Parameters.AddRange(pneMaterialType.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);

			}
			return result;
		}
		public ErrorInfo SavePNECredit(ref PNECreditInfo pneCredit)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PNECredit_SAV_PNECreditID"))
				{
					cmd.Parameters.AddRange(pneCredit.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveSampleLog(ref SampleLogInfo sampleLog)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleLog_UPD_SampleLogID"))
				{
					cmd.Parameters.AddRange(sampleLog.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAPIPendingAIMMaterial(ref APIPendingAIMMaterialInfo apiPendingAIMMaterial)
		{
			ErrorInfo result = new ErrorInfo();

			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_APIPendingAIMMaterial_SAV"))
				{
					cmd.Parameters.AddRange(apiPendingAIMMaterial.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//5/18/23
		public ErrorInfo SpecificIngredientsSaveAs(Int32 oldGroupID, Int32 newGroupID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredients_INS_SpecificIngredientsGroupID_NewGroupID"))
				{
					_database.AddInParameter(cmd, "@SpecificIngredientsGroupID", SqlDbType.Int, oldGroupID);
					_database.AddInParameter(cmd, "@NewGroupID", SqlDbType.Int, newGroupID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//5/18/23
		public ErrorInfo SpecificProductsSaveAs(Int32 oldGroupID, Int32 newGroupID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificProducts_INS_SpecificProductsGroupID_NewGroupID"))
				{
					_database.AddInParameter(cmd, "@SpecificProductsGroupID", SqlDbType.Int, oldGroupID);
					_database.AddInParameter(cmd, "@NewGroupID", SqlDbType.Int, newGroupID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public ErrorInfo SaveSampleByOwner(Boolean isActive, Int32 ownerID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_UPD_IsActive_OwnerID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, ownerID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public ErrorInfo SaveSampleByOwnerAndOwnerLocation(Boolean isActive, Int32 ownerID, Int32 ownerLocationID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_UPD_IsActive_OwnerID_OwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, ownerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, ownerLocationID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public ErrorInfo SaveSampleBySupplier(Boolean isActive, Int32 supplierID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_UPD_IsActive_SupplierID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, supplierID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public ErrorInfo SaveSampleBySupplierAndSupplierLocation(Boolean isActive, Int32 supplierID, Int32 supplierLocationID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_UPD_IsActive_SupplierID_SupplierLocationID"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, supplierID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, supplierLocationID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/11/18  
		public ErrorInfo SaveUserDefaults(ref UserDefaultsInfo defaults)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefaults_SAV"))
				{
					cmd.Parameters.AddRange(defaults.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		//10/20/22		
		public ErrorInfo UpdateSpecificIngredientIsActive(Int32 aimMaterialID, Boolean isActive, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredients_UPD_IsActive"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//5/18/23		
		public ErrorInfo UpdateSpecificProductIsActive(Int32 aimMaterialID, Boolean isActive, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificProducts_UPD_IsActive"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//9/23/22
		public ErrorInfo SaveSpecificIngredient(ref SpecificIngredientsInfo specific)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredients_SAV"))
				{
					_database.AddInParameter(cmd, "@SpecificIngredientsID", SqlDbType.Int, specific.SpecificIngredientsID);
					_database.AddInParameter(cmd, "@SpecificIngredientsGroupID", SqlDbType.Int, specific.SpecificIngredientsGroupID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, specific.AIMMaterialID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, specific.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, specific.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//5/18/23
		public ErrorInfo SaveSpecificProduct(ref SpecificProductsInfo specific)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificProducts_SAV"))
				{
					_database.AddInParameter(cmd, "@SpecificProductsID", SqlDbType.Int, specific.SpecificProductsID);
					_database.AddInParameter(cmd, "@SpecificProductsGroupID", SqlDbType.Int, specific.SpecificProductsGroupID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, specific.AIMMaterialID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, specific.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, specific.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//10/19/22		
		public ErrorInfo SaveSpecificIngredientGroup(ref SpecificIngredientsGroupInfo group)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificIngredientsGroup_SAV"))
				{
					_database.AddInParameter(cmd, "@SpecificIngredientsGroupID", SqlDbType.Int, group.SpecificIngredientsGroupID);
					_database.AddInParameter(cmd, "@SpecificIngredientsGroupCode", SqlDbType.NVarChar, group.SpecificIngredientsGroupCode);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, group.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, group.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//5/18/23		
		public ErrorInfo SaveSpecificProductGroup(ref SpecificProductsGroupInfo group)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SpecificProductsGroup_SAV"))
				{
					_database.AddInParameter(cmd, "@SpecificProductsGroupID", SqlDbType.Int, group.SpecificProductsGroupID);
					_database.AddInParameter(cmd, "@SpecificProductsGroupCode", SqlDbType.NVarChar, group.SpecificProductsGroupCode);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, group.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, group.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//11/22/22
		public List<AIMOwnerLocationGroupDetailInfo> GetOwnerLocationGroupDetail_GroupID(Int32 groupID, Boolean isActive)
		{
			List<AIMOwnerLocationGroupDetailInfo> result = new List<AIMOwnerLocationGroupDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationGroupDetail_SEL_GroupID_IsActive"))
				{
					_database.AddInParameter(cmd, "@GroupID", SqlDbType.Int, groupID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationGroupDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//11/28/22
		public List<AIMOwnerLocationGroupDetailInfo> GetOwnerLocationGroupDetail_GroupID(Int32 groupID)
		{
			List<AIMOwnerLocationGroupDetailInfo> result = new List<AIMOwnerLocationGroupDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationGroupDetail_SEL_GroupID"))
				{
					_database.AddInParameter(cmd, "@GroupID", SqlDbType.Int, groupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationGroupDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//11/22/22
		public List<AIMOwnerLocationGroupInfo> GetOwnerLocationGroup(Boolean isActive)
		{
			List<AIMOwnerLocationGroupInfo> result = new List<AIMOwnerLocationGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationGroup_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//11/22/22		
		public ErrorInfo SaveOwnerLocationGroup(ref AIMOwnerLocationGroupInfo group)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationGroup_SAV"))
				{
					_database.AddInParameter(cmd, "@OwnerLocationGroupID", SqlDbType.Int, group.AIMOwnerLocationGroupID);
					_database.AddInParameter(cmd, "@OwnerLocationGroupCode", SqlDbType.NVarChar, group.AIMOwnerLocationGroupCode);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, group.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, group.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//11/22/22		
		public ErrorInfo SaveOwnerLocationGroupDetail(ref AIMOwnerLocationGroupDetailInfo group)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationGroupDetail_SAV"))
				{
					_database.AddInParameter(cmd, "@OwnerLocationGroupDetailID", SqlDbType.Int, group.AIMOwnerLocationGroupDetailID);
					_database.AddInParameter(cmd, "@OwnerLocationGroupID", SqlDbType.NVarChar, group.AIMOwnerLocationGroupID);
					_database.AddInParameter(cmd, "@OwnerLocationID", SqlDbType.NVarChar, group.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, group.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, group.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//11/11/22
		public void DeleteOwnerLocationGroup(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationGroup_DEL"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//11/11/22
		public void DeleteOwnerLocationGroupDetail(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationGroupDetail_DEL"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationGroupDetailID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public ErrorInfo SaveAIMMaterial(ref AIMMaterialInfo AIMMaterial, Boolean bypassErrorMsg)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_SAV"))
				{
					cmd.Parameters.AddRange(AIMMaterial.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						if (bypassErrorMsg == false)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//6/18/20
		public ErrorInfo SaveAIMMaterialStockStatus(ref AIMMaterialStockStatusInfo materialLoc)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialStockStatus_SAV"))
				{
					cmd.Parameters.AddRange(materialLoc.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveSampleAIMRefCFCUser(ref SampleAIMRefCFCUserInfo sampleAIMRefCFCUser)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleAIMRefCFCUser_SAV"))
				{
					cmd.Parameters.AddRange(sampleAIMRefCFCUser.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveEmailAddress(ref EmailAddressInfo emailAddress)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EmailAddress_SAV"))
				{
					cmd.Parameters.AddRange(emailAddress.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveDataSourceUser(ref DataSourceTypeInfo dataSource)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceType_SAV"))
				{
					cmd.Parameters.AddRange(dataSource.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//1/22/21
		public ErrorInfo SaveSampleType(ref SampleTypeInfo sampleType)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleType_SAV"))
				{
					cmd.Parameters.AddRange(sampleType.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveDataSourceTypeRefFileType(ref DataSourceTypeRefFileTypeInfo def)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceTypeRefFileType_SAV"))
				{
					cmd.Parameters.AddRange(def.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//9/5/23
		public ErrorInfo SaveReportComments(ref ReportCommentsInfo comments)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ReportComments_SAV"))
				{
					cmd.Parameters.AddRange(comments.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveDataSourceDetail(ref DataSourceDetailInfo def)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceDetail_SAV"))
				{
					cmd.Parameters.AddRange(def.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAIMConfig()
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMConfig_SAV_CFCUserID"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
					_database.AddInParameter(cmd, "@ActiveDatabaseID", SqlDbType.Int, AIMCoreGlobal.AIMDatabaseID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, AIMCoreGlobal.AIMDatabaseIsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//5/19/23
		public ErrorInfo SaveAIMConfig(Int32 cfcUserID, Int32 activeDatabaseID, Int32 isActive)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMConfig_SAV_CFCUserID"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.AddInParameter(cmd, "@ActiveDatabaseID", SqlDbType.Int, activeDatabaseID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo DeleteAIMConfig()
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMConfig_DEL_CFCUserID"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAnalysisLog(ref AnalysisLogInfo log)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisLog_UPD_AnalysisLogID"))
				{
					_database.AddInParameter(cmd, "@AnalysisLogID", SqlDbType.Int, log.AnalysisLogID);
					_database.AddInParameter(cmd, "@Activity", SqlDbType.NVarChar, log.Activity);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, log.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, log.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		//5/21/19        
		public AnalysisViewTemplateInfo GetAnalysisViewTemplate(Int32 analysisViewTemplateID)
		{
			AnalysisViewTemplateInfo result = new AnalysisViewTemplateInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisViewTemplate_SEL_AnalysisViewTemplateID"))
				{
					_database.AddInParameter(cmd, "@AnalysisViewTemplateID", SqlDbType.Int, analysisViewTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalysisViewTemplateInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		public ErrorInfo SaveAnalysisViewTemplate(ref AnalysisViewTemplateInfo viewTemplate)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisViewTemplate_UPD_TemplateID"))
				{
					_database.AddInParameter(cmd, "@AnalysisViewTemplateID", SqlDbType.Int, viewTemplate.AnalysisViewTemplateID);
					_database.AddInParameter(cmd, "@AnalysisViewTemplateCode", SqlDbType.NVarChar, viewTemplate.AnalysisViewTemplateCode);
					_database.AddInParameter(cmd, "@AnalysisViewTemplateName", SqlDbType.NVarChar, viewTemplate.AnalysisViewTemplateName);
					_database.AddInParameter(cmd, "@AnalysisViewTemplateXML", SqlDbType.NVarChar, viewTemplate.AnalysisViewTemplateXML);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, viewTemplate.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, viewTemplate.IsActive);

					//4/4/18 -- GridNumber refers [currently] 2 different grids -- the grid for AnalysisIngredient = 1 and the grid for ReportSampleCustom = 3             
					_database.AddInParameter(cmd, "@GridNumber", SqlDbType.Int, viewTemplate.GridNumber);

					//4/12/18 TemplateType 0 = report doesn't use a gridControl or a pivotControl
					//        TemplateType 1 = report uses gridControl 
					//        TemplateType 2 = report uses pivotControl         
					_database.AddInParameter(cmd, "@TemplateType", SqlDbType.Int, viewTemplate.TemplateType);

					//5/21/19
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, viewTemplate.templateAIMMaterialID);
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, viewTemplate.templateAIMOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, viewTemplate.templateAIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, viewTemplate.templateAIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, viewTemplate.templateAIMSupplierLocationID);
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, viewTemplate.templateMaterialTypeID);
					//1175FG 7/1/2026
					_database.AddInParameter(cmd, "@PrintLayoutXML", SqlDbType.NVarChar, viewTemplate.PrintLayoutXML);   
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//6/29/22
		public ErrorInfo SaveSampleFurtherAnalysis(Int32 sampleFurtherAnalysisID, Int32 sampleID, Int32 datasourcetypeID, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleFurtherAnalysis_SAV"))
				{
					_database.AddInParameter(cmd, "@SampleFurtherAnalysisID", SqlDbType.Int, sampleFurtherAnalysisID);
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourcetypeID);
					_database.AddInParameter(cmd, "@CfcUserID", SqlDbType.Int, cfcUserID);

					cmd.CommandTimeout = 0;
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//6/29/22
		public ErrorInfo SaveSampleFurtherAnalysis(ref SampleFurtherAnalysisInfo sampleFurtherAnalysis)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleFurtherAnalysis_SAV"))
				{
					cmd.Parameters.AddRange(sampleFurtherAnalysis.SerializeAsSqlParameter());

					cmd.CommandTimeout = 0;
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveSampleAIM(Int32 analyzerNutrientID, Int32 aimNutrientID, Int32 CFCUserID, Int32 aimDatabaseID, Int32 dataSourceTypeID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleAIM_SAV_AnalyzerNutrientID_AimNutrientID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerNutrientID", SqlDbType.Int, analyzerNutrientID);
					_database.AddInParameter(cmd, "@AimNutrientID", SqlDbType.Int, aimNutrientID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					cmd.CommandTimeout = 0;
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveReportQuery(ref ReportQueryInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ReportQuery_UPD_ReportQueryID"))
				{
					_database.AddInParameter(cmd, "@ReportQueryID", SqlDbType.Int, refInfo.ReportQueryID);
					_database.AddInParameter(cmd, "@ReportID", SqlDbType.Int, refInfo.ReportID);
					_database.AddInParameter(cmd, "@ReportQueryCode", SqlDbType.NVarChar, refInfo.ReportQueryCode);
					_database.AddInParameter(cmd, "@ReportQueryName", SqlDbType.NVarChar, refInfo.ReportQueryName);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, refInfo.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, refInfo.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveQueryTemplate(ref QueryTemplateInfo queryTemplate)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplate_SAV"))
				{
					cmd.Parameters.AddRange(queryTemplate.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo CopyQueryTemplateDetail(Int32 queryTemplateID, Int32 CopyFromQueryTemplateID, Int32 CFCUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplate_COP"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@CopyFromQueryTemplateID", SqlDbType.Int, CopyFromQueryTemplateID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveQueryTemplateDetailNutrient(Int32 queryTemplateDetailNutrientID, Int32 queryTemplateDetailID, Int32 aimNutrientID, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetailNutrient_SAV"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateDetailNutrientID", SqlDbType.Int, queryTemplateDetailNutrientID);
					_database.AddInParameter(cmd, "@QueryTemplateDetailID", SqlDbType.Int, queryTemplateDetailID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//6/15/22 		
		public ErrorInfo SaveQueryTemplateDetailNutrientDataSource(Int32 queryTemplateDetailNutrientID, Int32 queryTemplateDetailID, Int32 dataSourceTypeID, Int32 aimNutrientID, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetailNutrient_SAV_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateDetailNutrientID", SqlDbType.Int, queryTemplateDetailNutrientID);
					_database.AddInParameter(cmd, "@QueryTemplateDetailID", SqlDbType.Int, queryTemplateDetailID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveQueryTemplateDetail(ref QueryTemplateDetailInfo queryTemplateDetail)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_SAV"))
				{
					cmd.Parameters.AddRange(queryTemplateDetail.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveQueryOperation(ref QueryOperationInfo queryOperation)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryOperation_SAV"))
				{
					cmd.Parameters.AddRange(queryOperation.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveQueryField(ref QueryFieldInfo queryField)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisTemplate_UPD_AnalysisTemplateID"))
				{
					cmd.Parameters.AddRange(queryField.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveAnalysisTemplate(ref AnalysisTemplateInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisTemplate_UPD_AnalysisTemplateID"))
				{
					_database.AddInParameter(cmd, "@AnalysisTemplateID", SqlDbType.Int, refInfo.AnalysisTemplateID);
					_database.AddInParameter(cmd, "@AnalysisTemplateName", SqlDbType.NChar, refInfo.AnalysisTemplateName);
					_database.AddInParameter(cmd, "@MathType", SqlDbType.Int, refInfo.MathType);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, refInfo.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, refInfo.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveReportQueryDetail(ref ReportQueryDetailInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ReportQueryDetail_UPD_ReportQueryDetailID"))
				{
					_database.AddInParameter(cmd, "@ReportQueryDetailID", SqlDbType.Int, refInfo.ReportQueryDetailID);
					_database.AddInParameter(cmd, "@ReportQueryID", SqlDbType.Int, refInfo.ReportQueryID);
					_database.AddInParameter(cmd, "@ReportQueryDetailAndOr", SqlDbType.NVarChar, refInfo.ReportQueryDetailAndOr);
					_database.AddInParameter(cmd, "@ReportQueryDetailField", SqlDbType.NVarChar, refInfo.ReportQueryDetailField);
					_database.AddInParameter(cmd, "@ReportQueryDetailOperator", SqlDbType.NVarChar, refInfo.ReportQueryDetailOperator);
					_database.AddInParameter(cmd, "@ReportQueryDetailValue", SqlDbType.NVarChar, refInfo.ReportQueryDetailValue);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, refInfo.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, refInfo.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		//4/7/18        
		public ErrorInfo GenerateAIMProductFromCFC()
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterial_INS_GenerateFromCFCProduct"))
				{
					cmd.CommandTimeout = 0;             //11/27/19 added for CSA  
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						if (result.ErrorNumber > 0)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//4/7/18        
		public ErrorInfo GenerateAIMProductXRefFromCFC()
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductRefAIMMaterial_INS_GenerateFromCFCProduct"))
				{
					cmd.CommandTimeout = 0;             //11/27/19 added for CSA 
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						if (result.ErrorNumber > 0)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//cross-ref tables
		public ErrorInfo SaveCFCPlantRefAIMOwnerLocation(ref CFCPlantRefAIMOwnerLocationInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlantRefAIMOwnerLocation_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						if (result.ErrorNumber > 0)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveCFCProductRefAIMMaterial(ref CFCProductRefAIMMaterialInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				//4/13/22 - this stp was mis-named
				//using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductRefAnalyzerMaterial_SAV"))
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProductRefAIMMaterial_SAV"))

				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveCFCIngredientRefAIMMaterial(ref CFCIngredientRefAIMMaterialInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCIngredientRefAIMMaterial_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveCFCNutrientRefAIMNutrient(ref CFCNutrientRefAIMNutrientInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrientRefAIMNutrient_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAIMNutrientRefAnalyzerNutrient(ref AIMNutrientRefAnalyzerNutrientInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrientRefAnalyzerNutrient_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//11/26/18        
		public ErrorInfo UpdateAIMSupplierRefAnalyzerSupplier(ref SampleInfo sampleToBeReplaced, ref SampleInfo sample, Boolean removeSupplierLocation)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationRefAnalyzerSupplierLocation_UPD_NewAIMSupplierID_NewAIMSupplierLocation"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, sampleToBeReplaced.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, sampleToBeReplaced.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@NewAIMSupplierID", SqlDbType.Int, sample.AIMSupplierID);
					_database.AddInParameter(cmd, "@NewAIMSupplierLocationID", SqlDbType.Int, sample.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, sample.CFCUserID);
					_database.AddInParameter(cmd, "@RemoveSupplierLocation", SqlDbType.Bit, removeSupplierLocation);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						result.ErrorNumber = -1;
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//11/29/18        
		public ErrorInfo UpdateAIMMaterialRefAnalyzerMaterial(ref SampleInfo sampleToBeReplaced, ref SampleInfo sample, Boolean removeMaterial)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_UPD_NewAIMaterialID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, sampleToBeReplaced.AnalyzerMaterialID);
					_database.AddInParameter(cmd, "@NewAnalyzerMaterialID", SqlDbType.Int, sample.AnalyzerMaterialID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, sample.CFCUserID);
					_database.AddInParameter(cmd, "@RemoveAnalyzerMaterial", SqlDbType.Bit, removeMaterial);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						result.ErrorNumber = -1;
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAIMSupplierRefAnalyzerSupplier(ref AIMSupplierRefAnalyzerSupplierInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierRefAnalyzerSupplier_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						result.ErrorNumber = -1;
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAIMSupplierLocationRefAnalyzerSupplierLocation(ref AIMSupplierLocationRefAnalyzerSupplierLocationInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationRefAnalyzerSupplierLocation_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						result.ErrorNumber = -1;
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAIMMaterialRefAnalyzerMaterial(ref AIMMaterialRefAnalyzerMaterialInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveAnalyzerSupplierToAim(Int32 analyzerSupplierID, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_INS_GenerateFromUser"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, analyzerSupplierID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						//commented - show dialog message to user instead, so they can answer yes/no
						//HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAnalyzerSupplierLocationToAim(Int32 analyzerSupplierLocationID, Int32 analyzerSupplierID, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_INS_GenerateFromAnalyzer"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationID", SqlDbType.Int, analyzerSupplierLocationID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, analyzerSupplierID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						//commented - show dialog message to user instead, so they can answer yes/no
						//HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAnalyzerOwnerLocationToAim(Int32 analyzerOwnerLocationID, Int32 analyzerOwnerID, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_INS_GenerateFromAnalyzer"))
				{
					_database.AddInParameter(cmd, "@AnalyzerOwnerLocationID", SqlDbType.Int, analyzerOwnerLocationID);
					_database.AddInParameter(cmd, "@AnalyzerOwnerID", SqlDbType.Int, analyzerOwnerID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						//commented - show dialog message to user instead, so they can answer yes/no
						//HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		//TODO - used?
		public ErrorInfo SaveUserDefinedSourceLocationToAim(Int32 userdefinedSourceLocationID, Int32 userdefinedSourceID, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_INS_GenerateFromUser"))
				{
					_database.AddInParameter(cmd, "@UserDefinedSourceLocationID", SqlDbType.Int, userdefinedSourceLocationID);
					_database.AddInParameter(cmd, "@UserDefinedSourceID", SqlDbType.Int, userdefinedSourceID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						//commented - show dialog message to user instead, so they can answer yes/no
						//HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterialList(Int32 analyzerMaterialID)
		{
			List<AIMMaterialRefAnalyzerMaterialInfo> result = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_SEL_AnalyzerMaterialID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, analyzerMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialRefAnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//7/11/23
		public List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterialListMaterialTypeID(Int32 materialTypeID)
		{
			List<AIMMaterialRefAnalyzerMaterialInfo> result = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_SEL_MaterialTypeID"))
				{
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, materialTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMMaterialRefAnalyzerMaterialInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMMaterialRefAnalyzerMaterialInfo GetAIMMaterialRefAnalyzerMaterialID(Int32 analyzerMaterialID)
		{
			AIMMaterialRefAnalyzerMaterialInfo result = new AIMMaterialRefAnalyzerMaterialInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_SEL_AnalyzerMaterialID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, analyzerMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMMaterialRefAnalyzerMaterialInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMSupplierRefAnalyzerSupplierInfo GetAIMSupplierRefAnalyzerSupplierAnalyzerID(Int32 analyzerSupplierID)
		{
			AIMSupplierRefAnalyzerSupplierInfo result = new AIMSupplierRefAnalyzerSupplierInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierRefAnalyzerSupplier_SEL_AnalyzerSupplierID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, analyzerSupplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMSupplierRefAnalyzerSupplierInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public AIMSupplierLocationRefAnalyzerSupplierLocationInfo GetAIMSupplierLocationRefAnalyzerSupplierLocationAnalyzerID(Int32 analyzerSupplierLocationID)
		{
			AIMSupplierLocationRefAnalyzerSupplierLocationInfo result = new AIMSupplierLocationRefAnalyzerSupplierLocationInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationRefAnalyzerSupplierLocation_SEL_AnalyzerSupplierLocationID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationID", SqlDbType.Int, analyzerSupplierLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public AIMNutrientRefAnalyzerNutrientInfo GetAIMNutrientRefAnalyzerNutrientAnalyzerID(Int32 analyzerNutrientID)
		{
			AIMNutrientRefAnalyzerNutrientInfo result = new AIMNutrientRefAnalyzerNutrientInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrientRefAnalyzerNutrient_SEL_AnalyzerNutrientID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerNutrientID", SqlDbType.Int, analyzerNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMNutrientRefAnalyzerNutrientInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public AIMNutrientRefAnalyzerNutrientInfo GetAIMNutrientRefAnalyzerNutrientAIMID(Int32 aimNutrientID)
		{
			AIMNutrientRefAnalyzerNutrientInfo result = new AIMNutrientRefAnalyzerNutrientInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrientRefAnalyzerNutrient_SEL_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMNutrientRefAnalyzerNutrientInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public AIMMaterialRefAnalyzerMaterialInfo GetAIMMaterialRefAnalyzerMaterialAIMID(Int32 aimMaterialID)
		{
			AIMMaterialRefAnalyzerMaterialInfo result = new AIMMaterialRefAnalyzerMaterialInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMMaterialRefAnalyzerMaterial_SEL_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMMaterialRefAnalyzerMaterialInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> GetAllAIMOwnerLocationRefAnalyzerOwnerLocation()
		{
			List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> result = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationRefAnalyzerOwnerLocation_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AIMOwnerRefAnalyzerOwnerInfo> GetAllAIMOwnerRefAnalyzerOwner()
		{
			List<AIMOwnerRefAnalyzerOwnerInfo> result = new List<AIMOwnerRefAnalyzerOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerRefAnalyzerOwner_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerRefAnalyzerOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public ErrorInfo SavePrinterDefault(ref PrinterDefaultInfo printer)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				//6/15/18
				//using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterType_SAV"))
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterDefault_SAV"))

				{
					cmd.Parameters.AddRange(printer.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//6/15/18
		public ErrorInfo SaveUserPrinterDefault(ref PrinterUserDefaultInfo printer)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_PrinterUserDefault_SAV"))
				{
					cmd.Parameters.AddRange(printer.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo CountSampleCode(String sampleCode)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_COUNT_SampleCode"))
				{
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.NVarChar, sampleCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo CountAnalyzerNutrient(Int32 AnalyzerNutrientID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_COUNT_AnalyzerNutrientID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerNutrientID", SqlDbType.Int, AnalyzerNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}

			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo CountQueryTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_COUNT_QueryTemplateID_QueryFieldID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					_database.AddInParameter(cmd, "@QueryFieldID", SqlDbType.Int, queryFieldID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}

			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo CountQueryTemplateTotal(Int32 queryTemplateID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_QueryTemplateDetail_COUNT_QueryTemplateID"))
				{
					_database.AddInParameter(cmd, "@QueryTemplateID", SqlDbType.Int, queryTemplateID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}

			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveSampleResultBySampleResultID(ref AnalysisInfo analysis)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_UPD_SampleResultID"))
				{
					_database.AddInParameter(cmd, "@SampleResultID", SqlDbType.Int, analysis.SampleResultID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, analysis.IsInclude);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, analysis.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveSampleResultForResultValue(ref SampleResultInfo sampleResult)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_UPD_ResultValue"))
				{
					_database.AddInParameter(cmd, "@SampleResultID", SqlDbType.Int, sampleResult.SampleResultID);
					_database.AddInParameter(cmd, "@ResultValue", SqlDbType.Decimal, sampleResult.ResultValue);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, sampleResult.AimDatabaseID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, sampleResult.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//1/9/18
		public ErrorInfo SaveSampleResultForResultValueQualityControl(ref SampleQualityInfo quality)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQuality_UPD_ResultValue"))
				{
					_database.AddInParameter(cmd, "@SampleQualityID", SqlDbType.Int, quality.SampleQualityID);
					_database.AddInParameter(cmd, "@ResultValueNumeric", SqlDbType.Decimal, quality.ResultValueNumeric);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, quality.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveSampleResultByOwnerLocationID(ref AnalysisSampleResult analysis)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_UPD_GroupByOwnerLocationID"))
				{
					//_database.AddInParameter(cmd, "@SampleResultID", SqlDbType.Int, analysis.SampleResultID);
					_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, analysis.AIMMaterialID);
					_database.AddInParameter(cmd, "@AnalyzerNutrientID", SqlDbType.Int, analysis.AIMNutrientID);
					_database.AddInParameter(cmd, "@OwnerLocationID", SqlDbType.Int, analysis.OwnerLocationID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, analysis.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, analysis.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveSampleResult(ref AnalysisSampleResult analysis)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleAIM_UPD"))
				{
					cmd.Parameters.AddRange(analysis.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveMathType(ref MathTypeInfo mathType)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_MathType_SAV"))
				{
					cmd.Parameters.AddRange(mathType.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//1/3/18        
		public ErrorInfo SaveSampleQualityEquation(ref SampleQualityEquationInfo equation)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityEquation_SAV"))
				{
					cmd.Parameters.AddRange(equation.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//1/4/18        
		public ErrorInfo SaveSampleQualityEquationBuild(ref SampleQualityEquationBuildInfo build)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityEquationBuild_SAV"))
				{
					cmd.Parameters.AddRange(build.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//1/4/18        
		public ErrorInfo UpdateSampleQualityEquationBuildStatus(Int32 equationID, Boolean status, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityEquationBuild_UPD_Status"))
				{
					_database.AddInParameter(cmd, "@SampleQualityEquationID", SqlDbType.Int, equationID);
					_database.AddInParameter(cmd, "@Status", SqlDbType.Bit, status);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//2/24/29
		public ErrorInfo UpdateIngredientDetailIsOverlay(Int32 headerID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientDetail_UPD_UpdateIngredientHeaderIDUpdateIsOverLay"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientHeaderID", SqlDbType.Int, headerID);					
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveSupplier_WithSampleCheck(ref AIMSupplierInfo supplier, Boolean bypassErrorMsg)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSource_SAV_AIMSourceID"))
				{
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, supplier.AIMSupplierID);
					_database.AddInParameter(cmd, "@SourceName", SqlDbType.VarChar, supplier.SupplierName);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, supplier.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, supplier.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						if (bypassErrorMsg == false)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}

					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveSupplierLocation_WithSampleCheck(ref AIMSupplierLocationInfo supplier)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SAV_AIMSourceLocationID"))
				{
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, supplier.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, supplier.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationCode", SqlDbType.NVarChar, supplier.AIMSupplierLocationCode);
					_database.AddInParameter(cmd, "@SourceLocationName", SqlDbType.VarChar, supplier.SupplierLocationName);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, supplier.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, supplier.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}

			return result;
		}

		public ErrorInfo SaveSupplierLocation(ref AIMSupplierLocationInfo supplier)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocation_SAV"))
				{
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, supplier.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, supplier.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationCode", SqlDbType.NVarChar, supplier.AIMSupplierLocationCode);
					_database.AddInParameter(cmd, "@SourceLocationName", SqlDbType.VarChar, supplier.SupplierLocationName);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, supplier.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, supplier.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}

			return result;
		}

		public void SaveSupplierLocationGuarantee(ref AIMSupplierLocationGuaranteeInfo supplier)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				//7/28/20
				//using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocationGuarantee_SAV_AIMSourceLocationGuaranteeID"))
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSourceLocationGuarantee_SAV"))

				{
					cmd.Parameters.AddRange(supplier.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
		}

		public ErrorInfo SaveAnalyzerSupplier(ref AnalyzerSupplierInfo AnalyzerSupplier)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplier_SAV_AnalyzerSupplierID"))
				{
					cmd.Parameters.AddRange(AnalyzerSupplier.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAnalyzerSupplierLocation(ref AnalyzerSupplierLocationInfo AnalyzerSupplierLocation)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SAV_AnalyzerSupplierLocationID"))
				{
					cmd.Parameters.AddRange(AnalyzerSupplierLocation.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveAnalyzerNutrient(ref AnalyzerNutrientInfo AnalyzerNutrient)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerNutrient_SAV_AnalyzerNutrientID"))
				{
					cmd.Parameters.AddRange(AnalyzerNutrient.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					//if (result.ErrorNumber > 0)
					//{
					//	HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					//}
					return result;
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveWarningLimits(ref WarningLimitsInfo warningLimits)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_WarningLimits_SAV"))
				{
					cmd.Parameters.AddRange(warningLimits.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//1/2/18
		public ErrorInfo SaveSampleQualityParameter(ref SampleQualityParameterInfo parameters)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityParameter_SAV"))
				{
					cmd.Parameters.AddRange(parameters.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveEmailAddressXRefMessage(ref EmailAddressXRefMessageInfo info)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EmailAddressXRefMessage_SAV"))
				{
					cmd.Parameters.AddRange(info.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveWebService(ref WebServiceInfo webService)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_WebService_SAV"))
				{
					cmd.Parameters.AddRange(webService.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public List<EmailAddressXRefMessageInfo> GetEmailMessageXrefMessage(Int32 CFCUserID, Int32 messageID)
		{
			List<EmailAddressXRefMessageInfo> result = new List<EmailAddressXRefMessageInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_EmailAddressXRefMessage_SEL_UserID_MessageID"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
					_database.AddInParameter(cmd, "@MessageID", SqlDbType.Int, messageID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<EmailAddressXRefMessageInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				return result;
			}
			return result;


		}

		public ErrorInfo UpdateSampleAimPriority(Int32 aimNutrientID, Int32 aimPriorityUsedValue, Int32 databaseTypeID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("usp_SampleAim_UPD_Priority"))
				{
					_database.AddInParameter(cmd, "@AimNutrientID", SqlDbType.Int, aimNutrientID);
					_database.AddInParameter(cmd, "@AimPriorityUsedValueID", SqlDbType.Int, aimPriorityUsedValue);
					_database.AddInParameter(cmd, "@DatabaseTypeID", SqlDbType.Int, databaseTypeID);
					cmd.CommandTimeout = 0;
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveIngredientDetail(ref UpdateIngredientDetail exportbatch)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("usp_UpdateIngredientDetail_SAV"))
				{
					cmd.Parameters.AddRange(exportbatch.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveIngredientDetailStatusAll(Int32 UpdateIngredientHeaderID, Int32 Status, Int32 Target)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientDetail_SAV_ALL_Status"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientHeaderID", SqlDbType.Int, UpdateIngredientHeaderID);
					_database.AddInParameter(cmd, "@Status", SqlDbType.Int, Status);
					_database.AddInParameter(cmd, "@Target", SqlDbType.Int, Target);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveIngredientDetailNumberOfSamples(Int32 UpdateIngredientDetailID, Int32 NumberOfSamples)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientDetail_UPD_NumberOfSamples"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientDetailID", SqlDbType.Int, UpdateIngredientDetailID);
					_database.AddInParameter(cmd, "@NumberOfSamples", SqlDbType.Int, NumberOfSamples);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveCFCUserDataMap(Int32 CFCUserID, Int32 lastActiveDataMapID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCUser_ActiveDataMap"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
					_database.AddInParameter(cmd, "@LastActiveDataMapID", SqlDbType.Int, lastActiveDataMapID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo UpdateSampleCode(Int32 sampleID, String sampleCode)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_UPD_SampleCode"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.Int, sampleCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo UpdateSampleDate()
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_UPD_SampleDate"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveDataSourceHeader(ref DataSourceHeaderInfo hdr)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceHeader_SAV"))
				{
					cmd.Parameters.AddRange(hdr.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public List<GuaranteeRefundHeaderInfo> GetGuaranteeRefundHeader()
		{
			List<GuaranteeRefundHeaderInfo> result = new List<GuaranteeRefundHeaderInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GuaranteeRefundHeader_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<GuaranteeRefundHeaderInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<GuaranteeRefundDetailInfo> GetGuaranteeRefundDetail(Int32 batchID, String sampleCode)
		{
			List<GuaranteeRefundDetailInfo> result = new List<GuaranteeRefundDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GuaranteeRefund_SEL_BatchID_SampleCode"))
				{
					_database.AddInParameter(cmd, "@BatchID", SqlDbType.Int, batchID);
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.NVarChar, sampleCode);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<GuaranteeRefundDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<GuaranteeRefundDetailInfo> GetGuaranteeRefund()
		{
			List<GuaranteeRefundDetailInfo> result = new List<GuaranteeRefundDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GuaranteeRefund_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<GuaranteeRefundDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<GuaranteeRefundDetailInfo> GetGuaranteeRefundDetail()
		{
			List<GuaranteeRefundDetailInfo> result = new List<GuaranteeRefundDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GuaranteeRefundDetail_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<GuaranteeRefundDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public ErrorInfo SaveGuaranteeRefundHeader(ref GuaranteeRefundHeaderInfo guaranteeHeader)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GuaranteeRefundHeader_SAV"))
				{
					cmd.Parameters.AddRange(guaranteeHeader.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveGuaranteeRefundDetail(Int32 batchID, String sampleCode, Int32 status)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GuaranteeRefundDetail_SAV_Status"))
				{
					_database.AddInParameter(cmd, "@BatchID", SqlDbType.Int, batchID);
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.NVarChar, sampleCode);
					_database.AddInParameter(cmd, "@Status", SqlDbType.Int, status);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveGuaranteeRefundDetail(Int32 detailID, Int32 status)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GuaranteeRefundDetail_SAV_GuaranteeRefundDetailID_Status"))
				{
					_database.AddInParameter(cmd, "@GuaranteeRefundDetailID", SqlDbType.Int, detailID);
					_database.AddInParameter(cmd, "@Status", SqlDbType.Int, status);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveGuaranteeRefundDetail(ref GuaranteeRefundDetailInfo guaranteeDetail)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_GuaranteeRefundDetail_SAV"))
				{
					cmd.Parameters.AddRange(guaranteeDetail.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//2/14/18
		public ErrorInfo DeleteIngredientHeaderAndDetail(Int32 headerID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientHeaderAndDetail_DEL"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientHeaderID", SqlDbType.Int, headerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveIngredientHeader(ref AnalysisExportBatchMaintenance exportbatch)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientHeader_SAV"))
				{
					cmd.Parameters.AddRange(exportbatch.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveIngredientDetailStatus(Int32 UpdateIngredientDetailID, Int32 Status, Int32 Target, Boolean BypassIsOverlay, Boolean bypassErrorMsg)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientDetail_SAV_Status"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientDetailID", SqlDbType.Int, UpdateIngredientDetailID);
					_database.AddInParameter(cmd, "@Status", SqlDbType.Int, Status);
					_database.AddInParameter(cmd, "@Target", SqlDbType.Int, Target);
					_database.AddInParameter(cmd, "@BypassIsOverlay", SqlDbType.Int, BypassIsOverlay);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						if (bypassErrorMsg == false)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//2/12/24
		//6/6/19
		//public ErrorInfo SaveIngredientDetailStatusOverride(Int32 UpdateIngredientDetailID, Int32 Status, Int32 Target, Boolean BypassIsOverlay, Boolean bypassErrorMsg, Decimal overrideAmt)
		public ErrorInfo SaveIngredientDetailStatusOverride(Int32 UpdateIngredientDetailID, Int32 Status, Decimal overrideAmt)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UpdateIngredientDetail_SAV_Status_Override"))
				{
					_database.AddInParameter(cmd, "@UpdateIngredientDetailID", SqlDbType.Int, UpdateIngredientDetailID);
					_database.AddInParameter(cmd, "@Status", SqlDbType.Int, Status);

					//2/12/24 - commented
					//_database.AddInParameter(cmd, "@Target", SqlDbType.Int, Target);
					//_database.AddInParameter(cmd, "@BypassIsOverlay", SqlDbType.Int, BypassIsOverlay);

					_database.AddInParameter(cmd, "@OverrideAmt", SqlDbType.Decimal, overrideAmt);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					//2/12/24 - commented
					//if (result.ErrorNumber > 0)
					//{
					//	if (bypassErrorMsg == false)
					//	{
					//		HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					//	}
					//}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ProgressBarInfo GetProgressBarValue(Int32 progressBarID)
		{
			ProgressBarInfo result = new ProgressBarInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProgressBar_SEL_ProgressBarID"))
				{
					_database.AddInParameter(cmd, "@ProgressBarID", SqlDbType.Int, progressBarID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ProgressBarInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//4/23/18
		public ProgressBarInfo GetProgressBarValue(String progressBarDescription, Int32 cfcUserID)
		{
			ProgressBarInfo result = new ProgressBarInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProgressBar_SEL_ProgressBarDescription_CFCUserID"))
				{
					_database.AddInParameter(cmd, "@ProgressBarDescription", SqlDbType.VarChar, progressBarDescription);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ProgressBarInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<ProgressBarInfo> GetProgressBarValueByCFCUserID(Int32 CFCUserID)
		{
			List<ProgressBarInfo> result = new List<ProgressBarInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProgressBar_SEL_CFCUserID"))
				{
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ProgressBarInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		public ErrorInfo InsertProgressBar(ref ProgressBarInfo progressBar)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProgressBar_INS"))
				{
					cmd.Parameters.AddRange(progressBar.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public void DeleteProgressBar(Int32 progressBarID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProgressBar_DEL"))
				{
					_database.AddInParameter(cmd, "@ProgressBarID", SqlDbType.Int, progressBarID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public ErrorInfo SaveAIMNutrient(ref AIMNutrientInfo aimNutrient, Boolean showErrorMessage)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrient_SAV_AIMNutrientID"))
				{
					cmd.Parameters.AddRange(aimNutrient.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (showErrorMessage == true)
					{
						if (result.ErrorNumber > 0)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//10/13/22
		public ErrorInfo SaveAIMNutrientDriver(ref AIMNutrientDriverInfo aimNutrient)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMNutrientDriver_SAV_AIMNutrientID"))
				{
					cmd.Parameters.AddRange(aimNutrient.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveCFCPlant(CFCPlantInfo cfcPlant)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCPlant_UPD_All"))
				{
					_database.AddInParameter(cmd, "@CFCPlantID", SqlDbType.Int, cfcPlant.CFCPlantID);
					_database.AddInParameter(cmd, "@CFCPlantCode", SqlDbType.VarChar, cfcPlant.CFCPlantCode);
					_database.AddInParameter(cmd, "@CFCPlantName", SqlDbType.VarChar, cfcPlant.CFCPlantName);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcPlant.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.VarChar, cfcPlant.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{

					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveCFCProduct(CFCProductInfo cfcProduct)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCProduct_SAV_CFCProductID"))
				{
					_database.AddInParameter(cmd, "@CFCProductID", SqlDbType.Int, cfcProduct.CFCProductID);
					_database.AddInParameter(cmd, "@CFCPlantID", SqlDbType.Int, cfcProduct.CFCPlantID);
					_database.AddInParameter(cmd, "@CFCProductCode", SqlDbType.VarChar, cfcProduct.CFCProductCode);
					_database.AddInParameter(cmd, "@CFCProductName", SqlDbType.VarChar, cfcProduct.CFCProductName);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcProduct.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.VarChar, cfcProduct.IsActive);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, cfcProduct.AimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveCFCIngredient(ref CFCIngredientInfo cfcIngredient)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCIngredient_SAV_CFCIngredientID"))
				{
					_database.AddInParameter(cmd, "@CFCIngredientID", SqlDbType.Int, cfcIngredient.CFCIngredientID);
					_database.AddInParameter(cmd, "@CFCIngredientCode", SqlDbType.VarChar, cfcIngredient.CFCIngredientCode);
					_database.AddInParameter(cmd, "@CFCIngredientName", SqlDbType.VarChar, cfcIngredient.CFCIngredientName);

					//CFCIngredient column is "MathType", not "MathTypeID"
					//_database.AddInParameter(cmd, "@MathTypeID", SqlDbType.Int, cfcIngredient.MathTypeID);
					_database.AddInParameter(cmd, "@MathTypeID", SqlDbType.Int, cfcIngredient.MathType);

					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, cfcIngredient.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcIngredient.CFCUserID);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, cfcIngredient.AimDatabaseID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveCFCNutrient(ref CFCNutrientInfo cfcNutrient)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_CFCNutrient_SAV_CFCNutrientID"))
				{
					_database.AddInParameter(cmd, "@CFCNutrientID", SqlDbType.Int, cfcNutrient.CFCNutrientID);
					_database.AddInParameter(cmd, "@CFCNutrientCode", SqlDbType.VarChar, cfcNutrient.CFCNutrientCode);
					_database.AddInParameter(cmd, "@CFCNutrientName", SqlDbType.VarChar, cfcNutrient.CFCNutrientName);
					_database.AddInParameter(cmd, "@MathTypeID", SqlDbType.Int, cfcNutrient.MathTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, cfcNutrient.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcNutrient.CFCUserID);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, cfcNutrient.AimDatabaseID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveImportConfig(ref ImportConfigInfo importConfig)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportConfig_SAV"))
				{
					cmd.Parameters.AddRange(importConfig.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveImportConfigConnectName(Int32 importConfigID, String connectName, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ImportConfig_SAV"))
				{
					_database.AddInParameter(cmd, "@ImportConfigID", SqlDbType.Int, importConfigID);
					_database.AddInParameter(cmd, "@ConnectName", SqlDbType.NVarChar, connectName);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo InsertAIMUpdateLog(ref AIMUpdateLogInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMUpdateLog_INS"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public AIMUpdateLogInfo AIMUpdateCheckSqlStatement(String sql)
		{
			AIMUpdateLogInfo result = new AIMUpdateLogInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMUpdateCheckSqlStatement"))
				{
					_database.AddInParameter(cmd, "@Sql", SqlDbType.NVarChar, sql);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMUpdateLogInfo>.GetScalar(reader);
					}
				}

			}
			catch (Exception ex)
			{
				_isMessageDisplay = false;
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveAnalyzerInstrument(ref AnalyzerInstrumentInfo instrument)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Instrument_SAV"))
				{
					cmd.Parameters.AddRange(instrument.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAIMOwner_WithSampleCheck(ref AIMOwnerInfo aimOwner, Boolean bypassErrorMsg)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwner_SAV_AIMOwnerID"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, aimOwner.AIMOwnerID);
					_database.AddInParameter(cmd, "@OwnerCode", SqlDbType.VarChar, aimOwner.OwnerCode);
					_database.AddInParameter(cmd, "@OwnerName", SqlDbType.VarChar, aimOwner.OwnerName);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, aimOwner.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, aimOwner.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						if (bypassErrorMsg == false)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}

			return result;
		}


		public ErrorInfo SaveAIMOwner(ref AIMOwnerInfo aimOwner, Boolean bypassErrorMsg)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwner_SAV"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, aimOwner.AIMOwnerID);
					_database.AddInParameter(cmd, "@OwnerCode", SqlDbType.VarChar, aimOwner.OwnerCode);
					_database.AddInParameter(cmd, "@OwnerName", SqlDbType.VarChar, aimOwner.OwnerName);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, aimOwner.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, aimOwner.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
					if (result.ErrorNumber > 0)
					{
						if (bypassErrorMsg == false)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}

			return result;
		}


		public ErrorInfo SampleCountAimID(Int32 ID, String Name)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_COUNT_By_AimID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, ID);
					_database.AddInParameter(cmd, "@Name", SqlDbType.VarChar, Name);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}

			return result;
		}


		public ErrorInfo SaveAIMSupplier_WithSampleCheck(ref AIMSupplierInfo aimSupplier, Boolean bypassErrorMsg)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplier_SAV_AIMSupplierID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, aimSupplier.AIMSupplierID);
					_database.AddInParameter(cmd, "@SupplierCode", SqlDbType.NVarChar, aimSupplier.AIMSupplierCode);
					_database.AddInParameter(cmd, "@SupplierName", SqlDbType.VarChar, aimSupplier.SupplierName);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, aimSupplier.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, aimSupplier.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						if (bypassErrorMsg == false)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}

			return result;
		}


		public ErrorInfo SaveAIMSupplier(ref AIMSupplierInfo aimSupplier, Boolean bypassErrorMsg)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplier_SAV"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, aimSupplier.AIMSupplierID);
					_database.AddInParameter(cmd, "@SupplierCode", SqlDbType.NVarChar, aimSupplier.AIMSupplierCode);
					_database.AddInParameter(cmd, "@SupplierName", SqlDbType.VarChar, aimSupplier.SupplierName);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, aimSupplier.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, aimSupplier.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						if (bypassErrorMsg == false)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}

			return result;
		}

		public ErrorInfo SaveUnitType(ref UnitTypeInfo units, Boolean showErrorMessage)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UnitType_SAV_UnitTypeID"))
				{
					_database.AddInParameter(cmd, "@UnitTypeID", SqlDbType.Int, units.UnitTypeID);
					_database.AddInParameter(cmd, "@UnitTypeCode", SqlDbType.VarChar, units.UnitTypeCode);
					_database.AddInParameter(cmd, "@UnitTypeDescription", SqlDbType.VarChar, units.UnitTypeDescription);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, units.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, units.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
					if (showErrorMessage == true)
					{
						if (result.ErrorNumber > 0)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo DeleteAIMOwner(Int32 aimOwnerID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwner_DEL_AIMOwnerID"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, aimOwnerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo DeleteAIMSupplier(Int32 aimSupplierID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplier_DEL_AIMSupplierID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, aimSupplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo DeleteAIMOwnerLocation(Int32 aimOwnerID, Int32 aimOwnerLocationID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_DEL_AIMOwnerID_AIMOwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, aimOwnerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo DeleteAIMSupplierLocation(Int32 aimSupplierID, Int32 aimSupplierLocationID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocation_DEL_AIMSupplierID_AIMSupplierLocationID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, aimSupplierLocationID);
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, aimSupplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveAIMOwnerLocation_WithSampleCheck(ref AIMOwnerLocationInfo aimOwnerLocation)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SAV_AIMOwnerID"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocation.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, aimOwnerLocation.AIMOwnerID);
					_database.AddInParameter(cmd, "@LocationCode", SqlDbType.VarChar, aimOwnerLocation.LocationCode);
					_database.AddInParameter(cmd, "@LocationName", SqlDbType.VarChar, aimOwnerLocation.LocationName);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, aimOwnerLocation.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, aimOwnerLocation.CFCUserID);
					_database.AddInParameter(cmd, "@IsFormulationPlant", SqlDbType.Bit, aimOwnerLocation.IsFormulationPlant);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveAIMOwnerLocation(ref AIMOwnerLocationInfo aimOwnerLocation)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_SAV"))
				{
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocation.AIMOwnerLocationID);
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, aimOwnerLocation.AIMOwnerID);
					_database.AddInParameter(cmd, "@LocationCode", SqlDbType.VarChar, aimOwnerLocation.LocationCode);
					_database.AddInParameter(cmd, "@LocationName", SqlDbType.VarChar, aimOwnerLocation.LocationName);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, aimOwnerLocation.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, aimOwnerLocation.CFCUserID);
					_database.AddInParameter(cmd, "@IsFormulationPlant", SqlDbType.Bit, aimOwnerLocation.IsFormulationPlant);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAnalyzerAnalyse(ref AnalyzerAnalyseInfo analyzer)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerAnalyse_SAV"))
				{
					cmd.Parameters.AddRange(analyzer.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//8/1/22
		public List<APILogInfo> GetAPILog(Int32 sampleID, Int32 datasourcetypeID)
		{
			List<APILogInfo> result = new List<APILogInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_APILog_SEL_SampleID_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourcetypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<APILogInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);

			}
			return result;
		}

		//8/1/22  
		public ErrorInfo InsertAPILog(Int32 sampleID, Int32 apiSampleID, Int32 datasourcetypeID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_APILog_INS_SampleID_APISampleID_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@APISampleID", SqlDbType.Int, apiSampleID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourcetypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAnalyzerPendingSample(ref AnalyzerPendingSampleInfo analyzer)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerPendingSample_SAV"))
				{
					cmd.Parameters.AddRange(analyzer.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo SaveAnalyzerMaterial(ref AnalyzerMaterialInfo analyzerMaterial, Boolean returnError)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterial_SAV"))
				{
					cmd.Parameters.AddRange(analyzerMaterial.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (returnError == true)
					{
						if (result.ErrorNumber > 0)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveDefaultSettings(ref DefaultSettingsInfo defaultSettings)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DefaultSettings_SAV"))
				{
					cmd.Parameters.AddRange(defaultSettings.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//12/16/20
		public ErrorInfo AnalysisUserDefaultSettingsSave(ref AnalysisUserDefaultSettingsInfo analysisUserDefaults)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalysisUserDefaultSettings_SAV"))
				{
					cmd.Parameters.AddRange(analysisUserDefaults.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//5/24/19
		public ErrorInfo WriteToLog(String text)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_LogTest_SAV"))
				{
					_database.AddInParameter(cmd, "@Text", SqlDbType.VarChar, text);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveSystemSettings(ref SystemSettingsInfo systemSettings)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SystemSettings_SAV"))
				{
					cmd.Parameters.AddRange(systemSettings.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//12/12/18
		public ErrorInfo SaveUserDefinedDataSourceTemplate(Int32 defID, Int32 mapColumnID, String mapColumnName, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_UPD_MapColumnID_MapColumnName"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, defID);
					_database.AddInParameter(cmd, "@MapColumnID", SqlDbType.Int, mapColumnID);
					_database.AddInParameter(cmd, "@MapColumnName", SqlDbType.NVarChar, mapColumnName);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveUserDefinedDataSourceTemplate(ref UserDefinedDataSourceTemplateInfo template)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_SAV"))
				{
					cmd.Parameters.AddRange(template.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveSampleImportLog(ref SampleImportLogInfo sample)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleImportLog_SAV"))
				{
					cmd.Parameters.AddRange(sample.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public List<DataSourceTypeInfo> GetAllDataSourceUser()
		{
			List<DataSourceTypeInfo> result = new List<DataSourceTypeInfo>();

			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceType_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//-------------
		public ErrorInfo SaveAIMDatabaseReturnID(ref AIMDatabaseInfo def)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMDatabase_SAV_ReturnID"))
				{
					_database.AddInParameter(cmd, "@AIMDatabaseID", SqlDbType.Int, def.AIMDatabaseID);
					_database.AddInParameter(cmd, "@Name", SqlDbType.VarChar, def.Name);
					_database.AddInParameter(cmd, "@CFCPath", SqlDbType.VarChar, def.CFCPath);
					_database.AddInParameter(cmd, "@DefaultForPropose", SqlDbType.Int, def.DefaultForPropose);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, def.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, def.IsActive);
					_database.AddInParameter(cmd, "@AIMPriorityUsedValueID", SqlDbType.Int, def.AIMPriorityUsedValueID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, def.DataSourceTypeID);
					_database.AddInParameter(cmd, "@SyncTime", SqlDbType.DateTime, def.SyncTime);



					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//-------------



		public ErrorInfo SaveDataSourceUserDefinition(ref UserDefinedDataSourceDefinitionInfo def)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceDefinition_SAV"))
				{
					cmd.Parameters.AddRange(def.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public Int32 DeleteAIMDatabase(ref AIMDatabaseInfo aimDatabase)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMDatabase_DEL"))
				{
					_database.AddInParameter(cmd, "@AIMDatabaseID", SqlDbType.Int, aimDatabase.AIMDatabaseID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;

		}


		public Int32 SaveAIMDatabase(ref AIMDatabaseInfo aimDatabase)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMDatabase_SAV"))
				{

					_database.AddInParameter(cmd, "@AIMDatabaseID", SqlDbType.Int, aimDatabase.AIMDatabaseID);
					_database.AddInParameter(cmd, "@DefaultForPropose", SqlDbType.Int, aimDatabase.DefaultForPropose);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, aimDatabase.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, aimDatabase.CFCUserID);
					_database.AddInParameter(cmd, "@AIMPriorityUsedValueID", SqlDbType.Int, aimDatabase.AIMPriorityUsedValueID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, aimDatabase.DataSourceTypeID);
					_database.AddInParameter(cmd, "@SyncTime", SqlDbType.DateTime, aimDatabase.SyncTime);
					if (aimDatabase.Name.Length > 0)
					{
						_database.AddInParameter(cmd, "@Name", SqlDbType.VarChar, aimDatabase.Name);
					}
					if (aimDatabase.CFCPath.Length > 0)
					{
						_database.AddInParameter(cmd, "@CFCPath", SqlDbType.VarChar, aimDatabase.CFCPath);
					}

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}


		public Int32 CopyAIMDatabaseXRef(Int32 aimDatabaseID, Int32 CopyFromAimDatabaseID, Int32 CFCUserID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMDatabase_COP"))
				{
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);
					_database.AddInParameter(cmd, "@CopyFromDatabaseID", SqlDbType.Int, CopyFromAimDatabaseID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, CFCUserID);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		//1/4/18
		public ErrorInfo SaveSampleQualityControlMassEntryDetail(ref SampleQualityControlMassEntryDetailInfo massDetail)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControlMassEntryDetail_SAV"))
				{
					cmd.Parameters.AddRange(massDetail.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveSampleResultTemplateDetail(ref SampleResultTemplateDetailInfo templateDetail)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultTemplateDetail_SAV"))
				{
					cmd.Parameters.AddRange(templateDetail.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}




		public ErrorInfo SaveSampleDetailExport(ref SampleExportDetailInfo exportDetail)
		{
			ErrorInfo result = new ErrorInfo();
			if (exportDetail.SampleDate == DateTime.MinValue)
			{
				exportDetail.SampleDate = (DateTime)SqlDateTime.MinValue;
			}



			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleExportDetail_SAV"))
				{
					cmd.Parameters.AddRange(exportDetail.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveSampleExportTemplate(ref SampleExportTemplateInfo sample)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleExportTemplate_SAV"))
				{
					cmd.Parameters.AddRange(sample.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveSampleResultTemplate(ref SampleResultTemplateInfo resultTemplate)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultTemplate_SAV"))
				{
					cmd.Parameters.AddRange(resultTemplate.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		#region TestType


		//8/17/23
		public List<SampleSubmittedSamplesReportInfo> GetSampleSubmitted(SampleSubmittedSamplesReportInfo activity)
		{
			List<SampleSubmittedSamplesReportInfo> result = new List<SampleSubmittedSamplesReportInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SampleSubmittedSamples"))
				{
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.VarChar, activity.FromDateString);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.VarChar, activity.ToDateString);
					_database.AddInParameter(cmd, "@SampleTestTypeID", SqlDbType.Int, activity.SampleTestTypeID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, activity.AIMMaterialID);	   
					_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, activity.MaterialTypeID);    //1/24/23 - uncommented
					_database.AddInParameter(cmd, "@UseSampleDate", SqlDbType.Bit, activity.UseSampleDate);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, activity.CFCUserID);
					_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, activity.AIMOwnerID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, activity.AIMOwnerLocationID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleSubmittedSamplesReportInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//9/1/23
		public List<SampleAnalysisRequestFormInfo> GetSampleAnalysisRequestForm(SampleAnalysisRequestFormInfo samples)
		{
			List<SampleAnalysisRequestFormInfo> result = new List<SampleAnalysisRequestFormInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Report_SampleAnalysisRequestForm"))
				{
					_database.AddInParameter(cmd, "@FromDate", SqlDbType.VarChar, samples.FromDateString);
					_database.AddInParameter(cmd, "@ToDate", SqlDbType.VarChar, samples.ToDateString);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, samples.DataSourceTypeID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, samples.CFCUserID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, samples.AIMMaterialID);

					//_database.AddInParameter(cmd, "@MaterialTypeID", SqlDbType.Int, samples.MaterialTypeID);	//commented 10/5/23
					
					////_database.AddInParameter(cmd, "@UseSampleDate", SqlDbType.Bit, samples.UseSampleDate);					

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleAnalysisRequestFormInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/10/23
		public List<SampleTestTypeInfo> GetSampleTestType()
		{
			List<SampleTestTypeInfo> result = new List<SampleTestTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestType_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTestTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/17/23
		public ErrorInfo SaveSampleTestType(Int32 sampleID, Int32 sampleTestTypeID, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SAV_SampleID_SampleTestTypeID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@SampleTestTypeID", SqlDbType.Int, sampleTestTypeID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		//8/16/23
		public List<SampleTestTypeInfo> GetSampleTestType(Int32 TestTypeID)
		{
			List<SampleTestTypeInfo> result = new List<SampleTestTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestType_SEL_SampleTestTypeID"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeID", SqlDbType.Int, TestTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTestTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/10/23	
		public ErrorInfo SaveSampleTestType(ref SampleTestTypeInfo lab)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestType_SAV"))
				{
					cmd.Parameters.AddRange(lab.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//8/10/23
		public List<SampleTestTypeDetailInfo> GetSampleTestTypeDetail(Int32 ID, Boolean isActive)
		{
			List<SampleTestTypeDetailInfo> result = new List<SampleTestTypeDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeDetail_SEL_SampleTestTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeID", SqlDbType.Int, ID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTestTypeDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/10/23
		public ErrorInfo SaveSampleTestTypeDetail(ref SampleTestTypeDetailInfo labDetail)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeDetail_SAV"))
				{
					cmd.Parameters.AddRange(labDetail.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//8/14/23
		public ErrorInfo SaveSampleTestTypeDetail(Int32 sampleTestTypeDetailID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeDetail_SAV"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeDetailID", SqlDbType.Int, sampleTestTypeDetailID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//8/10/23
		public Int32 DeleteSampleTestType(Int32 ID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestType_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeID", SqlDbType.Int, ID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		//8/10/23
		public List<SampleTestTypeInfo> GetSampleTestType(String name)
		{
			List<SampleTestTypeInfo> result = new List<SampleTestTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestType_SEL_Name"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeName", SqlDbType.VarChar, name);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTestTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/10/23
		public ErrorInfo GetSampleTestTypeDetailMaxSortNo(Int32 typeID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeDetail_SEL_MaxSortOrder"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeID", SqlDbType.Int, typeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/10/23
		public ErrorInfo GetSampleTestTypeDetailSortOrder(Int32 testTypeID, Int32 sortOrder)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeDetail_SEL_SampleTestTypeID_SortOrder"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeID", SqlDbType.Int, testTypeID);
					_database.AddInParameter(cmd, "@SortOrder", SqlDbType.Int, sortOrder);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/18/23
		public Int32 DeleteSampleTestTypeResult(Int32 sampleID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeResult_DEL_SampleID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		public void DeleteSampleTestTypeDetail(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeDetail_DEL_SampleTestTypeDetailID"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeDetailID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//8/11/23
		public List<SampleTestTypeResultInfo> GetTestTypeResults(Int32 sampleID, Boolean isActive)
		{
			List<SampleTestTypeResultInfo> result = new List<SampleTestTypeResultInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeResult_SEL_SampleID_IsActive"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTestTypeResultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/11/23
		public List<SampleTestTypeResultInfo> GetTestTypeResults(Int32 sampleID, Int32 dataSourceTypeID)
		{
			List<SampleTestTypeResultInfo> result = new List<SampleTestTypeResultInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeResult_SEL_SampleID_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTestTypeResultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/11/23
		public ErrorInfo SaveTestTypeResults(ref SampleTestTypeResultInfo type)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeResult_SAV"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeResultID", SqlDbType.Int, type.SampleTestTypeResultID);
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, type.SampleID);
					_database.AddInParameter(cmd, "@SampleTestTypeDetailID", SqlDbType.Int, type.SampleTestTypeDetailID);
					_database.AddInParameter(cmd, "@IsIncluded", SqlDbType.Bit, type.IsIncluded);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, type.IsActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, type.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}

			return result;
		}

		//8/14/23
		public ErrorInfo SaveTestTypeResults(Int32 SampleTestTypeResultID, Boolean isIncluded, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeResult_SAV_IsIncluded"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeResultID", SqlDbType.Int, SampleTestTypeResultID);
					_database.AddInParameter(cmd, "@IsIncluded", SqlDbType.Bit, isIncluded);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}

			return result;
		}

		//8/18/23
		public ErrorInfo SaveTestTypeResultsIsActive(Int32 SampleTestTypeResultID, Boolean isActive, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeResult_SAV_IsActive"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeResultID", SqlDbType.Int, SampleTestTypeResultID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}

			return result;
		}

		//8/18/23
		public ErrorInfo SaveTestTypeDetailIsActive(Int32 SampleTestTypeDetailID, Boolean isActive, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeDetail_SAV_IsActive"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeDetailID", SqlDbType.Int, SampleTestTypeDetailID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}

			return result;
		}

		//8/18/23
		public SampleInfo GetSampleUsingTestType(Int32 sampleTestTypeID)
		{
			SampleInfo info = new SampleInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SEL_SampleTestTypeID"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeID", SqlDbType.Int, sampleTestTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<SampleInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		#endregion

		//1/4/18
		public ErrorInfo SaveSampleQualityControlMassEntry(ref SampleQualityControlMassEntryInfo mass)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControlMassEntry_SAV"))
				{
					cmd.Parameters.AddRange(mass.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveSample(ref SampleInfo sampleInfo)
		{
			ErrorInfo result = new ErrorInfo();
			if (sampleInfo.SampleDate == DateTime.MinValue)
			{
				sampleInfo.SampleDate = (DateTime)SqlDateTime.MinValue;
			}

			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SAV"))
				{
					cmd.Parameters.AddRange(sampleInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//5/22/18
		public ErrorInfo SaveSample(Int32 sampleID, String lotNumber, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SAV_SampleID_LotNumber"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@LotNumber", SqlDbType.NVarChar, lotNumber);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		//9/27/19
		public ErrorInfo SaveSampleBillOfLoad(Int32 sampleID, String billOfLoad, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_SAV_SampleID_BillOfLoad"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@BillOfLoad", SqlDbType.NVarChar, billOfLoad);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveSample(ref SampleInfo sampleToBeRePlaced, ref SampleInfo sample)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Sample_UPD_SampleFields"))
				{
					_database.AddInParameter(cmd, "@AIMSourceID", SqlDbType.Int, sample.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationID", SqlDbType.Int, sample.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@AnalyzerMaterialID", SqlDbType.Int, sample.AnalyzerMaterialID);
					_database.AddInParameter(cmd, "@AIMSourceIDtoBeReplaced", SqlDbType.Int, sampleToBeRePlaced.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSourceLocationIDtoBeReplaced", SqlDbType.Int, sampleToBeRePlaced.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@AnalyzerMaterialIDtoBeReplaced", SqlDbType.Int, sampleToBeRePlaced.AnalyzerMaterialID);

					//11/11/22
					_database.AddInParameter(cmd, "@SampleCode", SqlDbType.NVarChar, sample.SampleCode);
					_database.AddInParameter(cmd, "@SampleCodetoBeReplaced", SqlDbType.NVarChar, sampleToBeRePlaced.SampleCode);

					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, sample.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//1/9/18 
		public ErrorInfo InsertSampleQuality(ref SampleQualityInfo quality)
		{
			ErrorInfo result = new ErrorInfo();

			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQuality_INS"))
				{
					cmd.Parameters.AddRange(quality.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//8/17/22
		//public ErrorInfo InsertSampleResult(ref SampleResultInfo sampleResult, String analyzerNutrientName)
		public ErrorInfo InsertSampleResult(ref SampleResultInfo sampleResult, String analyzerNutrientName, Int32 importType)

		{
			ErrorInfo result = new ErrorInfo();

			try
			{
				if (sampleResult.AnalysisDate == DateTime.MinValue)
				{
					sampleResult.AnalysisDate = DateTime.Now;
				}

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_INS"))
				{
					cmd.Parameters.AddRange(sampleResult.SerializeAsSqlParameter());
					_database.AddInParameter(cmd, "@AnalyzerNutrientName", SqlDbType.NVarChar, analyzerNutrientName);

					//6/25/19 - added because of memory problem at Smithfield
					_database.AddInParameter(cmd, "@ReturnMessage", SqlDbType.Bit, 1);

					//8/17/22 - added for Romer imports
					_database.AddInParameter(cmd, "@ImportType", SqlDbType.Int, importType);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//3/7/18
		public ErrorInfo SaveSampleResultUsingEquation(Int32 sampleID, Int32 cfcUserID, Int32 aimDatabaseID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_INS_SampleQualityEquation"))
				{
					_database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.AddInParameter(cmd, "@AimDatabaseID", SqlDbType.Int, aimDatabaseID);

					//6/25/19 - added because of memory problem at Smithfield
					_database.AddInParameter(cmd, "@ReturnMessage", SqlDbType.Bit, 1);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public ErrorInfo SaveSampleQualityControl(ref SampleQualityControlInfo sampleQuality)
		{
			ErrorInfo result = new ErrorInfo();

			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityControl_SAV"))
				{
					cmd.Parameters.AddRange(sampleQuality.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public LicensingConfigInfo GetCompanyName(Int32 configID)
		{
			LicensingConfigInfo info = new LicensingConfigInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("usp_LicensingConfig_SEL_ConfigID"))
				{
					_database.AddInParameter(cmd, "@ConfigID", SqlDbType.Int, configID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<LicensingConfigInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}


		public LicensingConfigInfo SaveCompanyName(Int32 configID, String companyName)
		{
			LicensingConfigInfo info = new LicensingConfigInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("licensing.usp_Config_SAV_CompanyName"))
				{
					_database.AddInParameter(cmd, "@ConfigID", SqlDbType.Int, configID);
					_database.AddInParameter(cmd, "@CompanyName", SqlDbType.VarChar, companyName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						info = ObjectFiller<LicensingConfigInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		//8/17/22
		//public ErrorInfo InsertSampleResult(ref SampleResultInfo sampleResult, ref SampleResultPredictInfo sampleResultPredict, String analyzerNutrientName)
		public ErrorInfo InsertSampleResult(ref SampleResultInfo sampleResult, ref SampleResultPredictInfo sampleResultPredict, String analyzerNutrientName, Int32 importType)
		{
			ErrorInfo result = new ErrorInfo();

			try
			{
				if (sampleResult.AnalysisDate == DateTime.MinValue)
				{
					sampleResult.AnalysisDate = (DateTime)SqlDateTime.MinValue;
				}
				sampleResult.CFCUserID = AIMCoreGlobal.CFCUserID;

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResult_INS"))
				{
					cmd.Parameters.AddRange(sampleResult.SerializeAsSqlParameter());
					_database.AddInParameter(cmd, "@IsResultPredict", SqlDbType.Bit, true);
					cmd.Parameters.AddRange(sampleResultPredict.SerializeAsSqlParameter());
					_database.AddInParameter(cmd, "@AnalyzerNutrientName", SqlDbType.NVarChar, analyzerNutrientName);

					//6/25/19 - added because of memory problem at Smithfield
					_database.AddInParameter(cmd, "@ReturnMessage", SqlDbType.Bit, 1);

					//8/17/22 - added for Romer imports
					_database.AddInParameter(cmd, "@ImportType", SqlDbType.Int, importType);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}


		public void UpdateTemplateDetailSortOrder(ref SampleResultTemplateDetailInfo templateDetail)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleResultTemplateDetail_UPD_SortOrder"))
				{
					_database.AddInParameter(cmd, "@SampleResultTemplateID", SqlDbType.Int, templateDetail.SampleResultTemplateID);
					_database.AddInParameter(cmd, "@SampleResultTemplateDetailID", SqlDbType.Int, templateDetail.SampleResultTemplateDetailID);
					_database.AddInParameter(cmd, "@SortOrder", SqlDbType.Int, templateDetail.SortOrder);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, templateDetail.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						//TODO - LOOK WHY result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				//result.ErrorNumber = -1;
			}
			return;
		}


		public void SaveTabOrder(ref TabOrderInfo tabOrderData)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_TabOrder_UPD_TabOrderID"))
				{
					_database.AddInParameter(cmd, "@TabOrderID", SqlDbType.Int, tabOrderData.TabOrderID);
					_database.AddInParameter(cmd, "@TabOrder", SqlDbType.Int, tabOrderData.TabOrder);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, tabOrderData.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return;
		}

		//6/29/20 - commented, labels moved to Labels table
		//public void SaveAlternateNameLabels(Int32 formID, Int32 controlID, Int32 alternateID, Int32 labelField, Boolean labelValue, Int32 cfcUserID)
		//{
		//	ErrorInfo result = new ErrorInfo();
		//	try
		//	{
		//		using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AlternateName_UPD_LabelFields"))
		//		{
		//			_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, formID);
		//			_database.AddInParameter(cmd, "@ControlID", SqlDbType.Int, controlID);
		//			_database.AddInParameter(cmd, "@AlternateNameID", SqlDbType.Int, alternateID);
		//			_database.AddInParameter(cmd, "@LabelField", SqlDbType.Int, labelField);
		//			_database.AddInParameter(cmd, "@LabelValue", SqlDbType.Bit, labelValue);
		//			_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);

		//			using (IDataReader reader = _database.ExecuteReader(cmd))
		//			{
		//				result = ObjectFiller<ErrorInfo>.GetScalar(reader);
		//			}
		//		}

		//		if (result.ErrorNumber > 0)
		//		{
		//			HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//		//result.ErrorNumber = -1;
		//	}
		//	return;
		//}

		public void SaveLabels(Int32 labelID, Int32 labelField, Boolean labelValue, Boolean boldValue, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Labels_SAV_LabelFields"))
				{
					_database.AddInParameter(cmd, "@LabelID", SqlDbType.Int, labelID);
					_database.AddInParameter(cmd, "@LabelField", SqlDbType.Int, labelField);
					_database.AddInParameter(cmd, "@LabelValue", SqlDbType.Bit, labelValue);
					_database.AddInParameter(cmd, "@LabelBold", SqlDbType.Bit, boldValue);      //added 6/29/21
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				//result.ErrorNumber = -1;
			}
			return;
		}

		//5/2/23
		public void SaveProdLabels(Int32 labelID, Int32 labelField, Boolean labelValue, Boolean boldValue, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Labels_SAV_LabelProdFields"))
				{
					_database.AddInParameter(cmd, "@LabelID", SqlDbType.Int, labelID);
					_database.AddInParameter(cmd, "@LabelField", SqlDbType.Int, labelField);
					_database.AddInParameter(cmd, "@LabelValue", SqlDbType.Bit, labelValue);
					_database.AddInParameter(cmd, "@LabelBold", SqlDbType.Bit, boldValue);      //added 6/29/21
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				//result.ErrorNumber = -1;
			}
			return;
		}

		//12/7/18
		public void SaveUseAsSampleCode(Int32 templateID, Boolean value, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_UserDefinedDataSourceTemplate_UPD_UseAsSampleCode"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceTemplateID", SqlDbType.Int, templateID);
					_database.AddInParameter(cmd, "@UseAsSampleCodeValue", SqlDbType.Bit, value);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				//result.ErrorNumber = -1;
			}
			return;
		}

		public void SaveAlternateName(ref AlternateNameInfo alt)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AlternateName_UPD"))
				{
					_database.AddInParameter(cmd, "@ControlID", SqlDbType.Int, alt.ControlID);
					_database.AddInParameter(cmd, "@FormID", SqlDbType.Int, alt.FormID);
					_database.AddInParameter(cmd, "@AlternateName", SqlDbType.NVarChar, alt.AlternateName);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, alt.CFCUserID);
					_database.AddInParameter(cmd, "@Visible", SqlDbType.Bit, alt.Visible);
					_database.AddInParameter(cmd, "@RequiredField", SqlDbType.Bit, alt.RequiredField);

					//6/19/20
					_database.AddInParameter(cmd, "@ShowCodeNameOrBothID", SqlDbType.Int, alt.ShowCodeNameOrBothID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				//result.ErrorNumber = -1;
			}
			return;
		}


		#endregion

		#region CrossReferences					
		public List<AnalyzerCrossReferenceMassUpdateInfo> GetAnalyzerCrossReferenceMassUpdate(Int32 ID, String requiredField)
		{
			List<AnalyzerCrossReferenceMassUpdateInfo> result = new List<AnalyzerCrossReferenceMassUpdateInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerCrossReferenceMassUpdate_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, ID);
					_database.AddInParameter(cmd, "@AIMRequiredField", SqlDbType.NVarChar, requiredField);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerCrossReferenceMassUpdateInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMOwnerRefAnalyzerOwnerInfo> GetAIMOwnerRefAnalyzerOwner(Int32 ID)
		{
			List<AIMOwnerRefAnalyzerOwnerInfo> result = new List<AIMOwnerRefAnalyzerOwnerInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerRefAnalyzerOwner_SEL_OwnerID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerOwnerID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerRefAnalyzerOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public AIMOwnerRefAnalyzerOwnerInfo GetAIMOwnerRefAnalyzerOwnerID(Int32 ID)
		{
			AIMOwnerRefAnalyzerOwnerInfo result = new AIMOwnerRefAnalyzerOwnerInfo();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerRefAnalyzerOwner_SEL_OwnerID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerOwnerID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMOwnerRefAnalyzerOwnerInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMOwnerRefAnalyzerOwnerInfo> GetAIMOwnerRefAnalyzerOwnerDataSourceTypeID(Int32 dataSourceTypeID)
		{
			List<AIMOwnerRefAnalyzerOwnerInfo> result = new List<AIMOwnerRefAnalyzerOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerRefAnalyzerOwner_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerRefAnalyzerOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMOwnerRefAnalyzerOwnerInfo> GetAIMOwnerRefAnalyzerOwnerDataSourceTypeIDAnalyzerOwnerID(Int32 dataSourceTypeID, Int32 analyzerOwnerID)
		{
			List<AIMOwnerRefAnalyzerOwnerInfo> result = new List<AIMOwnerRefAnalyzerOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerRefAnalyzerOwner_SEL_DataSourceTypeID_AnalyzerOwnerID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerOwnerID", SqlDbType.Int, analyzerOwnerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerRefAnalyzerOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveAIMOwnerRefAnalyzerOwner(ref AIMOwnerRefAnalyzerOwnerInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerRefAnalyzerOwner_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//4/17/18 - added cfcUserID
		public void DeleteAIMOwnerRefAnalyzerOwner(Int32 AIMOwnerRefAnalyzerOwnerID, Int32 cfcUserID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerRefAnalyzerOwner_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, AIMOwnerRefAnalyzerOwnerID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		//1/8/21 - commented, not used
		//public List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> GetAnalyzerCrossRefAimOwnerLocation(Int32 ID)
		//{
		//	List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> result = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
		//	try
		//	{

		//		using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationRefAnalyzerOwnerLocation_SEL_OwnerLocationID"))
		//		{
		//			_database.AddInParameter(cmd, "@AnalyzerOwnerLocationID", SqlDbType.Int, ID);
		//			using (IDataReader reader = _database.ExecuteReader(cmd))
		//			{
		//				ObjectFiller<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//	}
		//	return result;
		//}


		public AIMOwnerLocationRefAnalyzerOwnerLocationInfo GetAnalyzerCrossRefAimOwnerLocationID(Int32 ID)
		{
			AIMOwnerLocationRefAnalyzerOwnerLocationInfo result = new AIMOwnerLocationRefAnalyzerOwnerLocationInfo();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationRefAnalyzerOwnerLocation_SEL_OwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerOwnerLocationID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> GetAnalyzerCrossRefAimOwnerLocationDataSourceTypeID(Int32 ID)
		{
			List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> result = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationRefAnalyzerOwnerLocation_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> GetAnalyzerCrossRefAimOwnerLocationDataSourceTypeIDAnalyzerOwnerLocationID(Int32 dataSourceTypeID, Int32 analyzerOwnerLocationID, Int32 analyzerOwnerID)
		{
			List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> result = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationRefAnalyzerOwnerLocation_SEL_DataSourceTypeID_AnalyzerOwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerOwnerLocationID", SqlDbType.Int, analyzerOwnerLocationID);
					_database.AddInParameter(cmd, "@AnalyzerOwnerID", SqlDbType.Int, analyzerOwnerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveAnalyzerCrossRefAimOwnerLocation(ref AIMOwnerLocationRefAnalyzerOwnerLocationInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationRefAnalyzerOwnerLocation_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		//4/17/18 - added cfcUserID
		public void DeleteAnalyzerCrossRefAimOwnerLocation(Int32 UserDefinedCrossRefAimOwnerLocationID, Int32 cfcUserID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocationRefAnalyzerOwnerLocation_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, UserDefinedCrossRefAimOwnerLocationID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public ErrorInfo SaveAnalyzerOwnerLocationToAimFromUser(Int32 analyzerOwnerLocationID, Int32 analyzerOwnerID, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMOwnerLocation_INS_GenerateFromUser"))
				{
					_database.AddInParameter(cmd, "@AnalyzerOwnerLocationID", SqlDbType.Int, analyzerOwnerLocationID);
					_database.AddInParameter(cmd, "@AnalyzerOwnerID", SqlDbType.Int, analyzerOwnerID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						//commented - show dialog message to user instead, so they can answer yes/no
						//HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public List<AIMSupplierRefAnalyzerSupplierInfo> GetAnalyzerCrossRefAimSupplierDataSourceTypeIDAnalyzerSupplierID(Int32 dataSourceTypeID, Int32 analyzerSupplierID)
		{
			List<AIMSupplierRefAnalyzerSupplierInfo> result = new List<AIMSupplierRefAnalyzerSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerCrossRefAimSupplier_SEL_DataSourceTypeID_AnalyzerSupplierID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, analyzerSupplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierRefAnalyzerSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> GetAnalyzerCrossRefAimSupplierLocationDataSourceTypeIDAnalyzerSupplierLocationID(Int32 dataSourceTypeID, Int32 analyzerSupplierLocationID, Int32 analyzerSupplierID)
		{
			List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> result = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerCrossRefAimSupplierLocation_SEL_DataSourceTypeID_AnalyzerSupplierLocationID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationID", SqlDbType.Int, analyzerSupplierLocationID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, analyzerSupplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> GetAIMSupplierLocationRefAnalyzerSupplierLocation(Int32 dataSourceTypeID)
		{
			List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> result = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationRefAnalyzerSupplierLocation_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}



		#endregion

		#region Imports

		//3/13/18 - commented, not used
		//public SampleImportColumnNamesInfo GetSampleImportColumnInfo(String tableName, String columnName)
		//{
		//	SampleImportColumnNamesInfo result = new SampleImportColumnNamesInfo();
		//	try
		//	{
		//		using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleImport_ValidateFields"))
		//		{
		//			_database.AddInParameter(cmd, "@TableName", SqlDbType.NVarChar, tableName);
		//			_database.AddInParameter(cmd, "@ColumnName", SqlDbType.NVarChar, columnName);
		//			using (IDataReader reader = _database.ExecuteReader(cmd))
		//			{
		//				result = ObjectFiller<SampleImportColumnNamesInfo>.GetScalar(reader);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//	}
		//	return result;
		//}

		public List<SampleImportLogInfo> GetSampleImportLog(Int32 UserDefinedDataSourceDefinitionID)
		{
			List<SampleImportLogInfo> info = new List<SampleImportLogInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleImportLog_SEL_UserDefinedDataSourceDefinitionID"))
				{
					_database.AddInParameter(cmd, "@UserDefinedDataSourceDefinitionID", SqlDbType.Int, UserDefinedDataSourceDefinitionID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleImportLogInfo>.GetGenericCollection(ref info, reader);

					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		public List<SampleImportLogInfo> GetSampleImportLog()
		{
			List<SampleImportLogInfo> info = new List<SampleImportLogInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleImportLog_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleImportLogInfo>.GetGenericCollection(ref info, reader);

					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return info;
		}

		#endregion

		#region Analyzer

		public void DeleteAnalyzerMaterial(Int32 UserDefinedMaterialID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerMaterial_DEL_AnalyzerMaterialID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, UserDefinedMaterialID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}



		public List<SampleQualityParameterInfo> GetSampleQualityParameter(Int32 dataSourceTypeID)
		{
			List<SampleQualityParameterInfo> result = new List<SampleQualityParameterInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityParameter_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityParameterInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerOwnerInfo> GetAnalyzerOwner(Int32 DataSourceTypeID)
		{
			List<AnalyzerOwnerInfo> result = new List<AnalyzerOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwner_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//1/5/18
		public List<SampleQualityParameterInfo> GetSampleQualityParameter(Boolean isActive)
		{
			List<SampleQualityParameterInfo> result = new List<SampleQualityParameterInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityParameter_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityParameterInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AnalyzerOwnerInfo> GetAnalyzerOwner_OwnerID(Int32 ID)
		{
			List<AnalyzerOwnerInfo> result = new List<AnalyzerOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwner_SEL_OwnerID"))
				{
					_database.AddInParameter(cmd, "@OwnerID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AnalyzerOwnerInfo> GetAnalyzerOwner_OwnerID_DataSourceTypeID_IsActive(Int32 ID, Int32 datasourceTypeID, Int32 IsActive)
		{
			List<AnalyzerOwnerInfo> result = new List<AnalyzerOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwner_SEL_OwnerID_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerOwnerID", SqlDbType.Int, ID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerOwnerInfo> GetAnalyzerOwner(Int32 DataSourceTypeID, Boolean isActive)
		{
			List<AnalyzerOwnerInfo> result = new List<AnalyzerOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwner_SEL_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<SampleQualityParameterInfo> GetSampleQualityParameter(Int32 dataSourceTypeID, Boolean isActive)
		{
			List<SampleQualityParameterInfo> result = new List<SampleQualityParameterInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleQualityParameter_SEL_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleQualityParameterInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerOwnerInfo> GetAnalyzerOwner_DataSourceType_OwnerName(Int32 dataSourceTypeID, String ownerName)
		{
			List<AnalyzerOwnerInfo> result = new List<AnalyzerOwnerInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwner_SEL_DataSourceTypeID_OwnerName"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerOwnerName", SqlDbType.NVarChar, ownerName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveAnalyzerOwner(ref AnalyzerOwnerInfo refInfo, Boolean returnError)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwner_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (returnError == true)
					{
						if (result.ErrorNumber > 0)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public void DeleteAnalyzerOwner(Int32 analyzerOwnerID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwner_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, analyzerOwnerID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocation(Int32 ownerID)
		{
			List<AnalyzerOwnerLocationInfo> result = new List<AnalyzerOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_SEL_OwnerID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerOwnerID", SqlDbType.Int, ownerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}

			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocationByDataSource(Int32 dataSourceTypeID)
		{
			List<AnalyzerOwnerLocationInfo> result = new List<AnalyzerOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}

			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocation_OwnerLocationID(Int32 ID)
		{
			List<AnalyzerOwnerLocationInfo> result = new List<AnalyzerOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_SEL_OwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerOwnerLocationID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocation(Int32 ID, Int32 datasourceTypeID, Int32 isActive)
		{
			List<AnalyzerOwnerLocationInfo> result = new List<AnalyzerOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_SEL_OwnerLocationID_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@AnalyzerOwnerLocationID", SqlDbType.Int, ID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocationNameList(Int32 datasourceTypeID, Int32 analyzerOwnerID, String ownerLocationName)
		{
			List<AnalyzerOwnerLocationInfo> result = new List<AnalyzerOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_SEL_DataSourceTypeID_OwnerID_OwnerLocationName"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerOwnerID", SqlDbType.Int, analyzerOwnerID);
					_database.AddInParameter(cmd, "@AnalyzerOwnerLocationName", SqlDbType.NVarChar, ownerLocationName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//      public List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocation_DataSourceType_OwnerLocationName(Int32 dataSourceTypeID, Int32 analyzerOwnerID, String ownerLocationName)
		//{
		//	List<AnalyzerOwnerLocationInfo> result = new List<AnalyzerOwnerLocationInfo>();
		//	try
		//	{
		//		using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_SEL_DataSourceTypeID_OwnerLocationName"))
		//		{
		//			_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
		//                  _database.AddInParameter(cmd, "@AnalyzerOwnerID", SqlDbType.Int, analyzerOwnerID);
		//                  _database.AddInParameter(cmd, "@AnalyzerOwnerLocationName", SqlDbType.NVarChar, ownerLocationName);
		//			using (IDataReader reader = _database.ExecuteReader(cmd))
		//			{
		//				ObjectFiller<AnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
		//	}
		//	return result;
		//}

		public ErrorInfo SaveAnalyzerOwnerLocation(ref AnalyzerOwnerLocationInfo refInfo, Boolean returnError)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (returnError == true)
					{
						if (result.ErrorNumber > 0)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public void DeleteAnalyzerOwnerLocation(Int32 analyzerOwnerLocationID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, analyzerOwnerLocationID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocation(Int32 DataSourceTypeID, Boolean isActive)
		{
			List<AnalyzerOwnerLocationInfo> result = new List<AnalyzerOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_SEL_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocation(Int32 DataSourceTypeID, Int32 AnalyzerOwnerID, Boolean isActive)
		{
			List<AnalyzerOwnerLocationInfo> result = new List<AnalyzerOwnerLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerOwnerLocation_SEL_DataSourceTypeID_OwnerID_IsActive"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerOwnerID", SqlDbType.Int, AnalyzerOwnerID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerOwnerLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierInfo> GetAnalyzerSupplier_SupplierID(Int32 ID)
		{
			List<AnalyzerSupplierInfo> result = new List<AnalyzerSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplier_SEL_SupplierID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public ErrorInfo SaveAnalyzerSupplier(ref AnalyzerSupplierInfo refInfo, Boolean returnError)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplier_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (returnError == true)
					{
						if (result.ErrorNumber > 0)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public List<AnalyzerSupplierInfo> GetAnalyzerSupplier_DataSourceType_SupplierName(Int32 dataSourceTypeID, String supplierName)
		{
			List<AnalyzerSupplierInfo> result = new List<AnalyzerSupplierInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplier_SEL_DataSourceTypeID_SupplierName"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierName", SqlDbType.NVarChar, supplierName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public ErrorInfo SaveAnalyzerSupplierLocation(ref AnalyzerSupplierLocationInfo refInfo, Boolean returnError)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
					if (returnError == true)
					{
						if (result.ErrorNumber > 0)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocation_DataSourceType_SupplierLocationName(Int32 dataSourceTypeID, String supplierLocationName)
		{
			List<AnalyzerSupplierLocationInfo> result = new List<AnalyzerSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SEL_DataSourceTypeID_SupplierLocationName"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationName", SqlDbType.NVarChar, supplierLocationName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocation_SupplierLocationID(Int32 ID)
		{
			List<AnalyzerSupplierLocationInfo> result = new List<AnalyzerSupplierLocationInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocation_SEL_SupplierLocationID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<AnalyzerDataSourceTypeInfo> GetAnalyzerDataSource(Int32 DataSourceTypeID, Boolean isActive)
		{
			List<AnalyzerDataSourceTypeInfo> result = new List<AnalyzerDataSourceTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerDataSourceType_SEL_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerDataSourceTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveAnalyzerDataSource(ref AnalyzerDataSourceTypeInfo refInfo, Boolean returnError)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerDataSourceType_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (returnError == true)
					{
						if (result.ErrorNumber > 0)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public void DeleteAnalyzerDataSource(Int32 analyzerDataSourceTypeID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerDataSourceType_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, analyzerDataSourceTypeID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public List<DataSourceDetailInfo> GetDataSourceDetail(Int32 dataSourceTypeID)
		{
			List<DataSourceDetailInfo> result = new List<DataSourceDetailInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceDetail_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//8/20/18
		public List<DataSourceDetailInfo> GetDataSourceCrossReference(Int32 dataSourceTypeID)
		{
			List<DataSourceDetailInfo> result = new List<DataSourceDetailInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerDataSourceType_CrossReference_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}


		public List<DataSourceDetailInfo> GetDataSourceDetail(Int32 dataSourceTypeID, Int32 analyzerDataSourceTypeID)
		{
			List<DataSourceDetailInfo> result = new List<DataSourceDetailInfo>();
			try
			{

				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_DataSourceDetail_SEL_DataSourceTypeID_AnalyzerDataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerDataSourceTypeID", SqlDbType.Int, analyzerDataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<DataSourceDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public Boolean ExecuteSqlScript(StringCollection queryList)
		{
			CommandType cmd = CommandType.Text;

			using (DbConnection conn = _database.CreateConnection())
			{
				conn.Open();
				DbTransaction trans = conn.BeginTransaction();

				try
				{
					foreach (String sql in queryList)
					{
						// Credit the first account.
						_database.ExecuteNonQuery(trans, cmd, sql);
					}

					// Commit the transaction.
					trans.Commit();
					conn.Close();
					return true;
				}
				catch
				{
					// Roll back the transaction. 
					trans.Rollback();
					return false;
				}
			}
		}
		#endregion Maintenance

		//12/21/20
		#region AnalyzerSupplierLocationCombined

		public AnalyzerSupplierLocationCombinedInfo GetAnalyzerSupplierLocationCombined(Int32 analyzerSupplierLocationCombinedID, Int32 datasourceTypeID)
		{
			AnalyzerSupplierLocationCombinedInfo result = new AnalyzerSupplierLocationCombinedInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocationCombined_SEL_AnalyzerSupplierLocationCombinedID_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationCombinedID", SqlDbType.Int, analyzerSupplierLocationCombinedID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerSupplierLocationCombinedInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierLocationCombinedInfo> GetAllAnalyzerSupplierLocationCombined(Boolean isActive)
		{
			List<AnalyzerSupplierLocationCombinedInfo> result = new List<AnalyzerSupplierLocationCombinedInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocationCombined_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationCombinedInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierLocationCombinedInfo> GetAllAnalyzerSupplierLocationCombined()
		{
			List<AnalyzerSupplierLocationCombinedInfo> result = new List<AnalyzerSupplierLocationCombinedInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocationCombined_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationCombinedInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierLocationCombinedInfo> GetAnalyzerSupplierLocationCombined(Int32 DataSourceTypeID, Boolean isActive)
		{
			List<AnalyzerSupplierLocationCombinedInfo> result = new List<AnalyzerSupplierLocationCombinedInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocationCombined_SEL_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationCombinedInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierLocationCombinedInfo> GetAnalyzerSupplierLocationCombined(Int32 DataSourceTypeID)
		{
			List<AnalyzerSupplierLocationCombinedInfo> result = new List<AnalyzerSupplierLocationCombinedInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocationCombined_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, DataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationCombinedInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AnalyzerSupplierLocationCombinedInfo GetAnalyzerSupplierLocationCombined(Int32 dataSourceTypeID, String analyzerSupplierLocationCombinedName)
		{
			AnalyzerSupplierLocationCombinedInfo result = new AnalyzerSupplierLocationCombinedInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocationCombined_SEL_AnalyzerSupplierLocationCombinedName_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationCombinedName", SqlDbType.VarChar, analyzerSupplierLocationCombinedName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AnalyzerSupplierLocationCombinedInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierLocationCombinedInfo> GetAnalyzerSupplierLocationCombinedList(Int32 dataSourceTypeID, String analyzerSupplierLocationCombinedName)
		{
			List<AnalyzerSupplierLocationCombinedInfo> result = new List<AnalyzerSupplierLocationCombinedInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocationCombined_SEL_AnalyzerSupplierLocationCombinedName_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationCombinedName", SqlDbType.VarChar, analyzerSupplierLocationCombinedName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationCombinedInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public ErrorInfo SaveAnalyzerSupplierLocationCombined(ref AnalyzerSupplierLocationCombinedInfo AnalyzerSupplierLocationCombined)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocationCombined_SAV_AnalyzerSupplierLocationCombinedID"))
				{
					cmd.Parameters.AddRange(AnalyzerSupplierLocationCombined.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public ErrorInfo SaveAnalyzerSupplierLocationCombined(ref AnalyzerSupplierLocationCombinedInfo refInfo, Boolean returnError)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocationCombined_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (returnError == true)
					{
						if (result.ErrorNumber > 0)
						{
							HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						}
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		#endregion

		//12/21/20
		#region AnalyzerSupplierLocationCombined Cross-Reference

		public void DeleteAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(Int32 ID, Int32 cfcUserID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined_DEL_ID"))
				{
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, ID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}

		public ErrorInfo SaveAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(ref AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo refInfo)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined_SAV"))
				{
					cmd.Parameters.AddRange(refInfo.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
						result.ErrorNumber = -1;
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		public List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> GetAllAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined()
		{
			List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> result = new List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined_SEL_All"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo GetAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedAnalyzerID(Int32 ID)
		{
			AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo result = new AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined_SEL_AnalyzerSupplierLocationCombinedID"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> GetAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(Int32 dataSourceTypeID)
		{
			List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> result = new List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined_SEL_DataSourceTypeID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> GetAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(Int32 dataSourceTypeID, Int32 ID)
		{
			List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> result = new List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined_SEL_DataSourceTypeID_AnalyzerLocCombinedID"))
				{
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, dataSourceTypeID);
					_database.AddInParameter(cmd, "@ID", SqlDbType.Int, ID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public List<AnalyzerSupplierLocationCombinedInfo> GetAnalyzerSupplierLocationCombined(Int32 analyzerSupplierLocationCombinedID, Int32 datasourceTypeID, Int32 isActive)
		{
			List<AnalyzerSupplierLocationCombinedInfo> result = new List<AnalyzerSupplierLocationCombinedInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AnalyzerSupplierLocationCombined_SEL_AnalyzerSupplierLocationCombinedID_DataSourceTypeID_IsActive"))
				{
					_database.AddInParameter(cmd, "@AnalyzerSupplierLocationCombinedID", SqlDbType.Int, analyzerSupplierLocationCombinedID);
					_database.AddInParameter(cmd, "@DataSourceTypeID", SqlDbType.Int, datasourceTypeID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AnalyzerSupplierLocationCombinedInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		#endregion

		//12/8/22
		#region Nutrient Tolerance
		//Header --------------------------------------
		public List<NutrientToleranceHeaderInfo> GetNutrientToleranceHeader()
		{
			List<NutrientToleranceHeaderInfo> result = new List<NutrientToleranceHeaderInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceHeader_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<NutrientToleranceHeaderInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<NutrientToleranceHeaderInfo> GetNutrientToleranceHeaderName(String headerName)
		{
			List<NutrientToleranceHeaderInfo> result = new List<NutrientToleranceHeaderInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceHeader_SEL_NutrientToleranceHeaderName"))
				{
					_database.AddInParameter(cmd, "@NutrientToleranceHeaderName", SqlDbType.NVarChar, headerName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<NutrientToleranceHeaderInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveNutrientToleranceHeader(ref NutrientToleranceHeaderInfo header)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceHeader_SAV"))
				{
					cmd.Parameters.AddRange(header.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public Int32 DeleteNutrientToleranceHeader(Int32 headerID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceHeader_DEL"))
				{
					_database.AddInParameter(cmd, "@NutrientToleranceHeaderID", SqlDbType.Int, headerID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		//Detail --------------------------------------		
		public List<NutrientToleranceDetailInfo> GetNutrientToleranceDetail(Int32 headerID)
		{
			List<NutrientToleranceDetailInfo> result = new List<NutrientToleranceDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceDetail_SEL_NutrientToleranceHeaderID"))
				{
					_database.AddInParameter(cmd, "@NutrientToleranceHeaderID", SqlDbType.Int, headerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<NutrientToleranceDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<NutrientToleranceDetailInfo> GetNutrientToleranceDetail(Int32 headerID, Int32 aimNutrientID)
		{
			List<NutrientToleranceDetailInfo> result = new List<NutrientToleranceDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceDetail_SEL_NutrientToleranceHeaderID_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@NutrientToleranceHeaderID", SqlDbType.Int, headerID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<NutrientToleranceDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveNutrientToleranceDetail(ref NutrientToleranceDetailInfo detail)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceDetail_SAV"))
				{
					cmd.Parameters.AddRange(detail.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public Int32 DeleteNutrientToleranceDetail(Int32 headerID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceDetail_DEL_NutrientToleranceHeaderID"))
				{
					_database.AddInParameter(cmd, "@NutrientToleranceHeaderID", SqlDbType.Int, headerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}
		public Int32 DeleteNutrientToleranceDetail(Int32 headerID, Int32 aimNutrientID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceDetail_DEL_NutrientToleranceHeaderID_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@NutrientToleranceHeaderID", SqlDbType.Int, headerID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		//Target --------------------------------------		
		public List<NutrientToleranceDetailTargetInfo> GetNutrientToleranceDetailTarget(Int32 headerID)
		{
			List<NutrientToleranceDetailTargetInfo> result = new List<NutrientToleranceDetailTargetInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceDetailTarget_SEL_NutrientToleranceHeaderID"))
				{
					_database.AddInParameter(cmd, "@NutrientToleranceHeaderID", SqlDbType.Int, headerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<NutrientToleranceDetailTargetInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<NutrientToleranceDetailTargetInfo> GetNutrientToleranceDetailTarget(Int32 headerID, Int32 aimNutrientID)
		{
			List<NutrientToleranceDetailTargetInfo> result = new List<NutrientToleranceDetailTargetInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceDetailTarget_SEL_NutrientToleranceHeaderID_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@NutrientToleranceHeaderID", SqlDbType.Int, headerID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<NutrientToleranceDetailTargetInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveNutrientToleranceDetailTarget(ref NutrientToleranceDetailTargetInfo nutrient)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceDetailTarget_SAV"))
				{
					cmd.Parameters.AddRange(nutrient.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public Int32 DeleteNutrientToleranceDetailTarget(Int32 headerID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceDetailTarget_DEL_NutrientToleranceHeaderID"))
				{
					_database.AddInParameter(cmd, "@NutrientToleranceHeaderID", SqlDbType.Int, headerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}
		public Int32 DeleteNutrientToleranceDetailTarget(Int32 headerID, Int32 aimNutrientID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_NutrientToleranceDetailTarget_DEL_NutrientToleranceHeaderID_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@NutrientToleranceHeaderID", SqlDbType.Int, headerID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		#endregion

		//12/8/22
		#region Product Tolerance
		//Header --------------------------------------
		public List<ProductToleranceHeaderInfo> GetProductToleranceHeader()
		{
			List<ProductToleranceHeaderInfo> result = new List<ProductToleranceHeaderInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceHeader_SEL"))
				{
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ProductToleranceHeaderInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<ProductToleranceHeaderInfo> GetProductToleranceHeaderName(String headerName)
		{
			List<ProductToleranceHeaderInfo> result = new List<ProductToleranceHeaderInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceHeader_SEL_ProductToleranceHeaderName"))
				{
					_database.AddInParameter(cmd, "@ProductToleranceHeaderName", SqlDbType.NVarChar, headerName);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ProductToleranceHeaderInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<ProductToleranceHeaderInfo> GetProductToleranceHeaderID(Int32 headerID)
		{
			List<ProductToleranceHeaderInfo> result = new List<ProductToleranceHeaderInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceHeader_SEL_ProductToleranceHeaderID"))
				{
					_database.AddInParameter(cmd, "@ProductToleranceHeaderID", SqlDbType.Int, headerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ProductToleranceHeaderInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveProductToleranceHeader(ref ProductToleranceHeaderInfo header)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceHeader_SAV"))
				{
					cmd.Parameters.AddRange(header.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public Int32 DeleteProductToleranceHeader(Int32 headerID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceHeader_DEL"))
				{
					_database.AddInParameter(cmd, "@ProductToleranceHeaderID", SqlDbType.Int, headerID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		//Detail --------------------------------------		
		public List<ProductToleranceDetailInfo> GetProductToleranceDetail(Int32 headerID)
		{
			List<ProductToleranceDetailInfo> result = new List<ProductToleranceDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceDetail_SEL_ProductToleranceHeaderID"))
				{
					_database.AddInParameter(cmd, "@ProductToleranceHeaderID", SqlDbType.Int, headerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ProductToleranceDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<ProductToleranceDetailInfo> GetProductToleranceDetailMaterial(Int32 aimMaterialID)
		{
			List<ProductToleranceDetailInfo> result = new List<ProductToleranceDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceDetail_SEL_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ProductToleranceDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveProductToleranceDetail(ref ProductToleranceDetailInfo detail)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceDetail_SAV"))
				{
					cmd.Parameters.AddRange(detail.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public Int32 DeleteProductToleranceDetail(Int32 headerID, Int32 detailID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceDetail_DEL"))
				{
					_database.AddInParameter(cmd, "@ProductToleranceHeaderID", SqlDbType.Int, headerID);
					_database.AddInParameter(cmd, "@ProductToleranceDetailID", SqlDbType.Int, detailID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}
		public Int32 DeleteProductToleranceDetail(Int32 headerID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceDetail_DEL_ProductToleranceHeaderID"))
				{
					_database.AddInParameter(cmd, "@ProductToleranceHeaderID", SqlDbType.Int, headerID);


					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}

		//Nutrient --------------------------------------		
		public List<ProductToleranceNutrientInfo> GetProductToleranceNutrient(Int32 headerID)
		{
			List<ProductToleranceNutrientInfo> result = new List<ProductToleranceNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceNutrient_SEL_ProductToleranceHeaderID"))
				{
					_database.AddInParameter(cmd, "@ProductToleranceHeaderID", SqlDbType.Int, headerID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ProductToleranceNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<ProductToleranceNutrientInfo> GetProductToleranceNutrient(Int32 headerID, Int32 aimNutrientID)
		{
			List<ProductToleranceNutrientInfo> result = new List<ProductToleranceNutrientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceNutrient_SEL_ProductToleranceHeaderID_AIMNutrientID"))
				{
					_database.AddInParameter(cmd, "@ProductToleranceHeaderID", SqlDbType.Int, headerID);
					_database.AddInParameter(cmd, "@AIMNutrientID", SqlDbType.Int, aimNutrientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ProductToleranceNutrientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveProductToleranceNutrient(ref ProductToleranceNutrientInfo nutrient)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceNutrient_SAV"))
				{
					cmd.Parameters.AddRange(nutrient.SerializeAsSqlParameter());
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}

				if (result.ErrorNumber > 0)
				{
					HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public Int32 DeleteProductToleranceNutrient(Int32 headerID)
		{
			Int32 result = 0;
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceNutrient_DEL_ProductToleranceHeaderID"))
				{
					_database.AddInParameter(cmd, "@ProductToleranceHeaderID", SqlDbType.Int, headerID);

					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<Int32>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result = 0;
			}
			return result;
		}
		public void DeleteProductToleranceNutrient_NutrientID(Int32 nutrientID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ProductToleranceNutrient_DEL_ProductToleranceNutrientID"))
				{
					_database.AddInParameter(cmd, "@ProductToleranceNutrientID", SqlDbType.Int, nutrientID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		#endregion

		//-----------------------------------------------
		#region Supplier Groups
		public List<AIMSupplierGroupInfo> GetSupplierGroup(Boolean isActive)
		{
			List<AIMSupplierGroupInfo> result = new List<AIMSupplierGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierGroup_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMSupplierGroupDetailInfo> GetSupplierGroupDetail_GroupID(Int32 groupID)
		{
			List<AIMSupplierGroupDetailInfo> result = new List<AIMSupplierGroupDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierGroupDetail_SEL_GroupID"))
				{
					_database.AddInParameter(cmd, "@GroupID", SqlDbType.Int, groupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierGroupDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public void DeleteSupplierGroupDetail_GroupID(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierGroupDetail_DEL_AIMSupplierGroupID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public List<CFCUserInfo> UpdateCFCUsersAIMSupplierGroup(Int32 aimSupplierGroupID, Int32 value)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_CFCUser_UPD_AIMSupplierGroupID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierGroupID", SqlDbType.Int, aimSupplierGroupID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveSupplierGroup(ref AIMSupplierGroupInfo group)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierGroup_SAV"))
				{
					_database.AddInParameter(cmd, "@SupplierGroupID", SqlDbType.Int, group.AIMSupplierGroupID);
					_database.AddInParameter(cmd, "@SupplierGroupCode", SqlDbType.NVarChar, group.AIMSupplierGroupCode);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, group.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, group.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public void DeleteSupplierGroupDetail(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierGroupDetail_DEL"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierGroupDetailID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public ErrorInfo SaveSupplierGroupDetail(ref AIMSupplierGroupDetailInfo group)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierGroupDetail_SAV"))
				{
					_database.AddInParameter(cmd, "@SupplierGroupDetailID", SqlDbType.Int, group.AIMSupplierGroupDetailID);
					_database.AddInParameter(cmd, "@SupplierGroupID", SqlDbType.NVarChar, group.AIMSupplierGroupID);
					_database.AddInParameter(cmd, "@SupplierID", SqlDbType.NVarChar, group.AIMSupplierID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, group.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, group.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public void DeleteSupplierGroup(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierGroup_DEL"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public List<AIMSupplierGroupDetailInfo> GetSupplierGroupDetail_AIMSupplierID(Int32 supplierID)
		{
			List<AIMSupplierGroupDetailInfo> result = new List<AIMSupplierGroupDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierGroupDetail_SEL_AIMSupplierID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, supplierID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierGroupDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo UpdateSupplierGroupDetailIsActive(Int32 aimSupplierID, Boolean isActive, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierGroupDetail_UPD_IsActive"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, aimSupplierID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public void DeleteSupplierGroupDetail_AIMSupplierID(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierGroupDetail_DEL_AIMSupplierID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		#endregion

		//-----------------------------------------------
		#region Supplier Location Groups																			//8/30/23		
		public List<AIMSupplierLocationGroupInfo> GetSupplierLocationGroup(Boolean isActive)
		{
			List<AIMSupplierLocationGroupInfo> result = new List<AIMSupplierLocationGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationGroup_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMSupplierLocationGroupInfo> GetSupplierLocationGroupUserListing(Boolean isActive, Int32 supplierGroupID)
		{
			List<AIMSupplierLocationGroupInfo> result = new List<AIMSupplierLocationGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationGroup_SEL_TOP1_SupplierGroupID_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					_database.AddInParameter(cmd, "@SupplierGroupID", SqlDbType.Int, supplierGroupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMSupplierLocationGroupInfo> GetSupplierLocationGroup_GroupID(Int32 groupID)
		{
			List<AIMSupplierLocationGroupInfo> result = new List<AIMSupplierLocationGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationGroup_SEL_AIMSupplierGroupID"))
				{
					_database.AddInParameter(cmd, "@GroupID", SqlDbType.Int, groupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<AIMSupplierLocationGroupInfo> GetSupplierLocationGroup_AIMSupplierLocationID(Int32 aimSupplierLocationID)
		{
			List<AIMSupplierLocationGroupInfo> result = new List<AIMSupplierLocationGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationGroup_SEL_AIMSupplierLocationID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, aimSupplierLocationID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<AIMSupplierLocationGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo UpdateSupplierLocationGroupIsActive(Int32 aimSupplierLocationID, Boolean isActive, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationGroup_UPD_IsActive"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, aimSupplierLocationID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public void DeleteSupplierLocationGroupDetail_GroupID(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationGroupDetail_DEL_AIMSupplierLocationGroupID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierLocationGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public List<CFCUserInfo> UpdateCFCUsersAIMSupplierLocationGroup(Int32 aimSupplierLocationGroupID, Int32 value)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_CFCUser_UPD_AIMSupplierLocationGroupID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierLocationGroupID", SqlDbType.Int, aimSupplierLocationGroupID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveSupplierLocationGroup(ref xAIMSupplierLocationGroupInfo group)
		{
			ErrorInfo result = new ErrorInfo();
			////////try
			////////{
			////////	using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationGroup_SAV"))
			////////	{
			////////		_database.AddInParameter(cmd, "@SupplierLocationGroupID", SqlDbType.Int, group.AIMSupplierLocationGroupID);
			////////		_database.AddInParameter(cmd, "@SupplierLocationGroupCode", SqlDbType.NVarChar, group.AIMSupplierLocationGroupCode);
			////////		_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, group.CFCUserID);
			////////		_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, group.IsActive);
			////////		using (IDataReader reader = _database.ExecuteReader(cmd))
			////////		{
			////////			result = ObjectFiller<ErrorInfo>.GetScalar(reader);
			////////		}
			////////	}
			////////}
			////////catch (Exception ex)
			////////{
			////////	HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			////////	result.ErrorNumber = -1;
			////////}
			return result;
		}
		public ErrorInfo SaveSupplierLocationGroupDetail(ref AIMSupplierLocationGroupInfo group)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationGroup_SAV"))
				{
					_database.AddInParameter(cmd, "@SupplierLocationGroupID", SqlDbType.Int, group.AIMSupplierLocationGroupID);
					_database.AddInParameter(cmd, "@SupplierGroupID", SqlDbType.NVarChar, group.AIMSupplierGroupID);
					_database.AddInParameter(cmd, "@AIMSupplierID", SqlDbType.NVarChar, group.AIMSupplierID);
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.NVarChar, group.AIMSupplierLocationID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, group.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, group.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public void DeleteSupplierLocationGroup(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationGroup_DEL_AIMSupplierLocationGroupID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierLocationGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public void DeleteSupplierLocationGroupAIMSupplierGroupID(Int32 aimSupplierGroupID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationGroup_DEL_AIMSupplierGroupID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierGroupID", SqlDbType.Int, aimSupplierGroupID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public void DeleteSupplierLocationGroupAIMSupplierLocationID(Int32 aimSupplierLocationID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_AIMSupplierLocationGroup_DEL_AIMSupplierLocationID"))
				{
					_database.AddInParameter(cmd, "@AIMSupplierLocationID", SqlDbType.Int, aimSupplierLocationID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		#endregion

		#region Test Type Group		//10/3/23
		public ErrorInfo SaveTestTypeGroup(ref SampleTestTypeGroupInfo group)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeGroup_SAV"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeGroupID", SqlDbType.Int, group.SampleTestTypeGroupID);
					_database.AddInParameter(cmd, "@SampleTestTypeGroupCode", SqlDbType.NVarChar, group.SampleTestTypeGroupCode);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, group.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, group.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public List<SampleTestTypeGroupInfo> GetTestTypeGroup(Boolean isActive)
		{
			List<SampleTestTypeGroupInfo> result = new List<SampleTestTypeGroupInfo>();
			try
			{				
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeGroup_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTestTypeGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveTestTypeGroupDetail(ref SampleTestTypeGroupDetailInfo group)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeGroupDetail_SAV"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeGroupDetailID", SqlDbType.Int, group.SampleTestTypeGroupDetailID);
					_database.AddInParameter(cmd, "@SampleTestTypeGroupID", SqlDbType.NVarChar, group.SampleTestTypeGroupID);
					_database.AddInParameter(cmd, "@SampleTestTypeID", SqlDbType.NVarChar, group.SampleTestTypeID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, group.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, group.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public List<SampleTestTypeGroupDetailInfo> GetTestTypeGroupDetail_GroupID(Int32 groupID)
		{
			List<SampleTestTypeGroupDetailInfo> result = new List<SampleTestTypeGroupDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeGroupDetail_SEL_GroupID"))
				{
					_database.AddInParameter(cmd, "@GroupID", SqlDbType.Int, groupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTestTypeGroupDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public void DeleteTestTypeGroup(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeGroup_DEL"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public List<CFCUserInfo> UpdateCFCUsersSampleTestTypeGroup(Int32 testTypeGroupID, Int32 value)
		{
			List<CFCUserInfo> result = new List<CFCUserInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("CFCUser.usp_CFCUser_UPD_SampleTestTypeGroupID"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeGroupID", SqlDbType.Int, testTypeGroupID);
					_database.AddInParameter(cmd, "@Value", SqlDbType.Int, value);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<CFCUserInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public void DeleteTestTypeGroupDetail_TestTypeID(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeGroupDetail_DEL_SampleTestTypeID"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public void DeleteTestTypeGroupDetail_DetailID(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeGroupDetail_DEL_SampleTestTypeGroupDetailID"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeGroupDetailID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public List<SampleTestTypeGroupInfo> GetTestTypeGroup_GroupID(Int32 groupID)
		{
			List<SampleTestTypeGroupInfo> result = new List<SampleTestTypeGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeGroup_SEL_GroupID"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeGroupID", SqlDbType.Int, groupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTestTypeGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		//10/5/23
		public List<SampleTestTypeInfo> GetSampleTestTypeGroup(Int32 groupID)
		{
			List<SampleTestTypeInfo> result = new List<SampleTestTypeInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestType_SEL_SampleTestTypeGroupID"))
				{
					_database.AddInParameter(cmd, "@SampleTestTypeGroupID", SqlDbType.Int, groupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTestTypeInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		//12/1/23
		//public List<SampleTestTypeDetailInfo> GetSampleTestTypeGroupDetail(Int32 ID, Boolean isActive)
		public List<SampleTestTypeGroupDetailInfo> GetSampleTestTypeGroupDetail(Int32 ID, Boolean isActive)
		{
			List<SampleTestTypeGroupDetailInfo> result = new List<SampleTestTypeGroupDetailInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_SampleTestTypeGroupDetail_SEL_GroupID_IsActive"))
				{
					_database.AddInParameter(cmd, "@GroupID", SqlDbType.Int, ID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<SampleTestTypeGroupDetailInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		#endregion

		#region Export Ingredient Groups
		public List<ExportIngredientInfo> GetExportIngredient_GroupID(Int32 groupID)
		{
			List<ExportIngredientInfo> result = new List<ExportIngredientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredient_SEL_ExportIngredientGroupID"))
				{
					_database.AddInParameter(cmd, "@ExportIngredientGroupID", SqlDbType.Int, groupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExportIngredientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<ExportIngredientGroupInfo> GetExportIngredientGroup(Boolean isActive)
		{
			List<ExportIngredientGroupInfo> result = new List<ExportIngredientGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredientGroup_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExportIngredientGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<ExportIngredientGroupInfo> GetExportIngredientGroup(Int32 groupID)
		{
			List<ExportIngredientGroupInfo> result = new List<ExportIngredientGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredientGroup_SEL_ExportIngredientGroupID"))
				{
					_database.AddInParameter(cmd, "@ExportIngredientGroupID", SqlDbType.Int, groupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExportIngredientGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public void DeleteExportIngredient_GroupID(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredient_DEL_ExportIngredientGroupID"))
				{
					_database.AddInParameter(cmd, "@ExportIngredientGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public void DeleteExportIngredientGroup(Int32 ID)
		{
			try
			{				
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredientGroup_DEL"))
				{
					_database.AddInParameter(cmd, "@ExportIngredientGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public ErrorInfo SaveExportIngredientGroup(ref ExportIngredientGroupInfo group)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredientGroup_SAV"))
				{
					_database.AddInParameter(cmd, "@ExportIngredientGroupID", SqlDbType.Int, group.ExportIngredientGroupID);
					_database.AddInParameter(cmd, "@ExportIngredientGroupCode", SqlDbType.NVarChar, group.ExportIngredientGroupCode);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.NVarChar, group.AIMMaterialID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, group.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, group.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo ExportIngredientSaveAs(Int32 oldGroupID, Int32 newGroupID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredient_INS_ExportIngredientGroupID_NewGroupID"))
				{
					_database.AddInParameter(cmd, "@ExportIngredientGroupID", SqlDbType.Int, oldGroupID);
					_database.AddInParameter(cmd, "@NewGroupID", SqlDbType.Int, newGroupID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveExportIngredient(ref ExportIngredientInfo export)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredient_SAV"))
				{
					_database.AddInParameter(cmd, "@ExportIngredientID", SqlDbType.Int, export.ExportIngredientID);
					_database.AddInParameter(cmd, "@ExportIngredientGroupID", SqlDbType.Int, export.ExportIngredientGroupID);
					_database.AddInParameter(cmd, "@CFCIngredientID", SqlDbType.Int, export.CFCIngredientID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, export.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, export.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public void DeleteExportIngredient(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredient_DEL"))
				{
					_database.AddInParameter(cmd, "@ExportIngredientID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public List<ExportIngredientInfo> GetExportIngredient(Int32 aimMaterialID)
		{
			List<ExportIngredientInfo> result = new List<ExportIngredientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredient_SEL_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExportIngredientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo UpdateExportIngredientIsActive(Int32 cfcIngredientID, Boolean isActive, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredient_UPD_IsActive"))
				{
					_database.AddInParameter(cmd, "@CFCIngredientID", SqlDbType.Int, cfcIngredientID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Int, isActive);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo UpdateExportIngredientGroupAIMMaterialID(Int32 groupID, Int32 aimMaterialID, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredientGroup_UPD_AIMMaterialID"))
				{
					_database.AddInParameter(cmd, "@ExportIngredientGroupID", SqlDbType.Int, groupID);
					_database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public List<ExportIngredientInfo> GetExportIngredientCFCIngredient(Int32 cfcIngredientID)
		{
			List<ExportIngredientInfo> result = new List<ExportIngredientInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportIngredient_SEL_CFCIngredientID"))
				{
					_database.AddInParameter(cmd, "@CFCIngredientID", SqlDbType.Int, cfcIngredientID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExportIngredientInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		#endregion

		#region Export Plant Groups		//1/11/24
		public List<ExportPlantInfo> GetExportPlant_GroupID(Int32 groupID)
		{
			List<ExportPlantInfo> result = new List<ExportPlantInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportPlant_SEL_ExportPlantGroupID"))
				{
					_database.AddInParameter(cmd, "@ExportPlantGroupID", SqlDbType.Int, groupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExportPlantInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<ExportPlantGroupInfo> GetExportPlantGroup(Boolean isActive)
		{
			List<ExportPlantGroupInfo> result = new List<ExportPlantGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportPlantGroup_SEL_IsActive"))
				{
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, isActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExportPlantGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public void DeleteExportPlant_GroupID(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportPlant_DEL_ExportPlantGroupID"))
				{
					_database.AddInParameter(cmd, "@ExportPlantGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public void DeleteExportPlantGroup(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportPlantGroup_DEL"))
				{
					_database.AddInParameter(cmd, "@ExportPlantGroupID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public ErrorInfo SaveExportPlant(ref ExportPlantInfo export)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportPlant_SAV"))
				{
					_database.AddInParameter(cmd, "@ExportPlantID", SqlDbType.Int, export.ExportPlantID);
					_database.AddInParameter(cmd, "@ExportPlantGroupID", SqlDbType.Int, export.ExportPlantGroupID);
					_database.AddInParameter(cmd, "@CFCPlantID", SqlDbType.Int, export.CFCPlantID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, export.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, export.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public void DeleteExportPlant(Int32 ID)
		{
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportPlant_DEL"))
				{
					_database.AddInParameter(cmd, "@ExportPlantID", SqlDbType.Int, ID);
					_database.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public List<ExportPlantGroupInfo> GetExportPlantGroup(Int32 groupID)
		{
			List<ExportPlantGroupInfo> result = new List<ExportPlantGroupInfo>();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportPlantGroup_SEL_ExportPlantGroupID"))
				{
					_database.AddInParameter(cmd, "@ExportPlantGroupID", SqlDbType.Int, groupID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						ObjectFiller<ExportPlantGroupInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo UpdateExportPlantGroupAIMOwnerLocationID(Int32 groupID, Int32 aimOwnerLocationID, Int32 cfcUserID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportPlantGroup_UPD_AIMOwnerLocationID"))
				{
					_database.AddInParameter(cmd, "@ExportPlantGroupID", SqlDbType.Int, groupID);
					_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, aimOwnerLocationID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}
		public ErrorInfo ExportPlantSaveAs(Int32 oldGroupID, Int32 newGroupID)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportPlant_INS_ExportPlantGroupID_NewGroupID"))
				{
					_database.AddInParameter(cmd, "@ExportPlantGroupID", SqlDbType.Int, oldGroupID);
					_database.AddInParameter(cmd, "@NewGroupID", SqlDbType.Int, newGroupID);
					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, AIMCoreGlobal.CFCUserID);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}

					if (result.ErrorNumber > 0)
					{
						HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public ErrorInfo SaveExportPlantGroup(ref ExportPlantGroupInfo group)
		{
			ErrorInfo result = new ErrorInfo();
			try
			{
				using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_ExportPlantGroup_SAV"))
				{
					_database.AddInParameter(cmd, "@ExportPlantGroupID", SqlDbType.Int, group.ExportPlantGroupID);
					_database.AddInParameter(cmd, "@ExportPlantGroupCode", SqlDbType.NVarChar, group.ExportPlantGroupCode);

					//1/25/24 - commented
					//_database.AddInParameter(cmd, "@AIMOwnerID", SqlDbType.Int, group.AIMOwnerID);
					//_database.AddInParameter(cmd, "@AIMOwnerLocationID", SqlDbType.Int, group.AIMOwnerLocationID);

					_database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, group.CFCUserID);
					_database.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, group.IsActive);
					using (IDataReader reader = _database.ExecuteReader(cmd))
					{
						result = ObjectFiller<ErrorInfo>.GetScalar(reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
				result.ErrorNumber = -1;
			}
			return result;
		}

		#endregion
	}
}
