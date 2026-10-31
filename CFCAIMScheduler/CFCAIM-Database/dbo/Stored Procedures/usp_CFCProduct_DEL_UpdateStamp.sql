CREATE PROCEDURE [dbo].[usp_CFCProduct_DEL_UpdateStamp]
				@UpdateStamp		DATETIME
AS
BEGIN
SET NOCOUNT ON;

DELETE 
	dbo.CFCProductRefAIMMaterial
FROM
	dbo.CFCProduct CP
	INNER JOIN 
		dbo.CFCProductRefAIMMaterial CPRAM
			ON CPRAM.CFCProductID = CP.CFCProductID 
WHERE
	CP.UpdateStamp < @UpdateStamp

DELETE
	dbo.CFCProduct
WHERE 
	UpdateStamp < @UpdateStamp

END