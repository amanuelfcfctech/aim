using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Bruker
{
    public class BrukerSpectrumInfo : DataObjectBase
    {
        public String Token { get; set; }

        [DataReaderColumnMapping()]
        public Int32 BrukerSpectrumID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@BrukerSampleID", SqlDbType.Int)]
        public Int32 BrukerSampleID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@BrukerSpectrum", SqlDbType.VarBinary)]
        public byte[] BrukerSpectrum { get; set; }

    //    [DataReaderColumnMapping("@UpdateStamp", SqlDbType.DateTime)]
     //   public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime CreateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String PNEKey { get; set; }
    }
}
