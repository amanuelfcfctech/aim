using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;

namespace CFCAIM.AIMUtilities.ExportFunctions
{
    class GetExportBatch
    {
        public static List<UpdateIngredientDetail> GetExportBatchMaint()
        {
            List<UpdateIngredientDetail> exportBatchMaint = new List<UpdateIngredientDetail>();
            switch (CFCAIMCore.Global.exportbatchTargetID)

            {
                case (Int32)ExportTargetEnum.Base:
                case (Int32)ExportTargetEnum.BaseExpand:                         //10/16/23
                    exportBatchMaint = AIMManager.GetExportBatchMaintenance(CFCAIMCore.Global.exportbatchmaintenanceHeaderID);
                    break;

                case (Int32)ExportTargetEnum.DefaultOverlay:
                case (Int32)ExportTargetEnum.DefaultOverlayExpand:              //1/10/24
                    exportBatchMaint = AIMManager.GetExportBatchMaintenanceDefault(CFCAIMCore.Global.exportbatchmaintenanceHeaderID);
                    break;

                case (Int32)ExportTargetEnum.SpecificOverlay:
                case (Int32)ExportTargetEnum.SpecificOverlayExpand:             //1/10/24
                case (Int32)ExportTargetEnum.SpecificOverlayPlantGroup:         //2/29/24
                    exportBatchMaint = AIMManager.GetExportBatchMaintenanceSpecific(CFCAIMCore.Global.exportbatchmaintenanceHeaderID);
                    break;

                default:
                    exportBatchMaint = AIMManager.GetExportBatchMaintenance(CFCAIMCore.Global.exportbatchmaintenanceHeaderID);
                    break;
            }
            return exportBatchMaint;            
        }
                
    }
}
