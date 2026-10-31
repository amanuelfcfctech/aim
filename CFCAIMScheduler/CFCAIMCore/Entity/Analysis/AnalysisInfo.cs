using Core.Library.AdvanceMath;
using Core.Library.Data;
using System;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AnalysisInfo : DataObjectBase
    {
		private String updateExpression(String expression)
		{
            //7/15/21 - added for fatal error (Anel in Perdue was getting)
            if (expression != null)
            {          
				//1/8/18 - changed from Untrimmed to Trimmed -- Anel found this error
				expression = expression.Replace("MEAN", TrimmedMean.ToString());
				expression = expression.Replace("SD", TrimmedStandardDeviation.ToString());
				return expression;
			}

			//17/15/21
            else
            {
				return "0";
            }
		}

		#region Properties
		[DataReaderColumnMapping(isOptional: true)]
        public Int32 SampleResultID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 SampleSize { get; set; }

		//9/18/17
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 TrimmedSampleSize { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
        public Int32 NumberOfSample { get; set; }

        //2/10/17 - this is used on the Analysis screen, when an outlier row is made up of more than one sample, and the
        //		   user clicks the sample# hyperlink to select which samples to include/exclude, if the user makes
        //		   any changes, the "Include" box on the analysis form will be "modified" and changed to gray.
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ExcludedSample { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean IsInclude { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCPlantID { get; set; }
        		
		[DataReaderColumnMapping(isOptional: true)]
		public String CFCPlantCode { get; set; }
        		
		[DataReaderColumnMapping(isOptional: true)]
		public String CFCPlantName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SamplePlant { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMNutrientID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public MathTypeEnum MathTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Double SampleValue { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCIngredientID { get; set; }

        [DataReaderColumnMapping()]
        public String CFCIngredientCode { get; set; }

        [DataReaderColumnMapping()]
        public String CFCIngredientName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCNutrientID { get; set; }

        [DataReaderColumnMapping()]
        public String CFCNutrientCode { get; set; }

        [DataReaderColumnMapping()]
        public String CFCNutrientName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMOwnerLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String OwnerCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String LocationCode { get; set; }

		[DataReaderColumnMapping(ColumnName = "SourceID", IsOptional = true)]
		public Int32 SupplierID { get; set; }			

		[DataReaderColumnMapping(ColumnName = "SourceName", IsOptional = true)]
		public String SupplierName { get; set; }

		[DataReaderColumnMapping(ColumnName = "SourceLocationName", IsOptional = true)]
		public String SupplierLocationName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String LocationName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMLocationID { get; set; }
		
        [DataReaderColumnMapping(isOptional: true)]
        public String Unit { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Double CurrentNutrientVal { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleAIMID { get; set; }

		//Difference Percent
        [DataReaderColumnMapping(isOptional: true)]
        public Double Difference
        {
            get
            {
				//3/4/21
				//Double result = 0;
				Double result;

				if (CurrentNutrientVal == 0)
				{
					result = 0;
				}
				else
				{
					result = Math.Round((ProposedNutrientValue - CurrentNutrientVal) / CurrentNutrientVal * 100, 2);					
				}

				return Math.Round(result, 4);				
            }
        }

		//8/18/23
		[DataReaderColumnMapping(isOptional: true)]
		public Double DiffVal
		{
			get
			{				
				Double result;

				if (CurrentNutrientVal == 0)
				{
					result = 0;
				}
				else
				{
					result = Math.Round((ProposedNutrientValue - CurrentNutrientVal), 2);
				}

				return Math.Round(result, 4);
			}
		}

		[DataReaderColumnMapping(isOptional: true)]
		public Double ProposedNutrientValue
		{
			get
			{
				Double result = 0;

				switch (MathTypeID)
				{
					case MathTypeEnum.Default:
						switch (AIMCoreGlobal.DefaultForPropose)
						{
							case MathTypeEnum.Lower95Mean:
								result = TrimmedLower95Mean;
								break;
							case MathTypeEnum.Upper95Mean:
								result = TrimmedUpper95Mean;
								break;
							case MathTypeEnum.Lower90Mean:
								result = TrimmedLower90Mean;
								break;
							case MathTypeEnum.Upper90Mean:
								result = TrimmedUpper90Mean;
								break;
							case MathTypeEnum.Mean:
								result = TrimmedMean;
								break;
							case MathTypeEnum.MeanPlusHalfStandardDeviation:
								result = TrimmedMeanPlusHalfStandardDeviation;
								break;
							case MathTypeEnum.MeanMinusHalfStandardDeviation:
								result = TrimmedMeanMinusHalfStandardDeviation;
								break;
							case MathTypeEnum.MeanPlusOneStandardDeviation:
								result = TrimmedMeanPlusOneStandardDeviation;
								break;
							case MathTypeEnum.MeanMinusOneStandardDeviation:
								result = TrimmedMeanMinusOneStandardDeviation;
								break;
							case MathTypeEnum.Custom1:
								result = Math.Round(AdvanceMath.Evaluate(Custom1Expresstion), 4);
								break;
							case MathTypeEnum.Custom2:
								result = Math.Round(AdvanceMath.Evaluate(Custom2Expresstion), 4);
								break;
							case MathTypeEnum.Custom3:
								result = Math.Round(AdvanceMath.Evaluate(Custom3Expresstion), 4);
								break;
							case MathTypeEnum.Custom4:
								result = Math.Round(AdvanceMath.Evaluate(Custom4Expresstion), 4);
								break;
							case MathTypeEnum.Custom5:
								result = Math.Round(AdvanceMath.Evaluate(Custom5Expresstion), 4);
								break;
							case MathTypeEnum.Custom6:
								result = Math.Round(AdvanceMath.Evaluate(Custom6Expresstion), 4);
								break;
							case MathTypeEnum.Custom7:
								result = Math.Round(AdvanceMath.Evaluate(Custom7Expresstion), 4);
								break;
							case MathTypeEnum.Custom8:
								result = Math.Round(AdvanceMath.Evaluate(Custom8Expresstion), 4);
								break;
							case MathTypeEnum.Custom9:
								result = Math.Round(AdvanceMath.Evaluate(Custom9Expresstion), 4);
								break;
							case MathTypeEnum.Custom10:
								result = Math.Round(AdvanceMath.Evaluate(Custom10Expresstion), 4);
								break;
						}
						break;
					case MathTypeEnum.Lower95Mean:
						result = TrimmedLower95Mean;
						break;
					case MathTypeEnum.Upper95Mean:
						result = TrimmedUpper95Mean;
						break;
					case MathTypeEnum.Lower90Mean:
						result = TrimmedLower90Mean;
						break;
					case MathTypeEnum.Upper90Mean:
						result = TrimmedUpper90Mean;
						break;
					case MathTypeEnum.Mean:
						result = TrimmedMean;
						break;
					case MathTypeEnum.MeanPlusHalfStandardDeviation:
						result = TrimmedMeanPlusHalfStandardDeviation;
						break;
					case MathTypeEnum.MeanMinusHalfStandardDeviation:
						result = TrimmedMeanMinusHalfStandardDeviation;
						break;
					case MathTypeEnum.MeanPlusOneStandardDeviation:
						result = TrimmedMeanPlusOneStandardDeviation;
						break;
					case MathTypeEnum.MeanMinusOneStandardDeviation:
						result = TrimmedMeanMinusOneStandardDeviation;
						break;
					case MathTypeEnum.Custom1:
						result = Math.Round(AdvanceMath.Evaluate(Custom1Expresstion), 4);
						break;
					case MathTypeEnum.Custom2:
						result = Math.Round(AdvanceMath.Evaluate(Custom2Expresstion), 4);
						break;
					case MathTypeEnum.Custom3:
						result = Math.Round(AdvanceMath.Evaluate(Custom3Expresstion), 4);
						break;
					case MathTypeEnum.Custom4:
						result = Math.Round(AdvanceMath.Evaluate(Custom4Expresstion), 4);
						break;
					case MathTypeEnum.Custom5:
						result = Math.Round(AdvanceMath.Evaluate(Custom5Expresstion), 4);
						break;
					case MathTypeEnum.Custom6:
						result = Math.Round(AdvanceMath.Evaluate(Custom6Expresstion), 4);
						break;
					case MathTypeEnum.Custom7:
						result = Math.Round(AdvanceMath.Evaluate(Custom7Expresstion), 4);
						break;
					case MathTypeEnum.Custom8:
						result = Math.Round(AdvanceMath.Evaluate(Custom8Expresstion), 4);
						break;
					case MathTypeEnum.Custom9:
						result = Math.Round(AdvanceMath.Evaluate(Custom9Expresstion), 4);
						break;
					case MathTypeEnum.Custom10:
						result = Math.Round(AdvanceMath.Evaluate(Custom10Expresstion), 4);
						break;
				}
				return Math.Round(result, 4);				
			}
		}
		private String Custom1Expresstion
		{
			get
			{
				String expression = AIMCoreGlobal.Custom1Equation;
				return updateExpression(expression);
			}
		}
		private String Custom2Expresstion
		{
			get
			{
				String expression = AIMCoreGlobal.Custom2Equation;
				return updateExpression(expression); 
			}
		}
		private String Custom3Expresstion
		{
			get
			{
				String expression = AIMCoreGlobal.Custom3Equation;
				return updateExpression(expression);
			}
		}
		private String Custom4Expresstion
		{
			get
			{
				String expression = AIMCoreGlobal.Custom4Equation;
				return updateExpression(expression);
			}
		}
		private String Custom5Expresstion
		{
			get
			{
				String expression = AIMCoreGlobal.Custom5Equation;
				return updateExpression(expression);
			}
		}
		private String Custom6Expresstion
		{
			get
			{
				String expression = AIMCoreGlobal.Custom6Equation;
				return updateExpression(expression);
			}
		}
		private String Custom7Expresstion
		{
			get
			{
				String expression = AIMCoreGlobal.Custom7Equation;
				return updateExpression(expression);
			}
		}
		private String Custom8Expresstion
		{
			get
			{
				String expression = AIMCoreGlobal.Custom8Equation;
				return updateExpression(expression);
			}
		}
		private String Custom9Expresstion
		{
			get
			{
				String expression = AIMCoreGlobal.Custom9Equation;
				return updateExpression(expression);
			}
		}
		private String Custom10Expresstion
		{
			get
			{
				String expression = AIMCoreGlobal.Custom10Equation;
				return updateExpression(expression);
			}
		}

		[DataReaderColumnMapping(isOptional: true)]
        public Boolean Active { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime SampleDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public Boolean IsUntrimmedValidOutlier { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public Boolean IsTrimmedValidOutlier { get; set; }

		[DataReaderColumnMapping(isOptional: true, ColumnName = "Quartile1")]
		public Double UntrimmedFirstQuartile { get; set; }

		[DataReaderColumnMapping(isOptional: true, ColumnName = "Quartile3")]
		public Double UntrimmedThirdQuartile { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double UntrimmedMean { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double UntrimmedStandardDeviation { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double UntrimmedDetailMin { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double UntrimmedDetailMax { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double UntrimmedDetailSQRT { get; set; }

		//these 4 fields are used to set the background color on the Analysis screen
		[DataReaderColumnMapping(isOptional: true)]
		public Double LMin { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Double LMax { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Double UMin { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Double UMax { get; set; }
				
		public Double UntrimmedUpper95Mean 
		{ 
			get
			{
				return Math.Round(UntrimmedMean + (UntrimmedStandardDeviation * 1.96 / UntrimmedDetailSQRT), 4);				
			}
		}

		public Double UntrimmedLower95Mean
		{ 
			get
			{
				return Math.Round(UntrimmedMean - (UntrimmedStandardDeviation * 1.96 / UntrimmedDetailSQRT), 4);				
			}
		}

		public Double UntrimmedUpper90Mean
		{
			get
			{
				return Math.Round(UntrimmedMean + (UntrimmedStandardDeviation * 1.645 / UntrimmedDetailSQRT), 4);			

			}
		}

		public Double UntrimmedLower90Mean
		{ 
			get
			{
				return Math.Round(UntrimmedMean - (UntrimmedStandardDeviation * 1.645 / UntrimmedDetailSQRT), 4);				
			}
		}		

		public Double UntrimmedMeanPlusHalfStandardDeviation 
		{ 
			get
			{
				return Math.Round(UntrimmedMean + (UntrimmedStandardDeviation * .5), 4);				
			}
		}

		public Double UntrimmedMeanMinusHalfStandardDeviation
		{
			get
			{
				return Math.Round(UntrimmedMean - (UntrimmedStandardDeviation * .5), 4);				
			}
		}

		public Double UntrimmedMeanPlusOneStandardDeviation 
		{ 
			get
			{
				return Math.Round(UntrimmedMean + UntrimmedStandardDeviation, 4);				
			}
		}

		public Double UntrimmedMeanMinusOneStandardDeviation 
		{ 
			get
			{
				return Math.Round(UntrimmedMean - UntrimmedStandardDeviation, 4);				
			}
		}
		
		public Double UntrimmedOutlierLowerMild
		{
			get
			{
				Double outlier = Math.Round(UntrimmedFirstQuartile - 1.5 * (UntrimmedThirdQuartile - UntrimmedFirstQuartile), 4);			

				if (outlier < 0)
				{
					outlier = 0;
				}
				return outlier;
			}
		}

		public Double UntrimmedOutlierLowerExtreme
		{
			get
			{
				Double outlier = Math.Round(UntrimmedFirstQuartile - 3 * (UntrimmedThirdQuartile - UntrimmedFirstQuartile), 4);				

				if (outlier < 0)
				{
					outlier = 0;
				}
				return outlier;
			}
		}

		public Double UntrimmedOutlierUpperMild
		{
			get
			{
				Double outlier = Math.Round(UntrimmedThirdQuartile + 1.5 * (UntrimmedThirdQuartile - UntrimmedFirstQuartile), 4);

				if (outlier < 0)
				{
					outlier = 0;
				}
				return outlier;
			}
		}

		public Double UntrimmedOutlierUpperExtreme
		{
			get
			{
				Double outlier = Math.Round(UntrimmedThirdQuartile + 3 * (UntrimmedThirdQuartile - UntrimmedFirstQuartile), 4);

				if (outlier < 0)
				{
					outlier = 0;
				}
				return outlier;
			}
		}

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }
		
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 OwnerID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 OwnerLocationID { get; set; }			

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 WarningGlobalH { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 WarningMinMaxLimit { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String SampleCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double TrimmedFirstQuartile { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double TrimmedThirdQuartile { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double TrimmedMean { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double TrimmedStandardDeviation { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double TrimmedDetailMin { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double TrimmedDetailMax { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double TrimmedDetailSQRT { get; set; }

		public Double TrimmedUpper95Mean
		{
			get
			{
				return Math.Round(TrimmedMean + (TrimmedStandardDeviation * 1.96 / TrimmedDetailSQRT), 4);
			}
		}

		public Double TrimmedLower95Mean
		{
			get
			{
				return Math.Round(TrimmedMean - (TrimmedStandardDeviation * 1.96 / TrimmedDetailSQRT), 4);
			}
		}

		public Double TrimmedUpper90Mean
		{
			get
			{
				return Math.Round(TrimmedMean + (TrimmedStandardDeviation * 1.645 / TrimmedDetailSQRT), 4);
			}
		}

		public Double TrimmedLower90Mean
		{
			get
			{
				return Math.Round(TrimmedMean - (TrimmedStandardDeviation * 1.645 / TrimmedDetailSQRT), 4);
			}
		}

		public Double TrimmedMeanPlusHalfStandardDeviation
		{
			get
			{
				return Math.Round(TrimmedMean + (TrimmedStandardDeviation * .5), 4);
			}
		}

		public Double TrimmedMeanMinusHalfStandardDeviation
		{
			get
			{
				return Math.Round(TrimmedMean - (TrimmedStandardDeviation * .5), 4);
			}
		}

		public Double TrimmedMeanPlusOneStandardDeviation
		{
			get
			{
				return Math.Round(TrimmedMean + TrimmedStandardDeviation, 4);
			}
		}

		public Double TrimmedMeanMinusOneStandardDeviation
		{
			get
			{
				return Math.Round(TrimmedMean - TrimmedStandardDeviation, 4);
			}
		}

		public Double TrimmedOutlierLowerMild
		{
			get
			{
				return Math.Round(TrimmedFirstQuartile - 1.5 * (TrimmedThirdQuartile - TrimmedFirstQuartile), 4);
			}
		}

		public Double TrimmedOutlierLowerExtreme
		{
			get
			{
				return Math.Round(TrimmedFirstQuartile - 3 * (TrimmedThirdQuartile - TrimmedFirstQuartile), 4);
			}
		}

		public Double TrimmedOutlierUpperMild
		{
			get
			{
				return Math.Round(TrimmedThirdQuartile + 1.5 * (TrimmedThirdQuartile - TrimmedFirstQuartile), 4);
			}
		}

		public Double TrimmedOutlierUpperExtreme
		{
			get
			{
				return Math.Round(TrimmedThirdQuartile + 3 * (TrimmedThirdQuartile - TrimmedFirstQuartile), 4);
			}
		}

		public Double SafetyMargin
		{
			get
			{
				return Math.Round(ProposedNutrientValue - TrimmedMean,4);
			}
		}
		
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean IsOverlay { get; set; }

		//10/13/17
		[DataReaderColumnMapping(isOptional: true)]
		public String AIMSupplierCode { get; set; }

		//10/13/17
		[DataReaderColumnMapping(isOptional: true)]
		public String AIMSupplierLocationCode { get; set; }

        //3/16/20
        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceTypeName { get; set; }

        //5/7/20
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 DataSourceTypeID { get; set; }
        
        //10/2/20
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 TotalSampleSize { get; set; }

		//9/8/21
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SupplierOwnerOption { get; set; }

		



		#endregion
	}
}