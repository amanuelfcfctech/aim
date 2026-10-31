
CREATE PROCEDURE [dbo].[usp_TabOrderGroupDetail_DEL_TabOrderGroupDetailID]
				@TabOrderGroupDetailID			INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber	INT			= 0
DECLARE @ErrorMessage	VARCHAR(50)


DELETE
	dbo.TabOrderGroupDetail 
WHERE	
	TabOrderGroupDetailID = @TabOrderGroupDetailID

SET @ErrorNumber = @@ERROR

IF @ErrorNumber > 0
	BEGIN
		SET @ErrorMessage = 'Tab Order Group Detail did not get deleted'
	END

SELECT 
	  0 AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END