CREATE PROCEDURE [dbo].[usp_DataSourceTypeRefFileTypeAndDelimiterType_SEL_DataSourceTypeID]

	@DataSourceTypeID INT
AS
BEGIN
SET NOCOUNT ON;


--exec usp_DataSourceTypeRefFileTypeAndDelimiterType_SEL_DataSourceTypeID 1

SELECT
	  DX.DataSourceTypeRefFileTypeID
	, DX.DataSourceTypeID 	
	, DX.FileTypeID
	, FT.Name as FileTypeName
    , DT.Name as DelimiterTypeName
	, DT.Description as DelimiterTypeDescription
	, DX.UpdateStamp
    , DX.CFCUserID	    

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.DataSourceTypeRefFileType DX WITH(NOLOCK)
	INNER JOIN
		dbo.DataSourceType DST WITH(NOLOCK)
			ON DST.DataSourceTypeID = DX.DataSourceTypeID
	INNER JOIN	
		dbo.FileType FT WITH (NOLOCK)
			ON FT.FileTypeID = DX.FileTypeID
	LEFT JOIN
		dbo.FileTypeRefDelimiterType FX with (nolock)
			ON FT.FileTypeID = FX.FileTypeID 
	INNER JOIN
		dbo.DelimiterType DT with (nolock)
			ON DT.DelimiterTypeID= FX.DelimiterTypeID 
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON DX.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE
	DX.DataSourceTypeID = @DataSourceTypeID

END