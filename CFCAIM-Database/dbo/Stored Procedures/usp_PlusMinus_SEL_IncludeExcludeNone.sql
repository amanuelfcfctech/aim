CREATE PROCEDURE [dbo].[usp_PlusMinus_SEL_IncludeExcludeNone]
	
	@IncludeNone			BIT

AS
BEGIN
SET NOCOUNT ON;

-- exec dbo.usp_PlusMinus_SEL_IncludeExcludeNone 1

DECLARE @SQL				VARCHAR(MAX) = ''

SET @SQL = '
SELECT 
	PM.PlusMinusID
	, PM.PlusMinusName
	, PM.PlusMinusDescription
	
FROM 
	dbo.PlusMinus PM '

IF @IncludeNone = 0 
	SET @SQL+= ' WHERE PM.PlusMinusID <> 3'

EXEC(@SQL)

END