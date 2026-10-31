CREATE TABLE [dbo].[DataSourceTypeRefFileType] (
    [DataSourceTypeRefFileTypeID] INT      IDENTITY (1, 1) NOT NULL,
    [DataSourceTypeID]            INT      NULL,
    [FileTypeID]                  INT      NULL,
    [UpdateStamp]                 DATETIME CONSTRAINT [DF_DataSourceTypeRefFileType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                   INT      CONSTRAINT [DF_DataSourceTypeRefFileType_CFCUserID] DEFAULT ((1)) NULL,
    [IsSystem]                    BIT      NULL,
    CONSTRAINT [PK_DataSourceTypeRefFileType] PRIMARY KEY CLUSTERED ([DataSourceTypeRefFileTypeID] ASC)
);



