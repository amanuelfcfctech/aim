CREATE TABLE [dbo].[DataSourceHeader] (
    [DataSourceHeaderID] INT      IDENTITY (1, 1) NOT NULL,
    [DataSourceTypeID]   INT      NOT NULL,
    [UpdateStamp]        DATETIME NULL,
    [CFCUserID]          INT      NULL,
    CONSTRAINT [PK_DataSourceHeader] PRIMARY KEY CLUSTERED ([DataSourceHeaderID] ASC, [DataSourceTypeID] ASC)
);

