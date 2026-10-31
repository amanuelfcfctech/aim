CREATE TABLE [dbo].[AnalyzerAnalyse] (
    [AnalyzerAnalyseID] INT            IDENTITY (1, 1) NOT NULL,
    [DataSourceTypeID]  INT            CONSTRAINT [DF_AnalyzerAnalyse_DataSourceTypeID] DEFAULT ((0)) NULL,
    [Code]              NVARCHAR (10)  NULL,
    [Name]              NVARCHAR (30)  NULL,
    [Description]       NVARCHAR (100) NULL,
    [UpdateStamp]       DATETIME       NULL,
    [CFCUserID]         INT            NULL,
    [IsActive]          BIT            CONSTRAINT [DF_AnalyzerAnalyse_IsActive] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_AnalyzerAnalyse] PRIMARY KEY CLUSTERED ([AnalyzerAnalyseID] ASC)
);







