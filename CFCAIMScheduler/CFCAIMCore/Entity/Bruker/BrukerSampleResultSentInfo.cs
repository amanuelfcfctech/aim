using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Bruker
{
    public class BrukerSampleResultSentInfo : DataObjectBase
    {
        public String Token { get; set; }

        [DataReaderColumnMapping()]
        public Int32 BrukerSampleResultSentID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CustomerID", SqlDbType.Int)]
        public Int32 CustomerID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@BrukerSampleResultID", SqlDbType.Int)]
        public Int32 BrukerSampleResultID { get; set; }

        [DataReaderColumnMapping()]
        public DateTime CreatedStamp { get; set; }
    }
}