CREATE TABLE [dbo].[FileType] (
    [FileTypeID]  INT           NOT NULL,
    [Name]        NVARCHAR (10) NULL,
    [IsActive]    BIT           NULL,
    [UpdateStamp] DATETIME      CONSTRAINT [DF_FileType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]   INT           CONSTRAINT [DF_FileType_CFCUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_FileType] PRIMARY KEY CLUSTERED ([FileTypeID] ASC)
);

