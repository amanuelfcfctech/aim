CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_DEL_DefinitionID_SourceColumnNumber]
				  @UserDefinedDataSourceDefinitionID		INT
				, @SourceColumnNumber					    INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.UserDefinedDataSourceTemplate WITH(ROWLOCK)
WHERE
	UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID
	and SourceColumnNumber = @SourceColumnNumber
	
END