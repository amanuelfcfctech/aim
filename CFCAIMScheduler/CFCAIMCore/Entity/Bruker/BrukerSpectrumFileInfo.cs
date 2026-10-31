using System;
using System.Collections.Generic;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Bruker
{
    public class BrukerSpectrumFileInfo : DataObjectBase

    {
        [DataReaderColumnMapping()]
        public Int32 BrukerSpectrumID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@BrukerSampleID", SqlDbType.Int)]
        public Int32 BrukerSampleID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@BrukerSpectrum", SqlDbType.VarBinary)]
        public byte[] BrukerSpectrum { get; set; }
        //public List<byte> BrukerSpectrum { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        // public BrukerSpectrumFileInfo() {}

    }
}
