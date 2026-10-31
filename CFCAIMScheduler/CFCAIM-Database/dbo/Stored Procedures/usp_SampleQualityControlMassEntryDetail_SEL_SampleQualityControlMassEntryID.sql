CREATE PROCEDURE [dbo].[usp_SampleQualityControlMassEntryDetail_SEL_SampleQualityControlMassEntryID]

@SampleQualityControlMassEntryID INT

AS
BEGIN

SET NOCOUNT ON;

SELECT 
	MED.*		
	, SQP.SampleQualityParameterName
	, AU.UserName
	
FROM 
	dbo.SampleQualityControlMassEntryDetail MED with (nolock)
INNER JOIN
	dbo.SampleQualityParameter SQP with (nolock)
		ON MED.SampleQualityParameterID = SQP.SampleQualityParameterID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON MED.CFCUserID = U.CFCUserID
LEFT OUTER JOIN
	dbo.aspnet_Users AU WITH(NOLOCK)
		ON AU.UserId = U.UserID

WHERE MED.SampleQualityControlMassEntryID = @SampleQualityControlMassEntryID

ORDER BY MED.SortOrder 

END