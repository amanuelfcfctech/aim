CREATE PROCEDURE [dbo].[usp_ProductToleranceDetail_DEL_ProductToleranceHeaderID]
	  @ProductToleranceHeaderID		INT
	
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.ProductToleranceDetail 
WHERE 
	ProductToleranceHeaderID = @ProductToleranceHeaderID 
	
	
END