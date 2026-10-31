using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	public class BatchIDInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping()]
		public Int32 BatchID { get; set; }
		#endregion

	}
}
