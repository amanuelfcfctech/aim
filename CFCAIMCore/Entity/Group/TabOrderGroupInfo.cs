using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Group
{
    [Serializable()]
    public class TabOrderGroupInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TabOrderGroupID", SqlDbType.Int)]
        public Int32 TabOrderGroupID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TabOrderGroupCode", SqlDbType.VarChar)]
        public String TabOrderGroupCode { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public String UserName { get; set; }

        #endregion
    }
}
