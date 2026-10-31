using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Group
{
    public class SampleScreenDisplayGroupDetailInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleScreenDisplayGroupDetailID", SqlDbType.Int)]
        public Int32 SampleScreenDisplayGroupDetailID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleScreenDisplayGroupID", SqlDbType.Int)]
        public Int32 SampleScreenDisplayGroupID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ControlID", SqlDbType.Int)]
        public Int32 ControlID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ControlName", SqlDbType.NVarChar)]
        public String ControlName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@UserControlName", SqlDbType.NVarChar)]
        public String UserControlName { get; set; }

        //cursor change 08/25/2026 - optional so the grid still loads against a database where
        //                          usp_SampleScreenDisplayGroupDetail_SEL_SampleScreenDisplayGroupID has not been updated yet
        [DataReaderColumnMapping(isOptional: true)]
        public String AlternateName { get; set; }

        //cursor change 08/25/2026 - display name for the Sample Screen Display grid; matches the Add Column dropdown
        //                          so the user never sees the original column name they do not recognize
        public String FieldNameDisplay
        {
            get
            {
                String name = UserControlName;

                if (AlternateName != null && AlternateName.Length > 0)
                {
                    name = AlternateName;
                }

                return name;
            }
        }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public String UserName { get; set; }

        #endregion
    }
}
