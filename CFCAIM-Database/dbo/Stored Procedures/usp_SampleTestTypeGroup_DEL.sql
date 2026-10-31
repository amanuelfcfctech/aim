CREATE PROCEDURE [dbo].[usp_SampleTestTypeGroup_DEL]
				@SampleTestTypeGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleTestTypeGroup WITH(ROWLOCK)
WHERE
	SampleTestTypeGroupID = @SampleTestTypeGroupID

END