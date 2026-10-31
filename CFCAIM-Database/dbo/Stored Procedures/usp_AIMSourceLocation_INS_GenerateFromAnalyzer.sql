CREATE PROCEDURE [dbo].[usp_AIMSourceLocation_INS_GenerateFromAnalyzer]
			  @AnalyzerSupplierLocationID		INT
			, @AnalyzerSupplierID				INT
			, @CFCUserID						INT = NULL

						
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber				INT = 0
DECLARE @ErrorMessage				VARCHAR(200) = ''
DECLARE @AIMSourceLocationID		INT	
DECLARE @AIMSourceID				INT	
DECLARE @ReturnIntegerValue			INT
DECLARE @ReturnSecondIntegerValue	INT
DECLARE @ID							INT

IF EXISTS(SELECT 
				AIMSourceLocationID 	
			FROM 
				dbo.AnalyzerSupplierLocation ANSL
				INNER JOIN
					dbo.AIMSourceLocation ASL
						ON ASL.AIMSourceID = ANSL.AnalyzerSupplierID and 
						ASL.SourceLocationName = ANSL.AnalyzerSupplierLocationName 
			WHERE 
				ANSL.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID)
	BEGIN
		SET @ErrorMessage = 'Supplier Location Code already exists'
		SET @ErrorNumber = 1
		SET @ReturnIntegerValue = (SELECT TOP 1 AIMSourceLocationID 	
									FROM 
										dbo.AnalyzerSupplierLocation ANSL
									INNER JOIN
										dbo.AIMSourceLocation ASL
											ON ASL.SourceLocationName = ANSL.AnalyzerSupplierLocationName 
									WHERE 
										ANSL.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID)
		SET @ReturnSecondIntegerValue = (SELECT AIMSourceID 	
									FROM 										
										dbo.AIMSourceLocation ASL											
									WHERE 
										ASL.AIMSourceLocationID = @ReturnIntegerValue)
	END
ELSE
	BEGIN

		SELECT 
			@AIMSourceID = ASRAS.AIMSupplierID  	
		FROM 
			dbo.AnalyzerSupplierLocation ANSL
			INNER JOIN
				dbo.AnalyzerSupplier ANS
					ON ANS.AnalyzerSupplierID = ANSL.AnalyzerSupplierID 
			INNER JOIN 
				dbo.AIMSupplierRefAnalyzerSupplier ASRAS
					ON ASRAS.AnalyzerSupplierID = ANS.AnalyzerSupplierID 
		WHERE 
			ANSL.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID

		IF @AIMSourceID > 0 
			BEGIN							

				INSERT INTO 
					dbo.AIMSourceLocation 
						(
							  AIMSourceID
							, AIMSupplierLocationCode
							, SourceLocationName  
							, IsActive 
							, UpdateStamp 
							, CFCUserID 
						)
				SELECT 
					@AIMSourceID
					, AnalyzerSupplierLocationCode
					, AnalyzerSupplierLocationName
					, 1
					, GETDATE()
					, @CFCUserID
				FROM 
					dbo.AnalyzerSupplierLocation UDO
				WHERE 
					UDO.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID
									
				SET @AIMSourceLocationID = @@IDENTITY
				SET @ReturnIntegerValue = @AIMSourceID

				IF @AIMSourceLocationID > 0 
					BEGIN

						-- make sure parameter is same as what's on table
						SET @ID = (SELECT TOP 1 ANS.AnalyzerSupplierID
						FROM 
							dbo.AnalyzerSupplierLocation ANSL
							INNER JOIN
								dbo.AnalyzerSupplier ANS
									ON ANS.AnalyzerSupplierID = ANSL.AnalyzerSupplierID 
							INNER JOIN 
								dbo.AIMSupplierLocationRefAnalyzerSupplierLocation ASLRAS
									ON ASLRAS.AnalyzerSupplierID = ANS.AnalyzerSupplierID 
						WHERE 
							ANSL.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID)
						IF @ID <> @AnalyzerSupplierID
							SET @AnalyzerSupplierID = @ID

						INSERT INTO	
							dbo.AIMSupplierLocationRefAnalyzerSupplierLocation 
								(
									  AnalyzerSupplierLocationID
									, AIMSupplierLocationID 
									, AIMSupplierID									
									, UpdateStamp
									, CFCUserID
									, AnalyzerSupplierID
								)
						VALUES
							(
								  @AnalyzerSupplierLocationID
								, @AIMSourceLocationID
								, @AIMSourceID								
								, GETDATE()
								, @CFCUserID
								, @AnalyzerSupplierID
							)
					END
				ELSE
					BEGIN
						SET @ErrorMessage = 'Supplier Location Code was not found'
						SET @ErrorNumber = 2
					END
			END
		ELSE
			BEGIN
				SET @ErrorMessage = 'Supplier is not cross referenced to analyzer supplier'
				SET @ErrorNumber = 3
			END
	END

SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @ReturnIntegerValue AS ReturnIntegerValue
	, @ReturnSecondIntegerValue as ReturnSecondIntegerValue

END