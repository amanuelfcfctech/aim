CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_DEL_AIMOwnerID_AIMOwnerLocationID]
			  @AIMOwnerLocationID					INT
			, @AIMOwnerID							INT
			
AS

BEGIN

SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @ID INT

BEGIN
	SET @ID = (SELECT TOP 1 AIMOwnerLocationID FROM dbo.Sample S WITH(NOLOCK) 
				WHERE S.AIMOwnerLocationID = @AIMOwnerLocationID
								and S.AIMOwnerID = @AIMOwnerID)
	IF Len(@ID) > 0
		BEGIN			
			SET @ErrorNumber = 1
			SET @ErrorMessage = 'Unable to delete owner location because it is being used in sample table.' 
		END
END

IF @ErrorNumber = 0
	BEGIN
		DELETE FROM
			dbo.AIMOwnerLocation WITH(ROWLOCK)				
		WHERE
			AIMOwnerID = @AIMOwnerID  
			and AIMOwnerLocationID = @AIMOwnerLocationID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END