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
    public class TestMaterialInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestMaterialID", SqlDbType.Int)]
        public Int32 TestMaterialID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestID", SqlDbType.Int)]
        public Int32 TestID { get; set; }

        [DataReaderColumnMapping()]
        public String TestCode { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping()]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping()]
        public String AIMMaterialName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestMaterialCurrentUpcharge", SqlDbType.Decimal)]
        public Decimal TestMaterialCurrentUpcharge { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestMaterialPreviousUpcharge", SqlDbType.Decimal)]
        public Decimal TestMaterialPreviousUpcharge { get; set; }

        [DataReaderColumnMapping()]
        public Decimal TestMaterialCurrentCost { get; set; }

        [DataReaderColumnMapping()]
        public Decimal TestMaterialPreviousCost { get; set; }

        [DataReaderColumnMapping()]
        public Decimal TestCurrentCost { get; set; }

        [DataReaderColumnMapping()]
        public Decimal TestPreviousCost { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }
        #endregion
    }
}
