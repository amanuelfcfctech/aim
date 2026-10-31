CREATE PROCEDURE [dbo].[usp_AIMSupplierLocationGroup_DEL_AIMSupplierLocationGroupID]
	  @AIMSupplierLocationGroupID		INT
	
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.AIMSupplierLocationGroup
WHERE 
	AIMSupplierLocationGroupID = @AIMSupplierLocationGroupID 
	
	
END