CREATE TABLE [dbo].[CFCUserLoginLog] (
    [CFCUserLoginLog]    INT            IDENTITY (1, 1) NOT NULL,
    [CFCUserID]          INT            NULL,
    [DatabaseConnection] NVARCHAR (100) NULL,
    [LoginDate]          DATETIME       NULL
);

