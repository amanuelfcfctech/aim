using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ApplicationInfo : DataObjectBase
	{
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AppID", SqlDbType.Int)]
		public Int32 AppID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AppName", SqlDbType.NVarChar)]
		public String AppName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Int)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

	}
}

