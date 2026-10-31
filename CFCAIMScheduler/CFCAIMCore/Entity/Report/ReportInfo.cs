using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ReportInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 ReportID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ReportName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean IsAnalysis { get; set; }

        //4/12/18 TemplateType 0 = report doesn't use a gridControl or a pivotControl
        //        TemplateType 1 = report uses gridControl 
        //        TemplateType 2 = report uses pivotControl
        //This is used with the AnalysisViewTemplate
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 TemplateType { get; set; }

        //4/12/18 -- added so "different" reports (those w/grids or pivot controls) will display together in the list
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ReportListSortOrder { get; set; }

        //5/31/22 - added for reports that should show ONLY on advanced report screen
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean IsAdvanced { get; set; }

        //5/31/22 - used when a report allows only a certain number of datasources
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 DataSourceLimit { get; set; }

        //9/5/23
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean HasComments { get; set; }

        //9/5/23
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ReportCommentsID { get; set; }


        #endregion
    }
}