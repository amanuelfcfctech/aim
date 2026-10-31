CREATE PROCEDURE [dbo].[usp_Sample_SEL_SampleCode_ForImports]
	@SampleCode		VARCHAR(50)
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1
	    S.*
	  , AU.UserName		  
	  , CPA.ProductionVersion
	  , CPA.ProductionDate
	 
FROM 
	dbo.Sample S WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.CFCProductArchive CPA
			ON S.CFCProductArchiveID = CPA.CFCProductArchiveID

	LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON S.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
		dbo.aspnet_Users AU WITH(NOLOCK)
			ON AU.UserId = U.UserID
WHERE 
	S.SampleCode = @SampleCode

ORDER BY CreatedStamp desc

END