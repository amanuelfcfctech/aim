CREATE TABLE [dbo].[PrinterType] (
    [PrinterTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [PrinterTypeName] NVARCHAR (50) NULL,
    [IsActive]        BIT           CONSTRAINT [DF_PrinterType_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]     DATETIME      CONSTRAINT [DF_PrinterType_CreatedDate] DEFAULT (getdate()) NULL,
    [CFCUserID]       INT           NULL,
    CONSTRAINT [PK_PrinterType] PRIMARY KEY CLUSTERED ([PrinterTypeID] ASC)
);



