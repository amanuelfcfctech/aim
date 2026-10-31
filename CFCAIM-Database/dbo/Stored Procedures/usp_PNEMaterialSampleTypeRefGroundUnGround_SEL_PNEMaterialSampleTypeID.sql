CREATE PROCEDURE [dbo].[usp_PNEMaterialSampleTypeRefGroundUnGround_SEL_PNEMaterialSampleTypeID]

	@PNEMaterialSampleTypeID	INT


AS
BEGIN
SET NOCOUNT ON;

--exec usp_PNEMaterialSampleTypeRefGroundUnGround_SEL_PNEMaterialSampleTypeID_GroundUnGroundID 1


SELECT
		X.PNEMaterialSampleTypeRefGroundUnGroundID	  
		, X.PNEMaterialSampleTypeID
		, X.GroundUnGroundID
		, X.UpdateStamp
		, X.CFCUserID
	
FROM 
		dbo.PNEMaterialSampleTypeRefGroundUnGround X WITH(NOLOCK)
	
WHERE	X.PNEMaterialSampleTypeID = @PNEMaterialSampleTypeID
		
END