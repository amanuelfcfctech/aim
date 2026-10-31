CREATE PROCEDURE [dbo].[usp_DataSourceTypeRefFileType_SEL_DataSourceTypeID_IsSystem]
		@DataSourceTypeID			INT
		, @IsSystem			BIT
AS
BEGIN
SET NOCOUNT ON;

-- EXEC usp_DataSourceTypeRefFileType_SEL_DataSourceTypeID_IsSystem 6, 0


SELECT 
	
	X.*
    , DT.Name as DataSourceTypeName
	, FT.Name as FileTypeName
	
	
FROM 
	dbo.DataSourceTypeRefFileType X WITH(NOLOCK)
	INNER JOIN
		dbo.DataSourceType DT WITH(NOLOCK)
			ON DT.DataSourceTypeID = X.DataSourceTypeID
	INNER JOIN	
		dbo.FileType FT WITH (NOLOCK)
			ON FT.FileTypeID = X.FileTypeID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON X.CFCUserID = U.CFCUserID
	
WHERE
	X.DataSourceTypeID = @DataSourceTypeID
	AND X.IsSystem = @IsSystem

--GROUP BY 
--	X.DataSourceTypeID 
--	, X.FileTypeID
--	, X.IsSystem
--	, DT.Name
--	, FT.Name

END