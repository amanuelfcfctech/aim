using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.API
{
    public class APIPendingAIMMaterialInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@APIPendingAIMMaterialID", SqlDbType.Int)]
        public Int32 APIPendingAIMMaterialID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Int32 IsActive { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }
        #endregion
    }
}