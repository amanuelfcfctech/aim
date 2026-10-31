using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class LicensingConfigInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@ConfigID", SqlDbType.Int)]
		public Int32 ConfigID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CompanyName", SqlDbType.VarChar)]
		public String CompanyName { get; set; }

		
		#endregion
	}
}

