
CREATE PROCEDURE [dbo].[usp_Sample_SEL_SampleCode]
	@SampleCode		VARCHAR(50)
AS
BEGIN
SET NOCOUNT ON;

DECLARE @Days Int
SET @Days = 0
SET @Days = (Select TOP 1 IsNull(SampleNumberOfDays,0) from dbo.SystemSettings with (nolock)) 

IF @Days = 0
	SET @Days = 60


--exec usp_Sample_SEL_SampleCode '2Hot_0002031'


SELECT TOP 1
	    S.*	  		  
	  , CPA.ProductionVersion
	  , CPA.ProductionDate

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
	 
FROM 
	dbo.Sample S WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.CFCProductArchive CPA
			ON S.CFCProductArchiveID = CPA.CFCProductArchiveID	
	LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON S.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE 
	S.SampleCode = @SampleCode 
	AND S.SampleDate > GETDATE() - @Days

ORDER BY CreatedStamp desc

END
