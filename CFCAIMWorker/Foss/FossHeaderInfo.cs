using System;
using System.Collections.Generic;

namespace CFCAIMWorker.Foss
{
    public class FossHeaderInfo
    {
        public Int32 RowID { get; set; }

        public DateTime AnalysisTime { get; set; }

        public String ProductName { get; set; }

        public String ProductCode { get; set; }

        public String SampleType { get; set; }

        public String SampleNumber { get; set; }

        public String Comment { get; set; }

        public String InstrumentName { get; set; }

        public String InstrumentSerialNumber { get; set; }

        public List<FossDetailRowInfo> FossDetailRow { get; set; }
    }
}
