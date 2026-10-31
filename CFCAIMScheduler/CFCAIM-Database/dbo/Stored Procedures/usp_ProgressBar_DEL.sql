CREATE PROCEDURE [dbo].[usp_ProgressBar_DEL]
			  @ProgressBarID			INT						

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber			INT = 0
DECLARE @ErrorMessage			VARCHAR(200) = ''


BEGIN
	DELETE FROM 
		dbo.ProgressBar with (rowlock) 
	WHERE
		ProgressBarID =	@ProgressBarID 
END

SET @ProgressBarID	= @@IDENTITY

SELECT 
	  @ProgressBarID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
