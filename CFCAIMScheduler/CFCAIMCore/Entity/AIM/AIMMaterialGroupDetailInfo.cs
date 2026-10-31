using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMMaterialGroupDetailInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMaterialGroupDetailID", SqlDbType.Int)]
		public Int32 AIMMaterialGroupDetailID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMMaterialGroupHeaderID", SqlDbType.Int)]
		public Int32 AIMMaterialGroupHeaderID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
		public Int32 AIMMaterialID { get; set; }
				
		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping()]
		public String AIMMaterialCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 MaterialTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMOwnerLocationName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialGroupHeaderName { get; set; }

		#endregion
	}
}
