using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleQualityControlInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleQualityControlID", SqlDbType.Int)]
		public Int32 SampleQualityControlID { get; set; }
		
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleID", SqlDbType.Int)]
		public Int32 SampleID { get; set; }
		
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
		public Int32 AIMNutrientID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleWeightBegin", SqlDbType.Decimal)]
		public Decimal SampleWeightBegin { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleWeightEnd", SqlDbType.Decimal)]
		public Decimal SampleWeightEnd { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Decimal SampleWeightDifference
		{
			get
			{
				Decimal result = 0;
				{
					//result = Math.Round((ProposedNutrientValue - CurrentNutrientVal) / CurrentNutrientVal * 100, 2);
					result = Math.Round((SampleWeightEnd - SampleWeightBegin), 3);
				}

				return Math.Round(result, 3);
			}
		}

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		
		#endregion
	}
}
