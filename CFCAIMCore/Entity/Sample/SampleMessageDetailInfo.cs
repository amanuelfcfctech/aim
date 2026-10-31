using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleMessageDetailInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping()]
		public Int32 SampleResultPredictID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 SampleResultID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ResultValue", SqlDbType.Decimal)]
		public Decimal ResultValue { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsInPredictionRange", SqlDbType.Bit)]
		public Boolean IsResultPredict { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@GlobalH", SqlDbType.Decimal)]
		public Decimal GlobalH { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Limit", SqlDbType.Decimal)]
		public Decimal Limit { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsInMaterialRange", SqlDbType.Bit)]
		public Boolean IsInMaterialRange { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MaxMaterialRange", SqlDbType.Decimal)]
		public Decimal MaxMaterialRange { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MinMaterialRange", SqlDbType.Decimal)]
		public Decimal MinMaterialRange { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerNutrientCode", SqlDbType.NVarChar)]
		public String AnalyzerNutrientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerNutrientID", SqlDbType.Int)]
		public Int32 AnalyzerNutrientID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AimMaterialID", SqlDbType.Int)]
		public Int32 AimMaterialID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AimMaterialName", SqlDbType.NVarChar)]
		public String AimMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AimNutrientID", SqlDbType.Int)]
		public Int32 AimNutrientID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AimNutrientName", SqlDbType.NVarChar)]
		public String AimNutrientName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MinWarning", SqlDbType.Decimal)]
		public Decimal MinWarning { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MaxWarning", SqlDbType.Decimal)]
		public Decimal MaxWarning { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String Message { get; set; }
		
		#endregion
	}
}

