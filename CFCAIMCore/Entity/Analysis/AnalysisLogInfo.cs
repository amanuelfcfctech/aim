using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AnalysisLogInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 AnalysisLogID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 SampleResultID { get; set; }

        [DataReaderColumnMapping()]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public String UserName { get; set; }

        [DataReaderColumnMapping()]
        public String Activity { get; set; }
        
        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }
        #endregion
    }
}
