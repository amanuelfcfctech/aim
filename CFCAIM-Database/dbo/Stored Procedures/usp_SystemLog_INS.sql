
CREATE PROCEDURE [dbo].[usp_SystemLog_INS]
				@TypeID			INT				= NULL
			  ,	@Message		NVARCHAR(250)	= NULL
			  , @SourceLineNumber INT			= NULL			  			  
			  , @MethodName		NVARCHAR(50)	= NULL
			  , @CFCUserID		INT				= NULL

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber			INT = 0
DECLARE @AnalyzerAnalyseID		INT = 0
DECLARE @ErrorMessage			VARCHAR(200) = ''

--typeID -	1 = Info
--			2 = Warning
--			3 = Fatal
--			4 = Debug


INSERT
	INTO dbo.SystemLog
	(
	  TypeID
	, [Message]
	, SourceLineNumber		
	, MethodName
	, CFCUserID
	)
SELECT
	  @TypeID
	, @Message
	, @SourceLineNumber		
	, @MethodName
	, @CFCUserID

END

