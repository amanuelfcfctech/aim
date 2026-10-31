using System;
using System.Data;
using Core.Library.Data;


namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class NutrientToleranceDetailTargetInfo : DataObjectBase
	{
		#region Properties			

		[DataReaderColumnMapping(),
		SqlParameterMapping("@NutrientToleranceDetailTargetID", SqlDbType.Int)]
		public Int32 NutrientToleranceDetailTargetID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@NutrientToleranceHeaderID", SqlDbType.Int)]
		public Int32 NutrientToleranceHeaderID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
		public Int32 AIMNutrientID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientName { get; set; }
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MinAmount", SqlDbType.Decimal)]
		public Double MinAmount { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MinPercent", SqlDbType.Decimal)]
		public Double MinPercent { get; set; }			

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MaxAmount", SqlDbType.Decimal)]
		public Double MaxAmount { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MaxPercent", SqlDbType.Decimal)]
		public Double MaxPercent { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }


		#endregion
	}
}
