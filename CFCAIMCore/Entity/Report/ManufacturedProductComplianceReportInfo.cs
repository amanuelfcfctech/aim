using System;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ManufacturedProductComplianceReportInfo
    {
        #region Properties	
        [DataReaderColumnMapping()]
        public String OwnerName { get; set; }

        [DataReaderColumnMapping()]
        public String LocationName { get; set; }

        [DataReaderColumnMapping()]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping()]
        public String ProductionVersion { get; set; }

        [DataReaderColumnMapping()]
        public DateTime SampleDate { get; set; }

        [DataReaderColumnMapping()]
        public String SampleCode { get; set; }
        
        [DataReaderColumnMapping()]
        public String AIMNutrientName { get; set; }
        
        [DataReaderColumnMapping()]
        public String UnitTypeDescription { get; set; }  

        //(Result Value)
        [DataReaderColumnMapping()]
        public Double AIMUsedValue { get; set; }    //--> from SampleAIM table

        [DataReaderColumnMapping()]
        public Double NutrientMin { get; set; }

        [DataReaderColumnMapping()]
        public Double NutrientMax { get; set; }

        //(Target Value)
        [DataReaderColumnMapping()]
        public Double NutrientValue { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Double Difference
        {
            get
            {
                Double result = 0;
                if (NutrientValue != 0)
                {                                       
                    if (AIMUsedValue < (NutrientValue - (NutrientValue * .10)) || AIMUsedValue > (NutrientValue + (NutrientValue * .10)) )
                    {
                        result = AIMUsedValue - NutrientValue;
                    }                    
                }
                else
                {
                    result = 0;
                }

                return Math.Round(result, 2);
            }
        }
        public String FlagTarget
        {
            get
            {               
                if (NutrientValue != 0)
                {
                    if (AIMUsedValue < (NutrientValue - (NutrientValue * .10)) || AIMUsedValue > (NutrientValue + (NutrientValue * .10)))
                    {
                        return "*";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }                
            }
        }

        public String FlagMinMax
        {
            get
            {
                if ((AIMUsedValue < NutrientMin && NutrientMin != 0)  || (AIMUsedValue > NutrientMax && NutrientMax != 0))
                {
                    return "*";
                }
                else
                {
                    return "";
                }
            }
        }

        #endregion
    }
}