CREATE PROCEDURE [dbo].[usp_AnalyzerOwner_DEL_ID]
	  @ID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.AnalyzerOwner	WITH(ROWLOCK)
WHERE 
	AnalyzerOwnerID = @ID
END