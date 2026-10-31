using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class IngredientExportBatchDetailInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@BatchName", SqlDbType.VarChar)]
        public String BatchName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@PrintedDate", SqlDbType.DateTime)]
        public DateTime PrintedDate { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@DataMapID", SqlDbType.Int)]
        public Int32 DataMapID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@PlantOverlay", SqlDbType.Int)]
        public Int32 PlantOverlay { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CreateDate", SqlDbType.DateTime)]
        public DateTime CreateDate { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCIngredientCode", SqlDbType.VarChar)]
        public String CFCIngredientCode { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCIngredientName", SqlDbType.VarChar)]
        public String CFCIngredientName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCNutrientCode", SqlDbType.VarChar)]
        public String CFCNutrientCode { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCNutrientName", SqlDbType.VarChar)]
        public String CFCNutrientName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CurrentValue", SqlDbType.Decimal)]
        public Double CurrentValue { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@UsedValue", SqlDbType.Decimal)]
        public Double UsedValue { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@Difference", SqlDbType.Decimal)]
        public Double Difference { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@Mean", SqlDbType.Decimal)]
        public Double Mean { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@StandardDeviation", SqlDbType.Decimal)]
        public Double StandardDeviation { get; set; }
    }
}