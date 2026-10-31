CREATE PROCEDURE [dbo].[usp_SampleQualityControlMassEntryDetail_SEL_SampleQualityControlMassEntryID_SampleID_DataSourceTypeID]

@SampleQualityControlMassEntryID	INT
, @SampleID							INT
, @DataSourceTypeID					INT

AS
BEGIN

SET NOCOUNT ON;

SELECT 
	MED.*	
	, SQP.SampleQualityParameterName
	, AU.UserName	
	, 0 AS CurrentValue
		
FROM 
	dbo.SampleQualityControlMassEntryDetail MED with (nolock)

INNER JOIN
	dbo.SampleQualityParameter SQP with (nolock)
		ON MED.SampleQualityParameterID = SQP.SampleQualityParameterID

--LEFT OUTER JOIN
--	(SELECT SQP.SampleQualityParameterID, IsNull(ResultValue,0) as ResultValue
--	FROM 
--			dbo.SampleResult SR
--		INNER JOIN 
--			dbo.SampleQualityParameter SQP WITH (NOLOCK)
--				ON  SQP.DataSourceTypeID = @DatasourceTypeID
--				AND SR.SampleID = @SampleID
--				AND SQP.IsActive = 1				
--				AND SR.IsActive = 1) 
--	X ON SQP.SampleQualityParameterID = X.SampleQualityParameterID


LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON MED.CFCUserID = U.CFCUserID
LEFT OUTER JOIN
	dbo.aspnet_Users AU WITH(NOLOCK)
		ON AU.UserId = U.UserID

WHERE MED.SampleQualityControlMassEntryID = @SampleQualityControlMassEntryID

ORDER BY MED.SortOrder 

END