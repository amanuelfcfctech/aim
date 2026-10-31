CREATE TABLE [dbo].[APISprectrumLog] (
    [APISprectrumLogID] INT      IDENTITY (1, 1) NOT NULL,
    [SampleID]          INT      NULL,
    [APISpectrumID]     INT      NULL,
    [APINIRType]        INT      NULL,
    [CreateStamp]       DATETIME CONSTRAINT [DF_APISprectrumLog_CreateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_APISprectrumLogID] PRIMARY KEY CLUSTERED ([APISprectrumLogID] ASC)
);

