CREATE PROCEDURE [dbo].[usp_SampleQualityControlMassEntry_SAV]
			  @SampleQualityControlMassEntryID	INT			
			, @SampleQualityControlMassEntryName NVARCHAR(50)	= NULL			
			, @DataSourceTypeID					 INT			= NULL
			, @CFCUserID						 INT			= NULL		
			, @UpdateStamp						 DATETIME		= NULL
			

			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleQualityControlMassEntryID = 0
	BEGIN
		INSERT
			dbo.SampleQualityControlMassEntry
				(
				  SampleQualityControlMassEntryName				
				, DataSourceTypeID
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @SampleQualityControlMassEntryName											
				, @DataSourceTypeID
				, @CFCUserID
				, GetDate()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.SampleQualityControlMassEntry WITH(ROWLOCK)
				SET 
					  SampleQualityControlMassEntryName = ISNULL(@SampleQualityControlMassEntryName, SampleQualityControlMassEntryName)						
					, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)						
					, UpdateStamp = GETDATE()							
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					
					
				WHERE 
					SampleQualityControlMassEntryID = @SampleQualityControlMassEntryID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END