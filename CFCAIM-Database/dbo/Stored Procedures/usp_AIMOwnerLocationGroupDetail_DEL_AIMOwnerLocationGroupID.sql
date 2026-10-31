CREATE PROCEDURE [dbo].[usp_AIMOwnerLocationGroupDetail_DEL_AIMOwnerLocationGroupID]
	  @AIMOwnerLocationGroupID		INT
	
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.AIMOwnerLocationGroupDetail	
WHERE 
	AIMOwnerLocationGroupID = @AIMOwnerLocationGroupID 
	
	
END