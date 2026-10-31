using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFCAIMCore.Entity
{
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:crystal-reports:schemas:report-detail")]
    //[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:crystal-reports:schemas:report-detail", IsNullable = false)]
    public partial class FossXML0
    {
        private SectionField[] fieldField;

        //private SectionText[] textField;

        //private string sectionNumberField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Field")]
        public SectionField[] Field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }

        /// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("Text")]
        //public SectionText[] Text
        //{
        //    get
        //    {
        //        return this.textField;
        //    }
        //    set
        //    {
        //        this.textField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlAttributeAttribute()]
        //public string SectionNumber
        //{
        //    get
        //    {
        //        return this.sectionNumberField;
        //    }
        //    set
        //    {
        //        this.sectionNumberField = value;
        //    }
        //}
    }

    /// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:crystal-reports:schemas:report-detail")]
    public partial class SectionField
    {

        private string formattedValueField;

        private string valueField;

        private string nameField;

        private string fieldNameField;

        /// <remarks/>
        public string FormattedValue
        {
            get
            {
                return this.formattedValueField;
            }
            set
            {
                this.formattedValueField = value;
            }
        }

        /// <remarks/>
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FieldName
        {
            get
            {
                return this.fieldNameField;
            }
            set
            {
                this.fieldNameField = value;
            }
        }
    }

    /// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:crystal-reports:schemas:report-detail")]
    public partial class SectionText
    {

        private string textValueField;

        private string nameField;

        /// <remarks/>
        public string TextValue
        {
            get
            {
                return this.textValueField;
            }
            set
            {
                this.textValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:crystal-reports:schemas:report-detail")]
    //[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:crystal-reports:schemas:report-detail", IsNullable = false)]
    public partial class CrystalReport
    {

        private object[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        /// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("Details", typeof(CrystalReportDetails))]
        //[System.Xml.Serialization.XmlElementAttribute("ReportFooter", typeof(CrystalReportReportFooter))]
        //[System.Xml.Serialization.XmlElementAttribute("ReportHeader", typeof(CrystalReportReportHeader))]
        //[System.Xml.Serialization.XmlElementAttribute("Section", typeof(FossXML))]
        //[System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
    }

    /// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:crystal-reports:schemas:report-detail")]
    public partial class CrystalReportDetails
    {

        private FossXML0[] sectionField;

        private string levelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Section")]
        public FossXML0[] Section
        {
            get
            {
                return this.sectionField;
            }
            set
            {
                this.sectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:crystal-reports:schemas:report-detail")]
    public partial class CrystalReportReportFooter
    {

        //private FossXML[] sectionField;

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("Section")]
        //public FossXML[] Section
        //{
        //    get
        //    {
        //        return this.sectionField;
        //    }
        //    set
        //    {
        //        this.sectionField = value;
        //    }
        //}
    }

    /// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:crystal-reports:schemas:report-detail")]
    public partial class CrystalReportReportHeader
    {

        //private FossXML0[] sectionField;

        /// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("Section")]
        //public FossXML[] Section
        //{
        //    get
        //    {
        //        return this.sectionField;
        //    }
        //    set
        //    {
        //        this.sectionField = value;
        //    }
        //}
    }

    /// <remarks/>
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:crystal-reports:schemas:report-detail", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        Details,

        /// <remarks/>
        ReportFooter,

        /// <remarks/>
        ReportHeader,

        /// <remarks/>
        Section,
    }
}