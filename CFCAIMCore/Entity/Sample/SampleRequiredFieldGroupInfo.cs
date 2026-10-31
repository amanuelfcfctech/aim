using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    // task 1155 FG 8/5/26
    //Header for a required field group. The scope lives here: an ingredient group OR a
    //product group (never both) limits the materials, an owner location group limits the
    //locations. 0/NULL scope = all materials / all locations. The fields themselves are
    //SampleRequiredFieldGroupDetail rows.
    [Serializable()]
    public class SampleRequiredFieldGroupInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleRequiredFieldGroupID", SqlDbType.Int)]
        public Int32 SampleRequiredFieldGroupID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleRequiredFieldGroupCode", SqlDbType.NVarChar)]
        public String SampleRequiredFieldGroupCode { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SpecificIngredientsGroupID", SqlDbType.Int)]
        public Int32 SpecificIngredientsGroupID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SpecificProductsGroupID", SqlDbType.Int)]
        public Int32 SpecificProductsGroupID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMOwnerLocationGroupID", SqlDbType.Int)]
        public Int32 AIMOwnerLocationGroupID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }
    }
}
