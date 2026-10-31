CREATE PROCEDURE [dbo].[usp_CFCPlant_UPD_All]
			  @CFCPlantID			INT
			, @CFCPlantCode			VARCHAR(10)
			, @CFCPlantName			VARCHAR(30)
			, @IsActive				BIT
			, @CFCUserID			INT

AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @IsActive = 0
BEGIN
	SET @IDinOtherTable =
	(
		SELECT 
			CFCPlantID
		FROM
			dbo.CFCPlantRefAIMOwnerLocation REF
		WHERE
			REF.CFCPlantID = @CFCPlantID
	)
	IF @IDinOtherTable IS NOT NULL
		BEGIN
			SET @ErrorNumber = 1
			SET @ErrorMessage = 'Unable to delete plant because it is being used in FRM plant cross reference table.'
		END
END

IF @ErrorNumber = 0
BEGIN

	UPDATE
	dbo.CFCPlant WITH(ROWLOCK)
		SET		
		  CFCPlantName = @CFCPlantName
		, UpdateStamp = GETDATE()
		, IsActive = @IsActive
		, CFCUserID = @CFCUserID

		WHERE
			CFCPlantID = @CFCPlantID
END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
END
