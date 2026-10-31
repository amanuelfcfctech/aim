CREATE PROCEDURE [dbo].[usp_SupportingTables_UPD_CFCAIMInitial]

-- * * * * * * * THIS PROC UPDATES THE SUPPORTING TABLES ON THE INITIAL DATABASE * * * * * * * 

@UpdateDbase nvarchar(50)

AS

DECLARE @SupportingTablesID int = 0
DECLARE @UpdateCount int = 0
DECLARE @GoodRecordCount int = 0
DECLARE @SQL varchar(1000) = ''
DECLARE @TableName nvarchar(128) = ''
DECLARE @KeyName nvarchar(100) = ''
DECLARE @IdentityColumn smallint = 0
DECLARE @DevelopDbase nvarchar(50) = 'CFCAIMDevelopment'


-- exec usp_SupportingTables_UPD 'CFCAIMInitial'


BEGIN
SET NOCOUNT ON;   

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
				PRINT 'Records inserted into ' + @TableName + ' ..... '		
				SET @SQL = ''
				If @IdentityColumn = 1
					Begin
						SELECT @SQL = 'SET IDENTITY_INSERT ' + @UpdateDbase + '.dbo.' + @TableName + ' ON '										
					End	
													
				 SELECT @SQL += 'INSERT INTO ' + @UpdateDbase + '.dbo.' + @TableName + ' (' +
					STUFF ((
						SELECT ', [' + name + ']'						
						FROM sys.columns
						WHERE object_id = OBJECT_ID('' + @TableName + '') 
						FOR XML PATH('')), 1, 1, '') +
					') 	
					SELECT ' +
					STUFF ((
						SELECT ', D.' + name
						FROM sys.columns
						WHERE object_id = OBJECT_ID('' + @TableName + '') 
						FOR XML PATH('')), 1, 1, '') + 	
					' FROM ' + @DevelopDbase + '.dbo.' + @TableName + ' D
						LEFT OUTER JOIN ' + @UpdateDbase + '.dbo.' + @TableName + ' I
						ON D.' + @KeyName + ' = I.' + @KeyName + '
						WHERE I.' + @KeyName + ' IS NULL '														

				If @IdentityColumn = 1
					Begin
						SELECT @SQL += 'SET IDENTITY_INSERT ' + @UpdateDbase + '.dbo.' + @TableName + ' OFF '					
					End

				--print @sql
				EXEC (@SQL)			
			End

		------------------------------------------------------------------------
		-- DELETE RECORDS FROM INITIAL
		Else If @GoodRecordCount < @UpdateCount	
			Begin
				PRINT 'Records deleted from ' + @TableName + ' ..... '
				SET @SQL = ''
				If @IdentityColumn = 1
					Begin
						SELECT @SQL = 'SET IDENTITY_INSERT ' + @UpdateDbase + '.dbo.' + @TableName + ' ON '										
					End
				
				SELECT @SQL += 'DELETE ' + @UpdateDbase + '.dbo.' + @TableName + '
						FROM ' + @UpdateDbase + '.dbo.' + @TableName + ' I 
						LEFT OUTER JOIN ' + @DevelopDbase + '.dbo.' + @TableName + ' D
						ON I.' + @KeyName + ' = D.' + @KeyName + '
						WHERE D.' + @KeyName + ' IS NULL '				
				
				If @IdentityColumn = 1
					Begin
						SELECT @SQL += 'SET IDENTITY_INSERT ' + @UpdateDbase + '.dbo.' + @TableName + ' OFF '					
					End

				--print @sql
				EXEC (@SQL)			
			End 

		DROP TABLE ##Count
		DROP TABLE ##GoodCount

		FETCH NEXT FROM tempcur into @SupportingTablesID, @TableName, @KeyName, @IdentityColumn
	End

close tempcur
deallocate tempcur

END