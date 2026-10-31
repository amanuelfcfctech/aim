CREATE PROCEDURE [dbo].[usp_SampleQualityControlMassEntry_SEL_Name]
	@SampleQualityControlMassEntryName	VARCHAR(50)

AS
BEGIN


SET NOCOUNT ON;
SELECT 
	  SQCME.*	

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.SampleQualityControlMassEntry SQCME WITH(NOLOCK)		
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON SQCME.CFCUserID = U.CFCUserID

			-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
			--LEFT OUTER JOIN
			--	dbo.aspnet_Users AU WITH(NOLOCK)
			--		ON AU.UserId = U.UserID

WHERE

	SQCME.SampleQualityControlMassEntryName = @SampleQualityControlMassEntryName
END