CREATE PROCEDURE [dbo].[usp_SampleTestTypeDetail_DEL_SampleTestTypeDetailID]
	  @SampleTestTypeDetailID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.SampleTestTypeDetail	WITH(ROWLOCK)
WHERE 
	SampleTestTypeDetailID = @SampleTestTypeDetailID
END