CREATE PROCEDURE [dbo].[usp_SampleResultTemplateDetail_DEL_ID]
	  @SampleResultTemplateDetailID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.SampleResultTemplateDetail	WITH(ROWLOCK)
WHERE 
	SampleResultTemplateDetailID = @SampleResultTemplateDetailID
END