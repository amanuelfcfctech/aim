CREATE PROCEDURE [dbo].[usp_CFCProductArchiveNutrient_SAV_CFC5]
			  @CFCProductArchiveNutrientID	INT
			, @CFCProductArchiveID			INT				= NULL
			, @CFCNutrientCode				VARCHAR(8)		= NULL
			, @NutrientValue				NUMERIC(18,9)	= NULL
			, @UpdateStamp					DATETIME		= NULL
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


IF @CFCProductArchiveNutrientID = 0
	BEGIN
		SELECT 
			@CFCProductArchiveNutrientID = CPAN.CFCProductArchiveNutrientID 
		FROM 
			dbo.CFCProductArchiveNutrient CPAN
		WHERE 
			CPAN.CFCProductArchiveID = @CFCProductArchiveID
			AND CPAN.CFCNutrientID = @CFCNutrientID 
	END


IF @CFCNutrientID = 0
	BEGIN
		SET @ErrorNumber = 1
		SET @ErrorMessage = 'This Nutrient is not set up. Please contact IT. Information is not saved!'
	END
ELSE
	BEGIN
		IF @CFCProductArchiveNutrientID = 0
			BEGIN
				INSERT
					dbo.CFCProductArchiveNutrient
						(
							  CFCProductArchiveID
							, CFCNutrientID
							, NutrientValue
							, UpdateStamp
							, CFCUserID
						)
				VALUES
						(
							  @CFCProductArchiveID
							, @CFCNutrientID
							, @NutrientValue
							, GETDATE()
							, @CFCUserID
						)
				SET @RecordID = @@IDENTITY
			END			
		ELSE
			BEGIN
				UPDATE
					dbo.CFCProductArchiveNutrient
				SET			
					  CFCProductArchiveID = ISNULL(@CFCProductArchiveID, CFCProductArchiveID) 
					, CFCNutrientID = ISNULL(@CFCNutrientID, CFCNutrientID)
					, NutrientValue = ISNULL(@NutrientValue, NutrientValue)
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
				WHERE
					CFCProductArchiveNutrientID = @CFCProductArchiveNutrientID
				SET @RecordID = @CFCProductArchiveNutrientID
			END
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
END