CREATE TABLE [dbo].[AnalysisUserDefaultSettings] (
    [AnalysisUserDefaultSettingsID] INT          IDENTITY (1, 1) NOT NULL,
    [CFCUserID]                     INT          NULL,
    [UpdateStamp]                   DATETIME     CONSTRAINT [DF_AnalysisUserDefaultSettings_UpdateStamp] DEFAULT (getdate()) NULL,
    [GroupByDate]                   BIT          NULL,
    [GroupBySampleID]               BIT          NULL,
    [GroupByOwner]                  BIT          NULL,
    [GroupByOwnerLocation]          BIT          NULL,
    [GroupBySupplier]               BIT          NULL,
    [GroupBySupplierLocation]       BIT          NULL,
    [AllRecords]                    BIT          NULL,
    [OutliersOnly]                  BIT          NULL,
    [ExtremeOutlierInclude]         BIT          NULL,
    [ExtremeOutlierExclude]         BIT          NULL,
    [MildOutlierInclude]            BIT          NULL,
    [MildOutlierExclude]            BIT          NULL,
    [TargetBase]                    BIT          NULL,
    [TargetDefaultOverlay]          BIT          NULL,
    [TargetSpecificOverlay]         BIT          NULL,
    [TargetSpecificOverlayPlantID]  INT          NULL,
    [TemplateQuery]                 INT          NULL,
    [TemplateView]                  INT          NULL,
    [RecalculateOnModify]           BIT          NULL,
    [IngredientOrProduct]           NVARCHAR (1) NULL,
    CONSTRAINT [PK_AnalysisUserDefaultSettings] PRIMARY KEY CLUSTERED ([AnalysisUserDefaultSettingsID] ASC)
);



