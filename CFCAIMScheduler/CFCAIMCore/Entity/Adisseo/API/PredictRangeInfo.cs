using System.Collections.Generic;

namespace CFCAIMCore.Entity.Adisseo
{
	public class PredictRangeInfo
	{
		public bool IsIn { get; set; }
		public decimal GlobalH { get; set; }
		public decimal Limit { get; set; }
		public bool RefIn { get; set; }
		public decimal RefMax { get; set; }
		public decimal RefMin { get; set; }
		public decimal Precision { get; set; }
	}
}
