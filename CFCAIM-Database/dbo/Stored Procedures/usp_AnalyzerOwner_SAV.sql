CREATE PROCEDURE [dbo].[usp_AnalyzerOwner_SAV]
			  @AnalyzerOwnerID			INT			
			, @AnalyzerOwnerCode			NVARCHAR(20)  = NULL
			, @AnalyzerOwnerName			NVARCHAR(30)  = NULL
			, @DataSourceTypeID					INT = NULL
			, @IsActive							BIT
			, @CFCUserID						INT


-- * * Cross-references are not checked/deleted here because Analyzer Owner is never "deleted", just inactivated.

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

IF @AnalyzerOwnerID = 0

	BEGIN
			IF EXISTS(SELECT 
						AnalyzerOwnerID 
					FROM 
						dbo.AnalyzerOwner ANO
					WHERE 
						ANO.AnalyzerOwnerCode = @AnalyzerOwnerCode 
						and ANO.DataSourceTypeID = @DataSourceTypeID)
				BEGIN
					SET @ErrorNumber = 1
					SET @ErrorMessage = 'This Owner code already exists. Please enter a different code. Information is not saved.'

					SET @RecordID = (SELECT 
									AnalyzerOwnerID 
								FROM 
									dbo.AnalyzerOwner ANO
								WHERE 
									ANO.AnalyzerOwnerCode = @AnalyzerOwnerCode 
									and ANO.DataSourceTypeID = @DataSourceTypeID)
				END
			ELSE				
				BEGIN
					INSERT
						dbo.AnalyzerOwner
							(
							  AnalyzerOwnerCode
							, AnalyzerOwnerName							
							, DataSourceTypeID
							, UpdateStamp
							, CFCUserID
							)
					VALUES
							(
							  @AnalyzerOwnerCode
							, @AnalyzerOwnerName		
							, @DataSourceTypeID					 
							, GETDATE()
							, @CFCUserID
							)
					SET @RecordID = @@IDENTITY
				END
	END
ELSE

	BEGIN
		IF EXISTS(SELECT TOP 1 AnalyzerOwnerID FROM dbo.AnalyzerOwnerLocation AOL WITH(NOLOCK) 
				WHERE AOL.AnalyzerOwnerID = @AnalyzerOwnerID and
								
						AOL.IsActive = 1)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'Unable to inactivate owner because an Analyzer owner-location is assigned to it. You must first delete the owner-location.'
			END		

		ELSE
			BEGIN

				UPDATE
					dbo.AnalyzerOwner WITH(ROWLOCK)
						SET 
								AnalyzerOwnerCode = ISNULL(@AnalyzerOwnerCode, AnalyzerOwnerCode)
							, AnalyzerOwnerName = ISNULL(@AnalyzerOwnerName, AnalyzerOwnerName)
							, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
						WHERE 
							AnalyzerOwnerID = @AnalyzerOwnerID
				SET @RecordID = @AnalyzerOwnerID
			END
	END





SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END