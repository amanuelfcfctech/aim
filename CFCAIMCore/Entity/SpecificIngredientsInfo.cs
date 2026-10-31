using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SpecificIngredientsInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SpecificIngredientsID", SqlDbType.Int)]
        public Int32 SpecificIngredientsID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 SpecificIngredientsGroupID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SpecificIngredientsGroupCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }

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


        #endregion
    }
}
