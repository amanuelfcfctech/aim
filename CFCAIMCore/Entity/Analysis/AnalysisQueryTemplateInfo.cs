using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class AnalysisTemplateInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
		public Int32 AnalysisTemplateID { get; set; }

        [DataReaderColumnMapping()]
        public String AnalysisTemplateName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public MathTypeEnum MathType { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }
        #endregion
    }
}

