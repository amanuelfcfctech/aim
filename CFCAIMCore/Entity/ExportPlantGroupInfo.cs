using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ExportPlantGroupInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ExportPlantGroupID", SqlDbType.Int)]
        public Int32 ExportPlantGroupID { get; set; }

        //1/25/24 - commented
        ////Parent Owner
        //[DataReaderColumnMapping(isOptional: true),
        //SqlParameterMapping("@AIMOwnerID", SqlDbType.Int)]
        //public Int32 AIMOwnerID { get; set; }
        ////Parent Owner Location
        //[DataReaderColumnMapping(isOptional: true),
        //SqlParameterMapping("@AIMOwnerLocationID", SqlDbType.Int)]
        //public Int32 AIMOwnerLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ExportPlantGroupCode { get; set; }

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


        #endregion
    }
}
