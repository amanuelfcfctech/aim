CREATE PROCEDURE [dbo].[usp_AIMSupplierLocationRefAnalyzerSupplierLocation_UPD_NewAIMSupplierID_NewAIMSupplierLocation]
			  @AIMSupplierID							INT
			, @AIMSupplierLocationID					INT
			, @NewAIMSupplierID							INT
			, @NewAIMSupplierLocationID					INT						
			, @CFCUserID								INT
			, @RemoveSupplierLocation					BIT

-- exec dbo.usp_AIMSupplierLocationRefAnalyzerSupplierLocation_UPD_NewAIMSupplierID_NewAIMSupplierLocation 4, 4, 5, 5, 1

AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''
DECLARE @LogStatus						CHAR = ''
DECLARE @ID								INT = 0
DECLARE @AnalyzerSupplierID				INT = 0
DECLARE @AnalyzerSupplierLocationID		INT = 0

DECLARE @ProcName nvarchar(128);
SET @ProcName = OBJECT_NAME(@@PROCID);  


---------------------------------------------------------------------------------------------
-- GET ALL RECORDS FOR OLD AIM SUPPLIER AND SUPPLIER-LOCATION
DECLARE TempCur scroll cursor for
SELECT 
	X.AIMSupplierLocationRefAnalyzerSupplierLocationID, X.AnalyzerSupplierID, X.AnalyzerSupplierLocationID 
FROM 
	dbo.AIMSupplierLocationRefAnalyzerSupplierLocation X 
WHERE
	X.AIMSupplierID = @AIMSupplierID and X.AIMSupplierLocationID = @AIMSupplierLocationID
OPEN TempCur
FETCH NEXT FROM TempCur INTO @ID, @AnalyzerSupplierID, @AnalyzerSupplierLocationID


---------------------------------------------------------------------------------------------
-- NO CROSS-REFERENCES EXIST FOR OLD SUPPLIER/SUPPLIER-LOCATION, SO DELETE SUPPLIER-LOCATION
IF @@FETCH_STATUS <> 0
	BEGIN		
		if @RemoveSupplierLocation = 1
			Begin
				DELETE FROM 
					dbo.AIMSourceLocation with (rowlock)				
				WHERE
					AIMSourceID = @AIMSupplierID and AIMSourceLocationID = @AIMSupplierLocationID

				---------------------------------------------------------------------------
				-- INSERT LOG
				SET @LogStatus = 'D'
				INSERT INTO AIMSupplierLocationRefAnalyzerSupplierLocationLog (
					AIMSupplierLocationID
					, AIMSupplierID													
					, LogStatus
					, UpdateStamp
					, CFCUserID
					, Comment)
				SELECT	    
					@AIMSupplierLocationID
					, @AIMSupplierID					
					, @LogStatus
					, Getdate()
					, @CFCUserID
					, @ProcName
			End
	END


