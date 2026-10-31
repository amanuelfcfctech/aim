CREATE TABLE [dbo].[CFCProductArchiveSpec] (
    [CFCProductArchiveSpecID] INT             IDENTITY (1, 1) NOT NULL,
    [CFCProductArchiveID]     INT             NULL,
    [CFCNutrientID]           INT             NULL,
    [NutrientMin]             NUMERIC (18, 9) NULL,
    [NutrientMax]             NUMERIC (18, 9) NULL,
    [UpdateStamp]             DATETIME        CONSTRAINT [DF_CFCProductArchiveSpec_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]               INT             NULL,
    CONSTRAINT [PK_CFCProductArchiveSpec] PRIMARY KEY CLUSTERED ([CFCProductArchiveSpecID] ASC)
);






GO
CREATE NONCLUSTERED INDEX [CFCProductArchiveSpec_CFCProductArchiveIDCFCNutrientID]
    ON [dbo].[CFCProductArchiveSpec]([CFCProductArchiveID] ASC, [CFCNutrientID] ASC);


GO
CREATE NONCLUSTERED INDEX [CFCProductArchiveSpec_CFCProductArchiveID]
    ON [dbo].[CFCProductArchiveSpec]([CFCProductArchiveID] ASC);

