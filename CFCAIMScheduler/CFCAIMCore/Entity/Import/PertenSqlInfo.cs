using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class PertenSQLInfo : DataObjectBase
	{
		#region Properties
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleNumber", SqlDbType.NVarChar)]
		public String SampleNumber { get; set; }

		//Material
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalysisProfileName", SqlDbType.NVarChar)]
		public String AnalysisProfileName { get; set; }

		//Nutrient
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ParameterTypeName", SqlDbType.NVarChar)]
		public String ParameterTypeName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UnitTypeName", SqlDbType.NVarChar)]
		public String UnitTypeName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalysisStartTime", SqlDbType.DateTime)]
        //public String InvoiceNumber { get; set; }                                     //2/26/20 - Checkiing this entity when setting up Bruker, and found mistake
        public DateTime AnalysisStartTime { get; set; }

        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalysisEndTime", SqlDbType.DateTime)]
        //public String PONumber { get; set; }                                          //2/26/20 - Checkiing this entity when setting up Bruker, and found mistake
        public DateTime AnalysisEndTime { get; set; }

        [DataReaderColumnMapping,
		SqlParameterMapping("@ReportedResult", SqlDbType.Decimal)]
		public Decimal ReportedResult { get; set; }

        //7/19/17 (Task 445) - Result id
        [DataReaderColumnMapping,
		SqlParameterMapping("@ID", SqlDbType.UniqueIdentifier)]
		public Guid ID { get; set; }

		#endregion

	}
}
