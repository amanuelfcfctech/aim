CREATE PROCEDURE [dbo].[usp_QueryTemplateDetail_UPD_OrderBy]
			@QueryTemplateDetailID			INT		= NULL
			, @OrderBy						INT		= NULL
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
		OrderBy = @OrderBy		
		, UpdateStamp = GETDATE()
		, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
	WHERE 		
		QueryTemplateDetailID = @QueryTemplateDetailID


	IF EXISTS(SELECT TOP 1
			Q.OrderBy 
		FROM
			dbo.QueryTemplateDetail Q WITH(NOLOCK)
		WHERE 
			Q.OrderBy != 0
		GROUP BY
			Q.OrderBy
		HAVING 
			COUNT(*) > 1)
	BEGIN
		UPDATE
			dbo.QueryTemplateDetail WITH(ROWLOCK)
				SET
					  OrderBy = OrderBy + 1
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
				WHERE
						QueryTemplateDetailID != @QueryTemplateDetailID
					AND OrderBy >= @OrderBy
					AND OrderBy != 0
	END



END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END