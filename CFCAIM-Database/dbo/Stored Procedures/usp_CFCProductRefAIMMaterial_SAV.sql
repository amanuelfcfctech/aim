CREATE PROCEDURE [dbo].[usp_CFCProductRefAIMMaterial_SAV]
	  @CFCProductRefAIMMaterialID		INT
	, @CFCProductID					INT
	, @AIMMaterialID					INT
	, @CFCUserID					INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @RecordID INT = 0
DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''

--4/17/18
DECLARE @LogStatus		CHAR = ''

IF EXISTS(
			SELECT
				*
			FROM
				CFCProductRefAIMMaterial CPNP
			WHERE
					CPNP.CFCProductID = @CFCProductID
				AND CPNP.AIMMaterialID = @AIMMaterialID
				AND CPNP.CFCProductRefAIMMaterialID != @CFCProductRefAIMMaterialID
		)
BEGIN
	SET @ResultNumber = 1
	SET	@ResultMessage = 'This product cross reference already exists. Information is not saved!'
END

IF @ResultNumber = 0
BEGIN
	IF EXISTS(
				SELECT 
					CPNP.CFCProductRefAIMMaterialID
				FROM
					CFCProductRefAIMMaterial CPNP
				WHERE
					CPNP.CFCProductRefAIMMaterialID = @CFCProductRefAIMMaterialID
			)
		BEGIN
			SET @LogStatus = 'C'
			UPDATE
				dbo.CFCProductRefAIMMaterial	
					SET
						  CFCProductID = @CFCProductID
						, AIMMaterialID = @AIMMaterialID
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID
			WHERE 
				CFCProductRefAIMMaterialID = @CFCProductRefAIMMaterialID
			SET @RecordID = @@IDENTITY
		END
	ELSE
		BEGIN
		SET @LogStatus = 'A'
		INSERT
			dbo.CFCProductRefAIMMaterial
			(
					CFCProductID
				, AIMMaterialID
				, UpdateStamp
				, CFCUserID
			)
			VALUES
			(
					@CFCProductID
				, @AIMMaterialID
				, GETDATE()
				, @CFCUserID
			)
			SET @RecordID = @@IDENTITY
		END



	--4/17/18 ----------------------------------------
	INSERT INTO CFCProductRefAIMMaterialLog (CFCProductID, AIMMaterialID, LogStatus, UpdateStamp, CFCUserID)
	SELECT	    @CFCProductID, @AIMMaterialID, @LogStatus, Getdate(), @CFCUserID 
	--------------------------------------------------


END


SELECT 
	  @RecordID as RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage
END