CREATE PROCEDURE [dbo].[usp_SystemSettings_SAV]
			  @SystemSettingsID				INT
			, @AIMNutrientID				INT = NULL			
			, @CFCUserID					INT
			, @AllowAIMInactive				BIT = NULL
			, @ShowAnalyzerMaterials		BIT = NULL
			, @SampleAimNeedsRebuild		BIT = NULL
			, @SampleNumberOfDays			INT = NULL
			, @ShowNameFirstInDropdown		BIT = NULL			
			, @AutoGenerateLotNumber		BIT = NULL			
			, @NextLotNumber				INT = NULL			
			, @LabelSize					INT = NULL				-- 4/14/22 added for Butterball
			, @FurtherAnalysisDefaultID		INT = NULL				-- 8/31/22 added for Butterball
			, @GuaranteeReportDecimals		INT = NULL				-- 11/21/22 added for Versova
			, @GroundUnGroundDefaultID		INT = NULL				-- 11/21/22 added for Versova
			, @DryMatterAIMNutrientID		INT = NULL				-- 1/3/23 added for Versova (SupplierGuarantee report)
			, @AnalysisFilteredDataDefault	BIT = NULL				-- 10/19/23 added for Maschhoff
			, @AutoFillSampleTestTypeIncludeBox BIT = NULL			-- 10/19/23 Maschhoff
			

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @SystemSettingsID = 0 
	BEGIN
		IF EXISTS(SELECT 
					@SystemSettingsID 
				FROM 
					dbo.SystemSettings SS
				WHERE 
						SS.SystemSettingsID = @SystemSettingsID )
					
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This setting already exists. Information is not saved.'
			END
		ELSE
			BEGIN
				INSERT
					dbo.SystemSettings
						(
						  AIMNutrientID						
						, UpdateStamp
						, CFCUserID
						, AllowAIMInactive	
						, ShowAnalyzerMaterials
						, SampleAimNeedsRebuild		
						, SampleNumberOfDays
						, ShowNameFirstInDropdown	
						, AutoGenerateLotNumber		
						, NextLotNumber	
						, LabelSize
						, FurtherAnalysisDefaultID	
						, GuaranteeReportDecimals
						, GroundUnGroundDefaultID
						, DryMatterAIMNutrientID
						, AnalysisFilteredDataDefault						
						, AutoFillSampleTestTypeIncludeBox
						)
				VALUES
						(
						  @AIMNutrientID				
						, GETDATE()
						, @CFCUserID
						, @AllowAIMInactive	
						, @ShowAnalyzerMaterials	
						, @SampleAimNeedsRebuild		
						, @SampleNumberOfDays
						, @ShowNameFirstInDropdown	
						, @AutoGenerateLotNumber
						, @NextLotNumber
						, @LabelSize
						, @FurtherAnalysisDefaultID	
						, @GuaranteeReportDecimals
						, @GroundUnGroundDefaultID
						, @DryMatterAIMNutrientID
						, @AnalysisFilteredDataDefault
						, @AutoFillSampleTestTypeIncludeBox
						)
				SET @RecordID = @@IDENTITY
			END
		END
ELSE
	BEGIN	
		
		UPDATE
			dbo.SystemSettings WITH(ROWLOCK)
				SET 
					  AIMNutrientID = ISNULL(@AIMNutrientID, AIMNutrientID)					
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
					, AllowAIMInactive = ISNULL(@AllowAIMInactive, AllowAIMInactive)
					, ShowAnalyzerMaterials = ISNULL(@ShowAnalyzerMaterials, ShowAnalyzerMaterials)
					, SampleAimNeedsRebuild = ISNULL(@SampleAimNeedsRebuild, SampleAimNeedsRebuild)
					, SampleNumberOfDays = ISNULL(@SampleNumberOfDays, SampleNumberOfDays)	
					, ShowNameFirstInDropdown = ISNULL(@ShowNameFirstInDropdown, ShowNameFirstInDropdown)
					, AutoGenerateLotNumber = ISNULL(@AutoGenerateLotNumber, AutoGenerateLotNumber)
					, NextLotNumber = ISNULL(@NextLotNumber, NextLotNumber)
					, LabelSize = ISNULL(@LabelSize, LabelSize)
					, FurtherAnalysisDefaultID = ISNULL(@FurtherAnalysisDefaultID, FurtherAnalysisDefaultID)
					, GuaranteeReportDecimals = ISNULL(@GuaranteeReportDecimals, GuaranteeReportDecimals)
					, GroundUnGroundDefaultID = ISNULL(@GroundUnGroundDefaultID, GroundUnGroundDefaultID)
					, DryMatterAIMNutrientID = ISNULL(@DryMatterAIMNutrientID, DryMatterAIMNutrientID)
					, AnalysisFilteredDataDefault = ISNULL(@AnalysisFilteredDataDefault, AnalysisFilteredDataDefault)
					, AutoFillSampleTestTypeIncludeBox = ISNULL(@AutoFillSampleTestTypeIncludeBox, AutoFillSampleTestTypeIncludeBox)
					
				WHERE 
					SystemSettingsID = @SystemSettingsID

		SET @RecordID = @SystemSettingsID
		
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END