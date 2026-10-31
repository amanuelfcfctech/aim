
CREATE PROCEDURE [dbo].[usp_SampleResultTemplate_SEL_AIMMaterialID]

@AIMMaterialID	INT

AS
BEGIN

--exec usp_SampleResultTemplate_SEL_AIMMaterialID 0

SET NOCOUNT ON;

SELECT 
	  SR.*
	, DST.Name as DataSourceTypeName
	, AM.AIMMaterialName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 

	dbo.SampleResultTemplate SR WITH(NOLOCK)

	LEFT OUTER JOIN
		dbo.DataSourceType DST with (nolock)
			ON SR.DataSourceTypeID = DST.DataSourceTypeID
	LEFT OUTER JOIN
		dbo.AIMMaterial AM with (nolock)
			ON SR.AIMMaterialID = AM.AIMMaterialID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SR.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)				
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID


-- 3/20/18
--WHERE AM.AIMMaterialID = @AIMMaterialID
WHERE SR.AIMMaterialID = @AIMMaterialID or SR.AIMMaterialID = 0


ORDER BY SR.AIMMaterialID DESC

END