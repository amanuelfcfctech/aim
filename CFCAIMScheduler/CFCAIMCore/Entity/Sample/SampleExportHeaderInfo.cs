using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleExportHeaderInfo : DataObjectBase
	{
		#region Properties
				
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMOwnerID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMOwnerLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMSupplierID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMSupplierLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String SampleCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime SampleDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMOwnerCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMOwnerName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMOwnerLocationCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMOwnerLocationName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMSupplierCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMSupplierName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMSupplierLocationCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMSupplierLocationName { get; set; }	  

		[DataReaderColumnMapping(isOptional: true)]
		public String AlternateCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String BillOfLoad { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String ContractNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String InvoiceNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String LoadNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String LotNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String PoNumber { get; set; }		
		
		[DataReaderColumnMapping(isOptional: true)]
		public String TicketNumber { get; set; }  			

		[DataReaderColumnMapping(isOptional: true)]
		public String VehicleNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 MaterialTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 ExportStatus { get; set; }

		public String StatusDisplay
		{
			get
			{
				if (ExportStatus == 1)
				{
					return "Accepted";
				}
				else if (ExportStatus == 2)
				{
					return "Rejected";
				}				
				else
				{
					return "";
				}
			}
		}

		[DataReaderColumnMapping(isOptional: true)]
		public List<SampleExportDetailInfo> DetailExport { get; set; }

		#endregion
	}
}
