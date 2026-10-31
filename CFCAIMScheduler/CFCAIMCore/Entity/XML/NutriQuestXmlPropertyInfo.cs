using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	[XmlRootAttribute("Property")]
	public class NutriQuestXmlPropertyInfo : DataObjectBase
	{		
		[XmlElement("PropertyID")]
		public String PropertyID { get; set; }

		[XmlElement("PropertyName")]
		public String PropertyName { get; set; }

		[XmlElement("Unit")]
		public String Unit { get; set; }

		[XmlElement("ResultN")]
		public Nullable<decimal> ResultN { get; set; }
	}
}
