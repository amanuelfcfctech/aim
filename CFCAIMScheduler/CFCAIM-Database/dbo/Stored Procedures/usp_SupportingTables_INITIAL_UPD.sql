CREATE PROCEDURE [dbo].[usp_SupportingTables_INITIAL_UPD]
	@NewTableName		NVARCHAR(50) = NULL

AS

DECLARE @id int = 0
DECLARE @InitialCount int = 0
DECLARE @RecordCount int = 0
DECLARE @SQL varchar(1000) = ''
DECLARE @Name nvarchar(128) = ''
DECLARE @KeyName nvarchar(100) = ''
DECLARE @IDCol smallint = 0

DECLARE @InitialDbase nvarchar(50) = 'CFCAIMInitial'
DECLARE @DevelopDbase nvarchar(50) = 'CFCAIMDevelopment'


-- exec usp_SupportingTables_INITIAL_UPD						--> Only updates CFCAIMInitial
-- exec usp_SupportingTables_INITIAL_UPD 'SupportingTables'		--> Can be used to update any database

BEGIN
SET NOCOUNT ON;   

---------------------------------------------------------------------------
-- * * * When @NewTableName is not null, the records in the initial table are truncated, 
-- * * * and all the records are copied from development.

IF @NewTableName IS NOT NULL
	Begin

		PRINT 'Backing up ' + @InitialDbase + ' ..... '

		SET @SQL = 'BACKUP DATABASE ' + @InitialDbase + ' TO DISK = ' 
		SET @SQL += '''E:\Program Files\Microsoft SQL Server\MSSQL11.SQL2012\MSSQL\Backup\' + @InitialDbase
				     + convert(varchar(8),getdate(), 112) + '.Bak '''
		EXEC (@SQL)		

		-- get IDCol setting
		SET @IDCol = (SELECT ISNULL((Select TOP 1 is_identity From sys.columns c where c.object_id = ss.object_id and is_nullable = 0), 0)
		FROM sys.objects ss
		WHERE type = 'u' and schema_id = 1 and name = @NewTableName) 				

		SET @SQL = 'TRUNCATE TABLE ' + @InitialDbase + '.dbo.' + @NewTableName + ' '	

		If @IDCol = 1
			Begin
				SELECT @SQL += 'SET IDENTITY_INSERT ' + @InitialDbase + '.dbo.' + @NewTableName + ' ON '										
			End		
						
		SELECT @SQL += 'INSERT INTO ' + @InitialDbase + '.dbo.' + @NewTableName + ' (' +
					STUFF ((
						SELECT ', [' + name + ']'						
						FROM sys.columns
						WHERE object_id = OBJECT_ID('' + @NewTableName + '') 
						FOR XML PATH('')), 1, 1, '') +
					') 	
					SELECT ' +
					STUFF ((
						SELECT ', D.' + name
						FROM sys.columns
						WHERE object_id = OBJECT_ID('' + @NewTableName + '') 
						FOR XML PATH('')), 1, 1, '') + 	
					' FROM ' + @DevelopDbase + '.dbo.' + @NewTableName + ' D '														

		If @IDCol = 1
			Begin
				SELECT @SQL += 'SET IDENTITY_INSERT ' + @InitialDbase + '.dbo.' + @NewTableName + ' OFF '					
			End

		--print @sql
		EXEC (@SQL)	

		PRINT 'Records inserted into ' + @NewTableName + ' ..... '

		GOTO EXITPROC				
	End 
	
---------------------------------------------------------------------------
If @InitialDbase <> 'CFCAIMInitial'
	GOTO EXITPROC

---------------------------------------------------------------------------
-- GET TABLE-NAMES FROM SYS.OBJECTS
SELECT ss.name, ss.object_id as id
, ISNULL((Select TOP 1 name From sys.columns c where c.object_id = ss.object_id and is_nullable = 0), '') as keyname
, ISNULL((Select TOP 1 is_identity From sys.columns c where c.object_id = ss.object_id and is_nullable = 0), 0) as idcol
, 0 as InitialCount, 0 as RecCount
INTO ##TempInitial
FROM CFCAIMInitial.sys.objects ss
WHERE type = 'u' and schema_id = 1
ORDER BY name



