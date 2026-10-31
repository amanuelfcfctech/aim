CREATE PROCEDURE [dbo].[usp_SampleQualityControlMassEntry_SEL]
AS
BEGIN

--exec usp_SampleQualityControlMassEntry_SEL

SET NOCOUNT ON;
SELECT 
	  SQC.*	
	, DST.Name as DataSourceTypeName
	, AU.UserName
FROM 
	dbo.SampleQualityControlMassEntry SQC WITH(NOLOCK)

		LEFT OUTER JOIN
			dbo.DataSourceType DST with (nolock)
				ON SQC.DataSourceTypeID = DST.DataSourceTypeID
			
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON SQC.CFCUserID = U.CFCUserID
			LEFT OUTER JOIN
				dbo.aspnet_Users AU WITH(NOLOCK)
					ON AU.UserId = U.UserID


END