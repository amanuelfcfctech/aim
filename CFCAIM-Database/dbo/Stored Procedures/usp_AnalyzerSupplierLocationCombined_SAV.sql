CREATE PROCEDURE [dbo].[usp_AnalyzerSupplierLocationCombined_SAV]
			  @AnalyzerSupplierLocationCombinedID				INT						
			, @AnalyzerSupplierLocationCombinedName				NVARCHAR(30)  = NULL
			, @DataSourceTypeID									INT = NULL
			, @IsActive											BIT
			, @CFCUserID										INT


-- * * Cross-references are not checked/deleted here because Analyzer Supplier-Location is never "deleted", just inactivated.


--exec usp_AnalyzerSupplierLocationCombined_SAV_TEST 0, 'Premium Peanut', 10000, 1, 0

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

IF @AnalyzerSupplierLocationCombinedID = 0

	BEGIN
			IF EXISTS(SELECT 
						AnalyzerSupplierLocationCombinedID 
					FROM 
						dbo.AnalyzerSupplierLocationCombined ANS
					WHERE 
						ANS.AnalyzerSupplierLocationCombinedName = @AnalyzerSupplierLocationCombinedName
						and ANS.DataSourceTypeID = @DataSourceTypeID  )
				BEGIN
					SET @ErrorNumber = -1
					SET @ErrorMessage = 'This Supplier-SupplierLocation (combined) name already exists. Please enter a different name. Information is not saved.'

					SET @RecordID = (SELECT 
								AnalyzerSupplierLocationCombinedID 
							FROM 
								dbo.AnalyzerSupplierLocationCombined ANS
							WHERE 
								ANS.AnalyzerSupplierLocationCombinedName = @AnalyzerSupplierLocationCombinedName
								and ANS.DataSourceTypeID = @DataSourceTypeID  )
				END
			ELSE
				BEGIN
					
					INSERT
						dbo.AnalyzerSupplierLocationCombined
							(
							  AnalyzerSupplierLocationCombinedName	
							, DataSourceTypeID						
							, UpdateStamp
							, CFCUserID
							)
					VALUES
							(
							  @AnalyzerSupplierLocationCombinedName		
							, @DataSourceTypeID					 
							, GETDATE()
							, @CFCUserID
							)
					SET @RecordID = @@IDENTITY
				END
	END
ELSE
			
		BEGIN
			UPDATE
				dbo.AnalyzerSupplierLocationCombined WITH(ROWLOCK)
					SET 
							AnalyzerSupplierLocationCombinedName = ISNULL(@AnalyzerSupplierLocationCombinedName, AnalyzerSupplierLocationCombinedName)
						, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
						, IsActive = ISNULL(@IsActive, IsActive)
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID
					WHERE 
						AnalyzerSupplierLocationCombinedID = @AnalyzerSupplierLocationCombinedID
			SET @RecordID = @AnalyzerSupplierLocationCombinedID
		
		END
	
		



SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage




END