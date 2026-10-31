using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Foss
{
    public class FossSampleResultSentInfo : DataObjectBase
    {
        [DataReaderColumnMapping()]
        public Int32 FossSampleResultSentID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CustomerID", SqlDbType.Int)]
        public Int32 CustomerID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@FossSampleResultID", SqlDbType.Int)]
        public Int32 FossSampleResultID { get; set; }

        [DataReaderColumnMapping()]
        public DateTime CreatedStamp { get; set; }
    }
}