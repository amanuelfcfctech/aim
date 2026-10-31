CREATE PROCEDURE [dbo].[usp_AnalyzerOwnerLocation_DEL_ID]
	  @ID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.AnalyzerOwnerLocation	WITH(ROWLOCK)
WHERE 
	AnalyzerOwnerLocationID = @ID
END