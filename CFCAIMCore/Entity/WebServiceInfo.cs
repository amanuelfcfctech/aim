using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class WebServiceInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@WebServiceID", SqlDbType.Int)]
		public Int32 WebServiceID { get; set; }
		
		[DataReaderColumnMapping(),
		SqlParameterMapping("@WebAddress", SqlDbType.VarChar)]
		public String WebAddress { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Name", SqlDbType.VarChar)]
		public String Name { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Token", SqlDbType.VarChar)]
		public String Token { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		#endregion
	}
}
