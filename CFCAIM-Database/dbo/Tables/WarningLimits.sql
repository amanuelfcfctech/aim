CREATE TABLE [dbo].[WarningLimits] (
    [WarningLimitsID] INT             IDENTITY (1, 1) NOT NULL,
    [AIMMaterialID]   INT             NULL,
    [AIMNutrientID]   INT             NULL,
    [Min]             NUMERIC (18, 8) NULL,
    [Max]             NUMERIC (18, 8) NULL,
    [UpdateStamp]     DATETIME        CONSTRAINT [DF_AIMWarningLimits_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]       INT             NULL,
    CONSTRAINT [PK_AIMWarningLimits] PRIMARY KEY CLUSTERED ([WarningLimitsID] ASC)
);




GO
CREATE NONCLUSTERED INDEX [WarningLimits_AIMNutrientID]
    ON [dbo].[WarningLimits]([AIMNutrientID] ASC);


GO
CREATE NONCLUSTERED INDEX [WarningLimits_AIMMaterialID]
    ON [dbo].[WarningLimits]([AIMMaterialID] ASC);

