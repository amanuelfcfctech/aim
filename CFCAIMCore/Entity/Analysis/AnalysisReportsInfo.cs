using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalysisReportsInfo : DataObjectBase
	{
		private Double _Mean;
		#region Properties
		//to database
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleOwnerID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMOwnerLocationID { get; set; }

		[DataReaderColumnMapping(ColumnName = "AIMSourceID", IsOptional = true)]
		public Int32 AIMSupplierID { get; set; }

		[DataReaderColumnMapping(ColumnName = "AIMSourceLocationID", IsOptional = true)]
		public Int32 AIMSupplierLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime FromDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime ToDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMMaterialID { get; set; }

		//from database
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String SampleCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime AnalysisDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime SampleDate { get; set; }


		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AnalyzerNutrientID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 WeekID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double ResultValue { get; set; }


		[DataReaderColumnMapping(isOptional: true)]
		public Double Mean
		{
			get
			{
				return Math.Round(_Mean, 2);
			}
			set
			{
				_Mean = value;
			}
		}


		[DataReaderColumnMapping(isOptional: true)]
		public Double StandardDeviation { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String OwnerName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String LocationName { get; set; }

		[DataReaderColumnMapping(ColumnName = "SourceName", IsOptional = true)]
		public String SupplierName { get; set; }

		[DataReaderColumnMapping(ColumnName = "SourceLocationName", IsOptional = true)]
		public String SupplierLocationName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCIngredientID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String CFCIngredientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String CFCIngredientName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCProductID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String CFCProductCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String CFCProductName { get; set; }

		//5/18/18
		[DataReaderColumnMapping(isOptional: true)]
		public Double NutrientValue { get; set; }

		//5/18/18
		[DataReaderColumnMapping(isOptional: true)]
		public String ProductionVersion { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String MaterialCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String MaterialName { get; set; }

		//MaterialNameTotal is used on the pivot report
		[DataReaderColumnMapping(isOptional: true)]
		public String MaterialNameTotal { get; set; }


		//4/20/21
		public String SortNutrientCode
		{
			get
			{
				char pad = ' ';
				String SortedCode = String.Empty;
				Int32 val;
				Boolean result = int.TryParse(AIMNutrientCode, out val);
				if (result == true)     //value is a number
				{
					SortedCode = AIMNutrientCode.PadLeft(20 - AIMNutrientCode.Length, pad);
				}
				else
				{
					SortedCode = AIMNutrientCode;

				}
				return SortedCode;
			}
		}
						
		public String ReportGroupHeader
		{
			get
			{
				return OwnerName + LocationName + MaterialCode;
			}
		}              

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerMaterialCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerNutrientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String Unit { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String TicketNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCPlantID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCPlantCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCPlantName { get; set; }

        //5/17/18
        [DataReaderColumnMapping(isOptional: true)]
        public String ContractNumber { get; set; }

        //5/17/18
        [DataReaderColumnMapping(isOptional: true)]
        public String PoNumber { get; set; }

        //5/17/18
        [DataReaderColumnMapping(isOptional: true)]
        public String InvoiceNumber { get; set; }

        //5/17/18
        [DataReaderColumnMapping(isOptional: true)]
        public String LoadNumber { get; set; }

        //5/17/18
        [DataReaderColumnMapping(isOptional: true)]
        public String BillOfLoad { get; set; }

        //5/17/18
        [DataReaderColumnMapping(isOptional: true)]
        public String VehicleNumber { get; set; }

        //5/17/18
        [DataReaderColumnMapping(isOptional: true)]
        public String LotNumber { get; set; }	

        [DataReaderColumnMapping(isOptional: true)]
		public Int32 QueryTemplateID { get; set; }

		//used on pivot report
		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientHeading { get; set; }

		//used on pivot report
		[DataReaderColumnMapping(isOptional: true)]
		public String UnitTypeDescription { get; set; }

		//used on pivot report
		[DataReaderColumnMapping(isOptional: true)]
		public String MinMaxRange { get; set; }
		
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 Count { get; set; }
        
        [DataReaderColumnMapping(isOptional: true)]
		public Boolean SwOutliersOnly { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean SwExtremeOutlierExclude { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public Boolean SwMildOutlierExclude { get; set; }

        //4/20/18        
        //[DataReaderColumnMapping(isOptional: true)]
		public String StringSampleDate
		//Task1178 DB260415 { get; set; }
		{
			get
			{
				String sampleDate = String.Empty;

				if (SampleDate != null )
				{
                    sampleDate = SampleDate.ToShortDateString();
                }

				return sampleDate;		 //Task1178 DB260415 
            }
		}

        //8/14/18 - used on pivot report
        //Task1178 DB260415 [DataReaderColumnMapping(isOptional: true)]
        //7/19/22 - changed to datetime - wasn't sorting correctly on pivot report
        //public String ResultAnalysisDate { get; set; }
        //Task1178 public DateTime ResultAnalysisDate

        public String ResultAnalysisDate		//Task1178
        {

            //Task1199 { get; set; }
            get
            {
                return AnalysisDate.ToString("yyyyMMdd");
            }
        }

		//5/20/22
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 DataSourceTypeID { get; set; }

		//5/20/22
		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceName { get; set; }

		//6/15/23 - added for Mountaire, user-defined pivot data rept
		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceTypeCode { get; set; }

        //Task1147 DB260427
        [DataReaderColumnMapping(isOptional: true)]
        public String SampleTypeName { get; set; }

        //Task1147 DB260428
        [DataReaderColumnMapping(isOptional: true)]
        public Double WLMin { get; set; }

        //Task1147 DB260428
        [DataReaderColumnMapping(isOptional: true)]
        public Double WLMax { get; set; }

        //Task1147 DB260428
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ResultValueFlag { get; set; }

        //Task1147 DB260428
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMNutrientID { get; set; }

        #endregion
    }
}