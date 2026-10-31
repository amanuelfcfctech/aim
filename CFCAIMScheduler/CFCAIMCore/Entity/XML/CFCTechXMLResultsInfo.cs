using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    [XmlRootAttribute("Results")]
    public class CFCTechXmlResultsInfo : DataObjectBase
    {
        [XmlElement("SampleID")]
        public Int32 SampleID { get; set; }

        [XmlElement("SampleResultID")]
        public Int32 SampleResultID { get; set; }

        [XmlElement("AnalyzerNutrientCode")]
        public String AnalyzerNutrientCode { get; set; }

        [XmlElement("AIMNutrientName")]
        public String AIMNutrientName { get; set; }

        [XmlElement("ResultValue")]
        public Nullable<decimal> ResultValue { get; set; }

        [XmlElement("UnitType")]
        public String UnitType { get; set; }
    }
}
