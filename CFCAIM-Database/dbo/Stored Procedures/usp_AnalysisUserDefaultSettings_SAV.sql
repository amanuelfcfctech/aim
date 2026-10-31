CREATE PROCEDURE [dbo].[usp_AnalysisUserDefaultSettings_SAV]
			  
			@CFCUserID						INT
			, @GroupByDate					BIT	
			, @GroupBySampleID				BIT	
			, @GroupByOwner					BIT	
			, @GroupByOwnerLocation			BIT	
			, @GroupBySupplier				BIT	
			, @GroupBySupplierLocation		BIT	
			, @AllRecords					BIT	
			, @OutliersOnly					BIT
			, @ExtremeOutlierInclude		BIT
			, @ExtremeOutlierExclude		BIT
			, @MildOutlierInclude			BIT
			, @MildOutlierExclude			BIT
			, @TargetBase					BIT
			, @TargetDefaultOverlay			BIT
			, @TargetSpecificOverlay		BIT
			, @TargetSpecificOverlayPlantID INT
			, @TemplateQuery				INT
			, @TemplateView					INT
			, @RecalculateOnModify			BIT
			, @IngredientOrProduct			NVARCHAR(1)

-- exec usp_AnalysisUserDefaultSettings_SAV 1, 1,0,0,0,0,0, 1,0, 0,1,1,0, 1,0,0,0, 0,0,0,'P'

AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''


IF EXISTS(
			SELECT
				TOP 1 A.*
			FROM
				dbo.AnalysisUserDefaultSettings A
			WHERE
					A.CFCUserID = @CFCUserID )
		BEGIN
			

			
			UPDATE 
			dbo.AnalysisUserDefaultSettings 
				SET 						  				
						  GroupByDate = IsNULL(@GroupByDate, GroupByDate)
						, GroupBySampleID = IsNull(@GroupBySampleID, GroupBySampleID)
						, GroupByOwner = ISNull(@GroupByOwner, GroupByOwner)
						, GroupByOwnerLocation = IsNull (@GroupByOwnerLocation, GroupByOwnerLocation)
						, GroupBySupplier = IsNull(@GroupBySupplier, GroupBySupplier)
						, GroupBySupplierLocation = IsNull(@GroupBySupplierLocation, GroupBySupplierLocation)
						, AllRecords = IsNull(@AllRecords, AllRecords)
						, OutliersOnly = IsNull(@OutliersOnly, OutliersOnly)
						, ExtremeOutlierInclude = IsNull(@ExtremeOutlierInclude, ExtremeOutlierInclude)
						, ExtremeOutlierExclude = IsNull(@ExtremeOutlierExclude, ExtremeOutlierExclude)
						, MildOutlierInclude = IsNull(@MildOutlierInclude, MildOutlierInclude)
						, MildOutlierExclude = IsNull(@MildOutlierExclude, MildOutlierExclude)
						, TargetBase = IsNull(@TargetBase, TargetBase)
						, TargetDefaultOverlay = IsNull(@TargetDefaultOverlay, TargetDefaultOverlay)
						, TargetSpecificOverlay = IsNull(@TargetSpecificOverlay, TargetSpecificOverlay)
						, TargetSpecificOverlayPlantID = IsNull(@TargetSpecificOverlayPlantID, TargetSpecificOverlayPlantID)
						, TemplateQuery = IsNull(@TemplateQuery, TemplateQuery)
						, TemplateView = IsNull(@TemplateView, TemplateView)
						, RecalculateOnModify = IsNull(@RecalculateOnModify, RecalculateOnModify)
						, IngredientOrProduct = ISNULL(@IngredientOrProduct, IngredientOrProduct)
						, UpdateStamp = GETDATE()
					
					WHERE 
						CFCUserID = @CFCUserID

				SET @RecordID = @@IDENTITY 
			
		END
	ELSE
		BEGIN
			
			INSERT INTO
	
				dbo.AnalysisUserDefaultSettings 
					(
						  CFCUserID
					    , GroupByDate
						, GroupBySampleID
						, GroupByOwner
						, GroupByOwnerLocation
						, GroupBySupplier
						, GroupBySupplierLocation
						, AllRecords
						, OutliersOnly
						, ExtremeOutlierInclude
						, ExtremeOutlierExclude
						, MildOutlierInclude
						, MildOutlierExclude
						, TargetBase
						, TargetDefaultOverlay
						, TargetSpecificOverlay
						, TargetSpecificOverlayPlantID
						, TemplateQuery
						, TemplateView
						, RecalculateOnModify	
						, IngredientOrProduct
						, UpdateStamp		
					)
			VALUES
					(
						  @CFCUserID
					    , @GroupByDate
						, @GroupBySampleID
						, @GroupByOwner
						, @GroupByOwnerLocation
						, @GroupBySupplier
						, @GroupBySupplierLocation
						, @AllRecords
						, @OutliersOnly
						, @ExtremeOutlierInclude
						, @ExtremeOutlierExclude
						, @MildOutlierInclude
						, @MildOutlierExclude
						, @TargetBase
						, @TargetDefaultOverlay
						, @TargetSpecificOverlay
						, @TargetSpecificOverlayPlantID
						, @TemplateQuery
						, @TemplateView
						, @RecalculateOnModify	
						, @IngredientOrProduct
						, GETDATE()
					)
			SET @RecordID = @@IDENTITY 
			
		END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END