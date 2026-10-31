CREATE TABLE [dbo].[PrinterDefault] (
    [PrinterDefaultID] INT           IDENTITY (1, 1) NOT NULL,
    [PrinterTypeID]    INT           NULL,
    [PrinterName]      NVARCHAR (50) NULL,
    [UpdateStamp]      DATETIME      NULL,
    [CFCUserID]        INT           NULL,
    CONSTRAINT [PK_PrinterDefault] PRIMARY KEY CLUSTERED ([PrinterDefaultID] ASC)
);

