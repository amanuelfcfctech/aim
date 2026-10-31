CREATE PROCEDURE [dbo].[usp_CFCProductRefAIMMaterial_DEL_ID]
	  @ID		            INT
	  , @CFCUserID			INT
AS
BEGIN
SET NOCOUNT ON;


--4/17/18 ----------------------------------------
INSERT INTO CFCProductRefAIMMaterialLog (CFCProductID, AIMMaterialID, LogStatus, UpdateStamp, CFCUserID)
SELECT		CFCProductID, AIMMaterialID, 'D', Getdate(), @CFCUserID 
FROM		dbo.CFCProductRefAIMMaterial
WHERE		CFCProductRefAIMMaterialID = @ID
--------------------------------------------------

DELETE
	dbo.CFCProductRefAIMMaterial	WITH(ROWLOCK)
WHERE 
	CFCProductRefAIMMaterialID = @ID
END
