CREATE PROCEDURE [Vitapro].[usp_LineRefCFCPlant_SAV]
	  @LineRefCFCPlantID		INT
	, @LineID					INT
	, @LineName					VARCHAR(50) = NULL
	, @CFCPlantID				INT
	, @CFCUserID				INT
	, @IsActive					BIT = NULL

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

BEGIN
		

IF @IsActive = 1

	--------------------------------------------------------------------------------------
	IF EXISTS (SELECT 
					XREF.LineID 
				FROM 
					Vitapro.LineRefCFCPlant XREF 					
				--WHERE XRef.CFCPlantID = @CFCPlantID and Xref.IsActive = 1)
				WHERE XRef.CFCPlantID = @CFCPlantID and Xref.IsActive = 1 and XRef.LineID <> @LineID)
		BEGIN
			SET @ResultNumber = 1
			SET @ResultMessage = 'More than one cross-reference for this Concept 5 plant is not allowed. Information is not saved!'
		END
	

	ELSE
		BEGIN
			
			UPDATE
				Vitapro.LineRefCFCPlant
					SET
						  LineID = @LineID
						, CFCPlantID = @CFCPlantID
						, LineName = @LineName
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID
						, IsActive = ISNULL(@IsActive, IsActive)
			WHERE 
					LineRefCFCPlantID = @LineRefCFCPlantID

			SET @RecordID = @@IDENTITY

		END
ELSE
	BEGIN
		print 'update 2'
		UPDATE
			Vitapro.LineRefCFCPlant
				SET
					  LineID = @LineID
					, CFCPlantID = @CFCPlantID
					, LineName = @LineName
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
					, IsActive = ISNULL(@IsActive, IsActive)
		WHERE 
				LineRefCFCPlantID = @LineRefCFCPlantID

		SET @RecordID = @@IDENTITY

	END


END
	
SELECT 
	  @RecordID AS RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage

END