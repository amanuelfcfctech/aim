using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ExportIngredientInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ExportIngredientID", SqlDbType.Int)]
        public Int32 ExportIngredientID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ExportIngredientParentID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ExportIngredientGroupID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ExportIngredientGroupCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCIngredientID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCIngredientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCIngredientName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceTypeName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }

        #endregion
    }
}
