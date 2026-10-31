using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Foss
{
    public class FossSentLogInfo : DataObjectBase
    {
        [DataReaderColumnMapping()]
        public String Token { get; set; }

        public Int32 FossSentLogID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CustomerID", SqlDbType.Int)]
        public Int32 CustomerID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@FossSampleID", SqlDbType.Int)]
        public Int32 FossSampleID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsSkip", SqlDbType.Bit)]
        public Boolean IsSkip { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }
    }
}