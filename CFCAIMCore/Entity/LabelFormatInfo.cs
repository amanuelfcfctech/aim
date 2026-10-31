using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class LabelFormatInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(IsOptional = true)]
        public Int32 LabelFormatID { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public Int32 LabelID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@MaterialTypeID", SqlDbType.Int)]
        public Int32 MaterialTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@OrderNumber", SqlDbType.Int)]
        public Int32 OrderNumber { get; set; }
                
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsBold", SqlDbType.Bit)]
        public Boolean IsBold { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@FontSize", SqlDbType.Int)]
        public Int32 FontSize { get; set; }

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
               

        #endregion
    }
}