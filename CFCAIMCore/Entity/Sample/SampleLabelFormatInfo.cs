using Core.Library.Data;
using System;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleLabelFormatInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime SampleDate { get; set; }

        public String SampleDateFormatted
        {
            get
            { 
                return SampleDate.ToShortDateString();  
            }
        }

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


                
        //---------------------------------------------------
        //captions
        [DataReaderColumnMapping(isOptional: true)]
        public String Line1Caption { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String Line2Caption { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String Line3Caption { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String Line4Caption { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String Line5Caption { get; set; }


        //---------------------------------------------------
        //actual data
        // Task1141 DB251216 - Needed to modify the output of the procedure to handle date formatting

        //[DataReaderColumnMapping(isOptional: true)]
        //public String Line1Data { get; set; }
        //[DataReaderColumnMapping(isOptional: true)]
        //public String Line2Data { get; set; }
        //[DataReaderColumnMapping(isOptional: true)]
        //public String Line3Data { get; set; }
        //[DataReaderColumnMapping(isOptional: true)]
        //public String Line4Data { get; set; }
        //[DataReaderColumnMapping(isOptional: true)]
        //public String Line5Data { get; set; }
        
        //Task1141 DB251216 Start
        // Note:   We are taking LineXDataUnformatted from the database, then formatting the string IF it is a date
        //         Then storing it into the LineXData field. 

        [DataReaderColumnMapping(isOptional: true)]                 
        public String Line1DataUnformatted { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String Line2DataUnformatted { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String Line3DataUnformatted { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String Line4DataUnformatted { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String Line5DataUnformatted { get; set; }             

        public String Line1Data
        {
            get
            {
                String dataFormat = Line1DataUnformatted;
                DateTime dateTime = DateTime.Now;
                DateTime.TryParse(Line1DataUnformatted, out dateTime);
                if (dateTime != DateTime.MinValue)
                {
                    dataFormat = dateTime.ToShortDateString();
                }

                return dataFormat;
            }
        }

        public String Line2Data
        {
            get
            {
                String dataFormat = Line2DataUnformatted;
                DateTime dateTime = DateTime.Now;
                DateTime.TryParse(Line2DataUnformatted, out dateTime);
                if (dateTime != DateTime.MinValue)
                {
                    dataFormat = dateTime.ToShortDateString();
                }

                return dataFormat;
            }
        }

        public String Line3Data
        {
            get
            {
                String dataFormat = Line3DataUnformatted;
                DateTime dateTime = DateTime.Now;
                DateTime.TryParse(Line3DataUnformatted, out dateTime);
                if (dateTime != DateTime.MinValue)
                {
                    dataFormat = dateTime.ToShortDateString();
                }

                return dataFormat;
            }
        }

        public String Line4Data
        {
            get
            {
                String dataFormat = Line4DataUnformatted;
                DateTime dateTime = DateTime.Now;
                DateTime.TryParse(Line4DataUnformatted, out dateTime);
                if (dateTime != DateTime.MinValue)
                {
                    dataFormat = dateTime.ToShortDateString();
                }

                return dataFormat;
            }
        }

        public String Line5Data
        {
            get
            {
                String dataFormat = Line5DataUnformatted;
                DateTime dateTime = DateTime.Now;
                DateTime.TryParse(Line5DataUnformatted, out dateTime);
                if (dateTime != DateTime.MinValue)
                {
                    dataFormat = dateTime.ToShortDateString();
                }

                return dataFormat;
            }
        }               
        //Task1141 DB251216 End

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
        //size of font on the label Task1058 MW250515
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 Line1Size { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 Line2Size { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 Line3Size { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 Line4Size { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 Line5Size { get; set; }
        //---------------------------------------------------

        #endregion
    }
}
