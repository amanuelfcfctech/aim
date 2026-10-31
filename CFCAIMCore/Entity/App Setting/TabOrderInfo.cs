using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class TabOrderInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(IsOptional = true)]
        public Int32 TabOrderID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 UserID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 ControlID { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String ControlName { get; set; }

        [DataReaderColumnMapping()]
        public Int32 TabOrder { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

		//The UserControlName is the "nice" label that will show up for the user on the form
		//It's NOT the field used for the alternate name
        [DataReaderColumnMapping()]
        public String UserControlName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

		[DataReaderColumnMapping()]
		public String AlternateName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean RequiredField { get; set; }

        //9/30/22
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ShowCodeNameOrBothID { get; set; }

        #endregion
    }
}