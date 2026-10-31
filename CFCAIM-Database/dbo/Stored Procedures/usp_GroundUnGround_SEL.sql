CREATE PROCEDURE [dbo].[usp_GroundUnGround_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	GroundUnGroundID
	, GroundUnGroundString
	
FROM 
	dbo.GroundUnGround WITH(NOLOCK)

END