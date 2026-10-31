using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ProgressBarInfo : DataObjectBase
	{
		[DataReaderColumnMapping(),
		SqlParameterMapping("@ProgressBarID", SqlDbType.Int)]
		public Int32 ProgressBarID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ProgressBarValue", SqlDbType.Int)]
		public Int32 ProgressBarValue { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ProgressBarDescription", SqlDbType.VarChar)]
		public String ProgressBarDescription { get; set; }
				
		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }
				
		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ProgressBarType", SqlDbType.Int)]
        public Int32 ProgressBarType { get; set; }


    }
}

