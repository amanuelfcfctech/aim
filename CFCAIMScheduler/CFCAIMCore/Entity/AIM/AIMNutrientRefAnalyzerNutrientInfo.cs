using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMNutrientRefAnalyzerNutrientInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMNutrientRefAnalyzerNutrientID", SqlDbType.Int)]
		public Int32 AIMNutrientRefAnalyzerNutrientID { get; set; }		
		
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
		public Int32 AIMNutrientID { get; set; }
			
		[DataReaderColumnMapping()]	
		public String AIMNutrientCode { get; set; }

		[DataReaderColumnMapping()]
		public String AIMNutrientName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 UnitTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerNutrientID", SqlDbType.Int)]
		public Int32 AnalyzerNutrientID { get; set; }

		[DataReaderColumnMapping()]
		public String AnalyzerNutrientCode { get; set; }

		[DataReaderColumnMapping()]
		public String AnalyzerNutrientName { get; set; }				

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		//(Analyzer unit-type is string vs int)
		[DataReaderColumnMapping(isOptional: true)]
		public String UnitType { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 ConversionFactor { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceTypeName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }


		#endregion
	}
}