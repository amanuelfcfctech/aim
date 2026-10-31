CREATE PROCEDURE [dbo].[usp_DataSourceTypeRefFileType_DEL_DataSourceTypeID]
				  @DataSourceTypeID		INT
				
AS	
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.DataSourceTypeRefFileType WITH(ROWLOCK)
WHERE
	DataSourceTypeID = @DataSourceTypeID

END