CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_DEL_DefinitionID]
				@UserDefinedDataSourceDefinitionID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.UserDefinedDataSourceTemplate WITH(ROWLOCK)
WHERE
	UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID

END