CREATE TABLE [dbo].[LogTest] (
    [LogTestID]   INT          IDENTITY (1, 1) NOT NULL,
    [Sampleid]    INT          NULL,
    [Field1]      VARCHAR (50) NULL,
    [UpdateStamp] DATETIME     CONSTRAINT [DF_LogTest_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_LogTest] PRIMARY KEY CLUSTERED ([LogTestID] ASC)
);

