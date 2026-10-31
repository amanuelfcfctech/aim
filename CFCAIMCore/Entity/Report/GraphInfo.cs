using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class GraphInfo : DataObjectBase
    {
        #region Properties
		[DataReaderColumnMapping()]
		public String IngrendientName { get; set; }

		[DataReaderColumnMapping()]
		public String UserName { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AIMNutrientID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String ValueName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double Value { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime SampleDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String SampleCode { get; set; }

		#endregion
    }
}