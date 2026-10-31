using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class CFCIngredientNutrientOverlayInfo : DataObjectBase
    {
        #region Properties
        
        [DataReaderColumnMapping()]
        public Int32 CFCIngredientNutrientOverlayID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCPlantID { get; set; }
        [DataReaderColumnMapping()]
        public Int32 CFCIngredientID { get; set; }
        [DataReaderColumnMapping()]
        public Int32 CFCNutrientID { get; set; }
        [DataReaderColumnMapping()]
        public Decimal NutrientValue { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantCode { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCIngredientCode { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String CFCIngredientName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCNutrientCode { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String CFCNutrientName { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }
               
        #endregion
    }
}

