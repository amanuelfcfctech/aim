using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ProductToleranceHeaderInfo : DataObjectBase
	{
		#region Properties			

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ProductToleranceHeaderID", SqlDbType.Int)]
		public Int32 ProductToleranceHeaderID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ProductToleranceHeaderName", SqlDbType.NVarChar)]
		public String ProductToleranceHeaderName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
		public Int32 AIMMaterialID { get; set; }			

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMMaterialCode", SqlDbType.NVarChar)]
		public String AIMMaterialCode { get; set; }
		
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
		public List<ProductToleranceDetailInfo> GroupDetail { get; set; }

		#endregion
	}
}
