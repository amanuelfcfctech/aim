CREATE PROCEDURE [licensing].[usp_Config_SAV]
	  @ConfigID						INT
	, @IRUnits						VARCHAR(1)
	, @IsOwning						BIT
	, @IsAdjustmentRound			BIT
	, @NNOMoist						INT
	, @NNODM						INT
	, @TrialUseCount				INT
	, @TrialInstallCode				NUMERIC(18, 0)
	, @IsBulkDensityActive			BIT
	, @BatchSize					NUMERIC(18, 0)
	, @BatchNumberDecimals			INT
	, @IsBatchRound					BIT
	, @BatchRoundTo					NUMERIC(18, 0)
	, @IsBatchesInteger				BIT
	, @NumberNutDecDry				INT
	, @NumberNutDecLiquid			INT
	, @DatabaseDirectory			VARCHAR(300)
	, @CompanyName					VARCHAR(40)
	, @Address						VARCHAR(40)
	, @City							VARCHAR(40)
	, @State						VARCHAR(40)
	, @Zip							VARCHAR(20)
	, @Phone						VARCHAR(20)
	, @AuthorizationCode			NUMERIC(18, 0)
	, @LeaseCode					NUMERIC(18, 0)
	, @LeaseLenght					INT
	, @UnitOfMeasure				INT
	, @IngredientUnitCost			INT
	, @BatchIngredientOrder			INT
	, @IsLiquidBatchVolume			BIT
	, @LiquidBatchSize				NUMERIC(18, 0)
	, @LiquidBatchNumberDecimals	INT
	, @LiquidBatchIngredientOrder	INT
	, @IsLiquidBatchRound			BIT
	, @LiquidBatchRoundTo			NUMERIC(18, 0)
	, @IsLiquidBatchesInteger		BIT
	, @InstallDate					DATETIME
		
AS
BEGIN
SET NOCOUNT ON;

IF @ConfigID = 0
	BEGIN
		INSERT INTO 
			licensing.Config WITH(ROWLOCK)
			(
				  IRUnits
				, IsOwning
				, IsAdjustmentRound
				, NNOMoist
				, NNODM
				, TrialUseCount
				, TrialInstallCode
				, IsBulkDensityActive
				, BatchSize
				, BatchNumberDecimals
				, IsBatchRound
				, BatchRoundTo
				, IsBatchesInteger
				, NumberNutDecDry
				, NumberNutDecLiquid
				, DatabaseDirectory
				, CompanyName
				, Address
				, City
				, State
				, Zip
				, Phone
				, AuthorizationCode
				, LeaseCode
				, LeaseLenght
				, UnitOfMeasure
				, IngredientUnitCost
				, BatchIngredientOrder
				, IsLiquidBatchVolume
				, LiquidBatchSize
				, LiquidBatchNumberDecimals
				, LiquidBatchIngredientOrder
				, IsLiquidBatchRound
				, LiquidBatchRoundTo
				, IsLiquidBatchesInteger
				, InstallDate
				
			)
			VALUES
			(
				  @IRUnits
				, @IsOwning
				, @IsAdjustmentRound
				, @NNOMoist
				, @NNODM
				, @TrialUseCount
				, @TrialInstallCode
				, @IsBulkDensityActive
				, @BatchSize
				, @BatchNumberDecimals
				, @IsBatchRound
				, @BatchRoundTo
				, @IsBatchesInteger
				, @NumberNutDecDry
				, @NumberNutDecLiquid
				, @DatabaseDirectory
				, @CompanyName
				, @Address
				, @City
				, @State
				, @Zip
				, @Phone
				, @AuthorizationCode
				, @LeaseCode
				, @LeaseLenght
				, @UnitOfMeasure
				, @IngredientUnitCost
				, @BatchIngredientOrder
				, @IsLiquidBatchVolume
				, @LiquidBatchSize
				, @LiquidBatchNumberDecimals
				, @LiquidBatchIngredientOrder
				, @IsLiquidBatchRound
				, @LiquidBatchRoundTo
				, @IsLiquidBatchesInteger
				, @InstallDate
			)
	END
ELSE
	BEGIN
		UPDATE 
			licensing.Config WITH(ROWLOCK)
		SET  
			  IRUnits = @IRUnits
			, IsOwning = @IsOwning
			, IsAdjustmentRound = @IsAdjustmentRound
			, NNOMoist = @NNOMoist
			, NNODM = @NNODM
			, TrialUseCount = @TrialUseCount
			, TrialInstallCode = @TrialInstallCode
			, IsBulkDensityActive = @IsBulkDensityActive
			, BatchSize = @BatchSize
			, BatchNumberDecimals = @BatchNumberDecimals
			, IsBatchRound = @IsBatchRound
			, BatchRoundTo = @BatchRoundTo
			, IsBatchesInteger = @IsBatchesInteger
			, NumberNutDecDry = @NumberNutDecDry
			, NumberNutDecLiquid = @NumberNutDecLiquid
			, DatabaseDirectory = @DatabaseDirectory
			, CompanyName = @CompanyName
			, Address = @Address
			, City = @City
			, State = @State
			, Zip = @Zip
			, Phone = @Phone
			, AuthorizationCode = @AuthorizationCode
			, LeaseCode = @LeaseCode
			, LeaseLenght = @LeaseLenght
			, UnitOfMeasure = @UnitOfMeasure
			, IngredientUnitCost = @IngredientUnitCost
			, BatchIngredientOrder = @BatchIngredientOrder
			, IsLiquidBatchVolume = @IsLiquidBatchVolume
			, LiquidBatchSize = @LiquidBatchSize
			, LiquidBatchNumberDecimals = @LiquidBatchNumberDecimals
			, LiquidBatchIngredientOrder = @LiquidBatchIngredientOrder
			, IsLiquidBatchRound = @IsLiquidBatchRound
			, LiquidBatchRoundTo = @LiquidBatchRoundTo
			, IsLiquidBatchesInteger = @IsLiquidBatchesInteger
			, InstallDate = @InstallDate	
			, UpdateStamp = GETDATE()		
		WHERE
			ConfigID = @ConfigID
	END
END