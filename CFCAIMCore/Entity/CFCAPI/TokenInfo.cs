using System;
using System.Data;
using Core.Library.Data;
namespace CFCAIMCore.Entity.CFCAPI
{
	public class TokenInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping()]
		public String Token { get; set; }
		#endregion
	}
}
