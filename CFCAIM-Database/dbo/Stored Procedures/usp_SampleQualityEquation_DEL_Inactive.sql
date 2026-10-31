CREATE PROCEDURE [dbo].[usp_SampleQualityEquation_DEL_Inactive]
	
--exec usp_SampleQualityEquation_DEL_Inactive
				
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleQualityEquation WITH(ROWLOCK)
WHERE
	IsActive = 0


DELETE 
	SampleQualityEquationBuild
FROM 
	SampleQualityEquationBuild SQEB
LEFT OUTER JOIN SampleQualityEquation SQE 
	ON SQEB.SampleQualityEquationID = SQE.SampleQualityEquationID
WHERE 
	SQE.SampleQualityEquationID IS NULL

END