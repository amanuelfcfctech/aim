using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCustom.Entity.Vitapro
{
    [Serializable()]
    public class ExportInfo : DataObjectBase
    {
        [DataReaderColumnMapping()]
        public String LotNumber { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCIngredientID { get; set; }

        [DataReaderColumnMapping]
        public String CFCIngredientCode { get; set; }

        [DataReaderColumnMapping]
        public String CFCIngredientName { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCNutrientID { get; set; }

        [DataReaderColumnMapping]
        public String CFCNutrientCode { get; set; }

        [DataReaderColumnMapping]
        public String CFCNutrientName { get; set; }

        [DataReaderColumnMapping()]
        public Int32 AIMDatabaseID { get; set; }

        [DataReaderColumnMapping]
        public Double CurrentNutrientVal { get; set; }

        [DataReaderColumnMapping]
        public Double AIMUsedValue { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Boolean IsOverlay { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCPlantID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantCode { get; set; }

    }
}
