using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalysisSampleResult : DataObjectBase
	{
		#region Properties
		 [DataReaderColumnMapping(),
		 SqlParameterMapping("@SampleGroupByUpdateID", SqlDbType.Int)]
		 public AnalysisGroupByForUpdateEnum SampleResultID { get; set; }
			 		 
		 [DataReaderColumnMapping(isOptional: true),
		 SqlParameterMapping("@SampleID", SqlDbType.Int)]
		 public Int32 SampleID { get; set; }

		 [DataReaderColumnMapping(isOptional: true),
		 SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
		 public Int32 AIMMaterialID { get; set; }

		 [DataReaderColumnMapping(isOptional: true),
		 SqlParameterMapping("@SampleDate", SqlDbType.Date)]
		 public DateTime SampleDate { get; set; }

		 [DataReaderColumnMapping(isOptional: true),
		  SqlParameterMapping("@EndDate", SqlDbType.Date)]
		 public DateTime EndDate { get; set; }

		 [DataReaderColumnMapping(isOptional: true),
		 SqlParameterMapping("@OwnerLocationID", SqlDbType.Int)]
		 public Int32 OwnerLocationID { get; set; }

		 [DataReaderColumnMapping(isOptional: true),
		 SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
		 public Int32 AIMNutrientID { get; set; }

		 [DataReaderColumnMapping(isOptional: true),
		 SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		 public Int32 CFCUserID { get; set; }

		 [DataReaderColumnMapping(isOptional: true),
		 SqlParameterMapping("@IsActive", SqlDbType.Bit)]
		 public Boolean IsActive { get; set; }

		#endregion

	}
}
