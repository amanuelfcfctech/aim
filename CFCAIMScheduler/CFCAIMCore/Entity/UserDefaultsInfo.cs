using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class UserDefaultsInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@UserDefaultsID", SqlDbType.Int)]
        public Int32 UserDefaultsID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
         SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ControlID", SqlDbType.Int)]
        public Int32 ControlID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@FormID", SqlDbType.Int)]
        public Int32 FormID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ValueOnOff", SqlDbType.Bit)]
        public Boolean ValueOnOff { get; set; }
                
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@UpdateUserID", SqlDbType.Int)]
        public Int32 UpdateUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ControlName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }


        #endregion
    }
}
