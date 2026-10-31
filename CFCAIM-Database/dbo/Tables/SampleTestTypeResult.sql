CREATE TABLE [dbo].[SampleTestTypeResult] (
    [SampleTestTypeResultID] INT      IDENTITY (1, 1) NOT NULL,
    [SampleID]               INT      NULL,
    [SampleTestTypeDetailID] INT      NULL,
    [IsIncluded]             BIT      NULL,
    [IsActive]               BIT      NULL,
    [UpdateStamp]            DATETIME CONSTRAINT [DF_SampleTestTypeResult_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]              INT      NULL,
    CONSTRAINT [PK_SampleTestTypeResult] PRIMARY KEY CLUSTERED ([SampleTestTypeResultID] ASC)
);

