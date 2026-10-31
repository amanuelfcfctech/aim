using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCustom.Entity.Vitapro
{
    [Serializable()]
    public class SampleDetailInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleResultID", SqlDbType.Int)]
        public Int32 SampleResultID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleID", SqlDbType.Int)]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping,
        SqlParameterMapping("@AnalyzerNutrientCode", SqlDbType.NVarChar)]
        public String AnalyzerNutrientCode { get; set; }

        [DataReaderColumnMapping,
        SqlParameterMapping("@ResultValue", SqlDbType.Decimal)]
        public Decimal ResultValue { get; set; }

        //[DataReaderColumnMapping(isOptional: true),
        //SqlParameterMapping("@ResultValueString", SqlDbType.NVarChar)]
        //public String ResultValueString { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@UnitType", SqlDbType.NVarChar)]
        public String UnitType { get; set; }

    }
}
