using System;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleAnalysisRequestFormInfo
    {
        #region Properties

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime SampleDate { get; set; }        

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime CreatedStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMMaterialID { get; set; }
               
        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 MaterialTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ReportID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 DataSourceTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceTypeCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime FromDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime ToDate { get; set; }
                
        [DataReaderColumnMapping(isOptional: true)]
        public String FromDateString { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ToDateString { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Boolean UseSampleDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AnalysisString { get; set; }

        [DataReaderColumnMapping(isOptional: true)]             //Task1053 DB250814
        public Int32 SupplierID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]             //Task1053 DB250814
        public String Supplier { get; set; }

        [DataReaderColumnMapping(isOptional: true)]             //Task1053 DB250814
        public String SupplierLocation { get; set; }

        #endregion
    }
}