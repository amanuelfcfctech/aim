CREATE PROCEDURE [dbo].[usp_GroundUnGround_SEL_GroundUnGroundID]

	@GroundUnGroundID		INT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	GroundUnGroundID
	, GroundUnGroundString
	
FROM 
	dbo.GroundUnGround 

WHERE
	GroundUnGroundID = @GroundUnGroundID

END