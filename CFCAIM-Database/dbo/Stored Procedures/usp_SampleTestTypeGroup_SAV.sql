CREATE PROCEDURE  [dbo].[usp_SampleTestTypeGroup_SAV]
			  @SampleTestTypeGroupID					INT
			, @SampleTestTypeGroupCode				NVARCHAR(50)
			, @CFCUserID						INT
			, @IsActive							BIT

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @SampleTestTypeGroupID = 0 
	BEGIN
		IF EXISTS(SELECT 
					SampleTestTypeGroupID					
				FROM 
					dbo.SampleTestTypeGroup STTG
				WHERE 
						STTG.SampleTestTypeGroupCode = @SampleTestTypeGroupCode
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Group already exists. Information is not saved.'
				SET @RecordID = (SELECT 
									SampleTestTypeGroupID 
								FROM 
									dbo.SampleTestTypeGroup STTG
								WHERE 
									STTG.SampleTestTypeGroupCode = @SampleTestTypeGroupCode
									)
			END
		ELSE
			BEGIN
				INSERT
					dbo.SampleTestTypeGroup
						(				
						  SampleTestTypeGroupCode												
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						  @SampleTestTypeGroupCode						
						, GETDATE()
						, @CFCUserID						
						, 1
						)
				SET @RecordID = @@IDENTITY
			END
		END
ELSE
		BEGIN
			UPDATE
				dbo.SampleTestTypeGroup WITH(ROWLOCK)
					SET 
						  SampleTestTypeGroupCode = ISNULL(@SampleTestTypeGroupCode, SampleTestTypeGroupCode)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						SampleTestTypeGroupID = @SampleTestTypeGroupID

			SET @RecordID = @SampleTestTypeGroupID
		END
		
SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END