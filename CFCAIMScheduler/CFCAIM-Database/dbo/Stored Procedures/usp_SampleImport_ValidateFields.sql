CREATE PROCEDURE [dbo].[usp_SampleImport_ValidateFields]
			@TableName NVarChar(128)
			,@ColumnName NVARCHAR(128)
			
AS
BEGIN

--  exec usp_SampleImport_ValidateFields 'Sample', 'SampleID' 

SET NOCOUNT ON;

Declare @DataType NVARCHAR(50) = ''
Declare @MaxLength INT = 0

set @DataType = (Select ST.name as DataType
				From  sys.columns SC
				Inner Join sys.types ST ON SC.user_type_id = ST.user_type_id
				Where  SC.object_id = OBJECT_ID(@TableName) and SC.name = @ColumnName)

set @MaxLength = (Select SC.max_length as MaxLength
				From sys.columns SC 
				Inner Join sys.types ST ON SC.user_type_id = ST.user_type_id
				Where  SC.object_id = OBJECT_ID(@TableName) and SC.name = @ColumnName)


SELECT
	@DataType as DataType
	, @MaxLength as [MaxLength]
	


END