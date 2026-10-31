CREATE PROCEDURE [dbo].[usp_SampleType_SAV]
			  @SampleTypeID			INT
			, @SampleTypeName		VARCHAR(30) = NULL
			, @IsActive				BIT			= NULL									
			, @CFCUserID			INT			= NULL
			

--exec usp_SampleType_SAV 0, 'cm', 1, 0
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleTypeID = 0
	BEGIN
		INSERT
			dbo.SampleType
				(
				  SampleTypeName				
				, IsActive								
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @SampleTypeName				
				, @IsActive															
				, @CFCUserID
				, GETDATE()				
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.SampleType WITH(ROWLOCK)
				SET 
					  SampleTypeName = ISNULL(@SampleTypeName, SampleTypeName)					
					, IsActive = ISNULL(@IsActive, IsActive)						
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()					
					
				WHERE 
					SampleTypeID = @SampleTypeID

			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END