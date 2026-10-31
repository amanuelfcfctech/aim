CREATE PROCEDURE [CFCUser].[usp_CFCUser_UPD_AIMOwnerLocationGroupID]
	@AIMOwnerLocationGroupID		INT
	, @Value						INT

AS
BEGIN
SET NOCOUNT ON;

UPDATE 
	CFCUser.CFCUser 
SET 
	AIMOwnerLocationGroupID = @Value
WHERE 
	AIMOwnerLocationGroupID = @AIMOwnerLocationGroupID


END