CREATE PROCEDURE [dbo].[usp_SampleTestType_DEL]
				@SampleTestTypeID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleTestTypeDetail WITH(ROWLOCK)
WHERE
	SampleTestTypeID = @SampleTestTypeID


DELETE
FROM 
	dbo.SampleTestType 
WHERE
	SampleTestTypeID = @SampleTestTypeID

END