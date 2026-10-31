CREATE Procedure [dbo].[usp_Sample_SEL_ColumnNames]
@TableName		VarChar(50)

AS 

BEGIN

-- EXEC dbo.usp_Sample_SEL_ColumnNames 'Sample'

SELECT 
       C.name AS ColumnName ,
       P.name AS DataType ,
       P.max_length AS Size ,	  
       CAST(P.precision AS VARCHAR) + '/' + CAST(P.scale AS VARCHAR) AS PrecisionScale
FROM   sys.Objects AS T
       JOIN sys.Columns AS C ON T.object_id = C.object_id
       JOIN sys.types AS P ON C.system_type_id = P.system_type_id
WHERE  T.Name = @TableName and P.Name <> 'sysname'
order by c.Name

END