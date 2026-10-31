CREATE TABLE [dbo].[ExcelFileType] (
    [ExcelFileTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (10) NULL,
    [IsActive]        BIT           NULL,
    [UpdateStamp]     DATETIME      CONSTRAINT [DF_ExcelFileType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]       INT           CONSTRAINT [DF_ExcelFileType_CFCUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_ExcelFileType] PRIMARY KEY CLUSTERED ([ExcelFileTypeID] ASC)
);

