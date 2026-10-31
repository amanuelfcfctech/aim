using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.CFCAPI
{
    public class APITokenInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping()]
		public Int32 APITokenID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CustomerID", SqlDbType.Int)]
		public Int32 CustomerID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Token", SqlDbType.VarChar)]
		public String Token { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
		public Boolean IsActive { get; set; }

		//DB 230605
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@PNEKey", SqlDbType.VarChar)]
		public String PNEKey { get; set; }

		[DataReaderColumnMapping()]
		public DateTime CreateStamp { get; set; }

		[DataReaderColumnMapping()]
		public DateTime ExpireStamp { get; set; }

		#endregion
	}
}
