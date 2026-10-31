CREATE TABLE [dbo].[AnalyzerOwnerLocation] (
    [AnalyzerOwnerLocationID]   INT           IDENTITY (1, 1) NOT NULL,
    [AnalyzerOwnerID]           INT           NOT NULL,
    [AnalyzerOwnerLocationCode] NVARCHAR (20) NULL,
    [AnalyzerOwnerLocationName] NVARCHAR (30) NULL,
    [DataSourceTypeID]          INT           CONSTRAINT [DF_AnalyzerOwnerLocation_DataSourceTypeID] DEFAULT ((0)) NULL,
    [IsActive]                  BIT           CONSTRAINT [DF_AnalyzerOwnerLocation_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]               DATETIME      CONSTRAINT [DF_AnalyzerOwnerLocation_CreatedDate] DEFAULT (getdate()) NULL,
    [CFCUserID]                 INT           NULL
);

