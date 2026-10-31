CREATE PROCEDURE [dbo].[usp_AnalyzerSupplierLocationCombined_SEL_AnalyzerSupplierLocationCombinedName_DataSourceTypeID]
		@AnalyzerSupplierLocationCombinedName		Varchar(50)
		, @DataSourceTypeID		INT



AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	  ANS.*	
FROM 
	dbo.AnalyzerSupplierLocationCombined ANS WITH(NOLOCK)
	
WHERE 
	RTRIM(ANS.AnalyzerSupplierLocationCombinedName) = @AnalyzerSupplierLocationCombinedName and 
	ANS.DataSourceTypeID = @DataSourceTypeID

END