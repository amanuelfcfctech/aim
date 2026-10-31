CREATE PROCEDURE [dbo].[usp_SampleTestTypeGroupDetail_SEL_GroupID]

	@GroupID		INT

--exec usp_SampleTestTypeGroupDetail_SEL_GroupID 1

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  STTGD.SampleTestTypeGroupDetailID
	, STTGD.SampleTestTypeGroupID	
	, STTGD.SampleTestTypeID
	, STTGD.IsActive
	, STTGD.UpdateStamp
	, STTGD.CFCUserID	
	, U.UserName
	, STT.SampleTestTypeName
	, STTG.SampleTestTypeGroupCode
	

FROM 
	dbo.SampleTestTypeGroupDetail STTGD
	

INNER JOIN
	dbo.SampleTestTypeGroup STTG
		ON STTGD.SampleTestTypeGroupID = STTG.SampleTestTypeGroupID

LEFT OUTER JOIN
	dbo.SampleTestType STT
		on STTGD.SampleTestTypeID = STT.SampleTestTypeID

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON STTGD.CFCUserID = U.CFCUserID

WHERE
	STTGD.SampleTestTypeGroupID = @GroupID 
END