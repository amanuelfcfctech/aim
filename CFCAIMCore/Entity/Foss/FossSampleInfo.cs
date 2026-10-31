using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace CFCAIMCore.Entity.Foss
{
    public class FossSampleInfo
    {
		#region Properties
		public String Token { get; set; }

		public Int32 FossSampleID { get; set; }
		 
		[DataReaderColumnMapping(),
		SqlParameterMapping("@BatchID", SqlDbType.Int)]
		public Int32 BatchID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalysisTime", SqlDbType.DateTime )]
		public DateTime AnalysisTime { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ProductCode", SqlDbType.NVarChar)]
		public String ProductCode { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ProductName", SqlDbType.NVarChar)]
		public String ProductName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleType", SqlDbType.NVarChar)]
		public String SampleType { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleNumber", SqlDbType.NVarChar)]
		public String SampleNumber { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Comment", SqlDbType.NVarChar)]
		public String Comment { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@InstrumentName", SqlDbType.NVarChar)]
		public String InstrumentName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@InstrumentSerialNumber", SqlDbType.NVarChar)]
		public String InstrumentSerialNumber { get; set; }

		public List<FossResultInfo> FossResult { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime CreateStamp { get; set; }
		#endregion

	}
}
