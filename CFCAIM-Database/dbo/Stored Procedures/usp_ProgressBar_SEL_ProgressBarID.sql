CREATE PROCEDURE [dbo].[usp_ProgressBar_SEL_ProgressBarID]
				@ProgressBarID	INT
			
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  P.ProgressBarID
	, P.ProgressBarValue
	, P.ProgressBarDescription
	, P.UpdateStamp
	, P.CFCUserID
	, P.ProgressBarType
FROM 
	dbo.ProgressBar P 
WHERE 
	P.ProgressBarID = @ProgressBarID
	
END

