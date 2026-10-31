CREATE PROCEDURE [dbo].[usp_SEL_AllTableColumnNames]

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  CONVERT(NVARCHAR,st.name) AS TableName
	, st.object_id AS TableID
	, CONVERT(NVARCHAR,sc.name) AS ColumnName
	, sc.object_id AS ColumnID

FROM 
	sys.tables st
	INNER JOIN 
		sys.columns sc 
			ON st.object_id = sc.object_id

END