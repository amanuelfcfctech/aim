using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CFCWebAPI.Entity.Foss
{
    public class FossCSVResultSentInfo : DataObjectBase
    {
        public String Token { get; set; }

        [DataReaderColumnMapping()]
        public Int32 FossCSVResultSentID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CustomerID", SqlDbType.Int)]
        public Int32 CustomerID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@FossCSVResultID", SqlDbType.Int)]
        public Int32 FossCSVResultID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsSkip", SqlDbType.Bit)]
        public Int32 IsSkip { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

    }
}