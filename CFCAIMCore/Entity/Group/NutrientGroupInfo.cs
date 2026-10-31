using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity.Group
{
	[Serializable()]

    public class NutrientGroupInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@NutrientGroupID", SqlDbType.Int)]
        public Int32 NutrientGroupID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@NutrientGroupName", SqlDbType.NVarChar)]
        public String NutrientGroupName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }
    }
}
