CREATE TABLE [dbo].[SampleAIMRemoval] (
    [SampleAIMRemovalID] INT IDENTITY (1, 1) NOT NULL,
    [SampleAIMID]        INT NULL,
    [IsSystemRemoval]    BIT CONSTRAINT [DF_SampleAIMRemoval_IsSystemRemoval] DEFAULT ((1)) NULL,
    [CFCUserID]          INT CONSTRAINT [DF_SampleAIMRemoval_CFCUserID] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_SampleAIMRemoval] PRIMARY KEY CLUSTERED ([SampleAIMRemovalID] ASC)
);




GO
CREATE NONCLUSTERED INDEX [SampleAIMRemoval_SampleAIMID]
    ON [dbo].[SampleAIMRemoval]([SampleAIMID] ASC);

