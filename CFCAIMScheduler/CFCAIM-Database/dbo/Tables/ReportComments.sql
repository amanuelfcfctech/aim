CREATE TABLE [dbo].[ReportComments] (
    [ReportCommentsID] INT            IDENTITY (1, 1) NOT NULL,
    [ReportID]         INT            CONSTRAINT [DF_ReportComments_ReportID] DEFAULT ((1)) NULL,
    [ReportComment1]   NVARCHAR (200) NULL,
    [ReportComment2]   NVARCHAR (200) NULL,
    [ReportComment3]   NVARCHAR (200) NULL,
    [ReportComment4]   NVARCHAR (200) NULL,
    [ReportComment5]   NVARCHAR (200) NULL,
    [CFCUserID]        INT            NULL,
    [UpdateStamp]      DATETIME       CONSTRAINT [DF_ReportComments_UpdateStamp] DEFAULT (getdate()) NULL
);

