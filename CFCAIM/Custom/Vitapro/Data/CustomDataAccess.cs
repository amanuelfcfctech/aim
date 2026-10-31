using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Core.Library.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Windows.Forms;
using Core.Library.Notification;
using System.Reflection;
using System.Diagnostics;
using CFCAIMCore.Entity;

//9/16/20
//using CFCAIMCustom.Entity.Vitapro;
using CFCAIM.Custom.Vitapro.Entity;

//9/16/20
//namespace CFCAIMCustom.Data    
namespace CFCAIM.Custom.Vitapro.Data

{
    public class CustomDataAccess
    {
        #region Member Variables
        public static SqlDatabase _database;
        public static Boolean _isMessageDisplay = true;
        #endregion

        #region Constructors
        public CustomDataAccess(String connectionStringName, String connectionString)
        {
            if (connectionString.Length > 0)
            {
                _database = new SqlDatabase(connectionString);
            }
            else
            {
                _database = (SqlDatabase)EnterpriseLibraryContainer.Current.GetInstance<Database>(connectionStringName);
            }
        }
        #endregion

        #region Parameter
        public static Boolean IsMessageDisplay
        {
            get
            {
                return _isMessageDisplay;
            }

            set
            {
                _isMessageDisplay = value;
            }
        }
        #endregion

        #region Funcation
        private static void HandleMessage(Exception ex, String methodName)
        {
            String message = String.Empty;

            message = ex.Message + " Unable to " + methodName + ". Contact IT.";

            if (_isMessageDisplay)
            {
                MessageBox.Show(message, "Warning...", MessageBoxButtons.OK);
            }
            else
            {
                ApplicationLog.WriteLog(message, EventLogEntryType.Warning);
            }
        }

        private static void HandleMessage(ErrorInfo message, String methodName)
        {
            if (_isMessageDisplay)
            {
                MessageBox.Show(message.ErrorMessage, "Warning...", MessageBoxButtons.OK);
            }
            else
            {
                ApplicationLog.WriteLog(message.ErrorMessage, EventLogEntryType.Warning);
            }
        }
        #endregion
        
