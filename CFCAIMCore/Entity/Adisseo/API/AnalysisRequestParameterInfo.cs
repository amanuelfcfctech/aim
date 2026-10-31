using System.Collections.Generic;

namespace CFCAIMCore.Entity.Adisseo
{
	public class AnalysisRequestParameterInfo
	{
		public string sampleCode { get; set; }
		public string materialCode { get; set; }
		public List<string> analysesCodes { get; set; }
		public string samplePresentation { get; set; }
		public string spectraFileName { get; set; }
		public string spectraFile { get; set; }
//remove		public string description { get; set; }		
		//public string origin { get; set; }
		//public string criteria1 { get; set; }
		//public string criteria2 { get; set; }
		//public int customDate { get; set; }
	}
}