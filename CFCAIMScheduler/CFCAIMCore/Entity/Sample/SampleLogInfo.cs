using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleLogInfo : DataObjectBase
	{
		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleID", SqlDbType.Int)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleLogID", SqlDbType.Int)]
		public Int32 SampleLogID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@LogMessage", SqlDbType.NVarChar)]
		public String LogMessage { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CreatedStamp", SqlDbType.DateTime)]
		public DateTime CreatedStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public String UserName { get; set; }
	}
}
