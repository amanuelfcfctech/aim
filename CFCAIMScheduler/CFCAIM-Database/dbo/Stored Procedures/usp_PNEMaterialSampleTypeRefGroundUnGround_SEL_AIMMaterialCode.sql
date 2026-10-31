CREATE PROCEDURE [dbo].[usp_PNEMaterialSampleTypeRefGroundUnGround_SEL_AIMMaterialCode]
	@AIMMaterialCode	NVARCHAR(20)

AS
BEGIN
SET NOCOUNT ON;

--exec usp_PNEMaterialSampleTypeRefGroundUnGround_SEL_AIMMaterialCode '2005'

	SELECT
		PX.PNEMaterialSampleTypeRefGroundUnGroundID	  
		, PX.PNEMaterialSampleTypeID
		, PX.GroundUnGroundID
		, PX.UpdateStamp
		, PX.CFCUserID
		, U.UserName
		, PMST.PNEMaterialSampleTypeCode
		, G.GroundUnGroundString
	FROM 
		dbo.PNEMaterialSampleTypeRefGroundUnGround PX 
			
	INNER JOIN 
		dbo.PNEMaterialSampleType PMST 
			ON PMST.PNEMaterialSampleTypeID = PX.PNEMaterialSampleTypeID
	INNER JOIN 
		dbo.PNEMaterialType PMT 
			ON PMT.PNEMaterialSampleTypeID = PMST.PNEMaterialSampleTypeID
	INNER JOIN 
		dbo.AnalyzerMaterial ANZ 
			ON ANZ.AnalyzerMaterialID = PMT.AnalyzerMaterialID
	INNER JOIN 
		dbo.AIMMaterialRefAnalyzerMaterial x 
			ON x.AnalyzerMaterialID = ANZ.AnalyzerMaterialID and ANZ.DataSourceTypeID = 11
	INNER JOIN 
		dbo.AIMMaterial AM 
			ON AM.aimmaterialid = x.aimmaterialid
	LEFT OUTER JOIN
		GroundUnGround G
			ON PX.GroundUnGroundID = G.GroundUnGroundID

	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON PX.CFCUserID = U.CFCUserID 

WHERE AM.AIMMaterialCode = @AIMMaterialCode
	

END