using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    [XmlRoot("Samples")]
    public class CFCTechXmlHeaderInfo
    {
        [XmlElement("ExportName")]
        public String ExportName { get; set; }

        [XmlElement("ExportDate")]
        public String ExportDate { get; set; }        

        [XmlElement("Sample")]
        public List<CFCTechXmlSampleInfo> Sample { get; set; }
    }
}
