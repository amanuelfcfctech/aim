CREATE PROCEDURE [dbo].[usp_CFCNutrientRefAIMNutrient_DEL_ID]
	  @ID		            INT
	  , @CFCUserID			INT

AS
BEGIN
SET NOCOUNT ON;


--4/17/18 ----------------------------------------
INSERT INTO CFCNutrientRefAIMNutrientLog (CFCNutrientID, AIMNutrientID, LogStatus, UpdateStamp, CFCUserID)
SELECT		CFCNutrientID, AIMNutrientID, 'D', Getdate(), @CFCUserID 
FROM		dbo.CFCNutrientRefAIMNutrient
WHERE		CFCNutrientRefAIMNutrientID = @ID
--------------------------------------------------

DELETE
	dbo.CFCNutrientRefAIMNutrient	WITH(ROWLOCK)
WHERE 
	CFCNutrientRefAIMNutrientID = @ID
END
