using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AimSourceLocationGuaranteeReportInfo
	{
		#region Properties		
		[DataReaderColumnMapping()]
		public Int32 SampleID { get; set; }

        [DataReaderColumnMapping()]
		public DateTime SampleDate { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AimMaterialID { get; set; }

		[DataReaderColumnMapping(ColumnName = "AimSourceID")]
		public Int32 AimSupplierID { get; set; }

		[DataReaderColumnMapping(ColumnName = "AimSourceLocationID")]
		public Int32 AimSupplierLocationID { get; set; }

		[DataReaderColumnMapping(ColumnName = "SourceName")]
		public String SupplierName { get; set; }

		[DataReaderColumnMapping(ColumnName = "SourceLocationName")]
		public String SupplierLocationName { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping()]
		public String LoadNumber { get; set; }

        [DataReaderColumnMapping()]
        public String BillOfLoad { get; set; }

        [DataReaderColumnMapping()]
		public Int32 AimNutrientID { get; set; }

		[DataReaderColumnMapping()]
		public Double AimUsedValue { get; set; }

		[DataReaderColumnMapping()]
		public Int32 SampleResultID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AnalyzerNutrientID { get; set; }

		[DataReaderColumnMapping()]
		public Double ResultValue { get; set; }		

		[DataReaderColumnMapping()]
		public String AimMaterialName { get; set; }

		[DataReaderColumnMapping()]
		public String AimNutrientName { get; set; }

        //4/20/21
        [DataReaderColumnMapping(isOptional: true)]
        public String AimNutrientCode { get; set; }

		//4/20/21
		public String SortNutrientCode
		{
			get
			{
				char pad = ' ';
				String SortedCode = String.Empty;
				Int32 val;
				Boolean result = int.TryParse(AimNutrientCode, out val);
				if (result == true)     //value is a number
				{
					SortedCode = AimNutrientCode.PadLeft(20 - AimNutrientCode.Length, pad);
				}
				else
				{
					SortedCode = AimNutrientCode;

				}
				return SortedCode;
			}
		}


		//8/10/20
		[DataReaderColumnMapping()]
        public String OwnerLocationCode { get; set; }
        [DataReaderColumnMapping()]
        public String OwnerLocationName { get; set; }                


        [DataReaderColumnMapping()]
		public Double Min { get; set; }

		[DataReaderColumnMapping()]
		public Double Max { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime FromDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime ToDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]					
		public Int32 Flag { get; set; }

		public String FlagDisplay
		{
			get
			{				
				if (Flag == 1)
				{
					return "*";
				}				
				else
				{
					return "";
				}
			}
		}
		
		//(CURRENTCHANGE) 2/20/17 - Fields below added for Specific-Nutrients-Guarantees report

		[DataReaderColumnMapping("AIMNutrientID1", IsOptional = true)]
        public Int32 AIMNutrientID1 { get; set; }

        [DataReaderColumnMapping("AIMNutrientID2", IsOptional = true)]
        public Int32 AIMNutrientID2 { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public Double ResultValue1 { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public Double ResultValue2 { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public Double AimUsedValue1 { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public Double AimUsedValue2 { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String NutrientName1 { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String NutrientName2 { get; set; }

		//---------------------------------------------------------------
		//used in usp_Report_AIMSourceLocationGuaranteesSpecificNutrients
		[DataReaderColumnMapping(isOptional: true)]
        public Boolean Flag1 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
        public Boolean Flag2 { get; set; }
		public String Flag1Display
        {
            get
            {
                if (Flag1 == true)
                {
                    return "*";
                }
                else
                {
                    return "";
                }
            }
        }
		public String Flag2Display
        {
            get
            {
                if (Flag2 == true)
                {
                    return "*";
                }
                else
                {
                    return "";
                }
            }
        }
		//---------------------------------------------------------------

		//1/3/23
		[DataReaderColumnMapping(isOptional: true)]
		public Boolean FlagBothZero { get; set; }

		//1/3/23
		public String FlagBothZeroDisplay
		{
			get
			{
				if (FlagBothZero == true)
				{
					return "**";
				}
				else
				{
					return "";
				}
			}
		}

		//9/17/20
		[DataReaderColumnMapping(isOptional: true)]
        public String SampleCode { get; set; }

		//12/7/22 - this is the moisture column from supplier-guarantee MAINTENANCE
		[DataReaderColumnMapping()]
		public Double Moisture { get; set; }

		//12/22/22 - this is the moisture result for the SAMPLE
		[DataReaderColumnMapping()]
		public Double SampleMoisture { get; set; }		

		//1/3/23 - this is the dry matter result for the SAMPLE
		[DataReaderColumnMapping()]
		public Double SampleDryMatter { get; set; }

		//12/22/22
		[DataReaderColumnMapping(isOptional: true)]
		public Double AdjustedNutrientAmount						//This is the column labeled "Adj Moisture" on the report
		{
			get
			{
				Double result = 0;
				Double DMCurrent = 0;
				Double DMAdjusted = 0;

				//-----------------------------------------------------------
				// Moisture calc for supplier guarantee report:
				//
				// EX:
				// 13 = current moisture from sample 
				// 12 = adjusted moisture
				// 
				// 100 - 13 = 87
				// 100 - 12 = 88
				//
				// 88/87 = dry matter
				//
				// DM * nutrient amt = adjusted moisture
				//
				// when no moisture is on sample, moisture = 100 - drymatter
				//-----------------------------------------------------------

				if (FlagBothZero == true)    //(SampleMoisture == 0 && SampleDryMatter == 0)
				{
					result = ResultValue;					
				}

				if (FlagBothZero == false)
				{
					if (SampleMoisture == 0 && SampleDryMatter > 0)
					{
						DMCurrent = SampleDryMatter;		
					}
					else
					{
						DMCurrent = 100 - SampleMoisture;
					}

					DMAdjusted = 100 - Moisture;

					if (DMAdjusted != 0)
					{
						result = ResultValue * (DMCurrent / DMAdjusted);
					}
					else
					{
						result = 0;
					}
				}
				return Math.Round(result, 3);
			}			
		}

		//6/26/23
		[DataReaderColumnMapping(isOptional: true)]
		public String AdjustedOrResult { get; set; }


		////1/18/23
		//public Double DryMatterCurrent
		//{
		//	get
		//	{
		//		Double result = 0;				
		//		result = 100 - Moisture;				

		//	return Math.Round(result, 3);
		//	}
		//}

		////1/18/23
		//public Double DryMatterAdjusted
		//{
		//	get
		//	{
		//		Double result = 0;

		//		if (SampleMoisture == 0 && SampleDryMatter > 0)
		//		{
		//			result = SampleDryMatter;
		//		}
		//		else
		//		{
		//			result = 100 - SampleMoisture;
		//		}

		//		return Math.Round(result, 3);
		//	}
		//}

		#endregion
	}
}