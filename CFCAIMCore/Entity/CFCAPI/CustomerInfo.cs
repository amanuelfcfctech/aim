using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.CFCAPI
{
	public class CustomerInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping()]
		public Int32 APITokenID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CustomerID", SqlDbType.Int)]
		public Int32 CustomerID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Name", SqlDbType.VarChar)]
		public String Name { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMAPIKey", SqlDbType.VarChar)]
		public String AIMAPIKey { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@PNEKey", SqlDbType.VarChar)]
		public String PNEKey { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }
		#endregion
	}
}
