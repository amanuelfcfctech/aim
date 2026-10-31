using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalysisViewTemplateNameInfo : DataObjectBase
	{
		#region Properties	

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalysisViewTemplateName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean IsActive { get; set; }
		#endregion
	}
}
