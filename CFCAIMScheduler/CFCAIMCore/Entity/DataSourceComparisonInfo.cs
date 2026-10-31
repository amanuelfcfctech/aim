using System;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class DataSourceComparisonInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String SampleCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime SampleDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String TicketNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime ExpirationDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialCode { get; set; }			

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMOwnerID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String OwnerName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMOwnerLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String LocationName { get; set; }

		[DataReaderColumnMapping(ColumnName = "AIMSourceID", IsOptional = true)]
		public Int32 AIMSupplierID { get; set; }

		[DataReaderColumnMapping(ColumnName = "SourceName", IsOptional = true)]
		public String SupplierName { get; set; }

		[DataReaderColumnMapping(ColumnName = "AIMSourceLocationID", IsOptional = true)]
		public Int32 AIMSupplierLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMSupplierCode { get; set; }

		[DataReaderColumnMapping(ColumnName = "SourceLocationName", IsOptional = true)]
		public String SupplierLocationName { get; set; }

		//-----------------------------------------------------------------
		//Aim Nutrients for Grid
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMNutrientID1 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientCode1 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientName1 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMNutrientID2 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientCode2 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientName2 { get; set; }


		//-----------------------------------------------------------------
		//Datasource1
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 DataSourceTypeID1 { get; set; }				
		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceTypeName1 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceName1 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceName2 { get; set; }

		//-----------------------------------------------------------------
		//Datasource2
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 DataSourceTypeID2 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceTypeName2 { get; set; }

		//-----------------------------------------------------------------	
		//Nutrients for DataSource1
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 DS1_NutID { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String DS1_NutCode { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String DS1_NutName { get; set; }
				
		//-----------------------------------------------------------------
		//Nutrients for DataSource2
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 DS2_NutID { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String DS2_NutCode { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String DS2_NutName { get; set; }
		
		//-----------------------------------------------------------------
		//Nutrient Names for DataSource 1 - used on report
		[DataReaderColumnMapping(isOptional: true)]
		public String Datasource1AIMNutrientName1 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String Datasource1AIMNutrientName2 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String Datasource1AIMNutrientName3 { get; set; }

		//-----------------------------------------------------------------
		//Nutrient Names for DataSource 2 - used on report
		[DataReaderColumnMapping(isOptional: true)]
		public String Datasource2AIMNutrientName1 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String Datasource2AIMNutrientName2 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String Datasource2AIMNutrientName3 { get; set; }

		//-----------------------------------------------------------------
		//Result values for DataSource1 - used on report
		[DataReaderColumnMapping(isOptional: true)]
		public Double Datasource1Value1 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Double Datasource1Value2 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Double Datasource1Value3 { get; set; }

		//-----------------------------------------------------------------
		//Result values for DataSource2 - used on report
		[DataReaderColumnMapping(isOptional: true)]
		public Double Datasource2Value1 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Double Datasource2Value2 { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Double Datasource2Value3 { get; set; }

		//-----------------------------------------------------------------
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCPlantID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCPlantCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCPlantName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 QueryTemplateID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 QueryTemplateDetailID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean UseAnalysisDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime AnalysisDate { get; set; }

		//For Butterball, if the material is an ingredient, they want to see the SupplierLocation Name and,
		//                if the material is a product, they was to see the Material Code
		[DataReaderColumnMapping(isOptional: true)]
		public String MaterialSupplier { get; set; }

		//On the report, if Ticket# is blank, show sample code
		[DataReaderColumnMapping(isOptional: true)]
		public String SampleCodeTicketNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 MaterialTypeID { get; set; }

		//10/14/22 - added for DataSourceComparison report
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 DriverAIMNutrientID { get; set; }

		#endregion
	}
}