using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    public class FilesCountInfo
    {
        #region Properties
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 TotalFilesCount { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 SampleMismatchCount { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 WrongFormatCount { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 SuccessCount { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleImportAcceptCount { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleImportRejectCount { get; set; }

		#endregion
	}
}