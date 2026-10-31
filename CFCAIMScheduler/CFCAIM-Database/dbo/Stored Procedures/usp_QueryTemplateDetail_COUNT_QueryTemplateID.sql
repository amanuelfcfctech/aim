CREATE PROCEDURE [dbo].[usp_QueryTemplateDetail_COUNT_QueryTemplateID]
		@QueryTemplateID		INT
	    

-- exec usp_QueryTemplateDetail_COUNT_QueryTemplateID 21
AS
BEGIN

SET NOCOUNT ON;

DECLARE @ReturnCount INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''


SET @ReturnCount = 
	(SELECT Count(*) 
		FROM dbo.QueryTemplateDetail 
		WHERE QueryTemplateID = @QueryTemplateID)



SELECT 
	  @ReturnCount as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage	

END