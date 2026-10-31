using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class QueryAndOrInfo : DataObjectBase
	{
		[DataReaderColumnMapping(),
		SqlParameterMapping("@QueryAndOrID", SqlDbType.Int)]
		public Int32 QueryAndOrID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@QueryAndOr", SqlDbType.VarChar)]
		public String QueryAndOr { get; set; }
	}
}
