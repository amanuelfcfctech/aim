CREATE TABLE [dbo].[NutrientToleranceHeader] (
    [NutrientToleranceHeaderID]   INT           IDENTITY (1, 1) NOT NULL,
    [NutrientToleranceHeaderName] NVARCHAR (50) NULL,
    [UpdateStamp]                 DATETIME      NULL,
    [CFCUserID]                   INT           NULL,
    CONSTRAINT [PK_NutrientToleranceHeader] PRIMARY KEY CLUSTERED ([NutrientToleranceHeaderID] ASC)
);

