using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Adisseo
{
	[Serializable()]
	public class PNEForAnalyzerMaterialRefAnalyzerNutrientInfo : DataObjectBase
	{
		[DataReaderColumnMapping(),
		SqlParameterMapping("@PNEForAnalyzerMaterialRefAnalyzerNutrientID", SqlDbType.Int)]
		public Int32 PNEForAnalyzerMaterialRefAnalyzerNutrientID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerMaterialID", SqlDbType.Int)]
		public Int32 AnalyzerMaterialID { get; set; }

		[DataReaderColumnMapping()]
		public String AnalyzerMaterialCode { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerNutrientID", SqlDbType.Int)]
		public Int32 AnalyzerNutrientID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping()]
		public String AnalyzerNutrientCode { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }
	}
}
