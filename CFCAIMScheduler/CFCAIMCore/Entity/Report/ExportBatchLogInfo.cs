using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ExportBatchLogInfo
    {
        [DataReaderColumnMapping()]
        public Int32 UpdateIngredientDetailID { get; set; }

        [DataReaderColumnMapping()]
        public String CFCPlantCode { get; set; }

        [DataReaderColumnMapping()]
        public String CFCPlantName { get; set; }

        [DataReaderColumnMapping()]
        public String CFCIngredientCode { get; set; }

        [DataReaderColumnMapping()]
        public String CFCIngredientName { get; set; }

        [DataReaderColumnMapping()]
        public String CFCNutrientCode { get; set; }

        [DataReaderColumnMapping()]
        public String CFCNutrientName { get; set; }

        [DataReaderColumnMapping()]
        public Double BeforeNutrientValue { get; set; }

        [DataReaderColumnMapping()]
        public Double AfterNutrientValue { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public String UserName { get; set; }

        //----------------------------------------------------------        TASK951 6/3/24 - Added for Randy Mitchell at Perdue -- for Export Log report

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime FromDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime ToDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String FromDateString { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ToDateString { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 UpdateIngredientHeaderID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UpdateName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ExportTarget { get; set; }
        
        [DataReaderColumnMapping(isOptional: true)]
        public Decimal ProposedValue { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Decimal Mean { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Decimal StandardDeviation { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Decimal Override { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Decimal UsedValue { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Decimal CurrentNutrientVal { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Boolean IsOverlay { get; set; }

    }
}
