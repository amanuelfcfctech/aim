CREATE PROCEDURE [dbo].[usp_AnalyzerSupplier_SAV]
			  @AnalyzerSupplierID				INT			
			, @AnalyzerSupplierCode				NVARCHAR(20)  = NULL
			, @AnalyzerSupplierName				NVARCHAR(30)  = NULL
			, @DataSourceTypeID					INT = NULL
			, @IsActive							BIT
			, @CFCUserID						INT



-- * * Cross-references are not checked/deleted here because Analyzer Supplier is never "deleted", just inactivated.


--exec usp_AnalyzerSupplier_SAV_TEST 0, 'Premium Peanut', 'Premium Peanut', 10000, 1, 0

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

IF @AnalyzerSupplierID = 0

	BEGIN
			IF EXISTS(SELECT 
						AnalyzerSupplierID 
					FROM 
						dbo.AnalyzerSupplier ANS
					WHERE 
						ANS.AnalyzerSupplierCode = @AnalyzerSupplierCode
						and ANS.DataSourceTypeID = @DataSourceTypeID  )
				BEGIN
					SET @ErrorNumber = -1
					SET @ErrorMessage = 'This Supplier code already exists. Please enter a different code. Information is not saved.'

					SET @RecordID = (SELECT 
								AnalyzerSupplierID 
							FROM 
								dbo.AnalyzerSupplier ANS
							WHERE 
								ANS.AnalyzerSupplierCode = @AnalyzerSupplierCode
								and ANS.DataSourceTypeID = @DataSourceTypeID  )
				END
			ELSE
				BEGIN
					
					INSERT
						dbo.AnalyzerSupplier
							(
							  AnalyzerSupplierCode
							, AnalyzerSupplierName	
							, DataSourceTypeID						
							, UpdateStamp
							, CFCUserID
							)
					VALUES
							(
							  @AnalyzerSupplierCode
							, @AnalyzerSupplierName		
							, @DataSourceTypeID					 
							, GETDATE()
							, @CFCUserID
							)
					SET @RecordID = @@IDENTITY
				END
	END
ELSE
	BEGIN

		IF EXISTS(SELECT TOP 1 AnalyzerSupplierID FROM dbo.AnalyzerSupplierLocation ASL WITH(NOLOCK) 
							WHERE ASL.AnalyzerSupplierID = @AnalyzerSupplierID and
								  ASL.IsActive = 1)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'Unable to inactivate supplier because it is used in the Analyzer Supplier Location table.'
					END

		ELSE		
			BEGIN
				UPDATE
					dbo.AnalyzerSupplier WITH(ROWLOCK)
						SET 
							  AnalyzerSupplierCode = ISNULL(@AnalyzerSupplierCode, AnalyzerSupplierCode)
							, AnalyzerSupplierName = ISNULL(@AnalyzerSupplierName, AnalyzerSupplierName)
							, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
						WHERE 
							AnalyzerSupplierID = @AnalyzerSupplierID
				SET @RecordID = @AnalyzerSupplierID
		
			END
	END
		



SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage




END