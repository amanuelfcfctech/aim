CREATE TABLE [dbo].[ImportFileType] (
    [ImportFileTypeID] INT           NOT NULL,
    [Name]             NVARCHAR (10) NULL,
    [IsActive]         BIT           NULL,
    [UpdateStamp]      DATETIME      CONSTRAINT [DF_ImportFileType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]        INT           CONSTRAINT [DF_ImportFileType_CFCUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_ImportFileType] PRIMARY KEY CLUSTERED ([ImportFileTypeID] ASC)
);

