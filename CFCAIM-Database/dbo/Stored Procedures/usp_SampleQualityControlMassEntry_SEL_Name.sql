CREATE PROCEDURE [dbo].[usp_SampleQualityControlMassEntry_SEL_Name]
	@SampleQualityControlMassEntryName	VARCHAR(50)

AS
BEGIN


SET NOCOUNT ON;
SELECT 
	  SQCME.*	
	, AU.UserName
FROM 
	dbo.SampleQualityControlMassEntry SQCME WITH(NOLOCK)		
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON SQCME.CFCUserID = U.CFCUserID
			LEFT OUTER JOIN
				dbo.aspnet_Users AU WITH(NOLOCK)
					ON AU.UserId = U.UserID

WHERE

	SQCME.SampleQualityControlMassEntryName = @SampleQualityControlMassEntryName
END