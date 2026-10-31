using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class AIMSupplierLocationInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(ColumnName="AIMSourceLocationID")]
        public Int32 AIMSupplierLocationID { get; set; }

		[DataReaderColumnMapping(ColumnName = "AIMSourceID")]
        public Int32 AIMSupplierID { get; set; }

		//10/11/17
		[DataReaderColumnMapping(isOptional: true)]
		public String AIMSupplierLocationCode { get; set; }

		[DataReaderColumnMapping(ColumnName = "SourceLocationName", IsOptional = true)]
        public String SupplierLocationName { get; set; }

        [DataReaderColumnMapping()]
        public Boolean IsActive { get; set; }

		public String IsActiveDisplay
		{
			get
			{
				if (IsActive == true)
				{
					return "true";
				}
				else
				{
					return "";
				}
			}
		}

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        //8/23/17
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryTemplateDetailID { get; set; }        	

		//8/28/17
		[DataReaderColumnMapping(isOptional: true)]
        public String SourceName { get; set; }

        //10/12/17
        [DataReaderColumnMapping(isOptional: true)]
        public String AIMSupplierCode { get; set; }

        //2/6/18
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryOperationID { get; set; }


        #endregion
    }
}

