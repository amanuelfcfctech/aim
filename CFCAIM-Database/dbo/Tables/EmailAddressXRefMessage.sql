CREATE TABLE [dbo].[EmailAddressXRefMessage] (
    [EmailAddressXRefMessageID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCUserID]                 INT      NULL,
    [EmailAddressID]            INT      NULL,
    [MessageID]                 INT      NULL,
    [UpdateStamp]               DATETIME NULL,
    [UpdateCFCUserID]           INT      NULL,
    CONSTRAINT [PK_EmailAddressXRefMessage] PRIMARY KEY CLUSTERED ([EmailAddressXRefMessageID] ASC)
);

