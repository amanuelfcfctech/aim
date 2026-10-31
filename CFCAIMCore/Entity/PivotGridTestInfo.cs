using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class PivotGridTestInfo : DataObjectBase
    {
        #region Properties       

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleCode { get; set; }
               
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerNutrientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Decimal ResultValue { get; set; }

        #endregion
    }
}

