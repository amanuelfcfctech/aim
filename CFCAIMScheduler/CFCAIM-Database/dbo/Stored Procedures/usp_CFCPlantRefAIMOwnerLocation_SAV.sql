CREATE PROCEDURE [dbo].[usp_CFCPlantRefAIMOwnerLocation_SAV]
	  @CFCPlantRefAIMOwnerLocationID	INT
	, @CFCPlantID						INT
	, @AIMOwnerLocationID				INT
	, @CFCUserID						INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

--4/17/18
DECLARE @LogStatus		CHAR = ''

IF @CFCPlantRefAIMOwnerLocationID = 0
	BEGIN
		IF EXISTS(SELECT CFCPlantRefAIMOwnerLocationID FROM dbo.CFCPlantRefAIMOwnerLocation WHERE CFCPlantRefAIMOwnerLocationID = @CFCPlantRefAIMOwnerLocationID)
			BEGIN
				SET @ResultNumber = 1
				SET @ResultMessage = 'This plant cross reference already exists. Information is not saved!'
			END
	ELSE
		BEGIN
			SET @LogStatus = 'A'
			INSERT
				dbo.CFCPlantRefAIMOwnerLocation
				(
					CFCPlantID
				,	AIMOwnerLocationID
				,	UpdateStamp
				,	CFCUserID
				)
			VALUES
				(
					@CFCPlantID
				,	@AIMOwnerLocationID
				, getdate()
				, @CFCUserID
				)
			SET @RecordID = @@IDENTITY
		END
	END

ELSE
	BEGIN
		SET @LogStatus = 'C'
		UPDATE
			dbo.CFCPlantRefAIMOwnerLocation
				SET
						CFCPlantID = @CFCPlantID
					, AIMOwnerLocationID = @AIMOwnerLocationID
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
		WHERE 
			CFCPlantRefAIMOwnerLocationID = @CFCPlantRefAIMOwnerLocationID
		SET @RecordID = @@IDENTITY
	END



--4/17/18 ----------------------------------------
if @ResultNumber = 0
Begin
	INSERT INTO CFCPlantRefAIMOwnerLocationLog (CFCPlantID, AIMOwnerLocationID, LogStatus, UpdateStamp, CFCUserID)
	SELECT	    @CFCPlantID, @AIMOwnerLocationID, @LogStatus, Getdate(), @CFCUserID 
End
--------------------------------------------------

SELECT 
	  @RecordID AS RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage

END
