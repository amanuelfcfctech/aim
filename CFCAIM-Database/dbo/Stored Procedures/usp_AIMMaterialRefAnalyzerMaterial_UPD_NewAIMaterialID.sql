CREATE PROCEDURE [dbo].[usp_AIMMaterialRefAnalyzerMaterial_UPD_NewAIMaterialID]
			  @AnalyzerMaterialID						INT			
			, @NewAnalyzerMaterialID					INT			
			, @CFCUserID								INT
			, @RemoveAnalyzerMaterial					BIT

AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''
DECLARE @LogStatus						CHAR = ''
DECLARE @ID								INT = 0
DECLARE @AIMMaterialID				    INT = 0


DECLARE @ProcName nvarchar(128);
SET @ProcName = OBJECT_NAME(@@PROCID);  


---------------------------------------------------------------------------------------------
-- GET ALL RECORDS FOR OLD ANALYZER MATERIAL
DECLARE TempCur scroll cursor for
SELECT 
	X.AIMMaterialRefAnalyzerMaterialID, X.AIMMaterialID
FROM 
	dbo.AIMMaterialRefAnalyzerMaterial X 
WHERE
	X.AnalyzerMaterialID = @AnalyzerMaterialID
OPEN TempCur
FETCH NEXT FROM TempCur INTO @ID, @AIMMaterialID



---------------------------------------------------------------------------------------------
-- NO CROSS-REFERENCES EXIST FOR OLD ANALYZER MATERIAL, SO DELETE MATERIAL
IF @@FETCH_STATUS <> 0
	BEGIN		
		if @RemoveAnalyzerMaterial = 1
			Begin
				-- UPDATE ANALYZER TABLE - delete material
				DELETE FROM 
					dbo.AnalyzerMaterial with (rowlock)				
				WHERE
					AnalyzerMaterialID = @AnalyzerMaterialID

				---------------------------------------------------------------------------
				-- INSERT LOG
				SET @LogStatus = 'D'
				INSERT INTO AIMMaterialRefAnalyzerMaterialLog (
					  AIMMaterialID	
					, AnalyzerMaterialID 												
					, LogStatus
					, UpdateStamp
					, CFCUserID
					, Comment)
				SELECT	    
					@AIMMaterialID
					, @AnalyzerMaterialID					
					, @LogStatus
					, Getdate()
					, @CFCUserID
					, @ProcName
			End
	END


---------------------------------------------------------------------------------------------
-- CROSS-REFERENCES EXIST 
--ELSE
	WHILE @@FETCH_STATUS = 0
	Begin

		-- CHECK FOR ANY EXISTING CROSS-REFERENCE FOR "OLD" MATERIAL WITH SPECIFIC AIM MATERIAL		
	    IF Exists (Select XREF.AIMMaterialRefAnalyzerMaterialID
					FROM
						dbo.AIMMaterialRefAnalyzerMaterial XREF
					WHERE
						XREF.AnalyzerMaterialID = @AnalyzerMaterialID AND
						XREF.AIMMaterialID = @AIMMaterialID   )

			-- X-REF EXISTS
			BEGIN

				-- IF THERE ARE NO X-REFERENCES ALREADY SET UP FOR "NEW" MATERIAL FOR THE AIM MATERIAL, 
				-- UPDATE THE EXISTING CROSS-REFERENCE ANALYZER MATERIAL ID'S.
				IF NOT EXISTS (SELECT 
						XREF.AIMMaterialRefAnalyzerMaterialID
					FROM
						dbo.AIMMaterialRefAnalyzerMaterial XREF
					WHERE
						XREF.AnalyzerMaterialID = @NewAnalyzerMaterialID  AND						
						XREF.AIMMaterialID = @AIMMaterialID	  )
					BEGIN				

						--------------------------------------------------------------------
						-- UPDATE CROSS-REFERENCE TABLE					
						UPDATE
							dbo.AIMMaterialRefAnalyzerMaterial WITH(ROWLOCK)
						SET 
							  AnalyzerMaterialID = @NewAnalyzerMaterialID								
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
						WHERE 
							AIMMaterialRefAnalyzerMaterialID = @ID

						---------------------------------------------------------------------------
						-- INSERT LOG
						SET @LogStatus = 'C'
						INSERT INTO AIMMaterialRefAnalyzerMaterialLog (
							  AnalyzerMaterialID							
							, AIMMaterialID							
							, OLDanalyzerMaterialID					
							, LogStatus
							, UpdateStamp
							, CFCUserID
							, Comment)
						SELECT	
							 @NewAnalyzerMaterialID							
							, @AIMMaterialID							
							, @AnalyzerMaterialID					 
							, @LogStatus
							, Getdate()
							, @CFCUserID 
							, @ProcName

						---------------------------------------------------------------------------
						-- UPDATE ANALYZER TABLE - delete material
						DELETE FROM 
							dbo.AnalyzerMaterial with (rowlock)				
						WHERE
							AnalyzerMaterialID = @AnalyzerMaterialID
						---------------------------------------------------------------------------
						-- INSERT LOG
						SET @LogStatus = 'D'
						INSERT INTO AIMMaterialRefAnalyzerMaterialLog (
							  AIMMaterialID	
							, AnalyzerMaterialID 												
							, LogStatus
							, UpdateStamp
							, CFCUserID
							, Comment)
						SELECT	    
							@AIMMaterialID
							, @AnalyzerMaterialID					
							, @LogStatus
							, Getdate()
							, @CFCUserID
							, @ProcName
					END									
			END
		ELSE
			BEGIN

				-- No Cross-References exist for old analyzer material, so delete material
				If @RemoveAnalyzerMaterial = 1
					Begin
						-- UPDATE ANALYZER TABLE - delete material
						DELETE FROM 
							dbo.AnalyzerMaterial with (rowlock)				
						WHERE
							AnalyzerMaterialID = @AnalyzerMaterialID
						---------------------------------------------------------------------------
						-- INSERT LOG
						SET @LogStatus = 'D'
						INSERT INTO AIMMaterialRefAnalyzerMaterialLog (
							AnalyzerMaterialID													
							, LogStatus
							, UpdateStamp
							, CFCUserID
							, Comment)
						SELECT	    
							@AnalyzerMaterialID				
							, @LogStatus
							, Getdate()
							, @CFCUserID
							, @ProcName
					End
			END

		FETCH NEXT FROM TempCur INTO @ID, @AIMMaterialID 
	End		


CLOSE TempCur
DEALLOCATE TempCur


SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END