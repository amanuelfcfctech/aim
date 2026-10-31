CREATE PROCEDURE [dbo].[usp_ProgressBar_SEL_ProgressBarDescription_CFCUserID]
				@ProgressBarDescription		VARCHAR(50)
				, @CFCUserID				INT

--EXEC DBO.usp_ProgressBar_SEL_ProgressBarDescription_CFCUserID 'Rebuild',1
			
AS

DECLARE @STR Varchar(2000)

BEGIN
SET NOCOUNT ON;

SET @STR = '

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
		P.CFCUserID = ' + CAST(@CFCUserID AS VARCHAR) + ' and 	

		P.ProgressBarDescription LIKE ''%' + @ProgressBarDescription + '%''	
		'
	
--PRINT @str
EXEC (@STR)
END