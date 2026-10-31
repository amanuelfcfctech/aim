
CREATE PROCEDURE [dbo].[usp_CFCProduct_SAV_CFC5]
			  @CFCProductID			INT
			, @CFCPlantCode			VARCHAR(8)		= NULL
			, @CFCProductCode		VARCHAR(8)		= NULL
			, @CFCProductName		VARCHAR(40)		= NULL
			, @IsActive				BIT				= NULL
			, @CFCUserID			INT				= NULL
			, @AIMDatabaseID		INT				= NULL
			, @CFCInType			VARCHAR(200)	= NULL
AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ReturnValue	INT
DECLARE @CFCPlantID		INT
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''


SELECT 
	@CFCPlantID = CFCPlantID
FROM
	dbo.CFCPlant CP
WHERE
	CP.CFCPlantCode = @CFCPlantCode
	AND AIMDatabaseID = @AIMDatabaseID

IF @CFCProductID = 0
	BEGIN
		SELECT 
			@CFCProductID = CP.CFCProductID 
		FROM 
			dbo.CFCProduct CP
		WHERE 
			CP.CFCProductCode = @CFCProductCode
			AND CP.CFCPlantID = @CFCPlantID 
			AND CP.AIMDatabaseID = @AIMDatabaseID
	END

IF @CFCPlantID = 0
	BEGIN
		SET @ErrorNumber = 1
		SET @ErrorMessage = 'This Plant is not set up. Please contact IT. Information is not saved!'
	END
ELSE
	BEGIN
		IF @CFCProductID = 0 
			BEGIN
				INSERT
					dbo.CFCProduct
						(
							  CFCProductCode
							, CFCProductName
							, CFCUserID
							, CFCPlantID
							, AIMDatabaseID
						)
				VALUES
						(
								@CFCProductCode
							, @CFCProductName
							, @CFCUserID
							, @CFCPlantID
							, @AIMDatabaseID
						)
				SET @CFCProductID = @@IDENTITY 
			END
		ELSE
			BEGIN
				UPDATE
					dbo.CFCProduct
				SET			
					  CFCProductName = ISNULL(@CFCProductName, CFCProductName)
					, UpdateStamp = GETDATE()
					, IsActive = ISNULL(@IsActive, IsActive)
					, CFCUserID = @CFCUserID
					, AIMDatabaseID = ISNULL(@AIMDatabaseID, AIMDatabaseID)
				WHERE
					CFCProductID = @CFCProductID
			END
	END


SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
END