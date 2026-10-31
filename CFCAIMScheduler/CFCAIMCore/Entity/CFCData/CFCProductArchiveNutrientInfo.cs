using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class CFCProductArchiveNutrientInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCProductArchiveNutrientID", SqlDbType.Int)]
        public Int32 CFCProductArchiveNutrientID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCProductArchiveID", SqlDbType.Int)]
        public Int32 CFCProductID { get; set; }
       
        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCNutrientID", SqlDbType.Int)]
        public Int32 CFCNutrientID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@NutrientValue", SqlDbType.Decimal)]
        public Decimal NutrientValue { get; set; }

        #endregion
    }
}


