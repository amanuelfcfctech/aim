CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceDefinition_DEL]
				@UserDefinedDataSourceDefinitionID		INT
AS
BEGIN
SET NOCOUNT ON;


DELETE
FROM 
	dbo.UserDefinedDataSourceTemplate with (ROWLOCK)
WHERE
	UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID


DELETE
FROM 
	dbo.UserDefinedDataSourceDefinition WITH(ROWLOCK)
WHERE
	UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID

END