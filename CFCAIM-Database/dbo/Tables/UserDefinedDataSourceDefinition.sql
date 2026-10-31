CREATE TABLE [dbo].[UserDefinedDataSourceDefinition] (
    [UserDefinedDataSourceDefinitionID] INT            IDENTITY (1, 1) NOT NULL,
    [ImportFileName]                    NVARCHAR (50)  NULL,
    [DataSourceTypeID]                  INT            NULL,
    [FilePath]                          NVARCHAR (100) NULL,
    [FileHasHeader]                     BIT            NULL,
    [IsSpectraFile]                     BIT            NULL,
    [IsFurtherAnalyses]                 BIT            NULL,
    [AIMMaterialID]                     INT            NULL,
    [AIMSourceID]                       INT            NULL,
    [AIMSourceLocationID]               INT            NULL,
    [AIMOwnerID]                        INT            NULL,
    [AIMOwnerLocationID]                INT            NULL,
    [UpdateStamp]                       DATETIME       NULL,
    [CFCUserID]                         INT            NULL,
    [AIMMaterialMap]                    INT            NULL,
    [AIMSourceMap]                      INT            NULL,
    [AIMSourceLocationMap]              INT            NULL,
    [AIMOwnerMap]                       INT            NULL,
    [AIMOwnerLocationMap]               INT            NULL,
    [ProcessDirectory]                  NVARCHAR (250) NULL,
    [BackUpDirectory]                   NVARCHAR (250) NULL,
    [HeaderBeginRow]                    INT            NULL,
    [Orientation]                       BIT            NULL,
    [ImportFileTypeID]                  INT            NULL,
    [DelimiterTypeID]                   INT            NULL,
    [SpectraFileExtension]              NVARCHAR (10)  NULL,
    [SpectraDirectory]                  NVARCHAR (250) NULL,
    [ExcelSheetNumber]                  INT            CONSTRAINT [DF_UserDefinedDataSourceDefinition_ExcelSheetNumber] DEFAULT ((1)) NULL,
    [NutrientDetailBeginColumn]         INT            NULL,
    [CreateSampleOrMergeResults]        BIT            NULL,
    [DataSourceIsMapped]                BIT            CONSTRAINT [DF_UserDefinedDataSourceDefinition_DataSourceIsMapped] DEFAULT ((0)) NULL,
    [DateFormatID]                      INT            NULL,
    [ImportType]                        INT            NULL,
    [IsQualityControl]                  INT            CONSTRAINT [DF_UserDefinedDataSourceDefinition_IsQualityControl] DEFAULT ((0)) NULL,
    [DecimalSeparatorID]                INT            NULL,
    [DetailBeginRow]                    INT            NULL,
    [AllowDuplicateSampleCodes]         BIT            NULL,
    [SystemCreatedSampleCode]           BIT            NULL,
    [GridXMLStream]                     NVARCHAR (MAX) NULL,
    [IsCompositeBatch]                  BIT            NULL,
    CONSTRAINT [PK_UserDefinedDataSourceDefinition] PRIMARY KEY CLUSTERED ([UserDefinedDataSourceDefinitionID] ASC)
);

































