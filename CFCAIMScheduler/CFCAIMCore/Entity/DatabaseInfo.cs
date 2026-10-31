using System;
using System.Data;
using Core.Library.Data;
using Core.Library.Cryptography;

namespace  CFCAIMCore.Entity
{
	[Serializable()]
	public class DatabaseInfo : DataObjectBase
	{
		private static String _password;

		#region Properties
		public Int32 DatabaseID { get; set; }

		public String ConnectName { get; set; }

		public String ServerName { get; set; }

		public String Database { get; set; }

		public Boolean IsSQLAuthentication { get; set; }

		public String User { get; set; }

		public String Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}

		public String EncryPassword
		{
			get
			{
				if (_password.Length > 0)
				{
					return Crypto.EncryptTripleDES(_password);
				}
				else
				{
					return _password;
				}
			}
			set
			{
				if (value.Length > 0)
				{
					_password = Crypto.DecryptTripleDES(value);
				}
				else
				{
					_password = String.Empty;
				}
			}
		}
		#endregion
	}
}
