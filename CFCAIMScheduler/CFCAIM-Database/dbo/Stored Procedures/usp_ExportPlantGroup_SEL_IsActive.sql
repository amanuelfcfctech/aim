CREATE PROCEDURE [dbo].[usp_ExportPlantGroup_SEL_IsActive]

	@IsActive		BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  EP.ExportPlantGroupID
	, EP.ExportPlantGroupCode	

	--1/25/24 - commented
	--, EP.AIMOwnerID
 --   , EP.AIMOwnerLocationID
	, EP.IsActive
	, EP.UpdateStamp
	, EP.CFCUserID	
	, U.UserName

FROM 
	dbo.ExportPlantGroup EP

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON EP.CFCUserID = U.CFCUserID

WHERE
	EP.IsActive = @IsActive 
	
END