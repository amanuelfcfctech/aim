CREATE PROCEDURE [dbo].[usp_DataSourceType_SEL_DataSourceTypeID]
		  @DataSourceTypeID				INT
		
AS
BEGIN
SET NOCOUNT ON;

--exec usp_DataSourceType_SEL_DataSourceTypeID 2

SELECT 
	  DST.DataSourceTypeID
    , DST.Name
    , DST.IsActive
	, DST.IsFurtherAnalyses 
	, DST.IsUserDefined
    , DST.UpdateStamp
    , DST.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.DataSourceType DST WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON DST.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE
	DST.DataSourceTypeID = @DataSourceTypeID  	
	AND DST.IsActive = 1

END