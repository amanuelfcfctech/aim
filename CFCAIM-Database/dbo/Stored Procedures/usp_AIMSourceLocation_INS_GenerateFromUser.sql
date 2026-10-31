CREATE PROCEDURE [dbo].[usp_AIMSourceLocation_INS_GenerateFromUser]
			  @AnalyzerSupplierLocationID		INT
			, @AnalyzerSupplierID				INT
			, @CFCUserID						INT = NULL

						
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber				INT = 0
DECLARE @ErrorMessage				VARCHAR(200) = ''
DECLARE @AIMSupplierLocationID		INT	
DECLARE @AIMSupplierID				INT	
DECLARE @ReturnIntegerValue			INT
DECLARE @SupplierID					INT

IF EXISTS(SELECT 
				AIMSourceLocationID 	
			FROM 
				dbo.AnalyzerSupplierLocation ANSL
				INNER JOIN
					dbo.AIMSourceLocation ASL
						ON ASL.SourceLocationName = ANSL.AnalyzerSupplierLocationName 
			WHERE 
				ANSL.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID)
	BEGIN
		SET @ErrorMessage = 'Supplier Location Code already exists'
		SET @ErrorNumber = 1
		SET @ReturnIntegerValue = (SELECT AIMSourceLocationID 	
									FROM 
										dbo.AnalyzerSupplierLocation ANSL
									INNER JOIN
										dbo.AIMSourceLocation ASL
											ON ASL.SourceLocationName = ANSL.AnalyzerSupplierLocationName 
									WHERE 
										ANSL.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID)
	END
ELSE
	BEGIN

		SELECT 
			@AIMSupplierID = ASRAS.AIMSupplierID  	
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

		IF @AIMSupplierID > 0 
			BEGIN							

				INSERT INTO 
					dbo.AIMSourceLocation 
						(
							  AIMSourceID
							, SourceLocationName  
							, IsActive 
							, UpdateStamp 
							, CFCUserID 
						)
				SELECT 
					@AIMSupplierID
					, AnalyzerSupplierLocationName
					, 1
					, GETDATE()
					, @CFCUserID
				FROM 
					dbo.AnalyzerSupplierLocation ANSL
				WHERE 
					ANSL.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID

				SET @AIMSupplierLocationID = @@IDENTITY

				IF @AIMSupplierLocationID > 0 
					BEGIN

						-- make sure parameter is same as what's on table
						SET @SupplierID = (SELECT TOP 1 ANS.AnalyzerSupplierID
						FROM 
							dbo.AnalyzerSupplierLocation ANSL
							INNER JOIN
								dbo.AnalyzerSupplier ANS
									ON ANS.AnalyzerSupplierID = ANSL.AnalyzerSupplierID 
							INNER JOIN 
								dbo.AIMSupplierRefAnalyzerSupplier ASRAS
									ON ASRAS.AnalyzerSupplierID = ANS.AnalyzerSupplierID 
						WHERE 
							ANSL.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID)
						IF @SupplierID <> @AnalyzerSupplierID
							SET @AnalyzerSupplierID = @SupplierID

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
								, @AIMSupplierLocationID
								, @AIMSupplierID								
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
				SET @ErrorMessage = 'Supplier is not cross referenced to an Analyzer supplier'
				SET @ErrorNumber = 3
			END
	END

SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @ReturnIntegerValue AS ReturnIntegerValue

END