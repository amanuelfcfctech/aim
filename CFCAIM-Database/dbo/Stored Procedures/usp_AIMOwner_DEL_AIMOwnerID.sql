CREATE PROCEDURE [dbo].[usp_AIMOwner_DEL_AIMOwnerID]
			  @AIMOwnerID					INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @ID INT


BEGIN		

	SET @ID = (SELECT TOP 1 AIMOwnerID FROM dbo.Sample S WITH(NOLOCK) 
				WHERE S.AIMOwnerID = @AIMOwnerID)
	IF Len(@ID) > 0
		BEGIN			
			SET @ErrorNumber = 1
			SET @ErrorMessage = 'Unable to delete owner because it is being used in sample table.' 
		END
		
	IF EXISTS(SELECT TOP 1 AIMOwnerID FROM dbo.AIMOwnerLocation AOL WITH(NOLOCK) 
				WHERE AOL.AIMOwnerID = @AIMOwnerID and
						AOL.IsActive = 1)
		BEGIN
			SET @ErrorNumber = 2
			SET @ErrorMessage = 'Unable to delete Owner because an active Owner Location is assigned to it. You must first inactivate the owner-location(s).'										
		END

	--12/16/20
	IF EXISTS(SELECT TOP 1 AIMOwnerID FROM dbo.UserDefinedDataSourceDefinition DEF WITH(NOLOCK) 
				WHERE DEF.AIMOwnerID = @AIMOwnerID )
		BEGIN
			SET @ErrorNumber = 3
			SET @ErrorMessage = 'Unable to delete Owner because it is being used in user-defined import(s).'										
		END
END
	
IF @ErrorNumber = 0
	BEGIN

		---------------------------------------------------------------------------
		-- Delete OwnerLocations for this OwnerID in the AIMMaterialLocation table 
		DELETE M
		FROM	
			dbo.AIMMaterialLocation M
		INNER JOIN 
			dbo.AIMOwnerLocation AOL
				ON AOL.AIMOwnerLocationID = M.AIMOwnerLocationID
		INNER JOIN 
			dbo.AIMOwner AO
				ON AOL.AIMOwnerID = AO.AIMOwnerID
		WHERE 
			AOL.AIMOwnerID = @AIMOwnerID and AOL.IsActive = 0


		---------------------------------------------------------------------------
		-- Delete OwnerLocations for this OwnerID in the AIMOwnerLocation table 		
		DELETE FROM
			dbo.AIMOwnerLocation WITH(ROWLOCK)				
		WHERE
			AIMOwnerID = @AIMOwnerID and IsActive = 0


		---------------------------------------------------------------------------
		-- Delete Owner
		DELETE FROM
			dbo.AIMOwner WITH(ROWLOCK)				
		WHERE
			AIMOwnerID = @AIMOwnerID  			

	
	

	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	

END