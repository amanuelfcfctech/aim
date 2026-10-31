using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class GuaranteeRefundDetailInfo : DataObjectBase
	{
		#region Properties		 

		[DataReaderColumnMapping(),
		SqlParameterMapping("@GuaranteeRefundDetailID", SqlDbType.Int)]
		public Int32 GuaranteeRefundDetailID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@GuaranteeRefundHeaderID", SqlDbType.Int)]
		public Int32 GuaranteeRefundHeaderID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleID", SqlDbType.Int)]
		public Int32 SampleID { get; set; }		

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
		public Int32 AIMNutrientID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientName { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ResultValue", SqlDbType.Decimal)]
        public Double ResultValue { get; set; }

        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@GuaranteeMin", SqlDbType.Decimal)]
		public Double GuaranteeMin { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@GuaranteeMax", SqlDbType.Decimal)]
		public Double GuaranteeMax { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double Diff
		{
			get
			{
				if (ResultValue < GuaranteeMin)
				{
					return Math.Round(ResultValue - GuaranteeMin, 4);
				}
				else if (ResultValue > GuaranteeMax && GuaranteeMax != 0)
				{
					return Math.Round(ResultValue - GuaranteeMax,4);
				}
				else
				{
					return 0;
				}				
			}
		}

		[DataReaderColumnMapping(isOptional: true)]		
		public Double GuaranteeTarget { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleCode", SqlDbType.NVarChar)]
        public String SampleCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleDate", SqlDbType.Date)]
		public DateTime SampleDate { get; set; }

		[DataReaderColumnMapping(),
	    SqlParameterMapping("@LoadNumber", SqlDbType.NVarChar)]
		public String LoadNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CalculatedValue", SqlDbType.Decimal)]
        public Decimal CalculatedValue { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@Status", SqlDbType.Int)]
        public Int32 Status { get; set; }

        public String StatusDisplay
        {
            get
            {
                if (Status == 2)
                {
                    return "Ignored";
                }
                else if (Status == 1)
                {
                    return "Claimed";
                }
                else
                {
                    return "";
                }
            }
        }

        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String SourceName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String SourceLocationName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 BatchID { get; set; }

		#endregion
	}
}
