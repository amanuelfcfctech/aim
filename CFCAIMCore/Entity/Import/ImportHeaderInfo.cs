using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ImportHeaderInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ImportHeaderID", SqlDbType.Int)]
		public Int32 ImportHeaderID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleCode", SqlDbType.NVarChar)]
		public String SampleCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AlternateCode", SqlDbType.NVarChar)]
		public String AlternateCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleDate", SqlDbType.DateTime)]
		public DateTime SampleDate { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleDateString", SqlDbType.NVarChar)]
		public String SampleDateString { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@InvoiceNumber", SqlDbType.NVarChar)]
		public String InvoiceNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@PONumber", SqlDbType.NVarChar)]
		public String PONumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@TicketNumber", SqlDbType.NVarChar)]
		public String TicketNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@VehicleNumber", SqlDbType.NVarChar)]
		public String VehicleNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@BillOfLading", SqlDbType.NVarChar)]
        public String BillOfLading { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ContractNumber", SqlDbType.NVarChar)]
        public String ContractNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@LoadNumber", SqlDbType.NVarChar)]
        public String LoadNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@LotNumber", SqlDbType.NVarChar)]
        public String LotNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MaterialName", SqlDbType.NVarChar)]
		public String MaterialName { get; set; }		

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@OwnerName", SqlDbType.NVarChar)]
		public String OwnerName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@OwnerLocationName", SqlDbType.NVarChar)]
		public String OwnerLocationName { get; set; }
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SourceName", SqlDbType.NVarChar)]
		public String SourceName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SourceLocationName", SqlDbType.NVarChar)]
		public String SourceLocationName { get; set; }
				
		[DataReaderColumnMapping(),
		SqlParameterMapping("@Status", SqlDbType.Int)]
		public Int32 Status { get; set; }
		
		public String StatusDisplay
		{
			get
			{
				if (Status == 1)
				{
					return "Accept";
				}
				else if (Status == 2)
				{
					return "Reject";
				}

                //Status = 3 (Cancelled)

                else if (Status == 4)
                {
                    return "Ignore";
                }
                else
				{
					return "";
				}
			}
		}

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 MessageStatus { get; set; }
		//(using enum SampleImportStatus)
		// 2=Reject
		// 3=Warning

		[DataReaderColumnMapping]
		public String Message { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@MaterialID", SqlDbType.Int)]
		public Int32 MaterialID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@OwnerID", SqlDbType.Int)]
		public Int32 OwnerID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@OwnerLocationID", SqlDbType.Int)]
		public Int32 OwnerLocationID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SourceID", SqlDbType.Int)]
		public Int32 SourceID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SourceLocationID", SqlDbType.Int)]
		public Int32 SourceLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public List<ImportDetailInfo> DetailImport { get; set; }

		[DataReaderColumnMapping()]
		public Int32 WarningCount { get; set; }

		[DataReaderColumnMapping]
		public String ImportSourceFileName { get; set; }

		[DataReaderColumnMapping()]
		public Boolean SampleCodeExists { get; set; }

		[DataReaderColumnMapping()]
		public String ExistingCode { get; set; }

		[DataReaderColumnMapping()]
		public DateTime ExistingDate { get; set; }

		[DataReaderColumnMapping()]
		public Int32 ExistingImportHeaderID { get; set; }

		//4/4/17
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 DetailDataSourceCrossRefMissingCount { get; set; }


		[DataReaderColumnMapping(isOptional: true)]
		public Int32 DetailMessageErrorTotalCount { get; set; }

		public String DetailMessageErrorTotalCountDisplay
		{
			get
			{
				if (DetailMessageErrorTotalCount > 0)
				{
					return "*";
				}				
				else
				{
					return "";
				}
			}
		}

		[DataReaderColumnMapping()]
		public Int32 AnalyzerMaterialID { get; set; }
		[DataReaderColumnMapping()]
		public Int32 AnalyzerOwnerID { get; set; }
		[DataReaderColumnMapping()]
		public Int32 AnalyzerOwnerLocationID { get; set; }
		[DataReaderColumnMapping()]
		public Int32 UserDefinedSupplierID { get; set; }
		[DataReaderColumnMapping()]
		public Int32 UserDefinedSupplierLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 XrefMaterialStatus { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 XrefOwnerStatus { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 XrefOwnerLocationStatus { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 XrefSupplierStatus { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 XrefSupplierLocationStatus { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 ActiveMaterialStatus { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 ActiveOwnerStatus { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 ActiveOwnerLocationStatus { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 ActiveSupplierStatus { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 ActiveSupplierLocationStatus { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MaterialCode", SqlDbType.NVarChar)]
		public String MaterialCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@OwnerCode", SqlDbType.NVarChar)]
		public String OwnerCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@OwnerLocationCode", SqlDbType.NVarChar)]
		public String OwnerLocationCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SourceCode", SqlDbType.NVarChar)]
		public String SourceCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SourceLocationCode", SqlDbType.NVarChar)]
		public String SourceLocationCode { get; set; }

        //3/21/18
        [DataReaderColumnMapping()]
        public Int32 ExistingSampleID { get; set; }

        //9/11/18
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@Notes", SqlDbType.NVarChar)]
        public String Notes { get; set; }

		//12/24/20
		[DataReaderColumnMapping(),
		SqlParameterMapping("SupplierLocationCombinedID", SqlDbType.Int)]
		public Int32 SupplierLocationCombinedID { get; set; }

		//8/18/22 - added for Romer imports
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UniqueSampleID", SqlDbType.NVarChar)]
		public String UniqueSampleID { get; set; }


		#endregion

	}
}
