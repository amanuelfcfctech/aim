CREATE PROCEDURE [dbo].[usp_UserDefinedDataSource_SEL_DataSourceTypeID]
				@DataSourceTypeID INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  DST.*
	
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
	DST.IsActive = 1 and 
	DST.DataSourceTypeID = @DataSourceTypeID

END