

CREATE PROCEDURE [dbo].[usp_SystemSettings_SEL]
				
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  --SS.*
	  SS.SystemSettingsID
	  , SS.AIMNutrientID	  
	  , SS.AllowAIMInactive
	  , SS.ShowAnalyzerMaterials 
	  , SS.SampleAimNeedsRebuild 
	  , SS.SampleNumberOfDays 
	  , SS.ShowNameFirstInDropdown
	  , SS.AutoGenerateLotNumber
	  , SS.NextLotNumber	 
	  , SS.UpdateStamp
	  , U.CFCUserID
	  , SS.LabelSize
	  , SS.FurtherAnalysisDefaultID
	  , SS.GuaranteeReportDecimals
	  , SS.GroundUnGroundDefaultID
	  , U.UserName
	  , SS.DryMatterAIMNutrientID
	  , SS.AnalysisFilteredDataDefault				--10/19/23
	  , SS.AutoFillSampleTestTypeIncludeBox			--10/19/23
	  , SS.ShowFurtherAnalysis						--10/22/24 cm
	  , SS.IsVisibleFormulationXrefMessage			--11/14/24 cm
	  , SS.IsVisibleFurtherAnalysisXrefMessage		--11/14/24 cm
	  , SS.isAllowMenuPopout						--Task1102 DB250926
	  , SS.IsPrioritizeGroupTemplate				--Task1061 DB251029
	  , SS.SetSampleDateToToday						--Task1118 DB251203
	  , SS.ClearFieldsOnSampleScreen				--Task1087 DB251205
	
FROM 
	dbo.SystemSettings SS WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SS.CFCUserID = U.CFCUserID

END