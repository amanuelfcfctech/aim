using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AimNutrientRefAnalyzerNutrientOrphanInfo
	{
		#region Properties
		[DataReaderColumnMapping()]
		public String Type { get; set; }

		[DataReaderColumnMapping()]
		public Int32 NutrientID { get; set; }

		[DataReaderColumnMapping()]
		public String NutrientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String NutrientName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String Units { get; set; }

		[DataReaderColumnMapping()]
		public String DataSourceName { get; set; }

		[DataReaderColumnMapping()]
		public String IsActive { get; set; }


		//4/20/21
		public String SortNutrientCode
		{
			get
			{
				char pad = ' ';
				String SortedCode = String.Empty;
				Int32 val;
				Boolean result = int.TryParse(NutrientCode, out val);
				if (result == true)     //value is a number
				{
					SortedCode = NutrientCode.PadLeft(20 - NutrientCode.Length, pad);
				}
				else
				{
					SortedCode = NutrientCode;

				}
				return SortedCode;
			}
		}

		#endregion
	}
}