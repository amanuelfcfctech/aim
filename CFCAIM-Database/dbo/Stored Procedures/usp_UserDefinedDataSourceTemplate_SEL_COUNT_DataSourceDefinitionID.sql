CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_SEL_COUNT_DataSourceDefinitionID]
	  @UserDefinedDataSourceDefinitionID  INT		
	, @MapColumnName	nvarchar(30)


AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''


SET @RecordID = (SELECT Count(*) as Count 
	  
FROM 
	dbo.UserDefinedDataSourceTemplate DU WITH(NOLOCK)

WHERE
	UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID and MapColumnName = @MapColumnName )

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END