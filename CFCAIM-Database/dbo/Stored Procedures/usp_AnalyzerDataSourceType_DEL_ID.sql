create PROCEDURE [dbo].[usp_AnalyzerDataSourceType_DEL_ID]
	  @ID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.AnalyzerDataSourceType	WITH(ROWLOCK)
WHERE 
	AnalyzerDataSourceTypeID = @ID
END