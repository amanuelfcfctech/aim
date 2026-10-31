using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AIMSyncInfo : DataObjectBase
    {
        [DataReaderColumnMapping(isOptional: true)]
        public DateTime AIMSyncTime { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMSyncProgress { get; set; }
    }
}
