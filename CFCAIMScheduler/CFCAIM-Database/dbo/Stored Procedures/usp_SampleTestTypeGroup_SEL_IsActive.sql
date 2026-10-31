CREATE PROCEDURE [dbo].[usp_SampleTestTypeGroup_SEL_IsActive]

	@IsActive		BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  STTG.SampleTestTypeGroupID
	, STTG.SampleTestTypeGroupCode	
	, STTG.IsActive
	, STTG.UpdateStamp
	, STTG.CFCUserID	
	, U.UserName

FROM 
	dbo.SampleTestTypeGroup STTG

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON STTG.CFCUserID = U.CFCUserID

WHERE
	STTG.IsActive = @IsActive 

ORDER BY
	STTG.SampleTestTypeGroupCode ASC
	
END