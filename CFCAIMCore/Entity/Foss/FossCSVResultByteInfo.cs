using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Foss
{
    public class FossCSVResultByteInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@FossCSVResult", SqlDbType.VarBinary)]
        public byte[] FossCSVResult { get; set; }
    }
}
