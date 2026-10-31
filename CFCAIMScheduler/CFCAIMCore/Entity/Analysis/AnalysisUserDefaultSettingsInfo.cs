using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalysisUserDefaultSettingsInfo : DataObjectBase
	{
		#region Properties
		//[DataReaderColumnMapping(isOptional: true),
		//SqlParameterMapping("@AnalysisUserDefaultSettingsID", SqlDbType.Int)]
		//public Int32 AnalysisUserDefaultSettingsID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@GroupByDate", SqlDbType.Bit)]
		public Boolean GroupByDate { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@GroupBySampleID", SqlDbType.Bit)]
		public Boolean GroupBySampleID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@GroupByOwner", SqlDbType.Bit)]
		public Boolean GroupByOwner { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@GroupByOwnerLocation", SqlDbType.Bit)]
		public Boolean GroupByOwnerLocation { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@GroupBySupplier", SqlDbType.Bit)]
		public Boolean GroupBySupplier { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@GroupBySupplierLocation", SqlDbType.Bit)]
		public Boolean GroupBySupplierLocation { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AllRecords", SqlDbType.Bit)]
		public Boolean AllRecords { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@OutliersOnly", SqlDbType.Bit)]
		public Boolean OutliersOnly { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ExtremeOutlierInclude", SqlDbType.Bit)]
		public Boolean ExtremeOutlierInclude { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ExtremeOutlierExclude", SqlDbType.Bit)]
		public Boolean ExtremeOutlierExclude { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MildOutlierInclude", SqlDbType.Bit)]
		public Boolean MildOutlierInclude { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MildOutlierExclude", SqlDbType.Bit)]
		public Boolean MildOutlierExclude { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@TargetBase", SqlDbType.Bit)]
		public Boolean TargetBase { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@TargetDefaultOverlay", SqlDbType.Bit)]
		public Boolean TargetDefaultOverlay { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@TargetSpecificOverlay", SqlDbType.Bit)]
		public Boolean TargetSpecificOverlay { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@TargetSpecificOverlayPlantID", SqlDbType.Int)]
		public Int32 TargetSpecificOverlayPlantID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@TemplateQuery", SqlDbType.Int)]
		public Int32 TemplateQuery { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@TemplateView", SqlDbType.Int)]
		public Int32 TemplateView { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@RecalculateOnModify", SqlDbType.Bit)]
		public Boolean RecalculateOnModify { get; set; }

		//12/13/22	"I" = Ingredient   "P" = Product
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IngredientOrProduct", SqlDbType.NVarChar)]
		public String IngredientOrProduct { get; set; }


		#endregion

	}
}
