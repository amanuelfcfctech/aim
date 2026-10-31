CREATE PROCEDURE [dbo].[usp_DataSourceTypeRefFileType_SEL_DataSourceTypeID_FileTypeID] 
  @DataSourceTypeID INT			
 ,@FileTypeID INT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  X.DataSourceTypeID
	, X.FileTypeID
	, DT.Name as DataSourceTypeName
	, FT.Name as FileTypeName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM dbo.DataSourceTypeRefFileType X WITH (NOLOCK)
	INNER JOIN 
		dbo.DataSourceType DT with (nolock)
			ON X.DataSourceTypeID = DT.DataSourceTypeID
	LEFT OUTER JOIN
		dbo.FileType FT with (nolock)
			ON X.FileTypeID = FT.FileTypeID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON X.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE
	X.DataSourceTypeID = @DataSourceTypeID and
	X.FileTypeID = @FileTypeID

END