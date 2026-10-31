
CREATE PROCEDURE [dbo].[usp_AIMMessage_INS]
				@TypeID			INT				= NULL
			  ,	@Message		NVARCHAR(250)	= NULL
			  , @CFCUserID		INT				= NULL

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber			INT = 0
DECLARE @ErrorMessage			VARCHAR(200) = ''

--typeID -	1 = System
--			2 = User
--			3 = Nutrionist
--			4 = AIMWorker



INSERT
	INTO dbo.AIMMessage
	(
	  TypeID
	, [Message]	
	, CFCUserID
	, UpdateStamp
	)
SELECT
	  @TypeID
	, @Message
	, @CFCUserID
	, GetDate()

SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END