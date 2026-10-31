using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Test
{
    public class TestGridInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleID", SqlDbType.Int)]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleCode", SqlDbType.NVarChar)]
        public String SampleCode { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleDate", SqlDbType.DateTime)]
        public DateTime SampleDate { get; set; }

        //[DataReaderColumnMapping(),
        //SqlParameterMapping("@AnalysisDate", SqlDbType.DateTime)]
        //public DateTime AnalysisDate { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@LocationName", SqlDbType.NVarChar)]
        public String LocationName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestCode", SqlDbType.NVarChar)]
        public String TestCode { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMMaterialCode", SqlDbType.NVarChar)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@MaterialTypeName", SqlDbType.NVarChar)]
        public String MaterialTypeName { get; set; }


        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
        public Int32 AIMNutrientID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMNutrientName", SqlDbType.NVarChar)]
        public String AIMNutrientName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@NumberOfTest", SqlDbType.Int)]
        public Int32 NumberOfTest { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestCurrentCost", SqlDbType.Decimal)]
        public Decimal TestCurrentCost { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestMaterialCurrentUpcharge", SqlDbType.Decimal)]
        public Decimal TestMaterialCurrentUpcharge { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestNutrientResultCurrentUpcharge", SqlDbType.Decimal)]
        public Decimal TestNutrientResultCurrentUpcharge { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@Cost", SqlDbType.Decimal)]
        public Decimal Cost { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CostTotal", SqlDbType.Decimal)]
        public Decimal CostTotal { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestName", SqlDbType.NVarChar)]
        public String TestName { get; set; }

        #endregion
    }
}
