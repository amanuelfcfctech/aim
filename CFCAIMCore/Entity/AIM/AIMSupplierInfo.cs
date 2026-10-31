using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class AIMSupplierInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(ColumnName="AIMSourceID")]
        public Int32 AIMSupplierID { get; set; }

		//10/11/17
		[DataReaderColumnMapping(isOptional: true)]
		public String AIMSupplierCode { get; set; }

		[DataReaderColumnMapping(ColumnName = "SourceName", IsOptional = true)]
		public String SupplierName { get; set; }

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

        //8/22/17
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryTemplateDetailID { get; set; }

        //2/6/18
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryOperationID { get; set; }


        #endregion
    }
}

