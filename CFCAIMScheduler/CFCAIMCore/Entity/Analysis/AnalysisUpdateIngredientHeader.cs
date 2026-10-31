using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalysisUpdateIngredientHeader : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@UpdateIngredientHeaderID", SqlDbType.Int)]
		public Int32 UpdateIngredientHeaderID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UpdateName", SqlDbType.VarChar)]
		public String UpdateName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime CreateDate { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Status", SqlDbType.Int)]
		public Int32 Status { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsOverlay", SqlDbType.Bit)]
		public Boolean IsOverlay { get; set; }

		#endregion
	}
}

