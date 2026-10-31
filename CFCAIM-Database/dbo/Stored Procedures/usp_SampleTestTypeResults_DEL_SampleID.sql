CREATE PROCEDURE [dbo].[usp_SampleTestTypeResults_DEL_SampleID]
				@SampleID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleTestTypeResults WITH(ROWLOCK)
WHERE
	SampleID = @SampleID

END