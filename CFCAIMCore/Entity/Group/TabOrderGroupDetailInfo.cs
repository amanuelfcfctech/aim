using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Group
{
    public class TabOrderGroupDetailInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@TabOrderGroupDetailID", SqlDbType.Int)]
        public Int32 TabOrderGroupDetailID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TabOrderGroupID", SqlDbType.Int)]
        public Int32 TabOrderGroupID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ControlID", SqlDbType.Int)]
        public Int32 ControlID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ControlName", SqlDbType.NVarChar)]
        public String ControlName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@UserControlName", SqlDbType.NVarChar)]
        public String UserControlName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TabOrder", SqlDbType.Int)]
        public Int32 TabOrder { get; set; }

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
