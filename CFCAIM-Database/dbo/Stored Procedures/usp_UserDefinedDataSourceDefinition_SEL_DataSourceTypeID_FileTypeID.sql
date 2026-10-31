
CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceDefinition_SEL_DataSourceTypeID_FileTypeID] 
  @DataSourceTypeID INT			
 ,@FileTypeID INT

-- exec usp_UserDefinedDataSourceDefinition_SEL_DataSourceTypeID_FileTypeID 12012, 6

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	DS.DataSourceTypeID
  , DS.ImportFileTypeID
	
FROM dbo.UserDefinedDataSourceDefinition DS WITH (NOLOCK)
	
WHERE
	DS.DataSourceTypeID = @DataSourceTypeID and
	DS.ImportFileTypeID = @FileTypeID

END