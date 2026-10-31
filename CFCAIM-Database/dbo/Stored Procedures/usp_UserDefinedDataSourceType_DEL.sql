CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceType_DEL]
				@DataSourceTypeID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.DataSourceType WITH(ROWLOCK)
WHERE
	DataSourceTypeID = @DataSourceTypeID

END