---------------------------------------------------------------------------------------------
-- CROSS-REFERENCES EXIST 
ELSE
	WHILE @@FETCH_STATUS = 0
	Begin

		-- CHECK FOR ANY EXISTING CROSS-REFERENCE FOR "OLD" SUPPLIER/SUPPLIER-LOCATION WITH SPECIFIC ANALYZER SUPPLIER/SUPPLIER-LOCATION		
	    IF Exists (Select XREF.AIMSupplierLocationRefAnalyzerSupplierLocationID
					FROM
						dbo.AIMSupplierLocationRefAnalyzerSupplierLocation XREF
					WHERE
						XREF.AIMSupplierID = @AIMSupplierID  AND
						XREF.AIMSupplierLocationID = @AIMSupplierLocationID AND
						XREF.AnalyzerSupplierID = @AnalyzerSupplierID AND
						XREF.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID  )

			-- X-REF EXISTS
			BEGIN

				-- IF THERE ARE NO X-REFERENCES ALREADY SET UP FOR "NEW" SUPPLIER/SUPPLIER-LOCATION FOR THE ANALYZER SUPPLIER/LOC, THEN 
				-- UPDATE THE EXISTING CROSS-REFERENCE AIM SUPPLIER/LOCATION ID'S.
				IF NOT EXISTS (SELECT 
						XREF.AIMSupplierLocationRefAnalyzerSupplierLocationID
					FROM
						dbo.AIMSupplierLocationRefAnalyzerSupplierLocation XREF
					WHERE
						XREF.AIMSupplierID = @NewAIMSupplierID  AND
						XREF.AIMSupplierLocationID = @NewAIMSupplierLocationID AND
						XREF.AnalyzerSupplierID = @AnalyzerSupplierID AND
						XREF.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID  
						)
					BEGIN				

						--------------------------------------------------------------------
						-- UPDATE CROSS-REFERENCE TABLE					
						UPDATE
							dbo.AIMSupplierLocationRefAnalyzerSupplierLocation WITH(ROWLOCK)
						SET 
							  AIMSupplierID = @NewAIMSupplierID	
							, AIMSupplierLocationID = @NewAIMSupplierLocationID
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
						WHERE 
							AIMSupplierLocationRefAnalyzerSupplierLocationID = @ID

						---------------------------------------------------------------------------
						-- INSERT LOG
						SET @LogStatus = 'C'
						INSERT INTO AIMSupplierLocationRefAnalyzerSupplierLocationLog (
							  AIMSupplierLocationID
							, AIMSupplierID
							, AnalyzerSupplierLocationID					
							, AnalyzerSupplierID
							, OLDaimSupplierLocationID
							, OLDaimSupplierID					
							, LogStatus
							, UpdateStamp
							, CFCUserID
							, Comment)
						SELECT	
							  @NewAIMSupplierLocationID		    
							, @NewAIMSupplierID
							, @AnalyzerSupplierLocationID
							, @AnalyzerSupplierID
							, @AIMSupplierLocationID
							, @AIMSupplierID					 
							, @LogStatus
							, Getdate()
							, @CFCUserID 
							, @ProcName

						---------------------------------------------------------------------------
						-- UPDATE AIM TABLE - delete supplier-location
						DELETE FROM 
							dbo.AIMSourceLocation with (rowlock)										
						WHERE
							AIMSourceID = @AIMSupplierID and AIMSourceLocationID = @AIMSupplierLocationID
						---------------------------------------------------------------------------

						-- INSERT LOG
						SET @LogStatus = 'D'
						INSERT INTO AIMSupplierLocationRefAnalyzerSupplierLocationLog (
							AIMSupplierLocationID
							, AIMSupplierID													
							, LogStatus
							, UpdateStamp
							, CFCUserID
							, Comment)
						SELECT	    
							@AIMSupplierLocationID
							, @AIMSupplierID					
							, @LogStatus
							, Getdate()
							, @CFCUserID
							, @ProcName
					END	
					
					---------------------------------------------------------------------------	
					-- do the same for the aim/analyzer SOURCE cross-reference
					-- IF THERE ARE NO X-REFERENCES ALREADY SET UP FOR "NEW" SUPPLIER FOR THE ANALYZER SUPPLIER, THEN 
					-- UPDATE THE EXISTING CROSS-REFERENCE AIM SUPPLIER ID'S.
					IF NOT EXISTS (SELECT 
							XREF.AIMSupplierRefAnalyzerSupplierID
						FROM
							dbo.AIMSupplierRefAnalyzerSupplier XREF
						WHERE
							XREF.AIMSupplierID = @NewAIMSupplierID  AND						
							XREF.AnalyzerSupplierID = @AnalyzerSupplierID 
							)
						BEGIN				
	
							--------------------------------------------------------------------
							-- UPDATE CROSS-REFERENCE TABLE					
							UPDATE
								dbo.AIMSupplierRefAnalyzerSupplier WITH(ROWLOCK)
							SET 
								AIMSupplierID = @NewAIMSupplierID								
								, UpdateStamp = GETDATE()
								, CFCUserID = @CFCUserID
							WHERE 
								AnalyzerSupplierID = @AnalyzerSupplierID and 
								AIMSupplierID = @AIMSupplierID
	
							---------------------------------------------------------------------------
							-- INSERT LOG
							SET @LogStatus = 'C'
							INSERT INTO AIMSupplierRefAnalyzerSupplierLog (
								AIMSupplierID
								, AnalyzerSupplierID
								, OLDaimSupplierID					
								, LogStatus
								, UpdateStamp
								, CFCUserID
								, Comment)
							SELECT	
								@NewAIMSupplierID
								, @AnalyzerSupplierID
								, @AIMSupplierID					 
								, @LogStatus
								, Getdate()
								, @CFCUserID 
								, @ProcName
							
						END	
						---------------------------------------------------------------------------
													
			END
		ELSE
			BEGIN

				-- No Cross-References exist for old supplier/supplier-location, so delete supplier-location
				If @RemoveSupplierLocation = 1
					Begin
						DELETE FROM
							dbo.AIMSourceLocation with (rowlock)										
						WHERE
							AIMSourceID = @AIMSupplierID and AIMSourceLocationID = @AIMSupplierLocationID
						---------------------------------------------------------------------------
						-- INSERT LOG
						SET @LogStatus = 'D'
						INSERT INTO AIMSupplierLocationRefAnalyzerSupplierLocationLog (
							AIMSupplierLocationID
							, AIMSupplierID													
							, LogStatus
							, UpdateStamp
							, CFCUserID
							, Comment)
						SELECT	    
							@AIMSupplierLocationID
							, @AIMSupplierID					
							, @LogStatus
							, Getdate()
							, @CFCUserID
							, @ProcName
					End
			END

		FETCH NEXT FROM TempCur INTO @ID, @AnalyzerSupplierID, @AnalyzerSupplierLocationID
	End		

CLOSE TempCur
DEALLOCATE TempCur




SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END