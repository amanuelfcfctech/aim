using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AnalysisViewTemplateInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 AnalysisViewTemplateID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AnalysisViewTemplateCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AnalysisViewTemplateName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AnalysisViewTemplateXML { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
        public Boolean IsActive { get; set; }

        //4/4/18 -- GridNumber refers [currently] 2 different grids -- the grid for AnalysisIngredient = 1 and the grid for ReportSampleCustom = 3
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 GridNumber { get; set; }

        //4/12/18 TemplateType 0 = report doesn't use a gridControl or a pivotControl
        //        TemplateType 1 = report uses gridControl 
        //        TemplateType 2 = report uses pivotControl
        //This is used on the Report screen
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 TemplateType { get; set; }

        //5/21/19
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 templateAIMMaterialID { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 templateAIMOwnerID { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 templateAIMOwnerLocationID { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 templateAIMSupplierID { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 templateAIMSupplierLocationID { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 templateMaterialTypeID { get; set; }

        //3/8/23
        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        #endregion
    }
}
