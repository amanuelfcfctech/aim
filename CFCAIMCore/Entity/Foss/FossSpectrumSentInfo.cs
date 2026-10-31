using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Foss
{
    public class FossSpectrumSentInfo : DataObjectBase
    {
        public String Token { get; set; }

        [DataReaderColumnMapping()]
        public Int32 FossSpectrumSentID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CustomerID", SqlDbType.Int)]
        public Int32 CustomerID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@BrukerSpectrumID", SqlDbType.Int)]
        public Int32 FossSpectrumID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsSkip", SqlDbType.Bit)]
        public Int32 IsSkip { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }
    }
}
