using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class CFCProductSpecInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UnitTypeDescription { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Decimal NutrientValue { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantCode { get; set; }

        #endregion
    }
}


