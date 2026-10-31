using System.Collections.Generic;
using System;

namespace CFCAIMCore.Entity.Adisseo
{
	public class MaterialInfo
	{
		public String Code { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
		public List<String> AnalysesCodes { get; set; }
		public List<String> samplePresentations { get; set; }
	}
}
