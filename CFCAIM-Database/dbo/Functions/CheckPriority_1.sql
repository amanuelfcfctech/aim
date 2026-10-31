CREATE FUNCTION [dbo].[CheckPriority]
(
	   @SampleID			INT
	 , @AimNutrientID		INT
	 , @DataSourceTypeID	INT
	 , @AimDatabaseID		INT
	 , @AnalyzerNutrientID	INT			--2/7/24
	
)
RETURNS INT
AS
BEGIN
	
	-- Using AimNutrientID, get Priority (First In, Last In, Select Datasource) 
	--				and Data Source (Foss, Adisseo, etc.) from AimNutrient
	DECLARE @Priority INT
	DECLARE @NutrientDataSource INT
	DECLARE @SampleAimExists INT = 0
	DECLARE @Insert_sw INT = 0
	DECLARE @SampleAIMID	INT = 0					--10/24/23
	--DECLARE @AnalyzerNutrientID	INT = 0			--2/7/24 - commented		
	
	--select dbo.CheckPriority(35346, 71, 10003, 1, 81) 		

	SET @Priority = (Select top 1 AN.PriorityAIMUsedValue from dbo.AIMNutrient AN with (nolock) where AN.AIMNutrientID = @AimNutrientID)				
	SET @SampleAimExists = (Select top 1 SA.SampleID from dbo.SampleAIM SA where SA.SampleID = @SampleID and SA.AIMNutrientID = @AimNutrientID)					
	--SET @AnalyzerNutrientID = (Select top 1 AnalyzerNutrientID from dbo.AIMNutrientRefAnalyzerNutrient where AimNutrientID = @AimNutrientID)					    --2/7/24 commented	
	--SET @NutrientDataSource = (Select top 1 AN.DataSourceTypeID from dbo.AnalyzerNutrient AN with (nolock) where AN.AnalyzerNutrientID = @AnalyzerNutrientID)	    --2/7/24 commented
	SET @NutrientDataSource = (Select top 1 AN.DataSourceTypeID from dbo.AIMNutrient AN with (nolock) where AN.AIMNutrientID = @AimNutrientID)						--2/7/24	
	SET @SampleAIMID = (Select top 1 SA.SampleAIMID from dbo.SampleResult SR Inner Join dbo.SampleAIM SA ON SA.SampleResultID = SR.SampleResultID		
						where SR.SampleID = @SampleID AND SR.AnalyzerNutrientID = @AnalyzerNutrientID AND SR.IsActive = 0)

	if @Priority IN (0,1)
		Begin
			SET @Priority = (Select AD.AIMPriorityUsedValueID from dbo.AIMDatabase AD where AD.AIMDatabaseID = @AimDatabaseID)
		End		
		
	-- 0 = Null
	-- 1 = Default
	-- 2 = First In
	-- 3 = Last In
	-- 4 = Select Datasource

	IF (@SampleAimExists = 0 or @SampleAimExists IS NULL) and (@Priority IN (2,3))
		SET @Insert_sw = 1
	ELSE IF (@SampleAimExists = 0 or @SampleAimExists IS NULL) and @Priority = 4 and @DataSourceTypeID = @NutrientDataSource 
		SET @Insert_sw = 1
	ELSE IF	@SampleAimExists > 0 and @Priority = 2 AND @SampleAIMID > 0 
		SET @Insert_sw = 1
	ELSE IF @SampleAimExists > 0 and @Priority = 3	
		SET @Insert_Sw = 1
	ELSE IF @SampleAimExists > 0 and @Priority = 4 and @DataSourceTypeID = @NutrientDataSource 
		SET @Insert_Sw = 1
		
	-- Return the result of the function
	RETURN @Insert_Sw

END