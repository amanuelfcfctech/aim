CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceDefinition_SAV]
			  @UserDefinedDataSourceDefinitionID	INT			

			--, @ImportFileName				NVARCHAR(30) = NULL			--4/14/21
			, @ImportFileName				NVARCHAR(50) = NULL			--4/14/21

			, @DataSourceTypeID				INT = NULL			
			, @FilePath						NVARCHAR(100) = NULL
			, @FileHasHeader				BIT = NULL
			, @IsSpectraFile				BIT = NULL
			--, @IsFurtherAnalyses			BIT = NULL
			, @AIMMaterialID				INT	= NULL
			, @AIMSourceID					INT	= NULL
			, @AIMSourceLocationID			INT	= NULL
			, @AIMOwnerID					INT	= NULL
			, @AIMOwnerLocationID			INT	= NULL			
			, @CFCUserID					INT	= NULL
			, @AIMMaterialMap				INT	= NULL
			, @AIMSourceMap					INT	= NULL
			, @AIMSourceLocationMap			INT	= NULL
			, @AIMOwnerMap					INT	= NULL
			, @AIMOwnerLocationMap			INT	= NULL		
			, @ProcessDirectory				NVARCHAR(250) = NULL
			, @BackUpDirectory				NVARCHAR(250) = NULL						
			, @HeaderBeginRow				INT = NULL
			, @Orientation					BIT = NULL
			, @ImportFileTypeID				INT = NULL
			, @DelimiterTypeID				INT = NULL
			, @SpectraFileExtension			NVARCHAR(10) = NULL
			, @SpectraDirectory				NVARCHAR(250) = NULL	
			, @ExcelSheetNumber				INT = NULL
			, @NutrientDetailBeginColumn	INT = NULL
			, @CreateSampleOrMergeResults	BIT = NULL
			, @DataSourceIsMapped			BIT = NULL
			, @DateFormatID					INT = NULL
			, @ImportType					INT = NULL
			, @IsQualityControl				BIT = NULL

			--4/1/19
			, @DecimalSeparatorID			INT = NULL

			--8/23/19
			, @DetailBeginRow				INT = NULL

			--12/2/19
			, @AllowDuplicateSampleCodes	BIT = NULL
			, @SystemCreatedSampleCode		BIT = NULL

			, @GridXMLStream				NVARCHAR(MAX) = NULL

			--8/12/22
			, @IsCompositeBatch				BIT = NULL


