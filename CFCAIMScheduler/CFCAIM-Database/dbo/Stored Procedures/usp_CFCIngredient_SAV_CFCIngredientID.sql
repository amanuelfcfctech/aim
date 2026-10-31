CREATE PROCEDURE [dbo].[usp_CFCIngredient_SAV_CFCIngredientID]
			  @CFCIngredientID					INT
			, @CFCIngredientCode				VARCHAR(10)
			, @CFCIngredientName				VARCHAR(30)
			, @MathTypeID						INT
			, @IsActive							BIT
			, @CFCUserID						INT			
			, @AIMDatabaseID					INT
AS
BEGIN

-- exec usp_CFCIngredient_SAV_CFCIngredientID 1103, 'ggg', 'ggg', 3, 1, 2, 1016
-- select * from cfcingredient where cfcingredientid = 1103

SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @CFCIngredientID = 0 
	BEGIN
	IF EXISTS(SELECT CFCIngredientID FROM CFCIngredient 
	WHERE CFCIngredientCode = @CFCIngredientCode
			and AIMDatabaseID = @AIMDatabaseID 	)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Ingredient code already exists. Please enter a different code. Information is not saved!'

			END
		ELSE
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
			END
		END
ELSE
	BEGIN
		IF @IsActive = 0
			BEGIN
				SET @IDinOtherTable =
				(
					SELECT 
						REF.CFCIngredientID
					FROM
						dbo.CFCIngredientRefAIMMaterial REF WITH(NOLOCK)
					WHERE
						REF.CFCIngredientID = @CFCIngredientID
				)
				IF @IDinOtherTable IS NOT NULL
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'Unable to delete ingredient because it is being used in FRM ingredient cross reference table.'
					END
			END

		IF @ErrorNumber = 0
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
													
			END
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
