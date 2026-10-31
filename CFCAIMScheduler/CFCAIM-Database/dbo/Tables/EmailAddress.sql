CREATE TABLE [dbo].[EmailAddress] (
    [EmailAddressID] INT           IDENTITY (1, 1) NOT NULL,
    [EmailAddress]   NVARCHAR (50) NULL,
    [EmailIPAddress] NVARCHAR (50) NULL,
    [EmailUserName]  NVARCHAR (25) NULL,
    [EmailPassword]  NVARCHAR (25) NULL,
    [UpdateStamp]    DATETIME      NULL,
    [CFCUserID]      INT           NULL,
    CONSTRAINT [PK_EmailAddress] PRIMARY KEY CLUSTERED ([EmailAddressID] ASC)
);

