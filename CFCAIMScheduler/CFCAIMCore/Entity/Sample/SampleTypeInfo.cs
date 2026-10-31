using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleTypeInfo : DataObjectBase
    {
                
        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleTypeID", SqlDbType.Int)]
        public Int32 SampleTypeID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleTypeName", SqlDbType.NVarChar)]
        public String SampleTypeName { get; set; }

        //[DataReaderColumnMapping(isOptional: true),
        //SqlParameterMapping("@UpdateStamp", SqlDbType.DateTime)]
        //public DateTime UpdateStamp { get; set; }              
        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }


        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }       

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }
               
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryTemplateDetailID { get; set; }
                
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryOperationID { get; set; }



    }
}
