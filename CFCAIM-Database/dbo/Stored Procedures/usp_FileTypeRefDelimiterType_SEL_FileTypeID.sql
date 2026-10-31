CREATE PROCEDURE [dbo].[usp_FileTypeRefDelimiterType_SEL_FileTypeID]
		@FileTypeID			INT
AS
BEGIN
SET NOCOUNT ON;

-- exec [usp_FileTypeRefDelimiterType_SEL_FileTypeID] 3


SELECT
	  X.FileTypeRefDelimiterTypeID
	, X.FileTypeID 	
	, X.DelimiterTypeID
    , X.UpdateStamp
    , X.CFCUserID
	, FT.Name 
    , DT.Description 

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.FileTypeRefDelimiterType X WITH(NOLOCK)
	INNER JOIN
		dbo.FileType FT WITH(NOLOCK)
			ON FT.FileTypeID = X.FileTypeID
	INNER JOIN	
		dbo.DelimiterType DT WITH (NOLOCK)
			ON DT.DelimiterTypeID = X.DelimiterTypeID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON X.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE
	X.FileTypeID = @FileTypeID

END