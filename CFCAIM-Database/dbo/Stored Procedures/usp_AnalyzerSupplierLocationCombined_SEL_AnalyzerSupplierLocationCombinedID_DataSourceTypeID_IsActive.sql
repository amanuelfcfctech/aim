CREATE PROCEDURE [dbo].[usp_AnalyzerSupplierLocationCombined_SEL_AnalyzerSupplierLocationCombinedID_DataSourceTypeID_IsActive]
		@AnalyzerSupplierLocationCombinedID		INT
		, @DataSourceTypeID						INT
		, @IsActive								BIT



AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	  ANS.*	
FROM 
	dbo.AnalyzerSupplierLocationCombined ANS WITH(NOLOCK)
	
WHERE 
	ANS.AnalyzerSupplierLocationCombinedID = @AnalyzerSupplierLocationCombinedID and 
	ANS.DataSourceTypeID = @DataSourceTypeID and
	ANS.IsActive = @IsActive

END