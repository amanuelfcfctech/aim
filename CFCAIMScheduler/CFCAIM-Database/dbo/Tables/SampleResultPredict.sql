CREATE TABLE [dbo].[SampleResultPredict] (
    [SampleResultPredictID] INT              IDENTITY (1, 1) NOT NULL,
    [SampleResultID]        INT              NULL,
    [IsInPredictionRange]   BIT              NULL,
    [GlobalH]               NUMERIC (18, 12) NULL,
    [Limit]                 NUMERIC (18, 12) NULL,
    [IsInMaterialRange]     BIT              NULL,
    [MaxMaterialRange]      NUMERIC (18, 12) NULL,
    [MinMaterialRange]      NUMERIC (18, 12) NULL,
    CONSTRAINT [PK_SampleResultPredict] PRIMARY KEY CLUSTERED ([SampleResultPredictID] ASC)
);




GO
CREATE NONCLUSTERED INDEX [SampleResultPredict_SampleResultID]
    ON [dbo].[SampleResultPredict]([SampleResultID] ASC);

