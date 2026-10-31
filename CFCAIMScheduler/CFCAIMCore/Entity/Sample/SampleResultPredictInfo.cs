using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class SampleResultPredictInfo : DataObjectBase
    {
        #region Properties
		[DataReaderColumnMapping()]
		public Int32 SampleResultPredictID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 SampleResultID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsInPredictionRange", SqlDbType.Bit)]
		public Boolean IsResultPredict { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@GlobalH", SqlDbType.Decimal)]
		public Decimal GlobalH { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Limit", SqlDbType.Decimal)]
		public Decimal Limit { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsInMaterialRange", SqlDbType.Bit)]
		public Boolean IsInMaterialRange { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@MaxMaterialRange", SqlDbType.Decimal)]
		public Decimal MaxMaterialRange { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@MinMaterialRange", SqlDbType.Decimal)]
		public Decimal MinMaterialRange { get; set; }

		[DataReaderColumnMapping()]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AnalyzerNutrientID { get; set; }

		[DataReaderColumnMapping()]
		public String AnalyzerNutrientCode { get; set; }
				
		[DataReaderColumnMapping(isOptional: true)]
		public Decimal ResultValue { get; set; }
				
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 MessageOutside { get; set; }
				
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 MessageInside { get; set; }
        #endregion
    }
}

