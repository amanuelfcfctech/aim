CREATE PROCEDURE [dbo].[usp_SampleResult_SEL_DataSourceTypeID]
	  @DataSourceTypeID		INT
	, @IsActive				BIT

-- exec usp_SampleResult_SEL_DataSourceTypeID 10000, 1
AS
BEGIN
SET NOCOUNT ON;

SELECT 
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
	, AN.DataSourceTypeID
	, DST.Name as DataSourceTypeName	
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
WHERE

		AN.DataSourceTypeID = @DataSourceTypeID
	AND SR.IsActive = @IsActive

ORDER BY AN.AnalyzerNutrientCode
END