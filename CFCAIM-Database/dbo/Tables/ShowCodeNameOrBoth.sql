CREATE TABLE [dbo].[ShowCodeNameOrBoth] (
    [ShowCodeNameOrBothID] INT           IDENTITY (1, 1) NOT NULL,
    [Option]               NVARCHAR (25) NULL,
    [UpdateStamp]          DATETIME      CONSTRAINT [DF_ShowCodeNameOrBoth_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]            INT           NULL,
    CONSTRAINT [PK_ShowCodeNameOrBoth] PRIMARY KEY CLUSTERED ([ShowCodeNameOrBothID] ASC)
);

