CREATE PROCEDURE [dbo].[usp_SupportingTables_SEL]

@UpdateDbase nvarchar(50),
@DevelopDbase nvarchar(50)

AS

DECLARE @SupportingTablesID int = 0
DECLARE @UpdateCount int = 0
DECLARE @GoodRecordCount int = 0
DECLARE @SQL varchar(1000) = ''
DECLARE @TableName nvarchar(128) = ''
DECLARE @KeyName nvarchar(100) = ''
DECLARE @IdentityColumn smallint = 0
DECLARE @system_Type_id int = 0

-- exec usp_SupportingTables_SEL 'CFCAIMKeystone', 'CFCAIMDevelopment'


BEGIN
SET NOCOUNT ON;   

---------------------------------------------------------------------------
-- LOOP THROUGH SUPPORTING TABLE AND INSERT RECORD COUNTS
DECLARE tempcur scroll cursor for 
SELECT SupportingTablesID, TableName, KeyName, IdentityColumn 
FROM CFCAIMDevelopment.dbo.SupportingTables 
WHERE TableName IN ('Report')
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
				
				SELECT ' [' + name + '] ' as name, system_type_id
				into ##Columns
				FROM sys.columns
				WHERE object_id = OBJECT_ID('' + @TableName + '') 											
						
				SELECT ', [' + name + ']'						
						FROM sys.columns
						WHERE object_id = OBJECT_ID(' + Report + ') 
						FOR XML PATH('')		
											
				--select D.* 
				--into ##test
				--from report D
				--LEFT OUTER JOIN cfcaimkeystone.dbo.report I
				--ON D.ReportID = I.reportID
				--WHERE I.reportid IS NULL 		
				
										
				PRINT 'Records inserted into ' + @TableName + ' ..... '						
				SELECT 'SET IDENTITY_INSERT ' + @UpdateDbase + '.dbo.' + @TableName + ' ON '										
				union all			
													
				SELECT 'INSERT INTO ' + @UpdateDbase + '.dbo.' + @TableName + ' (' +
					STUFF ((
						SELECT ', [' + name + ']'						
						FROM sys.columns
						WHERE object_id = OBJECT_ID('' + @TableName + '') 
						FOR XML PATH('')), 1, 1, '') +
					') 	'				

				--declare colcursor scroll cursor for Select name, system_type_ID from ##Columns
				--open colcursor
				--fetch next from colcursor into @name, @system_Type_id
				--while @@fetch_status = 0
				
				--	begin
				--		if @system_Type_id = 56 --integer
				--			set @sql = '1'
				--		else if @system_Type_id = 231 -- nvarchar
				--			set @sql = '2'
				--		else
				--			set @sql = '3'


				--		fetch next from colcursor into @name, @system_Type_id
				--	end
				--close colcursor
				--deallocate colcursor

				SET @SQL = 'Select ''' + @TableName + ''' as name, count(*) as RecCount INTO ##GoodCount 
					From ' + @DevelopDbase + '.dbo.' + @TableName 
				
				SELECT ' 15, ' + '''Analysis Pivot Summary'', 1, ''' + getdate() +''', 1, 1 '
						
					--SELECT ' +
					--STUFF ((
					--	SELECT ', D.' + name
					--	FROM sys.columns
					--	WHERE object_id = OBJECT_ID('' + @TableName + '') 
					--	FOR XML PATH('')), 1, 1, '') + 	
					--' FROM ' + @DevelopDbase + '.dbo.' + @TableName + ' D
					--	LEFT OUTER JOIN ' + @UpdateDbase + '.dbo.' + @TableName + ' I
					--	ON D.' + @KeyName + ' = I.' + @KeyName + '
					--	WHERE I.' + @KeyName + ' IS NULL '														
				union all
				SELECT 'SET IDENTITY_INSERT ' + @UpdateDbase + '.dbo.' + @TableName + ' OFF '					
									
			End

		------------------------------------------------------------------------
		-- DELETE RECORDS FROM INITIAL
		Else If @GoodRecordCount < @UpdateCount	
			Begin
				PRINT 'Records deleted from ' + @TableName + ' ..... '
				SELECT 'SET IDENTITY_INSERT ' + @UpdateDbase + '.dbo.' + @TableName + ' ON '										
				SELECT 'DELETE ' + @UpdateDbase + '.dbo.' + @TableName + '
						FROM ' + @UpdateDbase + '.dbo.' + @TableName + ' I 
						LEFT OUTER JOIN ' + @DevelopDbase + '.dbo.' + @TableName + ' D
						ON I.' + @KeyName + ' = D.' + @KeyName + '
						WHERE D.' + @KeyName + ' IS NULL '				
				union all
				SELECT 'SET IDENTITY_INSERT ' + @UpdateDbase + '.dbo.' + @TableName + ' OFF '					
				
			End 

		DROP TABLE ##Count
		DROP TABLE ##GoodCount

		FETCH NEXT FROM tempcur into @SupportingTablesID, @TableName, @KeyName, @IdentityColumn
	End

close tempcur
deallocate tempcur

END