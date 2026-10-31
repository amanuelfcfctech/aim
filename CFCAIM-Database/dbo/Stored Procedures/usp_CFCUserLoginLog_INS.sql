CREATE PROCEDURE dbo.usp_CFCUserLoginLog_INS
	@CFCUserID INT
	,@DatabaseConnection NVARCHAR(100)
AS

DECLARE @ErrorNumber			INT = 0
DECLARE @ErrorMessage			VARCHAR(200) = ''


INSERT
	INTO dbo.CFCUserLoginLog
	(
	   CFCUserID 
	 , DatabaseConnection
	 , LoginDate 
	)
SELECT
	  @CFCUserID
    , @DatabaseConnection
	, GetDate()

SELECT 
	  @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage