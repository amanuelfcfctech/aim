using System;
using System.Data;
using Core.Library.Data;
using Core.Library.Extensions;
using DevExpress.DocumentView;
using DevExpress.XtraBars.Docking2010.Views.Widget;
using DevExpress.XtraPrinting;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class SampleLabelInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime SampleDate { get; set; }

        [DataReaderColumnMapping()]
        public Int32 AIMSourceID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SourceName { get; set; }

        [DataReaderColumnMapping()]
        public Int32 AIMSourceLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SourceLocationName { get; set; }               

        [DataReaderColumnMapping(isOptional: true)]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping()]
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String SampleCode { get; set; }

        //Label Field First
        [DataReaderColumnMapping(isOptional: true)]
        public String TicketNumber { get; set; }

        //Label Field Last
        [DataReaderColumnMapping(isOptional: true)]
        public String LoadNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String LabelForLoadNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String LabelForTicketNumber { get; set; }


        //6/23/21 - added for Vitapro
        //---------------------------------------------------
        //captions
        [DataReaderColumnMapping(isOptional: true)]
        public String LabelForLabelLine2 { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String LabelForLabelLine3 { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String LabelForLabelLine4 { get; set; }

        //---------------------------------------------------
        //fields
        [DataReaderColumnMapping(isOptional: true)]
        public String LabelFieldLine2 { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String LabelFieldLine3 { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String LabelFieldLine4 { get; set; }


        //---------------------------------------------------
        //bold or not
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean Line1Bold { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean Line2Bold { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean Line3Bold { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean Line4Bold { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean Line5Bold { get; set; }
        //---------------------------------------------------



        #endregion
    }
}
