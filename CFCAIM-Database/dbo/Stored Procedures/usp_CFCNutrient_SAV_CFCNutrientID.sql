CREATE PROCEDURE [dbo].[usp_CFCNutrient_SAV_CFCNutrientID]
			  @CFCNutrientID					INT
			, @CFCNutrientCode				VARCHAR(10)		= NULL
			, @CFCNutrientName				VARCHAR(30)		= NULL
			, @IsActive							BIT			= NULL
			, @CFCUserID						INT			= NULL
			, @MathTypeID						INT			= NULL
			, @AimDatabaseID					INT			= NULL
			, @IsComputed						BIT			= NULL
AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @CFCNutrientID = 0 
	BEGIN
	IF EXISTS(SELECT CFCNutrientID FROM CFCNutrient WHERE CFCNutrientCode = @CFCNutrientCode and 
						AIMDatabaseID = @AimDatabaseID)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Nutrient code already exists. Please select a different code. Information is not saved!'

			END
		ELSE
			BEGIN
				INSERT
					dbo.CFCNutrient
						(
						  CFCNutrientCode
						, CFCNutrientName
						, UpdateStamp
						, CFCUserID
						, MathTypeID
						, AIMDatabaseID
						, IsComputed
						)
				VALUES
						(
						  @CFCNutrientCode
						, @CFCNutrientName
						, GETDATE()
						, @CFCUserID
						, @MathTypeID
						, @AimDatabaseID
						, @IsComputed
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
						REF.CFCNutrientID
					FROM
						dbo.CFCNutrientRefAIMNutrient REF WITH(NOLOCK)
					WHERE
						REF.CFCNutrientID = @CFCNutrientID
				)
				IF @IDinOtherTable IS NOT NULL
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'Unable to delete Nutrient because its being used in CFC Nutrient Analyzer Nutrient Reference table'
					END
			END

		IF @ErrorNumber = 0
			BEGIN
				UPDATE
					dbo.CFCNutrient WITH(ROWLOCK)
						SET 
							  							
							  CFCNutrientName = ISNULL(@CFCNutrientName, CFCNutrientName)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
							, MathTypeID = ISNULL(@MathTypeID, MathTypeID)
							, AIMDatabaseID = ISNULL(@AimDatabaseID, AIMDatabaseID)
							, IsComputed = ISNULL(@IsComputed, IsComputed)
						WHERE 
							CFCNutrientID = @CFCNutrientID
			END
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END