CREATE PROCEDURE [dbo].[usp_DataSourceDetail_DEL_ID]
	  @ID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.DataSourceDetail	WITH(ROWLOCK)
WHERE 
	DataSourceDetailID = @ID
END