CREATE PROCEDURE [dbo].[usp_AIMSupplierRefAnalyzerSupplier_SAV]
			  @AIMSupplierRefAnalyzerSupplierID			INT
			, @AIMSupplierID							INT
			, @AnalyzerSupplierID						INT
			, @CFCUserID								INT
			, @DataSourceTypeID							INT 

-- exec usp_AIMSupplierRefAnalyzerSupplier_SAV 12, 18, 4, 2, 9

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''
DECLARE @Count			INT = 0


--4/17/18
DECLARE @LogStatus		CHAR = ''

BEGIN
IF @AIMSupplierRefAnalyzerSupplierID = 0	
	BEGIN
		-- 4/17/18 - allow user to assign more the same ANALYZER supplier/datasource to different AIM supplier (so, multiple cross-refs for an analyzer supplier/datasource). 		
		IF EXISTS (SELECT 
						XREF.AnalyzerSupplierID 
					FROM 
						dbo.AIMSupplierRefAnalyzerSupplier XREF 
					LEFT OUTER JOIN dbo.AnalyzerSupplier AM 
						ON XRef.AnalyzerSupplierID = AM.AnalyzerSupplierID
					WHERE AM.DataSourceTypeID = @DataSourceTypeID 
							and XRef.AIMSupplierID = @AIMSupplierID 
							and XRef.AnalyzerSupplierID = @AnalyzerSupplierID)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'More than one cross-reference for this AIM Supplier and Analyzer Supplier/DataSource is not allowed. Information is not saved!'
			END
		ELSE
		-- 7/14/17 - commented
		----IF EXISTS (
		----	SELECT 
		----		XREF.AnalyzerSupplierID 
		----	FROM
		----		dbo.AIMSupplierRefAnalyzerSupplier XREF
		----	LEFT OUTER JOIN dbo.AIMSource A 
		----		ON XREF.AIMSupplierID = A.AIMSourceID
		----	LEFT OUTER JOIN	dbo.AnalyzerSupplier N	
		----		ON XREF.AnalyzerSupplierID = N.AnalyzerSupplierID
		----	WHERE
		----		XREF.AIMSupplierID = @AIMSupplierID and N.DataSourceTypeID = @DataSourceTypeID)
	
		----		BEGIN
		----			SET @ErrorNumber = 1
		----			SET @ErrorMessage = 'More than one cross-reference for this AIM Supplier and DataSource is not allowed. Information is not saved!'
		----		END
		----IF @ErrorNumber = 0
				
		
			
			BEGIN
				SET @LogStatus = 'A'
				INSERT
					dbo.AIMSupplierRefAnalyzerSupplier 
					(
						AIMSupplierID
					, AnalyzerSupplierID
					, UpdateStamp
					, CFCUserID
					)
			VALUES
					(
						@AIMSupplierID
					, @AnalyzerSupplierID
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
			dbo.AIMSupplierRefAnalyzerSupplier WITH(ROWLOCK)
				SET 
					AIMSupplierID = ISNULL(@AIMSupplierID, AIMSupplierID), 
					AnalyzerSupplierID = ISNULL(@AnalyzerSupplierID, AnalyzerSupplierID)
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
				WHERE 
					AIMSupplierRefAnalyzerSupplierID = @AIMSupplierRefAnalyzerSupplierID
		SET @RecordID = @AIMSupplierRefAnalyzerSupplierID
	END
END



--4/17/18 ----------------------------------------
IF  @ErrorNumber = 0
	Begin
		INSERT INTO AIMSupplierRefAnalyzerSupplierLog (AIMSupplierID, AnalyzerSupplierID, LogStatus, UpdateStamp, CFCUserID)
		SELECT	    @AIMSupplierID, @AnalyzerSupplierID, @LogStatus, Getdate(), @CFCUserID 
	End
--------------------------------------------------
		

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END