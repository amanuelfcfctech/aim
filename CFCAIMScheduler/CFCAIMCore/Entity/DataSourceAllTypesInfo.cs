using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class DataSourceAllTypesInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping()]					
		public Int32 RowNumber { get; set; }

		[DataReaderColumnMapping()]                 
		public Int32 DataSourceTypeID { get; set; }
		
		[DataReaderColumnMapping()]                 
		public String Name { get; set; }

		[DataReaderColumnMapping()]                 
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean IsFurtherAnalyses { get; set; }

		[DataReaderColumnMapping()]                 
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping()]                 
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping()]                 
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String IsUserDefined { get; set; }

		//4/28/17
		[DataReaderColumnMapping(isOptional: true)]
		public String IsSQLDatabase { get; set; }

		#endregion
	}
}