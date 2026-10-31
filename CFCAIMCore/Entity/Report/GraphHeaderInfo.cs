using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class GraphHeaderInfo : DataObjectBase
    {
        #region Properties
		[DataReaderColumnMapping()]
		public String IngrendientName { get; set; }

		[DataReaderColumnMapping()]
		public String UserName { get; set; }

		[DataReaderColumnMapping()]
		public List<GraphInfo> Detail { get; set; }
        #endregion
    }
}