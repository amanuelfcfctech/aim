
CREATE PROCEDURE [dbo].[usp_SampleQuality_SEL_SampleID_DataSourceTypeID]
	  @SampleID					INT
	, @DataSourceTypeID			INT	


AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  SQ.SampleQualityID
	, SQ.SampleID
	, SQ.SampleQualityParameterID
	, SQP.SampleQualityParameterName
	, SQ.ResultValueNumeric
	, SQ.UpdateStamp
	, SQ.IsActive	
	, SQ.CFCUserID	
	, SQP.DataSourceTypeID	
	, AU.UserName	
	, DST.Name as DataSourceTypeName		
			
FROM 
	dbo.SampleQuality SQ
	INNER JOIN 
		dbo.SampleQualityParameter SQP WITH(NOLOCK)
			ON  SQ.SampleQualityParameterID = SQP.SampleQualityParameterID
			AND SQP.IsActive = 1				
	LEFT OUTER JOIN
		dbo.DataSourceType DST with (nolock)
			ON SQP.DataSourceTypeID = DST.DataSourceTypeID	
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SQ.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
		dbo.aspnet_Users AU WITH(NOLOCK)
			ON AU.UserId = U.UserID		

WHERE

		SQ.SampleID = @SampleID
	AND SQP.DataSourceTypeID = @DataSourceTypeID	
	
ORDER BY SQP.SampleQualityParameterName


END