CREATE TABLE [dbo].[FileTypeRefDelimiterType] (
    [FileTypeRefDelimiterTypeID] INT      IDENTITY (1, 1) NOT NULL,
    [FileTypeID]                 INT      NULL,
    [DelimiterTypeID]            INT      NULL,
    [UpdateStamp]                DATETIME CONSTRAINT [DF_FileTypeRefDelimiterType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                  INT      CONSTRAINT [DF_FileTypeRefDelimiterType_CFCUserID] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_FileTypeRefDelimiterType] PRIMARY KEY CLUSTERED ([FileTypeRefDelimiterTypeID] ASC)
);

