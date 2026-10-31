using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalysisExportBatchMaintenance : DataObjectBase
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

		//10/13/17
		public String CreateDateString
		{
			get
			{
				return CreateDate.ToString();
			}
		}

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Status", SqlDbType.Int)]
		public Int32 Status { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ExportTargetID", SqlDbType.Int)]
		public Int32 ExportTargetID { get; set; }

		public String Target
		{
			get
			{
				if (ExportTargetID == 1)
				{
					return "Base";
				}
				else if (ExportTargetID == 2)
				{
					return "Default OverLay";
				}
				else if (ExportTargetID == 3)
				{
					return "Specific OverLay";
				}

				//11/17/23
				else if (ExportTargetID == 4)
				{
					return "Base Expanded";
				}

				//1/10/24
				else if (ExportTargetID == 5)
				{
					return "Default Overlay Expanded";
				}
				else if (ExportTargetID == 6)
				{
					return "Specific Overlay Expanded";
				}

				//1/26/24
				else if (ExportTargetID == 7)
				{
					return "Specific Overlay (Group)";
				}

				else
				{
					return "";
				}
			}
		}

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMOwnerLocationID { get; set; }
		
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMDatabaseID", SqlDbType.Int)]
		public Int32 AIMDatabaseID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UpdateStamp", SqlDbType.DateTime)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UpdateCFCUserID", SqlDbType.Int)]
		public Int32 UpdateCFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@StartDate", SqlDbType.DateTime)]
		public DateTime ExportStartDate { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@EndDate", SqlDbType.DateTime)]
		public DateTime ExportEndDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]		
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCPlantID", SqlDbType.Int)]
		public Int32 CFCPlantID { get; set; }

        //5/24/18 - added for Vitapro
        [DataReaderColumnMapping(isOptional: true)]
        public String LotNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ShortName", SqlDbType.NVarChar)]
        public String ShortName { get; set; }

		//1/10/24
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ExportExpandID", SqlDbType.Int)]
		public Int32 ExportExpandID { get; set; }

		//1/10/24
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ExportExpandName", SqlDbType.NVarChar)]
		public String ExportExpandName { get; set; }

		//1/12/24
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ExportPlantGroupID", SqlDbType.Int)]
		public Int32 ExportPlantGroupID { get; set; }

		//1/12/24
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ExportPlantGroupName", SqlDbType.NVarChar)]
		public String ExportPlantGroupName { get; set; }

		#endregion
	}
}
