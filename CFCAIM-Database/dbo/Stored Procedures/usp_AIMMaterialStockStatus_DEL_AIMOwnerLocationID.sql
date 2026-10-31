CREATE PROCEDURE [dbo].[usp_AIMMaterialStockStatus_DEL_AIMOwnerLocationID]
	  @AIMOwnerLocationID	INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.AIMMaterialStockStatus	WITH(ROWLOCK)
WHERE 	
	AIMOwnerLocationID = @AIMOwnerLocationID
END