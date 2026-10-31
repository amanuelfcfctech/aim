using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace CFCAIMCustom.Entity.Vitapro
{
    [Serializable()]
    public class LineRefCFCPlantInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@LineRefCFCPlantID", SqlDbType.Int)]
        public Int32 LineRefCFCPlantID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@LineID", SqlDbType.Int)]
        public Int32 LineID { get; set; }
                
        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCPlantID", SqlDbType.Int)]
        public Int32 CFCPlantID { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String CFCPlantName { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String CFCPlantCode { get; set; }               

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        //5/10/19
        //[DataReaderColumnMapping(isOptional: true)]
        //public Boolean IsActive { get; set; }
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }               

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        //9/27/19
        [DataReaderColumnMapping(),
        SqlParameterMapping("@LineName", SqlDbType.VarChar)]
        public String LineName { get; set; }




    }
}
