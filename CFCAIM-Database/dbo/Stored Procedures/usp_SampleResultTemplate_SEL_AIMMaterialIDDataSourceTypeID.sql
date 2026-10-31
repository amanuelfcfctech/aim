CREATE PROCEDURE [dbo].[usp_SampleResultTemplate_SEL_AIMMaterialIDDataSourceTypeID]
				 @AIMMaterialID	INT
				,@DataSourceTypeID	INT
AS
BEGIN

SET NOCOUNT ON;

SELECT 
	  SR.*
	, DST.Name as DataSourceTypeName
	, AM.AIMMaterialName
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

WHERE 
		SR.AIMMaterialID = @AIMMaterialID
	AND SR.DataSourceTypeID = @DataSourceTypeID
	AND SR.IsGroupTemplate = 1


END