using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ProductToleranceNutrientInfo : DataObjectBase
	{
		#region Properties			

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ProductToleranceNutrientID", SqlDbType.Int)]
		public Int32 ProductToleranceNutrientID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ProductToleranceHeaderID", SqlDbType.Int)]
		public Int32 ProductToleranceHeaderID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
		public Int32 AIMNutrientID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@NutrientValue", SqlDbType.Decimal)]
		public Double NutrientValue { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@NutrientMin", SqlDbType.Decimal)]
		public Double NutrientMin { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@NutrientMax", SqlDbType.Decimal)]
		public Double NutrientMax { get; set; }			

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

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MinPlusMinus", SqlDbType.Int)]
		public Int32 MinPlusMinus { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MaxPlusMinus", SqlDbType.Int)]
		public Int32 MaxPlusMinus { get; set; }

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
