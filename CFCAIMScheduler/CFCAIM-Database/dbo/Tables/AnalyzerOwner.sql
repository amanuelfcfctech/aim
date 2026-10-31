CREATE TABLE [dbo].[AnalyzerOwner] (
    [AnalyzerOwnerID]   INT           IDENTITY (1, 1) NOT NULL,
    [AnalyzerOwnerCode] NVARCHAR (20) NULL,
    [AnalyzerOwnerName] NVARCHAR (50) NULL,
    [DataSourceTypeID]  INT           CONSTRAINT [DF_AnalyzerOwner_DataSourceTypeID] DEFAULT ((0)) NULL,
    [IsActive]          BIT           CONSTRAINT [DF_AnalyzerOwner_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]       DATETIME      CONSTRAINT [DF_AnalyzerOwner_CreatedDate] DEFAULT (getdate()) NULL,
    [CFCUserID]         INT           NULL
);

