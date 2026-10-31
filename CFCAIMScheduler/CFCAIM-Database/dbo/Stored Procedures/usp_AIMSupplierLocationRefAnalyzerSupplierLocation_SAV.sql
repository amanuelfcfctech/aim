CREATE PROCEDURE [dbo].[usp_AIMSupplierLocationRefAnalyzerSupplierLocation_SAV]
			  @AIMSupplierLocationRefAnalyzerSupplierLocationID			INT
			, @AIMSupplierID											INT
			, @AIMSupplierLocationID									INT
			, @AnalyzerSupplierID										INT
			, @AnalyzerSupplierLocationID								INT
			, @CFCUserID												INT
			, @DataSourceTypeID											INT 

-- exec usp_AIMSupplierLocationRefAnalyzerSupplierLocation_SAV 12, 18, 4, 2, 9

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''
DECLARE @Count			INT = 0

--4/17/18
DECLARE @LogStatus		CHAR = ''

DECLARE @ProcName nvarchar(128);
SET @ProcName = OBJECT_NAME(@@PROCID);  

BEGIN			
IF @AIMSupplierLocationRefAnalyzerSupplierLocationID = 0	
	BEGIN
		-- 4/17/18 - allow user to assign more the same ANALYZER supplier-loc/datasource to different AIM supplier locations (so, multiple cross-refs for an analyzer supplier-loc/datasource). 		
		IF EXISTS (SELECT 
						XREF.AnalyzerSupplierLocationID 
					FROM 
						dbo.AIMSupplierLocationRefAnalyzerSupplierLocation XREF 
					LEFT OUTER JOIN dbo.AnalyzerSupplierLocation ASOL
						ON XRef.AnalyzerSupplierLocationID = ASOL.AnalyzerSupplierLocationID
					WHERE ASOL.DataSourceTypeID = @DataSourceTypeID 
							and XRef.AIMSupplierID = @AIMSupplierID 
							and XRef.AIMSupplierLocationID = @AIMSupplierLocationID
							and XRef.AnalyzerSupplierID = @AnalyzerSupplierID
							and XREF.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'More than one cross-reference for this AIM Supplier Location and Analyzer Supplier Location/DataSource is not allowed. Information is not saved!'
			END
		ELSE
		-- 7/14/17 - commented
		----IF EXISTS (
		----	SELECT 
		----		XREF.AnalyzerSupplierLocationID 
		----	FROM
		----		dbo.AIMSupplierLocationRefAnalyzerSupplierLocation XREF
		----	LEFT OUTER JOIN dbo.AIMSourceLocation A
		----		ON XREF.AIMSupplierID = A.AIMSourceID and 
		----		   XREF.AIMSupplierLocationID = A.AIMSourceLocationID	
		----	LEFT OUTER JOIN	dbo.AnalyzerSupplierLocation N	
		----		ON XREF.AnalyzerSupplierID = N.AnalyzerSupplierID and
		----		   XREF.AnalyzerSupplierLocationID = N.AnalyzerSupplierLocationID
		----	WHERE
		----		XREF.AIMSupplierID = @AIMSupplierID and 
		----		XREF.AIMSupplierLocationID = @AIMSupplierLocationID and 
		----		N.DataSourceTypeID = @DataSourceTypeID)
	
		----		BEGIN
		----			SET @ErrorNumber = 1
		----			SET @ErrorMessage = 'More than one cross-reference for this AIM Supplier Location and DataSource is not allowed. Information is not saved!'
		----		END
		----IF @ErrorNumber = 0

	
			Begin
				SET @LogStatus = 'A'
				INSERT
					dbo.AIMSupplierLocationRefAnalyzerSupplierLocation 
					(
					  AIMSupplierID
					, AIMSupplierLocationID
					, AnalyzerSupplierID
					, AnalyzerSupplierLocationID
					, UpdateStamp
					, CFCUserID					
					)
				VALUES
					(
					  @AIMSupplierID
					, @AIMSupplierLocationID
					, @AnalyzerSupplierID
					, @AnalyzerSupplierLocationID
					, GETDATE()
					, @CFCUserID					
					)
				SET @RecordID = @@IDENTITY		
			End
		END
	ELSE	
		BEGIN
			SET @LogStatus = 'C'
			UPDATE
				dbo.AIMSupplierLocationRefAnalyzerSupplierLocation WITH(ROWLOCK)
					SET 
						AIMSupplierID = ISNULL(@AIMSupplierID, AIMSupplierID), 
						AIMSupplierLocationID = ISNULL(@AIMSupplierLocationID, AIMSupplierLocationID), 
						AnalyzerSupplierID = ISNULL(@AnalyzerSupplierID, AnalyzerSupplierID),
						AnalyzerSupplierLocationID = ISNULL(@AnalyzerSupplierLocationID, AnalyzerSupplierLocationID)
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID
					WHERE 
						AIMSupplierLocationRefAnalyzerSupplierLocationID = @AIMSupplierLocationRefAnalyzerSupplierLocationID
			SET @RecordID = @AIMSupplierLocationRefAnalyzerSupplierLocationID
		END
	END
		
--4/17/18 ----------------------------------------
IF @ErrorNumber = 0
	Begin
		INSERT INTO AIMSupplierLocationRefAnalyzerSupplierLocationLog (AIMSupplierID, AIMSupplierLocationID, AnalyzerSupplierID, AnalyzerSupplierLocationID, LogStatus, UpdateStamp, CFCUserID, Comment)
		SELECT	    @AIMSupplierID, @AIMSupplierLocationID, @AnalyzerSupplierID, @AnalyzerSupplierLocationID, @LogStatus, Getdate(), @CFCUserID, @ProcName
	End
--------------------------------------------------

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END