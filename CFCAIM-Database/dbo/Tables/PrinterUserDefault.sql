CREATE TABLE [dbo].[PrinterUserDefault] (
    [PrinterUserDefaultID] INT           IDENTITY (1, 1) NOT NULL,
    [CFCUserID]            INT           NULL,
    [PrinterTypeID]        INT           NULL,
    [PrinterName]          NVARCHAR (50) NULL,
    [UpdateStamp]          DATETIME      NULL,
    [UpdateCFCUserID]      INT           NULL,
    CONSTRAINT [PK_PrinterUserDefault] PRIMARY KEY CLUSTERED ([PrinterUserDefaultID] ASC)
);

