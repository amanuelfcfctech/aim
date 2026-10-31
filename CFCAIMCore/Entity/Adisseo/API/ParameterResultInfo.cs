using System.Collections.Generic;

namespace CFCAIMCore.Entity.Adisseo
{
	public class ParameterResultInfo
	{
		public string ParameterCode { get; set; }
		public string ParameterName { get; set; }
		public string Unit { get; set; }
		public decimal Value { get; set; }
		public bool Error { get; set; }
		public PredictRangeInfo PredictRange { get; set; }
	}
}
