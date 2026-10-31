CREATE PROCEDURE [dbo].[usp_SampleTestTypeResult_DEL_SampleID]
				@SampleID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleTestTypeResult WITH(ROWLOCK)
WHERE
	SampleID = @SampleID

END