CREATE PROCEDURE [dbo].[usp_CFCPlant_SAV_CFC5]
			  @CFCPlantID					INT
			, @CFCPlantCode					VARCHAR(10)		= NULL
			, @CFCPlantName					VARCHAR(40)		= NULL
			, @IsActive						BIT				= NULL
			, @CFCUserID					INT				= NULL	
			, @AimDatabaseID				INT				= NULL
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @CFCPlantID = 0 
	BEGIN
		SELECT 
			@CFCPlantID = CFCPlantID 
		FROM 
			dbo.CFCPlant CP
		WHERE 
			CP.CFCPlantCode = @CFCPlantCode 
			AND CP.AIMDatabaseID = @AimDatabaseID
	END

IF @CFCPlantID = 0 
	BEGIN
		INSERT
			dbo.CFCPlant
				(
				  CFCPlantCode
				, CFCPlantName
				, UpdateStamp
				, CFCUserID
				, AIMDatabaseID
				)
		VALUES
				(
				  @CFCPlantCode
				, @CFCPlantName
				, GETDATE()
				, @CFCUserID
				, @AimDatabaseID
				)
	END
ELSE
	BEGIN
		UPDATE
			dbo.CFCPlant WITH(ROWLOCK)
		SET 					  							
			  CFCPlantName = ISNULL(@CFCPlantName, CFCPlantName)
			, IsActive = ISNULL(@IsActive, IsActive)
			, UpdateStamp = GETDATE()
			, CFCUserID = @CFCUserID
			, AIMDatabaseID = ISNULL(@AimDatabaseID, AIMDatabaseID)
		WHERE 
			CFCPlantID = @CFCPlantID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END