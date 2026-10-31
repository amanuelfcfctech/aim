CREATE PROCEDURE [dbo].[usp_CFCNutrientRefAIMNutrient_SAV]
	  @CFCNutrientRefAIMNutrientID		INT	
	, @CFCNutrientID					INT	= null
	, @AIMNutrientID					INT	= null
	, @CFCUserID						INT	= null
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

--4/17/18
DECLARE @LogStatus		CHAR = ''

IF EXISTS(
			SELECT
				CNNN.*
			FROM
				CFCNutrientRefAIMNutrient CNNN
			WHERE
					CNNN.CFCNutrientID = @CFCNutrientID
				AND CNNN.AIMNutrientID = @AIMNutrientID
				AND CNNN.CFCNutrientRefAIMNutrientID != @CFCNutrientRefAIMNutrientID
		)
BEGIN
	SET @ResultNumber = 1	
	SET	@ResultMessage = 'This Formulation nutrient already has a cross-reference assigned. Information is not saved!'
END

IF @ResultNumber = 0
BEGIN
	IF EXISTS(
				SELECT 
					CNNN.CFCNutrientRefAIMNutrientID
				FROM
					CFCNutrientRefAIMNutrient CNNN
				WHERE
					CNNN.CFCNutrientRefAIMNutrientID = @CFCNutrientRefAIMNutrientID
			)
		BEGIN
			SET @LogStatus = 'C'
			UPDATE
				dbo.CFCNutrientRefAIMNutrient	
					SET
						  CFCNutrientID = @CFCNutrientID
						, AIMNutrientID = @AIMNutrientID
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID
			WHERE 
				CFCNutrientRefAIMNutrientID = @CFCNutrientRefAIMNutrientID
			SET @RecordID = @@IDENTITY
		END
ELSE
	BEGIN
	SET @LogStatus = 'A'
	INSERT
		dbo.CFCNutrientRefAIMNutrient
		(
				CFCNutrientID
			, AIMNutrientID
			, UpdateStamp
			, CFCUserID
		)
		VALUES
		(
				@CFCNutrientID
			, @AIMNutrientID
			, GETDATE()
			, @CFCUserID
		)
		SET @RecordID = @@IDENTITY
	END

	--4/17/18 ----------------------------------------
	INSERT INTO CFCNutrientRefAIMNutrientLog (CFCNutrientID, AIMNutrientID, LogStatus, UpdateStamp, CFCUserID)
	SELECT	    @CFCNutrientID, @AIMNutrientID, @LogStatus, Getdate(), @CFCUserID 
	--------------------------------------------------

END


SELECT 
	  @RecordID AS RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage
END
