CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_SEL_IsActive]
		@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	    OL.*	  
	  , O.OwnerCode

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
FROM 
	dbo.AIMOwnerLocation OL WITH(NOLOCK)

	--added 3/9/17
	INNER JOIN
		dbo.AIMOwner O
			ON OL.AIMOwnerID = O.AIMOwnerID

	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON OL.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	OL.IsActive = @IsActive

--2/11/21 - order by removed for CSA speed issue
--ORDER BY
--	O.OwnerCode, OL.LocationName 

END

