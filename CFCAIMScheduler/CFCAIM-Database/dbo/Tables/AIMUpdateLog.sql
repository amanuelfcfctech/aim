CREATE TABLE [dbo].[AIMUpdateLog] (
    [AIMUpdateLogID] INT            IDENTITY (1, 1) NOT NULL,
    [BatchID]        INT            NULL,
    [SqlCommand]     VARCHAR (MAX)  NULL,
    [CommandType]    INT            NULL,
    [Message]        NVARCHAR (200) NULL,
    [ErrorNumber]    INT            NULL,
    [UpdateStamp]    DATETIME       NULL,
    [CFCUserID]      INT            NULL
);

