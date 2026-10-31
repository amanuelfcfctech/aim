using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.OutsideLab
{
    public class OutsideLabSentLogInfo
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 OutsideLabSentLogID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CustomerID", SqlDbType.Int)]
        public Int32 CustomerID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@OutsideLabSampleID", SqlDbType.Int)]
        public Int32 OutsideLabSampleID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@OutsideLabSampleID", SqlDbType.Bit)]
        public Boolean IsSkip { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@UpdateStamp", SqlDbType.DateTime)]
        public DateTime UpdateStamp { get; set; }

        public String ErrorMessage { get; set; }
        #endregion
    }
}
