CREATE TABLE [dbo].[AIMMessage] (
    [AIMMessageID] INT            IDENTITY (1, 1) NOT NULL,
    [TypeID]       INT            NULL,
    [Message]      NVARCHAR (250) NULL,
    [UpdateStamp]  DATETIME       CONSTRAINT [DF_AIMMessage_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]    INT            NULL,
    CONSTRAINT [PK_AIMMessage] PRIMARY KEY CLUSTERED ([AIMMessageID] ASC)
);

