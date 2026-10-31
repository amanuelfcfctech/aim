CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_SEL_OwnerID_OwnerLocationCode]
		@OwnerID					INT
		, @OwnerLocationCode		NVARCHAR(30)
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	    AOL.AIMOwnerLocationID
	  , AOL.AIMOwnerID
	  , AOL.LocationCode
	  , AOL.LocationName
	  , AOL.UpdateStamp
	  , AOL.IsActive

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName

FROM 
	dbo.AIMOwnerLocation AOL WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AOL.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID
WHERE
		AOL.AIMOwnerID = @OwnerID 
	AND AOL.LocationCode = @OwnerLocationCode
	

ORDER BY
	AOL.LocationName

END