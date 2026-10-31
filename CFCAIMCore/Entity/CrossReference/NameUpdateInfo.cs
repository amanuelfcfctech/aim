using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class NameUpdateInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@NameUpdateID", SqlDbType.Int)]
        public Int32 NameUpdateID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@FRMID", SqlDbType.Int)]
        public Int32 FRMID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String FRMCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String FRMName { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMID", SqlDbType.Int)]
        public Int32 AIMID { get; set; }               

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Boolean UpdateSwitch { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        //These 2 are used on the Product Grid
        [DataReaderColumnMapping(isOptional: true)]
        public String FRMPlant { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMOwnerLocation { get; set; }
        #endregion
    }
}

