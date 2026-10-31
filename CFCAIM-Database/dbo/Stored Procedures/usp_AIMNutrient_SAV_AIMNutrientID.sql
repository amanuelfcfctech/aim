CREATE PROCEDURE [dbo].[usp_AIMNutrient_SAV_AIMNutrientID]
			  @AIMNutrientID					INT
			, @AIMNutrientCode					VARCHAR(10) = NULL
			, @AIMNutrientName					VARCHAR(30) = NULL
			, @AIMNutrientShortName				VARCHAR(15) = NULL
			, @UnitTypeID						INT = NULL
			, @MathTypeID						INT = NULL
			, @IsActive							BIT = NULL
			, @CFCUserID						INT = NULL
			, @PriorityAIMUsedValue				INT = NULL
			, @DataSourceTypeID					INT = NULL			
						
AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

-- 8/26/20
DECLARE @ActiveStatus BIT = 0

IF @AIMNutrientID = 0
	BEGIN
		IF EXISTS(SELECT AIMNutrientCode FROM dbo.AIMNutrient WHERE AIMNutrientCode = @AIMNutrientCode)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This AIM Nutrient Code already exists. Please choose a different one. Information is not saved!'
				SET @RecordID = (SELECT AIMNutrientID FROM dbo.AIMNutrient with (nolock) 
								WHERE AIMNutrientCode = @AIMNutrientCode)
				
				--8/26/20
				SET @ActiveStatus = (SELECT top 1 IsActive FROM dbo.AIMNutrient AM WHERE AM.AIMNutrientID = @RecordID)
				If @ActiveStatus = 0					
					SET @ErrorMessage = 'This Nutrient code already exists and is INACTIVE.  Please reactivate or select a different code.'
					
				
			END
		ELSE
			BEGIN
				INSERT
					dbo.AIMNutrient 
						(
						  AIMNutrientCode
						, AIMNutrientName
						, AIMNutrientShortName
						, UnitTypeID
						, MathTypeID 
						, UpdateStamp
						, CFCUserID
						, PriorityAIMUsedValue
						, DataSourceTypeID
						
						)
				VALUES
						(
						  @AIMNutrientCode
						, @AIMNutrientName
						, @AIMNutrientShortName
						, @UnitTypeID
						, @MathTypeID
						, GETDATE()
						, @CFCUserID
						, @PriorityAIMUsedValue
						, @DataSourceTypeID
						
						)
				SET @RecordID = @@IDENTITY
			END
	END
ELSE
	BEGIN		

		IF @ErrorNumber = 0
			BEGIN
				UPDATE
					dbo.AIMNutrient WITH(ROWLOCK)
						SET 
							  AIMNutrientCode = ISNULL(@AIMNutrientCode, AIMNutrientCode)
							, AIMNutrientName = ISNULL(@AIMNutrientName, AIMNutrientName)
							, AIMNutrientShortName = ISNULL(@AIMNutrientShortName, AIMNutrientShortName)
							, UnitTypeID = ISNULL(@UnitTypeID, UnitTypeID)
							, MathTypeID = ISNULL(@MathTypeID, MathTypeID)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
							, PriorityAIMUsedValue = ISNULL(@PriorityAIMUsedValue, PriorityAIMUsedValue)
							, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
							
						WHERE 
							AIMNutrientID = @AIMNutrientID
						SET @RecordID = @AIMNutrientID
			END
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
