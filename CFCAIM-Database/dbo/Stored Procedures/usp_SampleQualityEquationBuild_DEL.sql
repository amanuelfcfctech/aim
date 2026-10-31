CREATE PROCEDURE [dbo].[usp_SampleQualityEquationBuild_DEL]
				@SampleQualityEquationID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleQualityEquationBuild WITH(ROWLOCK)
WHERE
	SampleQualityEquationID = @SampleQualityEquationID

END