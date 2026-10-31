CREATE PROCEDURE [dbo].[usp_AnalyzerNutrient_SAV_AnalyzerNutrientID]
				@AnalyzerNutrientID					INT
			, @AnalyzerNutrientCode					NVARCHAR(100)	= NULL
			, @AnalyzerNutrientName					NVARCHAR(100)	= NULL	
			, @IsActive								BIT				= NULL	
			, @CFCUserID							INT				= NULL
			, @DataSourceTypeID						INT				= NULL
			, @UnitType								NVARCHAR(15)	= NULL

			--10/25/23 
			--, @ConversionFactor						INT				= NULL			
			, @ConversionFactor						NUMERIC(18,9)	= NULL			
AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @AnalyzerNutrientID = 0
	BEGIN
		IF EXISTS(SELECT 
						AnalyzerNutrientCode 
				FROM 
						dbo.AnalyzerNutrient 
				WHERE 
						AnalyzerNutrientCode = @AnalyzerNutrientCode 
						and DataSourceTypeID = @DataSourceTypeID)

			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Nutrient code already exists. Please enter a different code. Information is not saved!'
			END
		ELSE
			BEGIN
				INSERT
					dbo.AnalyzerNutrient 
						(
							AnalyzerNutrientCode
						, AnalyzerNutrientName
						, UpdateStamp
						, CFCUserID
						, DataSourceTypeID
						, UnitType
						, ConversionFactor
						
						)
				VALUES
						(
							@AnalyzerNutrientCode
						, @AnalyzerNutrientName
						, GETDATE()
						, @CFCUserID
						, @DataSourceTypeID
						, @UnitType
						, @ConversionFactor
						
						)
			END
	END
ELSE
	BEGIN
		IF @IsActive = 0
			BEGIN
				SET @IDinOtherTable =
				(
					SELECT 
						XREF.AnalyzerNutrientID
					FROM
						dbo.AIMNutrientRefAnalyzerNutrient XREF WITH(NOLOCK)
					WHERE
						XREF.AnalyzerNutrientID = @AnalyzerNutrientID
						
				)
				-- Cross-references are no longer checked/deleted because Nutrient is never "deleted", just inactivated.
				--IF @IDinOtherTable IS NOT NULL
				--	BEGIN
				--		SET @ErrorNumber = 1
				--		SET @ErrorMessage = 'Unable to delete nutrient because it is being used in the AIM/Analyzer nutrient cross reference table.'						
				--	END
			END

		IF @ErrorNumber = 0
			BEGIN
				UPDATE
					dbo.AnalyzerNutrient WITH(ROWLOCK)
						SET 
								AnalyzerNutrientCode = ISNULL(@AnalyzerNutrientCode, AnalyzerNutrientCode)
							, AnalyzerNutrientName = ISNULL(@AnalyzerNutrientName, AnalyzerNutrientName)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
							, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
							, UnitType = ISNULL(@UnitType, UnitType)
							, ConversionFactor = ISNULL(@ConversionFactor, ConversionFactor)							
							
						WHERE 
							AnalyzerNutrientID = @AnalyzerNutrientID
			END
	END

SELECT 
		@@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
