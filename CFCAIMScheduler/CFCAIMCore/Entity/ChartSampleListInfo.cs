using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ChartSampleListInfo : DataObjectBase
    {
        #region Properties       

        [DataReaderColumnMapping()]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime SampleDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Double ResultValue { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMSupplierID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMSupplierLocationID { get; set; }

        #endregion
    }
}