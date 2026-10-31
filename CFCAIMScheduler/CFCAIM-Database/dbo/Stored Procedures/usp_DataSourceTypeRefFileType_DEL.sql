CREATE PROCEDURE [dbo].[usp_DataSourceTypeRefFileType_DEL]
				  @DataSourceTypeRefFileTypeID		INT
				
AS	
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.DataSourceTypeRefFileType WITH(ROWLOCK)
WHERE
	DataSourceTypeRefFileTypeID = @DataSourceTypeRefFileTypeID

END