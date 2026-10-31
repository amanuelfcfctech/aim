using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMWorkerTest.Entity
{
    public class QAUnitTestInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsBrukerSampleError", SqlDbType.Bit)]
        public Boolean IsBrukerSampleError { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsBrukerSampleResultError", SqlDbType.Bit)]
        public Boolean IsBrukerSampleResultError { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsBrukerSampleResultReceiveError", SqlDbType.Bit)]
        public Boolean IsBrukerSampleResultReceiveError { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsBrukerSampleResultSentError", SqlDbType.Bit)]
        public Boolean IsBrukerSampleResultSentError { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsBrukerSentLogError", SqlDbType.Bit)]
        public Boolean IsBrukerSentLogError { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsBrukerSpectrumError", SqlDbType.Bit)]
        public Boolean IsBrukerSpectrumError { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsBrukerSpectrumReceiveError", SqlDbType.Bit)]
        public Boolean IsBrukerSpectrumReceiveError { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsBrukerSpectrumSentError", SqlDbType.Bit)]
        public Boolean IsBrukerSpectrumSentError { get; set; }

    }
}
