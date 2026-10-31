create PROCEDURE [dbo].[usp_DataSourceDetail_SAV]
			  @DataSourceDetailID								INT
			, @DataSourceHeaderID								INT = NULL
			, @DataSourceTypeID									INT = NULL
			, @AnalyzerDataSourceTypeID							INT = NULL																		
			, @CFCUserID										INT	= NULL
			
AS
BEGIN

SET NOCOUNT ON;

DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0 

IF EXISTS(
			SELECT
				Dtl.*
			FROM
				DataSourceDetail Dtl
			WHERE
					Dtl.DataSourceHeaderID = @DataSourceHeaderID
				AND	Dtl.DataSourceTypeID = @DataSourceTypeID
				AND Dtl.AnalyzerDataSourceTypeID = @AnalyzerDataSourceTypeID
				AND Dtl.DataSourceDetailID != @DataSourceDetailID
		)
BEGIN
	SET @ResultNumber = 1
	SET	@ResultMessage = 'This data source is already cross-referenced. Information is not saved!'
END

IF @ResultNumber = 0
BEGIN

	----------------------------------------------------
	--check detail table for this AnalyzerDataSourceTypeID
	DECLARE @CrossRefID INT = 0
	SET @CrossRefID = (SELECT TOP 1 DataSourceDetailID
				FROM dbo. DataSourceDetail with (nolock)
				WHERE AnalyzerDataSourceTypeID = @AnalyzerDataSourceTypeID
					and DataSourceHeaderID = @DataSourceHeaderID) 
	SET @DataSourceDetailID = ISNULL(@CrossRefID,0)
	----------------------------------------------------
	
	IF EXISTS(
				SELECT 
					xref.DataSourceDetailID
				FROM
					DataSourceDetail xref
				WHERE
					xref.DataSourceDetailID = @DataSourceDetailID
			)
		BEGIN
			UPDATE
				dbo.DataSourceDetail WITH(ROWLOCK)
				SET 
					  DataSourceHeaderID = ISNULL(@DataSourceHeaderID, DataSourceHeaderID)
					, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)	
					, AnalyzerDataSourceTypeID = ISNULL(@AnalyzerDataSourceTypeID, AnalyzerDataSourceTypeID)					
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()
					
				WHERE 
					DataSourceDetailID = @DataSourceDetailID
			SET @RecordID = @@IDENTITY 
		END
	ELSE	
		BEGIN
			INSERT
				dbo.DataSourceDetail
				(
				  DataSourceHeaderID
				, DataSourceTypeID
				, AnalyzerDataSourceTypeID												
				, CFCUserID
				, UpdateStamp				
				)
			VALUES
				(
				  @DataSourceHeaderID
				, @DataSourceTypeID				
				, @AnalyzerDataSourceTypeID				
				, @CFCUserID
				, GETDATE()
				)
			SET @RecordID = @@IDENTITY 
		END
	END

SELECT 
	  @RecordID as RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage

END