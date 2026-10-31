CREATE PROCEDURE [dbo].[usp_SampleQuality_SEL_SampleID_DataSourceTypeID_IsActive]
	  @SampleID					INT
	, @DataSourceTypeID			BIT	
	, @IsActive					BIT



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
	, DST.Name as DataSourceTypeName		

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName	
	, U.UserName	
			
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

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID		

WHERE

		SQ.SampleID = @SampleID
	AND SQP.DataSourceTypeID = @DataSourceTypeID	
	AND SQ.IsActive = @IsActive
	
ORDER BY SQP.SampleQualityParameterName


END