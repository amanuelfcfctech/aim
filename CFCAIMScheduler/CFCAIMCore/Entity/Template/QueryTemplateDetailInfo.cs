using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class QueryTemplateDetailInfo : DataObjectBase
    {
		
        #region Properties
        [DataReaderColumnMapping(),
		SqlParameterMapping("@QueryTemplateDetailID", SqlDbType.Int)]
        public Int32 QueryTemplateDetailID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@QueryTemplateID", SqlDbType.Int)]
		public Int32 QueryTemplateID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@QueryFieldID", SqlDbType.Int)]
		public Int32 QueryFieldID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@QueryOperationID", SqlDbType.Int)]
		public Int32 QueryOperationID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DateValue", SqlDbType.DateTime)]
		public DateTime DateValue { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Value", SqlDbType.Int)]
		public Int32 Value { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsAnd", SqlDbType.Bit)]
		public Int32 IsAnd { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

		public String StringValue
		{
			get
			{
				if (DateValue != DateTime.MinValue)				
				{
					return "";
				}
				else
				{
					return Value.ToString();
				}
			}
		}
		public String StringDate
		{
			get
			{
				if (DateValue == DateTime.MinValue)
				{
					return "";
				}
				else
				{
					return DateValue.ToString();
				}
			}
		}

		//9/8/17
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SecondDate", SqlDbType.DateTime)]
		public DateTime SecondDate { get; set; }

		//9/8/17
		public String StringSecondDate
		{
			get
			{
				if (SecondDate == DateTime.MinValue)
				{
					return "";
				}
				else
				{
					return SecondDate.ToString();
				}
			}
		}

		//9/8/17
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SecondValue", SqlDbType.Int)]
		public Int32 SecondValue { get; set; }

		//6/1/22 - this was initially set up for Sample Results Comparison report -- but that report is currently not used.
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@OrderBy", SqlDbType.Int)]
		public Int32 OrderBy { get; set; }


		#endregion
	}
}

