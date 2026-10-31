CREATE PROCEDURE [dbo].[usp_Sample_SEL_SampleID]
	@SampleID		INT
AS
BEGIN
SET NOCOUNT ON;


--we are using this proc for getting both active and inactive records
--so never have IsActive in where clause

SELECT
		S.*
	  , AIMM.AIMMaterialCode 	
	  , AIMM.MaterialTypeID 
	  , AM.AnalyzerMaterialCode 
	  , AM.AnalyzerMaterialName	 	 

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
FROM 
	dbo.Sample S WITH(NOLOCK)
	LEFT OUTER JOIN 
		dbo.AIMMaterial AIMM WITH(NOLOCK)
			ON AIMM.AIMMaterialID = S.AIMMaterialID 
	LEFT OUTER JOIN 
		dbo.AnalyzerMaterial AM WITH(NOLOCK)
			ON AM.AnalyzerMaterialID = S.AnalyzerMaterialID 
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON S.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE 
	S.SampleID = @SampleID
	--we are using this proc for getting both active and inactive records
	--so never have IsActive in where clause
END
