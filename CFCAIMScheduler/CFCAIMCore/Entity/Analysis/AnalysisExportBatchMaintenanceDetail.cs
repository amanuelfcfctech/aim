using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class UpdateIngredientDetail : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@UpdateIngredientDetailID", SqlDbType.Int)]
		public Int32 UpdateIngredientDetailID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@UpdateIngredientHeaderID", SqlDbType.Int)]
		public Int32 UpdateIngredientHeaderID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCIngredientID", SqlDbType.Int)]
		public Int32 CFCIngredientID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCNutrientID", SqlDbType.Int)]
     	public Int32 CFCNutrientID { get; set; }
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }
		
		[DataReaderColumnMapping(isOptional: true)]
		public String CFCIngredientCode { get; set; }


		[DataReaderColumnMapping(isOptional: true)]
		public String CFCIngredientName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Status", SqlDbType.Int)]
		public Int32 Status { get; set; }	

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCNutrientCode { get; set; }

		//TODO - ASK ANDY -- SHOULD NUTRIENTCODE BE CHANGED TO ALPHA, SAME AS INGREDIENTCODE??		
		/////public String CFCNutrientCode { get; set; }
		
		[DataReaderColumnMapping(isOptional: true)]
		public String CFCNutrientName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double CurrentNutrientVal { get; set; }

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

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ProposedValue", SqlDbType.Decimal)]
		public Double ProposedValue { get; set; }
		
		[DataReaderColumnMapping(isOptional: true)]
		public Double Difference
		{
			get
			{				
				return Math.Round(Used - CurrentNutrientVal, 4);
			}
		}
		
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Override", SqlDbType.Decimal)]
		public Double Override { get; set; }

		public Double Used
		{
			get
			{
				if (Override == 0)
				{
					return Math.Round(ProposedValue, 4);
				}
				else
				{
					return Math.Round(Override, 4);
				}
			}
		}

		public String Target
		{
			get
			{
				//ToDo
				//if (IsOverlay == true)
				//{
				//	return "Plant Override";
				//}
				//else
				//{
					return "Base";
				//}
			}
		}

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Mean", SqlDbType.Decimal)]
		public Double Mean { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double Upper95Mean { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double Lower95Mean { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double Lower90Mean { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double Upper90Mean { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double MeanPlusStandardDeviation { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double MeanMinusStandardDeviation { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@StandardDeviation", SqlDbType.Decimal)]
		public Double StandardDeviation { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime CreateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCPlantID", SqlDbType.Int)]
		public Int32 CFCPlantID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String CFCPlantName { get; set; }
        
        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantCode { get; set; }
        
        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@NumberOfSamples", SqlDbType.Int)]
		public Int32 NumberOfSamples { get; set; }
		        
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsOverlay", SqlDbType.Bit)]
        public Boolean IsOverlay { get; set; }

        //5/30/18 - added for Vitapro
        [DataReaderColumnMapping(isOptional: true)]
        public String LotNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Boolean IsComputed { get; set; }

        #endregion
    }
}
