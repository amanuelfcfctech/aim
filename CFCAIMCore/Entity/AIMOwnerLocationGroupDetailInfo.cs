using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AIMOwnerLocationGroupDetailInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMOwnerLocationGroupDetailID", SqlDbType.Int)]
        public Int32 AIMOwnerLocationGroupDetailID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMOwnerLocationGroupID", SqlDbType.Int)]
        public Int32 AIMOwnerLocationGroupID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMOwnerLocationID", SqlDbType.Int)]
        public Int32 AIMOwnerLocationID { get; set; }

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
        public String LocationCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String LocationName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMOwnerLocationGroupCode { get; set; }

        

        #endregion
    }
}
