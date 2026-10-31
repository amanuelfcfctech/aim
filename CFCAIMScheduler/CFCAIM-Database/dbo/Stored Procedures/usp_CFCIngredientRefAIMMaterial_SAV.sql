CREATE PROCEDURE [dbo].[usp_CFCIngredientRefAIMMaterial_SAV]
	  @CFCIngredientRefAIMMaterialID		INT
	, @CFCIngredientID						INT
	, @AnalyzerMaterialID						INT
	, @CFCUserID							INT
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
				CFCIngredientRefAIMMaterial CNNN
			WHERE
					CNNN.CFCIngredientID = @CFCIngredientID
				AND CNNN.CFCIngredientRefAIMMaterialID != @CFCIngredientRefAIMMaterialID
		)
BEGIN
	SET @ResultNumber = 1
	SET	@ResultMessage = 'This Formulation ingredient already has a cross-reference assigned. (Only 1 Formulation x-ref to many AIM x-refs is allowed.)  Information is not saved!'
END

IF @ResultNumber = 0
BEGIN
		IF EXISTS(
				SELECT 
					CNNN.CFCIngredientRefAIMMaterialID
				FROM
					CFCIngredientRefAIMMaterial CNNN
				WHERE
					CNNN.CFCIngredientRefAIMMaterialID = @CFCIngredientRefAIMMaterialID
				)
			BEGIN
				SET @LogStatus = 'C'
				UPDATE dbo.CFCIngredientRefAIMMaterial
					SET
						  CFCIngredientID = @CFCIngredientID
						, AIMMaterialID = @AnalyzerMaterialID
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID
				WHERE 
					CFCIngredientRefAIMMaterialID = @CFCIngredientRefAIMMaterialID
					SET @RecordID = @@IDENTITY
			END
		ELSE
			BEGIN

				SET @LogStatus = 'A'
				INSERT
					dbo.CFCIngredientRefAIMMaterial
					(
						  CFCIngredientID
						, AIMMaterialID
						, UpdateStamp
						, CFCUserID
					)
					VALUES
					(
						  @CFCIngredientID
						, @AnalyzerMaterialID
						, GETDATE()
						, @CFCUserID
					)
					SET @RecordID = @@IDENTITY
			END

	--4/17/18 ----------------------------------------
	INSERT INTO CFCIngredientRefAIMMaterialLog (CFCIngredientID, AIMMaterialID, LogStatus, UpdateStamp, CFCUserID)
	SELECT	    @CFCIngredientID, @AnalyzerMaterialID, @LogStatus, Getdate(), @CFCUserID 
	--------------------------------------------------

END



SELECT 
	  @RecordID as RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage
END
