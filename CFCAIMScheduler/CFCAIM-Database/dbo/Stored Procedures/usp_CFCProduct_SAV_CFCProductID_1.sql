CREATE PROCEDURE [dbo].[usp_CFCProduct_SAV_CFCProductID]
			  @CFCProductID			INT
			, @CFCPlantID			INT
			, @CFCProductCode		VARCHAR(10)
			, @CFCProductName		VARCHAR(30)
			, @IsActive				BIT
			, @CFCUserID			INT
			, @AIMDatabaseID		INT

AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ReturnValue INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @CFCProductID = 0
BEGIN
	IF EXISTS(SELECT CFCProductID FROM CFCProduct WHERE CFCProductCode = @CFCProductCode
					and AIMDatabaseID = @AIMDatabaseID)
		BEGIN
			SET @ErrorNumber = 1
			SET @ErrorMessage = 'This product code already exists. Please enter a different code. Information is not saved!'

		END
	ELSE
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
		END
END
ELSE
	BEGIN
	IF @IsActive = 0
		BEGIN
			IF Exists
			(
				SELECT 
					REF.CFCProductID
				FROM
					dbo.CFCProductRefAIMMaterial REF
				WHERE
					REF.CFCProductID = @CFCProductID
			)
				BEGIN
					SET @ErrorNumber = 1
					SET @ErrorMessage = 'Unable to delete product because it is being used in FRM product cross reference table.'								
				END
		END


	IF @ErrorNumber = 0
	BEGIN
		UPDATE
		dbo.CFCProduct WITH(ROWLOCK)
			SET			
			CFCProductName = @CFCProductName			
			, UpdateStamp = GETDATE()
			, IsActive = @IsActive
			, CFCUserID = @CFCUserID
			, AIMDatabaseID = @AIMDatabaseID

			WHERE
				CFCProductID = @CFCProductID
	END
END
SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
END