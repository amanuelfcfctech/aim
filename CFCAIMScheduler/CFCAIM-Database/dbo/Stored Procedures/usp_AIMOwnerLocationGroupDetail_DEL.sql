CREATE PROCEDURE [dbo].[usp_AIMOwnerLocationGroupDetail_DEL]
				@AIMOwnerLocationGroupDetailID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.AIMOwnerLocationGroupDetail WITH(ROWLOCK)
WHERE
	AIMOwnerLocationGroupDetailID = @AIMOwnerLocationGroupDetailID

END