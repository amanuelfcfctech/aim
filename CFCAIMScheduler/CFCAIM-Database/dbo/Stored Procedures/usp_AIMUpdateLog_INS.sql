CREATE PROCEDURE [dbo].[usp_AIMUpdateLog_INS]
			  @BatchID					INT = NULL
			, @SqlCommand				VARCHAR(MAX) = NULL
			, @CommandType				INT = NULL	
			, @Message					NVARCHAR(200) = NULL			
			, @ErrorNumber				INT = NULL			
			, @CFCUserID				INT = NULL
			, @UpdateStamp				DATETIME = NULL
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @AIMUpdateLogID			INT = 0
DECLARE @ProcErrorNumber		INT = 0
DECLARE @ProcErrorMessage		VARCHAR(200) = ''

	BEGIN
	INSERT
		dbo.AIMUpdateLog 
			(
			  BatchID
			, SqlCommand
			, CommandType
			, [Message]
			, ErrorNumber			
			, UpdateStamp
			, CFCUserID		
			)
	VALUES
			(
			  @BatchID
			, @SqlCommand
			, @CommandType
			, @Message
			, @ErrorNumber			
			, GETDATE()		
			, @CFCUserID		
		
			)
	SET @AIMUpdateLogID	= @@IDENTITY
	END

SELECT 
	  @AIMUpdateLogID AS RecordID
	, @ProcErrorNumber AS ErrorNumber
	, @ProcErrorMessage AS ErrorMessage

END