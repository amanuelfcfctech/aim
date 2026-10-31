CREATE PROCEDURE [dbo].[usp_AnalyzerAnalyse_SAV]
			  @AnalyzerAnalyseID				INT
			, @DataSourceTypeID					INT
			, @Code								NVARCHAR(10)
			, @Name								NVARCHAR(30)
			, @Description						NVARCHAR(100)
			, @IsActive							BIT
			, @CFCUserID						INT
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @AnalyzerAnalyseID = 0 
	BEGIN
	IF EXISTS(SELECT AnalyzerAnalyseID FROM dbo.AnalyzerAnalyse WHERE Code = @Code)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Analyzer Analysis code already exists. Please choose a different code. Information is not saved!'
			END
		ELSE
			BEGIN
				INSERT
					dbo.AnalyzerAnalyse
						(
						  DataSourceTypeID
						, Code
						, Name
						, Description
						, UpdateStamp
						, CFCUserID
						
						)
				VALUES
						(
						  @DataSourceTypeID
						, @Code
						, @Name
						, @Description
						, GETDATE()
						, @CFCUserID
						
						)
				SET @RecordID = @@IDENTITY 
			END
		END
ELSE
	BEGIN
		UPDATE
			dbo.AnalyzerAnalyse WITH(ROWLOCK)
				SET 
					  DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
					, Code = ISNULL(@Code, Code)
					, Name = ISNULL(@Name, Name)
					, Description = ISNULL(@Description, Description)
					, IsActive = ISNULL(@IsActive, IsActive)
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
					
				WHERE 
					AnalyzerAnalyseID = @AnalyzerAnalyseID
		SET @RecordID = @AnalyzerAnalyseID
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
