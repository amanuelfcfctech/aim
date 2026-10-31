CREATE TABLE [dbo].[CFCProductArchiveNutrient] (
    [CFCProductArchiveNutrientID] INT             IDENTITY (1, 1) NOT NULL,
    [CFCProductArchiveID]         INT             NULL,
    [CFCNutrientID]               INT             NULL,
    [NutrientValue]               NUMERIC (18, 9) NULL,
    [UpdateStamp]                 DATETIME        CONSTRAINT [DF_CFCProductArchiveNutrient_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                   INT             NULL,
    CONSTRAINT [PK_CFCProductArchiveNutrient] PRIMARY KEY CLUSTERED ([CFCProductArchiveNutrientID] ASC)
);




GO
CREATE NONCLUSTERED INDEX [CFCProductArchiveNutrient_CFCProductArchiveIDCFCNutrientID]
    ON [dbo].[CFCProductArchiveNutrient]([CFCProductArchiveID] ASC, [CFCNutrientID] ASC);

