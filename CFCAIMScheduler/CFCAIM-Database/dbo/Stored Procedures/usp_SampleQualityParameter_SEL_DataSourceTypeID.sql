CREATE PROCEDURE [dbo].[usp_SampleQualityParameter_SEL_DataSourceTypeID]
			
	@DataSourceTypeID   INT


AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  SQP.*
	  , DT.Name as DataSourceUserName
	, AU.UserName
FROM 
	dbo.SampleQualityParameter SQP WITH(NOLOCK)

	LEFT OUTER JOIN
	dbo.DataSourceType DT with (nolock)
		ON SQP.DataSourceTypeID = DT.DataSourceTypeID

	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SQP.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
		dbo.aspnet_Users AU WITH(NOLOCK)
			ON AU.UserId = U.UserID

WHERE
	SQP.DataSourceTypeID = @DataSourceTypeID

END