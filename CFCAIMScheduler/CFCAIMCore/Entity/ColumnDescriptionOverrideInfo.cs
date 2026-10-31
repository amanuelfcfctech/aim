using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ColumnDescriptionOverrideInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@ColumnDescriptionOverrideID", SqlDbType.Int)]
		public Int32 ColumnDescriptionOverrideID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@TableName", SqlDbType.NVarChar)]
		public String TableName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ColumnName", SqlDbType.NVarChar)]
		public String ColumnName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@OverrideName", SqlDbType.NVarChar)]
		public String OverrideName { get; set; }
		
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }


		#endregion
	}
}
