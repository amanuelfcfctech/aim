CREATE TABLE [dbo].[Report] (
    [ReportID]            INT           NOT NULL,
    [ReportName]          NVARCHAR (50) NULL,
    [IsActive]            BIT           NULL,
    [UpdateStamp]         DATETIME      CONSTRAINT [DF_Report_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]           INT           NULL,
    [IsAnalysis]          BIT           CONSTRAINT [DF_Report_IsAnalysis] DEFAULT ((0)) NULL,
    [TemplateType]        INT           CONSTRAINT [DF_Report_ReportDisplayType] DEFAULT ((0)) NULL,
    [ReportListSortOrder] INT           NULL,
    [IsAdvanced]          BIT           CONSTRAINT [DF_Report_IsAdvanced] DEFAULT ((0)) NULL,
    [DataSourceLimit]     INT           NULL,
    [HasComments]         BIT           NULL,
    CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED ([ReportID] ASC)
);







