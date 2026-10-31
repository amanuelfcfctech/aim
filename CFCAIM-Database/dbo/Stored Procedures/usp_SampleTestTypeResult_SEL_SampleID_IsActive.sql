CREATE PROCEDURE [dbo].[usp_SampleTestTypeResult_SEL_SampleID_IsActive]

	@SampleID	INT	
	, @IsActive	BIT


AS
BEGIN


SET NOCOUNT ON;
SELECT 
	  STTR.SampleTestTypeResultID
	, STTR.SampleID
	, STTR.SampleTestTypeDetailID	
	, STTD.DataSourceTypeID
	, STTR.IsIncluded
	, STTR.IsActive
	, STTR.UpdateStamp
	, STTR.CFCUserID
	, U.UserName
	, DST.Name as DataSourceName
	, STT.SampleTestTypeName

FROM 
	dbo.SampleTestTypeResult STTR WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.SampleTestTypeDetail STTD
			ON STTR.SampleTestTypeDetailID = STTD.SampleTestTypeDetailID

	LEFT OUTER JOIN
		dbo.SampleTestType STT
			ON STTD.SampleTestTypeID = STT.SampleTestTypeID

	LEFT OUTER JOIN
		dbo.DataSourceType DST
			ON STTD.DataSourceTypeID = DST.DataSourceTypeID
			
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON STTR.CFCUserID = U.CFCUserID

WHERE
	STTR.SampleID = @SampleID and 
	STTR.IsActive = @IsActive

END