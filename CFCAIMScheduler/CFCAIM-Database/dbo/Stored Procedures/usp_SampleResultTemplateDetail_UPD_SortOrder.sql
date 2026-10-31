CREATE PROCEDURE [dbo].[usp_SampleResultTemplateDetail_UPD_SortOrder]
			  @SampleResultTemplateID			INT
			, @SampleResultTemplateDetailID		INT
			, @SortOrder						INT
			, @CFCUserID						INT

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''


UPDATE
	dbo.SampleResultTemplateDetail WITH(ROWLOCK)
		SET
			SortOrder = @SortOrder
		  , UpdateStamp = GETDATE()
		  , CFCUserID = @CFCUserID

		WHERE 
			SampleResultTemplateID = @SampleResultTemplateID and 
			SampleResultTemplateDetailID = @SampleResultTemplateDetailID

IF EXISTS(SELECT TOP 1
			T.SortOrder 
		FROM
			dbo.SampleResultTemplateDetail T WITH(NOLOCK)
		WHERE
			SampleResultTemplateID = @SampleResultTemplateID and
			T.SortOrder != 0
		GROUP BY
			T.SortOrder
		HAVING 
			COUNT(*) > 1)
	BEGIN
		UPDATE
			dbo.SampleResultTemplateDetail WITH(ROWLOCK)
				SET
					  SortOrder = SortOrder + 1
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
				WHERE
						SampleResultTemplateID = @SampleResultTemplateID
					AND SampleResultTemplateDetailID != @SampleResultTemplateDetailID
					AND SortOrder >= @SortOrder
					AND SortOrder != 0
	END
END