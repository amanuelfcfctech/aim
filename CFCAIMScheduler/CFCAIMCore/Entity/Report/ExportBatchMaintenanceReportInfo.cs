using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ExportBatchMaintenanceReportInfo
	{
		[DataReaderColumnMapping()]
		public Int32 UpdateIngredientHeaderID { get; set; }

		[DataReaderColumnMapping()]
		public String UpdateName { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping()]
		public Int32 UpdateCFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public String AimDatabaseName { get; set; }

		[DataReaderColumnMapping()]
		public String TargetOverlay { get; set; }

		[DataReaderColumnMapping()]
		public DateTime CreateDate { get; set; }

		[DataReaderColumnMapping()]
		public String CFCPlantName { get; set; }

		[DataReaderColumnMapping()]
		public String CFCPlantCode { get; set; }

		[DataReaderColumnMapping()]
		public Int32 CFCIngredientID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 CFCNutrientID { get; set; }

		[DataReaderColumnMapping()]
		public String CFCIngredientCode { get; set; }	  

		[DataReaderColumnMapping()]
		public String CFCIngredientName { get; set; }

		[DataReaderColumnMapping()]
		public String CFCNutrientCode { get; set; }

		[DataReaderColumnMapping()]
		public String CFCNutrientName { get; set; }

		[DataReaderColumnMapping()]
		public Double ProposedValue { get; set; }

		[DataReaderColumnMapping()]
		public Double Mean { get; set; }

		[DataReaderColumnMapping()]
		public Double StandardDeviation { get; set; }

		[DataReaderColumnMapping()]
		public Double Override { get; set; }

		[DataReaderColumnMapping()]
		public Double UsedValue { get; set; }

		[DataReaderColumnMapping()]
		public String UserName { get; set; }

		[DataReaderColumnMapping(IsOptional = true)]
		public Int32 NumberOfSamples { get; set; }
				
		[DataReaderColumnMapping()]
		public Int32 Status { get; set; }

		public String StatusDisplay
		{
			get
			{
				if (Status == 1)
				{
					return "Rejected";
				}
				else if (Status == 2)
				{
					return "Accepted";
				}				
				else if (Status == 3)
				{
					return "Computed";
				}				
				else if (Status == 4)
				{
					return "No Overlay";
				}

				else
				{
					return "";
				}
			}
		}
				
		[DataReaderColumnMapping(isOptional: true)]
		public String PlantName { get; set; }

		//7/6/17
		[DataReaderColumnMapping()]
		public Double CurrentNutrientVal { get; set; }

		//4/20/21
		public String SortNutrientCode
		{
			get
			{
				char pad = ' ';
				String SortedCode = String.Empty;
				Int32 val;
				Boolean result = int.TryParse(CFCNutrientCode, out val);
				if (result == true)     //value is a number
				{
					SortedCode = CFCNutrientCode.PadLeft(20 - CFCNutrientCode.Length, pad);
				}
				else
				{
					SortedCode = CFCNutrientCode;

				}
				return SortedCode;
			}
		}


	}
}
