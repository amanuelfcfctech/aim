using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ReportCommentsInfo : DataObjectBase
    {
        #region Properties
        
        [DataReaderColumnMapping(),
        SqlParameterMapping("@ReportCommentsID", SqlDbType.Int)]
        public Int32 ReportCommentsID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ReportID", SqlDbType.Int)]
        public Int32 ReportID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ReportComment1", SqlDbType.NVarChar)]
        public String ReportComment1 { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ReportComment2", SqlDbType.NVarChar)]
        public String ReportComment2 { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ReportComment3", SqlDbType.NVarChar)]
        public String ReportComment3 { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ReportComment4", SqlDbType.NVarChar)]
        public String ReportComment4 { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ReportComment5", SqlDbType.NVarChar)]
        public String ReportComment5 { get; set; }


        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }


        #endregion
    }
}