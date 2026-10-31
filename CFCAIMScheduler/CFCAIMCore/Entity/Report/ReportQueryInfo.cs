using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ReportQueryInfo
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 ReportQueryID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 ReportID { get; set; }

        [DataReaderColumnMapping()]
        public String ReportQueryCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ReportQueryName { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping()]
        public Boolean IsActive { get; set; }
        #endregion
    }
}