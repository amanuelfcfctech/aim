CREATE TABLE [dbo].[WebService] (
    [WebServiceID]     INT           IDENTITY (1, 1) NOT NULL,
    [WebAddress]       NVARCHAR (50) NULL,
    [Token]            NVARCHAR (50) NULL,
    [UpdateStamp]      DATETIME      NULL,
    [CFCUserID]        INT           NULL,
    [DataSourceTypeID] INT           CONSTRAINT [DF_WebService_DataSourceTypeID] DEFAULT ((0)) NULL,
    [Name]             NVARCHAR (50) NULL,
    CONSTRAINT [PK_WebService] PRIMARY KEY CLUSTERED ([WebServiceID] ASC)
);









