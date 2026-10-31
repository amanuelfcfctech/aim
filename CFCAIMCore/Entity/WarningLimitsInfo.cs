using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class WarningLimitsInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@WarningLimitsID", SqlDbType.Int)]
		public Int32 WarningLimitsID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
		public Int32 AIMNutrientID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Min", SqlDbType.Decimal)]
		public Decimal Min { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Max", SqlDbType.Decimal)]
		public Decimal Max { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		#endregion
	}
}

