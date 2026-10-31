using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMMaterialGroupNutrientInfo : DataObjectBase
	{
		#region Properties			

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMaterialGroupNutrientID", SqlDbType.Int)]
		public Int32 AIMMaterialGroupNutrientID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMaterialGroupHeaderID", SqlDbType.Int)]
		public Int32 AIMMaterialGroupHeaderID { get; set; }

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
		SqlParameterMapping("@NutrientMinPlusMinus", SqlDbType.Int)]
		public Int32 NutrientMinPlusMinus { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@NutrientMinAmount", SqlDbType.Decimal)]
		public Double NutrientMinAmount { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@NutrientMinPercent", SqlDbType.Decimal)]
		public Double NutrientMinPercent { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@NutrientMax", SqlDbType.Decimal)]
		public Double NutrientMax { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@NutrientMaxPlusMinus", SqlDbType.Int)]
		public Int32 NutrientMaxPlusMinus { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@NutrientMaxAmount", SqlDbType.Decimal)]
		public Double NutrientMaxAmount { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@NutrientMaxPercent", SqlDbType.Decimal)]
		public Double NutrientMaxPercent { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		//4/28/22
		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }


		#endregion
	}
}
