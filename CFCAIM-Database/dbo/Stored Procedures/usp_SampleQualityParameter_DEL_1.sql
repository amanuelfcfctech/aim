CREATE PROCEDURE [dbo].[usp_SampleQualityParameter_DEL]
				@SampleQualityParameterID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleQualityParameter WITH(ROWLOCK)
WHERE
	SampleQualityParameterID = @SampleQualityParameterID

END