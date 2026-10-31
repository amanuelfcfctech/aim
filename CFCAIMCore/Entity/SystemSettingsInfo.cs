using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SystemSettingsInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@SystemSettingsID", SqlDbType.Int)]
		public Int32 SystemSettingsID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
		public Int32 AIMNutrientID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AllowAIMInactive", SqlDbType.Bit)]
		public Boolean AllowAIMInactive { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ShowAnalyzerMaterials", SqlDbType.Bit)]
		public Boolean ShowAnalyzerMaterials { get; set; }

		//9/28/17
		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleAimNeedsRebuild", SqlDbType.Bit)]
		public Boolean SampleAimNeedsRebuild { get; set; }

		//12/18/17
		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleNumberOfDays", SqlDbType.Int)]
		public Int32 SampleNumberOfDays { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ShowNameFirstInDropdown", SqlDbType.Bit)]
        public Boolean ShowNameFirstInDropdown { get; set; }

        //5/22/18
        [DataReaderColumnMapping(),
        SqlParameterMapping("@AutoGenerateLotNumber", SqlDbType.Bit)]
        public Boolean AutoGenerateLotNumber { get; set; }

        //5/22/18
        [DataReaderColumnMapping(),
        SqlParameterMapping("@NextLotNumber", SqlDbType.Int)]
        public Int32 NextLotNumber { get; set; }

		//5/22/18
		[DataReaderColumnMapping(),
		SqlParameterMapping("@LabelSize", SqlDbType.Int)]
		public Int32 LabelSize { get; set; }

		//8/31/22
		[DataReaderColumnMapping(),
		SqlParameterMapping("@FurtherAnalysisDefaultID", SqlDbType.Int)]
		public Int32 FurtherAnalysisDefaultID { get; set; }

		//11/21/22 - removed, not used
		//////9/23/22		
		////[DataReaderColumnMapping(),
		////SqlParameterMapping("@ShowSpecificIngredients", SqlDbType.Bit)]
		////public Boolean ShowSpecificIngredients { get; set; }

		//11/21/22		
		[DataReaderColumnMapping(),
		SqlParameterMapping("@GuaranteeReportDecimals", SqlDbType.Int)]
		public Int32 GuaranteeReportDecimals { get; set; }

		//11/21/22
		[DataReaderColumnMapping(),
		SqlParameterMapping("@GroundUnGroundDefaultID", SqlDbType.Int)]
		public Int32 GroundUnGroundDefaultID { get; set; }

		//12/9/22
		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		//1/3/23 - added for SupplierGuarantee report
		[DataReaderColumnMapping(),
		SqlParameterMapping("@DryMatterAIMNutrientID", SqlDbType.Int)]
		public Int32 DryMatterAIMNutrientID { get; set; }

		//10/19/23
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalysisFilteredDataDefault", SqlDbType.Bit)]
		public Boolean AnalysisFilteredDataDefault { get; set; }

		//10/19/23
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AutoFillSampleTestTypeIncludeBox", SqlDbType.Bit)]
		public Boolean AutoFillSampleTestTypeIncludeBox { get; set; }

        //10/22/24
        [DataReaderColumnMapping(),
        SqlParameterMapping("@ShowFurtherAnalysis", SqlDbType.Bit)]
		public Boolean ShowFurtherAnalysis { get; set; }

		//11/14/24
        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsVisibleFormulationXrefMessage", SqlDbType.Bit)]
        public Boolean IsVisibleFormulationXrefMessage { get; set; }

        //11/14/24
        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsVisibleFurtherAnalysisXrefMessage", SqlDbType.Bit)]
        public Boolean IsVisibleFurtherAnalysisXrefMessage { get; set; }

        //Task1103 DB250926 
        [DataReaderColumnMapping(),
        SqlParameterMapping("@isAllowMenuPopout", SqlDbType.Bit)]
        public Boolean IsAllowMenuPopout { get; set; }

        //Task1061 DB251029 
        [DataReaderColumnMapping(),
        SqlParameterMapping("@isPrioritizeGroupTemplate", SqlDbType.Bit)]
        public Boolean IsPrioritizeGroupTemplate { get; set; }

        //Task1118 DB251203
        [DataReaderColumnMapping(),
        SqlParameterMapping("@SetSampleDateToToday", SqlDbType.Bit)]
        public Boolean SetSampleDateToToday { get; set; }

        //Task1087 DB251205
        [DataReaderColumnMapping(),
        SqlParameterMapping("@ClearFieldsOnSampleScreen", SqlDbType.Bit)]
        public Boolean ClearFieldsOnSampleScreen { get; set; }
        #endregion
    }
}