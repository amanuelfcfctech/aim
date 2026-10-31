CREATE PROCEDURE [dbo].[usp_AnalyzerOwnerLocation_SAV]
			  @AnalyzerOwnerLocationID		INT			
			, @AnalyzerOwnerID				INT
			, @AnalyzerOwnerLocationCode	NVARCHAR(20)  = NULL
			, @AnalyzerOwnerLocationName	NVARCHAR(30)  = NULL
			, @DataSourceTypeID				INT = NULL
			, @IsActive						BIT
			, @CFCUserID					INT


AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

-- * * Cross-references are not checked/deleted here because Analyzer Owner-Location is never "deleted", just inactivated.


IF @AnalyzerOwnerLocationID = 0

	BEGIN
			IF EXISTS(SELECT 
						AnalyzerOwnerLocationID 
					FROM 
						dbo.AnalyzerOwnerLocation UDOL
					WHERE 
						UDOL.AnalyzerOwnerID = @AnalyzerOwnerID  
						and UDOL.AnalyzerOwnerLocationCode = @AnalyzerOwnerLocationCode
						and UDOL.DataSourceTypeID = @DataSourceTypeID )
				BEGIN
					SET @ErrorNumber = 1
					SET @ErrorMessage = 'This Owner Location code already exists. Please enter a different code. Information is not saved.'

					SET @RecordID = (SELECT 
									AnalyzerOwnerLocationID 
								FROM 
									dbo.AnalyzerOwnerLocation UDOL
								WHERE 
									UDOL.AnalyzerOwnerID = @AnalyzerOwnerID
									and UDOL.AnalyzerOwnerLocationCode = @AnalyzerOwnerLocationCode
									and UDOL.DataSourceTypeID = @DataSourceTypeID )
				END
			ELSE
				BEGIN
					INSERT
						dbo.AnalyzerOwnerLocation
							(
							  AnalyzerOwnerID
							, AnalyzerOwnerLocationCode
							, AnalyzerOwnerLocationName	
							, DataSourceTypeID						
							, UpdateStamp
							, CFCUserID
							)
					VALUES
							(
							  @AnalyzerOwnerID
							, @AnalyzerOwnerLocationCode
							, @AnalyzerOwnerLocationName		
							, @DataSourceTypeID					 
							, GETDATE()
							, @CFCUserID
							)
					SET @RecordID = @@IDENTITY
				END
	END
ELSE
	BEGIN
		IF @ErrorNumber = 0
			BEGIN

				UPDATE
					dbo.AnalyzerOwnerLocation WITH(ROWLOCK)
						SET 
							  AnalyzerOwnerID = ISNULL(@AnalyzerOwnerID, AnalyzerOwnerID)
							, AnalyzerOwnerLocationCode = ISNULL(@AnalyzerOwnerLocationCode, AnalyzerOwnerLocationCode)
							, AnalyzerOwnerLocationName = ISNULL(@AnalyzerOwnerLocationName, AnalyzerOwnerLocationName)
							, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
						WHERE 
							AnalyzerOwnerLocationID = @AnalyzerOwnerLocationID
				SET @RecordID = @AnalyzerOwnerLocationID
			END
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END