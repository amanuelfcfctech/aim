CREATE PROCEDURE [dbo].[usp_DataSourceTypeRefFileType_SEL]
			
AS
BEGIN
SET NOCOUNT ON;

SELECT
	X.*
    , DT.Name as DataSourceTypeName
	, FT.Name as FileTypeName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
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

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID


END