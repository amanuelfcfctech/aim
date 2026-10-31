CREATE PROCEDURE [dbo].[usp_CFCIngredientNutrient_INS_Array]
			    @RowData					VARCHAR(6000)
			  , @CFCIngredientID			INT
			  , @AIMDatabaseID				INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @SplitOn			VARCHAR(2)
DECLARE @SplitRecord		VARCHAR(2)
DECLARE @CFCNutrientCode	VARCHAR(8)
DECLARE @Computed			VARCHAR(1)
DECLARE @CFCNutrientID		INT
DECLARE @IsComputed			BIT

SET @SplitOn = '~'
SET @SplitRecord = '~~'

UPDATE 
	dbo.CFCIngredientNutrient 
SET
	Value = 0 
WHERE
	CFCIngredientID = @CFCIngredientID 

WHILE (CHARINDEX(@SplitOn, @RowData) > 0 ) 
    BEGIN  
		SET @CFCNutrientCode = LTRIM(RTRIM(SUBSTRING(@RowData, 1, CHARINDEX(@SplitOn, @RowData) - 1)))
		SET @RowData = SUBSTRING(@RowData, CHARINDEX(@SplitOn, @RowData) + 1, LEN(@RowData)) 
		SET @Computed = LTRIM(RTRIM(SUBSTRING(@RowData, 1, CHARINDEX(@SplitOn, @RowData) - 1)))
		SET @RowData = SUBSTRING(@RowData, CHARINDEX(@SplitOn, @RowData) + 1, LEN(@RowData)) 

		SET @IsComputed = 0
		IF @Computed = 'Y'
			BEGIN
				SET @IsComputed = 1
			END

		SELECT 
			@CFCNutrientID = CFCNutrientID
		FROM 
			dbo.CFCNutrient CN
		WHERE
			CN.CFCNutrientCode = @CFCNutrientCode
			AND CN.AIMDatabaseID = @AIMDatabaseID

		IF EXISTS(SELECT
						CFCIngredientNutrientID
					FROM 
						dbo.CFCIngredientNutrient
					WHERE
						CFCIngredientID = @CFCIngredientID
						AND CFCNutrientID = @CFCNutrientID)
			BEGIN
				UPDATE
					dbo.CFCIngredientNutrient 
				SET 
					  Value = LTRIM(RTRIM(SUBSTRING(@RowData, 1, CHARINDEX(@SplitOn, @RowData) - 1)))
					, IsComputed = @IsComputed 
					, UpdateStamp = GETDATE()
				WHERE
					CFCIngredientID = @CFCIngredientID
					AND CFCNutrientID = @CFCNutrientID					
			END
		ELSE
			BEGIN
				INSERT INTO 	
					dbo.CFCIngredientNutrient 
						(
						  CFCIngredientID
						, CFCNutrientID
						, Value 
						, IsComputed
						, UpdateStamp
						)
				VALUES
					(
					  @CFCIngredientID 
					, @CFCNutrientID 
					, LTRIM(RTRIM(SUBSTRING(@RowData, 1, CHARINDEX(@SplitOn, @RowData) - 1)))
					, @IsComputed
					, GETDATE()
					)
			END
		SET @RowData = SUBSTRING(@RowData, CHARINDEX(@SplitRecord, @RowData) + 2, LEN(@RowData)) 
    END 

DELETE
	dbo.CFCIngredientNutrient 
WHERE
	CFCIngredientID = @CFCIngredientID 
	AND Value = 0
	AND IsComputed = 0

END