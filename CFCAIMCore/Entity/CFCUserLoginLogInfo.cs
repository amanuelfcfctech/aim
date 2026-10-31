using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class CFCUserLoginLogInfo : DataObjectBase
    {
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserLoginLogID", SqlDbType.Int)]
        public Int32 CFCUserLoginLog { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@DatabaseConnection", SqlDbType.NVarChar)]
        public String DatabaseConnection { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCUserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime LoginDate { get; set; }
    }
}
