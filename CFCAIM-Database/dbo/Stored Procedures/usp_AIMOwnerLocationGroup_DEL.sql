CREATE PROCEDURE [dbo].[usp_AIMOwnerLocationGroup_DEL]
				@AIMOwnerLocationGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.AIMOwnerLocationGroup WITH(ROWLOCK)
WHERE
	AIMOwnerLocationGroupID = @AIMOwnerLocationGroupID

END