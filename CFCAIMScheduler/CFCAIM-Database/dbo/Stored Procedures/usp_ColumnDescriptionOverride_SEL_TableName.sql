CREATE PROCEDURE [dbo].[usp_ColumnDescriptionOverride_SEL_TableName]
				@TableName	Nvarchar(50)
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  CDO.*
FROM 
	dbo.ColumnDescriptionOverride CDO WITH(NOLOCK)
WHERE 
	CDO.TableName = @TableName

END