using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class LabelInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(IsOptional = true)]
        public Int32 LabelID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ControlID", SqlDbType.Int)]
        public Int32 ControlID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AlternateNameID", SqlDbType.Int)]
        public Int32 AlternateNameID { get; set; }
        
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsVisible", SqlDbType.Bit)]
        public Boolean IsVisible { get; set; }
        
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@UpdateStamp", SqlDbType.DateTime)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String ControlName { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String AlternateName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String UserControlName { get; set; }

        //cursor change 08/25/2026 - display name for the Labels dropdowns; the users work with the alternate name
        //                          from Customize Field Names, so fall back to the original control name only when
        //                          the control has no alternate name
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

        

        #endregion
    }
}