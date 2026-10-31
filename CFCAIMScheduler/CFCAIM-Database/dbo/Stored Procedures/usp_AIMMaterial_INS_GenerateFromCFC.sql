CREATE PROCEDURE [dbo].[usp_AIMMaterial_INS_GenerateFromCFC]
			  @CFCIngredientID					INT
			, @CFCUserID						INT = NULL
						
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @UnitTypeID			INT
DECLARE @AIMIngredientID	INT	

IF EXISTS(SELECT 
				AIMMaterialID 	
			FROM 
				dbo.CFCIngredient I
				INNER JOIN
					dbo.AIMMaterial AM
						ON AM.AIMMaterialCode = I.CFCIngredientCode 
			WHERE 
				I.CFCIngredientID = @CFCIngredientID)
	BEGIN
		SET @ErrorMessage = 'Ingredient Code ready exists'
		SET @ErrorNumber = 1
	END
ELSE
	BEGIN
		INSERT INTO 
			dbo.AIMMaterial 
				(
					  AIMMaterialCode 
					, AIMMaterialName 
					, MaterialTypeID
					, IsActive 
					, UpdateStamp 
					, CFCUserID 
				)
		SELECT 
			  CFCIngredientCode
			, CFCIngredientName
			, 1
			, 1
			, GETDATE()
			, @CFCUserID
		FROM 
			dbo.CFCIngredient I
		WHERE 
			I.CFCIngredientID = @CFCIngredientID

		SET @AIMIngredientID = @@IDENTITY

		INSERT INTO	
			dbo.CFCIngredientRefAIMMaterial 
				(
					  CFCIngredientID
					, AIMMaterialID 
					, IsActive 
					, UpdateStamp
					, CFCUserID
				)
		VALUES
			(
				  @CFCIngredientID
				, @AIMIngredientID
				, 1
				, GETDATE()
				, @CFCUserID
			)
	END
SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END