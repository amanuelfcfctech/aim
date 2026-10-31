using System;
using System.Collections.Generic;
using CFCAIMCore.Data;
using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Adisseo;
using System.Windows.Forms;


namespace CFCAIMCore.Manager
{
	class DirectManager
	{
		#region  Member Variables

		public static String _host;

		//VERSION 2017-02-02 (1/19/17) - changed these 3 from public to private
		private static String _databaseConnection;
		private static Boolean _isMultipleDatabase = false;
		private static DirectDataAccess _aimData;

		#endregion

		#region  Properties
		protected static DirectDataAccess AIMData
		{
			get
			{
				if (_aimData == null)
				{
					if (IsMultipleDatabase)
					{
						_aimData = new Data.DirectDataAccess(String.Empty, DatabaseConnection);
					}
					else
					{
						if (_host == "CFCAIMDEMO")
						{
							_aimData = new Data.DirectDataAccess("DemoConnectionString", String.Empty);
						}
						else
						{
							_aimData = new Data.DirectDataAccess("ConnectionString", String.Empty);
						}
					}
				}
				return _aimData;
			}
		}

		//11/3/22
		public static void CloseDirectData()
		{
			_aimData = null;
		}


		public static String Host
		{
			get
			{
				return _host;
			}

			set
			{
				_host = value;
			}
		}

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

		public static void CloseAIMData()
		{
			_aimData = null;
		}
		#endregion
	}
}
