CREATE PROCEDURE [dbo].[usp_Sample_SEL_SampleCode_ForImports]
	@SampleCode		    VARCHAR(50)
		
	--3/12/21 - removed, because datasourcetypeid on the sample header shouldn't be used -- because
	--			one sample can have several datasourcetypes (which are on the AnalyzerNutrient and part of the results)
	--,@DataSourceTypeID	INT
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1
	    S.*
	  , CPA.ProductionVersion
	  , CPA.ProductionDate
	  , U.UserName		  
	 
FROM 
	dbo.Sample S WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.CFCProductArchive CPA
			ON S.CFCProductArchiveID = CPA.CFCProductArchiveID

	LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON S.CFCUserID = U.CFCUserID
WHERE 
	S.SampleCode = @SampleCode 
	-----and	S.DataSourceTypeID = @DataSourceTypeID

ORDER BY CreatedStamp desc

END