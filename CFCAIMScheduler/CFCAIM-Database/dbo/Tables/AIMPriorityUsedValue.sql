CREATE TABLE [dbo].[AIMPriorityUsedValue] (
    [AIMPriorityUsedValueID] INT           IDENTITY (1, 1) NOT NULL,
    [Description]            NVARCHAR (25) NULL,
    [UpdateStamp]            DATETIME      CONSTRAINT [DF_AIMPriorityUsedValue_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]              INT           NULL,
    CONSTRAINT [PK_AIMPriorityUsedValue] PRIMARY KEY CLUSTERED ([AIMPriorityUsedValueID] ASC)
);

