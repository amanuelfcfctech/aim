CREATE PROCEDURE [dbo].[usp_AnalyzerMaterial_SAV]
			  @AnalyzerMaterialID				INT
			, @DataSourceTypeID					INT
			, @AnalyzerMaterialCode				NVARCHAR(20)
			, @AnalyzerMaterialName				NVARCHAR(30)
			, @IsActive							BIT
			, @CFCUserID						INT
			
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @AnalyzerMaterialID = 0 
	BEGIN
		IF EXISTS(SELECT 
					AnalyzerMaterialID 
				FROM 
					dbo.AnalyzerMaterial AM
				WHERE 
						AM.AnalyzerMaterialCode = @AnalyzerMaterialCode
					AND AM.DataSourceTypeID = @DataSourceTypeID	)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Material code already exists. Please select a different code. Information is not saved!'

				SET @RecordID = (SELECT 
									AnalyzerMaterialID 
								FROM 
									dbo.AnalyzerMaterial AM
								WHERE 
									AM.AnalyzerMaterialCode = @AnalyzerMaterialCode
									AND AM.DataSourceTypeID = @DataSourceTypeID	)
			END
		ELSE
			BEGIN
				INSERT
					dbo.AnalyzerMaterial
						(
						  DataSourceTypeID
						, AnalyzerMaterialCode
						, AnalyzerMaterialName
						, UpdateStamp
						, CFCUserID						
						)
				VALUES
						(
						  @DataSourceTypeID
						, @AnalyzerMaterialCode
						, @AnalyzerMaterialName
						, GETDATE()
						, @CFCUserID						
						)
				SET @RecordID = @@IDENTITY
			END
		END
ELSE
	
	-- 6/9/17 - commented (for Perdue, Task 381) -- taken out because analyzer materials are no longer deleted, just inactivated.
	--BEGIN
	--	IF @IsActive = 0
	--		BEGIN
	--			SET @ErrorNumber = (SELECT 
	--									COUNT(XREF.AnalyzerMaterialID)
	--								FROM
	--									dbo.AIMMaterialRefAnalyzerMaterial XREF WITH(NOLOCK)
	--								WHERE
	--									XREF.AnalyzerMaterialID = @AnalyzerMaterialID)
	--			IF @ErrorNumber > 0
	--				BEGIN
	--					SET @ErrorMessage = 'Unable to delete Material because its being used in the AIM/Analyzer Material Cross Reference table'
	--				END				
	--		END
	--	IF @ErrorNumber = 0

			BEGIN
				UPDATE
					dbo.AnalyzerMaterial WITH(ROWLOCK)
						SET 
							  DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
							, AnalyzerMaterialCode = ISNULL(@AnalyzerMaterialCode, AnalyzerMaterialCode)
							, AnalyzerMaterialName = ISNULL(@AnalyzerMaterialName, AnalyzerMaterialName)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID							
						WHERE 
							AnalyzerMaterialID = @AnalyzerMaterialID
				SET @RecordID = @AnalyzerMaterialID
			END
	--END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
