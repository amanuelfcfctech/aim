CREATE PROCEDURE [dbo].[usp_SampleType_DEL]
				@SampleTypeID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleType WITH(ROWLOCK)
WHERE
	SampleTypeID = @SampleTypeID

END