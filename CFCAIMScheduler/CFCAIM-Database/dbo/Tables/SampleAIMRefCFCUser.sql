CREATE TABLE [dbo].[SampleAIMRefCFCUser] (
    [SampleAIMRefCFCUserID] INT      IDENTITY (1, 1) NOT NULL,
    [SampleAIMID]           INT      NULL,
    [CFCUserID]             INT      NULL,
    [IsUserFlag]            BIT      CONSTRAINT [DF_SampleAIMRefCFCUser_IsUserFlag] DEFAULT ((0)) NULL,
    [UpdateStamp]           DATETIME NULL,
    CONSTRAINT [PK_SampleAIMRefCFCUser] PRIMARY KEY CLUSTERED ([SampleAIMRefCFCUserID] ASC)
);




GO
CREATE NONCLUSTERED INDEX [SampleAIMRefCFCUser_SampleAIMID]
    ON [dbo].[SampleAIMRefCFCUser]([SampleAIMID] ASC);

