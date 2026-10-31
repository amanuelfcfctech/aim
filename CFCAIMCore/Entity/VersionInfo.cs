using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class VersionInfo : DataObjectBase
	{
		[DataReaderColumnMapping(),
		SqlParameterMapping("@ID", SqlDbType.Int)]
		public Int32 ID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Version", SqlDbType.VarChar)]
		public String Version { get; set; }
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CreatedDate", SqlDbType.DateTime)]
		public DateTime CreatedDate { get; set; }
				
	}
}

