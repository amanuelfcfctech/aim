CREATE PROCEDURE [dbo].[usp_AnalyzerSupplier_SEL_AnalyzerSupplierCode_DataSourceTypeID]
		@AnalyzerSupplierCode		Varchar(30)
		, @DataSourceTypeID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	  ANS.*	
FROM 
	dbo.AnalyzerSupplier ANS WITH(NOLOCK)
	
WHERE 
	ANS.AnalyzerSupplierCode = @AnalyzerSupplierCode and ANS.DataSourceTypeID = @DataSourceTypeID

END