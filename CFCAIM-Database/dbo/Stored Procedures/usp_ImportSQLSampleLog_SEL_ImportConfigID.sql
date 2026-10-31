CREATE PROCEDURE [dbo].[usp_ImportSQLSampleLog_SEL_ImportConfigID]
		@ImportConfigID		INT		
			
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	L.ImportSQLSampleLogID
	, L.ImportConfigID
	, ISNULL(L.LastImportDate, getdate()) as LastImportDate
	, L.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.ImportSQLSampleLog L WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON L.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID 

ORDER BY
	L.CreatedStamp DESC 

END