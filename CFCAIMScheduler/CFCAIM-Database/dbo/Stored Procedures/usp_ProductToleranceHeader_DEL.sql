CREATE PROCEDURE [dbo].[usp_ProductToleranceHeader_DEL]
	  @ProductToleranceHeaderID		INT
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.ProductToleranceHeader 
WHERE 
	ProductToleranceHeaderID = @ProductToleranceHeaderID
	
	
END