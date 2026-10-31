CREATE PROCEDURE [dbo].[usp_AnalyzerSupplier_SEL_DataSourceTypeID_AnalyzerSupplierCode_AnalyzerSupplierName]
		@DataSourceTypeID		    INT
		, @AnalyzerSupplierCode		INT
		, @AnalyzerSupplierName		INT
		
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	  ANS.*	
FROM 
	dbo.AnalyzerSupplier ANS WITH(NOLOCK)
	
WHERE 
	ANS.DataSourceTypeID = @DataSourceTypeID
	and ANS.AnalyzerSupplierCode = @AnalyzerSupplierCode 
	and ANS.AnalyzerSupplierName = @AnalyzerSupplierName 
	

END