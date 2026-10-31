using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleForGraphTestInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleDate", SqlDbType.DateTime)]
        public DateTime SampleDate { get; set; }

        [DataReaderColumnMapping()]
        public Double AIMUsedValue { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientName { get; set; }
    }
}
