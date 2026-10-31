CREATE PROCEDURE [dbo].[usp_SampleTestType_SEL_Name]
	@SampleTestTypeName	VARCHAR(50)

AS
BEGIN


SET NOCOUNT ON;
SELECT 

	STT.SampleTestTypeID
	, STT.SampleTestTypeName
	, STT.UpdateStamp
	, STT.CFCUserID
	, U.UserName
FROM 
	dbo.SampleTestType STT WITH(NOLOCK)		
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON STT.CFCUserID = U.CFCUserID

WHERE

	STT.SampleTestTypeName = @SampleTestTypeName
END