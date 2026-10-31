using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFCAIMCore.Entity
{
    public class OutsideLabSampleInfo
    {
        #region Properties
        public String Token { get; set; }

        [DataReaderColumnMapping()]
        public Int32 OutsideLabSampleID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@BatchID", SqlDbType.Int)]
        public Int32 BatchID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@OutsideLabCode", SqlDbType.NVarChar)]
        public String OutsideLabCode { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AIMCode", SqlDbType.NVarChar)]
        public String AIMCode { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleType", SqlDbType.NVarChar)]
        public String SampleType { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

        public String ErrorMessage { get; set; }
        #endregion
    }
}
