using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleImportLogDetailInfo : DataObjectBase
	{
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleImportLogDetailID", SqlDbType.Int)]
        public Int32 SampleImportLogDetailID { get; set; }

        [DataReaderColumnMapping(),
		SqlParameterMapping("@SampleImportLogID", SqlDbType.Int)]
		public Int32 SampleImportLogID { get; set; }
        		
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleID", SqlDbType.Int)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String SampleCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime SampleDate { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Message", SqlDbType.NVarChar)]
		public String Message { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Status", SqlDbType.Int)]
		public Int32 Status { get; set; }

		public String StatusDisplay
		{
			get
			{
				if (Status == 1)
				{
					return "Accept";
				}
				else if (Status == 2)
				{
					return "Reject";
				}
				else
				{
					return "";
				}
			}
		}        

        #endregion
    }
}
