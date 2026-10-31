CREATE PROCEDURE [dbo].[usp_ExportPlant_SEL_ExportPlantGroupID]
	
		@ExportPlantGroupID		INT

--exec usp_ExportPlant_SEL_ExportPlantGroupID 5		

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  EP.ExportPlantID
	, EP.ExportPlantGroupID
	, EP.CFCPlantID	
	, CI.CFCPlantCode
	, CI.CFCPlantName
	, EP.IsActive
	, EP.UpdateStamp
	, EP.CFCUserID	
	, U.UserName
	, EPG.ExportPlantGroupCode

	--1/25/24 - commented
	--, EPG.AIMOwnerLocationID
	--, AOL.LocationCode as AIMOwnerLocationCode
	--, AOL.LocationName as AIMOwnerLocationName

FROM 
	dbo.ExportPlant EP
INNER JOIN
	dbo.ExportPlantGroup EPG
		ON EP.ExportPlantGroupID = EPG.ExportPlantGroupID
INNER JOIN
	dbo.CFCPlant CI
		ON CI.CFCPlantID = EP.CFCPlantID

--1/25/24 - commented
--LEFT OUTER JOIN
--	dbo.AIMOwnerLocation AOL 
--		ON AOL.AIMOwnerLocationID = EPG.AIMOwnerLocationID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON EP.CFCUserID = U.CFCUserID

WHERE
	EP.ExportPlantGroupID = @ExportPlantGroupID 
	
END