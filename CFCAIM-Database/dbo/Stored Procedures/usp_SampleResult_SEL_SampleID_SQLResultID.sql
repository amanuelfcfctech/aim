
CREATE PROCEDURE [dbo].[usp_SampleResult_SEL_SampleID_SQLResultID]
	@SampleID			INT
	, @SQLResultID		NVARCHAR(300)

AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	  SR.SampleResultID
	, SR.SampleID
	, SR.AnalyzerNutrientID
	, AN.AnalyzerNutrientName
	, SR.ResultValue
	, SR.UpdateStamp
	, SR.IsActive
	, SR.AnalysisDate
	, SR.CFCUserID
	, SR.IsCalculate
	, AN.DataSourceTypeID
	, SR.Unit
	, AN.AnalyzerNutrientCode 		
	, AN.UnitType	
	, AMN.AIMNutrientName
	, DST.Name AS DataSourceTypeName
	, SR.SQLResultID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.SampleResult SR
	INNER JOIN 
		dbo.AnalyzerNutrient AN WITH(NOLOCK)
			ON  AN.AnalyzerNutrientID = SR.AnalyzerNutrientID 
			AND AN.IsActive = 1
	LEFT OUTER JOIN
		dbo.DataSourceType DST with (nolock)
			ON AN.DataSourceTypeID = DST.DataSourceTypeID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SR.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

	LEFT OUTER JOIN dbo.AIMNutrientRefAnalyzerNutrient XREF WITH(NOLOCK) 
			ON XREF.AnalyzerNutrientID = AN.AnalyzerNutrientID
	LEFT OUTER JOIN dbo.AIMNutrient AMN 
			ON XREF.AIMNutrientID = AMN.AIMNutrientID

WHERE
	SR.SampleID = @SampleID AND 
	SR.SQLResultID = @SQLResultID

END