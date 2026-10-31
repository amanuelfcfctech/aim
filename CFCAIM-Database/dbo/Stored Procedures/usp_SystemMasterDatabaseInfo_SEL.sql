
CREATE PROCEDURE [dbo].[usp_SystemMasterDatabaseInfo_SEL]
	
	@DatabaseName			VarChar(100)
	, @FileType				VarChar(20)

--exec usp_SystemMasterDatabaseInfo_SEL 'CFCAIMDevelopment', 'Log'

			
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	d.name as DatabaseName, 
	f.name as LogicalName,
	f.physical_name as PhysicalName,
	f.type_desc as TypeOfFile

FROM 
	sys.master_files f

INNER JOIN 
	sys.databases d 
		ON d.database_id = f.database_id

WHERE 
	d.name = @DatabaseName and
	f.type_desc = @FileType


END