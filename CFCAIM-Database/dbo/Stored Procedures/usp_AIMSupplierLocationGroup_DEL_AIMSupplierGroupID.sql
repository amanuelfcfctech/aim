CREATE PROCEDURE [dbo].[usp_AIMSupplierLocationGroup_DEL_AIMSupplierGroupID]
	  @AIMSupplierGroupID		INT
	
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.AIMSupplierLocationGroup
WHERE 
	AIMSupplierGroupID = @AIMSupplierGroupID 
	
	
END