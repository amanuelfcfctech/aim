using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
		SqlParameterMapping("@SampleID", SqlDbType.Int)]
        public Int32 SampleID { get; set; }
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleCode", SqlDbType.NVarChar)]
		public String SampleCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AlternateCode", SqlDbType.NVarChar)]
		public String AlternateCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
        public DateTime CreatedStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleDate", SqlDbType.Date)]
        public DateTime SampleDate { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMOwnerID", SqlDbType.Int)]
        public Int32 AIMOwnerID { get; set; }

		[DataReaderColumnMapping(ColumnName="AIMSourceID", IsOptional=true),
		SqlParameterMapping("@AIMSourceID", SqlDbType.Int)]
		public Int32 AIMSupplierID { get; set; }
		
		[DataReaderColumnMapping(ColumnName="AIMSourceLocationID", IsOptional =true),
		SqlParameterMapping("@AIMSourceLocationID", SqlDbType.Int)]
		public Int32 AIMSupplierLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ContractNumber", SqlDbType.NVarChar)]
        public String ContractNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@PoNumber", SqlDbType.NVarChar)]
        public String PoNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@InvoiceNumber", SqlDbType.NVarChar)]
        public String InvoiceNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@TicketNumber", SqlDbType.NVarChar)]
        public String TicketNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@LoadNumber", SqlDbType.NVarChar)]
        public String LoadNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@BillOfLoad", SqlDbType.NVarChar)]
        public String BillOfLoad { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@VehicleNumber", SqlDbType.NVarChar)]
		public String VehicleNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@LotNumber", SqlDbType.NVarChar)]
        public String LotNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMOwnerLocationID", SqlDbType.Int)]
        public Int32 AIMOwnerLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialCode { get; set; }
		
		[DataReaderColumnMapping(isOptional: true)]                
		public String AIMMaterialName { get; set; }	

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalysisDate", SqlDbType.DateTime)]
		public DateTime AnalysisDate { get; set; }         
    
        //5/8/17 - Change will keep 0 records from getting put into Analyzer Cross-Reference tables table.
        //[DataReaderColumnMapping(isOptional: true)]
        //public Int32 AnalyzerMaterialID { get; set; }
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AnalyzerMaterialID", SqlDbType.Int)]
        public Int32 AnalyzerMaterialID { get; set; }

        //5/8/17 - Change will keep 0 records from getting put into Analyzer Cross-Reference tables table.
        //[DataReaderColumnMapping(isOptional: true)]
        //public Int32 AnalyzerSupplierID { get; set; }
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AnalyzerSupplierID", SqlDbType.Int)]
        public Int32 AnalyzerSupplierID { get; set; }

        //5/8/17 - Change will keep 0 records from getting put into Analyzer Cross-Reference tables table.
        //[DataReaderColumnMapping(isOptional: true)]
        //public Int32 AnalyzerSupplierLocationID { get; set; }
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AnalyzerSupplierLocationID", SqlDbType.Int)]
        public Int32 AnalyzerSupplierLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerMaterialCode", SqlDbType.NVarChar)]
		public String AnalyzerMaterialCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@FurtherAnalysisID", SqlDbType.Int)]
		public Int32 FurtherAnalysisID { get; set; }

		// The DataSourceTypeID is determined using the AnalyzerMaterialID.  
		// DataSourceTypeID is on the AnalyzerMaterial table,
		// so doesn't have to be on the Sample table

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }
        
		

        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerSupplierCode", SqlDbType.NVarChar)]
		public String AnalyzerSupplierCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerSupplierName", SqlDbType.NVarChar)]
		public String AnalyzerSupplierName { get; set; }        	

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerSupplierLocationCode", SqlDbType.NVarChar)]
		public String AnalyzerSupplierLocationCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerSupplierLocationName", SqlDbType.NVarChar)]
		public String AnalyzerSupplierLocationName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]        
        public Int32 MaterialTypeID { get; set; }
                
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCProductArchiveID", SqlDbType.Int)]
        public Int32 CFCProductArchiveID { get; set; }
                
        [DataReaderColumnMapping(isOptional: true)]
        public String ProductionVersion { get; set; }
                
        [DataReaderColumnMapping(isOptional: true)]
        public DateTime ProductionDate { get; set; }

        //2/12/18
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@Notes", SqlDbType.NVarChar)]
        public String Notes { get; set; }

        //5/29/18
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ExpirationDate", SqlDbType.Date)]
        public DateTime ExpirationDate { get; set; }

        //9/8/20
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SupportingDocumentPath", SqlDbType.VarChar)]
        public String SupportingDocumentPath { get; set; }

		//1/22/21
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleTypeID", SqlDbType.Int)]
		public Int32 SampleTypeID { get; set; }

		//9/30/22
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsGround", SqlDbType.Bit)]
		public Boolean IsGround { get; set; }

		//8/17/23
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleTestTypeID", SqlDbType.Int)]
		public Int32 SampleTestTypeID { get; set; }

		//1/19/24
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@GroundUnGroundID", SqlDbType.Int)]
		public Int32 GroundUnGroundID { get; set; }

		#endregion
	}
}
