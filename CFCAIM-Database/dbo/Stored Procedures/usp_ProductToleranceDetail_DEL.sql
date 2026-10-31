CREATE PROCEDURE [dbo].[usp_ProductToleranceDetail_DEL]
	  @ProductToleranceHeaderID		INT
	, @ProductToleranceDetailID		INT
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.ProductToleranceDetail	
WHERE 
	ProductToleranceHeaderID = @ProductToleranceHeaderID and
	ProductToleranceDetailID = @ProductToleranceDetailID
	
	
END