using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ControlInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(IsOptional = true)]
		public Int32 ControlID { get; set; }

		[DataReaderColumnMapping(IsOptional = true)]
		public String ControlName { get; set; }

		[DataReaderColumnMapping(IsOptional = true)]
		public Int32 FormID { get; set; }

		[DataReaderColumnMapping(IsOptional = true)]
		public DateTime UpdateStamp { get; set; }

		//The UserControlName is the "nice" label that will show up for the user on the form
		//It's NOT the field used for the alternate name
		[DataReaderColumnMapping(IsOptional = true)]
		public String UserControlName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean AllowAlternateName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean AllowTabOrder { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean AllowFilter { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean RequiredField { get; set; }

		[DataReaderColumnMapping(IsOptional = true)]
		public Int32 LinkToControlID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Boolean AllowUserDefault { get; set; }

        #endregion
    }
}