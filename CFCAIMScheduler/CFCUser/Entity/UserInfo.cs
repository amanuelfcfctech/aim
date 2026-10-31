using System;
using System.Data;
using Core.Library.Data;

namespace CFCUser.Entity
{
    [Serializable()]
    public class UserInfo: DataObjectBase
    {
	    #region Properties
        [DataReaderColumnMapping()]
        public string ApplicationName { get; set; }
        
        [DataReaderColumnMapping()]
        public int CFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public string UserName { get; set; }

		[DataReaderColumnMapping()]
		public string Password { get; set; }

        [DataReaderColumnMapping()]
        public DateTime LastLoginDate { get; set; }

		[DataReaderColumnMapping()]
		public string FirstName { get; set; }

        [DataReaderColumnMapping()]
        public string LastName { get; set; }

        [DataReaderColumnMapping()]
        public bool IsActive { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public string EmailAddress { get; set; }

        //10/21/22
        [DataReaderColumnMapping(IsOptional = true),
        SqlParameterMapping("@SpecificIngredientsGroupID", SqlDbType.Int)]
        public Int32 SpecificIngredientsGroupID { get; set; }

        //11/22/22
        [DataReaderColumnMapping(IsOptional = true),
        SqlParameterMapping("@AIMOwnerLocationGroupID", SqlDbType.Int)]
        public Int32 AIMOwnerLocationGroupID { get; set; }

        //5/18/23
        [DataReaderColumnMapping(IsOptional = true),
        SqlParameterMapping("@SpecificProductsGroupID", SqlDbType.Int)]
        public Int32 SpecificProductsGroupID { get; set; }

        //8/30/23
        [DataReaderColumnMapping(IsOptional = true),
        SqlParameterMapping("@AIMSupplierGroupID", SqlDbType.Int)]
        public Int32 AIMSupplierGroupID { get; set; }

        //8/30/23
        [DataReaderColumnMapping(IsOptional = true),
        SqlParameterMapping("@AIMSupplierLocationGroupID", SqlDbType.Int)]
        public Int32 AIMSupplierLocationGroupID { get; set; }

        //10/4/23
        [DataReaderColumnMapping(IsOptional = true),
        SqlParameterMapping("@SampleTestTypeGroupID", SqlDbType.Int)]
        public Int32 SampleTestTypeGroupID { get; set; }

        //10/4/23
        [DataReaderColumnMapping()]
        public string C5UserName { get; set; }
                
        #endregion
    }
}


