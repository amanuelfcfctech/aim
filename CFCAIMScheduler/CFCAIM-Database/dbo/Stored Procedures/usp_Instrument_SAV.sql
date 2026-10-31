CREATE PROCEDURE [dbo].[usp_Instrument_SAV]
			  @InstrumentID						INT
			, @InstrumentCode					NVARCHAR(20) = NULL
			, @Description  					NVARCHAR(30) = NULL
			, @ArchivedFolder					NVARCHAR(250) = NULL
			, @ProximateFolder					NVARCHAR(250) = NULL
			, @SpectraFolder					NVARCHAR(250) = NULL			
			, @FileTypeID						INT		= NULL
			, @DataSourceTypeID					INT		= NULL
			, @CFCUserID						INT		= NULL
			, @IsActive							INT		= NULL			
AS

BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @InstrumentID = 0
	BEGIN
		INSERT
			dbo.Instrument 
				(
				  InstrumentCode
				, Description
				, ArchivedFolder
				, ProximateFolder	
				, SpectraFolder						
				, FileTypeID
				, DataSourceTypeID
				, CFCUserID
				, UpdateStamp				
				, IsActive
			
				)
		VALUES
				(
				  @InstrumentCode
				, @Description
				, @ArchivedFolder
				, @ProximateFolder							
				, @SpectraFolder	
				, @FileTypeID
				, @DataSourceTypeID
				, @CFCUserID
				, GETDATE()
				, @IsActive
				
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.Instrument WITH(ROWLOCK)
				SET 
					  InstrumentCode = ISNULL(@InstrumentCode, InstrumentCode)
					, Description = ISNULL(@Description, Description)
					, ArchivedFolder = ISNULL(@ArchivedFolder, ArchivedFolder)
					, ProximateFolder = ISNULL(@ProximateFolder, ProximateFolder)
					, SpectraFolder	 = ISNULL(@SpectraFolder, SpectraFolder	)
					, FileTypeID = ISNULL(@FileTypeID, FileTypeID)
					, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)					
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()
					, IsActive = ISNULL(@IsActive, IsActive)
					
					
				WHERE 
					InstrumentID = @InstrumentID

			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END

