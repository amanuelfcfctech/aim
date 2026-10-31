CREATE PROCEDURE [dbo].[usp_CFCProductArchiveSpec_SAV_CFC5]
			  @CFCProductArchiveSpecID		INT
			, @CFCProductArchiveID			INT				= NULL
			, @CFCNutrientCode				VARCHAR(8)		= NULL
			, @NutrientMin					NUMERIC(18,9)	= NULL
			, @NutrientMax					NUMERIC(18,9)	= NULL
			, @CFCUserID					INT				= NULL
			, @AIMDatabaseID				INT				= NULL
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @CFCNutrientID	INT	= 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

SELECT 
	@CFCNutrientID = CFCNutrientID
FROM
	dbo.CFCNutrient CN
WHERE
	CN.CFCNutrientCode = @CFCNutrientCode
	AND AIMDatabaseID = @AIMDatabaseID

IF @CFCProductArchiveSpecID = 0
	BEGIN
		SELECT 
			@CFCProductArchiveSpecID = CPAS.CFCProductArchiveSpecID 
		FROM 
			dbo.CFCProductArchiveSpec CPAS
		WHERE 
			CPAS.CFCProductArchiveID = @CFCProductArchiveID
			AND CPAS.CFCNutrientID = @CFCNutrientID 
	END

IF @CFCNutrientID = 0
	BEGIN
		SET @ErrorNumber = 1
		SET @ErrorMessage = 'This Nutrient is not set up. Please contact IT. Information is not saved!'
	END
ELSE
	BEGIN
		IF @CFCProductArchiveSpecID = 0
			BEGIN
				INSERT
					dbo.CFCProductArchiveSpec
						(
								CFCProductArchiveID
							, CFCNutrientID
							, NutrientMin
							, NutrientMax
							, UpdateStamp
							, CFCUserID
						)
				VALUES
						(
								@CFCProductArchiveID
							, @CFCNutrientID
							, @NutrientMin
							, @NutrientMax
							, GETDATE()
							, @CFCUserID
						)
				SET @RecordID = @@IDENTITY
			END			
		ELSE
			BEGIN
				UPDATE
					dbo.CFCProductArchiveSpec
				SET			
					  CFCProductArchiveID = ISNULL(@CFCProductArchiveID, CFCProductArchiveID) 
					, CFCNutrientID = ISNULL(@CFCNutrientID, CFCNutrientID)
					, NutrientMin = ISNULL(@NutrientMin, NutrientMin)
					, NutrientMax = ISNULL(@NutrientMax, NutrientMax)
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
				WHERE
					@CFCProductArchiveSpecID = @CFCProductArchiveSpecID
				SET @RecordID = @CFCProductArchiveSpecID
			END
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END