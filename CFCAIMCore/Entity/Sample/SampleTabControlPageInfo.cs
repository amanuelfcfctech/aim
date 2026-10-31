using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleTabControlPageInfo : DataObjectBase
    {
        #region Properties         

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleTabControlPageID", SqlDbType.Int)]
        public Int32 SampleTabControlPageID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleTabControlPageName", SqlDbType.NVarChar)]
        public String SampleTabControlPageName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TabControlName", SqlDbType.NVarChar)]
        public String TabControlName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SortOrder", SqlDbType.Int)]
        public Int32 SortOrder { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsHidden", SqlDbType.Bit)]
        public Boolean IsHidden { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        


        #endregion
    }
}
