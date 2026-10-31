using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AIMMaterialInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(),
		SqlParameterMapping("@MaterialTypeID", SqlDbType.Int)]
        public Int32 MaterialTypeID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMMaterialCode", SqlDbType.NVarChar)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMMaterialName", SqlDbType.NVarChar)]
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
        public String MaterialTypeName { get; set; }

		[DataReaderColumnMapping(IsOptional = true),
		SqlParameterMapping("@InitialLoad_Sw", SqlDbType.Bit)]
		public Boolean InitialLoad_Sw { get; set; }
	   
        [DataReaderColumnMapping(IsOptional = true),
        SqlParameterMapping("@AIMOwnerLocationID", SqlDbType.Int)]
        public Int32 AIMOwnerLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMOwnerLocationName { get; set; }

        //8/22/17
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryTemplateDetailID { get; set; }

        //2/6/18
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryOperationID { get; set; }


        #endregion
    }
}
