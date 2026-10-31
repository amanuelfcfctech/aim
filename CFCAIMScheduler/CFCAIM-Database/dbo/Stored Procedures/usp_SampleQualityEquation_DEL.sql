CREATE PROCEDURE [dbo].[usp_SampleQualityEquation_DEL]
				@SampleQualityEquationID		INT
AS
BEGIN
SET NOCOUNT ON;

--EXEC usp_SampleQualityEquation_DEL 19

DELETE
FROM 
	dbo.SampleQualityEquation WITH(ROWLOCK)
WHERE
	SampleQualityEquationID = @SampleQualityEquationID


DELETE
	SampleQualityEquationBuild
FROM 
	dbo.SampleQualityEquationBuild SQEB WITH(ROWLOCK)
LEFT OUTER JOIN
	dbo.SampleQualityEquation SQE ON SQE.SampleQualityEquationID = SQEB.SampleQualityEquationID
WHERE
	SQE.SampleQualityEquationID IS NULL

END