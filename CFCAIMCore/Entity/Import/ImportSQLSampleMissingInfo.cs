using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ImportSQLSampleMissingInfo : DataObjectBase
	{
		[DataReaderColumnMapping(),
		SqlParameterMapping("@ImportSQLSampleMissingID", SqlDbType.Int)]
		public Int32 ImportSQLSampleMissingID { get; set; }

		[DataReaderColumnMapping(IsOptional = true),
		SqlParameterMapping("@ImportSQLSampleLogID", SqlDbType.Int)]
		public Int32 ImportSQLSampleLogID { get; set; }

		[DataReaderColumnMapping(IsOptional = true),
		SqlParameterMapping("@MissingSampleCode", SqlDbType.NVarChar)]
		public String MissingSampleCode { get; set; }

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
