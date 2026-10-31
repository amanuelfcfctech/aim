CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_UPD_MapColumnForDataSourceType]			
			 @UserDefinedDataSourceDefinitionID	INT
			, @UserDefinedDataSourceTemplateID	INT
			, @MapColumnID						INT	
			, @MapColumnName					NVARCHAR(30)


-- when user opts to map the analyzer datasource, actually maps the field, but then turns around and chooses NOT to map the datasource,
-- this stp will update the template record; otherwise the user would have to remember to go to the mapping and set the column to 'do not use'; 
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

UPDATE
	dbo.UserDefinedDataSourceTemplate WITH(ROWLOCK)
		SET 
			MapColumnID = ISNULL(@MapColumnID, MapColumnID)													
			, MapColumnName = ISNULL(@MapColumnName, MapColumnName)				
					
		WHERE 
			UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID
			AND UserDefinedDataSourceTemplateID = @UserDefinedDataSourceTemplateID
			AND MapColumnName = 'DataSource'

	SET @RecordID = @@IDENTITY 

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END