CREATE PROCEDURE [dbo].[usp_AnalyzerNutrient_SEL_SampleResultTemplateDetail]
		@DataSourceTypeID			INT    
		, @SampleResultTemplateID	INT
		, @IsActive					BIT

-- exec usp_AnalyzerNutrient_SEL_SampleResultTemplateDetail 1, 7, 1
-- exec usp_AnalyzerNutrient_SEL_SampleResultTemplateDetail 10007, 5, 1

AS
BEGIN
SET NOCOUNT ON;

SELECT DISTINCT 
	  NN.*
	, NN.UpdateStamp	
	, X.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalyzerNutrient NN WITH(NOLOCK)
LEFT OUTER JOIN
	(SELECT * 
	FROM dbo.SampleResultTemplateDetail SRTD with (nolock)
	where SampleResultTemplateID = @SampleResultTemplateID) X
		ON X.AnalyzerNutrientID = NN.AnalyzerNutrientID

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON NN.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 	
	NN.IsActive = 1  
	and X.AnalyzerNutrientID IS NULL

	-- 3/2/17
	and NN.DataSourceTypeID = @DataSourceTypeID

ORDER BY
	NN.AnalyzerNutrientCode
END