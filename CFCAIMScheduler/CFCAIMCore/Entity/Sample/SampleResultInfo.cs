using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class SampleResultInfo : DataObjectBase
    {
        #region Properties
		[DataReaderColumnMapping()]
		public Int32 SampleResultID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleID", SqlDbType.Int)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerNutrientID", SqlDbType.Int)]
		public Int32 AnalyzerNutrientID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerNutrientCode", SqlDbType.NVarChar)]
		public String AnalyzerNutrientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ResultValue", SqlDbType.Decimal)]
        public Decimal ResultValue { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalysisDate", SqlDbType.DateTime)]
        public DateTime AnalysisDate { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Int)]
        public Boolean IsActive { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping()]
        public Boolean IsCalculate { get; set; }			

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Unit", SqlDbType.NVarChar)]
		public String Unit { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UnitType", SqlDbType.VarChar)]
		public String UnitType { get; set; }
		
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }
				
		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceTypeName { get; set; }
				
		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AimDatabaseID", SqlDbType.Int)]
		public Int32 AimDatabaseID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 MessageOutside { get; set; }
				
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 MessageInside { get; set; }

		//6/21/16 - added to SampleResult (and removed from Sample)
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@InstrumentID", SqlDbType.Int)]
		public Int32 InstrumentID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String InstrumentDescription { get; set; }

        //7/19/17 (Task 445)
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SqlResultID", SqlDbType.NVarChar)]
        public String SqlResultID { get; set; }



        #endregion
    }
}
