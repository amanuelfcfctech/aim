CREATE PROCEDURE [dbo].[usp_PNEMaterialSampleTypeRefGroundUnGround_SEL]


AS
BEGIN
SET NOCOUNT ON;

--exec usp_PNEMaterialSampleTypeRefGroundUnGround_SEL


	SELECT
		X.PNEMaterialSampleTypeRefGroundUnGroundID	  
		, X.PNEMaterialSampleTypeID
		, X.GroundUnGroundID
		, X.UpdateStamp
		, X.CFCUserID
		, U.UserName
		, PMST.PNEMaterialSampleTypeCode
		, G.GroundUnGroundString
	FROM 
		dbo.PNEMaterialSampleTypeRefGroundUnGround X 
			
	LEFT OUTER JOIN	
		PNEMaterialSampleType PMST
			ON X.PNEMaterialSampleTypeID = PMST.PNEMaterialSampleTypeID
	LEFT OUTER JOIN
		GroundUnGround G
			ON X.GroundUnGroundID = G.GroundUnGroundID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON X.CFCUserID = U.CFCUserID 


END