---------------------------------------------------------------------------
-- LOOP THROUGH TEMP TABLE AND INSERT RECORD COUNTS
DECLARE tempcur scroll cursor for SELECT name, id FROM ##TempInitial 
OPEN tempcur
FETCH NEXT FROM tempcur into @Name, @id
WHILE @@fetch_status = 0
	Begin
				
		-------------------------------------------------------------------
		-- GET RECORD COUNT FROM INITIAL TABLE
		SET @SQL = 'Select ''' + @Name + ''' as name, count(*) as RecCount 
						INTO ##CountInitial from ' + @InitialDbase + '.dbo.' + @Name 						
		EXEC (@SQL)
		SET @InitialCount = (SELECT ISNULL(RecCount,0) FROM ##CountInitial)			
				
		-------------------------------------------------------------------
		-- GET RECORD COUNT FROM DEVELOPMENT TABLE
		SET @SQL = 'Select ''' + @Name + ''' as name, count(*) as RecCount 
						INTO ##Count from ' + @DevelopDbase + '.dbo.' + @Name 									
		EXEC (@SQL)
		SET @RecordCount = (SELECT RecCount FROM ##Count)

		-------------------------------------------------------------------
		-- UPDATE RECORD COUNTS ON TEMP TABLE
		UPDATE t
		SET 
			t.InitialCount = CI.RecCount,
			t.RecCount = C.RecCount
		FROM ##TempInitial t 
		INNER JOIN ##CountInitial CI on (t.name = CI.name)
		INNER JOIN ##Count C on (t.name = c.name)
		WHERE t.name = c.name 

		if OBJECT_ID('tempdb..##CountInitial') is not null
			DROP TABLE ##CountInitial

		if OBJECT_ID('tempdb..##Count') is not null
			DROP TABLE ##Count

		-------------------------------------------------------------------		
		FETCH NEXT FROM tempcur INTO @Name, @id
	End
CLOSE tempcur
DEALLOCATE tempcur


---------------------------------------------------------------------------
-- REMOVE RECORDS WITH 0
DELETE FROM ##TempInitial WHERE InitialCount = 0


---------------------------------------------------------------------------
-- COMPARE RECORD COUNTS AND ADD ANY MISSING RECORDS
DECLARE compareCur scroll cursor for 
SELECT name, id, keyname, idcol, InitialCount, RecCount 
FROM ##TempInitial 
ORDER BY name
OPEN compareCur
FETCH NEXT FROM compareCur INTO @Name, @id, @KeyName, @IDCol, @InitialCount, @RecordCount
WHILE @@fetch_status = 0
	Begin		
		
		--INSERT RECORDS FROM DEVELOPMENT
		If @RecordCount > @InitialCount		
			Begin				
				PRINT 'Records inserted into ' + @Name + ' ..... '
				SET @SQL = ''
				If @IDCol = 1
					Begin
						SELECT @SQL = 'SET IDENTITY_INSERT ' + @InitialDbase + '.dbo.' + @Name + ' ON '										
					End
				
				SELECT @SQL += 'INSERT INTO ' + @InitialDbase + '.dbo.' + @Name + ' (' +
					STUFF ((
						SELECT ', [' + name + ']'						
						FROM CFCAIMInitial.sys.columns
						WHERE object_id = OBJECT_ID('' + @Name + '') 
						FOR XML PATH('')), 1, 1, '') +
					') 	
					SELECT ' +
					STUFF ((
						SELECT ', D.' + name
						FROM CFCAIMInitial.sys.columns
						WHERE object_id = OBJECT_ID('' + @Name + '') 
						FOR XML PATH('')), 1, 1, '') + 	
					' FROM ' + @DevelopDbase + '.dbo.' + @Name + ' D
						LEFT OUTER JOIN dbo.' + @Name + ' I
						ON D.' + @KeyName + ' = I.' + @KeyName + '
						WHERE I.' + @KeyName + ' IS NULL '				
				
				If @IDCol = 1
					Begin
						SELECT @SQL += 'SET IDENTITY_INSERT ' + @InitialDbase + '.dbo.' + @Name + ' OFF '					
					End

				--print @sql
				EXEC (@SQL)			
			End

		------------------------------------------------------------------------
		-- DELETE RECORDS FROM INITIAL
		Else If @RecordCount < @InitialCount	
			Begin
				PRINT 'Records deleted from ' + @Name + ' ..... '
				SET @SQL = ''
				If @IDCol = 1
					Begin
						SELECT @SQL = 'SET IDENTITY_INSERT ' + @InitialDbase + '.dbo.' + @Name + ' ON '										
					End
				
				SELECT @SQL += 'DELETE ' + @InitialDbase + '.dbo.' + @Name + '
						FROM ' + @InitialDbase + '.dbo.' + @Name + ' I 
						LEFT OUTER JOIN ' + @DevelopDbase + '.dbo.' + @Name + ' D
						ON I.' + @KeyName + ' = D.' + @KeyName + '
						WHERE D.' + @KeyName + ' IS NULL '				
				
				If @IDCol = 1
					Begin
						SELECT @SQL += 'SET IDENTITY_INSERT ' + @InitialDbase + '.dbo.' + @Name + ' OFF '					
					End

				--print @sql
				EXEC (@SQL)			
			End 

		FETCH NEXT FROM compareCur INTO @Name, @id, @KeyName, @IDCol, @InitialCount, @RecordCount
	End
CLOSE compareCur
DEALLOCATE compareCur


if OBJECT_ID('tempdb..##TempInitial') is not null
	DROP TABLE ##TempInitial


EXITPROC:

END