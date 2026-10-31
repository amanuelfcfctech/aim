

CREATE PROCEDURE [dbo].[usp_SampleResultTemplate_SEL_SampleResultTemplateID]

@SampleResultTemplateID INT

AS
BEGIN

--exec [usp_SampleResultTemplate_SEL_SampleResultTemplateID] 14
DECLARE @SampleResultTemplateID_Default INT = 0
SET NOCOUNT ON;
SELECT 
	--  SR.*,
	 DST.Name as DataSourceTypeName
	, AM.AIMMaterialName	
	, SRD.SortOrder
	, SRD.AnalyzerNutrientID
	, AN.AnalyzerNutrientCode
	, AN.AnalyzerNutrientName
	, SR.SampleResultTemplateID
	, DST.DataSourceTypeID
	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, SR.CurrentIteration		--Task1061 DB251027
	, SR.IsGroupTemplate		--Task1061 DB251029
	, U.UserName				--Task1061 DB251029
	, @SampleResultTemplateID_Default AS SampleResultTemplateID_Default		--Task1061 DB251030

FROM 
	dbo.SampleResultTemplate SR WITH(NOLOCK)

LEFT OUTER JOIN
	dbo.SampleResultTemplateDetail SRD with (nolock) on SR.SampleResultTemplateID = SRD.SampleResultTemplateID

		LEFT OUTER JOIN
			dbo.DataSourceType DST with (nolock)
				ON SR.DataSourceTypeID = DST.DataSourceTypeID
		LEFT OUTER JOIN
			dbo.AIMMaterial AM with (nolock)
				ON SR.AIMMaterialID = AM.AIMMaterialID
		LEFT OUTER JOIN
			dbo.AnalyzerNutrient AN with (nolock)
				ON SRD.AnalyzerNutrientID = AN.AnalyzerNutrientID
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON SR.CFCUserID = U.CFCUserID

			-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
			--LEFT OUTER JOIN
			--	dbo.aspnet_Users AU WITH(NOLOCK)
			--		ON AU.UserId = U.UserID


WHERE SR.SampleResultTemplateID = @SampleResultTemplateID

END