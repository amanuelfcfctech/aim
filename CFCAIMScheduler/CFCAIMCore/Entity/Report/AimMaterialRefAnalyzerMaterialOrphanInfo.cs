using System;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class AimMaterialRefAnalyzerMaterialOrphanInfo
	{
		#region Properties
		[DataReaderColumnMapping()]
		public String Type { get; set; }

		[DataReaderColumnMapping()]
		public Int32 MaterialID { get; set; }

		[DataReaderColumnMapping()]
		public String MaterialCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String MaterialName { get; set; }
				
		[DataReaderColumnMapping()]
		public String DataSourceName { get; set; }

		[DataReaderColumnMapping()]
		public String IsActive { get; set; }
		#endregion
	}
}