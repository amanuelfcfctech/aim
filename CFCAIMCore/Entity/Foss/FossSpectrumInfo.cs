using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Foss
{
    public class FossSpectrumInfo : DataObjectBase
    {
        public String Token { get; set; }

        [DataReaderColumnMapping()]
        public Int32 FossSpectrumID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@FossSampleID", SqlDbType.Int)]
        public Int32 FossSampleID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@FossSpectrum", SqlDbType.VarBinary)]
        public byte[] FossSpectrum { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime CreateStamp { get; set; }
    }
}