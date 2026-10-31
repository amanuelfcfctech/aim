CREATE PROCEDURE [dbo].[usp_SampleQualityEquationBuild_DEL_EquationID_Inactive]
				@SampleQualityEquationID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleQualityEquationBuild WITH(ROWLOCK)
WHERE
	SampleQualityEquationID = @SampleQualityEquationID
	and [Status] = 0

END