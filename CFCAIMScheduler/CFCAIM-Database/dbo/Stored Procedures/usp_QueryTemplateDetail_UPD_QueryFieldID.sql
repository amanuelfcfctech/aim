CREATE PROCEDURE [dbo].[usp_QueryTemplateDetail_UPD_QueryFieldID]
			@QueryTemplateDetailID			INT		= NULL
			, @QueryFieldID					INT		= NULL
			, @CFCUserID					INT		= NULL

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''


BEGIN
	UPDATE
		dbo.QueryTemplateDetail WITH(ROWLOCK)
	SET 
		QueryFieldID = @QueryFieldID		
		, UpdateStamp = GETDATE()
		, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
	WHERE 		
		QueryTemplateDetailID = @QueryTemplateDetailID
			   
END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END