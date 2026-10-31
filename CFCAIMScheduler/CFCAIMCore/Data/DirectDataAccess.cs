using System;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Core.Library.Notification;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using System.Diagnostics;

namespace CFCAIMCore.Data
{
	public class DirectDataAccess
	{
		#region Member Variables
		public static SqlDatabase _database;
		public static Boolean _isMessageDisplay = true;
		#endregion

		#region Constructors
		public DirectDataAccess(String connectionStringName, String connectionString)
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

		#region Function
		private static void HandleMessage(Exception ex, String methodName)
		{
			String message = String.Empty;

			message = ex.Message + " Unable to " + methodName + ". Contact IT.";

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
	}
}
