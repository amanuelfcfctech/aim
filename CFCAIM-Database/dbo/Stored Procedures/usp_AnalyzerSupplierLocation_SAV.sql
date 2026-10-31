CREATE PROCEDURE [dbo].[usp_AnalyzerSupplierLocation_SAV]
			  @AnalyzerSupplierLocationID			INT			
			, @AnalyzerSupplierID					INT
			, @AnalyzerSupplierLocationCode			NVARCHAR(20)  = NULL
			, @AnalyzerSupplierLocationName			NVARCHAR(30)  = NULL
			, @DataSourceTypeID					INT = NULL
			, @IsActive							BIT
			, @CFCUserID						INT


-- * * Cross-references are not checked/deleted here because Analyzer Supplier-Location is never "deleted", just inactivated.

--exec usp_AnalyzerSupplierLocation_SAV 0, 1, '1', 'test', 1, 1

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

IF @AnalyzerSupplierLocationID = 0

	BEGIN
			IF EXISTS(SELECT 
						AnalyzerSupplierLocationID 
					FROM 
						dbo.AnalyzerSupplierLocation ANSL
					WHERE 
						ANSL.AnalyzerSupplierID = @AnalyzerSupplierID 
						and ANSL.AnalyzerSupplierLocationCode = @AnalyzerSupplierLocationCode
						and ANSL.DataSourceTypeID = @DataSourceTypeID )
				BEGIN
					SET @ErrorNumber = 1
					SET @ErrorMessage = 'This Supplier Location code already exists. Please enter a different code. Information is not saved.'

					SET @RecordID = (SELECT 
								AnalyzerSupplierLocationID 
							FROM 
								dbo.AnalyzerSupplierLocation ANSL
							WHERE 
								ANSL.AnalyzerSupplierID = @AnalyzerSupplierID 
								and ANSL.AnalyzerSupplierLocationCode = @AnalyzerSupplierLocationCode
								and ANSL.DataSourceTypeID = @DataSourceTypeID )
				END
			ELSE
				BEGIN
					INSERT
						dbo.AnalyzerSupplierLocation
							(
							  AnalyzerSupplierID
							, AnalyzerSupplierLocationCode
							, AnalyzerSupplierLocationName							
							, DataSourceTypeID
							, UpdateStamp
							, CFCUserID
							)
					VALUES
							(
							  @AnalyzerSupplierID
							, @AnalyzerSupplierLocationCode
							, @AnalyzerSupplierLocationName	
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
					dbo.AnalyzerSupplierLocation WITH(ROWLOCK)
						SET 
							  AnalyzerSupplierID = ISNULL(@AnalyzerSupplierID, AnalyzerSupplierID)
							, AnalyzerSupplierLocationCode = ISNULL(@AnalyzerSupplierLocationCode, AnalyzerSupplierLocationCode)
							, AnalyzerSupplierLocationName = ISNULL(@AnalyzerSupplierLocationName, AnalyzerSupplierLocationName)
							, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
						WHERE 
							AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID
				SET @RecordID = @AnalyzerSupplierLocationID
			END
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END