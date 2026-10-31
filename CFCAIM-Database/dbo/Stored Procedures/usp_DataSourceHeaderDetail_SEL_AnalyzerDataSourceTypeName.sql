CREATE PROCEDURE [dbo].[usp_DataSourceHeaderDetail_SEL_AnalyzerDataSourceTypeName]		
		 @AnalyzerDataSourceTypeName				VARCHAR(50)
		
--exec usp_DataSourceHeaderDetail_SEL_AnalyzerDataSourceTypeName 'NIR'

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

SET @RecordID = (SELECT 
	TOP 1 DST.DataSourceTypeID

FROM dbo.AnalyzerDataSourceType ADST
INNER JOIN
	dbo.DataSourceDetail DTL
		ON ADST.AnalyzerDataSourceTypeID = DTL.AnalyzerDataSourceTypeID
INNER JOIN
	dbo.DataSourceHeader HDR
		ON DTL.DataSourceHeaderID = HDR.DataSourceHeaderID
LEFT JOIN DataSourceType DST
	on DTL.DataSourceTypeID = DST.DataSourceTypeID

WHERE
	ADST.AnalyzerDataSourceTypeName = @AnalyzerDataSourceTypeName and
	ADST.IsActive = 1 and
	DST.IsActive = 1
	)
	

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END