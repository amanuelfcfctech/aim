

CREATE PROCEDURE [dbo].[usp_SampleResultTemplate_SEL]
	@IsGroupTemplate BIT			--Task1061 
AS
BEGIN
SET NOCOUNT ON;
DECLARE @SampleResultTemplateID_Default INT = 0
SELECT 
	  SR.*
	, DST.Name as DataSourceTypeName
	, AM.AIMMaterialName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
	, @SampleResultTemplateID_Default AS SampleResultTemplateID_Default	 --Task1061 DB251030
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
	IsGroupTemplate = @IsGroupTemplate

END