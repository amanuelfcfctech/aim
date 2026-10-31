using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ReportQueryDetailInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 ReportQueryDetailID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 ReportQueryID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping()]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping()]
        public String ReportQueryDetailField { get; set; }

        [DataReaderColumnMapping()]
        public String ReportQueryDetailAndOr { get; set; }

        [DataReaderColumnMapping()]
        public String ReportQueryDetailOperator { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ReportQueryDetailValue { get; set; }

        #endregion
    }
}