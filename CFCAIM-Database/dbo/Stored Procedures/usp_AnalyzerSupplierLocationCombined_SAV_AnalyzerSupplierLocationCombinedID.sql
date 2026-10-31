CREATE PROCEDURE [dbo].[usp_AnalyzerSupplierLocationCombined_SAV_AnalyzerSupplierLocationCombinedID]
			  @AnalyzerSupplierLocationCombinedID					INT						
			, @AnalyzerSupplierLocationCombinedName					NVARCHAR(50)	= NULL	
			, @IsActive								BIT				= NULL	
			, @CFCUserID							INT				= NULL
			, @DataSourceTypeID						INT				= NULL				
AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @AnalyzerSupplierLocationCombinedID = 0
	BEGIN				
		INSERT
			dbo.AnalyzerSupplierLocationCombined
				(
				  AnalyzerSupplierLocationCombinedName
				, UpdateStamp
				, CFCUserID
				, DataSourceTypeID											
				)
		VALUES
				(
				 @AnalyzerSupplierLocationCombinedName
				, GETDATE()
				, @CFCUserID
				, @DataSourceTypeID
					
				)
	END
ELSE
	BEGIN

		IF @IsActive = 1
			IF EXISTS(SELECT AnalyzerSupplierLocationCombinedID FROM AnalyzerSupplierLocationCombined 
							WHERE	DataSourceTypeID = @DataSourceTypeID
									and AnalyzerSupplierLocationCombinedName = @AnalyzerSupplierLocationCombinedName)
				BEGIN
					SET @ErrorNumber = 1
					SET @ErrorMessage = 'This Supplier/Supplier-Location (combined) Name already exists. Please select a different name. Information is not saved!'					
				END
			

		--NEED THIS WHEN XREF IS SET UP
		--IF @IsActive = 0
		--	BEGIN
		--		SET @IDinOtherTable =
		--		(
		--			SELECT 
		--				XREF.AnalyzerSupplierID
		--			FROM
		--				dbo.AIMSupplierRefAnalyzerSupplier XREF WITH(NOLOCK)
		--			WHERE
		--				XREF.AnalyzerSupplierID = @AnalyzerSupplierID
						
		--		)
		--		IF @IDinOtherTable IS NOT NULL
		--			BEGIN
		--				SET @ErrorNumber = 1
		--				SET @ErrorMessage = 'Unable to delete supplier because it is being used in the AIM/Analyzer supplier cross reference table.'						
		--			END


		--		IF EXISTS(SELECT TOP 1 AnalyzerSupplierID FROM dbo.AnalyzerSupplierLocation ASL WITH(NOLOCK) 
		--				WHERE ASL.AnalyzerSupplierID = @AnalyzerSupplierID and
		--						ASL.IsActive = 1)
		--		BEGIN
		--			SET @ErrorNumber = 1
		--			SET @ErrorMessage = 'Unable to inactivate supplier because an Analyzer supplier-location is assigned to it. You must first delete the supplier-location.'
		--		END

		--	END

		IF @ErrorNumber = 0
			BEGIN
				UPDATE
					dbo.AnalyzerSupplierLocationCombined WITH(ROWLOCK)
						SET 
							  AnalyzerSupplierLocationCombinedName = ISNULL(@AnalyzerSupplierLocationCombinedName, AnalyzerSupplierLocationCombinedName)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
							, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)												
							
						WHERE 
							AnalyzerSupplierLocationCombinedID = @AnalyzerSupplierLocationCombinedID
			END
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END