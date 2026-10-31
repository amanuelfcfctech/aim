CREATE PROCEDURE [dbo].[usp_AnalyzerSupplier_SEL_SupplierID]
		@AnalyzerSupplierID		INT
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_AnalyzerSupplier_SEL_SupplierID 3203

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
	ANS.AnalyzerSupplierID = @AnalyzerSupplierID


ORDER BY ANS.AnalyzerSupplierName

END