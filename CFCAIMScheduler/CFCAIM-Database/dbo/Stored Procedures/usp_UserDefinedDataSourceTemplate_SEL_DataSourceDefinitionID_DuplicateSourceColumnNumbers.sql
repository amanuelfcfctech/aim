CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_SEL_DataSourceDefinitionID_DuplicateSourceColumnNumbers]
	@UserDefinedDataSourceDefinitionID  INT		


--exec [usp_UserDefinedDataSourceTemplate_SEL_DataSourceDefinitionID_DuplicateSourceColumnNumbers] 4181

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

Declare @SourceColumnNumber INT = 0
Declare @Num INT = 0

SET @Num = (SELECT TOP 1 DU.SourceColumnNumber
							FROM 
								dbo.UserDefinedDataSourceTemplate DU WITH(NOLOCK)	
							WHERE	
								UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID
							GROUP BY DU.SourceColumnNumber

							HAVING count(du.SourceColumnNumber) > 1 )

if @Num IS NOT NULL
	SET @SourceColumnNumber = @Num

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @SourceColumnNumber AS ReturnIntegerValue



END