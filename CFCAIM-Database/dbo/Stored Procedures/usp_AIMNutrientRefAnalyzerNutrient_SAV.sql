CREATE PROCEDURE [dbo].[usp_AIMNutrientRefAnalyzerNutrient_SAV]
			  @AIMNutrientRefAnalyzerNutrientID			INT
			, @AIMNutrientID							INT
			, @AnalyzerNutrientID						INT
			, @CFCUserID								INT
			, @DataSourceTypeID							INT 

-- exec usp_AIMNutrientRefAnalyzerNutrient_SAV 0, 6, 3, 2, 1

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

--4/17/18
DECLARE @LogStatus		CHAR = ''

IF @AIMNutrientRefAnalyzerNutrientID = 0
	BEGIN
			
		-- 7/14/17 - commented here. ADDED WARNING IN CODE
		--IF EXISTS (SELECT 
		--			XREF.AnalyzerNutrientID 
		--		FROM
		--			dbo.AIMNutrientRefAnalyzerNutrient XREF
		--		LEFT OUTER JOIN dbo.AIMNutrient A 
		--			ON XREF.AIMNutrientID = A.AIMNutrientID
		--		LEFT OUTER JOIN	dbo.AnalyzerNutrient N	
		--			ON XREF.AnalyzerNutrientID = N.AnalyzerNutrientID
		--		WHERE
		--			XREF.AIMNutrientID = @AIMNutrientID and N.DataSourceTypeID = @DataSourceTypeID)
		--	BEGIN
		--		SET @ErrorNumber = 1
		--		SET @ErrorMessage = 'More than one cross-reference for this AIM Nutrient and DataSource is not allowed. Information is not saved!'
		--	END
		--ELSE

			BEGIN
				SET @LogStatus = 'A'
				INSERT
				dbo.AIMNutrientRefAnalyzerNutrient 
					(
					  AIMNutrientID
					, AnalyzerNutrientID
					, UpdateStamp
					, CFCUserID
					)
				VALUES
					(
					  @AIMNutrientID
					, @AnalyzerNutrientID
					, GETDATE()
					, @CFCUserID
					)
				SET @RecordID = @@IDENTITY
			END
	END
ELSE
	BEGIN
		IF @ErrorNumber = 0
			BEGIN
				SET @LogStatus = 'C'
				UPDATE
					dbo.AIMNutrientRefAnalyzerNutrient WITH(ROWLOCK)
						SET 
							--7/14/17 - next line was commented!? Now it's uncommented
							  AIMNutrientID = ISNULL(@AIMNutrientID, AIMNutrientID), 
							  AnalyzerNutrientID = ISNULL(@AnalyzerNutrientID, AnalyzerNutrientID)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
						WHERE 
							AIMNutrientRefAnalyzerNutrientID = @AIMNutrientRefAnalyzerNutrientID
				SET @RecordID = @AIMNutrientRefAnalyzerNutrientID
			END
	END

--4/17/18 ----------------------------------------
If @ErrorNumber = 0
	Begin
		INSERT INTO AIMNutrientRefAnalyzerNutrientLog (AIMNutrientID, AnalyzerNutrientID, LogStatus, UpdateStamp, CFCUserID)
		SELECT	    @AIMNutrientID, @AnalyzerNutrientID, @LogStatus, Getdate(), @CFCUserID 
	End
--------------------------------------------------

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END