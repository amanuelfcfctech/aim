CREATE PROCEDURE [dbo].[usp_TabOrderGroup_DEL_TabOrderGroupID]
				@TabOrderGroupID			INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber	INT			= 0
DECLARE @ErrorMessage	VARCHAR(50)


DELETE
	dbo.TabOrderGroupDetail 
WHERE	
	TabOrderGroupID = @TabOrderGroupID

SET @ErrorNumber = @@ERROR

IF @ErrorNumber > 0
	BEGIN
		SET @ErrorMessage = 'Tab Order Group Detail did not get deleted'
	END
ELSE
	BEGIN
		DELETE
			dbo.TabOrderGroup 
		WHERE	
			TabOrderGroupID = @TabOrderGroupID

		IF @ErrorNumber > 0
			BEGIN
				SET @ErrorMessage = 'Tab Order Group did not get deleted'
			END
	END


SELECT 
	  0 AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END