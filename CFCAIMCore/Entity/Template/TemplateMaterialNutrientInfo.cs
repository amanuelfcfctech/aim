using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class TemplateMaterialNutrientInfo : DataObjectBase
    {

        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TemplateMaterialNutrientID", SqlDbType.Int)]
        public Int32 TemplateMaterialNutrientID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@QueryTemplateDetailID", SqlDbType.Int)]
        public Int32 QueryTemplateDetailID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@QueryTemplateID", SqlDbType.Int)]
        public Int32 QueryTemplateID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMNutrientID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public List<QueryTemplateDetailNutrientInfo> NutrientDetail { get; set; }

        //2/6/18
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryOperationID { get; set; }

        #endregion
    }
}

