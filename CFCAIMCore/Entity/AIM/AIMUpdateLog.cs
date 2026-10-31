using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMUpdateLogInfo : DataObjectBase
	{
		#region Properties							 

		[DataReaderColumnMapping(IsOptional = true)]
		public Int32 AIMUpdateLogID { get; set; }
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("BatchID", SqlDbType.Int)]
		public Int32 BatchID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SqlCommand", SqlDbType.VarChar)]
		public String SqlCommand { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CommandType", SqlDbType.Int)]
		public Int32 CommandType { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Message", SqlDbType.NVarChar)]
		public String Message { get; set; }
		 
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ErrorNumber", SqlDbType.Int)]
		public Int32 ErrorNumber { get; set; }		 	
		
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UpdateStamp", SqlDbType.DateTime)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }
		
		#endregion
	}
}

