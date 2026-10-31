CREATE PROCEDURE [dbo].[usp_UnitType_SAV_UnitTypeID]
			  @UnitTypeID					INT			
		    , @UnitTypeCode					VARCHAR(15)
			, @UnitTypeDescription			VARCHAR(30)
			, @IsActive						BIT
			, @CFCUserID					INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @UnitTypeID = 0
	BEGIN
		IF EXISTS(SELECT UnitTypeID FROM dbo.UnitType WHERE UnitTypeDescription = @UnitTypeDescription)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Unit Type already exists. Please enter a different description. Information is not saved!'
				SET @RecordID = (Select UnitTypeID From dbo.UnitType with (nolock)
										Where UnitTypeDescription = @UnitTypeDescription) 
			END
		ELSE
			BEGIN
				INSERT
					dbo.UnitType 
						(
							UnitTypeCode	
						  , UnitTypeDescription
						  , CFCUserID
						)
				VALUES
						(
							@UnitTypeCode
						  ,	@UnitTypeDescription
						  , @CFCUserID
						)
				SET @RecordID = @@IDENTITY
			END
	END
ELSE
	BEGIN
		IF @IsActive = 0
			BEGIN
				IF EXISTS(SELECT TOP 1 UnitTypeID FROM dbo.AIMNutrient WITH(NOLOCK) WHERE UnitTypeID = @UnitTypeID)
					BEGIN
						SET @ErrorNumber = 2
						SET @ErrorMessage = 'Unable to delete unit type because it is being used in AIM Nutrient table.'
					END
			END
	END
IF @ErrorNumber = 0
	BEGIN
		UPDATE
			dbo.UnitType WITH(ROWLOCK)
				SET 
					  UnitTypeCode = ISNULL(@UnitTypeCode, UnitTypeCode)
					, UnitTypeDescription = ISNULL(@UnitTypeDescription, UnitTypeDescription)
					, IsActive = ISNULL(@IsActive, IsActive)
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				WHERE 
					UnitTypeID = @UnitTypeID
				SET @RecordID = @UnitTypeID
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END