using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleComparisonReportInfo
	{
		#region Properties

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 DataSource1 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 DataSource2 { get; set; }

		//Type1 is needed to use the correct datasource column ID name (DataSourceTypeID or DataSourceUserID) in the stp
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 Type1 { get; set; }

		//Type2 is needed to use the correct datasource column ID name (DataSourceTypeID or DataSourceUserID) in the stp
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 Type2 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AimMaterialID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AimMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AimNutrientID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
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

		[DataReaderColumnMapping(isOptional: true)]
		public Decimal ResultValue1 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Decimal ResultValue2 { get; set; }
		
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleOwnerID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMOwnerLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMSourceID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMSourceLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime FromDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime ToDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String TicketNumber { get; set; }

        //6/5/20 - default = false
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean BothMustHaveValues { get; set; }


        #endregion
    }
}