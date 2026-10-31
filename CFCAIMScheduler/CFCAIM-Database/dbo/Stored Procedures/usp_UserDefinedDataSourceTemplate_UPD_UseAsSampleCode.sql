CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_UPD_UseAsSampleCode]
				
			   @UserDefinedDataSourceTemplateID			INT			  
			  , @UseAsSampleCodeValue					BIT	  -- 0 or 1
			  , @CFCUserID								INT = NULL			

AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
	
BEGIN

		
	UPDATE
		dbo.UserDefinedDataSourceTemplate
	SET 
		UseAsSampleCode = @UseAsSampleCodeValue
		, CFCUserID = @CFCUserID
	WHERE
		UserDefinedDataSourceTemplateID = @UserDefinedDataSourceTemplateID

END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END