CREATE PROCEDURE [dbo].[usp_AIMSupplierLocationGroup_DEL_AIMSupplierLocationID]
	  @AIMSupplierLocationID		INT
	
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.AIMSupplierLocationGroup
WHERE 
	AIMSupplierLocationID = @AIMSupplierLocationID 
	
	
END