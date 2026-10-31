using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMMaterialGroupHeaderInfo : DataObjectBase
	{
		#region Properties			

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMaterialGroupHeaderID", SqlDbType.Int)]
		public Int32 AIMMaterialGroupHeaderID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMMaterialGroupHeaderName", SqlDbType.NVarChar)]
		public String AIMMaterialGroupHeaderName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MaterialTypeID", SqlDbType.Int)]
		public Int32 MaterialTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMMaterialCode", SqlDbType.NVarChar)]
		public String AIMMaterialCode { get; set; }

		//4/27/22
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SpecFrom", SqlDbType.Int)]
		public Int32 SpecFrom { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String ProductionVersion { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String ArchiveVersion { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }
				
		[DataReaderColumnMapping(isOptional: true)]
		public List<AIMMaterialGroupDetailInfo> GroupDetail { get; set; }

		#endregion
	}
}
