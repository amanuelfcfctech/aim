using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class UpdateIngredientLogInfo
    {
        [DataReaderColumnMapping()]
        public Int32 UpdateIngredientDetailID { get; set; }

        [DataReaderColumnMapping()]
        public String PlantCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String PlantName { get; set; }

        [DataReaderColumnMapping()]
        public String IngredientCode { get; set; }

        [DataReaderColumnMapping()]
        public String IngredientName { get; set; }

        [DataReaderColumnMapping()]
        public String NutrientCode { get; set; }

        [DataReaderColumnMapping()]
        public String NutrientName { get; set; }

        [DataReaderColumnMapping()]
        public Double BeforeNutrientValue { get; set; }

        [DataReaderColumnMapping()]
        public Double AfterNutrientValue { get; set; }

        [DataReaderColumnMapping()]
        public Double Used { get; set; }

        [DataReaderColumnMapping()]
        public DateTime CreateStamp { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public String UserName { get; set; }


    }
}
