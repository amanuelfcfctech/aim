CREATE TABLE [dbo].[SystemSettings] (
    [SystemSettingsID]                 INT      IDENTITY (1, 1) NOT NULL,
    [AIMNutrientID]                    INT      NULL,
    [CFCUserID]                        INT      NULL,
    [UpdateStamp]                      DATETIME NULL,
    [AllowAIMInactive]                 BIT      NULL,
    [ShowAnalyzerMaterials]            BIT      NULL,
    [SampleAimNeedsRebuild]            BIT      CONSTRAINT [DF_SystemSettings_SampleAimNeedsRebuild] DEFAULT ((0)) NULL,
    [SampleNumberOfDays]               INT      CONSTRAINT [DF_SystemSettings_SampleNumberOfDays] DEFAULT ((60)) NULL,
    [ShowNameFirstInDropdown]          BIT      CONSTRAINT [DF_SystemSettings_ShowNameFirstInDropdown] DEFAULT ((0)) NULL,
    [AutoGenerateLotNumber]            BIT      CONSTRAINT [DF_AutoGenerateLotNumber] DEFAULT ((0)) NULL,
    [NextLotNumber]                    INT      NULL,
    [LabelSize]                        INT      NULL,
    [FurtherAnalysisDefaultID]         INT      NULL,
    [GuaranteeReportDecimals]          INT      NULL,
    [GroundUnGroundDefaultID]          INT      NULL,
    [DryMatterAIMNutrientID]           INT      NULL,
    [AnalysisFilteredDataDefault]      BIT      NULL,
    [AutoFillSampleTestTypeIncludeBox] BIT      NULL,
    CONSTRAINT [PK_SystemSettings] PRIMARY KEY CLUSTERED ([SystemSettingsID] ASC)
);





























