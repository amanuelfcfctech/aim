CREATE PROCEDURE [dbo].[usp_AnalyzerSupplier_SEL_AnalyzerSupplierID_DataSourceTypeID]
		@AnalyzerSupplierID		INT
		, @DataSourceTypeID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  ANS.*	
FROM 
	dbo.AnalyzerSupplier ANS WITH(NOLOCK)
	
WHERE 
	ANS.AnalyzerSupplierID = @AnalyzerSupplierID and 
	ANS.DataSourceTypeID = @DataSourceTypeID and
	ANS.IsActive = 1


END