CREATE PROCEDURE [dbo].[usp_SampleResultTemplate_SEL_SampleResultTemplateName]
	@SampleResultTemplateName	VARCHAR(50)

AS
BEGIN

--exec usp_SampleResultTemplate_SEL_SampleResultTemplateName 'TEST'

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

WHERE

	SR.SampleResultTemplateName = @SampleResultTemplateName
END