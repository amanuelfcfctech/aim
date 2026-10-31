using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Adisseo
{
	[Serializable()]
	public class PNECreditInfo : DataObjectBase
	{
		[DataReaderColumnMapping(),
		SqlParameterMapping("@PNECreditID", SqlDbType.Int)]
		public Int32 PNECreditID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@PNEKey", SqlDbType.VarChar )]
		public String PNEKey { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@PNEDescription", SqlDbType.VarChar)]
		public String PNEDescription { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Credit", SqlDbType.Int)]
		public Int32 Credit { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }
	}
}
