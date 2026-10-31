CREATE PROCEDURE [dbo].[usp_AIMSourceLocation_SEL_IsActive]
		@IsActive		BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	    S.AIMSourceLocationID
	  , S.AIMSourceID
	  , S.SourceLocationName
	  , S.IsActive
	  , S.UpdateStamp
	  , S.CFCUserID
	  , S.AIMSupplierLocationCode
	  , AMS.AIMSupplierCode	  
	  , AMS.SourceName

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
FROM 
	dbo.AIMSourceLocation S WITH(NOLOCK)
INNER JOIN
	dbo.AIMSource AMS 
		ON S.AIMSourceID = AMS.AIMSourceID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON S.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN
--		dbo.aspnet_Users AU WITH(NOLOCK)
--			ON AU.UserId = U.UserID

WHERE 
	S.IsActive = @IsActive

ORDER BY AMS.AIMSupplierCode, S.SourceLocationName


END

