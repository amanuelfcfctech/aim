CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_SEL_UserDefinedSourceDefinitionID_MapColumnName]
	@UserDefinedDataSourceDefinitionID  INT		
  , @MapColumnName NVARCHAR(30) 

--exec dbo.usp_UserDefinedDataSourceTemplate_SEL_UserDefinedSourceDefinitionID_MapColumnName 2, 'DataSource'

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

SET @RecordID = (SELECT DU.UserDefinedDataSourceTemplateID	  

FROM 
	dbo.UserDefinedDataSourceTemplate DU WITH(NOLOCK)
	
WHERE
	UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID and
	MapColumnName = @MapColumnName and 
	SourceColumnNumber > 0)

SELECT 
	  isnull(@RecordID, -1) as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
END