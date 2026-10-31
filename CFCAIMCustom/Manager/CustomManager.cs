using System;
using CFCAIMCustom.Data;
using CFCAIMCustom.Entity.Vitapro;
using System.Collections.Generic;
using CFCAIMCustom.Entity;
using CFCAIMCore.Entity;

namespace CFCAIMCustom.Manager
{
    public class CustomManager
    {
        #region  Member Variables

        public static String _host;

        //1/19/17 - changed these 3 from public to private
        private static String _databaseConnection;
        private static Boolean _isMultipleDatabase = false;
        private static CustomDataAccess _customData;

        #endregion

        #region  Properties
        protected static CustomDataAccess CustomData
        {
            get
            {
                if (DatabaseConnection != null)
                {
                    _customData = new Data.CustomDataAccess(String.Empty, DatabaseConnection);
                }
                else
                
                    {
                    if (IsMultipleDatabase)
                    {
                        _customData = new Data.CustomDataAccess(String.Empty, DatabaseConnection);
                    }
                    else
                    {


                        if (_host == "CFCAIMDEMO")
                        {
                            _customData = new Data.CustomDataAccess("DemoConnectionString", String.Empty);
                        }
                        else
                        {
                            _customData = new Data.CustomDataAccess("ConnectionString", String.Empty);
                        }
                    }
                }
                return _customData;
            }
        }

        public static String Host
        {
            get
            {
                return _host;
            }

            set
            {
                _host = value;
            }
        }

        public static Boolean IsMultipleDatabase
        {
            get
            {
                return _isMultipleDatabase;
            }

            set
            {
                _isMultipleDatabase = value;
            }
        }

        public static String DatabaseConnection
        {
            get
            {
                return _databaseConnection;
            }

            set
            {
                _databaseConnection = value;
            }
        }

        public static void CloseAIMData()
        {
            _customData = null;
        }
        #endregion

        #region Get


        public static List<LotInfo> GetLot(Int32 days)
        {
            return CustomData.GetLot(days);
        }
        public static List<LineRefCFCPlantInfo> GetLineRefCFCPlant()
        {
            return CustomData.GetLineRefCFCPlant();
        }
        public static List<LineRefCFCPlantInfo> GetLineRefCFCPlant(Int32 lineNumber)
        {
            return CustomData.GetLineRefCFCPlant(lineNumber);
        }

        public static List<LotInfo> GetLotCurrent(Int32 sampleID)
        {
            return CustomData.GetLotCurrent(sampleID);
        }

        public static List<ExportInfo> GetExportDetail(Int32 sampleID, Int32 lineID)
        {
            return CustomData.GetExportDetail(sampleID, lineID);
        }

        //11/1/18
        public static ErrorInfo ValidateExportDetail(Int32 sampleID)
        {
            return CustomData.ValidateExportDetail(sampleID);
        }
               

        public static List<LotInfo> GetLotMaterialLine(Int32 aimMaterialID, Int32 lineNumber, Int32 productionStatus, Int32 sampleID)
        {
            return CustomData.GetLotMaterialLine(aimMaterialID, lineNumber, productionStatus, sampleID);
        }

        //9/27/19
        //public static List<LotInfo> GetLotProduction(Int32 days, Int32 aimProteinID)
        //{
        //    return CustomData.GetLotProduction(days, aimProteinID);
        //}
        //public static List<LotInfo> GetLotProduction(Int32 productionStatus, Int32 days, Int32 aimProteinID)
        //{
        //    return CustomData.GetLotProduction(productionStatus, days, aimProteinID);
        //}
        public static List<LotInfo> GetLotProduction(Int32 days, Int32 aimNutrientID1, Int32 aimNutrientID2, Int32 aimNutrientID3, Int32 aimNutrientID4)
        {
            return CustomData.GetLotProduction(days, aimNutrientID1, aimNutrientID2, aimNutrientID3, aimNutrientID4);
        }
        public static List<LotInfo> GetLotProduction(Int32 productionStatus, Int32 days, Int32 aimNutrientID1, Int32 aimNutrientID2, Int32 aimNutrientID3, Int32 aimNutrientID4)
        {
            return CustomData.GetLotProduction(productionStatus, days, aimNutrientID1, aimNutrientID2, aimNutrientID3, aimNutrientID4);
        }


        public static List<LotInfo> GetLotStatus(Int32 days, Int32 status)
        {
            return CustomData.GetLotStatus(days, status);
        }

        public static List<SampleDetailInfo> GetSampleDetail()
        {
            return CustomData.GetSampleDetail();
        }
               
        public static List<ProductionStatusInfo> GetProductionStatus()
        {
            return CustomData.GetProductionStatus();
        }

        public static List<LotInfo> GetLotExportStatus(Boolean status)
        {
            return CustomData.GetLotExportStatus(status);
        }


        public static List<ProductionStatusInfo> GetProductionStatusForDepleted()
        {
            return CustomData.GetProductionStatusForDepleted();
        }
              
        #endregion

        #region Save

        public static ErrorInfo SaveLineRefCFCPlant(ref LineRefCFCPlantInfo xref)
        {
            return CustomData.SaveLineRefCFCPlant(ref xref);
        }

        public static ErrorInfo SaveLotStatus(Int32 sampleID, Int32 lotStatusID, Int32 cfcUserID)
        {
            return CustomData.SaveLotStatus(sampleID, lotStatusID, cfcUserID);
        }
        public static ErrorInfo SaveLot(ref LotInfo lot)
        {
            return CustomData.SaveLot(ref lot);
        }
        public static ErrorInfo SaveExportStatus(Int32 sampleID, Boolean exportStatus)
        {
            return CustomData.SaveExportStatus(sampleID, exportStatus);
        }

        public static ErrorInfo SaveProductionLineStatus(Int32 sampleID, Int32 lineNumber, Int32 productionStatus, Int32 cfcUserID)
        {
            return CustomData.SaveProductionLineStatus(sampleID, lineNumber, productionStatus, cfcUserID);
        }

        //public static ErrorInfo SaveAmountAvailable(Int32 sampleID, Decimal amountAvailable)
        public static ErrorInfo SaveAmountAvailable(Int32 sampleID, Int32 amountAvailable)
        {
            return CustomData.SaveAmountAvailable(sampleID, amountAvailable);
        }

        public static ErrorInfo UpdateDepletedDate(Int32 sampleID, Int32 cfcUserID, Boolean clearDepletedDate)
        {
            return CustomData.UpdateDepletedDate(sampleID, cfcUserID, clearDepletedDate);
        }
        #endregion

        #region Insert
        public static ErrorInfo InsertLot(Int32 cfcUserID)
        {
            return CustomData.InsertLot(cfcUserID);
        }
        #endregion

        //8/26/19
        public static SystemSettingsCustomInfo GetSystemSettingsCustom()
        {
            return CustomData.GetSystemSettingsCustom();
        }

        //8/26/19
        public static ErrorInfo SaveSystemSettingsCustom(ref SystemSettingsCustomInfo settings)
        {
            return CustomData.SaveSystemSettingsCustom(ref settings);
        }
                

    }
}
