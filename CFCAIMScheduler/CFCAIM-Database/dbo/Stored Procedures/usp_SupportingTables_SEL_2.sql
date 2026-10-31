CREATE PROCEDURE [dbo].[usp_SupportingTables_SEL_2]

	@UpdateDbase nvarchar(50),
	@DevelopDbase as nvarchar(50)

AS

DECLARE @SupportingTablesID int = 0
DECLARE @UpdateCount int = 0
DECLARE @GoodRecordCount int = 0
DECLARE @SQL varchar(1000) = ''
DECLARE @TableName nvarchar(128) = ''
DECLARE @KeyName nvarchar(100) = ''
DECLARE @IdentityColumn smallint = 0
DECLARE @system_Type_id int = 0

declare @colName varchar(50) = ''
declare @colValue varchar(50) = ''
declare @colType varchar(50) = ''
declare @colID varchar(50) = ''
declare @tableID varchar(50) = ''
declare @savetableID varchar(50) = ''
declare @pos int = 0
declare @colList NVARCHAR(MAX) = N''
declare @val varchar(1000) = ''
declare @keyVal varchar (50) = ''


-- exec usp_SupportingTables_SEL_2 'CFCAIMDemo', 'CFCAIMDevelopment'

BEGIN
SET NOCOUNT ON;   


-- create temp ##Table
SELECT Space(50) as TableID,Space(50) as ColID,Space(50) as ColName, Space(50) as ColValue,Space(50) as ColType INTO ##Table	
						

