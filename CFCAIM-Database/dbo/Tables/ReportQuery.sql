CREATE TABLE [dbo].[ReportQuery] (
    [ReportQueryID]   INT        IDENTITY (1, 1) NOT NULL,
    [ReportQueryCode] NCHAR (20) NULL,
    [ReportQueryName] NCHAR (30) NULL,
    [CFCUserID]       INT        NULL,
    [UpdateStamp]     DATETIME   CONSTRAINT [DF_ReportQuery_UpdateStamp] DEFAULT (getdate()) NULL,
    [IsActive]        BIT        CONSTRAINT [DF_ReportQuery_IsActive] DEFAULT ((1)) NULL,
    [ReportID]        INT        CONSTRAINT [DF_ReportQuery_ReportID] DEFAULT ((1)) NULL
);

