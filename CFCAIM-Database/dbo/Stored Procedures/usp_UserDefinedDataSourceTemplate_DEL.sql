create PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_DEL]
				@UserDefinedDataSourceTemplateID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.UserDefinedDataSourceTemplate WITH(ROWLOCK)
WHERE
	UserDefinedDataSourceTemplateID = @UserDefinedDataSourceTemplateID

END