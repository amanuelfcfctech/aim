CREATE PROCEDURE [dbo].[usp_ProgressBar_UPD]

	@Description	NVARCHAR(50) = NULL
	, @CFCUserID	INT = NULL

AS

BEGIN	
SET NOCOUNT ON;	
DECLARE @intValu as INT 

SET @intValu = (SELECT
					ProgressBarValue 
				FROM 
					dbo.ProgressBar with (nolock))
IF @intValu IS NULL
	INSERT INTO 
		dbo.ProgressBar 
			(ProgressBarValue, ProgressBarDescription, CFCUserID)
	SELECT 5,'Begin Process', @CFCUserID
ELSE
	BEGIN
		BEGIN
			IF @intValu >= 100
				set @intValu = 0
			ELSE
				set @intValu = @intValu + 5		
		UPDATE 
			dbo.ProgressBar with (rowlock) 
		SET 
			ProgressBarValue = @intValu
			, ProgressBarDescription = ISNULL(@Description, ProgressBarDescription)
			, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
			, UpdateStamp = GetDate() 	
		END
	END
END
