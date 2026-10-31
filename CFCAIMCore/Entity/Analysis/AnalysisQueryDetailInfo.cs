using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AnalysisQueryDetailInfo : DataObjectBase
    {
        private String _AnalysisQueryDetailField;

        #region Properties
        [DataReaderColumnMapping()]
        public Int32 AnalysisQueryDetailID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 AnalysisTemplateID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping()]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping()]
        public String AnalysisQueryDetailField
        {
            get
            {            
                return _AnalysisQueryDetailField;
            }
            set
            {
                if (value != null)
                {
                    _AnalysisQueryDetailField = value.Trim();
                }
            }
        }

        [DataReaderColumnMapping()]
        public String AnalysisQueryDetailAndOr { get; set; }

        [DataReaderColumnMapping()]
        public String AnalysisQueryDetailOperator { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AnalysisQueryDetailValue { get; set; }

        #endregion
    }
}
