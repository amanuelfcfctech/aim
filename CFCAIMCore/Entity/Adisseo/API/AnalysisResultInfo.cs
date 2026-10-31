using System.Collections.Generic;

namespace CFCAIMCore.Entity.Adisseo
{
	public class AnalysisResultInfo
	{
		public string Id { get; set; }
		public string AnalysisRequestId { get; set; }
		public string SampleCode { get; set; }
		public string Status { get; set; }
		public string MaterialCode { get; set; }
		public string AnalysisCode { get; set; }
		public double SubmitDate { get; set; }
		public double ResultDate { get; set; }
		public string Origin { get; set; }
		public string SpectroManufacturer { get; set; }
		public string SpectroModel { get; set; }
		public string SpectroSerialNumber { get; set; }
		public string SpectroMatch { get; set; }
		public List<ParameterResultInfo> ParameterResult { get; set; }
	}
}
