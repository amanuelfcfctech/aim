using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class QueryTemplateDetailNutrientInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@QueryTemplateDetailNutrientID", SqlDbType.Int)]
        public Int32 QueryTemplateDetailNutrientID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@QueryTemplateDetailID", SqlDbType.Int)]
        public Int32 QueryTemplateDetailID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
        public Int32 AIMNutrientID { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }
       
        [DataReaderColumnMapping()]
        public String UserName { get; set; }

        //6/15/22 - added for Data Source tab on advanced reports
        [DataReaderColumnMapping(IsOptional = true),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

        //10/14/22 - added for DataSourceComparison report
        [DataReaderColumnMapping(IsOptional = true),
        SqlParameterMapping("@DriverAIMNutrientID", SqlDbType.Int)]
        public Int32 DriverAIMNutrientID { get; set; }

        #endregion
    }
}


