CREATE PROCEDURE [dbo].[usp_Sample_SEL_SampleTestTypeID]
	@SampleTestTypeID			INT


AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1
		S.*	 
	  , U.UserName
FROM 
	dbo.Sample S WITH(NOLOCK)
	
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON S.CFCUserID = U.CFCUserID

WHERE 
	S.SampleTestTypeID = @SampleTestTypeID 
END