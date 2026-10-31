using System;

namespace CFCAIMCore.Entity.PNE
{
    public class PNEResultsReceivedInfo
    {
        #region Properties
        public String Token { get; set; }

        public Int32 CustomerId { get; set; }

        public Int32 PNEResultsRecievedID { get; set; }

        public Int32 AnalysisRequestId { get; set; }

        public String PNEKey { get; set; }

        public String SampleCode { get; set; }

        public String Message { get; set; }

        public String APIName { get; set; }

        public Int32 ReceivedByEnum { get; set; }

        public Int32 BrukerSpectrumID { get; set; }

        public DateTime UpdateStamp { get; set; }
        #endregion
    }
}
