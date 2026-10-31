using Core.Library.Data;
using System;
using System.Data;

namespace CFCWebAPI.Entity.Foss
{
    public class FossCSVResultInfo : DataObjectBase
    {
        public String Token { get; set; }

        [DataReaderColumnMapping()]
        public Int32 FossCSVResultID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@FossSampleID", SqlDbType.Int)]
        public Int32 FossSampleID { get; set; }

        //Task1048 DB250421     
        [DataReaderColumnMapping(),
        SqlParameterMapping("@FossPath", SqlDbType.NVarChar)]
        public String FossPath { get; set; }

        //Task1048 DB250421     
        [DataReaderColumnMapping(),
        SqlParameterMapping("@FossFileName", SqlDbType.NVarChar)]
        public String FossFileName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@FossCSVResult", SqlDbType.VarBinary)]
        public byte[] FossCSVResult { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime CreateStamp { get; set; }
    }
}