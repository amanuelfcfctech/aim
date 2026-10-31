using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class QueryOperationSampleDateInfo : DataObjectBase
	{

		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@QueryOperationID", SqlDbType.Int)]
		public Int32 QueryOperationID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DateValue", SqlDbType.DateTime)]
		public DateTime DateValue { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SecondDateValue", SqlDbType.DateTime)]
		public DateTime SecondDateValue { get; set; }
		#endregion
	}
}

