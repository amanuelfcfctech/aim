CREATE PROCEDURE [dbo].[usp_CFCProductType_DEL_UpdateStampAIMDatabaseID]
					  @UpdateStamp					DATETIME
					, @AIMDatabaseID				INT				= NULL 
AS
BEGIN
SET NOCOUNT ON;

DELETE 
	dbo.CFCProductType 
WHERE
	UpdateStamp < @UpdateStamp
	AND AIMDatabaseID = @AIMDatabaseID	

END