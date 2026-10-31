CREATE TABLE [dbo].[SampleTestTypeResults] (
    [SampleTestTypeResultsID] INT      IDENTITY (1, 1) NOT NULL,
    [SampleID]                INT      NULL,
    [SampleTestTypeDetailID]  INT      NULL,
    [IsIncluded]              BIT      NULL,
    [IsActive]                BIT      NULL,
    [UpdateStamp]             DATETIME CONSTRAINT [DF_SampleTestTypeResults_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]               INT      NULL,
    CONSTRAINT [PK_SampleTestTypeResults] PRIMARY KEY CLUSTERED ([SampleTestTypeResultsID] ASC)
);

