using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ShowCodeNameOrBothInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@ShowCodeNameOrBothID", SqlDbType.Int)]
        public Int32 ShowCodeNameOrBothID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@Option", SqlDbType.VarChar)]
        public String Option { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        #endregion
    }
}
