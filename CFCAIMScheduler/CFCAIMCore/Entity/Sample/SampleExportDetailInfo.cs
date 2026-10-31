using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleExportDetailInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleExportDetailID", SqlDbType.Int)]
		public Int32 SampleExportDetailID { get; set; }					//--> for export from "Other", filling this with SampleResultID												   

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerNutrientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Decimal ResultValue { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String Unit { get; set; }


		//the fields below are used for Marilyn's sample export from the Sample screen

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleExportID", SqlDbType.Int)]
		public Int32 SampleExportID { get; set; }
		
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MaterialType", SqlDbType.NVarChar)]
		public String MaterialType { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 MaterialTypeID { get; set; }

		public String MaterialTypeRawOrFinished
		{
			get
			{
				if (MaterialTypeID == 1)
				{
					return "R";
				}
				else if (MaterialTypeID == 2)
				{
					return "F";
				}
				else
				{
					return "";
				}
			}
		}

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MaterialCode", SqlDbType.NVarChar)]
		public String MaterialCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MaterialName", SqlDbType.NVarChar)]
		public String MaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleDate", SqlDbType.Date)]
		public DateTime SampleDate { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleCode", SqlDbType.NVarChar)]
		public String SampleCode { get; set; }

		

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UpdateStamp", SqlDbType.DateTime)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		#endregion
	}
}
