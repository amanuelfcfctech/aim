using System.Collections.Generic;

namespace CFCAIMCore.Entity.Adisseo
{
	public class AnalysesInfo
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<string> MaterialsCodes { get; set; }
		public List<string> ParametersCodes { get; set; }
	}
}
