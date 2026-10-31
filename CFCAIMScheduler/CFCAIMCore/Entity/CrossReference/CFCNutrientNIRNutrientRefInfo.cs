using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class CFCNutrientRefAIMNutrientInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
		SqlParameterMapping("@CFCNutrientRefAIMNutrientID", SqlDbType.Int)]
		public Int32 CFCNutrientRefAIMNutrientID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCNutrientID", SqlDbType.Int)]
		public Int32 CFCNutrientID { get; set; }

        [DataReaderColumnMapping()]
        public String CFCNutrientCode {get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCNutrientName {get; set; }
		
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
		public Int32 AIMNutrientID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientCode { get; set; }
		
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerNutrientCode {get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerNutrientName {get; set; }

        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

		//CFCNutrient - units
		[DataReaderColumnMapping(isOptional: true)]
		public String Unit { get; set; }

		//AnalyzerNutrient - units
		[DataReaderColumnMapping(isOptional: true)]
		public String UnitType { get; set; }
			

        #endregion
    }
}


