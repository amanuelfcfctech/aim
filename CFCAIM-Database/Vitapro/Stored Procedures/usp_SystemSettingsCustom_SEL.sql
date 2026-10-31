CREATE PROCEDURE [Vitapro].[usp_SystemSettingsCustom_SEL]
				
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  SSC.*

	  , (Select AM.AIMNutrientName from dbo.AIMNutrient AM Where AM.AIMNutrientID = SSC.AIMNutrientID1_ForDisplay) as AIMNutrientName1
	  , (Select AM.AIMNutrientName from dbo.AIMNutrient AM Where AM.AIMNutrientID = SSC.AIMNutrientID2_ForDisplay) as AIMNutrientName2
	  , (Select AM.AIMNutrientName from dbo.AIMNutrient AM Where AM.AIMNutrientID = SSC.AIMNutrientID3_ForDisplay) as AIMNutrientName3
	  , (Select AM.AIMNutrientName from dbo.AIMNutrient AM Where AM.AIMNutrientID = SSC.AIMNutrientID4_ForDisplay) as AIMNutrientName4

	  
	
FROM 
	Vitapro.SystemSettingsCustom SSC WITH(NOLOCK)
	
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SSC.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

END