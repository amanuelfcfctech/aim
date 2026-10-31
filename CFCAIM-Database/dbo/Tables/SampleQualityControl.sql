CREATE TABLE [dbo].[SampleQualityControl] (
    [SampleQualityControlID] INT             IDENTITY (1, 1) NOT NULL,
    [SampleID]               INT             NULL,
    [AIMNutrientID]          INT             NULL,
    [SampleWeightBegin]      NUMERIC (18, 8) NULL,
    [SampleWeightEnd]        NUMERIC (18, 8) NULL,
    [CFCUserID]              INT             NULL,
    [UpdateStamp]            DATETIME        CONSTRAINT [DF_SampleQualityControl_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_SampleQualityControl] PRIMARY KEY CLUSTERED ([SampleQualityControlID] ASC)
);




GO
CREATE NONCLUSTERED INDEX [SampleQualityControl_SampleID]
    ON [dbo].[SampleQualityControl]([SampleID] ASC);

