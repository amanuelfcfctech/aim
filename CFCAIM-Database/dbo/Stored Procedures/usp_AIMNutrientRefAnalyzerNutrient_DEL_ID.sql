CREATE PROCEDURE [dbo].[usp_AIMNutrientRefAnalyzerNutrient_DEL_ID]
	  @ID		    INT
	 ,@CFCUserID	INT
AS
BEGIN
SET NOCOUNT ON;

--4/17/18 ----------------------------------------
INSERT INTO AIMNutrientRefAnalyzerNutrientLog (AIMNutrientID, AnalyzerNutrientID, LogStatus, UpdateStamp, CFCUserID)
SELECT		AIMNutrientID, AnalyzerNutrientID, 'D', Getdate(), @CFCUserID 
FROM		dbo.AIMNutrientRefAnalyzerNutrient
WHERE		AIMNutrientRefAnalyzerNutrientID = @ID
--------------------------------------------------

DELETE
	dbo.AIMNutrientRefAnalyzerNutrient	WITH(ROWLOCK)
WHERE 
	AIMNutrientRefAnalyzerNutrientID = @ID
END