        #region Get Data        
        
                
        public List<LotInfo> GetLotCurrent(Int32 sampleID)
        {
            List<LotInfo> result = new List<LotInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SEL_SampleID_Current"))
                {
                    _database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<LotInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        public List<ExportInfo> GetExportDetail(Int32 sampleID, Int32 lineID)
        {
            List<ExportInfo> result = new List<ExportInfo>();
            try
            {
                //11/1/18
                //using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Export_SEL_SampleID"))
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Sample_Export_SEL_SampleID"))

                {
                    _database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
                    _database.AddInParameter(cmd, "@LineID", SqlDbType.Int, lineID);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<ExportInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        //11/1/18
        public ErrorInfo ValidateExportDetail(Int32 sampleID)
        {
            ErrorInfo result = new ErrorInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Sample_Export_Validation_SEL_SampleID"))
                {
                    _database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);                   
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<ErrorInfo>.GetScalar(reader);
                    }

                    if (result.ErrorNumber > 0)
                    {
                        HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
                result.ErrorNumber = -1;
            }
            return result;
        }


        public List<LotInfo> GetLot(Int32 days)
        {
            List<LotInfo> result = new List<LotInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SEL"))
                {
                    _database.AddInParameter(cmd, "@Days", SqlDbType.Int, days);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<LotInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        public List<LineRefCFCPlantInfo> GetLineRefCFCPlant()
        {
            List<LineRefCFCPlantInfo> result = new List<LineRefCFCPlantInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_LineRefCFCPlant_SEL"))
                {
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<LineRefCFCPlantInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        public List<LineRefCFCPlantInfo> GetLineRefCFCPlant(Int32 LineNumber)
        {
            List<LineRefCFCPlantInfo> result = new List<LineRefCFCPlantInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_LineRefCFCPlant_SEL_LineID"))
                {
                    _database.AddInParameter(cmd, "@LineID", SqlDbType.Int, LineNumber);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<LineRefCFCPlantInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        public List<LotInfo> GetLotMaterialLine(Int32 aimMaterialID, Int32 lineNumber, Int32 productionStatus, Int32 sampleID)
        {
            List<LotInfo> result = new List<LotInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SEL_AIMMaterialID_ProductionStatusLine"))
                {
                    _database.AddInParameter(cmd, "@AIMMaterialID", SqlDbType.Int, aimMaterialID);
                    _database.AddInParameter(cmd, "@LineNumber", SqlDbType.Int, lineNumber);
                    _database.AddInParameter(cmd, "@Status", SqlDbType.Int, productionStatus);
                    _database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<LotInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

              

        public List<LotInfo> GetLotProduction(Int32 productionStatus, Int32 days, Int32 aimNutrientID1, Int32 aimNutrientID2, Int32 aimNutrientID3, Int32 aimNutrientID4)
        {
            List<LotInfo> result = new List<LotInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SEL_ProductionStatus"))
                {
                    _database.AddInParameter(cmd, "@ProductionStatus", SqlDbType.Int, productionStatus);
                    _database.AddInParameter(cmd, "@Days", SqlDbType.Int, days);
                    _database.AddInParameter(cmd, "@AimNutrientID1", SqlDbType.Int, aimNutrientID1);
                    _database.AddInParameter(cmd, "@AimNutrientID2", SqlDbType.Int, aimNutrientID2);
                    _database.AddInParameter(cmd, "@AimNutrientID3", SqlDbType.Int, aimNutrientID3);
                    _database.AddInParameter(cmd, "@AimNutrientID4", SqlDbType.Int, aimNutrientID4);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<LotInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        public List<LotInfo> GetLotProduction(Int32 days, Int32 aimNutrientID1, Int32 aimNutrientID2, Int32 aimNutrientID3, Int32 aimNutrientID4)
        {
            List<LotInfo> result = new List<LotInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SEL_All_ProductionStatus"))
                {
                    _database.AddInParameter(cmd, "@Days", SqlDbType.Int, days);
                    _database.AddInParameter(cmd, "@AimNutrientID1", SqlDbType.Int, aimNutrientID1);
                    _database.AddInParameter(cmd, "@AimNutrientID2", SqlDbType.Int, aimNutrientID2);
                    _database.AddInParameter(cmd, "@AimNutrientID3", SqlDbType.Int, aimNutrientID3);
                    _database.AddInParameter(cmd, "@AimNutrientID4", SqlDbType.Int, aimNutrientID4);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<LotInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        public List<LotInfo> GetLotStatus(Int32 days, Int32 status)
        {
            List<LotInfo> result = new List<LotInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SEL_Status"))
                {
                    _database.AddInParameter(cmd, "@Days", SqlDbType.Int, days);
                    _database.AddInParameter(cmd, "@Status", SqlDbType.Int, status);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<LotInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        public List<LotInfo> GetLotExportStatus(Boolean status)
        {
            List<LotInfo> result = new List<LotInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SEL_ExportStatus"))
                {
                    _database.AddInParameter(cmd, "@ExportStatus", SqlDbType.Bit, status);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<LotInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        //9/2/20
        public List<LotInfo> GetLotConcept5ExportStatus(Boolean status)
        {
            List<LotInfo> result = new List<LotInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SEL_Concept5ExportStatus"))
                {
                    _database.AddInParameter(cmd, "@Concept5ExportStatus", SqlDbType.Bit, status);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<LotInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        public List<SampleDetailInfo> GetSampleDetail()
        {
            List<SampleDetailInfo> result = new List<SampleDetailInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_SampleDetail_SEL"))
                {                    
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<SampleDetailInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
              

        public List<ProductionStatusInfo> GetProductionStatus()
        {
            List<ProductionStatusInfo> result = new List<ProductionStatusInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_ProductionStatus_SEL"))
                {                    
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<ProductionStatusInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        public List<ProductionStatusInfo> GetProductionStatusForDepleted()
        {
            List<ProductionStatusInfo> result = new List<ProductionStatusInfo>();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_ProductionStatus_SEL_ForDepleted"))
                {
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        ObjectFiller<ProductionStatusInfo>.GetGenericCollection(ref result, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }
               

        #endregion

        #region Save Data

        public ErrorInfo SaveLineRefCFCPlant(ref LineRefCFCPlantInfo xref)
        {
            ErrorInfo result = new ErrorInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_LineRefCFCPlant_SAV"))
                {
                    cmd.Parameters.AddRange(xref.SerializeAsSqlParameter());
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<ErrorInfo>.GetScalar(reader);
                    }

                    if (result.ErrorNumber > 0)
                    {
                        HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
                result.ErrorNumber = -1;
            }
            return result;
        }

        public ErrorInfo SaveLotStatus(Int32 sampleID, Int32 lotStatusID, Int32 cfcUserID)
        {
            ErrorInfo result = new ErrorInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SAV_Status"))
                {
                    _database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
                    _database.AddInParameter(cmd, "@LotStatusID", SqlDbType.Int, lotStatusID);
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<ErrorInfo>.GetScalar(reader);
                    }

                    if (result.ErrorNumber > 0)
                    {
                        HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
                result.ErrorNumber = -1;
            }
            return result;
        }

        
        public ErrorInfo SaveExportStatus(Int32 sampleID, Boolean exportStatus)
        {
            ErrorInfo result = new ErrorInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SAV_ExportStatus"))
                {
                    _database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
                    _database.AddInParameter(cmd, "@ExportStatus", SqlDbType.Int, exportStatus);                    
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<ErrorInfo>.GetScalar(reader);
                    }

                    if (result.ErrorNumber > 0)
                    {
                        HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
                result.ErrorNumber = -1;
            }
            return result;
        }

        //9/2/20
        public ErrorInfo SaveConcept5ExportStatus(Int32 sampleID, Boolean concept5ExportStatus)
        {
            ErrorInfo result = new ErrorInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SAV_Concept5ExportStatus"))
                {
                    _database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
                    _database.AddInParameter(cmd, "@Concept5ExportStatus", SqlDbType.Int, concept5ExportStatus);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<ErrorInfo>.GetScalar(reader);
                    }

                    if (result.ErrorNumber > 0)
                    {
                        HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
                result.ErrorNumber = -1;
            }
            return result;
        }

        public ErrorInfo SaveLot(ref LotInfo lot)
        {
            ErrorInfo result = new ErrorInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SAV"))
                {
                    cmd.Parameters.AddRange(lot.SerializeAsSqlParameter());
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<ErrorInfo>.GetScalar(reader);
                    }

                    if (result.ErrorNumber > 0)
                    {
                        HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
                result.ErrorNumber = -1;
            }
            return result;
        }
                      
        
        public ErrorInfo SaveProductionLineStatus(Int32 sampleID, Int32 lineNumber, Int32 productionStatus, Int32 cfcUserID)
        {
            ErrorInfo result = new ErrorInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SAV_ProductionLine_Status"))
                {
                    _database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
                    _database.AddInParameter(cmd, "@LineNumber", SqlDbType.Int, lineNumber);
                    _database.AddInParameter(cmd, "@ProductionStatus", SqlDbType.Int, productionStatus);
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<ErrorInfo>.GetScalar(reader);
                    }

                    if (result.ErrorNumber > 0)
                    {
                        HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
                result.ErrorNumber = -1;
            }
            return result;
        }

      
        public ErrorInfo SaveAmountAvailable(Int32 sampleID, Int32 amountAvailable)
        {
            ErrorInfo result = new ErrorInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SAV_AmountAvailable"))
                {
                    _database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);
                    _database.AddInParameter(cmd, "@AmountAvailable", SqlDbType.Int, amountAvailable);

                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<ErrorInfo>.GetScalar(reader);
                    }

                    if (result.ErrorNumber > 0)
                    {
                        HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
                result.ErrorNumber = -1;
            }
            return result;
        }

        public ErrorInfo UpdateDepletedDate(Int32 sampleID, Int32 cfcUserID, Boolean clearDepletedDate)
        {
            ErrorInfo result = new ErrorInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_SAV_DepletedDate"))
                {
                    _database.AddInParameter(cmd, "@SampleID", SqlDbType.Int, sampleID);                   
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    _database.AddInParameter(cmd, "@ClearDepletedDate", SqlDbType.Bit, clearDepletedDate);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<ErrorInfo>.GetScalar(reader);
                    }

                    if (result.ErrorNumber > 0)
                    {
                        HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
                result.ErrorNumber = -1;
            }
            return result;
        }

        #endregion

        #region Insert Data
        public ErrorInfo InsertLot(Int32 cfcUserID)
        {
            ErrorInfo result = new ErrorInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_Lot_INS"))
                {
                    _database.AddInParameter(cmd, "@CFCUserID", SqlDbType.Int, cfcUserID);
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<ErrorInfo>.GetScalar(reader);
                    }

                    if (result.ErrorNumber > 0)
                    {
                        HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
                result.ErrorNumber = -1;
            }
            return result;
        }

        //8/26/19
        public SystemSettingsCustomInfo GetSystemSettingsCustom()
        {
            SystemSettingsCustomInfo result = new SystemSettingsCustomInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_SystemSettingsCustom_SEL"))
                {
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<SystemSettingsCustomInfo>.GetScalar(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
            }
            return result;
        }

        //8/26/19
        public ErrorInfo SaveSystemSettingsCustom(ref SystemSettingsCustomInfo settings)
        {
            ErrorInfo result = new ErrorInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("Vitapro.usp_SystemSettingsCustom_SAV"))
                {
                    cmd.Parameters.AddRange(settings.SerializeAsSqlParameter());
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<ErrorInfo>.GetScalar(reader);
                    }

                    if (result.ErrorNumber > 0)
                    {
                        HandleMessage(result, MethodInfo.GetCurrentMethod().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleMessage(ex, MethodInfo.GetCurrentMethod().Name);
                result.ErrorNumber = -1;
            }
            return result;
        }
                

        #endregion
    }
}
