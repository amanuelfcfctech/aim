using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{	
	[Serializable()]	
	public class NutriQuestXmlSampleInfo : DataObjectBase
	{
		//Analyzer Supplier Location Code
		[XmlElement("ArticleCode")]		
		public String ArticleCode { get; set; }

		//Analyzer Supplier Location Name
		[XmlElement("ArticleName")]
		public String ArticleName { get; set; }

		//Analyzer Supplier	Code
		[XmlElement("ManufacturerCode")]
		public String ManufacturerCode { get; set; }

		//Analyzer Supplier	Name
		[XmlElement("ManufacturerName")]
		public String ManufacturerName { get; set; }

		//Sample Code
		[XmlElement("SampleIDLab")]
		public String SampleIDLab { get; set; }

		//Alternate Code
		[XmlElement("SampleID")]
		public String SampleID { get; set; }

		//Analyzer Material
		[XmlElement("SampleTypeCode")]
		public String SampleTypeCode { get; set; }

		//Sample Date
		[XmlElement("SampleArrivalDate")]
		public String SampleArrivalDate { get; set; }

		//Analysis Date
		[XmlElement("SamplingDate")]
		public String SamplingDate { get; set; }
		
		public Int32 AIMMaterialID { get; set; }		
		public Int32 AIMSupplierID { get; set; }		
		public Int32 AIMSupplierLocationID { get; set; }

		[XmlElement("Property")]
		public List<NutriQuestXmlPropertyInfo> Property { get; set; }
												  
		


	}
}
