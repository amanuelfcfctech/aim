CREATE PROCEDURE [dbo].[usp_SampleQualityControlMassEntryDetail_SAV]
			  @SampleQualityControlMassEntryDetailID	INT
			, @SampleQualityControlMassEntryID		INT				= NULL
			, @SampleQualityParameterID				INT				= NULL
			, @SortOrder							INT				= NULL			
			, @UpdateStamp							DATETIME		= NULL
			, @CFCUserID							INT				= NULL
			, @ResultValue							DECIMAL(18,8)	= NULL
						
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleQualityControlMassEntryDetailID = 0
	BEGIN

		IF EXISTS(SELECT 
						SampleQualityControlMassEntryDetailID 
				FROM 
						dbo.SampleQualityControlMassEntryDetail MED
				WHERE 							
						MED.SampleQualityControlMassEntryID = @SampleQualityControlMassEntryID and 
						MED.SampleQualityParameterID = @SampleQualityParameterID)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This parameter already exists on the mass entry list.'
			END
		ELSE
			BEGIN			

				INSERT
					dbo.SampleQualityControlMassEntryDetail
						(
						SampleQualityControlMassEntryID
						, SampleQualityParameterID
						, SortOrder
						, CFCUserID
						, UpdateStamp				
						)
				VALUES
						(
						@SampleQualityControlMassEntryID
						, @SampleQualityParameterID
						, @SortOrder									
						, @CFCUserID
						, GetDate()
						)
				SET @RecordID = @@IDENTITY 
			END
	END
ELSE
	BEGIN
		IF @ErrorNumber = 0
			UPDATE
				dbo.SampleQualityControlMassEntryDetail WITH(ROWLOCK)
					SET 
						  SampleQualityControlMassEntryID = ISNULL(@SampleQualityControlMassEntryID, SampleQualityControlMassEntryID)
						, SampleQualityParameterID = ISNULL(@SampleQualityParameterID, SampleQualityParameterID)					
						, SortOrder = ISNULL(@SortOrder, SortOrder)														
						, UpdateStamp = GETDATE()							
						, CFCUserID = ISNULL(@CFCUserID, CFCUserID)											
					WHERE 
						SampleQualityControlMassEntryDetailID = @SampleQualityControlMassEntryDetailID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END