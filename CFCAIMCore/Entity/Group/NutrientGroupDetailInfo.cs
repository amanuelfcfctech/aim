using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity.Group
{
	[Serializable()]

    public class NutrientGroupDetailInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@NutrientGroupDetailID", SqlDbType.Int)]
        public Int32 NutrientGroupDetailID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@NutrientGroupID", SqlDbType.Int)]
        public Int32 NutrientGroupID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
        public Int32 AIMNutrientID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String NutrientName { get; set; }

        [DataReaderColumnMapping(isOptional:true)]
        public String Unit { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SortOrder", SqlDbType.Int)]
        public Int32 SortOrder { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }
    }
}
