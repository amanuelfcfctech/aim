CREATE PROCEDURE [dbo].[usp_QueryTemplateDetail_COUNT_QueryTemplateID_QueryFieldID]
		@QueryTemplateID		INT
	    , @QueryFieldID			INT

-- exec usp_QueryTemplateDetail_COUNT_QueryTemplateID_QueryFieldID 21, 2
AS
BEGIN

SET NOCOUNT ON;

DECLARE @ReturnCount INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''


SET @ReturnCount = 
	(SELECT Count(*) 
		FROM dbo.QueryTemplateDetail 
		WHERE QueryTemplateID = @QueryTemplateID 
				and QueryFieldID = @QueryFieldID)



SELECT 
	  @ReturnCount as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage	

END