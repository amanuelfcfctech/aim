CREATE PROCEDURE [dbo].[usp_AIMMaterialStockStatus_DEL]
	  @AIMMaterialID		INT
	  , @AIMOwnerLocationID	INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.AIMMaterialStockStatus	WITH(ROWLOCK)
WHERE 
	AIMMaterialID = @AIMMaterialID and
	AIMOwnerLocationID = @AIMOwnerLocationID
END