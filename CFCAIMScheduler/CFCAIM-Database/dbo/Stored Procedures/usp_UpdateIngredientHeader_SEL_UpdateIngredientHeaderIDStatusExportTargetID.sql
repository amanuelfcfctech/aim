CREATE PROCEDURE [dbo].[usp_UpdateIngredientHeader_SEL_UpdateIngredientHeaderIDStatusExportTargetID]
			  @UpdateIngredientHeaderID			INT 
			, @Status							INT		= NULL
			, @ExportTargetID					INT		= NULL
AS
BEGIN

SET NOCOUNT ON;

SELECT 
	  H.UpdateIngredientHeaderID
	, H.UpdateName
	, H.CreateDate
	, H.CFCUserID
	, H.Status
	, H.AIMDatabaseID
	, H.UpdateStamp
	, H.UpdateCFCUserID
	, H.ExportTargetID
	, H.CFCPlantID
	, H.ShortName	
	, CP.CFCPlantCode 
	, H.ShortName
	, U.UserName 
	, H.ExportExpandID			--1/10/24
	, H.ExportExpandName		--1/10/24
	, H.ExportPlantGroupID		--1/12/24
	, H.ExportPlantGroupName	--1/12/24

FROM 
	dbo.UpdateIngredientHeader H WITH(NOLOCK)
	LEFT OUTER JOIN 
		dbo.CFCPlant CP WITH(NOLOCK)
			ON CP.CFCPlantID = H.CFCPlantID 
	LEFT OUTER JOIN 
		CFCUser.CFCUser U WITH(NOLOCK)
			ON h.CFCUserID = U.CFCUserID
				
WHERE 
	H.UpdateIngredientHeaderID = @UpdateIngredientHeaderID
	AND H.Status = ISNULL(@Status, H.Status)
	AND H.ExportTargetID = ISNULL(@ExportTargetID, H.ExportTargetID)

END