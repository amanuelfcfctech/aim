using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CFCWebAPI.Entity.Foss
{
    public class FossCSVResultReceiveInfo : DataObjectBase
    {
        #region Properties
        public String Token { get; set; }

        [DataReaderColumnMapping()]
        public Int32 FossCSVResultReceiveID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@FossCSVResultID", SqlDbType.Int)]
        public Int32 FossCSVResultID { get; set; }
        #endregion

    }
}