CREATE PROCEDURE [dbo].[usp_AnalyzerSupplierLocation_SAV_AnalyzerSupplierLocationID]
			  @AnalyzerSupplierLocationID			INT			
			, @AnalyzerSupplierID					INT				= NULL
			, @AnalyzerSupplierLocationCode			NVARCHAR(20)	= NULL
			, @AnalyzerSupplierLocationName			NVARCHAR(50)	= NULL	
			, @IsActive								BIT				= NULL	
			, @CFCUserID							INT				= NULL
			, @DataSourceTypeID						INT				= NULL				

AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @AnalyzerSupplierLocationID = 0
	BEGIN
		
		INSERT
			dbo.AnalyzerSupplierLocation
				(
				  AnalyzerSupplierID
				, AnalyzerSupplierLocationCode
				, AnalyzerSupplierLocationName
				, UpdateStamp
				, CFCUserID
				, DataSourceTypeID											
				)
		VALUES
				(
				  @AnalyzerSupplierID
				, @AnalyzerSupplierLocationCode
				, @AnalyzerSupplierLocationName
				, GETDATE()
				, @CFCUserID
				, @DataSourceTypeID
					
				)
	END
ELSE
	BEGIN
		IF @IsActive = 0
			BEGIN
				SET @IDinOtherTable =
				(
					SELECT 
						XREF.AnalyzerSupplierLocationID
					FROM
						dbo.AIMSupplierLocationRefAnalyzerSupplierLocation XREF WITH(NOLOCK)
					WHERE
						XREF.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID
						
				)
				IF @IDinOtherTable IS NOT NULL
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'Unable to delete supplier location because it is being used in the AIM/Analyzer supplier-location cross reference table.'						
					END
			END

		IF @ErrorNumber = 0
			BEGIN
				UPDATE
					dbo.AnalyzerSupplierLocation WITH(ROWLOCK)
						SET 
						      @AnalyzerSupplierID = ISNULL(@AnalyzerSupplierID, AnalyzerSupplierID)
							, AnalyzerSupplierLocationCode = ISNULL(@AnalyzerSupplierLocationCode, AnalyzerSupplierLocationCode)
							, AnalyzerSupplierLocationName = ISNULL(@AnalyzerSupplierLocationName, AnalyzerSupplierLocationName)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
							, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)												
							
						WHERE 
							AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID
			END
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END