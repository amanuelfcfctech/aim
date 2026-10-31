using Core.Library.Data;
using System;


namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class FontSizeInfo
    {

        #region Properties
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 FontSizeID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 FontSize { get; set; }

        #endregion

    }
}