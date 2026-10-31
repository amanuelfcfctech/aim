CREATE PROCEDURE [dbo].[usp_AnalyzerSupplierLocation_SEL_SupplierLocationID]
		@AnalyzerSupplierLocationID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  ASL.*	
	, DT.Name as DataSourceUserName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalyzerSupplierLocation ASL WITH(NOLOCK)
		LEFT OUTER JOIN
		dbo.DataSourceType DT with (nolock)
			ON ASL.DataSourceTypeID = DT.DataSourceTypeID
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON ASL.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID
WHERE 
	ASL.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID

ORDER BY ASL.AnalyzerSupplierLocationName

END