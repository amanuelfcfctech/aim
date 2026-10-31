using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Test
{

    [Serializable()]
    public class TestNutrientResultInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestNutrientResultID", SqlDbType.Int)]
        public Int32 TestNutrientResultID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestID", SqlDbType.Int)]
        public Int32 TestID { get; set; }

        [DataReaderColumnMapping()]
        public String TestCode { get; set; }

        [DataReaderColumnMapping()]
        public String TestName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMNutrientID { get; set; }

        [DataReaderColumnMapping()]
        public String AIMNutrientCode { get; set; }

        [DataReaderColumnMapping()]
        public String AIMNutrientName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestGroupNameID", SqlDbType.Int)]
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 TestGroupNameID { get; set; }

        [DataReaderColumnMapping()]
        public String TestGroupName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestNutrientResultCurrentUpcharge", SqlDbType.Decimal)]
        public Decimal TestNutrientResultCurrentUpcharge { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestNutrientResultPreviousUpcharge", SqlDbType.Decimal)]
        public Decimal TestNutrientResultPreviousUpcharge { get; set; }

        [DataReaderColumnMapping()]
        public Decimal TestNutrientResultCurrentCost { get; set; }

        [DataReaderColumnMapping()]
        public Decimal TestNutrientResultPreviousCost { get; set; }

        [DataReaderColumnMapping()]
        public Decimal TestCurrentCost { get; set; }

        [DataReaderColumnMapping()]
        public Decimal TestPreviousCost { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public String UserName { get; set; }
        #endregion
    }

}
