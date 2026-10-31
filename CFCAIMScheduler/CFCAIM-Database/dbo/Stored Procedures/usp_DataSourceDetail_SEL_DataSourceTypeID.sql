CREATE PROCEDURE [dbo].[usp_DataSourceDetail_SEL_DataSourceTypeID]
	@DataSourceTypeID INT

AS
BEGIN
SET NOCOUNT ON;

SELECT
	    DTL.*
		, ADST.AnalyzerDataSourceTypeName
		, DST.Name
		, HDR.*
	  
FROM 
	dbo.DataSourceDetail DTL WITH(NOLOCK)

	INNER JOIN
		dbo.DataSourceHeader HDR 
			ON DTL.DataSourceHeaderID = HDR.DataSourceHeaderID

	--8/20/18
	--LEFT OUTER JOIN
	INNER JOIN

		dbo.AnalyzerDataSourceType ADST
			ON DTL.AnalyzerDataSourceTypeID = ADST.AnalyzerDataSourceTypeID

	LEFT OUTER JOIN
		dbo.DataSourceType DST
			ON DTL.DataSourceTypeID = DST.DataSourceTypeID

	
WHERE HDR.DataSourceTypeID = @DataSourceTypeID

END