CREATE PROCEDURE [dbo].[usp_AnalyzerDataSourceType_CrossReference_SEL_DataSourceTypeID]
	@DataSourceTypeID INT


--EXEC DBO.usp_AnalyzerDataSourceType_CrossReference_SEL_DataSourceTypeID 10000

AS
BEGIN
SET NOCOUNT ON;

SELECT 
x.AnalyzerDataSourceTypeID
, x.AnalyzerDataSourceTypeName
, null as DataSourceTypeID
, null as DataSourceHeaderID



FROM (
	    
SELECT
	 ADST.AnalyzerDataSourceTypeID
	, ADST.AnalyzerDataSourceTypeName
	, ADST.DataSourceTypeID
FROM 
	dbo.AnalyzerDataSourceType ADST
LEFT OUTER JOIN 
	dbo.DataSourceDetail DTL 
		ON DTL.AnalyzerDataSourceTypeID = ADST.AnalyzerDataSourceTypeID
WHERE 
	ADST.DataSourceTypeID = @DataSourceTypeID) x

LEFT OUTER JOIN dbo.DataSourceDetail DSD on x.AnalyzerDataSourceTypeID = DSD.AnalyzerDataSourceTypeID

WHERE DSD.DataSourceDetailID IS NULL

END