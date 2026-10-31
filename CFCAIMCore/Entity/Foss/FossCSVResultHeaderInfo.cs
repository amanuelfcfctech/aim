using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Foss
{
    public class FossCSVResultHeaderInfo : DataObjectBase
    {
        public String Token { get; set; }

        [DataReaderColumnMapping()]
        public Int32 FossCSVResultID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@FossSampleID", SqlDbType.Int)]
        public Int32 FossSampleID { get; set; }
    }
}
