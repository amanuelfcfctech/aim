using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    // task 1155 FG 8/5/26
    //One required Sample screen field inside a required field group. Also used as the
    //result of the Sample screen validation proc (usp_SampleRequiredFieldRule_SEL_MaterialLocation),
    //which only returns the control columns - that is why the ID mapping is optional.
    [Serializable()]
    public class SampleRequiredFieldGroupDetailInfo : DataObjectBase
    {
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleRequiredFieldGroupDetailID", SqlDbType.Int)]
        public Int32 SampleRequiredFieldGroupDetailID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleRequiredFieldGroupID", SqlDbType.Int)]
        public Int32 SampleRequiredFieldGroupID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@RequiredControlID", SqlDbType.Int)]
        public Int32 RequiredControlID { get; set; }

        //Display-only columns returned by the SEL procs (not saved)
        [DataReaderColumnMapping(isOptional: true)]
        public String ControlName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserControlName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String FieldDisplayName { get; set; }

        //cursor change 08/25/2026 - optional so the grid still loads against a database where
        //                          usp_SampleRequiredFieldGroupDetail_SEL_GroupID has not been updated yet, and so the
        //                          Sample screen validation proc (which does not return it) keeps working unchanged
        [DataReaderColumnMapping(isOptional: true)]
        public String AlternateName { get; set; }

        //cursor change 08/25/2026 - display name for the Required Field grid; matches the Required Field dropdown.
        //                          FieldDisplayName is already the UserControlName/ControlName fallback from the proc,
        //                          so the alternate name is the only thing layered on top of it here
        public String FieldNameDisplay
        {
            get
            {
                String name = FieldDisplayName;

                if (AlternateName != null && AlternateName.Length > 0)
                {
                    name = AlternateName;
                }

                return name;
            }
        }

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
