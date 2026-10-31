using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFCAIMCore.Entity.Test
{
    [Serializable()]
    public class TestInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestID", SqlDbType.Int)]
        public Int32 TestID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestCode", SqlDbType.NVarChar)]
        public String TestCode { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestName", SqlDbType.NVarChar)]
        public String TestName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

        [DataReaderColumnMapping()]
        public String DataSourceTypeName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestCurrentCost", SqlDbType.Decimal)]
        public Decimal TestCurrentCost { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestPreviousCost", SqlDbType.Decimal)]
        public Decimal TestPreviousCost { get; set; }

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