--exec usp_UserDefinedDataSourceDefinition_SAV 20, 'test1', 2, false, false, false, 0, 0, 0, 0, 2, 1
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @UserDefinedDataSourceDefinitionID = 0
	BEGIN
		INSERT
				
			dbo.UserDefinedDataSourceDefinition
				(
				ImportFileName		
				, DataSourceTypeID		
				, FilePath					
				, FileHasHeader							
				, IsSpectraFile				
				--, IsFurtherAnalyses				
				, AIMMaterialID				
				, AIMSourceID					
				, AIMSourceLocationID			
				, AIMOwnerID					
				, AIMOwnerLocationID										
				, CFCUserID					
				, UpdateStamp	
				, AIMMaterialMap				
				, AIMSourceMap					
				, AIMSourceLocationMap			
				, AIMOwnerMap					
				, AIMOwnerLocationMap
				, ProcessDirectory
				, BackUpDirectory					
				, HeaderBeginRow	
				, Orientation	
				, ImportFileTypeID
				, DelimiterTypeID
				, SpectraFileExtension
				, SpectraDirectory
				, ExcelSheetNumber
				, NutrientDetailBeginColumn
				, CreateSampleOrMergeResults
				, DataSourceIsMapped
				, DateFormatID
				, ImportType
				, IsQualityControl

				--4/1/19
				, DecimalSeparatorID

				--8/2/19
				, DetailBeginRow

				--12/2/19
				, AllowDuplicateSampleCodes
				, SystemCreatedSampleCode

				--1/6/20
				, GridXMLStream

				--8/12/22
				, IsCompositeBatch

				)
		VALUES
				(
				@ImportFileName	
				, @DataSourceTypeID	
				, @FilePath						
				, @FileHasHeader							
				, @IsSpectraFile				
				--, @IsFurtherAnalyses				
				, @AIMMaterialID				
				, @AIMSourceID					
				, @AIMSourceLocationID			
				, @AIMOwnerID					
				, @AIMOwnerLocationID										
				, @CFCUserID	
				, GETDATE()
				, @AIMMaterialMap				
				, @AIMSourceMap					
				, @AIMSourceLocationMap			
				, @AIMOwnerMap					
				, @AIMOwnerLocationMap
				, @ProcessDirectory
				, @BackUpDirectory					
				, @HeaderBeginRow
				, @Orientation
				, @ImportFileTypeID
				, @DelimiterTypeID
				, @SpectraFileExtension
				, @SpectraDirectory
				, @ExcelSheetNumber
				, @NutrientDetailBeginColumn
				, @CreateSampleOrMergeResults
				, @DataSourceIsMapped
				, @DateFormatID
				, @ImportType
				, @IsQualityControl

				--4/1/19
				, @DecimalSeparatorID

				--8/23/19
				, @DetailBeginRow

				--12/2/19
				, @AllowDuplicateSampleCodes
				, @SystemCreatedSampleCode

				--1/6/20
				, @GridXMLStream

				--8/12/22
				, @IsCompositeBatch

				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.UserDefinedDataSourceDefinition WITH(ROWLOCK)
				SET 
					ImportFileName	 = ISNULL(@ImportFileName, ImportFileName)	
					, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)									
					, FilePath = ISNULL(@FilePath, FilePath)									
					, FileHasHeader = ISNULL(@FileHasHeader, FileHasHeader)
					, IsSpectraFile	 = ISNULL(@IsSpectraFile, IsSpectraFile)
					--, IsFurtherAnalyses	 = ISNULL(@IsFurtherAnalyses, IsFurtherAnalyses)
					, AIMMaterialID	 = ISNULL(@AIMMaterialID, AIMMaterialID)
					, AIMSourceID	 = ISNULL(@AIMSourceID, AIMSourceID)
					, AIMSourceLocationID	 = ISNULL(@AIMSourceLocationID, AIMSourceLocationID)
					, AIMOwnerID	 = ISNULL(@AIMOwnerID, AIMOwnerID)
					, AIMOwnerLocationID	 = ISNULL(@AIMOwnerLocationID, AIMOwnerLocationID)															
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()
					, AIMMaterialMap	 = ISNULL(@AIMMaterialMap, AIMMaterialMap)
					, AIMSourceMap	 = ISNULL(@AIMSourceMap, AIMSourceMap)
					, AIMSourceLocationMap	 = ISNULL(@AIMSourceLocationMap, AIMSourceLocationMap)
					, AIMOwnerMap	 = ISNULL(@AIMOwnerMap, AIMOwnerMap)
					, AIMOwnerLocationMap	 = ISNULL(@AIMOwnerLocationMap, AIMOwnerLocationMap)	
					, ProcessDirectory	 = ISNULL(@ProcessDirectory, ProcessDirectory)		
					, BackUpDirectory	 = ISNULL(@BackUpDirectory, BackUpDirectory)						
					, HeaderBeginRow = ISNULL(@HeaderBeginRow, HeaderBeginRow)		
					, Orientation = ISNULL(@Orientation, Orientation)
					, ImportFileTypeID = ISNULL(@ImportFileTypeID, ImportFileTypeID)
					, DelimiterTypeID = ISNULL(@DelimiterTypeID, DelimiterTypeID)
					, SpectraFileExtension	 = ISNULL(@SpectraFileExtension, SpectraFileExtension)	
					, SpectraDirectory	 = ISNULL(@SpectraDirectory, SpectraDirectory)	
					, ExcelSheetNumber	 = ISNULL(@ExcelSheetNumber, ExcelSheetNumber)	
					, NutrientDetailBeginColumn = ISNULL(@NutrientDetailBeginColumn, NutrientDetailBeginColumn)	
					, CreateSampleOrMergeResults = ISNULL(@CreateSampleOrMergeResults, CreateSampleOrMergeResults)	
					, DataSourceIsMapped = ISNULL(@DataSourceIsMapped, DataSourceIsMapped)	
					, DateFormatID = ISNULL(@DateFormatID, DateFormatID)
					, ImportType = ISNULL(@ImportType, ImportType)
					, IsQualityControl = ISNULL(@IsQualityControl, IsQualityControl)

					--4/1/19
					, DecimalSeparatorID = ISNULL(@DecimalSeparatorID, DecimalSeparatorID)

					--8/23/19
					, DetailBeginRow = ISNULL(@DetailBeginRow, DetailBeginRow)

					--12/2/19
					, AllowDuplicateSampleCodes = ISNULL(@AllowDuplicateSampleCodes, AllowDuplicateSampleCodes)
					, SystemCreatedSampleCode = ISNULL(@SystemCreatedSampleCode, SystemCreatedSampleCode)
					
					--1/6/20
					, GridXMLStream = ISNULL(@GridXMLStream, GridXMLStream)

					--8/12/22
					, IsCompositeBatch = ISNULL(@IsCompositeBatch, IsCompositeBatch)
					
					
				WHERE 
					UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID

			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END