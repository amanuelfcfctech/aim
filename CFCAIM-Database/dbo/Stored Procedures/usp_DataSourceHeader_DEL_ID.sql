CREATE PROCEDURE [dbo].[usp_DataSourceHeader_DEL_ID]
	  @ID		INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @COUNT as INT

SET @COUNT = 0
SET @COUNT = (SELECT Count(*) FROM dbo.DataSourceDetail WHERE DataSourceHeaderID = @ID)
IF @Count = 0

	DELETE
		dbo.DataSourceHeader	WITH(ROWLOCK)
	WHERE 
		DataSourceHeaderID = @ID
	


END