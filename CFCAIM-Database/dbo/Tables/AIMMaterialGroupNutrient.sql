CREATE TABLE [dbo].[AIMMaterialGroupNutrient] (
    [AIMMaterialGroupNutrientID] INT             IDENTITY (1, 1) NOT NULL,
    [AIMMaterialGroupHeaderID]   INT             NULL,
    [AIMNutrientID]              INT             NULL,
    [NutrientValue]              NUMERIC (18, 9) NULL,
    [NutrientMin]                NUMERIC (18, 9) NULL,
    [NutrientMinPlusMinus]       INT             NULL,
    [NutrientMinAmount]          NUMERIC (18, 9) NULL,
    [NutrientMinPercent]         NUMERIC (18, 9) NULL,
    [NutrientMax]                NUMERIC (18, 9) NULL,
    [NutrientMaxPlusMinus]       INT             NULL,
    [NutrientMaxAmount]          NUMERIC (18, 9) NULL,
    [NutrientMaxPercent]         NUMERIC (18, 9) NULL,
    [UpdateStamp]                DATETIME        NULL,
    [CFCUserID]                  INT             NULL,
    CONSTRAINT [PK_AIMMaterialGroupNutrient] PRIMARY KEY CLUSTERED ([AIMMaterialGroupNutrientID] ASC)
);

