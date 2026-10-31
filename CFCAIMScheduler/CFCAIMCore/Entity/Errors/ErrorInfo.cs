using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ErrorInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(IsOptional = true)]
        public Int32 RecordID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 ErrorNumber { get; set; }

        [DataReaderColumnMapping()]
        public String ErrorMessage { get; set; }

		[DataReaderColumnMapping(IsOptional = true)]
		public Int32 RecordCount { get; set; }

		[DataReaderColumnMapping(IsOptional = true)]
		public Int32 ReturnIntegerValue { get; set; }

		[DataReaderColumnMapping(IsOptional = true)]
		public Int32 ReturnSecondIntegerValue { get; set; }

		#endregion
	}
}