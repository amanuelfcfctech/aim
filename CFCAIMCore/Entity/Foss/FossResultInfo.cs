using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Foss
{
    public class FossResultInfo
    {
        public String Token { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@FossSampleID", SqlDbType.Int)]
        public Int32 FossSampleID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ResultName", SqlDbType.NVarChar)]
        public String ResultName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ResultValue", SqlDbType.Decimal)]
        public Double ResultValue { get; set; }
    }
}
