CREATE PROCEDURE [dbo].[usp_AIMMaterialRefAnalyzerMaterial_SAV]
			  @AIMMaterialRefAnalyzerMaterialID			INT
			, @AIMMaterialID							INT
			, @AnalyzerMaterialID						INT
			, @CFCUserID								INT
			, @DataSourceTypeID							INT
		

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

-- 4/17/18
DECLARE @LogStatus		CHAR = ''

IF @AIMMaterialRefAnalyzerMaterialID = 0
	BEGIN

		-- 4/17/18 - allow user to assign more the same ANALYZER material/datasource to different AIM materials (so, multiple cross-refs for an analyzer material/datasource). 		
		IF EXISTS (SELECT 
						XREF.AnalyzerMaterialID 
					FROM 
						dbo.AIMMaterialRefAnalyzerMaterial XREF 
					LEFT OUTER JOIN dbo.AnalyzerMaterial AM 
						ON XRef.AnalyzerMaterialID = AM.AnalyzerMaterialID
					WHERE AM.DataSourceTypeID = @DataSourceTypeID 
							and XRef.AIMMaterialID = @AIMMaterialID 
							and XRef.AnalyzerMaterialID = @AnalyzerMaterialID)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'More than one cross-reference for this AIM Material and Analyzer Material/DataSource is not allowed. Information is not saved!'
			END
		ELSE

		-- 7/14/17 - commented here. Added warning to user in code
		-- THIS IS COMMENTED BECAUSE USER IS ALLOWED TO ASSIGN MORE THAN ONE CROSS-REFERENCE FOR AN AIM MATERIAL/DATASOURCE.

		--IF EXISTS (SELECT 
		--				XREF.AnalyzerMaterialID 
		--			FROM
		--				dbo.AIMMaterialRefAnalyzerMaterial XREF
		--			LEFT OUTER JOIN dbo.AIMMaterial A 
		--				ON XREF.AIMMaterialID = A.AIMMaterialID
		--			LEFT OUTER JOIN	dbo.AnalyzerMaterial N	
		--				ON XREF.AnalyzerMaterialID = N.AnalyzerMaterialID
		--			WHERE
		--				XREF.aimMaterialid = @AIMMaterialID and N.DataSourceTypeid = @DataSourceTypeID)
		--	BEGIN
		--		SET @ErrorNumber = 1
		--		SET @ErrorMessage = 'More than one cross-reference for this AIM Material and DataSource is not allowed. Information is not saved!'
		--	END
		--ELSE

			BEGIN
				SET @LogStatus = 'A'						
				INSERT
					dbo.AIMMaterialRefAnalyzerMaterial 
						(
						  AIMMaterialID
						, AnalyzerMaterialID
						, UpdateStamp
						, CFCUserID
						)
				VALUES
						(
						  @AIMMaterialID
						, @AnalyzerMaterialID
						, GETDATE()
						, @CFCUserID
						)
				SET @RecordID = @@IDENTITY

			END
		END
ELSE
	BEGIN
		
		SET @LogStatus = 'C'
		UPDATE
			dbo.AIMMaterialRefAnalyzerMaterial WITH(ROWLOCK)
				SET 
					  AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)
					, AnalyzerMaterialID = ISNULL(@AnalyzerMaterialID, AnalyzerMaterialID)
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
				WHERE 
					AIMMaterialRefAnalyzerMaterialID = @AIMMaterialRefAnalyzerMaterialID
		SET @RecordID = @AIMMaterialRefAnalyzerMaterialID
	END

--4/17/18 ----------------------------------------
IF @ErrorNumber = 0
	Begin
		INSERT INTO AIMMaterialRefAnalyzerMaterialLog (AIMMaterialID, AnalyzerMaterialID, LogStatus, UpdateStamp, CFCUserID)
		SELECT	    @AIMMaterialID, @AnalyzerMaterialID, @LogStatus, Getdate(), @CFCUserID 
	End
--------------------------------------------------

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
