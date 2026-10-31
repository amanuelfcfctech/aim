CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_INS_GenerateFromUser]
			  @AnalyzerOwnerLocationID		INT
			, @AnalyzerOwnerID				INT
			, @CFCUserID						INT = NULL

--exec [usp_AIMOwnerLocation_INS_GenerateFromUser] 2058, 0
						
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber				INT = 0
DECLARE @ErrorMessage				VARCHAR(200) = ''
DECLARE @AIMOwnerLocationID		INT	
DECLARE @AIMOwnerID				INT	
DECLARE @ReturnIntegerValue		INT
DECLARE @UserOwnerID			INT

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
										

	END
ELSE
	BEGIN

		SELECT 
			@AIMOwnerID = UDCRAO.AIMOwnerID  	
		FROM 
			dbo.AnalyzerOwnerLocation ANOL
			INNER JOIN
				dbo.AnalyzerOwner ANO
					ON ANO.AnalyzerOwnerID = ANOL.AnalyzerOwnerID 
			INNER JOIN 
				dbo.AIMOwnerLocationRefAnalyzerOwnerLocation UDCRAO
					ON UDCRAO.AnalyzerOwnerID = ANO.AnalyzerOwnerID 
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
					, Left(AnalyzerOwnerLocationName, 20)
					, 1
					, GETDATE()
					, @CFCUserID
					, 1
				FROM 
					dbo.AnalyzerOwnerLocation UDO
				WHERE 
					UDO.AnalyzerOwnerLocationID = @AnalyzerOwnerLocationID

				SET @AIMOwnerLocationID = @@IDENTITY

				IF @AIMOwnerLocationID > 0 
					BEGIN

					-- make sure parameter is same as what's on table
						SET @UserOwnerID = (SELECT TOP 1 ANO.AnalyzerOwnerID
						FROM 
							dbo.AnalyzerOwnerLocation ANOL
							INNER JOIN
								dbo.AnalyzerOwner ANO
									ON ANO.AnalyzerOwnerID = ANOL.AnalyzerOwnerID 
							INNER JOIN 
								dbo.AIMOwnerLocationRefAnalyzerOwnerLocation UDCRAO
									ON UDCRAO.AnalyzerOwnerID = ANO.AnalyzerOwnerID 
						WHERE 
							ANOL.AnalyzerOwnerLocationID = @AnalyzerOwnerLocationID)
						IF @UserOwnerID <> @AnalyzerOwnerID
							SET @AnalyzerOwnerID = @UserOwnerID


						INSERT INTO	
							dbo.AIMOwnerLocationRefAnalyzerOwnerLocation 
								(
									  AnalyzerOwnerLocationID
									, AIMOwnerLocationID 
									, AIMOwnerID
									, IsActive 
									, UpdateStamp
									, CFCUserID
									, AnalyzerOwnerID
								)
						VALUES
							(
								  @AnalyzerOwnerLocationID
								, @AIMOwnerLocationID
								, @AIMOwnerID
								, 1
								, GETDATE()
								, @CFCUserID
								, @AnalyzerOwnerID
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
				SET @ErrorMessage = 'Owner is not cross referenced to user-defined owner'
				SET @ErrorNumber = 3
			END
	END
SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @ReturnIntegerValue AS ReturnIntegerValue

END