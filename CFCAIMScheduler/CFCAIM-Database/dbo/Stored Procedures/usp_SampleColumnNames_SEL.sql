CREATE PROCEDURE [dbo].[usp_SampleColumnNames_SEL]
	
	      @IncludeDataSource		BIT
		, @UseSystemSampleCodes		BIT
			
AS
BEGIN
SET NOCOUNT ON;

--exec usp_SampleColumnNames_SEL 0, 0

DECLARE @SQL				VARCHAR(1000) = ''

SET @SQL = 'SELECT Case when M.AlternateNameID is not null then A.AlternateName else M.MapColumnName end as MapColumnName, 
				MapColumnID, IsDetail, SortNumber as SortNo, CanAlter
				FROM dbo.MapColumnNames M
				LEFT OUTER JOIN dbo.AlternateName A on M.AlternateNameID = A.AlternateNameID'


IF @IncludeDataSource = 0 and @UseSystemSampleCodes = 0
	Begin
		
		SET @SQL += ' WHERE MapColumnName <> ''DataSource''' 

		--1/13/23 - added to remove blank lines from field list (for Versova) 
		SET @SQL += ' and (Case when M.AlternateNameID is not null then A.AlternateName else M.MapColumnName end <> '''') '

		SET @SQL+= ' ORDER BY SortNo'
	End

ELSE IF @IncludeDataSource = 0 and @UseSystemSampleCodes = 1		
	Begin
		SET @SQL += ' WHERE MapColumnName <> ''DataSource''' + ' and MapColumnName <> ''SampleCode''' 

		--1/13/23 - added to remove blank lines from field list (for Versova) 
		SET @SQL += ' and (Case when M.AlternateNameID is not null then A.AlternateName else M.MapColumnName end <> '''') '

		SET @SQL += ' ORDER BY SortNo'
	End


EXEC (@SQL)
--print @SQL

END