CREATE PROCEDURE [dbo].[usp_AnalyzerSupplier_SEL_DataSourceTypeID_SupplierName]
		@DataSourceTypeID				INT
		,@AnalyzerSupplierName		NVARCHAR(30)
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_AnalyzerSupplier_SEL_DataSourceTypeID_SupplierName 13008, '20/20Farm'

SELECT
	  ANS.*	
	, DT.Name as DataSourceUserName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalyzerSupplier ANS WITH(NOLOCK)
		LEFT OUTER JOIN
		dbo.DataSourceType DT with (nolock)
			ON ANS.DataSourceTypeID = DT.DataSourceTypeID
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON ANS.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID
WHERE 
	ANS.AnalyzerSupplierName = @AnalyzerSupplierName
	and ANS.DataSourceTypeID = @DataSourceTypeID

ORDER BY ANS.AnalyzerSupplierName

END