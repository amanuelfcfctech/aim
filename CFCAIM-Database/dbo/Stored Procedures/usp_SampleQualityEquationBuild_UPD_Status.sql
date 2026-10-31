CREATE PROCEDURE [dbo].[usp_SampleQualityEquationBuild_UPD_Status]			  
			 @SampleQualityEquationID				INT						
			, @Status								BIT			= NULL			
			, @CFCUserID							INT			= NULL			
			
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''


BEGIN
	UPDATE
		dbo.SampleQualityEquationBuild WITH(ROWLOCK)
			SET 
				 [Status] = ISNULL(@Status, [Status])															
				, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
				, UpdateStamp = GETDATE()								
					
			WHERE 
				SampleQualityEquationID = @SampleQualityEquationID

		SET @RecordID = @@IDENTITY 
END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END