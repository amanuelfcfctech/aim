CREATE PROCEDURE [dbo].[usp_SampleQualityControlMassEntry_SEL]
AS
BEGIN

--exec usp_SampleQualityControlMassEntry_SEL

SET NOCOUNT ON;
SELECT 
	  SQC.*	
	, DST.Name as DataSourceTypeName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.SampleQualityControlMassEntry SQC WITH(NOLOCK)

		LEFT OUTER JOIN
			dbo.DataSourceType DST with (nolock)
				ON SQC.DataSourceTypeID = DST.DataSourceTypeID
			
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON SQC.CFCUserID = U.CFCUserID

			-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
			--LEFT OUTER JOIN
			--	dbo.aspnet_Users AU WITH(NOLOCK)
			--		ON AU.UserId = U.UserID


END