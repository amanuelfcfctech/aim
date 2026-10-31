using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AIMSupplierGroupDetailInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMSupplierGroupDetailID", SqlDbType.Int)]
        public Int32 AIMSupplierGroupDetailID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMSupplierGroupID", SqlDbType.Int)]
        public Int32 AIMSupplierGroupID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMSupplierID", SqlDbType.Int)]
        public Int32 AIMSupplierID { get; set; }

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
        public String AIMSupplierCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMSupplierName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMSupplierGroupCode { get; set; }



        #endregion
    }
}
