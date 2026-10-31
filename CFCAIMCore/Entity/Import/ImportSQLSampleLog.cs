using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ImportSQLSampleLogInfo : DataObjectBase
	{
		[DataReaderColumnMapping(),
		SqlParameterMapping("@ImportSQLSampleLogID", SqlDbType.Int)]
		public Int32 ImportSQLSampleLogID { get; set; }

		[DataReaderColumnMapping(IsOptional = true),
		SqlParameterMapping("@ImportConfigID", SqlDbType.Int)]
		public Int32 ImportConfigID { get; set; }

		[DataReaderColumnMapping(IsOptional = true),
		SqlParameterMapping("@LastImportDate", SqlDbType.DateTime)]
		public DateTime LastImportDate { get; set; }

		[DataReaderColumnMapping(IsOptional = true),
		SqlParameterMapping("@CreatedStamp", SqlDbType.DateTime)]
		public DateTime CreatedStamp { get; set; }

		[DataReaderColumnMapping(IsOptional = true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(IsOptional = true)]
		public String UserName { get; set; }
	}
}
