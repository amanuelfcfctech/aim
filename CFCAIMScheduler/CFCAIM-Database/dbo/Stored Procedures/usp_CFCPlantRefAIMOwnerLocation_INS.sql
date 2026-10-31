CREATE PROCEDURE [dbo].[usp_CFCPlantRefAIMOwnerLocation_INS]
	  @CFCPlantID			INT
	, @AIMOwnerLocationID	INT
	, @CFCUserID			INT
AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

--4/17/18
DECLARE @LogStatus		CHAR = ''

IF EXISTS(SELECT 		
			CFCPlantRefAIMOwnerLocationID 
		FROM 
			dbo.CFCPlantRefAIMOwnerLocation XREF
		WHERE 
			XREF.CFCPlantID = @CFCPlantID )
		BEGIN
			SET @ErrorNumber = 1
			SET @ErrorMessage = 'A cross-reference already exists for this PlantID. Information is not saved.'
		END
ELSE
	
	BEGIN
				
		SET @LogStatus = 'A'
		INSERT INTO
		dbo.CFCPlantRefAIMOwnerLocation
			(
			  CFCPlantID
			, AIMOwnerLocationID
			, UpdateStamp
			, CFCUserID
			)
	VALUES
			(
			  @CFCPlantID
			, @AIMOwnerLocationID
			, GETDATE()
			, @CFCUserID
			)
	SET @RecordID = @@IDENTITY

	--4/17/18 ----------------------------------------
	INSERT INTO CFCPlantRefAIMOwnerLocationLog (CFCPlantID, AIMOwnerLocationID, LogStatus, UpdateStamp, CFCUserID)
	SELECT	    @CFCPlantID, @AIMOwnerLocationID, @LogStatus, Getdate(), @CFCUserID 
	--------------------------------------------------


	END




SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END

