CREATE PROCEDURE [dbo].[usp_ExportPlantGroup_SEL_ExportPlantGroupID]

	@ExportPlantGroupID		INT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  EP.ExportPlantGroupID
	, EP.ExportPlantGroupCode	

	--1/25/24 - commented
	--, EP.AIMOwnerID
    --, EP.AIMOwnerLocationID

	, EP.IsActive
	, EP.UpdateStamp
	, EP.CFCUserID	
	, U.UserName

	--1/25/24 - commented
	--, AOL.LocationCode as AIMOwnerLocationCode
	--, AOL.LocationName as AIMOwnerLocationName
	
FROM 
	dbo.ExportPlantGroup EP

--1/25/24 - commented
--INNER JOIN	
--	dbo.AIMOwner AO
--		ON EP.AIMOwnerID = AO.AIMOwnerID

--1/25/24 - commented	
--INNER JOIN
--	dbo.AIMOwnerLocation AOL
--		ON AO.AIMOwnerID = AOL.AIMOwnerID
--			AND EP.AIMOwnerLocationID = AOL.AIMOwnerLocationID

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON EP.CFCUserID = U.CFCUserID



WHERE
	EP.ExportPlantGroupID = @ExportPlantGroupID 
	
END