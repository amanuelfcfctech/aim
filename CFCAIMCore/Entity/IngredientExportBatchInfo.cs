using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class IngredientExportBatchInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@BatchName", SqlDbType.Int)]
        public String BatchName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@PrintedDate", SqlDbType.Int)]
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
        SqlParameterMapping("@CreateDate", SqlDbType.Int)]
        public DateTime CreateDate { get; set; }
    }
}
