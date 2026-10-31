CREATE PROCEDURE [dbo].[usp_AIMNutrientRefAnalyzerNutrient_SEL_DataSourceTypeID]

	@DataSourceTypeID INT
AS
BEGIN
SET NOCOUNT ON;


--exec usp_AIMNutrientRefAnalyzerNutrient_SEL_DataSourceTypeID 10000

SELECT
	    XREF.AIMNutrientRefAnalyzerNutrientID
	  , XREF.AIMNutrientID
	  , XREF.AnalyzerNutrientID
	  , XREF.UpdateStamp
	  , A.AIMNutrientCode
	  , A.AIMNutrientName
	  , A.UnitTypeID
	  , N.AnalyzerNutrientCode
	  , N.AnalyzerNutrientName	  
	  , N.UnitType  	  
	  , N.ConversionFactor
	  , DST.Name as DataSourceTypeName
	  , N.DataSourceTypeID

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
	  
	  
FROM 
	dbo.AIMNutrientRefAnalyzerNutrient XREF WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMNutrient A 
			ON XREF.AIMNutrientID = A.AIMNutrientID
	LEFT OUTER JOIN	dbo.AnalyzerNutrient N	
			ON XREF.AnalyzerNutrientID = N.AnalyzerNutrientID
	LEFT OUTER JOIN dbo.DataSourceType DST with (nolock)
			ON N.DataSourceTypeID = DST.DataSourceTypeID	
	LEFT OUTER JOIN 	CFCUser.CFCUser U WITH (NOLOCK) 
			ON XREF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN 	dbo.aspnet_Users AU WITH (NOLOCK)	
	--		ON AU.UserId = U.UserID

WHERE N.DataSourceTypeID = @DataSourceTypeID

END