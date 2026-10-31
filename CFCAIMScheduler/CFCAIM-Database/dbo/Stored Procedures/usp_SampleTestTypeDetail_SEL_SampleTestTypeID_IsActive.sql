CREATE PROCEDURE [dbo].[usp_SampleTestTypeDetail_SEL_SampleTestTypeID_IsActive]

@SampleTestTypeID	INT
, @IsActive			BIT

AS
BEGIN

SET NOCOUNT ON;

SELECT 
	STTD.SampleTestTypeDetailID
	, STTD.SampleTestTypeID
	, STTD.DataSourceTypeID
	, STTD.SortOrder
	, STTD.UpdateStamp
	, STTD.CFCUserID
	, STTD.IsActive
	, DST.Name as DataSourceName
	, U.UserName
	
	
FROM 
	dbo.SampleTestTypeDetail STTD with (nolock)
INNER JOIN
	dbo.DataSourceType DST with (nolock)
		ON STTD.DataSourceTypeID = DST.DataSourceTypeID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON STTD.CFCUserID = U.CFCUserID

WHERE STTD.SampleTestTypeID = @SampleTestTypeID AND
	  STTD.IsActive = @IsActive

ORDER BY STTD.SortOrder 

END