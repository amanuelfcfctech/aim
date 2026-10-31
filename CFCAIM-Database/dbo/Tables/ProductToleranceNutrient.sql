CREATE TABLE [dbo].[ProductToleranceNutrient] (
    [ProductToleranceNutrientID] INT             IDENTITY (1, 1) NOT NULL,
    [ProductToleranceHeaderID]   INT             NULL,
    [AIMNutrientID]              INT             NULL,
    [NutrientValue]              NUMERIC (18, 9) NULL,
    [NutrientMin]                NUMERIC (18, 9) NULL,
    [NutrientMax]                NUMERIC (18, 9) NULL,
    [MinAmount]                  NUMERIC (18, 9) NULL,
    [MinPercent]                 NUMERIC (18, 9) NULL,
    [MaxAmount]                  NUMERIC (18, 9) NULL,
    [MaxPercent]                 NUMERIC (18, 9) NULL,
    [MinPlusMinus]               INT             NULL,
    [MaxPlusMinus]               INT             NULL,
    [UpdateStamp]                DATETIME        NULL,
    [CFCUserID]                  INT             NULL,
    CONSTRAINT [PK_ProductToleranceNutrient] PRIMARY KEY CLUSTERED ([ProductToleranceNutrientID] ASC)
);

