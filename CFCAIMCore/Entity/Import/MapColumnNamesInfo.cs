using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class MapColumnNamesInfo : DataObjectBase
    {
        [DataReaderColumnMapping()]
        public Int32 SampleMapColumnNamesID { get; set; }

        [DataReaderColumnMapping()]
        public String TableName { get; set; }

        [DataReaderColumnMapping()]
        public Int32 MapColumnID { get; set; }

        [DataReaderColumnMapping()]
        public String MapColumnName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String Description { get; set; }

        [DataReaderColumnMapping()]
        public Int32 SortNumber { get; set; }

        [DataReaderColumnMapping()]
        public Boolean IsDetail { get; set; }

        [DataReaderColumnMapping()]
        public Boolean CanAlter { get; set; }

        [DataReaderColumnMapping()]
        public Int32 AlternateNameID { get; set; }
    }
}

