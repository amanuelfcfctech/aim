create PROCEDURE [dbo].[usp_CFCProductType_DEL_ProductTypeNumberAIMDatabaseID]
					  @ProductTypeNumber			INT
					, @AIMDatabaseID				INT				= NULL 
AS
BEGIN
SET NOCOUNT ON;

DECLARE @CFCProductTypeID	INT = 0

SELECT 
	@CFCProductTypeID = ISNULL(CFCProductTypeID, 0)
FROM 
	dbo.CFCProductType CPT
WHERE 
	CPT.ProductTypeNumber = @ProductTypeNumber 
	AND CPT.AIMDatabaseID = @AimDatabaseID

IF @CFCProductTypeID > 0 
	BEGIN
		DELETE 
			dbo.CFCProductType 
		WHERE	
			CFCProductTypeID = @CFCProductTypeID	

		DELETE
			dbo.CFCProductRefCFCProductType
		WHERE	
			CFCProductTypeID = @CFCProductTypeID	
	END
END