CREATE TABLE [dbo].[AIMMessageUser] (
    [AIMMessageUserID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMMessageID]     INT      NULL,
    [UpdateStamp]      DATETIME CONSTRAINT [DF_AIMMessageUser_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]        INT      NULL,
    CONSTRAINT [PK_AIMMessageUser] PRIMARY KEY CLUSTERED ([AIMMessageUserID] ASC)
);

