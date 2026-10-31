using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[XmlRoot("Transaction")]
	public class NutriQuestXmlHeaderInfo
	{
		[XmlElement("LaboratoryName")]
		public String LaboratoryName { get; set; }

		[XmlElement("ReportDate")]
		public String ReportDate { get; set; }

		[XmlElement("MessageType")]
		public String MessageType { get; set; }

		[XmlElement("MessageVersion")]
		public String MessageVersion { get; set; }

		[XmlElement("Sample")]
		public List<NutriQuestXmlSampleInfo> Sample { get; set; }
	}
}
