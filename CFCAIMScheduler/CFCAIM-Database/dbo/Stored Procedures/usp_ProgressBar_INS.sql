
CREATE PROCEDURE [dbo].[usp_ProgressBar_INS]
			  @ProgressBarID			INT
			, @ProgressBarValue			INT			 = NULL	
			, @ProgressBarDescription	NVARCHAR(50) = NULL
			, @CFCUserID				INT			 = NULL
			, @ProgressBarType			INT			 = NULL

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber			INT = 0
DECLARE @ErrorMessage			VARCHAR(200) = ''


IF @ProgressBarID = 0 
BEGIN
	INSERT INTO 
		dbo.ProgressBar 
			(
				ProgressBarValue 
			, ProgressBarDescription
			, CFCUserID
			, ProgressBarType
			)
	VALUES
			(
				@ProgressBarValue
			, @ProgressBarDescription
			, @CFCUserID
			, @ProgressBarType
						
			)
	SET	@ProgressBarID = @@IDENTITY
END

SET @ProgressBarID	= @@IDENTITY

SELECT 
	  @ProgressBarID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