---------------------------------------------------------------------------
-- LOOP THROUGH SUPPORTING TABLE AND INSERT RECORD COUNTS
DECLARE tempcur scroll cursor for 
SELECT SupportingTablesID, TableName, KeyName, IdentityColumn 
FROM CFCAIMDevelopment.dbo.SupportingTables 
OPEN tempcur
FETCH NEXT FROM tempcur into @SupportingTablesID, @TableName, @KeyName, @IdentityColumn
WHILE @@fetch_status = 0
	Begin				

		-------------------------------------------------------------------
		-- GET RECORD COUNT FROM TABLE ON DBASE TO BE UPDATED
		SET @SQL = 'Select ''' + @TableName + ''' as name, count(*) as RecCount INTO ##Count 
					From ' + @UpdateDbase + '.dbo.' + @TableName 	
		EXEC (@SQL)
		SET @UpdateCount = (SELECT RecCount FROM ##Count)	
				
		-------------------------------------------------------------------
		-- GET RECORD COUNT FROM DEVELOPMENT TABLE
		SET @SQL = 'Select ''' + @TableName + ''' as name, count(*) as RecCount INTO ##GoodCount 
					From ' + @DevelopDbase + '.dbo.' + @TableName 											
		EXEC(@SQL)
		SET @GoodRecordCount = (SELECT RecCount FROM ##GoodCount)
		
		-------------------------------------------------------------------
		--INSERT RECORDS FROM DEVELOPMENT
		If @GoodRecordCount > @UpdateCount		
			Begin						
				
				-- CREATE VALUES TEMP TABLE 
				Set @colList = ''
				SELECT @colList += N',' + 'D.' + name 
				FROM sys.dm_exec_describe_first_result_set(N'SELECT * FROM ' + @DevelopDbase + '.dbo.' + @tableName, NULL, 1);
				SET @colList = STUFF(@colList, 1, 1, N'');				

				set @sql = 'Select ' + @colList + ' Into ##Values 
							FROM ' + @DevelopDbase + '.dbo.' + @TableName + ' D
							LEFT OUTER JOIN ' + @UpdateDbase + '.dbo.' + @TableName + ' I
							ON D.' + @KeyName + ' = I.' + @KeyName + '
							WHERE I.' + @KeyName + ' IS NULL '																				
				exec (@sql)
				
				-- Reset @colList without the table alias
				Set @colList = ''
				SELECT @colList += N',' + name 
				FROM sys.dm_exec_describe_first_result_set(N'SELECT * FROM ' + @DevelopDbase + '.dbo.' + @tableName, NULL, 1);
				SET @colList = STUFF(@colList, 1, 1, N'')

				--------------------------------------------------------------------------------
				declare infocur scroll cursor for 
				SELECT ORDINAL_POSITION, COLUMN_NAME, data_type 
				FROM INFORMATION_SCHEMA.COLUMNS
				WHERE TABLE_NAME = @tableName
				ORDER BY ORDINAL_POSITION	
				open infocur
				fetch next from infocur into @pos, @colName, @colType
				while @@FETCH_STATUS = 0
				Begin	
	
					set @colName = (Select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = @tableName and  ORDINAL_POSITION = @pos)	
					set @tableID =(Select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS	WHERE TABLE_NAME = @tableName and  ORDINAL_POSITION = 1)		
				
					set @sql = 'Select '				  + @tableID + ' as TableID, ' 
														  + cast(@pos as varchar) + ' as ColID,''' 
														  + @colName + ''' as ColName, ' 
														  + @colName + ' as ColValue, ''' 
														  + @colType + ''' as ColType '
					set @sql += ' from ##values '			
					Insert into ##Table (TableID, ColID, ColName, ColValue, ColType) 
					exec (@sql)			
					fetch next from infocur into @pos, @colName, @colType	
				End
				close infocur
				deallocate infocur	
				
				--------------------------------------------------------------------------------				
				set @keyVal = (Select TOP 1 IsNull(ColValue, '') from ##Table where ColName = @tableID group by ColValue)
				if @keyVal <> ''
					Begin
						if @IdentityColumn = 1
						Begin
							Set @SQL = 'SET IDENTITY_INSERT ' + @UpdateDbase + '.dbo.' + @tablename + ' ON '
							print @sql
						End			
					End

				--------------------------------------------------------------------------------
				-- LOOP THRU and create select statements
				declare tempValues scroll cursor for 
				select ColValue from ##Table where ColName = @tableID group by ColValue
				open tempValues
				fetch next from tempValues into @keyVal
				while @@fetch_status = 0
					begin										
																	
						Set @SQL = 'INSERT INTO ' + @UpdateDbase + '.dbo.' + @tableName + ' (' + @colList + ') Values ('
						set @val = ''
						declare tempTable scroll cursor for 
						SELECT TableID, ColID, ColName, ColValue, ColType 
						FROM ##TABLE where TableID = @keyVal
						order by TableID, ColID
						open tempTable
						fetch next from tempTable into @tableID, @colID, @colName, @colValue, @colType
						while @@FETCH_STATUS = 0
							Begin	
								if @colType = 'nvarchar' or @colType = 'varchar' or @colType = 'nchar'
									set @val = @val + '''' + @colValue + ''', '
								else if @colType = 'datetime'
									set @val = @val + '''' + convert(varchar,convert(datetime, @colValue), 120) + ''', '
								else
									set @val = @val + @colValue + ', '
								fetch next from tempTable into @tableID, @colID, @colName, @colValue, @colType
							End
						close tempTable
						deallocate tempTable		
						fetch next from tempValues into @keyVal

						set @val = Left(@Val, Len(@Val) - 1)
						set @Sql = @SQL + @val + ')' 
		
						print @sql																			
		
					end
				
				--------------------------------------------------------------------------------
				if @keyVal <> ''
					Begin
						if @IdentityColumn = 1
						Begin
							Set @SQL = 'SET IDENTITY_INSERT ' + @UpdateDbase + '.dbo.' + @tablename + ' OFF '
							print @sql
						End			
					End

				close tempValues
				deallocate tempValues		
				drop table ##Values
				delete from ##table
							
									
			End

		--------------------------------------------------------------------------------
		-- DELETE RECORDS FROM INITIAL
		Else If @GoodRecordCount < @UpdateCount	
			Begin

				if @TableName IN ('DataSourceType', 'DataSourceTypeRefFileType')
					Set @SQL = 'DELETE ' + @UpdateDbase + '.dbo.' + @TableName + '
						FROM ' + @UpdateDbase + '.dbo.' + @TableName + ' I 
						LEFT OUTER JOIN ' + @DevelopDbase + '.dbo.' + @TableName + ' D
						ON I.' + @KeyName + ' = D.' + @KeyName + '
						WHERE D.' + @KeyName + ' IS NULL 
						AND D.DataSourceTypeID < 10000 '


				Else
					Set @SQL = 'DELETE ' + @UpdateDbase + '.dbo.' + @TableName + '
						FROM ' + @UpdateDbase + '.dbo.' + @TableName + ' I 
						LEFT OUTER JOIN ' + @DevelopDbase + '.dbo.' + @TableName + ' D
						ON I.' + @KeyName + ' = D.' + @KeyName + '
						WHERE D.' + @KeyName + ' IS NULL '				

				PRINT @SQL
								
				
			End 

		DROP TABLE ##Count
		DROP TABLE ##GoodCount

		FETCH NEXT FROM tempcur into @SupportingTablesID, @TableName, @KeyName, @IdentityColumn
	End

close tempcur
deallocate tempcur
drop table ##table

/*
close infocur
deallocate infocur	
close tempTable
deallocate tempTable
close tempValues
deallocate tempValues		
drop table ##Values
delete from ##table
DROP TABLE ##Count
DROP TABLE ##GoodCount
close tempcur
deallocate tempcur
drop table ##table

*/



END