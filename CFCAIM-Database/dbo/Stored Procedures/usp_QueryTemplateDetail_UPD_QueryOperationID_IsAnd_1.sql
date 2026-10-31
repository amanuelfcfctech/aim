create PROCEDURE [dbo].[usp_QueryTemplateDetail_UPD_QueryOperationID_IsAnd]
			  @QueryTemplateID				INT		= NULL
			, @QueryFieldID					INT		= NULL
			, @QueryOperationID				INT		= NULL
			, @IsAnd						BIT		= NULL
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
		QueryOperationID = @QueryOperationID
		, IsAnd = @IsAnd
		, UpdateStamp = GETDATE()
		, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
	WHERE 
		QueryTemplateID = @QueryTemplateID and
		QueryFieldID = @QueryFieldID
END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END