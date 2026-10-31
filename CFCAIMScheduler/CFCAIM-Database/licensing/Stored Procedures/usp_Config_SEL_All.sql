CREATE PROCEDURE [licensing].[usp_Config_SEL_All]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  C.ConfigID 
	, C.IRUnits
	, C.IsOwning
	, C.IsAdjustmentRound
	, C.NNOMoist
	, C.NNODM
	, C.TrialUseCount
	, C.TrialInstallCode
	, C.IsBulkDensityActive
	, C.BatchSize
	, C.BatchNumberDecimals
	, C.IsBatchRound
	, C.BatchRoundTo
	, C.IsBatchesInteger
	, C.NumberNutDecDry
	, C.NumberNutDecLiquid
	, C.DatabaseDirectory
	, C.CompanyName
	, C.Address
	, C.City
	, C.State
	, C.Zip
	, C.Phone
	, C.AuthorizationCode
	, C.InstallDate
	, C.LeaseCode
	, C.LeaseLenght
	, C.UnitOfMeasure
	, C.IngredientUnitCost
	, C.BatchIngredientOrder
	, C.IsLiquidBatchVolume
	, C.LiquidBatchSize
	, C.LiquidBatchNumberDecimals
	, C.LiquidBatchIngredientOrder
	, C.IsLiquidBatchRound
	, C.LiquidBatchRoundTo
	, C.IsLiquidBatchesInteger
FROM 
	licensing.Config C WITH(NOLOCK)
END


