using CFCLicensing.Entity;
using Core.Library.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace CFCLicensing.Data
{
    public class LicensingDataAccess
    {
        #region Member Variables
        SqlDatabase _database;
        #endregion

        #region Constructors

        //1/19/17
        //public LicensingDataAccess(string connectionStringName)
        public LicensingDataAccess(string connectionStringName, String connectionString)
        {
            //1/19/17
            //_database = (SqlDatabase)EnterpriseLibraryContainer.Current.GetInstance<Database>(connectionStringName);
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

        #region Get Config Table
        public VersionInfo GetVersion()
        {
            VersionInfo result = new VersionInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("dbo.usp_Version_SEL"))
                {
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        result = ObjectFiller<VersionInfo>.GetScalar(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Unable to get versoin information.  Contact IT.", "Warning...", MessageBoxButtons.OK);
            }
            return result;
        }
        public ConfigInfo GetConfig()
        {
            ConfigInfo config = new ConfigInfo();
            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("licensing.usp_Config_SEL_All"))
                {					
                    using (IDataReader reader = _database.ExecuteReader(cmd))
                    {
                        config = ObjectFiller<ConfigInfo>.GetScalar(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  Unable to get config.\nCalling GetConfig.  Contact IT.", "Warning...", MessageBoxButtons.OK);

                //CURRENTVERSION (2/22/17)
                config.ErrorNumber = -1;
                
            }
            return config;
        }
        #endregion

        #region Save Config Table
        

        public Boolean SaveConfig(ref ConfigInfo config)
        {
            Boolean isSuccessful = true;
            //converting few enums to Int
            Int32 batchIngredientOrder = 0;
            Int32.TryParse(config.BatchIngredientOrder.ToString(), out batchIngredientOrder);

            Int32 UnitOfMeasure = 0;
            Int32.TryParse(config.UnitOfMeasure.ToString(), out UnitOfMeasure);

            Int32 IngredientUnitCost = 0;
            Int32.TryParse(config.IngredientUnitCost.ToString(), out IngredientUnitCost);

            Int32 LiquidBatchIngredientOrder = 0;
            Int32.TryParse(config.LiquidBatchIngredientOrder.ToString(), out LiquidBatchIngredientOrder);

            try
            {
                using (DbCommand cmd = _database.GetStoredProcCommand("licensing.usp_Config_SAV"))
                {
                    _database.AddInParameter(cmd, "@configID", SqlDbType.Int, config.ConfigID);
                    _database.AddInParameter(cmd, "@IRUnits", SqlDbType.VarChar, config.IRUnits);
                    _database.AddInParameter(cmd, "@IsOwning", SqlDbType.Bit, config.IsOwning);
                    _database.AddInParameter(cmd, "@IsAdjustmentRound", SqlDbType.Bit, config.IsAdjustmentRound);
                    _database.AddInParameter(cmd, "@NNOMoist", SqlDbType.BigInt, config.NNOMoist);
                    _database.AddInParameter(cmd, "@NNODM", SqlDbType.BigInt, config.NNODM);
                    _database.AddInParameter(cmd, "@IsBulkDensityActive", SqlDbType.Bit, config.IsBulkDensityActive);
                    _database.AddInParameter(cmd, "@BatchSize", SqlDbType.Decimal, config.BatchSize);
                    _database.AddInParameter(cmd, "@BatchNumberDecimals", SqlDbType.BigInt, config.BatchNumberDecimals);
                    _database.AddInParameter(cmd, "@BatchIngredientOrder", SqlDbType.Int, batchIngredientOrder);
                    _database.AddInParameter(cmd, "@IsBatchRound", SqlDbType.Bit, config.IsBatchRound);
                    _database.AddInParameter(cmd, "@BatchRoundTo", SqlDbType.Decimal, config.BatchRoundTo);
                    _database.AddInParameter(cmd, "@IsBatchesInteger", SqlDbType.Bit, config.IsBatchesInteger);
                    _database.AddInParameter(cmd, "@NumberNutDecDry", SqlDbType.BigInt, config.NumberNutDecDry);
                    _database.AddInParameter(cmd, "@NumberNutDecLiquid", SqlDbType.BigInt, config.NumberNutDecLiquid);
                    _database.AddInParameter(cmd, "@DatabaseDirectory", SqlDbType.VarChar, config.DatabaseDirectory);
                    _database.AddInParameter(cmd, "@CompanyName", SqlDbType.VarChar, config.CompanyName);
                    _database.AddInParameter(cmd, "@Address", SqlDbType.VarChar, config.Address);
                    _database.AddInParameter(cmd, "@City", SqlDbType.VarChar, config.City);
                    _database.AddInParameter(cmd, "@State", SqlDbType.VarChar, config.State);
                    _database.AddInParameter(cmd, "@Zip", SqlDbType.VarChar, config.Zip);
                    _database.AddInParameter(cmd, "@Phone", SqlDbType.VarChar, config.Phone);
                    _database.AddInParameter(cmd, "@AuthorizationCode", SqlDbType.Decimal, config.AuthorizationCode);
                    _database.AddInParameter(cmd, "@LeaseLenght", SqlDbType.BigInt, config.LeaseLenght);
                    _database.AddInParameter(cmd, "@UnitOfMeasure", SqlDbType.Int, UnitOfMeasure);
                    _database.AddInParameter(cmd, "@IngredientUnitCost", SqlDbType.Int, config.IngredientUnitCost);
                    _database.AddInParameter(cmd, "@IsLiquidBatchVolume", SqlDbType.Bit, config.IsLiquidBatchVolume);
                    _database.AddInParameter(cmd, "@LiquidBatchSize", SqlDbType.Decimal, config.LiquidBatchSize);
                    _database.AddInParameter(cmd, "@LiquidBatchNumberDecimals", SqlDbType.BigInt, config.LiquidBatchNumberDecimals);
                    _database.AddInParameter(cmd, "@LiquidBatchIngredientOrder", SqlDbType.Int, config.LiquidBatchIngredientOrder);
                    _database.AddInParameter(cmd, "@IsLiquidBatchRound", SqlDbType.Bit, config.IsLiquidBatchRound);
                    _database.AddInParameter(cmd, "@LiquidBatchRoundTo", SqlDbType.Decimal, config.LiquidBatchRoundTo);
                    _database.AddInParameter(cmd, "@IsLiquidBatchesInteger", SqlDbType.Bit, config.IsLiquidBatchesInteger);

                    if (config.InstallDate == DateTime.MinValue)
                    {
                        _database.AddInParameter(cmd, "@TrialInstallCode", SqlDbType.Decimal, null);
                        _database.AddInParameter(cmd, "@InstallDate", SqlDbType.DateTime, null);
                        _database.AddInParameter(cmd, "@TrialUseCount", SqlDbType.BigInt, null);
                        _database.AddInParameter(cmd, "@LeaseCode", SqlDbType.Decimal, null);
                    }
                    else
                    {
                        _database.AddInParameter(cmd, "@TrialInstallCode", SqlDbType.Decimal, config.TrialInstallCode);
                        _database.AddInParameter(cmd, "@InstallDate", SqlDbType.DateTime, config.InstallDate);
                        _database.AddInParameter(cmd, "@TrialUseCount", SqlDbType.BigInt, config.TrialUseCount);
                        _database.AddInParameter(cmd, "@LeaseCode", SqlDbType.Decimal, config.LeaseCode);
                    }
                    _database.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                isSuccessful = false;
                //TODO ADD MESS
                MessageBox.Show(ex.Message + " Unable to save Config. Calling SaveConfig.  Contact IT.", "Warning...", MessageBoxButtons.OK);

            }
            return isSuccessful;
        }
        #endregion
    }
}
