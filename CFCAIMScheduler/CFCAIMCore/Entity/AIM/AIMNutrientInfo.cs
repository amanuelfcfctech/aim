using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AIMNutrientInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
		SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
        public Int32 AIMNutrientID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMNutrientCode", SqlDbType.NVarChar)]
        public String AIMNutrientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMNutrientName", SqlDbType.NVarChar)]
        public String AIMNutrientName { get; set; }     	

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMNutrientShortName", SqlDbType.NVarChar)]
		public String AIMNutrientShortName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@MathTypeID", SqlDbType.Int)]
		public Int32 MathTypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@UnitTypeID", SqlDbType.Int)]
		public Int32 UnitTypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@PriorityAIMUsedValue", SqlDbType.Int)]
		public Int32 PriorityAIMUsedValue { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        //8/30/17
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryTemplateDetailID { get; set; }

        //2/6/18
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryOperationID { get; set; }



        #endregion
    }
}
