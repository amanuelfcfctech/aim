using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleQualityEquationInfo : DataObjectBase
    {
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("SampleQualityEquationID", SqlDbType.Int)]
        public Int32 SampleQualityEquationID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("SampleQualityEquationName", SqlDbType.NVarChar)]
        public String SampleQualityEquationName { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AnalyzerNutrientID", SqlDbType.Int)]
        public Int32 AnalyzerNutrientID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("Equation", SqlDbType.VarChar)]
        public String Equation { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }
        
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String EquationTest { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Decimal Result { get; set; }
    }
}
