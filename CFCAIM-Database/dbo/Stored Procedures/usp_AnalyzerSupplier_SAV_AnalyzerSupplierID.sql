CREATE PROCEDURE [dbo].[usp_AnalyzerSupplier_SAV_AnalyzerSupplierID]
			  @AnalyzerSupplierID					INT			
			, @AnalyzerSupplierCode					NVARCHAR(20)	= NULL
			, @AnalyzerSupplierName					NVARCHAR(50)	= NULL	
			, @IsActive								BIT				= NULL	
			, @CFCUserID							INT				= NULL
			, @DataSourceTypeID						INT				= NULL				
AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @AnalyzerSupplierID = 0
	BEGIN				
		INSERT
			dbo.AnalyzerSupplier
				(
				  AnalyzerSupplierCode
				, AnalyzerSupplierName
				, UpdateStamp
				, CFCUserID
				, DataSourceTypeID											
				)
		VALUES
				(
				  @AnalyzerSupplierCode
				, @AnalyzerSupplierName
				, GETDATE()
				, @CFCUserID
				, @DataSourceTypeID
					
				)
	END
ELSE
	BEGIN

		IF @IsActive = 1
			IF EXISTS(SELECT AnalyzerSupplierID FROM AnalyzerSupplier 
							WHERE	DataSourceTypeID = @DataSourceTypeID
									and AnalyzerSupplierCode = @AnalyzerSupplierCode
									and AnalyzerSupplierName = @AnalyzerSupplierName)
				BEGIN
					SET @ErrorNumber = 1
					SET @ErrorMessage = 'This Supplier Name already exists for this Supplier. Please select a different name. Information is not saved!'					
				END
			

		IF @IsActive = 0
			BEGIN
				SET @IDinOtherTable =
				(
					SELECT 
						XREF.AnalyzerSupplierID
					FROM
						dbo.AIMSupplierRefAnalyzerSupplier XREF WITH(NOLOCK)
					WHERE
						XREF.AnalyzerSupplierID = @AnalyzerSupplierID
						
				)
				IF @IDinOtherTable IS NOT NULL
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'Unable to delete supplier because it is being used in the AIM/Analyzer supplier cross reference table.'						
					END


				IF EXISTS(SELECT TOP 1 AnalyzerSupplierID FROM dbo.AnalyzerSupplierLocation ASL WITH(NOLOCK) 
						WHERE ASL.AnalyzerSupplierID = @AnalyzerSupplierID and
								ASL.IsActive = 1)
				BEGIN
					SET @ErrorNumber = 1
					SET @ErrorMessage = 'Unable to inactivate supplier because an Analyzer supplier-location is assigned to it. You must first delete the supplier-location.'
				END

			END

		IF @ErrorNumber = 0
			BEGIN
				UPDATE
					dbo.AnalyzerSupplier WITH(ROWLOCK)
						SET 
							  AnalyzerSupplierCode = ISNULL(@AnalyzerSupplierCode, AnalyzerSupplierCode)
							, AnalyzerSupplierName = ISNULL(@AnalyzerSupplierName, AnalyzerSupplierName)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
							, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)												
							
						WHERE 
							AnalyzerSupplierID = @AnalyzerSupplierID
			END
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END