using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AIMNutrientDriverInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMNutrientDriverID", SqlDbType.Int)]
        public Int32 AIMNutrientDriverID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@QueryTemplateID", SqlDbType.Int)]
        public Int32 QueryTemplateID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
        public Int32 AIMNutrientID { get; set; }        

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMNutrientCode", SqlDbType.NVarChar)]
        public String AIMNutrientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMNutrientName", SqlDbType.NVarChar)]
        public String AIMNutrientName { get; set; }

        

        #endregion
    }
}
