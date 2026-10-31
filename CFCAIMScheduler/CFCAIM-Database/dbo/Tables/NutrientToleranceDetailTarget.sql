CREATE TABLE [dbo].[NutrientToleranceDetailTarget] (
    [NutrientToleranceDetailTargetID] INT             IDENTITY (1, 1) NOT NULL,
    [NutrientToleranceHeaderID]       INT             NULL,
    [AIMNutrientID]                   INT             NULL,
    [MinAmount]                       NUMERIC (18, 9) NULL,
    [MinPercent]                      NUMERIC (18, 9) NULL,
    [MaxAmount]                       NUMERIC (18, 9) NULL,
    [MaxPercent]                      NUMERIC (18, 9) NULL,
    [UpdateStamp]                     DATETIME        NULL,
    [CFCUserID]                       INT             NULL,
    CONSTRAINT [PK_NutrientToleranceDetailTarget] PRIMARY KEY CLUSTERED ([NutrientToleranceDetailTargetID] ASC)
);

