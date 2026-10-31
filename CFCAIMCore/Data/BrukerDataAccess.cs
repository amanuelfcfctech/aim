using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Bruker;
using Core.Library.Data;
using Core.Library.Notification;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DevExpress.Utils.Design;

namespace CFCAIMCore.Data
{
	public class BrukerDataAccess
	{
		#region Member Variables
		public static SqlDatabase _brukerDatabase;
		public static Boolean _isMessageDisplay = true;
		private static String _connectionString = String.Empty;
		#endregion

		#region Constructors
		public BrukerDataAccess()
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
		public static String BrukerDatabase
		{
			set
			{
				String connectionString = value;
				ConnectionString = connectionString;
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

		#region Function
		private static void HandleMessage(Exception ex, String methodName)
		{
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
		public void SQLCommand(String sqlCommand)
		{
			_brukerDatabase.ExecuteNonQuery(sqlCommand);
		}

		public void ExecuteNonQuerySQLCommand(String sqlCommand)
		{
			//String connectionString = @"Data Source=server2016\sql2019;Initial Catalog=BrukerButterBall;User ID=sa;Password=SQL2019!!";
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				connection.Open();

				using (SqlCommand cmd = new SqlCommand(sqlCommand, connection))
				{
					cmd.CommandType = CommandType.Text;
					cmd.ExecuteNonQuery();
				}
			}
		}

        //public void SQLStoredProcedure(String storedProcedureName, String sqlCommand)
        //{
        //	try
        //	{
        //		using (SqlConnection connection = new SqlConnection("YourConnectionString"))
        //		{
        //			connection.Open();
        //			ServerConnection srvCon = new ServerConnection(connection);
        //			Server srv = new Server(srvCon);
        //			Database db = srv.Databases[connection.Database];
        //			StoredProcedure sp = new StoredProcedure(db, storedProcedureName);
        //			sp.TextMode = false;
        //			sp.AnsiNullsStatus = false;
        //			sp.QuotedIdentifierStatus = true;
        //			sp.ImplementationType = ImplementationType.TransactSql;
        //			sp.Schema = "dbo";
        //			sp.Refresh();
        //			sp.TextBody = sqlCommand;
        //			sp.Alter();
        //		}
        //	}
        //	catch (Exception ex)
        //	{
        //		HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
        //	}
        //}
        #endregion

        #region Insert



        public void InsertCFCLogResult(BrukerCFCLogResultInfo brukerCFCLog)
		{
			ErrorInfo errorMessage = new ErrorInfo();
			String sql = String.Empty;

			try
			{

				sql = @"IF NOT EXISTS(SELECT
								CLR.PATH
							FROM
								dbo.CFCLogResult CLR
							WHERE
								CLR.Path = '" + brukerCFCLog.Path + @"'
								AND CLR.FileName = '" + brukerCFCLog.FileName + @"'
								AND CLR.DataBlock = '" + brukerCFCLog.DataBlock + @"'
								AND CLR.MethodID = " + brukerCFCLog.MethodID.ToString() + @" 
								AND CLR.DateTimeAnalysis = '" + brukerCFCLog.DateTimeAnalysis.ToString("yyyy-MM-dd HH:mm:ss") + @"'
								AND CLR.GMT = " + brukerCFCLog.GMT.ToString() + @" 
								AND CLR.ComponentName = '" + brukerCFCLog.ComponentName + @"'
								)
					BEGIN
						INSERT INTO
							dbo.CFCLogResult
								(Path, FileName, DataBlock, MethodID, DateTimeAnalysis, GMT, ComponentName, IsError, ErrorMessage) 
						VALUES
								('" + brukerCFCLog.Path + @"', '" + brukerCFCLog.FileName + @"', '" + brukerCFCLog.DataBlock + @"', " + brukerCFCLog.MethodID.ToString() + @" 
								, '" + brukerCFCLog.DateTimeAnalysis.ToString("yyyy-MM-dd HH:mm:ss") + @"', " + brukerCFCLog.GMT.ToString() + @", '" + brukerCFCLog.ComponentName + @"'
								, " + Convert.ToInt32(brukerCFCLog.IsError).ToString() + @", '" + brukerCFCLog.ErrorMessage + @"'
								)
					END";

				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					_brukerDatabase.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				errorMessage.ErrorMessage = ex.Message + " " + sql;
				HandleMessage(errorMessage, MethodInfo.GetCurrentMethod().Name);
			}
		}
		public void InsertCFCLogSpectrum(String path, String fileName, Boolean isError, String errorMessage)
		{
			String sql = String.Empty;

			try
			{
				sql = @"INSERT INTO		
							dbo.CFCLogSpectrum 
								(	 
								  Path 
								, FileName
								, IsError
								, ErrorMessage
								)
						VALUES
								(	 
								  '" + path + @"'
								, '" + fileName + @"'
								, " + Convert.ToInt32(isError) + @"
								, '" + errorMessage + @"'
								)";

				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					_brukerDatabase.ExecuteNonQuery(cmd);
				}

			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
		}
		#endregion

		#region Get
		//2/26/20
		public List<BrukerSQLInfo> GetBrukerSqlData(String sampleCode, DateTime startDate, DateTime endDate)
		{
			List<BrukerSQLInfo> info = new List<BrukerSQLInfo>();
			String sql = String.Empty;

			try
			{
				if (sampleCode.Length == 0)
				{
					sampleCode = null;
				}

				sql = @"SELECT
							  P.Value_Product_Info_1 as SampleCode
							, P.Product AS Material
							, P.Path
							, P.File_Name AS FileName
							, R.Date_Time_Analysis as SampleDate
							, R.Component_Name AS Nutrient
							, R.Component_Unit AS UnitType
							, R.Prediction_Calculated as ResultValue
							, R.Spectrum_Name + '-' + cast(R.Method_ID AS VARCHAR) UniqueID
						FROM
							dbo.T1_Lab_And_Process_Parameters P
						INNER JOIN
							dbo.T1_Quant2_Results R
								ON P.Path = R.Spectrum_Path
								AND P.File_Name = R.Spectrum_Name
						WHERE
							R.Date_Time_Analysis >= ISNULL(" + startDate.ToString("yyyy-MM-dd") + " 00:00:00.000" + @", R.Date_Time_Analysis)
							AND R.Date_Time_Analysis <= ISNULL(" + endDate.ToString("yyyy-MM-dd") + " 23:59:59.999" + @", R.Date_Time_Analysis)
							AND P.Value_Product_Info_1 = ISNULL(" + sampleCode + @", P.Value_Product_Info_1)
							AND R.Prediction_Calculated IS NOT NULL
						ORDER BY
							  P.Value_Product_Info_1
							, R.Date_Time_Analysis";

				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					using (IDataReader reader = _brukerDatabase.ExecuteReader(cmd))
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
		public List<BrukerSQLInfo> GetBrukerSqlData()
		{
			List<BrukerSQLInfo> info = new List<BrukerSQLInfo>();
			String sql = String.Empty;

			try
			{
				sql = @"SELECT 
						  P.Value_Product_Info_1 as SampleCode
						, P.Product as Material
						, P.Path 
						, P.File_Name AS FileName
						, R.Date_Time_Analysis as SampleDate
						, R.Component_Name as Nutrient
						, R.Component_Unit as UnitType
						, R.Prediction_Calculated as ResultValue	
						, R.Spectrum_Name + '-' + cast(R.Method_ID AS VARCHAR) UniqueID
					FROM            
						dbo.T1_Lab_And_Process_Parameters  P
						INNER JOIN
							dbo.T1_Quant2_Results R						 
								ON	P.Path = R.Spectrum_Path 
								AND	P.File_Name = R.Spectrum_Name
						LEFT OUTER JOIN
							dbo.CFCLog CL	 
								ON	CL.Path = P.Path 
								AND	CL.FileName = P.File_Name
					WHERE
						CL.Path IS NULL
						AND R.Prediction_Calculated IS NOT NULL
					ORDER BY
						  P.Value_Product_Info_1
						, R.Date_Time_Analysis ";

				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					using (IDataReader reader = _brukerDatabase.ExecuteReader(cmd))
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
		public List<BrukerSampleInfo> GetBrukerSpectrum()
		{
			List<BrukerSampleInfo> result = new List<BrukerSampleInfo>();
			String sql = String.Empty;
			try
			{
				sql = @"SELECT 
						  P.Path
						, P.File_Name
						, P.Scenario_Name
						, P.Measuring_Point_Name
						, P.Recipe
						, P.Batch_Number
						, P.Product_Group
						, P.Product
						, P.Label_Product_Info_1
						, P.Value_Product_Info_1
						, P.Label_Product_Info_2
						, P.Value_Product_Info_2
						, P.Label_Product_Info_3
						, P.Value_Product_Info_3
						, P.Label_Product_Info_4
						, P.Value_Product_Info_4
						, P.Label_Product_Info_5
						, P.Value_Product_Info_5
						, P.Device_Type
						, P.Device_Description
						, P.Position_Microplate
						, P.Sample_Wheel_Position
						, P.Spectral_Series
						, P.Info_Message_Ident
						, P.Info_Message_Quant
						, P.Comment_Ident
						, P.Comment_Quant
						, P.Temperature
						, P.Pressure
						, P.Flow_Rate
						, P.Flow_Speed
						, P.Humidity
						, P.pH_Value
						, P.Conductivity
						, P.Container_Number
						, P.Sample_ID
						, P.DBCodePage
					FROM
						dbo.T1_Lab_And_Process_Parameters P
						LEFT OUTER JOIN
							dbo.T1_Spectra_Parameters TSP
								ON P.Path = TSP.Path
								AND P.File_Name = TSP.File_Name
						LEFT OUTER JOIN
							dbo.CFCLogSpectrum CLS
								ON CLS.Path = P.Path
								AND CLS.FileName = P.File_Name
					WHERE
						CLS.Path IS NULL
						AND TSP.ScSm_Date_Time_of_Measurement IS NOT NULL
						AND DATEADD(ss, TSP.Scan_Time +60, TSP.ScSm_Date_Time_of_Measurement) < GETDATE()";

				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					using (IDataReader reader = _brukerDatabase.ExecuteReader(cmd))
					{
						ObjectFiller<BrukerSampleInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<BrukerSampleInfo> GetBrukerSample()
		{
			List<BrukerSampleInfo> result = new List<BrukerSampleInfo>();
			String sql = String.Empty;

			try
			{
				sql = @"SELECT 
							  TLAPP.Path
							, TLAPP.File_Name
							, TLAPP.Scenario_Name
							, TLAPP.Measuring_Point_Name
							, TLAPP.Recipe
							, TLAPP.Batch_Number
							, TLAPP.Product_Group
							, TLAPP.Product
							, TLAPP.Label_Product_Info_1
							, TLAPP.Value_Product_Info_1
							, TLAPP.Label_Product_Info_2
							, TLAPP.Value_Product_Info_2
							, TLAPP.Label_Product_Info_3
							, TLAPP.Value_Product_Info_3
							, TLAPP.Label_Product_Info_4
							, TLAPP.Value_Product_Info_4
							, TLAPP.Label_Product_Info_5
							, TLAPP.Value_Product_Info_5
							, TLAPP.Device_Type
							, TLAPP.Device_Description
							, TLAPP.Position_Microplate
							, TLAPP.Sample_Wheel_Position
							, TLAPP.Spectral_Series
							, TLAPP.Info_Message_Ident
							, TLAPP.Info_Message_Quant
							, TLAPP.Comment_Ident
							, TLAPP.Comment_Quant
							, TLAPP.Temperature
							, TLAPP.Pressure
							, TLAPP.Flow_Rate
							, TLAPP.Flow_Speed
							, TLAPP.Humidity
							, TLAPP.pH_Value
							, TLAPP.Conductivity
							, TLAPP.Container_Number
							, TLAPP.Sample_ID
							, TLAPP.DBCodePage
						FROM            
							dbo.T1_Lab_And_Process_Parameters TLAPP
							INNER JOIN 
								dbo.T1_Quant2_Results TQR
									ON TQR.Spectrum_Path = TLAPP.Path 
									AND TQR.Spectrum_Name = TLAPP.File_Name 
							INNER JOIN
								dbo.T1_Spectra_Parameters TSP
									ON TLAPP.Path = TSP.Path 
									AND TLAPP.File_Name = TSP.File_Name 
							LEFT OUTER JOIN
								dbo.CFCLogResult CLR
									ON CLR.Path = TQR.Spectrum_Path
									AND CLR.FileName = TQR.Spectrum_Name 
									AND CLR.DataBlock = TQR.Data_Block 
									AND CLR.MethodID = TQR.Method_ID 
									AND CLR.DateTimeAnalysis = TQR.Date_Time_Analysis 
									AND CLR.GMT = TQR.GMT
									AND CLR.ComponentName = TQR.Component_Name 
						WHERE
							CLR.Path IS NULL
							AND TSP.ScSm_Date_Time_of_Measurement IS NOT NULL
							AND DATEADD(ss,TSP.Scan_Time + 60, TSP.ScSm_Date_Time_of_Measurement) < GETDATE()
						GROUP BY
							  TLAPP.Path
							, TLAPP.File_Name
							, TLAPP.Scenario_Name
							, TLAPP.Measuring_Point_Name
							, TLAPP.Recipe
							, TLAPP.Batch_Number
							, TLAPP.Product_Group
							, TLAPP.Product
							, TLAPP.Label_Product_Info_1
							, TLAPP.Value_Product_Info_1
							, TLAPP.Label_Product_Info_2
							, TLAPP.Value_Product_Info_2
							, TLAPP.Label_Product_Info_3
							, TLAPP.Value_Product_Info_3
							, TLAPP.Label_Product_Info_4
							, TLAPP.Value_Product_Info_4
							, TLAPP.Label_Product_Info_5
							, TLAPP.Value_Product_Info_5
							, TLAPP.Device_Type
							, TLAPP.Device_Description
							, TLAPP.Position_Microplate
							, TLAPP.Sample_Wheel_Position
							, TLAPP.Spectral_Series
							, TLAPP.Info_Message_Ident
							, TLAPP.Info_Message_Quant
							, TLAPP.Comment_Ident
							, TLAPP.Comment_Quant
							, TLAPP.Temperature
							, TLAPP.Pressure
							, TLAPP.Flow_Rate
							, TLAPP.Flow_Speed
							, TLAPP.Humidity
							, TLAPP.pH_Value
							, TLAPP.Conductivity
							, TLAPP.Container_Number
							, TLAPP.Sample_ID
							, TLAPP.DBCodePage";

				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					using (IDataReader reader = _brukerDatabase.ExecuteReader(cmd))
					{
						ObjectFiller<BrukerSampleInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<BrukerSampleInfo> GetBrukerSampleAll()
		{
			List<BrukerSampleInfo> result = new List<BrukerSampleInfo>();
			try
			{
				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand("SELECT Path, File_Name, Scenario_Name, Measuring_Point_Name, Recipe, Batch_Number, Product_Group, Product, Label_Product_Info_1, Value_Product_Info_1, Label_Product_Info_2, Value_Product_Info_2, Label_Product_Info_3, Value_Product_Info_3, Label_Product_Info_4, Value_Product_Info_4, Label_Product_Info_5, Value_Product_Info_5, Device_Type, Device_Description, Position_Microplate, Sample_Wheel_Position, Spectral_Series, Info_Message_Ident, Info_Message_Quant, Comment_Ident, Comment_Quant, Temperature, Pressure, Flow_Rate, Flow_Speed, Humidity, pH_Value, Conductivity, Container_Number, Sample_ID, DBCodePage FROM dbo.T1_Lab_And_Process_Parameters"))
				{
					using (IDataReader reader = _brukerDatabase.ExecuteReader(cmd))
					{
						ObjectFiller<BrukerSampleInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<BrukerSampleResultInfo> GetBrukerSampleResultNotSent(String spectrumPath, String spectrumName)
		{
			List<BrukerSampleResultInfo> result = new List<BrukerSampleResultInfo>();
			String sql = String.Empty;
			try
			{

				sql = @"SELECT
						  TQR.Spectrum_Path
						, TQR.Spectrum_Name
						, TQR.Data_Block
						, TQR.Method_ID
						, TQR.Date_Time_Analysis
						, TQR.GMT
						, TQR.X_Startpoint
						, TQR.X_Endpoint
						, TQR.Component_Name
						, TQR.Component_Unit
						, TQR.Formatting
						, TQR.Prediction
						, TQR.Prediction_Outside
						, TQR.Target_Concentration
						, TQR.Warning_Upper_Limit
						, TQR.Warning_Lower_Limit
						, TQR.Alarm_Upper_Limit
						, TQR.Alarm_Lower_Limit
						, TQR.Mah_Dist
						, TQR.Mah_Dist_Limit
						, TQR.Mah_Dist_Outlier
						, TQR.Spec_Residual
						, TQR.F_Value
						, TQR.F_Prob
						, TQR.F_Prob_Limit
						, TQR.Spec_Residual_Outlier
						, TQR.Component_Value_Density
						, TQR.Component_Value_Density_Limit
						, TQR.Process_Channel_Name
						, TQR.Component_Name_User
						, TQR.Component_Unit_User
						, TQR.Prediction_Calculated
						, TQR.Prediction_Calculated_Outside
						, TQR.Formula
						, TQR.External_Bias
						, TQR.Mah_Dist_Limit_By_Factor
						, TQR.MDI
						, TQR.Method_Path
						, TQR.Method_Name
						, TQR.Method_Date_Time
						, TQR.Method_GMT
						, TQR.DBCodePage
					FROM
						dbo.T1_Quant2_Results TQR
						LEFT OUTER JOIN
							dbo.CFCLogResult CLR
								ON CLR.Path = TQR.Spectrum_Path
								AND CLR.FileName = TQR.Spectrum_Name
								AND CLR.DataBlock = TQR.Data_Block
								AND CLR.MethodID = TQR.Method_ID
								AND CLR.DateTimeAnalysis = TQR.Date_Time_Analysis
								AND CLR.GMT = TQR.GMT
								AND CLR.ComponentName = TQR.Component_Name
					WHERE
						CLR.Path IS NULL
						AND TQR.Spectrum_Path = '" + spectrumPath + @"' 
						AND TQR.Spectrum_Name = '" + spectrumName + "'";




				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					using (IDataReader reader = _brukerDatabase.ExecuteReader(cmd))
					{
						ObjectFiller<BrukerSampleResultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public List<BrukerSampleResultInfo> GetBrukerSampleResult(String spectrumPath, String spectrumName)
		{
			List<BrukerSampleResultInfo> result = new List<BrukerSampleResultInfo>();
			String sql = String.Empty;

			try
			{
				sql = "SELECT TQR.Spectrum_Path, TQR.Spectrum_Name, TQR.Data_Block, TQR.Method_ID, TQR.Date_Time_Analysis, TQR.GMT, TQR.X_Startpoint, TQR.X_Endpoint, TQR.Component_Name, TQR.Component_Unit, TQR.Formatting, TQR.Prediction, TQR.Prediction_Outside, TQR.Target_Concentration, TQR.Warning_Upper_Limit, TQR.Warning_Lower_Limit, TQR.Alarm_Upper_Limit, TQR.Alarm_Lower_Limit, TQR.Mah_Dist, TQR.Mah_Dist_Limit, TQR.Mah_Dist_Outlier, TQR.Spec_Residual, TQR.F_Value, TQR.F_Prob ";
				sql += ", TQR.F_Prob_Limit, TQR.Spec_Residual_Outlier, TQR.Component_Value_Density, TQR.Component_Value_Density_Limit, TQR.Process_Channel_Name, TQR.Component_Name_User, TQR.Component_Unit_User, TQR.Prediction_Calculated, TQR.Prediction_Calculated_Outside, TQR.Formula, TQR.External_Bias, TQR.Mah_Dist_Limit_By_Factor ";
				sql += ", TQR.MDI, TQR.Method_Path, TQR.Method_Name, TQR.Method_Date_Time, TQR.Method_GMT, TQR.DBCodePage FROM	dbo.T1_Quant2_Results TQR WHERE ";
				sql += " TQR.Spectrum_Path = '" + spectrumPath + "' AND TQR.Spectrum_Name = '" + spectrumName + "'";

				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					using (IDataReader reader = _brukerDatabase.ExecuteReader(cmd))
					{
						ObjectFiller<BrukerSampleResultInfo>.GetGenericCollection(ref result, reader);
					}
				}
			}
			catch (Exception ex)
			{
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		public Boolean GetSysObject(String name)
		{
			Boolean result = false;
			String sql = "SELECT Name FROM sysobjects WHERE Name = '" + name + "'";

			try
			{
				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					cmd.CommandType = CommandType.Text;
					using (IDataReader reader = _brukerDatabase.ExecuteReader(cmd))
					{
						if (reader.Read() == true)
						{
							if (reader["Name"].ToString().ToUpper() == name.ToUpper())
							{
								result = true;
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
		public String GetCurrentVersion()
		{
			String result = String.Empty;
			String sql = "SELECT Version FROM dbo.CFCVersion ORDER BY Version desc";

			try
			{
				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					cmd.CommandType = CommandType.Text;
					using (IDataReader reader = _brukerDatabase.ExecuteReader(cmd))
					{
						if (reader.Read() == true)
						{
							result = reader["Version"].ToString();
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
		public BrukerFileListInfo GetBrukerFileList(String path, String fileName)
		{
			BrukerFileListInfo result = new BrukerFileListInfo();
			String sql = String.Empty;

			try
			{
				sql = @"SELECT
							  TFL.Path
							, TFL.File_Name
							, TFL.Name_Part1
							, TFL.Extension
							, TFL.Date_Time_Entry
							, TFL.GMT_Entry
							, TFL.Date_Time_Last_Change
							, TFL.GMT_Last_Change
							, TFL.Spectrum
							, TFL.DBCodePage
						FROM
							dbo.T1_File_List TFL
						WHERE
							TFL.Path = '" + path + @"'
							AND TFL.File_Name = '" + fileName + @"'";

				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					using (IDataReader reader = _brukerDatabase.ExecuteReader(cmd))
					{
						result = ObjectFiller<BrukerFileListInfo>.GetScalar(reader);
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

		#region Delete
		public ErrorInfo DeleteCFCLogResult(String path, String fileName)
		{
			ErrorInfo result = new ErrorInfo();
			String sql = "DELETE dbo.CFCLogResult WHERE Path = '" + path + "' AND FileName = '" + fileName + "'";

			try
			{
				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					cmd.CommandType = CommandType.Text;
					_brukerDatabase.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				result.ErrorNumber = 9999;
				result.ErrorMessage = ex.Message;
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}

		public ErrorInfo DeleteCFCLogSpectrum(String path, String fileName)
		{
			ErrorInfo result = new ErrorInfo();
			String sql = "DELETE dbo.CFCLogSpectrum WHERE Path = '" + path + "' AND FileName = '" + fileName + "'";

			try
			{
				using (DbCommand cmd = _brukerDatabase.GetSqlStringCommand(sql))
				{
					cmd.CommandType = CommandType.Text;
					_brukerDatabase.ExecuteNonQuery(cmd);
				}
			}
			catch (Exception ex)
			{
				result.ErrorNumber = 9999;
				result.ErrorMessage = ex.Message;
				HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
			}
			return result;
		}
		#endregion
	}
}
