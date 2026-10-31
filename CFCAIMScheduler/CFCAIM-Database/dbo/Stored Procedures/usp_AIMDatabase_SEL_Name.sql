CREATE PROCEDURE [dbo].[usp_AIMDatabase_SEL_Name]
			@Name			VARCHAR(50)
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  LD.AIMDatabaseID
	, LD.Name
	, LD.CFCPath
	, LD.DefaultForPropose
	, LD.UpdateStamp
	, LD.IsActive

	-- 1/11/21 - changed to remove aspnet_User table
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AIMDatabase LD WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON LD.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE 
	LD.Name = @Name
END
