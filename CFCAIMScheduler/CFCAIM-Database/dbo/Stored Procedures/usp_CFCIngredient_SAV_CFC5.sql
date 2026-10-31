CREATE PROCEDURE [dbo].[usp_CFCIngredient_SAV_CFC5]
			  @CFCIngredientID					INT
			, @CFCIngredientCode				VARCHAR(10)
			, @CFCIngredientName				VARCHAR(40)
			, @MathTypeID						INT
			, @IsActive							BIT
			, @CFCUserID						INT			
			, @AIMDatabaseID					INT
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber	INT = 0
DECLARE @RecordID		INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

IF @CFCIngredientID = 0 
	BEGIN
		SELECT 
			@CFCIngredientID = CI.CFCIngredientID 
		FROM 
			dbo.CFCIngredient CI
		WHERE 
			CI.CFCIngredientCode = @CFCIngredientCode
			AND CI.AIMDatabaseID = @AIMDatabaseID 	
	END

IF @CFCIngredientID = 0 
	BEGIN
		INSERT
			dbo.CFCIngredient
				(
				  CFCIngredientCode
				, CFCIngredientName
				, UpdateStamp
				, CFCUserID
				, MathType
				, AIMDatabaseID
				)
		VALUES
				(
				  @CFCIngredientCode
				, @CFCIngredientName
				, GETDATE()
				, @CFCUserID
				, @MathTypeID
				, @AIMDatabaseID
				)
			SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.CFCIngredient WITH(ROWLOCK)
		SET 
			  CFCIngredientCode = ISNULL(@CFCIngredientCode, CFCIngredientCode)
			, CFCIngredientName = ISNULL(@CFCIngredientName, CFCIngredientName)
			, IsActive = ISNULL(@IsActive, IsActive)
			, UpdateStamp = GETDATE()
			, CFCUserID = @CFCUserID
			, MathType = ISNULL(@MathTypeID, MathType)
			, AIMDatabaseID = ISNULL(@AIMDatabaseID, AIMDatabaseID)
		WHERE 
			CFCIngredientID = @CFCIngredientID
		SET @RecordID = @CFCIngredientID								
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END