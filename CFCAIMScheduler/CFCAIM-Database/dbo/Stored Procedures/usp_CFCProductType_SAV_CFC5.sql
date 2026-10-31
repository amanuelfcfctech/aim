

CREATE PROCEDURE [dbo].[usp_CFCProductType_SAV_CFC5]
			  @ProductTypeNumber			INT
			, @ProductTypeName				VARCHAR(40)		= NULL
			, @AIMDatabaseID				INT				= NULL
			, @CFCUserID					INT				= NULL
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber		INT = 0
DECLARE @ErrorMessage		VARCHAR(200) = ''
DECLARE @CFCProductTypeID	INT = 0

SELECT 
	@CFCProductTypeID = CFCProductTypeID 
FROM 
	dbo.CFCProductType CPT
WHERE 
	CPT.ProductTypeNumber = @ProductTypeNumber 
	AND CPT.AIMDatabaseID = @AimDatabaseID

IF @CFCProductTypeID = 0 
	BEGIN
		INSERT
			dbo.CFCProductType
				(
				  ProductTypeNumber
				, ProductTypeName
				, UpdateStamp
				, CFCUserID
				, AIMDatabaseID
				)
		VALUES
				(
				  @ProductTypeNumber
				, @ProductTypeName
				, GETDATE()
				, @CFCUserID
				, @AimDatabaseID
				)
	END
ELSE
	BEGIN
		UPDATE
			dbo.CFCProductType 
		SET 					  							
			  ProductTypeName = ISNULL(@ProductTypeName, ProductTypeName)
			, UpdateStamp = GETDATE()
			, CFCUserID = @CFCUserID
			, AIMDatabaseID = ISNULL(@AimDatabaseID, AIMDatabaseID)
		WHERE 
			CFCProductTypeID = @CFCProductTypeID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END