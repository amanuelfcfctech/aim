using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class NutrientToleranceHeaderInfo : DataObjectBase
	{
		#region Properties			

		[DataReaderColumnMapping(),
		SqlParameterMapping("@NutrientToleranceHeaderID", SqlDbType.Int)]
		public Int32 NutrientToleranceHeaderID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@NutrientToleranceHeaderName", SqlDbType.NVarChar)]
		public String NutrientToleranceHeaderName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public List<NutrientToleranceDetailInfo> Detail { get; set; }

		#endregion
	}
}
