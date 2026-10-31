CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_INS_GenerateFromAnalyzer]
			  @AnalyzerOwnerLocationID		INT
			, @AnalyzerOwnerID				INT
			, @CFCUserID					INT = NULL
						
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber				INT = 0
DECLARE @ErrorMessage				VARCHAR(200) = ''
DECLARE @AIMOwnerLocationID		INT	
DECLARE @AIMOwnerID				INT	
DECLARE @ReturnIntegerValue			INT
DECLARE @ReturnSecondIntegerValue	INT
DECLARE @ID							INT

IF EXISTS(SELECT 
				AIMOwnerLocationID 	
			FROM 
				dbo.AnalyzerOwnerLocation ANOL
				INNER JOIN
					dbo.AIMOwnerLocation AOL
						ON AOL.LocationName = ANOL.AnalyzerOwnerLocationName 
			WHERE 
				ANOL.AnalyzerOwnerLocationID = @AnalyzerOwnerLocationID)
	BEGIN
		SET @ErrorMessage = 'Owner Location Code already exists'
		SET @ErrorNumber = 1
		SET @ReturnIntegerValue = (SELECT AIMOwnerLocationID 	
									FROM 
										dbo.AnalyzerOwnerLocation ANOL
									INNER JOIN
										dbo.AIMOwnerLocation AOL
											ON AOL.LocationName = ANOL.AnalyzerOwnerLocationName 
									WHERE 
										ANOL.AnalyzerOwnerLocationID = @AnalyzerOwnerLocationID)
		SET @ReturnSecondIntegerValue = (SELECT AIMOwnerID 	
									FROM 										
										dbo.AIMOwnerLocation AOL											
									WHERE 
										AOL.AIMOwnerLocationID = @ReturnIntegerValue)
	END
ELSE
	BEGIN

		SELECT 
			@AIMOwnerID = ASRAS.AIMOwnerID  	
		FROM 
			dbo.AnalyzerOwnerLocation ANOL
			INNER JOIN
				dbo.AnalyzerOwner ANS
					ON ANS.AnalyzerOwnerID = ANOL.AnalyzerOwnerID 
			INNER JOIN 
				dbo.AIMOwnerRefAnalyzerOwner ASRAS
					ON ASRAS.AnalyzerOwnerID = ANS.AnalyzerOwnerID 
		WHERE 
			ANOL.AnalyzerOwnerLocationID = @AnalyzerOwnerLocationID

		IF @AIMOwnerID > 0 
			BEGIN											
						
				INSERT INTO 
					dbo.AIMOwnerLocation 
						(
							  AIMOwnerID
							, LocationName  
							, LocationCode
							, IsActive 
							, UpdateStamp 
							, CFCUserID 
							, IsFormulationPlant
						)
				SELECT 
					@AIMOwnerID					
					, AnalyzerOwnerLocationName
					, AnalyzerOwnerLocationCode
					, 1
					, GETDATE()
					, @CFCUserID
					, 1

				FROM 
					dbo.AnalyzerOwnerLocation UDO
				WHERE 
					UDO.AnalyzerOwnerLocationID = @AnalyzerOwnerLocationID
									
				SET @AIMOwnerLocationID = @@IDENTITY
				SET @ReturnIntegerValue = @AIMOwnerID

				IF @AIMOwnerLocationID > 0 
					BEGIN

						-- make sure parameter is same as what's on table
						SET @ID = (SELECT TOP 1 ANS.AnalyzerOwnerID
						FROM 
							dbo.AnalyzerOwnerLocation ANOL
							INNER JOIN
								dbo.AnalyzerOwner ANS
									ON ANS.AnalyzerOwnerID = ANOL.AnalyzerOwnerID 
							INNER JOIN 
								dbo.AIMOwnerLocationRefAnalyzerOwnerLocation AOLRAS
									ON AOLRAS.AnalyzerOwnerID = ANS.AnalyzerOwnerID 
						WHERE 
							ANOL.AnalyzerOwnerLocationID = @AnalyzerOwnerLocationID)
						IF @ID <> @AnalyzerOwnerID
							SET @AnalyzerOwnerID = @ID

						INSERT INTO	
							dbo.AIMOwnerLocationRefAnalyzerOwnerLocation 
								(
									  AnalyzerOwnerLocationID
									, AnalyzerOwnerID
									, AIMOwnerLocationID 
									, AIMOwnerID									
									, UpdateStamp
									, CFCUserID									
								)
						VALUES
							(
								  @AnalyzerOwnerLocationID
								, @AnalyzerOwnerID
								, @AIMOwnerLocationID
								, @AIMOwnerID								
								, GETDATE()
								, @CFCUserID
								
							)
					END
				ELSE
					BEGIN
						SET @ErrorMessage = 'Owner Location Code was not found'
						SET @ErrorNumber = 2
					END
			END
		ELSE
			BEGIN
				SET @ErrorMessage = 'Owner is not cross referenced to analyzer Owner'
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