using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Group
{
    [Serializable()]
    public class SampleScreenDisplayGroupInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleScreenDisplayGroupID", SqlDbType.Int)]
        public Int32 SampleScreenDisplayGroupID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleScreenDisplayGroupCode", SqlDbType.VarChar)]
        public String SampleScreenDisplayGroupCode { get; set; }

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
