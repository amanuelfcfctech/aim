
CREATE PROCEDURE [dbo].[usp_SampleExport_SAV]
			  @SampleExportID	INT			
			, @SampleExportName NVARCHAR(50)	= NULL
			, @SampleExportType	INT				= NULL	
			, @SampleExportPath	NVARCHAR(100)	= NULL
			, @CFCUserID		INT				= NULL		
			, @UpdateStamp		DATETIME		= NULL
			
--exec usp_SampleExportDetail_SAV 0, 'test', 1, 'testpath', 1, '20160520' 
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleExportID = 0
	BEGIN
		INSERT
			dbo.SampleExport
				(
				  SampleExportName
				, SampleExportType
				, SampleExportPath
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @SampleExportName
				, @SampleExportType
				, @SampleExportPath										
				, @CFCUserID
				, GetDate()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.SampleExport WITH(ROWLOCK)
				SET 
					  SampleExportName = ISNULL(@SampleExportName, SampleExportName)
					, SampleExportType = ISNULL(@SampleExportType, SampleExportType)
					, SampleExportPath = ISNULL(@SampleExportPath, SampleExportPath)						
					, UpdateStamp = GETDATE()							
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					
					
				WHERE 
					SampleExportID = @SampleExportID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END