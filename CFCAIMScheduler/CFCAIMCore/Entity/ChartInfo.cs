using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ChartInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String SampleCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AlternateCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime SampleDate { get; set; }		

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMNutrientID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMNutrientID2 { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientName2 { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public Double ResultValue { get; set; }        	

		[DataReaderColumnMapping(isOptional: true)]
		public Double ResultValue2 { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Double ResultValue3 { get; set; }
        
        [DataReaderColumnMapping(isOptional: true)]
        public Double ResultValue4 { get; set; }
        
        public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
				
		public String SampleCodeFrom { get; set; }
		public String SampleCodeTo { get; set; }

        //6/28/21
		//public Int32 AIMOwnerID { get; set; }
		//public Int32 AIMOwnerLocationID { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerID { get; set; }

        //7/9/21 ----------------------------------
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerID2 { get; set; }
                
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerID3 { get; set; }
                
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerID4 { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerLocationID2 { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerLocationID3 { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerLocationID4 { get; set; }
        //-----------------------------------------

        [DataReaderColumnMapping(isOptional: true)]		
		public Int32 AIMSupplierID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMSupplierID2 { get; set; }

        //2/4/20
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMSupplierID3 { get; set; }

        //2/4/20
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMSupplierID4 { get; set; }

        //public Int32 AIMSupplierLocationID { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMSupplierLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMSupplierLocationID2 { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMSupplierLocationID3 { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMSupplierLocationID4 { get; set; }
        
        public String TicketNumber { get; set; }
                
        [DataReaderColumnMapping(isOptional: true)]                 //5/22/20 - added these 4 datasourcetypeID's
        public Int32 DataSourceTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 DataSourceTypeID2 { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 DataSourceTypeID3 { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 DataSourceTypeID4 { get; set; }

        [DataReaderColumnMapping(isOptional: true)]                 //9/8/21    1 = Supplier/SupplierLoc only
        public Int32 SupplierOwnerOption { get; set; }              //          2 = Owner/OwnerLoc only
                                                                    //          3 = Both Supplier and Owner
        #endregion
    }
}