CREATE PROCEDURE [dbo].[usp_UpdateIngredientDetail_SAV]
			  @UpdateIngredientDetailID		INT				
			, @UpdateIngredientHeaderID		INT				= NULL
			, @CFCIngredientID				INT				= NULL
			, @CFCNutrientID				INT				= NULL
			, @ProposedValue				NUMERIC(12, 4)	= NULL
			, @CreateStamp					DATETIME		= NULL
			, @CFCUserID					INT				= NULL
			, @UpdateStamp					DATETIME		= NULL
			, @UpdateCFCUserID				INT				= NULL
			, @Override						NUMERIC(12, 4)	= NULL 
			, @Mean							NUMERIC(12, 4)	= NULL
			, @StandardDeviation			NUMERIC(12, 4)	= NULL
			, @Status						INT				= NULL
			, @CFCPlantID					INT				= NULL
			, @NumberOfSamples				INT				= NULL

			--3/17/17
			, @IsOverlay					BIT				= NULL
AS
BEGIN



--exec usp_UpdateIngredientDetail_SAV 0, 1012, 1, 2, 14.053, '2/24/15 11:15:12', 2, null, null, null, 14.053, 1.0473

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @UpdateIngredientDetailID = 0
	BEGIN
		INSERT INTO
			dbo.UpdateIngredientDetail 
				(
				  UpdateIngredientHeaderID 
				, CFCIngredientID 
				, CFCNutrientID 
				, ProposedValue
				, CreateStamp 
				, CFCUserID 
				, Override
				, Mean 
				, StandardDeviation 
				, Status
				, CFCPlantID
				, NumberOfSamples
				, IsOverlay
				)
		VALUES
				(
				  @UpdateIngredientHeaderID 
				, @CFCIngredientID 
				, @CFCNutrientID 
				, @ProposedValue
				, GETDATE() 
				, @CFCUserID
				, @Override 
				, @Mean 
				, @StandardDeviation 
				, ISNULL(@Status, 1)
				, @CFCPlantID
				, @NumberOfSamples
				, @IsOverlay
				)
		SET @RecordID = @@IDENTITY
	END
ELSE			
	BEGIN
		UPDATE 
			dbo.UpdateIngredientDetail
		SET
			  UpdateIngredientHeaderID = ISNULL(@UpdateIngredientHeaderID, UpdateIngredientHeaderID)
			, CFCIngredientID = ISNULL(@CFCIngredientID, CFCIngredientID)
			, CFCNutrientID = ISNULL(@CFCNutrientID, CFCNutrientID)
			, ProposedValue = ISNULL(@ProposedValue, ProposedValue)
			, CreateStamp = ISNULL(@CreateStamp, CreateStamp)
			, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
			, UpdateStamp = ISNULL(@UpdateStamp, UpdateStamp)
			, UpdateCFCUserID = ISNULL(@UpdateCFCUserID, UpdateCFCUserID)
			, Override = ISNULL(@Override, Override)
			, Mean = ISNULL(@Mean, Mean)
			, StandardDeviation = ISNULL(@StandardDeviation, StandardDeviation)
			, Status = ISNULL(@Status, Status)
			, CFCPlantID = ISNULL(@CFCPlantID, CFCPlantID)
			, NumberOfSamples = ISNULL(@NumberOfSamples, NumberOfSamples)
			, IsOverlay = ISNULL(@IsOverlay, IsOverlay)

		WHERE
			UpdateIngredientDetailID = @UpdateIngredientDetailID
			
		SET @RecordID = @UpdateIngredientDetailID
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
