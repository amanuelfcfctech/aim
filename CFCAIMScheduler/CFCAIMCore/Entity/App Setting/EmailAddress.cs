using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class EmailAddressInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@EmailAddressID", SqlDbType.Int)]
		public Int32 EmailAddressID { get; set; }
	
		[DataReaderColumnMapping(),
		SqlParameterMapping("@EmailAddress", SqlDbType.VarChar)]
		public String EmailAddress { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@EmailIPAddress", SqlDbType.VarChar)]
		public String EmailIPAddress { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@EmailUserName", SqlDbType.VarChar)]
		public String EmailUserName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@EmailPassword", SqlDbType.VarChar)]
		public String EmailPassword { get; set; }
				
		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		#endregion
	}
}
