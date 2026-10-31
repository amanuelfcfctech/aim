using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class AnalyzerNutrientInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerNutrientID", SqlDbType.Int)]
        public Int32 AnalyzerNutrientID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerNutrientCode", SqlDbType.VarChar)]
		public String AnalyzerNutrientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerNutrientName", SqlDbType.VarChar)]
		public String AnalyzerNutrientName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UnitType", SqlDbType.VarChar)]
		public String UnitType { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }
		
		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		//10/25/23 - changed data type for CSA
		//[DataReaderColumnMapping(),
		//SqlParameterMapping("@ConversionFactor", SqlDbType.Int)]
		//public Int32 ConversionFactor { get; set; }
		[DataReaderColumnMapping(),
		SqlParameterMapping("@ConversionFactor", SqlDbType.Decimal)]
		public Double ConversionFactor { get; set; }



		#endregion
	}
}

