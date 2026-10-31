CREATE PROCEDURE [dbo].[usp_AnalyzerSupplierLocation_SEL]

AS
BEGIN
SET NOCOUNT ON;

SELECT
	  ANSL.*
	, ANSL.UpdateStamp	
	, DST.Name as DataSourceTypeName	

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalyzerSupplierLocation ANSL WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.DataSourceType DST with (nolock)
			ON ANSL.DataSourceTypeID = DST.DataSourceTypeID	
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON ANSL.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

END