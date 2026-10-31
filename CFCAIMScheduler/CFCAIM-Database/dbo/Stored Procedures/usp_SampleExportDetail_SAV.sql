
CREATE PROCEDURE [dbo].[usp_SampleExportDetail_SAV]
			  @SampleExportDetailID	INT
			, @SampleExportID	INT				= NULL
			, @MaterialType		NVARCHAR(30)	= NULL
			, @MaterialCode		NVARCHAR(30)	= NULL
			, @SampleDate		DATETIME		= NULL
			, @SampleCode		NVARCHAR(50)	= NULL
			, @UpdateStamp		DATETIME		= NULL
			, @CFCUserID		INT				= NULL
			, @MaterialName		NVARCHAR(50)	= NULL
			, @SampleExportName NVARCHAR(50)	= NULL
			, @SampleExportType	INT				= NULL	
			, @SampleExportPath	NVARCHAR(100)	= NULL
			
--exec usp_SampleExportDetail_SAV 0, 1, 'Ingredient', 'Meat and Bone', '20140609', '7609', null, 2
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleExportDetailID = 0
	BEGIN
		INSERT
			dbo.SampleExportDetail
				(
				  SampleExportID
				, MaterialType
				, MaterialCode
				, SampleDate
				, SampleCode
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @SampleExportID
				, @MaterialType
				, @MaterialCode
				, @SampleDate	
				, @SampleCode							
				, @CFCUserID
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.SampleExportDetail WITH(ROWLOCK)
				SET 
					  SampleExportID = ISNULL(@SampleExportID, SampleExportID)
					, MaterialType = ISNULL(@MaterialType, MaterialType)
					, MaterialCode = ISNULL(@MaterialCode, MaterialCode)
					, SampleDate = ISNULL(@SampleDate, SampleDate)									
					, SampleCode = ISNULL(@SampleCode, SampleCode)		
					, UpdateStamp = GETDATE()							
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					
					
				WHERE 
					SampleExportDetailID = @SampleExportDetailID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END