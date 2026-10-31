CREATE TABLE [dbo].[APILog] (
    [APILogID]         INT      IDENTITY (1, 1) NOT NULL,
    [SampleID]         INT      NULL,
    [APISampleID]      INT      NULL,
    [DataSourceTypeID] INT      NULL,
    [CreateStamp]      DATETIME CONSTRAINT [DF_APILog_CreateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_APILog] PRIMARY KEY CLUSTERED ([APILogID] ASC)
);

