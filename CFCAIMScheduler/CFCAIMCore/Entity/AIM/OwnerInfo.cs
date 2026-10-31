using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class AIMOwnerInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 AIMOwnerID { get; set; }

        [DataReaderColumnMapping()]
        public String OwnerCode { get; set; }

        [DataReaderColumnMapping()]
        public String OwnerName { get; set; }

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
