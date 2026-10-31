CREATE PROCEDURE [dbo].[usp_SampleQualityParameter_SAV]
			  @SampleQualityParameterID			INT
			, @SampleQualityParameterName		NVarchar(50)	= NULL
			, @DataSourceTypeID					INT = NULL
			, @ParameterIsNumeric				BIT	= NULL
			, @IsActive							BIT	= NULL
			, @AIMNutrientID					INT	= NULL		
			, @CFCUserID						INT	= NULL			
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''


IF EXISTS(
			SELECT
				SQP.*
			FROM
				SampleQualityParameter SQP
			WHERE
					SQP.SampleQualityParameterName = @SampleQualityParameterName	
				AND SQP.DataSourceTypeID = @DataSourceTypeID			
				AND SQP.SampleQualityParameterID != @SampleQualityParameterID
		)
BEGIN
	SET @ErrorNumber = 1
	SET	@ErrorMessage = 'This parameter name already exists for the datasource. Information is not saved!'
END

IF @ErrorNumber = 0
BEGIN
	IF @SampleQualityParameterID = 0
		BEGIN
			INSERT
				dbo.SampleQualityParameter 
					(
					  SampleQualityParameterName
					, DataSourceTypeID
					, ParameterIsNumeric
					, IsActive	
					, CFCUserID
					, UpdateStamp				
					)
			VALUES
					(
					  @SampleQualityParameterName
					, @DataSourceTypeID
					, @ParameterIsNumeric
					, @IsActive														
					, @CFCUserID
					, GETDATE()
					)
			SET @RecordID = @@IDENTITY 
		END
	ELSE
		BEGIN
			UPDATE
				dbo.SampleQualityParameter WITH(ROWLOCK)
					SET 
						  SampleQualityParameterName = ISNULL(@SampleQualityParameterName, SampleQualityParameterName)
						, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
						, ParameterIsNumeric = ISNULL(@ParameterIsNumeric, ParameterIsNumeric)
						, IsActive = ISNULL(@IsActive, IsActive)															
						, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
						, UpdateStamp = GETDATE()
					
					WHERE 
						SampleQualityParameterID = @SampleQualityParameterID

				SET @RecordID = @@IDENTITY 
		END
END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END