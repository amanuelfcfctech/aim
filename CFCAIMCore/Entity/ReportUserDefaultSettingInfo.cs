using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ReportUserDefaultSettingInfo : DataObjectBase
    {
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@ReportType", SqlDbType.Int)]
        public int ReportType { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@FromDate", SqlDbType.DateTime)]
        public DateTime FromDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@ToDate", SqlDbType.DateTime)]
        public DateTime ToDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@MaterialType", SqlDbType.Int)]
        public int MaterialType { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@ExtremeOutlier", SqlDbType.Int)]
        public int ExtremeOutlier { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@MaterialGroup", SqlDbType.Int)]
        public int MaterialGroup { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@MildOutlier", SqlDbType.Int)]
        public int MildOutlier { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@SortOnNutrientCode", SqlDbType.Int)]
        public int SortOnNutrientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@SampleResult", SqlDbType.Int)]
        public int SampleResult { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@AnalyzerOrFormulation", SqlDbType.Int)]
        public int AnalyzerOrFormulation { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@QualityControl", SqlDbType.Int)]
        public int QualityControl { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@DateSampleOrCreated", SqlDbType.Int)]
        public int DateSampleOrCreated { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@OwnerID", SqlDbType.Int)]
        public int OwnerID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@OwnerLocationID", SqlDbType.Int)]
        public int OwnerLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@SupplierID", SqlDbType.Int)]
        public int SupplierID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@SupplierLocationID", SqlDbType.Int)]
        public int SupplierLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@MaterialID", SqlDbType.Int)]
        public int MaterialID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@MaterialNameID", SqlDbType.Int)]
        public int MaterialNameID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@TestTypeID", SqlDbType.Int)]
        public int TestTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@TemplateID", SqlDbType.Int)]
        public int TemplateID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@TicketNumber", SqlDbType.NVarChar, size: 100)]
        public string TicketNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@CFCUserFormID", SqlDbType.Int)]
        public int CFCUserFormID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@OutOfRangeOnly", SqlDbType.Bit)]
        public bool OutOfRangeOnly { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        [SqlParameterMapping("@ShowViolations", SqlDbType.Bit)]
        public bool ShowViolations { get; set; }

    }
}
