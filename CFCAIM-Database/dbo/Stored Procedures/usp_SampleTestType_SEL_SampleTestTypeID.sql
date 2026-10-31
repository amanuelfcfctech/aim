CREATE PROCEDURE [dbo].[usp_SampleTestType_SEL_SampleTestTypeID]

	@SampleTestTypeID INT

AS
BEGIN

--exec usp_SampleTestType_SEL

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
	STT.SampleTestTypeID = @SampleTestTypeID

END