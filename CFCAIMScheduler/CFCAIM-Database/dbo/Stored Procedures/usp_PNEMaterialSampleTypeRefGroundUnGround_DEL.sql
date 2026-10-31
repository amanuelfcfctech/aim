CREATE PROCEDURE [dbo].[usp_PNEMaterialSampleTypeRefGroundUnGround_DEL]
				@PNEMaterialSampleTypeRefGroundUnGroundID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.PNEMaterialSampleTypeRefGroundUnGround WITH(ROWLOCK)
WHERE
	PNEMaterialSampleTypeRefGroundUnGroundID = @PNEMaterialSampleTypeRefGroundUnGroundID

END