CREATE PROCEDURE [dbo].[usp_AIMSource_INS_GenerateFromUser]
			  @AnalyzerSupplierID				INT
			, @CFCUserID						INT = NULL
						
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber		INT = 0
DECLARE @ErrorMessage		VARCHAR(200) = ''
DECLARE @ReturnIntegerValue	INT = 0
DECLARE @AIMSupplierID		INT	

IF EXISTS(SELECT 
				AIMSourceID 	
			FROM 
				dbo.AnalyzerSupplier ANS
				INNER JOIN
					dbo.AIMSource A
						ON A.SourceName = ANS.AnalyzerSupplierName 
			WHERE 
				ANS.AnalyzerSupplierID = @AnalyzerSupplierID)
	BEGIN
		SET @ErrorMessage = 'Supplier Code ready exists'			
		SET @ErrorNumber = 1
		SET @ReturnIntegerValue = (SELECT TOP 1	AIMSourceID 	
									FROM 
										dbo.AnalyzerSupplier ANS
									INNER JOIN
										dbo.AIMSource A
											ON A.SourceName = ANS.AnalyzerSupplierName 
									WHERE 
										ANS.AnalyzerSupplierID = @AnalyzerSupplierID)
	END
ELSE
	BEGIN
		INSERT INTO 
			dbo.AIMSource 
				(
					  SourceName  
					, IsActive 
					, UpdateStamp 
					, CFCUserID 
				)
		SELECT 
			  AnalyzerSupplierName
			, 1
			, GETDATE()
			, @CFCUserID
		FROM 
			dbo.AnalyzerSupplier ANS
		WHERE 
			ANS.AnalyzerSupplierID = @AnalyzerSupplierID

		SET @AIMSupplierID = @@IDENTITY

		IF @AIMSupplierID > 0 
			BEGIN
				INSERT INTO	
					dbo.AIMSupplierRefAnalyzerSupplier 
						(
							  AnalyzerSupplierID
							, AIMSupplierID 							
							, UpdateStamp
							, CFCUserID
						)
				VALUES
					(
						  @AnalyzerSupplierID
						, @AIMSupplierID					
						, GETDATE()
						, @CFCUserID
					)
			END
		ELSE
			BEGIN
				SET @ErrorMessage = 'Supplier Code was not found'
				SET @ErrorNumber = 2
			END
	END
SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @ReturnIntegerValue AS ReturnIntegerValue 

END