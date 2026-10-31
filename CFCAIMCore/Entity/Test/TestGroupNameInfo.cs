using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFCAIMCore.Entity.Test
{
    [Serializable()]
    public class TestGroupNameInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestGroupNameID", SqlDbType.Int)]
        public Int32 TestGroupNameID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestGroupName", SqlDbType.NVarChar)]
        public String TestGroupName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }
        #endregion
    }
}
