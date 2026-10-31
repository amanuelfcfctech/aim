using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.OutsideLab
{
    public class OutsideLabSampleResultSentInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 OutsideLabSampleResultSentID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@APIUserID", SqlDbType.Int)]
        public Int32 APIUserID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@OutsideLabSampleResultID", SqlDbType.Int)]
        public Int32 OutsideLabSampleResultID { get; set; }

        public String ErrorMessage { get; set; }
        #endregion
    }
}
