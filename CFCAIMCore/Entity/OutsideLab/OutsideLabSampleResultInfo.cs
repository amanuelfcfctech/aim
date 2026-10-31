using Core.Library.Data;
using System.Data;
using System;

namespace CFCAIMCore.Entity
{
    public class OutsideLabSampleResultInfo
    {
        #region Properties
        public String Token { get; set; }

        [DataReaderColumnMapping()]
        public Int32 OutsideLabSampleResultID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@OutsideLabSampleID", SqlDbType.Int)]
        public Int32 OutsideLabSampleID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@Method", SqlDbType.VarChar)]
        public String Method { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@MethodReference", SqlDbType.VarChar)]
        public String MethodReference { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AnalyteName", SqlDbType.VarChar)]
        public String AnalyteName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AnalyteValue", SqlDbType.Real)]
        public Double AnalyteValue { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AnalyteUnit", SqlDbType.VarChar)]
        public String AnalyteUnit { get; set; }

        public String ErrorMessage { get; set; }
        #endregion
    }
}
