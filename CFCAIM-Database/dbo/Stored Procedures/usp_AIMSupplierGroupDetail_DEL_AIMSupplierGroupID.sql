create PROCEDURE [dbo].[usp_AIMSupplierGroupDetail_DEL_AIMSupplierGroupID]
	  @AIMSupplierGroupID		INT
	
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.AIMSupplierGroupDetail	
WHERE 
	AIMSupplierGroupID = @AIMSupplierGroupID 
	
	
END