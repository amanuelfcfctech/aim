/// <Copyright>
/// The code is generated using the TRIAL version
/// of XOM 2.0 tool, thus its a property of
/// StepUp Frameworks. It should not be used for
/// commercial or individual use under any
/// circumstances. To do so, buy the licensed
/// version and re-generate this code again.
/// (www.stepupframeworks.com)
/// </Copyright>

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CFCAIMCore.Entity
{
    [XmlRoot("CrystalReport", Namespace = "")]
    public partial class CrystalReport
    {
        [XmlElement("Details")]
        public List<Details> DetailsList { get; set; }

        [XmlText]
        public string XValue { get; set; }
    }

    [XmlRoot("Details", Namespace = "")]
    public partial class Details
    {
        [XmlAttribute("Level")]
        public string Level { get; set; }

        [XmlElement("Field")]
        public List<Field> FieldList { get; set; }

        [XmlText]
        public string XValue { get; set; }
    }

    [XmlRoot("Field", Namespace = "")]
    public partial class Field
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("FieldName")]
        public string FieldName { get; set; }

        [XmlElement("FormattedValue")]
        public List<string> FormattedValueList { get; set; }

        [XmlElement("Value")]
        public List<string> ValueList { get; set; }

        [XmlText]
        public string XValue { get; set; }
    }
}