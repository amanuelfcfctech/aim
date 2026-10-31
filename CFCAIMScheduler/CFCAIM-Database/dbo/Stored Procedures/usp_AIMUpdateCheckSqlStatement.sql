CREATE PROCEDURE [dbo].[usp_AIMUpdateCheckSqlStatement]

@Sql nvarchar(max)
--usp_AIMUpdateCheckSqlStatement "insert into version (version) select '11/03/2016' "

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorMessage	VARCHAR(200) = ''
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorLine		INT = 0
DECLARE @ErrorSeverity	INT = 0
DECLARE @ErrorState		INT = 0
DECLARE @Testsql NVARCHAR(MAX)
DECLARE @Result INT

BEGIN TRY
	SET @Testsql = N'set parseonly on; ' + @Sql
	EXEC @Result = sp_executesql @Testsql
END TRY
BEGIN CATCH	
	SET @ErrorMessage = ERROR_MESSAGE()
	SET @ErrorNumber = ERROR_NUMBER()
	SET @ErrorLine =  ERROR_LINE()
	SET @ErrorSeverity = ERROR_SEVERITY()
	SET @ErrorState = ERROR_STATE() 
END CATCH

SELECT 
	  @ErrorMessage AS [Message]
	, @ErrorNumber AS ErrorNumber
	, @ErrorLine AS ErrorLine
	, @ErrorSeverity AS ErrorSeverity
	, @ErrorState AS ErrorState

END